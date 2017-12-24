using System;

namespace Masterplan.Data
{
	[Serializable]
	public class SkillChallengeResult
	{
		private int fSuccesses;

		private int fFails;

		public int Fails
		{
			get
			{
				return this.fFails;
			}
			set
			{
				this.fFails = value;
			}
		}

		public int Successes
		{
			get
			{
				return this.fSuccesses;
			}
			set
			{
				this.fSuccesses = value;
			}
		}

		public SkillChallengeResult()
		{
		}

		public SkillChallengeResult Copy()
		{
			SkillChallengeResult skillChallengeResult = new SkillChallengeResult()
			{
				Successes = this.fSuccesses,
				Fails = this.fFails
			};
			return skillChallengeResult;
		}
	}
}