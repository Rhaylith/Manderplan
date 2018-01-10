using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class SatelliteForm : Form
	{
		private Button OKBtn;

		private Button CancelBtn;

		private Label NameLbl;

		private TextBox NameBox;

		private Label PeriodLbl;

		private NumericUpDown PeriodBox;

		private NumericUpDown OffsetBox;

		private Label OffsetLbl;

		private Masterplan.Data.Satellite fSatellite;

		public Masterplan.Data.Satellite Satellite
		{
			get
			{
				return this.fSatellite;
			}
		}

		public SatelliteForm(Masterplan.Data.Satellite sat)
		{
			this.InitializeComponent();
			this.fSatellite = sat.Copy();
			if (this.fSatellite.Period == 0)
			{
				this.fSatellite.Period = 1;
			}
			this.NameBox.Text = this.fSatellite.Name;
			this.PeriodBox.Value = this.fSatellite.Period;
			this.OffsetBox.Value = this.fSatellite.Offset;
		}

		private void InitializeComponent()
		{
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.PeriodLbl = new Label();
			this.PeriodBox = new NumericUpDown();
			this.OffsetBox = new NumericUpDown();
			this.OffsetLbl = new Label();
			((ISupportInitialize)this.PeriodBox).BeginInit();
			((ISupportInitialize)this.OffsetBox).BeginInit();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(97, 100);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 6;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(178, 100);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 7;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new System.Drawing.Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.NameBox.Location = new Point(58, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(195, 20);
			this.NameBox.TabIndex = 1;
			this.PeriodLbl.AutoSize = true;
			this.PeriodLbl.Location = new Point(12, 40);
			this.PeriodLbl.Name = "PeriodLbl";
			this.PeriodLbl.Size = new System.Drawing.Size(40, 13);
			this.PeriodLbl.TabIndex = 2;
			this.PeriodLbl.Text = "Period:";
			this.PeriodBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.PeriodBox.Location = new Point(58, 38);
			NumericUpDown periodBox = this.PeriodBox;
			int[] numArray = new int[] { 10000, 0, 0, 0 };
			periodBox.Maximum = new decimal(numArray);
			NumericUpDown num = this.PeriodBox;
			int[] numArray1 = new int[] { 1, 0, 0, 0 };
			num.Minimum = new decimal(numArray1);
			this.PeriodBox.Name = "PeriodBox";
			this.PeriodBox.Size = new System.Drawing.Size(195, 20);
			this.PeriodBox.TabIndex = 3;
			NumericUpDown numericUpDown = this.PeriodBox;
			int[] numArray2 = new int[] { 1, 0, 0, 0 };
			numericUpDown.Value = new decimal(numArray2);
			this.OffsetBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.OffsetBox.Location = new Point(58, 64);
			NumericUpDown offsetBox = this.OffsetBox;
			int[] numArray3 = new int[] { 10000, 0, 0, 0 };
			offsetBox.Maximum = new decimal(numArray3);
			this.OffsetBox.Name = "OffsetBox";
			this.OffsetBox.Size = new System.Drawing.Size(195, 20);
			this.OffsetBox.TabIndex = 5;
			this.OffsetLbl.AutoSize = true;
			this.OffsetLbl.Location = new Point(12, 66);
			this.OffsetLbl.Name = "OffsetLbl";
			this.OffsetLbl.Size = new System.Drawing.Size(38, 13);
			this.OffsetLbl.TabIndex = 4;
			this.OffsetLbl.Text = "Offset:";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(265, 135);
			base.Controls.Add(this.OffsetBox);
			base.Controls.Add(this.OffsetLbl);
			base.Controls.Add(this.PeriodBox);
			base.Controls.Add(this.PeriodLbl);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SatelliteForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Satellite";
			((ISupportInitialize)this.PeriodBox).EndInit();
			((ISupportInitialize)this.OffsetBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fSatellite.Name = this.NameBox.Text;
			this.fSatellite.Period = (int)this.PeriodBox.Value;
			this.fSatellite.Offset = (int)this.OffsetBox.Value;
		}
	}
}