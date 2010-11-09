using System;
using System.IO;
using Multimedia;

namespace AutoMusic
{
    public class Player
    {
        public delegate void PlayDelegate(object sender, EventArgs e);
        public delegate void PauseDelegate(object sender, PauseEventArgs e);
        public delegate void StopDelegate(object sender, StopEventArgs e);
        public delegate void SeekDelegate(object sender, SeekEventArgs e);
        public delegate void FadeDelegate(object sender, EventArgs e);
        public delegate void NextDelegate(object sender, EventArgs e);
        public delegate void ErrorDelegate(object sender, ErrorEventArgs e);
        public delegate void VolumeChangeDelegate(object sender, VolumeChangeEventArgs e);
        public delegate void PanChangeDelegate(object sender, PanChangeEventArgs e);

        /// <summary>
        /// Occurs when playback is started
        /// </summary>
        public event PlayDelegate PlayStarted = delegate { };
        /// <summary>
        /// Occurs when playback is paused
        /// </summary>
        public event PauseDelegate PlayPaused = delegate { };
        /// <summary>
        /// Occurs when playback is stopped
        /// </summary>
        public event StopDelegate PlayStopped = delegate { };
        /// <summary>
        /// Occurs when a seek is completed
        /// </summary>
        public event SeekDelegate Seeking = delegate { };
        /// <summary>
        /// Occurs when a fade is started
        /// </summary>
        public event FadeDelegate FadeStarted = delegate { };
        /// <summary>
        /// Occurs when the player calls the next track
        /// </summary>
        public event NextDelegate NextCalled = delegate { };
        /// <summary>
        /// Occurs in case of an error
        /// </summary>
        public event ErrorDelegate Error = delegate { };
        /// <summary>
        /// Occurs when the player's volume has changed
        /// </summary>
        public event VolumeChangeDelegate VolumeChanged = delegate { };
        /// <summary>
        /// Occurs when the player's pan has changed
        /// </summary>
        public event PanChangeDelegate PanChanged = delegate { };

        protected FMOD.Channel FMODChannel = null;
        protected FMOD.Sound FMODSound = null;

        Timer PlayTimer;

        // Player settings
        string _Stream = "";
        public string Stream { get { return this._Stream; } }
        int _Volume = 1000;
        public int Volume
        {
            get { return this._Volume; }
            set
            {
                if (value < 0 || value > 1000) { throw new ArgumentException("Volume must be between 0 and 1000."); }
                this._Volume = value;
                if (this.Running) { this.SetFade(this, value, Fade.DefaultVolumeChangeLength, FadeType.VolumeChange); }
                this.VolumeChanged(this, new VolumeChangeEventArgs(value));
            }
        }
        public int RealVolume
        {
            get
            {
                float Volume = 0F;
                if(this.Initialized) { HandleError(this.FMODChannel.getVolume(ref Volume), true); }
                return Convert.ToInt32(Volume * 1000.0F);
            }
            protected set { this.SetVolume(value); }
        }
        protected void SetVolume() { this.SetVolume(this.Volume); }
        protected void SetVolume(int Volume)
        {
            if (this.Initialized) { HandleError(this.FMODChannel.setVolume((float)Volume / 1000F), true); }
        }
        int _Pan = 0;
        public int Pan
        {
            get { return this._Pan; }
            set
            {
                if (value < -1000 || value > 1000) { throw new ArgumentException("Pan must be between -1000 and 1000."); }
                this._Pan = value;
                this.RealPan = value;
                this.PanChanged(this, new PanChangeEventArgs(value));
            }
        }
        public int RealPan
        {
            get
            {
                float Pan = 0.0F;
                if (this.Initialized) { this.FMODChannel.getPan(ref Pan); }
                return Convert.ToInt32(Pan * 1000.0F);
            }
            protected set { this.SetPan(value); }
        }
        protected void SetPan() { SetPan(this.Pan); }
        protected void SetPan(int Pan)
        {
            if (this.Initialized) { HandleError(this.FMODChannel.setPan((float)Pan / 1000F), true); }
        }

        // Fade state and options
        protected Fade _Fade = null;
        public bool Fading { get { return this._Fade != null; } }
        public bool FinalFading { get { return Fading && this._Fade.Final; } }
        public bool FadeEnabled { get { return Fade.Enabled; } set { Fade.Enabled = value; } }
        protected bool CanFade { get { return this.Running; } }

