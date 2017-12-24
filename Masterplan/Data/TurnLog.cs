using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	internal class TurnLog
	{
		private Guid fID = Guid.Empty;

		private List<IEncounterLogEntry> fEntries = new List<IEncounterLogEntry>();

		public DateTime Start = DateTime.MinValue;

		public DateTime End = DateTime.MinValue;

		public List<IEncounterLogEntry> Entries
		{
			get
			{
				return this.fEntries;
			}
		}

		public Guid ID
		{
			get
			{
				return this.fID;
			}
		}

		public TurnLog(Guid id)
		{
			this.fID = id;
		}

		public int Damage(List<Guid> allyIDs)
		{
			int amount = 0;
			foreach (IEncounterLogEntry fEntry in this.fEntries)
			{
				DamageLogEntry damageLogEntry = fEntry as DamageLogEntry;
				if (damageLogEntry == null || !allyIDs.Contains(damageLogEntry.CombatantID))
				{
					continue;
				}
				amount += damageLogEntry.Amount;
			}
			return amount;
		}

		public int Movement()
		{
			int distance = 0;
			foreach (IEncounterLogEntry fEntry in this.fEntries)
			{
				MoveLogEntry moveLogEntry = fEntry as MoveLogEntry;
				if (moveLogEntry == null || moveLogEntry.Distance <= 0)
				{
					continue;
				}
				distance += moveLogEntry.Distance;
			}
			return distance;
		}

		public TimeSpan Time()
		{
			TimeSpan end = this.End - this.Start;
			if (end.Ticks < (long)0)
			{
				return new TimeSpan((long)0);
			}
			IEncounterLogEntry encounterLogEntry = null;
			foreach (IEncounterLogEntry fEntry in this.fEntries)
			{
				if (!(fEntry is PauseLogEntry))
				{
					if (!(fEntry is ResumeLogEntry) || encounterLogEntry == null)
					{
						continue;
					}
					end -= (fEntry.Timestamp - encounterLogEntry.Timestamp);
					encounterLogEntry = null;
				}
				else
				{
					encounterLogEntry = fEntry;
				}
			}
			return end;
		}
	}
}