using Masterplan;
using Masterplan.Data;
using System;
using System.Collections.Generic;
using Utils;

namespace Masterplan.Tools.Generators
{
	internal class EncounterBuilder
	{
		private const int TRIES = 100;

		private static List<EncounterTemplateGroup> fTemplateGroups;

		private static List<EncounterCard> fCreatures;

		private static List<Trap> fTraps;

		private static List<SkillChallenge> fChallenges;

		static EncounterBuilder()
		{
			EncounterBuilder.fTemplateGroups = new List<EncounterTemplateGroup>();
			EncounterBuilder.fCreatures = new List<EncounterCard>();
			EncounterBuilder.fTraps = new List<Trap>();
			EncounterBuilder.fChallenges = new List<SkillChallenge>();
		}

		public EncounterBuilder()
		{
		}

		private static bool add_challenge(Encounter enc)
		{
			if (EncounterBuilder.fChallenges.Count == 0)
			{
				return false;
			}
			int num = Session.Random.Next() % EncounterBuilder.fChallenges.Count;
			SkillChallenge item = EncounterBuilder.fChallenges[num];
			enc.SkillChallenges.Add(item.Copy() as SkillChallenge);
			return true;
		}

		private static bool add_lurker(Encounter enc)
		{
			List<EncounterCard> encounterCards = new List<EncounterCard>();
			foreach (EncounterCard fCreature in EncounterBuilder.fCreatures)
			{
				if (fCreature.Flag != RoleFlag.Standard || !fCreature.Roles.Contains(RoleType.Lurker))
				{
					continue;
				}
				encounterCards.Add(fCreature);
			}
			if (encounterCards.Count == 0)
			{
				return false;
			}
			int num = Session.Random.Next() % encounterCards.Count;
			EncounterSlot encounterSlot = new EncounterSlot()
			{
				Card = encounterCards[num]
			};
			encounterSlot.CombatData.Add(new CombatData());
			enc.Slots.Add(encounterSlot);
			return true;
		}

		private static void add_templates(ICreature creature)
		{
			if (creature.Role is Minion)
			{
				return;
			}
			if ((creature.Role as ComplexRole).Flag == RoleFlag.Solo)
			{
				return;
			}
			foreach (Library library in Session.Libraries)
			{
				foreach (CreatureTemplate template in library.Templates)
				{
					EncounterCard encounterCard = new EncounterCard()
					{
						CreatureID = creature.ID
					};
					encounterCard.TemplateIDs.Add(template.ID);
				}
			}
		}

		private static bool add_trap(Encounter enc)
		{
			if (EncounterBuilder.fTraps.Count == 0)
			{
				return false;
			}
			int num = Session.Random.Next() % EncounterBuilder.fTraps.Count;
			Trap item = EncounterBuilder.fTraps[num];
			enc.Traps.Add(item.Copy());
			return true;
		}

		public static bool Build(AutoBuildData data, Encounter enc, bool include_individual)
		{
			int num = Math.Max(data.Level - 4, 1);
			int level = data.Level + 5;
			EncounterBuilder.build_creature_list(num, level, data.Categories, data.Keywords, true);
			if (EncounterBuilder.fCreatures.Count == 0)
			{
				return false;
			}
			EncounterBuilder.build_template_list(data.Type, data.Difficulty, data.Level, include_individual);
			if (EncounterBuilder.fTemplateGroups.Count == 0)
			{
				return false;
			}
			EncounterBuilder.build_trap_list(data.Level);
			EncounterBuilder.build_challenge_list(data.Level);
			int num1 = 0;
			while (num1 < 100)
			{
				num1++;
				int num2 = Session.Random.Next() % EncounterBuilder.fTemplateGroups.Count;
				EncounterTemplateGroup item = EncounterBuilder.fTemplateGroups[num2];
				int num3 = Session.Random.Next() % item.Templates.Count;
				EncounterTemplate encounterTemplate = item.Templates[num3];
				bool flag = true;
				List<EncounterSlot> encounterSlots = new List<EncounterSlot>();
				foreach (EncounterTemplateSlot slot in encounterTemplate.Slots)
				{
					List<EncounterCard> encounterCards = new List<EncounterCard>();
					foreach (EncounterCard fCreature in EncounterBuilder.fCreatures)
					{
						if (!slot.Match(fCreature, data.Level))
						{
							continue;
						}
						encounterCards.Add(fCreature);
					}
					if (encounterCards.Count != 0)
					{
						int num4 = Session.Random.Next() % encounterCards.Count;
						EncounterSlot encounterSlot = new EncounterSlot()
						{
							Card = encounterCards[num4]
						};
						for (int i = 0; i != slot.Count; i++)
						{
							CombatData combatDatum = new CombatData();
							encounterSlot.CombatData.Add(combatDatum);
						}
						encounterSlots.Add(encounterSlot);
					}
					else
					{
						flag = false;
						break;
					}
				}
				if (!flag)
				{
					continue;
				}
				enc.Slots = encounterSlots;
				enc.Traps.Clear();
				enc.SkillChallenges.Clear();
				switch (Session.Random.Next(12))
				{
					case 4:
					case 5:
					{
						if (!EncounterBuilder.add_trap(enc))
						{
							break;
						}
						EncounterBuilder.remove_creature(enc);
						break;
					}
					case 6:
					{
						if (!EncounterBuilder.add_challenge(enc))
						{
							break;
						}
						EncounterBuilder.remove_creature(enc);
						break;
					}
					case 7:
					{
						if (!EncounterBuilder.add_lurker(enc))
						{
							break;
						}
						EncounterBuilder.remove_creature(enc);
						break;
					}
					case 8:
					case 9:
					{
						EncounterBuilder.add_trap(enc);
						Difficulty difficulty = enc.GetDifficulty(data.Level, data.Size);
						if (difficulty != Difficulty.Hard && difficulty != Difficulty.Extreme)
						{
							break;
						}
						EncounterBuilder.remove_creature(enc);
						break;
					}
					case 10:
					{
						Difficulty difficulty1 = enc.GetDifficulty(data.Level, data.Size);
						if (difficulty1 == Difficulty.Hard || difficulty1 == Difficulty.Extreme)
						{
							EncounterBuilder.remove_creature(enc);
						}
						EncounterBuilder.add_challenge(enc);
						break;
					}
					case 11:
					{
						EncounterBuilder.add_lurker(enc);
						Difficulty difficulty2 = enc.GetDifficulty(data.Level, data.Size);
						if (difficulty2 != Difficulty.Hard && difficulty2 != Difficulty.Extreme)
						{
							break;
						}
						EncounterBuilder.remove_creature(enc);
						break;
					}
				}
				while (enc.GetDifficulty(data.Level, data.Size) == Difficulty.Extreme && enc.Count > 1)
				{
					EncounterBuilder.remove_creature(enc);
				}
				foreach (EncounterSlot slot1 in enc.Slots)
				{
					slot1.SetDefaultDisplayNames();
				}
				return true;
			}
			return false;
		}

