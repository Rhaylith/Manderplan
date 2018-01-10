using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class RegionalMap
	{
		private string fName = "";

		private Guid fID = Guid.NewGuid();

		private System.Drawing.Image fImage;

		private List<MapLocation> fLocations = new List<MapLocation>();

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

		public System.Drawing.Image Image
		{
			get
			{
				return this.fImage;
			}
			set
			{
				this.fImage = value;
			}
		}

		public List<MapLocation> Locations
		{
			get
			{
				return this.fLocations;
			}
			set
			{
				this.fLocations = value;
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

		public RegionalMap()
		{
		}

		public RegionalMap Copy()
		{
			RegionalMap regionalMap = new RegionalMap()
			{
				Name = this.fName,
				ID = this.fID,
				Image = this.fImage
			};
			foreach (MapLocation fLocation in this.fLocations)
			{
				regionalMap.Locations.Add(fLocation.Copy());
			}
			return regionalMap;
		}

		public MapLocation FindLocation(Guid location_id)
		{
			MapLocation mapLocation;
			List<MapLocation>.Enumerator enumerator = this.fLocations.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					MapLocation current = enumerator.Current;
					if (current.ID != location_id)
					{
						continue;
					}
					mapLocation = current;
					return mapLocation;
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