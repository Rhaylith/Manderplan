using Masterplan.Data;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Masterplan
{
	internal class HeroGroup
	{
		public List<HeroData> Heroes = new List<HeroData>();

		public List<PrimaryAbility> RequiredAbilities
		{
			get
			{
				Array values = Enum.GetValues(typeof(PrimaryAbility));
				int num = 2147483647;
				foreach (PrimaryAbility value in values)
				{
					int num1 = this.Count(value);
					if (num1 >= num)
					{
						continue;
					}
					num = num1;
				}
				List<PrimaryAbility> primaryAbilities = new List<PrimaryAbility>();
				foreach (PrimaryAbility primaryAbility in values)
				{
					if (this.Count(primaryAbility) != num)
					{
						continue;
					}
					primaryAbilities.Add(primaryAbility);
				}
				return primaryAbilities;
			}
		}

		public List<PowerSource> RequiredPowerSources
		{
			get
			{
				Array values = Enum.GetValues(typeof(PowerSource));
				int num = 2147483647;
				foreach (PowerSource value in values)
				{
					int num1 = this.Count(value);
					if (num1 >= num)
					{
						continue;
					}
					num = num1;
				}
				List<PowerSource> powerSources = new List<PowerSource>();
				foreach (PowerSource powerSource in values)
				{
					if (this.Count(powerSource) != num)
					{
						continue;
					}
					powerSources.Add(powerSource);
				}
				return powerSources;
			}
		}

		public List<HeroRoleType> RequiredRoles
		{
			get
			{
				Array values = Enum.GetValues(typeof(HeroRoleType));
				int num = 2147483647;
				foreach (HeroRoleType value in values)
				{
					int num1 = this.Count(value);
					if (num1 >= num)
					{
						continue;
					}
					num = num1;
				}
				List<HeroRoleType> heroRoleTypes = new List<HeroRoleType>();
				foreach (HeroRoleType heroRoleType in values)
				{
					if (heroRoleType == HeroRoleType.Hybrid || this.Count(heroRoleType) != num)
					{
						continue;
					}
					heroRoleTypes.Add(heroRoleType);
				}
				return heroRoleTypes;
			}
		}

		public HeroGroup()
		{
		}

		public bool Contains(ClassData cd)
		{
			bool flag;
			List<HeroData>.Enumerator enumerator = this.Heroes.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Class != cd)
					{
						continue;
					}
					flag = true;
					return flag;
				}
				return false;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return flag;
		}

		public bool Contains(RaceData rd)
		{
			bool flag;
			List<HeroData>.Enumerator enumerator = this.Heroes.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Race != rd)
					{
						continue;
					}
					flag = true;
					return flag;
				}
				return false;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return flag;
		}

		public int Count(PowerSource power_source)
		{
			int num = 0;
			foreach (HeroData hero in this.Heroes)
			{
				if (hero.Class == null || hero.Class.PowerSource != power_source)
				{
					continue;
				}
				num++;
			}
			return num;
		}

		public int Count(PrimaryAbility key_ability)
		{
			int num = 0;
			foreach (HeroData hero in this.Heroes)
			{
				if (hero.Class == null || hero.Class.KeyAbility != key_ability)
				{
					continue;
				}
				num++;
			}
			return num;
		}

		public int Count(HeroRoleType role)
		{
			int num = 0;
			foreach (HeroData hero in this.Heroes)
			{
				if (hero.Class == null || hero.Class.Role != role)
				{
					continue;
				}
				num++;
			}
			return num;
		}

		public static HeroGroup CreateGroup(int size)
		{
			HeroGroup heroGroup = new HeroGroup();
			int num = 0;
			while (heroGroup.Heroes.Count != size)
			{
				HeroData heroDatum = heroGroup.Suggest();
				if (heroDatum == null)
				{
					num++;
				}
				else
				{
					heroGroup.Heroes.Add(heroDatum);
				}
				if (num < 100)
				{
					continue;
				}
				return HeroGroup.CreateGroup(size - 1);
			}
			return heroGroup;
		}

		public HeroData Suggest()
		{
			List<PowerSource> requiredPowerSources = this.RequiredPowerSources;
			List<PrimaryAbility> requiredAbilities = this.RequiredAbilities;
			List<HeroRoleType> requiredRoles = this.RequiredRoles;
			List<ClassData> classDatas = Sourcebook.Filter(requiredPowerSources, requiredAbilities, requiredRoles);
			if (classDatas.Count == 0)
			{
				classDatas = Sourcebook.Filter(requiredPowerSources, new List<PrimaryAbility>(), requiredRoles);
				if (classDatas.Count == 0)
				{
					return null;
				}
			}
			List<ClassData> classDatas1 = new List<ClassData>();
			foreach (ClassData classDatum in classDatas)
			{
				if (!this.Contains(classDatum))
				{
					continue;
				}
				classDatas1.Add(classDatum);
			}
			if (classDatas1.Count != classDatas.Count)
			{
				foreach (ClassData classData in classDatas1)
				{
					classDatas.Remove(classData);
				}
			}
			int num = Session.Random.Next() % classDatas.Count;
			ClassData item = classDatas[num];
			List<RaceData> raceDatas = Sourcebook.Filter(item.KeyAbility);
			if (raceDatas.Count == 0)
			{
				return null;
			}
			List<RaceData> raceDatas1 = new List<RaceData>();
			foreach (RaceData raceDatum in raceDatas)
			{
				if (!this.Contains(raceDatum))
				{
					continue;
				}
				raceDatas1.Add(raceDatum);
			}
			if (raceDatas1.Count != raceDatas.Count)
			{
				foreach (RaceData raceData in raceDatas1)
				{
					raceDatas.Remove(raceData);
				}
			}
			int num1 = Session.Random.Next() % raceDatas.Count;
			return new HeroData(raceDatas[num1], item);
		}
	}
}