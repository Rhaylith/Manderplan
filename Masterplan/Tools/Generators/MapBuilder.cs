using Masterplan;
using Masterplan.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Utils;

namespace Masterplan.Tools.Generators
{
	internal class MapBuilder
	{
		private static MapBuilderData fData;

		private static Map fMap;

		private static Dictionary<TileCategory, List<Tile>> fTiles;

		private static List<Tile> fRoomTiles;

		private static List<Tile> fCorridorTiles;

		private static List<Endpoint> fEndpoints;

		static MapBuilder()
		{
			MapBuilder.fData = null;
			MapBuilder.fMap = null;
			MapBuilder.fTiles = new Dictionary<TileCategory, List<Tile>>();
			MapBuilder.fRoomTiles = new List<Tile>();
			MapBuilder.fCorridorTiles = new List<Tile>();
			MapBuilder.fEndpoints = new List<Endpoint>();
		}

		public MapBuilder()
		{
		}

		private static bool add_area(Endpoint ep)
		{
			if (MapBuilder.fRoomTiles.Count == 0)
			{
				return false;
			}
			List<Tile> tiles = new List<Tile>();
			int num = 1 + Session.Random.Next() % 5;
			while (tiles.Count != num)
			{
				int num1 = Session.Random.Next() % MapBuilder.fRoomTiles.Count;
				tiles.Add(MapBuilder.fRoomTiles[num1]);
			}
			List<Endpoint> endpoints = new List<Endpoint>()
			{
				ep
			};
			List<Pair<Tile, TileData>> pairs = new List<Pair<Tile, TileData>>();
			foreach (Tile tile in tiles)
			{
				if (endpoints.Count == 0)
				{
					break;
				}
				int num2 = Session.Random.Next() % endpoints.Count;
				Endpoint item = endpoints[num2];
				Pair<TileData, Direction> pair = MapBuilder.add_tile(tile, item, false, false);
				if (pair == null)
				{
					continue;
				}
				endpoints.Remove(item);
				pairs.Add(new Pair<Tile, TileData>(tile, pair.First));
				if (pair.Second != Direction.South)
				{
					endpoints.Add(MapBuilder.get_endpoint(tile, pair.First, Direction.North));
				}
				if (pair.Second != Direction.West)
				{
					endpoints.Add(MapBuilder.get_endpoint(tile, pair.First, Direction.East));
				}
				if (pair.Second != Direction.North)
				{
					endpoints.Add(MapBuilder.get_endpoint(tile, pair.First, Direction.South));
				}
				if (pair.Second == Direction.East)
				{
					continue;
				}
				endpoints.Add(MapBuilder.get_endpoint(tile, pair.First, Direction.West));
			}
			if (pairs.Count != 0)
			{
				MapBuilder.add_map_area(pairs);
				List<Tile> item1 = MapBuilder.fTiles[TileCategory.Feature];
				if (item1.Count != 0)
				{
					int area = 0;
					foreach (Pair<Tile, TileData> pair1 in pairs)
					{
						area += pair1.First.Area;
					}
					int num3 = Session.Random.Next() % (area / 10);
					int num4 = 0;
					int num5 = 0;
					List<Pair<Tile, TileData>> pairs1 = new List<Pair<Tile, TileData>>();
					while (num4 != num3 && num5 != 1000)
					{
						int num6 = Session.Random.Next() % item1.Count;
						Tile tile1 = item1[num6];
						TileData tileDatum = new TileData()
						{
							TileID = tile1.ID,
							Rotations = Session.Random.Next() % 4
						};
						int num7 = (tileDatum.Rotations % 2 == 0 ? tile1.Size.Width : tile1.Size.Height);
						int num8 = (tileDatum.Rotations % 2 == 0 ? tile1.Size.Height : tile1.Size.Width);
						List<Pair<Tile, TileData>> pairs2 = new List<Pair<Tile, TileData>>();
						foreach (Pair<Tile, TileData> pair2 in pairs)
						{
							int num9 = (pair2.Second.Rotations % 2 == 0 ? pair2.First.Size.Width : pair2.First.Size.Height);
							int num10 = (pair2.Second.Rotations % 2 == 0 ? pair2.First.Size.Height : pair2.First.Size.Width);
							int num11 = num9 - num7;
							if (num11 < 0 || num10 - num8 < 0)
							{
								continue;
							}
							pairs2.Add(pair2);
						}
						bool flag = false;
						if (pairs2.Count != 0)
						{
							int num12 = Session.Random.Next() % pairs2.Count;
							Pair<Tile, TileData> item2 = pairs2[num12];
							int num13 = (item2.Second.Rotations % 2 == 0 ? item2.First.Size.Width : item2.First.Size.Height);
							int num14 = (item2.Second.Rotations % 2 == 0 ? item2.First.Size.Height : item2.First.Size.Width);
							int num15 = num13 - num7;
							int num16 = num14 - num8;
							if (num15 >= 0 && num16 >= 0)
							{
								Point location = item2.Second.Location;
								int x = location.X;
								if (num15 != 0)
								{
									x = x + Session.Random.Next() % num15;
								}
								location = item2.Second.Location;
								int y = location.Y;
								if (num16 != 0)
								{
									y = y + Session.Random.Next() % num16;
								}
								tileDatum.Location = new Point(x, y);
								bool flag1 = true;
								Rectangle _rect = MapBuilder.get_rect(tile1, tileDatum);
								foreach (Pair<Tile, TileData> pair3 in pairs1)
								{
									if (!MapBuilder.get_rect(pair3.First, pair3.Second).IntersectsWith(_rect))
									{
										continue;
									}
									flag1 = false;
									break;
								}
								if (flag1)
								{
									MapBuilder.fMap.Tiles.Add(tileDatum);
									pairs1.Add(new Pair<Tile, TileData>(tile1, tileDatum));
									flag = true;
									break;
								}
							}
						}
						if (!flag)
						{
							num5++;
						}
						else
						{
							num4++;
							num5 = 0;
						}
					}
				}
				int num17 = 1 + Session.Random.Next() % 3;
				int num18 = 0;
				int num19 = 0;
				while (num18 != num17 && endpoints.Count != 0 && num19 != 1000)
				{
					int num20 = Session.Random.Next() % endpoints.Count;
					Endpoint endpoint = endpoints[num20];
					bool flag2 = true;
					switch (Session.Random.Next() % 2)
					{
						case 0:
						{
							flag2 = MapBuilder.add_doorway(endpoint);
							break;
						}
						case 1:
						{
							flag2 = MapBuilder.add_corridor(endpoint, true);
							break;
						}
					}
					if (!flag2)
					{
						num19++;
					}
					else
					{
						num18++;
						endpoints.Remove(endpoint);
						num19 = 0;
					}
				}
			}
			return pairs.Count != 0;
		}

