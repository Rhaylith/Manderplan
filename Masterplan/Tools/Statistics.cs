using Masterplan.Data;
using System;

namespace Masterplan.Tools
{
	internal class Statistics
	{
		public Statistics()
		{
		}

		public static int AC(int level, IRole role)
		{
			int num = 14;
			if (role != null && role is ComplexRole)
			{
				RoleType type = (role as ComplexRole).Type;
				switch (type)
				{
					case RoleType.Artillery:
					case RoleType.Brute:
					{
						num = 12;
						break;
					}
					case RoleType.Blaster:
					{
						break;
					}
					default:
					{
						if (type == RoleType.Soldier)
						{
							num = 16;
							break;
						}
						else
						{
							break;
						}
					}
				}
			}
			return level + num;
		}

		public static int AttackBonus(DefenceType defence, int level, IRole role)
		{
			if (defence == DefenceType.AC)
			{
				return level + 5;
			}
			if (role != null && role is ComplexRole && (role as ComplexRole).Type == RoleType.Soldier)
			{
				return level + 5;
			}
			return level + 3;
		}

		public static string Damage(int level, DamageExpressionType det)
		{
			if (level < 1)
			{
				level = 1;
			}
			switch (det)
			{
				case DamageExpressionType.Normal:
				{
					return Statistics.NormalDamage(level);
				}
				case DamageExpressionType.Multiple:
				{
					return Statistics.MultipleDamage(level);
				}
				case DamageExpressionType.Minion:
				{
					return Statistics.MinionDamage(level).ToString();
				}
			}
			return null;
		}

		public static int HP(int level, ComplexRole role, int constitution_score)
		{
			int num = 8;
			if (role != null)
			{
				switch (role.Type)
				{
					case RoleType.Artillery:
					case RoleType.Lurker:
					{
						num = 6;
						break;
					}
					case RoleType.Brute:
					{
						num = 10;
						break;
					}
				}
			}
			int constitutionScore = constitution_score + (level + 1) * num;
			if (role != null)
			{
				switch (role.Flag)
				{
					case RoleFlag.Elite:
					{
						constitutionScore *= 2;
						break;
					}
					case RoleFlag.Solo:
					{
						constitutionScore *= 4;
						break;
					}
				}
			}
			return constitutionScore;
		}

		public static int Initiative(int level, IRole role)
		{
			int num = level / 2;
			if (role != null && role is ComplexRole)
			{
				RoleType type = (role as ComplexRole).Type;
				if (type != RoleType.Brute)
				{
					switch (type)
					{
						case RoleType.Skirmisher:
						{
							break;
						}
						case RoleType.Soldier:
						{
							num += 4;
							return num;
						}
						default:
						{
							return num;
						}
					}
				}
				num += 2;
			}
			return num;
		}

		public static int MinionDamage(int level)
		{
			switch (level)
			{
				case 1:
				case 2:
				case 3:
				{
					return 4;
				}
				case 4:
				case 5:
				case 6:
				{
					return 5;
				}
				case 7:
				case 8:
				case 9:
				{
					return 6;
				}
				case 10:
				case 11:
				case 12:
				{
					return 8;
				}
				case 13:
				case 14:
				case 15:
				{
					return 9;
				}
				case 16:
				case 17:
				case 18:
				{
					return 10;
				}
				case 19:
				case 20:
				case 21:
				{
					return 11;
				}
				case 22:
				case 23:
				case 24:
				{
					return 13;
				}
				case 25:
				case 26:
				case 27:
				{
					return 14;
				}
				case 28:
				case 29:
				case 30:
				{
					return 15;
				}
			}
			return 15;
		}

		public static string MultipleDamage(int level)
		{
			switch (level)
			{
				case 1:
				{
					return "1d6 +3";
				}
				case 2:
				{
					return "1d6 +4";
				}
				case 3:
				{
					return "1d6 +5";
				}
				case 4:
				{
					return "1d8 +5";
				}
				case 5:
				{
					return "1d8 +6";
				}
				case 6:
				{
					return "1d8 +6";
				}
				case 7:
				{
					return "2d6 +4";
				}
				case 8:
				{
					return "2d6 +5";
				}
				case 9:
				{
					return "2d6 +6";
				}
				case 10:
				{
					return "2d6 +6";
				}
				case 11:
				{
					return "2d6 +7";
				}
				case 12:
				{
					return "2d8 +6";
				}
				case 13:
				{
					return "2d8 +7";
				}
				case 14:
				{
					return "2d8 +7";
				}
				case 15:
				{
					return "2d8 +8";
				}
				case 16:
				{
					return "3d6 +8";
				}
				case 17:
				{
					return "3d6 +9";
				}
				case 18:
				{
					return "3d6 +9";
				}
				case 19:
				{
					return "3d6 +10";
				}
				case 20:
				{
					return "3d6 +11";
				}
				case 21:
				{
					return "3d8 +9";
				}
				case 22:
				{
					return "3d8 +9";
				}
				case 23:
				{
					return "3d8 +10";
				}
				case 24:
				{
					return "3d8 +11";
				}
				case 25:
				{
					return "3d8 +12";
				}
				case 26:
				{
					return "4d6 +11";
				}
				case 27:
				{
					return "4d6 +12";
				}
				case 28:
				{
					return "4d6 +13";
				}
				case 29:
				{
					return "4d6 +14";
				}
				case 30:
				{
					return "4d6 +15";
				}
			}
			return "4d6 +15";
		}

		public static int NAD(int level, IRole role)
		{
			return 12 + level;
		}

		public static string NormalDamage(int level)
		{
			switch (level)
			{
				case 1:
				{
					return "1d8 +4";
				}
				case 2:
				{
					return "1d8 +5";
				}
				case 3:
				{
					return "1d8 +6";
				}
				case 4:
				{
					return "2d6 +5";
				}
				case 5:
				{
					return "2d6 +6";
				}
				case 6:
				{
					return "2d6 +7";
				}
				case 7:
				{
					return "2d8 +6";
				}
				case 8:
				{
					return "2d8 +7";
				}
				case 9:
				{
					return "2d8 +8";
				}
				case 10:
				{
					return "2d8 +9";
				}
				case 11:
				{
					return "3d6 +9";
				}
				case 12:
				{
					return "3d6 +10";
				}
				case 13:
				{
					return "3d6 +11";
				}
				case 14:
				{
					return "3d6 +12";
				}
				case 15:
				{
					return "3d6 +13";
				}
				case 16:
				{
					return "3d8 +11";
				}
				case 17:
				{
					return "3d8 +12";
				}
				case 18:
				{
					return "3d8 +13";
				}
				case 19:
				{
					return "3d8 +14";
				}
				case 20:
				{
					return "3d8 +15";
				}
				case 21:
				{
					return "4d6 +15";
				}
				case 22:
				{
					return "4d6 +16";
				}
				case 23:
				{
					return "4d6 +17";
				}
				case 24:
				{
					return "4d6 +18";
				}
				case 25:
				{
					return "4d6 +19";
				}
				case 26:
				{
					return "4d8 +16";
				}
				case 27:
				{
					return "4d8 +17";
				}
				case 28:
				{
					return "4d8 +18";
				}
				case 29:
				{
					return "4d8 +19";
				}
				case 30:
				{
					return "4d8 +20";
				}
			}
			return "4d8 +20";
		}
	}
}