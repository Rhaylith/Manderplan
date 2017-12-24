using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	internal class ReportRow : IComparable<ReportRow>
	{
		private string fHeading = "";

		private Guid fCombatantID = Guid.Empty;

		private List<int> fValues = new List<int>();

		public double Average
		{
			get
			{
				return (double)this.Total / (double)this.fValues.Count;
			}
		}

		public Guid CombatantID
		{
			get
			{
				return this.fCombatantID;
			}
			set
			{
				this.fCombatantID = value;
			}
		}

		public string Heading
		{
			get
			{
				return this.fHeading;
			}
			set
			{
				this.fHeading = value;
			}
		}

		public int Total
		{
			get
			{
				int num = 0;
				foreach (int fValue in this.fValues)
				{
					num += fValue;
				}
				return num;
			}
		}

		public List<int> Values
		{
			get
			{
				return this.fValues;
			}
		}

		public ReportRow()
		{
		}

		public int CompareTo(ReportRow rhs)
		{
			return this.Total.CompareTo(rhs.Total) * -1;
		}
	}
}