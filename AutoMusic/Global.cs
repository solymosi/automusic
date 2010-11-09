using System;
using Multimedia;

namespace AutoMusic
{
    static class Global
    {
        static public Context Context;

        static int _Volume;
        static public int Volume
        {
            get { return _Volume; }
            set
            {
                if (value < 0 || value > 1000) { throw new ArgumentException("Volume must be between 0 and 1000."); }
                _Volume = value;
                if (Playlist.Active != null) { Playlist.Active.Volume = value; }
            }
        }

        static public void Initialize()
        {
            if (Playlist.Active != null) { Playlist.Active.Volume = Global.Volume; }
        }
    }
}
