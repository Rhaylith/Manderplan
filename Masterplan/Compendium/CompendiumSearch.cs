using Masterplan.Properties;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;

namespace Masterplan.Compendium
{
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "2.0.50727.4927")]
	[WebServiceBinding(Name="CompendiumSearchSoap", Namespace="http://ww2.wizards.com")]
	public class CompendiumSearch : SoapHttpClientProtocol
	{
		private SendOrPostCallback KeywordSearchOperationCompleted;

		private SendOrPostCallback KeywordSearchWithFiltersOperationCompleted;

		private SendOrPostCallback ViewAllOperationCompleted;

		private SendOrPostCallback GetFilterSelectOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;

		public new string Url
		{
			get
			{
				return base.Url;
			}
			set
			{
				if (this.IsLocalFileSystemWebService(base.Url) && !this.useDefaultCredentialsSetExplicitly && !this.IsLocalFileSystemWebService(value))
				{
					base.UseDefaultCredentials = false;
				}
				base.Url = value;
			}
		}

		public new bool UseDefaultCredentials
		{
			get
			{
				return base.UseDefaultCredentials;
			}
			set
			{
				base.UseDefaultCredentials = value;
				this.useDefaultCredentialsSetExplicitly = true;
			}
		}

		public CompendiumSearch()
		{
			this.Url = Settings.Default.Masterplan_Compendium_CompendiumSearch;
			if (!this.IsLocalFileSystemWebService(this.Url))
			{
				this.useDefaultCredentialsSetExplicitly = true;
				return;
			}
			this.UseDefaultCredentials = true;
			this.useDefaultCredentialsSetExplicitly = false;
		}

		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		[SoapDocumentMethod("http://ww2.wizards.com/GetFilterSelect", RequestNamespace="http://ww2.wizards.com", ResponseNamespace="http://ww2.wizards.com", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public XmlNode GetFilterSelect()
		{
			object[] objArray = base.Invoke("GetFilterSelect", new object[0]);
			return (XmlNode)objArray[0];
		}

		public void GetFilterSelectAsync()
		{
			this.GetFilterSelectAsync(null);
		}

		public void GetFilterSelectAsync(object userState)
		{
			if (this.GetFilterSelectOperationCompleted == null)
			{
				this.GetFilterSelectOperationCompleted = new SendOrPostCallback(this.OnGetFilterSelectOperationCompleted);
			}
			base.InvokeAsync("GetFilterSelect", new object[0], this.GetFilterSelectOperationCompleted, userState);
		}

		private bool IsLocalFileSystemWebService(string url)
		{
			if (url == null || url == string.Empty)
			{
				return false;
			}
			System.Uri uri = new System.Uri(url);
			if (uri.Port >= 1024 && string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			return false;
		}

		[SoapDocumentMethod("http://ww2.wizards.com/KeywordSearch", RequestNamespace="http://ww2.wizards.com", ResponseNamespace="http://ww2.wizards.com", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public XmlNode KeywordSearch(string Keywords, string NameOnly, string Tab)
		{
			object[] keywords = new object[] { Keywords, NameOnly, Tab };
			return (XmlNode)base.Invoke("KeywordSearch", keywords)[0];
		}

		public void KeywordSearchAsync(string Keywords, string NameOnly, string Tab)
		{
			this.KeywordSearchAsync(Keywords, NameOnly, Tab, null);
		}

		public void KeywordSearchAsync(string Keywords, string NameOnly, string Tab, object userState)
		{
			if (this.KeywordSearchOperationCompleted == null)
			{
				this.KeywordSearchOperationCompleted = new SendOrPostCallback(this.OnKeywordSearchOperationCompleted);
			}
			object[] keywords = new object[] { Keywords, NameOnly, Tab };
			base.InvokeAsync("KeywordSearch", keywords, this.KeywordSearchOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://ww2.wizards.com/KeywordSearchWithFilters", RequestNamespace="http://ww2.wizards.com", ResponseNamespace="http://ww2.wizards.com", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public XmlNode KeywordSearchWithFilters(string Keywords, string Tab, string Filters, string NameOnly)
		{
			object[] keywords = new object[] { Keywords, Tab, Filters, NameOnly };
			return (XmlNode)base.Invoke("KeywordSearchWithFilters", keywords)[0];
		}

		public void KeywordSearchWithFiltersAsync(string Keywords, string Tab, string Filters, string NameOnly)
		{
			this.KeywordSearchWithFiltersAsync(Keywords, Tab, Filters, NameOnly, null);
		}

		public void KeywordSearchWithFiltersAsync(string Keywords, string Tab, string Filters, string NameOnly, object userState)
		{
			if (this.KeywordSearchWithFiltersOperationCompleted == null)
			{
				this.KeywordSearchWithFiltersOperationCompleted = new SendOrPostCallback(this.OnKeywordSearchWithFiltersOperationCompleted);
			}
			object[] keywords = new object[] { Keywords, Tab, Filters, NameOnly };
			base.InvokeAsync("KeywordSearchWithFilters", keywords, this.KeywordSearchWithFiltersOperationCompleted, userState);
		}

		private void OnGetFilterSelectOperationCompleted(object arg)
		{
			if (this.GetFilterSelectCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.GetFilterSelectCompleted(this, new GetFilterSelectCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void OnKeywordSearchOperationCompleted(object arg)
		{
			if (this.KeywordSearchCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.KeywordSearchCompleted(this, new KeywordSearchCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void OnKeywordSearchWithFiltersOperationCompleted(object arg)
		{
			if (this.KeywordSearchWithFiltersCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.KeywordSearchWithFiltersCompleted(this, new KeywordSearchWithFiltersCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void OnViewAllOperationCompleted(object arg)
		{
			if (this.ViewAllCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.ViewAllCompleted(this, new ViewAllCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		[SoapDocumentMethod("http://ww2.wizards.com/ViewAll", RequestNamespace="http://ww2.wizards.com", ResponseNamespace="http://ww2.wizards.com", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public XmlNode ViewAll(string Tab)
		{
			object[] tab = new object[] { Tab };
			return (XmlNode)base.Invoke("ViewAll", tab)[0];
		}

		public void ViewAllAsync(string Tab)
		{
			this.ViewAllAsync(Tab, null);
		}

		public void ViewAllAsync(string Tab, object userState)
		{
			if (this.ViewAllOperationCompleted == null)
			{
				this.ViewAllOperationCompleted = new SendOrPostCallback(this.OnViewAllOperationCompleted);
			}
			object[] tab = new object[] { Tab };
			base.InvokeAsync("ViewAll", tab, this.ViewAllOperationCompleted, userState);
		}

		public event GetFilterSelectCompletedEventHandler GetFilterSelectCompleted;

		public event KeywordSearchCompletedEventHandler KeywordSearchCompleted;

		public event KeywordSearchWithFiltersCompletedEventHandler KeywordSearchWithFiltersCompleted;

		public event ViewAllCompletedEventHandler ViewAllCompleted;
	}
}