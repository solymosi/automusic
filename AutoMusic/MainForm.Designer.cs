namespace AutoMusic
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.PlaylistMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mPlaylistSelectedTracks = new System.Windows.Forms.ToolStripMenuItem();
            this.sPlaylist1 = new System.Windows.Forms.ToolStripSeparator();
            this.mPlaylistRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.mPlaylistExclude = new System.Windows.Forms.ToolStripMenuItem();
            this.mPlaylistDuplicate = new System.Windows.Forms.ToolStripMenuItem();
            this.sPlaylist2 = new System.Windows.Forms.ToolStripSeparator();
            this.mPlaylistCurrent = new System.Windows.Forms.ToolStripMenuItem();
            this.sPlaylist3 = new System.Windows.Forms.ToolStripSeparator();
            this.mPlaylistNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mPlaylistLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.mPlaylistSave = new System.Windows.Forms.ToolStripMenuItem();
            this.sPlaylist4 = new System.Windows.Forms.ToolStripSeparator();
            this.mPlaylistAddFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.mPlaylistAddFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.mPlaylistImport = new System.Windows.Forms.ToolStripMenuItem();
            this.sPlaylist5 = new System.Windows.Forms.ToolStripSeparator();
            this.mPlaylistReset = new System.Windows.Forms.ToolStripMenuItem();
            this.mPlaylistClear = new System.Windows.Forms.ToolStripMenuItem();
            this.mScheduleRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.ScheduleGrid = new System.Windows.Forms.ListView();
            this.RuleTypeHeader = new System.Windows.Forms.ColumnHeader();
            this.RuleFromHeader = new System.Windows.Forms.ColumnHeader();
            this.RuleToHeader = new System.Windows.Forms.ColumnHeader();
            this.ScheduleMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mScheduleCurrent = new System.Windows.Forms.ToolStripMenuItem();
            this.sSchedule1 = new System.Windows.Forms.ToolStripSeparator();
            this.mScheduleNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mScheduleLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.mScheduleSave = new System.Windows.Forms.ToolStripMenuItem();
            this.sSchedule2 = new System.Windows.Forms.ToolStripSeparator();
            this.mScheduleAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mScheduleEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mScheduleDuplicate = new System.Windows.Forms.ToolStripMenuItem();
            this.sSchedule3 = new System.Windows.Forms.ToolStripSeparator();
            this.mScheduleClear = new System.Windows.Forms.ToolStripMenuItem();
            this.TopBar = new System.Windows.Forms.Panel();
            this.VolumeBarMax = new System.Windows.Forms.Label();
            this.VolumeBarFill = new System.Windows.Forms.Label();
            this.SeekBarFill = new System.Windows.Forms.Label();
            this.StatusBar = new System.Windows.Forms.Label();
            this.AutoMode = new System.Windows.Forms.Button();
            this.AutoModeStatus = new System.Windows.Forms.Label();
            this.Play = new System.Windows.Forms.Button();
            this.VolumePicture = new System.Windows.Forms.PictureBox();
            this.Pause = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.VolumeBar = new System.Windows.Forms.Label();
            this.SeekBar = new System.Windows.Forms.Label();
            this.Next = new System.Windows.Forms.Button();
            this.Clock = new System.Windows.Forms.Label();
            this.Upcoming = new System.Windows.Forms.Label();
            this.Remaining = new System.Windows.Forms.Label();
            this.CurrentTrack = new System.Windows.Forms.Label();
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.InfoLoadBW = new System.ComponentModel.BackgroundWorker();
            this.LoadPlaylistDialog = new System.Windows.Forms.OpenFileDialog();
            this.SavePlaylistDialog = new System.Windows.Forms.SaveFileDialog();
            this.AddFilesDialog = new System.Windows.Forms.OpenFileDialog();
            this.AddFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.LoadScheduleDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveScheduleDialog = new System.Windows.Forms.SaveFileDialog();
            this.PlaylistGrid = new AutoMusic.ReorderableListView();
            this.Played = new System.Windows.Forms.ColumnHeader();
            this.Title = new System.Windows.Forms.ColumnHeader();
            this.Duration = new System.Windows.Forms.ColumnHeader();
            this.PlaylistMenu.SuspendLayout();
            this.ScheduleMenu.SuspendLayout();
            this.TopBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VolumePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // PlaylistMenu
            // 
            this.PlaylistMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mPlaylistSelectedTracks,
            this.sPlaylist1,
            this.mPlaylistRemove,
            this.mPlaylistExclude,
            this.mPlaylistDuplicate,
            this.sPlaylist2,
            this.mPlaylistCurrent,
            this.sPlaylist3,
            this.mPlaylistNew,
            this.mPlaylistLoad,
            this.mPlaylistSave,
            this.sPlaylist4,
            this.mPlaylistAddFiles,
            this.mPlaylistAddFolder,
            this.mPlaylistImport,
            this.sPlaylist5,
            this.mPlaylistReset,
            this.mPlaylistClear});
            this.PlaylistMenu.Name = "PlaylistGridMenu";
            this.PlaylistMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.PlaylistMenu.Size = new System.Drawing.Size(197, 320);
            // 
            // mPlaylistSelectedTracks
            // 
            this.mPlaylistSelectedTracks.Enabled = false;
            this.mPlaylistSelectedTracks.Name = "mPlaylistSelectedTracks";
            this.mPlaylistSelectedTracks.Size = new System.Drawing.Size(196, 22);
            this.mPlaylistSelectedTracks.Text = "No tracks selected";
            // 
            // sPlaylist1
            // 
            this.sPlaylist1.Name = "sPlaylist1";
            this.sPlaylist1.Size = new System.Drawing.Size(193, 6);
            // 
            // mPlaylistRemove
            // 
            this.mPlaylistRemove.Name = "mPlaylistRemove";
            this.mPlaylistRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.mPlaylistRemove.Size = new System.Drawing.Size(196, 22);
            this.mPlaylistRemove.Text = "Remove track";
            this.mPlaylistRemove.Click += new System.EventHandler(this.mPlaylistRemove_Click);
            // 
            // mPlaylistExclude
            // 
            this.mPlaylistExclude.Name = "mPlaylistExclude";
            this.mPlaylistExclude.ShortcutKeyDisplayString = "Minus";
            this.mPlaylistExclude.Size = new System.Drawing.Size(196, 22);
            this.mPlaylistExclude.Text = "Skip track";
            this.mPlaylistExclude.Click += new System.EventHandler(this.mPlaylistExclude_Click);
            // 
            // mPlaylistDuplicate
            // 
            this.mPlaylistDuplicate.Name = "mPlaylistDuplicate";
            this.mPlaylistDuplicate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.mPlaylistDuplicate.Size = new System.Drawing.Size(196, 22);
            this.mPlaylistDuplicate.Text = "Duplicate track";
            this.mPlaylistDuplicate.Click += new System.EventHandler(this.mPlaylistDuplicate_Click);
            // 
            // sPlaylist2
            // 
            this.sPlaylist2.Name = "sPlaylist2";
            this.sPlaylist2.Size = new System.Drawing.Size(193, 6);
            // 
            // mPlaylistCurrent
            // 
            this.mPlaylistCurrent.Enabled = false;
            this.mPlaylistCurrent.Name = "mPlaylistCurrent";
            this.mPlaylistCurrent.Size = new System.Drawing.Size(196, 22);
            this.mPlaylistCurrent.Text = "Current playlist: Unsaved";
            // 
            // sPlaylist3
            // 
            this.sPlaylist3.Name = "sPlaylist3";
            this.sPlaylist3.Size = new System.Drawing.Size(193, 6);
            // 
            // mPlaylistNew
            // 
            this.mPlaylistNew.Name = "mPlaylistNew";
            this.mPlaylistNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mPlaylistNew.Size = new System.Drawing.Size(196, 22);
            this.mPlaylistNew.Text = "New playlist";
            this.mPlaylistNew.Click += new System.EventHandler(this.mPlaylistNew_Click);
            // 
            // mPlaylistLoad
            // 
            this.mPlaylistLoad.Name = "mPlaylistLoad";
            this.mPlaylistLoad.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mPlaylistLoad.Size = new System.Drawing.Size(196, 22);
            this.mPlaylistLoad.Text = "Load playlist...";
            this.mPlaylistLoad.Click += new System.EventHandler(this.mPlaylistLoad_Click);
            // 
            // mPlaylistSave
            // 
            this.mPlaylistSave.Name = "mPlaylistSave";
            this.mPlaylistSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mPlaylistSave.Size = new System.Drawing.Size(196, 22);
            this.mPlaylistSave.Text = "Save a copy...";
            this.mPlaylistSave.Click += new System.EventHandler(this.mPlaylistSave_Click);
            // 
            // sPlaylist4
            // 
            this.sPlaylist4.Name = "sPlaylist4";
            this.sPlaylist4.Size = new System.Drawing.Size(193, 6);
            // 
            // mPlaylistAddFiles
            // 
            this.mPlaylistAddFiles.Name = "mPlaylistAddFiles";
            this.mPlaylistAddFiles.ShortcutKeyDisplayString = "Plus";
            this.mPlaylistAddFiles.Size = new System.Drawing.Size(196, 22);
            this.mPlaylistAddFiles.Text = "Add files...";
            this.mPlaylistAddFiles.Click += new System.EventHandler(this.mPlaylistAddFiles_Click);
            // 
            // mPlaylistAddFolder
            // 
            this.mPlaylistAddFolder.Name = "mPlaylistAddFolder";
            this.mPlaylistAddFolder.ShortcutKeyDisplayString = "Shift+Plus";
            this.mPlaylistAddFolder.Size = new System.Drawing.Size(196, 22);
            this.mPlaylistAddFolder.Text = "Add folder...";
            this.mPlaylistAddFolder.Click += new System.EventHandler(this.mPlaylistAddFolder_Click);
            // 
            // mPlaylistImport
            // 
            this.mPlaylistImport.Name = "mPlaylistImport";
            this.mPlaylistImport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.mPlaylistImport.Size = new System.Drawing.Size(196, 22);
            this.mPlaylistImport.Text = "Import playlist...";
            this.mPlaylistImport.Click += new System.EventHandler(this.mPlaylistImport_Click);
            // 
            // sPlaylist5
            // 
            this.sPlaylist5.Name = "sPlaylist5";
            this.sPlaylist5.Size = new System.Drawing.Size(193, 6);
            // 
            // mPlaylistReset
            // 
            this.mPlaylistReset.Name = "mPlaylistReset";
            this.mPlaylistReset.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.mPlaylistReset.Size = new System.Drawing.Size(196, 22);
            this.mPlaylistReset.Text = "Reset list";
            this.mPlaylistReset.Click += new System.EventHandler(this.mPlaylistReset_Click);
            // 
            // mPlaylistClear
            // 
            this.mPlaylistClear.Name = "mPlaylistClear";
            this.mPlaylistClear.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)));
            this.mPlaylistClear.Size = new System.Drawing.Size(196, 22);
            this.mPlaylistClear.Text = "Clear list";
            this.mPlaylistClear.Click += new System.EventHandler(this.mPlaylistClear_Click);
            // 
            // mScheduleRemove
            // 
            this.mScheduleRemove.Name = "mScheduleRemove";
            this.mScheduleRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.mScheduleRemove.Size = new System.Drawing.Size(205, 22);
            this.mScheduleRemove.Text = "Remove rule";
            this.mScheduleRemove.Click += new System.EventHandler(this.mScheduleRemove_Click);
            // 
            // ScheduleGrid
            // 
            this.ScheduleGrid.AllowDrop = true;
            this.ScheduleGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ScheduleGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ScheduleGrid.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.RuleTypeHeader,
            this.RuleFromHeader,
            this.RuleToHeader});
            this.ScheduleGrid.ContextMenuStrip = this.ScheduleMenu;
            this.ScheduleGrid.FullRowSelect = true;
            this.ScheduleGrid.GridLines = true;
            this.ScheduleGrid.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ScheduleGrid.LabelWrap = false;
            this.ScheduleGrid.Location = new System.Drawing.Point(527, 150);
            this.ScheduleGrid.Name = "ScheduleGrid";
            this.ScheduleGrid.ShowGroups = false;
            this.ScheduleGrid.ShowItemToolTips = true;
            this.ScheduleGrid.Size = new System.Drawing.Size(200, 308);
            this.ScheduleGrid.TabIndex = 18;
            this.ScheduleGrid.UseCompatibleStateImageBehavior = false;
            this.ScheduleGrid.View = System.Windows.Forms.View.Details;
            // 
            // RuleTypeHeader
            // 
            this.RuleTypeHeader.Text = "Allow";
            this.RuleTypeHeader.Width = 50;
            // 
            // RuleFromHeader
            // 
            this.RuleFromHeader.Text = "Start time";
            this.RuleFromHeader.Width = 75;
            // 
            // RuleToHeader
            // 
            this.RuleToHeader.Text = "End time";
            this.RuleToHeader.Width = 75;
            // 
            // ScheduleMenu
            // 
            this.ScheduleMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mScheduleCurrent,
            this.sSchedule1,
            this.mScheduleNew,
            this.mScheduleLoad,
            this.mScheduleSave,
            this.sSchedule2,
            this.mScheduleAdd,
            this.mScheduleEdit,
            this.mScheduleDuplicate,
            this.mScheduleRemove,
            this.sSchedule3,
            this.mScheduleClear});
            this.ScheduleMenu.Name = "PlaylistGridMenu";
            this.ScheduleMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScheduleMenu.Size = new System.Drawing.Size(206, 220);
            // 
            // mScheduleCurrent
            // 
            this.mScheduleCurrent.Enabled = false;
            this.mScheduleCurrent.Name = "mScheduleCurrent";
            this.mScheduleCurrent.Size = new System.Drawing.Size(205, 22);
            this.mScheduleCurrent.Text = "Current schedule: Unsaved";
            // 
            // sSchedule1
            // 
            this.sSchedule1.Name = "sSchedule1";
            this.sSchedule1.Size = new System.Drawing.Size(202, 6);
            // 
            // mScheduleNew
            // 
            this.mScheduleNew.Name = "mScheduleNew";
            this.mScheduleNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mScheduleNew.Size = new System.Drawing.Size(205, 22);
            this.mScheduleNew.Text = "New schedule";
            this.mScheduleNew.Click += new System.EventHandler(this.mScheduleNew_Click);
            // 
            // mScheduleLoad
            // 
            this.mScheduleLoad.Name = "mScheduleLoad";
            this.mScheduleLoad.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mScheduleLoad.Size = new System.Drawing.Size(205, 22);
            this.mScheduleLoad.Text = "Load schedule...";
            this.mScheduleLoad.Click += new System.EventHandler(this.mScheduleLoad_Click);
            // 
            // mScheduleSave
            // 
            this.mScheduleSave.Name = "mScheduleSave";
            this.mScheduleSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mScheduleSave.Size = new System.Drawing.Size(205, 22);
            this.mScheduleSave.Text = "Save a copy...";
            this.mScheduleSave.Click += new System.EventHandler(this.mScheduleSave_Click);
            // 
            // sSchedule2
            // 
            this.sSchedule2.Name = "sSchedule2";
            this.sSchedule2.Size = new System.Drawing.Size(202, 6);
            // 
            // mScheduleAdd
            // 
            this.mScheduleAdd.Name = "mScheduleAdd";
            this.mScheduleAdd.ShortcutKeyDisplayString = "Plus";
            this.mScheduleAdd.Size = new System.Drawing.Size(205, 22);
            this.mScheduleAdd.Text = "Add rule...";
            this.mScheduleAdd.Click += new System.EventHandler(this.mScheduleAdd_Click);
            // 
            // mScheduleEdit
            // 
            this.mScheduleEdit.Name = "mScheduleEdit";
            this.mScheduleEdit.ShortcutKeyDisplayString = "Enter";
            this.mScheduleEdit.Size = new System.Drawing.Size(205, 22);
            this.mScheduleEdit.Text = "Edit rule...";
            this.mScheduleEdit.Click += new System.EventHandler(this.mScheduleEdit_Click);
            // 
            // mScheduleDuplicate
            // 
            this.mScheduleDuplicate.Name = "mScheduleDuplicate";
            this.mScheduleDuplicate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.mScheduleDuplicate.Size = new System.Drawing.Size(205, 22);
            this.mScheduleDuplicate.Text = "Duplicate rule";
            this.mScheduleDuplicate.Click += new System.EventHandler(this.mScheduleDuplicate_Click);
            // 
            // sSchedule3
            // 
            this.sSchedule3.Name = "sSchedule3";
            this.sSchedule3.Size = new System.Drawing.Size(202, 6);
            // 
            // mScheduleClear
            // 
            this.mScheduleClear.Name = "mScheduleClear";
            this.mScheduleClear.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)));
            this.mScheduleClear.Size = new System.Drawing.Size(205, 22);
            this.mScheduleClear.Text = "Clear list";
            this.mScheduleClear.Click += new System.EventHandler(this.mScheduleClear_Click);
            // 
            // TopBar
            // 
            this.TopBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TopBar.Controls.Add(this.VolumeBarMax);
            this.TopBar.Controls.Add(this.VolumeBarFill);
            this.TopBar.Controls.Add(this.SeekBarFill);
            this.TopBar.Controls.Add(this.StatusBar);
            this.TopBar.Controls.Add(this.AutoMode);
            this.TopBar.Controls.Add(this.AutoModeStatus);
            this.TopBar.Controls.Add(this.Play);
            this.TopBar.Controls.Add(this.VolumePicture);
            this.TopBar.Controls.Add(this.Pause);
            this.TopBar.Controls.Add(this.Stop);
            this.TopBar.Controls.Add(this.VolumeBar);
            this.TopBar.Controls.Add(this.SeekBar);
            this.TopBar.Controls.Add(this.Next);
            this.TopBar.Controls.Add(this.Clock);
            this.TopBar.Controls.Add(this.Upcoming);
            this.TopBar.Controls.Add(this.Remaining);
            this.TopBar.Controls.Add(this.CurrentTrack);
            this.TopBar.Location = new System.Drawing.Point(0, 0);
            this.TopBar.Name = "TopBar";
            this.TopBar.Size = new System.Drawing.Size(745, 144);
            this.TopBar.TabIndex = 19;
            // 
            // VolumeBarMax
            // 
            this.VolumeBarMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.VolumeBarMax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.VolumeBarMax.Location = new System.Drawing.Point(719, 75);
            this.VolumeBarMax.Name = "VolumeBarMax";
            this.VolumeBarMax.Size = new System.Drawing.Size(8, 10);
            this.VolumeBarMax.TabIndex = 38;
            this.VolumeBarMax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VolumeBarMax.MouseLeave += new System.EventHandler(this.VolumeBarMax_MouseLeave);
            this.VolumeBarMax.MouseDown += new System.Windows.Forms.MouseEventHandler(this.VolumeBarMax_MouseDown);
            this.VolumeBarMax.MouseEnter += new System.EventHandler(this.VolumeBarMax_MouseEnter);
            // 
            // VolumeBarFill
            // 
            this.VolumeBarFill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.VolumeBarFill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.VolumeBarFill.Location = new System.Drawing.Point(626, 75);
            this.VolumeBarFill.Name = "VolumeBarFill";
            this.VolumeBarFill.Size = new System.Drawing.Size(93, 10);
            this.VolumeBarFill.TabIndex = 37;
            this.VolumeBarFill.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VolumeBarFill.MouseLeave += new System.EventHandler(this.VolumeBarFill_MouseLeave);
            this.VolumeBarFill.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VolumeBarFill_MouseMove);
            this.VolumeBarFill.MouseDown += new System.Windows.Forms.MouseEventHandler(this.VolumeBarFill_MouseDown);
            // 
            // SeekBarFill
            // 
            this.SeekBarFill.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SeekBarFill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.SeekBarFill.Location = new System.Drawing.Point(18, 75);
            this.SeekBarFill.Name = "SeekBarFill";
            this.SeekBarFill.Size = new System.Drawing.Size(550, 10);
            this.SeekBarFill.TabIndex = 36;
            this.SeekBarFill.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SeekBarFill.MouseLeave += new System.EventHandler(this.SeekBarFill_MouseLeave);
            this.SeekBarFill.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SeekBarFill_MouseMove);
            this.SeekBarFill.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SeekBarFill_MouseDown);
            // 
            // StatusBar
            // 
            this.StatusBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusBar.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.StatusBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.StatusBar.Location = new System.Drawing.Point(13, 43);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(714, 19);
            this.StatusBar.TabIndex = 35;
            this.StatusBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StatusBar.Visible = false;
            // 
            // AutoMode
            // 
            this.AutoMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AutoMode.BackColor = System.Drawing.Color.White;
            this.AutoMode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AutoMode.Image = global::AutoMusic.Properties.Resources.Exit;
            this.AutoMode.Location = new System.Drawing.Point(687, 98);
            this.AutoMode.Name = "AutoMode";
            this.AutoMode.Size = new System.Drawing.Size(40, 40);
            this.AutoMode.TabIndex = 34;
            this.AutoMode.UseVisualStyleBackColor = false;
            this.AutoMode.MouseLeave += new System.EventHandler(this.AutoMode_MouseLeave);
            this.AutoMode.Click += new System.EventHandler(this.AutoMode_Click);
            this.AutoMode.MouseEnter += new System.EventHandler(this.AutoMode_MouseEnter);
            // 
            // AutoModeStatus
            // 
            this.AutoModeStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AutoModeStatus.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.AutoModeStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.AutoModeStatus.Location = new System.Drawing.Point(548, 103);
            this.AutoModeStatus.Name = "AutoModeStatus";
            this.AutoModeStatus.Size = new System.Drawing.Size(129, 29);
            this.AutoModeStatus.TabIndex = 32;
            this.AutoModeStatus.Text = "Auto mode OFF";
            this.AutoModeStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Play
            // 
            this.Play.BackColor = System.Drawing.Color.White;
            this.Play.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Play.Image = global::AutoMusic.Properties.Resources.Play;
            this.Play.Location = new System.Drawing.Point(17, 98);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(40, 40);
            this.Play.TabIndex = 33;
            this.Play.UseVisualStyleBackColor = false;
            this.Play.MouseLeave += new System.EventHandler(this.Play_MouseLeave);
            this.Play.Click += new System.EventHandler(this.Play_Click);
            this.Play.MouseEnter += new System.EventHandler(this.Play_MouseEnter);
            // 
            // VolumePicture
            // 
            this.VolumePicture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.VolumePicture.Image = ((System.Drawing.Image)(resources.GetObject("VolumePicture.Image")));
            this.VolumePicture.Location = new System.Drawing.Point(583, 59);
            this.VolumePicture.Name = "VolumePicture";
            this.VolumePicture.Size = new System.Drawing.Size(38, 40);
            this.VolumePicture.TabIndex = 30;
            this.VolumePicture.TabStop = false;
            this.VolumePicture.MouseLeave += new System.EventHandler(this.VolumePicture_MouseLeave);
            this.VolumePicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.VolumePicture_MouseDown);
            this.VolumePicture.MouseEnter += new System.EventHandler(this.VolumePicture_MouseEnter);
            // 
            // Pause
            // 
            this.Pause.BackColor = System.Drawing.Color.White;
            this.Pause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pause.Image = global::AutoMusic.Properties.Resources.Pause;
            this.Pause.Location = new System.Drawing.Point(63, 98);
            this.Pause.Name = "Pause";
            this.Pause.Size = new System.Drawing.Size(40, 40);
            this.Pause.TabIndex = 32;
            this.Pause.UseVisualStyleBackColor = false;
            this.Pause.MouseLeave += new System.EventHandler(this.Pause_MouseLeave);
            this.Pause.Click += new System.EventHandler(this.Pause_Click);
            this.Pause.MouseEnter += new System.EventHandler(this.Pause_MouseEnter);
            // 
            // Stop
            // 
            this.Stop.BackColor = System.Drawing.Color.White;
            this.Stop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Stop.Image = global::AutoMusic.Properties.Resources.Stop;
            this.Stop.Location = new System.Drawing.Point(109, 98);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(40, 40);
            this.Stop.TabIndex = 28;
            this.Stop.UseVisualStyleBackColor = false;
            this.Stop.MouseLeave += new System.EventHandler(this.Stop_MouseLeave);
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            this.Stop.MouseEnter += new System.EventHandler(this.Stop_MouseEnter);
            // 
            // VolumeBar
            // 
            this.VolumeBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.VolumeBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.VolumeBar.Location = new System.Drawing.Point(626, 75);
            this.VolumeBar.Name = "VolumeBar";
            this.VolumeBar.Size = new System.Drawing.Size(93, 10);
            this.VolumeBar.TabIndex = 31;
            this.VolumeBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VolumeBar.MouseLeave += new System.EventHandler(this.VolumeBar_MouseLeave);
            this.VolumeBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VolumeBar_MouseMove);
            this.VolumeBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.VolumeBar_MouseDown);
            // 
            // SeekBar
            // 
            this.SeekBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SeekBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.SeekBar.Location = new System.Drawing.Point(18, 75);
            this.SeekBar.Name = "SeekBar";
            this.SeekBar.Size = new System.Drawing.Size(550, 10);
            this.SeekBar.TabIndex = 20;
            this.SeekBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SeekBar.MouseLeave += new System.EventHandler(this.SeekBar_MouseLeave);
            this.SeekBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SeekBar_MouseMove);
            this.SeekBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SeekBar_MouseDown);
            // 
            // Next
            // 
            this.Next.BackColor = System.Drawing.Color.White;
            this.Next.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Next.Image = global::AutoMusic.Properties.Resources.Next;
            this.Next.Location = new System.Drawing.Point(155, 98);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(40, 40);
            this.Next.TabIndex = 29;
            this.Next.UseVisualStyleBackColor = false;
            this.Next.MouseLeave += new System.EventHandler(this.Next_MouseLeave);
            this.Next.Click += new System.EventHandler(this.Next_Click);
            this.Next.MouseEnter += new System.EventHandler(this.Next_MouseEnter);
            // 
            // Clock
            // 
            this.Clock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Clock.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Clock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Clock.Location = new System.Drawing.Point(617, 10);
            this.Clock.Name = "Clock";
            this.Clock.Size = new System.Drawing.Size(118, 29);
            this.Clock.TabIndex = 19;
            this.Clock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Clock.MouseLeave += new System.EventHandler(this.Clock_MouseLeave);
            this.Clock.DoubleClick += new System.EventHandler(this.Clock_DoubleClick);
            this.Clock.MouseEnter += new System.EventHandler(this.Clock_MouseEnter);
            // 
            // Upcoming
            // 
            this.Upcoming.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Upcoming.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Upcoming.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Upcoming.Location = new System.Drawing.Point(13, 43);
            this.Upcoming.Name = "Upcoming";
            this.Upcoming.Size = new System.Drawing.Size(714, 19);
            this.Upcoming.TabIndex = 18;
            this.Upcoming.Text = "Loading player information...";
            this.Upcoming.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Remaining
            // 
            this.Remaining.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Remaining.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Remaining.Location = new System.Drawing.Point(127, 10);
            this.Remaining.Name = "Remaining";
            this.Remaining.Size = new System.Drawing.Size(90, 29);
            this.Remaining.TabIndex = 17;
            this.Remaining.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CurrentTrack
            // 
            this.CurrentTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CurrentTrack.AutoSize = true;
            this.CurrentTrack.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CurrentTrack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.CurrentTrack.Location = new System.Drawing.Point(12, 10);
            this.CurrentTrack.MaximumSize = new System.Drawing.Size(500, 29);
            this.CurrentTrack.Name = "CurrentTrack";
            this.CurrentTrack.Size = new System.Drawing.Size(109, 29);
            this.CurrentTrack.TabIndex = 16;
            this.CurrentTrack.Text = "Loading...";
            this.CurrentTrack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CurrentTrack.Resize += new System.EventHandler(this.CurrentTrack_Resize);
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Enabled = true;
            this.UpdateTimer.Interval = 500;
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // InfoLoadBW
            // 
            this.InfoLoadBW.WorkerReportsProgress = true;
            this.InfoLoadBW.WorkerSupportsCancellation = true;
            this.InfoLoadBW.DoWork += new System.ComponentModel.DoWorkEventHandler(this.InfoLoadBW_DoWork);
            this.InfoLoadBW.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.InfoLoadBW_RunWorkerCompleted);
            this.InfoLoadBW.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.InfoLoadBW_ProgressChanged);
            // 
            // LoadPlaylistDialog
            // 
            this.LoadPlaylistDialog.DefaultExt = "amp";
            this.LoadPlaylistDialog.Filter = "All supported files|*.amp;*.m3u|AutoMusic playlists|*.amp|M3U playlists|*.m3u|All" +
                " files|*.*";
            // 
            // SavePlaylistDialog
            // 
            this.SavePlaylistDialog.AddExtension = false;
            this.SavePlaylistDialog.Filter = "AutoMusic playlist|*.amp|M3U playlist|*.m3u|All files|*.*";
            this.SavePlaylistDialog.Title = "Save playlist";
            // 
            // AddFilesDialog
            // 
            this.AddFilesDialog.DefaultExt = "mp3";
            this.AddFilesDialog.Filter = "All supported files|*.mp3;*.wma;*.wav|MP3 files|*.mp3|Windows Media files|*.wma|M" +
                "icrosoft WAV files|*.wav|All files|*.*";
            this.AddFilesDialog.Multiselect = true;
            this.AddFilesDialog.Title = "Add files";
            // 
            // AddFolderDialog
            // 
            this.AddFolderDialog.Description = "Select a folder to add to the playlist. All supported files within the folder and" +
                " its subfolders will be added.";
            this.AddFolderDialog.ShowNewFolderButton = false;
            // 
            // LoadScheduleDialog
            // 
            this.LoadScheduleDialog.DefaultExt = "ams";
            this.LoadScheduleDialog.Filter = "AutoMusic schedule files|*.ams|All files|*.*";
            this.LoadScheduleDialog.Title = "Load schedule";
            // 
            // SaveScheduleDialog
            // 
            this.SaveScheduleDialog.DefaultExt = "ams";
            this.SaveScheduleDialog.Filter = "AutoMusic schedule|*.ams|All files|*.*";
            this.SaveScheduleDialog.Title = "Save schedule";
            // 
            // PlaylistGrid
            // 
            this.PlaylistGrid.AllowDrop = true;
            this.PlaylistGrid.AllowRowReorder = true;
            this.PlaylistGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PlaylistGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PlaylistGrid.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Played,
            this.Title,
            this.Duration});
            this.PlaylistGrid.ContextMenuStrip = this.PlaylistMenu;
            this.PlaylistGrid.FullRowSelect = true;
            this.PlaylistGrid.GridLines = true;
            this.PlaylistGrid.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.PlaylistGrid.HideSelection = false;
            this.PlaylistGrid.LabelWrap = false;
            this.PlaylistGrid.Location = new System.Drawing.Point(17, 150);
            this.PlaylistGrid.Name = "PlaylistGrid";
            this.PlaylistGrid.ShowGroups = false;
            this.PlaylistGrid.Size = new System.Drawing.Size(498, 308);
            this.PlaylistGrid.TabIndex = 14;
            this.PlaylistGrid.UseCompatibleStateImageBehavior = false;
            this.PlaylistGrid.View = System.Windows.Forms.View.Details;
            this.PlaylistGrid.Resize += new System.EventHandler(this.PlaylistGrid_Resize);
            this.PlaylistGrid.SelectedIndexChanged += new System.EventHandler(this.PlaylistGrid_SelectedIndexChanged);
            this.PlaylistGrid.DragEnter += new System.Windows.Forms.DragEventHandler(this.PlaylistGrid_DragEnter);
            this.PlaylistGrid.ItemsReordered += new AutoMusic.ReorderableListView.ReorderDelegate(this.PlaylistGrid_ItemsReordered);
            // 
            // Played
            // 
            this.Played.Text = "";
            this.Played.Width = 50;
            // 
            // Title
            // 
            this.Title.Text = "Track title";
            this.Title.Width = 200;
            // 
            // Duration
            // 
            this.Duration.Text = "Duration";
            this.Duration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Duration.Width = 100;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(79)))), ((int)(((byte)(124)))));
            this.ClientSize = new System.Drawing.Size(742, 473);
            this.Controls.Add(this.TopBar);
            this.Controls.Add(this.PlaylistGrid);
            this.Controls.Add(this.ScheduleGrid);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(750, 500);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AutoMusic Control Panel";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.PlaylistMenu.ResumeLayout(false);
            this.ScheduleMenu.ResumeLayout(false);
            this.TopBar.ResumeLayout(false);
            this.TopBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VolumePicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader Played;
        private System.Windows.Forms.ColumnHeader Title;
        private System.Windows.Forms.ColumnHeader Duration;
        private System.Windows.Forms.ListView ScheduleGrid;
        private System.Windows.Forms.ColumnHeader RuleTypeHeader;
        private System.Windows.Forms.ColumnHeader RuleFromHeader;
        private System.Windows.Forms.ColumnHeader RuleToHeader;
        private System.Windows.Forms.Panel TopBar;
        private System.Windows.Forms.Label CurrentTrack;
        private System.Windows.Forms.Label Upcoming;
        private System.Windows.Forms.Label Remaining;
        private System.Windows.Forms.Label Clock;
        private System.Windows.Forms.ContextMenuStrip PlaylistMenu;
        private System.Windows.Forms.ToolStripMenuItem mPlaylistDuplicate;
        private System.Windows.Forms.ToolStripMenuItem mPlaylistRemove;
        private System.Windows.Forms.ToolStripSeparator sPlaylist5;
        private System.Windows.Forms.ToolStripMenuItem mPlaylistClear;
        private System.Windows.Forms.ToolStripMenuItem mPlaylistReset;
        private System.Windows.Forms.ToolStripMenuItem mPlaylistAddFiles;
        private System.Windows.Forms.ToolStripMenuItem mPlaylistImport;
        private System.Windows.Forms.ToolStripSeparator sPlaylist2;
        private System.Windows.Forms.ContextMenuStrip ScheduleMenu;
        private System.Windows.Forms.ToolStripMenuItem mScheduleAdd;
        private System.Windows.Forms.ToolStripMenuItem mScheduleRemove;
        private System.Windows.Forms.ToolStripSeparator sSchedule3;
        private System.Windows.Forms.ToolStripMenuItem mScheduleClear;
        private System.Windows.Forms.ToolStripMenuItem mScheduleCurrent;
        private System.Windows.Forms.ToolStripSeparator sSchedule1;
        private System.Windows.Forms.ToolStripMenuItem mScheduleNew;
        private System.Windows.Forms.ToolStripMenuItem mScheduleLoad;
        private System.Windows.Forms.ToolStripMenuItem mScheduleSave;
        private System.Windows.Forms.ToolStripSeparator sSchedule2;
        private System.Windows.Forms.ToolStripMenuItem mScheduleEdit;
        private System.Windows.Forms.ToolStripMenuItem mScheduleDuplicate;
        private System.Windows.Forms.Timer UpdateTimer;
        private System.Windows.Forms.ToolStripMenuItem mPlaylistCurrent;
        private System.Windows.Forms.ToolStripSeparator sPlaylist4;
        private System.Windows.Forms.ToolStripMenuItem mPlaylistNew;
        private System.Windows.Forms.ToolStripMenuItem mPlaylistLoad;
        private System.Windows.Forms.ToolStripMenuItem mPlaylistSave;
        private System.Windows.Forms.ToolStripMenuItem mPlaylistAddFolder;
        private System.Windows.Forms.Button Pause;
        private System.Windows.Forms.PictureBox VolumePicture;
        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.Label SeekBar;
        private System.Windows.Forms.Label VolumeBar;
        private System.Windows.Forms.Label AutoModeStatus;
        private System.Windows.Forms.Button AutoMode;
        private System.Windows.Forms.ToolStripSeparator sPlaylist3;
        private System.Windows.Forms.Label StatusBar;
        private System.Windows.Forms.ToolStripMenuItem mPlaylistExclude;
        private System.Windows.Forms.Label SeekBarFill;
        private ReorderableListView PlaylistGrid;
        private System.Windows.Forms.Label VolumeBarFill;
        private System.Windows.Forms.Label VolumeBarMax;
        private System.ComponentModel.BackgroundWorker InfoLoadBW;
        private System.Windows.Forms.ToolStripMenuItem mPlaylistSelectedTracks;
        private System.Windows.Forms.ToolStripSeparator sPlaylist1;
        private System.Windows.Forms.OpenFileDialog LoadPlaylistDialog;
        private System.Windows.Forms.SaveFileDialog SavePlaylistDialog;
        private System.Windows.Forms.OpenFileDialog AddFilesDialog;
        private System.Windows.Forms.FolderBrowserDialog AddFolderDialog;
        private System.Windows.Forms.OpenFileDialog LoadScheduleDialog;
        private System.Windows.Forms.SaveFileDialog SaveScheduleDialog;

    }
}

