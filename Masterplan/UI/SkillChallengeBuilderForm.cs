using Masterplan;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Utils;
using Utils.Controls;

namespace Masterplan.UI
{
	internal class SkillChallengeBuilderForm : Form
	{
		private Masterplan.Data.SkillChallenge fChallenge;

		private Button OKBtn;

		private Button CancelBtn;

		private TabControl Pages;

		private TabPage SkillsPage;

		private ListView SkillList;

		private ColumnHeader SkillHdr;

		private ColumnHeader DCHdr;

		private ToolStrip SkillsToolbar;

		private ToolStripButton RemoveBtn;

		private ToolStripButton EditBtn;

		private SplitContainer SkillSplitter;

		private ListView SkillSourceList;

		private ColumnHeader SkillSourceHdr;

		private TabPage InfoPage;

		private SplitContainer InfoSplitter;

		private DefaultTextBox VictoryBox;

		private ToolStrip VictoryToolbar;

		private ToolStripLabel toolStripLabel1;

		private DefaultTextBox DefeatBox;

		private ToolStrip DefeatButton;

		private ToolStripLabel toolStripLabel2;

		private SplitContainer OverviewSplitter;

		private TabPage OverviewPage;

		private Label LevelLbl;

		private NumericUpDown LevelBox;

		private TextBox NameBox;

		private Label NameLbl;

		private Label CompLbl;

		private NumericUpDown CompBox;

		private ListView InfoList;

		private ColumnHeader InfoHdr;

		private ColumnHeader StdDCHdr;

		private GroupBox LevelGroup;

		private GroupBox CompGroup;

		private Label XPLbl;

		private Label XPInfoLbl;

		private Label LengthLbl;

		private Label LengthInfoLbl;

		private ColumnHeader AbilityHdr;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton BreakdownBtn;

		private TabPage NotesPage;

		private DefaultTextBox NotesBox;

		private ToolStrip Toolbar;

		private ToolStripDropDownButton FileMenu;

		private ToolStripMenuItem FileExport;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripLabel SuccessCountLbl;

		private ToolStripLabel FailureCountLbl;

		private ToolStripButton ResetProgressBtn;

		public SkillChallengeData SelectedSkill
		{
			get
			{
				if (this.SkillList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.SkillList.SelectedItems[0].Tag as SkillChallengeData;
			}
		}

		public string SelectedSourceSkill
		{
			get
			{
				if (this.SkillSourceList.SelectedItems.Count == 0)
				{
					return "";
				}
				return this.SkillSourceList.SelectedItems[0].Text;
			}
		}

		public Masterplan.Data.SkillChallenge SkillChallenge
		{
			get
			{
				return this.fChallenge;
			}
		}

		public SkillChallengeBuilderForm(Masterplan.Data.SkillChallenge sc)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fChallenge = sc.Copy() as Masterplan.Data.SkillChallenge;
			this.update_all();
			foreach (string skillName in Skills.GetSkillNames())
			{
				string str = Skills.GetKeyAbility(skillName).Substring(0, 3);
				string str1 = str.Substring(0, 3);
				ListViewItem item = this.SkillSourceList.Items.Add(skillName);
				ListViewItem.ListViewSubItem grayText = item.SubItems.Add(str1);
				item.UseItemStyleForSubItems = false;
				grayText.ForeColor = SystemColors.GrayText;
				item.Group = this.SkillSourceList.Groups[0];
			}
			foreach (string abilityName in Skills.GetAbilityNames())
			{
				string str2 = abilityName.Substring(0, 3);
				ListViewItem listViewItem = this.SkillSourceList.Items.Add(abilityName);
				ListViewItem.ListViewSubItem listViewSubItem = listViewItem.SubItems.Add(str2);
				listViewItem.UseItemStyleForSubItems = false;
				listViewSubItem.ForeColor = SystemColors.GrayText;
				listViewItem.Group = this.SkillSourceList.Groups[1];
			}
			ListViewItem item1 = this.SkillSourceList.Items.Add("(custom skill)");
			ListViewItem.ListViewSubItem grayText1 = item1.SubItems.Add("");
			item1.UseItemStyleForSubItems = false;
			grayText1.ForeColor = SystemColors.GrayText;
			item1.Group = this.SkillSourceList.Groups[2];
		}