		private static void build_challenge_list(int level)
		{
			int num = level - 3;
			int num1 = level + 5;
			EncounterBuilder.fChallenges.Clear();
			foreach (SkillChallenge skillChallenge in Session.SkillChallenges)
			{
				if (skillChallenge.Level < num || skillChallenge.Level > num1)
				{
					continue;
				}
				EncounterBuilder.fChallenges.Add(skillChallenge);
			}
		}

		private static void build_creature_list(int min_level, int max_level, List<string> categories, List<string> keywords, bool include_templates)
		{
			EncounterBuilder.fCreatures.Clear();
			foreach (Creature creature in Session.Creatures)
			{
				if (creature == null || min_level != -1 && creature.Level < min_level || max_level != -1 && creature.Level > max_level || categories != null && categories.Count != 0 && !categories.Contains(creature.Category))
				{
					continue;
				}
				if (keywords != null && keywords.Count != 0)
				{
					bool flag = false;
					foreach (string keyword in keywords)
					{
						if (!creature.Phenotype.ToLower().Contains(keyword.ToLower()))
						{
							continue;
						}
						flag = true;
						break;
					}
					if (!flag)
					{
						continue;
					}
				}
				EncounterCard encounterCard = new EncounterCard()
				{
					CreatureID = creature.ID
				};
				EncounterBuilder.fCreatures.Add(encounterCard);
				if (!include_templates)
				{
					continue;
				}
				EncounterBuilder.add_templates(creature);
			}
			foreach (CustomCreature customCreature in Session.Project.CustomCreatures)
			{
				EncounterCard encounterCard1 = new EncounterCard()
				{
					CreatureID = customCreature.ID
				};
				EncounterBuilder.fCreatures.Add(encounterCard1);
				if (!include_templates)
				{
					continue;
				}
				EncounterBuilder.add_templates(customCreature);
			}
		}

		private static void build_template_battlefield_control()
		{
			EncounterTemplateGroup encounterTemplateGroup = new EncounterTemplateGroup("Battlefield Control", "Entire Party");
			EncounterTemplate encounterTemplate = new EncounterTemplate(Difficulty.Easy);
			encounterTemplate.Slots.Add(new EncounterTemplateSlot(1, -2, RoleType.Controller));
			encounterTemplate.Slots.Add(new EncounterTemplateSlot(6, -4, RoleType.Skirmisher));
			encounterTemplateGroup.Templates.Add(encounterTemplate);
			EncounterTemplate encounterTemplate1 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate1.Slots.Add(new EncounterTemplateSlot(1, 1, RoleType.Controller));
			encounterTemplate1.Slots.Add(new EncounterTemplateSlot(6, -2, RoleType.Skirmisher));
			encounterTemplateGroup.Templates.Add(encounterTemplate1);
			EncounterTemplate encounterTemplate2 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate2.Slots.Add(new EncounterTemplateSlot(1, 5, RoleType.Controller));
			encounterTemplate2.Slots.Add(new EncounterTemplateSlot(5, 1, RoleType.Skirmisher));
			encounterTemplateGroup.Templates.Add(encounterTemplate2);
			EncounterBuilder.fTemplateGroups.Add(encounterTemplateGroup);
		}

