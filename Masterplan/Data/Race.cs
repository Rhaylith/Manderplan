using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class Race : IPlayerOption
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fQuote = "";

		private string fHeightRange = "";

		private string fWeightRange = "";

		private string fAbilityScores = "";

		private CreatureSize fSize = CreatureSize.Medium;

		private string fSpeed = "6 squares";

		private string fVision = "Normal";

		private string fLanguages = "Common";

		private string fSkillBonuses = "";

		private List<Feature> fFeatures = new List<Feature>();

		private List<PlayerPower> fPowers = new List<PlayerPower>();

		private string fDetails = "";

		public string AbilityScores
		{
			get
			{
				return this.fAbilityScores;
			}
			set
			{
				this.fAbilityScores = value;
			}
		}

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

		public List<Feature> Features
		{
			get
			{
				return this.fFeatures;
			}
			set
			{
				this.fFeatures = value;
			}
		}

		public string HeightRange
		{
			get
			{
				return this.fHeightRange;
			}
			set
			{
				this.fHeightRange = value;
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

		public string Languages
		{
			get
			{
				return this.fLanguages;
			}
			set
			{
				this.fLanguages = value;
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

		public List<PlayerPower> Powers
		{
			get
			{
				return this.fPowers;
			}
			set
			{
				this.fPowers = value;
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

		public CreatureSize Size
		{
			get
			{
				return this.fSize;
			}
			set
			{
				this.fSize = value;
			}
		}

		public string SkillBonuses
		{
			get
			{
				return this.fSkillBonuses;
			}
			set
			{
				this.fSkillBonuses = value;
			}
		}

		public string Speed
		{
			get
			{
				return this.fSpeed;
			}
			set
			{
				this.fSpeed = value;
			}
		}

		public string Vision
		{
			get
			{
				return this.fVision;
			}
			set
			{
				this.fVision = value;
			}
		}

		public string WeightRange
		{
			get
			{
				return this.fWeightRange;
			}
			set
			{
				this.fWeightRange = value;
			}
		}

		public Race()
		{
		}

		public Race Copy()
		{
			Race race = new Race()
			{
				ID = this.fID,
				Name = this.fName,
				Quote = this.fQuote,
				HeightRange = this.fHeightRange,
				WeightRange = this.fWeightRange,
				AbilityScores = this.fAbilityScores,
				Size = this.fSize,
				Speed = this.fSpeed,
				Vision = this.fVision,
				Languages = this.fLanguages,
				SkillBonuses = this.fSkillBonuses,
				Details = this.fDetails
			};
			foreach (Feature fFeature in this.fFeatures)
			{
				race.Features.Add(fFeature.Copy());
			}
			foreach (PlayerPower fPower in this.fPowers)
			{
				race.Powers.Add(fPower.Copy());
			}
			return race;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}