		private static bool add_corridor(Endpoint ep, bool follow)
		{
			if (MapBuilder.fCorridorTiles.Count == 0)
			{
				return false;
			}
			int num = Session.Random.Next() % MapBuilder.fCorridorTiles.Count;
			Tile item = MapBuilder.fCorridorTiles[num];
			if (ep != null)
			{
				Pair<TileData, Direction> pair = MapBuilder.add_tile(item, ep, follow, true);
				if (pair == null)
				{
					return false;
				}
				MapBuilder.fEndpoints.Add(MapBuilder.get_endpoint(item, pair.First, pair.Second));
			}
			else
			{
				TileData tileDatum = MapBuilder.add_first_tile(item);
				Direction startingDirection = MapBuilder.get_starting_direction(MapBuilder.get_orientation(item, tileDatum));
				MapBuilder.fEndpoints.Add(MapBuilder.get_endpoint(item, tileDatum, startingDirection));
			}
			return true;
		}

		private static bool add_doorway(Endpoint ep)
		{
			List<Tile> item = MapBuilder.fTiles[TileCategory.Doorway];
			if (item.Count == 0)
			{
				return false;
			}
			int num = Session.Random.Next() % item.Count;
			Tile tile = item[num];
			if (ep != null)
			{
				Pair<TileData, Direction> pair = MapBuilder.add_tile(tile, ep, true, true);
				if (pair == null)
				{
					return false;
				}
				MapBuilder.fEndpoints.Add(MapBuilder.get_endpoint(tile, pair.First, pair.Second));
			}
			return true;
		}

