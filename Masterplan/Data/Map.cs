using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class Map
	{
		private string fName = "";

		private Guid fID = Guid.NewGuid();

		private string fCategory = "";

		private List<TileData> fTiles = new List<TileData>();

		private List<MapArea> fAreas = new List<MapArea>();

		public List<MapArea> Areas
		{
			get
			{
				return this.fAreas;
			}
			set
			{
				this.fAreas = value;
			}
		}

		public string Category
		{
			get
			{
				return this.fCategory;
			}
			set
			{
				this.fCategory = value;
			}
		}

		public Guid ID
		{
			get
			{
				return this.fID;
			}
			set
			{
				this.fID = value;
			}
		}

		public string Name
		{
			get
			{
				return this.fName;
			}
			set
			{
				this.fName = value;
			}
		}

		public List<TileData> Tiles
		{
			get
			{
				return this.fTiles;
			}
			set
			{
				this.fTiles = value;
			}
		}

		public Map()
		{
		}

		public Map Copy()
		{
			Map map = new Map()
			{
				Name = this.fName,
				ID = this.fID,
				Category = this.fCategory
			};
			foreach (TileData fTile in this.fTiles)
			{
				map.Tiles.Add(fTile.Copy());
			}
			foreach (MapArea fArea in this.fAreas)
			{
				map.Areas.Add(fArea.Copy());
			}
			return map;
		}

		public MapArea FindArea(Guid area_id)
		{
			MapArea mapArea;
			List<MapArea>.Enumerator enumerator = this.fAreas.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					MapArea current = enumerator.Current;
					if (current.ID != area_id)
					{
						continue;
					}
					mapArea = current;
					return mapArea;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}