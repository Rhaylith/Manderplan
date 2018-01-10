using Masterplan.Data;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class TrapActionForm : Form
	{
		private Button CancelBtn;

		private Button OKBtn;

		private Label ActionLbl;

		private ComboBox ActionBox;

		private Label RangeLbl;

		private TextBox RangeBox;

		private Label TargetLbl;

		private TextBox TargetBox;

		private Label NameLbl;

		private TextBox NameBox;

		private TrapAttack fAttack;

		public TrapAttack Attack
		{
			get
			{
				return this.fAttack;
			}
		}

		public TrapActionForm(TrapAttack attack)
		{
			this.InitializeComponent();
			foreach (ActionType value in Enum.GetValues(typeof(ActionType)))
			{
				this.ActionBox.Items.Add(value);
			}
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fAttack = attack.Copy();
			this.NameBox.Text = this.fAttack.Name;
			this.ActionBox.SelectedItem = this.fAttack.Action;
			this.RangeBox.Text = this.fAttack.Range;
			this.TargetBox.Text = this.fAttack.Target;
		}

		private void Application_Idle(object sender, EventArgs e)
		{
		}

		private void InitializeComponent()
		{
			this.CancelBtn = new Button();
			this.OKBtn = new Button();
			this.ActionLbl = new Label();
			this.ActionBox = new ComboBox();
			this.RangeLbl = new Label();
			this.RangeBox = new TextBox();
			this.TargetLbl = new Label();
			this.TargetBox = new TextBox();
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			base.SuspendLayout();
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(222, 127);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 9;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(141, 127);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 8;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.ActionLbl.AutoSize = true;
			this.ActionLbl.Location = new Point(12, 41);
			this.ActionLbl.Name = "ActionLbl";
			this.ActionLbl.Size = new System.Drawing.Size(40, 13);
			this.ActionLbl.TabIndex = 2;
			this.ActionLbl.Text = "Action:";
			this.ActionBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.ActionBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.ActionBox.FormattingEnabled = true;
			this.ActionBox.Location = new Point(60, 38);
			this.ActionBox.Name = "ActionBox";
			this.ActionBox.Size = new System.Drawing.Size(237, 21);
			this.ActionBox.TabIndex = 3;
			this.RangeLbl.AutoSize = true;
			this.RangeLbl.Location = new Point(12, 68);
			this.RangeLbl.Name = "RangeLbl";
			this.RangeLbl.Size = new System.Drawing.Size(42, 13);
			this.RangeLbl.TabIndex = 4;
			this.RangeLbl.Text = "Range:";
			this.RangeBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.RangeBox.Location = new Point(60, 65);
			this.RangeBox.Name = "RangeBox";
			this.RangeBox.Size = new System.Drawing.Size(237, 20);
			this.RangeBox.TabIndex = 5;
			this.TargetLbl.AutoSize = true;
			this.TargetLbl.Location = new Point(12, 94);
			this.TargetLbl.Name = "TargetLbl";
			this.TargetLbl.Size = new System.Drawing.Size(41, 13);
			this.TargetLbl.TabIndex = 6;
			this.TargetLbl.Text = "Target:";
			this.TargetBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.TargetBox.Location = new Point(60, 91);
			this.TargetBox.Name = "TargetBox";
			this.TargetBox.Size = new System.Drawing.Size(237, 20);
			this.TargetBox.TabIndex = 7;
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new System.Drawing.Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.NameBox.Location = new Point(60, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(237, 20);
			this.NameBox.TabIndex = 1;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(309, 162);
			base.Controls.Add(this.NameLbl);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.RangeLbl);
			base.Controls.Add(this.RangeBox);
			base.Controls.Add(this.TargetLbl);
			base.Controls.Add(this.TargetBox);
			base.Controls.Add(this.ActionBox);
			base.Controls.Add(this.ActionLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TrapActionForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Trap Attack";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fAttack.Name = this.NameBox.Text;
			this.fAttack.Action = (ActionType)this.ActionBox.SelectedItem;
			this.fAttack.Range = this.RangeBox.Text;
			this.fAttack.Target = this.TargetBox.Text;
		}
	}
}