using System;

namespace Utils.Wizards
{
	public interface IWizardPage
	{
		bool AllowBack
		{
			get;
		}

		bool AllowFinish
		{
			get;
		}

		bool AllowNext
		{
			get;
		}

		bool OnBack();

		bool OnFinish();

		bool OnNext();

		void OnShown(object data);
	}
}