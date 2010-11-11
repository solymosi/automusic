using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoMusic
{
    public partial class RuleForm : Form
    {
        public RuleForm()
        {
            InitializeComponent();
        }

        private void Sync_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void SyncForm_Load(object sender, EventArgs e)
        {
            FirstHour.Value = Time.Corrected.Hour;
            FirstMinute.Value = Time.Corrected.Minute + 1;
        }

        public void SetFields(bool Edit, bool Disallow, Time Start, Time End)
        {
            this.Text = Edit ? "Edit rule" : "Add rule";
            this.Save.Text = Edit ? "Save changes" : "Add rule";
            this.Disallow.Checked = Disallow;
            this.FirstHour.Value = Start.Hour;
            this.FirstMinute.Value = Start.Minute;
            this.SecondHour.Value = End.Hour;
            this.SecondMinute.Value = End.Minute;
        }
    }
}