		private static void build_template_commander_and_troops()
		{
			EncounterTemplateGroup encounterTemplateGroup = new EncounterTemplateGroup("Commander and Troops", "Entire Party");
			EncounterTemplate encounterTemplate = new EncounterTemplate(Difficulty.Easy);
			List<EncounterTemplateSlot> slots = encounterTemplate.Slots;
			RoleType[] roleTypeArray = new RoleType[] { RoleType.Controller, RoleType.Soldier, RoleType.Lurker, RoleType.Skirmisher };
			slots.Add(new EncounterTemplateSlot(1, 0, roleTypeArray));
			List<EncounterTemplateSlot> encounterTemplateSlots = encounterTemplate.Slots;
			RoleType[] roleTypeArray1 = new RoleType[] { RoleType.Brute, RoleType.Soldier };
			encounterTemplateSlots.Add(new EncounterTemplateSlot(4, -3, roleTypeArray1));
			encounterTemplateGroup.Templates.Add(encounterTemplate);
			EncounterTemplate encounterTemplate1 = new EncounterTemplate(Difficulty.Moderate);
			List<EncounterTemplateSlot> slots1 = encounterTemplate1.Slots;
			RoleType[] roleTypeArray2 = new RoleType[] { RoleType.Controller, RoleType.Soldier, RoleType.Lurker, RoleType.Skirmisher };
			slots1.Add(new EncounterTemplateSlot(1, 3, roleTypeArray2));
			List<EncounterTemplateSlot> encounterTemplateSlots1 = encounterTemplate1.Slots;
			RoleType[] roleTypeArray3 = new RoleType[] { RoleType.Brute, RoleType.Soldier };
			encounterTemplateSlots1.Add(new EncounterTemplateSlot(5, -2, roleTypeArray3));
			encounterTemplateGroup.Templates.Add(encounterTemplate1);
			EncounterTemplate encounterTemplate2 = new EncounterTemplate(Difficulty.Hard);
			List<EncounterTemplateSlot> slots2 = encounterTemplate2.Slots;
			RoleType[] roleTypeArray4 = new RoleType[] { RoleType.Controller, RoleType.Soldier, RoleType.Lurker, RoleType.Skirmisher };
			slots2.Add(new EncounterTemplateSlot(1, 5, roleTypeArray4));
			List<EncounterTemplateSlot> encounterTemplateSlots2 = encounterTemplate2.Slots;
			RoleType[] roleTypeArray5 = new RoleType[] { RoleType.Brute, RoleType.Soldier };
			encounterTemplateSlots2.Add(new EncounterTemplateSlot(3, 1, roleTypeArray5));
			encounterTemplate2.Slots.Add(new EncounterTemplateSlot(2, 1, new RoleType[1]));
			encounterTemplateGroup.Templates.Add(encounterTemplate2);
			EncounterBuilder.fTemplateGroups.Add(encounterTemplateGroup);
		}

		private static void build_template_double_line()
		{
			EncounterTemplateGroup encounterTemplateGroup = new EncounterTemplateGroup("Double Line", "Entire Party");
			EncounterTemplate encounterTemplate = new EncounterTemplate(Difficulty.Easy);
			List<EncounterTemplateSlot> slots = encounterTemplate.Slots;
			RoleType[] roleTypeArray = new RoleType[] { RoleType.Brute, RoleType.Soldier };
			slots.Add(new EncounterTemplateSlot(3, -4, roleTypeArray));
			List<EncounterTemplateSlot> encounterTemplateSlots = encounterTemplate.Slots;
			RoleType[] roleTypeArray1 = new RoleType[] { RoleType.Artillery, RoleType.Controller };
			encounterTemplateSlots.Add(new EncounterTemplateSlot(2, -2, roleTypeArray1));
			encounterTemplateGroup.Templates.Add(encounterTemplate);
			EncounterTemplate encounterTemplate1 = new EncounterTemplate(Difficulty.Moderate);
			List<EncounterTemplateSlot> slots1 = encounterTemplate1.Slots;
			RoleType[] roleTypeArray2 = new RoleType[] { RoleType.Brute, RoleType.Soldier };
			slots1.Add(new EncounterTemplateSlot(3, 0, roleTypeArray2));
			List<EncounterTemplateSlot> encounterTemplateSlots1 = encounterTemplate1.Slots;
			RoleType[] roleTypeArray3 = new RoleType[] { RoleType.Artillery, RoleType.Controller };
			encounterTemplateSlots1.Add(new EncounterTemplateSlot(2, 0, roleTypeArray3));
			encounterTemplateGroup.Templates.Add(encounterTemplate1);
			EncounterTemplate encounterTemplate2 = new EncounterTemplate(Difficulty.Moderate);
			List<EncounterTemplateSlot> slots2 = encounterTemplate2.Slots;
			RoleType[] roleTypeArray4 = new RoleType[] { RoleType.Brute, RoleType.Soldier };
			slots2.Add(new EncounterTemplateSlot(3, -2, roleTypeArray4));
			List<EncounterTemplateSlot> encounterTemplateSlots2 = encounterTemplate2.Slots;
			RoleType[] roleTypeArray5 = new RoleType[] { RoleType.Artillery, RoleType.Controller };
			encounterTemplateSlots2.Add(new EncounterTemplateSlot(2, 3, roleTypeArray5));
			encounterTemplateGroup.Templates.Add(encounterTemplate2);
			EncounterTemplate encounterTemplate3 = new EncounterTemplate(Difficulty.Hard);
			List<EncounterTemplateSlot> slots3 = encounterTemplate3.Slots;
			RoleType[] roleTypeArray6 = new RoleType[] { RoleType.Brute, RoleType.Soldier };
			slots3.Add(new EncounterTemplateSlot(3, 2, roleTypeArray6));
			encounterTemplate3.Slots.Add(new EncounterTemplateSlot(1, 4, RoleType.Controller));
			List<EncounterTemplateSlot> encounterTemplateSlots3 = encounterTemplate3.Slots;
			RoleType[] roleTypeArray7 = new RoleType[] { RoleType.Artillery, RoleType.Lurker };
			encounterTemplateSlots3.Add(new EncounterTemplateSlot(1, 4, roleTypeArray7));
			encounterTemplateGroup.Templates.Add(encounterTemplate3);
			EncounterTemplate encounterTemplate4 = new EncounterTemplate(Difficulty.Hard);
			List<EncounterTemplateSlot> slots4 = encounterTemplate4.Slots;
			RoleType[] roleTypeArray8 = new RoleType[] { RoleType.Brute, RoleType.Soldier };
			slots4.Add(new EncounterTemplateSlot(3, 0, roleTypeArray8));
			encounterTemplate4.Slots.Add(new EncounterTemplateSlot(2, 1, RoleType.Artillery));
			encounterTemplate4.Slots.Add(new EncounterTemplateSlot(1, 2, RoleType.Controller));
			encounterTemplate4.Slots.Add(new EncounterTemplateSlot(1, 2, RoleType.Lurker));
			encounterTemplateGroup.Templates.Add(encounterTemplate4);
			EncounterBuilder.fTemplateGroups.Add(encounterTemplateGroup);
		}

