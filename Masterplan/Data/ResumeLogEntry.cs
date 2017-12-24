using System;

namespace Masterplan.Data
{
	[Serializable]
	public class ResumeLogEntry : IEncounterLogEntry
	{
		private DateTime fTimestamp = DateTime.Now;

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

		public ResumeLogEntry()
		{
		}

		public string Description(Encounter enc, bool detailed)
		{
			string[] shortTimeString = new string[] { "Resumed (", this.fTimestamp.ToShortTimeString(), " ", this.fTimestamp.ToShortDateString(), ")" };
			return string.Concat(shortTimeString);
		}
	}
}