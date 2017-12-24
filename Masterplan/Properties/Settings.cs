using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Masterplan.Properties
{
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance;

		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("http://www.wizards.com/compendiumsearch.asmx")]
		[SpecialSetting(SpecialSetting.WebServiceUrl)]
		public string Masterplan_Compendium_CompendiumSearch
		{
			get
			{
				return (string)this["Masterplan_Compendium_CompendiumSearch"];
			}
		}

		static Settings()
		{
			Settings.defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
		}

		public Settings()
		{
		}
	}
}