using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class InitiativeForm : Form
	{
		private Label InitLbl;

		private Button OKBtn;

		private Button CancelBtn;

		private NumericUpDown InitBox;

		private Label BonusLbl;

		private Label BonusValueLbl;

		public int Score
		{
			get
			{
				return (int)this.InitBox.Value;
			}
		}

		public InitiativeForm(int bonus, int score)
		{
			this.InitializeComponent();
			if (bonus < 0)
			{
				this.BonusValueLbl.Text = bonus.ToString();
			}
			else
			{
				this.BonusValueLbl.Text = string.Concat("+", bonus);
			}
			if (score == -2147483648)
			{
				score = bonus + 1;
			}
			this.InitBox.Value = score;
		}

		private void InitializeComponent()
		{
			this.InitLbl = new Label();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.InitBox = new NumericUpDown();
			this.BonusLbl = new Label();
			this.BonusValueLbl = new Label();
			((ISupportInitialize)this.InitBox).BeginInit();
			base.SuspendLayout();
			this.InitLbl.AutoSize = true;
			this.InitLbl.Location = new Point(12, 36);
			this.InitLbl.Name = "InitLbl";
			this.InitLbl.Size = new System.Drawing.Size(38, 13);
			this.InitLbl.TabIndex = 2;
			this.InitLbl.Text = "Score:";
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(79, 69);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 4;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(160, 69);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 5;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.InitBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.InitBox.Location = new Point(61, 34);
			NumericUpDown initBox = this.InitBox;
			int[] numArray = new int[] { 100, 0, 0, -2147483648 };
			initBox.Minimum = new decimal(numArray);
			this.InitBox.Name = "InitBox";
			this.InitBox.Size = new System.Drawing.Size(174, 20);
			this.InitBox.TabIndex = 3;
			this.BonusLbl.AutoSize = true;
			this.BonusLbl.Location = new Point(12, 9);
			this.BonusLbl.Name = "BonusLbl";
			this.BonusLbl.Size = new System.Drawing.Size(40, 13);
			this.BonusLbl.TabIndex = 0;
			this.BonusLbl.Text = "Bonus:";
			this.BonusValueLbl.AutoSize = true;
			this.BonusValueLbl.Location = new Point(58, 9);
			this.BonusValueLbl.Name = "BonusValueLbl";
			this.BonusValueLbl.Size = new System.Drawing.Size(58, 13);
			this.BonusValueLbl.TabIndex = 1;
			this.BonusValueLbl.Text = "[init bonus]";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(247, 104);
			base.Controls.Add(this.BonusValueLbl);
			base.Controls.Add(this.BonusLbl);
			base.Controls.Add(this.InitBox);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.InitLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "InitiativeForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Initiative";
			base.Shown += new EventHandler(this.InitiativeForm_Shown);
			((ISupportInitialize)this.InitBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void InitiativeForm_Shown(object sender, EventArgs e)
		{
			int num = 1;
			if (this.InitBox.Value >= new decimal(10))
			{
				num = 2;
			}
			if (this.InitBox.Value >= new decimal(100))
			{
				num = 3;
			}
			this.InitBox.Select(0, num);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
		}
	}
}