using System;
using System.Collections.Generic;
using Utils;

namespace Masterplan.Data
{
	[Serializable]
	public class Artifact
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private Masterplan.Data.Tier fTier;

		private string fDescription = "";

		private string fDetails = "";

		private string fGoals = "";

		private string fRoleplayingTips = "";

		private List<MagicItemSection> fSections = new List<MagicItemSection>();

		private List<Pair<string, string>> fConcordanceRules = new List<Pair<string, string>>();

		private List<ArtifactConcordance> fConcordanceLevels = new List<ArtifactConcordance>();

		public List<ArtifactConcordance> ConcordanceLevels
		{
			get
			{
				return this.fConcordanceLevels;
			}
			set
			{
				this.fConcordanceLevels = value;
			}
		}

		public List<Pair<string, string>> ConcordanceRules
		{
			get
			{
				return this.fConcordanceRules;
			}
			set
			{
				this.fConcordanceRules = value;
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

		public string Goals
		{
			get
			{
				return this.fGoals;
			}
			set
			{
				this.fGoals = value;
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

		public string RoleplayingTips
		{
			get
			{
				return this.fRoleplayingTips;
			}
			set
			{
				this.fRoleplayingTips = value;
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

		public Artifact()
		{
			this.AddStandardConcordanceLevels();
		}

		public void AddStandardConcordanceLevels()
		{
			this.fConcordanceLevels.Add(new ArtifactConcordance("Pleased", "16-20"));
			this.fConcordanceLevels.Add(new ArtifactConcordance("Satisfied", "12-15"));
			this.fConcordanceLevels.Add(new ArtifactConcordance("Normal", "5-11"));
			this.fConcordanceLevels.Add(new ArtifactConcordance("Unsatisfied", "1-4"));
			this.fConcordanceLevels.Add(new ArtifactConcordance("Angered", "0 or lower"));
			this.fConcordanceLevels.Add(new ArtifactConcordance("Moving On", ""));
		}

		public Artifact Copy()
		{
			Artifact artifact = new Artifact()
			{
				ID = this.fID,
				Name = this.fName,
				Tier = this.fTier,
				Description = this.fDescription,
				Details = this.fDetails,
				Goals = this.fGoals,
				RoleplayingTips = this.fRoleplayingTips
			};
			artifact.Sections.Clear();
			foreach (MagicItemSection fSection in this.fSections)
			{
				artifact.Sections.Add(fSection.Copy());
			}
			artifact.ConcordanceRules.Clear();
			foreach (Pair<string, string> fConcordanceRule in this.fConcordanceRules)
			{
				Pair<string, string> pair = new Pair<string, string>(fConcordanceRule.First, fConcordanceRule.Second);
				artifact.ConcordanceRules.Add(pair);
			}
			artifact.ConcordanceLevels.Clear();
			foreach (ArtifactConcordance fConcordanceLevel in this.fConcordanceLevels)
			{
				artifact.ConcordanceLevels.Add(fConcordanceLevel.Copy());
			}
			return artifact;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}