		private static void build_template_dragons_den()
		{
			EncounterTemplateGroup encounterTemplateGroup = new EncounterTemplateGroup("Dragon's Den", "Entire Party");
			EncounterTemplate encounterTemplate = new EncounterTemplate(Difficulty.Easy);
			encounterTemplate.Slots.Add(new EncounterTemplateSlot(1, -2, RoleFlag.Solo));
			encounterTemplateGroup.Templates.Add(encounterTemplate);
			EncounterTemplate encounterTemplate1 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate1.Slots.Add(new EncounterTemplateSlot(1, 0, RoleFlag.Solo));
			encounterTemplateGroup.Templates.Add(encounterTemplate1);
			EncounterTemplate encounterTemplate2 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate2.Slots.Add(new EncounterTemplateSlot(1, 1, RoleFlag.Solo));
			encounterTemplateGroup.Templates.Add(encounterTemplate2);
			EncounterTemplate encounterTemplate3 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate3.Slots.Add(new EncounterTemplateSlot(1, 3, RoleFlag.Solo));
			encounterTemplateGroup.Templates.Add(encounterTemplate3);
			EncounterTemplate encounterTemplate4 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate4.Slots.Add(new EncounterTemplateSlot(1, 1, RoleFlag.Solo));
			encounterTemplate4.Slots.Add(new EncounterTemplateSlot(1, 0, RoleFlag.Elite));
			encounterTemplateGroup.Templates.Add(encounterTemplate4);
			EncounterBuilder.fTemplateGroups.Add(encounterTemplateGroup);
		}

