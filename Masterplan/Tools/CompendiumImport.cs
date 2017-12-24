using Masterplan.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Utils;

namespace Masterplan.Tools
{
	internal class CompendiumImport
	{
		public CompendiumImport()
		{
		}

		private static int get_score(XmlNode node)
		{
			int num;
			string str = "";
			bool flag = false;
			string value = node.Value;
			for (int i = 0; i < value.Length; i++)
			{
				char chr = value[i];
				if (char.IsNumber(chr))
				{
					flag = true;
					str = string.Concat(str, chr);
				}
				else if (flag)
				{
					break;
				}
			}
			try
			{
				num = int.Parse(str);
				return num;
			}
			catch
			{
			}
			string value1 = node.Value;
			string[] strArrays = value1.Split(new string[0], StringSplitOptions.RemoveEmptyEntries);
			try
			{
				num = int.Parse(strArrays[0]);
			}
			catch
			{
				return 0;
			}
			return num;
		}

		private static void handle_combat_section(XmlNode node, Creature c)
		{
			XmlNode nextSibling = node.FirstChild.NextSibling;
			c.Initiative = CompendiumImport.get_score(nextSibling);
			XmlNode xmlNodes = nextSibling.NextSibling.NextSibling;
			c.Senses = xmlNodes.Value.Trim();
			XmlNode nextSibling1 = xmlNodes;
			while (true)
			{
				nextSibling1 = nextSibling1.NextSibling.NextSibling;
				if (nextSibling1.FirstChild.Value == "HP")
				{
					break;
				}
				Aura aura = new Aura()
				{
					Name = nextSibling1.FirstChild.Value,
					Details = nextSibling1.NextSibling.Value
				};
				c.Auras.Add(aura);
				nextSibling1 = nextSibling1.NextSibling;
			}
			XmlNode xmlNodes1 = nextSibling1.NextSibling;
			c.HP = CompendiumImport.get_score(xmlNodes1);
			XmlNode nextSibling2 = xmlNodes1.NextSibling.NextSibling.NextSibling.NextSibling;
			if (nextSibling2.FirstChild.Value != "Regeneration")
			{
				nextSibling1 = xmlNodes1;
			}
			else
			{
				nextSibling2 = nextSibling2.NextSibling;
				Regeneration regeneration = CreatureHelper.ConvertAura(nextSibling2.Value);
				if (regeneration != null)
				{
					c.Regeneration = regeneration;
				}
				nextSibling1 = nextSibling2;
			}
			while (nextSibling1.FirstChild == null || !(nextSibling1.FirstChild.Value == "AC"))
			{
				nextSibling1 = nextSibling1.NextSibling;
			}
			XmlNode xmlNodes2 = nextSibling1.NextSibling;
			c.AC = CompendiumImport.get_score(xmlNodes2);
			XmlNode nextSibling3 = xmlNodes2.NextSibling.NextSibling;
			c.Fortitude = CompendiumImport.get_score(nextSibling3);
			XmlNode xmlNodes3 = nextSibling3.NextSibling.NextSibling;
			c.Reflex = CompendiumImport.get_score(xmlNodes3);
			XmlNode nextSibling4 = xmlNodes3.NextSibling.NextSibling;
			c.Will = CompendiumImport.get_score(nextSibling4);
			nextSibling1 = nextSibling4.NextSibling;
			XmlNode xmlNodes4 = null;
			XmlNode nextSibling5 = null;
			XmlNode xmlNodes5 = null;
			XmlNode nextSibling6 = null;
			XmlNode xmlNodes6 = null;
			XmlNode nextSibling7 = null;
			while (true)
			{
				nextSibling1 = nextSibling1.NextSibling;
				if (nextSibling1 == null)
				{
					break;
				}
				XmlNode firstChild = nextSibling1.FirstChild;
				if (firstChild != null)
				{
					string str = firstChild.Value.Trim();
					if (str == "Immune")
					{
						xmlNodes4 = nextSibling1.NextSibling;
					}
					if (str == "Resist")
					{
						nextSibling5 = nextSibling1.NextSibling;
					}
					if (str == "Vulnerable")
					{
						xmlNodes5 = nextSibling1.NextSibling;
					}
					if (str == "Saving Throws")
					{
						nextSibling6 = nextSibling1.NextSibling;
					}
					if (str == "Speed")
					{
						xmlNodes6 = nextSibling1.NextSibling;
					}
					if (str == "Action Points")
					{
						nextSibling7 = nextSibling1.NextSibling;
					}
				}
			}
			if (xmlNodes4 != null)
			{
				string str1 = xmlNodes4.Value.Trim();
				string[] strArrays = new string[] { ",", ";" };
				string[] strArrays1 = str1.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
				string str2 = "";
				string[] strArrays2 = strArrays1;
				for (int i = 0; i < (int)strArrays2.Length; i++)
				{
					string str3 = strArrays2[i];
					string str4 = str3.Trim();
					int num = str4.IndexOf(" ");
					if (num != -1)
					{
						try
						{
							int.Parse(str4.Substring(0, num));
							string str5 = str4.Substring(num + 1);
							DamageModifier damageModifier = DamageModifier.Parse(str5, 0);
							if (damageModifier != null)
							{
								c.DamageModifiers.Add(damageModifier);
                                break;
							}
						}
						catch
						{
						}
					}
					if (str2 != "")
					{
						str2 = string.Concat(str2, ", ");
					}
					str2 = string.Concat(str2, str3);
				}
				c.Immune = str2;
			}
			if (nextSibling5 != null)
			{
				string str6 = nextSibling5.Value.Trim();
				string[] strArrays3 = new string[] { ",", ";" };
				string[] strArrays4 = str6.Split(strArrays3, StringSplitOptions.RemoveEmptyEntries);
				string str7 = "";
				string[] strArrays5 = strArrays4;
				for (int j = 0; j < (int)strArrays5.Length; j++)
				{
					string str8 = strArrays5[j];
					string str9 = str8.Trim();
					int num1 = str9.IndexOf(" ");
					if (num1 != -1)
					{
						try
						{
							int num2 = int.Parse(str9.Substring(0, num1));
							string str10 = str9.Substring(num1 + 1);
							DamageModifier damageModifier1 = DamageModifier.Parse(str10, -num2);
							if (damageModifier1 != null)
							{
								c.DamageModifiers.Add(damageModifier1);
                                break;
							}
						}
						catch
						{
						}
					}
					if (str7 != "")
					{
						str7 = string.Concat(str7, ", ");
					}
					str7 = string.Concat(str7, str8);
				}
				c.Resist = str7;
			}
			if (xmlNodes5 != null)
			{
				string str11 = xmlNodes5.Value.Trim();
				string[] strArrays6 = new string[] { ",", ";" };
				string[] strArrays7 = str11.Split(strArrays6, StringSplitOptions.RemoveEmptyEntries);
				string str12 = "";
				string[] strArrays8 = strArrays7;
				for (int k = 0; k < (int)strArrays8.Length; k++)
				{
					string str13 = strArrays8[k];
					string str14 = str13.Trim();
					int num3 = str14.IndexOf(" ");
					if (num3 != -1)
					{
						try
						{
							int num4 = int.Parse(str14.Substring(0, num3));
							string str15 = str14.Substring(num3 + 1);
							DamageModifier damageModifier2 = DamageModifier.Parse(str15, num4);
							if (damageModifier2 != null)
							{
								c.DamageModifiers.Add(damageModifier2);
                                break;
							}
						}
						catch
						{
						}
					}
					if (str12 != "")
					{
						str12 = string.Concat(str12, ", ");
					}
					str12 = string.Concat(str12, str13);
				}
				c.Vulnerable = str12;
			}
			if (xmlNodes6 != null)
			{
				c.Movement = xmlNodes6.Value.Trim();
			}
		}

