using Masterplan;
using Masterplan.Tools;
using System;

namespace Masterplan.Data
{
	[Serializable]
	public class DifficultyIssue : IIssue
	{
		private PlotPoint fPoint;

		public PlotPoint Point
		{
			get
			{
				return this.fPoint;
			}
		}

		public string Reason
		{
			get
			{
				if (this.fPoint.State != PlotPointState.Normal)
				{
					return "";
				}
				if (this.fPoint.Element == null)
				{
					return "";
				}
				string str = "game element";
				if (this.fPoint.Element is Encounter)
				{
					str = "encounter";
				}
				if (this.fPoint.Element is TrapElement)
				{
					str = ((this.fPoint.Element as TrapElement).Trap.Type == TrapType.Trap ? "trap" : "hazard");
				}
				if (this.fPoint.Element is SkillChallenge)
				{
					str = "skill challenge";
				}
				if (this.fPoint.Element is Quest)
				{
					str = "quest";
				}
				int partyLevel = Workspace.GetPartyLevel(this.fPoint);
				Difficulty difficulty = this.fPoint.Element.GetDifficulty(partyLevel, Session.Project.Party.Size);
				Difficulty difficulty1 = difficulty;
				if (difficulty1 == Difficulty.Trivial)
				{
					object[] objArray = new object[] { "This ", str, " is too easy for a party of level ", partyLevel, "." };
					return string.Concat(objArray);
				}
				if (difficulty1 != Difficulty.Extreme)
				{
					return "";
				}
				object[] objArray1 = new object[] { "This ", str, " is too difficult for a party of level ", partyLevel, "." };
				return string.Concat(objArray1);
			}
		}

		public DifficultyIssue(PlotPoint point)
		{
			this.fPoint = point;
		}

		public override string ToString()
		{
			return string.Concat(this.fPoint.Name, ": ", this.Reason);
		}
	}
}