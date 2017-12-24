using Masterplan.Data;
using System;
using System.Collections.Generic;
using Utils;

namespace Masterplan.Tools
{
	internal class CreatureHelper
	{
		public CreatureHelper()
		{
		}

		public static void AdjustCreatureLevel(ICreature creature, int delta)
		{
			if (creature.Role is ComplexRole)
			{
				ComplexRole role = creature.Role as ComplexRole;
				int num = 8;
				switch (role.Type)
				{
					case RoleType.Artillery:
					case RoleType.Lurker:
					{
						num = 6;
						goto case RoleType.Controller;
					}
					case RoleType.Blaster:
					case RoleType.Controller:
					{
						switch (role.Flag)
						{
							case RoleFlag.Elite:
							{
								num *= 2;
								break;
							}
							case RoleFlag.Solo:
							{
								num *= 5;
								break;
							}
						}
						ICreature hP = creature;
						hP.HP = hP.HP + num * delta;
						creature.HP = Math.Max(creature.HP, 1);
						break;
					}
					case RoleType.Brute:
					{
						num = 10;
						goto case RoleType.Controller;
					}
					default:
					{
						goto case RoleType.Controller;
					}
				}
			}
			int initiative = creature.Initiative - creature.Level / 2;
			creature.Initiative = initiative + (creature.Level + delta) / 2;
			ICreature aC = creature;
			aC.AC = aC.AC + delta;
			ICreature fortitude = creature;
			fortitude.Fortitude = fortitude.Fortitude + delta;
			ICreature reflex = creature;
			reflex.Reflex = reflex.Reflex + delta;
			ICreature will = creature;
			will.Will = will.Will + delta;
			foreach (CreaturePower creaturePower in creature.CreaturePowers)
			{
				CreatureHelper.AdjustPowerLevel(creaturePower, delta);
			}
			if (creature.Skills != "")
			{
				Dictionary<string, int> strs = CreatureHelper.ParseSkills(creature.Skills);
				BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
				foreach (string key in strs.Keys)
				{
					binarySearchTree.Add(key);
				}
				string str = "";
				foreach (string sortedList in binarySearchTree.SortedList)
				{
					if (str != "")
					{
						str = string.Concat(str, ", ");
					}
					int item = strs[sortedList];
					int level = item - creature.Level / 2;
					item = level + (creature.Level + delta) / 2;
					if (item < 0)
					{
						object obj = str;
						object[] objArray = new object[] { obj, sortedList, " ", item };
						str = string.Concat(objArray);
					}
					else
					{
						object obj1 = str;
						object[] objArray1 = new object[] { obj1, sortedList, " +", item };
						str = string.Concat(objArray1);
					}
				}
				creature.Skills = str;
			}
			ICreature level1 = creature;
			level1.Level = level1.Level + delta;
		}

		public static void AdjustPowerLevel(CreaturePower cp, int delta)
		{
			if (cp.Attack != null)
			{
				PowerAttack attack = cp.Attack;
				attack.Bonus = attack.Bonus + delta;
			}
			string str = AI.ExtractDamage(cp.Details);
			if (str != "")
			{
				DiceExpression diceExpression = DiceExpression.Parse(str);
				if (diceExpression != null)
				{
					DiceExpression diceExpression1 = diceExpression.Adjust(delta);
					if (diceExpression1 != null && diceExpression.ToString() != diceExpression1.ToString())
					{
						cp.Details = cp.Details.Replace(str, string.Concat(diceExpression1, " damage"));
					}
				}
			}
		}

		public static Regeneration ConvertAura(string aura_details)
		{
			Regeneration regeneration;
			aura_details = aura_details.Trim();
			bool flag = true;
			string str = "";
			string str1 = "";
			string auraDetails = aura_details;
			for (int i = 0; i < auraDetails.Length; i++)
			{
				char chr = auraDetails[i];
				if (!char.IsDigit(chr))
				{
					flag = false;
				}
				if (!flag)
				{
					str1 = string.Concat(str1, chr);
				}
				else
				{
					str = string.Concat(str, chr);
				}
			}
			str1 = str1.Trim();
			if (str1.StartsWith("(") && str1.EndsWith(")"))
			{
				str1 = str1.Substring(1);
				str1 = str1.Substring(0, str1.Length - 1);
				str1.Trim();
			}
			try
			{
				regeneration = new Regeneration((str != "" ? int.Parse(str) : 0), str1);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
				regeneration = null;
			}
			return regeneration;
		}

