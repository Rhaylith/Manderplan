using Masterplan.Data;
using System;
using System.Collections.Generic;

namespace Masterplan
{
	internal class Sourcebook
	{
		private static List<ClassData> all_classes;

		private static List<RaceData> all_races;

		public static List<ClassData> Classes
		{
			get
			{
				if (Sourcebook.all_classes == null)
				{
					Sourcebook.all_classes = new List<ClassData>()
					{
						new ClassData("Cleric", PowerSource.Divine, PrimaryAbility.Wisdom, HeroRoleType.Leader),
						new ClassData("Cleric", PowerSource.Divine, PrimaryAbility.Strength, HeroRoleType.Leader),
						new ClassData("Fighter", PowerSource.Martial, PrimaryAbility.Strength, HeroRoleType.Defender),
						new ClassData("Paladin", PowerSource.Divine, PrimaryAbility.Strength, HeroRoleType.Defender),
						new ClassData("Paladin", PowerSource.Divine, PrimaryAbility.Charisma, HeroRoleType.Defender),
						new ClassData("Ranger", PowerSource.Martial, PrimaryAbility.Strength, HeroRoleType.Striker),
						new ClassData("Ranger", PowerSource.Martial, PrimaryAbility.Dexterity, HeroRoleType.Striker),
						new ClassData("Rogue", PowerSource.Martial, PrimaryAbility.Dexterity, HeroRoleType.Striker),
						new ClassData("Warlock", PowerSource.Arcane, PrimaryAbility.Charisma, HeroRoleType.Striker),
						new ClassData("Warlord", PowerSource.Martial, PrimaryAbility.Strength, HeroRoleType.Leader),
						new ClassData("Wizard", PowerSource.Arcane, PrimaryAbility.Intelligence, HeroRoleType.Controller),
						new ClassData("Avenger", PowerSource.Divine, PrimaryAbility.Wisdom, HeroRoleType.Striker),
						new ClassData("Barbarian", PowerSource.Primal, PrimaryAbility.Strength, HeroRoleType.Striker),
						new ClassData("Bard", PowerSource.Arcane, PrimaryAbility.Charisma, HeroRoleType.Leader),
						new ClassData("Druid", PowerSource.Primal, PrimaryAbility.Wisdom, HeroRoleType.Controller),
						new ClassData("Invoker", PowerSource.Divine, PrimaryAbility.Wisdom, HeroRoleType.Controller),
						new ClassData("Shaman", PowerSource.Primal, PrimaryAbility.Wisdom, HeroRoleType.Leader),
						new ClassData("Sorcerer", PowerSource.Arcane, PrimaryAbility.Charisma, HeroRoleType.Striker),
						new ClassData("Warden", PowerSource.Primal, PrimaryAbility.Strength, HeroRoleType.Defender),
						new ClassData("Ardent", PowerSource.Psionic, PrimaryAbility.Charisma, HeroRoleType.Leader),
						new ClassData("Battlemind", PowerSource.Psionic, PrimaryAbility.Constitution, HeroRoleType.Defender),
						new ClassData("Monk", PowerSource.Psionic, PrimaryAbility.Dexterity, HeroRoleType.Striker),
						new ClassData("Psion", PowerSource.Psionic, PrimaryAbility.Intelligence, HeroRoleType.Controller),
						new ClassData("Runepriest", PowerSource.Divine, PrimaryAbility.Strength, HeroRoleType.Leader),
						new ClassData("Seeker", PowerSource.Primal, PrimaryAbility.Wisdom, HeroRoleType.Controller),
						new ClassData("Artificer", PowerSource.Arcane, PrimaryAbility.Intelligence, HeroRoleType.Leader),
						new ClassData("Assassin", PowerSource.Shadow, PrimaryAbility.Dexterity, HeroRoleType.Striker),
						new ClassData("Swordmage", PowerSource.Arcane, PrimaryAbility.Intelligence, HeroRoleType.Defender),
						new ClassData("Vampire", PowerSource.Shadow, PrimaryAbility.Dexterity, HeroRoleType.Striker)
					};
				}
				return Sourcebook.all_classes;
			}
		}

