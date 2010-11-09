using System;
using System.Collections.Generic;
using Multimedia;
using System.IO;

namespace AutoMusic
{
    class Schedule
    {
        public delegate void StateChangeDelegate(object sender, EventArgs e);
        public delegate void PlayDelegate(object sender, PlayEventArgs e);
        public delegate void StopDelegate(object sender, StopEventArgs e);

        string _Path;
        public string Path { get { return this._Path; } }
        List<TimeFrame> _TimeFrames;
        public List<TimeFrame> TimeFrames { get { return this._TimeFrames; } }

        public Schedule()
        {
            this._TimeFrames = new List<TimeFrame>();
        }

        public void Add(TimeFrame TimeFrame)
        {
            if (TimeFrame == null) { throw new ArgumentNullException(); }
            this.TimeFrames.Add(TimeFrame);
        }
        public void Remove(TimeFrame TimeFrame)
        {
            if (TimeFrame == null) { throw new ArgumentNullException(); }
            this.TimeFrames.Remove(TimeFrame);
        }
        public void Duplicate(TimeFrame TimeFrame)
        {
            this.Add(TimeFrame.Duplicate());
        }

        public void Save() { this.Save(""); }
        public void Save(string Path)
        {
            if (Path == "" && this.Path == "") { throw new InvalidOperationException("No path was specified."); }
            if (Path == "") { Path = this.Path; }
            StreamWriter Writer = new StreamWriter(Path);
            Writer.Write(this.ToString());
            Writer.Flush(); Writer.Close();
            this._Path = Path;
        }

        public override string ToString()
        {
            string P = "";
            for (int i = 0; i < this.TimeFrames.Count; i++)
            {
                P += this.TimeFrames[i].ToString() + "\n";
            }
            return P;
        }

        public bool AllowPlayNow { get { return this.AllowPlayAt(Time.Corrected); } }
        public bool AllowPlayAt(DateTime Time)
        {
            bool Allow = Schedule.Mode == ScheduleMode.WhiteList ? false : true;
            foreach (TimeFrame Frame in this.TimeFrames)
            {
                DateTime From = Frame.From.Date;
                DateTime To = Frame.To.Date;
                if (To < From) { To = To.AddDays(1); }
                if (Frame.Exclusion && From.AddSeconds(-1 * Schedule.StopBefore) <= Time && To.AddSeconds(Schedule.StartAfter) >= Time) { return false; }
                if (!Frame.Exclusion && From.AddSeconds(Schedule.StartAfter) <= Time && To.AddSeconds(-1 * Schedule.StopBefore) >= Time) { Allow = true; }
            }
            return Allow;
        }

        public void Sort()
        {
            this.TimeFrames.Sort(new Comparison<TimeFrame>(delegate(TimeFrame T1, TimeFrame T2)
                {
                    if (T1.From.Date == T2.From.Date)
                    {
                        if (T1.To.Date == T2.To.Date) { return 0; }
                        else
                        {
                            if (T1.To.Date > T2.To.Date) { return 1; }
                            else { return -1; }
                        }
                    }
                    else
                    {
                        if (T1.From.Date > T2.From.Date) { return 1; }
                        else { return -1; }
                    }
                }));
        }


        // Static members
        static public Schedule Active;

        static public int StartAfter = 10;
        static public int StopBefore = 10;

        static public bool Enabled
        {
            get { return (ScheduleTimer != null && ScheduleTimer.IsRunning); }
        }

        static public void Enable()
        {
            ScheduleTimer = new Timer();
            ScheduleTimer.Period = 1000;
            ScheduleTimer.Resolution = 1;
            ScheduleTimer.Tick += new EventHandler(ScheduleTimer_Tick);
            ScheduleTimer.Start();
        }
        static public void Disable()
        {
            ScheduleTimer.Stop();
            ScheduleTimer = null;
        }
        static Timer ScheduleTimer;
        static void ScheduleTimer_Tick(object sender, EventArgs e)
        {
            if (!Enabled || Playlist.Active == null) { return; }
            if (AllowPlay)
            {
                if (!Playlist.Active.Playing) { Playlist.Active.Play(); }
            }
            else
            {
                if (Playlist.Active.Playing) { Playlist.Active.Current.Stop(); }
            }
        }

        static public bool AllowPlay { get { return Active.AllowPlayNow; } }
        static public ScheduleMode Mode = ScheduleMode.WhiteList;

        static public Schedule Load(string File)
        {
            StreamReader Reader = new StreamReader(File);
            string[] Lines = Reader.ReadToEnd().Split(new string[] { "\n" }, StringSplitOptions.None);
            Schedule S = new Schedule();
            S._Path = File;
            for (int i = 0; i < Lines.Length; i++)
            {
                try { S.Add(TimeFrame.Parse(Lines[i].Trim(new char[] { '\r' }))); }
                catch { }
            }
            return S;
        }

        public class PlayEventArgs : EventArgs
        {
            public TimeFrame TimeFrame;
            public PlayEventArgs(TimeFrame TimeFrame)
            {
                this.TimeFrame = TimeFrame;
            }
        }
        public class StopEventArgs : EventArgs
        {
            public TimeFrame TimeFrame;
            public StopEventArgs(TimeFrame TimeFrame)
            {
                this.TimeFrame = TimeFrame;
            }
        }

        public enum ScheduleMode { WhiteList, BlackList }
    }
}