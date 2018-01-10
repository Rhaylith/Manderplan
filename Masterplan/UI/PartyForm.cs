using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class PartyForm : Form
	{
		private Button OKBtn;

		private Button CancelBtn;

		private Label SizeLbl;

		private NumericUpDown SizeBox;

		private Label LevelLbl;

		private NumericUpDown LevelBox;

		private Masterplan.Data.Party fParty;

		public Masterplan.Data.Party Party
		{
			get
			{
				return this.fParty;
			}
		}

		public PartyForm(Masterplan.Data.Party p)
		{
			this.InitializeComponent();
			this.fParty = p;
			this.SizeBox.Value = this.fParty.Size;
			this.LevelBox.Value = this.fParty.Level;
		}

		private void InitializeComponent()
		{
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.SizeLbl = new Label();
			this.SizeBox = new NumericUpDown();
			this.LevelLbl = new Label();
			this.LevelBox = new NumericUpDown();
			((ISupportInitialize)this.SizeBox).BeginInit();
			((ISupportInitialize)this.LevelBox).BeginInit();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(108, 75);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 4;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(189, 75);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 5;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.SizeLbl.AutoSize = true;
			this.SizeLbl.Location = new Point(12, 14);
			this.SizeLbl.Name = "SizeLbl";
			this.SizeLbl.Size = new System.Drawing.Size(57, 13);
			this.SizeLbl.TabIndex = 0;
			this.SizeLbl.Text = "Party Size:";
			this.SizeBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.SizeBox.Location = new Point(81, 12);
			NumericUpDown sizeBox = this.SizeBox;
			int[] numArray = new int[] { 20, 0, 0, 0 };
			sizeBox.Maximum = new decimal(numArray);
			NumericUpDown num = this.SizeBox;
			int[] numArray1 = new int[] { 1, 0, 0, 0 };
			num.Minimum = new decimal(numArray1);
			this.SizeBox.Name = "SizeBox";
			this.SizeBox.Size = new System.Drawing.Size(183, 20);
			this.SizeBox.TabIndex = 1;
			NumericUpDown numericUpDown = this.SizeBox;
			int[] numArray2 = new int[] { 1, 0, 0, 0 };
			numericUpDown.Value = new decimal(numArray2);
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(12, 40);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new System.Drawing.Size(63, 13);
			this.LevelLbl.TabIndex = 2;
			this.LevelLbl.Text = "Party Level:";
			this.LevelBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.LevelBox.Location = new Point(81, 38);
			NumericUpDown levelBox = this.LevelBox;
			int[] numArray3 = new int[] { 30, 0, 0, 0 };
			levelBox.Maximum = new decimal(numArray3);
			NumericUpDown levelBox1 = this.LevelBox;
			int[] numArray4 = new int[] { 1, 0, 0, 0 };
			levelBox1.Minimum = new decimal(numArray4);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.Size = new System.Drawing.Size(183, 20);
			this.LevelBox.TabIndex = 3;
			NumericUpDown num1 = this.LevelBox;
			int[] numArray5 = new int[] { 1, 0, 0, 0 };
			num1.Value = new decimal(numArray5);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(276, 110);
			base.Controls.Add(this.LevelBox);
			base.Controls.Add(this.LevelLbl);
			base.Controls.Add(this.SizeBox);
			base.Controls.Add(this.SizeLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PartyForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Party";
			((ISupportInitialize)this.SizeBox).EndInit();
			((ISupportInitialize)this.LevelBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fParty.Size = (int)this.SizeBox.Value;
			this.fParty.Level = (int)this.LevelBox.Value;
		}
	}
}