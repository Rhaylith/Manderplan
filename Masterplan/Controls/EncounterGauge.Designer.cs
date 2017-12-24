using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class EncounterGauge : UserControl
	{
		private const int CONTROL_HEIGHT = 20;

		private Masterplan.Data.Party fParty;

		private int fXP;

		private IContainer components;

		public Masterplan.Data.Party Party
		{
			get
			{
				return this.fParty;
			}
			set
			{
				this.fParty = value;
				base.Invalidate();
			}
		}

		public int XP
		{
			get
			{
				return this.fXP;
			}
			set
			{
				this.fXP = value;
				base.Invalidate();
			}
		}

		public EncounterGauge()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			base.Height = 20;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private int get_max_level()
		{
			int creatureLevel = Experience.GetCreatureLevel(this.fXP / this.fParty.Size);
			return Math.Max(this.fParty.Level + 5, creatureLevel + 1);
		}

		private int get_min_level()
		{
			int creatureLevel = Experience.GetCreatureLevel(this.fXP / this.fParty.Size);
			int num = Math.Min(this.fParty.Level - 3, creatureLevel);
			return Math.Max(num, 0);
		}

		private int get_x(int xp)
		{
			int creatureXP = Experience.GetCreatureXP(this.get_min_level()) * this.fParty.Size;
			int num = Experience.GetCreatureXP(this.get_max_level()) * this.fParty.Size;
			int num1 = Math.Min(this.fXP, creatureXP);
			int num2 = Math.Max(this.fXP, num) - num1;
			return (xp - num1) * base.Width / num2;
		}

		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Name = "EncounterGauge";
			base.ResumeLayout(false);
		}

		protected override void OnLayout(LayoutEventArgs e)
		{
			base.OnLayout(e);
			base.Height = 20;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (this.fParty == null)
			{
				return;
			}
			System.Drawing.Font font = new System.Drawing.Font(this.Font.FontFamily, 7f);
			Rectangle rectangle = new Rectangle(0, 4, this.get_x(this.fXP), base.Height - 8);
			if (rectangle.Width > 0)
			{
				Brush linearGradientBrush = new LinearGradientBrush(rectangle, SystemColors.Control, SystemColors.ControlDark, LinearGradientMode.Horizontal);
				e.Graphics.FillRectangle(linearGradientBrush, rectangle);
			}
			int num = Math.Max(this.get_min_level(), 1);
			int maxLevel = this.get_max_level();
			for (int i = num; i != maxLevel; i++)
			{
				int creatureXP = Experience.GetCreatureXP(i) * this.fParty.Size;
				int _x = this.get_x(creatureXP);
				e.Graphics.DrawLine(Pens.Black, new Point(_x, 1), new Point(_x, base.Height - 3));
				e.Graphics.DrawString(i.ToString(), font, SystemBrushes.WindowText, new PointF((float)_x, 1f));
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			base.Height = 20;
		}
	}
}