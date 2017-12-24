using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class Class : IPlayerOption
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fQuote = "";

		private string fRole = "";

		private string fPowerSource = "";

		private string fKeyAbilities = "";

		private string fArmourProficiencies = "";

		private string fWeaponProficiencies = "";

		private string fImplements = "";

		private string fDefenceBonuses = "";

		private int fHPFirst;

		private int fHPSubsequent;

		private int fHealingSurges;

		private string fTrainedSkills = "";

		private string fDescription = "";

		private string fOverviewCharacteristics = "";

		private string fOverviewReligion = "";

		private string fOverviewRaces = "";

		private LevelData fFeatureData = new LevelData();

		private List<LevelData> fLevels = new List<LevelData>();

		public string ArmourProficiencies
		{
			get
			{
				return this.fArmourProficiencies;
			}
			set
			{
				this.fArmourProficiencies = value;
			}
		}

		public string DefenceBonuses
		{
			get
			{
				return this.fDefenceBonuses;
			}
			set
			{
				this.fDefenceBonuses = value;
			}
		}

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

		public LevelData FeatureData
		{
			get
			{
				return this.fFeatureData;
			}
			set
			{
				this.fFeatureData = value;
			}
		}

		public int HealingSurges
		{
			get
			{
				return this.fHealingSurges;
			}
			set
			{
				this.fHealingSurges = value;
			}
		}

		public int HPFirst
		{
			get
			{
				return this.fHPFirst;
			}
			set
			{
				this.fHPFirst = value;
			}
		}

		public int HPSubsequent
		{
			get
			{
				return this.fHPSubsequent;
			}
			set
			{
				this.fHPSubsequent = value;
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

		public string Implements
		{
			get
			{
				return this.fImplements;
			}
			set
			{
				this.fImplements = value;
			}
		}

		public string KeyAbilities
		{
			get
			{
				return this.fKeyAbilities;
			}
			set
			{
				this.fKeyAbilities = value;
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

		public string OverviewCharacteristics
		{
			get
			{
				return this.fOverviewCharacteristics;
			}
			set
			{
				this.fOverviewCharacteristics = value;
			}
		}

		public string OverviewRaces
		{
			get
			{
				return this.fOverviewRaces;
			}
			set
			{
				this.fOverviewRaces = value;
			}
		}

		public string OverviewReligion
		{
			get
			{
				return this.fOverviewReligion;
			}
			set
			{
				this.fOverviewReligion = value;
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

		public string Role
		{
			get
			{
				return this.fRole;
			}
			set
			{
				this.fRole = value;
			}
		}

		public string TrainedSkills
		{
			get
			{
				return this.fTrainedSkills;
			}
			set
			{
				this.fTrainedSkills = value;
			}
		}

		public string WeaponProficiencies
		{
			get
			{
				return this.fWeaponProficiencies;
			}
			set
			{
				this.fWeaponProficiencies = value;
			}
		}

		public Class()
		{
		}

		public Class Copy()
		{
			Class @class = new Class()
			{
				ID = this.fID,
				Name = this.fName,
				Quote = this.fQuote,
				Role = this.fRole,
				PowerSource = this.fPowerSource,
				KeyAbilities = this.fKeyAbilities,
				ArmourProficiencies = this.fArmourProficiencies,
				WeaponProficiencies = this.fWeaponProficiencies,
				Implements = this.fImplements,
				DefenceBonuses = this.fDefenceBonuses,
				HPFirst = this.fHPFirst,
				HPSubsequent = this.fHPSubsequent,
				HealingSurges = this.fHealingSurges,
				TrainedSkills = this.fTrainedSkills,
				Description = this.fDescription,
				OverviewCharacteristics = this.fOverviewCharacteristics,
				OverviewReligion = this.fOverviewReligion,
				OverviewRaces = this.fOverviewRaces,
				FeatureData = this.fFeatureData.Copy()
			};
			foreach (LevelData fLevel in this.fLevels)
			{
				@class.Levels.Add(fLevel.Copy());
			}
			return @class;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}