using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils.Controls;

namespace Masterplan.UI
{
	internal class EncounterNoteForm : Form
	{
		private Button OKBtn;

		private Button CancelBtn;

		private Label TitleLbl;

		private TextBox TitleBox;

		private DefaultTextBox DetailsBox;

		private TabControl Pages;

		private TabPage DetailsPage;

		private StatusStrip Statusbar;

		private ToolStripStatusLabel toolStripStatusLabel1;

		private EncounterNote fNote;

		public EncounterNote Note
		{
			get
			{
				return this.fNote;
			}
		}

		public EncounterNoteForm(EncounterNote bg)
		{
			this.InitializeComponent();
			this.fNote = bg.Copy();
			this.TitleBox.Text = this.fNote.Title;
			this.DetailsBox.Text = this.fNote.Contents;
		}

		private void InitializeComponent()
		{
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.TitleLbl = new Label();
			this.TitleBox = new TextBox();
			this.DetailsBox = new DefaultTextBox();
			this.Pages = new TabControl();
			this.DetailsPage = new TabPage();
			this.Statusbar = new StatusStrip();
			this.toolStripStatusLabel1 = new ToolStripStatusLabel();
			this.Pages.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			this.Statusbar.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(308, 258);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(389, 258);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.TitleLbl.AutoSize = true;
			this.TitleLbl.Location = new Point(12, 15);
			this.TitleLbl.Name = "TitleLbl";
			this.TitleLbl.Size = new System.Drawing.Size(30, 13);
			this.TitleLbl.TabIndex = 0;
			this.TitleLbl.Text = "Title:";
			this.TitleBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.TitleBox.Location = new Point(48, 12);
			this.TitleBox.Name = "TitleBox";
			this.TitleBox.Size = new System.Drawing.Size(416, 20);
			this.TitleBox.TabIndex = 1;
			this.DetailsBox.AcceptsReturn = true;
			this.DetailsBox.AcceptsTab = true;
			this.DetailsBox.DefaultText = "(enter details here)";
			this.DetailsBox.Dock = DockStyle.Fill;
			this.DetailsBox.Location = new Point(3, 25);
			this.DetailsBox.Multiline = true;
			this.DetailsBox.Name = "DetailsBox";
			this.DetailsBox.ScrollBars = ScrollBars.Vertical;
			this.DetailsBox.Size = new System.Drawing.Size(438, 160);
			this.DetailsBox.TabIndex = 1;
			this.DetailsBox.Text = "(enter details here)";
			this.Pages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Location = new Point(12, 38);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new System.Drawing.Size(452, 214);
			this.Pages.TabIndex = 2;
			this.DetailsPage.Controls.Add(this.DetailsBox);
			this.DetailsPage.Controls.Add(this.Statusbar);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new System.Windows.Forms.Padding(3);
			this.DetailsPage.Size = new System.Drawing.Size(444, 188);
			this.DetailsPage.TabIndex = 0;
			this.DetailsPage.Text = "Details";
			this.DetailsPage.UseVisualStyleBackColor = true;
			this.Statusbar.Dock = DockStyle.Top;
			this.Statusbar.Items.AddRange(new ToolStripItem[] { this.toolStripStatusLabel1 });
			this.Statusbar.Location = new Point(3, 3);
			this.Statusbar.Name = "Statusbar";
			this.Statusbar.Size = new System.Drawing.Size(438, 22);
			this.Statusbar.SizingGrip = false;
			this.Statusbar.TabIndex = 0;
			this.Statusbar.Text = "statusStrip1";
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(202, 17);
			this.toolStripStatusLabel1.Text = "Note: HTML tags are supported here.";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(476, 293);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.TitleBox);
			base.Controls.Add(this.TitleLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MinimizeBox = false;
			base.Name = "EncounterNoteForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Encounter Note";
			this.Pages.ResumeLayout(false);
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			this.Statusbar.ResumeLayout(false);
			this.Statusbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fNote.Title = this.TitleBox.Text;
			this.fNote.Contents = (this.DetailsBox.Text != this.DetailsBox.DefaultText ? this.DetailsBox.Text : "");
		}
	}
}