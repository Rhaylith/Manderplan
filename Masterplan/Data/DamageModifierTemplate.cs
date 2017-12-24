using System;

namespace Masterplan.Data
{
	[Serializable]
	public class DamageModifierTemplate
	{
		private DamageType fType;

		private int fHeroicValue = -5;

		private int fParagonValue = -10;

		private int fEpicValue = -15;

		public int EpicValue
		{
			get
			{
				return this.fEpicValue;
			}
			set
			{
				this.fEpicValue = value;
			}
		}

		public int HeroicValue
		{
			get
			{
				return this.fHeroicValue;
			}
			set
			{
				this.fHeroicValue = value;
			}
		}

		public int ParagonValue
		{
			get
			{
				return this.fParagonValue;
			}
			set
			{
				this.fParagonValue = value;
			}
		}

		public DamageType Type
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

		public DamageModifierTemplate()
		{
		}

		public DamageModifierTemplate Copy()
		{
			DamageModifierTemplate damageModifierTemplate = new DamageModifierTemplate()
			{
				Type = this.fType,
				HeroicValue = this.fHeroicValue,
				ParagonValue = this.fParagonValue,
				EpicValue = this.fEpicValue
			};
			return damageModifierTemplate;
		}

		public override string ToString()
		{
			if (this.fHeroicValue + this.fParagonValue + this.fEpicValue == 0)
			{
				return string.Concat("Immune to ", this.fType.ToString().ToLower());
			}
			string str = (this.fHeroicValue < 0 ? "Resist" : "Vulnerable");
			int num = Math.Abs(this.fHeroicValue);
			int num1 = Math.Abs(this.fParagonValue);
			int num2 = Math.Abs(this.fEpicValue);
			object[] lower = new object[] { str, " ", num, " / ", num1, " / ", num2, " ", this.fType.ToString().ToLower() };
			return string.Concat(lower);
		}
	}
}