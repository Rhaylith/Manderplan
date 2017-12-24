using Masterplan.Data;
using Masterplan.Extensibility;
using Masterplan.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Utils;
using Utils.Forms;

namespace Masterplan
{
	internal class Session
	{
		private static Masterplan.Data.Project fProject;

		private static Masterplan.Preferences fPreferences;

		private static PlayerViewForm fPlayerView;

		private static bool fModified;

		private static string fFileName;

		private static System.Random fRandom;

		private static List<IAddIn> fAddIns;

		public static List<Library> Libraries;

		private static Masterplan.UI.MainForm fMainForm;

		private static Encounter fCurrentEncounter;

		private static List<string> fDisabledLibraries;

		public static List<IAddIn> AddIns
		{
			get
			{
				return Session.fAddIns;
			}
		}

		public static List<Artifact> Artifacts
		{
			get
			{
				List<Artifact> artifacts = new List<Artifact>();
				foreach (Library library in Session.Libraries)
				{
					foreach (Artifact artifact in library.Artifacts)
					{
						if (artifact == null)
						{
							continue;
						}
						artifacts.Add(artifact);
					}
				}
				if (Session.fProject != null)
				{
					BinarySearchTree<Guid> binarySearchTree = new BinarySearchTree<Guid>();
					foreach (Artifact artifact1 in artifacts)
					{
						if (artifact1 == null)
						{
							continue;
						}
						binarySearchTree.Add(artifact1.ID);
					}
					foreach (Artifact artifact2 in Session.fProject.Library.Artifacts)
					{
						if (artifact2 == null || binarySearchTree.Contains(artifact2.ID))
						{
							continue;
						}
						artifacts.Add(artifact2);
					}
				}
				return artifacts;
			}
		}

		public static List<Creature> Creatures
		{
			get
			{
				List<Creature> creatures = new List<Creature>();
				foreach (Library library in Session.Libraries)
				{
					foreach (Creature creature in library.Creatures)
					{
						if (creature == null)
						{
							continue;
						}
						creatures.Add(creature);
					}
				}
				if (Session.fProject != null)
				{
					BinarySearchTree<Guid> binarySearchTree = new BinarySearchTree<Guid>();
					foreach (Creature creature1 in creatures)
					{
						if (creature1 == null)
						{
							continue;
						}
						binarySearchTree.Add(creature1.ID);
					}
					foreach (Creature creature2 in Session.fProject.Library.Creatures)
					{
						if (creature2 == null || binarySearchTree.Contains(creature2.ID))
						{
							continue;
						}
						creatures.Add(creature2);
					}
				}
				return creatures;
			}
		}

		public static Encounter CurrentEncounter
		{
			get
			{
				return Session.fCurrentEncounter;
			}
			set
			{
				Session.fCurrentEncounter = value;
			}
		}

		public static List<string> DisabledLibraries
		{
			get
			{
				return Session.fDisabledLibraries;
			}
			set
			{
				Session.fDisabledLibraries = value;
			}
		}

		public static string FileName
		{
			get
			{
				return Session.fFileName;
			}
			set
			{
				Session.fFileName = value;
			}
		}

		public static string LibraryFolder
		{
			get
			{
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				return string.Concat(Utils.FileName.Directory(entryAssembly.Location), "Libraries\\");
			}
		}

		public static List<MagicItem> MagicItems
		{
			get
			{
				List<MagicItem> magicItems = new List<MagicItem>();
				foreach (Library library in Session.Libraries)
				{
					foreach (MagicItem magicItem in library.MagicItems)
					{
						if (magicItem == null)
						{
							continue;
						}
						magicItems.Add(magicItem);
					}
				}
				if (Session.fProject != null)
				{
					BinarySearchTree<Guid> binarySearchTree = new BinarySearchTree<Guid>();
					foreach (MagicItem magicItem1 in magicItems)
					{
						if (magicItem1 == null)
						{
							continue;
						}
						binarySearchTree.Add(magicItem1.ID);
					}
					foreach (MagicItem magicItem2 in Session.fProject.Library.MagicItems)
					{
						if (magicItem2 == null || binarySearchTree.Contains(magicItem2.ID))
						{
							continue;
						}
						magicItems.Add(magicItem2);
					}
				}
				return magicItems;
			}
		}

