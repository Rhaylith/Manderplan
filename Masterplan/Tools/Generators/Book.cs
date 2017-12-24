using Masterplan;
using Masterplan.Tools;
using System;
using System.Collections.Generic;

namespace Masterplan.Tools.Generators
{
	internal class Book
	{
		private static string[] fPrepositions;

		private static string[] fAbout;

		static Book()
		{
			string[] strArrays = new string[] { "and", "in", "of", "with", "without", "against", "for", "to" };
			Book.fPrepositions = strArrays;
			string[] strArrays1 = new string[] { "about", "on", "concerning", "regarding" };
			Book.fAbout = strArrays1;
		}

		public Book()
		{
		}

		private static string about()
		{
			int num = Session.Random.Next((int)Book.fAbout.Length);
			return Book.fAbout[num];
		}

		private static string adjective()
		{
			List<string> strs = new List<string>()
			{
				"dark",
				"bright",
				"tyrannous",
				"devout",
				"noble",
				"eldritch",
				"mystical",
				"magical",
				"sorcerous",
				"savage",
				"silent",
				"lonely",
				"violent",
				"peaceful",
				"black",
				"white",
				"gold",
				"silver",
				"red",
				"pale",
				"dying",
				"living",
				"ascending",
				"defiled",
				"mythical",
				"legendary",
				"heroic",
				"empty",
				"mighty",
				"despairing",
				"spellbound",
				"enchanted",
				"soaring",
				"falling",
				"visionary",
				"bold",
				"perilous"
			};
			return strs[Session.Random.Next(strs.Count)];
		}

		private static string gerund()
		{
			List<string> strs = new List<string>()
			{
				"killing",
				"murdering",
				"watching",
				"examining",
				"enchanting",
				"destroying",
				"defying",
				"betraying",
				"protecting",
				"silencing",
				"bearing",
				"fighting"
			};
			return strs[Session.Random.Next(strs.Count)];
		}

		private static string noun(bool concrete)
		{
			List<string> strs = new List<string>();
			if (!concrete)
			{
				strs.Add("darkness");
				strs.Add("light");
				strs.Add("dusk");
				strs.Add("twilight");
				strs.Add("revenge");
				strs.Add("vengeance");
				strs.Add("blood");
				strs.Add("earth");
				strs.Add("water");
				strs.Add("ice");
				strs.Add("wood");
				strs.Add("metal");
				strs.Add("lightning");
				strs.Add("thunder");
				strs.Add("mist");
				strs.Add("destruction");
				strs.Add("life");
				strs.Add("death");
				strs.Add("time");
				strs.Add("end");
				strs.Add("danger");
				strs.Add("luck");
				strs.Add("chaos");
				strs.Add("truth");
				strs.Add("music");
				strs.Add("one");
				strs.Add("two");
				strs.Add("three");
				strs.Add("four");
				strs.Add("five");
				strs.Add("six");
				strs.Add("seven");
				strs.Add("eight");
				strs.Add("nine");
				strs.Add("ten");
				strs.Add("eleven");
				strs.Add("twelve");
			}
			else
			{
				strs.Add("elf");
				strs.Add("eladrin");
				strs.Add("halfling");
				strs.Add("dwarf");
				strs.Add("gnome");
				strs.Add("deva");
				strs.Add("tiefling");
				strs.Add("dragonborn");
				strs.Add("goliath");
				strs.Add("changeling");
				strs.Add("drow");
				strs.Add("minotaur");
				strs.Add("beast");
				strs.Add("orc");
				strs.Add("goblin");
				strs.Add("hobgoblin");
				strs.Add("dragon");
				strs.Add("demon");
				strs.Add("devil");
				strs.Add("angel");
				strs.Add("god");
				strs.Add("gith");
				strs.Add("night");
				strs.Add("day");
				strs.Add("eclipse");
				strs.Add("shadow");
				strs.Add("sun");
				strs.Add("moon");
				strs.Add("star");
				strs.Add("battle");
				strs.Add("war");
				strs.Add("brawl");
				strs.Add("fist");
				strs.Add("blade");
				strs.Add("arrow");
				strs.Add("spell");
				strs.Add("prayer");
				strs.Add("eye");
				strs.Add("wing");
				strs.Add("army");
				strs.Add("legion");
				strs.Add("brigade");
				strs.Add("galleon");
				strs.Add("warship");
				strs.Add("frigate");
				strs.Add("potion");
				strs.Add("jewel");
				strs.Add("ring");
				strs.Add("amulet");
				strs.Add("cloak");
				strs.Add("sword");
				strs.Add("spear");
				strs.Add("helm");
				strs.Add("flame");
				strs.Add("wizard");
				strs.Add("king");
				strs.Add("queen");
				strs.Add("prince");
				strs.Add("princess");
				strs.Add("warlock");
				strs.Add("barbarian");
				strs.Add("sorcerer");
				strs.Add("thief");
				strs.Add("mage");
				strs.Add("child");
				strs.Add("wayfarer");
				strs.Add("adventurer");
				strs.Add("pirate");
				strs.Add("spy");
				strs.Add("sage");
				strs.Add("assassin");
				strs.Add("mountain");
				strs.Add("forest");
				strs.Add("peak");
				strs.Add("cave");
				strs.Add("cavern");
				strs.Add("lake");
				strs.Add("swamp");
				strs.Add("marshland");
				strs.Add("island");
				strs.Add("shore");
				strs.Add("city");
				strs.Add("town");
				strs.Add("village");
				strs.Add("tower");
				strs.Add("arena");
				strs.Add("castle");
				strs.Add("citadel");
				strs.Add("game");
				strs.Add("wager");
				strs.Add("quest");
				strs.Add("challenge");
				strs.Add("rose");
				strs.Add("lily");
				strs.Add("thorn");
				strs.Add("word");
				strs.Add("snake");
				strs.Add("song");
				strs.Add("lament");
				strs.Add("dirge");
				strs.Add("elegy");
			}
			strs.Add("wind");
			strs.Add("stone");
			strs.Add("fire");
			strs.Add("storm");
			return strs[Session.Random.Next(strs.Count)];
		}

