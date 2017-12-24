using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class OngoingDamageForm : Form
	{
		private IContainer components;

		private Button CancelBtn;

		private Button OKBtn;

		private Panel ListPanel;

		private ListView DamageList;

		private ColumnHeader DamageHdr;

		private ColumnHeader ValueHdr;

		private ColumnHeader TakenHdr;

		private Label InfoLbl;

		private CombatData fData;

		private EncounterCard fCard;

		private Encounter fEncounter;

		private int fTotalDamage;

		public OngoingDamageForm(CombatData data, EncounterCard card, Encounter enc)
		{
			this.InitializeComponent();
			this.fData = data;
			this.fCard = card;
			this.fEncounter = enc;
			this.Text = string.Concat("Ongoing Damage: ", this.fData.DisplayName);
			this.update_list();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private DamageModifier find_damage_modifier(DamageType type)
		{
			if (this.fCard == null)
			{
				return null;
			}
			List<DamageType> damageTypes = new List<DamageType>()
			{
				type
			};
			int damageModifier = this.fCard.GetDamageModifier(damageTypes, this.fData);
			if (damageModifier == 0)
			{
				return null;
			}
			if (damageModifier == -2147483648)
			{
				damageModifier = 0;
			}
			return new DamageModifier()
			{
				Type = type,
				Value = damageModifier
			};
		}

		private void InitializeComponent()
		{
			ListViewGroup listViewGroup = new ListViewGroup("Ongoing Damage", HorizontalAlignment.Left);
			ListViewGroup listViewGroup1 = new ListViewGroup("Total Damage", HorizontalAlignment.Left);
			this.CancelBtn = new Button();
			this.OKBtn = new Button();
			this.ListPanel = new Panel();
			this.DamageList = new ListView();
			this.DamageHdr = new ColumnHeader();
			this.ValueHdr = new ColumnHeader();
			this.TakenHdr = new ColumnHeader();
			this.InfoLbl = new Label();
			this.ListPanel.SuspendLayout();
			base.SuspendLayout();
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(361, 242);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 3;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(280, 242);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 2;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.ListPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.ListPanel.Controls.Add(this.DamageList);
			this.ListPanel.Location = new Point(12, 38);
			this.ListPanel.Name = "ListPanel";
			this.ListPanel.Size = new System.Drawing.Size(424, 198);
			this.ListPanel.TabIndex = 9;
			ListView.ColumnHeaderCollection columns = this.DamageList.Columns;
			ColumnHeader[] damageHdr = new ColumnHeader[] { this.DamageHdr, this.ValueHdr, this.TakenHdr };
			columns.AddRange(damageHdr);
			this.DamageList.Dock = DockStyle.Fill;
			this.DamageList.FullRowSelect = true;
			listViewGroup.Header = "Ongoing Damage";
			listViewGroup.Name = "DamageGrp";
			listViewGroup1.Header = "Total Damage";
			listViewGroup1.Name = "TotalGrp";
			this.DamageList.Groups.AddRange(new ListViewGroup[] { listViewGroup, listViewGroup1 });
			this.DamageList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.DamageList.HideSelection = false;
			this.DamageList.Location = new Point(0, 0);
			this.DamageList.MultiSelect = false;
			this.DamageList.Name = "DamageList";
			this.DamageList.Size = new System.Drawing.Size(424, 198);
			this.DamageList.TabIndex = 1;
			this.DamageList.UseCompatibleStateImageBehavior = false;
			this.DamageList.View = View.Details;
			this.DamageHdr.Text = "Damage";
			this.DamageHdr.Width = 229;
			this.ValueHdr.Text = "Modifier";
			this.ValueHdr.Width = 100;
			this.TakenHdr.Text = "Taken";
			this.TakenHdr.TextAlign = HorizontalAlignment.Right;
			this.InfoLbl.AutoSize = true;
			this.InfoLbl.Location = new Point(12, 9);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new System.Drawing.Size(319, 13);
			this.InfoLbl.TabIndex = 10;
			this.InfoLbl.Text = "The following ongoing damage will be applied when you press OK.";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(448, 277);
			base.Controls.Add(this.InfoLbl);
			base.Controls.Add(this.ListPanel);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "OngoingDamageForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Ongoing Damage";
			this.ListPanel.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			int num = this.fTotalDamage;
			if (this.fData.TempHP > 0)
			{
				int num1 = Math.Min(num, this.fData.TempHP);
				CombatData tempHP = this.fData;
				tempHP.TempHP = tempHP.TempHP - num1;
				num -= num1;
			}
			CombatData damage = this.fData;
			damage.Damage = damage.Damage + num;
		}

		private void update_list()
		{
			this.DamageList.Items.Clear();
			this.fTotalDamage = 0;
			foreach (OngoingCondition condition in this.fData.Conditions)
			{
				if (condition.Type != OngoingType.Damage)
				{
					continue;
				}
				int value = condition.Value;
				DamageModifier damageModifier = this.find_damage_modifier(condition.DamageType);
				if (damageModifier != null)
				{
					if (damageModifier.Value != 0)
					{
						value += damageModifier.Value;
						value = Math.Max(value, 0);
					}
					else
					{
						value = 0;
					}
				}
				ListViewItem item = this.DamageList.Items.Add(condition.ToString(this.fEncounter, false));
				item.SubItems.Add((damageModifier != null ? damageModifier.ToString() : ""));
				item.SubItems.Add(value.ToString());
				item.Tag = condition;
				item.Group = this.DamageList.Groups[0];
				this.fTotalDamage += value;
			}
			ListViewItem font = this.DamageList.Items.Add("Total");
			font.SubItems.Add("");
			font.SubItems.Add(this.fTotalDamage.ToString());
			font.Group = this.DamageList.Groups[1];
			font.Font = new System.Drawing.Font(this.Font, this.Font.Style | FontStyle.Bold);
			if (this.fData.Conditions.Count == 0)
			{
				ListViewItem grayText = this.DamageList.Items.Add("(no damage)");
				grayText.ForeColor = SystemColors.GrayText;
			}
		}
	}
}