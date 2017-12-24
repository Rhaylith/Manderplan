using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class HitPointGauge : UserControl
	{
		private IContainer components;

		private int fFullHP;

		private int fDamage;

		private int fTempHP;

		public int Damage
		{
			get
			{
				return this.fDamage;
			}
			set
			{
				this.fDamage = value;
				base.Invalidate();
			}
		}

		public int FullHP
		{
			get
			{
				return this.fFullHP;
			}
			set
			{
				this.fFullHP = value;
				base.Invalidate();
			}
		}

		public int TempHP
		{
			get
			{
				return this.fTempHP;
			}
			set
			{
				this.fTempHP = value;
				base.Invalidate();
			}
		}

		public HitPointGauge()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private int get_level(int value)
		{
			int num = Math.Min(0, this.fFullHP - this.fDamage);
			int num1 = Math.Max(this.fFullHP + this.fTempHP - this.fDamage, this.fFullHP);
			int num2 = num1 - num;
			if (num2 == 0)
			{
				return 0;
			}
			return (num1 - value) * base.Height / num2;
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (this.fFullHP == 0)
			{
				e.Graphics.DrawRectangle(Pens.Black, 0, 0, base.Width - 1, base.Height - 1);
				return;
			}
			int num = this.fFullHP - this.fDamage;
			int num1 = this.fFullHP / 2;
			int width = (int)((double)base.Width * 0.8);
			int _level = this.get_level(0);
			int _level1 = this.get_level(num1);
			int _level2 = this.get_level(this.fFullHP);
			int num2 = this.get_level(num);
			if (this.fFullHP != 0)
			{
				Rectangle rectangle = new Rectangle(width, _level2, base.Width - width, _level - _level2);
				Brush linearGradientBrush = new LinearGradientBrush(rectangle, Color.Black, Color.LightGray, LinearGradientMode.Horizontal);
				e.Graphics.FillRectangle(linearGradientBrush, rectangle);
			}
			if (num != 0)
			{
				int num3 = Math.Min(_level, num2);
				int num4 = Math.Max(_level, num2);
				Rectangle rectangle1 = new Rectangle(0, num3, width, num4 - num3);
				Brush brush = null;
				brush = (num <= num1 ? new LinearGradientBrush(rectangle1, Color.Red, Color.DarkRed, LinearGradientMode.Vertical) : new LinearGradientBrush(rectangle1, Color.Green, Color.DarkGreen, LinearGradientMode.Vertical));
				e.Graphics.FillRectangle(brush, rectangle1);
				e.Graphics.DrawRectangle(Pens.DarkGray, rectangle1);
			}
			if (this.fTempHP != 0)
			{
				int num5 = Math.Max(0, num + this.fTempHP);
				int _level3 = this.get_level(num5);
				int _level4 = this.get_level(num5 - this.fTempHP);
				Rectangle rectangle2 = new Rectangle(0, _level3, width, _level4 - _level3);
				Brush linearGradientBrush1 = new LinearGradientBrush(rectangle2, Color.Blue, Color.Navy, LinearGradientMode.Vertical);
				e.Graphics.FillRectangle(linearGradientBrush1, rectangle2);
				e.Graphics.DrawRectangle(Pens.DarkGray, rectangle2);
			}
			if (this.fFullHP != 0)
			{
				e.Graphics.DrawLine(Pens.DarkGray, 0, _level, width, _level);
				e.Graphics.DrawLine(Pens.DarkGray, 0, _level1, width, _level1);
				e.Graphics.DrawLine(Pens.DarkGray, 0, _level2, width, _level2);
			}
		}
	}
}