using System;

namespace Masterplan.Data
{
	[Serializable]
	public class EffectLogEntry : IEncounterLogEntry
	{
		private Guid fID = Guid.Empty;

		private DateTime fTimestamp = DateTime.Now;

		private string fEffectText = "";

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

		public string EffectText
		{
			get
			{
				return this.fEffectText;
			}
			set
			{
				this.fEffectText = value;
			}
		}

		public bool Important
		{
			get
			{
				return false;
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

		public EffectLogEntry()
		{
		}

		public string Description(Encounter enc, bool detailed)
		{
			string name = EncounterLog.GetName(this.fID, enc, detailed);
			if (this.fAdded)
			{
				return string.Concat(name, " gained ", this.fEffectText);
			}
			return string.Concat(name, " lost ", this.fEffectText);
		}
	}
}