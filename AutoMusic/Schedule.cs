﻿using System;
using System.Collections.Generic;
using Multimedia;
using System.IO;

namespace AutoMusic
{
    class Schedule : SingletonList<TimeFrame>
    {
        public delegate void StateChangeDelegate(object sender, EventArgs e);
        public delegate void PlayDelegate(object sender, PlayEventArgs e);
        public delegate void StopDelegate(object sender, StopEventArgs e);

        public Schedule()
            : base()
        { }

        public override void Add(TimeFrame TimeFrame)
        {
            base.Add(TimeFrame);
        }
        public override void Remove(TimeFrame TimeFrame)
        {
            base.Remove(TimeFrame);
        }
        public void Duplicate(TimeFrame TimeFrame)
        {
            this.Add(TimeFrame.Duplicate());
        }

        public override void Save(string Path)
        {
            if (Path == "" && this.Path == "") { throw new InvalidOperationException("No path was specified."); }
            if (Path == "") { Path = this.Path; }
            this.Export(Path);
            this._Path = Path;
        }

        public override void Export(string Path)
        {
            StreamWriter Writer = new StreamWriter(Path);
            Writer.Write(this.ToString());
            Writer.Flush();
            Writer.Close();
        }

        public override string ToString()
        {
            string P = "AMS\n";
            for (int i = 0; i < this.Items.Count; i++)
            {
                P += this.Items[i].ToString() + "\n";
            }
            return P;
        }

        public bool AllowPlayNow { get { return this.AllowPlayAt(Time.Corrected); } }
        public bool AllowPlayAt(DateTime Time)
        {
            bool Allow = Schedule.Mode == ScheduleMode.WhiteList ? false : true;
            foreach (TimeFrame Frame in this.Items)
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
            this.Items.Sort(new Comparison<TimeFrame>(delegate(TimeFrame T1, TimeFrame T2)
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

        static public new Schedule Active;

        static public int StartAfter = 20;
        static public int StopBefore = 20;

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

        static public new Schedule Load(string File)
        {
            StreamReader Reader = new StreamReader(File);
            string[] Lines = Reader.ReadToEnd().Split(new string[] { "\n" }, StringSplitOptions.None);
            Reader.Close();
            Schedule S = new Schedule();
            S._Path = File;
            for (int i = 1; i < Lines.Length; i++)
            {
                try
                {
                    S.Add(TimeFrame.Parse(Lines[i].Trim(new char[] { '\r' })));
                }
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