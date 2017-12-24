using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class PowerRangeForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label RangeLbl;

		private ComboBox RangeBox;

		public string PowerRange
		{
			get
			{
				return this.RangeBox.Text;
			}
		}

		public PowerRangeForm(CreaturePower power)
		{
			this.InitializeComponent();
			this.RangeBox.Items.Add("Melee");
			this.RangeBox.Items.Add("Melee Touch");
			this.RangeBox.Items.Add("Melee Weapon");
			this.RangeBox.Items.Add("Melee N");
			this.RangeBox.Items.Add("Reach N");
			this.RangeBox.Items.Add("Ranged N");
			this.RangeBox.Items.Add("Close Blast N");
			this.RangeBox.Items.Add("Close Burst N");
			this.RangeBox.Items.Add("Area Burst N within N");
			this.RangeBox.Items.Add("Personal");
			this.RangeBox.Text = power.Range;
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
			this.RangeLbl = new Label();
			this.RangeBox = new ComboBox();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(155, 46);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 15;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(236, 46);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 16;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.RangeLbl.AutoSize = true;
			this.RangeLbl.Location = new Point(12, 15);
			this.RangeLbl.Name = "RangeLbl";
			this.RangeLbl.Size = new System.Drawing.Size(42, 13);
			this.RangeLbl.TabIndex = 12;
			this.RangeLbl.Text = "Range:";
			this.RangeBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.RangeBox.AutoCompleteMode = AutoCompleteMode.Append;
			this.RangeBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.RangeBox.FormattingEnabled = true;
			this.RangeBox.Location = new Point(60, 12);
			this.RangeBox.Name = "RangeBox";
			this.RangeBox.Size = new System.Drawing.Size(251, 21);
			this.RangeBox.Sorted = true;
			this.RangeBox.TabIndex = 13;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(323, 81);
			base.Controls.Add(this.RangeBox);
			base.Controls.Add(this.RangeLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PowerRangeForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Power Range";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
		}
	}
}