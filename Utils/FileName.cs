using System;
using System.IO;

namespace Utils
{
	public static class FileName
	{
		public static void Change(string oldname, string newname)
		{
			File.Copy(oldname, newname);
			File.Delete(oldname);
		}

		public static string Directory(string filename)
		{
			if (filename == "")
			{
				return "";
			}
			string directoryName = (new FileInfo(filename)).DirectoryName;
			string str = Path.DirectorySeparatorChar.ToString();
			if (!directoryName.EndsWith(str))
			{
				directoryName = string.Concat(directoryName, str);
			}
			return directoryName;
		}

		public static string Extension(string filename)
		{
			if (filename == "")
			{
				return "";
			}
			string extension = (new FileInfo(filename)).Extension;
			if (extension.StartsWith("."))
			{
				extension = extension.Substring(1);
			}
			return extension;
		}

		private static string first_folder(string path)
		{
			string str = Path.DirectorySeparatorChar.ToString();
			int num = path.IndexOf(str);
			if (num == -1)
			{
				return "";
			}
			return path.Substring(0, num + str.Length);
		}

		public static string MakeAbsolute(string filename, string directory)
		{
			string str = Path.DirectorySeparatorChar.ToString();
			if (directory.EndsWith(str))
			{
				directory = directory.Remove(directory.Length - str.Length);
			}
			string str1 = string.Concat("..", str);
			while (filename.StartsWith(str1))
			{
				filename = filename.Remove(0, str1.Length);
				directory = directory.Remove(directory.LastIndexOf(str1));
			}
			return string.Concat(directory, str, filename);
		}

		public static string MakeRelative(string filename, string directory)
		{
			filename = FileName.remove_protocol(filename);
			directory = FileName.remove_protocol(directory);
			string str = Path.DirectorySeparatorChar.ToString();
			if (!directory.EndsWith(str))
			{
				directory = string.Concat(directory, str);
			}
			string str1 = FileName.first_folder(filename);
			string str2 = FileName.first_folder(directory);
			if (str1 != str2)
			{
				return filename;
			}
			filename = filename.Remove(0, str1.Length);
			directory = directory.Remove(0, str2.Length);
			while (true)
			{
				string str3 = FileName.first_folder(directory);
				if (str3 == "" || !filename.StartsWith(str3))
				{
					break;
				}
				filename = filename.Remove(0, str3.Length);
				directory = directory.Remove(0, str3.Length);
			}
			string str4 = "";
			while (true)
			{
				string str5 = FileName.first_folder(directory);
				if (str5 == "")
				{
					break;
				}
				directory = directory.Remove(0, str5.Length);
				str4 = string.Concat(str4, "..", str);
			}
			return string.Concat(str4, filename);
		}

		public static string Name(string filename)
		{
			if (filename == "")
			{
				return "";
			}
			string name = (new FileInfo(filename)).Name;
			int num = name.LastIndexOf(".");
			if (num != -1)
			{
				name = name.Remove(num);
			}
			return name;
		}

		private static string remove_protocol(string path)
		{
			string str = "://";
			int num = path.IndexOf(str);
			if (num == -1)
			{
				return path;
			}
			return path.Remove(0, num + str.Length);
		}

		public static string TrimInvalidCharacters(string filename)
		{
			string str = filename.Replace("\\", "");
			str = str.Replace("/", "");
			str = str.Replace(":", "");
			str = str.Replace("*", "");
			str = str.Replace("\"", "");
			str = str.Replace("?", "");
			str = str.Replace(".", "");
			str = str.Replace("|", "");
			return str.Replace("<", "").Replace(">", "");
		}
	}
}