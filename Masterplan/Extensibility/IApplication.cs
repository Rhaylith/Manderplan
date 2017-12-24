using Masterplan.Data;
using System;
using System.Collections.Generic;

namespace Masterplan.Extensibility
{
	public interface IApplication
	{
		List<IAddIn> AddIns
		{
			get;
		}

		Encounter CurrentEncounter
		{
			get;
		}

		List<Library> Libraries
		{
			get;
		}

		Masterplan.Data.Project Project
		{
			get;
			set;
		}

		string ProjectFile
		{
			get;
			set;
		}

		bool ProjectModified
		{
			get;
			set;
		}

		PlotPoint SelectedPoint
		{
			get;
		}

		void SaveLibrary(Library lib);

		void UpdateView();
	}
}