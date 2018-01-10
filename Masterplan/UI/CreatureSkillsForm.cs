using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class CreatureSkillsForm : Form
	{
		private Button OKBtn;

		private Button CancelBtn;

		private ListView SkillList;

		private ColumnHeader SkillHdr;

		private ColumnHeader TrainedHdr;

		private ColumnHeader AbilityHdr;

		private ColumnHeader MiscHdr;

		private ColumnHeader TotalHdr;

		private Panel SkillPanel;

		private ToolStrip Toolbar;

		private ToolStripButton TrainedBtn;

		private ToolStripButton EditSkillBtn;

		private ICreature fCreature;

		private List<CreatureSkillsForm.SkillData> fSkills = new List<CreatureSkillsForm.SkillData>();

		private CreatureSkillsForm.SkillData SelectedSkill
		{
			get
			{
				if (this.SkillList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.SkillList.SelectedItems[0].Tag as CreatureSkillsForm.SkillData;
			}
		}

		public CreatureSkillsForm(ICreature creature)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fCreature = creature;
			Dictionary<string, int> strs = CreatureHelper.ParseSkills(this.fCreature.Skills);
			foreach (string skillName in Skills.GetSkillNames())
			{
				int level = this.fCreature.Level / 2;
				int modifier = 0;
				string keyAbility = Skills.GetKeyAbility(skillName);
				string str = keyAbility;
				if (keyAbility != null)
				{
					if (str == "Strength")
					{
						modifier = this.fCreature.Strength.Modifier;
					}
					else if (str == "Constitution")
					{
						modifier = this.fCreature.Constitution.Modifier;
					}
					else if (str == "Dexterity")
					{
						modifier = this.fCreature.Dexterity.Modifier;
					}
					else if (str == "Intelligence")
					{
						modifier = this.fCreature.Intelligence.Modifier;
					}
					else if (str == "Wisdom")
					{
						modifier = this.fCreature.Wisdom.Modifier;
					}
					else if (str == "Charisma")
					{
						modifier = this.fCreature.Charisma.Modifier;
					}
				}
				CreatureSkillsForm.SkillData skillDatum = new CreatureSkillsForm.SkillData()
				{
					SkillName = skillName,
					Ability = modifier,
					Level = level
				};
				if (strs.ContainsKey(skillName))
				{
					int item = strs[skillName] - (modifier + level);
					if (item > 3)
					{
						skillDatum.Trained = true;
						item -= 5;
					}
					skillDatum.Misc = item;
				}
				this.fSkills.Add(skillDatum);
			}
			this.update_list();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.TrainedBtn.Enabled = this.SelectedSkill != null;
			this.TrainedBtn.Checked = (this.SelectedSkill == null ? false : this.SelectedSkill.Trained);
			this.EditSkillBtn.Enabled = this.SelectedSkill != null;
		}

		private void EditSkillBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSkill == null)
			{
				return;
			}
			string keyAbility = Skills.GetKeyAbility(this.SelectedSkill.SkillName);
			CreatureSkillForm creatureSkillForm = new CreatureSkillForm(this.SelectedSkill.SkillName, keyAbility, this.SelectedSkill.Ability, this.SelectedSkill.Level, this.SelectedSkill.Trained, this.SelectedSkill.Misc);
			if (creatureSkillForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.SelectedSkill.Trained = creatureSkillForm.Trained;
				this.SelectedSkill.Misc = creatureSkillForm.Misc;
				this.update_list();
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(CreatureSkillsForm));
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.SkillList = new ListView();
			this.SkillHdr = new ColumnHeader();
			this.TrainedHdr = new ColumnHeader();
			this.AbilityHdr = new ColumnHeader();
			this.MiscHdr = new ColumnHeader();
			this.TotalHdr = new ColumnHeader();
			this.SkillPanel = new Panel();
			this.Toolbar = new ToolStrip();
			this.TrainedBtn = new ToolStripButton();
			this.EditSkillBtn = new ToolStripButton();
			this.SkillPanel.SuspendLayout();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(243, 367);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 5;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(324, 367);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 6;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			ListView.ColumnHeaderCollection columns = this.SkillList.Columns;
			ColumnHeader[] skillHdr = new ColumnHeader[] { this.SkillHdr, this.TrainedHdr, this.AbilityHdr, this.MiscHdr, this.TotalHdr };
			columns.AddRange(skillHdr);
			this.SkillList.Dock = DockStyle.Fill;
			this.SkillList.FullRowSelect = true;
			this.SkillList.HideSelection = false;
			this.SkillList.Location = new Point(0, 25);
			this.SkillList.MultiSelect = false;
			this.SkillList.Name = "SkillList";
			this.SkillList.Size = new System.Drawing.Size(387, 324);
			this.SkillList.TabIndex = 7;
			this.SkillList.UseCompatibleStateImageBehavior = false;
			this.SkillList.View = View.Details;
			this.SkillList.DoubleClick += new EventHandler(this.SkillList_DoubleClick);
			this.SkillHdr.Text = "Skill";
			this.SkillHdr.Width = 120;
			this.TrainedHdr.Text = "Trained";
			this.TrainedHdr.TextAlign = HorizontalAlignment.Center;
			this.AbilityHdr.Text = "Ability";
			this.AbilityHdr.TextAlign = HorizontalAlignment.Right;
			this.MiscHdr.Text = "Misc";
			this.MiscHdr.TextAlign = HorizontalAlignment.Right;
			this.TotalHdr.Text = "Total";
			this.TotalHdr.TextAlign = HorizontalAlignment.Right;
			this.SkillPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.SkillPanel.Controls.Add(this.SkillList);
			this.SkillPanel.Controls.Add(this.Toolbar);
			this.SkillPanel.Location = new Point(12, 12);
			this.SkillPanel.Name = "SkillPanel";
			this.SkillPanel.Size = new System.Drawing.Size(387, 349);
			this.SkillPanel.TabIndex = 8;
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] trainedBtn = new ToolStripItem[] { this.TrainedBtn, this.EditSkillBtn };
			items.AddRange(trainedBtn);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(387, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.TrainedBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TrainedBtn.Image = (Image)componentResourceManager.GetObject("TrainedBtn.Image");
			this.TrainedBtn.ImageTransparentColor = Color.Magenta;
			this.TrainedBtn.Name = "TrainedBtn";
			this.TrainedBtn.Size = new System.Drawing.Size(51, 22);
			this.TrainedBtn.Text = "Trained";
			this.TrainedBtn.Click += new EventHandler(this.TrainedBtn_Click);
			this.EditSkillBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditSkillBtn.Image = (Image)componentResourceManager.GetObject("EditSkillBtn.Image");
			this.EditSkillBtn.ImageTransparentColor = Color.Magenta;
			this.EditSkillBtn.Name = "EditSkillBtn";
			this.EditSkillBtn.Size = new System.Drawing.Size(55, 22);
			this.EditSkillBtn.Text = "Edit Skill";
			this.EditSkillBtn.Click += new EventHandler(this.EditSkillBtn_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(411, 402);
			base.Controls.Add(this.SkillPanel);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CreatureSkillsForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Creature Skills";
			this.SkillPanel.ResumeLayout(false);
			this.SkillPanel.PerformLayout();
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			string str = "";
			foreach (CreatureSkillsForm.SkillData fSkill in this.fSkills)
			{
				if (!fSkill.Show)
				{
					continue;
				}
				if (str != "")
				{
					str = string.Concat(str, "; ");
				}
				str = string.Concat(str, fSkill.ToString());
			}
			this.fCreature.Skills = str;
		}

		private void SkillList_DoubleClick(object sender, EventArgs e)
		{
			this.TrainedBtn_Click(sender, e);
		}

		private void TrainedBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSkill == null)
			{
				return;
			}
			this.SelectedSkill.Trained = !this.SelectedSkill.Trained;
			this.update_list();
		}

		private void update_list()
		{
			this.SkillList.BeginUpdate();
			this.SkillList.Items.Clear();
			List<ListViewItem> listViewItems = new List<ListViewItem>();
			foreach (CreatureSkillsForm.SkillData fSkill in this.fSkills)
			{
				ListViewItem listViewItem = new ListViewItem(fSkill.SkillName);
				listViewItem.SubItems.Add((fSkill.Trained ? "Yes" : ""));
				listViewItem.SubItems.Add(fSkill.Ability.ToString());
				listViewItem.SubItems.Add((fSkill.Misc != 0 ? fSkill.Misc.ToString() : ""));
				listViewItem.SubItems.Add(fSkill.Total.ToString());
				if (!fSkill.Show)
				{
					listViewItem.ForeColor = SystemColors.GrayText;
					listViewItem.UseItemStyleForSubItems = false;
				}
				listViewItem.Tag = fSkill;
				listViewItems.Add(listViewItem);
			}
			this.SkillList.Items.AddRange(listViewItems.ToArray());
			this.SkillList.EndUpdate();
		}

		private class SkillData
		{
			public string SkillName;

			public bool Trained;

			public int Ability;

			public int Level;

			public int Misc;

			public bool Show
			{
				get
				{
					if (this.Trained)
					{
						return true;
					}
					return this.Misc != 0;
				}
			}

			public int Total
			{
				get
				{
					int num = (this.Trained ? 5 : 0);
					return num + this.Ability + this.Level + this.Misc;
				}
			}

			public SkillData()
			{
			}

			public override string ToString()
			{
				string str = (this.Total < 0 ? "-" : "");
				object[] skillName = new object[] { this.SkillName, " ", str, this.Total };
				return string.Concat(skillName);
			}
		}
	}
}