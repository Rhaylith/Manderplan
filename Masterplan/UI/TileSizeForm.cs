using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class TileSizeForm : Form
	{
		private IContainer components;

		private Label WidthLbl;

		private NumericUpDown WidthBox;

		private Label HeightLbl;

		private NumericUpDown HeightBox;

		private Button OKBtn;

		private Button CancelBtn;

		private List<Tile> fTiles;

		private System.Drawing.Size fSize = new System.Drawing.Size(2, 2);

		public System.Drawing.Size TileSize
		{
			get
			{
				return this.fSize;
			}
		}

		public TileSizeForm(List<Tile> tiles)
		{
			this.InitializeComponent();
			this.fTiles = tiles;
			int width = 0;
			int height = 0;
			foreach (Tile fTile in this.fTiles)
			{
				width += fTile.Size.Width;
				height += fTile.Size.Height;
			}
			width /= this.fTiles.Count;
			height /= this.fTiles.Count;
			this.WidthBox.Value = width;
			this.HeightBox.Value = height;
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
			this.WidthLbl = new Label();
			this.WidthBox = new NumericUpDown();
			this.HeightLbl = new Label();
			this.HeightBox = new NumericUpDown();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			((ISupportInitialize)this.WidthBox).BeginInit();
			((ISupportInitialize)this.HeightBox).BeginInit();
			base.SuspendLayout();
			this.WidthLbl.AutoSize = true;
			this.WidthLbl.Location = new Point(12, 14);
			this.WidthLbl.Name = "WidthLbl";
			this.WidthLbl.Size = new System.Drawing.Size(58, 13);
			this.WidthLbl.TabIndex = 0;
			this.WidthLbl.Text = "Tile Width:";
			this.WidthBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.WidthBox.Location = new Point(79, 12);
			NumericUpDown widthBox = this.WidthBox;
			int[] numArray = new int[] { 1, 0, 0, 0 };
			widthBox.Minimum = new decimal(numArray);
			this.WidthBox.Name = "WidthBox";
			this.WidthBox.Size = new System.Drawing.Size(175, 20);
			this.WidthBox.TabIndex = 1;
			NumericUpDown num = this.WidthBox;
			int[] numArray1 = new int[] { 1, 0, 0, 0 };
			num.Value = new decimal(numArray1);
			this.HeightLbl.AutoSize = true;
			this.HeightLbl.Location = new Point(12, 40);
			this.HeightLbl.Name = "HeightLbl";
			this.HeightLbl.Size = new System.Drawing.Size(61, 13);
			this.HeightLbl.TabIndex = 2;
			this.HeightLbl.Text = "Tile Height:";
			this.HeightBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.HeightBox.Location = new Point(79, 38);
			NumericUpDown heightBox = this.HeightBox;
			int[] numArray2 = new int[] { 1, 0, 0, 0 };
			heightBox.Minimum = new decimal(numArray2);
			this.HeightBox.Name = "HeightBox";
			this.HeightBox.Size = new System.Drawing.Size(175, 20);
			this.HeightBox.TabIndex = 3;
			NumericUpDown numericUpDown = this.HeightBox;
			int[] numArray3 = new int[] { 1, 0, 0, 0 };
			numericUpDown.Value = new decimal(numArray3);
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(98, 77);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 4;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(179, 77);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 5;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(266, 112);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.HeightBox);
			base.Controls.Add(this.HeightLbl);
			base.Controls.Add(this.WidthBox);
			base.Controls.Add(this.WidthLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TileSizeForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Tile Size";
			((ISupportInitialize)this.WidthBox).EndInit();
			((ISupportInitialize)this.HeightBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			int value = (int)this.WidthBox.Value;
			int num = (int)this.HeightBox.Value;
			this.fSize = new System.Drawing.Size(value, num);
		}
	}
}