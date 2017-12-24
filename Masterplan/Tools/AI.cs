using Masterplan.Data;
using System;

namespace Masterplan.Tools
{
	internal class AI
	{
		public AI()
		{
		}

		public static string ExtractDamage(string source)
		{
			string str;
			string[] newLine = new string[] { ",", ";", ".", ":", Environment.NewLine };
			string[] strArrays = source.Split(newLine, StringSplitOptions.RemoveEmptyEntries);
			string[] strArrays1 = strArrays;
			int num = 0;
			while (true)
			{
				if (num < (int)strArrays1.Length)
				{
					string lower = strArrays1[num].Trim().ToLower();
					if (lower.Contains("damage") || lower.Contains("dmg"))
					{
						str = lower;
						break;
					}
					else
					{
						num++;
					}
				}
				else
				{
					string[] strArrays2 = strArrays;
					int num1 = 0;
					while (num1 < (int)strArrays2.Length)
					{
						string str1 = strArrays2[num1];
						if (DiceExpression.Parse(str1) == null)
						{
							num1++;
						}
						else
						{
							str = str1;
							return str;
						}
					}
					return "";
				}
			}
			return str;
		}

		public static int GetSkillDC(Difficulty diff, int level)
		{
			switch (diff)
			{
				case Difficulty.Easy:
				{
					switch (level)
					{
						case 1:
						{
							return 8;
						}
						case 2:
						{
							return 9;
						}
						case 3:
						{
							return 9;
						}
						case 4:
						{
							return 10;
						}
						case 5:
						{
							return 10;
						}
						case 6:
						{
							return 11;
						}
						case 7:
						{
							return 11;
						}
						case 8:
						{
							return 12;
						}
						case 9:
						{
							return 12;
						}
						case 10:
						{
							return 13;
						}
						case 11:
						{
							return 13;
						}
						case 12:
						{
							return 14;
						}
						case 13:
						{
							return 14;
						}
						case 14:
						{
							return 15;
						}
						case 15:
						{
							return 15;
						}
						case 16:
						{
							return 16;
						}
						case 17:
						{
							return 16;
						}
						case 18:
						{
							return 17;
						}
						case 19:
						{
							return 17;
						}
						case 20:
						{
							return 18;
						}
						case 21:
						{
							return 19;
						}
						case 22:
						{
							return 20;
						}
						case 23:
						{
							return 20;
						}
						case 24:
						{
							return 21;
						}
						case 25:
						{
							return 21;
						}
						case 26:
						{
							return 22;
						}
						case 27:
						{
							return 22;
						}
						case 28:
						{
							return 23;
						}
						case 29:
						{
							return 23;
						}
						case 30:
						{
							return 24;
						}
					}
					break;
				}
				case Difficulty.Moderate:
				{
					switch (level)
					{
						case 1:
						{
							return 12;
						}
						case 2:
						{
							return 13;
						}
						case 3:
						{
							return 13;
						}
						case 4:
						{
							return 14;
						}
						case 5:
						{
							return 15;
						}
						case 6:
						{
							return 15;
						}
						case 7:
						{
							return 16;
						}
						case 8:
						{
							return 16;
						}
						case 9:
						{
							return 17;
						}
						case 10:
						{
							return 18;
						}
						case 11:
						{
							return 19;
						}
						case 12:
						{
							return 20;
						}
						case 13:
						{
							return 20;
						}
						case 14:
						{
							return 21;
						}
						case 15:
						{
							return 22;
						}
						case 16:
						{
							return 22;
						}
						case 17:
						{
							return 23;
						}
						case 18:
						{
							return 23;
						}
						case 19:
						{
							return 24;
						}
						case 20:
						{
							return 25;
						}
						case 21:
						{
							return 26;
						}
						case 22:
						{
							return 27;
						}
						case 23:
						{
							return 27;
						}
						case 24:
						{
							return 28;
						}
						case 25:
						{
							return 29;
						}
						case 26:
						{
							return 29;
						}
						case 27:
						{
							return 30;
						}
						case 28:
						{
							return 30;
						}
						case 29:
						{
							return 31;
						}
						case 30:
						{
							return 32;
						}
					}
					break;
				}
				case Difficulty.Hard:
				{
					switch (level)
					{
						case 1:
						{
							return 19;
						}
						case 2:
						{
							return 20;
						}
						case 3:
						{
							return 21;
						}
						case 4:
						{
							return 21;
						}
						case 5:
						{
							return 22;
						}
						case 6:
						{
							return 23;
						}
						case 7:
						{
							return 23;
						}
						case 8:
						{
							return 24;
						}
						case 9:
						{
							return 25;
						}
						case 10:
						{
							return 26;
						}
						case 11:
						{
							return 27;
						}
						case 12:
						{
							return 28;
						}
						case 13:
						{
							return 29;
						}
						case 14:
						{
							return 29;
						}
						case 15:
						{
							return 30;
						}
						case 16:
						{
							return 31;
						}
						case 17:
						{
							return 31;
						}
						case 18:
						{
							return 32;
						}
						case 19:
						{
							return 33;
						}
						case 20:
						{
							return 34;
						}
						case 21:
						{
							return 35;
						}
						case 22:
						{
							return 36;
						}
						case 23:
						{
							return 37;
						}
						case 24:
						{
							return 37;
						}
						case 25:
						{
							return 38;
						}
						case 26:
						{
							return 39;
						}
						case 27:
						{
							return 39;
						}
						case 28:
						{
							return 40;
						}
						case 29:
						{
							return 41;
						}
						case 30:
						{
							return 42;
						}
					}
					break;
				}
			}
			return 0;
		}

		public static Difficulty GetSkillDifficulty(int dc, int party_level)
		{
			int skillDC = AI.GetSkillDC(Difficulty.Easy, party_level);
			int num = AI.GetSkillDC(Difficulty.Moderate, party_level);
			int skillDC1 = AI.GetSkillDC(Difficulty.Hard, party_level);
			int num1 = skillDC1 + (skillDC1 - skillDC) / 2;
			if (dc < skillDC)
			{
				return Difficulty.Trivial;
			}
			if (dc < num)
			{
				return Difficulty.Easy;
			}
			if (dc < skillDC1)
			{
				return Difficulty.Moderate;
			}
			if (dc < num1)
			{
				return Difficulty.Hard;
			}
			return Difficulty.Extreme;
		}

		public static Difficulty GetThreatDifficulty(int threat_level, int party_level)
		{
			int threatLevel = threat_level - party_level;
			if (threatLevel > 5)
			{
				return Difficulty.Extreme;
			}
			if (threatLevel < -3)
			{
				return Difficulty.Trivial;
			}
			return Difficulty.Easy;
		}
	}
}