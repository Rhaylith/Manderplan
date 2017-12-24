using Masterplan;
using System;
using System.Collections;

namespace Masterplan.Tools
{
	internal class DiceExpression
	{
		private int fThrows;

		private int fSides;

		private int fConstant;

		public double Average
		{
			get
			{
				double num = (double)(this.fSides + 1) / 2;
				return (double)this.fThrows * num + (double)this.fConstant;
			}
		}

		public int Constant
		{
			get
			{
				return this.fConstant;
			}
			set
			{
				this.fConstant = value;
			}
		}

		public int Maximum
		{
			get
			{
				return this.fThrows * this.fSides + this.fConstant;
			}
		}

		public int Sides
		{
			get
			{
				return this.fSides;
			}
			set
			{
				this.fSides = value;
			}
		}

		public int Throws
		{
			get
			{
				return this.fThrows;
			}
			set
			{
				this.fThrows = value;
			}
		}

		public DiceExpression()
		{
			this.fThrows = 0;
			this.fSides = 0;
			this.fConstant = 0;
		}

		public DiceExpression(int throws, int sides)
		{
			this.fThrows = throws;
			this.fSides = sides;
			this.fConstant = 0;
		}

		public DiceExpression(int throws, int sides, int constant)
		{
			this.fThrows = throws;
			this.fSides = sides;
			this.fConstant = constant;
		}

		public DiceExpression Adjust(int level_adjustment)
		{
			Array values = Enum.GetValues(typeof(DamageExpressionType));
			int num = 2147483647;
			int num1 = 0;
			DamageExpressionType damageExpressionType = DamageExpressionType.Normal;
			DiceExpression diceExpression = null;
			for (int i = 1; i <= 30; i++)
			{
				foreach (DamageExpressionType value in values)
				{
					DiceExpression diceExpression1 = DiceExpression.Parse(Statistics.Damage(i, value));
					int num2 = Math.Abs(this.fThrows - diceExpression1.Throws);
					int num3 = Math.Abs(this.fSides - diceExpression1.Sides) / 2;
					int num4 = Math.Abs(this.fConstant - diceExpression1.Constant);
					int num5 = num2 * 10 + num3 * 100 + num4;
					if (num5 >= num)
					{
						continue;
					}
					num = num5;
					num1 = i;
					damageExpressionType = value;
					diceExpression = diceExpression1;
				}
			}
			if (diceExpression == null)
			{
				return this;
			}
			int throws = this.fThrows - diceExpression.Throws;
			int sides = this.fSides - diceExpression.Sides;
			int constant = this.fConstant - diceExpression.Constant;
			int num6 = Math.Max(num1 + level_adjustment, 1);
			DiceExpression diceExpression2 = DiceExpression.Parse(Statistics.Damage(num6, damageExpressionType));
			DiceExpression throws1 = diceExpression2;
			throws1.Throws = throws1.Throws + throws;
			DiceExpression sides1 = diceExpression2;
			sides1.Sides = sides1.Sides + sides;
			DiceExpression constant1 = diceExpression2;
			constant1.Constant = constant1.Constant + constant;
			if (this.fThrows != 0)
			{
				diceExpression2.Throws = Math.Max(diceExpression2.Throws, 1);
			}
			else
			{
				diceExpression2.Throws = 0;
			}
			switch (diceExpression2.Sides)
			{
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
				{
					diceExpression2.Sides = 4;
					break;
				}
				case 5:
				case 6:
				{
					diceExpression2.Sides = 6;
					break;
				}
				case 7:
				case 8:
				{
					diceExpression2.Sides = 8;
					break;
				}
				case 9:
				case 10:
				{
					diceExpression2.Sides = 10;
					break;
				}
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				{
					diceExpression2.Sides = 12;
					break;
				}
				default:
				{
					diceExpression2.Sides = 20;
					break;
				}
			}
			return diceExpression2;
		}

		public int Evaluate()
		{
			return Session.Dice(this.fThrows, this.fSides) + this.fConstant;
		}

		public static DiceExpression Parse(string str)
		{
			DiceExpression diceExpression = new DiceExpression();
			try
			{
				bool flag = false;
				bool flag1 = false;
				char[] chrArray = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
				str = str.ToLower();
				str = str.Replace("+", " + ");
				str = str.Replace("-", " - ");
				string[] strArrays = str.Split(null);
				for (int i = 0; i < (int)strArrays.Length; i++)
				{
					string str1 = strArrays[i];
					if (str1 == "damage" || str1 == "dmg")
					{
						break;
					}
					if (str1 == "-" && flag)
					{
						flag1 = true;
					}
					else if (str1.IndexOfAny(chrArray) != -1)
					{
						int num = str1.IndexOf("d");
						if (num != -1)
						{
							string str2 = str1.Substring(0, num);
							string str3 = str1.Substring(num + 1);
							if (str2 != "")
							{
								diceExpression.Throws = int.Parse(str2);
							}
							diceExpression.Sides = int.Parse(str3);
						}
						else if (diceExpression.Constant == 0)
						{
							diceExpression.Constant = int.Parse(str1);
							if (flag1)
							{
								diceExpression.Constant = -diceExpression.Constant;
							}
						}
						flag = true;
					}
				}
			}
			catch
			{
				diceExpression = null;
			}
			if (diceExpression != null && diceExpression.Throws == 0 && diceExpression.Constant == 0)
			{
				diceExpression = null;
			}
			return diceExpression;
		}

		public override string ToString()
		{
			string str = "";
			if (this.fThrows != 0)
			{
				str = string.Concat(this.fThrows, "d", this.fSides);
			}
			if (this.fConstant != 0)
			{
				if (str != "")
				{
					str = string.Concat(str, " ");
					if (this.fConstant > 0)
					{
						str = string.Concat(str, "+");
					}
				}
				str = string.Concat(str, this.fConstant.ToString());
			}
			if (str == "")
			{
				str = "0";
			}
			return str;
		}
	}
}