﻿using System;
using System.Collections.Generic;
using System.IO;

namespace AutoMusic
{
    public class Playlist
    {
        public delegate void CurrentChangeDelegate(object sender, EventArgs e);
        public delegate void FinishDelegate(object sender, EventArgs e);
        public delegate void PlayDelegate(object sender, EventArgs e);
        public delegate void PauseDelegate(object sender, Player.PauseEventArgs e);
        public delegate void StopDelegate(object sender, Player.StopEventArgs e);
        public delegate void SeekDelegate(object sender, Player.SeekEventArgs e);
        public delegate void FadeDelegate(object sender, EventArgs e);
        public delegate void NextDelegate(object sender, EventArgs e);
        public delegate void ErrorDelegate(object sender, Player.ErrorEventArgs e);
        public delegate void StateChangeDelegate(object sender, EventArgs e);

        public event CurrentChangeDelegate CurrentTrackChanged = delegate { };
        public event FinishDelegate Finished = delegate { };
        public event PlayDelegate PlayStarted = delegate { };
        public event PauseDelegate PlayPaused = delegate { };
        public event StopDelegate PlayStopped = delegate { };
        public event SeekDelegate Seeking = delegate { };
        public event FadeDelegate FadeStarted = delegate { };
        public event NextDelegate NextCalled = delegate { };
        public event ErrorDelegate Error = delegate { };
        public event StateChangeDelegate StateChanged = delegate { };

        string _Path = "";
        int _Volume = 1000;
        public int Volume
        {
            get { return this._Volume; }
            set
            {
                if (value < 0 || value > 1000) { throw new ArgumentException("Volume must be between 0 and 1000."); }
                this._Volume = value;
                if (this.IsCurrent) { this.Current.Volume = value; }
            }
        }
        int _Pan = 0;
        PlaylistFormat _Format = PlaylistFormat.Internal;
        public int Pan
        {
            get { return this._Pan; }
            set
            {
                if (value < -1000 || value > 1000) { throw new ArgumentException("Pan must be between -1000 and 1000."); }
                this._Pan = value;
                if (this.IsCurrent) { this.Current.Pan = value; }
            }
        }
        public string Path { get { return this._Path; } }
        List<Track> _Tracks;
        public List<Track> Tracks { get { return this._Tracks; } }
        Track _Current = null;
        public Track Current
        {
            get { return this._Current; }
            private set
            {
                this._Current = value;
                CurrentTrackChanged(this, new EventArgs());
            }
        }
        public Track Upcoming
        {
            get
            {
                for (int i = 0; i < this.Tracks.Count; i++)
                {
                    if (this.Tracks[i].State == TrackState.Queued) { return this.Tracks[i]; }
                }
                return null;
            }
        }
        public bool IsCurrent { get { return this.Current != null; } }
        public bool IsUpcoming { get { return this.Upcoming != null; } }
        public bool Playing
        {
            get
            {
                foreach (Track Track in this.Tracks)
                {
                    if (Track.Playing) { return true; }
                }
                return false;
            }
        }
        public bool Saved { get { return (this.Path != ""); } }
        public bool Empty { get { return (this.Tracks.Count == 0); } }
        public PlaylistFormat Format { get { return this._Format; } }

        public Playlist()
        {
            this._Tracks = new List<Track>();
        }

        public void Play()
        {
            if (this.IsCurrent) 
            {
                if (this.Current.Paused) { this.Current.Resume(); }
                return;
            }
            this.PlayNext();
        }

        void PlayNext()
        {
            if (!this.IsUpcoming) { this.Finished(this, new EventArgs()); return; }
            this.Current = this.Upcoming;
            this.Current.Volume = this.Volume;
            this.Current.Play();
        }

        public void Next()
        {
            if (!this.IsCurrent) { Play(); return; }
            this.Current.Next();
        }

        public void Add(Track Track)
        {
            if (Track == null) { throw new ArgumentNullException(); }
            this.AttachEventHandlers(Track);
            this.Tracks.Add(Track);
        }
        public void Add(Playlist Playlist)
        {
            if (Playlist == null) { throw new ArgumentNullException(); }
            this.AddRange(Playlist.Tracks);
        }
        public void AddRange(List<Track> Tracks)
        {
            for (int i = 0; i < Tracks.Count; i++) { this.Add(Tracks[i]); }
        }
        public void Insert(int Index, Track Track)
        {
            if (Track == null) { throw new ArgumentNullException(); }
            this.AttachEventHandlers(Track);
            this.Tracks.Insert(Index, Track);
        }
        public void InsertRange(int Index, List<Track> Tracks)
        {
            foreach (Track T in Tracks) { this.AttachEventHandlers(T); }
            this.Tracks.InsertRange(Index, Tracks);
        }
        public void Remove(Track Track)
        {
            if (Track == null) { throw new ArgumentNullException(); }
            this.DetachEventHandlers(Track);
            this.Tracks.Remove(Track);
        }
        public void Duplicate(Track Track)
        {
            this.Add(Track.Duplicate());
        }
        public void Reset()
        {
            for (int i = 0; i < this.Tracks.Count; i++)
            {
                this.Tracks[i].Reset();
            }
            this.Current = null;
        }
        public void Move(Track Track, int Position)
        {
            if (Track == null) { throw new ArgumentNullException(); }
            this.Tracks.Remove(Track);
            this.Tracks.Insert(Position, Track);
        }
        void AttachEventHandlers(Track Track)
        {
            Track.Error += new Player.ErrorDelegate(Track_Error);
            Track.FadeStarted += new Player.FadeDelegate(Track_FadeStarted);
            Track.NextCalled += new Player.NextDelegate(Track_NextCalled);
            Track.PlayPaused += new Player.PauseDelegate(Track_PlayPaused);
            Track.PlayStarted += new Player.PlayDelegate(Track_PlayStarted);
            Track.PlayStopped += new Player.StopDelegate(Track_PlayStopped);
            Track.StateChanged += new Track.StateChangeDelegate(Track_StateChanged);
        }
        void DetachEventHandlers(Track Track)
        {
            Track.Error -= Track_Error;
            Track.FadeStarted -= Track_FadeStarted;
            Track.NextCalled -= Track_NextCalled;
            Track.PlayPaused -= Track_PlayPaused;
            Track.PlayStarted -= Track_PlayStarted;
            Track.PlayStopped -= Track_PlayStopped;
            Track.StateChanged -= Track_StateChanged;
        }
        void Track_PlayStopped(object sender, Player.StopEventArgs e)
        {
            if (sender == this.Current) { this.Current = null; }
            this.PlayStopped(sender, e);
        }
        void Track_PlayStarted(object sender, EventArgs e)
        {
            this.PlayStarted(sender, e);
        }
        void Track_PlayPaused(object sender, Player.PauseEventArgs e)
        {
            this.PlayPaused(sender, e);
        }
        void Track_NextCalled(object sender, EventArgs e)
        {
            this.NextCalled(sender, e);
            this.PlayNext();
        }
        void Track_FadeStarted(object sender, EventArgs e)
        {
            this.FadeStarted(sender, e);
        }
        void Track_Error(object sender, Player.ErrorEventArgs e)
        {
            this.Error(sender, e);
        }
        void Track_StateChanged(object sender, EventArgs e)
        {
            this.StateChanged(sender, e);
        }

