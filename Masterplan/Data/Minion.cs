using System;

namespace Masterplan.Data
{
	[Serializable]
	public class Minion : IRole
	{
		private bool fHasRole;

		private RoleType fType;

		public bool HasRole
		{
			get
			{
				return this.fHasRole;
			}
			set
			{
				this.fHasRole = value;
			}
		}

		public RoleType Type
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

		public Minion()
		{
		}

		public IRole Copy()
		{
			Minion minion = new Minion()
			{
				HasRole = this.fHasRole,
				Type = this.fType
			};
			return minion;
		}

		public override string ToString()
		{
			if (!this.fHasRole)
			{
				return "Minion";
			}
			return string.Concat("Minion ", this.fType);
		}
	}
}