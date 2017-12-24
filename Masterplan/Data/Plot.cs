using System;
using System.Collections.Generic;
using Utils;

namespace Masterplan.Data
{
	[Serializable]
	public class Plot
	{
		private List<PlotPoint> fPoints = new List<PlotPoint>();

		private PartyGoals fGoals = new PartyGoals();

		private FiveByFiveData f5x5 = new FiveByFiveData();

		public List<PlotPoint> AllPlotPoints
		{
			get
			{
				List<PlotPoint> plotPoints = new List<PlotPoint>();
				foreach (PlotPoint fPoint in this.fPoints)
				{
					plotPoints.Add(fPoint);
					plotPoints.AddRange(fPoint.Subplot.AllPlotPoints);
				}
				return plotPoints;
			}
		}

		public FiveByFiveData FiveByFive
		{
			get
			{
				return this.f5x5;
			}
			set
			{
				this.f5x5 = value;
			}
		}

		public PartyGoals Goals
		{
			get
			{
				return this.fGoals;
			}
			set
			{
				this.fGoals = value;
			}
		}

		public List<PlotPoint> Points
		{
			get
			{
				return this.fPoints;
			}
			set
			{
				this.fPoints = value;
			}
		}

		public Plot()
		{
		}

		public Plot Copy()
		{
			Plot plot = new Plot();
			foreach (PlotPoint fPoint in this.fPoints)
			{
				plot.Points.Add(fPoint.Copy());
			}
			plot.Goals = this.fGoals.Copy();
			plot.FiveByFive = this.f5x5.Copy();
			return plot;
		}

		public PlotPoint FindPoint(Guid id)
		{
			PlotPoint plotPoint;
			List<PlotPoint>.Enumerator enumerator = this.fPoints.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					PlotPoint current = enumerator.Current;
					if (current.ID != id)
					{
						continue;
					}
					plotPoint = current;
					return plotPoint;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return plotPoint;
		}

		public PlotPoint FindPointForMapArea(Map map, MapArea area)
		{
			PlotPoint plotPoint;
			List<PlotPoint>.Enumerator enumerator = this.fPoints.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					PlotPoint current = enumerator.Current;
					Map map1 = null;
					MapArea mapArea = null;
					current.GetTacticalMapArea(ref map1, ref mapArea);
					if (map1 != map || mapArea != area)
					{
						continue;
					}
					plotPoint = current;
					return plotPoint;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return plotPoint;
		}

		public List<PlotPoint> FindPrerequisites(Guid point_id)
		{
			List<PlotPoint> plotPoints = new List<PlotPoint>();
			foreach (PlotPoint fPoint in this.fPoints)
			{
				if (!fPoint.Links.Contains(point_id))
				{
					continue;
				}
				plotPoints.Add(fPoint);
			}
			return plotPoints;
		}

		public List<Guid> FindRegionalMaps()
		{
			BinarySearchTree<Guid> binarySearchTree = new BinarySearchTree<Guid>();
			foreach (PlotPoint fPoint in this.fPoints)
			{
				if (!(fPoint.RegionalMapID != Guid.Empty) || !(fPoint.MapLocationID != Guid.Empty))
				{
					continue;
				}
				binarySearchTree.Add(fPoint.RegionalMapID);
			}
			List<Guid> sortedList = binarySearchTree.SortedList;
			sortedList.Remove(Guid.Empty);
			return sortedList;
		}

		public List<PlotPoint> FindSubtree(PlotPoint pp)
		{
			List<PlotPoint> plotPoints = new List<PlotPoint>()
			{
				pp
			};
			foreach (Guid link in pp.Links)
			{
				plotPoints.AddRange(this.FindSubtree(this.FindPoint(link)));
			}
			return plotPoints;
		}

		public List<Guid> FindTacticalMaps()
		{
			BinarySearchTree<Guid> binarySearchTree = new BinarySearchTree<Guid>();
			foreach (PlotPoint fPoint in this.fPoints)
			{
				if (fPoint.Element == null)
				{
					continue;
				}
				if (fPoint.Element is Encounter)
				{
					Encounter element = fPoint.Element as Encounter;
					if (element.MapID != Guid.Empty && element.MapAreaID != Guid.Empty)
					{
						binarySearchTree.Add(element.MapID);
					}
				}
				if (fPoint.Element is TrapElement)
				{
					TrapElement trapElement = fPoint.Element as TrapElement;
					if (trapElement.MapID != Guid.Empty && trapElement.MapAreaID != Guid.Empty)
					{
						binarySearchTree.Add(trapElement.MapID);
					}
				}
				if (fPoint.Element is SkillChallenge)
				{
					SkillChallenge skillChallenge = fPoint.Element as SkillChallenge;
					if (skillChallenge.MapID != Guid.Empty && skillChallenge.MapAreaID != Guid.Empty)
					{
						binarySearchTree.Add(skillChallenge.MapID);
					}
				}
				if (!(fPoint.Element is MapElement))
				{
					continue;
				}
				MapElement mapElement = fPoint.Element as MapElement;
				if (mapElement.MapID == Guid.Empty)
				{
					continue;
				}
				binarySearchTree.Add(mapElement.MapID);
			}
			List<Guid> sortedList = binarySearchTree.SortedList;
			sortedList.Remove(Guid.Empty);
			return sortedList;
		}

		public void RemovePoint(PlotPoint point)
		{
			List<Guid> guids = new List<Guid>();
			foreach (PlotPoint fPoint in this.fPoints)
			{
				if (!fPoint.Links.Contains(point.ID))
				{
					continue;
				}
				while (fPoint.Links.Contains(point.ID))
				{
					fPoint.Links.Remove(point.ID);
				}
				foreach (Guid link in point.Links)
				{
					if (fPoint.Links.Contains(link))
					{
						continue;
					}
					fPoint.Links.Add(link);
				}
			}
			this.fPoints.Remove(point);
		}
	}
}