        // Player state
        protected bool _Aborting = false;
        protected bool _NextCalled = false;
        protected bool _EndDone = false;

        /// <summary>
        /// Returns true if FMOD is initialized
        /// </summary>
        public bool Initialized
        {
            get
            {
                try { return (SoundSystem.Initialized && this.FMODChannel != null && this.FMODSound != null); }
                catch { return false; }
            }
        }
        
        /// <summary>
        /// Returns true if the player is paused.
        /// </summary>
        public bool Paused
        {
            get
            {
                bool Paused = false;
                if (!Initialized) { return false; }
                FMODChannel.getPaused(ref Paused);
                if (!Paused && Fading && this._Fade.Type == FadeType.Pause) { return true; }
                return Paused;
            }
        }
        /// <summary>
        /// Returns true if the player is running there is no final fade running.
        /// </summary>
        public bool Playing
        {
            get
            {
                bool Playing = this.Running;
                if (Playing && Fading && this._Fade.Final) { return false; }
                return Playing;
            }
        }
        /// <summary>
        /// Returns true if the player is running.
        /// </summary>
        public bool Running
        {
            get
            {
                bool Running = false;
                if (!Initialized) { return false; }
                try { FMODChannel.isPlaying(ref Running); }
                catch { return false; }
                return Running;
            }
        }
        public uint Position
        {
            get
            {
                uint Position = 0;
                if (!this.Running) { throw new InvalidOperationException("This player is not playing."); }
                HandleError(this.FMODChannel.getPosition(ref Position, FMOD.TIMEUNIT.MS), true);
                return Position;
            }
        }
        public uint Length
        {
            get
            {
                uint Length = 0;
                if (!this.Initialized) { throw new InvalidOperationException("This player is not initialized."); }
                HandleError(this.FMODSound.getLength(ref Length, FMOD.TIMEUNIT.MS), true);
                return Length;
            }
        }
        public uint Remaining { get { return this.Length - this.Position; } }

        /// <summary>
        /// Creates a new player instance
        /// </summary>
        /// <param name="Stream">Path of the file to load</param>
        public Player(string Stream)
        {
            this._Stream = Stream;
        }
        public void Initialize()
        {
            if (!SoundSystem.Initialized) { SoundSystem.Initialize(); }
            try
            {
                HandleError(SoundSystem.Active.createStream(this._Stream, FMOD.MODE.LOOP_OFF, ref this.FMODSound), true);
                HandleError(SoundSystem.Active.playSound(FMOD.CHANNELINDEX.REUSE, this.FMODSound, true, ref this.FMODChannel), true);
            }
            catch { }
            this.ResetParameters();
        }
        void Tick(object sender, EventArgs e)
        {
            if (!this.Initialized) { return; }
            try
            {
                SoundSystem.Update();
            }
            catch { this.FMODCallback((IntPtr)null, FMOD.CHANNEL_CALLBACKTYPE.END, (IntPtr)null, (IntPtr)null); }
            if (this.Fading) { this._Fade.Tick(); }
            Fade.SpawnCheck(this);
        }
        FMOD.RESULT FMODCallback(IntPtr FMODSystem, FMOD.CHANNEL_CALLBACKTYPE Type, IntPtr Data1, IntPtr Data2)
        {
            if (this._EndDone || this._Aborting) { return FMOD.RESULT.OK; }
            if (Type != FMOD.CHANNEL_CALLBACKTYPE.END) { return FMOD.RESULT.OK; }
            if (!this._NextCalled) { this.CallNext(); }
            if (this.FinalFading)
            {
                this._Fade.Finish();
                return FMOD.RESULT.OK;
            }
            this.StopFinal(StopReason.Finished);
            this._EndDone = true;
            return FMOD.RESULT.OK;
        }