		private void add_skill(string skill_name)
		{
			SkillChallengeSkillForm skillChallengeSkillForm = new SkillChallengeSkillForm(new SkillChallengeData()
			{
				SkillName = skill_name
			});
			if (skillChallengeSkillForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fChallenge.Skills.Add(skillChallengeSkillForm.SkillData);
				this.fChallenge.Skills.Sort();
				this.update_view();
				this.update_skills();
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveBtn.Enabled = this.SelectedSkill != null;
			this.EditBtn.Enabled = this.SelectedSkill != null;
			this.BreakdownBtn.Enabled = this.fChallenge.Skills.Count != 0;
			SkillChallengeResult results = this.fChallenge.Results;
			this.ResetProgressBtn.Enabled = results.Successes + results.Fails != 0;
		}

		private void BreakdownBtn_Click(object sender, EventArgs e)
		{
			(new SkillChallengeBreakdownForm(this.fChallenge)).ShowDialog();
		}

		private void CompBox_ValueChanged(object sender, EventArgs e)
		{
			this.update_view();
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSkill != null)
			{
				int skillData = this.fChallenge.Skills.IndexOf(this.SelectedSkill);
				SkillChallengeSkillForm skillChallengeSkillForm = new SkillChallengeSkillForm(this.SelectedSkill);
				if (skillChallengeSkillForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fChallenge.Skills[skillData] = skillChallengeSkillForm.SkillData;
					this.fChallenge.Skills.Sort();
					this.update_view();
					this.update_skills();
				}
			}
		}

