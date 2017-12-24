using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class MagicItem : IComparable<MagicItem>
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fType = "Weapon";

		private MagicItemRarity fRarity = MagicItemRarity.Uncommon;

		private int fLevel = 1;

		private string fDescription = "";

		private List<MagicItemSection> fSections = new List<MagicItemSection>();

		private string fURL = "";

		public string Description
		{
			get
			{
				return this.fDescription;
			}
			set
			{
				this.fDescription = value;
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

		public string Info
		{
			get
			{
				object[] lower = new object[] { "Level ", this.fLevel, " ", this.fType.ToLower() };
				return string.Concat(lower);
			}
		}

		public int Level
		{
			get
			{
				return this.fLevel;
			}
			set
			{
				this.fLevel = value;
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

		public MagicItemRarity Rarity
		{
			get
			{
				return this.fRarity;
			}
			set
			{
				this.fRarity = value;
			}
		}

		public List<MagicItemSection> Sections
		{
			get
			{
				return this.fSections;
			}
			set
			{
				this.fSections = value;
			}
		}

		public string Type
		{
			get
			{
				return this.fType;
			}
			set
			{
				this.fType = value;
			}
		}

		public string URL
		{
			get
			{
				return this.fURL;
			}
			set
			{
				this.fURL = value;
			}
		}

		public MagicItem()
		{
		}

		public int CompareTo(MagicItem rhs)
		{
			return this.fName.CompareTo(rhs.Name);
		}

		public MagicItem Copy()
		{
			MagicItem magicItem = new MagicItem()
			{
				ID = this.fID,
				Name = this.fName,
				Type = this.fType,
				Rarity = this.fRarity,
				Level = this.fLevel,
				Description = this.fDescription
			};
			foreach (MagicItemSection fSection in this.fSections)
			{
				magicItem.Sections.Add(fSection.Copy());
			}
			magicItem.URL = this.fURL;
			return magicItem;
		}
	}
}