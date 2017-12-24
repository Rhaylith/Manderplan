using Masterplan;
using Masterplan.Tools;
using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class PlotPoint
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private PlotPointState fState;

		private PlotPointColour fColour;

		private string fDetails = "";

		private string fReadAloud = "";

		private List<Guid> fLinks = new List<Guid>();

		private Plot fSubplot = new Plot();

		private IElement fElement;

		private List<Parcel> fParcels = new List<Parcel>();

		private List<Guid> fEncyclopediaEntries = new List<Guid>();

		private CalendarDate fDate;

		private Guid fRegionalMapID = Guid.Empty;

		private Guid fMapLocationID = Guid.Empty;

		private int fAdditionalXP;

		public int AdditionalXP
		{
			get
			{
				return this.fAdditionalXP;
			}
			set
			{
				this.fAdditionalXP = value;
			}
		}

		public PlotPointColour Colour
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

		public CalendarDate Date
		{
			get
			{
				return this.fDate;
			}
			set
			{
				this.fDate = value;
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

		public IElement Element
		{
			get
			{
				return this.fElement;
			}
			set
			{
				this.fElement = value;
			}
		}

		public List<Guid> EncyclopediaEntryIDs
		{
			get
			{
				return this.fEncyclopediaEntries;
			}
			set
			{
				this.fEncyclopediaEntries = value;
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

		public List<Guid> Links
		{
			get
			{
				return this.fLinks;
			}
			set
			{
				this.fLinks = value;
			}
		}

		public Guid MapLocationID
		{
			get
			{
				return this.fMapLocationID;
			}
			set
			{
				this.fMapLocationID = value;
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

		public List<Parcel> Parcels
		{
			get
			{
				return this.fParcels;
			}
			set
			{
				this.fParcels = value;
			}
		}

		public string ReadAloud
		{
			get
			{
				return this.fReadAloud;
			}
			set
			{
				this.fReadAloud = value;
			}
		}

		public Guid RegionalMapID
		{
			get
			{
				return this.fRegionalMapID;
			}
			set
			{
				this.fRegionalMapID = value;
			}
		}

		public PlotPointState State
		{
			get
			{
				return this.fState;
			}
			set
			{
				this.fState = value;
			}
		}

		public Plot Subplot
		{
			get
			{
				return this.fSubplot;
			}
			set
			{
				this.fSubplot = value;
			}
		}

		public List<PlotPoint> Subtree
		{
			get
			{
				List<PlotPoint> plotPoints = new List<PlotPoint>()
				{
					this
				};
				foreach (PlotPoint point in this.fSubplot.Points)
				{
					plotPoints.AddRange(point.Subtree);
				}
				return plotPoints;
			}
		}

		public PlotPoint()
		{
		}

		public PlotPoint(string name)
		{
			this.fName = name;
		}

		public PlotPoint Copy()
		{
			IElement element;
			CalendarDate calendarDate;
			PlotPoint plotPoint = new PlotPoint()
			{
				ID = this.fID,
				Name = this.fName,
				State = this.fState,
				Colour = this.fColour,
				Details = this.fDetails,
				ReadAloud = this.fReadAloud
			};
			plotPoint.Links.AddRange(this.fLinks);
			plotPoint.Subplot = this.fSubplot.Copy();
			PlotPoint plotPoint1 = plotPoint;
			if (this.fElement != null)
			{
				element = this.fElement.Copy();
			}
			else
			{
				element = null;
			}
			plotPoint1.Element = element;
			PlotPoint plotPoint2 = plotPoint;
			if (this.fDate != null)
			{
				calendarDate = this.fDate.Copy();
			}
			else
			{
				calendarDate = null;
			}
			plotPoint2.Date = calendarDate;
			plotPoint.RegionalMapID = this.fRegionalMapID;
			plotPoint.MapLocationID = this.fMapLocationID;
			plotPoint.AdditionalXP = this.fAdditionalXP;
			foreach (Parcel fParcel in this.fParcels)
			{
				plotPoint.Parcels.Add(fParcel.Copy());
			}
			foreach (Guid fEncyclopediaEntry in this.fEncyclopediaEntries)
			{
				plotPoint.EncyclopediaEntryIDs.Add(fEncyclopediaEntry);
			}
			return plotPoint;
		}

		public void GetRegionalMapArea(ref RegionalMap map, ref MapLocation map_location, Project project)
		{
			if (this.fRegionalMapID != Guid.Empty && this.fMapLocationID != Guid.Empty)
			{
				map = Session.Project.FindRegionalMap(this.fRegionalMapID);
				if (map != null)
				{
					map_location = map.FindLocation(this.fMapLocationID);
				}
			}
		}

		public void GetTacticalMapArea(ref Map map, ref MapArea map_area)
		{
			Guid empty = Guid.Empty;
			Guid mapAreaID = Guid.Empty;
			Encounter encounter = this.fElement as Encounter;
			if (encounter != null)
			{
				empty = encounter.MapID;
				mapAreaID = encounter.MapAreaID;
			}
			SkillChallenge skillChallenge = this.fElement as SkillChallenge;
			if (skillChallenge != null)
			{
				empty = skillChallenge.MapID;
				mapAreaID = skillChallenge.MapAreaID;
			}
			TrapElement trapElement = this.fElement as TrapElement;
			if (trapElement != null)
			{
				empty = trapElement.MapID;
				mapAreaID = trapElement.MapAreaID;
			}
			MapElement mapElement = this.fElement as MapElement;
			if (mapElement != null)
			{
				empty = mapElement.MapID;
				mapAreaID = mapElement.MapAreaID;
			}
			if (empty != Guid.Empty && mapAreaID != Guid.Empty)
			{
				map = Session.Project.FindTacticalMap(empty);
				if (map != null)
				{
					map_area = map.FindArea(mapAreaID);
				}
			}
		}

		public int GetXP()
		{
			int xP = this.fAdditionalXP;
			if (this.fElement != null)
			{
				xP += this.fElement.GetXP();
			}
			if (this.fSubplot.Points.Count != 0)
			{
				foreach (List<PlotPoint> plotPoints in Workspace.FindLayers(this.fSubplot))
				{
					xP += Workspace.GetLayerXP(plotPoints);
				}
			}
			return xP;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}