using Masterplan;
using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class EncounterTemplateSlot
	{
		private List<RoleType> fRoles = new List<RoleType>();

		private RoleFlag fFlag;

		private int fLevelAdjustment;

		private int fCount = 1;

		private bool fMinions;

		public int Count
		{
			get
			{
				return this.fCount;
			}
			set
			{
				this.fCount = value;
			}
		}

		public RoleFlag Flag
		{
			get
			{
				return this.fFlag;
			}
			set
			{
				this.fFlag = value;
			}
		}

		public int LevelAdjustment
		{
			get
			{
				return this.fLevelAdjustment;
			}
			set
			{
				this.fLevelAdjustment = value;
			}
		}

		public bool Minions
		{
			get
			{
				return this.fMinions;
			}
			set
			{
				this.fMinions = value;
			}
		}

		public List<RoleType> Roles
		{
			get
			{
				return this.fRoles;
			}
			set
			{
				this.fRoles = value;
			}
		}

		public EncounterTemplateSlot()
		{
		}

		public EncounterTemplateSlot(int count, int level_adj, RoleFlag flag, RoleType role)
		{
			this.fCount = count;
			this.fLevelAdjustment = level_adj;
			this.fFlag = flag;
			this.fRoles.Add(role);
		}

		public EncounterTemplateSlot(int count, int level_adj, RoleFlag flag, RoleType[] roles)
		{
			this.fCount = count;
			this.fLevelAdjustment = level_adj;
			this.fFlag = flag;
			this.fRoles.AddRange(roles);
		}

		public EncounterTemplateSlot(int count, int level_adj, RoleFlag flag)
		{
			this.fCount = count;
			this.fLevelAdjustment = level_adj;
			this.fFlag = flag;
		}

		public EncounterTemplateSlot(int count, int level_adj, RoleType role)
		{
			this.fCount = count;
			this.fLevelAdjustment = level_adj;
			this.fRoles.Add(role);
		}

		public EncounterTemplateSlot(int count, int level_adj, RoleType[] roles)
		{
			this.fCount = count;
			this.fLevelAdjustment = level_adj;
			this.fRoles.AddRange(roles);
		}

		public EncounterTemplateSlot(int count, int level_adj)
		{
			this.fCount = count;
			this.fLevelAdjustment = level_adj;
		}

		public EncounterTemplateSlot(int count, int level_adj, bool minions)
		{
			this.fCount = count;
			this.fLevelAdjustment = level_adj;
			this.fMinions = minions;
		}

		public EncounterTemplateSlot Copy()
		{
			EncounterTemplateSlot encounterTemplateSlot = new EncounterTemplateSlot();
			encounterTemplateSlot.Roles.AddRange(this.fRoles);
			encounterTemplateSlot.Flag = this.fFlag;
			encounterTemplateSlot.LevelAdjustment = this.fLevelAdjustment;
			encounterTemplateSlot.Count = this.fCount;
			encounterTemplateSlot.Minions = this.fMinions;
			return encounterTemplateSlot;
		}

		public bool Match(EncounterCard card, int encounter_level)
		{
			ICreature creature = Session.FindCreature(card.CreatureID, SearchType.Global);
			int encounterLevel = encounter_level + this.fLevelAdjustment;
			if (encounterLevel < 1)
			{
				encounterLevel = 1;
			}
			if (creature.Level != encounterLevel)
			{
				return false;
			}
			if (creature.Role is Minion != this.fMinions)
			{
				return false;
			}
			bool flag = false;
			if (this.fRoles.Count != 0)
			{
				ComplexRole role = creature.Role as ComplexRole;
				foreach (RoleType roleType in card.Roles)
				{
					if (!this.fRoles.Contains(role.Type))
					{
						continue;
					}
					flag = true;
					break;
				}
			}
			else
			{
				flag = true;
			}
			if (!flag)
			{
				return false;
			}
			if (this.fFlag != card.Flag)
			{
				return false;
			}
			return true;
		}
	}
}