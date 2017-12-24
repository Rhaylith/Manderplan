using System;

namespace Masterplan.Data
{
	[Serializable]
	public class MoveLogEntry : IEncounterLogEntry
	{
		private Guid fID = Guid.Empty;

		private DateTime fTimestamp = DateTime.Now;

		private int fDistance;

		private string fDetails = "";

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

		public int Distance
		{
			get
			{
				return this.fDistance;
			}
			set
			{
				this.fDistance = value;
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

		public MoveLogEntry()
		{
		}

		public string Description(Encounter enc, bool detailed)
		{
			string name = EncounterLog.GetName(this.fID, enc, detailed);
			string str = string.Concat(name, " moves");
			if (this.fDistance > 0)
			{
				object obj = str;
				object[] objArray = new object[] { obj, " ", this.fDistance, " sq" };
				str = string.Concat(objArray);
			}
			if (this.fDetails != "")
			{
				str = string.Concat(str, " ", this.fDetails.Trim());
			}
			return str;
		}
	}
}