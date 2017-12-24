using System;
using System.Collections.Generic;

namespace Masterplan.Tools
{
	internal class DiceStatistics
	{
		public DiceStatistics()
		{
		}

		public static string Expression(List<int> dice, int constant)
		{
			int num = 0;
			int num1 = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
		Label1:
			foreach (int num6 in dice)
			{
				int num7 = num6;
				switch (num7)
				{
					case 4:
					{
						num++;
						continue;
					}
					case 5:
					case 7:
					case 9:
					case 11:
					{
						continue;
					}
					case 6:
					{
						num1++;
						continue;
					}
					case 8:
					{
						num2++;
						continue;
					}
					case 10:
					{
						num3++;
						continue;
					}
					case 12:
					{
						num4++;
						continue;
					}
					default:
					{
						if (num7 == 20)
						{
							break;
						}
						else
						{
							goto Label1;
						}
					}
				}
				num5++;
			}
			string str = "";
			if (num != 0)
			{
				if (str != "")
				{
					str = string.Concat(str, " + ");
				}
				str = string.Concat(str, num, "d4");
			}
			if (num1 != 0)
			{
				if (str != "")
				{
					str = string.Concat(str, " + ");
				}
				str = string.Concat(str, num1, "d6");
			}
			if (num2 != 0)
			{
				if (str != "")
				{
					str = string.Concat(str, " + ");
				}
				str = string.Concat(str, num2, "d8");
			}
			if (num3 != 0)
			{
				if (str != "")
				{
					str = string.Concat(str, " + ");
				}
				str = string.Concat(str, num3, "d10");
			}
			if (num4 != 0)
			{
				if (str != "")
				{
					str = string.Concat(str, " + ");
				}
				str = string.Concat(str, num4, "d12");
			}
			if (num5 != 0)
			{
				if (str != "")
				{
					str = string.Concat(str, " + ");
				}
				str = string.Concat(str, num5, "d20");
			}
			if (constant != 0)
			{
				str = string.Concat(str, " ");
				if (constant > 0)
				{
					str = string.Concat(str, "+");
				}
				str = string.Concat(str, constant.ToString());
			}
			return str;
		}

		public static Dictionary<int, int> Odds(List<int> dice, int constant)
		{
			Dictionary<int, int> nums = new Dictionary<int, int>();
			if (dice.Count > 0)
			{
				int num = 1;
				foreach (int num1 in dice)
				{
					num *= num1;
				}
				int[] item = new int[dice.Count];
				item[dice.Count - 1] = 1;
				for (int i = dice.Count - 2; i >= 0; i--)
				{
					item[i] = item[i + 1] * dice[i + 1];
				}
				for (int j = 0; j != num; j++)
				{
					List<int> nums1 = new List<int>();
					for (int k = 0; k != dice.Count; k++)
					{
						int item1 = dice[k];
						int num2 = j / item[k] % item1 + 1;
						nums1.Add(num2);
					}
					int num3 = constant;
					foreach (int num4 in nums1)
					{
						num3 += num4;
					}
					if (!nums.ContainsKey(num3))
					{
						nums[num3] = 0;
					}
					Dictionary<int, int> item2 = nums;
					Dictionary<int, int> nums2 = item2;
					int num5 = num3;
					item2[num5] = nums2[num5] + 1;
				}
			}
			return nums;
		}
	}
}