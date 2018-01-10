using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class Calendar
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fDetails = "";

		private int fCampaignYear = 1000;

		private List<MonthInfo> fMonths = new List<MonthInfo>();

		private List<DayInfo> fDays = new List<DayInfo>();

		private List<CalendarEvent> fSeasons = new List<CalendarEvent>();

		private List<CalendarEvent> fEvents = new List<CalendarEvent>();

		private List<Satellite> fSatellites = new List<Satellite>();

		public int CampaignYear
		{
			get
			{
				return this.fCampaignYear;
			}
			set
			{
				this.fCampaignYear = value;
			}
		}

		public List<DayInfo> Days
		{
			get
			{
				return this.fDays;
			}
			set
			{
				this.fDays = value;
			}
		}

		public string Details
		{
			get
			{
				return this.fDetails;
			}
			set
			{
				this.fDetails = value;
			}
		}

		public List<CalendarEvent> Events
		{
			get
			{
				return this.fEvents;
			}
			set
			{
				this.fEvents = value;
			}
		}

		public Guid ID
		{
			get
			{
				return this.fID;
			}
			set
			{
				this.fID = value;
			}
		}

		public List<MonthInfo> Months
		{
			get
			{
				return this.fMonths;
			}
			set
			{
				this.fMonths = value;
			}
		}

		public string Name
		{
			get
			{
				return this.fName;
			}
			set
			{
				this.fName = value;
			}
		}

		public List<Satellite> Satellites
		{
			get
			{
				return this.fSatellites;
			}
			set
			{
				this.fSatellites = value;
			}
		}

		public List<CalendarEvent> Seasons
		{
			get
			{
				return this.fSeasons;
			}
			set
			{
				this.fSeasons = value;
			}
		}

		public Calendar()
		{
		}

		public Calendar Copy()
		{
			Calendar calendar = new Calendar()
			{
				ID = this.fID,
				Name = this.fName,
				Details = this.fDetails,
				CampaignYear = this.fCampaignYear
			};
			foreach (MonthInfo fMonth in this.fMonths)
			{
				calendar.Months.Add(fMonth.Copy());
			}
			foreach (DayInfo fDay in this.fDays)
			{
				calendar.Days.Add(fDay.Copy());
			}
			foreach (CalendarEvent fSeason in this.fSeasons)
			{
				calendar.Seasons.Add(fSeason.Copy());
			}
			foreach (CalendarEvent fEvent in this.fEvents)
			{
				calendar.Events.Add(fEvent.Copy());
			}
			foreach (Satellite fSatellite in this.fSatellites)
			{
				calendar.Satellites.Add(fSatellite.Copy());
			}
			return calendar;
		}

		public int DayCount(int year)
		{
			int dayCount = 0;
			foreach (MonthInfo fMonth in this.fMonths)
			{
				dayCount += fMonth.DayCount;
				if (fMonth.LeapModifier == 0 || fMonth.LeapPeriod == 0 || year % fMonth.LeapPeriod != 0)
				{
					continue;
				}
				dayCount += fMonth.LeapModifier;
			}
			return dayCount;
		}

		public MonthInfo FindMonth(Guid month_id)
		{
			MonthInfo monthInfo;
			List<MonthInfo>.Enumerator enumerator = this.fMonths.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					MonthInfo current = enumerator.Current;
					if (current.ID != month_id)
					{
						continue;
					}
					monthInfo = current;
					return monthInfo;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}