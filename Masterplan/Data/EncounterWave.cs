using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class EncounterWave
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private bool fActive;

		private List<EncounterSlot> fSlots = new List<EncounterSlot>();

		public bool Active
		{
			get
			{
				return this.fActive;
			}
			set
			{
				this.fActive = value;
			}
		}

		public int Count
		{
			get
			{
				int count = 0;
				foreach (EncounterSlot fSlot in this.fSlots)
				{
					count += fSlot.CombatData.Count;
				}
				return count;
			}
		}

		public Guid ID
		{
			get
			{
				return this.fID;
			}
			set
			{
				this.fID = value;
			}
		}

		public string Name
		{
			get
			{
				return this.fName;
			}
			set
			{
				this.fName = value;
			}
		}

		public List<EncounterSlot> Slots
		{
			get
			{
				return this.fSlots;
			}
			set
			{
				this.fSlots = value;
			}
		}

		public EncounterWave()
		{
		}

		public EncounterWave Copy()
		{
			EncounterWave encounterWave = new EncounterWave()
			{
				ID = this.fID,
				Name = this.fName,
				Active = this.fActive
			};
			foreach (EncounterSlot fSlot in this.fSlots)
			{
				encounterWave.Slots.Add(fSlot.Copy());
			}
			return encounterWave;
		}
	}
}