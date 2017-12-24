using Masterplan;
using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class HealForm : Form
	{
		private List<Pair<CombatData, EncounterCard>> fTokens;

		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label HPLbl;

		private NumericUpDown HPBox;

		private CheckBox TempHPBox;

		private Label SurgeLbl;

		private NumericUpDown SurgeBox;

		public HealForm(List<Pair<CombatData, EncounterCard>> tokens)
		{
			this.InitializeComponent();
			this.fTokens = tokens;
		}

		private void DamageForm_Shown(object sender, EventArgs e)
		{
			this.SurgeBox.Select(0, 1);
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
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.HPLbl = new Label();
			this.HPBox = new NumericUpDown();
			this.TempHPBox = new CheckBox();
			this.SurgeLbl = new Label();
			this.SurgeBox = new NumericUpDown();
			((ISupportInitialize)this.HPBox).BeginInit();
			((ISupportInitialize)this.SurgeBox).BeginInit();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(116, 110);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 5;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(197, 110);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 6;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.HPLbl.AutoSize = true;
			this.HPLbl.Location = new Point(12, 40);
			this.HPLbl.Name = "HPLbl";
			this.HPLbl.Size = new System.Drawing.Size(25, 13);
			this.HPLbl.TabIndex = 2;
			this.HPLbl.Text = "HP:";
			this.HPBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.HPBox.Location = new Point(68, 38);
			NumericUpDown hPBox = this.HPBox;
			int[] numArray = new int[] { 1000, 0, 0, 0 };
			hPBox.Maximum = new decimal(numArray);
			this.HPBox.Name = "HPBox";
			this.HPBox.Size = new System.Drawing.Size(204, 20);
			this.HPBox.TabIndex = 3;
			this.TempHPBox.AutoSize = true;
			this.TempHPBox.Location = new Point(68, 75);
			this.TempHPBox.Name = "TempHPBox";
			this.TempHPBox.Size = new System.Drawing.Size(153, 17);
			this.TempHPBox.TabIndex = 4;
			this.TempHPBox.Text = "Add as temporary hit points";
			this.TempHPBox.UseVisualStyleBackColor = true;
			this.SurgeLbl.AutoSize = true;
			this.SurgeLbl.Location = new Point(12, 14);
			this.SurgeLbl.Name = "SurgeLbl";
			this.SurgeLbl.Size = new System.Drawing.Size(43, 13);
			this.SurgeLbl.TabIndex = 0;
			this.SurgeLbl.Text = "Surges:";
			this.SurgeBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.SurgeBox.Location = new Point(68, 12);
			NumericUpDown surgeBox = this.SurgeBox;
			int[] numArray1 = new int[] { 10, 0, 0, 0 };
			surgeBox.Maximum = new decimal(numArray1);
			this.SurgeBox.Name = "SurgeBox";
			this.SurgeBox.Size = new System.Drawing.Size(204, 20);
			this.SurgeBox.TabIndex = 1;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(284, 145);
			base.Controls.Add(this.SurgeBox);
			base.Controls.Add(this.SurgeLbl);
			base.Controls.Add(this.TempHPBox);
			base.Controls.Add(this.HPBox);
			base.Controls.Add(this.HPLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "HealForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Heal";
			base.Shown += new EventHandler(this.DamageForm_Shown);
			((ISupportInitialize)this.HPBox).EndInit();
			((ISupportInitialize)this.SurgeBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			int value = (int)this.SurgeBox.Value;
			int num = (int)this.HPBox.Value;
			foreach (Pair<CombatData, EncounterCard> fToken in this.fTokens)
			{
				int hP = 0;
				if (fToken.Second == null)
				{
					Hero hero = Session.Project.FindHero(fToken.First.ID);
					if (hero != null)
					{
						hP = hero.HP;
					}
				}
				else
				{
					hP = fToken.Second.HP;
				}
				int num1 = hP / 4 * value + num;
				if (!this.TempHPBox.Checked)
				{
					if (fToken.First.Damage > hP)
					{
						fToken.First.Damage = hP;
					}
					CombatData first = fToken.First;
					first.Damage = first.Damage - num1;
					if (fToken.First.Damage >= 0)
					{
						continue;
					}
					fToken.First.Damage = 0;
				}
				else
				{
					fToken.First.TempHP = Math.Max(num1, fToken.First.TempHP);
				}
			}
		}
	}
}