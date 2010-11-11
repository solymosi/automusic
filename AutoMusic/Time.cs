using System;

namespace AutoMusic
{
    class Time
    {
        int _Hour;
        int _Minute;
        public int Hour
        {
            get { return this._Hour; }
            set
            {
                if (value < 0 || value > 23) { throw new ArgumentOutOfRangeException("Hour must be between 0 and 23."); }
                this._Hour = value;
            }
        }
        public int Minute
        {
            get { return this._Minute; }
            set
            {
                if (value < 0 || value > 59) { throw new ArgumentOutOfRangeException("Minute must be between 0 and 59."); }
                this._Minute = value;
            }
        }
        public DateTime Date
        {
            get { return new DateTime(Time.Corrected.Year, Time.Corrected.Month, Time.Corrected.Day, this.Hour, this.Minute, 0); }
            set { this.Hour = value.Hour; this.Minute = value.Minute; }
        }

        public Time(int Hour, int Minute)
        {
            this.Hour = Hour;
            this.Minute = Minute;
        }

        public override string ToString()
        {
            return this.Hour.ToString("00") + ":" + this.Minute.ToString("00");
        }
        static public int TimeOffset = 0;
        static public Time Parse(string Time)
        {
            try
            {
                string[] N = Time.Split(new string[] { ":" }, StringSplitOptions.None);
                Time T = new Time(int.Parse(N[0]), int.Parse(N[1]));
                return T;
            }
            catch { throw new FormatException("The time format is invalid."); }
        }
        static public DateTime Corrected { get { return DateTime.Now.AddSeconds((double)TimeOffset); } }
        static public string Format(int Milliseconds)
        {
            TimeSpan D = new TimeSpan(0, 0, 0, 0, (int)Milliseconds);
            return D.Minutes.ToString() + ":" + D.Seconds.ToString("00");
        }
        static public void CorrectTo(DateTime Time)
        {
            TimeOffset = (int)(Time - DateTime.Now).TotalSeconds;
        }
    }

    class TimeFrame
    {
        Time _From;
        Time _To;
        public Time From
        {
            get { return this._From; }
            set
            {
                if (value == null) { throw new ArgumentNullException(); }
                this._From = value;
            }
        }
        public Time To
        {
            get { return this._To; }
            set
            {
                if (value == null) { throw new ArgumentNullException(); }
                this._To = value;
            }
        }
        public bool Exclusion = false;

        public TimeFrame(Time From, Time To, bool Exclusion)
        {
            this.From = From;
            this.To = To;
            this.Exclusion = Exclusion;
        }

        public override string ToString()
        {
            return (Exclusion ? "E" : "I") + " " + this.From.ToString() + " " + this.To.ToString();
        }
        static public TimeFrame Parse(string TF)
        {
            try
            {
                string[] T = TF.Split(new string[] { " " }, StringSplitOptions.None);
                return new TimeFrame(Time.Parse(T[1]), Time.Parse(T[2]), T[0] == "E" ? true : false);
            }
            catch { throw new FormatException("The timeframe format is invalid."); }
        }

        public TimeFrame Duplicate()
        {
            return new TimeFrame(new Time(this.From.Hour, this.From.Minute), new Time(this.To.Hour, this.To.Minute), this.Exclusion);
        }
    }
}
