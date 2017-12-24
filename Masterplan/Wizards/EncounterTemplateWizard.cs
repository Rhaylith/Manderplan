using Masterplan.Data;
using System;
using System.Collections.Generic;
using Utils;
using Utils.Wizards;

namespace Masterplan.Wizards
{
	internal class EncounterTemplateWizard : Wizard
	{
		private AdviceData fData = new AdviceData();

		private Encounter fEncounter;

		public override object Data
		{
			get
			{
				return this.fData;
			}
			set
			{
				this.fData = value as AdviceData;
			}
		}

		public EncounterTemplateWizard(List<Pair<EncounterTemplateGroup, EncounterTemplate>> templates, Encounter enc, int party_level) : base("Encounter Templates")
		{
			this.fData.Templates = templates;
			this.fData.PartyLevel = party_level;
			this.fEncounter = enc;
			this.fData.TabulaRasa = this.fEncounter.Count == 0;
		}

		public override void AddPages()
		{
			base.Pages.Add(new EncounterTemplatePage());
			base.Pages.Add(new EncounterSelectionPage());
		}

		public override int BackPageIndex(int current_page)
		{
			return base.BackPageIndex(current_page);
		}

		public override int NextPageIndex(int current_page)
		{
			return base.NextPageIndex(current_page);
		}

		public override void OnCancel()
		{
		}

		public override void OnFinish()
		{
			foreach (EncounterTemplateSlot slot in this.fData.SelectedTemplate.Slots)
			{
				if (!this.fData.FilledSlots.ContainsKey(slot))
				{
					continue;
				}
				EncounterSlot encounterSlot = new EncounterSlot()
				{
					Card = this.fData.FilledSlots[slot]
				};
				for (int i = 0; i != slot.Count; i++)
				{
					encounterSlot.CombatData.Add(new CombatData());
				}
				this.fEncounter.Slots.Add(encounterSlot);
			}
		}
	}
}