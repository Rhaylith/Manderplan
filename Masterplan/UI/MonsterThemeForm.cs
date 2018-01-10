using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class MonsterThemeForm : Form
	{
		private MonsterTheme fTheme;

		private Label NameLbl;

		private TextBox NameBox;

		private TabControl Pages;

		private TabPage PowerPage;

		private TabPage SkillPage;

		private Button OKBtn;

		private Button CancelBtn;

		private ListView PowerList;

		private ColumnHeader PowerHdr;

		private ColumnHeader RoleHdr;

		private ToolStrip PowerToolbar;

		private ToolStripButton PowerRemoveBtn;

		private ListView SkillList;

		private ColumnHeader SkillHdr;

		private ToolStripSplitButton PowerAddBtn;

		private ToolStripMenuItem PowerBrowse;

		private ToolStripDropDownButton PowerEditBtn;

		private ToolStripMenuItem EditPower;

		private ToolStripMenuItem EditClassification;

		public ThemePowerData SelectedPower
		{
			get
			{
				if (this.PowerList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.PowerList.SelectedItems[0].Tag as ThemePowerData;
			}
		}

		public MonsterTheme Theme
		{
			get
			{
				return this.fTheme;
			}
		}

		public MonsterThemeForm(MonsterTheme theme)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fTheme = theme.Copy();
			foreach (string skillName in Skills.GetSkillNames())
			{
				ListViewItem listViewItem = this.SkillList.Items.Add(skillName);
				bool flag = false;
				foreach (Pair<string, int> skillBonuse in this.fTheme.SkillBonuses)
				{
					if (skillBonuse.First != skillName)
					{
						continue;
					}
					flag = true;
				}
				listViewItem.Checked = flag;
			}
			this.NameBox.Text = this.fTheme.Name;
			this.update_powers();
		}

		private void add_power(CreaturePower power)
		{
			ThemePowerData themePowerDatum = new ThemePowerData()
			{
				Power = power
			};
			this.fTheme.Powers.Add(themePowerDatum);
			this.update_powers();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.PowerRemoveBtn.Enabled = this.SelectedPower != null;
			this.PowerEditBtn.Enabled = this.SelectedPower != null;
		}

		private void EditClassification_Click(object sender, EventArgs e)
		{
			if (this.SelectedPower != null)
			{
				int power = this.fTheme.Powers.IndexOf(this.SelectedPower);
				MonsterThemePowerForm monsterThemePowerForm = new MonsterThemePowerForm(this.SelectedPower);
				if (monsterThemePowerForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fTheme.Powers[power] = monsterThemePowerForm.Power;
					this.update_powers();
				}
			}
		}

		private void InitializeComponent()
		{
			ListViewGroup listViewGroup = new ListViewGroup("Attack Powers", HorizontalAlignment.Left);
			ListViewGroup listViewGroup1 = new ListViewGroup("Utility Powers", HorizontalAlignment.Left);
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MonsterThemeForm));
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.Pages = new TabControl();
			this.PowerPage = new TabPage();
			this.PowerList = new ListView();
			this.PowerHdr = new ColumnHeader();
			this.RoleHdr = new ColumnHeader();
			this.PowerToolbar = new ToolStrip();
			this.PowerAddBtn = new ToolStripSplitButton();
			this.PowerBrowse = new ToolStripMenuItem();
			this.PowerRemoveBtn = new ToolStripButton();
			this.SkillPage = new TabPage();
			this.SkillList = new ListView();
			this.SkillHdr = new ColumnHeader();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.PowerEditBtn = new ToolStripDropDownButton();
			this.EditPower = new ToolStripMenuItem();
			this.EditClassification = new ToolStripMenuItem();
			this.Pages.SuspendLayout();
			this.PowerPage.SuspendLayout();
			this.PowerToolbar.SuspendLayout();
			this.SkillPage.SuspendLayout();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new System.Drawing.Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.NameBox.Location = new Point(56, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(328, 20);
			this.NameBox.TabIndex = 1;
			this.Pages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Pages.Controls.Add(this.PowerPage);
			this.Pages.Controls.Add(this.SkillPage);
			this.Pages.Location = new Point(12, 38);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new System.Drawing.Size(372, 291);
			this.Pages.TabIndex = 2;
			this.PowerPage.Controls.Add(this.PowerList);
			this.PowerPage.Controls.Add(this.PowerToolbar);
			this.PowerPage.Location = new Point(4, 22);
			this.PowerPage.Name = "PowerPage";
			this.PowerPage.Padding = new System.Windows.Forms.Padding(3);
			this.PowerPage.Size = new System.Drawing.Size(364, 265);
			this.PowerPage.TabIndex = 0;
			this.PowerPage.Text = "Powers";
			this.PowerPage.UseVisualStyleBackColor = true;
			ListView.ColumnHeaderCollection columns = this.PowerList.Columns;
			ColumnHeader[] powerHdr = new ColumnHeader[] { this.PowerHdr, this.RoleHdr };
			columns.AddRange(powerHdr);
			this.PowerList.Dock = DockStyle.Fill;
			this.PowerList.FullRowSelect = true;
			listViewGroup.Header = "Attack Powers";
			listViewGroup.Name = "listViewGroup1";
			listViewGroup1.Header = "Utility Powers";
			listViewGroup1.Name = "listViewGroup2";
			this.PowerList.Groups.AddRange(new ListViewGroup[] { listViewGroup, listViewGroup1 });
			this.PowerList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.PowerList.HideSelection = false;
			this.PowerList.Location = new Point(3, 28);
			this.PowerList.MultiSelect = false;
			this.PowerList.Name = "PowerList";
			this.PowerList.Size = new System.Drawing.Size(358, 234);
			this.PowerList.TabIndex = 1;
			this.PowerList.UseCompatibleStateImageBehavior = false;
			this.PowerList.View = View.Details;
			this.PowerList.DoubleClick += new EventHandler(this.PowerEditBtn_Click);
			this.PowerHdr.Text = "Power";
			this.PowerHdr.Width = 150;
			this.RoleHdr.Text = "Roles";
			this.RoleHdr.Width = 180;
			ToolStripItemCollection items = this.PowerToolbar.Items;
			ToolStripItem[] powerAddBtn = new ToolStripItem[] { this.PowerAddBtn, this.PowerRemoveBtn, this.PowerEditBtn };
			items.AddRange(powerAddBtn);
			this.PowerToolbar.Location = new Point(3, 3);
			this.PowerToolbar.Name = "PowerToolbar";
			this.PowerToolbar.Size = new System.Drawing.Size(358, 25);
			this.PowerToolbar.TabIndex = 0;
			this.PowerToolbar.Text = "toolStrip1";
			this.PowerAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PowerAddBtn.DropDownItems.AddRange(new ToolStripItem[] { this.PowerBrowse });
			this.PowerAddBtn.Image = (Image)componentResourceManager.GetObject("PowerAddBtn.Image");
			this.PowerAddBtn.ImageTransparentColor = Color.Magenta;
			this.PowerAddBtn.Name = "PowerAddBtn";
			this.PowerAddBtn.Size = new System.Drawing.Size(45, 22);
			this.PowerAddBtn.Text = "Add";
			this.PowerAddBtn.ButtonClick += new EventHandler(this.PowerAddBtn_Click);
			this.PowerBrowse.Name = "PowerBrowse";
			this.PowerBrowse.Size = new System.Drawing.Size(152, 22);
			this.PowerBrowse.Text = "Browse...";
			this.PowerBrowse.Click += new EventHandler(this.PowerBrowse_Click);
			this.PowerRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PowerRemoveBtn.Image = (Image)componentResourceManager.GetObject("PowerRemoveBtn.Image");
			this.PowerRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.PowerRemoveBtn.Name = "PowerRemoveBtn";
			this.PowerRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.PowerRemoveBtn.Text = "Remove";
			this.PowerRemoveBtn.Click += new EventHandler(this.PowerRemoveBtn_Click);
			this.SkillPage.Controls.Add(this.SkillList);
			this.SkillPage.Location = new Point(4, 22);
			this.SkillPage.Name = "SkillPage";
			this.SkillPage.Padding = new System.Windows.Forms.Padding(3);
			this.SkillPage.Size = new System.Drawing.Size(364, 265);
			this.SkillPage.TabIndex = 1;
			this.SkillPage.Text = "Skill Bonuses";
			this.SkillPage.UseVisualStyleBackColor = true;
			this.SkillList.CheckBoxes = true;
			this.SkillList.Columns.AddRange(new ColumnHeader[] { this.SkillHdr });
			this.SkillList.Dock = DockStyle.Fill;
			this.SkillList.FullRowSelect = true;
			this.SkillList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.SkillList.HideSelection = false;
			this.SkillList.Location = new Point(3, 3);
			this.SkillList.MultiSelect = false;
			this.SkillList.Name = "SkillList";
			this.SkillList.Size = new System.Drawing.Size(358, 259);
			this.SkillList.TabIndex = 0;
			this.SkillList.UseCompatibleStateImageBehavior = false;
			this.SkillList.View = View.Details;
			this.SkillHdr.Text = "Skill";
			this.SkillHdr.Width = 200;
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(228, 335);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(309, 335);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.PowerEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems = this.PowerEditBtn.DropDownItems;
			ToolStripItem[] editPower = new ToolStripItem[] { this.EditPower, this.EditClassification };
			dropDownItems.AddRange(editPower);
			this.PowerEditBtn.Image = (Image)componentResourceManager.GetObject("PowerEditBtn.Image");
			this.PowerEditBtn.ImageTransparentColor = Color.Magenta;
			this.PowerEditBtn.Name = "PowerEditBtn";
			this.PowerEditBtn.Size = new System.Drawing.Size(40, 22);
			this.PowerEditBtn.Text = "Edit";
			this.EditPower.Name = "EditPower";
			this.EditPower.Size = new System.Drawing.Size(152, 22);
			this.EditPower.Text = "Power";
			this.EditPower.Click += new EventHandler(this.PowerEditBtn_Click);
			this.EditClassification.Name = "EditClassification";
			this.EditClassification.Size = new System.Drawing.Size(152, 22);
			this.EditClassification.Text = "Classification";
			this.EditClassification.Click += new EventHandler(this.EditClassification_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(396, 370);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MonsterThemeForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Theme";
			this.Pages.ResumeLayout(false);
			this.PowerPage.ResumeLayout(false);
			this.PowerPage.PerformLayout();
			this.PowerToolbar.ResumeLayout(false);
			this.PowerToolbar.PerformLayout();
			this.SkillPage.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fTheme.Name = this.NameBox.Text;
			this.fTheme.SkillBonuses.Clear();
			foreach (ListViewItem checkedItem in this.SkillList.CheckedItems)
			{
				this.fTheme.SkillBonuses.Add(new Pair<string, int>(checkedItem.Text, 2));
			}
		}

		private void PowerAddBtn_Click(object sender, EventArgs e)
		{
			CreaturePower creaturePower = new CreaturePower()
			{
				Name = "New Power"
			};
			PowerBuilderForm powerBuilderForm = new PowerBuilderForm(creaturePower, null, false);
			if (powerBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.add_power(powerBuilderForm.Power);
			}
		}

		private void PowerBrowse_Click(object sender, EventArgs e)
		{
			PowerBrowserForm powerBrowserForm = new PowerBrowserForm(this.NameBox.Text, 0, null, new PowerCallback(this.add_power));
			powerBrowserForm.ShowDialog();
		}

		private void PowerEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedPower != null)
			{
				int power = this.fTheme.Powers.IndexOf(this.SelectedPower);
				PowerBuilderForm powerBuilderForm = new PowerBuilderForm(this.SelectedPower.Power, null, false);
				if (powerBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fTheme.Powers[power].Power = powerBuilderForm.Power;
					this.update_powers();
				}
			}
		}

		private void PowerRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedPower != null)
			{
				this.fTheme.Powers.Remove(this.SelectedPower);
				this.update_powers();
			}
		}

		private void update_powers()
		{
			this.PowerList.ShowGroups = true;
			this.PowerList.Items.Clear();
			foreach (ThemePowerData power in this.fTheme.Powers)
			{
				string str = "";
				if (power.Roles.Count != 6)
				{
					foreach (RoleType role in power.Roles)
					{
						if (str != "")
						{
							str = string.Concat(str, ", ");
						}
						str = string.Concat(str, role.ToString());
					}
				}
				else
				{
					str = "(any)";
				}
				ListViewItem item = this.PowerList.Items.Add(power.Power.Name);
				item.SubItems.Add(str);
				item.Tag = power;
				switch (power.Type)
				{
					case PowerType.Attack:
					{
						item.Group = this.PowerList.Groups[0];
						continue;
					}
					case PowerType.Utility:
					{
						item.Group = this.PowerList.Groups[1];
						continue;
					}
					default:
					{
						continue;
					}
				}
			}
			if (this.PowerList.Items.Count == 0)
			{
				this.PowerList.ShowGroups = false;
				ListViewItem grayText = this.PowerList.Items.Add("(no powers)");
				grayText.ForeColor = SystemColors.GrayText;
			}
		}
	}
}