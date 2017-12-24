using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class EncyclopediaGroup : IEncyclopediaItem, IComparable<EncyclopediaGroup>
	{
		private string fName = "";

		private Guid fID = Guid.NewGuid();

		private List<Guid> fIDs = new List<Guid>();

		public List<Guid> EntryIDs
		{
			get
			{
				return this.fIDs;
			}
			set
			{
				this.fIDs = value;
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

		public EncyclopediaGroup()
		{
		}

		public int CompareTo(EncyclopediaGroup rhs)
		{
			return this.fName.CompareTo(rhs.Name);
		}

		public EncyclopediaGroup Copy()
		{
			EncyclopediaGroup encyclopediaGroup = new EncyclopediaGroup()
			{
				Name = this.fName,
				ID = this.fID
			};
			foreach (Guid fID in this.fIDs)
			{
				encyclopediaGroup.EntryIDs.Add(fID);
			}
			return encyclopediaGroup;
		}
	}
}