		private static void build_template_duel()
		{
			EncounterTemplateGroup encounterTemplateGroup = new EncounterTemplateGroup("Duel vs Controller", "Individual PC");
			EncounterTemplate encounterTemplate = new EncounterTemplate(Difficulty.Easy);
			encounterTemplate.Slots.Add(new EncounterTemplateSlot(1, 0, RoleType.Artillery));
			encounterTemplateGroup.Templates.Add(encounterTemplate);
			EncounterTemplate encounterTemplate1 = new EncounterTemplate(Difficulty.Easy);
			List<EncounterTemplateSlot> slots = encounterTemplate1.Slots;
			RoleType[] roleTypeArray = new RoleType[] { RoleType.Controller, RoleType.Skirmisher };
			slots.Add(new EncounterTemplateSlot(1, -1, roleTypeArray));
			encounterTemplateGroup.Templates.Add(encounterTemplate1);
			EncounterTemplate encounterTemplate2 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate2.Slots.Add(new EncounterTemplateSlot(1, 2, RoleType.Artillery));
			encounterTemplateGroup.Templates.Add(encounterTemplate2);
			EncounterTemplate encounterTemplate3 = new EncounterTemplate(Difficulty.Moderate);
			List<EncounterTemplateSlot> encounterTemplateSlots = encounterTemplate3.Slots;
			RoleType[] roleTypeArray1 = new RoleType[] { RoleType.Controller, RoleType.Skirmisher };
			encounterTemplateSlots.Add(new EncounterTemplateSlot(1, 1, roleTypeArray1));
			encounterTemplateGroup.Templates.Add(encounterTemplate3);
			EncounterTemplate encounterTemplate4 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate4.Slots.Add(new EncounterTemplateSlot(1, 4, RoleType.Artillery));
			encounterTemplateGroup.Templates.Add(encounterTemplate4);
			EncounterTemplate encounterTemplate5 = new EncounterTemplate(Difficulty.Hard);
			List<EncounterTemplateSlot> slots1 = encounterTemplate5.Slots;
			RoleType[] roleTypeArray2 = new RoleType[] { RoleType.Controller, RoleType.Skirmisher };
			slots1.Add(new EncounterTemplateSlot(1, 3, roleTypeArray2));
			encounterTemplateGroup.Templates.Add(encounterTemplate5);
			EncounterBuilder.fTemplateGroups.Add(encounterTemplateGroup);
			EncounterTemplateGroup encounterTemplateGroup1 = new EncounterTemplateGroup("Duel vs Defender", "Individual PC");
			EncounterTemplate encounterTemplate6 = new EncounterTemplate(Difficulty.Easy);
			encounterTemplate6.Slots.Add(new EncounterTemplateSlot(1, 0, RoleType.Skirmisher));
			encounterTemplateGroup1.Templates.Add(encounterTemplate6);
			EncounterTemplate encounterTemplate7 = new EncounterTemplate(Difficulty.Easy);
			List<EncounterTemplateSlot> encounterTemplateSlots1 = encounterTemplate7.Slots;
			RoleType[] roleTypeArray3 = new RoleType[] { RoleType.Brute, RoleType.Soldier };
			encounterTemplateSlots1.Add(new EncounterTemplateSlot(1, -1, roleTypeArray3));
			encounterTemplateGroup1.Templates.Add(encounterTemplate7);
			EncounterTemplate encounterTemplate8 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate8.Slots.Add(new EncounterTemplateSlot(1, 2, RoleType.Skirmisher));
			encounterTemplateGroup1.Templates.Add(encounterTemplate8);
			EncounterTemplate encounterTemplate9 = new EncounterTemplate(Difficulty.Moderate);
			List<EncounterTemplateSlot> slots2 = encounterTemplate9.Slots;
			RoleType[] roleTypeArray4 = new RoleType[] { RoleType.Brute, RoleType.Soldier };
			slots2.Add(new EncounterTemplateSlot(1, 1, roleTypeArray4));
			encounterTemplateGroup1.Templates.Add(encounterTemplate9);
			EncounterTemplate encounterTemplate10 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate10.Slots.Add(new EncounterTemplateSlot(1, 4, RoleType.Skirmisher));
			encounterTemplateGroup1.Templates.Add(encounterTemplate10);
			EncounterTemplate encounterTemplate11 = new EncounterTemplate(Difficulty.Hard);
			List<EncounterTemplateSlot> encounterTemplateSlots2 = encounterTemplate11.Slots;
			RoleType[] roleTypeArray5 = new RoleType[] { RoleType.Controller, RoleType.Skirmisher };
			encounterTemplateSlots2.Add(new EncounterTemplateSlot(1, 3, roleTypeArray5));
			encounterTemplateGroup1.Templates.Add(encounterTemplate11);
			EncounterBuilder.fTemplateGroups.Add(encounterTemplateGroup1);
			EncounterTemplateGroup encounterTemplateGroup2 = new EncounterTemplateGroup("Duel vs Leader", "Individual PC");
			EncounterTemplate encounterTemplate12 = new EncounterTemplate(Difficulty.Easy);
			encounterTemplate12.Slots.Add(new EncounterTemplateSlot(1, 0, RoleType.Skirmisher));
			encounterTemplateGroup2.Templates.Add(encounterTemplate12);
			EncounterTemplate encounterTemplate13 = new EncounterTemplate(Difficulty.Easy);
			List<EncounterTemplateSlot> slots3 = encounterTemplate13.Slots;
			RoleType[] roleTypeArray6 = new RoleType[] { RoleType.Controller, RoleType.Soldier };
			slots3.Add(new EncounterTemplateSlot(1, -1, roleTypeArray6));
			encounterTemplateGroup2.Templates.Add(encounterTemplate13);
			EncounterTemplate encounterTemplate14 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate14.Slots.Add(new EncounterTemplateSlot(1, 2, RoleType.Skirmisher));
			encounterTemplateGroup2.Templates.Add(encounterTemplate14);
			EncounterTemplate encounterTemplate15 = new EncounterTemplate(Difficulty.Moderate);
			List<EncounterTemplateSlot> encounterTemplateSlots3 = encounterTemplate15.Slots;
			RoleType[] roleTypeArray7 = new RoleType[] { RoleType.Controller, RoleType.Soldier };
			encounterTemplateSlots3.Add(new EncounterTemplateSlot(1, 1, roleTypeArray7));
			encounterTemplateGroup2.Templates.Add(encounterTemplate15);
			EncounterTemplate encounterTemplate16 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate16.Slots.Add(new EncounterTemplateSlot(1, 4, RoleType.Skirmisher));
			encounterTemplateGroup2.Templates.Add(encounterTemplate16);
			EncounterTemplate encounterTemplate17 = new EncounterTemplate(Difficulty.Hard);
			List<EncounterTemplateSlot> slots4 = encounterTemplate17.Slots;
			RoleType[] roleTypeArray8 = new RoleType[] { RoleType.Controller, RoleType.Soldier };
			slots4.Add(new EncounterTemplateSlot(1, 3, roleTypeArray8));
			encounterTemplateGroup2.Templates.Add(encounterTemplate17);
			EncounterBuilder.fTemplateGroups.Add(encounterTemplateGroup2);
			EncounterTemplateGroup encounterTemplateGroup3 = new EncounterTemplateGroup("Duel vs Striker", "Individual PC");
			EncounterTemplate encounterTemplate18 = new EncounterTemplate(Difficulty.Easy);
			encounterTemplate18.Slots.Add(new EncounterTemplateSlot(1, 0, RoleType.Skirmisher));
			encounterTemplateGroup3.Templates.Add(encounterTemplate18);
			EncounterTemplate encounterTemplate19 = new EncounterTemplate(Difficulty.Easy);
			List<EncounterTemplateSlot> encounterTemplateSlots4 = encounterTemplate19.Slots;
			RoleType[] roleTypeArray9 = new RoleType[] { RoleType.Brute, RoleType.Soldier };
			encounterTemplateSlots4.Add(new EncounterTemplateSlot(1, -1, roleTypeArray9));
			encounterTemplateGroup3.Templates.Add(encounterTemplate19);
			EncounterTemplate encounterTemplate20 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate20.Slots.Add(new EncounterTemplateSlot(1, 2, RoleType.Skirmisher));
			encounterTemplateGroup3.Templates.Add(encounterTemplate20);
			EncounterTemplate encounterTemplate21 = new EncounterTemplate(Difficulty.Moderate);
			List<EncounterTemplateSlot> slots5 = encounterTemplate21.Slots;
			RoleType[] roleTypeArray10 = new RoleType[] { RoleType.Brute, RoleType.Soldier };
			slots5.Add(new EncounterTemplateSlot(1, 1, roleTypeArray10));
			encounterTemplateGroup3.Templates.Add(encounterTemplate21);
			EncounterTemplate encounterTemplate22 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate22.Slots.Add(new EncounterTemplateSlot(1, 4, RoleType.Skirmisher));
			encounterTemplateGroup3.Templates.Add(encounterTemplate22);
			EncounterTemplate encounterTemplate23 = new EncounterTemplate(Difficulty.Hard);
			List<EncounterTemplateSlot> encounterTemplateSlots5 = encounterTemplate23.Slots;
			RoleType[] roleTypeArray11 = new RoleType[] { RoleType.Brute, RoleType.Soldier };
			encounterTemplateSlots5.Add(new EncounterTemplateSlot(1, 3, roleTypeArray11));
			encounterTemplateGroup3.Templates.Add(encounterTemplate23);
			EncounterBuilder.fTemplateGroups.Add(encounterTemplateGroup3);
		}