		private void FileExport_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog()
			{
				Title = "Export Skill Challenge",
				FileName = this.fChallenge.Name,
				Filter = Program.SkillChallengeFilter
			};
			if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK && !Serialisation<Masterplan.Data.SkillChallenge>.Save(saveFileDialog.FileName, this.fChallenge, SerialisationMode.Binary))
			{
				MessageBox.Show("The skill challenge could not be exported.", "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void InitializeComponent()
		{
			ListViewGroup listViewGroup = new ListViewGroup("Standard Skill DCs", HorizontalAlignment.Left);
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(SkillChallengeBuilderForm));
			ListViewGroup listViewGroup1 = new ListViewGroup("Primary Skills", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Secondary Skills", HorizontalAlignment.Left);
			ListViewGroup listViewGroup3 = new ListViewGroup("Automatic Failure", HorizontalAlignment.Left);
			ListViewGroup listViewGroup4 = new ListViewGroup("Skills", HorizontalAlignment.Left);
			ListViewGroup listViewGroup5 = new ListViewGroup("Abilities", HorizontalAlignment.Left);
			ListViewGroup listViewGroup6 = new ListViewGroup("Custom", HorizontalAlignment.Left);
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.Pages = new TabControl();
			this.OverviewPage = new TabPage();
			this.OverviewSplitter = new SplitContainer();
			this.LevelGroup = new GroupBox();
			this.XPLbl = new Label();
			this.XPInfoLbl = new Label();
			this.LevelBox = new NumericUpDown();
			this.LevelLbl = new Label();
			this.CompGroup = new GroupBox();
			this.LengthLbl = new Label();
			this.LengthInfoLbl = new Label();
			this.CompBox = new NumericUpDown();
			this.CompLbl = new Label();
			this.InfoList = new ListView();
			this.InfoHdr = new ColumnHeader();
			this.StdDCHdr = new ColumnHeader();
			this.Toolbar = new ToolStrip();
			this.FileMenu = new ToolStripDropDownButton();
			this.FileExport = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.SuccessCountLbl = new ToolStripLabel();
			this.FailureCountLbl = new ToolStripLabel();
			this.ResetProgressBtn = new ToolStripButton();
			this.SkillsPage = new TabPage();
			this.SkillSplitter = new SplitContainer();
			this.SkillList = new ListView();
			this.SkillHdr = new ColumnHeader();
			this.DCHdr = new ColumnHeader();
			this.SkillSourceList = new ListView();
			this.SkillSourceHdr = new ColumnHeader();
			this.AbilityHdr = new ColumnHeader();
			this.SkillsToolbar = new ToolStrip();
			this.RemoveBtn = new ToolStripButton();
			this.EditBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.BreakdownBtn = new ToolStripButton();
			this.InfoPage = new TabPage();
			this.InfoSplitter = new SplitContainer();
			this.VictoryBox = new DefaultTextBox();
			this.VictoryToolbar = new ToolStrip();
			this.toolStripLabel1 = new ToolStripLabel();
			this.DefeatBox = new DefaultTextBox();
			this.DefeatButton = new ToolStrip();
			this.toolStripLabel2 = new ToolStripLabel();
			this.NotesPage = new TabPage();
			this.NotesBox = new DefaultTextBox();
			this.NameBox = new TextBox();
			this.NameLbl = new Label();
			this.Pages.SuspendLayout();
			this.OverviewPage.SuspendLayout();
			this.OverviewSplitter.Panel1.SuspendLayout();
			this.OverviewSplitter.Panel2.SuspendLayout();
			this.OverviewSplitter.SuspendLayout();
			this.LevelGroup.SuspendLayout();
			((ISupportInitialize)this.LevelBox).BeginInit();
			this.CompGroup.SuspendLayout();
			((ISupportInitialize)this.CompBox).BeginInit();
			this.Toolbar.SuspendLayout();
			this.SkillsPage.SuspendLayout();
			this.SkillSplitter.Panel1.SuspendLayout();
			this.SkillSplitter.Panel2.SuspendLayout();
			this.SkillSplitter.SuspendLayout();
			this.SkillsToolbar.SuspendLayout();
			this.InfoPage.SuspendLayout();
			this.InfoSplitter.Panel1.SuspendLayout();
			this.InfoSplitter.Panel2.SuspendLayout();
			this.InfoSplitter.SuspendLayout();
			this.VictoryToolbar.SuspendLayout();
			this.DefeatButton.SuspendLayout();
			this.NotesPage.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(352, 359);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(433, 359);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.Pages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Pages.Controls.Add(this.OverviewPage);
			this.Pages.Controls.Add(this.SkillsPage);
			this.Pages.Controls.Add(this.InfoPage);
			this.Pages.Controls.Add(this.NotesPage);
			this.Pages.Location = new Point(12, 38);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new System.Drawing.Size(496, 315);
			this.Pages.TabIndex = 2;
			this.OverviewPage.Controls.Add(this.OverviewSplitter);
			this.OverviewPage.Controls.Add(this.Toolbar);
			this.OverviewPage.Location = new Point(4, 22);
			this.OverviewPage.Name = "OverviewPage";
			this.OverviewPage.Padding = new System.Windows.Forms.Padding(3);
			this.OverviewPage.Size = new System.Drawing.Size(488, 289);
			this.OverviewPage.TabIndex = 5;
			this.OverviewPage.Text = "Overview";
			this.OverviewPage.UseVisualStyleBackColor = true;
			this.OverviewSplitter.Dock = DockStyle.Fill;
			this.OverviewSplitter.Location = new Point(3, 28);
			this.OverviewSplitter.Name = "OverviewSplitter";
			this.OverviewSplitter.Panel1.Controls.Add(this.LevelGroup);
			this.OverviewSplitter.Panel1.Controls.Add(this.CompGroup);
			this.OverviewSplitter.Panel2.Controls.Add(this.InfoList);
			this.OverviewSplitter.Size = new System.Drawing.Size(482, 258);
			this.OverviewSplitter.SplitterDistance = 237;
			this.OverviewSplitter.TabIndex = 0;
			this.LevelGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.LevelGroup.Controls.Add(this.XPLbl);
			this.LevelGroup.Controls.Add(this.XPInfoLbl);
			this.LevelGroup.Controls.Add(this.LevelBox);
			this.LevelGroup.Controls.Add(this.LevelLbl);
			this.LevelGroup.Location = new Point(4, 87);
			this.LevelGroup.Name = "LevelGroup";
			this.LevelGroup.Size = new System.Drawing.Size(230, 78);
			this.LevelGroup.TabIndex = 10;
			this.LevelGroup.TabStop = false;
			this.LevelGroup.Text = "Level";
			this.XPLbl.AutoSize = true;
			this.XPLbl.Location = new Point(69, 48);
			this.XPLbl.Name = "XPLbl";
			this.XPLbl.Size = new System.Drawing.Size(24, 13);
			this.XPLbl.TabIndex = 10;
			this.XPLbl.Text = "[xp]";
			this.XPInfoLbl.AutoSize = true;
			this.XPInfoLbl.Location = new Point(6, 48);
			this.XPInfoLbl.Name = "XPInfoLbl";
			this.XPInfoLbl.Size = new System.Drawing.Size(24, 13);
			this.XPInfoLbl.TabIndex = 9;
			this.XPInfoLbl.Text = "XP:";
			this.LevelBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.LevelBox.Location = new Point(72, 19);
			NumericUpDown levelBox = this.LevelBox;
			int[] numArray = new int[] { 30, 0, 0, 0 };
			levelBox.Maximum = new decimal(numArray);
			NumericUpDown num = this.LevelBox;
			int[] numArray1 = new int[] { 1, 0, 0, 0 };
			num.Minimum = new decimal(numArray1);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.Size = new System.Drawing.Size(152, 20);
			this.LevelBox.TabIndex = 8;
			NumericUpDown numericUpDown = this.LevelBox;
			int[] numArray2 = new int[] { 1, 0, 0, 0 };
			numericUpDown.Value = new decimal(numArray2);
			this.LevelBox.ValueChanged += new EventHandler(this.LevelBox_ValueChanged);
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(6, 21);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new System.Drawing.Size(36, 13);
			this.LevelLbl.TabIndex = 7;
			this.LevelLbl.Text = "Level:";
			this.CompGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.CompGroup.Controls.Add(this.LengthLbl);
			this.CompGroup.Controls.Add(this.LengthInfoLbl);
			this.CompGroup.Controls.Add(this.CompBox);
			this.CompGroup.Controls.Add(this.CompLbl);
			this.CompGroup.Location = new Point(3, 3);
			this.CompGroup.Name = "CompGroup";
			this.CompGroup.Size = new System.Drawing.Size(231, 78);
			this.CompGroup.TabIndex = 9;
			this.CompGroup.TabStop = false;
			this.CompGroup.Text = "Complexity / Length";
			this.LengthLbl.AutoSize = true;
			this.LengthLbl.Location = new Point(69, 49);
			this.LengthLbl.Name = "LengthLbl";
			this.LengthLbl.Size = new System.Drawing.Size(42, 13);
			this.LengthLbl.TabIndex = 5;
			this.LengthLbl.Text = "[length]";
			this.LengthInfoLbl.AutoSize = true;
			this.LengthInfoLbl.Location = new Point(6, 49);
			this.LengthInfoLbl.Name = "LengthInfoLbl";
			this.LengthInfoLbl.Size = new System.Drawing.Size(43, 13);
			this.LengthInfoLbl.TabIndex = 4;
			this.LengthInfoLbl.Text = "Length:";
			this.CompBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.CompBox.Location = new Point(72, 19);
			NumericUpDown compBox = this.CompBox;
			int[] numArray3 = new int[] { 20, 0, 0, 0 };
			compBox.Maximum = new decimal(numArray3);
			NumericUpDown compBox1 = this.CompBox;
			int[] numArray4 = new int[] { 1, 0, 0, 0 };
			compBox1.Minimum = new decimal(numArray4);
			this.CompBox.Name = "CompBox";
			this.CompBox.Size = new System.Drawing.Size(153, 20);
			this.CompBox.TabIndex = 3;
			NumericUpDown num1 = this.CompBox;
			int[] numArray5 = new int[] { 1, 0, 0, 0 };
			num1.Value = new decimal(numArray5);
			this.CompBox.ValueChanged += new EventHandler(this.CompBox_ValueChanged);
			this.CompLbl.AutoSize = true;
			this.CompLbl.Location = new Point(6, 21);
			this.CompLbl.Name = "CompLbl";
			this.CompLbl.Size = new System.Drawing.Size(60, 13);
			this.CompLbl.TabIndex = 2;
			this.CompLbl.Text = "Complexity:";
			ListView.ColumnHeaderCollection columns = this.InfoList.Columns;
			ColumnHeader[] infoHdr = new ColumnHeader[] { this.InfoHdr, this.StdDCHdr };
			columns.AddRange(infoHdr);
			this.InfoList.Dock = DockStyle.Fill;
			this.InfoList.Enabled = false;
			this.InfoList.FullRowSelect = true;
			listViewGroup.Header = "Standard Skill DCs";
			listViewGroup.Name = "listViewGroup1";
			this.InfoList.Groups.AddRange(new ListViewGroup[] { listViewGroup });
			this.InfoList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.InfoList.HideSelection = false;
			this.InfoList.Location = new Point(0, 0);
			this.InfoList.MultiSelect = false;
			this.InfoList.Name = "InfoList";
			this.InfoList.Size = new System.Drawing.Size(241, 258);
			this.InfoList.TabIndex = 0;
			this.InfoList.UseCompatibleStateImageBehavior = false;
			this.InfoList.View = View.Details;
			this.InfoHdr.Text = "Difficulty";
			this.InfoHdr.Width = 139;
			this.StdDCHdr.Text = "DC";
			this.StdDCHdr.TextAlign = HorizontalAlignment.Right;
			this.StdDCHdr.Width = 67;
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] fileMenu = new ToolStripItem[] { this.FileMenu, this.toolStripSeparator2, this.SuccessCountLbl, this.FailureCountLbl, this.ResetProgressBtn };
			items.AddRange(fileMenu);
			this.Toolbar.Location = new Point(3, 3);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(482, 25);
			this.Toolbar.TabIndex = 1;
			this.Toolbar.Text = "toolStrip1";
			this.FileMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.FileMenu.DropDownItems.AddRange(new ToolStripItem[] { this.FileExport });
			this.FileMenu.Image = (Image)componentResourceManager.GetObject("FileMenu.Image");
			this.FileMenu.ImageTransparentColor = Color.Magenta;
			this.FileMenu.Name = "FileMenu";
			this.FileMenu.Size = new System.Drawing.Size(38, 22);
			this.FileMenu.Text = "File";
			this.FileExport.Name = "FileExport";
			this.FileExport.Size = new System.Drawing.Size(152, 22);
			this.FileExport.Text = "Export...";
			this.FileExport.Click += new EventHandler(this.FileExport_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			this.SuccessCountLbl.Name = "SuccessCountLbl";
			this.SuccessCountLbl.Size = new System.Drawing.Size(66, 22);
			this.SuccessCountLbl.Text = "[successes]";
			this.FailureCountLbl.Name = "FailureCountLbl";
			this.FailureCountLbl.Size = new System.Drawing.Size(53, 22);
			this.FailureCountLbl.Text = "[failures]";
			this.ResetProgressBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ResetProgressBtn.Image = (Image)componentResourceManager.GetObject("ResetProgressBtn.Image");
			this.ResetProgressBtn.ImageTransparentColor = Color.Magenta;
			this.ResetProgressBtn.Name = "ResetProgressBtn";
			this.ResetProgressBtn.Size = new System.Drawing.Size(39, 22);
			this.ResetProgressBtn.Text = "Reset";
			this.ResetProgressBtn.Click += new EventHandler(this.ResetProgressBtn_Click);
			this.SkillsPage.Controls.Add(this.SkillSplitter);
			this.SkillsPage.Controls.Add(this.SkillsToolbar);
			this.SkillsPage.Location = new Point(4, 22);
			this.SkillsPage.Name = "SkillsPage";
			this.SkillsPage.Padding = new System.Windows.Forms.Padding(3);
			this.SkillsPage.Size = new System.Drawing.Size(488, 289);
			this.SkillsPage.TabIndex = 3;
			this.SkillsPage.Text = "Skills";
			this.SkillsPage.UseVisualStyleBackColor = true;
			this.SkillSplitter.Dock = DockStyle.Fill;
			this.SkillSplitter.Location = new Point(3, 28);
			this.SkillSplitter.Name = "SkillSplitter";
			this.SkillSplitter.Panel1.Controls.Add(this.SkillList);
			this.SkillSplitter.Panel2.Controls.Add(this.SkillSourceList);
			this.SkillSplitter.Size = new System.Drawing.Size(482, 258);
			this.SkillSplitter.SplitterDistance = 283;
			this.SkillSplitter.TabIndex = 2;
			this.SkillList.AllowDrop = true;
			ListView.ColumnHeaderCollection columnHeaderCollections = this.SkillList.Columns;
			ColumnHeader[] skillHdr = new ColumnHeader[] { this.SkillHdr, this.DCHdr };
			columnHeaderCollections.AddRange(skillHdr);
			this.SkillList.Dock = DockStyle.Fill;
			this.SkillList.FullRowSelect = true;
			listViewGroup1.Header = "Primary Skills";
			listViewGroup1.Name = "listViewGroup1";
			listViewGroup2.Header = "Secondary Skills";
			listViewGroup2.Name = "listViewGroup2";
			listViewGroup3.Header = "Automatic Failure";
			listViewGroup3.Name = "listViewGroup3";
			this.SkillList.Groups.AddRange(new ListViewGroup[] { listViewGroup1, listViewGroup2, listViewGroup3 });
			this.SkillList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.SkillList.HideSelection = false;
			this.SkillList.Location = new Point(0, 0);
			this.SkillList.MultiSelect = false;
			this.SkillList.Name = "SkillList";
			this.SkillList.Size = new System.Drawing.Size(283, 258);
			this.SkillList.TabIndex = 1;
			this.SkillList.UseCompatibleStateImageBehavior = false;
			this.SkillList.View = View.Details;
			this.SkillList.DoubleClick += new EventHandler(this.EditBtn_Click);
			this.SkillList.DragOver += new DragEventHandler(this.SkillList_DragOver);
			this.SkillHdr.Text = "Skill";
			this.SkillHdr.Width = 135;
			this.DCHdr.Text = "DC Level";
			this.DCHdr.Width = 103;
			ListView.ColumnHeaderCollection columns1 = this.SkillSourceList.Columns;
			ColumnHeader[] skillSourceHdr = new ColumnHeader[] { this.SkillSourceHdr, this.AbilityHdr };
			columns1.AddRange(skillSourceHdr);
			this.SkillSourceList.Dock = DockStyle.Fill;
			this.SkillSourceList.FullRowSelect = true;
			listViewGroup4.Header = "Skills";
			listViewGroup4.Name = "listViewGroup1";
			listViewGroup5.Header = "Abilities";
			listViewGroup5.Name = "listViewGroup2";
			listViewGroup6.Header = "Custom";
			listViewGroup6.Name = "listViewGroup3";
			this.SkillSourceList.Groups.AddRange(new ListViewGroup[] { listViewGroup4, listViewGroup5, listViewGroup6 });
			this.SkillSourceList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.SkillSourceList.HideSelection = false;
			this.SkillSourceList.Location = new Point(0, 0);
			this.SkillSourceList.MultiSelect = false;
			this.SkillSourceList.Name = "SkillSourceList";
			this.SkillSourceList.Size = new System.Drawing.Size(195, 258);
			this.SkillSourceList.TabIndex = 0;
			this.SkillSourceList.UseCompatibleStateImageBehavior = false;
			this.SkillSourceList.View = View.Details;
			this.SkillSourceList.DoubleClick += new EventHandler(this.SkillSourceList_DoubleClick);
			this.SkillSourceList.ItemDrag += new ItemDragEventHandler(this.SkillSourceList_ItemDrag);
			this.SkillSourceHdr.Text = "Skills";
			this.SkillSourceHdr.Width = 112;
			this.AbilityHdr.Text = "Ability";
			this.AbilityHdr.Width = 49;
			ToolStripItemCollection toolStripItemCollections = this.SkillsToolbar.Items;
			ToolStripItem[] removeBtn = new ToolStripItem[] { this.RemoveBtn, this.EditBtn, this.toolStripSeparator1, this.BreakdownBtn };
			toolStripItemCollections.AddRange(removeBtn);
			this.SkillsToolbar.Location = new Point(3, 3);
			this.SkillsToolbar.Name = "SkillsToolbar";
			this.SkillsToolbar.Size = new System.Drawing.Size(482, 25);
			this.SkillsToolbar.TabIndex = 0;
			this.SkillsToolbar.Text = "toolStrip1";
			this.RemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveBtn.Image = (Image)componentResourceManager.GetObject("RemoveBtn.Image");
			this.RemoveBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.RemoveBtn.Text = "Remove";
			this.RemoveBtn.Click += new EventHandler(this.RemoveBtn_Click);
			this.EditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditBtn.Image = (Image)componentResourceManager.GetObject("EditBtn.Image");
			this.EditBtn.ImageTransparentColor = Color.Magenta;
			this.EditBtn.Name = "EditBtn";
			this.EditBtn.Size = new System.Drawing.Size(31, 22);
			this.EditBtn.Text = "Edit";
			this.EditBtn.Click += new EventHandler(this.EditBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.BreakdownBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.BreakdownBtn.Image = (Image)componentResourceManager.GetObject("BreakdownBtn.Image");
			this.BreakdownBtn.ImageTransparentColor = Color.Magenta;
			this.BreakdownBtn.Name = "BreakdownBtn";
			this.BreakdownBtn.Size = new System.Drawing.Size(107, 22);
			this.BreakdownBtn.Text = "Ability Breakdown";
			this.BreakdownBtn.Click += new EventHandler(this.BreakdownBtn_Click);
			this.InfoPage.Controls.Add(this.InfoSplitter);
			this.InfoPage.Location = new Point(4, 22);
			this.InfoPage.Name = "InfoPage";
			this.InfoPage.Padding = new System.Windows.Forms.Padding(3);
			this.InfoPage.Size = new System.Drawing.Size(488, 289);
			this.InfoPage.TabIndex = 4;
			this.InfoPage.Text = "Victory / Defeat Details";
			this.InfoPage.UseVisualStyleBackColor = true;
			this.InfoSplitter.Dock = DockStyle.Fill;
			this.InfoSplitter.Location = new Point(3, 3);
			this.InfoSplitter.Name = "InfoSplitter";
			this.InfoSplitter.Panel1.Controls.Add(this.VictoryBox);
			this.InfoSplitter.Panel1.Controls.Add(this.VictoryToolbar);
			this.InfoSplitter.Panel2.Controls.Add(this.DefeatBox);
			this.InfoSplitter.Panel2.Controls.Add(this.DefeatButton);
			this.InfoSplitter.Size = new System.Drawing.Size(482, 283);
			this.InfoSplitter.SplitterDistance = 237;
			this.InfoSplitter.TabIndex = 0;
			this.VictoryBox.AcceptsReturn = true;
			this.VictoryBox.AcceptsTab = true;
			this.VictoryBox.DefaultText = "(enter victory information here)";
			this.VictoryBox.Dock = DockStyle.Fill;
			this.VictoryBox.Location = new Point(0, 25);
			this.VictoryBox.Multiline = true;
			this.VictoryBox.Name = "VictoryBox";
			this.VictoryBox.ScrollBars = ScrollBars.Vertical;
			this.VictoryBox.Size = new System.Drawing.Size(237, 258);
			this.VictoryBox.TabIndex = 1;
			this.VictoryBox.Text = "(enter victory information here)";
			this.VictoryToolbar.Items.AddRange(new ToolStripItem[] { this.toolStripLabel1 });
			this.VictoryToolbar.Location = new Point(0, 0);
			this.VictoryToolbar.Name = "VictoryToolbar";
			this.VictoryToolbar.Size = new System.Drawing.Size(237, 25);
			this.VictoryToolbar.TabIndex = 0;
			this.VictoryToolbar.Text = "toolStrip1";
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(47, 22);
			this.toolStripLabel1.Text = "Victory:";
			this.DefeatBox.AcceptsReturn = true;
			this.DefeatBox.AcceptsTab = true;
			this.DefeatBox.DefaultText = "(enter defeat information here)";
			this.DefeatBox.Dock = DockStyle.Fill;
			this.DefeatBox.Location = new Point(0, 25);
			this.DefeatBox.Multiline = true;
			this.DefeatBox.Name = "DefeatBox";
			this.DefeatBox.ScrollBars = ScrollBars.Vertical;
			this.DefeatBox.Size = new System.Drawing.Size(241, 258);
			this.DefeatBox.TabIndex = 2;
			this.DefeatBox.Text = "(enter defeat information here)";
			this.DefeatButton.Items.AddRange(new ToolStripItem[] { this.toolStripLabel2 });
			this.DefeatButton.Location = new Point(0, 0);
			this.DefeatButton.Name = "DefeatButton";
			this.DefeatButton.Size = new System.Drawing.Size(241, 25);
			this.DefeatButton.TabIndex = 0;
			this.DefeatButton.Text = "toolStrip2";
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new System.Drawing.Size(44, 22);
			this.toolStripLabel2.Text = "Defeat:";
			this.NotesPage.Controls.Add(this.NotesBox);
			this.NotesPage.Location = new Point(4, 22);
			this.NotesPage.Name = "NotesPage";
			this.NotesPage.Padding = new System.Windows.Forms.Padding(3);
			this.NotesPage.Size = new System.Drawing.Size(488, 289);
			this.NotesPage.TabIndex = 6;
			this.NotesPage.Text = "Notes";
			this.NotesPage.UseVisualStyleBackColor = true;
			this.NotesBox.AcceptsReturn = true;
			this.NotesBox.AcceptsTab = true;
			this.NotesBox.DefaultText = "(enter details here)";
			this.NotesBox.Dock = DockStyle.Fill;
			this.NotesBox.Location = new Point(3, 3);
			this.NotesBox.Multiline = true;
			this.NotesBox.Name = "NotesBox";
			this.NotesBox.ScrollBars = ScrollBars.Vertical;
			this.NotesBox.Size = new System.Drawing.Size(482, 283);
			this.NotesBox.TabIndex = 3;
			this.NotesBox.Text = "(enter details here)";
			this.NameBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.NameBox.Location = new Point(56, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(452, 20);
			this.NameBox.TabIndex = 5;
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new System.Drawing.Size(38, 13);
			this.NameLbl.TabIndex = 4;
			this.NameLbl.Text = "Name:";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(520, 394);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SkillChallengeBuilderForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Skill Challenge Builder";
			this.Pages.ResumeLayout(false);
			this.OverviewPage.ResumeLayout(false);
			this.OverviewPage.PerformLayout();
			this.OverviewSplitter.Panel1.ResumeLayout(false);
			this.OverviewSplitter.Panel2.ResumeLayout(false);
			this.OverviewSplitter.ResumeLayout(false);
			this.LevelGroup.ResumeLayout(false);
			this.LevelGroup.PerformLayout();
			((ISupportInitialize)this.LevelBox).EndInit();
			this.CompGroup.ResumeLayout(false);
			this.CompGroup.PerformLayout();
			((ISupportInitialize)this.CompBox).EndInit();
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.SkillsPage.ResumeLayout(false);
			this.SkillsPage.PerformLayout();
			this.SkillSplitter.Panel1.ResumeLayout(false);
			this.SkillSplitter.Panel2.ResumeLayout(false);
			this.SkillSplitter.ResumeLayout(false);
			this.SkillsToolbar.ResumeLayout(false);
			this.SkillsToolbar.PerformLayout();
			this.InfoPage.ResumeLayout(false);
			this.InfoSplitter.Panel1.ResumeLayout(false);
			this.InfoSplitter.Panel1.PerformLayout();
			this.InfoSplitter.Panel2.ResumeLayout(false);
			this.InfoSplitter.Panel2.PerformLayout();
			this.InfoSplitter.ResumeLayout(false);
			this.VictoryToolbar.ResumeLayout(false);
			this.VictoryToolbar.PerformLayout();
			this.DefeatButton.ResumeLayout(false);
			this.DefeatButton.PerformLayout();
			this.NotesPage.ResumeLayout(false);
			this.NotesPage.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void LevelBox_ValueChanged(object sender, EventArgs e)
		{
			this.update_view();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fChallenge.Name = this.NameBox.Text;
			this.fChallenge.Complexity = (int)this.CompBox.Value;
			if (this.LevelBox.Enabled)
			{
				this.fChallenge.Level = (int)this.LevelBox.Value;
			}
			this.fChallenge.Success = (this.VictoryBox.Text != this.VictoryBox.DefaultText ? this.VictoryBox.Text : "");
			this.fChallenge.Failure = (this.DefeatBox.Text != this.DefeatBox.DefaultText ? this.DefeatBox.Text : "");
			this.fChallenge.Notes = (this.NotesBox.Text != this.NotesBox.DefaultText ? this.NotesBox.Text : "");
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSkill != null)
			{
				this.fChallenge.Skills.Remove(this.SelectedSkill);
				this.update_view();
				this.update_skills();
			}
		}

		private void ResetProgressBtn_Click(object sender, EventArgs e)
		{
			foreach (SkillChallengeData skill in this.fChallenge.Skills)
			{
				skill.Results.Successes = 0;
				skill.Results.Fails = 0;
			}
			this.update_view();
		}

		private void SkillList_DragOver(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.None;
			string data = e.Data.GetData(typeof(string)) as string;
			if (data != null && data != "")
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

		private void SkillSourceList_DoubleClick(object sender, EventArgs e)
		{
			string selectedSourceSkill = this.SelectedSourceSkill;
			if (selectedSourceSkill != "")
			{
				this.add_skill(selectedSourceSkill);
			}
		}

		private void SkillSourceList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			ListViewItem item = e.Item as ListViewItem;
			if (item != null)
			{
				string text = item.Text;
				if (base.DoDragDrop(text, DragDropEffects.Copy) == DragDropEffects.Copy)
				{
					this.add_skill(text);
				}
			}
		}

		public void update_all()
		{
			this.NameBox.Text = this.fChallenge.Name;
			this.CompBox.Value = this.fChallenge.Complexity;
			this.VictoryBox.Text = this.fChallenge.Success;
			this.DefeatBox.Text = this.fChallenge.Failure;
			this.NotesBox.Text = this.fChallenge.Notes;
			if (this.fChallenge.Level == -1)
			{
				this.LevelBox.Enabled = false;
			}
			else
			{
				this.LevelBox.Value = this.fChallenge.Level;
			}
			this.update_view();
			this.update_skills();
		}

		private void update_skills()
		{
			this.SkillList.Items.Clear();
			foreach (SkillChallengeData skill in this.fChallenge.Skills)
			{
				string str = string.Concat(skill.Difficulty, " DCs");
				if (skill.DCModifier != 0)
				{
					str = (skill.DCModifier <= 0 ? string.Concat(str, " ", skill.DCModifier) : string.Concat(str, " +", skill.DCModifier));
				}
				ListViewItem item = this.SkillList.Items.Add(skill.SkillName);
				item.SubItems.Add(str);
				item.Tag = skill;
				switch (skill.Type)
				{
					case SkillType.Primary:
					{
						item.Group = this.SkillList.Groups[0];
						break;
					}
					case SkillType.Secondary:
					{
						item.Group = this.SkillList.Groups[1];
						break;
					}
					case SkillType.AutoFail:
					{
						item.Group = this.SkillList.Groups[2];
						break;
					}
				}
				if (skill.Details == "" && skill.Success == "" && skill.Failure == "")
				{
					item.ForeColor = SystemColors.GrayText;
				}
				if (skill.Difficulty != Difficulty.Trivial && skill.Difficulty != Difficulty.Extreme)
				{
					continue;
				}
				item.UseItemStyleForSubItems = false;
				item.SubItems[1].ForeColor = Color.Red;
			}
			if (this.SkillList.Groups[0].Items.Count == 0)
			{
				ListViewItem grayText = this.SkillList.Items.Add("(none)");
				grayText.Group = this.SkillList.Groups[0];
				grayText.ForeColor = SystemColors.GrayText;
			}
			if (this.SkillList.Groups[1].Items.Count == 0)
			{
				ListViewItem listViewItem = this.SkillList.Items.Add("(none)");
				listViewItem.Group = this.SkillList.Groups[1];
				listViewItem.ForeColor = SystemColors.GrayText;
			}
		}

		private void update_view()
		{
			int value = (int)this.LevelBox.Value;
			int num = (int)this.CompBox.Value;
			this.LengthLbl.Text = string.Concat(Masterplan.Data.SkillChallenge.GetSuccesses(num), " successes before 3 failures");
			this.InfoList.Items.Clear();
			if (this.fChallenge.Level == -1)
			{
				ListViewItem item = this.InfoList.Items.Add("DCs");
				ListViewItem.ListViewSubItem grayText = item.SubItems.Add("(varies by level)");
				item.UseItemStyleForSubItems = false;
				grayText.ForeColor = SystemColors.GrayText;
				item.Group = this.InfoList.Groups[0];
				this.XPLbl.Text = "(XP varies by level)";
			}
			else
			{
				ListViewItem listViewItem = this.InfoList.Items.Add("Easy");
				listViewItem.SubItems.Add(string.Concat("DC ", AI.GetSkillDC(Difficulty.Easy, value)));
				listViewItem.Group = this.InfoList.Groups[0];
				ListViewItem item1 = this.InfoList.Items.Add("Moderate");
				item1.SubItems.Add(string.Concat("DC ", AI.GetSkillDC(Difficulty.Moderate, value)));
				item1.Group = this.InfoList.Groups[0];
				ListViewItem listViewItem1 = this.InfoList.Items.Add("Hard");
				listViewItem1.SubItems.Add(string.Concat("DC ", AI.GetSkillDC(Difficulty.Hard, value)));
				listViewItem1.Group = this.InfoList.Groups[0];
				this.XPLbl.Text = string.Concat(Masterplan.Data.SkillChallenge.GetXP(value, num), " XP");
			}
			SkillChallengeResult results = this.fChallenge.Results;
			this.SuccessCountLbl.Text = string.Concat("Successes: ", results.Successes);
			this.FailureCountLbl.Text = string.Concat("Failures: ", results.Fails);
		}
	}
}