using Masterplan;
using Masterplan.Properties;
using Masterplan.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Utils;

namespace Masterplan.Data
{
	[Serializable]
	public class EncounterCard
	{
		private ICreature fCreature;

		private Guid fCreatureID = Guid.Empty;

		private List<Guid> fTemplateIDs = new List<Guid>();

		private int fLevelAdjustment;

		private Guid fThemeID = Guid.Empty;

		private Guid fThemeAttackPowerID = Guid.Empty;

		private Guid fThemeUtilityPowerID = Guid.Empty;

		private bool fDrawn;

		public int AC
		{
			get
			{
				int aC = 0;
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				if (creature != null)
				{
					aC += creature.AC;
				}
				foreach (Guid fTemplateID in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(fTemplateID, SearchType.Global);
					if (creatureTemplate == null)
					{
						continue;
					}
					aC += creatureTemplate.AC;
				}
				aC += this.fLevelAdjustment;
				if (Session.Project != null)
				{
					aC += Session.Project.CampaignSettings.ACBonus;
				}
				return aC;
			}
		}

		public List<Aura> Auras
		{
			get
			{
				List<Aura> auras = new List<Aura>();
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				if (creature != null)
				{
					auras.AddRange(creature.Auras);
				}
				foreach (Guid fTemplateID in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(fTemplateID, SearchType.Global);
					if (creatureTemplate == null)
					{
						continue;
					}
					auras.AddRange(creatureTemplate.Auras);
				}
				return auras;
			}
		}

		public CardCategory Category
		{
			get
			{
				if (((this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global))).Role is Minion)
				{
					return CardCategory.Minion;
				}
				if (this.Flag == RoleFlag.Solo)
				{
					return CardCategory.Solo;
				}
				List<RoleType> roles = this.Roles;
				if (roles.Contains(RoleType.Soldier) || roles.Contains(RoleType.Brute))
				{
					return CardCategory.SoldierBrute;
				}
				if (roles.Contains(RoleType.Skirmisher))
				{
					return CardCategory.Skirmisher;
				}
				if (roles.Contains(RoleType.Artillery))
				{
					return CardCategory.Artillery;
				}
				if (roles.Contains(RoleType.Controller))
				{
					return CardCategory.Controller;
				}
				if (!roles.Contains(RoleType.Lurker))
				{
					throw new Exception();
				}
				return CardCategory.Lurker;
			}
		}

		public Guid CreatureID
		{
			get
			{
				return this.fCreatureID;
			}
			set
			{
				this.fCreatureID = value;
				if (this.fCreatureID != Guid.Empty)
				{
					this.fCreature = Session.FindCreature(this.fCreatureID, SearchType.Global);
				}
			}
		}

		public List<CreaturePower> CreaturePowers
		{
			get
			{
				Enum.GetValues(typeof(DamageCategory));
				Enum.GetValues(typeof(DamageDegree));
				List<CreaturePower> creaturePowers = new List<CreaturePower>();
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				if (creature != null)
				{
					foreach (CreaturePower creaturePower in creature.CreaturePowers)
					{
						creaturePowers.Add(creaturePower.Copy());
					}
				}
				foreach (Guid fTemplateID in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(fTemplateID, SearchType.Global);
					if (creatureTemplate == null)
					{
						continue;
					}
					foreach (CreaturePower creaturePower1 in creatureTemplate.CreaturePowers)
					{
						CreaturePower creaturePower2 = creaturePower1.Copy();
						if (creatureTemplate.Type == CreatureTemplateType.Functional && creaturePower2.Attack != null)
						{
							PowerAttack attack = creaturePower2.Attack;
							attack.Bonus = attack.Bonus + this.Level;
						}
						creaturePowers.Add(creaturePower2);
					}
				}
				if (this.fThemeID != Guid.Empty)
				{
					MonsterTheme monsterTheme = Session.FindTheme(this.fThemeID, SearchType.Global);
					if (monsterTheme != null)
					{
						ThemePowerData themePowerDatum = monsterTheme.FindPower(this.fThemeAttackPowerID);
						if (themePowerDatum != null)
						{
							creaturePowers.Add(themePowerDatum.Power.Copy());
						}
						ThemePowerData themePowerDatum1 = monsterTheme.FindPower(this.fThemeUtilityPowerID);
						if (themePowerDatum1 != null)
						{
							creaturePowers.Add(themePowerDatum1.Power.Copy());
						}
					}
				}
				if (this.fLevelAdjustment != 0)
				{
					foreach (CreaturePower creaturePower3 in creaturePowers)
					{
						if (creaturePower3.Attack != null)
						{
							PowerAttack bonus = creaturePower3.Attack;
							bonus.Bonus = bonus.Bonus + this.fLevelAdjustment;
							if (Session.Project != null)
							{
								PowerAttack powerAttack = creaturePower3.Attack;
								powerAttack.Bonus = powerAttack.Bonus + Session.Project.CampaignSettings.AttackBonus;
							}
						}
						string str = AI.ExtractDamage(creaturePower3.Details);
						if (str == "")
						{
							continue;
						}
						DiceExpression diceExpression = DiceExpression.Parse(str);
						if (diceExpression == null)
						{
							continue;
						}
						DiceExpression diceExpression1 = diceExpression.Adjust(this.fLevelAdjustment);
						if (diceExpression1 == null || !(diceExpression.ToString() != diceExpression1.ToString()))
						{
							continue;
						}
						string details = creaturePower3.Details;
						object[] objArray = new object[] { diceExpression1, " damage (adjusted from ", str, ")" };
						creaturePower3.Details = details.Replace(str, string.Concat(objArray));
					}
				}
				return creaturePowers;
			}
		}

		public List<DamageModifier> DamageModifiers
		{
			get
			{
				List<DamageModifier> damageModifiers = new List<DamageModifier>();
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				if (creature != null)
				{
					foreach (DamageModifier damageModifier in creature.DamageModifiers)
					{
						damageModifiers.Add(damageModifier.Copy());
					}
				}
				foreach (Guid fTemplateID in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(fTemplateID, SearchType.Global);
					if (creatureTemplate == null)
					{
						continue;
					}
					foreach (DamageModifierTemplate damageModifierTemplate in creatureTemplate.DamageModifierTemplates)
					{
						DamageModifier damageModifier1 = null;
						foreach (DamageModifier damageModifier2 in damageModifiers)
						{
							if (damageModifier2.Type != damageModifierTemplate.Type)
							{
								continue;
							}
							damageModifier1 = damageModifier2;
							break;
						}
						if (damageModifier1 != null && damageModifier1.Value == 0)
						{
							continue;
						}
						if (damageModifier1 == null)
						{
							damageModifier1 = new DamageModifier()
							{
								Type = damageModifierTemplate.Type,
								Value = 0
							};
							damageModifiers.Add(damageModifier1);
						}
						if (damageModifierTemplate.HeroicValue + damageModifierTemplate.ParagonValue + damageModifierTemplate.EpicValue != 0)
						{
							int heroicValue = damageModifierTemplate.HeroicValue;
							if (creature.Level >= 10)
							{
								heroicValue = damageModifierTemplate.ParagonValue;
							}
							if (creature.Level >= 20)
							{
								heroicValue = damageModifierTemplate.EpicValue;
							}
							DamageModifier value = damageModifier1;
							value.Value = value.Value + heroicValue;
							if (damageModifier1.Value != 0)
							{
								continue;
							}
							damageModifiers.Remove(damageModifier1);
						}
						else
						{
							damageModifier1.Value = 0;
						}
					}
				}
				return damageModifiers;
			}
		}

