using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace AutoMusic
{
    public class Track : Player
    {
        public delegate void StateChangeDelegate(object sender, EventArgs e);
        public event StateChangeDelegate StateChanged = delegate { };

        // Track state
        TrackResult _Result = TrackResult.None;
        bool _Excluded = false;
        public bool Excluded
        {
            get { return this._Excluded; }
            set
            {
                if (!value || this._Result != TrackResult.None || this._Excluded) { throw new InvalidOperationException("Cannot exclude a track with this state."); }
                this._Excluded = value;
                this.StateChanged(this, new EventArgs());
            }
        }

        // Track information
        bool _InfoAvailable = false;
        public bool InfoAvailable { get { return this._InfoAvailable; } }
        uint _Length = 0;
        public new uint Length { get { if (!this._InfoAvailable) { this.LoadInfo(); } return this._Length; } }
        string _Artist = "";
        public string Artist { get { if (!this._InfoAvailable) { this.LoadInfo(); } return this._Artist; } }
        string _Title = "";
        public string Title { get { if (!this._InfoAvailable) { this.LoadInfo(); } return this._Title; } }
        public string Name
        {
            get
            {
                if (Title == "") { return Path.GetFileName(this.Stream); }
                return (Artist == "" ? Title : Artist + " - " + Title);
            }
        }

        public TrackState State
        {
            get
            {
                if (this._Excluded) { return TrackState.Excluded; }
                switch (this._Result)
                {
                    case TrackResult.None:
                        if (this.Paused) { return TrackState.Paused; }
                        if (this.Playing) { return TrackState.Playing; }
                        if (this.FinalFading) { return TrackState.Fading; }
                        break;
                    case TrackResult.Success: return TrackState.Done;
                    case TrackResult.Aborted: return TrackState.Aborted;
                    case TrackResult.Error: return TrackState.Error;
                }
                return TrackState.Queued;
            }
        }

        public Track(string Stream)
            : base(Stream)
        {
            this.Error += new ErrorDelegate(Track_Error);
            this.FadeStarted += new FadeDelegate(Track_FadeStarted);
            this.PlayPaused += new PauseDelegate(Track_PlayPaused);
            this.PlayStarted += new PlayDelegate(Track_PlayStarted);
            this.PlayStopped += new StopDelegate(Track_PlayStopped);
            this.Volume = Playlist.Active.Volume;
        }

        void Track_PlayStarted(object sender, EventArgs e)
        {
            this.StateChanged(this, new EventArgs());
        }
        void Track_PlayPaused(object sender, Player.PauseEventArgs e)
        {
            this.StateChanged(this, new EventArgs());
        }
        void Track_FadeStarted(object sender, EventArgs e)
        {
            this.StateChanged(this, new EventArgs());
        }
        void Track_Error(object sender, Player.ErrorEventArgs e)
        {
            this.StateChanged(this, new EventArgs());
        }
        void Track_PlayStopped(object sender, Player.StopEventArgs e)
        {
            switch (e.Reason)
            {
                case StopReason.Finished: this._Result = TrackResult.Success; break;
                case StopReason.Aborted: this._Result = TrackResult.Aborted; break;
                default: this._Result = TrackResult.Error; break;
            }
            this.StateChanged(this, new EventArgs());
        }
        public void LoadInfo()
        {
            bool Finish = !this.Initialized;
            if (!this.Initialized) { this.Initialize(); }
            if (this.State != TrackState.Error)
            {
                this._Length = base.Length;
                this._Artist = this.GetTag("Artist").Trim();
                this._Title = this.GetTag("Title").Trim();
            }
            if (Finish) { this.Finish(); }
            this._InfoAvailable = true;
        }
        string GetTag(string Name)
        {
            try
            {
                FMOD.TAG TagData = new FMOD.TAG();
                this.FMODSound.getTag(Name.ToUpper(), 0, ref TagData);
                return Marshal.PtrToStringAnsi(TagData.data);
            }
            catch { return ""; }
        }
        public Track Duplicate()
        {
            return new Track(this.Stream);
        }
        public void Reset()
        {
            if (this.Initialized) { this.StopFinal(StopReason.Aborted); }
            this.ResetParameters();
            this._Excluded = false;
            this._Result = TrackResult.None;
            this.StateChanged(this, new EventArgs());
        }
        public override string ToString() { return this.ToString(PlaylistFormat.Internal); }
        public string ToString(PlaylistFormat Format)
        {
            switch (Format)
            {
                case PlaylistFormat.Auto:
                    throw new ArgumentException("A format must be specified.");
                case PlaylistFormat.Internal:
                    string State;
                    switch (this.State)
                    {
                        case TrackState.Queued: State = "Q"; break;
                        case TrackState.Playing:
                        case TrackState.Paused:
                            if ((double)this.Position / (double)this.Length < 0.38D) { State = "Q"; } else { State = "A"; } break;
                        case TrackState.Fading: if (this._Fade.Type == FadeType.End) { State = "D"; } else { State = "A"; } break;
                        case TrackState.Excluded: State = "S"; break;
                        case TrackState.Aborted: State = "A"; break;
                        case TrackState.Done: State = "D"; break;
                        case TrackState.Error: State = "E"; break;
                        default: State = "E"; break;
                    }
                    return State + " " + this.Stream;
                case PlaylistFormat.M3U:
                    return "#EXTINF:" + (this._InfoAvailable ? this.Length.ToString() : "0") + "," + (this._InfoAvailable ? this.Name : Path.GetFileNameWithoutExtension(this.Stream)) + "\n" + this.Stream;

            }
            throw new ArgumentException("The specified playlist format is not supported.");
        }

        static public string StateFormat(TrackState State)
        {
            switch (State)
            {
                case TrackState.Queued: return "Queued";
                case TrackState.Playing: return "Playing";
                case TrackState.Paused: return "Paused";
                case TrackState.Fading: return "Fading";
                case TrackState.Excluded: return "Skipped";
                case TrackState.Aborted: return "Stopped";
                case TrackState.Done: return "Done";
                case TrackState.Error: return "Error";
                default: return "";
            }
        }

        static public Track Parse(string Line, PlaylistFormat Format)
        {
            switch (Format)
            {
                case PlaylistFormat.Auto:
                    throw new ArgumentException("Format recognition is not supported at track level.");
                case PlaylistFormat.Internal:
                    string State = Line.Substring(0, 1);
                    string Path = System.IO.Path.GetFullPath(Line.Substring(2));
                    Track Track = new Track(Path);
                    switch (State)
                    {
                        case "Q": Track._Result = TrackResult.None; break;
                        case "A": Track._Result = TrackResult.Aborted; break;
                        case "D": Track._Result = TrackResult.Success; break;
                        case "S": Track._Result = TrackResult.None; Track._Excluded = true; break;
                        default: Track._Result = TrackResult.Error; break;
                    }
                    return Track;
                case PlaylistFormat.M3U:
                    try
                    {
                        string[] Lines = Line.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                        Track M3UTrack = new Track(System.IO.Path.GetFullPath(Lines[1].Trim().Trim(new char[] { '\r' })));
                        M3UTrack._Result = TrackResult.None;
                        return M3UTrack;
                    }
                    catch { throw new ArgumentException("The specified playlist line could not be recognized."); }
            }
            throw new ArgumentException("The specified playlist format is not supported.");
        }
    }

    public enum TrackResult { None,  Success, Aborted, Error }
    public enum TrackState { Queued, Playing, Paused, Fading, Excluded, Aborted, Done, Error }
    
    public enum PlaylistFormat { Auto, Internal, M3U }
}