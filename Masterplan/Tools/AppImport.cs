using Masterplan.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Xml;
using Utils;

namespace Masterplan.Tools
{
	internal class AppImport
	{
		public AppImport()
		{
		}

		private static XmlNode get_stat_node(XmlNode parent, string name)
		{
			XmlNode xmlNodes;
			XmlNode xmlNodes1 = XMLHelper.FindChildWithAttribute(parent, "name", name);
			if (xmlNodes1 != null)
			{
				return xmlNodes1;
			}
			IEnumerator enumerator = parent.ChildNodes.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					XmlNode current = (XmlNode)enumerator.Current;
					xmlNodes1 = XMLHelper.FindChildWithAttribute(current, "name", name);
					if (xmlNodes1 == null)
					{
						continue;
					}
					xmlNodes = current;
					return xmlNodes;
				}
				return null;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return xmlNodes;
		}

		private static void import_power(XmlNode power_node, Creature c)
		{
			try
			{
				CreaturePower creaturePower = new CreaturePower()
				{
					Name = XMLHelper.NodeText(power_node, "Name")
				};
				string str = XMLHelper.NodeText(power_node, "Requirements");
				if (str != "")
				{
					creaturePower.Condition = str;
				}
				if (XMLHelper.NodeText(power_node, "Type") == "Trait")
				{
					XmlNode xmlNodes = XMLHelper.FindChild(power_node, "Range");
					if (xmlNodes != null)
					{
						int intAttribute = XMLHelper.GetIntAttribute(xmlNodes, "FinalValue");
						string str1 = XMLHelper.NodeText(power_node, "Details");
						if (intAttribute != 0)
						{
							Aura aura = new Aura()
							{
								Name = creaturePower.Name,
								Details = string.Concat(intAttribute, " ", str1)
							};
							c.Auras.Add(aura);
							return;
						}
						else
						{
							creaturePower.Action = null;
							creaturePower.Details = str1;
						}
					}
				}
				else
				{
					string str2 = XMLHelper.NodeText(power_node, "Action");
					string str3 = XMLHelper.NodeText(power_node, "IsBasic");
					string str4 = XMLHelper.NodeText(power_node, "Usage");
					creaturePower.Action = new PowerAction();
					if (str3 == "true")
					{
						creaturePower.Action.Use = PowerUseType.Basic;
					}
					else if (!str4.StartsWith("At-Will"))
					{
						creaturePower.Action.Use = PowerUseType.Encounter;
						if (!str4.StartsWith("Encounter") && str4.ToLower().StartsWith("recharge"))
						{
							string str5 = XMLHelper.NodeText(power_node, "UsageDetails");
							if (str5 == "")
							{
								str5 = "Recharges on 6";
							}
							if (str5 == "2")
							{
								str5 = "Recharges on 2-6";
							}
							if (str5 == "3")
							{
								str5 = "Recharges on 3-6";
							}
							if (str5 == "4")
							{
								str5 = "Recharges on 4-6";
							}
							if (str5 == "5")
							{
								str5 = "Recharges on 5-6";
							}
							if (str5 == "6")
							{
								str5 = "Recharges on 6";
							}
							creaturePower.Action.Recharge = str5;
						}
					}
					else
					{
						creaturePower.Action.Use = PowerUseType.AtWill;
					}
					if (str2.ToLower().StartsWith("standard"))
					{
						creaturePower.Action.Action = ActionType.Standard;
					}
					if (str2.ToLower().StartsWith("move"))
					{
						creaturePower.Action.Action = ActionType.Move;
					}
					if (str2.ToLower().StartsWith("minor"))
					{
						creaturePower.Action.Action = ActionType.Minor;
					}
					if (str2.ToLower().StartsWith("immediate interrupt"))
					{
						creaturePower.Action.Action = ActionType.Interrupt;
					}
					if (str2.ToLower().StartsWith("immediate reaction"))
					{
						creaturePower.Action.Action = ActionType.Reaction;
					}
					if (str2.ToLower().StartsWith("opportunity"))
					{
						creaturePower.Action.Action = ActionType.Opportunity;
					}
					if (str2.ToLower().StartsWith("free"))
					{
						creaturePower.Action.Action = ActionType.Free;
					}
					if (str2.ToLower().StartsWith("none"))
					{
						creaturePower.Action.Action = ActionType.None;
					}
					if (str2.ToLower().StartsWith("no action"))
					{
						creaturePower.Action.Action = ActionType.None;
					}
					if (str2 == "")
					{
						creaturePower.Action.Action = ActionType.None;
					}
				}
				if (creaturePower.Action != null)
				{
					creaturePower.Action.Trigger = XMLHelper.NodeText(power_node, "Trigger");
				}
				if (creaturePower.Action != null && creaturePower.Action.Trigger != "")
				{
					string str6 = creaturePower.Action.Trigger.Trim();
					if (str6.StartsWith(", "))
					{
						str6 = str6.Substring(2);
					}
					if (str6.StartsWith("; "))
					{
						str6 = str6.Substring(2);
					}
					if (str6.StartsWith("("))
					{
						str6 = str6.Substring(1);
					}
					if (str6.EndsWith(")"))
					{
						str6 = str6.Substring(0, str6.Length - 1);
					}
					creaturePower.Action.Trigger = str6;
				}
				XmlNode xmlNodes1 = XMLHelper.FindChild(power_node, "Keywords");
				if (xmlNodes1 != null)
				{
					foreach (XmlNode childNode in xmlNodes1.ChildNodes)
					{
						XmlNode xmlNodes2 = XMLHelper.FindChild(childNode, "ReferencedObject");
						string str7 = XMLHelper.NodeText(xmlNodes2, "Name");
						if (str7 == "")
						{
							continue;
						}
						if (creaturePower.Keywords != "")
						{
							CreaturePower creaturePower1 = creaturePower;
							creaturePower1.Keywords = string.Concat(creaturePower1.Keywords, ", ");
						}
						CreaturePower creaturePower2 = creaturePower;
						creaturePower2.Keywords = string.Concat(creaturePower2.Keywords, str7);
					}
				}
				XmlNode xmlNodes3 = XMLHelper.FindChild(power_node, "Attacks");
				if (xmlNodes3 == null)
				{
					creaturePower.Details = XMLHelper.NodeText(power_node, "Details");
				}
				else
				{
					string lower = "";
					string innerText = "";
					string str8 = "";
					string str9 = "";
					foreach (XmlNode childNode1 in xmlNodes3.ChildNodes)
					{
						XmlNode xmlNodes4 = XMLHelper.FindChild(childNode1, "AttackBonuses");
						bool flag = (xmlNodes4 == null ? false : xmlNodes4.ChildNodes.Count != 0);
						foreach (XmlNode childNode2 in childNode1.ChildNodes)
						{
							if (childNode2.Name == "Name")
							{
								continue;
							}
							if (childNode2.Name == "Range")
							{
								lower = childNode2.InnerText.ToLower();
								lower = lower.Replace("basic ", "");
							}
							else if (childNode2.Name == "Targets")
							{
								innerText = childNode2.InnerText;
							}
							else if (childNode2.Name == "AttackBonuses")
							{
								if (childNode2.FirstChild == null)
								{
									continue;
								}
								int num = XMLHelper.GetIntAttribute(childNode2.FirstChild, "FinalValue");
								XmlNode xmlNodes5 = XMLHelper.FindChild(childNode2.FirstChild, "Defense");
								XmlNode xmlNodes6 = XMLHelper.FindChild(XMLHelper.FindChild(xmlNodes5, "ReferencedObject"), "DefenseName");
								string innerText1 = xmlNodes6.InnerText;
								creaturePower.Attack = new PowerAttack()
								{
									Bonus = num,
									Defence = (DefenceType)Enum.Parse(typeof(DefenceType), innerText1)
								};
							}
							else if (childNode2.Name == "Description")
							{
								creaturePower.Description = childNode2.InnerText;
							}
							else if (childNode2.Name != "Damage")
							{
								string str10 = XMLHelper.NodeText(childNode2, "Name");
								if (str10 == "")
								{
									str10 = "Hit";
								}
								if (!flag && (str10 == "Hit" || str10 == "Miss"))
								{
									continue;
								}
								XmlNode xmlNodes7 = XMLHelper.FindChild(childNode2, "Damage");
								if (xmlNodes7 != null)
								{
									str8 = XMLHelper.NodeText(xmlNodes7, "Expression");
								}
								string str11 = XMLHelper.NodeText(childNode2, "Description");
								if (str8 != "" && str11 == "")
								{
									str11 = "damage";
								}
								if (str11 == "")
								{
									continue;
								}
								string[] strArrays = new string[] { str10, ": ", str8, " ", str11 };
								string str12 = string.Concat(strArrays);
								string str13 = XMLHelper.NodeText(childNode2, "Special");
								if (str13 != "")
								{
									str12 = string.Concat(str12, Environment.NewLine, "Special: ", str13);
								}
								XmlNode xmlNodes8 = XMLHelper.FindChild(childNode2, "Attacks");
								if (xmlNodes8 != null)
								{
									foreach (XmlNode childNode3 in xmlNodes8.ChildNodes)
									{
										string str14 = AppImport.secondary_attack(childNode3);
										if (str14 == "")
										{
											continue;
										}
										str12 = string.Concat(str12, Environment.NewLine);
										str12 = string.Concat(str12, str14);
									}
								}
								if (str9 != "")
								{
									str9 = string.Concat(str9, "\n");
								}
								str9 = string.Concat(str9, str12);
							}
							else
							{
								str8 = Statistics.NormalDamage(c.Level);
							}
						}
					}
					string str15 = lower;
					if (innerText != "")
					{
						str15 = string.Concat(str15, " (", innerText, ")");
					}
					creaturePower.Range = str15;
					creaturePower.Details = str9;
				}
				c.CreaturePowers.Add(creaturePower);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		public static Creature ImportCreature(string xml)
		{
			object movement;
			object[] objArray;
			Creature creature = null;
			try
			{
				XmlDocument xmlDocument = XMLHelper.LoadSource(xml);
				if (xmlDocument != null)
				{
					XmlNode documentElement = xmlDocument.DocumentElement;
					creature = new Creature();
					bool flag = false;
					foreach (XmlNode childNode in documentElement.ChildNodes)
					{
						if (childNode.Name == "ID")
						{
							continue;
						}
						if (childNode.Name == "AbilityScores")
						{
							try
							{
								foreach (XmlNode xmlNodes in childNode.FirstChild.ChildNodes)
								{
									string str = XMLHelper.NodeText(xmlNodes, "Name");
									int intAttribute = XMLHelper.GetIntAttribute(xmlNodes, "FinalValue");
									intAttribute = Math.Max(intAttribute, 0);
									if (str == "Strength")
									{
										creature.Strength.Score = intAttribute;
									}
									if (str == "Constitution")
									{
										creature.Constitution.Score = intAttribute;
									}
									if (str == "Dexterity")
									{
										creature.Dexterity.Score = intAttribute;
									}
									if (str == "Intelligence")
									{
										creature.Intelligence.Score = intAttribute;
									}
									if (str == "Wisdom")
									{
										creature.Wisdom.Score = intAttribute;
									}
									if (str != "Charisma")
									{
										continue;
									}
									creature.Charisma.Score = intAttribute;
								}
							}
							catch
							{
								LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
							}
						}
						else if (childNode.Name != "Defenses")
						{
							if (childNode.Name == "AttackBonuses")
							{
								continue;
							}
							if (childNode.Name == "Skills")
							{
								try
								{
									string str1 = "";
									foreach (XmlNode childNode1 in childNode.FirstChild.ChildNodes)
									{
										string str2 = XMLHelper.NodeText(childNode1, "Name");
										int num = XMLHelper.GetIntAttribute(childNode1, "FinalValue");
										bool flag1 = false;
										string str3 = XMLHelper.NodeText(childNode1, "Trained");
										if (str3 != "")
										{
											flag1 = bool.Parse(str3);
										}
										if (!flag1)
										{
											continue;
										}
										if (str1 != "")
										{
											str1 = string.Concat(str1, ", ");
										}
										string str4 = (num >= 0 ? "+" : "");
										movement = str1;
										objArray = new object[] { movement, str2, " ", str4, num };
										str1 = string.Concat(objArray);
									}
									creature.Skills = str1;
								}
								catch
								{
									LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
								}
							}
							else if (childNode.Name == "Name")
							{
								try
								{
									creature.Name = childNode.InnerText;
								}
								catch
								{
									LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
								}
							}
							else if (childNode.Name == "Level")
							{
								try
								{
									creature.Level = int.Parse(childNode.InnerText);
								}
								catch
								{
									LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
								}
							}
							else if (childNode.Name == "Size")
							{
								try
								{
									XmlNode xmlNodes1 = XMLHelper.FindChild(childNode, "ReferencedObject");
									if (xmlNodes1 != null)
									{
										XmlNode xmlNodes2 = XMLHelper.FindChild(xmlNodes1, "Name");
										if (xmlNodes2 != null)
										{
											string innerText = xmlNodes2.InnerText;
											creature.Size = (CreatureSize)Enum.Parse(typeof(CreatureSize), innerText);
										}
									}
								}
								catch
								{
									LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
								}
							}
							else if (childNode.Name == "Origin")
							{
								try
								{
									XmlNode xmlNodes3 = XMLHelper.FindChild(childNode, "ReferencedObject");
									if (xmlNodes3 != null)
									{
										XmlNode xmlNodes4 = XMLHelper.FindChild(xmlNodes3, "Name");
										if (xmlNodes4 != null)
										{
											string innerText1 = xmlNodes4.InnerText;
											creature.Origin = (CreatureOrigin)Enum.Parse(typeof(CreatureOrigin), innerText1);
										}
									}
								}
								catch
								{
									LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
								}
							}
							else if (childNode.Name == "Type")
							{
								try
								{
									XmlNode xmlNodes5 = XMLHelper.FindChild(childNode, "ReferencedObject");
									if (xmlNodes5 != null)
									{
										XmlNode xmlNodes6 = XMLHelper.FindChild(xmlNodes5, "Name");
										if (xmlNodes6 != null)
										{
											string str5 = xmlNodes6.InnerText.Replace(" ", "");
											creature.Type = (CreatureType)Enum.Parse(typeof(CreatureType), str5);
										}
									}
								}
								catch
								{
									LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
								}
							}
							else if (childNode.Name == "GroupRole")
							{
								try
								{
									XmlNode xmlNodes7 = XMLHelper.FindChild(childNode, "ReferencedObject");
									if (xmlNodes7 != null)
									{
										XmlNode xmlNodes8 = XMLHelper.FindChild(xmlNodes7, "Name");
										if (xmlNodes8 != null)
										{
											string innerText2 = xmlNodes8.InnerText;
											if (innerText2 != "Minion")
											{
												RoleFlag roleFlag = (RoleFlag)Enum.Parse(typeof(RoleFlag), innerText2);
												(creature.Role as ComplexRole).Flag = roleFlag;
											}
											else
											{
												Minion minion = new Minion();
												if (flag)
												{
													ComplexRole role = creature.Role as ComplexRole;
													minion.HasRole = true;
													minion.Type = role.Type;
												}
												creature.Role = minion;
											}
										}
									}
								}
								catch
								{
									LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
								}
							}
							else if (childNode.Name == "Role")
							{
								try
								{
									XmlNode xmlNodes9 = XMLHelper.FindChild(childNode, "ReferencedObject");
									if (xmlNodes9 != null)
									{
										XmlNode xmlNodes10 = XMLHelper.FindChild(xmlNodes9, "Name");
										if (xmlNodes10 != null)
										{
											string innerText3 = xmlNodes10.InnerText;
											RoleType roleType = (RoleType)Enum.Parse(typeof(RoleType), innerText3);
											if (creature.Role is ComplexRole)
											{
												(creature.Role as ComplexRole).Type = roleType;
											}
											if (creature.Role is Minion)
											{
												Minion role1 = creature.Role as Minion;
												role1.HasRole = true;
												role1.Type = roleType;
											}
											flag = true;
										}
									}
								}
								catch
								{
									LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
								}
							}
							else if (childNode.Name == "IsLeader")
							{
								try
								{
									bool flag2 = bool.Parse(childNode.InnerText);
									if (creature.Role is ComplexRole && flag2)
									{
										(creature.Role as ComplexRole).Leader = true;
									}
								}
								catch
								{
									LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
								}
							}
							else if (childNode.Name == "Items")
							{
								try
								{
									foreach (XmlNode childNode2 in childNode.ChildNodes)
									{
										XmlNode xmlNodes11 = XMLHelper.FindChild(childNode2, "Item");
										XmlNode xmlNodes12 = XMLHelper.FindChild(XMLHelper.FindChild(xmlNodes11, "ReferencedObject"), "Name");
										string innerText4 = xmlNodes12.InnerText;
										int num1 = int.Parse(XMLHelper.NodeText(childNode2, "Quantity"));
										if (creature.Equipment != "")
										{
											Creature creature1 = creature;
											creature1.Equipment = string.Concat(creature1.Equipment, ", ");
										}
										Creature creature2 = creature;
										creature2.Equipment = string.Concat(creature2.Equipment, innerText4);
										if (num1 == 1)
										{
											continue;
										}
										Creature creature3 = creature;
										creature3.Equipment = string.Concat(creature3.Equipment, " x", num1);
									}
								}
								catch
								{
									LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
								}
							}
							else if (childNode.Name == "Languages")
							{
								try
								{
									string str6 = "";
									foreach (XmlNode childNode3 in childNode.ChildNodes)
									{
										XmlNode xmlNodes13 = XMLHelper.FindChild(childNode3, "ReferencedObject");
										if (xmlNodes13 == null)
										{
											continue;
										}
										XmlNode xmlNodes14 = XMLHelper.FindChild(xmlNodes13, "Name");
										if (xmlNodes14 == null)
										{
											continue;
										}
										string innerText5 = xmlNodes14.InnerText;
										if (str6 != "")
										{
											str6 = string.Concat(str6, ", ");
										}
										str6 = string.Concat(str6, innerText5);
									}
									creature.Languages = str6;
								}
								catch
								{
									LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
								}
							}
							else if (childNode.Name == "Senses")
							{
								try
								{
									string str7 = "";
									foreach (XmlNode childNode4 in childNode.ChildNodes)
									{
										XmlNode xmlNodes15 = XMLHelper.FindChild(childNode4, "ReferencedObject");
										if (xmlNodes15 == null)
										{
											continue;
										}
										string str8 = XMLHelper.NodeText(xmlNodes15, "Name");
										string str9 = XMLHelper.NodeText(childNode4, "Range");
										if (str9 != "" && str9 != "0")
										{
											str8 = string.Concat(str8, " ", str9);
										}
										if (str7 != "")
										{
											str7 = string.Concat(str7, ", ");
										}
										str7 = string.Concat(str7, str8);
									}
									if (str7 != "")
									{
										if (creature.Senses != "")
										{
											Creature creature4 = creature;
											creature4.Senses = string.Concat(creature4.Senses, "; ");
										}
										Creature creature5 = creature;
										creature5.Senses = string.Concat(creature5.Senses, str7);
									}
								}
								catch
								{
									LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
								}
							}
							else if (childNode.Name == "Regeneration")
							{
								try
								{
									Regeneration regeneration = new Regeneration()
									{
										Value = XMLHelper.GetIntAttribute(childNode, "FinalValue")
									};
									string str10 = XMLHelper.NodeText(childNode, "Details");
									if (str10 != "")
									{
										regeneration.Details = str10;
									}
									if (regeneration.Value != 0 || regeneration.Details != "")
									{
										creature.Regeneration = regeneration;
									}
								}
								catch
								{
									LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
								}
							}
							else if (childNode.Name == "Keywords")
							{
								try
								{
									string str11 = "";
									foreach (XmlNode childNode5 in childNode.ChildNodes)
									{
										XmlNode xmlNodes16 = XMLHelper.FindChild(childNode5, "ReferencedObject");
										if (xmlNodes16 != null)
										{
											XmlNode xmlNodes17 = XMLHelper.FindChild(xmlNodes16, "Name");
											if (xmlNodes17 != null)
											{
												string innerText6 = xmlNodes17.InnerText;
												if (str11 != "")
												{
													str11 = string.Concat(str11, ", ");
												}
												str11 = string.Concat(str11, innerText6);
											}
										}
										creature.Keywords = str11;
									}
								}
								catch
								{
									LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
								}
							}
							else if (childNode.Name == "Alignment")
							{
								try
								{
									XmlNode xmlNodes18 = XMLHelper.FindChild(childNode, "ReferencedObject");
									if (xmlNodes18 != null)
									{
										XmlNode xmlNodes19 = XMLHelper.FindChild(xmlNodes18, "Name");
										if (xmlNodes19 != null)
										{
											creature.Alignment = xmlNodes19.InnerText;
										}
									}
								}
								catch
								{
									LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
								}
							}
							else if (childNode.Name == "Powers")
							{
								try
								{
									foreach (XmlNode childNode6 in childNode.ChildNodes)
									{
										AppImport.import_power(childNode6, creature);
									}
								}
								catch
								{
									LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
								}
							}
							else if (childNode.Name == "Initiative")
							{
								try
								{
									creature.Initiative = XMLHelper.GetIntAttribute(childNode, "FinalValue");
								}
								catch
								{
									LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
								}
							}
							else if (childNode.Name != "HitPoints")
							{
								if (childNode.Name == "ActionPoints" || childNode.Name == "Experience" || childNode.Name == "Auras")
								{
									continue;
								}
								if (childNode.Name == "LandSpeed")
								{
									try
									{
										XmlNode xmlNodes20 = XMLHelper.FindChild(childNode, "Speed");
										int intAttribute1 = XMLHelper.GetIntAttribute(xmlNodes20, "FinalValue");
										creature.Movement = intAttribute1.ToString();
										string innerText7 = "";
										XmlNode xmlNodes21 = XMLHelper.FindChild(childNode, "Details");
										if (xmlNodes21 != null)
										{
											innerText7 = xmlNodes21.InnerText;
										}
										if (innerText7 != "")
										{
											Creature creature6 = creature;
											creature6.Movement = string.Concat(creature6.Movement, " ", innerText7);
										}
									}
									catch
									{
										LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
									}
								}
								else if (childNode.Name != "Speeds")
								{
									if (childNode.Name == "SavingThrows")
									{
										continue;
									}
									if (childNode.Name == "Resistances")
									{
										try
										{
											string str12 = "";
											foreach (XmlNode childNode7 in childNode.ChildNodes)
											{
												XmlNode xmlNodes22 = XMLHelper.FindChild(XMLHelper.FindChild(childNode7, "ReferencedObject"), "Name");
												XmlNode xmlNodes23 = XMLHelper.FindChild(childNode7, "Amount");
												XmlNode xmlNodes24 = XMLHelper.FindChild(childNode7, "Details");
												string innerText8 = xmlNodes22.InnerText;
												int intAttribute2 = XMLHelper.GetIntAttribute(xmlNodes23, "FinalValue");
												string str13 = (xmlNodes24 != null ? xmlNodes24.InnerText : "");
												if (str13 == "")
												{
													DamageModifier damageModifier = DamageModifier.Parse(innerText8, -intAttribute2);
													if (damageModifier != null)
													{
														creature.DamageModifiers.Add(damageModifier);
														continue;
													}
												}
												if (innerText8 == "" && str13 == "")
												{
													continue;
												}
												if (str12 != "")
												{
													str12 = string.Concat(str12, ", ");
												}
												string str14 = "";
												if (innerText8 != "0")
												{
													str14 = string.Concat(str14, innerText8);
												}
												if (intAttribute2 <= 0)
												{
													str12 = string.Concat(str12, str14);
												}
												else
												{
													movement = str12;
													objArray = new object[] { movement, str14, " ", intAttribute2 };
													str12 = string.Concat(objArray);
												}
												if (str13 == "")
												{
													continue;
												}
												if (str12 != "")
												{
													str12 = string.Concat(str12, " ");
												}
												str12 = string.Concat(str12, str13);
											}
											creature.Resist = str12;
										}
										catch
										{
											LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
										}
									}
									else if (childNode.Name == "Weaknesses")
									{
										try
										{
											string str15 = "";
											foreach (XmlNode childNode8 in childNode.ChildNodes)
											{
												XmlNode xmlNodes25 = XMLHelper.FindChild(XMLHelper.FindChild(childNode8, "ReferencedObject"), "Name");
												XmlNode xmlNodes26 = XMLHelper.FindChild(childNode8, "Amount");
												XmlNode xmlNodes27 = XMLHelper.FindChild(childNode8, "Details");
												string innerText9 = xmlNodes25.InnerText;
												int num2 = XMLHelper.GetIntAttribute(xmlNodes26, "FinalValue");
												string str16 = (xmlNodes27 != null ? xmlNodes27.InnerText : "");
												if (str16 == "")
												{
													DamageModifier damageModifier1 = DamageModifier.Parse(innerText9, num2);
													if (damageModifier1 != null)
													{
														creature.DamageModifiers.Add(damageModifier1);
														continue;
													}
												}
												if (innerText9 == "" && str16 == "")
												{
													continue;
												}
												if (str15 != "")
												{
													str15 = string.Concat(str15, ", ");
												}
												string str17 = "";
												if (innerText9 != "0")
												{
													str17 = string.Concat(str17, innerText9);
												}
												if (num2 <= 0)
												{
													str15 = string.Concat(str15, str17);
												}
												else
												{
													movement = str15;
													objArray = new object[] { movement, str17, " ", num2 };
													str15 = string.Concat(objArray);
												}
												if (str16 == "")
												{
													continue;
												}
												if (str15 != "")
												{
													str15 = string.Concat(str15, " ");
												}
												str15 = string.Concat(str15, str16);
											}
											creature.Vulnerable = str15;
										}
										catch
										{
											LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
										}
									}
									else if (childNode.Name == "Immunities")
									{
										try
										{
											string str18 = "";
											foreach (XmlNode childNode9 in childNode.ChildNodes)
											{
												XmlNode xmlNodes28 = XMLHelper.FindChild(XMLHelper.FindChild(childNode9, "ReferencedObject"), "Name");
												DamageModifier damageModifier2 = DamageModifier.Parse(xmlNodes28.InnerText, 0);
												if (damageModifier2 == null)
												{
													if (str18 != "")
													{
														str18 = string.Concat(str18, ", ");
													}
													str18 = string.Concat(str18, xmlNodes28.InnerText);
												}
												else
												{
													creature.DamageModifiers.Add(damageModifier2);
												}
											}
											creature.Immune = str18;
										}
										catch
										{
											LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
										}
									}
									else if (childNode.Name != "Tactics")
									{
										if (childNode.Name == "SourceBook" || childNode.Name == "Description" || childNode.Name == "Race" || childNode.Name == "TemplateApplications" || childNode.Name == "EliteUpgradeID" || childNode.Name == "FullPortrait" || childNode.Name == "CompendiumUrl" || childNode.Name == "Phasing" || childNode.Name == "SourceBooks")
										{
											continue;
										}
										LogSystem.Trace(string.Concat("Unhandled XML node: ", childNode.Name));
									}
									else
									{
										try
										{
											creature.Tactics = childNode.InnerText;
										}
										catch
										{
											LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
										}
									}
								}
								else
								{
									try
									{
										foreach (XmlNode childNode10 in childNode.ChildNodes)
										{
											XmlNode xmlNodes29 = XMLHelper.FindChild(childNode10, "ReferencedObject");
											XmlNode xmlNodes30 = XMLHelper.FindChild(childNode10, "Speed");
											XmlNode xmlNodes31 = XMLHelper.FindChild(childNode10, "Details");
											string innerText10 = xmlNodes29.FirstChild.NextSibling.InnerText;
											int intAttribute3 = XMLHelper.GetIntAttribute(xmlNodes30, "FinalValue");
											string str19 = (xmlNodes31 != null ? xmlNodes31.InnerText : "");
											if (creature.Movement != "")
											{
												Creature creature7 = creature;
												creature7.Movement = string.Concat(creature7.Movement, ", ");
											}
											string str20 = "";
											if (innerText10 != "")
											{
												str20 = string.Concat(str20, innerText10);
											}
											if (str19 != "")
											{
												if (str20 != "")
												{
													str20 = string.Concat(str20, " ");
												}
												str20 = string.Concat(str20, str19);
											}
											Creature creature8 = creature;
											movement = creature8.Movement;
											objArray = new object[] { movement, str20, " ", intAttribute3 };
											creature8.Movement = string.Concat(objArray);
										}
									}
									catch
									{
										LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
									}
								}
							}
							else
							{
								try
								{
									creature.HP = XMLHelper.GetIntAttribute(childNode, "FinalValue");
								}
								catch
								{
									LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
								}
							}
						}
						else
						{
							try
							{
								foreach (XmlNode childNode11 in childNode.FirstChild.ChildNodes)
								{
									string str21 = XMLHelper.NodeText(childNode11, "Name");
									int num3 = XMLHelper.GetIntAttribute(childNode11, "FinalValue");
									if (str21 == "AC")
									{
										creature.AC = num3;
									}
									if (str21 == "Fortitude")
									{
										creature.Fortitude = num3;
									}
									if (str21 == "Reflex")
									{
										creature.Reflex = num3;
									}
									if (str21 != "Will")
									{
										continue;
									}
									creature.Will = num3;
								}
							}
							catch
							{
								LogSystem.Trace(string.Concat("Error parsing ", childNode.Name));
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
			}
			return creature;
		}

		public static Hero ImportHero(string xml)
		{
			Hero hero = new Hero();
			try
			{
				xml = xml.Replace("RESISTANCE_+", "RESISTANCE_PLUS");
				xml = xml.Replace("CORMYR!", "CORMYR");
				xml = xml.Replace("SILVER_TONGUE,", "SILVER_TONGUE");
				XmlDocument xmlDocument = XMLHelper.LoadSource(xml);
				if (xmlDocument != null)
				{
					XmlNode xmlNodes = XMLHelper.FindChild(xmlDocument.DocumentElement, "CharacterSheet");
					if (xmlNodes != null)
					{
						XmlNode xmlNodes1 = XMLHelper.FindChild(xmlNodes, "Details");
						if (xmlNodes1 != null)
						{
							hero.Name = XMLHelper.NodeText(xmlNodes1, "name").Trim();
							hero.Player = XMLHelper.NodeText(xmlNodes1, "Player").Trim();
							hero.Level = int.Parse(XMLHelper.NodeText(xmlNodes1, "Level"));
							string str = XMLHelper.NodeText(xmlNodes1, "Portrait").Trim();
							if (str != "")
							{
								try
								{
									string str1 = "file://";
									if (str.StartsWith(str1))
									{
										str = str.Substring(str1.Length);
									}
									if (File.Exists(str))
									{
										hero.Portrait = Image.FromFile(str);
									}
								}
								catch
								{
								}
							}
						}
						XmlNode xmlNodes2 = XMLHelper.FindChild(xmlNodes, "StatBlock");
						if (xmlNodes2 != null)
						{
							XmlNode statNode = AppImport.get_stat_node(xmlNodes2, "Hit Points");
							if (statNode != null)
							{
								hero.HP = XMLHelper.GetIntAttribute(statNode, "value");
							}
							XmlNode statNode1 = AppImport.get_stat_node(xmlNodes2, "AC");
							if (statNode1 != null)
							{
								hero.AC = XMLHelper.GetIntAttribute(statNode1, "value");
							}
							XmlNode statNode2 = AppImport.get_stat_node(xmlNodes2, "Fortitude Defense");
							if (statNode2 != null)
							{
								hero.Fortitude = XMLHelper.GetIntAttribute(statNode2, "value");
							}
							XmlNode statNode3 = AppImport.get_stat_node(xmlNodes2, "Reflex Defense");
							if (statNode3 != null)
							{
								hero.Reflex = XMLHelper.GetIntAttribute(statNode3, "value");
							}
							XmlNode xmlNodes3 = AppImport.get_stat_node(xmlNodes2, "Will Defense");
							if (xmlNodes3 != null)
							{
								hero.Will = XMLHelper.GetIntAttribute(xmlNodes3, "value");
							}
							XmlNode statNode4 = AppImport.get_stat_node(xmlNodes2, "Initiative");
							if (statNode4 != null)
							{
								hero.InitBonus = XMLHelper.GetIntAttribute(statNode4, "value");
							}
							XmlNode xmlNodes4 = AppImport.get_stat_node(xmlNodes2, "Passive Perception");
							if (xmlNodes4 != null)
							{
								hero.PassivePerception = XMLHelper.GetIntAttribute(xmlNodes4, "value");
							}
							XmlNode statNode5 = AppImport.get_stat_node(xmlNodes2, "Passive Insight");
							if (statNode5 != null)
							{
								hero.PassiveInsight = XMLHelper.GetIntAttribute(statNode5, "value");
							}
						}
						XmlNode xmlNodes5 = XMLHelper.FindChild(xmlNodes, "RulesElementTally");
						if (xmlNodes5 != null)
						{
							XmlNode xmlNodes6 = XMLHelper.FindChildWithAttribute(xmlNodes5, "type", "Race");
							if (xmlNodes6 != null)
							{
								hero.Race = XMLHelper.GetAttribute(xmlNodes6, "name");
							}
							XmlNode xmlNodes7 = XMLHelper.FindChildWithAttribute(xmlNodes5, "type", "Class");
							if (xmlNodes7 != null)
							{
								hero.Class = XMLHelper.GetAttribute(xmlNodes7, "name");
							}
							XmlNode xmlNodes8 = XMLHelper.FindChildWithAttribute(xmlNodes5, "type", "Paragon Path");
							if (xmlNodes8 != null)
							{
								hero.ParagonPath = XMLHelper.GetAttribute(xmlNodes8, "name");
							}
							XmlNode xmlNodes9 = XMLHelper.FindChildWithAttribute(xmlNodes5, "type", "Epic Destiny");
							if (xmlNodes9 != null)
							{
								hero.EpicDestiny = XMLHelper.GetAttribute(xmlNodes9, "name");
							}
							XmlNode xmlNodes10 = XMLHelper.FindChildWithAttribute(xmlNodes5, "type", "Role");
							if (xmlNodes10 != null)
							{
								hero.Role = (HeroRoleType)Enum.Parse(typeof(HeroRoleType), XMLHelper.GetAttribute(xmlNodes10, "name"));
							}
							XmlNode xmlNodes11 = XMLHelper.FindChildWithAttribute(xmlNodes5, "type", "Power Source");
							if (xmlNodes11 != null)
							{
								hero.PowerSource = XMLHelper.GetAttribute(xmlNodes11, "name");
							}
							foreach (XmlNode xmlNodes12 in XMLHelper.FindChildrenWithAttribute(xmlNodes5, "type", "Language"))
							{
								string attribute = XMLHelper.GetAttribute(xmlNodes12, "name");
								if (attribute == "")
								{
									continue;
								}
								if (hero.Languages != "")
								{
									Hero hero1 = hero;
									hero1.Languages = string.Concat(hero1.Languages, ", ");
								}
								Hero hero2 = hero;
								hero2.Languages = string.Concat(hero2.Languages, attribute);
							}
						}
					}
					XmlNode xmlNodes13 = XMLHelper.FindChild(xmlDocument.DocumentElement, "Level");
					if (xmlNodes13 != null)
					{
						XmlNode xmlNodes14 = XMLHelper.FindChildWithAttribute(xmlNodes13, "name", "1");
						if (xmlNodes14 != null)
						{
							if (hero.Race == "")
							{
								XmlNode xmlNodes15 = XMLHelper.FindChildWithAttribute(xmlNodes14, "type", "Race");
								if (xmlNodes15 != null)
								{
									hero.Race = XMLHelper.GetAttribute(xmlNodes15, "name");
								}
							}
							if (hero.Class == "")
							{
								XmlNode xmlNodes16 = XMLHelper.FindChildWithAttribute(xmlNodes14, "type", "Class");
								if (xmlNodes16 != null)
								{
									hero.Class = XMLHelper.GetAttribute(xmlNodes16, "name");
								}
							}
						}
						XmlNode xmlNodes17 = XMLHelper.FindChildWithAttribute(xmlNodes13, "name", "11");
						if (xmlNodes17 != null && hero.ParagonPath == "")
						{
							XmlNode xmlNodes18 = XMLHelper.FindChildWithAttribute(xmlNodes17, "type", "ParagonPath");
							if (xmlNodes18 != null)
							{
								hero.ParagonPath = XMLHelper.GetAttribute(xmlNodes18, "name");
							}
						}
						XmlNode xmlNodes19 = XMLHelper.FindChildWithAttribute(xmlNodes13, "name", "21");
						if (xmlNodes19 != null && hero.EpicDestiny == "")
						{
							XmlNode xmlNodes20 = XMLHelper.FindChildWithAttribute(xmlNodes19, "type", "EpicDestiny");
							if (xmlNodes20 != null)
							{
								hero.EpicDestiny = XMLHelper.GetAttribute(xmlNodes20, "name");
							}
						}
					}
				}
				else
				{
					return null;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
			return hero;
		}

		public static bool ImportIPlay4e(Hero hero)
		{
			if (hero.Key == null || hero.Key == "")
			{
				return false;
			}
			bool flag = true;
			try
			{
				string str = string.Concat("http://iplay4e.appspot.com/view?xsl=jPint&key=", hero.Key);
				string str1 = (new WebClient()).DownloadString(str);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(str1);
				XmlNode documentElement = xmlDocument.DocumentElement;
				hero.Name = XMLHelper.GetAttribute(documentElement, "name");
				XmlNode xmlNodes = XMLHelper.FindChild(documentElement, "Build");
				if (xmlNodes != null)
				{
					hero.Level = XMLHelper.GetIntAttribute(xmlNodes, "level");
					try
					{
						string attribute = XMLHelper.GetAttribute(xmlNodes, "role");
						hero.Role = (HeroRoleType)Enum.Parse(typeof(HeroRoleType), attribute);
					}
					catch
					{
					}
					try
					{
						string attribute1 = XMLHelper.GetAttribute(xmlNodes, "size");
						hero.Size = (CreatureSize)Enum.Parse(typeof(CreatureSize), attribute1);
					}
					catch
					{
					}
					hero.PowerSource = XMLHelper.GetAttribute(xmlNodes, "powersource");
					hero.Class = XMLHelper.GetAttribute(xmlNodes, "name");
					XmlNode xmlNodes1 = XMLHelper.FindChild(xmlNodes, "Race");
					if (xmlNodes1 != null)
					{
						hero.Race = XMLHelper.GetAttribute(xmlNodes1, "name");
					}
					XmlNode xmlNodes2 = XMLHelper.FindChild(xmlNodes, "ParagonPath");
					if (xmlNodes2 != null)
					{
						hero.ParagonPath = XMLHelper.GetAttribute(xmlNodes2, "name");
					}
					XmlNode xmlNodes3 = XMLHelper.FindChild(xmlNodes, "EpicDestiny");
					if (xmlNodes3 != null)
					{
						hero.EpicDestiny = XMLHelper.GetAttribute(xmlNodes3, "name");
					}
				}
				XmlNode xmlNodes4 = XMLHelper.FindChild(documentElement, "Health");
				if (xmlNodes4 != null)
				{
					XmlNode xmlNodes5 = XMLHelper.FindChild(xmlNodes4, "MaxHitPoints");
					if (xmlNodes5 != null)
					{
						hero.HP = XMLHelper.GetIntAttribute(xmlNodes5, "value");
					}
				}
				XmlNode xmlNodes6 = XMLHelper.FindChild(documentElement, "Movement");
				if (xmlNodes6 != null)
				{
					XmlNode xmlNodes7 = XMLHelper.FindChild(xmlNodes6, "Initiative");
					if (xmlNodes7 != null)
					{
						hero.InitBonus = XMLHelper.GetIntAttribute(xmlNodes7, "value");
					}
				}
				XmlNode xmlNodes8 = XMLHelper.FindChild(documentElement, "Defenses");
				if (xmlNodes8 != null)
				{
					XmlNode xmlNodes9 = XMLHelper.FindChildWithAttribute(xmlNodes8, "abbreviation", "AC");
					if (xmlNodes9 != null)
					{
						hero.AC = XMLHelper.GetIntAttribute(xmlNodes9, "value");
					}
					XmlNode xmlNodes10 = XMLHelper.FindChildWithAttribute(xmlNodes8, "abbreviation", "Fort");
					if (xmlNodes10 != null)
					{
						hero.Fortitude = XMLHelper.GetIntAttribute(xmlNodes10, "value");
					}
					XmlNode xmlNodes11 = XMLHelper.FindChildWithAttribute(xmlNodes8, "abbreviation", "Ref");
					if (xmlNodes11 != null)
					{
						hero.Reflex = XMLHelper.GetIntAttribute(xmlNodes11, "value");
					}
					XmlNode xmlNodes12 = XMLHelper.FindChildWithAttribute(xmlNodes8, "abbreviation", "Will");
					if (xmlNodes12 != null)
					{
						hero.Will = XMLHelper.GetIntAttribute(xmlNodes12, "value");
					}
				}
				XmlNode xmlNodes13 = XMLHelper.FindChild(documentElement, "PassiveSkills");
				if (xmlNodes13 != null)
				{
					XmlNode xmlNodes14 = XMLHelper.FindChildWithAttribute(xmlNodes13, "name", "Insight");
					if (xmlNodes14 != null)
					{
						hero.PassiveInsight = XMLHelper.GetIntAttribute(xmlNodes14, "value");
					}
					XmlNode xmlNodes15 = XMLHelper.FindChildWithAttribute(xmlNodes13, "name", "Perception");
					if (xmlNodes15 != null)
					{
						hero.PassivePerception = XMLHelper.GetIntAttribute(xmlNodes15, "value");
					}
				}
				XmlNode xmlNodes16 = XMLHelper.FindChild(documentElement, "Languages");
				if (xmlNodes16 != null)
				{
					string str2 = "";
					foreach (XmlNode childNode in xmlNodes16.ChildNodes)
					{
						string attribute2 = XMLHelper.GetAttribute(childNode, "name");
						if (str2 != "")
						{
							str2 = string.Concat(str2, ", ");
						}
						str2 = string.Concat(str2, attribute2);
					}
					hero.Languages = str2;
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		public static List<Hero> ImportParty(string key)
		{
			List<Hero> heros = new List<Hero>();
			try
			{
				string str = string.Concat("http://iplay4e.appspot.com/campaigns/", key, "/main");
				string str1 = (new WebClient()).DownloadString(str);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(str1);
				XmlNode documentElement = xmlDocument.DocumentElement;
				if (documentElement != null)
				{
					XmlNode xmlNodes = XMLHelper.FindChild(documentElement, "Characters");
					if (xmlNodes != null)
					{
						foreach (XmlNode childNode in xmlNodes.ChildNodes)
						{
							try
							{
								string attribute = XMLHelper.GetAttribute(childNode, "key");
								Hero hero = new Hero()
								{
									Key = attribute
								};
								if (AppImport.ImportIPlay4e(hero))
								{
									heros.Add(hero);
								}
							}
							catch
							{
							}
						}
					}
				}
			}
			catch
			{
			}
			return heros;
		}

		private static string secondary_attack(XmlNode attack_node)
		{
			string str = XMLHelper.NodeText(attack_node, "Name");
			string str1 = XMLHelper.NodeText(attack_node, "Description");
			string str2 = string.Concat(str, ": ", str1);
			string str3 = "";
			string str4 = "";
			string str5 = "";
			foreach (XmlNode childNode in attack_node.ChildNodes)
			{
				string name = childNode.Name;
				string str6 = name;
				if (name == null)
				{
					continue;
				}
				if (str6 == "Hit")
				{
					str3 = AppImport.secondary_attack_details(childNode);
				}
				else if (str6 == "Miss")
				{
					str4 = AppImport.secondary_attack_details(childNode);
				}
				else if (str6 == "Effect")
				{
					str5 = AppImport.secondary_attack_details(childNode);
				}
			}
			if (str3 != "")
			{
				str2 = string.Concat(str2, Environment.NewLine, "Hit: ", str3);
			}
			if (str4 != "")
			{
				str2 = string.Concat(str2, Environment.NewLine, "Miss: ", str4);
			}
			if (str5 != "")
			{
				str2 = string.Concat(str2, Environment.NewLine, "Effect: ", str5);
			}
			return str2;
		}

		private static string secondary_attack_details(XmlNode details_node)
		{
			XmlNode xmlNodes = XMLHelper.FindChild(details_node, "Damage");
			string str = XMLHelper.NodeText(xmlNodes, "Expression");
			string str1 = XMLHelper.NodeText(details_node, "Description");
			if (str != "" && str1 != "")
			{
				return string.Concat(str, " ", str1);
			}
			if (str != "" && str1 == "")
			{
				return string.Concat(str, " damage");
			}
			if (str == "" && str1 != "")
			{
				return str1;
			}
			return "";
		}
	}
}