		public bool Drawn
		{
			get
			{
				return this.fDrawn;
			}
			set
			{
				this.fDrawn = value;
			}
		}

		public string Equipment
		{
			get
			{
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				if (creature.Equipment == null)
				{
					return "";
				}
				return creature.Equipment;
			}
		}

		public RoleFlag Flag
		{
			get
			{
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				if (creature == null || creature.Role is Minion)
				{
					return RoleFlag.Standard;
				}
				int count = this.fTemplateIDs.Count;
				ComplexRole role = creature.Role as ComplexRole;
				if (role == null)
				{
					return RoleFlag.Standard;
				}
				switch (role.Flag)
				{
					case RoleFlag.Elite:
					{
						count++;
						break;
					}
					case RoleFlag.Solo:
					{
						count += 2;
						break;
					}
				}
				if (count == 0)
				{
					return RoleFlag.Standard;
				}
				if (count == 1)
				{
					return RoleFlag.Elite;
				}
				return RoleFlag.Solo;
			}
		}

		public int Fortitude
		{
			get
			{
				int fortitude = 0;
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				if (creature != null)
				{
					fortitude += creature.Fortitude;
				}
				foreach (Guid fTemplateID in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(fTemplateID, SearchType.Global);
					if (creatureTemplate == null)
					{
						continue;
					}
					fortitude += creatureTemplate.Fortitude;
				}
				fortitude += this.fLevelAdjustment;
				if (Session.Project != null)
				{
					fortitude += Session.Project.CampaignSettings.NADBonus;
				}
				return fortitude;
			}
		}

		public int HP
		{
			get
			{
				int hP = 0;
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				if (creature != null)
				{
					hP += creature.HP;
				}
				if (this.fTemplateIDs.Count != 0)
				{
					int num = 0;
					foreach (Guid fTemplateID in this.fTemplateIDs)
					{
						CreatureTemplate creatureTemplate = Session.FindTemplate(fTemplateID, SearchType.Global);
						if (creatureTemplate == null || creatureTemplate.HP <= num)
						{
							continue;
						}
						num = creatureTemplate.HP;
					}
					hP = hP + num * this.Level;
					hP += creature.Constitution.Score;
					if (this.Flag == RoleFlag.Solo)
					{
						hP *= 2;
					}
				}
				if (this.fLevelAdjustment != 0 && creature != null && creature.Role is ComplexRole)
				{
					ComplexRole role = creature.Role as ComplexRole;
					int num1 = 1;
					switch (role.Flag)
					{
						case RoleFlag.Elite:
						{
							num1 = 2;
							break;
						}
						case RoleFlag.Solo:
						{
							num1 = 5;
							break;
						}
					}
					switch (role.Type)
					{
						case RoleType.Artillery:
						{
							hP = hP + 6 * this.fLevelAdjustment * num1;
							break;
						}
						case RoleType.Brute:
						{
							hP = hP + 10 * this.fLevelAdjustment * num1;
							break;
						}
						case RoleType.Controller:
						{
							hP = hP + 8 * this.fLevelAdjustment * num1;
							break;
						}
						case RoleType.Lurker:
						{
							hP = hP + 6 * this.fLevelAdjustment * num1;
							break;
						}
						case RoleType.Skirmisher:
						{
							hP = hP + 8 * this.fLevelAdjustment * num1;
							break;
						}
						case RoleType.Soldier:
						{
							hP = hP + 8 * this.fLevelAdjustment * num1;
							break;
						}
					}
				}
				if (Session.Project != null && creature != null && creature.Role is ComplexRole)
				{
					hP = (int)((double)hP * Session.Project.CampaignSettings.HP);
				}
				return hP;
			}
		}

		public string Immune
		{
			get
			{
				string str = "";
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				if (creature != null)
				{
					str = string.Concat(str, creature.Immune);
				}
				foreach (Guid fTemplateID in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(fTemplateID, SearchType.Global);
					if (creatureTemplate == null || creatureTemplate.Immune == "")
					{
						continue;
					}
					if (str != "")
					{
						str = string.Concat(str, ", ");
					}
					str = string.Concat(str, creatureTemplate.Immune);
				}
				return str;
			}
		}

		public string Info
		{
			get
			{
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				if (creature == null)
				{
					return "";
				}
				int level = creature.Level + this.fLevelAdjustment;
				if (creature.Role is Minion)
				{
					object[] role = new object[] { "Level ", level, " ", creature.Role };
					return string.Concat(role);
				}
				string str = "";
				switch (this.Flag)
				{
					case RoleFlag.Elite:
					{
						str = "Elite ";
						break;
					}
					case RoleFlag.Solo:
					{
						str = "Solo ";
						break;
					}
				}
				string str1 = "";
				foreach (RoleType roleType in this.Roles)
				{
					if (str1 != "")
					{
						str1 = string.Concat(str1, " / ");
					}
					str1 = string.Concat(str1, roleType.ToString());
				}
				if (this.Leader)
				{
					str1 = string.Concat(str1, " (L)");
				}
				object[] objArray = new object[] { "Level ", level, " ", str, str1 };
				return string.Concat(objArray);
			}
		}

		public int Initiative
		{
			get
			{
				int initiative = 0;
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				if (creature != null)
				{
					initiative += creature.Initiative;
				}
				foreach (Guid fTemplateID in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(fTemplateID, SearchType.Global);
					if (creatureTemplate == null)
					{
						continue;
					}
					initiative += creatureTemplate.Initiative;
				}
				if (this.fLevelAdjustment != 0)
				{
					initiative = initiative + this.fLevelAdjustment / 2;
				}
				return initiative;
			}
		}

		public bool Leader
		{
			get
			{
				bool flag;
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				if (creature == null || creature.Role is Minion)
				{
					return false;
				}
				ComplexRole role = creature.Role as ComplexRole;
				if (role != null && role.Leader)
				{
					return true;
				}
				List<Guid>.Enumerator enumerator = this.fTemplateIDs.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						CreatureTemplate creatureTemplate = Session.FindTemplate(enumerator.Current, SearchType.Global);
						if (creatureTemplate == null || !creatureTemplate.Leader)
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
			}
		}