		private static void build_template_grand_melee()
		{
			EncounterTemplateGroup encounterTemplateGroup = new EncounterTemplateGroup("Grand Melee", "Entire Party");
			EncounterTemplate encounterTemplate = new EncounterTemplate(Difficulty.Easy);
			encounterTemplate.Slots.Add(new EncounterTemplateSlot(4, -2, RoleType.Brute));
			encounterTemplate.Slots.Add(new EncounterTemplateSlot(11, -4, true));
			encounterTemplateGroup.Templates.Add(encounterTemplate);
			EncounterTemplate encounterTemplate1 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate1.Slots.Add(new EncounterTemplateSlot(2, -1, RoleType.Soldier));
			encounterTemplate1.Slots.Add(new EncounterTemplateSlot(4, -2, RoleType.Brute));
			encounterTemplate1.Slots.Add(new EncounterTemplateSlot(12, -4, true));
			encounterTemplateGroup.Templates.Add(encounterTemplate1);
			EncounterTemplate encounterTemplate2 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate2.Slots.Add(new EncounterTemplateSlot(2, 0, RoleType.Soldier));
			encounterTemplate2.Slots.Add(new EncounterTemplateSlot(4, -1, RoleType.Brute));
			encounterTemplate2.Slots.Add(new EncounterTemplateSlot(17, -2, true));
			encounterTemplateGroup.Templates.Add(encounterTemplate2);
			EncounterBuilder.fTemplateGroups.Add(encounterTemplateGroup);
		}

		private static void build_template_list(string group_name, Difficulty diff, int level, bool include_individual)
		{
			EncounterBuilder.fTemplateGroups.Clear();
			EncounterBuilder.build_template_battlefield_control();
			EncounterBuilder.build_template_commander_and_troops();
			EncounterBuilder.build_template_double_line();
			EncounterBuilder.build_template_dragons_den();
			EncounterBuilder.build_template_grand_melee();
			EncounterBuilder.build_template_wolf_pack();
			if (include_individual)
			{
				EncounterBuilder.build_template_duel();
			}
			if (group_name != "")
			{
				List<EncounterTemplateGroup> encounterTemplateGroups = new List<EncounterTemplateGroup>();
				foreach (EncounterTemplateGroup fTemplateGroup in EncounterBuilder.fTemplateGroups)
				{
					if (fTemplateGroup.Name == group_name)
					{
						continue;
					}
					encounterTemplateGroups.Add(fTemplateGroup);
				}
				foreach (EncounterTemplateGroup encounterTemplateGroup in encounterTemplateGroups)
				{
					EncounterBuilder.fTemplateGroups.Remove(encounterTemplateGroup);
				}
			}
			if (diff != Difficulty.Random)
			{
				List<EncounterTemplateGroup> encounterTemplateGroups1 = new List<EncounterTemplateGroup>();
				foreach (EncounterTemplateGroup fTemplateGroup1 in EncounterBuilder.fTemplateGroups)
				{
					List<EncounterTemplate> encounterTemplates = new List<EncounterTemplate>();
					foreach (EncounterTemplate template in fTemplateGroup1.Templates)
					{
						if (template.Difficulty == diff)
						{
							continue;
						}
						encounterTemplates.Add(template);
					}
					foreach (EncounterTemplate encounterTemplate in encounterTemplates)
					{
						fTemplateGroup1.Templates.Remove(encounterTemplate);
					}
					if (fTemplateGroup1.Templates.Count != 0)
					{
						continue;
					}
					encounterTemplateGroups1.Add(fTemplateGroup1);
				}
				foreach (EncounterTemplateGroup encounterTemplateGroup1 in encounterTemplateGroups1)
				{
					EncounterBuilder.fTemplateGroups.Remove(encounterTemplateGroup1);
				}
			}
			if (level != -1)
			{
				List<EncounterTemplateGroup> encounterTemplateGroups2 = new List<EncounterTemplateGroup>();
				foreach (EncounterTemplateGroup fTemplateGroup2 in EncounterBuilder.fTemplateGroups)
				{
					List<EncounterTemplate> encounterTemplates1 = new List<EncounterTemplate>();
					foreach (EncounterTemplate template1 in fTemplateGroup2.Templates)
					{
						bool flag = true;
						foreach (EncounterTemplateSlot slot in template1.Slots)
						{
							if (level + slot.LevelAdjustment >= 1)
							{
								continue;
							}
							flag = false;
							break;
						}
						if (flag)
						{
							continue;
						}
						encounterTemplates1.Add(template1);
					}
					foreach (EncounterTemplate encounterTemplate1 in encounterTemplates1)
					{
						fTemplateGroup2.Templates.Remove(encounterTemplate1);
					}
					if (fTemplateGroup2.Templates.Count != 0)
					{
						continue;
					}
					encounterTemplateGroups2.Add(fTemplateGroup2);
				}
				foreach (EncounterTemplateGroup encounterTemplateGroup2 in encounterTemplateGroups2)
				{
					EncounterBuilder.fTemplateGroups.Remove(encounterTemplateGroup2);
				}
			}
		}

