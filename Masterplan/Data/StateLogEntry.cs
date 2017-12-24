using System;

namespace Masterplan.Data
{
	[Serializable]
	public class StateLogEntry : IEncounterLogEntry
	{
		private Guid fID = Guid.Empty;

		private DateTime fTimestamp = DateTime.Now;

		private CreatureState fState;

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
				return true;
			}
		}

		public CreatureState State
		{
			get
			{
				return this.fState;
			}
			set
			{
				this.fState = value;
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

		public StateLogEntry()
		{
		}

		public string Description(Encounter enc, bool detailed)
		{
			string lower = "not bloodied";
			if (this.fState != CreatureState.Active)
			{
				lower = this.fState.ToString().ToLower();
			}
			return string.Concat(EncounterLog.GetName(this.fID, enc, detailed), " is <B>", lower, "</B>");
		}
	}
}