using Masterplan;
using Masterplan.Tools;
using System;
using System.Collections.Generic;

namespace Masterplan.Tools.Generators
{
	internal class Potion
	{
		public Potion()
		{
		}

		private static string adjective()
		{
			List<string> strs = new List<string>()
			{
				"watery",
				"syrupy",
				"thick",
				"viscous",
				"gloopy",
				"thin",
				"runny",
				"translucent",
				"effervescent",
				"fizzing",
				"bubbling",
				"foaming",
				"volatile",
				"smoking",
				"fuming",
				"vaporous",
				"steaming",
				"cold",
				"icy cold",
				"hot",
				"sparkling",
				"iridescent",
				"cloudy",
				"opalescent",
				"luminous",
				"phosphorescent",
				"glowing"
			};
			return strs[Session.Random.Next(strs.Count)];
		}

		private static string colour(bool complex)
		{
			List<string> strs = new List<string>()
			{
				"red",
				"scarlet",
				"crimson",
				"vermillion"
			};
			if (complex)
			{
				strs.Add("blood red");
				strs.Add("cherry red");
				strs.Add("ruby-coloured");
			}
			strs.Add("pink");
			if (complex)
			{
				strs.Add("rose-coloured");
			}
			strs.Add("blue");
			strs.Add("royal blue");
			strs.Add("sky blue");
			strs.Add("light blue");
			strs.Add("dark blue");
			strs.Add("midnight blue");
			strs.Add("indigo");
			if (complex)
			{
				strs.Add("sapphire-coloured");
			}
			strs.Add("yellow");
			strs.Add("lemon yellow");
			strs.Add("amber");
			if (complex)
			{
				strs.Add("straw-coloured");
			}
			strs.Add("green");
			strs.Add("light green");
			strs.Add("dark green");
			strs.Add("sea green");
			strs.Add("turquoise");
			strs.Add("aquamarine");
			strs.Add("emerald");
			if (complex)
			{
				strs.Add("olive-coloured");
			}
			strs.Add("purple");
			strs.Add("lavender");
			strs.Add("lilac");
			strs.Add("mauve");
			if (complex)
			{
				strs.Add("plum-coloured");
			}
			strs.Add("orange");
			strs.Add("brown");
			strs.Add("maroon");
			strs.Add("ochre");
			if (complex)
			{
				strs.Add("mud-coloured");
			}
			strs.Add("black");
			strs.Add("dark grey");
			strs.Add("grey");
			strs.Add("light grey");
			if (complex)
			{
				strs.Add("cream-coloured");
				strs.Add("ivory-coloured");
			}
			strs.Add("off-white");
			strs.Add("white");
			strs.Add("golden");
			strs.Add("silver");
			if (complex)
			{
				strs.Add("bronze-coloured");
			}
			if (complex)
			{
				strs.Add("colourless");
				strs.Add("clear");
				strs.Add("transparent");
			}
			return strs[Session.Random.Next(strs.Count)];
		}

		private static string container()
		{
			List<string> strs = new List<string>()
			{
				"small",
				"rounded",
				"tall",
				"square",
				"irregularly-shaped",
				"long-necked",
				"cylindrical",
				"round-bottomed"
			};
			List<string> strs1 = new List<string>()
			{
				"glass",
				"metal",
				"ceramic",
				"crystal"
			};
			List<string> strs2 = new List<string>()
			{
				"vial",
				"jar",
				"bottle",
				"flask"
			};
			int num = Session.Random.Next(strs.Count);
			string item = strs[num];
			int num1 = Session.Random.Next(strs1.Count);
			string str = strs1[num1];
			int num2 = Session.Random.Next(strs2.Count);
			string item1 = strs2[num2];
			if (Session.Random.Next(3) == 0)
			{
				str = string.Concat(Potion.colour(true), " ", str);
			}
			string str1 = "";
			switch (Session.Random.Next(2))
			{
				case 0:
				{
					str1 = string.Concat(str, " ", item1);
					break;
				}
				case 1:
				{
					string[] strArrays = new string[] { item, " ", str, " ", item1 };
					str1 = string.Concat(strArrays);
					break;
				}
			}
			return string.Concat((TextHelper.StartsWithVowel(str1) ? "an" : "a"), " ", str1);
		}

