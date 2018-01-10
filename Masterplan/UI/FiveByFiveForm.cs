using Masterplan.Controls;
using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class FiveByFiveForm : Form
	{
		private Button OKBtn;

		private Button CancelBtn;

		private Masterplan.Controls.FiveByFivePanel FiveByFivePanel;

		private FiveByFiveData f5x5;

		private bool fCreatePlot;

		public bool CreatePlot
		{
			get
			{
				return this.fCreatePlot;
			}
		}

		public FiveByFiveData FiveByFive
		{
			get
			{
				return this.f5x5;
			}
		}

		public FiveByFiveForm(FiveByFiveData five_by_five)
		{
			this.InitializeComponent();
			this.f5x5 = five_by_five.Copy();
			this.FiveByFivePanel.Data = this.f5x5;
		}

		private void FiveByFiveForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.DialogResult == System.Windows.Forms.DialogResult.OK)
			{
				System.Windows.Forms.DialogResult dialogResult = MessageBox.Show("Do you want to build a plotline from these items?", "Masterplan", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (dialogResult != System.Windows.Forms.DialogResult.Cancel)
				{
					switch (dialogResult)
					{
						case System.Windows.Forms.DialogResult.Yes:
						{
							this.fCreatePlot = true;
							return;
						}
						case System.Windows.Forms.DialogResult.No:
						{
							this.fCreatePlot = false;
							return;
						}
						default:
						{
							return;
						}
					}
				}
				e.Cancel = true;
			}
		}

		private void InitializeComponent()
		{
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.FiveByFivePanel = new Masterplan.Controls.FiveByFivePanel();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(401, 306);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 0;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(482, 306);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 1;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.FiveByFivePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.FiveByFivePanel.BorderStyle = BorderStyle.FixedSingle;
			this.FiveByFivePanel.Location = new Point(12, 12);
			this.FiveByFivePanel.Name = "FiveByFivePanel";
			this.FiveByFivePanel.Size = new System.Drawing.Size(545, 288);
			this.FiveByFivePanel.TabIndex = 2;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(569, 341);
			base.Controls.Add(this.FiveByFivePanel);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FiveByFiveForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Five by Five";
			base.FormClosing += new FormClosingEventHandler(this.FiveByFiveForm_FormClosing);
			base.ResumeLayout(false);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
		}
	}
}