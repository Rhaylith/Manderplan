using System;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class CustomToken : IToken
	{
		private Guid fID = Guid.NewGuid();

		private CustomTokenType fType;

		private string fName = "";

		private string fDetails = "";

		private CreatureSize fTokenSize = CreatureSize.Medium;

		private Size fOverlaySize = new Size(3, 3);

		private Masterplan.Data.OverlayStyle fOverlayStyle;

		private Color fColour = Color.DarkBlue;

		private System.Drawing.Image fImage;

		private bool fDifficultTerrain;

		private bool fOpaque;

		private CombatData fData = new CombatData();

		private Masterplan.Data.TerrainPower fTerrainPower;

		private Guid fCreatureID = Guid.Empty;

		public Color Colour
		{
			get
			{
				return this.fColour;
			}
			set
			{
				this.fColour = value;
			}
		}

		public Guid CreatureID
		{
			get
			{
				return this.fCreatureID;
			}
			set
			{
				this.fCreatureID = value;
			}
		}

		public CombatData Data
		{
			get
			{
				return this.fData;
			}
			set
			{
				this.fData = value;
			}
		}

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

		public bool DifficultTerrain
		{
			get
			{
				return this.fDifficultTerrain;
			}
			set
			{
				this.fDifficultTerrain = value;
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

		public bool Opaque
		{
			get
			{
				return this.fOpaque;
			}
			set
			{
				this.fOpaque = value;
			}
		}

        public bool IsTerrainLayer
        {
            get;
            set;
        }

        public bool IsUnSelectable
        {
            get;
            set;
        }

        // TODO:  Need to support cover as well
        public bool BlocksLineOfSight
        {
            get
            {
                return this.Opaque;
            }
            set
            {
                this.Opaque = value;
            }
        }

        public Size OverlaySize
		{
			get
			{
				return this.fOverlaySize;
			}
			set
			{
				this.fOverlaySize = value;
			}
		}

		public Masterplan.Data.OverlayStyle OverlayStyle
		{
			get
			{
				return this.fOverlayStyle;
			}
			set
			{
				this.fOverlayStyle = value;
			}
		}

		public Masterplan.Data.TerrainPower TerrainPower
		{
			get
			{
				return this.fTerrainPower;
			}
			set
			{
				this.fTerrainPower = value;
			}
		}

		public CreatureSize TokenSize
		{
			get
			{
				return this.fTokenSize;
			}
			set
			{
				this.fTokenSize = value;
			}
		}

		public CustomTokenType Type
		{
			get
			{
				return this.fType;
			}
			set
			{
				this.fType = value;
			}
		}

		public CustomToken()
		{
		}

		public CustomToken Copy()
		{
			Masterplan.Data.TerrainPower terrainPower;
			CustomToken customToken = new CustomToken()
			{
				ID = this.fID,
				Type = this.fType,
				Name = this.fName,
				Details = this.fDetails,
				TokenSize = this.fTokenSize,
				OverlaySize = this.fOverlaySize,
				OverlayStyle = this.fOverlayStyle,
				Colour = this.fColour,
				Image = this.fImage,
				DifficultTerrain = this.fDifficultTerrain,
				Opaque = this.fOpaque,
                IsTerrainLayer = this.IsTerrainLayer,
                IsUnSelectable = this.IsUnSelectable,
				Data = this.fData.Copy()
			};
			CustomToken customToken1 = customToken;
			if (this.fTerrainPower != null)
			{
				terrainPower = this.fTerrainPower.Copy();
			}
			else
			{
				terrainPower = null;
			}
			customToken1.TerrainPower = terrainPower;
			customToken.CreatureID = this.fCreatureID;
			return customToken;
		}
	}
}