		private static void build_template_wolf_pack()
		{
			EncounterTemplateGroup encounterTemplateGroup = new EncounterTemplateGroup("Wolf Pack", "Entire Party");
			EncounterTemplate encounterTemplate = new EncounterTemplate(Difficulty.Easy);
			encounterTemplate.Slots.Add(new EncounterTemplateSlot(7, -4, RoleType.Skirmisher));
			encounterTemplateGroup.Templates.Add(encounterTemplate);
			EncounterTemplate encounterTemplate1 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate1.Slots.Add(new EncounterTemplateSlot(7, -2, RoleType.Skirmisher));
			encounterTemplateGroup.Templates.Add(encounterTemplate1);
			EncounterTemplate encounterTemplate2 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate2.Slots.Add(new EncounterTemplateSlot(5, 0, RoleType.Skirmisher));
			encounterTemplateGroup.Templates.Add(encounterTemplate2);
			EncounterTemplate encounterTemplate3 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate3.Slots.Add(new EncounterTemplateSlot(3, 5, RoleType.Skirmisher));
			encounterTemplateGroup.Templates.Add(encounterTemplate3);
			EncounterTemplate encounterTemplate4 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate4.Slots.Add(new EncounterTemplateSlot(4, 5, RoleType.Skirmisher));
			encounterTemplateGroup.Templates.Add(encounterTemplate4);
			EncounterTemplate encounterTemplate5 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate5.Slots.Add(new EncounterTemplateSlot(6, 2, RoleType.Skirmisher));
			encounterTemplateGroup.Templates.Add(encounterTemplate5);
			EncounterBuilder.fTemplateGroups.Add(encounterTemplateGroup);
		}

		private static void build_trap_list(int level)
		{
			int num = level - 3;
			int num1 = level + 5;
			EncounterBuilder.fTraps.Clear();
			foreach (Trap trap in Session.Traps)
			{
				if (trap.Level < num || trap.Level > num1)
				{
					continue;
				}
				EncounterBuilder.fTraps.Add(trap);
			}
		}

		public static EncounterDeck BuildDeck(int level, List<string> categories, List<string> keywords)
		{
			EncounterBuilder.build_creature_list(level - 2, level + 5, categories, keywords, false);
			if (EncounterBuilder.fCreatures.Count == 0)
			{
				return null;
			}
			Dictionary<CardCategory, Pair<int, int>> cardCategories = new Dictionary<CardCategory, Pair<int, int>>();
			cardCategories[CardCategory.SoldierBrute] = new Pair<int, int>(0, 18);
			cardCategories[CardCategory.Skirmisher] = new Pair<int, int>(0, 14);
			cardCategories[CardCategory.Minion] = new Pair<int, int>(0, 5);
			cardCategories[CardCategory.Artillery] = new Pair<int, int>(0, 5);
			cardCategories[CardCategory.Controller] = new Pair<int, int>(0, 5);
			cardCategories[CardCategory.Lurker] = new Pair<int, int>(0, 2);
			cardCategories[CardCategory.Solo] = new Pair<int, int>(0, 1);
			Dictionary<Difficulty, Pair<int, int>> difficulties = new Dictionary<Difficulty, Pair<int, int>>();
			if (level < 3)
			{
				difficulties[Difficulty.Easy] = new Pair<int, int>(0, 37);
			}
			else
			{
				difficulties[Difficulty.Trivial] = new Pair<int, int>(0, 7);
				difficulties[Difficulty.Easy] = new Pair<int, int>(0, 30);
			}
			difficulties[Difficulty.Moderate] = new Pair<int, int>(0, 8);
			difficulties[Difficulty.Hard] = new Pair<int, int>(0, 5);
			difficulties[Difficulty.Extreme] = new Pair<int, int>(0, 0);
			EncounterDeck encounterDeck = new EncounterDeck()
			{
				Level = level
			};
			int num = 0;
			do
			{
			Label0:
				if (num >= 100)
				{
					break;
				}
				num++;
				int num1 = Session.Random.Next() % EncounterBuilder.fCreatures.Count;
				EncounterCard item = EncounterBuilder.fCreatures[num1];
				CardCategory category = item.Category;
				Pair<int, int> pair = cardCategories[category];
				if (pair.First < pair.Second)
				{
					Difficulty difficulty = item.GetDifficulty(level);
					Pair<int, int> item1 = difficulties[difficulty];
					if (item1.First < item1.Second)
					{
						encounterDeck.Cards.Add(item);
						Pair<int, int> first = cardCategories[category];
						first.First = first.First + 1;
						Pair<int, int> first1 = difficulties[difficulty];
						first1.First = first1.First + 1;
					}
					else
					{
						goto Label0;
					}
				}
				else
				{
					goto Label0;
				}
			}
			while (encounterDeck.Cards.Count != 50);
			EncounterBuilder.FillDeck(encounterDeck);
			return encounterDeck;
		}