		public static List<RaceData> Races
		{
			get
			{
				if (Sourcebook.all_races == null)
				{
					Sourcebook.all_races = new List<RaceData>();
					List<RaceData> allRaces = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray = new PrimaryAbility[] { PrimaryAbility.Charisma, PrimaryAbility.Strength };
					allRaces.Add(new RaceData("Dragonborn", primaryAbilityArray));
					List<RaceData> raceDatas = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray1 = new PrimaryAbility[] { PrimaryAbility.Constitution, PrimaryAbility.Wisdom };
					raceDatas.Add(new RaceData("Dwarf", primaryAbilityArray1));
					List<RaceData> allRaces1 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray2 = new PrimaryAbility[] { PrimaryAbility.Dexterity, PrimaryAbility.Intelligence };
					allRaces1.Add(new RaceData("Eladrin", primaryAbilityArray2));
					List<RaceData> raceDatas1 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray3 = new PrimaryAbility[] { PrimaryAbility.Dexterity, PrimaryAbility.Wisdom };
					raceDatas1.Add(new RaceData("Elf", primaryAbilityArray3));
					List<RaceData> allRaces2 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray4 = new PrimaryAbility[] { PrimaryAbility.Charisma, PrimaryAbility.Constitution };
					allRaces2.Add(new RaceData("Half-Elf", primaryAbilityArray4));
					List<RaceData> raceDatas2 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray5 = new PrimaryAbility[] { PrimaryAbility.Charisma, PrimaryAbility.Dexterity };
					raceDatas2.Add(new RaceData("Halfling", primaryAbilityArray5));
					List<RaceData> allRaces3 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray6 = new PrimaryAbility[] { PrimaryAbility.Strength, PrimaryAbility.Constitution, PrimaryAbility.Dexterity, PrimaryAbility.Intelligence, PrimaryAbility.Wisdom, PrimaryAbility.Charisma };
					allRaces3.Add(new RaceData("Human", primaryAbilityArray6));
					List<RaceData> raceDatas3 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray7 = new PrimaryAbility[] { PrimaryAbility.Charisma, PrimaryAbility.Intelligence };
					raceDatas3.Add(new RaceData("Tiefling", primaryAbilityArray7));
					List<RaceData> allRaces4 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray8 = new PrimaryAbility[] { PrimaryAbility.Intelligence, PrimaryAbility.Wisdom };
					allRaces4.Add(new RaceData("Deva", primaryAbilityArray8));
					List<RaceData> raceDatas4 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray9 = new PrimaryAbility[] { PrimaryAbility.Charisma, PrimaryAbility.Intelligence };
					raceDatas4.Add(new RaceData("Gnome", primaryAbilityArray9));
					List<RaceData> allRaces5 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray10 = new PrimaryAbility[] { PrimaryAbility.Constitution, PrimaryAbility.Strength };
					allRaces5.Add(new RaceData("Goliath", primaryAbilityArray10));
					List<RaceData> raceDatas5 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray11 = new PrimaryAbility[] { PrimaryAbility.Dexterity, PrimaryAbility.Strength };
					raceDatas5.Add(new RaceData("Half-Orc", primaryAbilityArray11));
					List<RaceData> allRaces6 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray12 = new PrimaryAbility[] { PrimaryAbility.Strength, PrimaryAbility.Wisdom };
					allRaces6.Add(new RaceData("Longtooth Shifter", primaryAbilityArray12));
					List<RaceData> raceDatas6 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray13 = new PrimaryAbility[] { PrimaryAbility.Dexterity, PrimaryAbility.Wisdom };
					raceDatas6.Add(new RaceData("Razorclaw Shifter", primaryAbilityArray13));
					List<RaceData> allRaces7 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray14 = new PrimaryAbility[] { PrimaryAbility.Wisdom, PrimaryAbility.Dexterity, PrimaryAbility.Intelligence };
					allRaces7.Add(new RaceData("Githzerai", primaryAbilityArray14));
					List<RaceData> raceDatas7 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray15 = new PrimaryAbility[] { PrimaryAbility.Strength, PrimaryAbility.Constitution, PrimaryAbility.Wisdom };
					raceDatas7.Add(new RaceData("Minotaur", primaryAbilityArray15));
					List<RaceData> allRaces8 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray16 = new PrimaryAbility[] { PrimaryAbility.Intelligence, PrimaryAbility.Charisma, PrimaryAbility.Wisdom };
					allRaces8.Add(new RaceData("Shardmind", primaryAbilityArray16));
					List<RaceData> raceDatas8 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray17 = new PrimaryAbility[] { PrimaryAbility.Wisdom, PrimaryAbility.Constitution, PrimaryAbility.Dexterity };
					raceDatas8.Add(new RaceData("Wilden", primaryAbilityArray17));
					List<RaceData> allRaces9 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray18 = new PrimaryAbility[] { PrimaryAbility.Charisma, PrimaryAbility.Dexterity };
					allRaces9.Add(new RaceData("Drow", primaryAbilityArray18));
					List<RaceData> raceDatas9 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray19 = new PrimaryAbility[] { PrimaryAbility.Intelligence, PrimaryAbility.Strength };
					raceDatas9.Add(new RaceData("Genasi", primaryAbilityArray19));
					List<RaceData> allRaces10 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray20 = new PrimaryAbility[] { PrimaryAbility.Charisma, PrimaryAbility.Dexterity, PrimaryAbility.Intelligence };
					allRaces10.Add(new RaceData("Changeling", primaryAbilityArray20));
					List<RaceData> raceDatas10 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray21 = new PrimaryAbility[] { PrimaryAbility.Charisma, PrimaryAbility.Wisdom };
					raceDatas10.Add(new RaceData("Kalashtar", primaryAbilityArray21));
					List<RaceData> allRaces11 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray22 = new PrimaryAbility[] { PrimaryAbility.Constitution, PrimaryAbility.Strength };
					allRaces11.Add(new RaceData("Warforged", primaryAbilityArray22));
					List<RaceData> raceDatas11 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray23 = new PrimaryAbility[] { PrimaryAbility.Constitution, PrimaryAbility.Dexterity };
					raceDatas11.Add(new RaceData("Revenant", primaryAbilityArray23));
					List<RaceData> allRaces12 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray24 = new PrimaryAbility[] { PrimaryAbility.Dexterity, PrimaryAbility.Intelligence };
					allRaces12.Add(new RaceData("Shadar-kai", primaryAbilityArray24));
					List<RaceData> raceDatas12 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray25 = new PrimaryAbility[] { PrimaryAbility.Dexterity, PrimaryAbility.Charisma };
					raceDatas12.Add(new RaceData("Shade", primaryAbilityArray25));
					List<RaceData> allRaces13 = Sourcebook.all_races;
					PrimaryAbility[] primaryAbilityArray26 = new PrimaryAbility[] { PrimaryAbility.Dexterity, PrimaryAbility.Charisma };
					allRaces13.Add(new RaceData("Vryloka", primaryAbilityArray26));
				}
				return Sourcebook.all_races;
			}
		}

