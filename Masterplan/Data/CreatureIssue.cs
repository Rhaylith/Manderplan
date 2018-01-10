using Masterplan.Tools;
using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class CreatureIssue : IIssue
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
				string str;
				if (this.fPoint.State != PlotPointState.Normal)
				{
					return "";
				}
				Encounter element = this.fPoint.Element as Encounter;
				if (element == null)
				{
					return "";
				}
				int partyLevel = Workspace.GetPartyLevel(this.fPoint);
				List<EncounterSlot>.Enumerator enumerator = element.Slots.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						EncounterSlot current = enumerator.Current;
						int level = current.Card.Level - partyLevel;
						if (level >= -4)
						{
							if (level <= 5)
							{
								continue;
							}
							str = string.Concat(current.Card.Title, " is more than five levels higher than the party level.");
							return str;
						}
						else
						{
							str = string.Concat(current.Card.Title, " is more than four levels lower than the party level.");
							return str;
						}
					}
					return "";
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		public CreatureIssue(PlotPoint point)
		{
			this.fPoint = point;
		}

		public override string ToString()
		{
			return string.Concat(this.fPoint.Name, ": ", this.Reason);
		}
	}
}