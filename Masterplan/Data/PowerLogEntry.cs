using System;

namespace Masterplan.Data
{
	[Serializable]
	public class PowerLogEntry : IEncounterLogEntry
	{
		private Guid fID = Guid.Empty;

		private DateTime fTimestamp = DateTime.Now;

		private string fPowerName = "";

		private bool fAdded = true;

		public bool Added
		{
			get
			{
				return this.fAdded;
			}
			set
			{
				this.fAdded = value;
			}
		}

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

		public string PowerName
		{
			get
			{
				return this.fPowerName;
			}
			set
			{
				this.fPowerName = value;
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

		public PowerLogEntry()
		{
		}

		public string Description(Encounter enc, bool detailed)
		{
			string name = EncounterLog.GetName(this.fID, enc, detailed);
			if (this.fAdded)
			{
				return string.Concat(name, " used <B>", this.fPowerName, "</B>");
			}
			return string.Concat(name, " recharged <B>", this.fPowerName, "</B>");
		}
	}
}