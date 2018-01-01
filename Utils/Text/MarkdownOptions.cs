using System;
using System.Runtime.CompilerServices;

namespace Utils.Text
{
	public class MarkdownOptions
	{
		public bool AutoHyperlink
		{
			get;
			set;
		}

		public bool AutoNewlines
		{
			get;
			set;
		}

		public string EmptyElementSuffix
		{
			get;
			set;
		}

		public bool EncodeProblemUrlCharacters
		{
			get;
			set;
		}

		public bool LinkEmails
		{
			get;
			set;
		}

		public bool StrictBoldItalic
		{
			get;
			set;
		}

		public MarkdownOptions()
		{
		}
	}
}