		public static string Description()
		{
			string str = "";
			string str1 = Potion.colour(true);
			string str2 = Potion.adjective();
			string str3 = Potion.feature();
			List<string> strs = new List<string>()
			{
				"liquid",
				"solution",
				"draught",
				"oil",
				"elixir",
				"potion"
			};
			int num = Session.Random.Next(strs.Count);
			string item = strs[num];
			switch (Session.Random.Next(5))
			{
				case 0:
				{
					str = string.Concat(str1, " ", item);
					break;
				}
				case 1:
				{
					string[] strArrays = new string[] { str1, " ", item, " ", str3 };
					str = string.Concat(strArrays);
					break;
				}
				case 2:
				{
					string[] strArrays1 = new string[] { str2, " ", str1, " ", item };
					str = string.Concat(strArrays1);
					break;
				}
				case 3:
				{
					string[] strArrays2 = new string[] { str2, " ", str1, " ", item, " ", str3 };
					str = string.Concat(strArrays2);
					break;
				}
				case 4:
				{
					string[] strArrays3 = new string[] { str2, " ", item, ", ", str1, " ", str3, "," };
					str = string.Concat(strArrays3);
					break;
				}
			}
			string str4 = (TextHelper.StartsWithVowel(str) ? "An" : "A");
			string[] strArrays4 = new string[] { str4, " ", str, " in ", Potion.container(), "." };
			str = string.Concat(strArrays4);
			switch (Session.Random.Next(5))
			{
				case 0:
				{
					str = string.Concat(str, " It smells ", Potion.smell(), ".");
					break;
				}
				case 1:
				{
					str = string.Concat(str, " It tastes ", Potion.smell(), ".");
					break;
				}
				case 2:
				{
					string str5 = str;
					string[] strArrays5 = new string[] { str5, " It smells ", Potion.smell(), " but tastes ", Potion.smell(), "." };
					str = string.Concat(strArrays5);
					break;
				}
				case 3:
				{
					str = string.Concat(str, " It smells and tastes ", Potion.smell(), ".");
					break;
				}
			}
			return str;
		}

		private static string feature()
		{
			switch (Session.Random.Next(5))
			{
				case 0:
				{
					return string.Concat("with ", Potion.colour(true), " specks");
				}
				case 1:
				{
					return string.Concat("with flecks of ", Potion.colour(false));
				}
				case 2:
				{
					string str = Potion.colour(true);
					string str1 = (TextHelper.StartsWithVowel(str) ? "an" : "a");
					string[] strArrays = new string[] { "with ", str1, " ", str, " suspension" };
					return string.Concat(strArrays);
				}
				case 3:
				{
					return string.Concat("with a floating ", Potion.colour(true), " layer");
				}
				case 4:
				{
					return string.Concat("with a ribbon of ", Potion.colour(false));
				}
			}
			return "";
		}

		private static string smell()
		{
			List<string> strs = new List<string>()
			{
				"acidic",
				"acrid",
				"of ammonia",
				"of apples",
				"bitter",
				"brackish",
				"buttery",
				"of cherries",
				"delicious",
				"earthy",
				"of earwax",
				"of fish",
				"floral",
				"of lavender",
				"lemony",
				"of honey",
				"fruity",
				"meaty",
				"metallic",
				"musty",
				"of onions",
				"of oranges",
				"peppery",
				"of perfume",
				"rotten",
				"salty",
				"sickly sweet",
				"starchy",
				"sugary",
				"smokey",
				"sour",
				"spicy",
				"of sweat",
				"sweet",
				"unpleasant",
				"vile",
				"vinegary"
			};
			return strs[Session.Random.Next(strs.Count)];
		}
	}
}