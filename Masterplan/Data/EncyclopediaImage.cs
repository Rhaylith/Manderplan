using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace Masterplan.Data
{
	[Serializable]
	public class EncyclopediaImage
	{
		private string fName = "";

		private Guid fID = Guid.NewGuid();

		private System.Drawing.Image fImage;

		public Guid ID
		{
			get
			{
				return this.fID;
			}
			set
			{
				this.fID = value;
			}
		}

		[XmlIgnore]
		public System.Drawing.Image Image
		{
			get
			{
				return this.fImage;
			}
			set
			{
				this.fImage = value;
			}
		}

		public byte[] ImageData
		{
			get
			{
				if (this.fImage == null)
				{
					return null;
				}
				TypeConverter converter = TypeDescriptor.GetConverter(this.fImage.GetType());
				return (byte[])converter.ConvertTo(this.fImage, typeof(byte[]));
			}
			set
			{
				if (value == null)
				{
					this.fImage = null;
					return;
				}
				this.fImage = new Bitmap(new MemoryStream(value));
			}
		}

		public string Name
		{
			get
			{
				return this.fName;
			}
			set
			{
				this.fName = value;
			}
		}

		public EncyclopediaImage()
		{
		}

		public EncyclopediaImage Copy()
		{
			EncyclopediaImage encyclopediaImage = new EncyclopediaImage()
			{
				ID = this.fID,
				Name = this.fName,
				Image = this.fImage
			};
			return encyclopediaImage;
		}
	}
}