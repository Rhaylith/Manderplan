using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class DamageLogEntry : IEncounterLogEntry
	{
		private Guid fID = Guid.Empty;

		private DateTime fTimestamp = DateTime.Now;

		private int fAmount;

		private List<DamageType> fTypes = new List<DamageType>();

		public int Amount
		{
			get
			{
				return this.fAmount;
			}
			set
			{
				this.fAmount = value;
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

		public List<DamageType> Types
		{
			get
			{
				return this.fTypes;
			}
			set
			{
				this.fTypes = value;
			}
		}

		public DamageLogEntry()
		{
		}

		public string Description(Encounter enc, bool detailed)
		{
			string str = "";
			if (this.fTypes != null)
			{
				foreach (DamageType fType in this.fTypes)
				{
					str = string.Concat(str, " ");
					str = string.Concat(str, fType.ToString().ToLower());
				}
			}
			string str1 = (this.fAmount >= 0 ? "takes" : "heals");
			object[] name = new object[] { EncounterLog.GetName(this.fID, enc, detailed), " ", str1, " ", Math.Abs(this.fAmount), str, " damage" };
			return string.Concat(name);
		}
	}
}