		private static void handle_end_section(XmlNode node, Creature c)
		{
			XmlNode nextSibling = node.FirstChild.NextSibling;
			c.Alignment = nextSibling.Value.Trim();
			XmlNode xmlNodes = nextSibling.NextSibling.NextSibling;
			string str = xmlNodes.Value.Trim();
			c.Languages = str.Replace("-", "");
			XmlNode xmlNodes1 = xmlNodes;
			if (xmlNodes.NextSibling.NextSibling.FirstChild.Value == "Skills")
			{
				XmlNode nextSibling1 = xmlNodes.NextSibling.NextSibling.NextSibling;
				c.Skills = nextSibling1.Value.Trim();
				xmlNodes1 = nextSibling1;
			}
			XmlNode nextSibling2 = xmlNodes1.NextSibling.NextSibling.NextSibling;
			c.Strength.Score = CompendiumImport.get_score(nextSibling2);
			XmlNode xmlNodes2 = nextSibling2.NextSibling.NextSibling;
			c.Dexterity.Score = CompendiumImport.get_score(xmlNodes2);
			XmlNode nextSibling3 = xmlNodes2.NextSibling.NextSibling;
			c.Wisdom.Score = CompendiumImport.get_score(nextSibling3);
			XmlNode xmlNodes3 = nextSibling3.NextSibling.NextSibling.NextSibling;
			c.Constitution.Score = CompendiumImport.get_score(xmlNodes3);
			XmlNode nextSibling4 = xmlNodes3.NextSibling.NextSibling;
			c.Intelligence.Score = CompendiumImport.get_score(nextSibling4);
			XmlNode xmlNodes4 = nextSibling4.NextSibling.NextSibling;
			c.Charisma.Score = CompendiumImport.get_score(xmlNodes4);
		}

