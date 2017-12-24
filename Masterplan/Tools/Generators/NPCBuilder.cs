using Masterplan;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using Utils;

namespace Masterplan.Tools.Generators
{
	internal class NPCBuilder
	{
		private static string[] fProfession;

		private static string[] fAge;

		private static string[] fHeight;

		private static string[] fWeight;

		private static string[] fHairColour;

		private static string[] fHairStyle;

		private static string[] fPhysical;

		private static string[] fMental;

		private static string[] fSpeech;

		static NPCBuilder()
		{
			string[] strArrays = new string[] { "apothecary", "architect", "armourer", "arrowsmith", "astrologer", "baker", "barber", "lawyer", "beggar", "bellfounder", "blacksmith", "bookbinder", "brewer", "bricklayer", "butcher", "carpenter", "carter", "cartwright", "chandler", "peddler", "clerk", "clockmaker", "cobbler", "cook", "cooper", "merchant", "embroiderer", "engraver", "fisherman", "fishmonger", "forester", "furrier", "gardener", "gemcutter", "glassblower", "goldsmith", "grocer", "haberdasher", "stableman", "courtier", "herbalist", "innkeeper", "ironmonger", "labourer", "painter", "locksmith", "mason", "messenger", "miller", "miner", "minstrel", "ploughman", "farmer", "porter", "sailor", "scribe", "seamstress", "shepherd", "shipwright", "soapmaker", "tailor", "tinker", "vintner", "weaver" };
			NPCBuilder.fProfession = strArrays;
			string[] strArrays1 = new string[] { "elderly", "middle-aged", "teenage", "youthful", "young", "old" };
			NPCBuilder.fAge = strArrays1;
			string[] strArrays2 = new string[] { "gangly", "gigantic", "hulking", "lanky", "short", "small", "stumpy", "tall", "tiny", "willowy" };
			NPCBuilder.fHeight = strArrays2;
			string[] strArrays3 = new string[] { "broad-shouldered", "fat", "gaunt", "obese", "plump", "pot-bellied", "rotund", "skinny", "slender", "slim", "statuesque", "stout", "thin" };
			NPCBuilder.fWeight = strArrays3;
			string[] strArrays4 = new string[] { "black", "brown", "dark brown", "light brown", "red", "ginger", "strawberry blonde", "blonde", "ash blonde", "graying", "silver", "white", "gray", "auburn" };
			NPCBuilder.fHairColour = strArrays4;
			string[] strArrays5 = new string[] { "short", "cropped", "long", "braided", "dreadlocked", "shoulder-length", "wiry", "balding", "receeding", "curly", "tightly-curled", "straight", "greasy", "limp", "sparse", "thinning", "wavy" };
			NPCBuilder.fHairStyle = strArrays5;
			string[] strArrays6 = new string[] { "bearded", "buck-toothed", "chiselled", "doe-eyed", "fine-featured", "florid", "gap-toothed", "goggle-eyed", "grizzled", "jowly", "jug-eared", "pock-marked", "broken nose", "red-cheeked", "scarred", "squinting", "thin-lipped", "toothless", "weather-beaten", "wrinkled" };
			NPCBuilder.fPhysical = strArrays6;
			string[] strArrays7 = new string[] { "hot-tempered", "overbearing", "antagonistic", "haughty", "elitist", "proud", "rude", "aloof", "mischievous", "impulsive", "lusty", "irreverent", "madcap", "thoughtless", "absent-minded", "insensitive", "brave", "craven", "shy", "fearless", "obsequious", "inquisitive", "prying", "intellectual", "perceptive", "keen", "perfectionist", "stern", "harsh", "punctual", "driven", "trusting", "kind-hearted", "forgiving", "easy-going", "compassionate", "miserly", "hard-hearted", "covetous", "avaricious", "thrifty", "wastrel", "spendthrift", "extravagant", "kind", "charitable", "gloomy", "morose", "compulsive", "irritable", "vengeful", "honest", "truthful", "innocent", "gullible", "bigoted", "biased", "narrow-minded", "cheerful", "happy", "diplomatic", "pleasant", "foolhardy", "affable", "fatalistic", "depressing", "cynical", "sarcastic", "realistic", "secretive", "retiring", "practical", "level-headed", "dull", "reverent", "scheming", "paranoid", "cautious", "deceitful", "nervous", "uncultured", "boorish", "barbaric", "graceless", "crude", "cruel", "sadistic", "immoral", "jealous", "belligerent", "argumentative", "arrogant", "careless", "curious", "exacting", "friendly", "greedy", "generous", "moody", "naive", "opinionated", "optimistic", "pessimistic", "quiet", "sober", "suspicious", "uncivilised", "violent", "peaceful" };
			NPCBuilder.fMental = strArrays7;
			string[] strArrays8 = new string[] { "accented", "articulate", "garrulous", "breathless", "crisp", "gutteral", "high-pitched", "lisping", "loud", "nasal", "slow", "fast", "squeaky", "stuttering", "wheezy", "whiny", "whispery", "soft-spoken", "laconic", "blustering" };
			NPCBuilder.fSpeech = strArrays8;
		}

