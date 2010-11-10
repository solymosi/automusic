using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace AutoMusic
{
    public partial class MainForm : Form
    {
        public int VolumeBeforeMute = Global.Volume;
        public MainForm()
        {
            InitializeComponent();
            this.PlaylistGrid_Resize(null, null);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
        private bool OnlyManual()
        {
            if (Schedule.Enabled)
            {
                if (Tools.Question("Auto mode is ON. Do you want to turn it OFF?", MessageBoxIcon.Exclamation) == DialogResult.Yes) { Schedule.Disable(); }
            }
            return Schedule.Enabled;
        }
        private void Play_Click(object sender, EventArgs e)
        {
            if (!Playlist.Active.Playing && OnlyManual()) { return; }
            Playlist.Active.Play();
        }
        private void Stop_Click(object sender, EventArgs e)
        {
            if (OnlyManual()) { return; }
            if (Playlist.Active.IsCurrent) { Playlist.Active.Current.Stop(); }
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateAll();
        }
        private void Pause_Click(object sender, EventArgs e)
        {
            if (!Playlist.Active.IsCurrent) { return; }
            Playlist.Active.Current.Pause();
        }
        private void Next_Click(object sender, EventArgs e)
        {
            if (Playlist.Active.IsUpcoming) { Playlist.Active.Next(); }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.AttachUpdateEventHandlers();
            this.UpdateAll();
        }
        void AttachUpdateEventHandlers()
        {
            Playlist.Active.CurrentTrackChanged += new Playlist.CurrentChangeDelegate(Playlist_CurrentTrackChanged);
            Playlist.Active.StateChanged += new Playlist.StateChangeDelegate(Playlist_StateChanged);
        }
        void DetachUpdateEventHandlers()
        {
            Playlist.Active.CurrentTrackChanged -= new Playlist.CurrentChangeDelegate(Playlist_CurrentTrackChanged);
            Playlist.Active.StateChanged -= new Playlist.StateChangeDelegate(Playlist_StateChanged);
        }
        void Playlist_StateChanged(object sender, EventArgs e)
        {
            UpdateAll();
        }
        void Playlist_CurrentTrackChanged(object sender, EventArgs e)
        {
            this.ScrollToCurrentTrack();
            this.UpdateAll();
        }
        private void UpdateAll()
        {
            UpdateGlobal();
            UpdateBars();
            UpdatePlaylistGrid();
            UpdateScheduleGrid();
            UpdatePlaylistMenu();
        }
        private void UpdatePlaylistGrid()
        {
            if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate { this.UpdatePlaylistGrid(); }); return; }
            for (int i = Playlist.Active.Tracks.Count; i < PlaylistGrid.Items.Count; )
            {
                PlaylistGrid.Items.RemoveAt(i);
            }
            for (int i = 0; i < Playlist.Active.Tracks.Count; i++)
            {
                Track Current = Playlist.Active.Tracks[i];
                TrackState State = Current.State;
                string Name = Path.GetFileName(Current.File);
                int Length = 0;
                if (Current.InfoAvailable)
                {
                    Name = Current.Name;
                    Length = Current.Length;
                }
                else
                {
                    if (!InfoLoadBW.IsBusy) { InfoLoadBW.RunWorkerAsync(); }
                }
                bool Running = Current.Running;
                int Position = 0;
                if(Running) { Position = Current.Position; }
                string Duration = "";
                if(Running) { Duration = Time.Format(Position) + " / " + Time.Format(Length); }
                else { Duration = (Length > 0 ? Time.Format(Length) : ""); }
                ListViewItem CurrentItem;
                try
                {
                    CurrentItem = PlaylistGrid.Items[i];
                    CurrentItem.Tag = Current;
                    if (CurrentItem.SubItems[0].Text != Track.StateFormat(State)) { CurrentItem.SubItems[0].Text = Track.StateFormat(State); }
                    if (CurrentItem.SubItems[1].Text != Name) { CurrentItem.SubItems[1].Text = Name; }
                    if (CurrentItem.SubItems[2].Text != Duration) { CurrentItem.SubItems[2].Text = Duration; }
                    this.ApplyPlaylistLineFormatting(CurrentItem, State);

                }
                catch
                {
                    CurrentItem = new ListViewItem(new ListViewItem.ListViewSubItem[] { new ListViewItem.ListViewSubItem(null, Track.StateFormat(State), Color.Empty, Color.Empty, new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold)), new ListViewItem.ListViewSubItem(null, Name), new ListViewItem.ListViewSubItem(null, Duration) }, 0);
                    CurrentItem.Tag = Current;
                    this.ApplyPlaylistLineFormatting(CurrentItem, State);
                    PlaylistGrid.Items.Add(CurrentItem);
                }
            }
        }
        private void UpdateScheduleGrid()
        {
            if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate { this.UpdateScheduleGrid(); }); return; }
            Schedule.Active.Sort();
            for (int i = 0; i < Schedule.Active.TimeFrames.Count; i++)
            {
                TimeFrame Current = Schedule.Active.TimeFrames[i];
                string Allow = Current.Exclusion ? "NO" : "YES";
                string From = Current.From.ToString();
                string To = Current.To.ToString();
                ListViewItem CurrentItem;
                try
                {
                    CurrentItem = ScheduleGrid.Items[i];
                    CurrentItem.Tag = Current;
                    if (CurrentItem.SubItems[0].Text != Allow) { CurrentItem.SubItems[0].Text = Allow; }
                    if (CurrentItem.SubItems[1].Text != From) { CurrentItem.SubItems[1].Text = From; }
                    if (CurrentItem.SubItems[2].Text != To) { CurrentItem.SubItems[2].Text = To; }
                    this.ApplyScheduleLineFormatting(CurrentItem, Current.Exclusion);
                }
                catch
                {
                    CurrentItem = new ListViewItem(new ListViewItem.ListViewSubItem[] { new ListViewItem.ListViewSubItem(null, Allow, Color.Empty, Color.Empty, new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold)), new ListViewItem.ListViewSubItem(null, From), new ListViewItem.ListViewSubItem(null, To) }, 0);
                    CurrentItem.Tag = Current;
                    this.ApplyScheduleLineFormatting(CurrentItem, Current.Exclusion);
                    ScheduleGrid.Items.Add(CurrentItem);
                }
            }
        }
        private void UpdateBars()
        {
            if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate { this.UpdateBars(); }); return; }
            if (Playlist.Active.IsCurrent && Playlist.Active.Current.Running)
            {
                this.SeekBarFill.Width = (int)Math.Round((double)((double)Playlist.Active.Current.Position / (double)Playlist.Active.Current.Length) * (double)this.SeekBar.Width);
                if (!this.SeekBarFill.Visible) { this.SeekBarFill.Visible = true; }
                if (!this.SeekBar.BackColor.Equals(Color.FromArgb(255, 255, 192))) { this.SeekBar.BackColor = Color.FromArgb(255, 255, 192); }
            }
            else
            {
                if (this.SeekBarFill.Visible) { this.SeekBarFill.Visible = false; }
                if (!this.SeekBar.BackColor.Equals(Color.FromArgb(192, 192, 255))) { this.SeekBar.BackColor = Color.FromArgb(192, 192, 255); }
            }

            this.VolumeBarFill.Width = (int)Math.Round((double)((double)Global.Volume / (double)1000) * (double)this.VolumeBar.Width);
        }
        private void UpdateGlobal()
        {
            if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate { this.UpdateGlobal(); }); return; }
            string AutoModeNew = "";
            if (Schedule.Enabled) { AutoModeNew = "Auto mode ON"; }
            else { AutoModeNew = "Auto mode OFF"; }
            if (this.AutoModeStatus.Text != AutoModeNew) { this.AutoModeStatus.Text = AutoModeNew; }
            if (this.Clock.Text != Time.Corrected.ToLongTimeString()) { this.Clock.Text = Time.Corrected.ToLongTimeString(); }

            if ((Playlist.Active.IsCurrent && Playlist.Active.Current.Paused) || (!Playlist.Active.IsCurrent && Playlist.Active.IsUpcoming)) { Play.Enabled = true; }
            else { Play.Enabled = false; }

            if (Playlist.Active.IsCurrent && Playlist.Active.Current.Playing && !Playlist.Active.Current.Paused) { Pause.Enabled = true; }
            else { Pause.Enabled = false; }

            if (Playlist.Active.IsCurrent && Playlist.Active.Current.Playing) { Stop.Enabled = true; }
            else { Stop.Enabled = false; }

            if (Playlist.Active.IsCurrent && Playlist.Active.Current.Playing && Playlist.Active.IsUpcoming) { Next.Enabled = true; }
            else { Next.Enabled = false; }

            if (Playlist.Active.IsCurrent)
            {
                string Name = Playlist.Active.Current.Name;
                string Title = "AutoMusic Control Panel  ::  " + Name;
                Color ForeColor = Color.FromArgb(255, 255, 192);
                string Remaining = "";
                if (Playlist.Active.Current.Running) { Remaining = Time.Format(Playlist.Active.Current.Remaining); }
                if (this.Text != Title) { this.Text = Title; }
                if (this.CurrentTrack.Text != Name) { this.CurrentTrack.Text = Name; }
                if (this.CurrentTrack.ForeColor != ForeColor) { this.CurrentTrack.ForeColor = ForeColor; }
                if (this.Remaining.Text != Remaining) { this.Remaining.Text = Remaining; }
            }
            else
            {
                string Name = "No track playing";
                string Title = "AutoMusic Control Panel";
                Color ForeColor = Color.FromArgb(192, 192, 255);
                if (this.Text != Title) { this.Text = Title; }
                if (this.CurrentTrack.Text != Name) { this.CurrentTrack.Text = Name; }
                if (this.CurrentTrack.ForeColor != ForeColor) { this.CurrentTrack.ForeColor = ForeColor; }
                this.Remaining.Text = "";
            }
            if (Playlist.Active.IsUpcoming)
            {
                string Upcoming = "Upcoming track:  " + Playlist.Active.Upcoming.Name;
                if (this.Upcoming.Text != Upcoming) { this.Upcoming.Text = Upcoming; }
            }
            else
            {
                string Upcoming = "No upcoming track";
                if (this.Upcoming.Text != Upcoming) { this.Upcoming.Text = Upcoming; }
            }
        }
        private void UpdatePlaylistMenu()
        {
            if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate { this.UpdatePlaylistMenu(); }); return; }
            this.mPlaylistSave.Enabled = (Playlist.Active.Empty ? false : true);
            if (!Playlist.Active.Saved)
            {
                this.mPlaylistCurrent.Text = "Current playlist: Unsaved";
                this.mPlaylistSave.Text = "Save playlist...";
            }
            else
            {
                this.mPlaylistCurrent.Text = "Current playlist: " + Path.GetFileName(Playlist.Active.Path);
                this.mPlaylistSave.Text = "Save a copy...";
            }
            if (PlaylistGrid.SelectedItems.Count == 0)
            {
                this.mPlaylistSelectedTracks.Text = "No tracks selected";
                this.mPlaylistDuplicate.Enabled = false;
                this.mPlaylistExclude.Enabled = false;
                this.mPlaylistRemove.Enabled = false;
            }
            else
            {
                this.mPlaylistSelectedTracks.Text = (this.PlaylistGrid.SelectedItems.Count == 1 ? "One track selected" : PlaylistGrid.SelectedItems.Count.ToString() + " tracks selected");
                this.mPlaylistDuplicate.Enabled = true;
                this.mPlaylistExclude.Enabled = true;
                this.mPlaylistRemove.Enabled = true;
                if (PlaylistGrid.SelectedItems.Count == 1)
                {
                    this.mPlaylistDuplicate.Text = "Duplicate track";
                    this.mPlaylistExclude.Text = "Skip track";
                    this.mPlaylistRemove.Text = "Remove track";
                }
                else
                {
                    this.mPlaylistDuplicate.Text = "Duplicate tracks";
                    this.mPlaylistExclude.Text = "Skip tracks";
                    this.mPlaylistRemove.Text = "Remove tracks";
                }
            }
        }
        private void SetStatus(string Status)
        {
            if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate { this.SetStatus(Status); }); return; }

            if (StatusBar.Text != Status) { StatusBar.Text = Status; }
            if (!StatusBar.Visible) { StatusBar.Visible = true; }
            if (Upcoming.Visible) { Upcoming.Visible = false; }
        }
        private void HideStatus()
        {
            if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate { this.HideStatus(); }); return; }
            if (!Upcoming.Visible) { Upcoming.Visible = true; }
            if (StatusBar.Visible) { StatusBar.Visible = false; }
        }
        private void ApplyPlaylistLineFormatting(ListViewItem Line, TrackState State)
        {
            switch (State)
            {
                case TrackState.Playing:
                case TrackState.Paused:
                case TrackState.Fading:
                    if (!Line.Font.Equals(new Font(this.Font, FontStyle.Bold))) { Line.Font = new Font(this.Font, FontStyle.Bold); }
                    break;
                default:
                    if (!Line.Font.Equals(new Font(this.Font, FontStyle.Regular))) { Line.Font = new Font(this.Font, FontStyle.Regular); }
                    break;
            }
            switch (State)
            {
                case TrackState.Aborted:
                    if (Line.ForeColor != Color.DarkRed) { Line.ForeColor = Color.DarkRed; }
                    break;
                case TrackState.Done:
                    if (Line.ForeColor != Color.DarkGreen) { Line.ForeColor = Color.DarkGreen; }
                    break;
                case TrackState.Excluded:
                    if (Line.ForeColor != Color.Gray) { Line.ForeColor = Color.Gray; }
                    break;
                case TrackState.Error:
                    if (Line.ForeColor != Color.Red) { Line.ForeColor = Color.Red; }
                    break;
                default:
                    if (Line.ForeColor != Color.Black) { Line.ForeColor = Color.Black; }
                    break;
            }
            switch (State)
            {
                case TrackState.Playing:
                case TrackState.Paused:
                    if (Line.BackColor != Color.FromArgb(255, 255, 192)) { Line.BackColor = Color.FromArgb(255, 255, 192); }
                    break;
                case TrackState.Fading:
                    if (Line.BackColor != Color.FromArgb(255, 255, 224)) { Line.BackColor = Color.FromArgb(255, 255, 224); }
                    break;
                default:
                    if (Line.BackColor != Color.White) { Line.BackColor = Color.White; }
                    break;
            }
        }

        private void ApplyScheduleLineFormatting(ListViewItem Line, bool Exclusion)
        {

        }

        //NEEDS TO BE REMOVED!!!!
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global.Context.ExitThread();
        }
        private void PlaylistGrid_Resize(object sender, EventArgs e)
        {
            PlaylistGrid.Columns[1].Width = PlaylistGrid.ClientSize.Width - PlaylistGrid.Columns[0].Width - PlaylistGrid.Columns[2].Width;
        }
        private void CurrentTrack_Resize(object sender, EventArgs e)
        {
            Remaining.Left = CurrentTrack.Left + CurrentTrack.Width + 6;
        }
        private void AutoMode_Click(object sender, EventArgs e)
        {
            if (Schedule.Enabled) { Schedule.Disable(); } else { Schedule.Enable(); }
            this.AutoMode_MouseEnter(sender, e);
            UpdateAll();
        }
        private void PlaylistGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePlaylistMenu();
        }
        private void MainForm_Resize(object sender, EventArgs e)
        {
            UpdateBars();
            this.CurrentTrack.MaximumSize = new Size(this.Width - 250, this.CurrentTrack.MaximumSize.Height);
        }
        private void SeekBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (Playlist.Active.IsCurrent && Playlist.Active.Current.Running)
            {
                int TargetPosition = (int)Math.Round((double)((double)e.X / (double)SeekBar.Width) * (double)Playlist.Active.Current.Length);
                Playlist.Active.Current.Seek(TargetPosition);
                this.UpdateBars();
            }
        }
        private void SeekBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (Playlist.Active.IsCurrent && Playlist.Active.Current.Running)
            {
                int TargetPosition = (int)Math.Round((double)((double)e.X / (double)SeekBar.Width) * (double)Playlist.Active.Current.Length);
                this.SetStatus("Seek to " + Time.Format(TargetPosition));
            }
        }
        private void SeekBar_MouseLeave(object sender, EventArgs e)
        {
            this.HideStatus();
        }
        private void SeekBarFill_MouseDown(object sender, MouseEventArgs e)
        {
            this.SeekBar_MouseDown(sender, e);
        }
        private void SeekBarFill_MouseMove(object sender, MouseEventArgs e)
        {
            this.SeekBar_MouseMove(sender, e);
        }
        private void SeekBarFill_MouseLeave(object sender, EventArgs e)
        {
            this.SeekBar_MouseLeave(sender, e);
        }
        private void VolumeBar_MouseDown(object sender, MouseEventArgs e)
        {
            int TargetVolume = (int)Math.Round((double)((double)e.X / (double)VolumeBar.Width) * (double)1000);
            Global.Volume = TargetVolume;
            this.UpdateBars();
        }
        private void VolumeBar_MouseMove(object sender, MouseEventArgs e)
        {
            int TargetVolume = (int)Math.Round((double)((double)e.X / (double)VolumeBar.Width) * (double)100);
            this.SetStatus("Set volume to " + TargetVolume.ToString() + "%");
        }
        private void VolumeBar_MouseLeave(object sender, EventArgs e)
        {
            this.HideStatus();
        }
        private void VolumeBarFill_MouseDown(object sender, MouseEventArgs e)
        {
            this.VolumeBar_MouseDown(sender, e);
        }
        private void VolumeBarFill_MouseMove(object sender, MouseEventArgs e)
        {
            this.VolumeBar_MouseMove(sender, e);
        }
        private void VolumeBarFill_MouseLeave(object sender, EventArgs e)
        {
            this.VolumeBar_MouseLeave(sender, e);
        }
        private void VolumeBarMax_MouseDown(object sender, MouseEventArgs e)
        {
            Global.Volume = 1000;
            this.UpdateBars();
        }
        private void VolumeBarMax_MouseEnter(object sender, EventArgs e)
        {
            this.SetStatus("Set volume to maximum");
        }
        private void VolumeBarMax_MouseLeave(object sender, EventArgs e)
        {
            this.HideStatus();
        }
        private void VolumePicture_MouseDown(object sender, MouseEventArgs e)
        {
            if (Global.Volume > 0)
            {
                this.VolumeBeforeMute = Global.Volume;
                Global.Volume = 0;
            }
            else
            {
                Global.Volume = this.VolumeBeforeMute;
            }
            this.UpdateBars();
        }
        private void VolumePicture_MouseEnter(object sender, EventArgs e)
        {
            this.SetStatus("Mute / unmute");
        }
        private void VolumePicture_MouseLeave(object sender, EventArgs e)
        {
            this.HideStatus();
        }
        private void PlaylistGrid_ItemsReordered(object sender, EventArgs e)
        {
            for (int i = 0; i < this.PlaylistGrid.Items.Count; i++)
            {
                Playlist.Active.Tracks.Remove((Track)this.PlaylistGrid.Items[i].Tag);
                Playlist.Active.Tracks.Insert(i, (Track)this.PlaylistGrid.Items[i].Tag);
            }
            this.UpdateAll();
        }
        private void InfoLoadBW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                for (int i = 0; i < Playlist.Active.Tracks.Count; i++)
                {
                    Track Current = Playlist.Active.Tracks[i];
                    if (!Current.InfoAvailable)
                    {
                        try
                        {
                            Current.LoadInfo();
                            this.InfoLoadBW.ReportProgress(0);
                            Thread.Sleep(500);
                        }
                        catch { }
                    }
                }
            }
            catch { }
        }
        private void InfoLoadBW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.UpdatePlaylistGrid();
        }
        private void InfoLoadBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        private void PlaylistGrid_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
        }
        private void ScrollToCurrentTrack()
        {
            this.Invoke((MethodInvoker)delegate
            {
                foreach (ListViewItem Item in PlaylistGrid.Items)
                {
                    Track T = (Track)Item.Tag;
                    if (Playlist.Active.IsCurrent && Playlist.Active.Current == T && !PlaylistMenu.Visible)
                    {
                        Item.EnsureVisible();
                    }
                }
            });
        }
        private void AutoMode_MouseEnter(object sender, EventArgs e)
        {
            this.SetStatus("Click to turn Auto mode " + (Schedule.Enabled ? "OFF" : "ON"));
        }
        private void AutoMode_MouseLeave(object sender, EventArgs e) { this.HideStatus(); }
        private void Play_MouseEnter(object sender, EventArgs e)
        {
            if (Playlist.Active.IsCurrent && Playlist.Active.Current.Paused) { this.SetStatus("RESUME playback"); }
            else { this.SetStatus("PLAY next track"); }
        }
        private void Play_MouseLeave(object sender, EventArgs e) { this.HideStatus(); }
        private void Pause_MouseEnter(object sender, EventArgs e)
        {
            this.SetStatus("PAUSE playback");
        }
        private void Pause_MouseLeave(object sender, EventArgs e) { this.HideStatus(); }
        private void Stop_MouseEnter(object sender, EventArgs e)
        {
            this.SetStatus("STOP playback");
        }
        private void Stop_MouseLeave(object sender, EventArgs e) { this.HideStatus(); }
        private void Next_MouseEnter(object sender, EventArgs e)
        {
            this.SetStatus("Fade to next track");
        }
        private void Next_MouseLeave(object sender, EventArgs e) { this.HideStatus(); }
        private void Clock_MouseEnter(object sender, EventArgs e)
        {
            if (Time.TimeOffset == 0)
            {
                this.SetStatus("Double-click to synchronize with an external time source");
            }
            else
            {
                this.SetStatus("Current time offset is " + (Time.TimeOffset > 0 ? "+" : "-") + Math.Abs(Time.TimeOffset).ToString() + " sec. Double-click to synchronize again");
            }
        }
        private void Clock_MouseLeave(object sender, EventArgs e)
        {
            this.HideStatus();
        }

        private void mPlaylistRemove_Click(object sender, EventArgs e)
        {
            List<Track> RT = new List<Track>();
            foreach (ListViewItem Item in PlaylistGrid.SelectedItems)
            {
                Track T = (Track)Item.Tag;
                if (T.State == TrackState.Queued || T.State == TrackState.Excluded || T.State == TrackState.Error || T.State == TrackState.Aborted || T.State == TrackState.Done)
                {
                    RT.Add(T);
                }
            }
            this.DetachUpdateEventHandlers();
            foreach (Track T in RT)
            {
                Playlist.Active.Remove(T);
            }
            PlaylistGrid.SelectedItems.Clear();
            UpdateAll();
            this.AttachUpdateEventHandlers();
        }
        private void mPlaylistExclude_Click(object sender, EventArgs e)
        {
            List<Track> ST = new List<Track>();
            bool CallNext = false;
            foreach (ListViewItem Item in PlaylistGrid.SelectedItems)
            {
                Track T = (Track)Item.Tag;
                if (T.State == TrackState.Queued)
                {
                    ST.Add(T);
                }
                if (T.State == TrackState.Playing || T.State == TrackState.Paused)
                {
                    CallNext = true;
                }
            }
            this.DetachUpdateEventHandlers();
            foreach (Track T in ST)
            {
                T.Excluded = true;
            }
            PlaylistGrid.SelectedItems.Clear();
            UpdateAll();
            this.AttachUpdateEventHandlers();
            if (CallNext) { Playlist.Active.Next(); }
        }

        private void mPlaylistDuplicate_Click(object sender, EventArgs e)
        {
            List<Track> DT = new List<Track>();
            foreach (ListViewItem Item in PlaylistGrid.SelectedItems)
            {
                Track T = (Track)Item.Tag;
                if (T.State != TrackState.Error)
                {
                    DT.Add(T);
                }
            }
            this.DetachUpdateEventHandlers();
            foreach (Track T in DT)
            {
                Playlist.Active.Add(T.Duplicate());
            }
            PlaylistGrid.SelectedItems.Clear();
            UpdateAll();
            this.AttachUpdateEventHandlers();
        }

        private void mPlaylistNew_Click(object sender, EventArgs e)
        {
            if (!Playlist.Active.Saved && !Playlist.Active.Empty && Tools.Question("Current playlist is not saved. Discard it and create a new one?", MessageBoxIcon.Exclamation) == DialogResult.No) { return; }
            if (Playlist.Active.IsCurrent) { Playlist.Active.Current.Stop(); }
            Playlist.Active = new Playlist();
            UpdateAll();
        }

        private void mPlaylistLoad_Click(object sender, EventArgs e)
        {
            if (!Playlist.Active.Saved && !Playlist.Active.Empty && Tools.Question("Current playlist is not saved. Discard it and load another one?", MessageBoxIcon.Exclamation) == DialogResult.No) { return; }
            LoadPlaylistDialog.Title = "Load Playlist";
            if (LoadPlaylistDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Playlist NewList = Playlist.Load(LoadPlaylistDialog.FileName);
                    if (Playlist.Active.IsCurrent) { Playlist.Active.Current.Stop(); }
                    Playlist.Active = NewList;
                    UpdateAll();
                }
                catch (FormatException) { Tools.Message("Could not load " + LoadPlaylistDialog.FileName + ": the file format cannot be recognized.", MessageBoxIcon.Error); }
                catch { Tools.Message("Could not load " + LoadPlaylistDialog.FileName + ": error opening the file.", MessageBoxIcon.Error); }
            }
        }
        private void mPlaylistSave_Click(object sender, EventArgs e)
        {
            SavePlaylistDialog.Title = (Playlist.Active.Saved ? "Save a copy" : "Save playlist");
            if (SavePlaylistDialog.ShowDialog() == DialogResult.OK)
            {
                PlaylistFormat Format = (SavePlaylistDialog.FilterIndex == 2 ? PlaylistFormat.M3U : PlaylistFormat.Internal);
                string File = SavePlaylistDialog.FileName;
                if (Playlist.Active.Saved && Path.GetFullPath(Playlist.Active.Path).ToLower() == Path.GetFullPath(File).ToLower())
                {
                    Tools.Message("You cannot save the playlist over itself. Please choose another name or location!", MessageBoxIcon.Error);
                    this.mPlaylistSave_Click(sender, e);
                    return;
                }
                if (Format == PlaylistFormat.M3U)
                {
                    if (Tools.Question("Saving the playlist in M3U format will not retain which tracks have already been played. Would you like to continue?", MessageBoxIcon.Exclamation) == DialogResult.No)
                    {
                        SavePlaylistDialog.FileName = "";
                        SavePlaylistDialog.FilterIndex = 1;
                        this.mPlaylistSave_Click(sender, e); 
                        return;
                    }
                }
                try
                {
                    if (!Playlist.Active.Saved) { Playlist.Active.Save(SavePlaylistDialog.FileName, Format); }
                    else { Playlist.Active.Export(SavePlaylistDialog.FileName, Format); }
                    UpdateAll();
                }
                catch { Tools.Message("Could not save playlist. Check that you have permission to save to the selected location!", MessageBoxIcon.Error); }
            }
        }

        private void mPlaylistAddFiles_Click(object sender, EventArgs e)
        {
            if (AddFilesDialog.ShowDialog() == DialogResult.OK)
            {
                List<Track> Tracks = new List<Track>();
                foreach (string File in AddFilesDialog.FileNames)
                {
                    Tracks.Add(new Track(File));
                }
                Playlist.Active.AddRange(Tracks);
                UpdateAll();
            }
        }

        private void mPlaylistAddFolder_Click(object sender, EventArgs e)
        {
            if (AddFolderDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<Track> Tracks = new List<Track>();
                    foreach (string Pattern in new string[] { "*.mp3", "*.wma", "*.wav" })
                    {
                        foreach (string File in Directory.GetFiles(AddFolderDialog.SelectedPath, Pattern, SearchOption.AllDirectories))
                        {
                            Tracks.Add(new Track(File));
                        }
                    }
                    Playlist.Active.AddRange(Tracks);
                    UpdateAll();
                }
                catch { Tools.Message("Could not search for files in " + AddFolderDialog.SelectedPath + ": error opening the folder", MessageBoxIcon.Error); }
            }
        }

        private void mPlaylistImport_Click(object sender, EventArgs e)
        {
            LoadPlaylistDialog.Title = "Import Playlist";
            if (LoadPlaylistDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Playlist NewList = Playlist.Load(LoadPlaylistDialog.FileName);
                    Playlist.Active.AddRange(NewList.Tracks);
                    UpdateAll();
                }
                catch (FormatException) { Tools.Message("Could not load " + LoadPlaylistDialog.FileName + ": the file format cannot be recognized.", MessageBoxIcon.Error); }
                catch { Tools.Message("Could not load " + LoadPlaylistDialog.FileName + ": error opening the file.", MessageBoxIcon.Error); }
            }
        }

        private void mPlaylistReset_Click(object sender, EventArgs e)
        {
            if (Tools.Question("This will re-queue every track in this playlist regardless of its status. Do you want to continue?", MessageBoxIcon.Exclamation) == DialogResult.No) { return; }
            foreach (Track Track in Playlist.Active.Tracks)
            {
                if (!Track.Running)
                {
                    Track.Reset();
                }
            }
        }
    }
}