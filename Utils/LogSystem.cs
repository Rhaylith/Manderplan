using System;
using System.IO;

namespace Utils
{
	public class LogSystem
	{
		private static string fLogFile;

		private static int fIndent;

		public static int Indent
		{
			get
			{
				return LogSystem.fIndent;
			}
			set
			{
				LogSystem.fIndent = value;
			}
		}

		public static string LogFile
		{
			get
			{
				return LogSystem.fLogFile;
			}
			set
			{
				LogSystem.fLogFile = value;
			}
		}

		static LogSystem()
		{
			LogSystem.fLogFile = "";
			LogSystem.fIndent = 0;
		}

		public LogSystem()
		{
		}

		public static void Trace(string message)
		{
			try
			{
				string str = "";
				for (int i = 0; i < LogSystem.fIndent; i++)
				{
					str = string.Concat(str, "\t");
				}
				str = string.Concat(str, message, Environment.NewLine);
				Console.Write(str);
				if (LogSystem.fLogFile != null && LogSystem.fLogFile != "")
				{
					try
					{
						string str1 = string.Concat(DateTime.Now, "\t", str);
						File.AppendAllText(LogSystem.fLogFile, str1);
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
		}

		public static void Trace(object obj)
		{
			try
			{
				LogSystem.Trace(obj.ToString());
			}
			catch
			{
			}
		}

		public static void Trace(Exception ex)
		{
			try
			{
				LogSystem.Trace(ex.Message);
				LogSystem.Trace(ex.StackTrace);
				while (ex.InnerException != null)
				{
					ex = ex.InnerException;
					LogSystem.Indent = LogSystem.Indent + 1;
					LogSystem.Trace(ex);
					LogSystem.Indent = LogSystem.Indent - 1;
				}
			}
			catch
			{
			}
		}
	}
}