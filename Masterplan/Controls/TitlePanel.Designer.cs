using Masterplan;
using Masterplan.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class TitlePanel : UserControl
	{
		private const int MAX_ALPHA = 255;

		private const int MAX_COLOR = 60;

		private string fTitle = "";

		private TitlePanel.TitlePanelMode fMode;

		private bool fZooming;

		private string fVersion = TitlePanel.get_version_string();

		private Rectangle fTitleRect = Rectangle.Empty;

		private Rectangle fVersionRect = Rectangle.Empty;

		private StringFormat fFormat = new StringFormat();

		private int fAlpha;

		private IContainer components;

		private Timer FadeTimer;

		private Timer PulseTimer;

		[Category("Layout")]
		public TitlePanel.TitlePanelMode Mode
		{
			get
			{
				return this.fMode;
			}
			set
			{
				this.fMode = value;
				base.Invalidate();
			}
		}

		[Category("Appearance")]
		public string Title
		{
			get
			{
				return this.fTitle;
			}
			set
			{
				this.fTitle = value;
			}
		}

		[Category("Behavior")]
		public bool Zooming
		{
			get
			{
				return this.fZooming;
			}
			set
			{
				this.fZooming = value;
			}
		}

		public TitlePanel()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.fFormat.Alignment = StringAlignment.Center;
			this.fFormat.LineAlignment = StringAlignment.Center;
			this.fFormat.Trimming = StringTrimming.EllipsisWord;
			this.FadeTimer.Enabled = true;
		}

		private Color change_colour(Color colour)
		{
			int r = colour.R;
			int g = colour.G;
			int b = colour.B;
			switch (Session.Random.Next() % 4)
			{
				case 0:
				{
					r = Math.Min(60, r + 1);
					break;
				}
				case 1:
				{
					g = Math.Min(60, g + 1);
					break;
				}
				case 2:
				{
					b = Math.Min(60, b + 1);
					break;
				}
				case 3:
				{
					r = Math.Max(0, r - 1);
					break;
				}
				case 4:
				{
					g = Math.Max(0, g - 1);
					break;
				}
				case 5:
				{
					b = Math.Max(0, b - 1);
					break;
				}
			}
			return Color.FromArgb(r, g, b);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void FadeTimer_Tick(object sender, EventArgs e)
		{
			this.fAlpha = Math.Min(this.fAlpha + 4, 255);
			base.Invalidate();
			if (this.fAlpha == 255)
			{
				this.FadeTimer.Enabled = false;
				this.OnFadeFinished();
				if (this.fMode == TitlePanel.TitlePanelMode.PlayerView)
				{
					this.PulseTimer.Enabled = true;
				}
			}
		}

		private static string get_version_string()
		{
			string str = "Adventure Design Studio";
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			if (entryAssembly != null)
			{
				Version version = entryAssembly.GetName().Version;
				if (version != null)
				{
					if (str != "")
					{
						str = string.Concat(str, Environment.NewLine);
					}
					str = string.Concat(str, "Version ", version.Major);
					if (version.Build != 0)
					{
						object obj = str;
						object[] minor = new object[] { obj, ".", version.Minor, ".", version.Build };
						str = string.Concat(minor);
					}
					else if (version.Minor != 0)
					{
						str = string.Concat(str, ".", version.Minor);
					}
				}
			}
			if (Program.IsBeta)
			{
				if (str != "")
				{
					str = string.Concat(str, Environment.NewLine, Environment.NewLine);
				}
				str = string.Concat(str, "BETA");
			}
			return str;
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.FadeTimer = new Timer(this.components);
			this.PulseTimer = new Timer(this.components);
			base.SuspendLayout();
			this.FadeTimer.Interval = 25;
			this.FadeTimer.Tick += new EventHandler(this.FadeTimer_Tick);
			this.PulseTimer.Tick += new EventHandler(this.PulseTimer_Tick);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ForeColor = Color.MidnightBlue;
			base.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			base.Name = "TitlePanel";
			base.Size = new System.Drawing.Size(150, 151);
			base.ResumeLayout(false);
		}

		protected void OnFadeFinished()
		{
			if (this.FadeFinished != null)
			{
				this.FadeFinished(this, new EventArgs());
			}
		}

		protected override void OnLayout(LayoutEventArgs e)
		{
			base.OnLayout(e);
			this.reset_view();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
			if (this.fTitleRect == Rectangle.Empty)
			{
				Rectangle clientRectangle = base.ClientRectangle;
				SizeF sizeF = e.Graphics.MeasureString(this.fVersion, this.Font);
				double height = (double)(sizeF.Height + (float)(base.Height / 10));
				this.fTitleRect = new Rectangle(clientRectangle.Left, clientRectangle.Top, clientRectangle.Width - 1, (int)((double)clientRectangle.Height - height - 1));
				this.fVersionRect = new Rectangle(clientRectangle.Left, this.fTitleRect.Bottom, clientRectangle.Width - 1, (int)height);
			}
			if (this.fMode == TitlePanel.TitlePanelMode.WelcomeScreen)
			{
				ColorMatrix colorMatrix = new ColorMatrix()
				{
					Matrix33 = 0.25f * (float)this.fAlpha / 255f
				};
				ImageAttributes imageAttribute = new ImageAttributes();
				imageAttribute.SetColorMatrix(colorMatrix);
				Image scroll = Resources.Scroll;
				int y = base.ClientRectangle.Y;
				Rectangle rectangle = base.ClientRectangle;
				int num = y + (int)((double)rectangle.Height * 0.1);
				int height1 = (int)((double)base.ClientRectangle.Height * 0.8);
				int width = scroll.Width * height1 / scroll.Height;
				if (width > base.ClientRectangle.Width)
				{
					width = base.ClientRectangle.Width;
					height1 = scroll.Height * width / scroll.Width;
				}
				int x = base.ClientRectangle.X;
				Rectangle clientRectangle1 = base.ClientRectangle;
				int width1 = x + (clientRectangle1.Width - width) / 2;
				Rectangle rectangle1 = new Rectangle(width1, num, width, height1);
				e.Graphics.DrawImage(scroll, rectangle1, 0, 0, scroll.Width, scroll.Height, GraphicsUnit.Pixel, imageAttribute);
			}
			using (Brush solidBrush = new SolidBrush(Color.FromArgb(this.fAlpha, this.ForeColor)))
			{
				float single = (float)this.fTitleRect.Height / 2f;
				float single1 = (float)(this.fTitleRect.Width / this.fTitle.Length);
				float single2 = Math.Min(single, single1);
				if (this.fZooming)
				{
					float single3 = 0.1f * (float)this.fAlpha / 255f;
					single2 = single2 * (0.9f + single3);
				}
				if (single > 0f)
				{
					using (System.Drawing.Font font = new System.Drawing.Font(this.Font.FontFamily, single2))
					{
						e.Graphics.DrawString(this.fTitle, font, solidBrush, this.fTitleRect, this.fFormat);
					}
				}
				if (this.fMode == TitlePanel.TitlePanelMode.WelcomeScreen)
				{
					e.Graphics.DrawString(this.fVersion, this.Font, solidBrush, this.fVersionRect, this.fFormat);
				}
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.reset_view();
		}

		private void PulseTimer_Tick(object sender, EventArgs e)
		{
			this.fAlpha = Math.Max(this.fAlpha - 1, 0);
			if (Session.Random.Next() % 10 == 0)
			{
				this.BackColor = this.change_colour(this.BackColor);
			}
			base.Invalidate();
		}

		private void reset_view()
		{
			this.fTitleRect = Rectangle.Empty;
			this.fVersionRect = Rectangle.Empty;
			base.Invalidate();
		}

		public void Wake()
		{
			if (this.PulseTimer.Enabled)
			{
				this.PulseTimer.Enabled = false;
				this.FadeTimer.Enabled = true;
			}
		}

		public event EventHandler FadeFinished;

		public enum TitlePanelMode
		{
			WelcomeScreen,
			PlayerView
		}
	}
}