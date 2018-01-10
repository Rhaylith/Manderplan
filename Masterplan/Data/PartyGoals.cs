using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class PartyGoals
	{
		private List<Goal> fGoals = new List<Goal>();

		public List<Goal> Goals
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

		public PartyGoals()
		{
		}

		public PartyGoals Copy()
		{
			PartyGoals partyGoal = new PartyGoals();
			foreach (Goal fGoal in this.fGoals)
			{
				partyGoal.Goals.Add(fGoal.Copy());
			}
			return partyGoal;
		}

		private List<Goal> find_list(Goal target, List<Goal> list)
		{
			List<Goal> goals;
			if (list.Contains(target))
			{
				return list;
			}
			List<Goal>.Enumerator enumerator = list.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					List<Goal> goals1 = this.find_list(target, enumerator.Current.Prerequisites);
					if (goals1 == null)
					{
						continue;
					}
					goals = goals1;
					return goals;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		public List<Goal> FindList(Goal goal)
		{
			return this.find_list(goal, this.fGoals);
		}
	}
}