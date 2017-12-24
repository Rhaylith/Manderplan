using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Utils;

namespace Masterplan.Controls
{
	internal class InitiativePanel : UserControl
	{
		private const int BORDER = 8;

		private IContainer components;

		private int fHoveredInit = -2147483648;

		private StringFormat fCentred = new StringFormat();

		private Pen fTickPen = new Pen(Color.Gray, 0.5f);

		private List<int> fInitiatives = new List<int>();

		private int fCurrent;

		public int CurrentInitiative
		{
			get
			{
				return this.fCurrent;
			}
			set
			{
				this.fCurrent = value;
				base.Invalidate();
			}
		}

		public List<int> InitiativeScores
		{
			get
			{
				return this.fInitiatives;
			}
			set
			{
				this.fInitiatives = value;
				base.Invalidate();
			}
		}

		public int Maximum
		{
			get
			{
				return this.get_range().Second;
			}
		}

		public int Minimum
		{
			get
			{
				return this.get_range().First;
			}
		}

		public InitiativePanel()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.fCentred.Alignment = StringAlignment.Center;
			this.fCentred.LineAlignment = StringAlignment.Center;
			this.fCentred.Trimming = StringTrimming.EllipsisWord;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private Pair<int, int> get_range()
		{
			int num = 2147483647;
			int num1 = -2147483648;
			foreach (int fInitiative in this.fInitiatives)
			{
				num = Math.Min(num, fInitiative);
				num1 = Math.Max(num1, fInitiative);
			}
			if (this.fCurrent != -2147483648)
			{
				num = Math.Min(num, this.fCurrent);
				num1 = Math.Max(num1, this.fCurrent);
			}
			if (num == 2147483647)
			{
				num = 0;
			}
			if (num1 == -2147483648)
			{
				num1 = 20;
			}
			if (num == num1)
			{
				num -= 5;
				num1 += 5;
			}
			return new Pair<int, int>(num, num1);
		}

		private RectangleF get_rect(int score)
		{
			Pair<int, int> _range = this.get_range();
			int second = _range.Second - _range.First + 1;
			Rectangle clientRectangle = base.ClientRectangle;
			float height = (float)((clientRectangle.Height - 16) / second);
			int num = score - _range.First;
			float single = (float)(base.ClientRectangle.Height - 8);
			single = single - (float)num * height - height;
			Rectangle rectangle = base.ClientRectangle;
			return new RectangleF(0f, single, (float)rectangle.Width, height);
		}

		private int get_score(int y)
		{
			Pair<int, int> _range = this.get_range();
			for (int i = _range.First; i <= _range.Second; i++)
			{
				RectangleF _rect = this.get_rect(i);
				if (_rect.Top <= (float)y && _rect.Bottom >= (float)y)
				{
					return i;
				}
			}
			return -2147483648;
		}

		private float get_y(int score)
		{
			RectangleF _rect = this.get_rect(score);
			return _rect.Top + _rect.Height / 2f;
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		}

		protected void OnInitiativeChanged()
		{
			if (this.InitiativeChanged != null)
			{
				this.InitiativeChanged(this, new EventArgs());
			}
		}

		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			Point client = base.PointToClient(System.Windows.Forms.Cursor.Position);
			this.fCurrent = this.get_score(client.Y);
			this.OnInitiativeChanged();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			this.fHoveredInit = -2147483648;
			base.Invalidate();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			Point client = base.PointToClient(System.Windows.Forms.Cursor.Position);
			this.fHoveredInit = this.get_score(client.Y);
			base.Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			e.Graphics.FillRectangle(Brushes.White, base.ClientRectangle);
			float right = (float)(base.ClientRectangle.Right - 8);
			PointF pointF = new PointF(right, 8f);
			Rectangle clientRectangle = base.ClientRectangle;
			PointF pointF1 = new PointF(right, (float)(clientRectangle.Bottom - 8));
			e.Graphics.DrawLine(Pens.Black, pointF, pointF1);
			Pair<int, int> _range = this.get_range();
			for (int i = _range.First; i <= _range.Second; i++)
			{
				if (i % 5 == 0)
				{
					float _y = this.get_y(i);
					PointF pointF2 = new PointF(right - 5f, _y);
					PointF pointF3 = new PointF(right, _y);
					e.Graphics.DrawLine(this.fTickPen, pointF2, pointF3);
				}
			}
			foreach (int fInitiative in this.fInitiatives)
			{
				float single = this.get_y(fInitiative);
				PointF pointF4 = new PointF(right, single);
				PointF pointF5 = new PointF(right - 10f, single - 5f);
				PointF pointF6 = new PointF(right - 10f, single + 5f);
				Graphics graphics = e.Graphics;
				Brush white = Brushes.White;
				PointF[] pointFArray = new PointF[] { pointF4, pointF5, pointF6 };
				graphics.FillPolygon(white, pointFArray);
				Graphics graphic = e.Graphics;
				Pen gray = Pens.Gray;
				PointF[] pointFArray1 = new PointF[] { pointF4, pointF5, pointF6 };
				graphic.DrawPolygon(gray, pointFArray1);
			}
			if (this.fCurrent != -2147483648)
			{
				float _y1 = this.get_y(this.fCurrent);
				Rectangle rectangle = base.ClientRectangle;
				RectangleF rectangleF = new RectangleF(8f, _y1 - 8f, (float)(rectangle.Width - 16), 16f);
				using (Brush linearGradientBrush = new LinearGradientBrush(rectangleF, Color.Blue, Color.DarkBlue, LinearGradientMode.Vertical))
				{
					e.Graphics.FillRectangle(linearGradientBrush, rectangleF);
					e.Graphics.DrawRectangle(Pens.Black, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
					e.Graphics.DrawString(this.fCurrent.ToString(), this.Font, Brushes.White, rectangleF, this.fCentred);
				}
			}
			if (this.fHoveredInit != -2147483648 && this.fHoveredInit != this.fCurrent)
			{
				float single1 = this.get_y(this.fHoveredInit);
				Rectangle clientRectangle1 = base.ClientRectangle;
				RectangleF rectangleF1 = new RectangleF(8f, single1 - 8f, (float)(clientRectangle1.Width - 16), 16f);
				e.Graphics.FillRectangle(Brushes.White, rectangleF1);
				e.Graphics.DrawRectangle(Pens.Gray, rectangleF1.X, rectangleF1.Y, rectangleF1.Width, rectangleF1.Height);
				e.Graphics.DrawString(this.fHoveredInit.ToString(), this.Font, Brushes.Gray, rectangleF1, this.fCentred);
			}
		}

		public event EventHandler InitiativeChanged;
	}
}