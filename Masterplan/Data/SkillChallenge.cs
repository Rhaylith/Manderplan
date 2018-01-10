using Masterplan;
using Masterplan.Tools;
using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class SkillChallenge : IElement
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private int fLevel = -1;

		private int fComplexity = 1;

		private List<SkillChallengeData> fSkills = new List<SkillChallengeData>();

		private string fSuccess = "";

		private string fFailure = "";

		private string fNotes = "";

		private Guid fMapID = Guid.Empty;

		private Guid fMapAreaID = Guid.Empty;

		public int Complexity
		{
			get
			{
				return this.fComplexity;
			}
			set
			{
				this.fComplexity = value;
			}
		}

		public string Failure
		{
			get
			{
				return this.fFailure;
			}
			set
			{
				this.fFailure = value;
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
				if (this.fLevel == -1)
				{
					return string.Concat(this.Successes, " successes before 3 failures");
				}
				object[] successes = new object[] { "Level ", this.fLevel, ", ", this.Successes, " successes before 3 failures" };
				return string.Concat(successes);
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

		public Guid MapAreaID
		{
			get
			{
				return this.fMapAreaID;
			}
			set
			{
				this.fMapAreaID = value;
			}
		}

		public Guid MapID
		{
			get
			{
				return this.fMapID;
			}
			set
			{
				this.fMapID = value;
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

		public string Notes
		{
			get
			{
				return this.fNotes;
			}
			set
			{
				this.fNotes = value;
			}
		}

		public SkillChallengeResult Results
		{
			get
			{
				SkillChallengeResult skillChallengeResult = new SkillChallengeResult();
				foreach (SkillChallengeData fSkill in this.fSkills)
				{
					if (fSkill.Results == null)
					{
						continue;
					}
					SkillChallengeResult successes = skillChallengeResult;
					successes.Successes = successes.Successes + fSkill.Results.Successes;
					SkillChallengeResult fails = skillChallengeResult;
					fails.Fails = fails.Fails + fSkill.Results.Fails;
				}
				return skillChallengeResult;
			}
		}

		public List<SkillChallengeData> Skills
		{
			get
			{
				return this.fSkills;
			}
			set
			{
				this.fSkills = value;
			}
		}

		public string Success
		{
			get
			{
				return this.fSuccess;
			}
			set
			{
				this.fSuccess = value;
			}
		}

		public int Successes
		{
			get
			{
				return SkillChallenge.GetSuccesses(this.fComplexity);
			}
		}

		public SkillChallenge()
		{
		}

		public IElement Copy()
		{
			SkillChallenge skillChallenge = new SkillChallenge()
			{
				ID = this.fID,
				Name = this.fName,
				Level = this.fLevel,
				Complexity = this.fComplexity
			};
			foreach (SkillChallengeData fSkill in this.fSkills)
			{
				skillChallenge.Skills.Add(fSkill.Copy());
			}
			skillChallenge.Success = this.fSuccess;
			skillChallenge.Failure = this.fFailure;
			skillChallenge.Notes = this.fNotes;
			skillChallenge.MapID = this.fMapID;
			skillChallenge.MapAreaID = this.fMapAreaID;
			return skillChallenge;
		}

		public SkillChallengeData FindSkill(string skill_name)
		{
			SkillChallengeData skillChallengeDatum;
			List<SkillChallengeData>.Enumerator enumerator = this.fSkills.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					SkillChallengeData current = enumerator.Current;
					if (current.SkillName != skill_name)
					{
						continue;
					}
					skillChallengeDatum = current;
					return skillChallengeDatum;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		public Difficulty GetDifficulty(int party_level, int party_size)
		{
			if (this.fSkills.Count == 0)
			{
				return Difficulty.Trivial;
			}
			List<Difficulty> difficulties = new List<Difficulty>()
			{
				AI.GetThreatDifficulty(this.fLevel, party_level)
			};
			foreach (SkillChallengeData fSkill in this.fSkills)
			{
				difficulties.Add(fSkill.Difficulty);
			}
			if (difficulties.Contains(Difficulty.Extreme))
			{
				return Difficulty.Extreme;
			}
			if (difficulties.Contains(Difficulty.Hard))
			{
				return Difficulty.Hard;
			}
			if (difficulties.Contains(Difficulty.Moderate))
			{
				return Difficulty.Moderate;
			}
			if (difficulties.Contains(Difficulty.Easy))
			{
				return Difficulty.Easy;
			}
			return Difficulty.Trivial;
		}

		public static int GetSuccesses(int complexity)
		{
			return (complexity + 1) * 2;
		}

		public static int GetXP(int level, int complexity)
		{
			int creatureXP = Experience.GetCreatureXP(level) * complexity;
			if (Session.Project != null)
			{
				creatureXP = (int)((double)creatureXP * Session.Project.CampaignSettings.XP);
			}
			return creatureXP;
		}

		public int GetXP()
		{
			return SkillChallenge.GetXP(this.fLevel, this.fComplexity);
		}
	}
}