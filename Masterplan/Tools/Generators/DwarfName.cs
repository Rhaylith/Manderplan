using Masterplan;
using Masterplan.Tools;
using System;
using System.Collections.Generic;

namespace Masterplan.Tools.Generators
{
	internal class DwarfName
	{
		public DwarfName()
		{
		}

		public static string FemaleName()
		{
			return DwarfName.name(false);
		}

		public static string MaleName()
		{
			return DwarfName.name(true);
		}

		private static string name(bool male)
		{
			string str = "";
			switch (Session.Random.Next(8))
			{
				case 0:
				{
					str = string.Concat(DwarfName.prefix(), (male ? DwarfName.suffix_male() : DwarfName.suffix_female()));
					break;
				}
				case 1:
				case 2:
				case 3:
				case 4:
				{
					string[] strArrays = new string[] { DwarfName.prefix(), null, null, null, null };
					strArrays[1] = (male ? DwarfName.suffix_male() : DwarfName.suffix_female());
					strArrays[2] = " ";
					strArrays[3] = DwarfName.thing(true);
					strArrays[4] = DwarfName.thing(false);
					str = string.Concat(strArrays);
					break;
				}
				case 5:
				case 6:
				{
					string[] strArrays1 = new string[] { DwarfName.prefix(), null, null, null, null };
					strArrays1[1] = (male ? DwarfName.suffix_male() : DwarfName.suffix_female());
					strArrays1[2] = " ";
					strArrays1[3] = DwarfName.prefix();
					strArrays1[4] = (male ? DwarfName.suffix_male() : DwarfName.suffix_female());
					str = string.Concat(strArrays1);
					break;
				}
				case 7:
				{
					string[] strArrays2 = new string[] { DwarfName.prefix(), null, null, null, null, null, null, null, null, null };
					strArrays2[1] = (male ? DwarfName.suffix_male() : DwarfName.suffix_female());
					strArrays2[2] = " ";
					strArrays2[3] = DwarfName.prefix();
					strArrays2[4] = (male ? DwarfName.suffix_male() : DwarfName.suffix_female());
					strArrays2[5] = " '";
					strArrays2[6] = DwarfName.thing(true);
					strArrays2[7] = "-";
					strArrays2[8] = DwarfName.thing(false);
					strArrays2[9] = "'";
					str = string.Concat(strArrays2);
					break;
				}
			}
			return TextHelper.Capitalise(str, true);
		}

		private static string prefix()
		{
			List<string> strs = new List<string>()
			{
				"Al",
				"An",
				"Ar",
				"Ara",
				"Az",
				"Bal",
				"Bar",
				"Bari",
				"Baz",
				"Bel",
				"Bof",
				"Bol",
				"Dal",
				"Dar",
				"Dare",
				"Del",
				"Dol",
				"Dor",
				"Dora",
				"Duer",
				"Dur",
				"Duri",
				"Dw",
				"Dwo",
				"Dy",
				"El",
				"Er",
				"Eri",
				"Fal",
				"Fall",
				"Far",
				"Gar",
				"Gil",
				"Gim",
				"Glan",
				"Glor",
				"Glori",
				"Har",
				"Hel",
				"Jar",
				"Kil",
				"Ma",
				"Mar",
				"Mor",
				"Mori",
				"Nal",
				"Nor",
				"Nora",
				"Nur",
				"Nura",
				"Ol",
				"Or",
				"Ori",
				"Ov",
				"Rei",
				"Th",
				"Ther",
				"Tho",
				"Thor",
				"Thr",
				"Thra",
				"Tor",
				"Tore",
				"Ur",
				"Urni",
				"Val",
				"Von",
				"Wer",
				"Wera",
				"Whur",
				"Yur"
			};
			return strs[Session.Random.Next(strs.Count)];
		}

		public static string Sentence()
		{
			string str = "";
			int num = Session.Dice(4, 8);
			for (int i = 0; i != num; i++)
			{
				string lower = "";
				int num1 = 0;
				switch (Session.Random.Next(4))
				{
					case 0:
					{
						num1 = 1;
						break;
					}
					case 1:
					case 2:
					{
						num1 = 2;
						break;
					}
					case 3:
					{
						num1 = 3;
						break;
					}
				}
				for (int j = 0; j != num1; j++)
				{
					switch (Session.Random.Next(2))
					{
						case 0:
						{
							lower = string.Concat(lower, DwarfName.prefix());
							break;
						}
						case 1:
						{
							lower = string.Concat(lower, DwarfName.suffix_male());
							break;
						}
					}
					if (j != num1 && Session.Random.Next(10) == 0)
					{
						List<string> strs = new List<string>()
						{
							"k",
							"z",
							"g",
							"-",
							"'"
						};
						int num2 = Session.Random.Next(strs.Count);
						lower = string.Concat(lower, strs[num2]);
					}
				}
				lower = lower.ToLower();
				if (str != "")
				{
					str = string.Concat(str, " ");
					if (Session.Random.Next(20) == 0)
					{
						lower = TextHelper.Capitalise(lower, false);
					}
				}
				else
				{
					lower = TextHelper.Capitalise(lower, false);
				}
				str = string.Concat(str, lower);
			}
			str = string.Concat(str, ".");
			return str;
		}