		public static Masterplan.UI.MainForm MainForm
		{
			get
			{
				return Session.fMainForm;
			}
			set
			{
				Session.fMainForm = value;
			}
		}

		public static bool Modified
		{
			get
			{
				return Session.fModified;
			}
			set
			{
				Session.fModified = value;
			}
		}

		public static PlayerViewForm PlayerView
		{
			get
			{
				return Session.fPlayerView;
			}
			set
			{
				Session.fPlayerView = value;
			}
		}

		public static Masterplan.Preferences Preferences
		{
			get
			{
				return Session.fPreferences;
			}
			set
			{
				Session.fPreferences = value;
			}
		}

		public static Masterplan.Data.Project Project
		{
			get
			{
				return Session.fProject;
			}
			set
			{
				Session.fProject = value;
			}
		}

		public static System.Random Random
		{
			get
			{
				return Session.fRandom;
			}
		}

		public static List<SkillChallenge> SkillChallenges
		{
			get
			{
				List<SkillChallenge> skillChallenges = new List<SkillChallenge>();
				foreach (Library library in Session.Libraries)
				{
					skillChallenges.AddRange(library.SkillChallenges);
				}
				if (Session.fProject != null)
				{
					BinarySearchTree<Guid> binarySearchTree = new BinarySearchTree<Guid>();
					foreach (SkillChallenge skillChallenge in skillChallenges)
					{
						if (skillChallenge == null)
						{
							continue;
						}
						binarySearchTree.Add(skillChallenge.ID);
					}
					foreach (SkillChallenge skillChallenge1 in Session.fProject.Library.SkillChallenges)
					{
						if (skillChallenge1 == null || binarySearchTree.Contains(skillChallenge1.ID))
						{
							continue;
						}
						skillChallenges.Add(skillChallenge1);
					}
				}
				return skillChallenges;
			}
		}

		public static List<CreatureTemplate> Templates
		{
			get
			{
				List<CreatureTemplate> creatureTemplates = new List<CreatureTemplate>();
				foreach (Library library in Session.Libraries)
				{
					foreach (CreatureTemplate template in library.Templates)
					{
						if (template == null)
						{
							continue;
						}
						creatureTemplates.Add(template);
					}
				}
				if (Session.fProject != null)
				{
					BinarySearchTree<Guid> binarySearchTree = new BinarySearchTree<Guid>();
					foreach (CreatureTemplate creatureTemplate in creatureTemplates)
					{
						if (creatureTemplate == null)
						{
							continue;
						}
						binarySearchTree.Add(creatureTemplate.ID);
					}
					foreach (CreatureTemplate template1 in Session.fProject.Library.Templates)
					{
						if (template1 == null || binarySearchTree.Contains(template1.ID))
						{
							continue;
						}
						creatureTemplates.Add(template1);
					}
				}
				return creatureTemplates;
			}
		}

		public static List<TerrainPower> TerrainPowers
		{
			get
			{
				List<TerrainPower> terrainPowers = new List<TerrainPower>();
				foreach (Library library in Session.Libraries)
				{
					foreach (TerrainPower terrainPower in library.TerrainPowers)
					{
						if (terrainPower == null)
						{
							continue;
						}
						terrainPowers.Add(terrainPower);
					}
				}
				if (Session.fProject != null)
				{
					BinarySearchTree<Guid> binarySearchTree = new BinarySearchTree<Guid>();
					foreach (TerrainPower terrainPower1 in terrainPowers)
					{
						if (terrainPower1 == null)
						{
							continue;
						}
						binarySearchTree.Add(terrainPower1.ID);
					}
					foreach (TerrainPower terrainPower2 in Session.fProject.Library.TerrainPowers)
					{
						if (terrainPower2 == null || binarySearchTree.Contains(terrainPower2.ID))
						{
							continue;
						}
						terrainPowers.Add(terrainPower2);
					}
				}
				return terrainPowers;
			}
		}

		public static List<MonsterTheme> Themes
		{
			get
			{
				List<MonsterTheme> monsterThemes = new List<MonsterTheme>();
				foreach (Library library in Session.Libraries)
				{
					foreach (MonsterTheme theme in library.Themes)
					{
						if (theme == null)
						{
							continue;
						}
						monsterThemes.Add(theme);
					}
				}
				if (Session.fProject != null)
				{
					BinarySearchTree<Guid> binarySearchTree = new BinarySearchTree<Guid>();
					foreach (MonsterTheme monsterTheme in monsterThemes)
					{
						if (monsterTheme == null)
						{
							continue;
						}
						binarySearchTree.Add(monsterTheme.ID);
					}
					foreach (MonsterTheme theme1 in Session.fProject.Library.Themes)
					{
						if (theme1 == null || binarySearchTree.Contains(theme1.ID))
						{
							continue;
						}
						monsterThemes.Add(theme1);
					}
				}
				return monsterThemes;
			}
		}

