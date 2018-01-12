using Masterplan;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Utils;

namespace Masterplan.Controls
{
    partial class PlotView
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

        private const int ARROW_SIZE = 6;

        private PlotPoint fHoverPoint;

        private StringFormat fCentred = new StringFormat();

        private List<List<PlotPoint>> fLayers;

        private Dictionary<Guid, RectangleF> fRegions;

        private Dictionary<Guid, Dictionary<Guid, List<PointF>>> fLinkPaths;

        private Rectangle fUpRect = Rectangle.Empty;

        private Rectangle fDownRect = Rectangle.Empty;

        private Masterplan.Data.Plot fPlot;

        private PlotViewMode fMode;

        private PlotViewLinkStyle fLinkStyle;

        private string fFilter = "";

        private bool fShowLevels = true;

        private PlotPoint fSelectedPoint;

        private bool fShowTooltips = true;

        private PlotView.DragLocation fDragLocation;

        private ToolTip Tooltip;

        [Category("Behavior")]
        [Description("Plot points which do not contain this text are not shown.")]
        public string Filter
        {
            get
            {
                return this.fFilter;
            }
            set
            {
                this.fFilter = value;
                base.Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("How plot point links should be displayed.")]
        public PlotViewLinkStyle LinkStyle
        {
            get
            {
                return this.fLinkStyle;
            }
            set
            {
                this.fLinkStyle = value;
                this.RecalculateLayout();
                base.Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("How the plot should be displayed.")]
        public PlotViewMode Mode
        {
            get
            {
                return this.fMode;
            }
            set
            {
                this.fMode = value;
                base.Invalidate();
            }
        }

        [Category("Data")]
        [Description("The plot to display.")]
        public Masterplan.Data.Plot Plot
        {
            get
            {
                return this.fPlot;
            }
            set
            {
                if (this.fPlot != value)
                {
                    this.fPlot = value;
                    this.fSelectedPoint = null;
                    this.fHoverPoint = null;
                    this.RecalculateLayout();
                    base.Invalidate();
                    this.OnSelectionChanged();
                }
            }
        }

        [Category("Behavior")]
        [Description("The selected point.")]
        public PlotPoint SelectedPoint
        {
            get
            {
                return this.fSelectedPoint;
            }
            set
            {
                if (this.fSelectedPoint != value)
                {
                    this.fSelectedPoint = value;
                    base.Invalidate();
                    this.OnSelectionChanged();
                }
            }
        }

        [Category("Appearance")]
        [Description("Determines whether levelling information is shown.")]
        public bool ShowLevels
        {
            get
            {
                return this.fShowLevels;
            }
            set
            {
                this.fShowLevels = value;
                base.Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("Determines whether tooltips are shown.")]
        public bool ShowTooltips
        {
            get
            {
                return this.fShowTooltips;
            }
            set
            {
                this.fShowTooltips = value;
            }
        }

        private bool allow_drop(PlotPoint dragged, PlotPoint target)
        {
            bool flag;
            try
            {
                if (dragged == target)
                {
                    flag = false;
                }
                else if (target.Links.Contains(dragged.ID))
                {
                    flag = false;
                }
                else if (!this.fPlot.FindSubtree(dragged).Contains(target))
                {
                    return true;
                }
                else
                {
                    flag = false;
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
                return true;
            }
            return flag;
        }

        private PlotView.DragLocation allow_drop(PlotPoint dragged, Point pt)
        {
            PlotView.DragLocation dragLocation;
            try
            {
                RectangleF item = this.fRegions[dragged.ID];
                float y = item.Y;
                Rectangle clientRectangle = base.ClientRectangle;
                RectangleF rectangleF = new RectangleF(0f, y, (float)clientRectangle.Width, item.Height);
                if (rectangleF.Contains(pt))
                {
                    List<PlotPoint> plotPoints = new List<PlotPoint>();
                    foreach (PlotPoint point in this.fPlot.Points)
                    {
                        RectangleF item1 = this.fRegions[point.ID];
                        if (!rectangleF.Contains(item1))
                        {
                            continue;
                        }
                        if (!item1.Contains(pt))
                        {
                            plotPoints.Add(point);
                        }
                        else
                        {
                            dragLocation = null;
                            return dragLocation;
                        }
                    }
                    if (plotPoints.Count != 0)
                    {
                        List<Pair<PlotPoint, PlotPoint>> pairs = new List<Pair<PlotPoint, PlotPoint>>();
                        foreach (PlotPoint plotPoint in plotPoints)
                        {
                            int num = plotPoints.IndexOf(plotPoint);
                            if (num != 0)
                            {
                                pairs.Add(new Pair<PlotPoint, PlotPoint>(plotPoints[num - 1], plotPoint));
                            }
                            else
                            {
                                pairs.Add(new Pair<PlotPoint, PlotPoint>(null, plotPoint));
                            }
                            if (num != plotPoints.Count - 1)
                            {
                                continue;
                            }
                            pairs.Add(new Pair<PlotPoint, PlotPoint>(plotPoint, null));
                        }
                        foreach (Pair<PlotPoint, PlotPoint> pair in pairs)
                        {
                            if (pair.First == dragged || pair.Second == dragged)
                            {
                                continue;
                            }
                            float right = 0f;
                            float width = (float)base.ClientRectangle.Width;
                            if (pair.First != null)
                            {
                                right = this.fRegions[pair.First.ID].Right;
                            }
                            if (pair.Second != null)
                            {
                                width = this.fRegions[pair.Second.ID].Left;
                            }
                            RectangleF rectangleF1 = new RectangleF(right, rectangleF.Y, width - right, rectangleF.Height);
                            if (!rectangleF1.Contains(pt))
                            {
                                continue;
                            }
                            PlotView.DragLocation dragLocation1 = new PlotView.DragLocation()
                            {
                                LHS = pair.First,
                                RHS = pair.Second,
                                Rect = rectangleF1
                            };
                            dragLocation = dragLocation1;
                            return dragLocation;
                        }
                        return null;
                    }
                    else
                    {
                        dragLocation = null;
                    }
                }
                else
                {
                    dragLocation = null;
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
                return null;
            }
            return dragLocation;
        }

        private void clear_layout_calculations()
        {
            this.fUpRect = Rectangle.Empty;
            this.fDownRect = Rectangle.Empty;
            this.fLayers = null;
            this.fRegions = null;
            this.fLinkPaths = null;
        }

        private void do_layout_calculations()
        {
            try
            {
                this.clear_layout_calculations();
                Rectangle clientRectangle = base.ClientRectangle;
                Rectangle rectangle = base.ClientRectangle;
                this.fUpRect = new Rectangle(clientRectangle.Right - 35, rectangle.Top + 15, 25, 20);
                Rectangle clientRectangle1 = base.ClientRectangle;
                Rectangle rectangle1 = base.ClientRectangle;
                this.fDownRect = new Rectangle(clientRectangle1.Right - 35, rectangle1.Top + 40, 25, 20);
                this.fLayers = Workspace.FindLayers(this.fPlot);
                this.fRegions = new Dictionary<Guid, RectangleF>();
                int count = this.fLayers.Count * 2 + 1;
                Rectangle clientRectangle2 = base.ClientRectangle;
                float height = (float)(clientRectangle2.Height - 1) / (float)count;
                foreach (List<PlotPoint> fLayer in this.fLayers)
                {
                    int num = this.fLayers.IndexOf(fLayer) * 2 + 1;
                    float single = (float)num * height;
                    float x = (float)base.ClientRectangle.X;
                    Rectangle rectangle2 = base.ClientRectangle;
                    RectangleF rectangleF = new RectangleF(x, single, (float)rectangle2.Width, height);
                    int count1 = fLayer.Count * 2 + 1;
                    float width = rectangleF.Width / (float)count1;
                    foreach (PlotPoint plotPoint in fLayer)
                    {
                        int num1 = fLayer.IndexOf(plotPoint) * 2 + 1;
                        float single1 = (float)num1 * width;
                        RectangleF rectangleF1 = new RectangleF(single1, single, width, height);
                        this.fRegions[plotPoint.ID] = rectangleF1;
                    }
                }
                if (this.fLinkStyle != PlotViewLinkStyle.Straight)
                {
                    this.fLinkPaths = new Dictionary<Guid, Dictionary<Guid, List<PointF>>>();
                    foreach (PlotPoint point in this.fPlot.Points)
                    {
                        if (!this.fRegions.ContainsKey(point.ID))
                        {
                            continue;
                        }
                        Dictionary<Guid, List<PointF>> guids = new Dictionary<Guid, List<PointF>>();
                        foreach (Guid link in point.Links)
                        {
                            if (!this.fRegions.ContainsKey(link))
                            {
                                continue;
                            }
                            RectangleF item = this.fRegions[point.ID];
                            RectangleF item1 = this.fRegions[link];
                            PointF pointF = new PointF(item.X + item.Width / 2f, item.Bottom + 12f);
                            PointF pointF1 = new PointF(item1.X + item1.Width / 2f, item1.Top - 12f);
                            List<PointF> pointFs = new List<PointF>()
                            {
                                pointF
                            };
                            bool flag = false;
                            while (!flag)
                            {
                                PlotPoint blockingPoint = null;
                                if (this.find_layer_index(link, this.fLayers) - this.find_layer_index(point.ID, this.fLayers) > 1)
                                {
                                    blockingPoint = this.get_blocking_point(pointF, pointF1);
                                }
                                if (blockingPoint != null)
                                {
                                    RectangleF item2 = this.fRegions[blockingPoint.ID];
                                    int num2 = this.find_layer_index(blockingPoint.ID, this.fLayers);
                                    List<PlotPoint> plotPoints = this.fLayers[num2];
                                    float width1 = item2.Width / 3f;
                                    if (plotPoints.Count == 1)
                                    {
                                        width1 = item2.Width / 6f;
                                    }
                                    float single2 = ((float)Math.Round((double)(item2.Left + item2.Width / 2f)) >= pointF1.X ? item2.Left - width1 : item2.Right + width1);
                                    PointF pointF2 = new PointF(single2, item2.Top);
                                    PointF pointF3 = new PointF(single2, item2.Bottom);
                                    pointFs.Add(pointF2);
                                    pointFs.Add(pointF3);
                                    pointF = pointF3;
                                }
                                else
                                {
                                    flag = true;
                                }
                            }
                            pointFs.Add(pointF1);
                            guids[link] = pointFs;
                        }
                        this.fLinkPaths[point.ID] = guids;
                    }
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
            }
        }

        private bool draw_link(PlotPoint pp1, PlotPoint pp2)
        {
            bool flag;
            try
            {
                if (this.fFilter != "")
                {
                    flag = (!this.draw_point(pp1) ? false : this.draw_point(pp2));
                }
                else if (this.fMode != PlotViewMode.HighlightSelected)
                {
                    return true;
                }
                else
                {
                    flag = (this.fSelectedPoint == null || pp1 == this.fSelectedPoint ? true : pp2 == this.fSelectedPoint);
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
                return true;
            }
            return flag;
        }

        private bool draw_point(PlotPoint pp)
        {
            bool count;
            bool flag;
            try
            {
                if (this.fFilter != "")
                {
                    count = this.match_filter(pp);
                }
                else if (this.fMode == PlotViewMode.HighlightSelected)
                {
                    if (this.fSelectedPoint == null || pp == this.fSelectedPoint)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = (this.fSelectedPoint.Links.Contains(pp.ID) ? true : pp.Links.Contains(this.fSelectedPoint.ID));
                    }
                    count = flag;
                }
                else if (this.fMode != PlotViewMode.HighlightEncounter)
                {
                    if (this.fMode == PlotViewMode.HighlightTrap)
                    {
                        if (pp.Element == null)
                        {
                            count = false;
                            return count;
                        }
                        else if (pp.Element is TrapElement)
                        {
                            count = true;
                            return count;
                        }
                        else if (pp.Element is Encounter)
                        {
                            count = (pp.Element as Encounter).Traps.Count != 0;
                            return count;
                        }
                    }
                    if (this.fMode == PlotViewMode.HighlightChallenge)
                    {
                        if (pp.Element == null)
                        {
                            count = false;
                            return count;
                        }
                        else if (pp.Element is SkillChallenge)
                        {
                            count = true;
                            return count;
                        }
                        else if (pp.Element is Encounter)
                        {
                            count = (pp.Element as Encounter).SkillChallenges.Count != 0;
                            return count;
                        }
                    }
                    if (this.fMode == PlotViewMode.HighlightQuest)
                    {
                        count = (pp.Element == null ? false : pp.Element is Quest);
                    }
                    else if (this.fMode != PlotViewMode.HighlightParcel)
                    {
                        return true;
                    }
                    else
                    {
                        count = pp.Parcels.Count != 0;
                    }
                }
                else
                {
                    count = (pp.Element == null ? false : pp.Element is Encounter);
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
                return true;
            }
            return count;
        }

        private List<PlotPoint> find_layer(float y)
        {
            try
            {
                foreach (List<PlotPoint> fLayer in this.fLayers)
                {
                    PlotPoint item = fLayer[0];
                    if (y >= this.fRegions[item.ID].Bottom)
                    {
                        continue;
                    }
                    return fLayer;
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
            }
            return null;
        }

        private int find_layer_index(Guid point_id, List<List<PlotPoint>> layers)
        {
            try
            {
                for (int i = 0; i != layers.Count; i++)
                {
                    foreach (PlotPoint item in layers[i])
                    {
                        if (item.ID != point_id)
                        {
                            continue;
                        }
                        return i;
                    }
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
            }
            return -1;
        }

        private PlotPoint find_point_at(Point pt)
        {
            try
            {
                if (this.fRegions == null)
                {
                    this.do_layout_calculations();
                }
                foreach (Guid key in this.fRegions.Keys)
                {
                    if (!this.fRegions[key].Contains(pt))
                    {
                        continue;
                    }
                    return this.fPlot.FindPoint(key);
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
            }
            return null;
        }

        private PlotPoint get_blocking_point(PointF from_pt, PointF to_pt)
        {
            PlotPoint plotPoint;
            try
            {
                List<PlotPoint> item = this.find_layer(from_pt.Y);
                List<PlotPoint> plotPoints = this.find_layer(to_pt.Y);
                if (item == null || item == plotPoints)
                {
                    plotPoint = null;
                }
                else
                {
                    if (item != null)
                    {
                        int num = this.fLayers.IndexOf(item);
                        int num1 = this.fLayers.IndexOf(plotPoints);
                        int num2 = Math.Min(num1, this.fLayers.Count) - 1;
                        for (int i = num; i <= num2; i++)
                        {
                            item = this.fLayers[i];
                            PlotPoint item1 = item[0];
                            RectangleF rectangleF = this.fRegions[item1.ID];
                            float top = rectangleF.Top;
                            float bottom = rectangleF.Bottom;
                            float x = to_pt.X - from_pt.X;
                            float y = to_pt.Y - from_pt.Y;
                            float single = (top - from_pt.Y) / y;
                            float y1 = (bottom - from_pt.Y) / y;
                            float x1 = from_pt.X + single * x;
                            float single1 = from_pt.X + y1 * x;
                            PointF pointF = new PointF(x1, top);
                            PointF pointF1 = new PointF(single1, bottom);
                            foreach (PlotPoint plotPoint1 in item)
                            {
                                RectangleF rectangleF1 = this.fRegions[plotPoint1.ID];
                                RectangleF rectangleF2 = RectangleF.Inflate(rectangleF1, 2f, 2f);
                                if (!rectangleF2.Contains(pointF) && !rectangleF2.Contains(pointF1))
                                {
                                    continue;
                                }
                                plotPoint = plotPoint1;
                                return plotPoint;
                            }
                        }
                    }
                    return null;
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
                return null;
            }
            return plotPoint;
        }

        internal static Pair<Color, Color> GetColourGradient(PlotPointColour colour, int alpha)
        {
            Color white = Color.White;
            Color black = Color.Black;
            switch (colour)
            {
                case PlotPointColour.Yellow:
                    {
                        white = Color.FromArgb(alpha, 255, 255, 215);
                        black = Color.FromArgb(alpha, 255, 255, 165);
                        break;
                    }
                case PlotPointColour.Blue:
                    {
                        white = Color.FromArgb(alpha, 215, 215, 255);
                        black = Color.FromArgb(alpha, 165, 165, 255);
                        break;
                    }
                case PlotPointColour.Green:
                    {
                        white = Color.FromArgb(alpha, 215, 255, 215);
                        black = Color.FromArgb(alpha, 165, 255, 165);
                        break;
                    }
                case PlotPointColour.Purple:
                    {
                        white = Color.FromArgb(alpha, 240, 205, 255);
                        black = Color.FromArgb(alpha, 240, 150, 255);
                        break;
                    }
                case PlotPointColour.Orange:
                    {
                        white = Color.FromArgb(alpha, 255, 240, 210);
                        black = Color.FromArgb(alpha, 255, 165, 120);
                        break;
                    }
                case PlotPointColour.Brown:
                    {
                        white = Color.FromArgb(alpha, 255, 240, 215);
                        black = Color.FromArgb(alpha, 170, 140, 110);
                        break;
                    }
                case PlotPointColour.Grey:
                    {
                        white = Color.FromArgb(alpha, 225, 225, 225);
                        black = Color.FromArgb(alpha, 175, 175, 175);
                        break;
                    }
            }
            return new Pair<Color, Color>(white, black);
        }

        private bool match_filter(PlotPoint pp)
        {
            try
            {
                string[] strArrays = this.fFilter.Split(new char[0]);
                int num = 0;
                while (num < (int)strArrays.Length)
                {
                    if (this.match_token(pp, strArrays[num]))
                    {
                        num++;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
            }
            return true;
        }

        private bool match_token(PlotPoint pp, string token)
        {
            bool flag;
            try
            {
                token = token.ToLower();
                if (pp.Name.ToLower().Contains(token))
                {
                    flag = true;
                }
                else if (pp.Details.ToLower().Contains(token))
                {
                    flag = true;
                }
                else if (!pp.ReadAloud.ToLower().Contains(token))
                {
                    if (pp.Element is Encounter)
                    {
                        Encounter element = pp.Element as Encounter;
                        foreach (EncounterSlot slot in element.Slots)
                        {
                            if (!Session.FindCreature(slot.Card.CreatureID, SearchType.Global).Name.ToLower().Contains(token))
                            {
                                continue;
                            }
                            flag = true;
                            return flag;
                        }
                        foreach (EncounterNote note in element.Notes)
                        {
                            if (!note.Contents.ToLower().Contains(token))
                            {
                                continue;
                            }
                            flag = true;
                            return flag;
                        }
                    }
                    if (pp.Element is SkillChallenge)
                    {
                        SkillChallenge skillChallenge = pp.Element as SkillChallenge;
                        if (skillChallenge.Success.ToLower().Contains(token))
                        {
                            flag = true;
                            return flag;
                        }
                        else if (!skillChallenge.Failure.ToLower().Contains(token))
                        {
                            foreach (SkillChallengeData skill in skillChallenge.Skills)
                            {
                                if (!skill.SkillName.ToLower().Contains(token))
                                {
                                    if (!skill.Details.ToLower().Contains(token))
                                    {
                                        continue;
                                    }
                                    flag = true;
                                    return flag;
                                }
                                else
                                {
                                    flag = true;
                                    return flag;
                                }
                            }
                        }
                        else
                        {
                            flag = true;
                            return flag;
                        }
                    }
                    if (pp.Element is TrapElement)
                    {
                        TrapElement trapElement = pp.Element as TrapElement;
                        if (!trapElement.Trap.Name.ToLower().Contains(token))
                        {
                            foreach (TrapSkillData trapSkillDatum in trapElement.Trap.Skills)
                            {
                                if (!trapSkillDatum.SkillName.ToLower().Contains(token))
                                {
                                    if (!trapSkillDatum.Details.ToLower().Contains(token))
                                    {
                                        continue;
                                    }
                                    flag = true;
                                    return flag;
                                }
                                else
                                {
                                    flag = true;
                                    return flag;
                                }
                            }
                        }
                        else
                        {
                            flag = true;
                            return flag;
                        }
                    }
                    return false;
                }
                else
                {
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
                return false;
            }
            return flag;
        }

        public bool Navigate(Keys key)
        {
            bool flag;
            try
            {
                if (this.SelectedPoint != null)
                {
                    List<List<PlotPoint>> lists = Workspace.FindLayers(this.fPlot);
                    int num = 0;
                    while (num != lists.Count && !lists[num].Contains(this.SelectedPoint))
                    {
                        num++;
                    }
                    if (key == Keys.Up)
                    {
                        if (num != 0)
                        {
                            foreach (PlotPoint item in lists[num - 1])
                            {
                                if (!item.Links.Contains(this.SelectedPoint.ID))
                                {
                                    continue;
                                }
                                this.SelectedPoint = item;
                                break;
                            }
                        }
                        flag = true;
                    }
                    else if (key == Keys.Down)
                    {
                        if (num != lists.Count - 1)
                        {
                            List<PlotPoint> plotPoints = lists[num + 1];
                            foreach (PlotPoint plotPoint in plotPoints)
                            {
                                if (!this.SelectedPoint.Links.Contains(plotPoint.ID))
                                {
                                    continue;
                                }
                                this.SelectedPoint = plotPoints[0];
                                break;
                            }
                        }
                        flag = true;
                    }
                    else if (key == Keys.Left)
                    {
                        List<PlotPoint> item1 = lists[num];
                        int num1 = item1.IndexOf(this.SelectedPoint);
                        if (num1 != 0)
                        {
                            this.SelectedPoint = item1[num1 - 1];
                        }
                        flag = true;
                    }
                    else if (key != Keys.Right)
                    {
                        return false;
                    }
                    else
                    {
                        List<PlotPoint> plotPoints1 = lists[num];
                        int num2 = plotPoints1.IndexOf(this.SelectedPoint);
                        if (num2 != plotPoints1.Count - 1)
                        {
                            this.SelectedPoint = plotPoints1[num2 + 1];
                        }
                        flag = true;
                    }
                }
                else
                {
                    flag = false;
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
                return false;
            }
            return flag;
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            try
            {
                PlotPoint data = e.Data.GetData(typeof(PlotPoint)) as PlotPoint;
                if (this.fHoverPoint == null)
                {
                    if (this.fDragLocation != null)
                    {
                        this.fPlot.Points.Remove(data);
                        if (this.fDragLocation.RHS == null)
                        {
                            this.fPlot.Points.Add(data);
                            this.OnPlotLayoutChanged();
                        }
                        else
                        {
                            int num = this.fPlot.Points.IndexOf(this.fDragLocation.RHS);
                            this.fPlot.Points.Insert(num, data);
                            this.OnPlotLayoutChanged();
                        }
                    }
                    this.fDragLocation = null;
                }
                else if (this.allow_drop(data, this.fHoverPoint))
                {
                    this.fHoverPoint.Links.Add(data.ID);
                    this.OnPlotLayoutChanged();
                }
                this.do_layout_calculations();
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
            }
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            try
            {
                Point client = base.PointToClient(System.Windows.Forms.Cursor.Position);
                this.fHoverPoint = this.find_point_at(client);
                PlotPoint data = e.Data.GetData(typeof(PlotPoint)) as PlotPoint;
                e.Effect = DragDropEffects.None;
                if (this.fHoverPoint == null)
                {
                    this.fDragLocation = this.allow_drop(data, client);
                    if (this.fDragLocation != null)
                    {
                        e.Effect = DragDropEffects.Move;
                    }
                }
                else if (this.allow_drop(data, this.fHoverPoint))
                {
                    e.Effect = DragDropEffects.Move;
                }
                base.Invalidate();
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            try
            {
                if (this.fMode != PlotViewMode.Plain)
                {
                    Point client = base.PointToClient(System.Windows.Forms.Cursor.Position);
                    if (this.fUpRect.Contains(client))
                    {
                        PlotPoint plotPoint = Session.Project.FindParent(this.fPlot);
                        if (plotPoint != null)
                        {
                            Masterplan.Data.Plot plot = Session.Project.FindParent(plotPoint);
                            if (plot != null)
                            {
                                this.Plot = plot;
                                this.OnPlotChanged();
                            }
                        }
                    }
                    if (this.fDownRect.Contains(client) && this.fSelectedPoint != null)
                    {
                        this.Plot = this.fSelectedPoint.Subplot;
                        this.OnPlotChanged();
                    }
                    PlotPoint plotPoint1 = this.find_point_at(client);
                    if (this.fSelectedPoint != plotPoint1)
                    {
                        this.fSelectedPoint = plotPoint1;
                        base.Invalidate();
                        this.OnSelectionChanged();
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
                this.fHoverPoint = null;
                base.Invalidate();
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
                if (this.fMode != PlotViewMode.Plain)
                {
                    Point client = base.PointToClient(System.Windows.Forms.Cursor.Position);
                    PlotPoint plotPoint = this.find_point_at(client);
                    if (this.fHoverPoint != plotPoint)
                    {
                        this.fHoverPoint = plotPoint;
                        this.set_tooltip();
                    }
                    if (e.Button == System.Windows.Forms.MouseButtons.Left && this.fSelectedPoint != null)
                    {
                        base.DoDragDrop(this.fSelectedPoint, DragDropEffects.All);
                    }
                    base.Invalidate();
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Point[] pointArray;
            PointF[] pointFArray;
            try
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                if (this.fLayers == null)
                {
                    this.do_layout_calculations();
                }
                if (this.fMode != PlotViewMode.Plain)
                {
                    Color color = Color.FromArgb(240, 240, 240);
                    Color color1 = Color.FromArgb(170, 170, 170);
                    using (Brush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, color, color1, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
                    }
                    Point client = base.PointToClient(System.Windows.Forms.Cursor.Position);
                    PlotPoint plotPoint = Session.Project.FindParent(this.fPlot);
                    if (plotPoint != null)
                    {
                        using (System.Drawing.Font font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size * 2f))
                        {
                            Graphics graphics = e.Graphics;
                            string name = plotPoint.Name;
                            Brush darkGray = Brushes.DarkGray;
                            Rectangle clientRectangle = base.ClientRectangle;
                            float left = (float)(clientRectangle.Left + 10);
                            clientRectangle = base.ClientRectangle;
                            graphics.DrawString(name, font, darkGray, left, (float)(clientRectangle.Top + 10));
                        }
                    }
                    Color color2 = (plotPoint == null ? Color.DarkGray : Color.Black);
                    Color color3 = (this.fSelectedPoint == null ? Color.DarkGray : Color.Black);
                    using (Pen pen = new Pen(color2))
                    {
                        using (Pen pen1 = new Pen(color3))
                        {
                            using (Brush solidBrush = new SolidBrush(color2))
                            {
                                using (Brush brush = new SolidBrush(color3))
                                {
                                    Point point = new Point(this.fUpRect.Left + 5, this.fUpRect.Bottom - 5);
                                    Point point1 = new Point(this.fUpRect.Right - 5, this.fUpRect.Bottom - 5);
                                    Point point2 = new Point((this.fUpRect.Right + this.fUpRect.Left) / 2, this.fUpRect.Top + 5);
                                    if (!this.fUpRect.Contains(client))
                                    {
                                        Graphics graphic = e.Graphics;
                                        pointArray = new Point[] { point, point1, point2 };
                                        graphic.DrawPolygon(pen, pointArray);
                                    }
                                    else
                                    {
                                        Graphics graphics1 = e.Graphics;
                                        pointArray = new Point[] { point, point1, point2 };
                                        graphics1.FillPolygon(solidBrush, pointArray);
                                    }
                                    Point point3 = new Point(this.fDownRect.Left + 5, this.fDownRect.Top + 5);
                                    Point point4 = new Point(this.fDownRect.Right - 5, this.fDownRect.Top + 5);
                                    Point point5 = new Point((this.fDownRect.Right + this.fDownRect.Left) / 2, this.fDownRect.Bottom - 5);
                                    if (!this.fDownRect.Contains(client))
                                    {
                                        Graphics graphic1 = e.Graphics;
                                        pointArray = new Point[] { point3, point4, point5 };
                                        graphic1.DrawPolygon(pen1, pointArray);
                                    }
                                    else
                                    {
                                        Graphics graphics2 = e.Graphics;
                                        pointArray = new Point[] { point3, point4, point5 };
                                        graphics2.FillPolygon(brush, pointArray);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    e.Graphics.FillRectangle(SystemBrushes.Window, base.ClientRectangle);
                }
                if (Session.Project == null)
                {
                    string str = "(no project)";
                    e.Graphics.DrawString(str, this.Font, SystemBrushes.WindowText, base.ClientRectangle, this.fCentred);
                }
                else if (this.fPlot == null || this.fPlot.Points.Count == 0)
                {
                    string str1 = "(no plot points)";
                    e.Graphics.DrawString(str1, this.Font, SystemBrushes.WindowText, base.ClientRectangle, this.fCentred);
                }
                else
                {
                    if (this.fDragLocation != null && this.fHoverPoint == null)
                    {
                        float single = this.fDragLocation.Rect.Left + this.fDragLocation.Rect.Width / 2f;
                        using (Pen pen2 = new Pen(Color.DarkBlue, 2f))
                        {
                            e.Graphics.DrawLine(pen2, single, this.fDragLocation.Rect.Top, single, this.fDragLocation.Rect.Bottom);
                        }
                        float single1 = 3f;
                        using (Pen pen3 = new Pen(Color.DarkBlue, 1f))
                        {
                            PointF pointF = new PointF(single - single1, this.fDragLocation.Rect.Top);
                            PointF pointF1 = new PointF(single + single1, this.fDragLocation.Rect.Top);
                            e.Graphics.DrawLine(pen3, pointF, pointF1);
                            PointF pointF2 = new PointF(single - single1, this.fDragLocation.Rect.Bottom);
                            PointF pointF3 = new PointF(single + single1, this.fDragLocation.Rect.Bottom);
                            e.Graphics.DrawLine(pen3, pointF2, pointF3);
                        }
                    }
                    if (this.fShowLevels)
                    {
                        for (int i = 0; i != this.fLayers.Count; i++)
                        {
                            List<PlotPoint> item = this.fLayers[i];
                            int totalXP = Workspace.GetTotalXP(item[0]);
                            int layerXP = totalXP + Workspace.GetLayerXP(item);
                            int heroLevel = Experience.GetHeroLevel(totalXP / Session.Project.Party.Size);
                            int num = Experience.GetHeroLevel(layerXP / Session.Project.Party.Size);
                            if (heroLevel != num)
                            {
                                PlotPoint item1 = item[0];
                                RectangleF rectangleF = this.fRegions[item1.ID];
                                int num1 = this.fLayers.IndexOf(item);
                                float height = rectangleF.Height * ((float)(num1 * 2) + 2.5f);
                                PointF pointF4 = new PointF(0f, height);
                                PointF pointF5 = new PointF((float)base.Width, height);
                                e.Graphics.DrawLine(SystemPens.ControlDarkDark, pointF4, pointF5);
                                PointF pointF6 = new PointF(0f, height - (float)this.Font.Height);
                                e.Graphics.DrawString(string.Concat("Level ", num), this.Font, SystemBrushes.WindowText, pointF6);
                            }
                        }
                    }
                    foreach (PlotPoint plotPoint1 in this.fPlot.Points)
                    {
                        if (!this.fRegions.ContainsKey(plotPoint1.ID))
                        {
                            continue;
                        }
                        foreach (Guid link in plotPoint1.Links)
                        {
                            if (!this.fRegions.ContainsKey(link))
                            {
                                continue;
                            }
                            RectangleF rectangleF1 = this.fRegions[plotPoint1.ID];
                            RectangleF item2 = this.fRegions[link];
                            PointF pointF7 = new PointF(rectangleF1.X + rectangleF1.Width / 2f, rectangleF1.Bottom);
                            PointF pointF8 = new PointF(rectangleF1.X + rectangleF1.Width / 2f, rectangleF1.Bottom + 12f);
                            PointF pointF9 = new PointF(item2.X + item2.Width / 2f, item2.Top - 12f);
                            PointF pointF10 = new PointF(pointF9.X, pointF9.Y + 6f);
                            PointF pointF11 = new PointF(item2.X + item2.Width / 2f, item2.Top);
                            int num2 = 130;
                            float single2 = 2f;
                            if (!this.draw_link(plotPoint1, this.fPlot.FindPoint(link)))
                            {
                                num2 = 60;
                                single2 = 0.5f;
                                pointF10 = new PointF(pointF9.X, item2.Top);
                            }
                            else
                            {
                                PointF pointF12 = new PointF(pointF10.X - 6f, pointF10.Y);
                                PointF pointF13 = new PointF(pointF10.X + 6f, pointF10.Y);
                                PointF pointF14 = new PointF(pointF10.X, pointF10.Y + 6f);
                                Graphics graphic2 = e.Graphics;
                                Brush window = SystemBrushes.Window;
                                pointFArray = new PointF[] { pointF12, pointF13, pointF14 };
                                graphic2.FillPolygon(window, pointFArray);
                                Graphics graphics3 = e.Graphics;
                                Pen maroon = Pens.Maroon;
                                pointFArray = new PointF[] { pointF12, pointF13, pointF14 };
                                graphics3.DrawPolygon(maroon, pointFArray);
                            }
                            using (Pen pen4 = new Pen(Color.FromArgb(num2, Color.Maroon), single2))
                            {
                                e.Graphics.DrawLine(pen4, pointF7, pointF8);
                                e.Graphics.DrawLine(pen4, pointF9, pointF10);
                                switch (this.fLinkStyle)
                                {
                                    case PlotViewLinkStyle.Curved:
                                        {
                                            bool flag = false;
                                            if (this.fLinkPaths.ContainsKey(plotPoint1.ID))
                                            {
                                                Dictionary<Guid, List<PointF>> guids = this.fLinkPaths[plotPoint1.ID];
                                                if (guids.ContainsKey(link))
                                                {
                                                    List<PointF> pointFs = guids[link];
                                                    e.Graphics.DrawCurve(pen4, pointFs.ToArray());
                                                    flag = true;
                                                }
                                            }
                                            if (flag)
                                            {
                                                break;
                                            }
                                            e.Graphics.DrawLine(pen4, pointF8, pointF9);
                                            break;
                                        }
                                    case PlotViewLinkStyle.Angled:
                                        {
                                            bool flag1 = false;
                                            if (this.fLinkPaths.ContainsKey(plotPoint1.ID))
                                            {
                                                Dictionary<Guid, List<PointF>> guids1 = this.fLinkPaths[plotPoint1.ID];
                                                if (guids1.ContainsKey(link))
                                                {
                                                    List<PointF> pointFs1 = guids1[link];
                                                    e.Graphics.DrawLines(pen4, pointFs1.ToArray());
                                                    flag1 = true;
                                                }
                                            }
                                            if (flag1)
                                            {
                                                break;
                                            }
                                            e.Graphics.DrawLine(pen4, pointF8, pointF9);
                                            break;
                                        }
                                    case PlotViewLinkStyle.Straight:
                                        {
                                            e.Graphics.DrawLine(pen4, pointF8, pointF9);
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    foreach (PlotPoint plotPoint2 in this.fPlot.Points)
                    {
                        if (!this.fRegions.ContainsKey(plotPoint2.ID))
                        {
                            continue;
                        }
                        RectangleF rectangleF2 = this.fRegions[plotPoint2.ID];
                        int num3 = 255;
                        if (plotPoint2.State != PlotPointState.Normal)
                        {
                            num3 = 50;
                        }
                        Brush white = null;
                        if (plotPoint2 != this.fSelectedPoint)
                        {
                            Pair<Color, Color> colourGradient = PlotView.GetColourGradient(plotPoint2.Colour, num3);
                            white = new LinearGradientBrush(rectangleF2, colourGradient.First, colourGradient.Second, LinearGradientMode.Vertical);
                        }
                        else
                        {
                            white = Brushes.White;
                        }
                        Pen red = (plotPoint2 == this.fHoverPoint ? SystemPens.Highlight : SystemPens.WindowText);
                        System.Drawing.Font font1 = (plotPoint2 != this.fSelectedPoint ? this.Font : new System.Drawing.Font(this.Font, this.Font.Style | FontStyle.Bold));
                        if (plotPoint2.State == PlotPointState.Skipped)
                        {
                            font1 = new System.Drawing.Font(font1, this.Font.Style | FontStyle.Strikeout);
                        }
                        if (plotPoint2.Element != null)
                        {
                            int partyLevel = Workspace.GetPartyLevel(plotPoint2);
                            Difficulty difficulty = plotPoint2.Element.GetDifficulty(partyLevel, Session.Project.Party.Size);
                            if ((difficulty == Difficulty.Trivial || difficulty == Difficulty.Extreme) && plotPoint2 != this.fSelectedPoint)
                            {
                                white = new SolidBrush(Color.FromArgb(num3, Color.Pink));
                                red = Pens.Red;
                            }
                        }
                        if (!this.draw_point(plotPoint2))
                        {
                            using (Pen pen5 = new Pen(Color.FromArgb(60, red.Color)))
                            {
                                e.Graphics.DrawRectangle(red, rectangleF2.X, rectangleF2.Y, rectangleF2.Width, rectangleF2.Height);
                            }
                        }
                        else
                        {
                            Brush windowText = SystemBrushes.WindowText;
                            if (plotPoint2.State == PlotPointState.Normal)
                            {
                                RectangleF rectangleF3 = new RectangleF(rectangleF2.Location, rectangleF2.Size);
                                rectangleF3.Offset(3f, 4f);
                                using (Brush solidBrush1 = new SolidBrush(Color.FromArgb(100, Color.Black)))
                                {
                                    e.Graphics.FillRectangle(solidBrush1, rectangleF3);
                                }
                            }
                            else if (plotPoint2 != this.fSelectedPoint)
                            {
                                windowText = new SolidBrush(Color.FromArgb(num3, Color.Black));
                            }
                            e.Graphics.FillRectangle(white, rectangleF2);
                            e.Graphics.DrawRectangle(red, rectangleF2.X, rectangleF2.Y, rectangleF2.Width, rectangleF2.Height);
                            float size = font1.Size;
                            while (e.Graphics.MeasureString(plotPoint2.Name, font1, (int)rectangleF2.Width).Height > rectangleF2.Height)
                            {
                                size *= 0.95f;
                                font1 = new System.Drawing.Font(font1.FontFamily, size, font1.Style);
                            }
                            e.Graphics.DrawString(plotPoint2.Name, font1, windowText, rectangleF2, this.fCentred);
                            if (plotPoint2.Subplot.Points.Count > 0)
                            {
                                rectangleF2 = RectangleF.Inflate(rectangleF2, -2f, -2f);
                                e.Graphics.DrawRectangle(red, rectangleF2.X, rectangleF2.Y, rectangleF2.Width, rectangleF2.Height);
                            }
                            if (!(plotPoint2.Details != "") && !(plotPoint2.ReadAloud != ""))
                            {
                                continue;
                            }
                            double num4 = Math.Atan(0.25) * 2;
                            float single3 = 20f - (float)(20 * Math.Cos(num4));
                            float single4 = (float)(20 * Math.Sin(num4));
                            PointF pointF15 = new PointF(rectangleF2.Right - 20f, rectangleF2.Bottom);
                            PointF pointF16 = new PointF(rectangleF2.Right, rectangleF2.Bottom - 5f);
                            PointF pointF17 = new PointF(rectangleF2.Right - single3, rectangleF2.Bottom - single4);
                            PointF pointF18 = new PointF(rectangleF2.Right, rectangleF2.Bottom);
                            Graphics graphic3 = e.Graphics;
                            Pen gray = Pens.Gray;
                            pointFArray = new PointF[] { pointF17, pointF16, pointF15 };
                            graphic3.DrawPolygon(gray, pointFArray);
                            using (Brush brush1 = new SolidBrush(Color.FromArgb(80, 0, 0, 0)))
                            {
                                Graphics graphics4 = e.Graphics;
                                pointFArray = new PointF[] { pointF16, pointF15, pointF18 };
                                graphics4.FillPolygon(brush1, pointFArray);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
            }
        }

        protected void OnPlotChanged()
        {
            try
            {
                if (this.PlotChanged != null)
                {
                    this.PlotChanged(this, new EventArgs());
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
            }
        }

        protected void OnPlotLayoutChanged()
        {
            try
            {
                if (this.PlotLayoutChanged != null)
                {
                    this.PlotLayoutChanged(this, new EventArgs());
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            try
            {
                base.OnResize(e);
                this.clear_layout_calculations();
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
            }
        }

        protected void OnSelectionChanged()
        {
            try
            {
                if (this.SelectionChanged != null)
                {
                    this.SelectionChanged(this, new EventArgs());
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
            }
        }

        public void RecalculateLayout()
        {
            this.clear_layout_calculations();
        }

        private void set_tooltip()
        {
            try
            {
                if (!this.fShowTooltips || this.fHoverPoint == null)
                {
                    this.Tooltip.ToolTipTitle = "";
                    this.Tooltip.ToolTipIcon = ToolTipIcon.None;
                    this.Tooltip.SetToolTip(this, null);
                }
                else
                {
                    List<string> strs = new List<string>();
                    if (this.fHoverPoint.Element != null)
                    {
                        if (this.fHoverPoint.Element is Encounter)
                        {
                            Encounter element = this.fHoverPoint.Element as Encounter;
                            string str = string.Concat("Encounter: ", element.GetXP(), " XP");
                            foreach (EncounterSlot slot in element.Slots)
                            {
                                ICreature creature = Session.FindCreature(slot.Card.CreatureID, SearchType.Global);
                                if (creature == null)
                                {
                                    continue;
                                }
                                str = string.Concat(str, Environment.NewLine, creature.Name);
                                if (slot.CombatData.Count <= 1)
                                {
                                    continue;
                                }
                                object obj = str;
                                object[] count = new object[] { obj, " (x", slot.CombatData.Count, ")" };
                                str = string.Concat(count);
                            }
                            foreach (Trap trap in element.Traps)
                            {
                                string str1 = str;
                                string[] newLine = new string[] { str1, Environment.NewLine, trap.Name, ": ", trap.Info };
                                str = string.Concat(newLine);
                            }
                            foreach (SkillChallenge skillChallenge in element.SkillChallenges)
                            {
                                string str2 = str;
                                string[] strArrays = new string[] { str2, Environment.NewLine, skillChallenge.Name, ": ", skillChallenge.Info };
                                str = string.Concat(strArrays);
                            }
                            strs.Add(str);
                        }
                        if (this.fHoverPoint.Element is TrapElement)
                        {
                            TrapElement trapElement = this.fHoverPoint.Element as TrapElement;
                            object[] name = new object[] { trapElement.Trap.Name, ": ", trapElement.GetXP(), " XP" };
                            string str3 = string.Concat(name);
                            string[] newLine1 = new string[] { str3, Environment.NewLine, trapElement.Trap.Info, " ", trapElement.Trap.Type.ToString().ToLower() };
                            strs.Add(string.Concat(newLine1));
                        }
                        if (this.fHoverPoint.Element is SkillChallenge)
                        {
                            SkillChallenge element1 = this.fHoverPoint.Element as SkillChallenge;
                            object[] objArray = new object[] { element1.Name, ": ", element1.GetXP(), " XP" };
                            string str4 = string.Concat(objArray);
                            str4 = string.Concat(str4, Environment.NewLine, element1.Info);
                            strs.Add(str4);
                        }
                        if (this.fHoverPoint.Element is Quest)
                        {
                            Quest quest = this.fHoverPoint.Element as Quest;
                            string str5 = "";
                            switch (quest.Type)
                            {
                                case QuestType.Major:
                                    {
                                        str5 = string.Concat("Major quest: ", quest.GetXP(), " XP");
                                        break;
                                    }
                                case QuestType.Minor:
                                    {
                                        str5 = string.Concat("Minor quest: ", quest.GetXP(), " XP");
                                        break;
                                    }
                            }
                            strs.Add(str5);
                        }
                    }
                    string str6 = "";
                    foreach (Parcel parcel in this.fHoverPoint.Parcels)
                    {
                        if (str6 != "")
                        {
                            str6 = string.Concat(str6, ", ");
                        }
                        str6 = string.Concat(str6, parcel.Name);
                    }
                    if (str6 != "")
                    {
                        strs.Add(string.Concat("Treasure parcels: ", str6));
                    }
                    string str7 = "";
                    foreach (string str8 in strs)
                    {
                        if (str7 != "")
                        {
                            str7 = string.Concat(str7, Environment.NewLine, Environment.NewLine);
                        }
                        str7 = string.Concat(str7, TextHelper.Wrap(str8));
                    }
                    this.Tooltip.ToolTipTitle = this.fHoverPoint.Name;
                    this.Tooltip.ToolTipIcon = ToolTipIcon.Info;
                    this.Tooltip.SetToolTip(this, str7);
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
            }
        }

        [Category("Property Changed")]
        [Description("Occurs when the current plot changes.")]
        public event EventHandler PlotChanged;

        [Category("Property Changed")]
        [Description("Occurs when the plot layout changes.")]
        public event EventHandler PlotLayoutChanged;

        [Category("Property Changed")]
        [Description("Occurs when the selected point changes.")]
        public event EventHandler SelectionChanged;

        private class DragLocation
        {
            public PlotPoint LHS;

            public PlotPoint RHS;

            public RectangleF Rect;

            public DragLocation()
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

            this.components = new System.ComponentModel.Container();
            this.Tooltip = new ToolTip(this.components);
            base.SuspendLayout();
            this.AllowDrop = true;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Name = "PlotView";
            base.ResumeLayout(false);

        }

        #endregion
    }
}