		static Sourcebook()
		{
		}

		public Sourcebook()
		{
		}

		public static List<ClassData> Filter(List<PowerSource> power_sources, List<PrimaryAbility> abilities, List<HeroRoleType> roles)
		{
			List<ClassData> classDatas = new List<ClassData>();
			foreach (ClassData @class in Sourcebook.Classes)
			{
				if (power_sources.Count != 0 && !power_sources.Contains(@class.PowerSource) || abilities.Count != 0 && !abilities.Contains(@class.KeyAbility) || roles.Count != 0 && !roles.Contains(@class.Role))
				{
					continue;
				}
				classDatas.Add(@class);
			}
			return classDatas;
		}

		public static List<RaceData> Filter(PrimaryAbility ability)
		{
			List<RaceData> raceDatas = new List<RaceData>();
			foreach (RaceData race in Sourcebook.Races)
			{
				if (!race.Abilities.Contains(ability))
				{
					continue;
				}
				raceDatas.Add(race);
			}
			return raceDatas;
		}

		public static ClassData GetClass(string name)
		{
			ClassData classDatum;
			List<ClassData>.Enumerator enumerator = Sourcebook.Classes.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					ClassData current = enumerator.Current;
					if (current.Name != name)
					{
						continue;
					}
					classDatum = current;
					return classDatum;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return classDatum;
		}

		public static RaceData GetRace(string name)
		{
			RaceData raceDatum;
			List<RaceData>.Enumerator enumerator = Sourcebook.Races.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					RaceData current = enumerator.Current;
					if (current.Name != name)
					{
						continue;
					}
					raceDatum = current;
					return raceDatum;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return raceDatum;
		}
	}
}