using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class EncounterTemplate
	{
		private Masterplan.Data.Difficulty fDifficulty = Masterplan.Data.Difficulty.Moderate;

		private List<EncounterTemplateSlot> fSlots = new List<EncounterTemplateSlot>();

		public Masterplan.Data.Difficulty Difficulty
		{
			get
			{
				return this.fDifficulty;
			}
			set
			{
				this.fDifficulty = value;
			}
		}

		public List<EncounterTemplateSlot> Slots
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

		public EncounterTemplate()
		{
		}

		public EncounterTemplate(Masterplan.Data.Difficulty diff)
		{
			this.fDifficulty = diff;
		}

		public EncounterTemplate Copy()
		{
			EncounterTemplate encounterTemplate = new EncounterTemplate()
			{
				Difficulty = this.fDifficulty
			};
			foreach (EncounterTemplateSlot fSlot in this.fSlots)
			{
				encounterTemplate.Slots.Add(fSlot.Copy());
			}
			return encounterTemplate;
		}

		public EncounterTemplateSlot FindSlot(EncounterSlot enc_slot, int level)
		{
			EncounterTemplateSlot encounterTemplateSlot;
			List<EncounterTemplateSlot>.Enumerator enumerator = this.fSlots.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					EncounterTemplateSlot current = enumerator.Current;
					if (current.Count < enc_slot.CombatData.Count || !current.Match(enc_slot.Card, level))
					{
						continue;
					}
					encounterTemplateSlot = current;
					return encounterTemplateSlot;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return encounterTemplateSlot;
		}
	}
}