		private static string noun_phrase(bool concrete_noun, bool article)
		{
			string str = Book.noun(concrete_noun);
			bool flag = false;
			if (concrete_noun && Session.Random.Next(5) == 0)
			{
				str = string.Concat(str, "s");
				flag = true;
			}
			if (Session.Random.Next(3) == 0)
			{
				str = string.Concat(Book.adjective(), " ", str);
			}
			if (article && Session.Random.Next(2) == 0)
			{
				switch (Session.Random.Next(2))
				{
					case 0:
					{
						str = string.Concat("the ", str);
						break;
					}
					case 1:
					{
						if (flag)
						{
							switch (Session.Random.Next(6))
							{
								case 0:
								{
									str = string.Concat("two ", str);
									break;
								}
								case 1:
								{
									str = string.Concat("three ", str);
									break;
								}
								case 2:
								{
									str = string.Concat("four ", str);
									break;
								}
								case 3:
								{
									str = string.Concat("five ", str);
									break;
								}
								case 4:
								{
									str = string.Concat("six ", str);
									break;
								}
								case 5:
								{
									str = string.Concat("seven ", str);
									break;
								}
							}
						}
						else
						{
							switch (Session.Random.Next(2))
							{
								case 0:
								{
									str = string.Concat((TextHelper.StartsWithVowel(str) ? "an" : "a"), " ", str);
									break;
								}
								case 1:
								{
									str = string.Concat("one ", str);
									break;
								}
							}
						}
						break;
					}
				}
			}
			return str;
		}

		private static string preposition()
		{
			int num = Session.Random.Next((int)Book.fPrepositions.Length);
			return Book.fPrepositions[num];
		}

		public static string Title()
		{
			string str = "";
			switch (Session.Random.Next(5))
			{
				case 0:
				{
					bool flag = Session.Random.Next(2) == 0;
					bool flag1 = Session.Random.Next(2) == 0;
					str = string.Concat(Book.noun_phrase(flag, flag), "'s ", Book.noun_phrase(flag1, false));
					break;
				}
				case 1:
				{
					bool flag2 = Session.Random.Next(2) == 0;
					bool flag3 = Session.Random.Next(2) == 0;
					string[] strArrays = new string[] { Book.noun_phrase(flag2, flag2), " ", Book.preposition(), " ", Book.noun_phrase(flag3, flag3) };
					str = string.Concat(strArrays);
					break;
				}
				case 2:
				{
					bool flag4 = Session.Random.Next(2) == 0;
					str = string.Concat(Book.gerund(), " the ", Book.noun_phrase(flag4, false));
					break;
				}
				case 3:
				{
					if (Session.Random.Next(2) != 0)
					{
						str = string.Concat(Book.about(), " ", Book.noun(false));
						break;
					}
					else
					{
						str = string.Concat(Book.about(), " ", Book.noun(true), "s");
						break;
					}
				}
				case 4:
				{
					bool flag5 = Session.Random.Next(2) == 0;
					str = Book.noun_phrase(flag5, true);
					break;
				}
			}
			if (Session.Random.Next(10) == 0)
			{
				string str1 = "";
				switch (Session.Random.Next(2))
				{
					case 0:
					{
						str1 = "volume";
						break;
					}
					case 1:
					{
						str1 = "part";
						break;
					}
				}
				switch (Session.Random.Next(5))
				{
					case 0:
					{
						str = string.Concat(str, ", ", str1, " one");
						break;
					}
					case 1:
					{
						str = string.Concat(str, ", ", str1, " two");
						break;
					}
					case 2:
					{
						str = string.Concat(str, ", ", str1, " three");
						break;
					}
					case 3:
					{
						str = string.Concat(str, ", ", str1, " four");
						break;
					}
					case 4:
					{
						str = string.Concat(str, ", ", str1, " five");
						break;
					}
				}
			}
			str = TextHelper.Capitalise(str, true);
			return str;
		}
	}
}