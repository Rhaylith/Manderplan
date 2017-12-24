using Masterplan.Data;
using Masterplan.Tools;
using Masterplan.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Utils;
using Utils.Forms;

namespace Masterplan
{
	internal static class Program
	{
		internal static bool fIsBeta;

		public static ProgressScreen SplashScreen;

		public static string ProjectFilter;

		public static string LibraryFilter;

		public static string EncounterFilter;

		public static string BackgroundFilter;

		public static string EncyclopediaFilter;

		public static string RulesFilter;

		public static string CreatureAndMonsterFilter;

		public static string MonsterFilter;

		public static string CreatureFilter;

		public static string CreatureTemplateFilter;

		public static string ThemeFilter;

		public static string CreatureTemplateAndThemeFilter;

		public static string TrapFilter;

		public static string SkillChallengeFilter;

		public static string MagicItemFilter;

		public static string ArtifactFilter;

		public static string MapTileFilter;

		public static string TerrainPowerFilter;

		public static string HTMLFilter;

		public static string ImageFilter;

		internal static bool CopyProtection
		{
			get
			{
				return !Program.IsBeta;
			}
		}

		internal static bool IsBeta
		{
			get
			{
				return Program.fIsBeta;
			}
		}

		internal static string SecurityData
		{
			get
			{
				string lower = SystemInformation.UserName.ToLower();
				string str = SystemInformation.ComputerName.ToLower();
				return string.Concat(lower, " on ", str);
			}
		}

		static Program()
		{
			Program.fIsBeta = false;
			Program.SplashScreen = null;
			Program.ProjectFilter = "Masterplan Project|*.masterplan";
			Program.LibraryFilter = "Masterplan Library|*.library";
			Program.EncounterFilter = "Masterplan Encounter|*.encounter";
			Program.BackgroundFilter = "Masterplan Campaign Background|*.background";
			Program.EncyclopediaFilter = "Masterplan Campaign Encyclopedia|*.encyclopedia";
			Program.RulesFilter = "Masterplan Rules|*.crunch";
			Program.CreatureAndMonsterFilter = "Creatures|*.creature;*.monster";
			Program.MonsterFilter = "Adventure Tools Creatures|*.monster";
			Program.CreatureFilter = "Creatures|*.creature";
			Program.CreatureTemplateFilter = "Creature Template|*.creaturetemplate";
			Program.ThemeFilter = "Themes|*.theme";
			Program.CreatureTemplateAndThemeFilter = "Creature Templates and Themes|*.creaturetemplate;*.theme";
			Program.TrapFilter = "Traps|*.trap";
			Program.SkillChallengeFilter = "Skill Challenges|*.skillchallenge";
			Program.MagicItemFilter = "Magic Items|*.magicitem";
			Program.ArtifactFilter = "Artifacts|*.artifact";
			Program.MapTileFilter = "Map Tiles|*.maptile";
			Program.TerrainPowerFilter = "Terrain Powers|*.terrainpower";
			Program.HTMLFilter = "HTML File|*.htm";
			Program.ImageFilter = "Image File|*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tga";
		}

		private static void check_for_logs()
		{
			string logFile = LogSystem.LogFile;
			if (logFile == null || logFile == "")
			{
				return;
			}
			if (!File.Exists(logFile))
			{
				return;
			}
			Process.Start(FileName.Directory(logFile));
		}

		private static List<Creature> get_creatures(List<Creature> creatures, int level, bool is_minion, bool is_leader, RoleType role, RoleFlag flag)
		{
			List<Creature> creatures1 = new List<Creature>();
			foreach (Creature creature in creatures)
			{
				if (creature.Level != level)
				{
					continue;
				}
				ComplexRole complexRole = creature.Role as ComplexRole;
				Minion minion = creature.Role as Minion;
				if (minion != null && !minion.HasRole || minion != null != is_minion)
				{
					continue;
				}
				if ((complexRole == null ? false : complexRole.Leader) != is_leader)
				{
					continue;
				}
				RoleType type = RoleType.Blaster;
				RoleFlag roleFlag = RoleFlag.Standard;
				if (complexRole != null)
				{
					type = complexRole.Type;
					roleFlag = complexRole.Flag;
				}
				if (minion != null)
				{
					type = minion.Type;
					roleFlag = RoleFlag.Standard;
				}
				if (type != role || roleFlag != flag)
				{
					continue;
				}
				creatures1.Add(creature);
			}
			return creatures1;
		}