		public static List<Tile> Tiles
		{
			get
			{
				List<Tile> tiles = new List<Tile>();
				foreach (Library library in Session.Libraries)
				{
					foreach (Tile tile in library.Tiles)
					{
						if (tile == null)
						{
							continue;
						}
						tiles.Add(tile);
					}
				}
				if (Session.fProject != null)
				{
					BinarySearchTree<Guid> binarySearchTree = new BinarySearchTree<Guid>();
					foreach (Tile tile1 in tiles)
					{
						if (tile1 == null)
						{
							continue;
						}
						binarySearchTree.Add(tile1.ID);
					}
					foreach (Tile tile2 in Session.fProject.Library.Tiles)
					{
						if (tile2 == null || binarySearchTree.Contains(tile2.ID))
						{
							continue;
						}
						tiles.Add(tile2);
					}
				}
				return tiles;
			}
		}

		public static List<Trap> Traps
		{
			get
			{
				List<Trap> traps = new List<Trap>();
				foreach (Library library in Session.Libraries)
				{
					foreach (Trap trap in library.Traps)
					{
						if (trap == null)
						{
							continue;
						}
						traps.Add(trap);
					}
				}
				if (Session.fProject != null)
				{
					BinarySearchTree<Guid> binarySearchTree = new BinarySearchTree<Guid>();
					foreach (Trap trap1 in traps)
					{
						if (trap1 == null)
						{
							continue;
						}
						binarySearchTree.Add(trap1.ID);
					}
					foreach (Trap trap2 in Session.fProject.Library.Traps)
					{
						if (trap2 == null || binarySearchTree.Contains(trap2.ID))
						{
							continue;
						}
						traps.Add(trap2);
					}
				}
				return traps;
			}
		}

		static Session()
		{
			Session.fProject = null;
			Session.fPreferences = new Masterplan.Preferences();
			Session.fPlayerView = null;
			Session.fModified = false;
			Session.fFileName = "";
			Session.fRandom = new System.Random();
			Session.fAddIns = new List<IAddIn>();
			Session.Libraries = new List<Library>();
			Session.fMainForm = null;
			Session.fCurrentEncounter = null;
			Session.fDisabledLibraries = new List<string>();
		}

		public Session()
		{
		}

		public static bool CheckPassword(Masterplan.Data.Project p)
		{
			if (p.Password == null || p.Password == "")
			{
				return true;
			}
			PasswordCheckForm passwordCheckForm = new PasswordCheckForm(p.Password, p.PasswordHint);
			return passwordCheckForm.ShowDialog() == DialogResult.OK;
		}

