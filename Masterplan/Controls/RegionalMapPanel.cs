using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Utils;
using Utils.Graphics;

namespace Masterplan.Controls
{
	public class RegionalMapPanel : UserControl
	{
		private const float LOCATION_RADIUS = 8f;

		private IContainer components;

		private RegionalMap fMap;

		private MapViewMode fMode;

		private Masterplan.Data.Plot fPlot;

		private bool fShowLocations = true;

		private bool fAllowEditing;

		private MapLocation fHoverLocation;

		private MapLocation fSelectedLocation;

		private MapLocation fHighlightedLocation;

		private StringFormat fCentred = new StringFormat();

		public bool AllowEditing
		{
			get
			{
				return this.fAllowEditing;
			}
			set
			{
				this.fAllowEditing = value;
			}
		}

		public MapLocation HighlightedLocation
		{
			get
			{
				return this.fHighlightedLocation;
			}
			set
			{
				this.fHighlightedLocation = value;
				base.Invalidate();
			}
		}

		public MapLocation HoverLocation
		{
			get
			{
				return this.fHoverLocation;
			}
		}

		public RegionalMap Map
		{
			get
			{
				return this.fMap;
			}
			set
			{
				this.fMap = value;
				base.Invalidate();
			}
		}

		public RectangleF MapRectangle
		{
			get
			{
				if (this.fMap == null || this.fMap.Image == null)
				{
					return RectangleF.Empty;
				}
				Rectangle clientRectangle = base.ClientRectangle;
				double width = (double)clientRectangle.Width / (double)this.fMap.Image.Width;
				Rectangle rectangle = base.ClientRectangle;
				double height = (double)rectangle.Height / (double)this.fMap.Image.Height;
				float single = (float)Math.Min(width, height);
				float width1 = single * (float)this.fMap.Image.Width;
				float height1 = single * (float)this.fMap.Image.Height;
				Rectangle clientRectangle1 = base.ClientRectangle;
				float single1 = ((float)clientRectangle1.Width - width1) / 2f;
				Rectangle rectangle1 = base.ClientRectangle;
				float height2 = ((float)rectangle1.Height - height1) / 2f;
				return new RectangleF(single1, height2, width1, height1);
			}
		}

		public MapViewMode Mode
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

		public Masterplan.Data.Plot Plot
		{
			get
			{
				return this.fPlot;
			}
			set
			{
				this.fPlot = value;
				base.Invalidate();
			}
		}

		public MapLocation SelectedLocation
		{
			get
			{
				return this.fSelectedLocation;
			}
			set
			{
				this.fSelectedLocation = value;
				base.Invalidate();
			}
		}

		public bool ShowLocations
		{
			get
			{
				return this.fShowLocations;
			}
			set
			{
				this.fShowLocations = value;
				base.Invalidate();
			}
		}

		public RegionalMapPanel()
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

		private PointF get_loc_pt(MapLocation loc, RectangleF img_rect)
		{
			float x = img_rect.X + img_rect.Width * loc.Point.X;
			float y = img_rect.Y + img_rect.Height * loc.Point.Y;
			return new PointF(x, y);
		}

		private RectangleF get_loc_rect(MapLocation loc, RectangleF img_rect)
		{
			PointF locPt = this.get_loc_pt(loc, img_rect);
			float single = 8f;
			return new RectangleF(locPt.X - single, locPt.Y - single, 2f * single, 2f * single);
		}