		private static void handle_arg(string arg)
		{
			try
			{
				if (arg == "-headlines")
				{
					Session.Preferences.ShowHeadlines = true;
				}
				if (arg == "-noheadlines")
				{
					Session.Preferences.ShowHeadlines = false;
				}
				if (arg == "-creaturestats")
				{
					Program.run_creature_stats();
				}
				FileInfo fileInfo = new FileInfo(arg);
				if (fileInfo.Exists)
				{
					Program.SplashScreen.CurrentAction = "Loading project...";
					Program.SplashScreen.CurrentSubAction = FileName.Name(fileInfo.Name);
					Project project = Serialisation<Project>.Load(arg, SerialisationMode.Binary);
					if (project == null)
					{
						project = Session.LoadBackup(arg);
					}
					else
					{
						Session.CreateBackup(arg);
					}
					if (project != null && Session.CheckPassword(project))
					{
						Session.Project = project;
						Session.FileName = arg;
						project.Update();
						project.SimplifyProjectLibrary();
					}
				}
			}
			catch
			{
			}
		}

		private static void init_logging()
		{
			try
			{
				string str = FileName.Directory(Application.ExecutablePath);
				string str1 = string.Concat(str, "Log", Path.DirectorySeparatorChar);
				if (!Directory.Exists(str1) && Directory.CreateDirectory(str1) == null)
				{
					throw new UnauthorizedAccessException();
				}
				DateTime now = DateTime.Now;
				string str2 = string.Concat(str1, now.Ticks, ".log");
				LogSystem.LogFile = str2;
			}
			catch
			{
			}
		}

		private static void load_libraries()
		{
			try
			{
				Program.SplashScreen.CurrentAction = "Loading libraries...";
				string str = FileName.Directory(Assembly.GetEntryAssembly().Location);
				string str1 = string.Concat(str, "Libraries\\");
				if (!Directory.Exists(str1))
				{
					Directory.CreateDirectory(str1);
				}
				string[] files = Directory.GetFiles(str, "*.library");
				for (int i = 0; i < (int)files.Length; i++)
				{
					string str2 = files[i];
					try
					{
						string str3 = string.Concat(str1, FileName.Name(str2), ".library");
						if (!File.Exists(str3))
						{
							File.Move(str2, str3);
						}
					}
					catch (Exception exception)
					{
						LogSystem.Trace(exception);
					}
				}
				string[] strArrays = Directory.GetFiles(str1, "*.library");
				Program.SplashScreen.Actions = (int)strArrays.Length;
				string[] strArrays1 = strArrays;
				for (int j = 0; j < (int)strArrays1.Length; j++)
				{
					Session.LoadLibrary(strArrays1[j]);
				}
				Session.Libraries.Sort();
			}
			catch (Exception exception1)
			{
				LogSystem.Trace(exception1);
			}
		}