		public static void CreateBackup(string filename)
		{
			try
			{
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				string str = string.Concat(Utils.FileName.Directory(entryAssembly.Location), "Backup\\");
				if (!Directory.Exists(str))
				{
					Directory.CreateDirectory(str);
				}
				string str1 = string.Concat(str, Utils.FileName.Name(filename));
				File.Copy(filename, str1, true);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		public static void DeleteLibrary(Library lib)
		{
			(new FileInfo(Session.GetLibraryFilename(lib))).Delete();
			Session.Libraries.Remove(lib);
		}

		public static int Dice(int throws, int sides)
		{
			int num = 0;
			for (int i = 0; i != throws; i++)
			{
				int num1 = 1 + Session.fRandom.Next() % sides;
				num += num1;
			}
			return num;
		}

		public static Artifact FindArtifact(Guid artifact_id, SearchType search_type)
		{
			Artifact artifact;
			Artifact artifact1;
			if (search_type == SearchType.External || search_type == SearchType.Global)
			{
				List<Library>.Enumerator enumerator = Session.Libraries.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						Artifact artifact2 = enumerator.Current.FindArtifact(artifact_id);
						if (artifact2 == null)
						{
							continue;
						}
						artifact1 = artifact2;
						return artifact1;
					}
					if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
					{
						artifact = Session.Project.Library.FindArtifact(artifact_id);
						if (artifact != null)
						{
							return artifact;
						}
					}
					return null;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				return artifact1;
			}
			if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
			{
				artifact = Session.Project.Library.FindArtifact(artifact_id);
				if (artifact != null)
				{
					return artifact;
				}
			}
			return null;
		}

		public static ICreature FindCreature(Guid creature_id, SearchType search_type)
		{
			Creature creature;
			CustomCreature customCreature;
			NPC nPC;
			ICreature creature1;
			if (search_type == SearchType.External || search_type == SearchType.Global)
			{
				List<Library>.Enumerator enumerator = Session.Libraries.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						Creature creature2 = enumerator.Current.FindCreature(creature_id);
						if (creature2 == null)
						{
							continue;
						}
						creature1 = creature2;
						return creature1;
					}
					if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
					{
						creature = Session.Project.Library.FindCreature(creature_id);
						if (creature != null)
						{
							return creature;
						}
						customCreature = Session.Project.FindCustomCreature(creature_id);
						if (customCreature != null)
						{
							return customCreature;
						}
						nPC = Session.Project.FindNPC(creature_id);
						if (nPC != null)
						{
							return nPC;
						}
					}
					return null;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				return creature1;
			}
			if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
			{
				creature = Session.Project.Library.FindCreature(creature_id);
				if (creature != null)
				{
					return creature;
				}
				customCreature = Session.Project.FindCustomCreature(creature_id);
				if (customCreature != null)
				{
					return customCreature;
				}
				nPC = Session.Project.FindNPC(creature_id);
				if (nPC != null)
				{
					return nPC;
				}
			}
			return null;
		}

		public static Library FindLibrary(string name)
		{
			Library library;
			string str = Utils.FileName.TrimInvalidCharacters(name);
			List<Library>.Enumerator enumerator = Session.Libraries.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Library current = enumerator.Current;
					if (current.Name != name)
					{
						if (Utils.FileName.TrimInvalidCharacters(current.Name) != str)
						{
							continue;
						}
						library = current;
						return library;
					}
					else
					{
						library = current;
						return library;
					}
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return library;
		}

		public static Library FindLibrary(Creature c)
		{
			Library library;
			if (c == null)
			{
				return null;
			}
			foreach (Library library1 in Session.Libraries)
			{
				foreach (Creature creature in library1.Creatures)
				{
					if (creature == null || !(creature.ID == c.ID))
					{
						continue;
					}
					library = library1;
					return library;
				}
			}
			if (Session.fProject != null)
			{
				foreach (Creature creature1 in Session.fProject.Library.Creatures)
				{
					if (creature1 == null || !(creature1.ID == c.ID))
					{
						continue;
					}
					library = Session.fProject.Library;
					return library;
				}
			}
			return null;
		}

		public static Library FindLibrary(CreatureTemplate ct)
		{
			Library library;
			if (ct == null)
			{
				return null;
			}
			foreach (Library library1 in Session.Libraries)
			{
				foreach (CreatureTemplate template in library1.Templates)
				{
					if (template == null || !(template.ID == ct.ID))
					{
						continue;
					}
					library = library1;
					return library;
				}
			}
			if (Session.fProject != null)
			{
				foreach (CreatureTemplate creatureTemplate in Session.fProject.Library.Templates)
				{
					if (creatureTemplate == null || !(creatureTemplate.ID == ct.ID))
					{
						continue;
					}
					library = Session.fProject.Library;
					return library;
				}
			}
			return null;
		}

		public static Library FindLibrary(MonsterTheme mt)
		{
			Library library;
			if (mt == null)
			{
				return null;
			}
			foreach (Library library1 in Session.Libraries)
			{
				foreach (MonsterTheme theme in library1.Themes)
				{
					if (theme == null || !(theme.ID == mt.ID))
					{
						continue;
					}
					library = library1;
					return library;
				}
			}
			if (Session.fProject != null)
			{
				foreach (MonsterTheme monsterTheme in Session.fProject.Library.Themes)
				{
					if (monsterTheme == null || !(monsterTheme.ID == mt.ID))
					{
						continue;
					}
					library = Session.fProject.Library;
					return library;
				}
			}
			return null;
		}

