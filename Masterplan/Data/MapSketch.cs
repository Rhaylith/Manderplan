using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class MapSketch
	{
		private Color fColour = Color.Black;

		private int fWidth = 3;

		private List<MapSketchPoint> fPoints = new List<MapSketchPoint>();

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

		public List<MapSketchPoint> Points
		{
			get
			{
				return this.fPoints;
			}
		}

		public int Width
		{
			get
			{
				return this.fWidth;
			}
			set
			{
				this.fWidth = value;
			}
		}

		public MapSketch()
		{
		}

		public MapSketch Copy()
		{
			MapSketch mapSketch = new MapSketch()
			{
				Colour = this.fColour,
				Width = this.fWidth
			};
			foreach (MapSketchPoint fPoint in this.fPoints)
			{
				mapSketch.Points.Add(fPoint.Copy());
			}
			return mapSketch;
		}
	}
}