		private static TileData add_first_tile(Tile t)
		{
			TileData tileDatum = new TileData()
			{
				TileID = t.ID,
				Location = new Point(0, 0),
				Rotations = Session.Random.Next() % 4
			};
			MapBuilder.fMap.Tiles.Add(tileDatum);
			return tileDatum;
		}

		private static void add_map_area(List<Pair<Tile, TileData>> tiles)
		{
			int left = 2147483647;
			int top = 2147483647;
			int right = -2147483648;
			int bottom = -2147483648;
			foreach (Pair<Tile, TileData> tile in tiles)
			{
				Rectangle _rect = MapBuilder.get_rect(tile.First, tile.Second);
				if (_rect.Left < left)
				{
					left = _rect.Left;
				}
				if (_rect.Right > right)
				{
					right = _rect.Right;
				}
				if (_rect.Top < top)
				{
					top = _rect.Top;
				}
				if (_rect.Bottom <= bottom)
				{
					continue;
				}
				bottom = _rect.Bottom;
			}
			left--;
			top--;
			right++;
			bottom++;
			MapArea mapArea = new MapArea()
			{
				Name = string.Concat("Area ", MapBuilder.fMap.Areas.Count + 1),
				Region = new Rectangle(left, top, right - left, bottom - top)
			};
			MapBuilder.fMap.Areas.Add(mapArea);
		}

		private static bool add_stairway(Endpoint ep)
		{
			List<Tile> item = MapBuilder.fTiles[TileCategory.Stairway];
			if (item.Count == 0)
			{
				return false;
			}
			int num = Session.Random.Next() % item.Count;
			Tile tile = item[num];
			if (ep == null)
			{
				TileData tileDatum = MapBuilder.add_first_tile(tile);
				Direction startingDirection = MapBuilder.get_starting_direction(MapBuilder.get_orientation(tile, tileDatum));
				MapBuilder.fEndpoints.Add(MapBuilder.get_endpoint(tile, tileDatum, startingDirection));
			}
			else if (MapBuilder.add_tile(tile, ep, true, true) == null)
			{
				return false;
			}
			return true;
		}

		private static Pair<TileData, Direction> add_tile(Tile t, Endpoint ep, bool follow_direction, bool not_alongside)
		{
			TileData tileDatum = new TileData()
			{
				TileID = t.ID
			};
			Direction direction = ep.Direction;
			if (!follow_direction)
			{
				List<Direction> directions = new List<Direction>();
				if (ep.Direction != Direction.North)
				{
					directions.Add(Direction.South);
				}
				if (ep.Direction != Direction.East)
				{
					directions.Add(Direction.West);
				}
				if (ep.Direction != Direction.South)
				{
					directions.Add(Direction.North);
				}
				if (ep.Direction != Direction.West)
				{
					directions.Add(Direction.East);
				}
				int num = Session.Random.Next() % directions.Count;
				direction = directions[num];
			}
			if (!follow_direction)
			{
				tileDatum.Rotations = Session.Random.Next() % 4;
			}
			else
			{
				int num1 = Math.Min(t.Size.Width, t.Size.Height);
				if (direction == Direction.North || direction == Direction.South)
				{
					if (num1 > 1)
					{
						if (t.Size.Width > t.Size.Height)
						{
							tileDatum.Rotations = 1;
						}
					}
					else if (t.Size.Width < t.Size.Height)
					{
						tileDatum.Rotations = 1;
					}
				}
				if (direction == Direction.East || direction == Direction.West)
				{
					if (num1 > 1)
					{
						if (t.Size.Height > t.Size.Width)
						{
							tileDatum.Rotations = 1;
						}
					}
					else if (t.Size.Height < t.Size.Width)
					{
						tileDatum.Rotations = 1;
					}
				}
			}
			int num2 = (tileDatum.Rotations % 2 == 0 ? t.Size.Width : t.Size.Height);
			int num3 = (tileDatum.Rotations % 2 == 0 ? t.Size.Height : t.Size.Width);
			switch (ep.Direction)
			{
				case Direction.North:
				{
					tileDatum.Location = new Point(ep.TopLeft.X, ep.TopLeft.Y - (num3 - 1));
					break;
				}
				case Direction.East:
				{
					tileDatum.Location = ep.TopLeft;
					break;
				}
				case Direction.South:
				{
					tileDatum.Location = ep.TopLeft;
					break;
				}
				case Direction.West:
				{
					tileDatum.Location = new Point(ep.TopLeft.X - (num2 - 1), ep.TopLeft.Y);
					break;
				}
			}
			Rectangle _rect = MapBuilder.get_rect(t, tileDatum);
			if (not_alongside)
			{
				switch (direction)
				{
					case Direction.North:
					case Direction.South:
					{
						_rect = new Rectangle(_rect.X - 1, _rect.Y, _rect.Width + 2, _rect.Height);
						break;
					}
					case Direction.East:
					case Direction.West:
					{
						_rect = new Rectangle(_rect.X, _rect.Y - 1, _rect.Width, _rect.Height + 2);
						break;
					}
				}
			}
			if (!MapBuilder.check_rect_is_empty(_rect))
			{
				return null;
			}
			MapBuilder.fMap.Tiles.Add(tileDatum);
			return new Pair<TileData, Direction>(tileDatum, direction);
		}

