using System;

namespace Masterplan.Data
{
	public interface IEncounterLogEntry
	{
		Guid CombatantID
		{
			get;
		}

		bool Important
		{
			get;
		}

		DateTime Timestamp
		{
			get;
		}

		string Description(Encounter enc, bool detailed);
	}
}