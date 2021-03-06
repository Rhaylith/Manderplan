using Masterplan;
using Masterplan.Data;
using Masterplan.Extensibility;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class QuickReferenceForm : Form
	{
		private Label LevelLbl;

		private NumericUpDown LevelBox;

		private GroupBox SkillGroup;

		private GroupBox DamageGroup;

		private ListView SkillList;

		private ColumnHeader DiffHdr;

		private ColumnHeader DCHdr;

		private ListView DamageList;

		private ColumnHeader TargetHdr;

		private ColumnHeader DmgHdr;

		private TabControl Pages;

		private TabPage ReferencePage;

		public QuickReferenceForm()
		{
			this.InitializeComponent();
			foreach (IAddIn addIn in Session.AddIns)
			{
				foreach (IPage quickReferencePage in addIn.QuickReferencePages)
				{
					TabPage tabPage = new TabPage()
					{
						Text = quickReferencePage.Name
					};
					tabPage.Controls.Add(quickReferencePage.Control);
					quickReferencePage.Control.Dock = DockStyle.Fill;
					this.Pages.TabPages.Add(tabPage);
				}
			}
			this.UpdateView();
		}

		private void InitializeComponent()
		{
			ListViewGroup listViewGroup = new ListViewGroup("Normal Damage", HorizontalAlignment.Left);
			ListViewGroup listViewGroup1 = new ListViewGroup("Limited Damage", HorizontalAlignment.Left);
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(QuickReferenceForm));
			this.LevelLbl = new Label();
			this.LevelBox = new NumericUpDown();
			this.SkillGroup = new GroupBox();
			this.SkillList = new ListView();
			this.DiffHdr = new ColumnHeader();
			this.DCHdr = new ColumnHeader();
			this.DamageGroup = new GroupBox();
			this.DamageList = new ListView();
			this.TargetHdr = new ColumnHeader();
			this.DmgHdr = new ColumnHeader();
			this.Pages = new TabControl();
			this.ReferencePage = new TabPage();
			((ISupportInitialize)this.LevelBox).BeginInit();
			this.SkillGroup.SuspendLayout();
			this.DamageGroup.SuspendLayout();
			this.Pages.SuspendLayout();
			this.ReferencePage.SuspendLayout();
			base.SuspendLayout();
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(8, 8);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new System.Drawing.Size(36, 13);
			this.LevelLbl.TabIndex = 0;
			this.LevelLbl.Text = "Level:";
			this.LevelBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.LevelBox.Location = new Point(50, 6);
			NumericUpDown levelBox = this.LevelBox;
			int[] numArray = new int[] { 30, 0, 0, 0 };
			levelBox.Maximum = new decimal(numArray);
			NumericUpDown num = this.LevelBox;
			int[] numArray1 = new int[] { 1, 0, 0, 0 };
			num.Minimum = new decimal(numArray1);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.Size = new System.Drawing.Size(291, 20);
			this.LevelBox.TabIndex = 1;
			NumericUpDown numericUpDown = this.LevelBox;
			int[] numArray2 = new int[] { 1, 0, 0, 0 };
			numericUpDown.Value = new decimal(numArray2);
			this.LevelBox.ValueChanged += new EventHandler(this.LevelBox_ValueChanged);
			this.SkillGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.SkillGroup.Controls.Add(this.SkillList);
			this.SkillGroup.Location = new Point(8, 32);
			this.SkillGroup.Name = "SkillGroup";
			this.SkillGroup.Size = new System.Drawing.Size(333, 118);
			this.SkillGroup.TabIndex = 2;
			this.SkillGroup.TabStop = false;
			this.SkillGroup.Text = "Skill DCs";
			this.SkillList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			ListView.ColumnHeaderCollection columns = this.SkillList.Columns;
			ColumnHeader[] diffHdr = new ColumnHeader[] { this.DiffHdr, this.DCHdr };
			columns.AddRange(diffHdr);
			this.SkillList.FullRowSelect = true;
			this.SkillList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.SkillList.HideSelection = false;
			this.SkillList.Location = new Point(6, 19);
			this.SkillList.MultiSelect = false;
			this.SkillList.Name = "SkillList";
			this.SkillList.Size = new System.Drawing.Size(321, 93);
			this.SkillList.TabIndex = 0;
			this.SkillList.UseCompatibleStateImageBehavior = false;
			this.SkillList.View = View.Details;
			this.DiffHdr.Text = "Difficulty";
			this.DiffHdr.Width = 200;
			this.DCHdr.Text = "DC";
			this.DCHdr.TextAlign = HorizontalAlignment.Right;
			this.DCHdr.Width = 80;
			this.DamageGroup.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.DamageGroup.Controls.Add(this.DamageList);
			this.DamageGroup.Location = new Point(8, 156);
			this.DamageGroup.Name = "DamageGroup";
			this.DamageGroup.Size = new System.Drawing.Size(333, 257);
			this.DamageGroup.TabIndex = 3;
			this.DamageGroup.TabStop = false;
			this.DamageGroup.Text = "Damage Expressions";
			this.DamageList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			ListView.ColumnHeaderCollection columnHeaderCollections = this.DamageList.Columns;
			ColumnHeader[] targetHdr = new ColumnHeader[] { this.TargetHdr, this.DmgHdr };
			columnHeaderCollections.AddRange(targetHdr);
			this.DamageList.FullRowSelect = true;
			listViewGroup.Header = "Normal Damage";
			listViewGroup.Name = "listViewGroup1";
			listViewGroup1.Header = "Limited Damage";
			listViewGroup1.Name = "listViewGroup2";
			this.DamageList.Groups.AddRange(new ListViewGroup[] { listViewGroup, listViewGroup1 });
			this.DamageList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.DamageList.HideSelection = false;
			this.DamageList.Location = new Point(6, 19);
			this.DamageList.MultiSelect = false;
			this.DamageList.Name = "DamageList";
			this.DamageList.Size = new System.Drawing.Size(321, 232);
			this.DamageList.TabIndex = 1;
			this.DamageList.UseCompatibleStateImageBehavior = false;
			this.DamageList.View = View.Details;
			this.TargetHdr.Text = "Target";
			this.TargetHdr.Width = 200;
			this.DmgHdr.Text = "Damage";
			this.DmgHdr.TextAlign = HorizontalAlignment.Right;
			this.DmgHdr.Width = 80;
			this.Pages.Controls.Add(this.ReferencePage);
			this.Pages.Dock = DockStyle.Fill;
			this.Pages.Location = new Point(0, 0);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new System.Drawing.Size(357, 447);
			this.Pages.TabIndex = 4;
			this.ReferencePage.Controls.Add(this.LevelBox);
			this.ReferencePage.Controls.Add(this.DamageGroup);
			this.ReferencePage.Controls.Add(this.LevelLbl);
			this.ReferencePage.Controls.Add(this.SkillGroup);
			this.ReferencePage.Location = new Point(4, 22);
			this.ReferencePage.Name = "ReferencePage";
			this.ReferencePage.Padding = new System.Windows.Forms.Padding(3);
			this.ReferencePage.Size = new System.Drawing.Size(349, 421);
			this.ReferencePage.TabIndex = 0;
			this.ReferencePage.Text = "Reference";
			this.ReferencePage.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(357, 447);
			base.Controls.Add(this.Pages);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "QuickReferenceForm";
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Quick Reference";
			base.FormClosed += new FormClosedEventHandler(this.QuickReferenceForm_FormClosed);
			((ISupportInitialize)this.LevelBox).EndInit();
			this.SkillGroup.ResumeLayout(false);
			this.DamageGroup.ResumeLayout(false);
			this.Pages.ResumeLayout(false);
			this.ReferencePage.ResumeLayout(false);
			this.ReferencePage.PerformLayout();
			base.ResumeLayout(false);
		}

		private void LevelBox_ValueChanged(object sender, EventArgs e)
		{
			this.update_skills();
		}

		private void QuickReferenceForm_FormClosed(object sender, FormClosedEventArgs e)
		{
		}

		private void update_skills()
		{
			int value = (int)this.LevelBox.Value;
			this.SkillList.BeginUpdate();
			this.SkillList.Items.Clear();
			ListViewItem.ListViewSubItemCollection subItems = this.SkillList.Items.Add("Easy").SubItems;
			int skillDC = AI.GetSkillDC(Difficulty.Easy, value);
			subItems.Add(skillDC.ToString());
			ListViewItem.ListViewSubItemCollection listViewSubItemCollections = this.SkillList.Items.Add("Moderate").SubItems;
			int num = AI.GetSkillDC(Difficulty.Moderate, value);
			listViewSubItemCollections.Add(num.ToString());
			ListViewItem.ListViewSubItemCollection subItems1 = this.SkillList.Items.Add("Hard").SubItems;
			int skillDC1 = AI.GetSkillDC(Difficulty.Hard, value);
			subItems1.Add(skillDC1.ToString());
			this.SkillList.EndUpdate();
			this.DamageList.BeginUpdate();
			this.DamageList.Items.Clear();
			this.DamageList.ShowGroups = false;
			ListViewItem item = this.DamageList.Items.Add("Against a single target");
			item.SubItems.Add(Statistics.NormalDamage(value));
			item.Group = this.DamageList.Groups[0];
			ListViewItem listViewItem = this.DamageList.Items.Add("Against multiple targets");
			listViewItem.SubItems.Add(Statistics.MultipleDamage(value));
			listViewItem.Group = this.DamageList.Groups[0];
			this.DamageList.EndUpdate();
		}

		public void UpdateView()
		{
			if (Session.Project != null)
			{
				this.LevelBox.Value = Session.Project.Party.Level;
			}
			this.update_skills();
			foreach (IAddIn addIn in Session.AddIns)
			{
				foreach (IPage quickReferencePage in addIn.QuickReferencePages)
				{
					quickReferencePage.UpdateView();
				}
			}
		}
	}
}