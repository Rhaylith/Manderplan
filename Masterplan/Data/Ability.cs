using System;

namespace Masterplan.Data
{
	[Serializable]
	public class Ability
	{
		private int fScore = 10;

		public int Cost
		{
			get
			{
				return Ability.GetCost(this.fScore);
			}
		}

		public int Modifier
		{
			get
			{
				return Ability.GetModifier(this.fScore);
			}
		}

		public int Score
		{
			get
			{
				return this.fScore;
			}
			set
			{
				this.fScore = value;
			}
		}

		public Ability()
		{
		}

		public Ability Copy()
		{
			return new Ability()
			{
				Score = this.fScore
			};
		}

		public static int GetCost(int score)
		{
			if (score < 10)
			{
				return 0;
			}
			switch (score)
			{
				case 10:
				{
					return 0;
				}
				case 11:
				{
					return 1;
				}
				case 12:
				{
					return 2;
				}
				case 13:
				{
					return 3;
				}
				case 14:
				{
					return 5;
				}
				case 15:
				{
					return 7;
				}
				case 16:
				{
					return 9;
				}
				case 17:
				{
					return 12;
				}
				case 18:
				{
					return 16;
				}
			}
			return -1;
		}

		public static int GetModifier(int score)
		{
			return score / 2 - 5;
		}

		public override string ToString()
		{
			string str = this.fScore.ToString();
			string str1 = this.Modifier.ToString();
			if (this.Modifier >= 0)
			{
				str1 = string.Concat("+", str1);
			}
			return string.Concat(str, " (", str1, ")");
		}
	}
}