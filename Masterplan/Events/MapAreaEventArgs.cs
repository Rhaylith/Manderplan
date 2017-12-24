using Masterplan.Data;
using System;

namespace Masterplan.Events
{
	public class MapAreaEventArgs : EventArgs
	{
		private Masterplan.Data.MapArea fArea;

		public Masterplan.Data.MapArea MapArea
		{
			get
			{
				return this.fArea;
			}
		}

		public MapAreaEventArgs(Masterplan.Data.MapArea area)
		{
			this.fArea = area;
		}
	}
}