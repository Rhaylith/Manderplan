using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class LevelForm : Form
	{
		private Label LevelLbl;

		private Button OKBtn;

		private Button CancelBtn;

		private NumericUpDown LevelBox;

		public int Level
		{
			get
			{
				return (int)this.LevelBox.Value;
			}
		}

		public LevelForm(int score)
		{
			this.InitializeComponent();
			if (score == -2147483648)
			{
				score = 0;
			}
			this.LevelBox.Value = score;
		}

        private void InitializeComponent()
		{
			this.LevelLbl = new Label();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.LevelBox = new NumericUpDown();
			((ISupportInitialize)this.LevelBox).BeginInit();
			base.SuspendLayout();
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(12, 14);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new System.Drawing.Size(36, 13);
			this.LevelLbl.TabIndex = 0;
			this.LevelLbl.Text = "Level:";
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(55, 45);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 2;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(136, 45);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 3;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.LevelBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.LevelBox.Location = new Point(54, 12);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.Size = new System.Drawing.Size(157, 20);
			this.LevelBox.TabIndex = 1;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(223, 80);
			base.Controls.Add(this.LevelBox);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.LevelLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "LevelForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Level";
			((ISupportInitialize)this.LevelBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
		}
	}
}