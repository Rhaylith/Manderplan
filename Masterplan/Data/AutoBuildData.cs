using Masterplan;
using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class AutoBuildData
	{
		private Masterplan.Data.Difficulty fDifficulty;

		private int fLevel = Session.Project.Party.Level;

		private int fSize = Session.Project.Party.Size;

		private string fType = "";

		private List<string> fCategories;

		private List<string> fKeywords = new List<string>();

		public List<string> Categories
		{
			get
			{
				return this.fCategories;
			}
			set
			{
				this.fCategories = value;
			}
		}

		public Masterplan.Data.Difficulty Difficulty
		{
			get
			{
				return this.fDifficulty;
			}
			set
			{
				this.fDifficulty = value;
			}
		}

		public List<string> Keywords
		{
			get
			{
				return this.fKeywords;
			}
			set
			{
				this.fKeywords = value;
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

		public int Size
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

		public string Type
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

		public AutoBuildData()
		{
		}

		public AutoBuildData Copy()
		{
			AutoBuildData autoBuildDatum = new AutoBuildData()
			{
				Difficulty = this.fDifficulty,
				Level = this.fLevel,
				Size = this.fSize,
				Type = this.fType
			};
			if (this.fKeywords != null)
			{
				autoBuildDatum.Keywords = new List<string>();
				autoBuildDatum.Keywords.AddRange(this.fKeywords);
			}
			if (this.fCategories != null)
			{
				autoBuildDatum.Categories = new List<string>();
				autoBuildDatum.Categories.AddRange(this.fCategories);
			}
			return autoBuildDatum;
		}
	}
}