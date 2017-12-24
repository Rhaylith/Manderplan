using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class DamageModifier
	{
		private DamageType fType = DamageType.Fire;

		private int fValue = -5;

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

		public int Value
		{
			get
			{
				return this.fValue;
			}
			set
			{
				this.fValue = value;
			}
		}

		public DamageModifier()
		{
		}

		public DamageModifier Copy()
		{
			DamageModifier damageModifier = new DamageModifier()
			{
				Type = this.fType,
				Value = this.fValue
			};
			return damageModifier;
		}

		public static DamageModifier Parse(string damage_type, int value)
		{
			DamageModifier damageModifier;
			string[] names = Enum.GetNames(typeof(DamageType));
			List<string> strs = new List<string>();
			string[] strArrays = names;
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				strs.Add(strArrays[i]);
			}
			try
			{
				DamageModifier damageModifier1 = new DamageModifier()
				{
					Type = (DamageType)Enum.Parse(typeof(DamageType), damage_type, true),
					Value = value
				};
				damageModifier = damageModifier1;
			}
			catch
			{
				return null;
			}
			return damageModifier;
		}

		public override string ToString()
		{
			if (this.fValue == 0)
			{
				return string.Concat("Immune to ", this.fType.ToString().ToLower());
			}
			string str = (this.fValue < 0 ? "Resist" : "Vulnerable");
			int num = Math.Abs(this.fValue);
			object[] lower = new object[] { str, " ", num, " ", this.fType.ToString().ToLower() };
			return string.Concat(lower);
		}
	}
}