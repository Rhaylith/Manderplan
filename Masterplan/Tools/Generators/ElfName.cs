using Masterplan;
using Masterplan.Tools;
using System;
using System.Collections.Generic;

namespace Masterplan.Tools.Generators
{
	internal class ElfName
	{
		public ElfName()
		{
		}

		public static string FemaleName()
		{
			return ElfName.name(false);
		}

		public static string MaleName()
		{
			return ElfName.name(true);
		}

		private static string name(bool male)
		{
			string str = "";
			switch (Session.Random.Next(10))
			{
				case 0:
				case 1:
				case 2:
				case 3:
				{
					str = string.Concat(ElfName.prefix(), ElfName.suffix(male));
					break;
				}
				case 4:
				case 5:
				case 6:
				{
					str = string.Concat(ElfName.prefix(), ElfName.suffix(male), ElfName.suffix(male));
					break;
				}
				case 7:
				case 8:
				{
					string[] strArrays = new string[] { ElfName.prefix(), ElfName.suffix(male), " ", ElfName.prefix(), ElfName.suffix(male) };
					str = string.Concat(strArrays);
					break;
				}
				case 9:
				{
					string[] strArrays1 = new string[] { ElfName.suffix(male), "'", ElfName.prefix(), ElfName.suffix(male), ElfName.suffix(male) };
					str = string.Concat(strArrays1);
					break;
				}
			}
			return TextHelper.Capitalise(str, true);
		}