        // Player actions
        public void Play()
        {
            if (!this.Initialized) { this.Initialize(); }
            if (!this.Initialized) { this.CallNext(); return; }
            HandleError(this.FMODChannel.setCallback(new FMOD.CHANNEL_CALLBACK(FMODCallback)), true);
            HandleError(this.FMODChannel.setPaused(false), true);
            this.PlayTimer = new Multimedia.Timer();
            this.PlayTimer.Resolution = 1;
            this.PlayTimer.Tick += new EventHandler(Tick);
            this.PlayTimer.Start();
            this.SetVolume(); this.SetPan();
            int FMODFix = this.RealVolume;
            PlayStarted(this, new EventArgs());
        }
        public void Stop() { this.Stop(StopReason.Aborted); }
        protected void Stop(StopReason Reason)
        {
            if (!this.Initialized) { return; }
            this._Fade = new FinalFade(this, FinalFade.DefaultStopLength, FadeType.Stop);
        }
        protected void StopFinal(StopReason Reason) 
        {
            this._Aborting = true;
            this.Finish();
            PlayStopped(this, new StopEventArgs(Reason));
        }
        protected void Finish()
        {
            this.ClearFade();
            if (this.FMODChannel != null)
            {
                try { this.FMODChannel.stop(); }
                catch { }
            }
            if (this.FMODSound != null)
            {
                try { this.FMODSound.release(); }
                catch { }
            }
            this.PlayTimer = null;
            this.FMODChannel = null;
            this.FMODSound = null;
            this.ResetParameters();
        }
        public void Pause()
        {
            if (!this.Playing) { return; }
            this.SetFade(this, 0, Fade.DefaultPauseLength, FadeType.Pause);
            PlayPaused(this, new PauseEventArgs(true));
        }
        protected void PauseFinal()
        {
            if (!this.Playing) { return; }
            HandleError(this.FMODChannel.setPaused(true), true);
        }
        public void Resume()
        {
            if (!this.Initialized || !this.Paused) { return; }
            this.RealVolume = 0;
            HandleError(this.FMODChannel.setPaused(false), true);
            this.SetFade(this, this.Volume, Fade.DefaultResumeLength, FadeType.Resume);
            PlayPaused(this, new PauseEventArgs(false));
        }
        public void Seek(uint Position)
        {
            if (!this.Playing) { return; }
            HandleError(this.FMODChannel.setPosition(Position, FMOD.TIMEUNIT.MS), true);
            this.ResetParameters();
            this.Seeking(this, new SeekEventArgs(this.Position));
        }
        public void Next()
        {
            if (this.FinalFading) { return; }
            SetFinalFade(this, FinalFade.DefaultNextLength, FadeType.Next);
        }
        protected void CallNext()
        {
            this._NextCalled = true;
            NextCalled(this, new EventArgs());
        }
        protected void SetFade(Player Player, int TargetVolume, int Length, FadeType FadeType)
        {
            new Fade(Player, TargetVolume, Length, FadeType);
        }
        protected void SetFinalFade(Player Player, int Length, FadeType Type)
        {
            new FinalFade(Player, Length, Type);
        }
        protected void ClearFade() { this._Fade = null; }

        // Helper methods
        protected void ResetParameters()
        {
            this.ClearFade();
            this.RealVolume = this.Volume;
            this.RealPan = this.Pan;
            this._NextCalled = false;
            this._EndDone = false;
            this._Aborting = false;
        }
        void HandleError(FMOD.RESULT FMODResult, bool Abort)
        {
            try { SoundSystem.HandleError(FMODResult); }
            catch (FMODException FMODException)
            {
                if (Abort)
                {
                    this.StopFinal(StopReason.Error);
                }
                this.Error(this, new ErrorEventArgs(FMODException));
            }
        }


        public class Fade
        {
            protected Player _Player;
            protected FadeState _State = FadeState.None;
            protected uint _StartPosition = 0;
            protected uint _EndPosition = 0;
            protected int _StartVolume = 0;
            protected int _EndVolume = 0;
            protected FadeType _Type;

            public Player Player { get { return this._Player; } }
            public FadeState State { get { return this._State; } }
            public uint StartPosition { get { return this._StartPosition; } }
            public uint EndPosition { get { return this._EndPosition; } }
            public int StartVolume { get { return this._StartVolume; } }
            public int EndVolume { get { return this._EndVolume; } }
            public FadeType Type { get { return this._Type; } }
            public bool Final { get { return this.Type == FadeType.Stop || this.Type == FadeType.Next || this.Type == FadeType.End; } }
            public bool CanFade { get { return this.Player != null && this.Player.Running; } }

            static public bool Enabled = true;
            static public int DefaultPauseLength = 1000;
            static public int DefaultResumeLength = 500;
            static public int DefaultVolumeChangeLength = 1000;

