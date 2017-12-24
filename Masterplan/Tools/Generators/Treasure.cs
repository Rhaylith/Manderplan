using Masterplan;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;

namespace Masterplan.Tools.Generators
{
	internal class Treasure
	{
		private static List<Guid> fPlaceholderIDs;

		private static List<int> fValues;

		private static List<string> fObjects;

		private static List<string> fJewellery;

		private static List<string> fInstruments;

		private static List<string> fStones;

		private static List<string> fMetals;

		public static List<Guid> PlaceholderIDs
		{
			get
			{
				if (Treasure.fPlaceholderIDs == null)
				{
					Treasure.fPlaceholderIDs = new List<Guid>();
					for (int i = 1; i <= 30; i++)
					{
						Guid guid = new Guid(i, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
						Treasure.fPlaceholderIDs.Add(guid);
					}
				}
				return Treasure.fPlaceholderIDs;
			}
		}

		static Treasure()
		{
			Treasure.fPlaceholderIDs = null;
			Treasure.fValues = new List<int>(new int[] { 125000, 100000, 75000, 50000, 25000, 20000, 10000, 7500, 5000, 2500, 2000, 1000, 7500, 5000, 2500, 2000, 1000, 750, 500, 250, 200, 100, 50 });
			string[] strArrays = new string[] { "medal", "statuette", "sculpture", "idol", "chalice", "goblet", "dish", "bowl" };
			Treasure.fObjects = new List<string>(strArrays);
			string[] strArrays1 = new string[] { "ring", "necklace", "crown", "circlet", "bracelet", "anklet", "torc", "brooch", "pendant", "locket", "diadem", "tiara", "earring" };
			Treasure.fJewellery = new List<string>(strArrays1);
			string[] strArrays2 = new string[] { "lute", "lyre", "mandolin", "violin", "drum", "flute", "clarinet", "accordion", "banjo", "bodhran", "ocarina", "zither", "djembe" };
			Treasure.fInstruments = new List<string>(strArrays2);
			string[] strArrays3 = new string[] { "diamond", "ruby", "sapphire", "emerald", "amethyst", "garnet", "topaz", "pearl", "black pearl", "opal", "fire opal", "amber", "coral", "agate", "carnelian", "jade", "peridot", "moonstone", "alexandrite", "aquamarine", "jacinth", "marble" };
			Treasure.fStones = new List<string>(strArrays3);
			string[] strArrays4 = new string[] { "gold", "silver", "bronze", "platinum", "electrum", "mithral", "orium", "adamantine" };
			Treasure.fMetals = new List<string>(strArrays4);
		}

		public Treasure()
		{
		}

		public static string ArtObject()
		{
			string str = Treasure.random_item_type(false, false);
			str = string.Concat((TextHelper.StartsWithVowel(str) ? "An" : "A"), " ", str);
			return str;
		}

		private static string coins(int gp)
		{
			int num = gp / 10000;
			if (num > 0 && gp % 10000 == 0)
			{
				string str = "astral diamond";
				if (num > 1)
				{
					str = string.Concat(str, "s");
				}
				return string.Concat(num, " ", str);
			}
			int num1 = gp / 100;
			if (num1 >= 100 && gp % 100 == 0)
			{
				return string.Concat(num1, " PP");
			}
			int num2 = gp * 10;
			if (num2 <= 100)
			{
				return string.Concat(num2, " SP");
			}
			return string.Concat(gp, " GP");
		}

		private static List<string> create_from_gp(int gp)
		{
			int i;
			int _value = 0;
			int num = 0;
			List<string> strs = new List<string>();
			if (Session.Random.Next() % 4 != 0)
			{
				for (i = gp; i != 0; i = i - _value * num)
				{
					_value = Treasure.get_value(i);
					if (_value == 0)
					{
						break;
					}
					num = i / _value;
					string str = Treasure.random_item_type(num != 1, true);
					if (num != 1)
					{
						object[] objArray = new object[] { num, " ", str, " (worth ", _value, " GP each)" };
						strs.Add(string.Concat(objArray));
					}
					else
					{
						string str1 = (TextHelper.StartsWithVowel(str) ? "an" : "a");
						object[] objArray1 = new object[] { str1, " ", str, " (worth ", _value, " GP)" };
						strs.Add(string.Concat(objArray1));
					}
				}
				if (i != 0)
				{
					strs.Add(Treasure.coins(i));
				}
			}
			else
			{
				strs.Add(Treasure.coins(gp));
			}
			for (int j = 0; j != strs.Count; j++)
			{
				strs[j] = TextHelper.Capitalise(strs[j], false);
			}
			return strs;
		}

		public static Parcel CreateParcel(int level, int size, bool placeholder)
		{
			List<Parcel> parcels = Treasure.CreateParcelSet(level, size, placeholder);
			int num = Session.Random.Next() % parcels.Count;
			return parcels[num];
		}

		public static Parcel CreateParcel(int value, bool placeholder)
		{
			Parcel parcel = new Parcel()
			{
				Name = string.Concat("Items worth ", value, " GP"),
				Value = value
			};
			if (!placeholder)
			{
				parcel.Details = Treasure.RandomMundaneItem(value);
			}
			return parcel;
		}

		public static List<Parcel> CreateParcelSet(int level, int size, bool placeholder_items)
		{
			List<Parcel> parcels = new List<Parcel>();
			switch (size)
			{
				case 1:
				{
					parcels.Add(Treasure.get_magic_item(level + 2, placeholder_items));
					break;
				}
				case 2:
				{
					parcels.Add(Treasure.get_magic_item(level + 2, placeholder_items));
					break;
				}
				case 3:
				{
					parcels.Add(Treasure.get_magic_item(level + 4, placeholder_items));
					parcels.Add(Treasure.get_magic_item(level + 2, placeholder_items));
					break;
				}
				case 4:
				{
					parcels.Add(Treasure.get_magic_item(level + 4, placeholder_items));
					parcels.Add(Treasure.get_magic_item(level + 3, placeholder_items));
					parcels.Add(Treasure.get_magic_item(level + 1, placeholder_items));
					break;
				}
				case 5:
				{
					parcels.Add(Treasure.get_magic_item(level + 4, placeholder_items));
					parcels.Add(Treasure.get_magic_item(level + 3, placeholder_items));
					parcels.Add(Treasure.get_magic_item(level + 2, placeholder_items));
					parcels.Add(Treasure.get_magic_item(level + 1, placeholder_items));
					break;
				}
				case 6:
				{
					parcels.Add(Treasure.get_magic_item(level + 4, placeholder_items));
					parcels.Add(Treasure.get_magic_item(level + 3, placeholder_items));
					parcels.Add(Treasure.get_magic_item(level + 2, placeholder_items));
					parcels.Add(Treasure.get_magic_item(level + 2, placeholder_items));
					parcels.Add(Treasure.get_magic_item(level + 1, placeholder_items));
					break;
				}
				case 7:
				{
					parcels.Add(Treasure.get_magic_item(level + 4, placeholder_items));
					parcels.Add(Treasure.get_magic_item(level + 3, placeholder_items));
					parcels.Add(Treasure.get_magic_item(level + 2, placeholder_items));
					parcels.Add(Treasure.get_magic_item(level + 2, placeholder_items));
					parcels.Add(Treasure.get_magic_item(level + 1, placeholder_items));
					parcels.Add(Treasure.get_magic_item(level + 1, placeholder_items));
					break;
				}
				case 8:
				{
					parcels.Add(Treasure.get_magic_item(level + 4, placeholder_items));
					parcels.Add(Treasure.get_magic_item(level + 3, placeholder_items));
					parcels.Add(Treasure.get_magic_item(level + 3, placeholder_items));
					parcels.Add(Treasure.get_magic_item(level + 2, placeholder_items));
					parcels.Add(Treasure.get_magic_item(level + 2, placeholder_items));
					parcels.Add(Treasure.get_magic_item(level + 1, placeholder_items));
					parcels.Add(Treasure.get_magic_item(level + 1, placeholder_items));
					break;
				}
			}
			List<int> gpValues = Treasure.get_gp_values(level);
			if (size == 1)
			{
				gpValues.RemoveAt(0);
			}
			foreach (int gpValue in gpValues)
			{
				parcels.Add(Treasure.CreateParcel(gpValue, placeholder_items));
			}
			return parcels;
		}

		private static List<int> get_gp_values(int level)
		{
			switch (level)
			{
				case 1:
				{
					return new List<int>(new int[] { 200, 180, 120, 120, 60, 40 });
				}
				case 2:
				{
					return new List<int>(new int[] { 290, 260, 170, 170, 90, 60 });
				}
				case 3:
				{
					return new List<int>(new int[] { 380, 340, 225, 225, 110, 75 });
				}
				case 4:
				{
					return new List<int>(new int[] { 470, 420, 280, 280, 140, 90 });
				}
				case 5:
				{
					return new List<int>(new int[] { 550, 500, 340, 340, 160, 110 });
				}
				case 6:
				{
					return new List<int>(new int[] { 1000, 900, 600, 600, 300, 200 });
				}
				case 7:
				{
					return new List<int>(new int[] { 1500, 1300, 850, 850, 400, 300 });
				}
				case 8:
				{
					return new List<int>(new int[] { 1900, 1700, 1100, 1100, 600, 400 });
				}
				case 9:
				{
					return new List<int>(new int[] { 2400, 2100, 1400, 1400, 700, 400 });
				}
				case 10:
				{
					return new List<int>(new int[] { 2800, 2500, 1700, 1700, 800, 500 });
				}
				case 11:
				{
					return new List<int>(new int[] { 5000, 4000, 3000, 3000, 2000, 1000 });
				}
				case 12:
				{
					return new List<int>(new int[] { 7200, 7000, 4400, 4400, 2000, 1000 });
				}
				case 13:
				{
					return new List<int>(new int[] { 9500, 8500, 5700, 5700, 2800, 1800 });
				}
				case 14:
				{
					return new List<int>(new int[] { 12000, 10000, 7000, 7000, 4000, 2000 });
				}
				case 15:
				{
					return new List<int>(new int[] { 14000, 12000, 8500, 8500, 5000, 2000 });
				}
				case 16:
				{
					return new List<int>(new int[] { 25000, 22000, 15000, 15000, 8000, 5000 });
				}
				case 17:
				{
					return new List<int>(new int[] { 36000, 33000, 22000, 22000, 11000, 6000 });
				}
				case 18:
				{
					return new List<int>(new int[] { 48000, 42000, 29000, 29000, 15000, 7000 });
				}
				case 19:
				{
					return new List<int>(new int[] { 60000, 52000, 35000, 35000, 18000, 10000 });
				}
				case 20:
				{
					return new List<int>(new int[] { 70000, 61000, 42000, 42000, 21000, 14000 });
				}
				case 21:
				{
					return new List<int>(new int[] { 125000, 112000, 75000, 75000, 38000, 25000 });
				}
				case 22:
				{
					return new List<int>(new int[] { 180000, 160000, 110000, 110000, 55000, 35000 });
				}
				case 23:
				{
					return new List<int>(new int[] { 240000, 210000, 140000, 140000, 70000, 50000 });
				}
				case 24:
				{
					return new List<int>(new int[] { 300000, 250000, 175000, 175000, 90000, 60000 });
				}
				case 25:
				{
					return new List<int>(new int[] { 350000, 320000, 200000, 200000, 100000, 80000 });
				}
				case 26:
				{
					return new List<int>(new int[] { 625000, 560000, 375000, 375000, 190000, 125000 });
				}
				case 27:
				{
					return new List<int>(new int[] { 900000, 800000, 550000, 550000, 280000, 170000 });
				}
				case 28:
				{
					return new List<int>(new int[] { 1200000, 1000000, 720000, 720000, 360000, 250000 });
				}
				case 29:
				{
					return new List<int>(new int[] { 1500000, 1300000, 875000, 875000, 450000, 250000 });
				}
				case 30:
				{
					return new List<int>(new int[] { 1750000, 1500000, 1000000, 1000000, 600000, 400000 });
				}
			}
			return null;
		}

		private static Parcel get_magic_item(int level, bool placeholder)
		{
			int num = Math.Min(30, level);
			if (placeholder)
			{
				return new Parcel(Treasure.get_placeholder_item(level));
			}
			MagicItem magicItem = Treasure.RandomMagicItem(num);
			if (magicItem != null)
			{
				return new Parcel(magicItem);
			}
			Parcel parcel = new Parcel()
			{
				Details = string.Concat("Random magic item (level ", num, ")")
			};
			return parcel;
		}

		private static MagicItem get_placeholder_item(int level)
		{
			int num = Math.Min(level, 30);
			MagicItem magicItem = new MagicItem()
			{
				Name = string.Concat("Magic Item (level ", num, ")"),
				Level = num,
				ID = Treasure.PlaceholderIDs[num - 1]
			};
			return magicItem;
		}

		private static int get_value(int total)
		{
			List<int> nums = new List<int>();
			foreach (int fValue in Treasure.fValues)
			{
				int num = total / fValue;
				if (num < 1 || num > 10)
				{
					continue;
				}
				nums.Add(fValue);
			}
			if (nums.Count == 0)
			{
				return 0;
			}
			int num1 = Session.Random.Next() % nums.Count;
			return nums[num1];
		}

		public static int GetItemValue(int level)
		{
			switch (level)
			{
				case 1:
				{
					return 360;
				}
				case 2:
				{
					return 520;
				}
				case 3:
				{
					return 680;
				}
				case 4:
				{
					return 840;
				}
				case 5:
				{
					return 1000;
				}
				case 6:
				{
					return 1800;
				}
				case 7:
				{
					return 2600;
				}
				case 8:
				{
					return 3400;
				}
				case 9:
				{
					return 4200;
				}
				case 10:
				{
					return 5000;
				}
				case 11:
				{
					return 9000;
				}
				case 12:
				{
					return 13000;
				}
				case 13:
				{
					return 17000;
				}
				case 14:
				{
					return 21000;
				}
				case 15:
				{
					return 25000;
				}
				case 16:
				{
					return 45000;
				}
				case 17:
				{
					return 65000;
				}
				case 18:
				{
					return 85000;
				}
				case 19:
				{
					return 105000;
				}
				case 20:
				{
					return 125000;
				}
				case 21:
				{
					return 225000;
				}
				case 22:
				{
					return 325000;
				}
				case 23:
				{
					return 425000;
				}
				case 24:
				{
					return 525000;
				}
				case 25:
				{
					return 625000;
				}
				case 26:
				{
					return 1125000;
				}
				case 27:
				{
					return 1625000;
				}
				case 28:
				{
					return 2125000;
				}
				case 29:
				{
					return 2625000;
				}
				case 30:
				{
					return 3125000;
				}
			}
			return 0;
		}

		private static string random_item_type(bool plural, bool allow_potion)
		{
			string[] strArrays;
			string str;
			string str1 = "";
			if (allow_potion && Session.Random.Next() % 4 == 0)
			{
				str1 = "potion";
				if (plural)
				{
					str1 = string.Concat(str1, "s");
				}
				return str1;
			}
			switch (Session.Random.Next() % 12)
			{
				case 0:
				case 1:
				case 2:
				{
					int num = Session.Random.Next() % Treasure.fStones.Count;
					string item = Treasure.fStones[num];
					switch (Session.Random.Next() % 2)
					{
						case 0:
						{
							item = string.Concat(item, " gemstone");
							break;
						}
						case 1:
						{
							item = string.Concat("piece of ", item);
							break;
						}
					}
					switch (Session.Random.Next() % 12)
					{
						case 0:
						{
							item = string.Concat("well cut ", item);
							break;
						}
						case 1:
						{
							item = string.Concat("rough-cut ", item);
							break;
						}
						case 2:
						{
							item = string.Concat("poorly cut ", item);
							break;
						}
						case 3:
						{
							item = string.Concat("small ", item);
							break;
						}
						case 4:
						{
							item = string.Concat("large ", item);
							break;
						}
						case 5:
						{
							item = string.Concat("oddly shaped ", item);
							break;
						}
						case 6:
						{
							item = string.Concat("highly polished ", item);
							break;
						}
					}
					str1 = item;
					break;
				}
				case 3:
				case 4:
				case 5:
				{
					int num1 = Session.Random.Next() % Treasure.fObjects.Count;
					string item1 = Treasure.fObjects[num1];
					List<string> strs = new List<string>()
					{
						"small",
						"large",
						"light",
						"heavy",
						"delicate",
						"fragile",
						"masterwork",
						"elegant"
					};
					int num2 = Session.Random.Next() % strs.Count;
					string item2 = strs[num2];
					str1 = string.Concat(item2, " ", item1);
					break;
				}
				case 6:
				case 7:
				case 8:
				{
					int num3 = Session.Random.Next() % Treasure.fJewellery.Count;
					string str2 = Treasure.fJewellery[num3];
					int num4 = Session.Random.Next() % Treasure.fMetals.Count;
					string item3 = Treasure.fMetals[num4];
					str1 = string.Concat(item3, " ", str2);
					switch (Session.Random.Next(5))
					{
						case 0:
						{
							str1 = string.Concat((Session.Random.Next(2) == 0 ? "enamelled" : "laquered"), " ", str1);
							break;
						}
						case 1:
						{
							num4 = Session.Random.Next() % Treasure.fMetals.Count;
							item3 = Treasure.fMetals[num4];
							string str3 = (Session.Random.Next(2) == 0 ? "plated" : "filigreed");
							strArrays = new string[] { item3, "-", str3, " ", str1 };
							str1 = string.Concat(strArrays);
							break;
						}
					}
					switch (Session.Random.Next() % 10)
					{
						case 0:
						{
							str1 = string.Concat("delicate ", str1);
							break;
						}
						case 1:
						{
							str1 = string.Concat("intricate ", str1);
							break;
						}
						case 2:
						{
							str1 = string.Concat("elegant ", str1);
							break;
						}
						case 3:
						{
							str1 = string.Concat("simple ", str1);
							break;
						}
						case 4:
						{
							str1 = string.Concat("plain ", str1);
							break;
						}
					}
					break;
				}
				case 9:
				case 10:
				{
					string str4 = "";
					switch (Session.Random.Next(2))
					{
						case 0:
						{
							str4 = "painting";
							switch (Session.Random.Next(2))
							{
								case 0:
								{
									str4 = string.Concat("oil ", str4);
									break;
								}
								case 1:
								{
									str4 = string.Concat("watercolour ", str4);
									break;
								}
							}
							break;
						}
						case 1:
						{
							str4 = "drawing";
							switch (Session.Random.Next(2))
							{
								case 0:
								{
									str4 = string.Concat("pencil ", str4);
									break;
								}
								case 1:
								{
									str4 = string.Concat("charcoal ", str4);
									break;
								}
							}
							break;
						}
					}
					List<string> strs1 = new List<string>()
					{
						"small",
						"large",
						"delicate",
						"fragile",
						"elegant",
						"detailed"
					};
					List<string> strs2 = new List<string>()
					{
						"canvas",
						"paper",
						"parchment",
						"wood panels",
						"fabric"
					};
					int num5 = Session.Random.Next() % strs1.Count;
					string item4 = strs1[num5];
					int num6 = Session.Random.Next() % strs2.Count;
					string item5 = strs2[num6];
					strArrays = new string[] { item4, " ", str4, " on ", item5 };
					str1 = string.Concat(strArrays);
					break;
				}
				case 11:
				{
					int num7 = Session.Random.Next() % Treasure.fInstruments.Count;
					string str5 = Treasure.fInstruments[num7];
					List<string> strs3 = new List<string>()
					{
						"small",
						"large",
						"light",
						"heavy",
						"delicate",
						"fragile",
						"masterwork",
						"elegant"
					};
					int num8 = Session.Random.Next() % strs3.Count;
					string item6 = strs3[num8];
					str1 = string.Concat(item6, " ", str5);
					break;
				}
			}
			if (plural)
			{
				str1 = string.Concat(str1, "s");
			}
			switch (Session.Random.Next() % 5)
			{
				case 0:
				{
					List<string> strs4 = new List<string>()
					{
						"feywild",
						"shadowfell",
						"elemental chaos",
						"astral plane",
						"abyss",
						"distant north",
						"distant east",
						"distant west",
						"distant south"
					};
					int num9 = Session.Random.Next() % strs4.Count;
					string str6 = strs4[num9];
					str1 = string.Concat(str1, " from the ", str6);
					return str1;
				}
				case 1:
				{
					List<string> strs5 = new List<string>()
					{
						"decorated with",
						"inscribed with",
						"engraved with",
						"embossed with",
						"carved with"
					};
					List<string> strs6 = new List<string>()
					{
						"indecipherable",
						"ancient",
						"curious",
						"unusual",
						"dwarven",
						"eladrin",
						"elven",
						"draconic",
						"gith"
					};
					List<string> strs7 = new List<string>()
					{
						"script",
						"designs",
						"sigils",
						"runes",
						"glyphs",
						"patterns"
					};
					int num10 = Session.Random.Next() % strs5.Count;
					string item7 = strs5[num10];
					int num11 = Session.Random.Next() % strs6.Count;
					string str7 = strs6[num11];
					int num12 = Session.Random.Next() % strs7.Count;
					string item8 = strs7[num12];
					str = str1;
					strArrays = new string[] { str, " ", item7, " ", str7, " ", item8 };
					str1 = string.Concat(strArrays);
					return str1;
				}
				case 2:
				{
					List<string> strs8 = new List<string>()
					{
						"glowing with",
						"suffused with",
						"infused with",
						"humming with",
						"pulsing with"
					};
					List<string> strs9 = new List<string>()
					{
						"arcane",
						"divine",
						"primal",
						"psionic",
						"shadow",
						"elemental",
						"unknown"
					};
					List<string> strs10 = new List<string>()
					{
						"energy",
						"power",
						"magic"
					};
					int num13 = Session.Random.Next() % strs8.Count;
					string str8 = strs8[num13];
					int num14 = Session.Random.Next() % strs9.Count;
					string item9 = strs9[num14];
					int num15 = Session.Random.Next() % strs10.Count;
					string str9 = strs10[num15];
					str = str1;
					strArrays = new string[] { str, " ", str8, " ", item9, " ", str9 };
					str1 = string.Concat(strArrays);
					return str1;
				}
				case 3:
				{
					return str1;
				}
				case 4:
				{
					List<string> strs11 = new List<string>()
					{
						"set with",
						"inlaid with",
						"studded with",
						"with shards of"
					};
					int num16 = Session.Random.Next() % Treasure.fStones.Count;
					string item10 = Treasure.fStones[num16];
					item10 = (Session.Random.Next() % 2 != 0 ? string.Concat("a single ", item10) : string.Concat(item10, "s"));
					int num17 = Session.Random.Next() % strs11.Count;
					string str10 = strs11[num17];
					str = str1;
					strArrays = new string[] { str, " ", str10, " ", item10 };
					str1 = string.Concat(strArrays);
					return str1;
				}
				default:
				{
					return str1;
				}
			}
		}

		public static Artifact RandomArtifact(Tier tier)
		{
			List<Artifact> artifacts = new List<Artifact>();
			foreach (Artifact artifact in Session.Artifacts)
			{
				if (artifact.Tier != tier)
				{
					continue;
				}
				artifacts.Add(artifact);
			}
			if (artifacts.Count == 0)
			{
				return null;
			}
			int num = Session.Random.Next() % artifacts.Count;
			return artifacts[num];
		}

		public static MagicItem RandomMagicItem(int level)
		{
			int num = Math.Min(30, level);
			List<MagicItem> magicItems = new List<MagicItem>();
			foreach (MagicItem magicItem in Session.MagicItems)
			{
				if (magicItem.Level != num)
				{
					continue;
				}
				magicItems.Add(magicItem);
			}
			if (magicItems.Count == 0)
			{
				return null;
			}
			int num1 = Session.Random.Next() % magicItems.Count;
			return magicItems[num1];
		}

		public static string RandomMundaneItem(int value)
		{
			List<string> strs = Treasure.create_from_gp(value);
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
}