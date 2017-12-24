using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class Goal
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fDetails = "";

		private List<Goal> fPrerequisites = new List<Goal>();

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

		public List<Goal> Prerequisites
		{
			get
			{
				return this.fPrerequisites;
			}
			set
			{
				this.fPrerequisites = value;
			}
		}

		public List<Goal> Subtree
		{
			get
			{
				List<Goal> goals = new List<Goal>()
				{
					this
				};
				foreach (Goal fPrerequisite in this.fPrerequisites)
				{
					goals.AddRange(fPrerequisite.Subtree);
				}
				return goals;
			}
		}

		public Goal()
		{
		}

		public Goal(string name)
		{
			this.fName = name;
		}

		public Goal Copy()
		{
			Goal goal = new Goal()
			{
				ID = this.fID,
				Name = this.fName,
				Details = this.fDetails
			};
			foreach (Goal fPrerequisite in this.fPrerequisites)
			{
				goal.Prerequisites.Add(fPrerequisite.Copy());
			}
			return goal;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}