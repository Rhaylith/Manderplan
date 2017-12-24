using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	internal class RoundLog
	{
		private int fRound;

		private List<TurnLog> fTurns = new List<TurnLog>();

		public int Count
		{
			get
			{
				int count = 0;
				foreach (TurnLog fTurn in this.fTurns)
				{
					count += fTurn.Entries.Count;
				}
				return count;
			}
		}

		public int Round
		{
			get
			{
				return this.fRound;
			}
		}

		public List<TurnLog> Turns
		{
			get
			{
				return this.fTurns;
			}
		}

		public RoundLog(int round)
		{
			this.fRound = round;
		}

		public TurnLog GetTurn(Guid id)
		{
			TurnLog turnLog;
			List<TurnLog>.Enumerator enumerator = this.fTurns.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					TurnLog current = enumerator.Current;
					if (current.ID != id)
					{
						continue;
					}
					turnLog = current;
					return turnLog;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return turnLog;
		}
	}
}