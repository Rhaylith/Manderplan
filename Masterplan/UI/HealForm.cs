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
			this.HPBox.Select(0, 1);
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
            this.OKBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.HPLbl = new System.Windows.Forms.Label();
            this.HPBox = new System.Windows.Forms.NumericUpDown();
            this.TempHPBox = new System.Windows.Forms.CheckBox();
            this.SurgeLbl = new System.Windows.Forms.Label();
            this.SurgeBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.HPBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SurgeBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OKBtn
            // 
            this.OKBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKBtn.Location = new System.Drawing.Point(116, 110);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 4;
            this.OKBtn.Text = "OK";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(197, 110);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 5;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // HPLbl
            // 
            this.HPLbl.AutoSize = true;
            this.HPLbl.Location = new System.Drawing.Point(12, 14);
            this.HPLbl.Name = "HPLbl";
            this.HPLbl.Size = new System.Drawing.Size(25, 13);
            this.HPLbl.TabIndex = 0;
            this.HPLbl.Text = "HP:";
            // 
            // HPBox
            // 
            this.HPBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HPBox.Location = new System.Drawing.Point(68, 12);
            this.HPBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.HPBox.Name = "HPBox";
            this.HPBox.Size = new System.Drawing.Size(204, 20);
            this.HPBox.TabIndex = 1;
            // 
            // TempHPBox
            // 
            this.TempHPBox.AutoSize = true;
            this.TempHPBox.Location = new System.Drawing.Point(68, 75);
            this.TempHPBox.Name = "TempHPBox";
            this.TempHPBox.Size = new System.Drawing.Size(153, 17);
            this.TempHPBox.TabIndex = 3;
            this.TempHPBox.Text = "Add as temporary hit points";
            this.TempHPBox.UseVisualStyleBackColor = true;
            // 
            // SurgeLbl
            // 
            this.SurgeLbl.AutoSize = true;
            this.SurgeLbl.Location = new System.Drawing.Point(12, 40);
            this.SurgeLbl.Name = "SurgeLbl";
            this.SurgeLbl.Size = new System.Drawing.Size(43, 13);
            this.SurgeLbl.TabIndex = 0;
            this.SurgeLbl.Text = "Surges:";
            // 
            // SurgeBox
            // 
            this.SurgeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SurgeBox.Location = new System.Drawing.Point(68, 38);
            this.SurgeBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.SurgeBox.Name = "SurgeBox";
            this.SurgeBox.Size = new System.Drawing.Size(204, 20);
            this.SurgeBox.TabIndex = 2;
            // 
            // HealForm
            // 
            this.AcceptButton = this.OKBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(284, 145);
            this.Controls.Add(this.SurgeBox);
            this.Controls.Add(this.SurgeLbl);
            this.Controls.Add(this.TempHPBox);
            this.Controls.Add(this.HPBox);
            this.Controls.Add(this.HPLbl);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OKBtn);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HealForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Heal";
            this.Shown += new System.EventHandler(this.DamageForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.HPBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SurgeBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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