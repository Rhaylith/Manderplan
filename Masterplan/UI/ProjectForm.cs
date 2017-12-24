using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class ProjectForm : Form
	{
		private Masterplan.Data.Project fProject;

		private IContainer components;

		private Label NameLbl;

		private TextBox NameBox;

		private Button OKBtn;

		private Button CancelBtn;

		private Label SizeLbl;

		private NumericUpDown SizeBox;

		private Label LevelLbl;

		private NumericUpDown LevelBox;

		private TextBox AuthorBox;

		private Label AuthorLbl;

		private Label XPLbl;

		private NumericUpDown XPBox;

		public Masterplan.Data.Project Project
		{
			get
			{
				return this.fProject;
			}
		}

		public ProjectForm(Masterplan.Data.Project p)
		{
			this.InitializeComponent();
			this.fProject = p;
			this.NameBox.Text = this.fProject.Name;
			this.AuthorBox.Text = this.fProject.Author;
			this.SizeBox.Value = this.fProject.Party.Size;
			this.LevelBox.Value = this.fProject.Party.Level;
			this.LevelBox_ValueChanged(null, null);
			this.XPBox.Value = this.fProject.Party.XP;
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
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.SizeLbl = new Label();
			this.SizeBox = new NumericUpDown();
			this.LevelLbl = new Label();
			this.LevelBox = new NumericUpDown();
			this.AuthorBox = new TextBox();
			this.AuthorLbl = new Label();
			this.XPLbl = new Label();
			this.XPBox = new NumericUpDown();
			((ISupportInitialize)this.SizeBox).BeginInit();
			((ISupportInitialize)this.LevelBox).BeginInit();
			((ISupportInitialize)this.XPBox).BeginInit();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new System.Drawing.Size(74, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Project Name:";
			this.NameBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.NameBox.Location = new Point(92, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(263, 20);
			this.NameBox.TabIndex = 1;
			this.NameBox.TextChanged += new EventHandler(this.NameBox_TextChanged);
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(199, 173);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 10;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(280, 173);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 11;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.SizeLbl.AutoSize = true;
			this.SizeLbl.Location = new Point(12, 80);
			this.SizeLbl.Name = "SizeLbl";
			this.SizeLbl.Size = new System.Drawing.Size(57, 13);
			this.SizeLbl.TabIndex = 4;
			this.SizeLbl.Text = "Party Size:";
			this.SizeBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.SizeBox.Location = new Point(92, 78);
			NumericUpDown sizeBox = this.SizeBox;
			int[] numArray = new int[] { 20, 0, 0, 0 };
			sizeBox.Maximum = new decimal(numArray);
			NumericUpDown num = this.SizeBox;
			int[] numArray1 = new int[] { 1, 0, 0, 0 };
			num.Minimum = new decimal(numArray1);
			this.SizeBox.Name = "SizeBox";
			this.SizeBox.Size = new System.Drawing.Size(263, 20);
			this.SizeBox.TabIndex = 5;
			NumericUpDown numericUpDown = this.SizeBox;
			int[] numArray2 = new int[] { 1, 0, 0, 0 };
			numericUpDown.Value = new decimal(numArray2);
			this.SizeBox.ValueChanged += new EventHandler(this.SizeBox_ValueChanged);
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(12, 106);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new System.Drawing.Size(63, 13);
			this.LevelLbl.TabIndex = 6;
			this.LevelLbl.Text = "Party Level:";
			this.LevelBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.LevelBox.Location = new Point(92, 104);
			NumericUpDown levelBox = this.LevelBox;
			int[] numArray3 = new int[] { 30, 0, 0, 0 };
			levelBox.Maximum = new decimal(numArray3);
			NumericUpDown levelBox1 = this.LevelBox;
			int[] numArray4 = new int[] { 1, 0, 0, 0 };
			levelBox1.Minimum = new decimal(numArray4);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.Size = new System.Drawing.Size(263, 20);
			this.LevelBox.TabIndex = 7;
			NumericUpDown num1 = this.LevelBox;
			int[] numArray5 = new int[] { 1, 0, 0, 0 };
			num1.Value = new decimal(numArray5);
			this.LevelBox.ValueChanged += new EventHandler(this.LevelBox_ValueChanged);
			this.AuthorBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.AuthorBox.Location = new Point(92, 38);
			this.AuthorBox.Name = "AuthorBox";
			this.AuthorBox.Size = new System.Drawing.Size(263, 20);
			this.AuthorBox.TabIndex = 3;
			this.AuthorLbl.AutoSize = true;
			this.AuthorLbl.Location = new Point(12, 41);
			this.AuthorLbl.Name = "AuthorLbl";
			this.AuthorLbl.Size = new System.Drawing.Size(41, 13);
			this.AuthorLbl.TabIndex = 2;
			this.AuthorLbl.Text = "Author:";
			this.XPLbl.AutoSize = true;
			this.XPLbl.Location = new Point(12, 132);
			this.XPLbl.Name = "XPLbl";
			this.XPLbl.Size = new System.Drawing.Size(63, 13);
			this.XPLbl.TabIndex = 8;
			this.XPLbl.Text = "Starting XP:";
			this.XPBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			NumericUpDown xPBox = this.XPBox;
			int[] numArray6 = new int[] { 50, 0, 0, 0 };
			xPBox.Increment = new decimal(numArray6);
			this.XPBox.Location = new Point(92, 130);
			NumericUpDown xPBox1 = this.XPBox;
			int[] numArray7 = new int[] { 10000000, 0, 0, 0 };
			xPBox1.Maximum = new decimal(numArray7);
			this.XPBox.Name = "XPBox";
			this.XPBox.Size = new System.Drawing.Size(263, 20);
			this.XPBox.TabIndex = 9;
			this.XPBox.ValueChanged += new EventHandler(this.XPBox_ValueChanged);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(367, 208);
			base.Controls.Add(this.XPBox);
			base.Controls.Add(this.XPLbl);
			base.Controls.Add(this.AuthorBox);
			base.Controls.Add(this.AuthorLbl);
			base.Controls.Add(this.LevelBox);
			base.Controls.Add(this.LevelLbl);
			base.Controls.Add(this.SizeBox);
			base.Controls.Add(this.SizeLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ProjectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Project Properties";
			((ISupportInitialize)this.SizeBox).EndInit();
			((ISupportInitialize)this.LevelBox).EndInit();
			((ISupportInitialize)this.XPBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void LevelBox_ValueChanged(object sender, EventArgs e)
		{
			int value = (int)this.LevelBox.Value;
			this.XPBox.Minimum = Experience.GetHeroXP(value);
			this.XPBox.Maximum = Math.Max(Experience.GetHeroXP(value + 1) - 1, this.XPBox.Minimum);
			this.XPBox.Value = this.XPBox.Minimum;
		}

		private void NameBox_TextChanged(object sender, EventArgs e)
		{
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fProject.Name = this.NameBox.Text;
			this.fProject.Author = this.AuthorBox.Text;
			this.fProject.Party.Size = (int)this.SizeBox.Value;
			this.fProject.Party.Level = (int)this.LevelBox.Value;
			this.fProject.Party.XP = (int)this.XPBox.Value;
			this.fProject.Library.Name = this.fProject.Name;
		}

		private void SizeBox_ValueChanged(object sender, EventArgs e)
		{
		}

		private void XPBox_ValueChanged(object sender, EventArgs e)
		{
		}
	}
}