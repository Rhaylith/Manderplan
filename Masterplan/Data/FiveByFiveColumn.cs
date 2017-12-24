using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class FiveByFiveColumn
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private PlotPointColour fColour;

		private List<FiveByFiveItem> fItems = new List<FiveByFiveItem>();

		public PlotPointColour Colour
		{
			get
			{
				return this.fColour;
			}
			set
			{
				this.fColour = value;
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

		public List<FiveByFiveItem> Items
		{
			get
			{
				return this.fItems;
			}
			set
			{
				this.fItems = value;
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

		public FiveByFiveColumn()
		{
		}

		public FiveByFiveColumn Copy()
		{
			FiveByFiveColumn fiveByFiveColumn = new FiveByFiveColumn()
			{
				ID = this.fID,
				Name = this.fName,
				Colour = this.fColour
			};
			foreach (FiveByFiveItem fItem in this.fItems)
			{
				fiveByFiveColumn.Items.Add(fItem.Copy());
			}
			return fiveByFiveColumn;
		}
	}
}