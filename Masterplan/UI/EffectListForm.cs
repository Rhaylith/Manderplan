using Masterplan;
using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class EffectListForm : Form
	{
		private Encounter fEncounter;

		private CombatData fCurrentActor;

		private int fCurrentRound = -2147483648;

		private IContainer components;

		private ListView EffectList;

		private ColumnHeader EffectHdr;

		private ToolStrip Toolbar;

		private ToolStripButton RemoveBtn;

		private ToolStripButton EditBtn;

		public Pair<CombatData, OngoingCondition> SelectedEffect
		{
			get
			{
				if (this.EffectList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.EffectList.SelectedItems[0].Tag as Pair<CombatData, OngoingCondition>;
			}
		}

		public EffectListForm(Encounter enc, CombatData current_actor, int current_round)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fEncounter = enc;
			this.fCurrentActor = current_actor;
			this.fCurrentRound = current_round;
			this.update_list();
		}

		private void add_conditions(CombatData cd)
		{
			ListViewGroup listViewGroup = this.EffectList.Groups.Add(cd.DisplayName, cd.DisplayName);
			foreach (OngoingCondition condition in cd.Conditions)
			{
				ListViewItem pair = this.EffectList.Items.Add(condition.ToString(this.fEncounter, false));
				pair.Tag = new Pair<CombatData, OngoingCondition>(cd, condition);
				pair.Group = listViewGroup;
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveBtn.Enabled = this.SelectedEffect != null;
			this.EditBtn.Enabled = this.SelectedEffect != null;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedEffect != null)
			{
				CombatData first = this.SelectedEffect.First;
				OngoingCondition second = this.SelectedEffect.Second;
				int num = first.Conditions.IndexOf(second);
				EffectForm effectForm = new EffectForm(second, this.fEncounter, this.fCurrentActor, this.fCurrentRound);
				if (effectForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					first.Conditions[num] = effectForm.Effect;
					this.update_list();
				}
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(EffectListForm));
			this.Toolbar = new ToolStrip();
			this.RemoveBtn = new ToolStripButton();
			this.EditBtn = new ToolStripButton();
			this.EffectList = new ListView();
			this.EffectHdr = new ColumnHeader();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] removeBtn = new ToolStripItem[] { this.RemoveBtn, this.EditBtn };
			items.AddRange(removeBtn);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(398, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
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
			this.EffectList.Columns.AddRange(new ColumnHeader[] { this.EffectHdr });
			this.EffectList.Dock = DockStyle.Fill;
			this.EffectList.FullRowSelect = true;
			this.EffectList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.EffectList.HideSelection = false;
			this.EffectList.Location = new Point(0, 25);
			this.EffectList.MultiSelect = false;
			this.EffectList.Name = "EffectList";
			this.EffectList.Size = new System.Drawing.Size(398, 289);
			this.EffectList.TabIndex = 1;
			this.EffectList.UseCompatibleStateImageBehavior = false;
			this.EffectList.View = View.Details;
			this.EffectList.DoubleClick += new EventHandler(this.EditBtn_Click);
			this.EffectHdr.Text = "Effect";
			this.EffectHdr.Width = 363;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(398, 314);
			base.Controls.Add(this.EffectList);
			base.Controls.Add(this.Toolbar);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "EffectListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Ongoing Effects";
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedEffect != null)
			{
				CombatData first = this.SelectedEffect.First;
				OngoingCondition second = this.SelectedEffect.Second;
				first.Conditions.Remove(second);
				this.update_list();
			}
		}

		private void update_list()
		{
			this.EffectList.Groups.Clear();
			this.EffectList.Items.Clear();
			foreach (Hero hero in Session.Project.Heroes)
			{
				CombatData combatData = hero.CombatData;
				if (combatData.Conditions.Count <= 0)
				{
					continue;
				}
				this.add_conditions(combatData);
			}
			foreach (EncounterSlot slot in this.fEncounter.Slots)
			{
				foreach (CombatData combatDatum in slot.CombatData)
				{
					if (combatDatum.Conditions.Count <= 0)
					{
						continue;
					}
					this.add_conditions(combatDatum);
				}
			}
		}
	}
}