using Masterplan;
using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Utils;

namespace Masterplan.Extensibility
{
	internal class ExtensibilityManager : IApplication
	{
		private MainForm fMainForm;

		public List<IAddIn> AddIns
		{
			get
			{
				return Session.AddIns;
			}
		}

		public Encounter CurrentEncounter
		{
			get
			{
				return Session.CurrentEncounter;
			}
		}

		public List<Library> Libraries
		{
			get
			{
				return Session.Libraries;
			}
		}

		public Masterplan.Data.Project Project
		{
			get
			{
				return Session.Project;
			}
			set
			{
				Session.Project = value;
			}
		}

		public string ProjectFile
		{
			get
			{
				return Session.FileName;
			}
			set
			{
				Session.FileName = value;
			}
		}

		public bool ProjectModified
		{
			get
			{
				return Session.Modified;
			}
			set
			{
				Session.Modified = value;
			}
		}

		public PlotPoint SelectedPoint
		{
			get
			{
				return this.fMainForm.PlotView.SelectedPoint;
			}
		}

		public ExtensibilityManager(MainForm main_form)
		{
			this.fMainForm = main_form;
			this.Load(string.Concat(Application.StartupPath, "\\AddIns"));
		}

		private static int compare_addins(IAddIn x, IAddIn y)
		{
			return x.Name.CompareTo(y.Name);
		}

		private void install(IAddIn addin)
		{
			if (addin.Initialise(this))
			{
				Session.AddIns.Add(addin);
			}
		}

		private bool is_addin(Type t)
		{
			Type[] interfaces = t.GetInterfaces();
			for (int i = 0; i < (int)interfaces.Length; i++)
			{
				Type type = interfaces[i];
				if (type != null && type == typeof(IAddIn))
				{
					return true;
				}
			}
			return false;
		}

		public void Load(string path)
		{
			if (File.Exists(path))
			{
				Assembly assembly = Assembly.LoadFile(path);
				if (assembly != null)
				{
					this.load_file(assembly);
				}
			}
			if (Directory.Exists(path))
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(path);
				FileInfo[] files = directoryInfo.GetFiles("*.dll");
				for (int i = 0; i < (int)files.Length; i++)
				{
					this.Load(files[i].FullName);
				}
				DirectoryInfo[] directories = directoryInfo.GetDirectories();
				for (int j = 0; j < (int)directories.Length; j++)
				{
					this.Load(directories[j].FullName);
				}
			}
			Session.AddIns.Sort(new Comparison<IAddIn>(ExtensibilityManager.compare_addins));
		}

		private void load_file(Assembly assembly)
		{
			try
			{
				Type[] types = assembly.GetTypes();
				for (int i = 0; i < (int)types.Length; i++)
				{
					Type type = types[i];
					if (this.is_addin(type))
					{
						ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
						if (constructor != null)
						{
							IAddIn addIn = constructor.Invoke(null) as IAddIn;
							if (addIn != null)
							{
								this.install(addIn);
							}
						}
					}
				}
			}
			catch (ReflectionTypeLoadException reflectionTypeLoadException1)
			{
				ReflectionTypeLoadException reflectionTypeLoadException = reflectionTypeLoadException1;
				string name = assembly.ManifestModule.Name;
				LogSystem.Trace(string.Concat("The add-in '", name, "' could not be loaded; contact the developer for an updated version."));
				Exception[] loaderExceptions = reflectionTypeLoadException.LoaderExceptions;
				for (int j = 0; j < (int)loaderExceptions.Length; j++)
				{
					Console.WriteLine(loaderExceptions[j]);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		public void SaveLibrary(Library lib)
		{
			string libraryFilename = Session.GetLibraryFilename(lib);
			Serialisation<Library>.Save(libraryFilename, lib, SerialisationMode.Binary);
		}

		public void UpdateView()
		{
			this.fMainForm.UpdateView();
		}
	}
}