		public static Library FindLibrary(Trap t)
		{
			Library library;
			if (t == null)
			{
				return null;
			}
			foreach (Library library1 in Session.Libraries)
			{
				foreach (Trap trap in library1.Traps)
				{
					if (trap == null || !(trap.ID == t.ID))
					{
						continue;
					}
					library = library1;
					return library;
				}
			}
			if (Session.fProject != null)
			{
				foreach (Trap trap1 in Session.fProject.Library.Traps)
				{
					if (trap1 == null || !(trap1.ID == t.ID))
					{
						continue;
					}
					library = Session.fProject.Library;
					return library;
				}
			}
			return null;
		}

		public static Library FindLibrary(SkillChallenge sc)
		{
			Library library;
			if (sc == null)
			{
				return null;
			}
			foreach (Library library1 in Session.Libraries)
			{
				foreach (SkillChallenge skillChallenge in library1.SkillChallenges)
				{
					if (skillChallenge == null || !(skillChallenge.ID == sc.ID))
					{
						continue;
					}
					library = library1;
					return library;
				}
			}
			if (Session.fProject != null)
			{
				foreach (SkillChallenge skillChallenge1 in Session.fProject.Library.SkillChallenges)
				{
					if (skillChallenge1 == null || !(skillChallenge1.ID == sc.ID))
					{
						continue;
					}
					library = Session.fProject.Library;
					return library;
				}
			}
			return null;
		}

		public static Library FindLibrary(MagicItem mi)
		{
			Library library;
			if (mi == null)
			{
				return null;
			}
			foreach (Library library1 in Session.Libraries)
			{
				foreach (MagicItem magicItem in library1.MagicItems)
				{
					if (magicItem == null || !(magicItem.ID == mi.ID))
					{
						continue;
					}
					library = library1;
					return library;
				}
			}
			if (Session.fProject != null)
			{
				foreach (MagicItem magicItem1 in Session.fProject.Library.MagicItems)
				{
					if (magicItem1 == null || !(magicItem1.ID == mi.ID))
					{
						continue;
					}
					library = Session.fProject.Library;
					return library;
				}
			}
			return null;
		}

		public static Library FindLibrary(Artifact a)
		{
			Library library;
			if (a == null)
			{
				return null;
			}
			foreach (Library library1 in Session.Libraries)
			{
				foreach (Artifact artifact in library1.Artifacts)
				{
					if (artifact == null || !(artifact.ID == a.ID))
					{
						continue;
					}
					library = library1;
					return library;
				}
			}
			if (Session.fProject != null)
			{
				foreach (Artifact artifact1 in Session.fProject.Library.Artifacts)
				{
					if (artifact1 == null || !(artifact1.ID == a.ID))
					{
						continue;
					}
					library = Session.fProject.Library;
					return library;
				}
			}
			return null;
		}

		public static Library FindLibrary(Tile t)
		{
			Library library;
			if (t == null)
			{
				return null;
			}
			foreach (Library library1 in Session.Libraries)
			{
				foreach (Tile tile in library1.Tiles)
				{
					if (tile == null || !(tile.ID == t.ID))
					{
						continue;
					}
					library = library1;
					return library;
				}
			}
			if (Session.fProject != null)
			{
				foreach (Tile tile1 in Session.fProject.Library.Tiles)
				{
					if (tile1 == null || !(tile1.ID == t.ID))
					{
						continue;
					}
					library = Session.fProject.Library;
					return library;
				}
			}
			return null;
		}

		public static Library FindLibrary(TerrainPower tp)
		{
			Library library;
			if (tp == null)
			{
				return null;
			}
			foreach (Library library1 in Session.Libraries)
			{
				foreach (TerrainPower terrainPower in library1.TerrainPowers)
				{
					if (terrainPower == null || !(terrainPower.ID == tp.ID))
					{
						continue;
					}
					library = library1;
					return library;
				}
			}
			if (Session.fProject != null)
			{
				foreach (TerrainPower terrainPower1 in Session.fProject.Library.TerrainPowers)
				{
					if (terrainPower1 == null || !(terrainPower1.ID == tp.ID))
					{
						continue;
					}
					library = Session.fProject.Library;
					return library;
				}
			}
			return null;
		}

