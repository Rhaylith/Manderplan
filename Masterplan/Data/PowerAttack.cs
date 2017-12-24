using System;

namespace Masterplan.Data
{
	[Serializable]
	public class PowerAttack
	{
		private int fBonus;

		private DefenceType fDefence;

		public int Bonus
		{
			get
			{
				return this.fBonus;
			}
			set
			{
				this.fBonus = value;
			}
		}

		public DefenceType Defence
		{
			get
			{
				return this.fDefence;
			}
			set
			{
				this.fDefence = value;
			}
		}

		public PowerAttack()
		{
		}

		public PowerAttack Copy()
		{
			PowerAttack powerAttack = new PowerAttack()
			{
				Bonus = this.fBonus,
				Defence = this.fDefence
			};
			return powerAttack;
		}

		public override string ToString()
		{
			string str = (this.fBonus >= 0 ? "+" : "");
			object[] objArray = new object[] { str, this.fBonus, " vs ", this.fDefence };
			return string.Concat(objArray);
		}
	}
}