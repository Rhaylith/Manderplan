using System;
using System.Windows.Forms;

namespace Masterplan.Extensibility
{
	public interface IPage
	{
		System.Windows.Forms.Control Control
		{
			get;
		}

		string Name
		{
			get;
		}

		void UpdateView();
	}
}