		public static void FillDeck(EncounterDeck deck)
		{
			EncounterBuilder.build_creature_list(deck.Level - 2, deck.Level + 5, null, null, false);
			if (EncounterBuilder.fCreatures.Count == 0)
			{
				return;
			}
			while (deck.Cards.Count < 50)
			{
				int num = Session.Random.Next() % EncounterBuilder.fCreatures.Count;
				EncounterCard item = EncounterBuilder.fCreatures[num];
				deck.Cards.Add(item);
			}
		}

		public static List<EncounterCard> FindCreatures(EncounterTemplateSlot slot, int party_level, string query)
		{
			int partyLevel = party_level + slot.LevelAdjustment;
			EncounterBuilder.build_creature_list(partyLevel, partyLevel, null, null, true);
			List<EncounterCard> encounterCards = new List<EncounterCard>();
			foreach (EncounterCard fCreature in EncounterBuilder.fCreatures)
			{
				if (!slot.Match(fCreature, party_level) || !EncounterBuilder.match(fCreature, query))
				{
					continue;
				}
				encounterCards.Add(fCreature);
			}
			return encounterCards;
		}

		public static List<string> FindTemplateNames()
		{
			EncounterBuilder.build_template_list("", Difficulty.Random, -1, true);
			List<string> strs = new List<string>();
			foreach (EncounterTemplateGroup fTemplateGroup in EncounterBuilder.fTemplateGroups)
			{
				strs.Add(fTemplateGroup.Name);
			}
			strs.Sort();
			return strs;
		}

		public static List<Pair<EncounterTemplateGroup, EncounterTemplate>> FindTemplates(Encounter enc, int level, bool include_individual)
		{
			EncounterBuilder.build_template_list("", Difficulty.Random, level, include_individual);
			List<Pair<EncounterTemplateGroup, EncounterTemplate>> pairs = new List<Pair<EncounterTemplateGroup, EncounterTemplate>>();
			foreach (EncounterTemplateGroup fTemplateGroup in EncounterBuilder.fTemplateGroups)
			{
				foreach (EncounterTemplate template in fTemplateGroup.Templates)
				{
					EncounterTemplate encounterTemplate = template.Copy();
					bool flag = true;
					foreach (EncounterSlot slot in enc.Slots)
					{
						EncounterTemplateSlot encounterTemplateSlot = encounterTemplate.FindSlot(slot, level);
						if (encounterTemplateSlot == null)
						{
							flag = false;
							break;
						}
						else
						{
							EncounterTemplateSlot count = encounterTemplateSlot;
							count.Count = count.Count - slot.CombatData.Count;
							if (encounterTemplateSlot.Count > 0)
							{
								continue;
							}
							encounterTemplate.Slots.Remove(encounterTemplateSlot);
						}
					}
					if (!flag)
					{
						continue;
					}
					bool flag1 = true;
					foreach (EncounterTemplateSlot slot1 in encounterTemplate.Slots)
					{
						bool flag2 = false;
						int num = level + slot1.LevelAdjustment;
						EncounterBuilder.build_creature_list(num, num, null, null, true);
						foreach (EncounterCard fCreature in EncounterBuilder.fCreatures)
						{
							if (!slot1.Match(fCreature, level))
							{
								continue;
							}
							flag2 = true;
							break;
						}
						if (flag2)
						{
							continue;
						}
						flag1 = false;
						break;
					}
					if (!flag1)
					{
						continue;
					}
					pairs.Add(new Pair<EncounterTemplateGroup, EncounterTemplate>(fTemplateGroup, encounterTemplate));
				}
			}
			return pairs;
		}

		private static bool match(EncounterCard card, string query)
		{
			string[] strArrays = query.ToLower().Split(new char[0]);
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				if (!EncounterBuilder.match_token(card, strArrays[i]))
				{
					return false;
				}
			}
			return true;
		}

		private static bool match_token(EncounterCard card, string token)
		{
			if (!card.Title.ToLower().Contains(token) && !card.Category.ToString().ToLower().Contains(token))
			{
				return false;
			}
			return true;
		}

		private static void remove_creature(Encounter enc)
		{
			if (enc.Count == 0)
			{
				return;
			}
			int num = Session.Random.Next() % enc.Slots.Count;
			EncounterSlot item = enc.Slots[num];
			if (item.CombatData.Count == 1)
			{
				enc.Slots.Remove(item);
				return;
			}
			item.CombatData.RemoveAt(item.CombatData.Count - 1);
		}
	}
}