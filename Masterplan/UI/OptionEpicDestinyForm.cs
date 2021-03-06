using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class OptionEpicDestinyForm : Form
	{
		private Masterplan.Data.EpicDestiny fEpicDestiny;

		private Label NameLbl;

		private TextBox NameBox;

		private TabControl Pages;

		private TabPage DetailsPage;

		private Button OKBtn;

		private Button CancelBtn;

		private TabPage LevelPage;

		private ListView LevelList;

		private ColumnHeader LevelHdr;

		private ToolStrip LevelToolbar;

		private ToolStripButton LevelEditBtn;

		private TextBox PrereqBox;

		private Label PrereqLbl;

		private TabPage ImmortalityPage;

		private TextBox ImmortalityBox;

		private TextBox QuoteBox;

		private Label QuoteLbl;

		private TextBox DetailsBox;

		public Masterplan.Data.EpicDestiny EpicDestiny
		{
			get
			{
				return this.fEpicDestiny;
			}
		}

		public LevelData SelectedLevel
		{
			get
			{
				if (this.LevelList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.LevelList.SelectedItems[0].Tag as LevelData;
			}
		}

		public OptionEpicDestinyForm(Masterplan.Data.EpicDestiny pp)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fEpicDestiny = pp.Copy();
			this.NameBox.Text = this.fEpicDestiny.Name;
			this.PrereqBox.Text = this.fEpicDestiny.Prerequisites;
			this.DetailsBox.Text = this.fEpicDestiny.Details;
			this.QuoteBox.Text = this.fEpicDestiny.Quote;
			this.ImmortalityBox.Text = this.fEpicDestiny.Immortality;
			this.update_levels();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.LevelEditBtn.Enabled = this.SelectedLevel != null;
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(OptionEpicDestinyForm));
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.Pages = new TabControl();
			this.DetailsPage = new TabPage();
			this.ImmortalityPage = new TabPage();
			this.ImmortalityBox = new TextBox();
			this.LevelPage = new TabPage();
			this.LevelList = new ListView();
			this.LevelHdr = new ColumnHeader();
			this.LevelToolbar = new ToolStrip();
			this.LevelEditBtn = new ToolStripButton();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.PrereqBox = new TextBox();
			this.PrereqLbl = new Label();
			this.QuoteBox = new TextBox();
			this.QuoteLbl = new Label();
			this.DetailsBox = new TextBox();
			this.Pages.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			this.ImmortalityPage.SuspendLayout();
			this.LevelPage.SuspendLayout();
			this.LevelToolbar.SuspendLayout();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new System.Drawing.Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.NameBox.Location = new Point(88, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(273, 20);
			this.NameBox.TabIndex = 1;
			this.Pages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Controls.Add(this.ImmortalityPage);
			this.Pages.Controls.Add(this.LevelPage);
			this.Pages.Location = new Point(12, 64);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new System.Drawing.Size(349, 265);
			this.Pages.TabIndex = 4;
			this.DetailsPage.Controls.Add(this.QuoteBox);
			this.DetailsPage.Controls.Add(this.QuoteLbl);
			this.DetailsPage.Controls.Add(this.DetailsBox);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new System.Windows.Forms.Padding(3);
			this.DetailsPage.Size = new System.Drawing.Size(341, 239);
			this.DetailsPage.TabIndex = 0;
			this.DetailsPage.Text = "Details";
			this.DetailsPage.UseVisualStyleBackColor = true;
			this.ImmortalityPage.Controls.Add(this.ImmortalityBox);
			this.ImmortalityPage.Location = new Point(4, 22);
			this.ImmortalityPage.Name = "ImmortalityPage";
			this.ImmortalityPage.Padding = new System.Windows.Forms.Padding(3);
			this.ImmortalityPage.Size = new System.Drawing.Size(341, 239);
			this.ImmortalityPage.TabIndex = 4;
			this.ImmortalityPage.Text = "Immortality";
			this.ImmortalityPage.UseVisualStyleBackColor = true;
			this.ImmortalityBox.AcceptsReturn = true;
			this.ImmortalityBox.AcceptsTab = true;
			this.ImmortalityBox.Dock = DockStyle.Fill;
			this.ImmortalityBox.Location = new Point(3, 3);
			this.ImmortalityBox.Multiline = true;
			this.ImmortalityBox.Name = "ImmortalityBox";
			this.ImmortalityBox.ScrollBars = ScrollBars.Vertical;
			this.ImmortalityBox.Size = new System.Drawing.Size(335, 233);
			this.ImmortalityBox.TabIndex = 1;
			this.LevelPage.Controls.Add(this.LevelList);
			this.LevelPage.Controls.Add(this.LevelToolbar);
			this.LevelPage.Location = new Point(4, 22);
			this.LevelPage.Name = "LevelPage";
			this.LevelPage.Padding = new System.Windows.Forms.Padding(3);
			this.LevelPage.Size = new System.Drawing.Size(341, 239);
			this.LevelPage.TabIndex = 2;
			this.LevelPage.Text = "Levels";
			this.LevelPage.UseVisualStyleBackColor = true;
			this.LevelList.Columns.AddRange(new ColumnHeader[] { this.LevelHdr });
			this.LevelList.Dock = DockStyle.Fill;
			this.LevelList.FullRowSelect = true;
			this.LevelList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.LevelList.HideSelection = false;
			this.LevelList.Location = new Point(3, 28);
			this.LevelList.MultiSelect = false;
			this.LevelList.Name = "LevelList";
			this.LevelList.Size = new System.Drawing.Size(335, 208);
			this.LevelList.TabIndex = 1;
			this.LevelList.UseCompatibleStateImageBehavior = false;
			this.LevelList.View = View.Details;
			this.LevelList.DoubleClick += new EventHandler(this.LevelEditBtn_Click);
			this.LevelHdr.Text = "Level";
			this.LevelHdr.Width = 300;
			this.LevelToolbar.Items.AddRange(new ToolStripItem[] { this.LevelEditBtn });
			this.LevelToolbar.Location = new Point(3, 3);
			this.LevelToolbar.Name = "LevelToolbar";
			this.LevelToolbar.Size = new System.Drawing.Size(335, 25);
			this.LevelToolbar.TabIndex = 0;
			this.LevelToolbar.Text = "toolStrip1";
			this.LevelEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LevelEditBtn.Image = (Image)componentResourceManager.GetObject("LevelEditBtn.Image");
			this.LevelEditBtn.ImageTransparentColor = Color.Magenta;
			this.LevelEditBtn.Name = "LevelEditBtn";
			this.LevelEditBtn.Size = new System.Drawing.Size(31, 22);
			this.LevelEditBtn.Text = "Edit";
			this.LevelEditBtn.Click += new EventHandler(this.LevelEditBtn_Click);
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(205, 335);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 5;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(286, 335);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 6;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.PrereqBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.PrereqBox.Location = new Point(88, 38);
			this.PrereqBox.Name = "PrereqBox";
			this.PrereqBox.Size = new System.Drawing.Size(273, 20);
			this.PrereqBox.TabIndex = 3;
			this.PrereqLbl.AutoSize = true;
			this.PrereqLbl.Location = new Point(12, 41);
			this.PrereqLbl.Name = "PrereqLbl";
			this.PrereqLbl.Size = new System.Drawing.Size(70, 13);
			this.PrereqLbl.TabIndex = 2;
			this.PrereqLbl.Text = "Prerequisites:";
			this.QuoteBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.QuoteBox.Location = new Point(51, 213);
			this.QuoteBox.Name = "QuoteBox";
			this.QuoteBox.Size = new System.Drawing.Size(284, 20);
			this.QuoteBox.TabIndex = 5;
			this.QuoteLbl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.QuoteLbl.AutoSize = true;
			this.QuoteLbl.Location = new Point(6, 216);
			this.QuoteLbl.Name = "QuoteLbl";
			this.QuoteLbl.Size = new System.Drawing.Size(39, 13);
			this.QuoteLbl.TabIndex = 4;
			this.QuoteLbl.Text = "Quote:";
			this.DetailsBox.AcceptsReturn = true;
			this.DetailsBox.AcceptsTab = true;
			this.DetailsBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.DetailsBox.Location = new Point(6, 6);
			this.DetailsBox.Multiline = true;
			this.DetailsBox.Name = "DetailsBox";
			this.DetailsBox.ScrollBars = ScrollBars.Vertical;
			this.DetailsBox.Size = new System.Drawing.Size(329, 201);
			this.DetailsBox.TabIndex = 3;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(373, 370);
			base.Controls.Add(this.PrereqBox);
			base.Controls.Add(this.PrereqLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "OptionEpicDestinyForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Epic Destiny";
			this.Pages.ResumeLayout(false);
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			this.ImmortalityPage.ResumeLayout(false);
			this.ImmortalityPage.PerformLayout();
			this.LevelPage.ResumeLayout(false);
			this.LevelPage.PerformLayout();
			this.LevelToolbar.ResumeLayout(false);
			this.LevelToolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void LevelEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLevel != null)
			{
				int level = this.fEpicDestiny.Levels.IndexOf(this.SelectedLevel);
				OptionLevelForm optionLevelForm = new OptionLevelForm(this.SelectedLevel, true);
				if (optionLevelForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fEpicDestiny.Levels[level] = optionLevelForm.Level;
					this.update_levels();
				}
			}
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fEpicDestiny.Name = this.NameBox.Text;
			this.fEpicDestiny.Prerequisites = this.PrereqBox.Text;
			this.fEpicDestiny.Details = this.DetailsBox.Text;
			this.QuoteBox.Text = this.fEpicDestiny.Quote;
			this.fEpicDestiny.Immortality = this.ImmortalityBox.Text;
		}

		private void update_levels()
		{
			this.LevelList.Items.Clear();
			foreach (LevelData level in this.fEpicDestiny.Levels)
			{
				ListViewItem grayText = this.LevelList.Items.Add(level.ToString());
				grayText.Tag = level;
				if (level.Count != 0)
				{
					continue;
				}
				grayText.ForeColor = SystemColors.GrayText;
			}
		}
	}
}