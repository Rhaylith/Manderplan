using System;
using System.Collections.Generic;
using Utils;

namespace Masterplan.Data
{
	[Serializable]
	public class MonsterTheme
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private List<Pair<string, int>> fSkillBonuses = new List<Pair<string, int>>();

		private List<ThemePowerData> fPowers = new List<ThemePowerData>();

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

		public List<ThemePowerData> Powers
		{
			get
			{
				return this.fPowers;
			}
			set
			{
				this.fPowers = value;
			}
		}

		public List<Pair<string, int>> SkillBonuses
		{
			get
			{
				return this.fSkillBonuses;
			}
			set
			{
				this.fSkillBonuses = value;
			}
		}

		public MonsterTheme()
		{
		}

		public MonsterTheme Copy()
		{
			MonsterTheme monsterTheme = new MonsterTheme()
			{
				ID = this.fID,
				Name = this.fName
			};
			foreach (Pair<string, int> fSkillBonuse in this.fSkillBonuses)
			{
				monsterTheme.SkillBonuses.Add(new Pair<string, int>(fSkillBonuse.First, fSkillBonuse.Second));
			}
			foreach (ThemePowerData fPower in this.fPowers)
			{
				monsterTheme.Powers.Add(fPower.Copy());
			}
			return monsterTheme;
		}

		public ThemePowerData FindPower(Guid power_id)
		{
			ThemePowerData themePowerDatum;
			List<ThemePowerData>.Enumerator enumerator = this.fPowers.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					ThemePowerData current = enumerator.Current;
					if (current.Power.ID != power_id)
					{
						continue;
					}
					themePowerDatum = current;
					return themePowerDatum;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return themePowerDatum;
		}

		public List<ThemePowerData> ListPowers(List<RoleType> creature_roles, PowerType type)
		{
			List<ThemePowerData> themePowerDatas = new List<ThemePowerData>();
			foreach (ThemePowerData fPower in this.fPowers)
			{
				if (fPower.Type != type)
				{
					continue;
				}
				if (fPower.Roles.Count != 0)
				{
					bool flag = false;
					foreach (RoleType creatureRole in creature_roles)
					{
						if (!fPower.Roles.Contains(creatureRole))
						{
							continue;
						}
						flag = true;
					}
					if (!flag)
					{
						continue;
					}
					themePowerDatas.Add(fPower);
				}
				else
				{
					themePowerDatas.Add(fPower);
				}
			}
			return themePowerDatas;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}