		private MapLocation get_location_at(Point pt)
		{
			MapLocation mapLocation;
			if (this.fMap == null)
			{
				return null;
			}
			RectangleF mapRectangle = this.MapRectangle;
			List<MapLocation>.Enumerator enumerator = this.fMap.Locations.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					MapLocation current = enumerator.Current;
					if (!this.get_loc_rect(current, mapRectangle).Contains(pt))
					{
						continue;
					}
					mapLocation = current;
					return mapLocation;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		private PointF get_point(Point pt)
		{
			RectangleF mapRectangle = this.MapRectangle;
			if (!mapRectangle.Contains(pt))
			{
				return PointF.Empty;
			}
			float x = ((float)pt.X - mapRectangle.X) / mapRectangle.Width;
			float y = ((float)pt.Y - mapRectangle.Y) / mapRectangle.Height;
			return new PointF(x, y);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		}

		protected void OnLocationModified()
		{
			if (this.LocationModified != null)
			{
				this.LocationModified(this, new EventArgs());
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (this.fMode == MapViewMode.Plain || this.fMode == MapViewMode.PlayerView)
			{
				return;
			}
			this.fSelectedLocation = this.fHoverLocation;
			this.OnSelectedLocationModified();
			base.Invalidate();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			if (this.fMode == MapViewMode.Plain || this.fMode == MapViewMode.PlayerView)
			{
				return;
			}
			this.fHoverLocation = null;
			base.Invalidate();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (this.fMode == MapViewMode.Plain || this.fMode == MapViewMode.PlayerView)
			{
				return;
			}
			Point client = base.PointToClient(System.Windows.Forms.Cursor.Position);
			this.fHoverLocation = this.get_location_at(client);
			if (this.fAllowEditing && e.Button == System.Windows.Forms.MouseButtons.Left && this.fSelectedLocation != null)
			{
				PointF _point = this.get_point(client);
				if (_point != PointF.Empty)
				{
					this.fSelectedLocation.Point = _point;
					this.OnLocationModified();
				}
				else
				{
					this.fSelectedLocation = null;
				}
			}
			base.Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			bool flag;
			try
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
				switch (this.fMode)
				{
					case MapViewMode.Normal:
					case MapViewMode.Thumbnail:
					{
						Color color = Color.FromArgb(240, 240, 240);
						Color color1 = Color.FromArgb(170, 170, 170);
						Brush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, color, color1, LinearGradientMode.Vertical);
						e.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
						break;
					}
					case MapViewMode.Plain:
					{
						e.Graphics.FillRectangle(Brushes.White, base.ClientRectangle);
						break;
					}
					case MapViewMode.PlayerView:
					{
						e.Graphics.FillRectangle(Brushes.Black, base.ClientRectangle);
						break;
					}
				}
				if (this.fMap == null || this.fMap.Image == null)
				{
					e.Graphics.DrawString("(no map selected)", this.Font, Brushes.Black, base.ClientRectangle, this.fCentred);
				}
				else
				{
					RectangleF mapRectangle = this.MapRectangle;
					e.Graphics.DrawImage(this.fMap.Image, mapRectangle);
					if (this.fShowLocations)
					{
						foreach (MapLocation location in this.fMap.Locations)
						{
							if (location == null || this.fHighlightedLocation != null && location.ID != this.fHighlightedLocation.ID)
							{
								continue;
							}
							Color white = Color.White;
							if (location == this.fHoverLocation)
							{
								white = Color.Blue;
							}
							if (location == this.fSelectedLocation)
							{
								white = Color.Blue;
							}
							RectangleF locRect = this.get_loc_rect(location, mapRectangle);
							e.Graphics.DrawEllipse(new Pen(Color.Black, 5f), locRect);
							e.Graphics.DrawEllipse(new Pen(white, 2f), locRect);
						}
					}
					if (this.fPlot != null)
					{
						foreach (PlotPoint point in this.fPlot.Points)
						{
							if (point.RegionalMapID != this.fMap.ID)
							{
								continue;
							}
							MapLocation mapLocation = this.fMap.FindLocation(point.MapLocationID);
							if (mapLocation == null)
							{
								continue;
							}
							PointF locPt = this.get_loc_pt(mapLocation, mapRectangle);
							RectangleF rectangleF = this.get_loc_rect(mapLocation, mapRectangle);
							rectangleF.Inflate(-5f, -5f);
							foreach (Guid link in point.Links)
							{
								PlotPoint plotPoint = this.fPlot.FindPoint(link);
								if (plotPoint == null || plotPoint.RegionalMapID != this.fMap.ID)
								{
									continue;
								}
								MapLocation mapLocation1 = this.fMap.FindLocation(plotPoint.MapLocationID);
								if (mapLocation1 == null)
								{
									continue;
								}
								PointF pointF = this.get_loc_pt(mapLocation1, mapRectangle);
								e.Graphics.DrawLine(new Pen(Color.Red, 3f), locPt, pointF);
								RectangleF locRect1 = this.get_loc_rect(mapLocation1, mapRectangle);
								locRect1.Inflate(-5f, -5f);
								e.Graphics.FillEllipse(Brushes.Red, rectangleF);
								e.Graphics.FillEllipse(Brushes.Red, locRect1);
							}
						}
					}
					if (this.fShowLocations)
					{
						foreach (MapLocation location1 in this.fMap.Locations)
						{
							if (this.fHighlightedLocation != null && location1 != this.fHighlightedLocation || location1 != this.fHoverLocation && location1 != this.fSelectedLocation && location1 != this.fHighlightedLocation)
							{
								continue;
							}
							if (location1.Category == "")
							{
								flag = false;
							}
							else
							{
								flag = (this.fMode == MapViewMode.Normal ? true : this.fMode == MapViewMode.Thumbnail);
							}
							bool flag1 = flag;
							RectangleF rectangleF1 = this.get_loc_rect(location1, mapRectangle);
							SizeF sizeF = e.Graphics.MeasureString(location1.Name, this.Font);
							SizeF sizeF1 = e.Graphics.MeasureString(location1.Category, this.Font);
							float single = (flag1 ? Math.Max(sizeF.Width, sizeF1.Width) : sizeF.Width);
							SizeF sizeF2 = new SizeF(single, (flag1 ? sizeF.Height + sizeF1.Height : sizeF.Height))
							{
								Width = sizeF1.Width + 2f,
								Height = sizeF1.Height + 2f
							};
							float x = rectangleF1.X + rectangleF1.Width / 2f - sizeF2.Width / 2f;
							float top = rectangleF1.Top - sizeF2.Height - 5f;
							if (top < (float)base.ClientRectangle.Top)
							{
								top = rectangleF1.Bottom + 5f;
							}
							x = Math.Max(x, 0f);
							float width = x + sizeF2.Width - (float)base.ClientRectangle.Right;
							if (width > 0f)
							{
								x -= width;
							}
							RectangleF rectangleF2 = new RectangleF(new PointF(x, top), sizeF2);
							GraphicsPath graphicsPath = RoundedRectangle.Create(rectangleF2, (float)this.Font.Height * 0.35f);
							e.Graphics.FillPath(Brushes.LightYellow, graphicsPath);
							e.Graphics.DrawPath(Pens.Black, graphicsPath);
							if (!flag1)
							{
								e.Graphics.DrawString(location1.Name, this.Font, Brushes.Black, rectangleF2, this.fCentred);
							}
							else
							{
								float height = rectangleF2.Height / 2f;
								float y = rectangleF2.Y + height;
								RectangleF rectangleF3 = new RectangleF(rectangleF2.X, rectangleF2.Y, rectangleF2.Width, height);
								RectangleF rectangleF4 = new RectangleF(rectangleF2.X, y, rectangleF2.Width, height);
								e.Graphics.DrawLine(Pens.Gray, rectangleF2.X, y, rectangleF2.X + rectangleF2.Width, y);
								e.Graphics.DrawString(location1.Name, this.Font, Brushes.Black, rectangleF3, this.fCentred);
								e.Graphics.DrawString(location1.Category, this.Font, Brushes.DarkGray, rectangleF4, this.fCentred);
							}
						}
					}
					if (this.fMode == MapViewMode.Normal && this.fMap.Locations.Count == 0)
					{
						string str = "Double-click on the map to set a location.";
						float single1 = 10f;
						Rectangle clientRectangle = base.ClientRectangle;
						float width1 = (float)clientRectangle.Width - 2f * single1;
						SizeF sizeF3 = e.Graphics.MeasureString(str, this.Font, (int)width1);
						float height1 = sizeF3.Height * 2f;
						RectangleF rectangleF5 = new RectangleF(single1, single1, width1, height1);
						GraphicsPath graphicsPath1 = RoundedRectangle.Create(rectangleF5, height1 / 3f);
						e.Graphics.FillPath(new SolidBrush(Color.FromArgb(200, Color.Black)), graphicsPath1);
						e.Graphics.DrawPath(Pens.Black, graphicsPath1);
						e.Graphics.DrawString(str, this.Font, Brushes.White, rectangleF5, this.fCentred);
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		protected void OnSelectedLocationModified()
		{
			if (this.SelectedLocationModified != null)
			{
				this.SelectedLocationModified(this, new EventArgs());
			}
		}

		public event EventHandler LocationModified;

		public event EventHandler SelectedLocationModified;
	}
}