		public int Level
		{
			get
			{
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				if (creature == null)
				{
					return this.fLevelAdjustment;
				}
				return creature.Level + this.fLevelAdjustment;
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

		public string Movement
		{
			get
			{
				string movement = ((this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global))).Movement;
				foreach (Guid fTemplateID in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(fTemplateID, SearchType.Global);
					if (creatureTemplate == null || !(creatureTemplate.Movement != ""))
					{
						continue;
					}
					if (movement != "")
					{
						movement = string.Concat(movement, "; ");
					}
					movement = string.Concat(movement, creatureTemplate.Movement);
				}
				return movement;
			}
		}

		public int Reflex
		{
			get
			{
				int reflex = 0;
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				if (creature != null)
				{
					reflex += creature.Reflex;
				}
				foreach (Guid fTemplateID in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(fTemplateID, SearchType.Global);
					if (creatureTemplate == null)
					{
						continue;
					}
					reflex += creatureTemplate.Reflex;
				}
				reflex += this.fLevelAdjustment;
				if (Session.Project != null)
				{
					reflex += Session.Project.CampaignSettings.NADBonus;
				}
				return reflex;
			}
		}

		public Masterplan.Data.Regeneration Regeneration
		{
			get
			{
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				if (creature == null)
				{
					return null;
				}
				Masterplan.Data.Regeneration regeneration = creature.Regeneration;
				foreach (Guid fTemplateID in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(fTemplateID, SearchType.Global);
					if (creatureTemplate == null || creatureTemplate.Regeneration == null)
					{
						continue;
					}
					if (regeneration == null)
					{
						regeneration = creatureTemplate.Regeneration;
					}
					else
					{
						if (creatureTemplate.Regeneration.Value <= regeneration.Value)
						{
							continue;
						}
						regeneration = creatureTemplate.Regeneration;
					}
				}
				if (regeneration == null)
				{
					return null;
				}
				return regeneration.Copy();
			}
		}

		public string Resist
		{
			get
			{
				string str = "";
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				if (creature != null)
				{
					str = string.Concat(str, creature.Resist);
				}
				foreach (Guid fTemplateID in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(fTemplateID, SearchType.Global);
					if (creatureTemplate == null || creatureTemplate.Resist == "")
					{
						continue;
					}
					if (str != "")
					{
						str = string.Concat(str, ", ");
					}
					str = string.Concat(str, creatureTemplate.Resist);
				}
				return str;
			}
		}

		public List<RoleType> Roles
		{
			get
			{
				List<RoleType> roleTypes = new List<RoleType>();
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				if (creature == null || creature.Role is Minion)
				{
					return roleTypes;
				}
				ComplexRole role = creature.Role as ComplexRole;
				if (role != null)
				{
					roleTypes.Add(role.Type);
				}
				foreach (Guid fTemplateID in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(fTemplateID, SearchType.Global);
					if (creatureTemplate == null || roleTypes.Contains(creatureTemplate.Role))
					{
						continue;
					}
					roleTypes.Add(creatureTemplate.Role);
				}
				return roleTypes;
			}
		}

		public string Senses
		{
			get
			{
				List<string> strs = new List<string>();
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				if (creature.Senses != "")
				{
					strs.Add(creature.Senses);
				}
				foreach (Guid fTemplateID in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(fTemplateID, SearchType.Global);
					if (creatureTemplate == null || !(creatureTemplate.Senses != "") || strs.Contains(creatureTemplate.Senses))
					{
						continue;
					}
					strs.Add(creatureTemplate.Senses);
				}
				string str = "";
				foreach (string str1 in strs)
				{
					if (str != "")
					{
						str = string.Concat(str, "; ");
					}
					str = string.Concat(str, str1);
				}
				return str;
			}
		}

		public string Skills
		{
			get
			{
				MonsterTheme monsterTheme;
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				if (creature == null)
				{
					return "";
				}
				Dictionary<string, int> second = CreatureHelper.ParseSkills(creature.Skills);
				if (this.fThemeID != Guid.Empty)
				{
					monsterTheme = Session.FindTheme(this.fThemeID, SearchType.Global);
				}
				else
				{
					monsterTheme = null;
				}
				MonsterTheme monsterTheme1 = monsterTheme;
				if (monsterTheme1 != null)
				{
					foreach (Pair<string, int> skillBonuse in monsterTheme1.SkillBonuses)
					{
						if (!second.ContainsKey(skillBonuse.First))
						{
							int level = this.Level / 2;
							string keyAbility = Masterplan.Tools.Skills.GetKeyAbility(skillBonuse.First);
							if (keyAbility == "Strength")
							{
								level += creature.Strength.Modifier;
							}
							if (keyAbility == "Constitution")
							{
								level += creature.Constitution.Modifier;
							}
							if (keyAbility == "Dexterity")
							{
								level += creature.Dexterity.Modifier;
							}
							if (keyAbility == "Intelligence")
							{
								level += creature.Intelligence.Modifier;
							}
							if (keyAbility == "Wisdom")
							{
								level += creature.Wisdom.Modifier;
							}
							if (keyAbility == "Charisma")
							{
								level += creature.Charisma.Modifier;
							}
							second[skillBonuse.First] = skillBonuse.Second + level;
						}
						else
						{
							Dictionary<string, int> item = second;
							Dictionary<string, int> strs = item;
							string first = skillBonuse.First;
							string str = first;
							item[first] = strs[str] + skillBonuse.Second;
						}
					}
				}
				BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
				foreach (string key in second.Keys)
				{
					binarySearchTree.Add(key);
				}
				string str1 = "";
				foreach (string sortedList in binarySearchTree.SortedList)
				{
					if (str1 != "")
					{
						str1 = string.Concat(str1, ", ");
					}
					int num = second[sortedList];
					int level1 = num - creature.Level / 2;
					num = level1 + (creature.Level + this.fLevelAdjustment) / 2;
					if (num < 0)
					{
						object obj = str1;
						object[] objArray = new object[] { obj, sortedList, " ", num };
						str1 = string.Concat(objArray);
					}
					else
					{
						object obj1 = str1;
						object[] objArray1 = new object[] { obj1, sortedList, " +", num };
						str1 = string.Concat(objArray1);
					}
				}
				return str1;
			}
		}

		public string Tactics
		{
			get
			{
				string str = "";
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				if (creature != null)
				{
					str = string.Concat(str, creature.Tactics);
				}
				foreach (Guid fTemplateID in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(fTemplateID, SearchType.Global);
					if (creatureTemplate == null && creatureTemplate.Tactics == "")
					{
						continue;
					}
					if (str != "")
					{
						str = string.Concat(str, ", ");
					}
					str = string.Concat(str, creatureTemplate.Tactics);
				}
				return str;
			}
		}

		public List<Guid> TemplateIDs
		{
			get
			{
				return this.fTemplateIDs;
			}
			set
			{
				this.fTemplateIDs = value;
			}
		}

		public Guid ThemeAttackPowerID
		{
			get
			{
				return this.fThemeAttackPowerID;
			}
			set
			{
				this.fThemeAttackPowerID = value;
			}
		}

		public Guid ThemeID
		{
			get
			{
				return this.fThemeID;
			}
			set
			{
				this.fThemeID = value;
			}
		}

		public Guid ThemeUtilityPowerID
		{
			get
			{
				return this.fThemeUtilityPowerID;
			}
			set
			{
				this.fThemeUtilityPowerID = value;
			}
		}

		public string Title
		{
			get
			{
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				string str = (creature != null ? creature.Name : "(unknown creature)");
				foreach (Guid fTemplateID in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(fTemplateID, SearchType.Global);
					if (creatureTemplate == null)
					{
						continue;
					}
					str = string.Concat(creatureTemplate.Name, " ", str);
				}
				if (this.fThemeID != Guid.Empty)
				{
					MonsterTheme monsterTheme = Session.FindTheme(this.fThemeID, SearchType.Global);
					if (monsterTheme != null)
					{
						str = string.Concat(str, " (", monsterTheme.Name, ")");
					}
				}
				return str;
			}
		}

		public string Vulnerable
		{
			get
			{
				string str = "";
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				if (creature != null)
				{
					str = string.Concat(str, creature.Vulnerable);
				}
				foreach (Guid fTemplateID in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(fTemplateID, SearchType.Global);
					if (creatureTemplate == null || creatureTemplate.Vulnerable == "")
					{
						continue;
					}
					if (str != "")
					{
						str = string.Concat(str, ", ");
					}
					str = string.Concat(str, creatureTemplate.Vulnerable);
				}
				return str;
			}
		}

		public int Will
		{
			get
			{
				int will = 0;
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				if (creature != null)
				{
					will += creature.Will;
				}
				foreach (Guid fTemplateID in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(fTemplateID, SearchType.Global);
					if (creatureTemplate == null)
					{
						continue;
					}
					will += creatureTemplate.Will;
				}
				will += this.fLevelAdjustment;
				if (Session.Project != null)
				{
					will += Session.Project.CampaignSettings.NADBonus;
				}
				return will;
			}
		}

		public int XP
		{
			get
			{
				int creatureXP = 0;
				ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
				if (creature != null)
				{
					if (!(creature.Role is Minion))
					{
						creatureXP = Experience.GetCreatureXP(creature.Level + this.fLevelAdjustment);
						switch (this.Flag)
						{
							case RoleFlag.Elite:
							{
								creatureXP *= 2;
								break;
							}
							case RoleFlag.Solo:
							{
								creatureXP *= 5;
								break;
							}
						}
					}
					else
					{
						float single = (float)Experience.GetCreatureXP(creature.Level + this.fLevelAdjustment) / 4f;
						creatureXP = (int)Math.Round((double)single, MidpointRounding.AwayFromZero);
					}
				}
				if (Session.Project != null)
				{
					creatureXP = (int)((double)creatureXP * Session.Project.CampaignSettings.XP);
				}
				return creatureXP;
			}
		}

		public EncounterCard()
		{
		}

		public EncounterCard(Guid creature_id)
		{
			this.fCreatureID = creature_id;
			if (this.fCreatureID != Guid.Empty)
			{
				this.fCreature = Session.FindCreature(this.fCreatureID, SearchType.Global);
			}
		}

		public EncounterCard(ICreature creature)
		{
			this.fCreature = creature;
			this.fCreatureID = creature.ID;
		}

		private string ability(Ability ab, CardMode mode)
		{
			if (ab == null)
			{
				return "-";
			}
			int modifier = ab.Modifier + this.Level / 2;
			string str = "";
			switch (mode)
			{
				case CardMode.Combat:
				{
					object obj = str;
					object[] objArray = new object[] { obj, "<A href=\"ability:", modifier, "\">" };
					str = string.Concat(objArray);
					break;
				}
				case CardMode.Build:
				{
					str = string.Concat(str, "<A href=build:ability>");
					break;
				}
			}
			int score = ab.Score;
			str = string.Concat(str, score.ToString());
			str = string.Concat(str, " ");
			string str1 = modifier.ToString();
			if (modifier >= 0)
			{
				str1 = string.Concat("+", str1);
			}
			str = string.Concat(str, "(", str1, ")");
			switch (mode)
			{
				case CardMode.Combat:
				{
					str = string.Concat(str, "</A>");
					break;
				}
				case CardMode.Build:
				{
					str = string.Concat(str, "</A>");
					break;
				}
			}
			return str;
		}

		public List<string> AsText(CombatData combat_data, CardMode mode, bool full)
		{
			object[] d;
			object obj;
			string[] strArrays;
			int i;
			ICreature creature = (this.fCreature != null ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global));
			if (creature == null)
			{
				if (mode == CardMode.Text)
				{
					return new List<string>()
					{
						"(unknown creature)"
					};
				}
				List<string> strs = new List<string>()
				{
					"<TABLE>",
					"<TR class=creature>",
					"<TD>",
					"<B>(unknown creature)</B>",
					"</TD>",
					"</TR>",
					"<TR>",
					"<TD>",
					"No details",
					"</TD>",
					"</TR>",
					"</TABLE>"
				};
				return strs;
			}
			List<string> strs1 = new List<string>();
			if (mode != CardMode.Text)
			{
				string str = (combat_data == null ? this.Title : combat_data.DisplayName);
				strs1.Add("<TABLE>");
				if (mode == CardMode.Build)
				{
					bool flag = false;
					foreach (CreaturePower creaturePower in this.CreaturePowers)
					{
						if (creaturePower.Action == null || creaturePower.Action.Use != PowerUseType.Basic || creaturePower.Attack == null)
						{
							continue;
						}
						flag = true;
					}
					if (!flag)
					{
						strs1.Add("<TR class=warning>");
						strs1.Add("<TD colspan=3 align=center>");
						strs1.Add("<B>Warning</B>: This creature has no basic attack");
						strs1.Add("</TD>");
						strs1.Add("</TR>");
					}
					if (this.CreaturePowers.Count > 10)
					{
						strs1.Add("<TR class=warning>");
						strs1.Add("<TD colspan=3 align=center>");
						strs1.Add("<B>Warning</B>: This many powers might be slow in play");
						strs1.Add("</TD>");
						strs1.Add("</TR>");
					}
				}
				strs1.Add("<TR class=creature>");
				strs1.Add("<TD colspan=2>");
				strs1.Add(string.Concat("<B>", HTML.Process(str, true), "</B>"));
				strs1.Add("<BR>");
				strs1.Add(creature.Phenotype);
				strs1.Add("</TD>");
				strs1.Add("<TD>");
				strs1.Add(string.Concat("<B>", HTML.Process(this.Info, true), "</B>"));
				strs1.Add("<BR>");
				strs1.Add(string.Concat(this.XP, " XP"));
				strs1.Add("</TD>");
				strs1.Add("</TR>");
				if (mode == CardMode.Build)
				{
					strs1.Add("<TR class=creature>");
					strs1.Add("<TD colspan=3 align=center>");
					strs1.Add("<A href=build:profile style=\"color:white\">(click here to edit this top section)</A>");
					strs1.Add("</TD>");
					strs1.Add("</TR>");
				}
			}
			if (mode != CardMode.Text)
			{
				strs1.Add("<TR>");
			}
			string str1 = this.HP.ToString();
			if (combat_data != null && combat_data.Damage != 0)
			{
				int hP = this.HP - combat_data.Damage;
				str1 = (!(creature.Role is Minion) ? string.Concat(hP, " of ", this.HP) : hP.ToString());
			}
			string str2 = (mode != CardMode.Text ? "<B>HP</B>" : "HP");
			str2 = string.Concat(str2, " ", str1);
			if (combat_data != null && mode == CardMode.Combat)
			{
				if (!(creature.Role is Minion))
				{
					d = new object[] { str2, " (<A href=dmg:", combat_data.ID, ">dmg</A> | <A href=heal:", combat_data.ID, ">heal</A>)" };
					str2 = string.Concat(d);
				}
				else if (combat_data.Damage != 0)
				{
					d = new object[] { str2, " (<A href=revive:", combat_data.ID, ">revive</A>)" };
					str2 = string.Concat(d);
				}
				else
				{
					d = new object[] { str2, " (<A href=kill:", combat_data.ID, ">kill</A>)" };
					str2 = string.Concat(d);
				}
			}
			if (!(creature.Role is Minion))
			{
				string str3 = (mode != CardMode.Text ? "<B>Bloodied</B>" : "Bloodied");
				obj = str2;
				d = new object[] { obj, "; ", str3, " ", this.HP / 2 };
				str2 = string.Concat(d);
			}
			if (combat_data != null && combat_data.TempHP > 0)
			{
				obj = str2;
				d = new object[] { obj, "; ", null, null, null };
				d[2] = (mode != CardMode.Text ? "<B>Temp HP</B>" : "Temp HP");
				d[3] = " ";
				d[4] = combat_data.TempHP;
				str2 = string.Concat(d);
			}
			if (mode == CardMode.Build)
			{
				str2 = string.Concat(" <A href=build:combat>", str2, "</A>");
			}
			if (mode == CardMode.Text)
			{
				strs1.Add(str2);
			}
			else
			{
				strs1.Add("<TD colspan=2>");
				strs1.Add(str2);
				strs1.Add("</TD>");
			}
			int initiative = this.Initiative;
			string str4 = initiative.ToString();
			if (initiative >= 0)
			{
				str4 = string.Concat("+", str4);
			}
			if (combat_data != null && combat_data.Initiative != -2147483648)
			{
				d = new object[] { combat_data.Initiative, " (", str4, ")" };
				str4 = string.Concat(d);
			}
			switch (mode)
			{
				case CardMode.Text:
				{
					strs1.Add(string.Concat("Initiative ", str4));
					break;
				}
				case CardMode.View:
				{
					strs1.Add("<TD>");
					strs1.Add(string.Concat("<B>Initiative</B> ", str4));
					strs1.Add("</TD>");
					break;
				}
				case CardMode.Combat:
				{
					strs1.Add("<TD>");
					d = new object[] { "<B>Initiative</B> <A href=init:", combat_data.ID, ">", str4, "</A>" };
					strs1.Add(string.Concat(d));
					strs1.Add("</TD>");
					break;
				}
				case CardMode.Build:
				{
					strs1.Add("<TD>");
					strs1.Add(string.Concat("<A href=build:combat><B>Initiative</B> ", str4, "</A>"));
					strs1.Add("</TD>");
					break;
				}
			}
			if (mode != CardMode.Text)
			{
				strs1.Add("</TR>");
				strs1.Add("<TR>");
			}
			string str5 = (mode != CardMode.Text ? "<B>AC</B>" : "AC");
			string str6 = (mode != CardMode.Text ? "<B>Fort</B>" : "Fort");
			string str7 = (mode != CardMode.Text ? "<B>Ref</B>" : "Ref");
			string str8 = (mode != CardMode.Text ? "<B>Will</B>" : "Will");
			int aC = this.AC;
			int fortitude = this.Fortitude;
			int reflex = this.Reflex;
			int will = this.Will;
			if (combat_data != null)
			{
				foreach (OngoingCondition condition in combat_data.Conditions)
				{
					if (condition.Type != OngoingType.DefenceModifier)
					{
						continue;
					}
					if (condition.Defences.Contains(DefenceType.AC))
					{
						aC += condition.DefenceMod;
					}
					if (condition.Defences.Contains(DefenceType.Fortitude))
					{
						fortitude += condition.DefenceMod;
					}
					if (condition.Defences.Contains(DefenceType.Reflex))
					{
						reflex += condition.DefenceMod;
					}
					if (!condition.Defences.Contains(DefenceType.Will))
					{
						continue;
					}
					will += condition.DefenceMod;
				}
			}
			if (aC == this.AC || mode == CardMode.Text)
			{
				str5 = string.Concat(str5, " ", aC);
			}
			else
			{
				obj = str5;
				d = new object[] { obj, " <B><I>", aC, "</I></B>" };
				str5 = string.Concat(d);
			}
			if (fortitude == this.Fortitude || mode == CardMode.Text)
			{
				str6 = string.Concat(str6, " ", fortitude);
			}
			else
			{
				obj = str6;
				d = new object[] { obj, " <B><I>", fortitude, "</I></B>" };
				str6 = string.Concat(d);
			}
			if (reflex == this.Reflex || mode == CardMode.Text)
			{
				str7 = string.Concat(str7, " ", reflex);
			}
			else
			{
				obj = str7;
				d = new object[] { obj, " <B><I>", reflex, "</I></B>" };
				str7 = string.Concat(d);
			}
			if (will == this.Will || mode == CardMode.Text)
			{
				str8 = string.Concat(str8, " ", will);
			}
			else
			{
				obj = str8;
				d = new object[] { obj, " <B><I>", will, "</I></B>" };
				str8 = string.Concat(d);
			}
			string[] strArrays1 = new string[] { str5, "; ", str6, "; ", str7, "; ", str8 };
			string str9 = string.Concat(strArrays1);
			if (mode != CardMode.Text)
			{
				strs1.Add("<TD colspan=2>");
			}
			if (mode == CardMode.Build)
			{
				str9 = string.Concat("<A href=build:combat>", str9, "</A>");
			}
			strs1.Add(str9);
			if (mode != CardMode.Text)
			{
				strs1.Add("</TD>");
			}
			if (mode != CardMode.Text)
			{
				string str10 = "";
				if (creature.Skills != null && creature.Skills != "")
				{
					string skills = creature.Skills;
					strArrays1 = new string[] { ";", "," };
					strArrays = skills.Split(strArrays1, StringSplitOptions.RemoveEmptyEntries);
					for (i = 0; i < (int)strArrays.Length; i++)
					{
						string str11 = strArrays[i].Trim();
						if (str11.ToLower().Contains("perc"))
						{
							str10 = str11;
						}
					}
				}
				if (str10 == "")
				{
					int modifier = creature.Wisdom.Modifier + this.Level / 2;
					str10 = "Perception ";
					if (modifier >= 0)
					{
						str10 = string.Concat(str10, "+");
					}
					str10 = string.Concat(str10, modifier.ToString());
				}
				if (str10 != "")
				{
					strs1.Add("<TD>");
					if (mode == CardMode.Build)
					{
						str10 = string.Concat("<A href=build:skills>", str10, "</A>");
					}
					strs1.Add(str10);
					strs1.Add("</TD>");
				}
			}
			if (mode != CardMode.Text)
			{
				strs1.Add("</TR>");
				strs1.Add("<TR>");
			}
			if (mode != CardMode.Text)
			{
				string str12 = HTML.Process(this.Movement, true);
				if (str12 != "")
				{
					str12 = string.Concat("<B>Speed</B> ", str12);
				}
				if (mode == CardMode.Build && str12 == "")
				{
					str12 = "(specify movement)";
				}
				if (str12 != "")
				{
					strs1.Add("<TD colspan=2>");
					if (mode == CardMode.Build)
					{
						str12 = string.Concat("<A href=build:movement>", str12, "</A>");
					}
					strs1.Add(str12);
					strs1.Add("</TD>");
				}
			}
			if (mode != CardMode.Text)
			{
				string senses = this.Senses ?? "";
				senses = HTML.Process(senses, true);
				if (senses.ToLower().Contains("perception"))
				{
					strArrays1 = new string[] { ";", "," };
					string[] strArrays2 = senses.Split(strArrays1, StringSplitOptions.RemoveEmptyEntries);
					senses = "";
					strArrays = strArrays2;
					for (i = 0; i < (int)strArrays.Length; i++)
					{
						string str13 = strArrays[i];
						if (!str13.ToLower().Contains("perception"))
						{
							if (senses != "")
							{
								senses = string.Concat(senses, "; ");
							}
							senses = string.Concat(senses, str13);
						}
					}
				}
				int num = (this.Flag == RoleFlag.Standard ? 1 : 2);
				int count = this.DamageModifiers.Count;
				if (combat_data != null)
				{
					foreach (OngoingCondition ongoingCondition in combat_data.Conditions)
					{
						if (ongoingCondition.Type != OngoingType.DamageModifier)
						{
							continue;
						}
						count++;
					}
				}
				if (this.Resist != "" || this.Vulnerable != "" || this.Immune != "" || count != 0 || mode == CardMode.Build)
				{
					num++;
				}
				if (mode == CardMode.Build)
				{
					if (senses == "")
					{
						senses = "(specify senses)";
					}
					senses = string.Concat("<A href=build:senses>", senses, "</A>");
				}
				d = new object[] { "<TD rowspan=", num, ">", senses, "</TD>" };
				strs1.Add(string.Concat(d));
			}
			if (mode != CardMode.Text)
			{
				strs1.Add("</TR>");
			}
			if (mode != CardMode.Text)
			{
				string str14 = HTML.Process(this.Resist, true);
				string str15 = HTML.Process(this.Vulnerable, true);
				string str16 = HTML.Process(this.Immune, true);
				if (str14 == null)
				{
					str14 = "";
				}
				if (str15 == null)
				{
					str15 = "";
				}
				if (str16 == null)
				{
					str16 = "";
				}
				List<DamageModifier> damageModifiers = new List<DamageModifier>();
				damageModifiers.AddRange(this.DamageModifiers);
				if (combat_data != null)
				{
					foreach (OngoingCondition condition1 in combat_data.Conditions)
					{
						if (condition1.Type != OngoingType.DamageModifier)
						{
							continue;
						}
						damageModifiers.Add(condition1.DamageModifier);
					}
				}
				foreach (DamageModifier damageModifier in damageModifiers)
				{
					if (damageModifier.Value == 0)
					{
						if (str16 != "")
						{
							str16 = string.Concat(str16, ", ");
						}
						str16 = string.Concat(str16, damageModifier.Type.ToString().ToLower());
					}
					if (damageModifier.Value > 0)
					{
						if (str15 != "")
						{
							str15 = string.Concat(str15, ", ");
						}
						obj = str15;
						d = new object[] { obj, damageModifier.Value, " ", damageModifier.Type.ToString().ToLower() };
						str15 = string.Concat(d);
					}
					if (damageModifier.Value >= 0)
					{
						continue;
					}
					if (str14 != "")
					{
						str14 = string.Concat(str14, ", ");
					}
					int num1 = Math.Abs(damageModifier.Value);
					obj = str14;
					d = new object[] { obj, num1, " ", damageModifier.Type.ToString().ToLower() };
					str14 = string.Concat(d);
				}
				string str17 = "";
				if (str16 != "")
				{
					str17 = string.Concat(str17, "<B>Immune</B> ", str16);
				}
				if (str14 != "")
				{
					if (str17 != "")
					{
						str17 = string.Concat(str17, "; ");
					}
					str17 = string.Concat(str17, "<B>Resist</B> ", str14);
				}
				if (str15 != "")
				{
					if (str17 != "")
					{
						str17 = string.Concat(str17, "; ");
					}
					str17 = string.Concat(str17, "<B>Vulnerable</B> ", str15);
				}
				if (str17 != "")
				{
					if (mode == CardMode.Build)
					{
						str17 = string.Concat("<A href=build:damage>", str17, "</A>");
					}
					strs1.Add("<TR>");
					strs1.Add("<TD colspan=2>");
					strs1.Add(str17);
					strs1.Add("</TD>");
					strs1.Add("</TR>");
				}
				else if (mode == CardMode.Build)
				{
					strs1.Add("<TR>");
					strs1.Add("<TD colspan=2>");
					strs1.Add("<A href=build:damage>No resistances / vulnerabilities / immunities</A>");
					strs1.Add("</TD>");
					strs1.Add("</TR>");
				}
			}
			bool flag1 = false;
			if (mode != CardMode.Text)
			{
				int num2 = 0;
				int num3 = 0;
				switch (this.Flag)
				{
					case RoleFlag.Elite:
					{
						num2 = 2;
						num3 = 1;
						break;
					}
					case RoleFlag.Solo:
					{
						num2 = 5;
						num3 = 2;
						break;
					}
				}
				if (num3 != 0)
				{
					strs1.Add("<TD colspan=2>");
					d = new object[] { "<B>Saving Throws</B> +", num2, " <B>Action Points</B> ", num3 };
					strs1.Add(string.Concat(d));
					strs1.Add("</TD>");
					flag1 = true;
				}
			}
			if (flag1 && mode != CardMode.Text)
			{
				strs1.Add("</TR>");
			}
			if (mode == CardMode.Build)
			{
				strs1.Add("<TR>");
				strs1.Add("<TD colspan=3 align=center>");
				strs1.Add("(click on any value in this section to edit it)");
				strs1.Add("</TD>");
				strs1.Add("</TR>");
			}
			if (mode != CardMode.Text && full)
			{
				if (mode == CardMode.Build)
				{
					strs1.Add("<TR class=creature>");
					strs1.Add("<TD colspan=3>");
					strs1.Add("<B>Powers and Traits</B>");
					strs1.Add("</TD>");
					strs1.Add("</TR>");
					strs1.Add("<TR>");
					strs1.Add("<TD colspan=3 align=center>");
					strs1.Add("<A href=power:addtrait>add a trait</A>");
					strs1.Add("|");
					strs1.Add("<A href=power:addpower>add a power</A>");
					strs1.Add("|");
					strs1.Add("<A href=power:addaura>add an aura</A>");
					if (this.Regeneration == null)
					{
						strs1.Add("|");
						strs1.Add("<A href=power:regenedit>add regeneration</A>");
					}
					strs1.Add("<BR>");
					strs1.Add("<A href=power:browse>browse for an existing power or trait</A>");
					strs1.Add("</TD>");
					strs1.Add("</TR>");
				}
				Dictionary<CreaturePowerCategory, List<CreaturePower>> creaturePowerCategories = new Dictionary<CreaturePowerCategory, List<CreaturePower>>();
				Array values = Enum.GetValues(typeof(CreaturePowerCategory));
				foreach (CreaturePowerCategory value in values)
				{
					creaturePowerCategories[value] = new List<CreaturePower>();
				}
				foreach (CreaturePower creaturePower1 in this.CreaturePowers)
				{
					creaturePowerCategories[creaturePower1.Category].Add(creaturePower1);
				}
				foreach (CreaturePowerCategory creaturePowerCategory in values)
				{
					creaturePowerCategories[creaturePowerCategory].Sort();
				}
				foreach (CreaturePowerCategory value1 in values)
				{
					int count1 = creaturePowerCategories[value1].Count;
					if (value1 == CreaturePowerCategory.Trait)
					{
						count1 += this.Auras.Count;
						if (combat_data != null)
						{
							foreach (OngoingCondition ongoingCondition1 in combat_data.Conditions)
							{
								if (ongoingCondition1.Type != OngoingType.Aura)
								{
									continue;
								}
								count1++;
							}
						}
						bool regeneration = this.Regeneration != null;
						if (combat_data != null)
						{
							foreach (OngoingCondition condition2 in combat_data.Conditions)
							{
								if (condition2.Type != OngoingType.Regeneration)
								{
									continue;
								}
								regeneration = true;
							}
						}
						if (regeneration)
						{
							count1++;
						}
					}
					if (count1 == 0)
					{
						continue;
					}
					string str18 = "";
					switch (value1)
					{
						case CreaturePowerCategory.Trait:
						{
							str18 = "Traits";
							break;
						}
						case CreaturePowerCategory.Standard:
						case CreaturePowerCategory.Move:
						case CreaturePowerCategory.Minor:
						case CreaturePowerCategory.Free:
						{
							str18 = string.Concat(value1, " Actions");
							break;
						}
						case CreaturePowerCategory.Triggered:
						{
							str18 = "Triggered Actions";
							break;
						}
						case CreaturePowerCategory.Other:
						{
							str18 = "Other Actions";
							break;
						}
					}
					strs1.Add("<TR class=creature>");
					strs1.Add("<TD colspan=3>");
					strs1.Add(string.Concat("<B>", str18, "</B>"));
					strs1.Add("</TD>");
					strs1.Add("</TR>");
					if (value1 == CreaturePowerCategory.Trait)
					{
						List<Aura> auras = new List<Aura>();
						auras.AddRange(this.Auras);
						if (combat_data != null)
						{
							foreach (OngoingCondition ongoingCondition2 in combat_data.Conditions)
							{
								if (ongoingCondition2.Type != OngoingType.Aura)
								{
									continue;
								}
								auras.Add(ongoingCondition2.Aura);
							}
						}
						foreach (Aura aura in auras)
						{
							string str19 = HTML.Process(aura.Description.Trim(), true);
							if (str19.StartsWith("aura", StringComparison.OrdinalIgnoreCase))
							{
								str19 = string.Concat("A", str19.Substring(1));
							}
							MemoryStream memoryStream = new MemoryStream();
							Resources.Aura.Save(memoryStream, ImageFormat.Png);
							string base64String = Convert.ToBase64String(memoryStream.ToArray());
							strs1.Add("<TR class=shaded>");
							strs1.Add("<TD colspan=3>");
							strs1.Add(string.Concat("<img src=data:image/png;base64,", base64String, ">"));
							strs1.Add(string.Concat("<B>", HTML.Process(aura.Name, true), "</B>"));
							if (aura.Keywords != "")
							{
								strs1.Add(string.Concat("(", aura.Keywords, ")"));
							}
							if (aura.Radius > 0)
							{
								strs1.Add(string.Concat(" &diams; Aura ", aura.Radius));
							}
							strs1.Add("</TD>");
							strs1.Add("</TR>");
							strs1.Add("<TR>");
							strs1.Add("<TD colspan=3>");
							strs1.Add(str19);
							strs1.Add("</TD>");
							strs1.Add("</TR>");
							if (mode != CardMode.Build)
							{
								continue;
							}
							strs1.Add("<TR>");
							strs1.Add("<TD colspan=3 align=center>");
							strs1.Add(string.Concat("<A href=auraedit:", aura.ID, ">edit</A>"));
							strs1.Add("|");
							strs1.Add(string.Concat("<A href=auraremove:", aura.ID, ">remove</A>"));
							strs1.Add("this aura");
							strs1.Add("</TD>");
							strs1.Add("</TR>");
						}
						List<Masterplan.Data.Regeneration> regenerations = new List<Masterplan.Data.Regeneration>();
						if (this.Regeneration != null)
						{
							regenerations.Add(this.Regeneration);
						}
						if (combat_data != null)
						{
							foreach (OngoingCondition condition3 in combat_data.Conditions)
							{
								if (condition3.Type != OngoingType.Regeneration)
								{
									continue;
								}
								regenerations.Add(condition3.Regeneration);
							}
						}
						foreach (Masterplan.Data.Regeneration regeneration1 in regenerations)
						{
							strs1.Add("<TR class=shaded>");
							strs1.Add("<TD colspan=3>");
							strs1.Add("<B>Regeneration</B>");
							strs1.Add("</TD>");
							strs1.Add("</TR>");
							strs1.Add("<TR>");
							strs1.Add("<TD colspan=3>");
							strs1.Add(string.Concat("Regeneration ", HTML.Process(regeneration1.ToString(), true)));
							strs1.Add("</TD>");
							strs1.Add("</TR>");
							if (mode != CardMode.Build)
							{
								continue;
							}
							strs1.Add("<TR>");
							strs1.Add("<TD colspan=3 align=center>");
							strs1.Add("<A href=power:regenedit>edit</A>");
							strs1.Add("|");
							strs1.Add("<A href=power:regenremove>remove</A>");
							strs1.Add("this trait");
							strs1.Add("</TD>");
							strs1.Add("</TR>");
						}
					}
					foreach (CreaturePower item in creaturePowerCategories[value1])
					{
						CardMode cardMode = mode;
						if (mode == CardMode.Build)
						{
							cardMode = CardMode.View;
						}
						if (combat_data != null)
						{
							combat_data.UsedPowers.Contains(item.ID);
						}
						strs1.AddRange(item.AsHTML(combat_data, cardMode, false));
						if (mode != CardMode.Build)
						{
							continue;
						}
						strs1.Add("<TR>");
						strs1.Add("<TD colspan=3 align=center>");
						strs1.Add(string.Concat("<A href=\"poweredit:", item.ID, "\">edit</A>"));
						strs1.Add("|");
						strs1.Add(string.Concat("<A href=\"powerremove:", item.ID, "\">remove</A>"));
						strs1.Add("|");
						strs1.Add(string.Concat("<A href=\"powerduplicate:", item.ID, "\">duplicate</A>"));
						if (value1 != CreaturePowerCategory.Trait)
						{
							strs1.Add("this power");
						}
						else
						{
							strs1.Add("this trait");
						}
						strs1.Add("</TD>");
						strs1.Add("</TR>");
					}
				}
				string skills1 = this.Skills;
				if (skills1 != null && skills1.ToLower().Contains("perception"))
				{
					string str20 = "";
					strArrays1 = new string[] { ",", ";" };
					strArrays = skills1.Split(strArrays1, StringSplitOptions.RemoveEmptyEntries);
					for (i = 0; i < (int)strArrays.Length; i++)
					{
						string str21 = strArrays[i];
						if (!str21.ToLower().Contains("perception"))
						{
							if (str20 != "")
							{
								str20 = string.Concat(str20, "; ");
							}
							str20 = string.Concat(str20, str21);
						}
					}
					skills1 = str20;
				}
				if (skills1 == null)
				{
					skills1 = "";
				}
				if (skills1 == "" && mode == CardMode.Build)
				{
					skills1 = "(none)";
				}
				if (skills1 != "")
				{
					skills1 = HTML.Process(skills1, true);
					if (mode == CardMode.Build)
					{
						skills1 = string.Concat("<A href=build:skills>", skills1, "</A>");
					}
					strs1.Add("<TR class=shaded>");
					strs1.Add("<TD colspan=3>");
					strs1.Add(string.Concat("<B>Skills</B> ", skills1));
					strs1.Add("</TD>");
					strs1.Add("</TR>");
				}
				strs1.Add("<TR class=shaded>");
				strs1.Add("<TD>");
				strs1.Add(string.Concat("<B>Str</B>: ", this.ability(creature.Strength, mode)));
				strs1.Add("<BR>");
				strs1.Add(string.Concat("<B>Con</B>: ", this.ability(creature.Constitution, mode)));
				strs1.Add("</TD>");
				strs1.Add("<TD>");
				strs1.Add(string.Concat("<B>Dex</B>: ", this.ability(creature.Dexterity, mode)));
				strs1.Add("<BR>");
				strs1.Add(string.Concat("<B>Int</B>: ", this.ability(creature.Intelligence, mode)));
				strs1.Add("</TD>");
				strs1.Add("<TD>");
				strs1.Add(string.Concat("<B>Wis</B>: ", this.ability(creature.Wisdom, mode)));
				strs1.Add("<BR>");
				strs1.Add(string.Concat("<B>Cha</B>: ", this.ability(creature.Charisma, mode)));
				strs1.Add("</TD>");
				strs1.Add("</TR>");
				string alignment = creature.Alignment ?? "";
				if (alignment == "")
				{
					alignment = (mode != CardMode.Build ? "Unaligned" : "(not set)");
				}
				if (alignment != "")
				{
					alignment = HTML.Process(alignment, true);
					if (mode == CardMode.Build)
					{
						alignment = string.Concat("<A href=build:alignment>", alignment, "</A>");
					}
					strs1.Add("<TR>");
					strs1.Add("<TD colspan=3>");
					strs1.Add(string.Concat("<B>Alignment</B> ", alignment));
					strs1.Add("</TD>");
					strs1.Add("</TR>");
				}
				string languages = creature.Languages ?? "";
				if (languages == "" && mode == CardMode.Build)
				{
					languages = "(none)";
				}
				if (languages != "")
				{
					languages = HTML.Process(languages, true);
					if (mode == CardMode.Build)
					{
						languages = string.Concat("<A href=build:languages>", languages, "</A>");
					}
					strs1.Add("<TR>");
					strs1.Add("<TD colspan=3>");
					strs1.Add(string.Concat("<B>Languages</B> ", languages));
					strs1.Add("</TD>");
					strs1.Add("</TR>");
				}
				string equipment = this.Equipment ?? "";
				if (equipment == "" && mode == CardMode.Build)
				{
					equipment = "(none)";
				}
				if (equipment != "")
				{
					equipment = HTML.Process(equipment, true);
					if (mode == CardMode.Build)
					{
						equipment = string.Concat("<A href=build:equipment>", equipment, "</A>");
					}
					strs1.Add("<TR>");
					strs1.Add("<TD colspan=3>");
					strs1.Add(string.Concat("<B>Equipment</B> ", equipment));
					strs1.Add("</TD>");
					strs1.Add("</TR>");
				}
				string tactics = this.Tactics ?? "";
				if (tactics == "" && mode == CardMode.Build)
				{
					tactics = "(none specified)";
				}
				if (tactics != "")
				{
					tactics = HTML.Process(tactics, true);
					if (mode == CardMode.Build)
					{
						tactics = string.Concat("<A href=build:tactics>", tactics, "</A>");
					}
					strs1.Add("<TR>");
					strs1.Add("<TD colspan=3>");
					strs1.Add(string.Concat("<B>Tactics</B> ", tactics));
					strs1.Add("</TD>");
					strs1.Add("</TR>");
				}
				Creature creature1 = creature as Creature;
				List<string> strs2 = new List<string>();
				if (creature1 != null)
				{
					Library library = Session.FindLibrary(creature1);
					if (library != null && library.Name != "" && (Session.Project == null || library != Session.Project.Library))
					{
						strs2.Add(HTML.Process(library.Name, true));
					}
				}
				foreach (Guid fTemplateID in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(fTemplateID, SearchType.Global);
					Library library1 = Session.FindLibrary(creatureTemplate);
					if (library1 == null || library1 == Session.Project.Library)
					{
						continue;
					}
					if (strs2.Count != 0)
					{
						strs2.Add("<BR>");
					}
					string str22 = HTML.Process(library1.Name, true);
					strs2.Add(string.Concat(creatureTemplate.Name, " template: ", str22));
				}
				if (strs2.Count != 0)
				{
					strs1.Add("<TR class=shaded>");
					strs1.Add("<TD colspan=3>");
					foreach (string str23 in strs2)
					{
						strs1.Add(str23);
					}
					strs1.Add("</TD>");
					strs1.Add("</TR>");
				}
				if (creature1 != null && creature1.URL != "")
				{
					strs1.Add("<TR>");
					strs1.Add("<TD colspan=3>");
					strs1.Add(string.Concat("Copyright <A href=\"", creature1.URL, "\">Wizards of the Coast</A> 2010"));
					strs1.Add("</TD>");
					strs1.Add("</TR>");
				}
			}
			if (mode != CardMode.Text)
			{
				strs1.Add("</TABLE>");
			}
			return strs1;
		}