		private static void begin_map()
		{
			List<TileCategory> tileCategories = new List<TileCategory>();
			if (MapBuilder.fCorridorTiles.Count != 0)
			{
				tileCategories.Add(TileCategory.Plain);
			}
			if (MapBuilder.fTiles[TileCategory.Stairway].Count != 0)
			{
				tileCategories.Add(TileCategory.Stairway);
			}
			if (tileCategories.Count == 0)
			{
				return;
			}
			switch (tileCategories[Session.Random.Next() % tileCategories.Count])
			{
				case TileCategory.Plain:
				{
					MapBuilder.add_corridor(null, false);
					return;
				}
				case TileCategory.Doorway:
				{
					return;
				}
				case TileCategory.Stairway:
				{
					MapBuilder.add_stairway(null);
					return;
				}
				default:
				{
					return;
				}
			}
		}

		private static void build_filled_area(EventHandler callback)
		{
			List<Tile> tiles = new List<Tile>();
			List<Tile> tiles1 = new List<Tile>();
			foreach (Library library in MapBuilder.fData.Libraries)
			{
				foreach (Tile tile in library.Tiles)
				{
					if (tile.Category != TileCategory.Plain && tile.Category != TileCategory.Feature)
					{
						continue;
					}
					tiles.Add(tile);
					if (tile.Area != 1)
					{
						continue;
					}
					tiles1.Add(tile);
				}
			}
			if (tiles.Count == 0 || tiles1.Count == 0)
			{
				return;
			}
			MapArea mapArea = new MapArea()
			{
				Name = "Area",
				Region = new Rectangle(0, 0, MapBuilder.fData.Width, MapBuilder.fData.Height)
			};
			MapBuilder.fMap.Areas.Add(mapArea);
			int area = 0;
			int num = 0;
			do
			{
				bool flag = Session.Random.Next(20) == 0;
				List<Tile> tiles2 = (flag ? tiles1 : tiles);
				int num1 = Session.Random.Next(tiles2.Count);
				Tile item = tiles2[num1];
				TileData tileDatum = new TileData()
				{
					TileID = item.ID,
					Rotations = Session.Random.Next(4)
				};
				int width = item.Size.Width;
				int height = item.Size.Height;
				if (tileDatum.Rotations == 1 || tileDatum.Rotations == 3)
				{
					width = item.Size.Height;
					height = item.Size.Width;
				}
				List<Point> points = new List<Point>();
				if (!flag)
				{
					int num2 = (item.Area < 4 ? 1 : 2);
					for (int i = 0; i <= MapBuilder.fData.Width; i += num2)
					{
						for (int j = 0; j <= MapBuilder.fData.Height; j += num2)
						{
							Rectangle rectangle = new Rectangle(i, j, width, height);
							if (rectangle.Right <= MapBuilder.fData.Width && rectangle.Bottom <= MapBuilder.fData.Height && MapBuilder.check_rect_is_empty(rectangle))
							{
								points.Add(new Point(i, j));
							}
						}
					}
				}
				else
				{
					for (int k = 0; k <= MapBuilder.fData.Width; k++)
					{
						for (int l = 0; l <= MapBuilder.fData.Height; l++)
						{
							Point point = new Point(k, l);
							if (MapBuilder.tile_at_point(point) == null)
							{
								int num3 = 0;
								if (MapBuilder.tile_at_point(new Point(k + 1, l)) != null)
								{
									num3++;
								}
								if (MapBuilder.tile_at_point(new Point(k - 1, l)) != null)
								{
									num3++;
								}
								if (MapBuilder.tile_at_point(new Point(k, l + 1)) != null)
								{
									num3++;
								}
								if (MapBuilder.tile_at_point(new Point(k, l - 1)) != null)
								{
									num3++;
								}
								if (num3 >= 3)
								{
									points.Add(point);
								}
							}
						}
					}
				}
				if (points.Count == 0)
				{
					num++;
					if (num >= 100)
					{
						num = 0;
						if (MapBuilder.fMap.Tiles.Count != 0)
						{
							int num4 = Session.Random.Next(MapBuilder.fMap.Tiles.Count);
							TileData item1 = MapBuilder.fMap.Tiles[num4];
							MapBuilder.fMap.Tiles.Remove(item1);
							area -= Session.FindTile(item1.TileID, SearchType.Global).Area;
						}
					}
				}
				else
				{
					int num5 = Session.Random.Next(points.Count);
					tileDatum.Location = points[num5];
					MapBuilder.fMap.Tiles.Add(tileDatum);
					area += item.Area;
				}
				callback(null, null);
			}
			while (area != MapBuilder.fData.Width * MapBuilder.fData.Height);
			MapBuilder.fMap.Areas.Clear();
		}

