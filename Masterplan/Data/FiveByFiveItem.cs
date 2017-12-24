using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class FiveByFiveItem
	{
		private Guid fID = Guid.NewGuid();

		private string fDetails = "";

		private List<Guid> fLinkIDs = new List<Guid>();

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

		public List<Guid> LinkIDs
		{
			get
			{
				return this.fLinkIDs;
			}
			set
			{
				this.fLinkIDs = value;
			}
		}

		public FiveByFiveItem()
		{
		}

		public FiveByFiveItem Copy()
		{
			FiveByFiveItem fiveByFiveItem = new FiveByFiveItem()
			{
				ID = this.fID,
				Details = this.fDetails
			};
			foreach (Guid fLinkID in this.fLinkIDs)
			{
				fiveByFiveItem.LinkIDs.Add(fLinkID);
			}
			return fiveByFiveItem;
		}
	}
}