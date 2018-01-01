using Masterplan.Controls;
using Masterplan.UI;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan
{
	[Serializable]
	public class Preferences
	{
		private string fLastFile = "";

		private bool fShowHeadlines = true;

		private bool fMaximised;

		private System.Drawing.Size fSize = System.Drawing.Size.Empty;

		private Point fLocation = Point.Empty;

		private bool fNavigation;

		private bool fPreview = true;

		private PlotViewLinkStyle fLinkStyle;

		private bool fAllXP = true;

		private Masterplan.UI.TileView fTileView = Masterplan.UI.TileView.Size;

		private Masterplan.UI.TileSize fTileSize = Masterplan.UI.TileSize.Medium;

		private List<Guid> fTileLibraries = new List<Guid>();

		private Data.Combat.InitiativeMode fInitiativeMode;

		private Data.Combat.InitiativeMode fHeroInitiativeMode = Data.Combat.InitiativeMode.ManualIndividual;

		private Data.Combat.InitiativeMode fTrapInitiativeMode = Data.Combat.InitiativeMode.AutoIndividual;

		private bool fCreatureAutoRemove = true;

		private bool fiPlay4E = true;

		private bool fCombatTwoColumns;

		private bool fCombatTwoColumnsNoMap = true;

		private bool fCombatMapRight = true;

		private CreatureViewMode fCombatFog;

		private CreatureViewMode fPlayerViewFog = CreatureViewMode.Visible;

		private bool fCombatHealthBars;

		private bool fPlayerViewConditionBadges = true;

		private bool fCombatConditionBadges = true;

		private bool fPlayerViewHealthBars;

		private bool fCreatureLabels;

		private bool fCombatPictureTokens = true;

		private bool fPlayerViewPictureTokens = true;

		private bool fPlayerViewMap = true;

		private bool fPlayerViewInitiative = true;

		private bool fCombatGrid;

		private bool fPlayerViewGrid;

		private bool fCombatGridLabels;

		private bool fPlayerViewGridLabels;

		private bool fCombatColumnInitiative = true;

		private bool fCombatColumnHP = true;

		private bool fCombatColumnDefences;

		private bool fCombatColumnEffects;

		public bool AllXP
		{
			get
			{
				return this.fAllXP;
			}
			set
			{
				this.fAllXP = value;
			}
		}

		public bool CombatColumnDefences
		{
			get
			{
				return this.fCombatColumnDefences;
			}
			set
			{
				this.fCombatColumnDefences = value;
			}
		}

		public bool CombatColumnEffects
		{
			get
			{
				return this.fCombatColumnEffects;
			}
			set
			{
				this.fCombatColumnEffects = value;
			}
		}

		public bool CombatColumnHP
		{
			get
			{
				return this.fCombatColumnHP;
			}
			set
			{
				this.fCombatColumnHP = value;
			}
		}

		public bool CombatColumnInitiative
		{
			get
			{
				return this.fCombatColumnInitiative;
			}
			set
			{
				this.fCombatColumnInitiative = value;
			}
		}

		public bool CombatConditionBadges
		{
			get
			{
				return this.fCombatConditionBadges;
			}
			set
			{
				this.fCombatConditionBadges = value;
			}
		}

		public CreatureViewMode CombatFog
		{
			get
			{
				return this.fCombatFog;
			}
			set
			{
				this.fCombatFog = value;
			}
		}

		public bool CombatGrid
		{
			get
			{
				return this.fCombatGrid;
			}
			set
			{
				this.fCombatGrid = value;
			}
		}

		public bool CombatGridLabels
		{
			get
			{
				return this.fCombatGridLabels;
			}
			set
			{
				this.fCombatGridLabels = value;
			}
		}

		public bool CombatHealthBars
		{
			get
			{
				return this.fCombatHealthBars;
			}
			set
			{
				this.fCombatHealthBars = value;
			}
		}

		public bool CombatMapRight
		{
			get
			{
				return this.fCombatMapRight;
			}
			set
			{
				this.fCombatMapRight = value;
			}
		}

		public bool CombatPictureTokens
		{
			get
			{
				return this.fCombatPictureTokens;
			}
			set
			{
				this.fCombatPictureTokens = value;
			}
		}

		public bool CombatTwoColumns
		{
			get
			{
				return this.fCombatTwoColumns;
			}
			set
			{
				this.fCombatTwoColumns = value;
			}
		}

		public bool CombatTwoColumnsNoMap
		{
			get
			{
				return this.fCombatTwoColumnsNoMap;
			}
			set
			{
				this.fCombatTwoColumnsNoMap = value;
			}
		}

		public bool CreatureAutoRemove
		{
			get
			{
				return this.fCreatureAutoRemove;
			}
			set
			{
				this.fCreatureAutoRemove = value;
			}
		}

		public Data.Combat.InitiativeMode HeroInitiativeMode
		{
			get
			{
				return this.fHeroInitiativeMode;
			}
			set
			{
				this.fHeroInitiativeMode = value;
			}
		}

		public Data.Combat.InitiativeMode InitiativeMode
		{
			get
			{
				return this.fInitiativeMode;
			}
			set
			{
				this.fInitiativeMode = value;
			}
		}

		public bool iPlay4E
		{
			get
			{
				return this.fiPlay4E;
			}
			set
			{
				this.fiPlay4E = value;
			}
		}

		public string LastFile
		{
			get
			{
				return this.fLastFile;
			}
			set
			{
				this.fLastFile = value;
			}
		}

		public PlotViewLinkStyle LinkStyle
		{
			get
			{
				return this.fLinkStyle;
			}
			set
			{
				this.fLinkStyle = value;
			}
		}

		public Point Location
		{
			get
			{
				return this.fLocation;
			}
			set
			{
				this.fLocation = value;
			}
		}

		public bool Maximised
		{
			get
			{
				return this.fMaximised;
			}
			set
			{
				this.fMaximised = value;
			}
		}

		public bool PlayerViewConditionBadges
		{
			get
			{
				return this.fPlayerViewConditionBadges;
			}
			set
			{
				this.fPlayerViewConditionBadges = value;
			}
		}

		public bool PlayerViewCreatureLabels
		{
			get
			{
				return this.fCreatureLabels;
			}
			set
			{
				this.fCreatureLabels = value;
			}
		}

		public CreatureViewMode PlayerViewFog
		{
			get
			{
				return this.fPlayerViewFog;
			}
			set
			{
				this.fPlayerViewFog = value;
			}
		}

		public bool PlayerViewGrid
		{
			get
			{
				return this.fPlayerViewGrid;
			}
			set
			{
				this.fPlayerViewGrid = value;
			}
		}

		public bool PlayerViewGridLabels
		{
			get
			{
				return this.fPlayerViewGridLabels;
			}
			set
			{
				this.fPlayerViewGridLabels = value;
			}
		}

		public bool PlayerViewHealthBars
		{
			get
			{
				return this.fPlayerViewHealthBars;
			}
			set
			{
				this.fPlayerViewHealthBars = value;
			}
		}

		public bool PlayerViewInitiative
		{
			get
			{
				return this.fPlayerViewInitiative;
			}
			set
			{
				this.fPlayerViewInitiative = value;
			}
		}

		public bool PlayerViewMap
		{
			get
			{
				return this.fPlayerViewMap;
			}
			set
			{
				this.fPlayerViewMap = value;
			}
		}

		public bool PlayerViewPictureTokens
		{
			get
			{
				return this.fPlayerViewPictureTokens;
			}
			set
			{
				this.fPlayerViewPictureTokens = value;
			}
		}

		public bool ShowHeadlines
		{
			get
			{
				return this.fShowHeadlines;
			}
			set
			{
				this.fShowHeadlines = value;
			}
		}

		public bool ShowNavigation
		{
			get
			{
				return this.fNavigation;
			}
			set
			{
				this.fNavigation = value;
			}
		}

		public bool ShowPreview
		{
			get
			{
				return this.fPreview;
			}
			set
			{
				this.fPreview = value;
			}
		}

		public System.Drawing.Size Size
		{
			get
			{
				return this.fSize;
			}
			set
			{
				this.fSize = value;
			}
		}

		public List<Guid> TileLibraries
		{
			get
			{
				return this.fTileLibraries;
			}
			set
			{
				this.fTileLibraries = value;
			}
		}

		public Masterplan.UI.TileSize TileSize
		{
			get
			{
				return this.fTileSize;
			}
			set
			{
				this.fTileSize = value;
			}
		}

		public Masterplan.UI.TileView TileView
		{
			get
			{
				return this.fTileView;
			}
			set
			{
				this.fTileView = value;
			}
		}

		public Data.Combat.InitiativeMode TrapInitiativeMode
		{
			get
			{
				return this.fTrapInitiativeMode;
			}
			set
			{
				this.fTrapInitiativeMode = value;
			}
		}

		public Preferences()
		{
		}
	}
}