using System;
using System.Windows.Forms;

namespace AutoMusic
{
    class Context : ApplicationContext
    {
        NotifyIcon Icon;
        public Context(string[] args)
        {
            InitializeSession();
            Schedule.Enable();
            this.ThreadExit += new EventHandler(Context_ThreadExit);
            Icon = new NotifyIcon();
            Icon.Icon = AutoMusic.Properties.Resources.AutoMusicTray;
            Icon.Text = "AutoMusic";
            Icon.Visible = true;
            MainForm mf = new MainForm();
            mf.Show();
            Global.Initialize();
        }

        void Context_ThreadExit(object sender, EventArgs e)
        {
            this.Icon.Visible = false;
            try
            {
                //Global.CurrentPlaylist.Playlist.Active();
                //Global.CurrentSchedule.Playlist.Active();
                Tools.SetRegistry("Playlist", Playlist.Active.Path);
                Tools.SetRegistry("Schedule", Schedule.Active.Path);
            }
            catch { }
        }

        public void InitializeSession()
        {
            try
            {
                Playlist.Active = Playlist.Load(Tools.GetRegistry("CurrentPlaylist", ""));
            }
            catch
            {
                Playlist.Active = new Playlist();
            }
            try
            {
                Schedule.Active = Schedule.Load(Tools.GetRegistry("CurrentSchedule", ""));
            }
            catch
            {
                Schedule.Active = new Schedule();
            }
            try
            {
                Global.Volume = int.Parse(Tools.GetRegistry("Volume", "500"));
            }
            catch
            {
                Global.Volume = 1000;
            }
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            
        }
    }
}