		private static void build_freeform_area(EventHandler callback)
		{
			List<Tile> tiles = new List<Tile>();
			foreach (Library library in MapBuilder.fData.Libraries)
			{
				foreach (Tile tile in library.Tiles)
				{
					if (tile.Category != TileCategory.Plain && tile.Category != TileCategory.Feature)
					{
						continue;
					}
					tiles.Add(tile);
				}
			}
			if (tiles.Count == 0)
			{
				return;
			}
			int height = MapBuilder.fData.Height * MapBuilder.fData.Width;
			while (height > 0)
			{
				callback(null, null);
				if (true)
				{
					int num = Session.Random.Next() % tiles.Count;
					Tile item = tiles[num];
					Point point = new Point(0, 0);
					if (MapBuilder.fMap.Tiles.Count != 0)
					{
						int num1 = Session.Random.Next() % MapBuilder.fMap.Tiles.Count;
						TileData tileDatum = MapBuilder.fMap.Tiles[num1];
						Tile tile1 = Session.FindTile(tileDatum.TileID, SearchType.Global);
						List<Rectangle> rectangles = new List<Rectangle>();
						int x = tileDatum.Location.X;
						Size size = item.Size;
						int width = x - (size.Width - 1);
						int x1 = tileDatum.Location.X + (tile1.Size.Width - 1);
						int y = tileDatum.Location.Y - (item.Size.Height - 1);
						int y1 = tileDatum.Location.Y + (tile1.Size.Height - 1);
						for (int i = width; i <= x1; i++)
						{
							int y2 = tileDatum.Location.Y - item.Size.Height;
							int width1 = item.Size.Width;
							Size size1 = item.Size;
							Rectangle rectangle = new Rectangle(i, y2, width1, size1.Height);
							rectangles.Add(rectangle);
						}
						for (int j = width; j <= x1; j++)
						{
							int num2 = tileDatum.Location.Y + tile1.Size.Height;
							int width2 = item.Size.Width;
							size = item.Size;
							Rectangle rectangle1 = new Rectangle(j, num2, width2, size.Height);
							rectangles.Add(rectangle1);
						}
						for (int k = y; k <= y1; k++)
						{
							int x2 = tileDatum.Location.X;
							size = tile1.Size;
							int width3 = x2 - size.Width;
							int num3 = item.Size.Width;
							size = item.Size;
							Rectangle rectangle2 = new Rectangle(width3, k, num3, size.Height);
							rectangles.Add(rectangle2);
						}
						for (int l = y; l <= y1; l++)
						{
							int x3 = tileDatum.Location.X;
							size = tile1.Size;
							int width4 = x3 + size.Width;
							int num4 = item.Size.Width;
							size = item.Size;
							Rectangle rectangle3 = new Rectangle(width4, l, num4, size.Height);
							rectangles.Add(rectangle3);
						}
						List<Rectangle> rectangles1 = new List<Rectangle>();
						foreach (Rectangle rectangle4 in rectangles)
						{
							if (!MapBuilder.check_rect_is_empty(rectangle4))
							{
								continue;
							}
							rectangles1.Add(rectangle4);
						}
						if (rectangles1.Count == 0)
						{
							continue;
						}
						int num5 = Session.Random.Next() % rectangles1.Count;
						point = rectangles1[num5].Location;
					}
					TileData tileDatum1 = new TileData()
					{
						TileID = item.ID,
						Location = point
					};
					MapBuilder.fMap.Tiles.Add(tileDatum1);
					height -= item.Area;
				}
				else
				{
					Tile tile2 = null;
					if (tile2 == null)
					{
						continue;
					}
					Point point1 = new Point(0, 0);
					TileData tileDatum2 = new TileData()
					{
						TileID = tile2.ID,
						Location = point1
					};
					MapBuilder.fMap.Tiles.Add(tileDatum2);
					height -= tile2.Area;
				}
			}
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			int num9 = 0;
			foreach (TileData tile3 in MapBuilder.fMap.Tiles)
			{
				Tile tile4 = Session.FindTile(tile3.TileID, SearchType.Global);
				Rectangle rectangle5 = new Rectangle(tile3.Location, tile4.Size);
				num6 = Math.Min(num6, rectangle5.Left);
				num7 = Math.Max(num7, rectangle5.Right);
				num8 = Math.Min(num8, rectangle5.Top);
				num9 = Math.Max(num9, rectangle5.Bottom);
			}
			MapArea mapArea = new MapArea()
			{
				Name = "Area",
				Region = new Rectangle(num6, num8, num7 - num6, num9 - num8)
			};
			MapBuilder.fMap.Areas.Add(mapArea);
		}

