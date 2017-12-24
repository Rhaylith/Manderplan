using System;

namespace Masterplan.Data
{
	[Serializable]
	public class ComplexRole : IRole
	{
		private RoleType fType;

		private RoleFlag fFlag;

		private bool fLeader;

		public RoleFlag Flag
		{
			get
			{
				return this.fFlag;
			}
			set
			{
				this.fFlag = value;
			}
		}

		public bool Leader
		{
			get
			{
				return this.fLeader;
			}
			set
			{
				this.fLeader = value;
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

		public ComplexRole()
		{
		}

		public ComplexRole(RoleType type)
		{
			this.fType = type;
		}

		public IRole Copy()
		{
			ComplexRole complexRole = new ComplexRole()
			{
				Type = this.fType,
				Flag = this.fFlag,
				Leader = this.fLeader
			};
			return complexRole;
		}

		public override string ToString()
		{
			string str = "";
			switch (this.fFlag)
			{
				case RoleFlag.Elite:
				{
					str = "Elite ";
					break;
				}
				case RoleFlag.Solo:
				{
					str = "Solo ";
					break;
				}
			}
			string str1 = this.fType.ToString();
			return string.Concat(str, str1, (this.fLeader ? " (L)" : ""));
		}
	}
}