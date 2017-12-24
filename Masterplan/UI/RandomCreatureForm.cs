using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class RandomCreatureForm : Form
	{
		private IContainer components;

		private Label LevelLbl;

		private NumericUpDown LevelBox;

		private Label RoleLbl;

		private Button RoleBtn;

		private Button OKBtn;

		private Button CancelBtn;

		private IRole fRole;

		public int Level
		{
			get
			{
				return (int)this.LevelBox.Value;
			}
		}

		public IRole Role
		{
			get
			{
				return this.fRole;
			}
		}

		public RandomCreatureForm(int level, IRole role)
		{
			this.InitializeComponent();
			this.fRole = role;
			this.LevelBox.Value = level;
			this.RoleBtn.Text = this.fRole.ToString();
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
			this.LevelLbl = new Label();
			this.LevelBox = new NumericUpDown();
			this.RoleLbl = new Label();
			this.RoleBtn = new Button();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			((ISupportInitialize)this.LevelBox).BeginInit();
			base.SuspendLayout();
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(12, 14);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new System.Drawing.Size(36, 13);
			this.LevelLbl.TabIndex = 2;
			this.LevelLbl.Text = "Level:";
			this.LevelBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.LevelBox.Location = new Point(54, 12);
			NumericUpDown levelBox = this.LevelBox;
			int[] numArray = new int[] { 40, 0, 0, 0 };
			levelBox.Maximum = new decimal(numArray);
			NumericUpDown num = this.LevelBox;
			int[] numArray1 = new int[] { 1, 0, 0, 0 };
			num.Minimum = new decimal(numArray1);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.Size = new System.Drawing.Size(176, 20);
			this.LevelBox.TabIndex = 3;
			NumericUpDown numericUpDown = this.LevelBox;
			int[] numArray2 = new int[] { 1, 0, 0, 0 };
			numericUpDown.Value = new decimal(numArray2);
			this.RoleLbl.AutoSize = true;
			this.RoleLbl.Location = new Point(12, 43);
			this.RoleLbl.Name = "RoleLbl";
			this.RoleLbl.Size = new System.Drawing.Size(32, 13);
			this.RoleLbl.TabIndex = 4;
			this.RoleLbl.Text = "Role:";
			this.RoleBtn.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.RoleBtn.Location = new Point(54, 38);
			this.RoleBtn.Name = "RoleBtn";
			this.RoleBtn.Size = new System.Drawing.Size(176, 23);
			this.RoleBtn.TabIndex = 5;
			this.RoleBtn.Text = "[role]";
			this.RoleBtn.UseVisualStyleBackColor = true;
			this.RoleBtn.Click += new EventHandler(this.RoleBtn_Click);
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(74, 78);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 16;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(155, 78);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 17;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(242, 113);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.RoleBtn);
			base.Controls.Add(this.RoleLbl);
			base.Controls.Add(this.LevelBox);
			base.Controls.Add(this.LevelLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "RandomCreatureForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Random Creature";
			((ISupportInitialize)this.LevelBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
		}

		private void RoleBtn_Click(object sender, EventArgs e)
		{
			RoleForm roleForm = new RoleForm(this.fRole, ThreatType.Creature);
			if (roleForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fRole = roleForm.Role;
				this.RoleBtn.Text = this.fRole.ToString();
			}
		}
	}
}