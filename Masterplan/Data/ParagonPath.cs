using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class ParagonPath : IPlayerOption
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fQuote = "";

		private string fPrerequisites = "11th level";

		private string fDetails = "";

		private List<LevelData> fLevels = new List<LevelData>();

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

		public List<LevelData> Levels
		{
			get
			{
				return this.fLevels;
			}
			set
			{
				this.fLevels = value;
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

		public string Prerequisites
		{
			get
			{
				return this.fPrerequisites;
			}
			set
			{
				this.fPrerequisites = value;
			}
		}

		public string Quote
		{
			get
			{
				return this.fQuote;
			}
			set
			{
				this.fQuote = value;
			}
		}

		public ParagonPath()
		{
		}

		public ParagonPath Copy()
		{
			ParagonPath paragonPath = new ParagonPath()
			{
				ID = this.fID,
				Name = this.fName,
				Quote = this.fQuote,
				Prerequisites = this.fPrerequisites,
				Details = this.fDetails
			};
			foreach (LevelData fLevel in this.fLevels)
			{
				paragonPath.Levels.Add(fLevel.Copy());
			}
			return paragonPath;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}