using Masterplan;
using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class CombatantSelectForm : Form
	{
		private Button OKBtn;

		private ListView CombatantList;

		private Button CancelBtn;

		private ColumnHeader NameHdr;

		public CombatData SelectedCombatant
		{
			get
			{
				if (this.CombatantList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.CombatantList.SelectedItems[0].Tag as CombatData;
			}
		}

		public CombatantSelectForm(Encounter enc)
		{
			this.InitializeComponent();
			foreach (EncounterSlot slot in enc.Slots)
			{
				foreach (CombatData combatDatum in slot.CombatData)
				{
					ListViewItem item = this.CombatantList.Items.Add(combatDatum.DisplayName);
					item.Tag = combatDatum;
					item.Group = this.CombatantList.Groups[1];
				}
			}
			foreach (Hero hero in Session.Project.Heroes)
			{
				ListViewItem combatData = this.CombatantList.Items.Add(hero.Name);
				combatData.Tag = hero.CombatData;
				combatData.Group = this.CombatantList.Groups[0];
			}
			foreach (Trap trap in enc.Traps)
			{
				ListViewItem listViewItem = this.CombatantList.Items.Add(trap.CombatData.DisplayName);
				listViewItem.Tag = trap.CombatData;
				listViewItem.Group = this.CombatantList.Groups[2];
			}
			Application.Idle += new EventHandler(this.Application_Idle);
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = this.SelectedCombatant != null;
		}

		private void InitializeComponent()
		{
			ListViewGroup listViewGroup = new ListViewGroup("PCs", HorizontalAlignment.Left);
			ListViewGroup listViewGroup1 = new ListViewGroup("Creatures", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Traps", HorizontalAlignment.Left);
			this.OKBtn = new Button();
			this.CombatantList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.CancelBtn = new Button();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(166, 354);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.CombatantList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.CombatantList.Columns.AddRange(new ColumnHeader[] { this.NameHdr });
			this.CombatantList.FullRowSelect = true;
			listViewGroup.Header = "PCs";
			listViewGroup.Name = "listViewGroup1";
			listViewGroup1.Header = "Creatures";
			listViewGroup1.Name = "listViewGroup2";
			listViewGroup2.Header = "Traps";
			listViewGroup2.Name = "listViewGroup3";
			this.CombatantList.Groups.AddRange(new ListViewGroup[] { listViewGroup, listViewGroup1, listViewGroup2 });
			this.CombatantList.HideSelection = false;
			this.CombatantList.Location = new Point(12, 12);
			this.CombatantList.MultiSelect = false;
			this.CombatantList.Name = "CombatantList";
			this.CombatantList.Size = new System.Drawing.Size(310, 336);
			this.CombatantList.Sorting = SortOrder.Ascending;
			this.CombatantList.TabIndex = 0;
			this.CombatantList.UseCompatibleStateImageBehavior = false;
			this.CombatantList.View = View.Details;
			this.CombatantList.DoubleClick += new EventHandler(this.TileList_DoubleClick);
			this.NameHdr.Text = "Name";
			this.NameHdr.Width = 270;
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(247, 354);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(334, 389);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.CombatantList);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CombatantSelectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Select Combatant";
			base.ResumeLayout(false);
		}

		private void TileList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedCombatant != null)
			{
				base.DialogResult = System.Windows.Forms.DialogResult.OK;
				base.Close();
			}
		}
	}
}