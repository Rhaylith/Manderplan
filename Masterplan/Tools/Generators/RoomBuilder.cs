using Masterplan;
using System;
using System.Collections.Generic;

namespace Masterplan.Tools.Generators
{
	internal class RoomBuilder
	{
		public RoomBuilder()
		{
		}

		public static string Details()
		{
			List<string> strs = new List<string>();
			while (strs.Count == 0)
			{
				if (Session.Random.Next(2) == 0)
				{
					strs.Add(RoomBuilder.random_wall());
				}
				if (Session.Random.Next(3) == 0)
				{
					strs.Add(RoomBuilder.random_finish());
				}
				if (Session.Random.Next(4) == 0)
				{
					strs.Add(RoomBuilder.random_air());
				}
				if (Session.Random.Next(5) == 0)
				{
					strs.Add(RoomBuilder.random_smell());
				}
				if (Session.Random.Next(5) == 0)
				{
					strs.Add(RoomBuilder.random_sound());
				}
				if (Session.Random.Next(10) != 0)
				{
					continue;
				}
				strs.Add(RoomBuilder.random_activity());
			}
			string str = "";
			foreach (string str1 in strs)
			{
				if (str != "")
				{
					str = string.Concat(str, " ");
				}
				str = string.Concat(str, str1);
			}
			return str;
		}

		public static string Name()
		{
			List<string> strs = new List<string>()
			{
				"Antechamber",
				"Arena",
				"Armoury",
				"Aviary",
				"Audience Chamber",
				"Banquet Hall",
				"Bath Chamber",
				"Barracks",
				"Bedroom",
				"Boudoir",
				"Bestiary",
				"Burial Chamber",
				"Cells",
				"Chamber",
				"Chantry",
				"Chapel",
				"Classroom",
				"Closet",
				"Court",
				"Crypt",
				"Dining Room",
				"Dormitory",
				"Dressing Room",
				"Dumping Ground",
				"Entrance Hall",
				"Gallery",
				"Game Room",
				"Great Hall",
				"Guard Post",
				"Hall",
				"Harem",
				"Hoard",
				"Infirmary",
				"Kennels",
				"Kitchens",
				"Laboratory",
				"Lair",
				"Library",
				"Mausoleum",
				"Meditation Room",
				"Museum",
				"Nursery",
				"Observatory",
				"Office",
				"Pantry",
				"Prison",
				"Quarters",
				"Reception Room",
				"Refectory",
				"Ritual Chamber",
				"Shrine",
				"Smithy",
				"Stable",
				"Storeroom",
				"Study",
				"Temple",
				"Throne Room",
				"Torture Chamber",
				"Trophy Room",
				"Training Area",
				"Treasury",
				"Waiting Room",
				"Workroom",
				"Workshop",
				"Vault",
				"Vestibule"
			};
			int num = Session.Random.Next() % strs.Count;
			return strs[num];
		}

		private static string random_activity()
		{
			List<string> strs = new List<string>()
			{
				"The dust swirls as if disturbed by movement.",
				"You catch a sudden movement out of the corner of your eye.",
				"From time to time tiny pieces of debris fall from the ceiling.",
				"Water drips slowly from a crack in the ceiling.",
				"Water drips down the walls."
			};
			int num = Session.Random.Next() % strs.Count;
			return strs[num];
		}

		private static string random_air()
		{
			List<string> strs = new List<string>()
			{
				"The room is bitterly cold.",
				"There is a distinct chill in the air.",
				"A cold breeze blows through this area.",
				"A warm draught blows through this area.",
				"The area is uncomfortably hot.",
				"The air is dank.",
				"The air here is warm and humid.",
				"A strange mist hangs in the air here.",
				"The room's surfaces are covered in frost."
			};
			int num = Session.Random.Next() % strs.Count;
			return strs[num];
		}

		private static string random_colour()
		{
			List<string> strs = new List<string>()
			{
				"black",
				"white",
				"grey",
				"red",
				"blue",
				"yellow",
				"purple",
				"green",
				"orange",
				"brown",
				"pink"
			};
			int num = Session.Random.Next() % strs.Count;
			return strs[num];
		}