		public NPCBuilder()
		{
		}

		public static string Description()
		{
			string str = NPCBuilder.fAge[Session.Random.Next((int)NPCBuilder.fAge.Length)];
			string str1 = NPCBuilder.fProfession[Session.Random.Next((int)NPCBuilder.fProfession.Length)];
			string str2 = "";
			switch (Session.Random.Next(3))
			{
				case 0:
				case 1:
				{
					str2 = str1;
					break;
				}
				case 2:
				{
					str2 = string.Concat(str, " ", str1);
					break;
				}
			}
			str2 = string.Concat((TextHelper.StartsWithVowel(str2) ? "An" : "A"), " ", str2);
			string str3 = NPCBuilder.fHeight[Session.Random.Next((int)NPCBuilder.fHeight.Length)];
			string str4 = NPCBuilder.fWeight[Session.Random.Next((int)NPCBuilder.fWeight.Length)];
			string str5 = "";
			switch (Session.Random.Next(4))
			{
				case 0:
				case 1:
				{
					str5 = string.Concat(str3, " and ", str4);
					break;
				}
				case 2:
				{
					str5 = str3;
					break;
				}
				case 3:
				{
					str5 = str4;
					break;
				}
			}
			string str6 = NPCBuilder.fHairColour[Session.Random.Next((int)NPCBuilder.fHairColour.Length)];
			string str7 = NPCBuilder.fHairStyle[Session.Random.Next((int)NPCBuilder.fHairStyle.Length)];
			string str8 = string.Concat(str7, " ", str6);
			string str9 = "";
			switch (Session.Random.Next(4))
			{
				case 0:
				case 1:
				{
					string[] strArrays = new string[] { str2, ", ", str5, " with ", str8, " hair." };
					str9 = string.Concat(strArrays);
					break;
				}
				case 2:
				{
					str9 = string.Concat(str2, " with ", str8, " hair.");
					break;
				}
				case 3:
				{
					str9 = string.Concat(str2, ", ", str5, ".");
					break;
				}
			}
			return str9;
		}

		private static int get_number()
		{
			switch (Session.Random.Next(10))
			{
				case 0:
				{
					return 0;
				}
				case 1:
				case 2:
				case 3:
				{
					return 1;
				}
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				{
					return 2;
				}
				case 9:
				{
					return 3;
				}
			}
			return 1;
		}

		private static string get_values(string[] array)
		{
			int _number = NPCBuilder.get_number();
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			while (binarySearchTree.Count != _number)
			{
				string str = array[Session.Random.Next((int)array.Length)];
				binarySearchTree.Add(str);
			}
			List<string> sortedList = binarySearchTree.SortedList;
			string str1 = "";
			foreach (string str2 in sortedList)
			{
				if (str1 != "")
				{
					str1 = (str2 != sortedList[sortedList.Count - 1] ? string.Concat(str1, ", ") : string.Concat(str1, " and "));
				}
				str1 = string.Concat(str1, str2);
			}
			if (str1 != "")
			{
				str1 = TextHelper.Capitalise(str1, false);
			}
			return str1;
		}

		public static string Personality()
		{
			return NPCBuilder.get_values(NPCBuilder.fMental);
		}

		public static string Physical()
		{
			return NPCBuilder.get_values(NPCBuilder.fPhysical);
		}

		public static string Speech()
		{
			return NPCBuilder.get_values(NPCBuilder.fSpeech);
		}
	}
}