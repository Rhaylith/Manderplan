using Masterplan.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class OptionPowerForm : Form
	{
		private Label NameLbl;

		private TextBox NameBox;

		private TabControl Pages;

		private TabPage ReadAloudPage;

		private TextBox ReadAloudBox;

		private Button OKBtn;

		private Button CancelBtn;

		private Label TypeLbl;

		private Label ActionLbl;

		private TextBox KeywordBox;

		private Label KeywordLbl;

		private ComboBox TypeBox;

		private ComboBox ActionBox;

		private TabPage SectionPage;

		private ListView SectionList;

		private ColumnHeader SectionHdr;

		private ToolStrip SectionToolbar;

		private ToolStripButton SectionAddBtn;

		private ToolStripButton SectionRemoveBtn;

		private ToolStripButton SectionEditBtn;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton SectionUpBtn;

		private ToolStripButton SectionDownBtn;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton SectionLeftBtn;

		private ToolStripButton SectionRightBtn;

		private Label RangeLbl;

		private ComboBox RangeBox;

		private TabPage HeaderPage;

		private PlayerPower fPower;

		public PlayerPower Power
		{
			get
			{
				return this.fPower;
			}
		}

		public PlayerPowerSection SelectedSection
		{
			get
			{
				if (this.SectionList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.SectionList.SelectedItems[0].Tag as PlayerPowerSection;
			}
		}

		public OptionPowerForm(PlayerPower power)
		{
			this.InitializeComponent();
			foreach (PlayerPowerType value in Enum.GetValues(typeof(PlayerPowerType)))
			{
				this.TypeBox.Items.Add(value);
			}
			foreach (ActionType actionType in Enum.GetValues(typeof(ActionType)))
			{
				this.ActionBox.Items.Add(actionType);
			}
			this.RangeBox.Items.Add("Personal");
			this.RangeBox.Items.Add("Melee touch");
			this.RangeBox.Items.Add("Melee 1");
			this.RangeBox.Items.Add("Melee weapon");
			this.RangeBox.Items.Add("Ranged 10");
			this.RangeBox.Items.Add("Ranged weapon");
			this.RangeBox.Items.Add("Ranged sight");
			this.RangeBox.Items.Add("Close burst 1");
			this.RangeBox.Items.Add("Close blast 3");
			this.RangeBox.Items.Add("Area burst 3 within 10");
			this.RangeBox.Items.Add("Area wall 3 within 10");
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fPower = power.Copy();
			this.NameBox.Text = this.fPower.Name;
			this.TypeBox.SelectedItem = this.fPower.Type;
			this.ActionBox.SelectedItem = this.fPower.Action;
			this.KeywordBox.Text = this.fPower.Keywords;
			this.RangeBox.Text = this.fPower.Range;
			this.ReadAloudBox.Text = this.fPower.ReadAloud;
			this.update_sections();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			int num = this.fPower.Sections.IndexOf(this.SelectedSection);
			this.SectionRemoveBtn.Enabled = this.SelectedSection != null;
			this.SectionEditBtn.Enabled = this.SelectedSection != null;
			this.SectionUpBtn.Enabled = (this.SelectedSection == null ? false : num != 0);
			this.SectionDownBtn.Enabled = (this.SelectedSection == null ? false : num != this.fPower.Sections.Count - 1);
			this.SectionLeftBtn.Enabled = (this.SelectedSection == null ? false : this.SelectedSection.Indent > 0);
			this.SectionRightBtn.Enabled = false;
			if (num > 0)
			{
				PlayerPowerSection item = this.fPower.Sections[num - 1];
				this.SectionRightBtn.Enabled = (this.SelectedSection == null ? false : this.SelectedSection.Indent <= item.Indent);
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(OptionPowerForm));
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.Pages = new TabControl();
			this.HeaderPage = new TabPage();
			this.RangeBox = new ComboBox();
			this.RangeLbl = new Label();
			this.ActionBox = new ComboBox();
			this.TypeLbl = new Label();
			this.TypeBox = new ComboBox();
			this.ActionLbl = new Label();
			this.KeywordBox = new TextBox();
			this.KeywordLbl = new Label();
			this.ReadAloudPage = new TabPage();
			this.ReadAloudBox = new TextBox();
			this.SectionPage = new TabPage();
			this.SectionList = new ListView();
			this.SectionHdr = new ColumnHeader();
			this.SectionToolbar = new ToolStrip();
			this.SectionAddBtn = new ToolStripButton();
			this.SectionRemoveBtn = new ToolStripButton();
			this.SectionEditBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.SectionUpBtn = new ToolStripButton();
			this.SectionDownBtn = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.SectionLeftBtn = new ToolStripButton();
			this.SectionRightBtn = new ToolStripButton();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.Pages.SuspendLayout();
			this.HeaderPage.SuspendLayout();
			this.ReadAloudPage.SuspendLayout();
			this.SectionPage.SuspendLayout();
			this.SectionToolbar.SuspendLayout();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(6, 9);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new System.Drawing.Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.NameBox.Location = new Point(68, 6);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(259, 20);
			this.NameBox.TabIndex = 1;
			this.Pages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Pages.Controls.Add(this.HeaderPage);
			this.Pages.Controls.Add(this.ReadAloudPage);
			this.Pages.Controls.Add(this.SectionPage);
			this.Pages.Location = new Point(12, 12);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new System.Drawing.Size(341, 216);
			this.Pages.TabIndex = 10;
			this.HeaderPage.Controls.Add(this.RangeBox);
			this.HeaderPage.Controls.Add(this.NameBox);
			this.HeaderPage.Controls.Add(this.RangeLbl);
			this.HeaderPage.Controls.Add(this.NameLbl);
			this.HeaderPage.Controls.Add(this.ActionBox);
			this.HeaderPage.Controls.Add(this.TypeLbl);
			this.HeaderPage.Controls.Add(this.TypeBox);
			this.HeaderPage.Controls.Add(this.ActionLbl);
			this.HeaderPage.Controls.Add(this.KeywordBox);
			this.HeaderPage.Controls.Add(this.KeywordLbl);
			this.HeaderPage.Location = new Point(4, 22);
			this.HeaderPage.Name = "HeaderPage";
			this.HeaderPage.Padding = new System.Windows.Forms.Padding(3);
			this.HeaderPage.Size = new System.Drawing.Size(333, 190);
			this.HeaderPage.TabIndex = 2;
			this.HeaderPage.Text = "Information";
			this.HeaderPage.UseVisualStyleBackColor = true;
			this.RangeBox.FormattingEnabled = true;
			this.RangeBox.Location = new Point(68, 113);
			this.RangeBox.Name = "RangeBox";
			this.RangeBox.Size = new System.Drawing.Size(259, 21);
			this.RangeBox.Sorted = true;
			this.RangeBox.TabIndex = 9;
			this.RangeLbl.AutoSize = true;
			this.RangeLbl.Location = new Point(6, 116);
			this.RangeLbl.Name = "RangeLbl";
			this.RangeLbl.Size = new System.Drawing.Size(42, 13);
			this.RangeLbl.TabIndex = 8;
			this.RangeLbl.Text = "Range:";
			this.ActionBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.ActionBox.FormattingEnabled = true;
			this.ActionBox.Location = new Point(68, 86);
			this.ActionBox.Name = "ActionBox";
			this.ActionBox.Size = new System.Drawing.Size(259, 21);
			this.ActionBox.TabIndex = 7;
			this.TypeLbl.AutoSize = true;
			this.TypeLbl.Location = new Point(6, 35);
			this.TypeLbl.Name = "TypeLbl";
			this.TypeLbl.Size = new System.Drawing.Size(34, 13);
			this.TypeLbl.TabIndex = 2;
			this.TypeLbl.Text = "Type:";
			this.TypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.TypeBox.FormattingEnabled = true;
			this.TypeBox.Location = new Point(68, 32);
			this.TypeBox.Name = "TypeBox";
			this.TypeBox.Size = new System.Drawing.Size(259, 21);
			this.TypeBox.TabIndex = 3;
			this.ActionLbl.AutoSize = true;
			this.ActionLbl.Location = new Point(6, 89);
			this.ActionLbl.Name = "ActionLbl";
			this.ActionLbl.Size = new System.Drawing.Size(40, 13);
			this.ActionLbl.TabIndex = 6;
			this.ActionLbl.Text = "Action:";
			this.KeywordBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.KeywordBox.Location = new Point(68, 59);
			this.KeywordBox.Name = "KeywordBox";
			this.KeywordBox.Size = new System.Drawing.Size(259, 20);
			this.KeywordBox.TabIndex = 5;
			this.KeywordLbl.AutoSize = true;
			this.KeywordLbl.Location = new Point(6, 62);
			this.KeywordLbl.Name = "KeywordLbl";
			this.KeywordLbl.Size = new System.Drawing.Size(56, 13);
			this.KeywordLbl.TabIndex = 4;
			this.KeywordLbl.Text = "Keywords:";
			this.ReadAloudPage.Controls.Add(this.ReadAloudBox);
			this.ReadAloudPage.Location = new Point(4, 22);
			this.ReadAloudPage.Name = "ReadAloudPage";
			this.ReadAloudPage.Padding = new System.Windows.Forms.Padding(3);
			this.ReadAloudPage.Size = new System.Drawing.Size(333, 190);
			this.ReadAloudPage.TabIndex = 0;
			this.ReadAloudPage.Text = "Read-Aloud Text";
			this.ReadAloudPage.UseVisualStyleBackColor = true;
			this.ReadAloudBox.AcceptsReturn = true;
			this.ReadAloudBox.AcceptsTab = true;
			this.ReadAloudBox.Dock = DockStyle.Fill;
			this.ReadAloudBox.Location = new Point(3, 3);
			this.ReadAloudBox.Multiline = true;
			this.ReadAloudBox.Name = "ReadAloudBox";
			this.ReadAloudBox.ScrollBars = ScrollBars.Vertical;
			this.ReadAloudBox.Size = new System.Drawing.Size(327, 184);
			this.ReadAloudBox.TabIndex = 0;
			this.SectionPage.Controls.Add(this.SectionList);
			this.SectionPage.Controls.Add(this.SectionToolbar);
			this.SectionPage.Location = new Point(4, 22);
			this.SectionPage.Name = "SectionPage";
			this.SectionPage.Padding = new System.Windows.Forms.Padding(3);
			this.SectionPage.Size = new System.Drawing.Size(333, 190);
			this.SectionPage.TabIndex = 1;
			this.SectionPage.Text = "Sections";
			this.SectionPage.UseVisualStyleBackColor = true;
			this.SectionList.Columns.AddRange(new ColumnHeader[] { this.SectionHdr });
			this.SectionList.Dock = DockStyle.Fill;
			this.SectionList.FullRowSelect = true;
			this.SectionList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.SectionList.HideSelection = false;
			this.SectionList.Location = new Point(3, 28);
			this.SectionList.MultiSelect = false;
			this.SectionList.Name = "SectionList";
			this.SectionList.Size = new System.Drawing.Size(327, 159);
			this.SectionList.TabIndex = 1;
			this.SectionList.UseCompatibleStateImageBehavior = false;
			this.SectionList.View = View.Details;
			this.SectionHdr.Text = "Section";
			this.SectionHdr.Width = 300;
			ToolStripItemCollection items = this.SectionToolbar.Items;
			ToolStripItem[] sectionAddBtn = new ToolStripItem[] { this.SectionAddBtn, this.SectionRemoveBtn, this.SectionEditBtn, this.toolStripSeparator1, this.SectionUpBtn, this.SectionDownBtn, this.toolStripSeparator2, this.SectionLeftBtn, this.SectionRightBtn };
			items.AddRange(sectionAddBtn);
			this.SectionToolbar.Location = new Point(3, 3);
			this.SectionToolbar.Name = "SectionToolbar";
			this.SectionToolbar.Size = new System.Drawing.Size(327, 25);
			this.SectionToolbar.TabIndex = 0;
			this.SectionToolbar.Text = "toolStrip1";
			this.SectionAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SectionAddBtn.Image = (Image)componentResourceManager.GetObject("SectionAddBtn.Image");
			this.SectionAddBtn.ImageTransparentColor = Color.Magenta;
			this.SectionAddBtn.Name = "SectionAddBtn";
			this.SectionAddBtn.Size = new System.Drawing.Size(33, 22);
			this.SectionAddBtn.Text = "Add";
			this.SectionAddBtn.Click += new EventHandler(this.SectionAddBtn_Click);
			this.SectionRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SectionRemoveBtn.Image = (Image)componentResourceManager.GetObject("SectionRemoveBtn.Image");
			this.SectionRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.SectionRemoveBtn.Name = "SectionRemoveBtn";
			this.SectionRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.SectionRemoveBtn.Text = "Remove";
			this.SectionRemoveBtn.Click += new EventHandler(this.SectionRemoveBtn_Click);
			this.SectionEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SectionEditBtn.Image = (Image)componentResourceManager.GetObject("SectionEditBtn.Image");
			this.SectionEditBtn.ImageTransparentColor = Color.Magenta;
			this.SectionEditBtn.Name = "SectionEditBtn";
			this.SectionEditBtn.Size = new System.Drawing.Size(31, 22);
			this.SectionEditBtn.Text = "Edit";
			this.SectionEditBtn.Click += new EventHandler(this.SectionEditBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.SectionUpBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SectionUpBtn.Image = (Image)componentResourceManager.GetObject("SectionUpBtn.Image");
			this.SectionUpBtn.ImageTransparentColor = Color.Magenta;
			this.SectionUpBtn.Name = "SectionUpBtn";
			this.SectionUpBtn.Size = new System.Drawing.Size(26, 22);
			this.SectionUpBtn.Text = "Up";
			this.SectionUpBtn.Click += new EventHandler(this.SectionUpBtn_Click);
			this.SectionDownBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SectionDownBtn.Image = (Image)componentResourceManager.GetObject("SectionDownBtn.Image");
			this.SectionDownBtn.ImageTransparentColor = Color.Magenta;
			this.SectionDownBtn.Name = "SectionDownBtn";
			this.SectionDownBtn.Size = new System.Drawing.Size(42, 22);
			this.SectionDownBtn.Text = "Down";
			this.SectionDownBtn.Click += new EventHandler(this.SectionDownBtn_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			this.SectionLeftBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SectionLeftBtn.Image = (Image)componentResourceManager.GetObject("SectionLeftBtn.Image");
			this.SectionLeftBtn.ImageTransparentColor = Color.Magenta;
			this.SectionLeftBtn.Name = "SectionLeftBtn";
			this.SectionLeftBtn.Size = new System.Drawing.Size(31, 22);
			this.SectionLeftBtn.Text = "Left";
			this.SectionLeftBtn.Click += new EventHandler(this.SectionLeftBtn_Click);
			this.SectionRightBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SectionRightBtn.Image = (Image)componentResourceManager.GetObject("SectionRightBtn.Image");
			this.SectionRightBtn.ImageTransparentColor = Color.Magenta;
			this.SectionRightBtn.Name = "SectionRightBtn";
			this.SectionRightBtn.Size = new System.Drawing.Size(39, 22);
			this.SectionRightBtn.Text = "Right";
			this.SectionRightBtn.Click += new EventHandler(this.SectionRightBtn_Click);
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(197, 234);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 11;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(278, 234);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 12;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(365, 269);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.Pages);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "OptionPowerForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Power";
			this.Pages.ResumeLayout(false);
			this.HeaderPage.ResumeLayout(false);
			this.HeaderPage.PerformLayout();
			this.ReadAloudPage.ResumeLayout(false);
			this.ReadAloudPage.PerformLayout();
			this.SectionPage.ResumeLayout(false);
			this.SectionPage.PerformLayout();
			this.SectionToolbar.ResumeLayout(false);
			this.SectionToolbar.PerformLayout();
			base.ResumeLayout(false);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fPower.Name = this.NameBox.Text;
			this.fPower.Type = (PlayerPowerType)this.TypeBox.SelectedItem;
			this.fPower.Action = (ActionType)this.ActionBox.SelectedItem;
			this.fPower.Keywords = this.KeywordBox.Text;
			this.fPower.Range = this.RangeBox.Text;
			this.fPower.ReadAloud = this.ReadAloudBox.Text;
		}

		private void SectionAddBtn_Click(object sender, EventArgs e)
		{
			OptionPowerSectionForm optionPowerSectionForm = new OptionPowerSectionForm(new PlayerPowerSection());
			if (optionPowerSectionForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fPower.Sections.Add(optionPowerSectionForm.Section);
				this.update_sections();
			}
		}

		private void SectionDownBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSection != null)
			{
				int selectedSection = this.fPower.Sections.IndexOf(this.SelectedSection);
				PlayerPowerSection item = this.fPower.Sections[selectedSection + 1];
				this.fPower.Sections[selectedSection + 1] = this.SelectedSection;
				this.fPower.Sections[selectedSection] = item;
				this.update_sections();
				this.SectionList.SelectedIndices.Add(selectedSection + 1);
			}
		}

		private void SectionEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSection != null)
			{
				int section = this.fPower.Sections.IndexOf(this.SelectedSection);
				OptionPowerSectionForm optionPowerSectionForm = new OptionPowerSectionForm(this.SelectedSection);
				if (optionPowerSectionForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fPower.Sections[section] = optionPowerSectionForm.Section;
					this.update_sections();
				}
			}
		}

		private void SectionLeftBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSection != null)
			{
				int num = this.fPower.Sections.IndexOf(this.SelectedSection);
				PlayerPowerSection selectedSection = this.SelectedSection;
				selectedSection.Indent = selectedSection.Indent - 1;
				this.update_sections();
				this.SectionList.SelectedIndices.Add(num);
			}
		}

		private void SectionRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSection != null)
			{
				this.fPower.Sections.Remove(this.SelectedSection);
				this.update_sections();
			}
		}

		private void SectionRightBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSection != null)
			{
				int num = this.fPower.Sections.IndexOf(this.SelectedSection);
				PlayerPowerSection selectedSection = this.SelectedSection;
				selectedSection.Indent = selectedSection.Indent + 1;
				this.update_sections();
				this.SectionList.SelectedIndices.Add(num);
			}
		}

		private void SectionUpBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSection != null)
			{
				int selectedSection = this.fPower.Sections.IndexOf(this.SelectedSection);
				PlayerPowerSection item = this.fPower.Sections[selectedSection - 1];
				this.fPower.Sections[selectedSection - 1] = this.SelectedSection;
				this.fPower.Sections[selectedSection] = item;
				this.update_sections();
				this.SectionList.SelectedIndices.Add(selectedSection - 1);
			}
		}

		private void update_sections()
		{
			this.SectionList.Items.Clear();
			foreach (PlayerPowerSection section in this.fPower.Sections)
			{
				string str = "";
				for (int i = 0; i != section.Indent; i++)
				{
					str = string.Concat(str, "    ");
				}
				str = string.Concat(str, section.Header, ": ", section.Details);
				this.SectionList.Items.Add(str).Tag = section;
			}
			if (this.fPower.Sections.Count == 0)
			{
				ListViewItem grayText = this.SectionList.Items.Add("(none)");
				grayText.ForeColor = SystemColors.GrayText;
			}
		}
	}
}