		private static void build_tile_lists()
		{
			MapBuilder.fTiles.Clear();
			foreach (TileCategory value in Enum.GetValues(typeof(TileCategory)))
			{
				MapBuilder.fTiles[value] = new List<Tile>();
			}
			foreach (Library library in MapBuilder.fData.Libraries)
			{
				foreach (Tile tile in library.Tiles)
				{
					MapBuilder.fTiles[tile.Category].Add(tile);
				}
			}
			MapBuilder.fRoomTiles.Clear();
			MapBuilder.fCorridorTiles.Clear();
			foreach (Tile item in MapBuilder.fTiles[TileCategory.Plain])
			{
				int num = Math.Min(item.Size.Width, item.Size.Height);
				if (num == 2)
				{
					MapBuilder.fCorridorTiles.Add(item);
				}
				if (num <= 2)
				{
					continue;
				}
				MapBuilder.fRoomTiles.Add(item);
			}
		}

		private static void build_warren(EventHandler callback)
		{
			MapBuilder.begin_map();
			int num = 0;
			while (MapBuilder.fMap.Areas.Count < MapBuilder.fData.MaxAreaCount && MapBuilder.fEndpoints.Count != 0 && num != 100)
			{
				int num1 = Session.Random.Next() % MapBuilder.fEndpoints.Count;
				Endpoint item = MapBuilder.fEndpoints[num1];
				bool flag = true;
				switch (Session.Random.Next() % 10)
				{
					case 0:
					case 1:
					case 2:
					{
						try
						{
							flag = MapBuilder.add_area(item);
							break;
						}
						catch (Exception exception)
						{
							LogSystem.Trace(exception);
							flag = false;
							break;
						}
						break;
					}
					case 3:
					case 4:
					case 5:
					case 6:
					case 7:
					{
						try
						{
							flag = MapBuilder.add_corridor(item, false);
							break;
						}
						catch (Exception exception1)
						{
							LogSystem.Trace(exception1);
							flag = false;
							break;
						}
						break;
					}
					case 8:
					{
						try
						{
							if (item.Category != TileCategory.Doorway)
							{
								flag = MapBuilder.add_doorway(item);
							}
							break;
						}
						catch (Exception exception2)
						{
							LogSystem.Trace(exception2);
							flag = false;
							break;
						}
						break;
					}
					case 9:
					{
						try
						{
							flag = MapBuilder.add_stairway(item);
							break;
						}
						catch (Exception exception3)
						{
							LogSystem.Trace(exception3);
							flag = false;
							break;
						}
						break;
					}
				}
				if (!flag)
				{
					num++;
				}
				else
				{
					MapBuilder.fEndpoints.Remove(item);
					num = 0;
					callback(null, null);
				}
			}
			List<TileData> tileDatas = new List<TileData>();
			foreach (TileData tile in MapBuilder.fMap.Tiles)
			{
				Tile tile1 = Session.FindTile(tile.TileID, SearchType.Global);
				if (tile1 != null)
				{
					if (tile1.Category != TileCategory.Doorway)
					{
						continue;
					}
					Rectangle _rect = MapBuilder.get_rect(tile1, tile);
					int num2 = 0;
					int left = _rect.Left;
					while (left != _rect.Right)
					{
						int top = _rect.Top - 1;
						if (MapBuilder.tile_at_point(new Point(left, top)) != null)
						{
							left++;
						}
						else
						{
							num2++;
							break;
						}
					}
					int left1 = _rect.Left;
					while (left1 != _rect.Right)
					{
						int bottom = _rect.Bottom + 1;
						if (MapBuilder.tile_at_point(new Point(left1, bottom)) != null)
						{
							left1++;
						}
						else
						{
							num2++;
							break;
						}
					}
					int top1 = _rect.Top;
					while (top1 != _rect.Bottom)
					{
						int left2 = _rect.Left - 1;
						if (MapBuilder.tile_at_point(new Point(left2, top1)) != null)
						{
							top1++;
						}
						else
						{
							num2++;
							break;
						}
					}
					int top2 = _rect.Top;
					while (top2 != _rect.Bottom)
					{
						int right = _rect.Right + 1;
						if (MapBuilder.tile_at_point(new Point(right, top2)) != null)
						{
							top2++;
						}
						else
						{
							num2++;
							break;
						}
					}
					if (num2 == 2)
					{
						continue;
					}
					tileDatas.Add(tile);
				}
				else
				{
					tileDatas.Add(tile);
				}
			}
			foreach (TileData tileData in tileDatas)
			{
				MapBuilder.fMap.Tiles.Remove(tileData);
				callback(null, null);
			}
		}

