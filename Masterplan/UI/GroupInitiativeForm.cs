using Masterplan;
using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class GroupInitiativeForm : Form
	{
		private IContainer components;

		private Button CloseBtn;

		private Label InfoLbl;

		private ListView CombatantList;

		private ColumnHeader CombatantHdr;

		private ColumnHeader InitHdr;

		private Dictionary<string, List<CombatData>> fCombatants;

		private Encounter fEncounter;

		public Dictionary<string, List<CombatData>> Combatants
		{
			get
			{
				return this.fCombatants;
			}
		}

		public List<CombatData> SelectedCombatantGroup
		{
			get
			{
				if (this.CombatantList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.CombatantList.SelectedItems[0].Tag as List<CombatData>;
			}
		}

		public GroupInitiativeForm(Dictionary<string, List<CombatData>> combatants, Encounter enc)
		{
			this.InitializeComponent();
			this.fCombatants = combatants;
			this.fEncounter = enc;
			this.update_list();
		}

		private void CombatantList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedCombatantGroup != null)
			{
				int initBonus = 0;
				CombatData item = this.SelectedCombatantGroup[0];
				EncounterSlot encounterSlot = this.fEncounter.FindSlot(item);
				if (encounterSlot == null)
				{
					Hero hero = Session.Project.FindHero(item.ID);
					if (hero != null)
					{
						initBonus = hero.InitBonus;
					}
					Trap trap = this.fEncounter.FindTrap(item.ID);
					if (trap != null)
					{
						initBonus = trap.Initiative;
					}
				}
				else
				{
					initBonus = encounterSlot.Card.Initiative;
				}
				InitiativeForm initiativeForm = new InitiativeForm(initBonus, item.Initiative);
				if (initiativeForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					foreach (CombatData selectedCombatantGroup in this.SelectedCombatantGroup)
					{
						selectedCombatantGroup.Initiative = initiativeForm.Score;
					}
					this.update_list();
				}
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
			this.CloseBtn = new Button();
			this.InfoLbl = new Label();
			this.CombatantList = new ListView();
			this.CombatantHdr = new ColumnHeader();
			this.InitHdr = new ColumnHeader();
			base.SuspendLayout();
			this.CloseBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CloseBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.CloseBtn.Location = new Point(292, 301);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new System.Drawing.Size(75, 23);
			this.CloseBtn.TabIndex = 2;
			this.CloseBtn.Text = "OK";
			this.CloseBtn.UseVisualStyleBackColor = true;
			this.InfoLbl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.InfoLbl.Location = new Point(12, 9);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new System.Drawing.Size(355, 22);
			this.InfoLbl.TabIndex = 0;
			this.InfoLbl.Text = "Double-click on a combatant in the list to set its initiative score.";
			this.CombatantList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			ListView.ColumnHeaderCollection columns = this.CombatantList.Columns;
			ColumnHeader[] combatantHdr = new ColumnHeader[] { this.CombatantHdr, this.InitHdr };
			columns.AddRange(combatantHdr);
			this.CombatantList.FullRowSelect = true;
			this.CombatantList.HideSelection = false;
			this.CombatantList.Location = new Point(12, 34);
			this.CombatantList.MultiSelect = false;
			this.CombatantList.Name = "CombatantList";
			this.CombatantList.Size = new System.Drawing.Size(355, 261);
			this.CombatantList.TabIndex = 1;
			this.CombatantList.UseCompatibleStateImageBehavior = false;
			this.CombatantList.View = View.Details;
			this.CombatantList.DoubleClick += new EventHandler(this.CombatantList_DoubleClick);
			this.CombatantHdr.Text = "Combatant";
			this.CombatantHdr.Width = 234;
			this.InitHdr.Text = "Initiative";
			this.InitHdr.TextAlign = HorizontalAlignment.Right;
			this.InitHdr.Width = 68;
			base.AcceptButton = this.CloseBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(379, 336);
			base.Controls.Add(this.CombatantList);
			base.Controls.Add(this.InfoLbl);
			base.Controls.Add(this.CloseBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "GroupInitiativeForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Set Initiative Scores";
			base.ResumeLayout(false);
		}

		private void update_list()
		{
			this.CombatantList.Items.Clear();
			foreach (string key in this.fCombatants.Keys)
			{
				ListViewItem grayText = this.CombatantList.Items.Add(key);
				List<CombatData> item = this.fCombatants[key];
				CombatData combatDatum = item[0];
				if (combatDatum.Initiative != -2147483648)
				{
					grayText.SubItems.Add(combatDatum.Initiative.ToString());
				}
				else
				{
					grayText.SubItems.Add("(not set)").ForeColor = SystemColors.GrayText;
				}
				grayText.UseItemStyleForSubItems = false;
				grayText.Tag = item;
			}
		}
	}
}