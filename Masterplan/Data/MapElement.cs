using System;

namespace Masterplan.Data
{
	[Serializable]
	public class MapElement : IElement
	{
		private Guid fMapID = Guid.Empty;

		private Guid fMapAreaID = Guid.Empty;

		public Guid MapAreaID
		{
			get
			{
				return this.fMapAreaID;
			}
			set
			{
				this.fMapAreaID = value;
			}
		}

		public Guid MapID
		{
			get
			{
				return this.fMapID;
			}
			set
			{
				this.fMapID = value;
			}
		}

		public MapElement()
		{
		}

		public MapElement(Guid map_id, Guid map_area_id)
		{
			this.fMapID = map_id;
			this.fMapAreaID = map_area_id;
		}

		public IElement Copy()
		{
			MapElement mapElement = new MapElement()
			{
				MapID = this.fMapID,
				MapAreaID = this.fMapAreaID
			};
			return mapElement;
		}

		public Difficulty GetDifficulty(int party_level, int party_size)
		{
			return Difficulty.Moderate;
		}

		public int GetXP()
		{
			return 0;
		}
	}
}