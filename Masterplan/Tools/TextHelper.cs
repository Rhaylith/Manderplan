using System;
using System.Collections.Generic;

namespace Masterplan.Tools
{
	internal class TextHelper
	{
		private static int LINE_LENGTH;

		private static List<char> fVowels;

		static TextHelper()
		{
			TextHelper.LINE_LENGTH = 50;
			TextHelper.fVowels = null;
		}

		public TextHelper()
		{
		}

		public static string Abbreviation(string title)
		{
			string str = "";
			string[] strArrays = title.Split(null);
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				string str1 = strArrays[i];
				if (str1 != "")
				{
                    int result;
					if (!int.TryParse(str1, out result))
					{
						char chr = str1[0];
						if (char.IsUpper(chr))
						{
							str = string.Concat(str, chr);
						}
					}
					else
					{
						str = string.Concat(str, str1);
					}
				}
			}
			return str;
		}

		public static string Capitalise(string str, bool title_case)
		{
			if (!title_case)
			{
				char chr = str[0];
				return string.Concat(char.ToUpper(chr), str.Substring(1));
			}
			string[] strArrays = str.Split(null);
			str = "";
			string[] strArrays1 = strArrays;
			for (int i = 0; i < (int)strArrays1.Length; i++)
			{
				string str1 = strArrays1[i];
				if (str != "")
				{
					str = string.Concat(str, " ");
				}
				str = string.Concat(str, TextHelper.Capitalise(str1, false));
			}
			return str;
		}

		private static string get_first_line(ref string str)
		{
			string str1 = "";
			int num = Math.Min(TextHelper.LINE_LENGTH, str.Length);
			int num1 = str.IndexOf(" ", num);
			if (num1 != -1)
			{
				str1 = str.Substring(0, num1);
				str = str.Substring(num1 + 1);
			}
			else
			{
				str1 = str;
				str = "";
			}
			return str1;
		}

		public static bool IsVowel(char ch)
		{
			if (TextHelper.fVowels == null)
			{
				TextHelper.fVowels = new List<char>()
				{
					'a',
					'e',
					'i',
					'o',
					'u'
				};
			}
			return TextHelper.fVowels.Contains(ch);
		}

		public static bool StartsWithVowel(string str)
		{
			if (str.Length == 0)
			{
				return false;
			}
			char lower = char.ToLower(str[0]);
			return TextHelper.IsVowel(lower);
		}

		public static string Wrap(string str)
		{
			List<string> strs = new List<string>();
			while (str != "")
			{
				strs.Add(TextHelper.get_first_line(ref str));
			}
			string str1 = "";
			foreach (string str2 in strs)
			{
				if (str1 != "")
				{
					str1 = string.Concat(str1, Environment.NewLine);
				}
				str1 = string.Concat(str1, str2);
			}
			return str1;
		}
	}
}