		public static void CopyFields(ICreature copy_from, ICreature copy_to)
		{
			IRole role;
			Regeneration regeneration;
			try
			{
				if (copy_from != null)
				{
					copy_to.ID = copy_from.ID;
					copy_to.Name = copy_from.Name;
					copy_to.Details = copy_from.Details;
					copy_to.Size = copy_from.Size;
					copy_to.Origin = copy_from.Origin;
					copy_to.Type = copy_from.Type;
					copy_to.Keywords = copy_from.Keywords;
					copy_to.Level = copy_from.Level;
					ICreature copyTo = copy_to;
					if (copy_from.Role != null)
					{
						role = copy_from.Role.Copy();
					}
					else
					{
						role = null;
					}
					copyTo.Role = role;
					copy_to.Senses = copy_from.Senses;
					copy_to.Movement = copy_from.Movement;
					copy_to.Alignment = copy_from.Alignment;
					copy_to.Languages = copy_from.Languages;
					copy_to.Skills = copy_from.Skills;
					copy_to.Equipment = copy_from.Equipment;
					copy_to.Category = copy_from.Category;
					copy_to.Strength = copy_from.Strength.Copy();
					copy_to.Constitution = copy_from.Constitution.Copy();
					copy_to.Dexterity = copy_from.Dexterity.Copy();
					copy_to.Intelligence = copy_from.Intelligence.Copy();
					copy_to.Wisdom = copy_from.Wisdom.Copy();
					copy_to.Charisma = copy_from.Charisma.Copy();
					copy_to.HP = copy_from.HP;
					copy_to.Initiative = copy_from.Initiative;
					copy_to.AC = copy_from.AC;
					copy_to.Fortitude = copy_from.Fortitude;
					copy_to.Reflex = copy_from.Reflex;
					copy_to.Will = copy_from.Will;
					ICreature creature = copy_to;
					if (copy_from.Regeneration != null)
					{
						regeneration = copy_from.Regeneration.Copy();
					}
					else
					{
						regeneration = null;
					}
					creature.Regeneration = regeneration;
					copy_to.Auras.Clear();
					foreach (Aura aura in copy_from.Auras)
					{
						copy_to.Auras.Add(aura.Copy());
					}
					copy_to.CreaturePowers.Clear();
					foreach (CreaturePower creaturePower in copy_from.CreaturePowers)
					{
						copy_to.CreaturePowers.Add(creaturePower.Copy());
					}
					copy_to.DamageModifiers.Clear();
					foreach (DamageModifier damageModifier in copy_from.DamageModifiers)
					{
						copy_to.DamageModifiers.Add(damageModifier.Copy());
					}
					copy_to.Resist = copy_from.Resist;
					copy_to.Vulnerable = copy_from.Vulnerable;
					copy_to.Immune = copy_from.Immune;
					copy_to.Tactics = copy_from.Tactics;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		public static List<CreaturePower> CreaturePowersByCategory(ICreature c, CreaturePowerCategory category)
		{
			List<CreaturePower> creaturePowers = new List<CreaturePower>();
			foreach (CreaturePower creaturePower in c.CreaturePowers)
			{
				if (creaturePower.Category != category)
				{
					continue;
				}
				creaturePowers.Add(creaturePower);
			}
			return creaturePowers;
		}

		public static Aura FindAura(ICreature c, string name)
		{
			Aura aura;
			List<Aura>.Enumerator enumerator = c.Auras.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Aura current = enumerator.Current;
					if (current.Name != name)
					{
						continue;
					}
					aura = current;
					return aura;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return aura;
		}

		public static Dictionary<string, int> ParseSkills(string source)
		{
			Dictionary<string, int> strs = new Dictionary<string, int>();
			if (source != null && source != "")
			{
				string[] strArrays = new string[] { ",", ";" };
				string[] strArrays1 = source.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < (int)strArrays1.Length; i++)
				{
					string str = strArrays1[i].Trim();
					int num = str.IndexOf(" ");
					if (num != -1)
					{
						string str1 = str.Substring(0, num);
						string str2 = str.Substring(num + 1);
						int num1 = 0;
						try
						{
							num1 = int.Parse(str2);
						}
						catch
						{
							num1 = 0;
						}
						strs[str1] = num1;
					}
				}
			}
			return strs;
		}

		public static void UpdatePowerRange(ICreature c, CreaturePower power)
		{
			if (power.Range != null && power.Range != "")
			{
				return;
			}
			List<string> strs = new List<string>()
			{
				"close blast",
				"close burst",
				"area burst",
				"melee",
				"ranged"
			};
			string str = "";
			string details = power.Details;
			string[] strArrays = new string[] { ";" };
			string[] strArrays1 = details.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < (int)strArrays1.Length; i++)
			{
				string str1 = strArrays1[i];
				bool flag = false;
				foreach (string str2 in strs)
				{
					if (!str1.ToLower().Contains(str2))
					{
						continue;
					}
					flag = true;
					break;
				}
				if (!flag)
				{
					if (str != "")
					{
						str = string.Concat(str, "; ");
					}
					str = string.Concat(str, str1);
				}
				else
				{
					power.Range = str1;
				}
			}
			power.Details = str;
		}

		public static void UpdateRegen(ICreature c)
		{
			Aura aura = CreatureHelper.FindAura(c, "Regeneration") ?? CreatureHelper.FindAura(c, "Regen");
			if (aura != null)
			{
				Regeneration regeneration = CreatureHelper.ConvertAura(aura.Details);
				if (regeneration != null)
				{
					c.Regeneration = regeneration;
					c.Auras.Remove(aura);
				}
			}
		}
	}
}