		public static MagicItem FindMagicItem(Guid item_id, SearchType search_type)
		{
			MagicItem magicItem;
			MagicItem magicItem1;
			if (search_type == SearchType.External || search_type == SearchType.Global)
			{
				List<Library>.Enumerator enumerator = Session.Libraries.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						MagicItem magicItem2 = enumerator.Current.FindMagicItem(item_id);
						if (magicItem2 == null)
						{
							continue;
						}
						magicItem1 = magicItem2;
						return magicItem1;
					}
					if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
					{
						magicItem = Session.Project.Library.FindMagicItem(item_id);
						if (magicItem != null)
						{
							return magicItem;
						}
					}
					return null;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				return magicItem1;
			}
			if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
			{
				magicItem = Session.Project.Library.FindMagicItem(item_id);
				if (magicItem != null)
				{
					return magicItem;
				}
			}
			return null;
		}

		public static SkillChallenge FindSkillChallenge(Guid sc_id, SearchType search_type)
		{
			SkillChallenge skillChallenge;
			SkillChallenge skillChallenge1;
			if (search_type == SearchType.External || search_type == SearchType.Global)
			{
				List<Library>.Enumerator enumerator = Session.Libraries.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						SkillChallenge skillChallenge2 = enumerator.Current.FindSkillChallenge(sc_id);
						if (skillChallenge2 == null)
						{
							continue;
						}
						skillChallenge1 = skillChallenge2;
						return skillChallenge1;
					}
					if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
					{
						skillChallenge = Session.Project.Library.FindSkillChallenge(sc_id);
						if (skillChallenge != null)
						{
							return skillChallenge;
						}
					}
					return null;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				return skillChallenge1;
			}
			if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
			{
				skillChallenge = Session.Project.Library.FindSkillChallenge(sc_id);
				if (skillChallenge != null)
				{
					return skillChallenge;
				}
			}
			return null;
		}

		public static CreatureTemplate FindTemplate(Guid template_id, SearchType search_type)
		{
			CreatureTemplate creatureTemplate;
			CreatureTemplate creatureTemplate1;
			if (search_type == SearchType.External || search_type == SearchType.Global)
			{
				List<Library>.Enumerator enumerator = Session.Libraries.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						CreatureTemplate creatureTemplate2 = enumerator.Current.FindTemplate(template_id);
						if (creatureTemplate2 == null)
						{
							continue;
						}
						creatureTemplate1 = creatureTemplate2;
						return creatureTemplate1;
					}
					if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
					{
						creatureTemplate = Session.Project.Library.FindTemplate(template_id);
						if (creatureTemplate != null)
						{
							return creatureTemplate;
						}
					}
					return null;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				return creatureTemplate1;
			}
			if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
			{
				creatureTemplate = Session.Project.Library.FindTemplate(template_id);
				if (creatureTemplate != null)
				{
					return creatureTemplate;
				}
			}
			return null;
		}

		public static TerrainPower FindTerrainPower(Guid power_id, SearchType search_type)
		{
			TerrainPower terrainPower;
			TerrainPower terrainPower1;
			if (search_type == SearchType.External || search_type == SearchType.Global)
			{
				List<Library>.Enumerator enumerator = Session.Libraries.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						TerrainPower terrainPower2 = enumerator.Current.FindTerrainPower(power_id);
						if (terrainPower2 == null)
						{
							continue;
						}
						terrainPower1 = terrainPower2;
						return terrainPower1;
					}
					if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
					{
						terrainPower = Session.Project.Library.FindTerrainPower(power_id);
						if (terrainPower != null)
						{
							return terrainPower;
						}
					}
					return null;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				return terrainPower1;
			}
			if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
			{
				terrainPower = Session.Project.Library.FindTerrainPower(power_id);
				if (terrainPower != null)
				{
					return terrainPower;
				}
			}
			return null;
		}

		public static MonsterTheme FindTheme(Guid theme_id, SearchType search_type)
		{
			MonsterTheme monsterTheme;
			MonsterTheme monsterTheme1;
			if (search_type == SearchType.External || search_type == SearchType.Global)
			{
				List<Library>.Enumerator enumerator = Session.Libraries.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						MonsterTheme monsterTheme2 = enumerator.Current.FindTheme(theme_id);
						if (monsterTheme2 == null)
						{
							continue;
						}
						monsterTheme1 = monsterTheme2;
						return monsterTheme1;
					}
					if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
					{
						monsterTheme = Session.Project.Library.FindTheme(theme_id);
						if (monsterTheme != null)
						{
							return monsterTheme;
						}
					}
					return null;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				return monsterTheme1;
			}
			if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
			{
				monsterTheme = Session.Project.Library.FindTheme(theme_id);
				if (monsterTheme != null)
				{
					return monsterTheme;
				}
			}
			return null;
		}

		public static Tile FindTile(Guid tile_id, SearchType search_type)
		{
			Tile tile;
			Tile tile1;
			if (search_type == SearchType.External || search_type == SearchType.Global)
			{
				List<Library>.Enumerator enumerator = Session.Libraries.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						Tile tile2 = enumerator.Current.FindTile(tile_id);
						if (tile2 == null)
						{
							continue;
						}
						tile1 = tile2;
						return tile1;
					}
					if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
					{
						tile = Session.Project.Library.FindTile(tile_id);
						if (tile != null)
						{
							return tile;
						}
					}
					return null;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				return tile1;
			}
			if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
			{
				tile = Session.Project.Library.FindTile(tile_id);
				if (tile != null)
				{
					return tile;
				}
			}
			return null;
		}

		public static Trap FindTrap(Guid trap_id, SearchType search_type)
		{
			Trap trap;
			Trap trap1;
			if (search_type == SearchType.External || search_type == SearchType.Global)
			{
				List<Library>.Enumerator enumerator = Session.Libraries.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						Trap trap2 = enumerator.Current.FindTrap(trap_id);
						if (trap2 == null)
						{
							continue;
						}
						trap1 = trap2;
						return trap1;
					}
					if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
					{
						trap = Session.Project.Library.FindTrap(trap_id);
						if (trap != null)
						{
							return trap;
						}
					}
					return null;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				return trap1;
			}
			if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
			{
				trap = Session.Project.Library.FindTrap(trap_id);
				if (trap != null)
				{
					return trap;
				}
			}
			return null;
		}

		public static string GetLibraryFilename(Library lib)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(Session.LibraryFolder);
			string str = Utils.FileName.TrimInvalidCharacters(lib.Name);
			return string.Concat(directoryInfo, str, ".library");
		}

		public static Masterplan.Data.Project LoadBackup(string filename)
		{
			Masterplan.Data.Project project = null;
			try
			{
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				string str = string.Concat(Utils.FileName.Directory(entryAssembly.Location), "Backup\\");
				if (!Directory.Exists(str))
				{
					Directory.CreateDirectory(str);
				}
				string str1 = string.Concat(str, Utils.FileName.Name(filename));
				if (File.Exists(str1))
				{
					project = Serialisation<Masterplan.Data.Project>.Load(str1, SerialisationMode.Binary);
					if (project != null)
					{
						MessageBox.Show("There was a problem opening this project; it has been recovered from its most recent backup version.", "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
			return project;
		}

		public static Library LoadLibrary(string filename)
		{
			Library library;
			try
			{
				if (Program.SplashScreen != null)
				{
					Program.SplashScreen.CurrentSubAction = Utils.FileName.Name(filename);
					ProgressScreen splashScreen = Program.SplashScreen;
					splashScreen.Progress = splashScreen.Progress + 1;
				}
				string str = Program.SimplifySecurityData(Program.SecurityData);
				Library library1 = Serialisation<Library>.Load(filename, SerialisationMode.Binary);
				if (library1 == null)
				{
					LogSystem.Trace(string.Concat("Could not load ", Utils.FileName.Name(filename)));
				}
				else
				{
					library1.Name = Utils.FileName.Name(filename);
					library1.Update();
					if (Program.CopyProtection)
					{
						if (library1.SecurityData == null || library1.SecurityData == "")
						{
							library1.SecurityData = str;
							if (!Serialisation<Library>.Save(filename, library1, SerialisationMode.Binary))
							{
								LogSystem.Trace(string.Concat("Could not save ", library1.Name));
							}
						}
						string str1 = Program.SimplifySecurityData(library1.SecurityData);
						if (str1 != str)
						{
							string[] name = new string[] { "Could not load ", library1.Name, ": ", str1, " vs ", str };
							LogSystem.Trace(string.Concat(name));
							Session.DisabledLibraries.Add(library1.Name);
							library = null;
							return library;
						}
					}
					else if (library1.SecurityData != "")
					{
						library1.SecurityData = "";
						Serialisation<Library>.Save(filename, library1, SerialisationMode.Binary);
					}
					Session.Libraries.Add(library1);
				}
				library = library1;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
				return null;
			}
			return library;
		}
	}
}