		public static void BuildMap(MapBuilderData data, Map map, EventHandler callback)
		{
			MapBuilder.fData = data;
			MapBuilder.fMap = map;
			MapBuilder.fMap.Tiles.Clear();
			MapBuilder.fMap.Areas.Clear();
			switch (MapBuilder.fData.Type)
			{
				case MapAutoBuildType.Warren:
				{
					MapBuilder.fEndpoints.Clear();
					MapBuilder.build_tile_lists();
					MapBuilder.build_warren(callback);
					return;
				}
				case MapAutoBuildType.FilledArea:
				{
					MapBuilder.build_filled_area(callback);
					return;
				}
				case MapAutoBuildType.Freeform:
				{
					MapBuilder.build_freeform_area(callback);
					return;
				}
				default:
				{
					return;
				}
			}
		}

		private static bool check_rect_is_empty(Rectangle rect)
		{
			bool flag;
			List<TileData>.Enumerator enumerator = MapBuilder.fMap.Tiles.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					TileData current = enumerator.Current;
					if (!MapBuilder.get_rect(Session.FindTile(current.TileID, SearchType.Global), current).IntersectsWith(rect))
					{
						continue;
					}
					flag = false;
					return flag;
				}
				return true;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return flag;
		}

		private static Endpoint get_endpoint(Tile t, TileData td, Direction dir)
		{
			Endpoint endpoint = new Endpoint()
			{
				Category = t.Category,
				Direction = dir
			};
			int num = (td.Rotations % 2 == 0 ? t.Size.Width : t.Size.Height);
			int num1 = (td.Rotations % 2 == 0 ? t.Size.Height : t.Size.Width);
			switch (dir)
			{
				case Direction.North:
				{
					int x = td.Location.X;
					Point location = td.Location;
					endpoint.TopLeft = new Point(x, location.Y - 1);
					Point point = td.Location;
					Point location1 = td.Location;
					endpoint.BottomRight = new Point(point.X + num - 1, location1.Y - 1);
					break;
				}
				case Direction.East:
				{
					Point point1 = td.Location;
					Point location2 = td.Location;
					endpoint.TopLeft = new Point(point1.X + num, location2.Y);
					Point point2 = td.Location;
					Point location3 = td.Location;
					endpoint.BottomRight = new Point(point2.X + num, location3.Y + num1 - 1);
					break;
				}
				case Direction.South:
				{
					int x1 = td.Location.X;
					Point point3 = td.Location;
					endpoint.TopLeft = new Point(x1, point3.Y + num1);
					Point location4 = td.Location;
					Point point4 = td.Location;
					endpoint.BottomRight = new Point(location4.X + num - 1, point4.Y + num1);
					break;
				}
				case Direction.West:
				{
					Point location5 = td.Location;
					Point point5 = td.Location;
					endpoint.TopLeft = new Point(location5.X - 1, point5.Y);
					Point location6 = td.Location;
					Point point6 = td.Location;
					endpoint.BottomRight = new Point(location6.X - 1, point6.Y + num1 - 1);
					break;
				}
			}
			return endpoint;
		}

