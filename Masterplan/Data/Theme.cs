using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class Theme : IPlayerOption
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fQuote = "";

		private string fPrerequisites = "";

		private string fSecondaryRole = "";

		private string fPowerSource = "";

		private PlayerPower fGrantedPower = new PlayerPower();

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

		public PlayerPower GrantedPower
		{
			get
			{
				return this.fGrantedPower;
			}
			set
			{
				this.fGrantedPower = value;
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

		public string PowerSource
		{
			get
			{
				return this.fPowerSource;
			}
			set
			{
				this.fPowerSource = value;
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

		public string SecondaryRole
		{
			get
			{
				return this.fSecondaryRole;
			}
			set
			{
				this.fSecondaryRole = value;
			}
		}

		public Theme()
		{
		}

		public Theme Copy()
		{
			Theme theme = new Theme()
			{
				ID = this.fID,
				Name = this.fName,
				Quote = this.fQuote,
				Prerequisites = this.fPrerequisites,
				SecondaryRole = this.fSecondaryRole,
				PowerSource = this.fPowerSource,
				GrantedPower = this.fGrantedPower.Copy(),
				Details = this.fDetails
			};
			foreach (LevelData fLevel in this.fLevels)
			{
				theme.Levels.Add(fLevel.Copy());
			}
			return theme;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}