		private static void handle_title_section(XmlNode node, Creature c)
		{
			string innerText = node.FirstChild.InnerText;
			string str = node.FirstChild.NextSibling.NextSibling.FirstChild.InnerText;
			string innerText1 = node.FirstChild.NextSibling.NextSibling.NextSibling.NextSibling.FirstChild.InnerText;
			c.Name = innerText.Trim();
			int num = 0;
			bool flag = false;
			bool flag1 = false;
			RoleFlag roleFlag = RoleFlag.Standard;
			RoleType roleType = RoleType.Artillery;
			bool flag2 = false;
			string[] strArrays = innerText1.Split(null);
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				string str1 = strArrays[i];
				if (str1 != "")
				{
					if (str1.ToLower() == "minion")
					{
						flag = true;
					}
					else if (str1.ToLower() != "(leader)")
					{
						try
						{
							num = int.Parse(str1);
                            break;
						}
						catch
						{
						}
						try
						{
							if (str1 != num.ToString())
							{
								roleFlag = (RoleFlag)Enum.Parse(typeof(RoleFlag), str1);
                                break;
							}
						}
						catch
						{
						}
						try
						{
							if (str1 != num.ToString())
							{
								roleType = (RoleType)Enum.Parse(typeof(RoleType), str1);
								flag2 = true;
							}
						}
						catch
						{
						}
					}
					else
					{
						flag1 = true;
					}
				}
			}
			c.Level = num;
			if (!flag)
			{
				ComplexRole complexRole = new ComplexRole()
				{
					Type = roleType,
					Flag = roleFlag,
					Leader = flag1
				};
				c.Role = complexRole;
			}
			else
			{
				Minion minion = new Minion()
				{
					HasRole = flag2
				};
				if (minion.HasRole)
				{
					minion.Type = roleType;
				}
				c.Role = minion;
			}
			int num1 = str.IndexOf("(");
			int num2 = str.IndexOf(")");
			if (num1 != -1 && num2 != -1)
			{
				int num3 = num2 - (num1 + 1);
				string str2 = str.Substring(num1 + 1, num3);
				str = str.Replace(str2, "");
				c.Keywords = str2;
			}
			int num4 = str.IndexOf(",");
			if (num4 == -1)
			{
				num4 = str.IndexOf(";");
			}
			if (num4 != -1)
			{
				string str3 = str.Substring(num4 + 1).Trim();
				str = str.Substring(0, num4);
				if (c.Keywords != "")
				{
					Creature creature = c;
					creature.Keywords = string.Concat(creature.Keywords, "; ");
				}
				Creature creature1 = c;
				creature1.Keywords = string.Concat(creature1.Keywords, str3);
			}
			string[] strArrays1 = str.Split(null);
			for (int j = 0; j < (int)strArrays1.Length; j++)
			{
				string str4 = strArrays1[j];
				if (str4 != "")
				{
					string str5 = string.Concat(char.ToUpper(str4[0]), str4.Substring(1));
					try
					{
						c.Size = (CreatureSize)Enum.Parse(typeof(CreatureSize), str5);
                        break;
					}
					catch
					{
					}
					try
					{
						c.Origin = (CreatureOrigin)Enum.Parse(typeof(CreatureOrigin), str5);
                        break;
					}
					catch
					{
					}
					try
					{
						c.Type = (CreatureType)Enum.Parse(typeof(CreatureType), str5);
					}
					catch
					{
					}
				}
			}
		}