            public Fade(Player Player, int TargetVolume, int Length, FadeType Type)
            {
                if (!Player.CanFade) { return; }
                if (Type == FadeType.VolumeChange && Player._Fade != null && Player._Fade.Type != FadeType.VolumeChange)
                {
                    return;
                }
                this._Player = Player;
                uint Position = Player.Position;
                this._StartPosition = Position;
                this._EndPosition = Position + (uint)Length;
                this._StartVolume = Player.RealVolume;
                this._EndVolume = TargetVolume;
                this._Type = Type;
                Player._Fade = this;
                if (!Fade.Enabled) { this.Finish(); }
            }
            public virtual void Tick()
            {
                if (!this.CanFade || !Fade.Enabled) { return; }
                uint Position = this.Player.Position;
                uint Length = this.Player.Length;
                this.Player.RealVolume = this.StartVolume + (int)((double)(Position - this.StartPosition) / (double)(this.EndPosition - this.StartPosition) * (double)(this.EndVolume - this.StartVolume));
                if (Position >= this.EndPosition)
                {
                    this.Finish();
                }
            }
            public virtual void Finish()
            {
                if (!this.CanFade) { return; }
                Player.RealVolume = this._EndVolume;
                Player.ClearFade();
                GC.Collect();
                if(this.Type == FadeType.Pause) { Player.PauseFinal(); }
            }
            public static void SpawnCheck(Player Player)
            {
                if (!Player.CanFade || !Fade.Enabled) { return; }
                uint Position = Player.Position;
                uint Length = Player.Length;
                if (!Player.FinalFading && Position >= Length - FinalFade.DefaultEndLength && Length > FinalFade.DefaultEndLength * 4)
                {
                    Player.SetFinalFade(Player, FinalFade.DefaultEndLength, FadeType.End);
                }
            }
        }

        public class FinalFade : Fade
        {
            static public int DefaultStopLength = 5000;
            static public int DefaultNextLength = 5000;
            static public int DefaultEndLength = 8000;
            static public int NextOverlap = 3000;
            static public int EndOverlap = 5000;

            public FinalFade(Player Player, int Length, FadeType Type)
                : base(Player, 0, Length, Type)
            {
                if (!Player.CanFade) { return; }
                if (Player.Paused) { this.Finish(); }
                Player.FadeStarted(Player, new EventArgs());
            }

            public override void Tick()
            {
                base.Tick();
                if (!this.CanFade || !Fade.Enabled) { return; }
                uint Position = Player.Position;
                uint Length = Player.Length;
                if ((this.Type == FadeType.Next || this.Type == FadeType.End) && !Player._NextCalled && Position >= this.EndPosition - (this.Type == FadeType.Next ? NextOverlap : EndOverlap))
                {
                    Player.CallNext();
                }
            }

            public override void Finish()
            {
                base.Finish();
                if (this.Type == FadeType.Next && !this.Player._NextCalled) { Player.CallNext(); }
                Player.StopFinal(this.Type == FadeType.End ? StopReason.Finished : StopReason.Aborted);
            }
        }

        public enum FadeState { None, Running, Done }

        public enum FadeType
        {
            None,
            VolumeChange,
            Pause,
            Resume,
            Stop,
            Next,
            End
        }

        public class ErrorEventArgs : EventArgs
        {
            public FMODException Exception;
            public ErrorEventArgs(FMODException Exception)
                : base()
            {
                this.Exception = Exception;
            }
        }
        public class PauseEventArgs : EventArgs
        {
            public bool Paused;
            public PauseEventArgs(bool Paused)
                : base()
            {
                this.Paused = Paused;
            }
        }
        public class StopEventArgs : EventArgs
        {
            public StopReason Reason;
            public StopEventArgs(StopReason Reason)
                : base()
            {
                this.Reason = Reason;
            }
        }
        public class SeekEventArgs : EventArgs
        {
            public uint Position;
            public SeekEventArgs(uint Position)
                : base()
            {
                this.Position = Position;
            }
        }
        public class VolumeChangeEventArgs : EventArgs
        {
            public int Volume;
            public VolumeChangeEventArgs(int Volume)
                : base()
            {
                this.Volume = Volume;
            }
        }
        public class PanChangeEventArgs : EventArgs
        {
            public int Pan;
            public PanChangeEventArgs(int Pan)
                : base()
            {
                this.Pan = Pan;
            }
        }

        public enum StopReason { Finished, Aborted, Error }

    }

    public class FMODException : Exception
    {
        public FMOD.RESULT FMODResult;
        public FMODException(FMOD.RESULT FMODResult)
            : base(FMOD.Error.String(FMODResult))
        {
            this.FMODResult = FMODResult;
        }
    }
}