using System;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class MapArea
	{
		private string fName = "";

		private Guid fID = Guid.NewGuid();

		private string fDetails = "";

		private Rectangle fRegion = new Rectangle(0, 0, 1, 1);

		public string Details
		{
			get
			{
				return this.fDetails;
			}
			set
			{
				this.fDetails = value;
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

		public Rectangle Region
		{
			get
			{
				return this.fRegion;
			}
			set
			{
				this.fRegion = value;
			}
		}

		public MapArea()
		{
		}

		public MapArea Copy()
		{
			MapArea mapArea = new MapArea()
			{
				Name = this.fName,
				ID = this.fID,
				Details = this.fDetails,
				Region = new Rectangle(this.fRegion.X, this.fRegion.Y, this.fRegion.Width, this.fRegion.Height)
			};
			return mapArea;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}