		private static string prefix()
		{
			List<string> strs = new List<string>()
			{
				"ael",
				"aer",
				"af",
				"ah",
				"al",
				"am",
				"ama",
				"an",
				"ang",
				"ansr",
				"ar",
				"ari",
				"arn",
				"aza",
				"bael",
				"bes",
				"cael",
				"cal",
				"cas",
				"cla",
				"cor",
				"cy",
				"dae",
				"dho",
				"dre",
				"du",
				"eli",
				"eir",
				"el",
				"er",
				"ev",
				"fera",
				"fi",
				"fir",
				"fis",
				"gael",
				"gar",
				"gil",
				"ha",
				"hu",
				"ia",
				"il",
				"ja",
				"jar",
				"ka",
				"kan",
				"ker",
				"keth",
				"koeh",
				"kor",
				"ky",
				"la",
				"laf",
				"lam",
				"lue",
				"ly",
				"mai",
				"mal",
				"mara",
				"my",
				"na",
				"nai",
				"nim",
				"nu",
				"ny",
				"py",
				"raer",
				"re",
				"ren",
				"rid",
				"ru",
				"rua",
				"rum",
				"ry",
				"sae",
				"seh",
				"sel",
				"sha",
				"she",
				"si",
				"sim",
				"sol",
				"sum",
				"syl",
				"ta",
				"tahl",
				"tha",
				"tho",
				"ther",
				"thro",
				"tia",
				"tra",
				"ty",
				"uth",
				"ver",
				"vil",
				"von",
				"ya",
				"za",
				"zy"
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
				switch (Session.Random.Next(6))
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
					case 4:
					{
						num1 = 3;
						break;
					}
					case 5:
					{
						num1 = 4;
						break;
					}
				}
				for (int j = 0; j != num1; j++)
				{
					switch (Session.Random.Next(3))
					{
						case 0:
						{
							lower = string.Concat(lower, ElfName.prefix());
							break;
						}
						case 1:
						{
							lower = string.Concat(lower, ElfName.suffix(true));
							break;
						}
						case 2:
						{
							lower = string.Concat(lower, ElfName.suffix(false));
							break;
						}
					}
					if (j != num1 && Session.Random.Next(10) == 0)
					{
						List<string> strs = new List<string>()
						{
							"y",
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

		private static string suffix(bool male)
		{
			List<string> strs = new List<string>()
			{
				"ae",
				"nae",
				"ael",
				(male ? "aer" : "aera"),
				(male ? "aias" : "aia"),
				(male ? "ah" : "aha"),
				(male ? "aith" : "aira"),
				(male ? "al" : "ala"),
				"ali",
				(male ? "am" : "ama"),
				(male ? "an" : "ana"),
				(male ? "ar" : "ara"),
				"ari",
				"ri",
				"aro",
				"ro",
				"as",
				"ash",
				"sah",
				"ath",
				"avel",
				"brar",
				"abrar",
				"ibrar",
				"dar",
				"adar",
				"odar",
				"deth",
				"eath",
				"eth",
				"dre",
				"drim",
				"drimme",
				"udrim",
				"dul",
				"ean",
				"el",
				(male ? "ele" : "ela"),
				"emar",
				"en",
				"er",
				"erl",
				"ern",
				"ess",
				"esti",
				"evar",
				"fel",
				"afel",
				"efel",
				"hal",
				"ahal",
				"ihal",
				"har",
				"ihar",
				"uhar",
				"hel",
				"ahel",
				"ihel",
				(male ? "ian" : "ianna"),
				"ia",
				"ii",
				"ion",
				"iat",
				"ik",
				(male ? "il" : "ila"),
				"iel",
				"lie",
				"im",
				"in",
				"inar",
				"ine",
				(male ? "ir" : "ira"),
				"ire",
				"is",
				"iss",
				"ist",
				"ith",
				"lath",
				"lith",
				"lyth",
				"kash",
				"ashk",
				"okash",
				"ki",
				(male ? "lan" : "lanna"),
				"lean",
				(male ? "olan" : "ola"),
				"lam",
				"ilam",
				"ulam",
				"lar",
				"lirr",
				"las",
				(male ? "lian" : "lia"),
				"lis",
				"elis",
				"lys",
				"lon",
				"ellon",
				"lyn",
				"llin",
				"lihn",
				(male ? "mah" : "ma"),
				"mahs",
				"mil",
				"imil",
				"umil",
				"mus",
				"nal",
				"inal",
				"onal",
				"nes",
				"nin",
				"nine",
				"nyn",
				"nis",
				"anis",
				(male ? "on" : "onna"),
				"or",
				"oro",
				"oth",
				"othi",
				"que",
				"quis",
				"rah",
				"rae",
				"raee",
				"rad",
				"rahd",
				(male ? "rail" : "ria"),
				"aral",
				"ral",
				"ryl",
				"ran",
				"re",
				"reen",
				"reth",
				"rath",
				"ro",
				"ri",
				"ron",
				"ruil",
				"aruil",
				"eruil",
				"sal",
				"isal",
				"sali",
				"san",
				"sar",
				"asar",
				"isar",
				"sel",
				"asel",
				"isel",
				"sha",
				"she",
				"shor",
				"spar",
				"tae",
				"itae",
				"tas",
				"itas",
				"ten",
				"iten",
				(male ? "thal" : "tha"),
				(male ? "ethel" : "etha"),
				"thar",
				"ethar",
				"ithar",
				"ther",
				"ather",
				"thir",
				"thi",
				"ethil",
				"thil",
				(male ? "thus" : "thas"),
				(male ? "aethus" : "aethas"),
				"ti",
				"eti",
				"til",
				(male ? "tril" : "tria"),
				"atri",
				(male ? "atril" : "atria"),
				"ual",
				"lua",
				"uath",
				"uth",
				"luth",
				(male ? "us" : "ua"),
				(male ? "van" : "vanna"),
				(male ? "var" : "vara"),
				(male ? "avar" : "avara"),
				"vain",
				"avain",
				"via",
				"avia",
				"vin",
				"avin",
				"wyn",
				"ya",
				(male ? "yr" : "yn"),
				"yth",
				(male ? "zair" : "zara"),
				(male ? "azair" : "ezara")
			};
			return strs[Session.Random.Next(strs.Count)];
		}
	}
}