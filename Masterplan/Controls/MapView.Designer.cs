using Masterplan;
using Masterplan.Commands;
using Masterplan.Commands.Combat;
using Masterplan.Data;
using Masterplan.Data.Combat.Visibility;
using Masterplan.Events;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Utils;
using Utils.Graphics;

using Microsoft.VisualStudio.Profiler;

namespace Masterplan.Controls
{
    partial class MapView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool ShouldRenderVisibility = true;
        public bool HideNonVisibleTokens = false;
        public bool ShowLabelsForNonVisibleTokens = true;
        Color TokenInCoverColor = Color.DimGray;
        Color TokenNotVisibleColor = Color.Yellow;
        Color SquareInCoverColor = Color.FromArgb(128, Color.Black);
        Color SquareObscurredColor = Color.Black; //Color.FromArgb(128, Color.Black);

        private Masterplan.Data.Map fMap;

        private Masterplan.Data.Map fBackgroundMap;

        private Masterplan.Data.Encounter fEncounter;

        private bool fShowAllWaves;

        private Masterplan.Data.Plot fPlot;

        private Rectangle fViewpoint = Rectangle.Empty;

        private MapViewMode fMode;

        private bool fTactical;

        private bool fHighlightAreas;

        private MapGridMode fShowGrid;

        private bool fShowGridLabels;

        private bool fShowPictureTokens = true;

        private int fBorderSize;

        private List<TileData> fSelectedTiles;

        private List<IToken> fBoxedTokens = new List<IToken>();

        private List<IToken> fSelectedTokens = new List<IToken>();

        private IToken fHoverToken;

        private TokenLink fHoverTokenLink;

        private Rectangle fCurrentOutline = Rectangle.Empty;

        private CreatureViewMode fShowCreatures;

        private bool fShowCreatureLabels = true;

        private bool fShowHealthBars;

        private bool fShowConditions = true;

        private bool fShowAuras = true;

        private MapArea fHighlightedArea;

        private MapArea fSelectedArea;

        private bool fAllowLinkCreation;

        private List<TokenLink> fTokenLinks;

        private Dictionary<TokenLink, RectangleF> fTokenLinkRegions = new Dictionary<TokenLink, RectangleF>();

        private bool fAllowScrolling;

        private double fScalingFactor = 1;

        private MapDisplayType fFrameType = MapDisplayType.Dimmed;

        private bool fLineOfSight;

        private MapView.DrawingData fDrawing;

        private List<MapSketch> fSketches = new List<MapSketch>();

        private string fCaption = "";

        private MapData fLayoutData;

        private MapView.NewTile fNewTile;

        private TileData fHoverTile;

        private MapView.DraggedTiles fDraggedTiles;

        private MapView.NewToken fNewToken;

        private MapView.DraggedToken fDraggedToken;

        private StringFormat fCentred = new StringFormat();

        private StringFormat fTop = new StringFormat();

        private StringFormat fBottom = new StringFormat();

        private StringFormat fLeft = new StringFormat();

        private StringFormat fRight = new StringFormat();

        private MapView.DraggedOutline fDraggedOutline;

        private MapView.ScrollingData fScrollingData;

        private Dictionary<Guid, List<Rectangle>> fSlotRegions = new Dictionary<Guid, List<Rectangle>>();

        private BufferedGraphics backbufferGraphics;
        private Graphics drawingGraphics;

        private BufferedGraphicsContext backbufferContext;

        private Bitmap terrainBitmap = null;

        private void RecreateBuffers()
        {
            // Check initialization has completed so we know backbufferContext has been assigned.
            // Check that we aren't disposing or this could be invalid.
            //if (!initializationComplete || isDisposing)
            //    return;

            // We recreate the buffer with a width and height of the control. The "+ 1"
            // guarantees we never have a buffer with a width or height of 0.
            backbufferContext.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);

            // Dispose of old backbufferGraphics (if one has been created already)
            if (backbufferGraphics != null)
                backbufferGraphics.Dispose();

            // Create new backbufferGrpahics that matches the current size of buffer.
            backbufferGraphics = backbufferContext.Allocate(this.CreateGraphics(),
          new Rectangle(0, 0, Math.Max(this.Width, 1), Math.Max(this.Height, 1)));

            // Assign the Graphics object on backbufferGraphics to "drawingGraphics" for easy reference elsewhere.
            drawingGraphics = backbufferGraphics.Graphics;

            // This is a good place to assign drawingGraphics.SmoothingMode if you want a better anti-aliasing technique.

            if (terrainBitmap != null)
            {
                terrainBitmap.Dispose();
            }

