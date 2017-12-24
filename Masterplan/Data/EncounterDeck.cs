using Masterplan;
using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class EncounterDeck
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private int fLevel = 1;

		private List<EncounterCard> fCards = new List<EncounterCard>();

		public List<EncounterCard> Cards
		{
			get
			{
				return this.fCards;
			}
			set
			{
				this.fCards = value;
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

		public int Level
		{
			get
			{
				return this.fLevel;
			}
			set
			{
				this.fLevel = value;
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

		public EncounterDeck()
		{
		}

		public EncounterDeck Copy()
		{
			EncounterDeck encounterDeck = new EncounterDeck()
			{
				ID = this.fID,
				Name = this.fName,
				Level = this.fLevel
			};
			foreach (EncounterCard fCard in this.fCards)
			{
				encounterDeck.Cards.Add(fCard.Copy());
			}
			return encounterDeck;
		}

		public int Count(CardCategory cat)
		{
			int num = 0;
			foreach (EncounterCard fCard in this.fCards)
			{
				if (fCard.Category != cat)
				{
					continue;
				}
				num++;
			}
			return num;
		}

		public int Count(int level)
		{
			int num = 0;
			foreach (EncounterCard fCard in this.fCards)
			{
				if (fCard.Level != level)
				{
					continue;
				}
				num++;
			}
			return num;
		}

		public bool DrawDelve(PlotPoint pp, Map map)
		{
			bool flag;
			List<MapArea>.Enumerator enumerator = map.Areas.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					MapArea current = enumerator.Current;
					Encounter encounter = new Encounter();
					if (this.DrawEncounter(encounter))
					{
						PlotPoint plotPoint = new PlotPoint(current.Name)
						{
							Element = encounter
						};
						pp.Subplot.Points.Add(plotPoint);
					}
					else
					{
						flag = false;
						return flag;
					}
				}
				return true;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return flag;
		}

		public bool DrawEncounter(Encounter enc)
		{
			if (this.fCards.Count == 0)
			{
				return false;
			}
			List<EncounterCard> encounterCards = new List<EncounterCard>();
			List<EncounterCard> encounterCards1 = new List<EncounterCard>();
			foreach (EncounterCard fCard in this.fCards)
			{
				if (fCard.Drawn)
				{
					continue;
				}
				encounterCards1.Add(fCard);
			}
			int num = 0;
			while (true)
			{
				num++;
				bool flag = false;
				int size = Session.Project.Party.Size;
				while (encounterCards.Count < size && encounterCards1.Count != 0)
				{
					int num1 = Session.Random.Next() % encounterCards1.Count;
					EncounterCard item = encounterCards1[num1];
					encounterCards.Add(item);
					encounterCards1.Remove(item);
					if (item.Category != CardCategory.Lurker || flag)
					{
						continue;
					}
					size++;
					flag = true;
				}
				int num2 = 0;
				foreach (EncounterCard encounterCard in encounterCards)
				{
					if (encounterCard.Category != CardCategory.SoldierBrute)
					{
						continue;
					}
					num2++;
				}
				if (num2 == 1 || num == 1000)
				{
					break;
				}
				encounterCards1.AddRange(encounterCards);
				encounterCards.Clear();
			}
			foreach (EncounterCard encounterCard1 in encounterCards)
			{
				if (encounterCard1.Category != CardCategory.Solo)
				{
					continue;
				}
				encounterCards.Remove(encounterCard1);
				encounterCards1.AddRange(encounterCards);
				encounterCards.Clear();
				encounterCards.Add(encounterCard1);
				break;
			}
			foreach (EncounterCard encounterCard2 in encounterCards)
			{
				encounterCard2.Drawn = true;
			}
			enc.Slots.Clear();
			foreach (EncounterCard encounterCard3 in encounterCards)
			{
				EncounterSlot encounterSlot = null;
				foreach (EncounterSlot slot in enc.Slots)
				{
					if (slot.Card.CreatureID != encounterCard3.CreatureID)
					{
						continue;
					}
					encounterSlot = slot;
					break;
				}
				if (encounterSlot == null)
				{
					encounterSlot = new EncounterSlot()
					{
						Card = encounterCard3
					};
					enc.Slots.Add(encounterSlot);
				}
				int num3 = 1;
				switch (encounterCard3.Category)
				{
					case CardCategory.SoldierBrute:
					{
						num3 = 2;
						break;
					}
					case CardCategory.Minion:
					{
						num3 += 4;
						break;
					}
				}
				for (int i = 0; i != num3; i++)
				{
					CombatData combatDatum = new CombatData();
					encounterSlot.CombatData.Add(combatDatum);
				}
			}
			foreach (EncounterSlot slot1 in enc.Slots)
			{
				slot1.SetDefaultDisplayNames();
			}
			return true;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}