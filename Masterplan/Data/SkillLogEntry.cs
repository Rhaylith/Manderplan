using System;

namespace Masterplan.Data
{
	[Serializable]
	public class SkillLogEntry : IEncounterLogEntry
	{
		private Guid fID = Guid.Empty;

		private DateTime fTimestamp = DateTime.Now;

		private string fSkillName = "";

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

		public string SkillName
		{
			get
			{
				return this.fSkillName;
			}
			set
			{
				this.fSkillName = value;
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

		public SkillLogEntry()
		{
		}

		public string Description(Encounter enc, bool detailed)
		{
			string name = EncounterLog.GetName(this.fID, enc, detailed);
			return string.Concat(name, " used <B>", this.fSkillName, "</B>");
		}
	}
}