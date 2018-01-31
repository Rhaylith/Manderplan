using System.Drawing;

namespace Masterplan.Data.Combat.Visibility
{
    public class VisibilityMap
    {
        Visibility.OcclusionLevel[,] map;

        public int Width = 0;
        public int Height = 0;

        private OcclusionLevel DefaultVisibility = OcclusionLevel.Visible;

        public VisibilityMap(int sizex, int sizey)
        {
            Width = sizex;
            Height = sizey;
            map = new Visibility.OcclusionLevel[sizex, sizey];
        }

        public VisibilityMap(OcclusionLevel defaultVis)
        {
            Width = 0;
            Height = 0;
            map = null;
            DefaultVisibility = defaultVis;
        }

        public Visibility.OcclusionLevel this[int row, int col]
        {
            get { return map[row, col]; }
            set { map[row, col] = value; }
        }

        public OcclusionLevel this[CombatData data]
        {
            get { return map == null ? this.DefaultVisibility : map[data.Location.X, data.Location.Y]; }
        }

        public OcclusionLevel this[Point point]
        {
            get { return map == null ? this.DefaultVisibility : map[point.X, point.Y]; }
        }

        public bool IsVisible(CombatData data)
        {
            return this[data] != OcclusionLevel.Obscured;
        }

        public bool IsVisible(Point point)
        {
            return this[point] != OcclusionLevel.Obscured;
        }

    }
}
