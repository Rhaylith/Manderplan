using Masterplan;
using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Tools.Generators
{
	internal class DelveBuilder
	{
		public DelveBuilder()
		{
		}

		public static PlotPoint AutoBuild(Map map, AutoBuildData data)
		{
			PlotPoint plotPoint = new PlotPoint(string.Concat(map.Name, " Delve"))
			{
				Details = "This delve was automatically generated.",
				Element = new MapElement(map.ID, Guid.Empty)
			};
			int level = data.Level;
			List<Parcel> parcels = Treasure.CreateParcelSet(data.Level, Session.Project.Party.Size, false);
			foreach (MapArea area in map.Areas)
			{
				PlotPoint _encounter = new PlotPoint(area.Name);
				switch (Session.Random.Next() % 8)
				{
					case 0:
					case 1:
					case 2:
					case 3:
					case 4:
					case 5:
					{
						_encounter.Element = DelveBuilder.get_encounter(map, area, data);
						break;
					}
					case 6:
					{
						_encounter.Element = DelveBuilder.get_encounter(map, area, data);
						break;
					}
					case 7:
					{
						_encounter.Element = DelveBuilder.get_encounter(map, area, data);
						break;
					}
				}
				int num = 0;
				switch (Session.Random.Next() % 8)
				{
					case 0:
					case 1:
					{
						num = 0;
						break;
					}
					case 2:
					case 3:
					case 4:
					case 5:
					case 6:
					{
						num = 1;
						break;
					}
					case 7:
					{
						num = 2;
						break;
					}
				}
				for (int i = 0; i != num; i++)
				{
					if (parcels.Count == 0)
					{
						level = Math.Min(30, level + 1);
						parcels = Treasure.CreateParcelSet(level, Session.Project.Party.Size, false);
					}
					int num1 = Session.Random.Next() % parcels.Count;
					Parcel item = parcels[num1];
					parcels.RemoveAt(num1);
					_encounter.Parcels.Add(item);
				}
				plotPoint.Subplot.Points.Add(_encounter);
			}
			return plotPoint;
		}

		private static SkillChallenge get_challenge(Map map, MapArea ma, AutoBuildData data)
		{
			SkillChallenge d = DelveBuilder.select_challenge(data);
			if (d == null)
			{
				return null;
			}
			d.MapID = map.ID;
			d.MapAreaID = ma.ID;
			return d;
		}

		private static Encounter get_encounter(Map map, MapArea ma, AutoBuildData data)
		{
			object[] objArray;
			Encounter encounter = new Encounter()
			{
				MapID = map.ID,
				MapAreaID = ma.ID
			};
			EncounterBuilder.Build(data, encounter, false);
			if (encounter.GetDifficulty(Session.Project.Party.Level, Session.Project.Party.Size) != Difficulty.Extreme)
			{
				switch (Session.Random.Next() % 6)
				{
					case 0:
					case 1:
					case 3:
					{
						Trap trap = DelveBuilder.select_trap(data);
						if (trap == null)
						{
							break;
						}
						encounter.Traps.Add(trap);
						break;
					}
					case 4:
					{
						SkillChallenge skillChallenge = DelveBuilder.select_challenge(data);
						if (skillChallenge == null)
						{
							break;
						}
						encounter.SkillChallenges.Add(skillChallenge);
						break;
					}
				}
			}
			List<Rectangle> rectangles = new List<Rectangle>();
			foreach (TileData tile in map.Tiles)
			{
				Tile tile1 = Session.FindTile(tile.TileID, SearchType.Global);
				int num = (tile.Rotations % 2 == 0 ? tile1.Size.Width : tile1.Size.Height);
				Size size = new Size(num, (tile.Rotations % 2 == 0 ? tile1.Size.Height : tile1.Size.Width));
				rectangles.Add(new Rectangle(tile.Location, size));
			}
			Dictionary<Point, bool> points = new Dictionary<Point, bool>();
			for (int i = ma.Region.Left; i != ma.Region.Right; i++)
			{
				for (int j = ma.Region.Top; j != ma.Region.Bottom; j++)
				{
					Point point = new Point(i, j);
					bool flag = false;
					foreach (Rectangle rectangle in rectangles)
					{
						if (!rectangle.Contains(point))
						{
							continue;
						}
						flag = true;
						break;
					}
					points[point] = flag;
				}
			}
			foreach (EncounterSlot slot in encounter.Slots)
			{
				ICreature creature = Session.FindCreature(slot.Card.CreatureID, SearchType.Global);
				int size1 = Creature.GetSize(creature.Size);
				foreach (CombatData combatDatum in slot.CombatData)
				{
					List<Point> points1 = new List<Point>();
					for (int k = ma.Region.Left; k != ma.Region.Right; k++)
					{
						for (int l = ma.Region.Top; l != ma.Region.Bottom; l++)
						{
							Point point1 = new Point(k, l);
							bool flag1 = true;
							for (int m = point1.X; m != point1.X + size1; m++)
							{
								for (int n = point1.Y; n != point1.Y + size1; n++)
								{
									Point point2 = new Point(m, n);
									if (!points.ContainsKey(point2) || !points[point2])
									{
										flag1 = false;
									}
								}
							}
							if (flag1)
							{
								points1.Add(point1);
							}
						}
					}
					if (points1.Count == 0)
					{
						continue;
					}
					int num1 = Session.Random.Next() % points1.Count;
					Point item = points1[num1];
					combatDatum.Location = item;
					for (int o = item.X; o != item.X + size1; o++)
					{
						for (int p = item.Y; p != item.Y + size1; p++)
						{
							points[new Point(o, p)] = false;
						}
					}
				}
			}
			encounter.SetStandardEncounterNotes();
			EncounterNote encounterNote = encounter.FindNote("Illumination");
			if (encounterNote != null)
			{
				switch (Session.Random.Next(6))
				{
					case 0:
					case 1:
					case 2:
					{
						encounterNote.Contents = "The area is in bright light.";
						break;
					}
					case 3:
					case 4:
					{
						encounterNote.Contents = "The area is in dim light.";
						break;
					}
					case 5:
					{
						encounterNote.Contents = "None.";
						break;
					}
				}
			}
			EncounterNote item1 = encounter.FindNote("Victory Conditions");
			if (item1 != null)
			{
				List<string> strs = new List<string>();
				List<string> strs1 = new List<string>();
				bool flag2 = false;
				int count = 0;
				foreach (EncounterSlot encounterSlot in encounter.Slots)
				{
					if (encounterSlot.CombatData.Count == 1 && (encounterSlot.Card.Leader || encounterSlot.Card.Flag == RoleFlag.Elite || encounterSlot.Card.Flag == RoleFlag.Solo))
					{
						strs1.Add(encounterSlot.CombatData[0].DisplayName);
					}
					ICreature creature1 = Session.FindCreature(encounterSlot.Card.CreatureID, SearchType.Global);
					if (creature1 == null)
					{
						continue;
					}
					if (!(creature1.Role is Minion))
					{
						count += encounterSlot.CombatData.Count;
					}
					else
					{
						flag2 = true;
					}
				}
				if (strs1.Count != 0)
				{
					int num2 = Session.Random.Next() % strs1.Count;
					string str = strs1[num2];
					if (Session.Random.Next() % 12 == 0)
					{
						strs.Add(string.Concat("Defeat ", str, "."));
						strs.Add(string.Concat("Capture ", str, "."));
					}
					if (Session.Random.Next() % 12 == 0)
					{
						int num3 = Session.Dice(2, 4);
						objArray = new object[] { "The party must defeat ", str, " within ", num3, " rounds." };
						strs.Add(string.Concat(objArray));
					}
					if (Session.Random.Next() % 12 == 0)
					{
						int num4 = Session.Dice(2, 4);
						objArray = new object[] { "After ", num4, ", ", str, " will flee or surrender." };
						strs.Add(string.Concat(objArray));
					}
					if (Session.Random.Next() % 12 == 0)
					{
						int num5 = 10 * Session.Dice(1, 4);
						objArray = new object[] { "At ", num5, "% HP, ", str, " will flee or surrender." };
						strs.Add(string.Concat(objArray));
					}
					if (Session.Random.Next() % 12 == 0)
					{
						strs.Add(string.Concat("The party must obtain an item from ", str, "."));
					}
					if (Session.Random.Next() % 12 == 0)
					{
						strs.Add(string.Concat("Defeat ", str, " by destroying a guarded object in the area."));
					}
					if (flag2)
					{
						strs.Add(string.Concat("Minions will flee or surrender when ", str, " is defeated."));
					}
				}
				if (Session.Random.Next() % 12 == 0)
				{
					int num6 = 2 + Session.Random.Next() % 4;
					strs.Add(string.Concat("The party must defeat their opponents within ", num6, " rounds."));
				}
				if (flag2 && Session.Random.Next() % 12 == 0)
				{
					int num7 = 2 + Session.Random.Next() % 4;
					strs.Add(string.Concat("The party must defend a certain area from ", num7, " waves of minions."));
				}
				if (Session.Random.Next() % 12 == 0)
				{
					int num8 = 2 + Session.Random.Next() % 4;
					strs.Add(string.Concat("At least one character must get to a certain area and stay there for ", num8, " consecutive rounds."));
				}
				if (Session.Random.Next() % 12 == 0)
				{
					int num9 = 2 + Session.Random.Next() % 4;
					strs.Add(string.Concat("The party must leave the area within ", num9, " rounds."));
				}
				if (Session.Random.Next() % 12 == 0)
				{
					strs.Add("The party must keep the enemy away from a certain area for the duration of the encounter.");
				}
				if (Session.Random.Next() % 12 == 0)
				{
					strs.Add("The party must escort an NPC safely through the encounter area.");
				}
				if (Session.Random.Next() % 12 == 0)
				{
					strs.Add("The party must rescue an NPC from their opponents.");
				}
				if (Session.Random.Next() % 12 == 0)
				{
					strs.Add("The party must avoid contact with the enemy in this area.");
				}
				if (Session.Random.Next() % 12 == 0)
				{
					strs.Add("The party must attack and destroy a feature of the area.");
				}
				if (count > 1 && Session.Random.Next() % 12 == 0)
				{
					int num10 = 1 + Session.Random.Next(count);
					strs.Add(string.Concat("The party must defeat ", num10, " non-minion opponents."));
				}
				if (strs.Count != 0)
				{
					int num11 = Session.Random.Next() % strs.Count;
					item1.Contents = strs[num11];
				}
			}
			return encounter;
		}

		private static TrapElement get_trap(Map map, MapArea ma, AutoBuildData data)
		{
			Trap trap = DelveBuilder.select_trap(data);
			if (trap == null)
			{
				return null;
			}
			TrapElement trapElement = new TrapElement()
			{
				Trap = trap,
				MapID = map.ID,
				MapAreaID = ma.ID
			};
			return trapElement;
		}

		private static SkillChallenge select_challenge(AutoBuildData data)
		{
			List<SkillChallenge> skillChallenges = new List<SkillChallenge>();
			int level = data.Level - 3;
			int num = data.Level + 5;
			skillChallenges.Clear();
			foreach (SkillChallenge skillChallenge in Session.SkillChallenges)
			{
				if (skillChallenge.Level != -1)
				{
					if (skillChallenge.Level < level || skillChallenge.Level > num)
					{
						continue;
					}
					skillChallenges.Add(skillChallenge.Copy() as SkillChallenge);
				}
				else
				{
					SkillChallenge level1 = skillChallenge.Copy() as SkillChallenge;
					level1.Level = Session.Project.Party.Level;
					skillChallenges.Add(level1);
				}
			}
			if (skillChallenges.Count == 0)
			{
				return null;
			}
			int num1 = Session.Random.Next() % skillChallenges.Count;
			return skillChallenges[num1];
		}

		private static Trap select_trap(AutoBuildData data)
		{
			List<Trap> traps = new List<Trap>();
			int level = data.Level - 3;
			int num = data.Level + 5;
			traps.Clear();
			foreach (Trap trap in Session.Traps)
			{
				if (trap.Level < level || trap.Level > num)
				{
					continue;
				}
				traps.Add(trap.Copy());
			}
			if (traps.Count == 0)
			{
				return null;
			}
			int num1 = Session.Random.Next() % traps.Count;
			return traps[num1];
		}
	}
}