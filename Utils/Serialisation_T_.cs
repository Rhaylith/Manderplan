using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Utils
{
	public class Serialisation<T>
	{
		public Serialisation()
		{
		}

		public static T Load(string filename, SerialisationMode mode)
		{
			T t = default(T);
			try
			{
				switch (mode)
				{
					case SerialisationMode.Binary:
					{
						Stream fileStream = new FileStream(filename, FileMode.Open);
						try
						{
							t = (T)(new BinaryFormatter()).Deserialize(fileStream);
						}
						catch
						{
							t = default(T);
						}
						fileStream.Close();
						break;
					}
					case SerialisationMode.XML:
					{
						XmlTextReader xmlTextReader = new XmlTextReader(filename);
						try
						{
							t = (T)(new XmlSerializer(typeof(T))).Deserialize(xmlTextReader);
						}
						catch
						{
							t = default(T);
						}
						xmlTextReader.Close();
						break;
					}
				}
			}
			catch (Exception exception)
			{
				t = default(T);
			}
			return t;
		}

		public static bool Save(string filename, T obj, SerialisationMode mode)
		{
			bool flag = false;
			string str = string.Concat(filename, ".save");
			try
			{
				switch (mode)
				{
					case SerialisationMode.Binary:
					{
						Stream fileStream = new FileStream(str, FileMode.Create);
						try
						{
							(new BinaryFormatter()).Serialize(fileStream, obj);
							fileStream.Flush();
							flag = true;
						}
						catch (Exception exception)
						{
							Console.WriteLine(exception);
							flag = false;
						}
						fileStream.Close();
						break;
					}
					case SerialisationMode.XML:
					{
						XmlTextWriter xmlTextWriter = new XmlTextWriter(str, Encoding.UTF8)
						{
							Formatting = Formatting.Indented
						};
						try
						{
							(new XmlSerializer(typeof(T))).Serialize(xmlTextWriter, obj);
							xmlTextWriter.Flush();
							flag = true;
						}
						catch (Exception exception1)
						{
							Console.WriteLine(exception1);
							flag = false;
						}
						xmlTextWriter.Close();
						break;
					}
				}
			}
			catch (Exception exception2)
			{
				flag = false;
			}
			if (flag)
			{
				if (File.Exists(filename))
				{
					File.Delete(filename);
				}
				File.Move(str, filename);
			}
			return flag;
		}
	}
}