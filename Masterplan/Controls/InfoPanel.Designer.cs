using Masterplan.Data;
using Masterplan.Tools;
using Masterplan.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class InfoPanel : UserControl
	{
		private IContainer components;

		private NumericUpDown LevelBox;

		private Label LevelLbl;

		private ListView SkillList;

		private ColumnHeader DiffHdr;

		private ColumnHeader DCHdr;

		public int Level
		{
			get
			{
				return (int)this.LevelBox.Value;
			}
			set
			{
				this.LevelBox.Value = value;
			}
		}

		public DiceExpression SelectedDamageExpression
		{
			get
			{
				if (this.SkillList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.SkillList.SelectedItems[0].Tag as DiceExpression;
			}
		}

		public InfoPanel()
		{
			this.InitializeComponent();
			this.update_list();
		}

		private void DamageList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedDamageExpression != null)
			{
				DieRollerForm dieRollerForm = new DieRollerForm()
				{
					Expression = this.SelectedDamageExpression
				};
				dieRollerForm.ShowDialog();
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ListViewGroup listViewGroup = new ListViewGroup("Skill DCs", HorizontalAlignment.Left);
			ListViewGroup listViewGroup1 = new ListViewGroup("Aid Another", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Damage Expressions", HorizontalAlignment.Left);
			ListViewGroup listViewGroup3 = new ListViewGroup("Monster Knowledge", HorizontalAlignment.Left);
			this.LevelBox = new NumericUpDown();
			this.LevelLbl = new Label();
			this.SkillList = new ListView();
			this.DiffHdr = new ColumnHeader();
			this.DCHdr = new ColumnHeader();
			((ISupportInitialize)this.LevelBox).BeginInit();
			base.SuspendLayout();
			this.LevelBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.LevelBox.Location = new Point(45, 3);
			NumericUpDown levelBox = this.LevelBox;
			int[] numArray = new int[] { 30, 0, 0, 0 };
			levelBox.Maximum = new decimal(numArray);
			NumericUpDown num = this.LevelBox;
			int[] numArray1 = new int[] { 1, 0, 0, 0 };
			num.Minimum = new decimal(numArray1);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.Size = new System.Drawing.Size(214, 20);
			this.LevelBox.TabIndex = 10;
			NumericUpDown numericUpDown = this.LevelBox;
			int[] numArray2 = new int[] { 1, 0, 0, 0 };
			numericUpDown.Value = new decimal(numArray2);
			this.LevelBox.ValueChanged += new EventHandler(this.LevelBox_ValueChanged);
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(3, 5);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new System.Drawing.Size(36, 13);
			this.LevelLbl.TabIndex = 9;
			this.LevelLbl.Text = "Level:";
			this.SkillList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			ListView.ColumnHeaderCollection columns = this.SkillList.Columns;
			ColumnHeader[] diffHdr = new ColumnHeader[] { this.DiffHdr, this.DCHdr };
			columns.AddRange(diffHdr);
			this.SkillList.FullRowSelect = true;
			listViewGroup.Header = "Skill DCs";
			listViewGroup.Name = "listViewGroup1";
			listViewGroup1.Header = "Aid Another";
			listViewGroup1.Name = "listViewGroup2";
			listViewGroup2.Header = "Damage Expressions";
			listViewGroup2.Name = "listViewGroup3";
			listViewGroup3.Header = "Monster Knowledge";
			listViewGroup3.Name = "listViewGroup4";
			ListViewGroupCollection groups = this.SkillList.Groups;
			ListViewGroup[] listViewGroupArray = new ListViewGroup[] { listViewGroup, listViewGroup1, listViewGroup2, listViewGroup3 };
			groups.AddRange(listViewGroupArray);
			this.SkillList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.SkillList.HideSelection = false;
			this.SkillList.Location = new Point(3, 29);
			this.SkillList.MultiSelect = false;
			this.SkillList.Name = "SkillList";
			this.SkillList.Size = new System.Drawing.Size(256, 252);
			this.SkillList.TabIndex = 0;
			this.SkillList.UseCompatibleStateImageBehavior = false;
			this.SkillList.View = View.Details;
			this.SkillList.DoubleClick += new EventHandler(this.DamageList_DoubleClick);
			this.DiffHdr.Text = "Information";
			this.DiffHdr.Width = 135;
			this.DCHdr.Text = "Value";
			this.DCHdr.TextAlign = HorizontalAlignment.Right;
			this.DCHdr.Width = 94;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.SkillList);
			base.Controls.Add(this.LevelBox);
			base.Controls.Add(this.LevelLbl);
			base.Name = "InfoPanel";
			base.Size = new System.Drawing.Size(262, 284);
			((ISupportInitialize)this.LevelBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void LevelBox_ValueChanged(object sender, EventArgs e)
		{
			this.update_list();
		}

		private void update_list()
		{
			int value = (int)this.LevelBox.Value;
			int num = 10 + value / 2;
			string str = Statistics.NormalDamage(value);
			string str1 = Statistics.MultipleDamage(value);
			string str2 = Statistics.MinionDamage(value).ToString();
			this.SkillList.BeginUpdate();
			this.SkillList.Items.Clear();
			ListViewItem item = this.SkillList.Items.Add("Easy");
			item.SubItems.Add(string.Concat("DC ", AI.GetSkillDC(Difficulty.Easy, value)));
			item.Group = this.SkillList.Groups[0];
			ListViewItem listViewItem = this.SkillList.Items.Add("Moderate");
			listViewItem.SubItems.Add(string.Concat("DC ", AI.GetSkillDC(Difficulty.Moderate, value)));
			listViewItem.Group = this.SkillList.Groups[0];
			ListViewItem item1 = this.SkillList.Items.Add("Hard");
			item1.SubItems.Add(string.Concat("DC ", AI.GetSkillDC(Difficulty.Hard, value)));
			item1.Group = this.SkillList.Groups[0];
			ListViewItem listViewItem1 = this.SkillList.Items.Add("Aid Another");
			listViewItem1.SubItems.Add(string.Concat("DC ", num));
			listViewItem1.Group = this.SkillList.Groups[1];
			ListViewItem item2 = this.SkillList.Items.Add("Against a single target");
			item2.SubItems.Add(str);
			item2.Tag = DiceExpression.Parse(str);
			item2.Group = this.SkillList.Groups[2];
			ListViewItem listViewItem2 = this.SkillList.Items.Add("Against multiple targets");
			listViewItem2.SubItems.Add(str1);
			listViewItem2.Tag = DiceExpression.Parse(str1);
			listViewItem2.Group = this.SkillList.Groups[2];
			ListViewItem item3 = this.SkillList.Items.Add("From a minion");
			item3.SubItems.Add(str2);
			item3.Tag = DiceExpression.Parse(str2);
			item3.Group = this.SkillList.Groups[2];
			ListViewItem listViewItem3 = this.SkillList.Items.Add("Aberrant");
			listViewItem3.SubItems.Add("Dungeoneering");
			listViewItem3.Group = this.SkillList.Groups[3];
			ListViewItem item4 = this.SkillList.Items.Add("Elemental");
			item4.SubItems.Add("Arcana");
			item4.Group = this.SkillList.Groups[3];
			ListViewItem listViewItem4 = this.SkillList.Items.Add("Fey");
			listViewItem4.SubItems.Add("Arcana");
			listViewItem4.Group = this.SkillList.Groups[3];
			ListViewItem item5 = this.SkillList.Items.Add("Immortal");
			item5.SubItems.Add("Religion");
			item5.Group = this.SkillList.Groups[3];
			ListViewItem listViewItem5 = this.SkillList.Items.Add("Natural");
			listViewItem5.SubItems.Add("Nature");
			listViewItem5.Group = this.SkillList.Groups[3];
			ListViewItem item6 = this.SkillList.Items.Add("Shadow");
			item6.SubItems.Add("Arcana");
			item6.Group = this.SkillList.Groups[3];
			ListViewItem listViewItem6 = this.SkillList.Items.Add("Undead keyword");
			listViewItem6.SubItems.Add("Religion");
			listViewItem6.Group = this.SkillList.Groups[3];
			this.SkillList.EndUpdate();
		}
	}
}