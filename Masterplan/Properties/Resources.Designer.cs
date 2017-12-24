using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Masterplan.Properties
{
	[CompilerGenerated]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
	internal class Resources
	{
		private static System.Resources.ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		internal static Bitmap Area
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Area", Resources.resourceCulture);
			}
		}

		internal static Bitmap Aura
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Aura", Resources.resourceCulture);
			}
		}

		internal static Bitmap Close
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Close", Resources.resourceCulture);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		internal static Bitmap Melee
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Melee", Resources.resourceCulture);
			}
		}

		internal static Bitmap MeleeBasic
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("MeleeBasic", Resources.resourceCulture);
			}
		}

		internal static Bitmap Purpled20
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Purpled20", Resources.resourceCulture);
			}
		}

		internal static Bitmap Ranged
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Ranged", Resources.resourceCulture);
			}
		}

		internal static Bitmap RangedBasic
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("RangedBasic", Resources.resourceCulture);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static System.Resources.ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					Resources.resourceMan = new System.Resources.ResourceManager("Masterplan.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		internal static Bitmap Scroll
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Scroll", Resources.resourceCulture);
			}
		}

		internal Resources()
		{
		}
	}
}