		private static void load_preferences()
		{
			try
			{
				string str = FileName.Directory(Assembly.GetEntryAssembly().Location);
				string str1 = string.Concat(str, "Preferences.xml");
				if (File.Exists(str1))
				{
					Program.SplashScreen.CurrentAction = "Loading user preferences";
					Preferences preference = Serialisation<Preferences>.Load(str1, SerialisationMode.XML);
					if (preference != null)
					{
						Session.Preferences = preference;
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			try
			{
				Program.init_logging();
				Program.SplashScreen = new ProgressScreen("Masterplan", 0)
				{
					CurrentAction = "Loading..."
				};
				Program.SplashScreen.Show();
				Program.load_preferences();
				Program.load_libraries();
				string[] strArrays = args;
				for (int i = 0; i < (int)strArrays.Length; i++)
				{
					Program.handle_arg(strArrays[i]);
				}
				Program.SplashScreen.CurrentAction = "Starting Masterplan...";
				Program.SplashScreen.Actions = 0;
				try
				{
					Application.Run(new MainForm());
				}
				catch (Exception exception)
				{
					LogSystem.Trace(exception);
				}
				List<Form> forms = new List<Form>();
				foreach (Form openForm in Application.OpenForms)
				{
					forms.Add(openForm);
				}
				foreach (Form form in forms)
				{
					form.Close();
				}
				Program.save_preferences();
				if (Program.IsBeta)
				{
					Program.check_for_logs();
				}
			}
			catch (Exception exception1)
			{
				LogSystem.Trace(exception1);
			}
		}

		private static void run_creature_stats()
		{
			List<Creature> creatures = Session.Creatures;
			bool[] flagArray = new bool[] { false, true };
			bool[] flagArray1 = new bool[] { false, true };
			string str = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "\\Creatures.csv");
			StreamWriter streamWriter = new StreamWriter(str);
			try
			{
				try
				{
					streamWriter.Write("Level,Flag,Role,Minion,Leader,Tier,TierX,Creatures,Powers");
					foreach (string condition in Conditions.GetConditions())
					{
						streamWriter.Write(string.Concat(",", condition));
					}
					foreach (DamageType value in Enum.GetValues(typeof(DamageType)))
					{
						streamWriter.Write(string.Concat(",", value));
					}
					streamWriter.WriteLine();
					for (int i = 1; i <= 40; i++)
					{
						bool[] flagArray2 = flagArray;
						for (int j = 0; j < (int)flagArray2.Length; j++)
						{
							bool flag = flagArray2[j];
							bool[] flagArray3 = flagArray1;
							for (int k = 0; k < (int)flagArray3.Length; k++)
							{
								bool flag1 = flagArray3[k];
								foreach (RoleType roleType in Enum.GetValues(typeof(RoleType)))
								{
									foreach (RoleFlag roleFlag in Enum.GetValues(typeof(RoleFlag)))
									{
										List<Creature> _creatures = Program.get_creatures(creatures, i, flag, flag1, roleType, roleFlag);
										List<CreaturePower> creaturePowers = new List<CreaturePower>();
										foreach (Creature _creature in _creatures)
										{
											creaturePowers.AddRange(_creature.CreaturePowers);
										}
										if (creaturePowers.Count == 0)
										{
											continue;
										}
										string str1 = "";
										if (i >= 11)
										{
											str1 = (i >= 21 ? "epic" : "paragon");
										}
										else
										{
											str1 = "heroic";
										}
										string str2 = "";
										if (i < 4)
										{
											str2 = "early heroic";
										}
										else if (i < 8)
										{
											str2 = "mid heroic";
										}
										else if (i < 11)
										{
											str2 = "late heroic";
										}
										else if (i < 14)
										{
											str2 = "early paragon";
										}
										else if (i < 18)
										{
											str2 = "mid paragon";
										}
										else if (i < 21)
										{
											str2 = "late paragon";
										}
										else if (i < 24)
										{
											str2 = "early epic";
										}
										else if (i >= 28)
										{
											str2 = (i >= 31 ? "epic plus" : "late epic");
										}
										else
										{
											str2 = "mid epic";
										}
										object[] count = new object[] { i, ",", roleFlag, ",", roleType, ",", flag, ",", flag1, ",", str1, ",", str2, ",", _creatures.Count, ",", creaturePowers.Count };
										streamWriter.Write(string.Concat(count));
										foreach (string condition1 in Conditions.GetConditions())
										{
											int num = 0;
											string lower = condition1.ToLower();
											foreach (CreaturePower creaturePower in creaturePowers)
											{
												if (!creaturePower.Details.ToLower().Contains(lower))
												{
													continue;
												}
												num++;
											}
											double count1 = 0;
											if (creaturePowers.Count != 0)
											{
												count1 = (double)num / (double)creaturePowers.Count;
											}
											streamWriter.Write(string.Concat(",", count1));
										}
										foreach (DamageType damageType in Enum.GetValues(typeof(DamageType)))
										{
											int num1 = 0;
											string lower1 = damageType.ToString().ToLower();
											foreach (CreaturePower creaturePower1 in creaturePowers)
											{
												if (!creaturePower1.Details.ToLower().Contains(lower1))
												{
													continue;
												}
												num1++;
											}
											double count2 = 0;
											if (creaturePowers.Count != 0)
											{
												count2 = (double)num1 / (double)creaturePowers.Count;
											}
											streamWriter.Write(string.Concat(",", count2));
										}
										streamWriter.WriteLine();
									}
								}
							}
						}
					}
				}
				catch (Exception exception)
				{
					LogSystem.Trace(exception);
				}
			}
			finally
			{
				streamWriter.Close();
			}
		}

		private static void save_preferences()
		{
			try
			{
				string str = FileName.Directory(Assembly.GetEntryAssembly().Location);
				string str1 = string.Concat(str, "Preferences.xml");
				Serialisation<Preferences>.Save(str1, Session.Preferences, SerialisationMode.XML);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		internal static void SetResolution(Image img)
		{
			Bitmap bitmap = img as Bitmap;
			if (bitmap != null)
			{
				try
				{
					float single = Math.Min(bitmap.HorizontalResolution, 96f);
					float single1 = Math.Min(bitmap.VerticalResolution, 96f);
					bitmap.SetResolution(single, single1);
				}
				catch
				{
				}
			}
		}

		internal static string SimplifySecurityData(string raw_data)
		{
			string[] strArrays = new string[] { " on " };
			string[] strArrays1 = raw_data.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
			if ((int)strArrays1.Length != 2)
			{
				return "";
			}
			string lower = strArrays1[0].ToLower();
			int num = lower.IndexOf(".");
			if (num != -1)
			{
				lower = lower.Substring(0, num);
			}
			string str = strArrays1[1].ToLower();
			int num1 = str.IndexOf(".");
			if (num1 != -1)
			{
				str = str.Substring(0, num1);
			}
			return string.Concat(lower, " on ", str);
		}
	}
}