            terrainBitmap = new Bitmap(this.Width, this.Height);
            RebuildTerrainLayer();
        }

        public bool AllowDrawing
        {
            get
            {
                return this.fDrawing != null;
            }
            set
            {
                if (!value)
                {
                    this.fDrawing = null;
                }
                else
                {
                    this.fDrawing = new MapView.DrawingData();
                }
                this.Cursor = (this.fDrawing == null ? Cursors.Default : Cursors.Cross);
                base.Invalidate();
                if (this.fDrawing == null)
                {
                    this.OnCancelledDrawing();
                }
            }
        }

        [Category("Behavior")]
        [Description("Determines whether links between tokens can be created.")]
        public bool AllowLinkCreation
        {
            get
            {
                return this.fAllowLinkCreation;
            }
            set
            {
                this.fAllowLinkCreation = value;
            }
        }

        [Category("Behavior")]
        [Description("Determines whether the map can be scrolled.")]
        public bool AllowScrolling
        {
            get
            {
                return this.fAllowScrolling;
            }
            set
            {
                this.fAllowScrolling = value;
                if (!this.fAllowScrolling)
                {
                    if (this.fScalingFactor != 1)
                    {
                        this.fViewpoint = this.get_current_zoom_rect(false);
                    }
                    this.fScalingFactor = 1;
                }
                this.Cursor = (this.fAllowScrolling ? Cursors.SizeAll : Cursors.Default);
                base.Invalidate();
                if (!this.fAllowScrolling)
                {
                    this.OnCancelledScrolling();
                }
            }
        }

        [Category("Data")]
        [Description("The map to be displayed in the background.")]
        public Masterplan.Data.Map BackgroundMap
        {
            get
            {
                return this.fBackgroundMap;
            }
            set
            {
                Masterplan.Data.Map map;
                if (value != null)
                {
                    map = value.Copy();
                }
                else
                {
                    map = null;
                }
                this.fBackgroundMap = map;
                this.fLayoutData = null;
                base.Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The number of squares to be drawn around the viewpoint.")]
        public int BorderSize
        {
            get
            {
                return this.fBorderSize;
            }
            set
            {
                this.fBorderSize = value;
                base.Invalidate();
            }
        }

        [Category("Data")]
        [Description("The list of boxed map tokens.")]
        public List<IToken> BoxedTokens
        {
            get
            {
                return this.fBoxedTokens;
            }
        }

        public string Caption
        {
            get
            {
                return this.fCaption;
            }
            set
            {
                this.fCaption = value;
            }
        }

        [Category("Data")]
        [Description("The encounter to be displayed.")]
        public Masterplan.Data.Encounter Encounter
        {
            get
            {
                return this.fEncounter;
            }
            set
            {
                this.fEncounter = value;
            }
        }

        [Category("Appearance")]
        [Description("The appearance of the frame around the viewpoint.")]
        public MapDisplayType FrameType
        {
            get
            {
                return this.fFrameType;
            }
            set
            {
                this.fFrameType = value;
            }
        }

        [Category("Appearance")]
        [Description("Determines whether areas are highlighted.")]
        public bool HighlightAreas
        {
            get
            {
                return this.fHighlightAreas;
            }
            set
            {
                this.fHighlightAreas = value;
            }
        }

        [Category("Appearance")]
        [Description("The highlighted MapArea.")]
        public MapArea HighlightedArea
        {
            get
            {
                return this.fHighlightedArea;
            }
        }

        [Category("Appearance")]
        [Description("The hovered map token.")]
        public IToken HoverToken
        {
            get
            {
                return this.fHoverToken;
            }
            set
            {
                this.fHoverToken = value;
            }
        }

        [Category("Appearance")]
        [Description("The hovered token link.")]
        public TokenLink HoverTokenLink
        {
            get
            {
                return this.fHoverTokenLink;
            }
            set
            {
                this.fHoverTokenLink = value;
            }
        }

        internal MapData LayoutData
        {
            get
            {
                if (this.fLayoutData == null)
                {
                    this.fLayoutData = new MapData(this, this.fScalingFactor);
                }
                return this.fLayoutData;
            }
        }

        [Category("Appearance")]
        [Description("How the line of sight is displayed.")]
        public bool LineOfSight
        {
            get
            {
                return this.fLineOfSight;
            }
            set
            {
                this.fLineOfSight = value;
                if (!this.fLineOfSight)
                {
                    this.OnCancelledLOS();
                }
            }
        }

        [Category("Data")]
        [Description("The map to be displayed.")]
        public Masterplan.Data.Map Map
        {
            get
            {
                return this.fMap;
            }
            set
            {
                this.fMap = value;
                this.fLayoutData = null;
            }
        }

        [Category("Appearance")]
        [Description("The mode in which to display the map.")]
        public MapViewMode Mode
        {
            get
            {
                return this.fMode;
            }
            set
            {
                this.fMode = value;
            }
        }

        [Category("Data")]
        [Description("The plot to be displayed.")]
        public Masterplan.Data.Plot Plot
        {
            get
            {
                return this.fPlot;
            }
            set
            {
                this.fPlot = value;
            }
        }

        [Category("Appearance")]
        [Description("The scaling factor for the map; this can be used to zoom in and out.")]
        public double ScalingFactor
        {
            get
            {
                return this.fScalingFactor;
            }
            set
            {
                this.fScalingFactor = value;
            }
        }

        [Category("Appearance")]
        [Description("The selected MapArea.")]
        public MapArea SelectedArea
        {
            get
            {
                return this.fSelectedArea;
            }
            set
            {
                this.fSelectedArea = value;
            }
        }

        [Category("Data")]
        [Description("The list of selected tiles.")]
        public List<TileData> SelectedTiles
        {
            get
            {
                return this.fSelectedTiles;
            }
            set
            {
                this.fSelectedTiles = value;
            }
        }

        [Category("Appearance")]
        [Description("The list of selected map tokens.")]
        public List<IToken> SelectedTokens
        {
            get
            {
                return this.fSelectedTokens;
            }
        }

        [Category("Appearance")]
        [Description("The rubber-band selected rectangle.")]
        public Rectangle Selection
        {
            get
            {
                return this.fCurrentOutline;
            }
            set
            {
                this.fCurrentOutline = value;
            }
        }

        [Category("Appearance")]
        [Description("Whether we should show all waves, or only active waves.")]
        public bool ShowAllWaves
        {
            get
            {
                return this.fShowAllWaves;
            }
            set
            {
                this.fShowAllWaves = value;
            }
        }

        [Category("Appearance")]
        [Description("Whether creature auras should be shown.")]
        public bool ShowAuras
        {
            get
            {
                return this.fShowAuras;
            }
            set
            {
                this.fShowAuras = value;
            }
        }

        [Category("Appearance")]
        [Description("Whether condition badges should be shown.")]
        public bool ShowConditions
        {
            get
            {
                return this.fShowConditions;
            }
            set
            {
                this.fShowConditions = value;
            }
        }

        [Category("Appearance")]
        [Description("Whether creatures should be shown with abbreviated labels.")]
        public bool ShowCreatureLabels
        {
            get
            {
                return this.fShowCreatureLabels;
            }
            set
            {
                this.fShowCreatureLabels = value;
            }
        }

        [Category("Appearance")]
        [Description("Determines how creatures should be displayed.")]
        public CreatureViewMode ShowCreatures
        {
            get
            {
                return this.fShowCreatures;
            }
            set
            {
                this.fShowCreatures = value;
            }
        }

        [Category("Appearance")]
        [Description("Determines how gridlines are shown.")]
        public MapGridMode ShowGrid
        {
            get
            {
                return this.fShowGrid;
            }
            set
            {
                this.fShowGrid = value;
            }
        }

        [Category("Appearance")]
        [Description("Determines whether grid rows and columns are labelled.")]
        public bool ShowGridLabels
        {
            get
            {
                return this.fShowGridLabels;
            }
            set
            {
                this.fShowGridLabels = value;
            }
        }

        [Category("Appearance")]
        [Description("Whether creatures should be shown with an HP bar.")]
        public bool ShowHealthBars
        {
            get
            {
                return this.fShowHealthBars;
            }
            set
            {
                this.fShowHealthBars = value;
            }
        }

        [Category("Appearance")]
        [Description("Determines whether token images are shown as tokens.")]
        public bool ShowPictureTokens
        {
            get
            {
                return this.fShowPictureTokens;
            }
            set
            {
                this.fShowPictureTokens = value;
            }
        }

        public List<MapSketch> Sketches
        {
            get
            {
                return this.fSketches;
            }
        }

        [Category("Behavior")]
        [Description("Determines whether map tokens can be moved around the map.")]
        public bool Tactical
        {
            get
            {
                return this.fTactical;
            }
            set
            {
                this.fTactical = value;
            }
        }

        [Category("Data")]
        [Description("The list of links between map tokens.")]
        public List<TokenLink> TokenLinks
        {
            get
            {
                return this.fTokenLinks;
            }
            set
            {
                this.fTokenLinks = value;
            }
        }

        [Category("Appearance")]
        [Description("The tile co-ordinates to view.")]
        public Rectangle Viewpoint
        {
            get
            {
                return this.fViewpoint;
            }
            set
            {
                this.fViewpoint = value;
                this.fLayoutData = null;
            }
        }

        private bool allow_creature_move(Rectangle target_rect, Point initial_location)
        {
            for (int i = 0; i != target_rect.Width; i++)
            {
                for (int j = 0; j != target_rect.Height; j++)
                {
                    Point point = new Point(i + target_rect.X, j + target_rect.Y);
                    if (this.fViewpoint != Rectangle.Empty && !this.fViewpoint.Contains(point))
                    {
                        return false;
                    }
                    if (this.fLayoutData.GetTileAtSquare(point) == null)
                    {
                        return false;
                    }
                    Pair<IToken, Rectangle> tokenAt = this.get_token_at(point);
                    if (tokenAt != null && tokenAt.Second.Location != initial_location)
                    {
                        CustomToken first = tokenAt.First as CustomToken;
                        if ((first == null ? true : first.Type != CustomTokenType.Overlay))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void draw_creature(Graphics g, Point pt, EncounterCard card, CombatData data, bool selected, bool hovered, bool ghost, OcclusionLevel occlusionLevel)
        {
            ICreature creature = Session.FindCreature(card.CreatureID, SearchType.Global);
            if (creature == null)
            {
                return;
            }
            Color black = Color.Black;
            if (creature is NPC)
            {
                black = Color.DarkBlue;
            }
            if (data != null)
            {
                switch (this.fEncounter.FindSlot(data).GetState(data))
                {
                    case CreatureState.Bloodied:
                        {
                            black = Color.Maroon;
                            break;
                        }
                    case CreatureState.Defeated:
                        {
                            black = Color.Gray;
                            break;
                        }
                }
            }
            if (occlusionLevel != OcclusionLevel.Visible)
            {
                black = occlusionLevel == OcclusionLevel.Cover ? this.TokenInCoverColor : this.TokenNotVisibleColor;
            }

            bool flag = false;
            foreach (IToken fBoxedToken in this.fBoxedTokens)
            {
                if (!(fBoxedToken is CreatureToken) || !((fBoxedToken as CreatureToken).Data.ID == data.ID))
                {
                    continue;
                }
                flag = true;
                break;
            }
            bool visible = data.Visible;
            if (!visible && this.fShowCreatures == CreatureViewMode.Visible)
            {
                return;
            }
            string str = "";
            str = (!this.fShowCreatureLabels ? creature.Category : data.DisplayName);
            string str1 = TextHelper.Abbreviation(str);
            ghost = (ghost ? true : !visible);
            //bool isVisible = true;

            this.draw_token(g, pt, creature.Size, creature.Image, black, str1, selected, flag, hovered, ghost, data.Conditions, data.Altitude);
            if (this.fShowHealthBars && data != null)
            {
                int size = Creature.GetSize(creature.Size);
                System.Drawing.Size size1 = new System.Drawing.Size(size, size);
                RectangleF region = this.fLayoutData.GetRegion(pt, size1);
                this.draw_health_bar(g, region, data, card.HP);
            }
        }

        private void draw_custom(Graphics g, Point pt, CustomToken ct, bool selected, bool hovered, bool ghost)
        {
            bool flag = this.fBoxedTokens.Contains(ct);
            string str = TextHelper.Abbreviation(ct.Name);
            bool visible = ct.Data.Visible;
            if (!visible && this.fShowCreatures == CreatureViewMode.Visible)
            {
                return;
            }
            switch (ct.Type)
            {
                case CustomTokenType.Token:
                    {
                        ghost = (ghost ? true : !visible);
                        List<OngoingCondition> ongoingConditions = new List<OngoingCondition>();
                        this.draw_token(g, pt, ct.TokenSize, ct.Image, ct.Colour, str, selected, flag, hovered, ghost, ongoingConditions, 0);
                        return;
                    }
                case CustomTokenType.Overlay:
                    {
                        RectangleF region = this.fLayoutData.GetRegion(pt, ct.OverlaySize);
                        RoundedRectangle.RectangleCorners rectangleCorner = RoundedRectangle.RectangleCorners.All;
                        int num = (flag ? 220 : 140);
                        if (ct.OverlayStyle == OverlayStyle.Block)
                        {
                            rectangleCorner = RoundedRectangle.RectangleCorners.None;
                            num = 255;
                        }
                        float squareSize = this.fLayoutData.SquareSize * 0.3f;
                        GraphicsPath graphicsPath = RoundedRectangle.Create(region, squareSize, rectangleCorner);
                        if (ct.Image != null)
                        {
                            if (ct.OverlayStyle != OverlayStyle.Rounded)
                            {
                                g.DrawImage(ct.Image, region);
                            }
                            else
                            {
                                ColorMatrix colorMatrix = new ColorMatrix()
                                {
                                    Matrix33 = 0.4f
                                };
                                ImageAttributes imageAttribute = new ImageAttributes();
                                imageAttribute.SetColorMatrix(colorMatrix);
                                Rectangle rectangle = new Rectangle((int)region.X, (int)region.Y, (int)region.Width, (int)region.Height);
                                g.SetClip(graphicsPath);
                                g.DrawImage(ct.Image, rectangle, 0, 0, ct.Image.Width, ct.Image.Height, GraphicsUnit.Pixel, imageAttribute);
                                g.ResetClip();
                            }
                            if ((selected ? true : hovered) && this.fShowCreatureLabels)
                            {
                                using (Pen pen = new Pen(Color.FromArgb(num, Color.White)))
                                {
                                    g.DrawPath(pen, graphicsPath);
                                }
                            }
                        }
                        else
                        {
                            using (Brush solidBrush = new SolidBrush(Color.FromArgb(num, ct.Colour)))
                            {
                                g.FillPath(solidBrush, graphicsPath);
                            }
                            if (this.fShowCreatureLabels)
                            {
                                g.DrawPath((selected || hovered ? Pens.White : new Pen(ct.Colour)), graphicsPath);
                            }
                        }
                        if (ct.DifficultTerrain)
                        {
                            for (int i = pt.X; i < pt.X + ct.OverlaySize.Width; i++)
                            {
                                for (int j = pt.Y; j < pt.Y + ct.OverlaySize.Height; j++)
                                {
                                    RectangleF rectangleF = this.fLayoutData.GetRegion(new Point(i, j), new System.Drawing.Size(1, 1));
                                    float width = rectangleF.Width / 10f;
                                    float single = rectangleF.Width / 5f;
                                    float x = rectangleF.X + rectangleF.Width - width;
                                    float y = rectangleF.Y + single + width;
                                    PointF pointF = new PointF(x - single / 2f, y - single);
                                    PointF pointF1 = new PointF(x - single, y);
                                    PointF pointF2 = new PointF(x, y);
                                    using (Brush brush = new SolidBrush(Color.FromArgb(180, Color.White)))
                                    {
                                        PointF[] pointFArray = new PointF[] { pointF, pointF1, pointF2 };
                                        g.FillPolygon(brush, pointFArray);
                                    }
                                    Pen darkGray = Pens.DarkGray;
                                    PointF[] pointFArray1 = new PointF[] { pointF, pointF1, pointF2 };
                                    g.DrawPolygon(darkGray, pointFArray1);
                                }
                            }
                        }
                        if (this.fSelectedTokens.Contains(ct) && this.fShowCreatureLabels)
                        {
                            StringFormat stringFormat = this.fCentred;
                            if (region.Height > region.Width)
                            {
                                stringFormat = new StringFormat(this.fCentred)
                                {
                                    FormatFlags = stringFormat.FormatFlags | StringFormatFlags.DirectionVertical
                                };
                            }
                            using (System.Drawing.Font font = new System.Drawing.Font(this.Font.FontFamily, this.fLayoutData.SquareSize / 5f))
                            {
                                SizeF sizeF = g.MeasureString(ct.Name, font, region.Size, stringFormat);
                                sizeF += new SizeF(4f, 4f);
                                RectangleF rectangleF1 = new RectangleF(region.X + (region.Width - sizeF.Width) / 2f, region.Y + (region.Height - sizeF.Height) / 2f, sizeF.Width, sizeF.Height);
                                g.DrawRectangle(Pens.Black, rectangleF1.X, rectangleF1.Y, rectangleF1.Width, rectangleF1.Height);
                                using (Brush solidBrush1 = new SolidBrush(Color.FromArgb(210, Color.White)))
                                {
                                    g.FillRectangle(solidBrush1, rectangleF1);
                                }
                                g.DrawString(ct.Name, font, Brushes.Black, rectangleF1, stringFormat);
                            }
                        }
                        return;
                    }
                default:
                    {
                        return;
                    }
            }
        }

        private void draw_grid_label(Graphics g, string str, System.Drawing.Font font, RectangleF rect, StringFormat sf)
        {
            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    RectangleF rectangleF = new RectangleF(rect.X + (float)i, rect.Y + (float)j, rect.Width, rect.Height);
                    using (Brush solidBrush = new SolidBrush(Color.FromArgb(50, Color.White)))
                    {
                        g.DrawString(str, font, solidBrush, rectangleF, sf);
                    }
                }
            }
            g.DrawString(str, font, Brushes.Black, rect, sf);
        }

        private void draw_health_bar(Graphics g, RectangleF rect, CombatData data, int hp_full)
        {
            int hpFull = hp_full + data.TempHP;
            int num = hp_full - data.Damage;
            Color green = Color.Green;
            if (num <= 0)
            {
                green = Color.Black;
            }
            if (num <= hp_full / 2)
            {
                green = Color.Maroon;
            }
            num = Math.Max(num, 0);
            float tempHP = (float)(num + data.TempHP) / (float)hpFull;
            float single = (float)num / (float)hpFull;
            float single1 = Math.Max(rect.Height / 12f, 4f);
            RectangleF rectangleF = new RectangleF(rect.X, rect.Bottom - single1, rect.Width, single1);
            g.FillRectangle(Brushes.White, rectangleF);
            if (data.TempHP > 0)
            {
                RectangleF rectangleF1 = new RectangleF(rectangleF.X, rectangleF.Y, rectangleF.Width * tempHP, rectangleF.Height);
                g.FillRectangle(Brushes.Blue, rectangleF1);
            }
            using (Brush solidBrush = new SolidBrush(green))
            {
                RectangleF rectangleF2 = new RectangleF(rectangleF.X, rectangleF.Y, rectangleF.Width * single, rectangleF.Height);
                g.FillRectangle(solidBrush, rectangleF2);
            }
            g.DrawRectangle(Pens.Black, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
        }

        private void draw_hero(Graphics g, Point pt, Hero hero, bool selected, bool hovered, bool ghost)
        {
            Color gray = Color.FromArgb(0, 80, 0);
            int hP = hero.HP;
            if (hP != 0)
            {
                int num = hP / 2;
                int tempHP = hP + hero.CombatData.TempHP - hero.CombatData.Damage;
                if (tempHP <= 0)
                {
                    gray = Color.Gray;
                }
                else if (tempHP <= hP / 2)
                {
                    gray = Color.Maroon;
                }
            }
            bool flag = this.fBoxedTokens.Contains(hero);
            string str = TextHelper.Abbreviation(hero.Name);
            bool flag1 = true;
            if (!flag1 && this.fShowCreatures == CreatureViewMode.Visible)
            {
                return;
            }
            ghost = (ghost ? true : !flag1);
            this.draw_token(g, pt, hero.Size, hero.Portrait, gray, str, selected, flag, hovered, ghost, hero.CombatData.Conditions, hero.CombatData.Altitude);
            if (this.fShowHealthBars && hero.CombatData != null && hero.HP != 0)
            {
                int size = Creature.GetSize(hero.Size);
                System.Drawing.Size size1 = new System.Drawing.Size(size, size);
                RectangleF region = this.fLayoutData.GetRegion(pt, size1);
                this.draw_health_bar(g, region, hero.CombatData, hero.HP);
            }
        }

        private void draw_sketch(Graphics g, MapSketch sketch)
        {
            using (Pen pen = new Pen(sketch.Colour, (float)sketch.Width))
            {
                for (int i = 1; i < sketch.Points.Count; i++)
                {
                    PointF _point = this.get_point(sketch.Points[i - 1]);
                    PointF pointF = this.get_point(sketch.Points[i]);
                    g.DrawLine(pen, _point, pointF);
                }
            }
        }

        private void draw_tile(Graphics g, Tile tile, int rotation, RectangleF rect, bool ghost)
        {
            try
            {
                //Image image = tile.Image ?? tile.BlankImage;
                ImageAttributes imageAttribute = new ImageAttributes();
                if (ghost)
                {
                    imageAttribute.SetColorMatrix(new ColorMatrix()
                    {
                        Matrix33 = 0.4f
                    });
                }
                Rectangle rectangle = new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
                CompositingMode compositingMode = g.CompositingMode;
                g.CompositingMode = CompositingMode.SourceCopy;

                InterpolationMode interpolationMode = g.InterpolationMode;
                g.InterpolationMode = InterpolationMode.NearestNeighbor;

                Bitmap bitmap = tile.BitmapResource;
                switch (rotation % 4)
                {
                    case 0:
                        {
                            g.DrawImage(bitmap, rectangle, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttribute);
                            break;
                        }
                    case 1:
                        {
                            bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            g.DrawImage(bitmap, rectangle, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttribute);
                            bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            break;
                        }
                    case 2:
                        {
                            bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            g.DrawImage(bitmap, rectangle, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttribute);
                            bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            break;
                        }
                    case 3:
                        {
                            bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            g.DrawImage(bitmap, rectangle, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttribute);
                            bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            break;
                        }
                }

                g.CompositingMode = compositingMode;
                g.InterpolationMode = interpolationMode;
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
            }
        }

        private void draw_token(Graphics g, Point pt, CreatureSize size, Image img, Color c, string text, bool selected, bool boxed, bool hovered, bool ghost, List<OngoingCondition> conditions, int altitude)
        {
            try
            {
                int num = Creature.GetSize(size);
                RectangleF region = this.fLayoutData.GetRegion(pt, new System.Drawing.Size(num, num));
                RectangleF rectangleF = region;
                if (boxed)
                {
                    g.FillRectangle(Brushes.Blue, rectangleF);
                }
                if (size == CreatureSize.Small || size == CreatureSize.Tiny)
                {
                    float width = rectangleF.Width / 7f;
                    rectangleF = new RectangleF(rectangleF.X + width, rectangleF.Y + width, rectangleF.Width - 2f * width, rectangleF.Height - 2f * width);
                }
                if (img != null)
                {
                    ImageAttributes imageAttribute = new ImageAttributes();
                    if (ghost)
                    {
                        imageAttribute.SetColorMatrix(new ColorMatrix()
                        {
                            Matrix33 = 0.4f
                        });
                    }
                    Rectangle rectangle = new Rectangle((int)rectangleF.X, (int)rectangleF.Y, (int)rectangleF.Width, (int)rectangleF.Height);
                    if (!this.fShowPictureTokens)
                    {
                        g.DrawImage(img, rectangle, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imageAttribute);
                        if (c == Color.Maroon)
                        {
                            Color color = Color.FromArgb(100, Color.Red);
                            using (Brush solidBrush = new SolidBrush(color))
                            {
                                g.FillRectangle(solidBrush, rectangle);
                            }
                        }
                    }
                    else
                    {
                        float single = Math.Min(rectangleF.Width, this.fLayoutData.SquareSize) * 0.5f;
                        GraphicsPath graphicsPath = RoundedRectangle.Create(rectangleF, single);
                        g.SetClip(graphicsPath);
                        g.DrawImage(img, rectangle, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imageAttribute);
                        g.ResetClip();
                        Color black = Color.Black;
                        float single1 = 2f;
                        if (selected || hovered)
                        {
                            black = Color.Red;
                            single1 = 1f;
                        }
                        using (Pen pen = new Pen(black, single1))
                        {
                            g.DrawPath(pen, graphicsPath);
                        }
                        if (c == Color.Maroon)
                        {
                            Color color1 = Color.FromArgb(100, Color.Red);
                            using (Brush brush = new SolidBrush(color1))
                            {
                                g.FillPath(brush, graphicsPath);
                            }
                        }
                    }
                }
                else
                {
                    Pen white = Pens.White;
                    if (selected || hovered)
                    {
                        white = Pens.Red;
                    }
                    float single2 = 2f;
                    RectangleF rectangleF1 = new RectangleF(rectangleF.X + single2, rectangleF.Y + single2, rectangleF.Width - 2f * single2, rectangleF.Height - 2f * single2);
                    using (Brush solidBrush1 = new SolidBrush(Color.FromArgb((ghost ? 140 : 255), c)))
                    {
                        g.FillEllipse(solidBrush1, rectangleF);
                    }
                    g.DrawEllipse(white, rectangleF1);
                    float squareSize = this.fLayoutData.SquareSize * (float)num / 6f;
                    if (squareSize > 0f)
                    {
                        using (System.Drawing.Font font = new System.Drawing.Font(this.Font.FontFamily, squareSize))
                        {
                            g.DrawString(text, font, Brushes.White, rectangleF, this.fCentred);
                        }
                    }
                }
                if (boxed)
                {
                    using (Pen pen1 = new Pen(Color.White, 2f))
                    {
                        g.DrawRectangle(pen1, region.X, region.Y, region.Width, region.Height);
                    }
                }
                if (this.fShowConditions && conditions.Count != 0)
                {
                    float squareSize1 = this.fLayoutData.SquareSize * 0.4f;
                    PointF pointF = new PointF(region.Right - squareSize1, region.Top);
                    RectangleF rectangleF2 = new RectangleF(pointF.X, pointF.Y, squareSize1, squareSize1);
                    using (Brush brush1 = new SolidBrush(Color.FromArgb(200, 0, 0)))
                    {
                        g.FillEllipse(brush1, rectangleF2);
                    }
                    using (System.Drawing.Font font1 = new System.Drawing.Font(this.Font.FontFamily, squareSize1 / 3f, this.Font.Style | FontStyle.Bold))
                    {
                        int count = conditions.Count;
                        g.DrawString(count.ToString(), font1, Brushes.White, rectangleF2, this.fCentred);
                    }
                    using (Pen pen2 = new Pen(Color.White, 2f))
                    {
                        g.DrawEllipse(pen2, rectangleF2);
                    }
                }
                if (altitude != 0)
                {
                    float squareSize2 = this.fLayoutData.SquareSize * 0.4f;
                    PointF pointF1 = new PointF(region.Left, region.Top);
                    RectangleF rectangleF3 = new RectangleF(pointF1.X, pointF1.Y, squareSize2, squareSize2);
                    string str = string.Concat((altitude > 0 ? "↑" : "↓"), Math.Abs(altitude));
                    using (Brush solidBrush2 = new SolidBrush(Color.FromArgb(80, 80, 80)))
                    {
                        g.FillEllipse(solidBrush2, rectangleF3);
                    }
                    using (System.Drawing.Font font2 = new System.Drawing.Font(this.Font.FontFamily, squareSize2 / 3f, this.Font.Style | FontStyle.Bold))
                    {
                        g.DrawString(str, font2, Brushes.White, rectangleF3, this.fCentred);
                    }
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
            }
        }

        private void draw_token_placeholder(Graphics g, Point start_location, Point new_location, CreatureSize size, bool has_picture)
        {
            int num = Creature.GetSize(size);
            RectangleF region = this.fLayoutData.GetRegion(start_location, new System.Drawing.Size(num, num));
            RectangleF rectangleF = this.fLayoutData.GetRegion(new_location, new System.Drawing.Size(num, num));
            if (size == CreatureSize.Small || size == CreatureSize.Tiny)
            {
                float width = region.Width / 7f;
                region = new RectangleF(region.X + width, region.Y + width, region.Width - 2f * width, region.Height - 2f * width);
            }
            int num1 = 2;
            RectangleF rectangleF1 = new RectangleF(region.X + (float)num1, region.Y + (float)num1, region.Width - (float)(2 * num1), region.Height - (float)(2 * num1));
            if (!has_picture)
            {
                using (Brush solidBrush = new SolidBrush(Color.FromArgb(180, Color.White)))
                {
                    g.FillEllipse(solidBrush, rectangleF1);
                    g.DrawEllipse(Pens.Red, rectangleF1);
                }
            }
            else
            {
                float single = Math.Min(region.Width, this.fLayoutData.SquareSize) * 0.5f;
                GraphicsPath graphicsPath = RoundedRectangle.Create(region, single);
                using (Brush brush = new SolidBrush(Color.FromArgb(180, Color.White)))
                {
                    g.FillPath(brush, graphicsPath);
                    g.DrawPath(Pens.Red, graphicsPath);
                }
            }
            using (System.Drawing.Font font = new System.Drawing.Font(this.Font.FontFamily, this.fLayoutData.SquareSize * (float)num / 4f))
            {
                int _distance = Utils.MMath.CalcDistance(start_location, new_location);
                g.DrawString(_distance.ToString(), font, Brushes.Red, rectangleF1, this.fCentred);
            }
            PointF pointF = new PointF(region.X + region.Width / 2f, region.Y + region.Height / 2f);
            PointF x = new PointF(rectangleF.X + rectangleF.Width / 2f, rectangleF.Y + rectangleF.Height / 2f);
            double width1 = (double)(rectangleF1.Width / 2f);
            if (new_location.X != start_location.X)
            {
                double num2 = Math.Atan((double)(new_location.Y - start_location.Y) / (double)(new_location.X - start_location.X));
                float single1 = (float)(width1 * Math.Cos(num2));
                float single2 = (float)(width1 * Math.Sin(num2));
                pointF.X = pointF.X + (new_location.X > start_location.X ? single1 : -single1);
                pointF.Y = pointF.Y + (new_location.X > start_location.X ? single2 : -single2);
                x.X = x.X + (new_location.X > start_location.X ? -single1 : single1);
                x.Y = x.Y + (new_location.X > start_location.X ? -single2 : single2);
            }
            else
            {
                pointF.Y = pointF.Y + (new_location.Y > start_location.Y ? (float)width1 : -(float)width1);
                x.Y = x.Y + (new_location.Y > start_location.Y ? -(float)width1 : (float)width1);
            }
            g.DrawLine(Pens.Red, pointF, x);
        }

        private TokenLink find_link(IToken t1, IToken t2)
        {
            TokenLink tokenLink;
            RectangleF tokenRect = this.get_token_rect(t1);
            RectangleF rectangleF = this.get_token_rect(t2);
            List<TokenLink>.Enumerator enumerator = this.fTokenLinks.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    TokenLink current = enumerator.Current;
                    RectangleF tokenRect1 = this.get_token_rect(current.Tokens[0]);
                    RectangleF rectangleF1 = this.get_token_rect(current.Tokens[1]);
                    bool flag = (tokenRect == tokenRect1 ? true : rectangleF == tokenRect1);
                    if (!flag || (tokenRect == rectangleF1 ? false : rectangleF != rectangleF1))
                    {
                        continue;
                    }
                    tokenLink = current;
                    return tokenLink;
                }
                return null;
            }
            finally
            {
                ((IDisposable)enumerator).Dispose();
            }
        }

        private PointF get_closest_vertex(Point pt)
        {
            Point squareAtPoint = this.fLayoutData.GetSquareAtPoint(pt);
            RectangleF region = this.fLayoutData.GetRegion(squareAtPoint, new System.Drawing.Size(1, 1));
            List<PointF> pointFs = new List<PointF>()
            {
                new PointF(region.Left, region.Top),
                new PointF(region.Left, region.Bottom - 1f),
                new PointF(region.Right - 1f, region.Top),
                new PointF(region.Right - 1f, region.Bottom - 1f)
            };
            double num = double.MaxValue;
            PointF empty = PointF.Empty;
            foreach (PointF pointF in pointFs)
            {
                float x = pointF.X - (float)pt.X;
                float y = pointF.Y - (float)pt.Y;
                double num1 = Math.Sqrt((double)(x * x + y * y));
                if (num1 >= num)
                {
                    continue;
                }
                empty = pointF;
                num = num1;
            }
            return empty;
        }

        private CombatData get_combat_data(IToken token)
        {
            if (token is CreatureToken)
            {
                return (token as CreatureToken).Data;
            }
            if (token is CustomToken)
            {
                return (token as CustomToken).Data;
            }
            if (!(token is Hero))
            {
                return null;
            }
            return (token as Hero).CombatData;
        }

        private Rectangle get_current_zoom_rect(bool use_scaling)
        {
            MapData mapDatum = (use_scaling ? new MapData(this, 1) : this.fLayoutData);
            Point squareAtPoint = mapDatum.GetSquareAtPoint(new Point(1, 1));
            Rectangle clientRectangle = base.ClientRectangle;
            Rectangle rectangle = base.ClientRectangle;
            Point point = mapDatum.GetSquareAtPoint(new Point(clientRectangle.Right - 1, rectangle.Bottom - 1));
            int x = 1 + (point.X - squareAtPoint.X);
            int y = 1 + (point.Y - squareAtPoint.Y);
            return new Rectangle(squareAtPoint, new System.Drawing.Size(x, y));
        }

        private PointF get_point(MapSketchPoint msp)
        {
            RectangleF region = this.fLayoutData.GetRegion(msp.Square, new System.Drawing.Size(1, 1));
            float width = region.Width * msp.Location.X;
            float height = region.Height * msp.Location.Y;
            return new PointF(region.X + width, region.Y + height);
        }

        private Pair<IToken, Rectangle> get_token_at(Point square)
        {
            Pair<IToken, Rectangle> pair;
            if (this.fEncounter == null)
            {
                return null;
            }
            foreach (Guid key in this.fSlotRegions.Keys)
            {
                foreach (Rectangle item in this.fSlotRegions[key])
                {
                    if (!item.Contains(square))
                    {
                        continue;
                    }
                    EncounterSlot encounterSlot = this.fEncounter.FindSlot(key);
                    CombatData combatDatum = encounterSlot.FindCombatData(item.Location);
                    CreatureToken creatureToken = new CreatureToken()
                    {
                        SlotID = key,
                        Data = combatDatum
                    };
                    pair = new Pair<IToken, Rectangle>(creatureToken, item);
                    return pair;
                }
            }
            List<Hero>.Enumerator enumerator = Session.Project.Heroes.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    Hero current = enumerator.Current;
                    if (current != null)
                    {
                        int size = Creature.GetSize(current.Size);
                        Rectangle rectangle = new Rectangle(current.CombatData.Location, new System.Drawing.Size(size, size));
                        if (!rectangle.Contains(square))
                        {
                            continue;
                        }
                        return new Pair<IToken, Rectangle>(current, rectangle);
                    }
                    else
                    {
                        return null;
                    }
                }
                //goto Label1;
            }
            finally
            {
                ((IDisposable)enumerator).Dispose();
            }
            //			return pair;
            foreach (CustomToken customToken in this.fEncounter.CustomTokens)
            {
                if (customToken.Type != CustomTokenType.Token)
                {
                    continue;
                }
                System.Drawing.Size overlaySize = customToken.OverlaySize;
                if (customToken.Type == CustomTokenType.Token)
                {
                    int num = Creature.GetSize(customToken.TokenSize);
                    overlaySize = new System.Drawing.Size(num, num);
                }
                Rectangle rectangle1 = new Rectangle(customToken.Data.Location, overlaySize);
                if (!rectangle1.Contains(square))
                {
                    continue;
                }
                pair = new Pair<IToken, Rectangle>(customToken, rectangle1);
                return pair;
            }
            foreach (CustomToken customToken1 in this.fEncounter.CustomTokens)
            {
                if (customToken1.Type != CustomTokenType.Overlay)
                {
                    continue;
                }
                System.Drawing.Size overlaySize1 = customToken1.OverlaySize;
                if (customToken1.Type == CustomTokenType.Token)
                {
                    int size1 = Creature.GetSize(customToken1.TokenSize);
                    overlaySize1 = new System.Drawing.Size(size1, size1);
                }
                Rectangle rectangle2 = new Rectangle(customToken1.Data.Location, overlaySize1);
                if (!rectangle2.Contains(square))
                {
                    continue;
                }
                pair = new Pair<IToken, Rectangle>(customToken1, rectangle2);
                return pair;
            }
            return null;
        }

        private Point get_token_location(IToken token)
        {
            if (token is CreatureToken)
            {
                return (token as CreatureToken).Data.Location;
            }
            if (token is Hero)
            {
                return (token as Hero).CombatData.Location;
            }
            if (!(token is CustomToken))
            {
                return CombatData.NoPoint;
            }
            return (token as CustomToken).Data.Location;
        }

        private RectangleF get_token_rect(IToken token)
        {
            Point tokenLocation = this.get_token_location(token);
            if (tokenLocation == CombatData.NoPoint)
            {
                return RectangleF.Empty;
            }
            System.Drawing.Size tokenSize = this.get_token_size(token);
            return this.fLayoutData.GetRegion(tokenLocation, tokenSize);
        }

        private System.Drawing.Size get_token_size(IToken token)
        {
            if (token is CreatureToken)
            {
                EncounterSlot encounterSlot = this.fEncounter.FindSlot((token as CreatureToken).SlotID);
                ICreature creature = Session.FindCreature(encounterSlot.Card.CreatureID, SearchType.Global);
                int size = Creature.GetSize(creature.Size);
                return new System.Drawing.Size(size, size);
            }
            if (token is Hero)
            {
                int num = Creature.GetSize((token as Hero).Size);
                return new System.Drawing.Size(num, num);
            }
            if (token is CustomToken)
            {
                CustomToken customToken = token as CustomToken;
                if (customToken.Type == CustomTokenType.Token)
                {
                    int size1 = Creature.GetSize(customToken.TokenSize);
                    return new System.Drawing.Size(size1, size1);
                }
                if (customToken.Type == CustomTokenType.Overlay)
                {
                    return customToken.OverlaySize;
                }
            }
            return new System.Drawing.Size(1, 1);
        }

        public bool HandleKey(Keys key)
        {
            if (key != Keys.Left && key != Keys.Right && key != (Keys.LButton | Keys.MButton | Keys.XButton1 | Keys.Space | Keys.Prior | Keys.PageUp | Keys.Home | Keys.Left | Keys.Shift) && key != (Keys.LButton | Keys.RButton | Keys.Cancel | Keys.MButton | Keys.XButton1 | Keys.XButton2 | Keys.Space | Keys.Prior | Keys.PageUp | Keys.Next | Keys.PageDown | Keys.End | Keys.Home | Keys.Left | Keys.Up | Keys.Right | Keys.Shift) && key != Keys.Up && key != Keys.Down && key != Keys.Delete)
            {
                return false;
            }
            return true;
        }

        private bool is_visible(CombatData cd)
        {
            if (cd == null)
            {
                return false;
            }
            switch (this.fShowCreatures)
            {
                case CreatureViewMode.All:
                    {
                        return true;
                    }
                case CreatureViewMode.Visible:
                    {
                        return cd.Visible;
                    }
                case CreatureViewMode.None:
                    {
                        return false;
                    }
            }
            return false;
        }

        protected override bool IsInputKey(Keys key)
        {
            if (this.HandleKey(key))
            {
                return true;
            }
            return base.IsInputKey(key);
        }

        public void MapChanged()
        {
            this.fLayoutData = null;
        }

        public void Nudge(KeyEventArgs e)
        {
            this.OnKeyDown(e);
        }

        protected void OnAreaActivated(MapArea area)
        {
            if (this.AreaActivated != null)
            {
                this.AreaActivated(this, new MapAreaEventArgs(area));
            }
        }

        protected void OnAreaSelected(MapArea area)
        {
            if (this.AreaSelected != null)
            {
                this.AreaSelected(this, new MapAreaEventArgs(area));
            }
        }

        protected void OnCancelledDrawing()
        {
            if (this.CancelledDrawing != null)
            {
                this.CancelledDrawing(this, new EventArgs());
            }
        }

        protected void OnCancelledLOS()
        {
            if (this.CancelledLOS != null)
            {
                this.CancelledLOS(this, new EventArgs());
            }
        }

        protected void OnCancelledScrolling()
        {
            if (this.CancelledScrolling != null)
            {
                this.CancelledScrolling(this, new EventArgs());
            }
        }

        protected TokenLink OnCreateTokenLink(List<IToken> tokens)
        {
            if (this.CreateTokenLink == null)
            {
                return null;
            }
            return this.CreateTokenLink(this, new TokenListEventArgs(tokens));
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            try
            {
                if (this.fLineOfSight)
                {
                    this.LineOfSight = false;
                }
                else if (this.fDrawing != null)
                {
                    this.AllowDrawing = false;
                }
                else if (!this.fAllowScrolling)
                {
                    if (this.fSelectedTokens.Count == 1)
                    {
                        this.OnTokenActivated(this.fSelectedTokens[0]);
                    }
                    if (this.fHighlightedArea != null)
                    {
                        this.OnAreaActivated(this.fHighlightedArea);
                    }
                    if (this.fHoverTokenLink != null)
                    {
                        int num = this.fTokenLinks.IndexOf(this.fHoverTokenLink);
                        TokenLink tokenLink = this.OnEditTokenLink(this.fHoverTokenLink);
                        if (tokenLink != null)
                        {
                            this.fTokenLinks[num] = tokenLink;
                        }
                    }
                    base.OnDoubleClick(e);
                }
                else
                {
                    this.AllowScrolling = false;
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
            }
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            if (e.Data.GetData(typeof(Tile)) is Tile)
            {
                TileData tileDatum = new TileData()
                {
                    TileID = this.fNewTile.Tile.ID,
                    Location = this.fNewTile.Location
                };
                this.fNewTile = null;
                this.fMap.Tiles.Add(tileDatum);
                this.fSelectedTiles = new List<TileData>()
                {
                    tileDatum
                };
                this.fLayoutData = null;

                // Fix this!  Changed OnItemDrop to pass the combatdata of the token it's moving, but that doesn't work with tiles
                //this.OnItemDropped();
            }
            Point newLocation = this.fNewToken.Location;
            CreatureToken data = e.Data.GetData(typeof(CreatureToken)) as CreatureToken;
            if (data != null)
            {
                //data.Data.Location = this.fNewToken.Location;
                this.fNewToken = null;
                this.OnItemDropped(data.Data, newLocation);
            }
            if (e.Data.GetData(typeof(Hero)) is Hero)
            {
                Hero token = this.fNewToken.Token as Hero;
                //token.CombatData.Location = this.fNewToken.Location;
                this.fNewToken = null;
                this.OnItemDropped(token.CombatData, newLocation);
            }
            CustomToken location = e.Data.GetData(typeof(CustomToken)) as CustomToken;
            if (location != null)
            {
                //location.Data.Location = this.fNewToken.Location;
                this.fNewToken = null;
                this.OnItemDropped(location.Data, newLocation);
            }
        }

        protected override void OnDragLeave(EventArgs e)
        {
            this.fNewTile = null;
            this.fNewToken = null;
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            Point client = base.PointToClient(System.Windows.Forms.Cursor.Position);
            Point squareAtPoint = this.fLayoutData.GetSquareAtPoint(client);
            Tile data = e.Data.GetData(typeof(Tile)) as Tile;
            if (data != null)
            {
                e.Effect = DragDropEffects.Copy;
                this.fNewTile = new MapView.NewTile()
                {
                    Tile = data,
                    Location = this.fLayoutData.GetSquareAtPoint(client),
                    Region = this.fLayoutData.GetRegion(this.fNewTile.Location, data.Size)
                };
            }
            CreatureToken creatureToken = e.Data.GetData(typeof(CreatureToken)) as CreatureToken;
            if (creatureToken != null)
            {
                EncounterSlot encounterSlot = this.fEncounter.FindSlot(creatureToken.SlotID);
                ICreature creature = Session.FindCreature(encounterSlot.Card.CreatureID, SearchType.Global);
                int size = Creature.GetSize(creature.Size);
                if (this.allow_creature_move(new Rectangle(squareAtPoint, new System.Drawing.Size(size, size)), CombatData.NoPoint))
                {
                    this.fNewToken = new MapView.NewToken()
                    {
                        Token = creatureToken,
                        Location = squareAtPoint
                    };
                    e.Effect = DragDropEffects.Move;
                }
            }
            Hero hero = e.Data.GetData(typeof(Hero)) as Hero;
            if (hero != null)
            {
                int num = Creature.GetSize(hero.Size);
                if (this.allow_creature_move(new Rectangle(squareAtPoint, new System.Drawing.Size(num, num)), CombatData.NoPoint))
                {
                    this.fNewToken = new MapView.NewToken()
                    {
                        Token = hero,
                        Location = squareAtPoint
                    };
                    e.Effect = DragDropEffects.Move;
                }
            }
            CustomToken customToken = e.Data.GetData(typeof(CustomToken)) as CustomToken;
            if (customToken != null)
            {
                this.fNewToken = new MapView.NewToken()
                {
                    Token = customToken,
                    Location = squareAtPoint
                };
                e.Effect = DragDropEffects.Move;
            }
        }

        protected TokenLink OnEditTokenLink(TokenLink link)
        {
            if (this.EditTokenLink == null)
            {
                return null;
            }
            return this.EditTokenLink(this, new TokenLinkEventArgs(link));
        }

        protected void OnHighlightedAreaChanged()
        {
            if (!this.fHighlightAreas)
            {
                return;
            }
            if (this.fHighlightedArea != null && this.fViewpoint == this.fHighlightedArea.Region)
            {
                return;
            }
            if (this.HighlightedAreaChanged != null)
            {
                this.HighlightedAreaChanged(this, new EventArgs());
            }
        }

        protected void OnHoverTokenChanged()
        {
            if (this.HoverTokenChanged != null)
            {
                this.HoverTokenChanged(this, new EventArgs());
            }

            this.Redraw();
        }

        protected void OnItemDropped(CombatData data, Point location)
        {
            if (this.ItemDropped != null)
            {
                this.ItemDropped(data, location);
            }
        }

        protected void OnItemMoved(DraggedToken moveResult)
        {
            if (this.ItemMoved != null)
            {
                this.ItemMoved(moveResult.Token, moveResult.Start, moveResult.Location);
            }
        }

        protected void OnItemRemoved()
        {
            if (this.ItemRemoved != null)
            {
                this.ItemRemoved(this, new EventArgs());
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            bool flag = false;
            bool flag1 = false;
            Keys keyCode = e.KeyCode;
            switch (keyCode)
            {
                case Keys.Left:
                    {
                        if (this.fSelectedTiles == null || this.fSelectedTiles.Count == 0)
                        {
                            break;
                        }
                        if (!e.Shift)
                        {
                            foreach (TileData fSelectedTile in this.fSelectedTiles)
                            {
                                Point location = fSelectedTile.Location;
                                Point point = fSelectedTile.Location;
                                fSelectedTile.Location = new Point(location.X - 1, point.Y);
                            }
                            flag1 = true;
                            break;
                        }
                        else
                        {
                            foreach (TileData tileDatum in this.fSelectedTiles)
                            {
                                TileData rotations = tileDatum;
                                rotations.Rotations = rotations.Rotations - 1;
                            }
                            flag1 = true;
                            break;
                        }
                    }
                case Keys.Up:
                    {
                        if (this.fSelectedTiles == null || this.fSelectedTiles.Count == 0)
                        {
                            break;
                        }
                        foreach (TileData fSelectedTile1 in this.fSelectedTiles)
                        {
                            int x = fSelectedTile1.Location.X;
                            Point location1 = fSelectedTile1.Location;
                            fSelectedTile1.Location = new Point(x, location1.Y - 1);
                        }
                        flag1 = true;
                        break;
                    }
                case Keys.Right:
                    {
                        if (this.fSelectedTiles == null || this.fSelectedTiles.Count == 0)
                        {
                            break;
                        }
                        if (!e.Shift)
                        {
                            foreach (TileData point1 in this.fSelectedTiles)
                            {
                                Point location2 = point1.Location;
                                Point point2 = point1.Location;
                                point1.Location = new Point(location2.X + 1, point2.Y);
                            }
                            flag1 = true;
                            break;
                        }
                        else
                        {
                            foreach (TileData tileDatum1 in this.fSelectedTiles)
                            {
                                TileData rotations1 = tileDatum1;
                                rotations1.Rotations = rotations1.Rotations + 1;
                            }
                            flag1 = true;
                            break;
                        }
                    }
                case Keys.Down:
                    {
                        if (this.fSelectedTiles == null || this.fSelectedTiles.Count == 0)
                        {
                            break;
                        }
                        foreach (TileData fSelectedTile2 in this.fSelectedTiles)
                        {
                            int num = fSelectedTile2.Location.X;
                            Point location3 = fSelectedTile2.Location;
                            fSelectedTile2.Location = new Point(num, location3.Y + 1);
                        }
                        flag1 = true;
                        break;
                    }
                default:
                    {
                        if (keyCode == Keys.Delete)
                        {
                            if (this.fSelectedTiles == null || this.fSelectedTiles.Count == 0)
                            {
                                break;
                            }
                            foreach (TileData tileDatum2 in this.fSelectedTiles)
                            {
                                this.fMap.Tiles.Remove(tileDatum2);
                            }
                            flag = true;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
            }
            this.fLayoutData = null;
            if (flag1)
            {
                // WTF?
               // this.OnItemMoved(1);
            }
            if (flag)
            {
                this.OnItemRemoved();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            try
            {
                base.Focus();
                if (this.fMap != null)
                {
                    if (this.fLayoutData == null)
                    {
                        this.fLayoutData = new MapData(this, this.fScalingFactor);
                    }
                    if (this.fDrawing == null)
                    {
                        Point client = base.PointToClient(System.Windows.Forms.Cursor.Position);
                        Point squareAtPoint = this.fLayoutData.GetSquareAtPoint(client);
                        if (!this.fAllowScrolling)
                        {
                            if (this.fTactical && this.fEncounter != null)
                            {
                                Pair<IToken, Rectangle> tokenAt = this.get_token_at(squareAtPoint);
                                if (tokenAt == null)
                                {
                                    if (this.fSelectedTokens.Count > 0)
                                    {
                                        this.fSelectedTokens.Clear();
                                        this.OnSelectedTokensChanged();
                                    }
                                }
                                else
                                {
                                    bool modifierKeys = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
                                    bool flag = (Control.ModifierKeys & Keys.Control) == Keys.Control;
                                    bool button = e.Button == System.Windows.Forms.MouseButtons.Right;
                                    CreatureToken first = tokenAt.First as CreatureToken;
                                    CustomToken customToken = tokenAt.First as CustomToken;
                                    if (first != null && !this.is_visible(first.Data))
                                    {
                                        if (!modifierKeys && !flag && !button)
                                        {
                                            this.fSelectedTokens.Clear();
                                        }
                                    }
                                    else if (customToken != null && !this.is_visible(customToken.Data))
                                    {
                                        if (!modifierKeys && !flag && !button)
                                        {
                                            this.fSelectedTokens.Clear();
                                        }
                                    }
                                    else if (customToken != null && customToken.CreatureID != Guid.Empty)
                                    {
                                        if (!modifierKeys && !flag && !button)
                                        {
                                            this.fSelectedTokens.Clear();
                                        }
                                    }
                                    else if (!modifierKeys && !flag)
                                    {
                                        this.fDraggedToken = new MapView.DraggedToken();
                                        this.fDraggedToken.Token = tokenAt.First;
                                        this.fDraggedToken.Start = tokenAt.Second.Location;
                                        this.fDraggedToken.Location = this.fDraggedToken.Start;

                                        MapView.DraggedToken size = this.fDraggedToken;
                                        int x = squareAtPoint.X - tokenAt.Second.Location.X;
                                        int y = squareAtPoint.Y;
                                        Point location = tokenAt.Second.Location;
                                        size.Offset = new System.Drawing.Size(x, y - location.Y);
                                        bool flag1 = false;
                                        CombatData combatData = this.get_combat_data(tokenAt.First);
                                        foreach (IToken fSelectedToken in this.fSelectedTokens)
                                        {
                                            CombatData combatDatum = this.get_combat_data(fSelectedToken);
                                            if (combatData.ID != combatDatum.ID)
                                            {
                                                continue;
                                            }
                                            flag1 = true;
                                            break;
                                        }
                                        if (!flag1)
                                        {
                                            this.fSelectedTokens.Clear();
                                            this.fSelectedTokens.Add(tokenAt.First);
                                            this.OnSelectedTokensChanged();
                                        }
                                    }
                                    else if (e.Button == System.Windows.Forms.MouseButtons.Left)
                                    {
                                        bool flag2 = false;
                                        foreach (IToken token in this.fSelectedTokens)
                                        {
                                            if (this.get_token_location(token) != this.get_token_location(tokenAt.First))
                                            {
                                                continue;
                                            }
                                            flag2 = true;
                                            this.fSelectedTokens.Remove(token);
                                            break;
                                        }
                                        if (!flag2)
                                        {
                                            this.fSelectedTokens.Add(tokenAt.First);
                                        }
                                        this.OnSelectedTokensChanged();
                                    }
                                }
                            }
                            if (this.fMode == MapViewMode.Normal)
                            {
                                if (this.fSelectedTiles == null)
                                {
                                    this.fSelectedTiles = new List<TileData>();
                                }
                                if ((Control.ModifierKeys == Keys.Control ? false : Control.ModifierKeys != Keys.Shift))
                                {
                                    this.fSelectedTiles.Clear();
                                }
                                TileData tileAtSquare = this.fLayoutData.GetTileAtSquare(squareAtPoint);
                                if (tileAtSquare != null && this.fMap.Tiles.Contains(tileAtSquare))
                                {
                                    this.fSelectedTiles.Add(tileAtSquare);
                                }
                                System.Windows.Forms.MouseButtons mouseButton = e.Button;
                                if (mouseButton != System.Windows.Forms.MouseButtons.Left)
                                {
                                    if (mouseButton == System.Windows.Forms.MouseButtons.Right)
                                    {
                                        this.fCurrentOutline = Rectangle.Empty;
                                        this.fDraggedOutline = new MapView.DraggedOutline()
                                        {
                                            Start = client,
                                            Region = new Rectangle(squareAtPoint, new System.Drawing.Size(1, 1))
                                        };
                                    }
                                }
                                else if (this.fSelectedTiles.Count != 0)
                                {
                                    this.fDraggedTiles = new MapView.DraggedTiles()
                                    {
                                        Tiles = this.fSelectedTiles,
                                        Start = client
                                    };
                                }
                            }
                        }
                        else
                        {
                            if (this.fViewpoint == Rectangle.Empty)
                            {
                                this.fViewpoint = this.get_current_zoom_rect(true);
                                this.fLayoutData = null;
                            }
                            this.fScrollingData = new MapView.ScrollingData()
                            {
                                Start = squareAtPoint
                            };
                        }
                    }
                    else if (this.fDrawing.CurrentSketch == null)
                    {
                        this.fDrawing.CurrentSketch = new MapSketch();
                    }
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            try
            {
                if (this.fDrawing != null)
                {
                    if (this.fDrawing.CurrentSketch != null)
                    {
                        this.fSketches.Add(this.fDrawing.CurrentSketch);
                        this.OnSketchCreated(this.fDrawing.CurrentSketch);
                    }
                    this.fDrawing.CurrentSketch = null;
                }
                else if (!this.fAllowScrolling)
                {
                    if (this.fTactical)
                    {
                        if (this.fDraggedToken != null)
                        {
                            this.fDraggedToken = null;
                            this.OnTokenDragged();
                        }
                    }
                    if (this.fMode == MapViewMode.Normal)
                    {
                        this.fHoverTile = null;
                        this.fHoverToken = null;
                        this.fHoverTokenLink = null;
                        if (this.fSelectedTokens.Count != 0)
                        {
                            this.fSelectedTokens.Clear();
                            this.OnSelectedTokensChanged();
                        }
                        if (this.fHighlightedArea != null)
                        {
                            this.fHighlightedArea = null;
                            this.OnHighlightedAreaChanged();
                        }
                        this.fDraggedTiles = null;
                        this.fDraggedOutline = null;
                    }
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            try
            {
                if (this.fMap != null)
                {
                    if (this.fLayoutData == null)
                    {
                        this.fLayoutData = new MapData(this, this.fScalingFactor);
                    }
                    Point client = base.PointToClient(System.Windows.Forms.Cursor.Position);
                    Point squareAtPoint = this.fLayoutData.GetSquareAtPoint(client);
                    if (this.fDrawing != null)
                    {
                        if (this.fDrawing.CurrentSketch != null)
                        {
                            RectangleF region = this.fLayoutData.GetRegion(squareAtPoint, new System.Drawing.Size(1, 1));
                            float x = ((float)client.X - region.X) / region.Width;
                            float y = ((float)client.Y - region.Y) / region.Height;
                            MapSketchPoint mapSketchPoint = new MapSketchPoint()
                            {
                                Square = squareAtPoint,
                                Location = new PointF(x, y)
                            };
                            Console.WriteLine(this.get_point(mapSketchPoint));
                            this.fDrawing.CurrentSketch.Points.Add(mapSketchPoint);
                        }
                    }
                    else if (!this.fAllowScrolling)
                    {
                        if (this.fTactical)
                        {
                            bool flag = false;
                            if (this.fDraggedToken == null && (Control.ModifierKeys & Keys.Control) != Keys.Control)
                            {
                                this.fHoverTokenLink = null;
                                foreach (TokenLink key in this.fTokenLinkRegions.Keys)
                                {
                                    if (!this.fTokenLinkRegions[key].Contains(client))
                                    {
                                        continue;
                                    }
                                    this.fHoverTokenLink = key;
                                }
                                Pair<IToken, Rectangle> tokenAt = this.get_token_at(squareAtPoint);
                                if (tokenAt != null)
                                {
                                    CreatureToken first = tokenAt.First as CreatureToken;
                                    CustomToken customToken = tokenAt.First as CustomToken;
                                    if ((first == null || this.is_visible(first.Data)) && (customToken == null || this.is_visible(customToken.Data)))
                                    {
                                        if (this.fHoverToken != null)
                                        {
                                            if (tokenAt.First is CreatureToken)
                                            {
                                                CreatureToken creatureToken = this.fHoverToken as CreatureToken;
                                                CreatureToken first1 = tokenAt.First as CreatureToken;
                                                flag = (creatureToken == null ? true : creatureToken.Data.ID != first1.Data.ID);
                                            }
                                            if (tokenAt.First is CustomToken)
                                            {
                                                CustomToken customToken1 = this.fHoverToken as CustomToken;
                                                CustomToken first2 = tokenAt.First as CustomToken;
                                                flag = (customToken1 == null ? true : customToken1.Data.ID != first2.Data.ID);
                                            }
                                            if (tokenAt.First is Hero)
                                            {
                                                Hero hero = this.fHoverToken as Hero;
                                                Hero hero1 = tokenAt.First as Hero;
                                                flag = (hero == null ? true : hero.ID != hero1.ID);
                                            }
                                        }
                                        else
                                        {
                                            flag = true;
                                        }
                                        this.fHoverToken = tokenAt.First;
                                    }
                                }
                                else if (this.fHoverToken != null)
                                {
                                    flag = true;
                                    this.fHoverToken = null;
                                }
                            }
                            if (this.fDraggedToken != null)
                            {
                                this.fDraggedToken.LinkedToken = null;
                                Point offset = squareAtPoint - this.fDraggedToken.Offset;
                                System.Drawing.Size tokenSize = this.get_token_size(this.fDraggedToken.Token);
                                Rectangle rectangle = new Rectangle(offset, tokenSize);
                                CustomToken token = this.fDraggedToken.Token as CustomToken;
                                if ((token == null ? false : token.Type == CustomTokenType.Overlay) || this.allow_creature_move(rectangle, this.fDraggedToken.Start))
                                {
                                    if (offset != this.fDraggedToken.Location)
                                    {
                                        this.fDraggedToken.Location = offset;
                                        this.OnTokenDragged();
                                    }
                                }
                                else if (this.fAllowLinkCreation)
                                {
                                    Pair<IToken, Rectangle> pair = this.get_token_at(squareAtPoint);
                                    if (pair != null)
                                    {
                                        this.fDraggedToken.Location = this.fDraggedToken.Start;
                                        this.fDraggedToken.LinkedToken = pair.First;
                                        this.OnTokenDragged();
                                    }
                                }
                            }
                            if (flag)
                            {
                                this.OnHoverTokenChanged();
                            }
                        }
                        MapArea mapArea = null;
                        foreach (MapArea area in this.fMap.Areas)
                        {
                            if (!area.Region.Contains(squareAtPoint))
                            {
                                continue;
                            }
                            mapArea = area;
                        }
                        if (this.fHighlightedArea != mapArea)
                        {
                            this.fHighlightedArea = mapArea;
                            this.OnHighlightedAreaChanged();
                        }
                        if (this.fMode == MapViewMode.Normal)
                        {
                            if (this.fDraggedTiles != null)
                            {
                                foreach (TileData tile in this.fDraggedTiles.Tiles)
                                {
                                    Tile item = this.fLayoutData.Tiles[tile];
                                    int num = (int)((float)(client.X - this.fDraggedTiles.Start.X) / this.fLayoutData.SquareSize);
                                    int y1 = (int)((float)(client.Y - this.fDraggedTiles.Start.Y) / this.fLayoutData.SquareSize);
                                    this.fDraggedTiles.Offset = new System.Drawing.Size(num, y1);
                                    Point location = tile.Location;
                                    Point point = tile.Location;
                                    Point point1 = new Point(location.X + num, point.Y + y1);
                                    System.Drawing.Size size = item.Size;
                                    if (tile.Rotations % 2 != 0)
                                    {
                                        size = new System.Drawing.Size(item.Size.Height, item.Size.Width);
                                    }
                                    this.fDraggedTiles.Region = this.fLayoutData.GetRegion(point1, size);
                                }
                            }
                            else if (this.fDraggedOutline == null)
                            {
                                this.fHoverTile = this.fLayoutData.GetTileAtSquare(squareAtPoint);
                            }
                            else
                            {
                                Point squareAtPoint1 = this.fLayoutData.GetSquareAtPoint(this.fDraggedOutline.Start);
                                Point squareAtPoint2 = this.fLayoutData.GetSquareAtPoint(client);
                                int num1 = Math.Min(squareAtPoint2.X, squareAtPoint1.X);
                                int num2 = Math.Min(squareAtPoint2.Y, squareAtPoint1.Y);
                                int num3 = Math.Abs(squareAtPoint2.X - squareAtPoint1.X) + 1;
                                int num4 = Math.Abs(squareAtPoint2.Y - squareAtPoint1.Y) + 1;
                                this.fDraggedOutline.Region = new Rectangle(num1, num2, num3, num4);
                            }
                        }
                    }
                    else if (this.fScrollingData != null && this.fViewpoint != Rectangle.Empty && this.fScrollingData.Start != squareAtPoint)
                    {
                        int x1 = this.fScrollingData.Start.X - squareAtPoint.X;
                        int y2 = this.fScrollingData.Start.Y - squareAtPoint.Y;
                        this.fViewpoint.X = this.fViewpoint.X + x1;
                        this.fViewpoint.Y = this.fViewpoint.Y + y2;
                        this.fLayoutData = null;
                        this.Redraw();
                    }
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            try
            {
                if (this.fMap != null)
                {
                    if (this.fLayoutData == null)
                    {
                        this.fLayoutData = new MapData(this, this.fScalingFactor);
                    }
                    if (this.fDrawing == null)
                    {
                        Point client = base.PointToClient(System.Windows.Forms.Cursor.Position);
                        if (this.fScrollingData == null)
                        {
                            if (this.fTactical)
                            {
                                if (this.fDraggedToken != null)
                                {
                                    if (this.fDraggedToken.Location != this.fDraggedToken.Start)
                                    {
                                        //int _distance = MMath.CalcDistance(this.fDraggedToken.Location, this.fDraggedToken.Start);

                                        // Need to set fDraggedToken to null for....reasons.  OnPaint can get called and then the locations don't match between fDraggedToken and the actual token
                                        DraggedToken dragResult = this.fDraggedToken;
                                        this.fDraggedToken = null;
                                        this.OnItemMoved(dragResult);
                                    }
                                    else if (this.fDraggedToken.LinkedToken != null)
                                    {
                                        TokenLink tokenLink = this.find_link(this.fDraggedToken.Token, this.fDraggedToken.LinkedToken);
                                        if (tokenLink != null)
                                        {
                                            //this.fTokenLinks.Remove(tokenLink);
                                            CommandManager.GetInstance().ExecuteCommand(new AddRemoveLinkCommand(AddRemoveLinkCommand.AddRemoveOption.Remove, this.fTokenLinks, tokenLink));
                                        }
                                        else if (this.fDraggedToken.Token != this.fDraggedToken.LinkedToken)
                                        {
                                            List<IToken> tokens = new List<IToken>()
                                        {
                                            this.fDraggedToken.Token,
                                            this.fDraggedToken.LinkedToken
                                        };
                                            TokenLink tokenLink1 = this.OnCreateTokenLink(tokens);
                                            if (tokenLink1 != null)
                                            {
                                                CommandManager.GetInstance().ExecuteCommand(new AddRemoveLinkCommand(AddRemoveLinkCommand.AddRemoveOption.Add, this.fTokenLinks, tokenLink1));
                                            }
                                        }
                                        this.fDraggedToken = null;
                                    }

                                    this.fDraggedToken = null;
                                    this.OnTokenDragged();
                                }
                            }
                            Point squareAtPoint = this.fLayoutData.GetSquareAtPoint(client);
                            MapArea mapArea = null;
                            foreach (MapArea area in this.fMap.Areas)
                            {
                                if (!area.Region.Contains(squareAtPoint))
                                {
                                    continue;
                                }
                                mapArea = area;
                            }
                            if (this.fSelectedArea != mapArea)
                            {
                                this.fSelectedArea = mapArea;
                                this.OnAreaSelected(this.fSelectedArea);
                            }
                            if (this.fMode == MapViewMode.Normal)
                            {
                                if (this.fDraggedTiles != null)
                                {
                                    if (client != this.fDraggedTiles.Start)
                                    {
                                        int num = Utils.MMath.CalcDistance(client, this.fDraggedTiles.Start);
                                        foreach (TileData tile in this.fDraggedTiles.Tiles)
                                        {
                                            Point point = tile.Location;
                                            int x = point.X + this.fDraggedTiles.Offset.Width;
                                            Point location1 = tile.Location;
                                            int y = location1.Y + this.fDraggedTiles.Offset.Height;
                                            tile.Location = new Point(x, y);
                                            this.fMap.Tiles.Remove(tile);
                                            this.fMap.Tiles.Add(tile);
                                        }

                                        // FIX THIS
                                        //this.OnItemMoved(num);
                                    }
                                    this.fDraggedTiles = null;
                                    this.fLayoutData = null;
                                }
                                else if (this.fDraggedOutline != null)
                                {
                                    if (client == this.fDraggedOutline.Start)
                                    {
                                        Point squareAtPoint1 = this.fLayoutData.GetSquareAtPoint(client);
                                        TileData tileAtSquare = this.fLayoutData.GetTileAtSquare(squareAtPoint1);
                                        if (tileAtSquare != null)
                                        {
                                            this.fSelectedTiles = new List<TileData>()
                                            {
                                                tileAtSquare
                                            };
                                            this.OnTileContext(tileAtSquare);
                                        }
                                    }
                                    else
                                    {
                                        this.fCurrentOutline = this.fDraggedOutline.Region;
                                        this.OnRegionSelected();
                                    }
                                    this.fDraggedOutline = null;
                                }
                            }
                        }
                        else
                        {
                            if (this.fViewpoint != Rectangle.Empty)
                            {
                                this.fViewpoint = this.get_current_zoom_rect(true);
                                this.fLayoutData = null;
                            }
                            this.fScrollingData = null;
                            this.Redraw();
                        }
                    }
                    else
                    {
                        if (this.fDrawing.CurrentSketch != null)
                        {
                            this.fSketches.Add(this.fDrawing.CurrentSketch);
                            this.OnSketchCreated(this.fDrawing.CurrentSketch);
                        }
                        this.fDrawing.CurrentSketch = null;
                    }
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            this.fAllowScrolling = true;
            this.OnMouseZoom(e);
        }

        protected void OnMouseZoom(MouseEventArgs args)
        {
            if (this.MouseZoomed != null)
            {
                this.MouseZoomed(this, args);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (backbufferGraphics != null)
            {
                backbufferGraphics.Render(e.Graphics);
            }
        }

        private void AddAllEnemiesToOcclusion(bool isHero)
        {
            VisibilitySystem.GetInstance().Blockers.Clear();
            VisibilitySystem.GetInstance().AddMapBlockers();

            if (isHero)
            {
                // Enemies provide cover
                foreach (EncounterSlot allSlot in this.fEncounter.AllSlots)
                {
                    //int size = Creature.GetSize(creature.Size);
                    foreach (CombatData combatDatum in allSlot.CombatData)
                    {
                        var blocker = new Data.Combat.RectangleVisibilityBlocker(new RectangleF(combatDatum.Location.X, combatDatum.Location.Y, 1.0f, 1.0f), OcclusionLevel.Cover);
                        blocker.Type = Data.Combat.VisibilityBlocker.BlockerType.BlocksLookingThrough;
                        VisibilitySystem.GetInstance().Blockers.Add(blocker);
                    }
                }
            }
            else
            {
                // Heroes provide cover
                foreach (Hero hero in Session.Project.Heroes)
                {
                    if (hero.CombatData.Location == CombatData.NoPoint)
                    {
                        continue;
                    }

                    CombatData data = hero.CombatData;
                    var blocker = new Data.Combat.RectangleVisibilityBlocker(new RectangleF(data.Location.X, data.Location.Y, 1.0f, 1.0f), OcclusionLevel.Cover);
                    blocker.Type = Data.Combat.VisibilityBlocker.BlockerType.BlocksLookingThrough;
                    VisibilitySystem.GetInstance().Blockers.Add(blocker);
                }
            }
        }

        // Visibility Info
        CombatData latestTurnVisData = null;
        VisibilityMap latestTurnVisMap = new VisibilityMap(OcclusionLevel.Visible);

        public void SetNewVisibilitySource(CombatData data, bool isHero)
        {
            latestTurnVisData = data;
            RecalculateVisibility();
        }

        public void RecalculateVisibility()
        { 
            if (latestTurnVisData == null)
            {
                // we haven't set any vis source yet, so nothing we can do.
                return;
            }

            if (latestTurnVisData.Location == CombatData.NoPoint)
            {
                // We have a vis source but that person isn't on the map so they can't see anything
                latestTurnVisMap = new VisibilityMap(OcclusionLevel.Obscured);
                return;
            }

            AddAllEnemiesToOcclusion(this.fMode == MapViewMode.PlayerView);
            //Point min = new Point(this.LayoutData.MinX, this.LayoutData.MinY);
            //Point max = new Point(this.LayoutData.MaxX, this.LayoutData.MaxY);
            Point min = new Point(0, 0);
            Point max = new Point(this.LayoutData.OverallMaxX, this.LayoutData.OverallMaxY);
            VisibilitySystem.GetInstance().SetSize(min, max);
            VisibilitySystem.GetInstance().RecalculateFromPosition(latestTurnVisData.Location);
            latestTurnVisMap = VisibilitySystem.GetInstance().VisibilityMap;
        }

        public void DrawVisibility()
        {
            if (this.latestTurnVisMap == null || this.latestTurnVisMap.Width == 0)
            {
                return;
            }

            for (int x = this.LayoutData.MinX; x < this.LayoutData.MaxX; ++x)
            {
                for (int y = this.LayoutData.MinY; y < this.LayoutData.MaxY; ++y)
                {
                    OcclusionLevel vis = this.latestTurnVisMap[x, y];
                    if (vis != OcclusionLevel.Visible)
                    {
                        Color color = vis == OcclusionLevel.Obscured ? this.SquareObscurredColor : this.SquareInCoverColor;
                        Point point = new Point(x, y);
                        RectangleF region = this.fLayoutData.GetRegion(point, new Size(1, 1));

                        // This gives us the full size but don't draw the full size because then it overlaps
                        using (Brush solidBrush = new SolidBrush(color))
                        {
                            drawingGraphics.FillRectangle(solidBrush, region);
                        }
                    }
                }
            }
        }

        public bool RebuildTerrainLayer()
        {
            Graphics drawingGraphics = Graphics.FromImage(this.terrainBitmap);
            Rectangle clientRectangle;

            if (this.fLayoutData == null)
            {
                this.fLayoutData = new MapData(this, this.fScalingFactor);
            }
            drawingGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            drawingGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            drawingGraphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            if (this.fMap == null)
            {
                Brush windowText = SystemBrushes.WindowText;
                if (this.fMode == MapViewMode.Normal)
                {
                    windowText = Brushes.White;
                }
                drawingGraphics.DrawString("(no map selected)", this.Font, windowText, base.ClientRectangle, this.fCentred);
                return false;
            }

            if (this.fShowGrid == MapGridMode.Behind && this.fLayoutData.SquareSize >= 10f)
            {
                using (Pen pen = new Pen(Color.FromArgb(100, 140, 190)))
                {
                    using (Pen pen1 = new Pen(Color.FromArgb(150, 200, 230)))
                    {
                        float squareSize = 0f;
                        float width = this.fLayoutData.MapOffset.Width % this.fLayoutData.SquareSize;
                        int num = 0;
                        while (squareSize <= (float)base.ClientRectangle.Width)
                        {
                            if (num % 4 != 0)
                            {
                                clientRectangle = base.ClientRectangle;
                                drawingGraphics.DrawLine(pen, squareSize + width, 0f, squareSize + width, (float)clientRectangle.Height);
                            }
                            else
                            {
                                clientRectangle = base.ClientRectangle;
                                drawingGraphics.DrawLine(pen1, squareSize + width, 0f, squareSize + width, (float)clientRectangle.Height);
                            }
                            squareSize += this.fLayoutData.SquareSize;
                            num++;
                        }
                        float single = 0f;
                        float height = this.fLayoutData.MapOffset.Height % this.fLayoutData.SquareSize;
                        int num1 = 0;
                        while (single <= (float)base.ClientRectangle.Height)
                        {
                            if (num1 % 4 != 0)
                            {
                                clientRectangle = base.ClientRectangle;
                                drawingGraphics.DrawLine(pen, 0f, single + height, (float)clientRectangle.Width, single + height);
                            }
                            else
                            {
                                clientRectangle = base.ClientRectangle;
                                drawingGraphics.DrawLine(pen1, 0f, single + height, (float)clientRectangle.Width, single + height);
                            }
                            single += this.fLayoutData.SquareSize;
                            num1++;
                        }
                    }
                }
            }
            if (this.fHighlightAreas)
            {
                foreach (MapArea area in this.fMap.Areas)
                {
                    MapData mapDatum = this.fLayoutData;
                    Point point = area.Region.Location;
                    clientRectangle = area.Region;
                    RectangleF region = mapDatum.GetRegion(point, clientRectangle.Size);
                    Brush lightBlue = null;
                    if (area != this.fSelectedArea)
                    {
                        Color color2 = Color.FromArgb(255, 255, 255);
                        Color color3 = Color.FromArgb(210, 210, 210);
                        lightBlue = new LinearGradientBrush(base.ClientRectangle, color2, color3, LinearGradientMode.Vertical);
                    }
                    else
                    {
                        lightBlue = Brushes.LightBlue;
                    }
                    if (this.fPlot != null && this.fPlot.FindPointForMapArea(this.fMap, area) == null)
                    {
                        lightBlue = null;
                    }
                    if (lightBlue == null)
                    {
                        continue;
                    }
                    drawingGraphics.FillRectangle(lightBlue, region);
                }
            }
            if (this.fCurrentOutline != Rectangle.Empty)
            {
                RectangleF rectangleF = this.fLayoutData.GetRegion(this.fCurrentOutline.Location, this.fCurrentOutline.Size);
                drawingGraphics.FillRectangle(Brushes.LightBlue, rectangleF);
            }
            if (this.fBackgroundMap != null)
            {
                foreach (TileData tile in this.fBackgroundMap.Tiles)
                {
                    if (!this.fLayoutData.Tiles.ContainsKey(tile))
                    {
                        continue;
                    }
                    Tile item = this.fLayoutData.Tiles[tile];
                    RectangleF item1 = this.fLayoutData.TileRegions[tile];
                    this.draw_tile(drawingGraphics, item, tile.Rotations, item1, true);
                }
            }
            foreach (TileData tileDatum in this.fMap.Tiles)
            {
                if (this.fDraggedTiles != null && this.fDraggedTiles.Tiles.Contains(tileDatum) || !this.fLayoutData.Tiles.ContainsKey(tileDatum))
                {
                    continue;
                }
                Tile tile1 = this.fLayoutData.Tiles[tileDatum];
                RectangleF rectangleF1 = this.fLayoutData.TileRegions[tileDatum];
                this.draw_tile(drawingGraphics, tile1, tileDatum.Rotations, rectangleF1, false);
                if (this.fSelectedTiles == null || !this.fSelectedTiles.Contains(tileDatum))
                {
                    if (tileDatum != this.fHoverTile)
                    {
                        continue;
                    }
                    drawingGraphics.DrawRectangle(Pens.DarkBlue, rectangleF1.X, rectangleF1.Y, rectangleF1.Width, rectangleF1.Height);
                }
                else
                {
                    drawingGraphics.DrawRectangle(Pens.Blue, rectangleF1.X, rectangleF1.Y, rectangleF1.Width, rectangleF1.Height);
                }
            }
            if (this.fNewTile != null)
            {
                this.draw_tile(drawingGraphics, this.fNewTile.Tile, 0, this.fNewTile.Region, false);
            }
            if (this.fDraggedTiles != null)
            {
                foreach (TileData tileDatum1 in this.fDraggedTiles.Tiles)
                {
                    Tile item2 = this.fLayoutData.Tiles[tileDatum1];
                    this.draw_tile(drawingGraphics, item2, tileDatum1.Rotations, this.fDraggedTiles.Region, false);
                }
            }

            return true;
        }

        public void DrawTerrain(Graphics drawingGraphics)
        {
            if (this.terrainBitmap != null)
            {
                drawingGraphics.DrawImage(this.terrainBitmap, new Point(0, 0));
            }
        }

        public void Redraw()
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            Rectangle clientRectangle;
            Point location;
            if (this.fLayoutData == null)
            {
                this.fLayoutData = new MapData(this, this.fScalingFactor);
            }
            drawingGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            drawingGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            drawingGraphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            switch (this.fMode)
            {
                case MapViewMode.Normal:
                    {
                        using (Brush solidBrush = new SolidBrush(Color.FromArgb(70, 100, 170)))
                        {
                            drawingGraphics.FillRectangle(solidBrush, base.ClientRectangle);
                            break;
                        }
                    }
                case MapViewMode.Plain:
                    {
                        drawingGraphics.FillRectangle(Brushes.White, base.ClientRectangle);
                        break;
                    }
                case MapViewMode.Thumbnail:
                    {
                        Color color = Color.FromArgb(240, 240, 240);
                        Color color1 = Color.FromArgb(170, 170, 170);
                        using (Brush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, color, color1, LinearGradientMode.Vertical))
                        {
                            drawingGraphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
                            break;
                        }
                    }
                case MapViewMode.PlayerView:
                    {
                        drawingGraphics.FillRectangle(Brushes.Black, base.ClientRectangle);
                        break;
                    }
            }

            if (this.fEncounter != null)
            {
                this.fSlotRegions.Clear();
                foreach (EncounterSlot allSlot in this.fEncounter.AllSlots)
                {
                    this.fSlotRegions[allSlot.ID] = new List<Rectangle>();
                    ICreature creature = Session.FindCreature(allSlot.Card.CreatureID, SearchType.Global);
                    if (creature == null)
                    {
                        continue;
                    }
                    int size = Creature.GetSize(creature.Size);
                    foreach (CombatData combatDatum in allSlot.CombatData)
                    {
                        this.fSlotRegions[allSlot.ID].Add(new Rectangle(combatDatum.Location, new System.Drawing.Size(size, size)));
                    }
                }
            }


            DrawTerrain(drawingGraphics);

            if (this.fMap == null)
            {
                return;
            }

            if (this.ShouldRenderVisibility)
            {
                DrawVisibility();
            }

            if (this.fShowGrid == MapGridMode.Overlay && this.fLayoutData.SquareSize >= 10f)
            {
                Pen darkGray = Pens.DarkGray;
                float squareSize1 = 0f;
                float width1 = this.fLayoutData.MapOffset.Width % this.fLayoutData.SquareSize;
                while (squareSize1 <= (float)base.ClientRectangle.Width)
                {
                    clientRectangle = base.ClientRectangle;
                    drawingGraphics.DrawLine(darkGray, squareSize1 + width1, 0f, squareSize1 + width1, (float)clientRectangle.Height);
                    squareSize1 += this.fLayoutData.SquareSize;
                }
                float single1 = 0f;
                float height1 = this.fLayoutData.MapOffset.Height % this.fLayoutData.SquareSize;
                while (single1 <= (float)base.ClientRectangle.Height)
                {
                    clientRectangle = base.ClientRectangle;
                    drawingGraphics.DrawLine(darkGray, 0f, single1 + height1, (float)clientRectangle.Width, single1 + height1);
                    single1 += this.fLayoutData.SquareSize;
                }
            }
            if (this.fShowGridLabels)
            {
                string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                float squareSize2 = this.fLayoutData.SquareSize / 4f;
                System.Drawing.Font font = new System.Drawing.Font(this.Font.FontFamily, squareSize2);
                for (int i = this.fLayoutData.MinX; i <= this.fLayoutData.MaxX; i++)
                {
                    int minX = i - this.fLayoutData.MinX + 1;
                    string str1 = minX.ToString();
                    RectangleF region1 = this.fLayoutData.GetRegion(new Point(i, this.fLayoutData.MinY), new System.Drawing.Size(1, 1));
                    this.draw_grid_label(drawingGraphics, str1, font, region1, this.fTop);
                    RectangleF region2 = this.fLayoutData.GetRegion(new Point(i, this.fLayoutData.MaxY), new System.Drawing.Size(1, 1));
                    this.draw_grid_label(drawingGraphics, str1, font, region2, this.fBottom);
                }
                for (int j = this.fLayoutData.MinY; j <= this.fLayoutData.MaxY; j++)
                {
                    int minY = j - this.fLayoutData.MinY;
                    string str2 = "";
                    if (minY >= str.Length)
                    {
                        int length = minY / str.Length;
                        str2 = string.Concat(str2, str.Substring(length - 1, 1));
                    }
                    int length1 = minY % str.Length;
                    str2 = string.Concat(str2, str.Substring(length1, 1));
                    RectangleF rectangleF2 = this.fLayoutData.GetRegion(new Point(this.fLayoutData.MinX, j), new System.Drawing.Size(1, 1));
                    this.draw_grid_label(drawingGraphics, str2, font, rectangleF2, this.fLeft);
                    RectangleF region3 = this.fLayoutData.GetRegion(new Point(this.fLayoutData.MaxX, j), new System.Drawing.Size(1, 1));
                    this.draw_grid_label(drawingGraphics, str2, font, region3, this.fRight);
                }
            }
            if (this.fHighlightAreas)
            {
                foreach (MapArea mapArea in this.fMap.Areas)
                {
                    PlotPointState state = PlotPointState.Normal;
                    if (this.fPlot != null)
                    {
                        PlotPoint plotPoint = this.fPlot.FindPointForMapArea(this.fMap, mapArea);
                        if (plotPoint != null)
                        {
                            state = plotPoint.State;
                        }
                    }
                    MapData mapDatum1 = this.fLayoutData;
                    Point location1 = mapArea.Region.Location;
                    clientRectangle = mapArea.Region;
                    RectangleF rectangleF3 = mapDatum1.GetRegion(location1, clientRectangle.Size);
                    Pen darkBlue = Pens.DarkGray;
                    if (mapArea == this.fHighlightedArea || mapArea == this.fSelectedArea)
                    {
                        darkBlue = Pens.DarkBlue;
                    }
                    drawingGraphics.DrawRectangle(darkBlue, rectangleF3.X, rectangleF3.Y, rectangleF3.Width, rectangleF3.Height);
                    if (state == PlotPointState.Completed || state == PlotPointState.Skipped)
                    {
                        PointF pointF = new PointF(rectangleF3.Left, rectangleF3.Top);
                        PointF pointF1 = new PointF(rectangleF3.Right, rectangleF3.Top);
                        PointF pointF2 = new PointF(rectangleF3.Left, rectangleF3.Bottom);
                        PointF pointF3 = new PointF(rectangleF3.Right, rectangleF3.Bottom);
                        Pen pen2 = new Pen(Color.DarkGray, 2f);
                        drawingGraphics.DrawLine(pen2, pointF, pointF3);
                        drawingGraphics.DrawLine(pen2, pointF2, pointF1);
                    }
                    if (!(this.fViewpoint == Rectangle.Empty) || !(mapArea.Name != "") || this.fNewTile != null || this.fDraggedTiles != null)
                    {
                        continue;
                    }
                    System.Drawing.Font font1 = this.Font;
                    if (state == PlotPointState.Skipped)
                    {
                        font1 = new System.Drawing.Font(font1, font1.Style | FontStyle.Strikeout);
                    }
                    float single2 = 8f;
                    SizeF sizeF = drawingGraphics.MeasureString(mapArea.Name, font1);
                    sizeF = new SizeF(sizeF.Width + single2, sizeF.Height + single2);
                    float width2 = (rectangleF3.Width - sizeF.Width) / 2f;
                    float height2 = (rectangleF3.Height - sizeF.Height) / 2f;
                    RectangleF rectangleF4 = new RectangleF(rectangleF3.Left + width2, rectangleF3.Top + height2, sizeF.Width, sizeF.Height);
                    GraphicsPath graphicsPath = RoundedRectangle.Create(rectangleF4, rectangleF4.Height / 3f);
                    using (Brush brush = new SolidBrush(Color.FromArgb(200, Color.Black)))
                    {
                        drawingGraphics.FillPath(brush, graphicsPath);
                    }
                    drawingGraphics.DrawPath(Pens.Black, graphicsPath);
                    drawingGraphics.DrawString(mapArea.Name, font1, Brushes.White, rectangleF4, this.fCentred);
                }
            }
            else if (this.fPlot != null)
            {
                foreach (MapArea area1 in this.fMap.Areas)
                {
                    PlotPoint plotPoint1 = this.fPlot.FindPointForMapArea(this.fMap, area1);
                    if (plotPoint1 != null && plotPoint1.State == PlotPointState.Completed)
                    {
                        continue;
                    }
                    MapData mapDatum2 = this.fLayoutData;
                    Point point1 = area1.Region.Location;
                    clientRectangle = area1.Region;
                    RectangleF region4 = mapDatum2.GetRegion(point1, clientRectangle.Size);
                    drawingGraphics.FillRectangle(Brushes.Black, region4);
                }
            }
            if (this.fShowAuras)
            {
                List<IToken> tokens = new List<IToken>();
                tokens.AddRange(this.fSelectedTokens);
                if (this.fHoverToken != null)
                {
                    tokens.Add(this.fHoverToken);
                }
                foreach (IToken token in tokens)
                {
                    Dictionary<Aura, Rectangle> auras = new Dictionary<Aura, Rectangle>();
                    CreatureToken creatureToken = token as CreatureToken;
                    if (creatureToken != null)
                    {
                        if (creatureToken.Data.Location == CombatData.NoPoint)
                        {
                            continue;
                        }
                        List<Aura> auras1 = new List<Aura>();
                        foreach (OngoingCondition condition in creatureToken.Data.Conditions)
                        {
                            if (condition.Type != OngoingType.Aura)
                            {
                                continue;
                            }
                            auras1.Add(condition.Aura);
                        }
                        EncounterSlot encounterSlot = this.fEncounter.FindSlot(creatureToken.SlotID);
                        if (encounterSlot != null)
                        {
                            auras1.AddRange(encounterSlot.Card.Auras);
                        }
                        if (encounterSlot != null)
                        {
                            ICreature creature1 = Session.FindCreature(encounterSlot.Card.CreatureID, SearchType.Global);
                            int num2 = (creature1 != null ? Creature.GetSize(creature1.Size) : 1);
                            foreach (Aura aura in auras1)
                            {
                                int radius = aura.Radius + num2 + aura.Radius;
                                location = creatureToken.Data.Location;
                                int x = location.X - aura.Radius;
                                location = creatureToken.Data.Location;
                                Point point2 = new Point(x, location.Y - aura.Radius);
                                System.Drawing.Size size1 = new System.Drawing.Size(radius, radius);
                                auras[aura] = new Rectangle(point2, size1);
                            }
                        }
                    }
                    Hero hero = token as Hero;
                    if (hero != null)
                    {
                        int size2 = Creature.GetSize(hero.Size);
                        CombatData combatData = hero.CombatData;
                        if (combatData != null)
                        {
                            foreach (OngoingCondition ongoingCondition in combatData.Conditions)
                            {
                                if (ongoingCondition.Type != OngoingType.Aura)
                                {
                                    continue;
                                }
                                int radius1 = ongoingCondition.Aura.Radius + size2 + ongoingCondition.Aura.Radius;
                                location = combatData.Location;
                                int x1 = location.X - ongoingCondition.Aura.Radius;
                                location = combatData.Location;
                                Point point3 = new Point(x1, location.Y - ongoingCondition.Aura.Radius);
                                System.Drawing.Size size3 = new System.Drawing.Size(radius1, radius1);
                                auras[ongoingCondition.Aura] = new Rectangle(point3, size3);
                            }
                        }
                    }
                    foreach (Aura key in auras.Keys)
                    {
                        Rectangle rectangle = auras[key];
                        RectangleF region5 = this.fLayoutData.GetRegion(rectangle.Location, rectangle.Size);
                        float squareSize3 = this.fLayoutData.SquareSize * 0.8f;
                        GraphicsPath graphicsPath1 = RoundedRectangle.Create(region5, squareSize3);
                        using (Pen pen3 = new Pen(Color.FromArgb(200, Color.Red)))
                        {
                            drawingGraphics.DrawPath(pen3, graphicsPath1);
                        }
                        using (Brush solidBrush1 = new SolidBrush(Color.FromArgb(15, Color.Red)))
                        {
                            drawingGraphics.FillPath(solidBrush1, graphicsPath1);
                        }
                    }
                }
            }
            if (this.fTokenLinks != null)
            {
                foreach (TokenLink fTokenLink in this.fTokenLinks)
                {
                    IToken token1 = fTokenLink.Tokens[0];
                    IToken token2 = fTokenLink.Tokens[1];
                    CombatData combatData1 = this.get_combat_data(token1);
                    CombatData combatDatum1 = this.get_combat_data(token2);
                    if (!combatData1.Visible || !combatDatum1.Visible)
                    {
                        continue;
                    }

                    if (this.HideNonVisibleTokens && (!this.latestTurnVisMap.IsVisible(combatData1) || !this.latestTurnVisMap.IsVisible(combatDatum1)))
                    {
                        continue;
                    }

                    RectangleF tokenRect = this.get_token_rect(token1);
                    RectangleF tokenRect1 = this.get_token_rect(token2);
                    if (tokenRect == RectangleF.Empty || tokenRect1 == RectangleF.Empty)
                    {
                        continue;
                    }
                    Color color4 = (fTokenLink == this.fHoverTokenLink ? Color.Navy : Color.Black);
                    PointF pointF4 = new PointF((tokenRect.Left + tokenRect.Right) / 2f, (tokenRect.Top + tokenRect.Bottom) / 2f);
                    PointF pointF5 = new PointF((tokenRect1.Left + tokenRect1.Right) / 2f, (tokenRect1.Top + tokenRect1.Bottom) / 2f);
                    using (Pen pen4 = new Pen(color4, 2f))
                    {
                        drawingGraphics.DrawLine(pen4, pointF4, pointF5);
                    }
                }
            }
            if (this.fEncounter != null)
            {
                foreach (CustomToken customToken in this.fEncounter.CustomTokens)
                {
                    if (customToken.Type != CustomTokenType.Overlay || !this.is_visible(customToken.Data))
                    {
                        continue;
                    }
                    if (customToken.CreatureID != Guid.Empty)
                    {
                        CreatureSize creatureSize = CreatureSize.Medium;
                        CombatData combatDatum2 = this.fEncounter.FindCombatData(customToken.CreatureID);
                        if (combatDatum2 != null)
                        {
                            customToken.Data.Location = combatDatum2.Location;
                            EncounterSlot encounterSlot1 = this.fEncounter.FindSlot(combatDatum2);
                            ICreature creature2 = Session.FindCreature(encounterSlot1.Card.CreatureID, SearchType.Global);
                            creatureSize = creature2.Size;
                        }
                        Hero hero1 = Session.Project.FindHero(customToken.CreatureID);
                        if (hero1 != null)
                        {
                            customToken.Data.Location = hero1.CombatData.Location;
                            creatureSize = hero1.Size;
                        }
                        if (customToken.Data.Location != CombatData.NoPoint)
                        {
                            int num3 = (Creature.GetSize(creatureSize) + 1) / 2;
                            location = customToken.Data.Location;
                            int x2 = location.X;
                            System.Drawing.Size overlaySize = customToken.OverlaySize;
                            int width3 = x2 - (overlaySize.Width - num3) / 2;
                            location = customToken.Data.Location;
                            int y = location.Y;
                            overlaySize = customToken.OverlaySize;
                            int height3 = y - (overlaySize.Height - num3) / 2;
                            customToken.Data.Location = new Point(width3, height3);
                        }
                    }
                    if (customToken.Data.Location == CombatData.NoPoint)
                    {
                        continue;
                    }
                    bool flag = this.fSelectedTokens.Contains(customToken);
                    bool d = false;
                    if (this.fHoverToken != null)
                    {
                        d = this.get_combat_data(this.fHoverToken).ID == customToken.Data.ID;
                    }
                    this.draw_custom(drawingGraphics, customToken.Data.Location, customToken, flag, d, false);
                }
                foreach (CustomToken customToken1 in this.fEncounter.CustomTokens)
                {
                    if (customToken1.Type != CustomTokenType.Token || customToken1.Data.Location == CombatData.NoPoint || !this.is_visible(customToken1.Data))
                    {
                        continue;
                    }
                    if (this.fDraggedToken != null && this.fDraggedToken.Token is CustomToken)
                    {
                        CustomToken customToken2 = this.fDraggedToken.Token as CustomToken;
                        if (customToken2.Type == CustomTokenType.Token && customToken1.ID == customToken2.ID && customToken1.Data.Location == this.fDraggedToken.Start)
                        {
                            if (customToken1.Data.Location == this.fDraggedToken.Location)
                            {
                                continue;
                            }
                            this.draw_token_placeholder(drawingGraphics, customToken1.Data.Location, this.fDraggedToken.Location, customToken1.TokenSize, false);
                            continue;
                        }
                    }
                    bool flag1 = this.fSelectedTokens.Contains(customToken1);
                    bool d1 = false;
                    if (this.fHoverToken != null)
                    {
                        d1 = this.get_combat_data(this.fHoverToken).ID == customToken1.Data.ID;
                    }
                    this.draw_custom(drawingGraphics, customToken1.Data.Location, customToken1, flag1, d1, false);
                }
                foreach (EncounterSlot allSlot1 in this.fEncounter.AllSlots)
                {
                    EncounterWave encounterWave = this.fEncounter.FindWave(allSlot1);
                    if (encounterWave != null && !encounterWave.Active && !this.fShowAllWaves)
                    {
                        continue;
                    }
                    foreach (CombatData combatDatum3 in allSlot1.CombatData)
                    {
                        if (combatDatum3.Location == CombatData.NoPoint || !this.is_visible(combatDatum3))
                        {
                            continue;
                        }

                        if (this.HideNonVisibleTokens && !this.latestTurnVisMap.IsVisible(combatDatum3))
                        {
                            continue;
                        }

                        if (this.fDraggedToken != null && this.fDraggedToken.Token is CreatureToken)
                        {
                            CreatureToken creatureToken1 = this.fDraggedToken.Token as CreatureToken;
                            if (allSlot1.ID == creatureToken1.SlotID && combatDatum3.Location == this.fDraggedToken.Start)
                            {
                                if (this.HideNonVisibleTokens && !this.latestTurnVisMap.IsVisible(this.fDraggedToken.Location))
                                {
                                    continue;
                                }

                                if (combatDatum3.Location == this.fDraggedToken.Location)
                                {
                                    continue;
                                }
                                ICreature creature3 = Session.FindCreature(allSlot1.Card.CreatureID, SearchType.Global);
                                bool image = creature3.Image != null;
                                this.draw_token_placeholder(drawingGraphics, combatDatum3.Location, this.fDraggedToken.Location, creature3.Size, image);
                                continue;
                            }
                        }
                        bool flag2 = false;
                        foreach (IToken fSelectedToken in this.fSelectedTokens)
                        {
                            CreatureToken creatureToken2 = fSelectedToken as CreatureToken;
                            if (creatureToken2 == null || combatDatum3 != creatureToken2.Data)
                            {
                                continue;
                            }
                            flag2 = true;
                        }
                        bool flag3 = false;
                        CreatureToken creatureToken3 = this.fHoverToken as CreatureToken;
                        if (creatureToken3 != null && combatDatum3 == creatureToken3.Data)
                        {
                            flag3 = true;
                        }

                        this.draw_creature(drawingGraphics, combatDatum3.Location, allSlot1.Card, combatDatum3, flag2, flag3, false, this.latestTurnVisMap[combatDatum3]);
                    }
                }
            }
            if (this.fEncounter != null)
            {
                foreach (Hero hero2 in Session.Project.Heroes)
                {
                    if (hero2 == null || hero2.CombatData.Location == CombatData.NoPoint)
                    {
                        continue;
                    }
                    if (this.fDraggedToken != null && this.fDraggedToken.Token is Hero)
                    {
                        Hero token3 = this.fDraggedToken.Token as Hero;
                        if (hero2.ID == token3.ID && hero2.CombatData.Location == this.fDraggedToken.Start)
                        {
                            if (hero2.CombatData.Location == this.fDraggedToken.Location)
                            {
                                continue;
                            }
                            bool portrait = hero2.Portrait != null;
                            this.draw_token_placeholder(drawingGraphics, hero2.CombatData.Location, this.fDraggedToken.Location, hero2.Size, portrait);
                            continue;
                        }
                    }
                    bool flag4 = this.fSelectedTokens.Contains(hero2);
                    bool flag5 = hero2 == this.fHoverToken;
                    this.draw_hero(drawingGraphics, hero2.CombatData.Location, hero2, flag4, flag5, false);
                }
            }
            if (this.fNewToken != null)
            {
                if (this.fNewToken.Token is CreatureToken)
                {
                    CreatureToken token4 = this.fNewToken.Token as CreatureToken;
                    EncounterSlot encounterSlot2 = this.fEncounter.FindSlot(token4.SlotID);
                    Session.FindCreature(encounterSlot2.Card.CreatureID, SearchType.Global);

                    if (!this.HideNonVisibleTokens || this.latestTurnVisMap.IsVisible(this.fNewToken.Location))
                    {
                        this.draw_creature(drawingGraphics, this.fNewToken.Location, encounterSlot2.Card, token4.Data, true, true, true, this.latestTurnVisMap[this.fNewToken.Location]);
                    }
                }
                if (this.fNewToken.Token is Hero)
                {
                    Hero hero3 = this.fNewToken.Token as Hero;
                    this.draw_hero(drawingGraphics, this.fNewToken.Location, hero3, true, true, true);
                }
                if (this.fNewToken.Token is CustomToken)
                {
                    CustomToken customToken3 = this.fNewToken.Token as CustomToken;
                    this.draw_custom(drawingGraphics, this.fNewToken.Location, customToken3, true, true, true);
                }
            }
            if (this.fDraggedToken != null)
            {
                if (this.fDraggedToken.Token is CreatureToken)
                {
                    CreatureToken creatureToken4 = this.fDraggedToken.Token as CreatureToken;
                    EncounterSlot encounterSlot3 = this.fEncounter.FindSlot(creatureToken4.SlotID);

                    if (!this.HideNonVisibleTokens || this.latestTurnVisMap.IsVisible(this.fDraggedToken.Location))
                    {
                        this.draw_creature(drawingGraphics, this.fDraggedToken.Location, encounterSlot3.Card, creatureToken4.Data, true, true, true, this.latestTurnVisMap[this.fDraggedToken.Location]);
                    }
                }
                if (this.fDraggedToken.Token is Hero)
                {
                    Hero hero4 = this.fDraggedToken.Token as Hero;
                    this.draw_hero(drawingGraphics, this.fDraggedToken.Location, hero4, true, true, true);
                }
                if (this.fDraggedToken.Token is CustomToken)
                {
                    CustomToken customToken4 = this.fDraggedToken.Token as CustomToken;
                    this.draw_custom(drawingGraphics, this.fDraggedToken.Location, customToken4, true, true, true);
                }
                if (this.fDraggedToken.LinkedToken != null)
                {
                    Pen pen5 = new Pen(Color.Red, 2f);
                    RectangleF tokenRect2 = this.get_token_rect(this.fDraggedToken.LinkedToken);
                    drawingGraphics.DrawRectangle(pen5, tokenRect2.X, tokenRect2.Y, tokenRect2.Width, tokenRect2.Height);
                }
            }
            this.fTokenLinkRegions.Clear();
            if (this.fTokenLinks != null)
            {
                foreach (TokenLink tokenLink in this.fTokenLinks)
                {
                    if (tokenLink.Text == "")
                    {
                        continue;
                    }
                    IToken item3 = tokenLink.Tokens[0];
                    IToken item4 = tokenLink.Tokens[1];
                    CombatData combatData2 = this.get_combat_data(item3);
                    CombatData combatData3 = this.get_combat_data(item4);
                    if (!combatData2.Visible || !combatData3.Visible)
                    {
                        continue;
                    }
                    if (this.HideNonVisibleTokens && (!this.latestTurnVisMap.IsVisible(combatData2) || !this.latestTurnVisMap.IsVisible(combatData3)))
                    {
                        continue;
                    }
                    Point tokenLocation = this.get_token_location(item3);
                    Point tokenLocation1 = this.get_token_location(item4);
                    if (tokenLocation == CombatData.NoPoint || tokenLocation1 == CombatData.NoPoint)
                    {
                        continue;
                    }
                    RectangleF tokenRect3 = this.get_token_rect(item3);
                    RectangleF tokenRect4 = this.get_token_rect(item4);
                    PointF pointF6 = new PointF((tokenRect3.Left + tokenRect3.Right) / 2f, (tokenRect3.Top + tokenRect3.Bottom) / 2f);
                    PointF pointF7 = new PointF((tokenRect4.Left + tokenRect4.Right) / 2f, (tokenRect4.Top + tokenRect4.Bottom) / 2f);
                    string text = tokenLink.Text;
                    float single3 = Math.Min(this.Font.Size, this.fLayoutData.SquareSize / 4f);
                    using (System.Drawing.Font font2 = new System.Drawing.Font(this.Font.FontFamily, single3))
                    {
                        SizeF sizeF1 = drawingGraphics.MeasureString(text, font2);
                        sizeF1 = new SizeF(sizeF1.Width * 1.2f, sizeF1.Height * 1.2f);
                        PointF pointF8 = new PointF((pointF6.X + pointF7.X) / 2f, (pointF6.Y + pointF7.Y) / 2f);
                        PointF pointF9 = new PointF(pointF8.X - sizeF1.Width / 2f, pointF8.Y - sizeF1.Height / 2f);
                        RectangleF rectangleF5 = new RectangleF(pointF9, sizeF1);
                        Pen pen6 = (tokenLink == this.fHoverTokenLink ? Pens.Blue : Pens.Navy);
                        drawingGraphics.FillRectangle(Brushes.White, rectangleF5);
                        drawingGraphics.DrawString(text, font2, Brushes.Navy, rectangleF5, this.fCentred);
                        drawingGraphics.DrawRectangle(pen6, rectangleF5.X, rectangleF5.Y, rectangleF5.Width, rectangleF5.Height);
                        this.fTokenLinkRegions[tokenLink] = rectangleF5;
                    }
                }
            }
            foreach (MapSketch fSketch in this.fSketches)
            {
                this.draw_sketch(drawingGraphics, fSketch);
            }
            if (this.fDrawing != null && this.fDrawing.CurrentSketch != null)
            {
                this.draw_sketch(drawingGraphics, this.fDrawing.CurrentSketch);
            }
            if (this.fLineOfSight)
            {
                Point client = base.PointToClient(System.Windows.Forms.Cursor.Position);
                if (base.ClientRectangle.Contains(client))
                {
                    PointF closestVertex = this.get_closest_vertex(client);
                    float single4 = Math.Max(this.fLayoutData.SquareSize / 10f, 3f);
                    foreach (IToken fSelectedToken1 in this.fSelectedTokens)
                    {
                        RectangleF tokenRect5 = this.get_token_rect(fSelectedToken1);
                        List<PointF> pointFs = new List<PointF>()
                        {
                            new PointF(tokenRect5.Left, tokenRect5.Top),
                            new PointF(tokenRect5.Left, tokenRect5.Bottom),
                            new PointF(tokenRect5.Right, tokenRect5.Top),
                            new PointF(tokenRect5.Right, tokenRect5.Bottom)
                        };
                        foreach (PointF pointF10 in pointFs)
                        {
                            drawingGraphics.DrawLine(Pens.Blue, closestVertex, pointF10);
                            RectangleF rectangleF6 = new RectangleF(pointF10.X - single4, pointF10.Y - single4, single4 * 2f, single4 * 2f);
                            drawingGraphics.FillEllipse(Brushes.LightBlue, rectangleF6);
                            drawingGraphics.DrawEllipse(Pens.Blue, rectangleF6);
                        }
                    }
                    RectangleF rectangleF7 = new RectangleF(closestVertex.X - single4, closestVertex.Y - single4, single4 * 2f, single4 * 2f);
                    drawingGraphics.FillEllipse(Brushes.LightBlue, rectangleF7);
                    drawingGraphics.DrawEllipse(Pens.Blue, rectangleF7);
                }
            }
            if (this.fDraggedOutline != null)
            {
                RectangleF region6 = this.fLayoutData.GetRegion(this.fDraggedOutline.Region.Location, this.fDraggedOutline.Region.Size);
                drawingGraphics.DrawRectangle(Pens.DarkBlue, region6.X, region6.Y, region6.Width, region6.Height);
                string str3 = string.Concat(this.fDraggedOutline.Region.Width, "x", this.fDraggedOutline.Region.Height);
                SizeF sizeF2 = drawingGraphics.MeasureString(str3, this.Font);
                sizeF2.Width = Math.Min(region6.Width, sizeF2.Width);
                sizeF2.Height = Math.Min(region6.Height, sizeF2.Height);
                float width4 = (region6.Width - sizeF2.Width) / 2f;
                float height4 = (region6.Height - sizeF2.Height) / 2f;
                RectangleF rectangleF8 = new RectangleF(region6.X + width4, region6.Y + height4, sizeF2.Width, sizeF2.Height);
                using (Brush brush1 = new SolidBrush(Color.FromArgb(150, Color.White)))
                {
                    drawingGraphics.FillRectangle(brush1, rectangleF8);
                }
                drawingGraphics.DrawString(str3, this.Font, Brushes.DarkBlue, region6, this.fCentred);
            }
            RectangleF region7 = this.fLayoutData.GetRegion(this.fCurrentOutline.Location, this.fCurrentOutline.Size);
            drawingGraphics.DrawRectangle(Pens.LightBlue, region7.X, region7.Y, region7.Width, region7.Height);
            if (this.fFrameType != MapDisplayType.None && this.fViewpoint != Rectangle.Empty && !this.fAllowScrolling)
            {
                Color black = Color.Black;
                if (this.fMode == MapViewMode.Plain)
                {
                    black = Color.White;
                }
                int num4 = 255;
                switch (this.fFrameType)
                {
                    case MapDisplayType.Dimmed:
                        {
                            num4 = 160;
                            break;
                        }
                    case MapDisplayType.Opaque:
                        {
                            num4 = 255;
                            break;
                        }
                }
                RectangleF region8 = this.fLayoutData.GetRegion(this.fViewpoint.Location, this.fViewpoint.Size);
                using (Brush solidBrush2 = new SolidBrush(Color.FromArgb(num4, black)))
                {
                    clientRectangle = base.ClientRectangle;
                    drawingGraphics.FillRectangle(solidBrush2, 0f, 0f, (float)clientRectangle.Width, region8.Top);
                    float bottom = region8.Bottom;
                    float width5 = (float)base.ClientRectangle.Width;
                    clientRectangle = base.ClientRectangle;
                    drawingGraphics.FillRectangle(solidBrush2, 0f, bottom, width5, (float)clientRectangle.Height);
                    drawingGraphics.FillRectangle(solidBrush2, 0f, region8.Top, region8.Left, region8.Height);
                    float right = region8.Right;
                    float top = region8.Top;
                    clientRectangle = base.ClientRectangle;
                    drawingGraphics.FillRectangle(solidBrush2, right, top, (float)clientRectangle.Width - region8.Right, region8.Height);
                }
                if (this.fHighlightAreas)
                {
                    drawingGraphics.DrawRectangle(SystemPens.ControlLight, region8.X, region8.Y, region8.Width, region8.Height);
                }
            }
            string str4 = this.fCaption;
            if (str4 == "")
            {
                if (this.fMode == MapViewMode.Normal && this.fMap.Areas.Count == 0)
                {
                    str4 = "To create map areas (rooms etc), right-click on the map and drag.";
                }
                if (this.fMap.Name == "")
                {
                    str4 = "You need to give this map a name.";
                }
                if (this.fMode == MapViewMode.Normal && this.fMap.Tiles.Count == 0)
                {
                    str4 = "To begin, drag tiles from the list on the right onto the blueprint.";
                }
                if (this.fAllowScrolling)
                {
                    str4 = "Map is in scroll / zoom mode; double-click to return to tactical mode.";
                }
                if (this.fDrawing != null)
                {
                    str4 = "Map is in drawing mode; double-click to return to tactical mode.";
                }
                if (this.fLineOfSight)
                {
                    str4 = "Map is in line of sight mode; select tokens to check sightlines, or double-click to return to tactical mode.";
                }
                if (this.fDraggedToken != null && this.fDraggedToken.LinkedToken != null)
                {
                    TokenLink tokenLink1 = this.find_link(this.fDraggedToken.Token, this.fDraggedToken.LinkedToken);
                    str4 = (tokenLink1 != null ? string.Concat("Release here to remove the ", (tokenLink1.Text == "" ? "link" : string.Concat("'", tokenLink1.Text, "' link")), ".") : "Release here to create a link.");
                }
            }
            if (str4 != "")
            {
                float single5 = 10f;
                clientRectangle = base.ClientRectangle;
                float width6 = (float)clientRectangle.Width - 2f * single5;
                SizeF sizeF3 = drawingGraphics.MeasureString(str4, this.Font, (int)width6);
                float height5 = sizeF3.Height * 2f;
                RectangleF rectangleF9 = new RectangleF(single5, single5, width6, height5);
                GraphicsPath graphicsPath2 = RoundedRectangle.Create(rectangleF9, height5 / 3f);
                using (Brush brush2 = new SolidBrush(Color.FromArgb(200, Color.Black)))
                {
                    drawingGraphics.FillPath(brush2, graphicsPath2);
                }
                drawingGraphics.DrawPath(Pens.Black, graphicsPath2);
                drawingGraphics.DrawString(str4, this.Font, Brushes.White, rectangleF9, this.fCentred);
            }
            sw.Stop();
            Console.WriteLine("Redraw took {0} ms", sw.ElapsedMilliseconds);

            base.Invalidate();

            DataCollection.StopProfile(ProfileLevel.Global, DataCollection.CurrentId);
        }

        protected void OnRegionSelected()
        {
            if (this.RegionSelected != null)
            {
                this.RegionSelected(this, new EventArgs());
            }
        }

        protected override void OnResize(EventArgs e)
        {
            this.fLayoutData = null;
            this.RecreateBuffers();
            this.Redraw();
        }

        protected void OnSelectedTokensChanged()
        {
            if (this.SelectedTokensChanged != null)
            {
                this.SelectedTokensChanged(this, new EventArgs());
            }

            this.Redraw();
        }

        protected void OnSketchCreated(MapSketch sketch)
        {
            if (this.SketchCreated != null)
            {
                this.SketchCreated(this, new MapSketchEventArgs(sketch));
            }
        }

        protected void OnTileContext(TileData tile)
        {
            if (this.TileContext != null)
            {
                this.TileContext(this, new TileEventArgs(tile));
            }
        }

        protected void OnTokenActivated(IToken token)
        {
            if (this.TokenActivated != null)
            {
                this.TokenActivated(this, new TokenEventArgs(token));
            }
        }

        protected void OnTokenDragged()
        {
            Point point;
            if (this.TokenDragged != null)
            {
                Point point1 = (this.fDraggedToken != null ? this.fDraggedToken.Start : CombatData.NoPoint);
                point = (this.fDraggedToken != null ? this.fDraggedToken.Location : CombatData.NoPoint);
                this.TokenDragged(this, new DraggedTokenEventArgs(point1, point));
            }
            this.Redraw();
        }

        public void SelectTokens(List<IToken> tokens, bool raise_event)
        {
            if (tokens == null)
            {
                this.fSelectedTokens.Clear();
                return;
            }
            foreach (IToken token in tokens)
            {
                if (this.fSelectedTokens.Contains(token))
                {
                    continue;
                }
                this.fSelectedTokens.Add(token);
            }
            List<IToken> tokens1 = new List<IToken>();
            foreach (IToken fSelectedToken in this.fSelectedTokens)
            {
                if (tokens.Contains(fSelectedToken))
                {
                    continue;
                }
                tokens1.Add(fSelectedToken);
            }
            foreach (IToken token1 in tokens1)
            {
                this.fSelectedTokens.Remove(token1);
            }
            if (raise_event)
            {
                this.OnSelectedTokensChanged();
            }
        }

        public void SetDragInfo(Point old_point, Point new_point)
        {
            if (old_point == CombatData.NoPoint)
            {
                this.fDraggedToken = null;
                return;
            }
            Pair<IToken, Rectangle> tokenAt = this.get_token_at(old_point);
            if (tokenAt != null)
            {
                this.fDraggedToken = new MapView.DraggedToken()
                {
                    Token = tokenAt.First,
                    Start = old_point,
                    Location = new_point
                };
            }
        }

        [Category("Action")]
        [Description("Occurs when a map area is double-clicked.")]
        public event MapAreaEventHandler AreaActivated;

        [Category("Action")]
        [Description("Occurs when a map area is clicked.")]
        public event MapAreaEventHandler AreaSelected;

        [Category("Action")]
        [Description("Called when the drawing mode is cancelled.")]
        public event EventHandler CancelledDrawing;

        [Category("Action")]
        [Description("Called when the LOS mode is cancelled.")]
        public event EventHandler CancelledLOS;

        [Category("Action")]
        [Description("Called when the scrolling mode is cancelled.")]
        public event EventHandler CancelledScrolling;

        [Category("Action")]
        [Description("Occurs when a link should be created.")]
        public event CreateTokenLinkEventHandler CreateTokenLink;

        [Category("Action")]
        [Description("Occurs when a link should be edited.")]
        public event TokenLinkEventHandler EditTokenLink;

        [Category("Property Changed")]
        [Description("Occurs when the highlighted map area has changed.")]
        public event EventHandler HighlightedAreaChanged;

        [Category("Property Changed")]
        [Description("Occurs when the hovered token has changed.")]
        public event EventHandler HoverTokenChanged;

        [Category("Action")]
        [Description("Called when a tile or token is dropped onto the map.")]
        public event ItemDroppedEventHandler ItemDropped;

        [Category("Action")]
        [Description("Called when a tile or token is moved around the map.")]
        public event MovementEventHandler ItemMoved;

        [Category("Action")]
        [Description("Called when a tile or token is removed from the map.")]
        public event EventHandler ItemRemoved;

        [Category("Action")]
        [Description("Occurs when the mouse wheel is scrolled.")]
        public event MouseEventHandler MouseZoomed;

        [Category("Action")]
        [Description("Called when an area has been selected.")]
        public event EventHandler RegionSelected;

        [Category("Property Changed")]
        [Description("Occurs when the selected tokens have changed.")]
        public event EventHandler SelectedTokensChanged;

        [Category("Action")]
        [Description("Occurs when a sketch is created.")]
        public event MapSketchEventHandler SketchCreated;

        [Category("Action")]
        [Description("Called when a context menu should be displayed.")]
        public event EventHandler TileContext;

        [Category("Action")]
        [Description("Occurs when a map token is double-clicked.")]
        public event TokenEventHandler TokenActivated;

        [Category("Action")]
        [Description("Occurs when a map token is dragged.")]
        public event DraggedTokenEventHandler TokenDragged;

        private class DraggedOutline
        {
            public Point Start;

            public Rectangle Region;

            public DraggedOutline()
            {
            }
        }

        private class DraggedTiles
        {
            public List<TileData> Tiles;

            public Point Start;

            public System.Drawing.Size Offset;

            public RectangleF Region;

            public DraggedTiles()
            {
            }
        }

        public class DraggedToken
        {
            public IToken Token;

            public Point Start;

            public System.Drawing.Size Offset;

            public Point Location;

            public IToken LinkedToken;

            public DraggedToken()
            {
            }
        }

        private class DrawingData
        {
            public MapSketch CurrentSketch;

            public DrawingData()
            {
            }
        }

        private class NewTile
        {
            public Tile Tile;

            public Point Location;

            public RectangleF Region;

            public NewTile()
            {
            }
        }

        private class NewToken
        {
            public IToken Token;

            public Point Location;

            public NewToken()
            {
            }
        }

        private class ScrollingData
        {
            public Point Start;

            public ScrollingData()
            {
            }
        }

#region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            //components = new System.ComponentModel.Container();
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            base.SuspendLayout();
            this.AllowDrop = true;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Name = "MapView";
            base.ResumeLayout(false);
        }

#endregion
    }
}