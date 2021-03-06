using System;

namespace Masterplan.Data
{
	[Serializable]
	public class MonthInfo
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private int fDayCount = 30;

		private int fModifier;

		private int fPeriod = 4;

		public int DayCount
		{
			get
			{
				return this.fDayCount;
			}
			set
			{
				this.fDayCount = value;
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

		public int LeapModifier
		{
			get
			{
				return this.fModifier;
			}
			set
			{
				this.fModifier = value;
			}
		}

		public int LeapPeriod
		{
			get
			{
				return this.fPeriod;
			}
			set
			{
				this.fPeriod = value;
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

		public MonthInfo()
		{
		}

		public MonthInfo Copy()
		{
			MonthInfo monthInfo = new MonthInfo()
			{
				ID = this.fID,
				Name = this.fName,
				DayCount = this.fDayCount,
				LeapModifier = this.fModifier,
				LeapPeriod = this.fPeriod
			};
			return monthInfo;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}