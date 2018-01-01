using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
	public class StringHelper
	{
		public StringHelper()
		{
		}

		public static string LongestCommonSubstring(string str1, string str2)
		{
			if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
			{
				return "";
			}
			int[,] numArray = new int[str1.Length, str2.Length];
			int num = 0;
			int num1 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < str1.Length; i++)
			{
				for (int j = 0; j < str2.Length; j++)
				{
					if (str1[i] == str2[j])
					{
						if (i == 0 || j == 0)
						{
							numArray[i, j] = 1;
						}
						else
						{
							numArray[i, j] = 1 + numArray[i - 1, j - 1];
						}
						if (numArray[i, j] > num)
						{
							num = numArray[i, j];
							int num2 = i - numArray[i, j] + 1;
							if (num1 != num2)
							{
								num1 = num2;
								stringBuilder.Remove(0, stringBuilder.Length);
								stringBuilder.Append(str1.Substring(num1, i + 1 - num1));
							}
							else
							{
								stringBuilder.Append(str1[i]);
							}
						}
					}
					else
					{
						numArray[i, j] = 0;
					}
				}
			}
			return stringBuilder.ToString();
		}

		public static string LongestCommonToken(string str1, string str2)
		{
			string[] strArrays = str1.Split(null);
			string[] strArrays1 = str2.Split(null);
			List<string> strs = new List<string>();
			string[] strArrays2 = strArrays;
			for (int i = 0; i < (int)strArrays2.Length; i++)
			{
				string str = strArrays2[i];
				string[] strArrays3 = strArrays1;
				for (int j = 0; j < (int)strArrays3.Length; j++)
				{
					string str3 = StringHelper.LongestCommonSubstring(str, strArrays3[j]);
					if (str3 != "")
					{
						strs.Add(str3);
					}
				}
			}
			string str4 = "";
			foreach (string str5 in strs)
			{
				if (str5.Length <= str4.Length)
				{
					continue;
				}
				str4 = str5;
			}
			return str4;
		}
	}
}