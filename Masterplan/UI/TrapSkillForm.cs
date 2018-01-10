using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class TrapSkillForm : Form
	{
		private Button CancelBtn;

		private Button OKBtn;

		private Label DCLbl;

		private NumericUpDown DCBox;

		private TabControl Pages;

		private TabPage DetailsPage;

		private TextBox DetailsBox;

		private Label SkillLbl;

		private ComboBox SkillBox;

		private TabPage AdvicePage;

		private ListView AdviceList;

		private ColumnHeader AdviceHdr;

		private ColumnHeader InfoHdr;

		private CheckBox DCBtn;

		private TrapSkillData fSkillData;

		private int fLevel = 1;

		public TrapSkillData SkillData
		{
			get
			{
				return this.fSkillData;
			}
		}

		public TrapSkillForm(TrapSkillData tsd, int level)
		{
			this.InitializeComponent();
			foreach (string skillName in Skills.GetSkillNames())
			{
				this.SkillBox.Items.Add(skillName);
			}
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fSkillData = tsd.Copy();
			this.fLevel = level;
			this.SkillBox.Text = this.fSkillData.SkillName;
			this.DCBtn.Checked = this.fSkillData.DC != 0;
			this.DCBox.Value = this.fSkillData.DC;
			this.DetailsBox.Text = this.fSkillData.Details;
			this.update_advice();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.DCLbl.Enabled = this.DCBtn.Checked;
			this.DCBox.Enabled = this.DCBtn.Checked;
			this.OKBtn.Enabled = (this.SkillBox.Text == "" ? false : this.DetailsBox.Text != "");
		}

		private void InitializeComponent()
		{
			ListViewGroup listViewGroup = new ListViewGroup("Skill DCs", HorizontalAlignment.Left);
			this.CancelBtn = new Button();
			this.OKBtn = new Button();
			this.DCLbl = new Label();
			this.DCBox = new NumericUpDown();
			this.Pages = new TabControl();
			this.DetailsPage = new TabPage();
			this.DetailsBox = new TextBox();
			this.AdvicePage = new TabPage();
			this.AdviceList = new ListView();
			this.AdviceHdr = new ColumnHeader();
			this.InfoHdr = new ColumnHeader();
			this.SkillLbl = new Label();
			this.SkillBox = new ComboBox();
			this.DCBtn = new CheckBox();
			((ISupportInitialize)this.DCBox).BeginInit();
			this.Pages.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			this.AdvicePage.SuspendLayout();
			base.SuspendLayout();
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(213, 298);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 7;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(132, 298);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 6;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.DCLbl.AutoSize = true;
			this.DCLbl.Location = new Point(12, 72);
			this.DCLbl.Name = "DCLbl";
			this.DCLbl.Size = new System.Drawing.Size(25, 13);
			this.DCLbl.TabIndex = 3;
			this.DCLbl.Text = "DC:";
			this.DCBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.DCBox.Location = new Point(47, 70);
			this.DCBox.Name = "DCBox";
			this.DCBox.Size = new System.Drawing.Size(241, 20);
			this.DCBox.TabIndex = 4;
			this.Pages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Controls.Add(this.AdvicePage);
			this.Pages.Location = new Point(12, 109);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new System.Drawing.Size(276, 183);
			this.Pages.TabIndex = 5;
			this.DetailsPage.Controls.Add(this.DetailsBox);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new System.Windows.Forms.Padding(3);
			this.DetailsPage.Size = new System.Drawing.Size(268, 157);
			this.DetailsPage.TabIndex = 0;
			this.DetailsPage.Text = "Details";
			this.DetailsPage.UseVisualStyleBackColor = true;
			this.DetailsBox.AcceptsReturn = true;
			this.DetailsBox.AcceptsTab = true;
			this.DetailsBox.Dock = DockStyle.Fill;
			this.DetailsBox.Location = new Point(3, 3);
			this.DetailsBox.Multiline = true;
			this.DetailsBox.Name = "DetailsBox";
			this.DetailsBox.ScrollBars = ScrollBars.Vertical;
			this.DetailsBox.Size = new System.Drawing.Size(262, 151);
			this.DetailsBox.TabIndex = 0;
			this.AdvicePage.Controls.Add(this.AdviceList);
			this.AdvicePage.Location = new Point(4, 22);
			this.AdvicePage.Name = "AdvicePage";
			this.AdvicePage.Padding = new System.Windows.Forms.Padding(3);
			this.AdvicePage.Size = new System.Drawing.Size(268, 178);
			this.AdvicePage.TabIndex = 1;
			this.AdvicePage.Text = "Advice";
			this.AdvicePage.UseVisualStyleBackColor = true;
			ListView.ColumnHeaderCollection columns = this.AdviceList.Columns;
			ColumnHeader[] adviceHdr = new ColumnHeader[] { this.AdviceHdr, this.InfoHdr };
			columns.AddRange(adviceHdr);
			this.AdviceList.Dock = DockStyle.Fill;
			this.AdviceList.FullRowSelect = true;
			listViewGroup.Header = "Skill DCs";
			listViewGroup.Name = "listViewGroup1";
			this.AdviceList.Groups.AddRange(new ListViewGroup[] { listViewGroup });
			this.AdviceList.HeaderStyle = ColumnHeaderStyle.None;
			this.AdviceList.HideSelection = false;
			this.AdviceList.Location = new Point(3, 3);
			this.AdviceList.MultiSelect = false;
			this.AdviceList.Name = "AdviceList";
			this.AdviceList.Size = new System.Drawing.Size(262, 172);
			this.AdviceList.TabIndex = 2;
			this.AdviceList.UseCompatibleStateImageBehavior = false;
			this.AdviceList.View = View.Details;
			this.AdviceHdr.Text = "Advice";
			this.AdviceHdr.Width = 150;
			this.InfoHdr.Text = "Information";
			this.InfoHdr.Width = 100;
			this.SkillLbl.AutoSize = true;
			this.SkillLbl.Location = new Point(12, 15);
			this.SkillLbl.Name = "SkillLbl";
			this.SkillLbl.Size = new System.Drawing.Size(29, 13);
			this.SkillLbl.TabIndex = 0;
			this.SkillLbl.Text = "Skill:";
			this.SkillBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.SkillBox.AutoCompleteMode = AutoCompleteMode.Append;
			this.SkillBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.SkillBox.FormattingEnabled = true;
			this.SkillBox.Location = new Point(47, 12);
			this.SkillBox.Name = "SkillBox";
			this.SkillBox.Size = new System.Drawing.Size(241, 21);
			this.SkillBox.TabIndex = 1;
			this.DCBtn.AutoSize = true;
			this.DCBtn.Location = new Point(12, 47);
			this.DCBtn.Name = "DCBtn";
			this.DCBtn.Size = new System.Drawing.Size(148, 17);
			this.DCBtn.TabIndex = 2;
			this.DCBtn.Text = "This skill requires a check";
			this.DCBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(300, 333);
			base.Controls.Add(this.DCBtn);
			base.Controls.Add(this.SkillBox);
			base.Controls.Add(this.SkillLbl);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.DCBox);
			base.Controls.Add(this.DCLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TrapSkillForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Skill Data";
			((ISupportInitialize)this.DCBox).EndInit();
			this.Pages.ResumeLayout(false);
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			this.AdvicePage.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fSkillData.SkillName = this.SkillBox.Text;
			if (!this.DCBtn.Checked)
			{
				this.fSkillData.DC = 0;
			}
			else
			{
				this.fSkillData.DC = (int)this.DCBox.Value;
			}
			this.fSkillData.Details = this.DetailsBox.Text;
		}

		private void update_advice()
		{
			ListViewItem item = this.AdviceList.Items.Add("Skill DC (easy)");
			ListViewItem.ListViewSubItemCollection subItems = item.SubItems;
			int skillDC = AI.GetSkillDC(Difficulty.Easy, this.fLevel);
			subItems.Add(skillDC.ToString());
			item.Group = this.AdviceList.Groups[0];
			ListViewItem listViewItem = this.AdviceList.Items.Add("Skill DC (moderate)");
			ListViewItem.ListViewSubItemCollection listViewSubItemCollections = listViewItem.SubItems;
			int num = AI.GetSkillDC(Difficulty.Moderate, this.fLevel);
			listViewSubItemCollections.Add(num.ToString());
			listViewItem.Group = this.AdviceList.Groups[0];
			ListViewItem item1 = this.AdviceList.Items.Add("Skill DC (hard)");
			ListViewItem.ListViewSubItemCollection subItems1 = item1.SubItems;
			int skillDC1 = AI.GetSkillDC(Difficulty.Hard, this.fLevel);
			subItems1.Add(skillDC1.ToString());
			item1.Group = this.AdviceList.Groups[0];
		}
	}
}