		private static void handle_title_section(XmlNode node, Trap t)
		{
			string innerText = node.FirstChild.InnerText;
			string str = node.FirstChild.NextSibling.NextSibling.NextSibling.NextSibling.FirstChild.InnerText;
			t.Name = innerText;
			int num = 0;
			bool flag = false;
			bool flag1 = false;
			RoleFlag roleFlag = RoleFlag.Standard;
			RoleType roleType = RoleType.Artillery;
			bool flag2 = false;
			string[] strArrays = str.Split(null);
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				string str1 = strArrays[i];
				if (str1 != "")
				{
					if (str1.ToLower() == "minion")
					{
						flag = true;
					}
					else if (str1.ToLower() != "(leader)")
					{
						try
						{
							num = int.Parse(str1);
						}
						catch
						{
						}
						try
						{
							if (str1 != num.ToString())
							{
								roleFlag = (RoleFlag)Enum.Parse(typeof(RoleFlag), str1);
							}
						}
						catch
						{
						}
						try
						{
							if (str1 != num.ToString())
							{
								roleType = (RoleType)Enum.Parse(typeof(RoleType), str1);
								flag2 = true;
							}
						}
						catch
						{
						}
					}
					else
					{
						flag1 = true;
					}
				}
			}
			t.Level = num;
			if (!flag)
			{
				ComplexRole complexRole = new ComplexRole()
				{
					Type = roleType,
					Flag = roleFlag,
					Leader = flag1
				};
				t.Role = complexRole;
				return;
			}
			Minion minion = new Minion()
			{
				HasRole = flag2
			};
			if (minion.HasRole)
			{
				minion.Type = roleType;
			}
			t.Role = minion;
		}

		public static Creature ImportCreatureFromHTML(string html, string url)
		{
			Creature creature = null;
			try
			{
				html = CompendiumImport.simplify_html(html);
				XmlDocument xmlDocument = XMLHelper.LoadSource(html);
				if (xmlDocument != null)
				{
					creature = new Creature()
					{
						URL = url
					};
					XmlNode firstChild = xmlDocument.DocumentElement.FirstChild;
					try
					{
						CompendiumImport.handle_title_section(firstChild, creature);
					}
					catch
					{
					}
					firstChild = firstChild.NextSibling;
					try
					{
						CompendiumImport.handle_combat_section(firstChild, creature);
					}
					catch
					{
					}
					while (true)
					{
						firstChild = firstChild.NextSibling;
						XmlAttribute itemOf = firstChild.NextSibling.Attributes["class"];
						if (itemOf == null || itemOf.Value != "flavorIndent")
						{
							break;
						}
						CreaturePower creaturePower = null;
						try
						{
							creaturePower = CompendiumImport.parse_power(firstChild);
							while (true)
							{
								XmlAttribute xmlAttribute = firstChild.NextSibling.Attributes["class"];
								if (xmlAttribute != null && xmlAttribute.Value == "flavor alt")
								{
									break;
								}
								firstChild = firstChild.NextSibling;
								if (creaturePower.Details != "")
								{
									CreaturePower creaturePower1 = creaturePower;
									creaturePower1.Details = string.Concat(creaturePower1.Details, Environment.NewLine);
								}
								CreaturePower creaturePower2 = creaturePower;
								creaturePower2.Details = string.Concat(creaturePower2.Details, firstChild.FirstChild.Value);
							}
							creaturePower.ExtractAttackDetails();
						}
						catch
						{
						}
						if (creaturePower != null)
						{
							creature.CreaturePowers.Add(creaturePower);
						}
					}
					try
					{
						CompendiumImport.handle_end_section(firstChild, creature);
					}
					catch
					{
					}
					firstChild = firstChild.NextSibling;
					if (firstChild.FirstChild != null)
					{
						if (firstChild.FirstChild.FirstChild.Value == "Equipment")
						{
							string str = "";
							for (XmlNode i = firstChild.FirstChild.NextSibling; i != null; i = i.NextSibling)
							{
								if (i.FirstChild != null)
								{
									if (str != "")
									{
										str = string.Concat(str, "; ");
									}
									str = string.Concat(str, i.FirstChild.Value.Trim());
								}
							}
							creature.Equipment = str;
						}
						else if (firstChild.FirstChild.FirstChild.Value == "Description")
						{
							XmlNode nextSibling = firstChild.FirstChild.NextSibling;
							if (nextSibling != null)
							{
								string value = nextSibling.Value;
								if (value.StartsWith(":"))
								{
									value = value.Substring(1);
								}
								value = value.Trim();
								creature.Details = value;
							}
						}
					}
				}
				else
				{
					return null;
				}
			}
			catch
			{
				Console.WriteLine(string.Concat("Problem with creature: ", creature.Name));
				creature = null;
			}
			return creature;
		}

		public static MagicItem ImportItemFromHTML(string html, string url)
		{
			MagicItem magicItem = null;
			try
			{
				XmlDocument xmlDocument = XMLHelper.LoadSource(CompendiumImport.simplify_html(html));
				if (xmlDocument != null)
				{
					magicItem = new MagicItem()
					{
						URL = url
					};
					XmlNode firstChild = xmlDocument.DocumentElement.FirstChild;
					magicItem.Name = firstChild.InnerText.Trim();
					XmlNode nextSibling = firstChild.NextSibling;
					magicItem.Description = nextSibling.InnerText.Trim();
					try
					{
						XmlNode xmlNodes = nextSibling.NextSibling.FirstChild;
						while (xmlNodes.NextSibling != null)
						{
							if (xmlNodes.NextSibling.NextSibling != null)
							{
								while (xmlNodes.Name != "b")
								{
									xmlNodes = xmlNodes.NextSibling;
								}
								MagicItemSection magicItemSection = new MagicItemSection()
								{
									Header = xmlNodes.InnerText,
									Details = xmlNodes.NextSibling.InnerText
								};
								if (!(magicItemSection.Header == "Level") || !(magicItemSection.Details != ""))
								{
									if (magicItemSection.Details.StartsWith(":"))
									{
										magicItemSection.Details = magicItemSection.Details.Substring(1).Trim();
									}
									if (magicItemSection.Details == "")
									{
										magicItem.Type = magicItemSection.ToString();
									}
									else if (magicItemSection.Header != "Item Slot")
									{
										magicItem.Sections.Add(magicItemSection);
									}
									else
									{
										magicItem.Type = string.Concat(magicItemSection.Header, " (", magicItemSection.Details.ToLower(), ")");
									}
									if (magicItemSection.Header == "Weapon")
									{
										magicItem.Type = "Weapon";
									}
									if (magicItemSection.Header == "Armor")
									{
										magicItem.Type = "Armour";
									}
								}
								else
								{
									try
									{
										string str = magicItemSection.Details.Substring(1).Trim();
										magicItem.Level = int.Parse(str);
									}
									catch
									{
									}
								}
								xmlNodes = xmlNodes.NextSibling.NextSibling;
							}
							else
							{
								break;
							}
						}
					}
					catch
					{
					}
					try
					{
						for (XmlNode i = nextSibling.NextSibling.NextSibling; i != null && !i.InnerXml.ToLower().Contains("<a"); i = i.NextSibling)
						{
							string innerText = i.InnerText;
							int num = innerText.IndexOf(":");
							if (num != -1)
							{
								MagicItemSection magicItemSection1 = new MagicItemSection()
								{
									Header = innerText.Substring(0, num).Trim(),
									Details = innerText.Substring(num).Trim()
								};
								if (magicItemSection1.Details.StartsWith(":"))
								{
									magicItemSection1.Details = magicItemSection1.Details.Substring(1).Trim();
								}
								magicItem.Sections.Add(magicItemSection1);
							}
						}
					}
					catch
					{
					}
				}
				else
				{
					return null;
				}
			}
			catch
			{
				Console.WriteLine(string.Concat("Problem with magic item: ", magicItem.Name));
				magicItem = null;
			}
			if (magicItem.Type == "")
			{
				magicItem = null;
			}
			return magicItem;
		}

		public static Trap ImportTrapFromHTML(string html, string url)
		{
			Trap trap = null;
			try
			{
				XmlDocument xmlDocument = XMLHelper.LoadSource(CompendiumImport.simplify_html(html));
				if (xmlDocument != null)
				{
					trap = new Trap()
					{
						URL = url
					};
					if (!xmlDocument.InnerText.ToLower().Contains("hazard"))
					{
						trap.Type = TrapType.Trap;
					}
					else
					{
						trap.Type = TrapType.Hazard;
					}
					XmlNode firstChild = xmlDocument.DocumentElement.FirstChild;
					try
					{
						CompendiumImport.handle_title_section(firstChild, trap);
					}
					catch
					{
					}
					string lower = firstChild.NextSibling.InnerText.ToLower();
					if (!lower.StartsWith("trap") && !lower.StartsWith("hazard"))
					{
						firstChild = firstChild.NextSibling;
						trap.ReadAloud = firstChild.InnerText;
					}
					if (firstChild.FirstChild.NextSibling != null)
					{
						firstChild = firstChild.NextSibling;
						trap.Details = firstChild.FirstChild.NextSibling.InnerText;
					}
					try
					{
						firstChild = firstChild.NextSibling;
						while (true)
						{
							string innerText = firstChild.InnerText;
							if (innerText.StartsWith("Trap") || innerText.StartsWith("Hazard"))
							{
								firstChild = firstChild.NextSibling;
							}
							else
							{
								if (innerText.StartsWith("Trigger") || innerText.StartsWith("Initiative") || innerText.StartsWith("Target"))
								{
									break;
								}
								TrapSkillData trapSkillDatum = new TrapSkillData()
								{
									SkillName = innerText,
									DC = 0
								};
								foreach (XmlNode childNode in firstChild.NextSibling.ChildNodes)
								{
									string str = childNode.InnerText;
									if (str.StartsWith("DC "))
									{
										str = str.Substring(3);
										int num = str.IndexOf(":");
										string str1 = str.Substring(0, num);
										try
										{
											trapSkillDatum.DC = int.Parse(str1);
										}
										catch
										{
										}
										str = str.Substring(num + 1);
										str = str.Trim();
									}
									if (str == "")
									{
										continue;
									}
									if (trapSkillDatum.Details != "")
									{
										TrapSkillData trapSkillDatum1 = trapSkillDatum;
										trapSkillDatum1.Details = string.Concat(trapSkillDatum1.Details, Environment.NewLine);
									}
									TrapSkillData trapSkillDatum2 = trapSkillDatum;
									trapSkillDatum2.Details = string.Concat(trapSkillDatum2.Details, str);
								}
								trap.Skills.Add(trapSkillDatum);
								firstChild = firstChild.NextSibling.NextSibling;
							}
						}
					}
					catch
					{
					}
					if (firstChild.InnerText == "Initiative")
					{
						try
						{
							int num1 = int.Parse(firstChild.FirstChild.NextSibling.InnerText);
							trap.Attack.HasInitiative = true;
							trap.Attack.Initiative = num1;
						}
						catch
						{
						}
					}
					firstChild = firstChild.NextSibling;
					if (firstChild.FirstChild != null && firstChild.FirstChild.InnerText == "Trigger")
					{
						while (firstChild.NextSibling.FirstChild == null)
						{
							firstChild = firstChild.NextSibling;
						}
						trap.Attack.Trigger = firstChild.NextSibling.FirstChild.InnerText;
					}
					firstChild = firstChild.NextSibling.NextSibling;
					while (true)
					{
						firstChild = firstChild.NextSibling;
						if (firstChild.Name.ToLower() == "p")
						{
							break;
						}
						string lower1 = firstChild.FirstChild.InnerText.ToLower();
						if (lower1.StartsWith("countermeasure"))
						{
							break;
						}
						if (lower1.StartsWith("target"))
						{
							trap.Attack.Target = firstChild.FirstChild.NextSibling.InnerText;
						}
						else if (lower1.StartsWith("attack"))
						{
							if (firstChild.FirstChild.NextSibling != null)
							{
								string[] strArrays = firstChild.FirstChild.NextSibling.InnerText.Split(null);
								int num2 = 0;
								DefenceType defenceType = DefenceType.AC;
								try
								{
									num2 = int.Parse(strArrays[0]);
									defenceType = (DefenceType)Enum.Parse(typeof(DefenceType), strArrays[2]);
								}
								catch
								{
								}
								trap.Attack.Attack.Bonus = num2;
								trap.Attack.Attack.Defence = defenceType;
							}
						}
						else if (lower1.StartsWith("hit"))
						{
							if (firstChild.FirstChild.NextSibling == null)
							{
								foreach (XmlNode xmlNodes in firstChild.NextSibling.ChildNodes)
								{
									if (trap.Attack.OnHit != "")
									{
										TrapAttack attack = trap.Attack;
										attack.OnHit = string.Concat(attack.OnHit, Environment.NewLine);
									}
									TrapAttack trapAttack = trap.Attack;
									trapAttack.OnHit = string.Concat(trapAttack.OnHit, xmlNodes.InnerText);
								}
								firstChild = firstChild.NextSibling;
							}
							else
							{
								trap.Attack.OnHit = firstChild.FirstChild.NextSibling.InnerText;
							}
						}
						else if (lower1.StartsWith("miss"))
						{
							if (firstChild.FirstChild.NextSibling == null)
							{
								foreach (XmlNode childNode1 in firstChild.NextSibling.ChildNodes)
								{
									if (trap.Attack.OnMiss != "")
									{
										TrapAttack attack1 = trap.Attack;
										attack1.OnMiss = string.Concat(attack1.OnMiss, Environment.NewLine);
									}
									TrapAttack trapAttack1 = trap.Attack;
									trapAttack1.OnMiss = string.Concat(trapAttack1.OnMiss, childNode1.InnerText);
								}
								firstChild = firstChild.NextSibling;
							}
							else
							{
								trap.Attack.OnMiss = firstChild.FirstChild.NextSibling.InnerText;
							}
						}
						else if (lower1.StartsWith("effect"))
						{
							if (firstChild.FirstChild.NextSibling == null)
							{
								foreach (XmlNode xmlNodes1 in firstChild.NextSibling.ChildNodes)
								{
									if (trap.Attack.Effect != "")
									{
										TrapAttack attack2 = trap.Attack;
										attack2.Effect = string.Concat(attack2.Effect, Environment.NewLine);
									}
									TrapAttack trapAttack2 = trap.Attack;
									trapAttack2.Effect = string.Concat(trapAttack2.Effect, xmlNodes1.InnerText);
								}
								firstChild = firstChild.NextSibling;
							}
							else
							{
								trap.Attack.Effect = firstChild.FirstChild.NextSibling.InnerText;
							}
						}
						else if (!lower1.Contains(":"))
						{
							XmlNode nextSibling = firstChild.FirstChild;
							trap.Attack.Action = CompendiumImport.parse_action(nextSibling.InnerText);
							nextSibling = nextSibling.NextSibling;
							if (nextSibling != null)
							{
								string innerText1 = nextSibling.InnerText;
								nextSibling = nextSibling.NextSibling;
								if (nextSibling != null)
								{
									innerText1 = string.Concat(innerText1, nextSibling.InnerText);
								}
								trap.Attack.Range = innerText1;
							}
						}
						else if (firstChild.FirstChild.NextSibling != null)
						{
							string innerText2 = firstChild.FirstChild.NextSibling.InnerText;
							trap.Details = string.Concat(lower1, innerText2, Environment.NewLine, trap.Details);
						}
					}
					if (firstChild.InnerText != "Countermeasures")
					{
						if (firstChild.NextSibling != null)
						{
							firstChild = firstChild.NextSibling;
						}
						if (firstChild.FirstChild != null)
						{
							firstChild = firstChild.FirstChild;
						}
						while (firstChild != null && firstChild.Name.ToUpper() != "P" && firstChild.InnerText != "Encounter Uses")
						{
							string str2 = firstChild.InnerText;
							trap.Countermeasures.Add(str2);
							firstChild = firstChild.NextSibling;
						}
					}
					else
					{
						for (XmlNode i = firstChild.NextSibling.FirstChild; i != null; i = i.NextSibling)
						{
							string str3 = i.InnerText.Trim();
							if (str3 != "")
							{
								trap.Countermeasures.Add(str3);
							}
						}
					}
				}
				else
				{
					return null;
				}
			}
			catch
			{
				Console.WriteLine(string.Concat("Problem with trap: ", trap.Name));
				trap = null;
			}
			return trap;
		}

		private static ActionType parse_action(string str)
		{
			if (str == "standard" || str == "standard action")
			{
				return ActionType.Standard;
			}
			if (str == "move" || str == "move action")
			{
				return ActionType.Move;
			}
			if (str == "minor" || str == "minor action")
			{
				return ActionType.Minor;
			}
			if (str == "free" || str == "free action")
			{
				return ActionType.Free;
			}
			if (str == "immediate interrupt")
			{
				return ActionType.Interrupt;
			}
			if (str == "immediate reaction")
			{
				return ActionType.Reaction;
			}
			return ActionType.Standard;
		}

		private static CreaturePower parse_power(XmlNode node)
		{
			CreaturePower creaturePower = new CreaturePower();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.Name != "b")
				{
					continue;
				}
				creaturePower.Name = childNode.FirstChild.Value.Trim();
				break;
			}
			string str = "";
			bool flag = false;
			foreach (XmlNode xmlNodes in node.ChildNodes)
			{
				if (xmlNodes.Name == "img")
				{
					string value = xmlNodes.Attributes["src"].Value;
					if (value.EndsWith("S2.gif"))
					{
						creaturePower.Action = new PowerAction()
						{
							Use = PowerUseType.Basic
						};
						flag = true;
					}
					else if (value.EndsWith("x.gif"))
					{
						creaturePower.Keywords = xmlNodes.NextSibling.FirstChild.Value;
					}
					else if (!value.EndsWith("Z1a.gif") && !value.EndsWith("Z2a.gif") && !value.EndsWith("Z3a.gif") && !value.EndsWith("Z4a.gif"))
					{
						str = string.Concat(str, " ", value.Substring(value.Length - 6, 1));
					}
				}
				if (xmlNodes.Name != "#text")
				{
					continue;
				}
				string str1 = xmlNodes.Value.Trim();
				str = string.Concat(str, str1);
			}
			if (str != "")
			{
				str = str.Trim();
				str = str.Substring(1, str.Length - 2);
				str = str.Trim();
				str = str.Replace(",", ";");
				string[] strArrays = new string[] { ";" };
				List<string> strs = new List<string>(str.Split(strArrays, StringSplitOptions.RemoveEmptyEntries));
				if (strs.Count != 0)
				{
					for (int i = 0; i != strs.Count; i++)
					{
						strs[i] = strs[i].Trim();
					}
					string lower = strs[0].ToLower();
					if (!lower.StartsWith("standard") && !lower.StartsWith("move") && !lower.StartsWith("minor") && !lower.StartsWith("free") && !lower.StartsWith("immediate"))
					{
						creaturePower.Condition = strs[0];
						strs.RemoveAt(0);
					}
					if (strs.Count != 0 && creaturePower.Action == null)
					{
						creaturePower.Action = new PowerAction()
						{
							Action = ActionType.None
						};
					}
					for (int j = 0; j != strs.Count; j++)
					{
						string item = strs[j];
						string lower1 = item.ToLower();
						if (lower1.StartsWith("standard"))
						{
							creaturePower.Action.Action = ActionType.Standard;
						}
						else if (lower1.StartsWith("move"))
						{
							creaturePower.Action.Action = ActionType.Move;
						}
						else if (lower1.StartsWith("minor"))
						{
							creaturePower.Action.Action = ActionType.Minor;
						}
						else if (lower1.StartsWith("free"))
						{
							creaturePower.Action.Action = ActionType.Free;
						}
						else if (lower1 == "immediate interrupt")
						{
							creaturePower.Action.Action = ActionType.Interrupt;
						}
						else if (lower1 == "immediate reaction")
						{
							creaturePower.Action.Action = ActionType.Reaction;
						}
						else if (lower1 == "at-will")
						{
							if (!flag)
							{
								creaturePower.Action.Use = PowerUseType.AtWill;
							}
						}
						else if (lower1 == "encounter")
						{
							creaturePower.Action.Use = PowerUseType.Encounter;
						}
						else if (lower1 != "daily")
						{
							string str2 = "recharge ";
							if (lower1.StartsWith(str2))
							{
								creaturePower.Action.Use = PowerUseType.Encounter;
								string str3 = item.Substring(str2.Length);
								if (str3 == "6")
								{
									creaturePower.Action.Recharge = "Recharges on 6";
								}
								else if (str3 == "5 6")
								{
									creaturePower.Action.Recharge = "Recharges on 5-6";
								}
								else if (str3 == "4 5 6")
								{
									creaturePower.Action.Recharge = "Recharges on 4-6";
								}
								else if (str3 == "3 4 5 6")
								{
									creaturePower.Action.Recharge = "Recharges on 3-6";
								}
								else if (str3 != "2 3 4 5 6")
								{
									creaturePower.Action.Recharge = item;
								}
								else
								{
									creaturePower.Action.Recharge = "Recharges on 2-6";
								}
							}
							else if (!lower1.StartsWith("recharge"))
							{
								string str4 = "sustain ";
								if (lower1.StartsWith(str4))
								{
									try
									{
										string str5 = item.Substring(str4.Length);
										str5 = string.Concat(char.ToUpper(str5[0]), str5.Substring(1));
										ActionType actionType = (ActionType)Enum.Parse(typeof(ActionType), str5);
										creaturePower.Action.SustainAction = actionType;
									}
									catch
									{
									}
								}
								else if (lower1.StartsWith("when") || lower1.StartsWith("if"))
								{
									creaturePower.Action.Trigger = item;
								}
								else
								{
									creaturePower.Condition = item;
								}
							}
							else
							{
								creaturePower.Action.Recharge = item;
							}
						}
						else
						{
							creaturePower.Action.Use = PowerUseType.Daily;
						}
					}
				}
			}
			return creaturePower;
		}

		private static string simplify_html(string source)
		{
			int num = source.IndexOf("<div id=\"detail\">", StringComparison.OrdinalIgnoreCase);
			if (num == -1)
			{
				return "";
			}
			int num1 = source.IndexOf("</div>", num) + 6;
			source = source.Substring(num, num1 - num);
			source = source.Replace("<br>", "<br/>");
			source = source.Replace("<BR>", "<BR/>");
			source = source.Replace("&nbsp;", " ");
			while (true)
			{
				string str = "href=\"";
				int num2 = source.IndexOf(str);
				if (num2 == -1)
				{
					break;
				}
				int num3 = source.IndexOf("\"", num2 + str.Length);
				int num4 = num3 - num2 + 1;
				string str1 = source.Substring(num2, num4);
				source = source.Replace(str1, "");
			}
			return source;
		}
	}
}