		private static string suffix_female()
		{
			List<string> strs = new List<string>()
			{
				"aed",
				"ala",
				"la",
				"alsia",
				"ana",
				"ani",
				"astr",
				"bela",
				"bera",
				"bena",
				"bo",
				"bryn",
				"deth",
				"dis",
				"dred",
				"drid",
				"dris",
				"esli",
				"gret",
				"gunn",
				"hild",
				"ia",
				"ida",
				"iess",
				"iff",
				"ifra",
				"ila",
				"ild",
				"ina",
				"ip",
				"ippa",
				"isi",
				"iz",
				"ja",
				"kara",
				"li",
				"ili",
				"lin",
				"lydd",
				"mora",
				"moa",
				"ola",
				"on",
				"ona",
				"ora",
				"oa",
				"re",
				"rra",
				"ren",
				"serd",
				"shar",
				"sha",
				"thra",
				"tia",
				"tryd",
				"unn",
				"wynn",
				"ya",
				"ydd"
			};
			return strs[Session.Random.Next(strs.Count)];
		}

		private static string suffix_male()
		{
			List<string> strs = new List<string>()
			{
				"aim",
				"and",
				"ain",
				"arn",
				"ak",
				"ar",
				"ard",
				"auk",
				"bere",
				"bir",
				"bin",
				"dak",
				"dek",
				"dal",
				"din",
				"el",
				"ent",
				"erl",
				"gal",
				"gan",
				"gar",
				"gath",
				"gen",
				"grim",
				"gur",
				"guk",
				"ik",
				"ias",
				"ili",
				"li",
				"im",
				"rim",
				"in",
				"rin",
				"ir",
				"init",
				"kas",
				"kral",
				"lond",
				"oak",
				"on",
				"lon",
				"or",
				"ror",
				"oril",
				"oric",
				"rak",
				"ral",
				"rek",
				"ric",
				"rid",
				"rim",
				"ring",
				"ster",
				"stili",
				"sun",
				"ten",
				"thal",
				"then",
				"thic",
				"thur",
				"ur",
				"rur",
				"urt",
				"ut",
				"unt",
				"val",
				"var",
				"villi"
			};
			return strs[Session.Random.Next(strs.Count)];
		}

		private static string thing(bool first)
		{
			List<string> strs = new List<string>()
			{
				"forge",
				"anvil",
				"hammer",
				"maul",
				"shield",
				"hide",
				"gold",
				"silver",
				"bronze",
				"brass",
				"steel",
				"iron",
				"copper",
				"glint",
				"rock",
				"stone",
				"crag",
				"mountain",
				"cave",
				"wrath",
				"foe",
				"bound"
			};
			if (!first)
			{
				strs.Add("tooth");
				strs.Add("bone");
				strs.Add("heart");
				strs.Add("fist");
				strs.Add("hold");
				strs.Add("fast");
				strs.Add("guard");
				strs.Add("hunter");
				strs.Add("killer");
				strs.Add("delver");
				strs.Add("crusher");
				strs.Add("mauler");
				strs.Add("breaker");
				strs.Add("smasher");
				strs.Add("slayer");
				strs.Add("striker");
				strs.Add("keeper");
				strs.Add("warden");
				strs.Add("smith");
				strs.Add("mason");
				strs.Add("friend");
				strs.Add("master");
			}
			else
			{
				strs.Add("goblin");
				strs.Add("drake");
				strs.Add("giant");
				strs.Add("wolf");
				strs.Add("boar");
				strs.Add("bear");
				strs.Add("red");
				strs.Add("black");
				strs.Add("white");
				strs.Add("winter");
				strs.Add("ice");
				strs.Add("storm");
				strs.Add("deep");
				strs.Add("dark");
				strs.Add("mighty");
				strs.Add("stout");
				strs.Add("proud");
				strs.Add("brave");
				strs.Add("bold");
				strs.Add("sure");
				strs.Add("strong");
				strs.Add("wise");
				strs.Add("riven");
			}
			return strs[Session.Random.Next(strs.Count)];
		}
	}
}