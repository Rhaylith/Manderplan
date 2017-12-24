using System;

namespace Masterplan.Extensibility
{
	public interface ICommand
	{
		bool Active
		{
			get;
		}

		bool Available
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

		void Execute();
	}
}