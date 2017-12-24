using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class LevelData
	{
		private int fLevel;

		private List<Feature> fFeatures = new List<Feature>();

		private List<PlayerPower> fPowers = new List<PlayerPower>();

		public int Count
		{
			get
			{
				return this.fFeatures.Count + this.fPowers.Count;
			}
		}

		public List<Feature> Features
		{
			get
			{
				return this.fFeatures;
			}
			set
			{
				this.fFeatures = value;
			}
		}

		public int Level
		{
			get
			{
				return this.fLevel;
			}
			set
			{
				this.fLevel = value;
			}
		}

		public List<PlayerPower> Powers
		{
			get
			{
				return this.fPowers;
			}
			set
			{
				this.fPowers = value;
			}
		}

		public LevelData()
		{
		}

		public LevelData Copy()
		{
			LevelData levelDatum = new LevelData()
			{
				Level = this.fLevel
			};
			foreach (Feature fFeature in this.fFeatures)
			{
				levelDatum.Features.Add(fFeature.Copy());
			}
			foreach (PlayerPower fPower in this.fPowers)
			{
				levelDatum.Powers.Add(fPower.Copy());
			}
			return levelDatum;
		}

		public override string ToString()
		{
			string str = "";
			foreach (Feature fFeature in this.fFeatures)
			{
				if (str != "")
				{
					str = string.Concat(str, "; ");
				}
				str = string.Concat(str, fFeature.Name);
			}
			foreach (PlayerPower fPower in this.fPowers)
			{
				if (str != "")
				{
					str = string.Concat(str, "; ");
				}
				str = string.Concat(str, fPower.Name);
			}
			if (str == "")
			{
				str = "(empty)";
			}
			if (this.fLevel < 1)
			{
				return str;
			}
			object[] objArray = new object[] { "Level ", this.fLevel, ": ", str };
			return string.Concat(objArray);
		}
	}
}