using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class CampaignSettingsForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label HPLbl;

		private NumericUpDown HPBox;

		private NumericUpDown AttackBox;

		private Label AttackLbl;

		private NumericUpDown ACBox;

		private Label ACLbl;

		private Label InfoLbl;

		private NumericUpDown DefenceBox;

		private Label DefenceLbl;

		private NumericUpDown XPBox;

		private Label XPLbl;

		private CampaignSettings fSettings;

		public CampaignSettingsForm(CampaignSettings settings)
		{
			this.InitializeComponent();
			this.fSettings = settings;
			this.HPBox.Value = (int)(this.fSettings.HP * 100);
			this.XPBox.Value = (int)(this.fSettings.XP * 100);
			this.AttackBox.Value = this.fSettings.AttackBonus;
			this.ACBox.Value = this.fSettings.ACBonus;
			this.DefenceBox.Value = this.fSettings.NADBonus;
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
			this.AttackBox = new NumericUpDown();
			this.AttackLbl = new Label();
			this.ACBox = new NumericUpDown();
			this.ACLbl = new Label();
			this.InfoLbl = new Label();
			this.DefenceBox = new NumericUpDown();
			this.DefenceLbl = new Label();
			this.XPBox = new NumericUpDown();
			this.XPLbl = new Label();
			((ISupportInitialize)this.HPBox).BeginInit();
			((ISupportInitialize)this.AttackBox).BeginInit();
			((ISupportInitialize)this.ACBox).BeginInit();
			((ISupportInitialize)this.DefenceBox).BeginInit();
			((ISupportInitialize)this.XPBox).BeginInit();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(110, 231);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 11;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(191, 231);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 12;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.HPLbl.AutoSize = true;
			this.HPLbl.Location = new Point(12, 55);
			this.HPLbl.Name = "HPLbl";
			this.HPLbl.Size = new System.Drawing.Size(72, 13);
			this.HPLbl.TabIndex = 1;
			this.HPLbl.Text = "Hit Points (%):";
			this.HPBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			NumericUpDown hPBox = this.HPBox;
			int[] numArray = new int[] { 5, 0, 0, 0 };
			hPBox.Increment = new decimal(numArray);
			this.HPBox.Location = new Point(130, 53);
			NumericUpDown num = this.HPBox;
			int[] numArray1 = new int[] { 1000, 0, 0, 0 };
			num.Maximum = new decimal(numArray1);
			this.HPBox.Name = "HPBox";
			this.HPBox.Size = new System.Drawing.Size(136, 20);
			this.HPBox.TabIndex = 2;
			this.AttackBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.AttackBox.Location = new Point(130, 121);
			NumericUpDown attackBox = this.AttackBox;
			int[] numArray2 = new int[] { 100, 0, 0, -2147483648 };
			attackBox.Minimum = new decimal(numArray2);
			this.AttackBox.Name = "AttackBox";
			this.AttackBox.Size = new System.Drawing.Size(136, 20);
			this.AttackBox.TabIndex = 6;
			this.AttackLbl.AutoSize = true;
			this.AttackLbl.Location = new Point(12, 123);
			this.AttackLbl.Name = "AttackLbl";
			this.AttackLbl.Size = new System.Drawing.Size(74, 13);
			this.AttackLbl.TabIndex = 5;
			this.AttackLbl.Text = "Attack Bonus:";
			this.ACBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.ACBox.Location = new Point(130, 163);
			NumericUpDown aCBox = this.ACBox;
			int[] numArray3 = new int[] { 100, 0, 0, -2147483648 };
			aCBox.Minimum = new decimal(numArray3);
			this.ACBox.Name = "ACBox";
			this.ACBox.Size = new System.Drawing.Size(136, 20);
			this.ACBox.TabIndex = 8;
			this.ACLbl.AutoSize = true;
			this.ACLbl.Location = new Point(12, 165);
			this.ACLbl.Name = "ACLbl";
			this.ACLbl.Size = new System.Drawing.Size(24, 13);
			this.ACLbl.TabIndex = 7;
			this.ACLbl.Text = "AC:";
			this.InfoLbl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.InfoLbl.Location = new Point(12, 9);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new System.Drawing.Size(254, 33);
			this.InfoLbl.TabIndex = 0;
			this.InfoLbl.Text = "These settings will apply to all the creatures, traps and hazards in the campaign.";
			this.DefenceBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.DefenceBox.Location = new Point(130, 189);
			NumericUpDown defenceBox = this.DefenceBox;
			int[] numArray4 = new int[] { 100, 0, 0, -2147483648 };
			defenceBox.Minimum = new decimal(numArray4);
			this.DefenceBox.Name = "DefenceBox";
			this.DefenceBox.Size = new System.Drawing.Size(136, 20);
			this.DefenceBox.TabIndex = 10;
			this.DefenceLbl.AutoSize = true;
			this.DefenceLbl.Location = new Point(12, 191);
			this.DefenceLbl.Name = "DefenceLbl";
			this.DefenceLbl.Size = new System.Drawing.Size(85, 13);
			this.DefenceLbl.TabIndex = 9;
			this.DefenceLbl.Text = "Other Defences:";
			this.XPBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			NumericUpDown xPBox = this.XPBox;
			int[] numArray5 = new int[] { 5, 0, 0, 0 };
			xPBox.Increment = new decimal(numArray5);
			this.XPBox.Location = new Point(130, 79);
			NumericUpDown numericUpDown = this.XPBox;
			int[] numArray6 = new int[] { 1000, 0, 0, 0 };
			numericUpDown.Maximum = new decimal(numArray6);
			this.XPBox.Name = "XPBox";
			this.XPBox.Size = new System.Drawing.Size(136, 20);
			this.XPBox.TabIndex = 4;
			this.XPLbl.AutoSize = true;
			this.XPLbl.Location = new Point(12, 81);
			this.XPLbl.Name = "XPLbl";
			this.XPLbl.Size = new System.Drawing.Size(112, 13);
			this.XPLbl.TabIndex = 3;
			this.XPLbl.Text = "Experience Points (%):";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(278, 266);
			base.Controls.Add(this.XPBox);
			base.Controls.Add(this.XPLbl);
			base.Controls.Add(this.DefenceBox);
			base.Controls.Add(this.DefenceLbl);
			base.Controls.Add(this.InfoLbl);
			base.Controls.Add(this.ACBox);
			base.Controls.Add(this.ACLbl);
			base.Controls.Add(this.AttackBox);
			base.Controls.Add(this.AttackLbl);
			base.Controls.Add(this.HPBox);
			base.Controls.Add(this.HPLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CampaignSettingsForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Campaign Settings";
			((ISupportInitialize)this.HPBox).EndInit();
			((ISupportInitialize)this.AttackBox).EndInit();
			((ISupportInitialize)this.ACBox).EndInit();
			((ISupportInitialize)this.DefenceBox).EndInit();
			((ISupportInitialize)this.XPBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fSettings.HP = (double)((double)this.HPBox.Value) / 100;
			this.fSettings.XP = (double)((double)this.XPBox.Value) / 100;
			this.fSettings.AttackBonus = (int)this.AttackBox.Value;
			this.fSettings.ACBonus = (int)this.ACBox.Value;
			this.fSettings.NADBonus = (int)this.DefenceBox.Value;
		}
	}
}