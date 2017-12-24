using Masterplan;
using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	internal class ReportTable
	{
		private Masterplan.Data.ReportType fReportType;

		private Masterplan.Data.BreakdownType fBreakdownType;

		private List<ReportRow> fRows = new List<ReportRow>();

		public Masterplan.Data.BreakdownType BreakdownType
		{
			get
			{
				return this.fBreakdownType;
			}
			set
			{
				this.fBreakdownType = value;
			}
		}

		public int GrandTotal
		{
			get
			{
				int total = 0;
				foreach (ReportRow fRow in this.fRows)
				{
					total += fRow.Total;
				}
				return total;
			}
		}

		public Masterplan.Data.ReportType ReportType
		{
			get
			{
				return this.fReportType;
			}
			set
			{
				this.fReportType = value;
			}
		}

		public int Rounds
		{
			get
			{
				int num = 0;
				foreach (ReportRow fRow in this.fRows)
				{
					num = Math.Max(num, fRow.Values.Count);
				}
				return num;
			}
		}

		public List<ReportRow> Rows
		{
			get
			{
				return this.fRows;
			}
		}

		public ReportTable()
		{
		}

		public int ColumnTotal(int column)
		{
			int item = 0;
			foreach (ReportRow fRow in this.fRows)
			{
				item += fRow.Values[column];
			}
			return item;
		}

		public void ReduceToPCs()
		{
			List<ReportRow> reportRows = new List<ReportRow>();
			foreach (ReportRow fRow in this.fRows)
			{
				if (Session.Project.FindHero(fRow.CombatantID) != null)
				{
					continue;
				}
				reportRows.Add(fRow);
			}
			foreach (ReportRow reportRow in reportRows)
			{
				this.fRows.Remove(reportRow);
			}
		}
	}
}