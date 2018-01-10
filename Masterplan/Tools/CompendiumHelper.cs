using Masterplan.Compendium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Utils;

namespace Masterplan.Tools
{
	internal class CompendiumHelper
	{
		public CompendiumHelper()
		{
		}

		public static List<CompendiumHelper.CompendiumItem> GetCreatures()
		{
			List<CompendiumHelper.CompendiumItem> compendiumItems = new List<CompendiumHelper.CompendiumItem>();
			try
			{
				foreach (XmlNode childNode in (new CompendiumSearch()).ViewAll("Monster").FirstChild.ChildNodes)
				{
					string str = XMLHelper.NodeText(childNode, "ID");
					string str1 = XMLHelper.NodeText(childNode, "Name");
					string str2 = XMLHelper.NodeText(childNode, "SourceBook");
					string str3 = XMLHelper.NodeText(childNode, "Level");
					if (str3 != "")
					{
						str3 = string.Concat("Level ", str3);
					}
					string str4 = XMLHelper.NodeText(childNode, "GroupRole");
					string str5 = XMLHelper.NodeText(childNode, "CombatRole");
					str4 = str4.Replace("Standard", "");
					str5 = str5.Replace("No role", "");
					string[] strArrays = new string[] { str3, " ", str4, " ", str5 };
					string str6 = string.Concat(strArrays);
					str6 = str6.Trim().Replace("  ", " ");
					compendiumItems.Add(new CompendiumHelper.CompendiumItem(CompendiumHelper.ItemType.Creature, str, str1, str2, str6));
				}
			}
			catch
			{
				compendiumItems = null;
			}
			return compendiumItems;
		}

		public static List<CompendiumHelper.CompendiumItem> GetMagicItems()
		{
			List<CompendiumHelper.CompendiumItem> compendiumItems = new List<CompendiumHelper.CompendiumItem>();
			try
			{
				XmlNode xmlNodes = (new CompendiumSearch()).ViewAll("Item");
				List<string> strs = new List<string>()
				{
					"Head",
					"Neck",
					"Arms",
					"Hands",
					"Waist",
					"Feet"
				};
				foreach (XmlNode childNode in xmlNodes.FirstChild.ChildNodes)
				{
					string str = XMLHelper.NodeText(childNode, "ID");
					string str1 = XMLHelper.NodeText(childNode, "Name");
					string str2 = XMLHelper.NodeText(childNode, "SourceBook");
					string str3 = (XMLHelper.NodeText(childNode, "IsMundane") == "True" ? "Mundane" : "");
					string str4 = XMLHelper.NodeText(childNode, "Level");
					if (str4 != "")
					{
						str4 = string.Concat("Level ", str4);
					}
					string str5 = XMLHelper.NodeText(childNode, "Category");
					if (strs.Contains(str5))
					{
						str5 = string.Concat(str5, " slot item");
					}
					if (str5 == "Alchemical" || str5 == "Wondrous" || str5 == "Consumable")
					{
						str5 = string.Concat(str5, " item");
					}
					if (str5 == "Whetstones")
					{
						str5 = "Whetstone";
					}
					string[] strArrays = new string[] { str3, " ", str4, " ", str5 };
					string str6 = string.Concat(strArrays);
					str6 = str6.Trim().Replace("  ", " ");
					compendiumItems.Add(new CompendiumHelper.CompendiumItem(CompendiumHelper.ItemType.MagicItem, str, str1, str2, str6));
				}
			}
			catch
			{
				compendiumItems = null;
			}
			return compendiumItems;
		}

		public static List<CompendiumHelper.CompendiumItem> GetTraps()
		{
			List<CompendiumHelper.CompendiumItem> compendiumItems = new List<CompendiumHelper.CompendiumItem>();
			try
			{
				foreach (XmlNode childNode in (new CompendiumSearch()).ViewAll("Trap").FirstChild.ChildNodes)
				{
					string str = XMLHelper.NodeText(childNode, "ID");
					string str1 = XMLHelper.NodeText(childNode, "Name");
					string str2 = XMLHelper.NodeText(childNode, "SourceBook");
					string str3 = XMLHelper.NodeText(childNode, "Level");
					if (str3 != "")
					{
						str3 = string.Concat("Level ", str3);
					}
					string str4 = XMLHelper.NodeText(childNode, "GroupRole");
					string str5 = XMLHelper.NodeText(childNode, "Type");
					str4 = str4.Replace("Standard", "");
					string[] strArrays = new string[] { str3, " ", str4, " ", str5 };
					string str6 = string.Concat(strArrays);
					str6 = str6.Trim().Replace("  ", " ");
					compendiumItems.Add(new CompendiumHelper.CompendiumItem(CompendiumHelper.ItemType.Trap, str, str1, str2, str6));
				}
			}
			catch
			{
				compendiumItems = null;
			}
			return compendiumItems;
		}

		public class CompendiumItem
		{
			public CompendiumHelper.ItemType Type;

			public string ID;

			public string Name;

			public string SourceBook;

			public string Info;

			public string URL
			{
				get
				{
					switch (this.Type)
					{
						case CompendiumHelper.ItemType.Creature:
						{
							return string.Concat("http://www.wizards.com/dndinsider/compendium/monster.aspx?id=", this.ID);
						}
						case CompendiumHelper.ItemType.Trap:
						{
							return string.Concat("http://www.wizards.com/dndinsider/compendium/trap.aspx?id=", this.ID);
						}
						case CompendiumHelper.ItemType.MagicItem:
						{
							return string.Concat("http://www.wizards.com/dndinsider/compendium/item.aspx?id=", this.ID);
						}
					}
					return "";
				}
			}

			public CompendiumItem(CompendiumHelper.ItemType type, string id, string name, string source_book, string info)
			{
				this.Type = type;
				this.ID = id;
				this.Name = name;
				this.SourceBook = source_book;
				this.Info = info;
			}
		}

		public enum ItemType
		{
			Creature,
			Trap,
			MagicItem
		}

		public class SourceBook
		{
			public string Name;

#pragma warning disable 0649
            public List<CompendiumHelper.CompendiumItem> Creatures;

			public List<CompendiumHelper.CompendiumItem> Traps;

			public List<CompendiumHelper.CompendiumItem> MagicItems;
#pragma warning restore 0649

            public SourceBook()
			{
			}
		}
	}
}