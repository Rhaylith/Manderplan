using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Masterplan.Compendium
{
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "2.0.50727.4927")]
	public class GetFilterSelectCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		internal GetFilterSelectCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}