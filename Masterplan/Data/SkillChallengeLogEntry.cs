using System;

namespace Masterplan.Data
{
	[Serializable]
	public class SkillChallengeLogEntry : IEncounterLogEntry
	{
		private Guid fID = Guid.Empty;

		private DateTime fTimestamp = DateTime.Now;

		private bool fSuccess = true;

		public Guid CombatantID
		{
			get
			{
				return JustDecompileGenerated_get_CombatantID();
			}
			set
			{
				JustDecompileGenerated_set_CombatantID(value);
			}
		}

		public Guid JustDecompileGenerated_get_CombatantID()
		{
			return this.fID;
		}

		public void JustDecompileGenerated_set_CombatantID(Guid value)
		{
			this.fID = value;
		}

		public bool Important
		{
			get
			{
				return false;
			}
		}

		public bool Success
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

		public DateTime Timestamp
		{
			get
			{
				return JustDecompileGenerated_get_Timestamp();
			}
			set
			{
				JustDecompileGenerated_set_Timestamp(value);
			}
		}

		public DateTime JustDecompileGenerated_get_Timestamp()
		{
			return this.fTimestamp;
		}

		public void JustDecompileGenerated_set_Timestamp(DateTime value)
		{
			this.fTimestamp = value;
		}

		public SkillChallengeLogEntry()
		{
		}

		public string Description(Encounter enc, bool detailed)
		{
			string name = EncounterLog.GetName(this.fID, enc, detailed);
			if (this.fSuccess)
			{
				return string.Concat(name, " gained a success");
			}
			return string.Concat(name, " incurred a failure");
		}
	}
}