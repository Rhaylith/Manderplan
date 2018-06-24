using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class EncounterSlot
	{
		private Guid fID = Guid.NewGuid();

		private EncounterCard fCard = new EncounterCard();

		private EncounterSlotType fType;

		private List<Masterplan.Data.CombatData> fCombatData = new List<Masterplan.Data.CombatData>();

        public bool KnowledgeKnown
        {
            get;
            set;
        }

		public EncounterCard Card
		{
			get
			{
				return this.fCard;
			}
			set
			{
				this.fCard = value;
			}
		}

		public List<Masterplan.Data.CombatData> CombatData
		{
			get
			{
				return this.fCombatData;
			}
			set
			{
				this.fCombatData = value;
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

		public EncounterSlotType Type
		{
			get
			{
				return this.fType;
			}
			set
			{
				this.fType = value;
			}
		}

		public int XP
		{
			get
			{
				int num = 0;
				switch (this.fType)
				{
					case EncounterSlotType.Opponent:
					{
						num = 1;
						break;
					}
					case EncounterSlotType.Ally:
					{
						num = -1;
						break;
					}
					case EncounterSlotType.Neutral:
					{
						num = 0;
						break;
					}
				}
				return this.fCard.XP * this.fCombatData.Count * num;
			}
		}

		public EncounterSlot()
		{
		}

		public EncounterSlot Copy()
		{
			EncounterSlot encounterSlot = new EncounterSlot()
			{
				ID = this.fID,
				Card = this.fCard.Copy(),
				Type = this.fType
			};
			foreach (Masterplan.Data.CombatData fCombatDatum in this.fCombatData)
			{
				encounterSlot.CombatData.Add(fCombatDatum.Copy());
			}
			return encounterSlot;
		}

		public Masterplan.Data.CombatData FindCombatData(Point location)
		{
			Masterplan.Data.CombatData combatDatum;
			List<Masterplan.Data.CombatData>.Enumerator enumerator = this.fCombatData.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Masterplan.Data.CombatData current = enumerator.Current;
					if (current.Location != location)
					{
						continue;
					}
					combatDatum = current;
					return combatDatum;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		public CreatureState GetState(Masterplan.Data.CombatData data)
		{
			int hP = this.fCard.HP;
			int num = hP / 2;
			int damage = hP - data.Damage;
			if (damage <= 0)
			{
				return CreatureState.Defeated;
			}
			if (damage <= num)
			{
				return CreatureState.Bloodied;
			}
			return CreatureState.Active;
		}

		public void SetDefaultDisplayNames()
		{
			string title = this.fCard.Title;
			if (this.fCombatData == null)
			{
				this.fCombatData = new List<Masterplan.Data.CombatData>()
				{
					new Masterplan.Data.CombatData()
				};
			}
			foreach (Masterplan.Data.CombatData fCombatDatum in this.fCombatData)
			{
				if (this.fCombatData.Count != 1)
				{
					int num = this.fCombatData.IndexOf(fCombatDatum) + 1;
					fCombatDatum.DisplayName = string.Concat(title, " ", num);
				}
				else
				{
					fCombatDatum.DisplayName = title;
				}
			}
		}
	}
}