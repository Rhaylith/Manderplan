using System;

namespace Masterplan.Data
{
	[Serializable]
	public class PlayerBackground : IPlayerOption
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fDetails = "";

		private string fAssociatedSkills = "";

		private string fRecommendedFeats = "";

		public string AssociatedSkills
		{
			get
			{
				return this.fAssociatedSkills;
			}
			set
			{
				this.fAssociatedSkills = value;
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

		public string RecommendedFeats
		{
			get
			{
				return this.fRecommendedFeats;
			}
			set
			{
				this.fRecommendedFeats = value;
			}
		}

		public PlayerBackground()
		{
		}

		public PlayerBackground Copy()
		{
			PlayerBackground playerBackground = new PlayerBackground()
			{
				ID = this.fID,
				Name = this.fName,
				Details = this.fDetails,
				AssociatedSkills = this.fAssociatedSkills,
				RecommendedFeats = this.fRecommendedFeats
			};
			return playerBackground;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}