		private static Orientation get_orientation(Tile t, TileData td)
		{
			bool width = t.Size.Width >= t.Size.Height;
			if (td.Rotations % 2 == 0)
			{
				if (!width)
				{
					return Orientation.NorthSouth;
				}
				return Orientation.EastWest;
			}
			if (!width)
			{
				return Orientation.EastWest;
			}
			return Orientation.NorthSouth;
		}

		private static Rectangle get_rect(Tile t, TileData td)
		{
			int num = (td.Rotations % 2 == 0 ? t.Size.Width : t.Size.Height);
			int num1 = (td.Rotations % 2 == 0 ? t.Size.Height : t.Size.Width);
			return new Rectangle(td.Location.X, td.Location.Y, num, num1);
		}

		private static Direction get_starting_direction(Orientation orient)
		{
			switch (orient)
			{
				case Orientation.NorthSouth:
				{
					return Direction.South;
				}
				case Orientation.EastWest:
				{
					return Direction.East;
				}
			}
			if (Session.Random.Next() % 2 != 0)
			{
				return Direction.South;
			}
			return Direction.East;
		}

		private static TileData tile_at_point(Point pt)
		{
			TileData tileDatum;
			List<TileData>.Enumerator enumerator = MapBuilder.fMap.Tiles.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					TileData current = enumerator.Current;
					if (!MapBuilder.get_rect(Session.FindTile(current.TileID, SearchType.Global), current).Contains(pt))
					{
						continue;
					}
					tileDatum = current;
					return tileDatum;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return tileDatum;
		}
	}
}