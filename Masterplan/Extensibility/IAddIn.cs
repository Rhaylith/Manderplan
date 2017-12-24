using System;
using System.Collections.Generic;

namespace Masterplan.Extensibility
{
	public interface IAddIn
	{
		List<ICommand> CombatCommands
		{
			get;
		}

		List<ICommand> Commands
		{
			get;
		}

		string Description
		{
			get;
		}

		string Name
		{
			get;
		}

		List<IPage> Pages
		{
			get;
		}

		List<IPage> QuickReferencePages
		{
			get;
		}

		System.Version Version
		{
			get;
		}

		bool Initialise(IApplication app);
	}
}