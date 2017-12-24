using Masterplan;
using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	public class CalendarPanel : UserControl
	{
		private Masterplan.Data.Calendar fCalendar;

		private int fYear;

		private int fMonthIndex;

		private StringFormat fCentred = new StringFormat();

		private StringFormat fTopRight = new StringFormat();

		private int fWeeks;

		private int fDayOffset;

		private IContainer components;

		[Category("Data")]
		[Description("The calendar to display.")]
		public Masterplan.Data.Calendar Calendar
		{
			get
			{
				return this.fCalendar;
			}
			set
			{
				this.fCalendar = value;
				base.Invalidate();
			}
		}

		[Category("Data")]
		[Description("The 0-based index of the month to be displayed.")]
		public int MonthIndex
		{
			get
			{
				return this.fMonthIndex;
			}
			set
			{
				this.fMonthIndex = value;
				base.Invalidate();
			}
		}

		[Category("Data")]
		[Description("The year to be displayed.")]
		public int Year
		{
			get
			{
				return this.fYear;
			}
			set
			{
				this.fYear = value;
				base.Invalidate();
			}
		}

		public CalendarPanel()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.fCentred.Alignment = StringAlignment.Center;
			this.fCentred.LineAlignment = StringAlignment.Center;
			this.fCentred.Trimming = StringTrimming.EllipsisWord;
			this.fTopRight.Alignment = StringAlignment.Far;
			this.fTopRight.LineAlignment = StringAlignment.Near;
			this.fTopRight.Trimming = StringTrimming.EllipsisWord;
		}

		private void analyse_month()
		{
			this.fWeeks = 0;
			this.fDayOffset = 0;
			if (this.fCalendar == null)
			{
				return;
			}
			int daysSoFar = this.get_days_so_far();
			this.fDayOffset = daysSoFar % this.fCalendar.Days.Count;
			if (this.fDayOffset < 0)
			{
				this.fDayOffset += this.fCalendar.Days.Count;
			}
			MonthInfo item = this.fCalendar.Months[this.fMonthIndex];
			int dayCount = item.DayCount + this.fDayOffset;
			if (item.LeapModifier != 0 && item.LeapPeriod != 0 && this.fYear % item.LeapPeriod == 0)
			{
				dayCount += item.LeapModifier;
			}
			this.fWeeks = dayCount / this.fCalendar.Days.Count;
			if (dayCount % this.fCalendar.Days.Count != 0)
			{
				this.fWeeks++;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private int get_days_so_far()
		{
			int dayCount = 0;
			int num = Math.Min(this.fYear, this.fCalendar.CampaignYear);
			int num1 = Math.Max(this.fYear, this.fCalendar.CampaignYear);
			for (int i = num; i != num1; i++)
			{
				dayCount += this.fCalendar.DayCount(i);
			}
			if (this.fYear < this.fCalendar.CampaignYear)
			{
				dayCount = -dayCount;
			}
			for (int j = 0; j != this.fMonthIndex; j++)
			{
				dayCount += this.fCalendar.Months[j].DayCount;
			}
			return dayCount;
		}

		private RectangleF get_rect(int day_index)
		{
			int dayIndex = this.fDayOffset + day_index;
			int count = dayIndex % this.fCalendar.Days.Count;
			int num = dayIndex / this.fCalendar.Days.Count;
			return this.get_rect(count, num);
		}

		private RectangleF get_rect(int day, int week)
		{
			float single = 25f;
			Rectangle clientRectangle = base.ClientRectangle;
			float width = (float)clientRectangle.Width / (float)this.fCalendar.Days.Count;
			Rectangle rectangle = base.ClientRectangle;
			float height = (float)((float)rectangle.Height - single) / (float)this.fWeeks;
			if (week == -1)
			{
				return new RectangleF((float)day * width, 0f, width, single);
			}
			return new RectangleF((float)day * width, (float)week * height + single, width, height);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Brush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(225, 225, 225), Color.FromArgb(180, 180, 180), LinearGradientMode.Vertical);
			e.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
			if (this.fCalendar == null)
			{
				e.Graphics.DrawString("(no calendar)", this.Font, SystemBrushes.WindowText, base.ClientRectangle, this.fCentred);
				return;
			}
			this.analyse_month();
			System.Drawing.Font font = new System.Drawing.Font(this.Font, FontStyle.Bold);
			for (int i = 0; i != this.fCalendar.Days.Count; i++)
			{
				DayInfo item = this.fCalendar.Days[i];
				RectangleF _rect = this.get_rect(i, -1);
				e.Graphics.DrawString(item.Name, font, SystemBrushes.WindowText, _rect, this.fCentred);
			}
			MonthInfo monthInfo = this.fCalendar.Months[this.fMonthIndex];
			int dayCount = monthInfo.DayCount;
			if (monthInfo.LeapModifier != 0 && monthInfo.LeapPeriod != 0 && this.fYear % monthInfo.LeapPeriod == 0)
			{
				dayCount += monthInfo.LeapModifier;
			}
			Dictionary<int, List<PlotPoint>> nums = new Dictionary<int, List<PlotPoint>>();
			foreach (PlotPoint allPlotPoint in Session.Project.AllPlotPoints)
			{
				if (allPlotPoint.Date == null || allPlotPoint.Date.CalendarID != this.fCalendar.ID || allPlotPoint.Date.MonthID != monthInfo.ID || allPlotPoint.Date.Year != this.fYear)
				{
					continue;
				}
				if (!nums.ContainsKey(allPlotPoint.Date.DayIndex))
				{
					nums[allPlotPoint.Date.DayIndex] = new List<PlotPoint>();
				}
				nums[allPlotPoint.Date.DayIndex].Add(allPlotPoint);
			}
			for (int j = 0; j != dayCount; j++)
			{
				int num = j + 1;
				int daysSoFar = this.get_days_so_far() + j;
				string str = "";
				string str1 = "";
				foreach (Satellite satellite in this.fCalendar.Satellites)
				{
					if (satellite.Period == 0)
					{
						continue;
					}
					int offset = (daysSoFar - satellite.Offset) % satellite.Period;
					if (offset < 0)
					{
						offset += satellite.Period;
					}
					if (offset == 0)
					{
						str1 = string.Concat(str1, "●");
					}
					if (offset != satellite.Period / 2)
					{
						continue;
					}
					str1 = string.Concat(str1, "○");
				}
				foreach (CalendarEvent season in this.fCalendar.Seasons)
				{
					if (!(season.MonthID == monthInfo.ID) || season.DayIndex != j)
					{
						continue;
					}
					if (str != "")
					{
						str = string.Concat(str, Environment.NewLine);
					}
					str = string.Concat(str, "Start of ", season.Name);
				}
				foreach (CalendarEvent @event in this.fCalendar.Events)
				{
					if (!(@event.MonthID == monthInfo.ID) || @event.DayIndex != j)
					{
						continue;
					}
					if (str != "")
					{
						str = string.Concat(str, Environment.NewLine);
					}
					str = string.Concat(str, @event.Name);
				}
				if (nums.ContainsKey(j))
				{
					foreach (PlotPoint plotPoint in nums[j])
					{
						if (str != "")
						{
							str = string.Concat(str, Environment.NewLine);
						}
						str = string.Concat(str, plotPoint.Name);
					}
				}
				RectangleF rectangleF = this.get_rect(j);
				e.Graphics.FillRectangle(SystemBrushes.Window, rectangleF);
				RectangleF rectangleF1 = new RectangleF(rectangleF.X, rectangleF.Y, 25f, 20f);
				e.Graphics.DrawString(num.ToString(), this.Font, SystemBrushes.WindowText, rectangleF1, this.fCentred);
				e.Graphics.DrawRectangle(Pens.Gray, rectangleF1.X, rectangleF1.Y, rectangleF1.Width, rectangleF1.Height);
				if (str1 != "")
				{
					e.Graphics.DrawString(str1, this.Font, SystemBrushes.WindowText, rectangleF, this.fTopRight);
				}
				if (str != "")
				{
					RectangleF rectangleF2 = new RectangleF(rectangleF.X, rectangleF1.Bottom, rectangleF.Width, rectangleF.Bottom - rectangleF1.Bottom);
					e.Graphics.DrawString(str, this.Font, SystemBrushes.WindowText, rectangleF2, this.fCentred);
				}
				e.Graphics.DrawRectangle(SystemPens.ControlDark, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
			}
		}
	}
}