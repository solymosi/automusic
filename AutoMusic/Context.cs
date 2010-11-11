using System;
using System.Windows.Forms;

namespace AutoMusic
{
    class Context : ApplicationContext
    {
        NotifyIcon Icon;
        MainForm Form;

        public Context(string[] args)
        {
            InitializeSession();
            Schedule.Enable();
            this.ThreadExit += new EventHandler(Context_ThreadExit);
            Icon = new NotifyIcon();
            Icon.Icon = AutoMusic.Properties.Resources.AutoMusicTray;
            Icon.Click += new EventHandler(Icon_Click);
            Icon.Text = "AutoMusic";
            Icon.Visible = true;
            Global.Initialize();
            ShowForm();
        }

        void Icon_Click(object sender, EventArgs e)
        {
            ShowForm();
        }

        void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Playlist.Active != null && Playlist.Active.IsCurrent && Player.Fade.Enabled)
            {
                this.Icon.Visible = false;
                Playlist.Active.Current.Stop();
                Timer FadeTimer = new Timer();
                FadeTimer.Interval = Player.FinalFade.DefaultStopLength;
                FadeTimer.Tick += new EventHandler(delegate { this.ExitThread(); });
                FadeTimer.Start();
            }
            else
            {
                this.ExitThread();
            }
        }

        void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Tools.Question("Are you sure you want to exit " + Application.ProductName + "?", MessageBoxIcon.Exclamation) == DialogResult.No) { e.Cancel = true; }
        }

        void Form_Resize(object sender, EventArgs e)
        {
            if (Form.Visible && Form.WindowState == FormWindowState.Minimized)
            {
                Form.Hide();
            }
        }

        void Context_ThreadExit(object sender, EventArgs e)
        {
            this.Icon.Visible = false;
            try { Tools.SetRegistry("Playlist", Playlist.Active.Path); }
            catch { }
            try { Tools.SetRegistry("Schedule", Schedule.Active.Path); }
            catch { }
            try { Tools.SetRegistry("Volume", Global.Volume.ToString()); }
            catch { }
            try { Tools.SetRegistry("TimeOffset", Time.TimeOffset.ToString()); }
            catch { }
        }

        public void InitializeSession()
        {
            Playlist.Active = new Playlist();
            try { Playlist.Active = Playlist.Load(Tools.GetRegistry("Playlist", "")); }
            catch { }
            Schedule.Active = new Schedule();
            try { Schedule.Active = Schedule.Load(Tools.GetRegistry("Schedule", "")); }
            catch { }
            try { Global.Volume = int.Parse(Tools.GetRegistry("Volume", "500")); }
            catch { Global.Volume = 1000; }
            try { Time.TimeOffset = int.Parse(Tools.GetRegistry("TimeOffset", "0")); }
            catch { Time.TimeOffset = 0; }
        }

        public void ShowForm()
        {
            if (Form == null)
            {
                Form = new MainForm();
                Form.Resize += new EventHandler(Form_Resize);
                Form.FormClosing += new FormClosingEventHandler(Form_FormClosing);
                Form.FormClosed += new FormClosedEventHandler(Form_FormClosed);
            }
            Form.Show();
            Form.Activate();
            Form.WindowState = (Form.WindowState == FormWindowState.Minimized ? FormWindowState.Normal : Form.WindowState);
        }
    }
}
