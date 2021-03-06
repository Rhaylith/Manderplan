using System;

namespace Masterplan.Data
{
	[Serializable]
	public class Feat : IPlayerOption
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private Masterplan.Data.Tier fTier;

		private string fPrerequisites = "";

		private string fBenefits = "";

		public string Benefits
		{
			get
			{
				return this.fBenefits;
			}
			set
			{
				this.fBenefits = value;
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

		public Masterplan.Data.Tier Tier
		{
			get
			{
				return this.fTier;
			}
			set
			{
				this.fTier = value;
			}
		}

		public Feat()
		{
		}

		public Feat Copy()
		{
			Feat feat = new Feat()
			{
				ID = this.fID,
				Name = this.fName,
				Tier = this.fTier,
				Prerequisites = this.fPrerequisites,
				Benefits = this.fBenefits
			};
			return feat;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}