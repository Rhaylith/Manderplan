using Masterplan.Data;
using System;
using System.Collections.Generic;

namespace Masterplan.Wizards
{
	internal class VariantData
	{
		public ICreature BaseCreature;

		public List<CreatureTemplate> Templates = new List<CreatureTemplate>();

		public int SelectedRoleIndex;

		public List<RoleType> Roles
		{
			get
			{
				List<RoleType> roleTypes = new List<RoleType>();
				if (this.BaseCreature != null && this.BaseCreature.Role is ComplexRole)
				{
					roleTypes.Add((this.BaseCreature.Role as ComplexRole).Type);
				}
				foreach (CreatureTemplate template in this.Templates)
				{
					if (roleTypes.Contains(template.Role))
					{
						continue;
					}
					roleTypes.Add(template.Role);
				}
				roleTypes.Sort();
				return roleTypes;
			}
		}

		public VariantData()
		{
		}
	}
}