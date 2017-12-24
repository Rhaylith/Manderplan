using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class ThemePowerData
	{
		private CreaturePower fPower = new CreaturePower();

		private PowerType fType;

		private List<RoleType> fRoles = new List<RoleType>();

		public CreaturePower Power
		{
			get
			{
				return this.fPower;
			}
			set
			{
				this.fPower = value;
			}
		}

		public List<RoleType> Roles
		{
			get
			{
				return this.fRoles;
			}
			set
			{
				this.fRoles = value;
			}
		}

		public PowerType Type
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

		public ThemePowerData()
		{
		}

		public ThemePowerData Copy()
		{
			ThemePowerData themePowerDatum = new ThemePowerData()
			{
				Power = this.fPower.Copy(),
				Type = this.fType
			};
			foreach (RoleType fRole in this.fRoles)
			{
				themePowerDatum.Roles.Add(fRole);
			}
			return themePowerDatum;
		}

		public override string ToString()
		{
			return this.fPower.Name;
		}
	}
}