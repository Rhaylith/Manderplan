using Masterplan.Data;
using System;
using System.Collections.Generic;

namespace Masterplan
{
	internal class HeroData
	{
		public RaceData Race;

		public ClassData Class;

		public HeroData(RaceData rd, ClassData cd)
		{
			this.Race = rd;
			this.Class = cd;
		}

		public Hero ConvertToHero()
		{
			Hero hero = new Hero()
			{
				Name = string.Concat(this.Race.Name, " ", this.Class.Name),
				Class = this.Class.Name,
				Role = this.Class.Role,
				PowerSource = this.Class.PowerSource.ToString(),
				Race = this.Race.Name
			};
			return hero;
		}

		public static HeroData Create()
		{
			int num = Session.Random.Next() % Sourcebook.Classes.Count;
			ClassData item = Sourcebook.Classes[num];
			int num1 = Session.Random.Next() % Sourcebook.Races.Count;
			return new HeroData(Sourcebook.Races[num1], item);
		}
	}
}