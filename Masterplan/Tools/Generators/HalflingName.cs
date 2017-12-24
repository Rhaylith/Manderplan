using Masterplan;
using Masterplan.Tools;
using System;
using System.Collections.Generic;

namespace Masterplan.Tools.Generators
{
	internal class HalflingName
	{
		public HalflingName()
		{
		}

		private static string earned(bool first)
		{
			List<string> strs = new List<string>();
			if (!first)
			{
				strs.Add("caller");
				strs.Add("dancer");
				strs.Add("strider");
				strs.Add("weaver");
				strs.Add("wanderer");
			}
			else
			{
				strs.Add("laughing");
				strs.Add("fast");
				strs.Add("happy");
				strs.Add("kind");
				strs.Add("nimble");
				strs.Add("little");
				strs.Add("proud");
				strs.Add("quick");
				strs.Add("sly");
				strs.Add("small");
				strs.Add("smooth");
				strs.Add("snug");
				strs.Add("stout");
				strs.Add("sweet");
				strs.Add("swift");
				strs.Add("warm");
				strs.Add("wild");
				strs.Add("young");
				strs.Add("under");
			}
			strs.Add("badger");
			strs.Add("burrow");
			strs.Add("home");
			strs.Add("rascal");
			strs.Add("riddle");
			strs.Add("bottom");
			strs.Add("cloak");
			strs.Add("earth");
			strs.Add("eye");
			strs.Add("fellow");
			strs.Add("flower");
			strs.Add("finger");
			strs.Add("foot");
			strs.Add("glen");
			strs.Add("glitter");
			strs.Add("gold");
			strs.Add("hand");
			strs.Add("heart");
			strs.Add("hearth");
			strs.Add("hill");
			strs.Add("hollow");
			strs.Add("leaf");
			strs.Add("light");
			strs.Add("love");
			strs.Add("meadow");
			strs.Add("moon");
			strs.Add("reed");
			strs.Add("silver");
			strs.Add("skin");
			strs.Add("sun");
			strs.Add("thistle");
			strs.Add("will");
			strs.Add("whisper");
			return strs[Session.Random.Next(strs.Count)];
		}

		public static string FemaleName()
		{
			return HalflingName.name(false);
		}

		public static string MaleName()
		{
			return HalflingName.name(true);
		}

		private static string name(bool male)
		{
			string str = "";
			string str1 = "";
			switch (Session.Random.Next(20))
			{
				case 0:
				case 1:
				case 2:
				{
					str = HalflingName.simple(true);
					break;
				}
				case 3:
				case 4:
				case 5:
				case 6:
				{
					str = string.Concat(HalflingName.simple(true), HalflingName.simple(false));
					break;
				}
				case 7:
				case 8:
				case 9:
				case 10:
				{
					str = HalflingName.simple(true);
					str1 = string.Concat(HalflingName.simple(true), HalflingName.simple(false));
					break;
				}
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				{
					str = string.Concat(HalflingName.simple(true), HalflingName.simple(false));
					str1 = HalflingName.simple(true);
					break;
				}
				case 16:
				case 17:
				case 18:
				case 19:
				{
					str = string.Concat(HalflingName.simple(true), HalflingName.simple(false));
					str1 = string.Concat(HalflingName.earned(true), HalflingName.earned(false));
					break;
				}
			}
			if (!male)
			{
				char chr = str[str.Length - 1];
				if (!TextHelper.IsVowel(chr))
				{
					str = string.Concat(str, chr);
					str = string.Concat(str, "a");
				}
			}
			string str2 = str;
			if (str1 != "")
			{
				str2 = string.Concat(str2, " ", str1);
			}
			return TextHelper.Capitalise(str2, true);
		}

		private static string simple(bool start)
		{
			List<string> strs = new List<string>()
			{
				"arv",
				"baris",
				"brand",
				"bren",
				"cal",
				"chen",
				"cyrr",
				"dair",
				"dal",
				"deree",
				"dric",
				"essel",
				"fur",
				"galan",
				"gen",
				"gren",
				"ien",
				"illi",
				"indy",
				"iss",
				"kal",
				"kep",
				"kin",
				"li",
				"lur",
				"mel",
				"opee",
				"ped",
				"pery",
				"penel",
				"reen",
				"rill",
				"royl",
				"sheel",
				"thea",
				"ur",
				"wort",
				"yon"
			};
			if (!start)
			{
				strs.Add("eere");
				strs.Add("llalee");
			}
			return strs[Session.Random.Next(strs.Count)];
		}
	}
}