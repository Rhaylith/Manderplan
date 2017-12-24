using Masterplan.Tools.Generators;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils.Wizards;

namespace Masterplan.Wizards
{
	internal class MapSizePage : UserControl, IWizardPage
	{
		private MapBuilderData fData;

		private IContainer components;

		private Label InfoLbl;

		private NumericUpDown HeightBox;

		private Label HeightLbl;

		private NumericUpDown WidthBox;

		private Label WidthLbl;

		public bool AllowBack
		{
			get
			{
				return true;
			}
		}

		public bool AllowFinish
		{
			get
			{
				return true;
			}
		}

		public bool AllowNext
		{
			get
			{
				return false;
			}
		}

		public MapSizePage()
		{
			this.InitializeComponent();
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
			this.InfoLbl = new Label();
			this.HeightBox = new NumericUpDown();
			this.HeightLbl = new Label();
			this.WidthBox = new NumericUpDown();
			this.WidthLbl = new Label();
			((ISupportInitialize)this.HeightBox).BeginInit();
			((ISupportInitialize)this.WidthBox).BeginInit();
			base.SuspendLayout();
			this.InfoLbl.Dock = DockStyle.Top;
			this.InfoLbl.Location = new Point(0, 0);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new System.Drawing.Size(307, 40);
			this.InfoLbl.TabIndex = 0;
			this.InfoLbl.Text = "What size of map would you like to build?";
			this.HeightBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.HeightBox.Location = new Point(58, 66);
			NumericUpDown heightBox = this.HeightBox;
			int[] numArray = new int[] { 200, 0, 0, 0 };
			heightBox.Maximum = new decimal(numArray);
			NumericUpDown num = this.HeightBox;
			int[] numArray1 = new int[] { 1, 0, 0, 0 };
			num.Minimum = new decimal(numArray1);
			this.HeightBox.Name = "HeightBox";
			this.HeightBox.Size = new System.Drawing.Size(246, 20);
			this.HeightBox.TabIndex = 10;
			NumericUpDown numericUpDown = this.HeightBox;
			int[] numArray2 = new int[] { 1, 0, 0, 0 };
			numericUpDown.Value = new decimal(numArray2);
			this.HeightLbl.AutoSize = true;
			this.HeightLbl.Location = new Point(3, 68);
			this.HeightLbl.Name = "HeightLbl";
			this.HeightLbl.Size = new System.Drawing.Size(41, 13);
			this.HeightLbl.TabIndex = 9;
			this.HeightLbl.Text = "Height:";
			this.WidthBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.WidthBox.Location = new Point(58, 40);
			NumericUpDown widthBox = this.WidthBox;
			int[] numArray3 = new int[] { 200, 0, 0, 0 };
			widthBox.Maximum = new decimal(numArray3);
			NumericUpDown widthBox1 = this.WidthBox;
			int[] numArray4 = new int[] { 1, 0, 0, 0 };
			widthBox1.Minimum = new decimal(numArray4);
			this.WidthBox.Name = "WidthBox";
			this.WidthBox.Size = new System.Drawing.Size(246, 20);
			this.WidthBox.TabIndex = 7;
			NumericUpDown num1 = this.WidthBox;
			int[] numArray5 = new int[] { 1, 0, 0, 0 };
			num1.Value = new decimal(numArray5);
			this.WidthLbl.AutoSize = true;
			this.WidthLbl.Location = new Point(3, 42);
			this.WidthLbl.Name = "WidthLbl";
			this.WidthLbl.Size = new System.Drawing.Size(38, 13);
			this.WidthLbl.TabIndex = 6;
			this.WidthLbl.Text = "Width:";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.HeightBox);
			base.Controls.Add(this.HeightLbl);
			base.Controls.Add(this.WidthBox);
			base.Controls.Add(this.WidthLbl);
			base.Controls.Add(this.InfoLbl);
			base.Name = "MapSizePage";
			base.Size = new System.Drawing.Size(307, 114);
			((ISupportInitialize)this.HeightBox).EndInit();
			((ISupportInitialize)this.WidthBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public bool OnBack()
		{
			return true;
		}

		public bool OnFinish()
		{
			this.fData.Width = (int)this.WidthBox.Value;
			this.fData.Height = (int)this.HeightBox.Value;
			return true;
		}

		public bool OnNext()
		{
			return true;
		}

		public void OnShown(object data)
		{
			if (this.fData == null)
			{
				this.fData = data as MapBuilderData;
				this.WidthBox.Value = this.fData.Width;
				this.HeightBox.Value = this.fData.Height;
			}
		}
	}
}