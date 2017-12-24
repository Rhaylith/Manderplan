using Masterplan;
using Masterplan.Tools;
using System;
using System.Collections.Generic;

namespace Masterplan.Tools.Generators
{
	internal class ExoticName
	{
		public ExoticName()
		{
		}

		public static string FullName()
		{
			string str = TextHelper.Capitalise(ExoticName.get_word(), true);
			string str1 = TextHelper.Capitalise(ExoticName.get_word(), true);
			return string.Concat(str, " ", str1);
		}

		private static string get_word()
		{
			List<string> strs = new List<string>()
			{
				"a",
				"e",
				"i",
				"o",
				"u",
				"ae",
				"ai",
				"ao",
				"au",
				"ea",
				"ee",
				"ei",
				"eo",
				"eu",
				"ia",
				"ie",
				"io",
				"iu",
				"oa",
				"oe",
				"oi",
				"oo",
				"ou",
				"ua",
				"ue",
				"ui",
				"uo",
				"y"
			};
			List<string> strs1 = new List<string>();
			strs1.AddRange(new string[] { "b" });
			strs1.AddRange(new string[] { "c", "ch" });
			strs1.AddRange(new string[] { "d" });
			strs1.AddRange(new string[] { "f", "fl", "fr" });
			string[] strArrays = new string[] { "g", "gh", "gn", "gr" };
			strs1.AddRange(strArrays);
			strs1.AddRange(new string[] { "h" });
			strs1.AddRange(new string[] { "j" });
			strs1.AddRange(new string[] { "k", "kh", "kr" });
			strs1.AddRange(new string[] { "l", "ll" });
			strs1.AddRange(new string[] { "m" });
			strs1.AddRange(new string[] { "n" });
			strs1.AddRange(new string[] { "p", "ph", "pr" });
			strs1.AddRange(new string[] { "q" });
			strs1.AddRange(new string[] { "r", "rh" });
			string[] strArrays1 = new string[] { "s", "sc", "sch", "sh", "sk", "sp", "st" };
			strs1.AddRange(strArrays1);
			strs1.AddRange(new string[] { "t", "th" });
			strs1.AddRange(new string[] { "v" });
			strs1.AddRange(new string[] { "w", "wr" });
			string str = "-";
			if (Session.Random.Next(3) == 0)
			{
				str = "'";
			}
			string str1 = "";
			int num = Session.Random.Next(2) + 1;
			for (int i = 0; i != num; i++)
			{
				if (str1 != "" && Session.Random.Next(10) == 0)
				{
					str1 = string.Concat(str1, str);
				}
				if (str1 == "")
				{
					int num1 = Session.Random.Next(strs1.Count);
					str1 = string.Concat(str1, strs1[num1]);
				}
				int num2 = Session.Random.Next(strs.Count);
				str1 = string.Concat(str1, strs[num2]);
				int num3 = Session.Random.Next(strs1.Count);
				str1 = string.Concat(str1, strs1[num3]);
			}
			if (Session.Random.Next(4) == 0)
			{
				int num4 = Session.Random.Next(strs.Count);
				string item = strs[num4];
				str1 = string.Concat(str1, item[0]);
			}
			return str1;
		}

		public static string Sentence()
		{
			string str = "";
			int num = Session.Dice(3, 6);
			for (int i = 0; i != num; i++)
			{
				if (str != "")
				{
					str = string.Concat(str, " ");
				}
				str = string.Concat(str, ExoticName.get_word());
			}
			str = string.Concat(str, ".");
			return TextHelper.Capitalise(str, false);
		}

		public static string SingleName()
		{
			return TextHelper.Capitalise(ExoticName.get_word(), true);
		}
	}
}