		private static string random_deco()
		{
			List<string> strs = new List<string>()
			{
				"abstract artwork",
				"battle scenes",
				"landscape scenes",
				"hunting scenes",
				"pastoral scenes",
				"religious symbols",
				"runes",
				"glyphs",
				"sigils",
				"cryptic signs"
			};
			int num = Session.Random.Next() % strs.Count;
			return strs[num];
		}

		private static string random_finish()
		{
			List<string> strs = new List<string>()
			{
				"The walls here are covered in black soot."
			};
			string[] strArrays = new string[] { "The walls are ", RoomBuilder.random_style(), " with ", RoomBuilder.random_deco(), "." };
			strs.Add(string.Concat(strArrays));
			strs.Add("Claw marks cover the walls here.");
			strs.Add(string.Concat("The walls have been painted ", RoomBuilder.random_colour(), "."));
			strs.Add("It is possible to tell that the walls were once plastered.");
			strs.Add("Here and there, graffiti covers the walls.");
			strs.Add("A thick layer of dust covers the room.");
			strs.Add("Moss and lichen grows here and there on the walls.");
			strs.Add("The patina of age covers the walls.");
			strs.Add("Childlike drawings and sketches cover the walls.");
			strs.Add("Cryptic signs have been scratched into the walls.");
			int num = Session.Random.Next() % strs.Count;
			return strs[num];
		}

		private static string random_material()
		{
			List<string> strs = new List<string>()
			{
				"granite",
				"slate",
				"sandstone",
				"brick",
				"marble",
				"slabs of rock"
			};
			int num = Session.Random.Next() % strs.Count;
			string item = strs[num];
			if (Session.Random.Next(3) == 0)
			{
				List<string> strs1 = new List<string>()
				{
					"polished",
					"rough",
					"chiselled",
					"uneven",
					"worked"
				};
				int num1 = Session.Random.Next() % strs1.Count;
				item = string.Concat(strs1[num1], " ", item);
			}
			return item;
		}

		private static string random_smell()
		{
			List<string> strs = new List<string>()
			{
				"A smell of burning hangs in the air.",
				"The air feels stagnant.",
				"From time to time the smell of blood catches your nostrils.",
				"The stench of rotting meat is in the air.",
				"The area has a strange musky smell.",
				"You notice a strangly acrid smell throughout the area.",
				"The area is filled with an unpleasant putrid smell."
			};
			int num = Session.Random.Next() % strs.Count;
			return strs[num];
		}

		private static string random_sound()
		{
			List<string> strs = new List<string>()
			{
				"You can hear distant chanting.",
				"You can hear a quiet buzzing sound.",
				"The sound of running water fills this area.",
				"A low humming sound can be heard.",
				"The area is unnaturally quiet.",
				"A quiet susurration can just be heard.",
				"Creaking sounds fill the area.",
				"Scratching sounds can be heard.",
				"From time to time, a distant voice can be heard."
			};
			int num = Session.Random.Next() % strs.Count;
			return strs[num];
		}

		private static string random_style()
		{
			List<string> strs = new List<string>()
			{
				"painted",
				"engraved"
			};
			int num = Session.Random.Next() % strs.Count;
			return strs[num];
		}

		private static string random_wall()
		{
			List<string> strs = new List<string>()
			{
				string.Concat("The walls of this area are ", RoomBuilder.random_material(), ".")
			};
			string[] strArrays = new string[] { "The walls of this area are ", RoomBuilder.random_material(), " and ", RoomBuilder.random_material(), "." };
			strs.Add(string.Concat(strArrays));
			strs.Add(string.Concat("The floor of this area is made from ", RoomBuilder.random_material(), "."));
			string[] strArrays1 = new string[] { "The walls of this area are made of ", RoomBuilder.random_material(), ", while the ceiling and floor are ", RoomBuilder.random_material(), "." };
			strs.Add(string.Concat(strArrays1));
			strs.Add("This area has been hewn out of solid rock.");
			strs.Add("This area has been panelled in dark wood.");
			int num = Session.Random.Next() % strs.Count;
			return strs[num];
		}
	}
}