		public EncounterCard Copy()
		{
			EncounterCard encounterCard = new EncounterCard()
			{
				CreatureID = this.fCreatureID
			};
			foreach (Guid fTemplateID in this.fTemplateIDs)
			{
				encounterCard.TemplateIDs.Add(fTemplateID);
			}
			encounterCard.LevelAdjustment = this.fLevelAdjustment;
			encounterCard.ThemeID = this.fThemeID;
			encounterCard.ThemeAttackPowerID = this.fThemeAttackPowerID;
			encounterCard.ThemeUtilityPowerID = this.fThemeUtilityPowerID;
			encounterCard.Drawn = this.fDrawn;
			return encounterCard;
		}

		public CreaturePower FindPower(Guid power_id)
		{
			CreaturePower creaturePower;
			List<CreaturePower>.Enumerator enumerator = this.CreaturePowers.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					CreaturePower current = enumerator.Current;
					if (current.ID != power_id)
					{
						continue;
					}
					creaturePower = current;
					return creaturePower;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		public int GetDamageModifier(DamageType type, CombatData data)
		{
			List<DamageModifier> damageModifiers = new List<DamageModifier>();
			damageModifiers.AddRange(this.DamageModifiers);
			if (data != null)
			{
				foreach (OngoingCondition condition in data.Conditions)
				{
					if (condition.Type != OngoingType.DamageModifier)
					{
						continue;
					}
					damageModifiers.Add(condition.DamageModifier);
				}
			}
			if (damageModifiers.Count == 0)
			{
				return 0;
			}
			List<int> nums = new List<int>();
			foreach (DamageModifier damageModifier in damageModifiers)
			{
				if (damageModifier.Type != type)
				{
					continue;
				}
				if (damageModifier.Value != 0)
				{
					nums.Add(damageModifier.Value);
				}
				else
				{
					nums.Add(-2147483648);
				}
			}
			int num = 0;
			if (!nums.Contains(-2147483648))
			{
				int num1 = 0;
				int num2 = 0;
				foreach (int num3 in nums)
				{
					if (num3 > 0 && num3 > num1)
					{
						num1 = num3;
					}
					if (num3 >= 0 || num3 >= num2)
					{
						continue;
					}
					num2 = num3;
				}
				num = num1 + num2;
			}
			else
			{
				num = -2147483648;
			}
			return num;
		}

		public int GetDamageModifier(List<DamageType> types, CombatData data)
		{
			if (types == null || types.Count == 0)
			{
				return 0;
			}
			Dictionary<DamageType, int> damageTypes = new Dictionary<DamageType, int>();
			foreach (DamageType type in types)
			{
				damageTypes[type] = this.GetDamageModifier(type, data);
			}
			List<int> nums = new List<int>();
			List<int> nums1 = new List<int>();
			List<int> nums2 = new List<int>();
			foreach (DamageType damageType in types)
			{
				int item = damageTypes[damageType];
				if (item == -2147483648)
				{
					nums.Add(item);
				}
				if (item < 0)
				{
					nums1.Add(item);
				}
				if (item <= 0)
				{
					continue;
				}
				nums2.Add(item);
			}
			if (nums.Count == types.Count)
			{
				return -2147483648;
			}
			if (nums1.Count == types.Count)
			{
				nums1.Sort();
				nums1.Reverse();
				return nums1[0];
			}
			if (nums2.Count != types.Count)
			{
				return 0;
			}
			nums2.Sort();
			return nums2[0];
		}

		public Difficulty GetDifficulty(int party_level)
		{
			int level = this.Level - party_level;
			if (level < -1)
			{
				return Difficulty.Trivial;
			}
			Difficulty difficulty = Difficulty.Extreme;
			switch (level)
			{
				case -1:
				case 0:
				case 1:
				{
					difficulty = Difficulty.Easy;
					break;
				}
				case 2:
				case 3:
				{
					difficulty = Difficulty.Moderate;
					break;
				}
				case 4:
				case 5:
				{
					difficulty = Difficulty.Hard;
					break;
				}
			}
			return difficulty;
		}
	}
}