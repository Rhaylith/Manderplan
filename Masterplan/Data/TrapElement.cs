using Masterplan.Tools;
using System;

namespace Masterplan.Data
{
	[Serializable]
	public class TrapElement : IElement
	{
		private Masterplan.Data.Trap fTrap = new Masterplan.Data.Trap();

		private Guid fMapID = Guid.Empty;

		private Guid fMapAreaID = Guid.Empty;

		public Guid MapAreaID
		{
			get
			{
				return this.fMapAreaID;
			}
			set
			{
				this.fMapAreaID = value;
			}
		}

		public Guid MapID
		{
			get
			{
				return this.fMapID;
			}
			set
			{
				this.fMapID = value;
			}
		}

		public Masterplan.Data.Trap Trap
		{
			get
			{
				return this.fTrap;
			}
			set
			{
				this.fTrap = value;
			}
		}

		public TrapElement()
		{
		}

		public IElement Copy()
		{
			TrapElement trapElement = new TrapElement()
			{
				Trap = this.fTrap.Copy(),
				MapID = this.fMapID,
				MapAreaID = this.fMapAreaID
			};
			return trapElement;
		}

		public Difficulty GetDifficulty(int party_level, int party_size)
		{
			return AI.GetThreatDifficulty(this.fTrap.Level, party_level);
		}

		public int GetXP()
		{
			return this.fTrap.XP;
		}
	}
}