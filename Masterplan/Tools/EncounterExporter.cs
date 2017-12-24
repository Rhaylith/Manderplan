using Masterplan;
using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.Xml;
using Utils;

namespace Masterplan.Tools
{
	internal class EncounterExporter
	{
		public EncounterExporter()
		{
		}

		public static string ExportXML(Encounter enc)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.AppendChild(xmlDocument.CreateElement("Encounter"));
			XMLHelper.CreateChild(xmlDocument, xmlDocument.DocumentElement, "Source").InnerText = "Masterplan Adventure Design Studio";
			XmlNode xmlNodes = XMLHelper.CreateChild(xmlDocument, xmlDocument.DocumentElement, "Creatures");
			foreach (EncounterSlot slot in enc.Slots)
			{
				ICreature creature = Session.FindCreature(slot.Card.CreatureID, SearchType.Global);
				foreach (CombatData combatDatum in slot.CombatData)
				{
					XmlNode xmlNodes1 = XMLHelper.CreateChild(xmlDocument, xmlNodes, "Creature");
					string str = "";
					if (creature.Role is Minion)
					{
						str = string.Concat(str, "Minion");
					}
					foreach (RoleType role in slot.Card.Roles)
					{
						if (str != "")
						{
							str = string.Concat(str, ", ");
						}
						str = string.Concat(str, role);
					}
					if (slot.Card.Leader)
					{
						str = string.Concat(str, " (L)");
					}
					XMLHelper.CreateChild(xmlDocument, xmlNodes1, "Name").InnerText = combatDatum.DisplayName;
					XMLHelper.CreateChild(xmlDocument, xmlNodes1, "Level").InnerText = slot.Card.Level.ToString();
					XMLHelper.CreateChild(xmlDocument, xmlNodes1, "Role").InnerText = str;
					XMLHelper.CreateChild(xmlDocument, xmlNodes1, "Size").InnerText = creature.Size.ToString();
					XMLHelper.CreateChild(xmlDocument, xmlNodes1, "Type").InnerText = creature.Type.ToString();
					XMLHelper.CreateChild(xmlDocument, xmlNodes1, "Origin").InnerText = creature.Origin.ToString();
					XMLHelper.CreateChild(xmlDocument, xmlNodes1, "Keywords").InnerText = creature.Keywords;
					XMLHelper.CreateChild(xmlDocument, xmlNodes1, "Size").InnerText = creature.Size.ToString();
					XMLHelper.CreateChild(xmlDocument, xmlNodes1, "HP").InnerText = slot.Card.HP.ToString();
					XMLHelper.CreateChild(xmlDocument, xmlNodes1, "InitBonus").InnerText = slot.Card.Initiative.ToString();
					XMLHelper.CreateChild(xmlDocument, xmlNodes1, "Speed").InnerText = slot.Card.Movement;
					XmlNode xmlNodes2 = XMLHelper.CreateChild(xmlDocument, xmlNodes1, "Defenses");
					XMLHelper.CreateChild(xmlDocument, xmlNodes2, "AC").InnerText = slot.Card.AC.ToString();
					XMLHelper.CreateChild(xmlDocument, xmlNodes2, "Fortitude").InnerText = slot.Card.Fortitude.ToString();
					XMLHelper.CreateChild(xmlDocument, xmlNodes2, "Reflex").InnerText = slot.Card.Reflex.ToString();
					XMLHelper.CreateChild(xmlDocument, xmlNodes2, "Will").InnerText = slot.Card.Will.ToString();
					if (slot.Card.Regeneration != null)
					{
						XmlNode xmlNodes3 = XMLHelper.CreateChild(xmlDocument, xmlNodes1, "Regeneration");
						XMLHelper.CreateChild(xmlDocument, xmlNodes3, "Value").InnerText = slot.Card.Regeneration.Value.ToString();
						XMLHelper.CreateChild(xmlDocument, xmlNodes3, "Details").InnerText = slot.Card.Regeneration.Details;
					}
					XmlNode xmlNodes4 = XMLHelper.CreateChild(xmlDocument, xmlNodes1, "Damage");
					foreach (DamageModifier damageModifier in slot.Card.DamageModifiers)
					{
						if (damageModifier.Value < 0)
						{
							XmlNode xmlNodes5 = XMLHelper.CreateChild(xmlDocument, xmlNodes4, "Resist");
							XMLHelper.CreateChild(xmlDocument, xmlNodes5, "Type").InnerText = damageModifier.Type.ToString();
							XmlNode str1 = XMLHelper.CreateChild(xmlDocument, xmlNodes5, "Details");
							int num = Math.Abs(damageModifier.Value);
							str1.InnerText = num.ToString();
						}
						else if (damageModifier.Value <= 0)
						{
							XmlNode xmlNodes6 = XMLHelper.CreateChild(xmlDocument, xmlNodes4, "Immune");
							XMLHelper.CreateChild(xmlDocument, xmlNodes6, "Type").InnerText = damageModifier.Type.ToString();
						}
						else
						{
							XmlNode xmlNodes7 = XMLHelper.CreateChild(xmlDocument, xmlNodes4, "Vulnerable");
							XMLHelper.CreateChild(xmlDocument, xmlNodes7, "Type").InnerText = damageModifier.Type.ToString();
							XmlNode str2 = XMLHelper.CreateChild(xmlDocument, xmlNodes7, "Details");
							int num1 = Math.Abs(damageModifier.Value);
							str2.InnerText = num1.ToString();
						}
					}
					if (slot.Card.Resist != "")
					{
						XMLHelper.CreateChild(xmlDocument, xmlNodes4, "Resist").InnerText = slot.Card.Resist;
					}
					if (slot.Card.Vulnerable != "")
					{
						XMLHelper.CreateChild(xmlDocument, xmlNodes4, "Vulnerable").InnerText = slot.Card.Vulnerable;
					}
					if (slot.Card.Immune != "")
					{
						XMLHelper.CreateChild(xmlDocument, xmlNodes4, "Immune").InnerText = slot.Card.Immune;
					}
					XmlNode xmlNodes8 = XMLHelper.CreateChild(xmlDocument, xmlNodes1, "AbilityModifiers");
					XMLHelper.CreateChild(xmlDocument, xmlNodes8, "Strength").InnerText = creature.Strength.Modifier.ToString();
					XMLHelper.CreateChild(xmlDocument, xmlNodes8, "Constitution").InnerText = creature.Constitution.Modifier.ToString();
					XMLHelper.CreateChild(xmlDocument, xmlNodes8, "Dexterity").InnerText = creature.Dexterity.Modifier.ToString();
					XMLHelper.CreateChild(xmlDocument, xmlNodes8, "Intelligence").InnerText = creature.Intelligence.Modifier.ToString();
					XMLHelper.CreateChild(xmlDocument, xmlNodes8, "Wisdom").InnerText = creature.Wisdom.Modifier.ToString();
					XMLHelper.CreateChild(xmlDocument, xmlNodes8, "Charisma").InnerText = creature.Charisma.Modifier.ToString();
					XMLHelper.CreateChild(xmlDocument, xmlNodes1, "Senses").InnerText = slot.Card.Senses;
					XMLHelper.CreateChild(xmlDocument, xmlNodes1, "Skills").InnerText = slot.Card.Skills;
					XMLHelper.CreateChild(xmlDocument, xmlNodes1, "Equipment").InnerText = slot.Card.Equipment;
					XMLHelper.CreateChild(xmlDocument, xmlNodes1, "Tactics").InnerText = slot.Card.Tactics;
				}
			}
			XmlNode xmlNodes9 = XMLHelper.CreateChild(xmlDocument, xmlDocument.DocumentElement, "PCs");
			foreach (Hero hero in Session.Project.Heroes)
			{
				XmlNode xmlNodes10 = XMLHelper.CreateChild(xmlDocument, xmlNodes9, "PC");
				XMLHelper.CreateChild(xmlDocument, xmlNodes10, "Name").InnerText = hero.Name;
				XMLHelper.CreateChild(xmlDocument, xmlNodes10, "Description").InnerText = hero.Info;
				XMLHelper.CreateChild(xmlDocument, xmlNodes10, "Size").InnerText = hero.Size.ToString();
				XMLHelper.CreateChild(xmlDocument, xmlNodes10, "HP").InnerText = hero.HP.ToString();
				XMLHelper.CreateChild(xmlDocument, xmlNodes10, "InitBonus").InnerText = hero.InitBonus.ToString();
				XmlNode xmlNodes11 = XMLHelper.CreateChild(xmlDocument, xmlNodes10, "Defenses");
				XMLHelper.CreateChild(xmlDocument, xmlNodes11, "AC").InnerText = hero.AC.ToString();
				XMLHelper.CreateChild(xmlDocument, xmlNodes11, "Fortitude").InnerText = hero.Fortitude.ToString();
				XMLHelper.CreateChild(xmlDocument, xmlNodes11, "Reflex").InnerText = hero.Reflex.ToString();
				XMLHelper.CreateChild(xmlDocument, xmlNodes11, "Will").InnerText = hero.Will.ToString();
				if (hero.Key == "")
				{
					continue;
				}
				XMLHelper.CreateChild(xmlDocument, xmlNodes10, "Key").InnerText = hero.Key;
			}
			return xmlDocument.OuterXml;
		}
	}
}