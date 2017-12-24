using System;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class MapLocation
	{
		private string fName = "";

		private string fCategory = "";

		private Guid fID = Guid.NewGuid();

		private PointF fPoint = PointF.Empty;

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

		public PointF Point
		{
			get
			{
				return this.fPoint;
			}
			set
			{
				this.fPoint = value;
			}
		}

		public MapLocation()
		{
		}

		public MapLocation Copy()
		{
			MapLocation mapLocation = new MapLocation()
			{
				Name = this.fName,
				Category = this.fCategory,
				ID = this.fID,
				Point = new PointF(this.fPoint.X, this.fPoint.Y)
			};
			return mapLocation;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}