        public void Save() { this.Save(this.Path, this.Format); }
        public void Save(string Path, PlaylistFormat Format)
        {
            this.Export(Path, Format);
            this._Path = Path;
            this._Format = Format;
        }

        public void Export(string Path, PlaylistFormat Format)
        {
            if (Path == "") { throw new InvalidOperationException("No path was specified."); }
            StreamWriter Writer = new StreamWriter(Path);
            Writer.Write(this.ToString(Format));
            Writer.Flush(); Writer.Close();
        }

        public override string ToString() { return this.ToString(PlaylistFormat.Internal); }
        public string ToString(PlaylistFormat Format)
        {
            switch (Format)
            {
                case PlaylistFormat.Auto:
                    throw new ArgumentException("A format must be specified.");
                case PlaylistFormat.Internal:
                    string P = "AMP\n";
                    for (int i = 0; i < this.Tracks.Count; i++)
                    {
                        P += this.Tracks[i].ToString(PlaylistFormat.Internal) + "\n";
                    }
                    return P;
                case PlaylistFormat.M3U:
                    string Q = "#EXTM3U\n";
                    for (int i = 0; i < this.Tracks.Count; i++)
                    {
                        Q += this.Tracks[i].ToString(PlaylistFormat.M3U) + "\n";
                    }
                    return Q;
            }
            throw new ArgumentException("The specified playlist format is invalid.");
        }

        // Static members

        static public Playlist Active;

        static public Playlist Load(string File) { return Playlist.Load(File, PlaylistFormat.Auto); }
        static public Playlist Load(string File, PlaylistFormat Format)
        {
            Directory.SetCurrentDirectory(System.IO.Path.GetDirectoryName(File));
            StreamReader Reader = new StreamReader(File);
            string[] Lines = Reader.ReadToEnd().Split(new string[] { "\n" }, StringSplitOptions.None);
            switch (Format)
            {
                case PlaylistFormat.Auto:
                    try
                    {
                        if (Lines[0].Trim(new char[] { '\r' }) == "#EXTM3U") { return Playlist.Load(File, PlaylistFormat.M3U); }
                        if (Lines[0].Trim(new char[] { '\r' }) == "AMP") { return Playlist.Load(File, PlaylistFormat.Internal); }
                        throw new FormatException();
                    }
                    catch { throw new FormatException("The playlist format could not be recognized."); }
                case PlaylistFormat.Internal:
                    Playlist P = new Playlist();
                    P._Path = File;
                    P._Format = PlaylistFormat.Internal;
                    for (int i = 1; i < Lines.Length; i++)
                    {
                        try { P.Add(Track.Parse(Lines[i].Trim(new char[] { '\r' }), PlaylistFormat.Internal)); }
                        catch { }
                    }
                    return P;
                case PlaylistFormat.M3U:
                    Playlist Q = new Playlist();
                    Q._Path = File;
                    Q._Format = PlaylistFormat.M3U;
                    for (int i = 1; i < Lines.Length; i++)
                    {
                        if (Lines[i].Trim(new char[] { '\r' }).StartsWith("#EXTINF"))
                        {
                            try { Q.Add(Track.Parse(Lines[i].Trim(new char[] { '\r' }) + "\n" + Lines[i + 1].Trim(new char[] { '\r' }), PlaylistFormat.M3U)); }
                            catch { }
                            i++;
                        }
                    }
                    return Q;
            }
            throw new FormatException("The specified playlist format is invalid.");
        }

        static public bool IsPlaylist(string File)
        {
            try
            {
                string Extension = System.IO.Path.GetExtension(File).ToLower().Trim();
                if (Extension == "amp" || Extension == "m3u")
                {
                    Playlist P = Playlist.Load(File);
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        static public List<Track> GetTracks(string File)
        {
            Playlist P = Playlist.Load(File);
            return P.Tracks;
        }
    }
}
