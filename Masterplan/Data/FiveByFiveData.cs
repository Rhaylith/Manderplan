using System;
using System.Collections;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class FiveByFiveData
	{
		private List<FiveByFiveColumn> fColumns = new List<FiveByFiveColumn>();

		public List<FiveByFiveColumn> Columns
		{
			get
			{
				return this.fColumns;
			}
			set
			{
				this.fColumns = value;
			}
		}

		public FiveByFiveData()
		{
		}

		public FiveByFiveData Copy()
		{
			FiveByFiveData fiveByFiveDatum = new FiveByFiveData();
			foreach (FiveByFiveColumn fColumn in this.fColumns)
			{
				fiveByFiveDatum.Columns.Add(fColumn.Copy());
			}
			return fiveByFiveDatum;
		}

		public void Initialise()
		{
			this.fColumns.Clear();
			List<PlotPointColour> plotPointColours = new List<PlotPointColour>();
			foreach (PlotPointColour value in Enum.GetValues(typeof(PlotPointColour)))
			{
				plotPointColours.Add(value);
			}
			for (int i = 0; i != 5; i++)
			{
				PlotPointColour item = plotPointColours[i % plotPointColours.Count];
				FiveByFiveColumn fiveByFiveColumn = new FiveByFiveColumn()
				{
					Name = item.ToString(),
					Colour = item
				};
				this.fColumns.Add(fiveByFiveColumn);
				for (int j = 1; j <= 5; j++)
				{
					FiveByFiveItem fiveByFiveItem = new FiveByFiveItem()
					{
						Details = string.Concat(fiveByFiveColumn.Name, " ", j)
					};
					fiveByFiveColumn.Items.Add(fiveByFiveItem);
				}
			}
		}
	}
}