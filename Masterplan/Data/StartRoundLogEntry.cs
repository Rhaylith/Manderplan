using System;

namespace Masterplan.Data
{
	[Serializable]
	public class StartRoundLogEntry : IEncounterLogEntry
	{
		private DateTime fTimestamp = DateTime.Now;

		private int fRound = 1;

		public Guid CombatantID
		{
			get
			{
				return Guid.Empty;
			}
		}

		public bool Important
		{
			get
			{
				return true;
			}
		}

		public int Round
		{
			get
			{
				return this.fRound;
			}
			set
			{
				this.fRound = value;
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

		public StartRoundLogEntry()
		{
		}

		public string Description(Encounter enc, bool detailed)
		{
			return string.Concat("Round ", this.fRound);
		}
	}
}