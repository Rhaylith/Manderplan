using System;
using Utils;

namespace Masterplan.Data
{
	[Serializable]
	public class Attachment : IComparable<Attachment>
	{
		private Guid fID = Guid.NewGuid();

		private string fName;

		private byte[] fContents;

		public byte[] Contents
		{
			get
			{
				return this.fContents;
			}
			set
			{
				this.fContents = value;
			}
		}

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

		public AttachmentType Type
		{
			get
			{
				string lower = FileName.Extension(this.fName).ToLower();
				if (lower == "txt")
				{
					return AttachmentType.PlainText;
				}
				if (lower == "rtf")
				{
					return AttachmentType.RichText;
				}
				if (lower == "bmp")
				{
					return AttachmentType.Image;
				}
				if (lower == "jpg")
				{
					return AttachmentType.Image;
				}
				if (lower == "jpeg")
				{
					return AttachmentType.Image;
				}
				if (lower == "gif")
				{
					return AttachmentType.Image;
				}
				if (lower == "tga")
				{
					return AttachmentType.Image;
				}
				if (lower == "png")
				{
					return AttachmentType.Image;
				}
				if (lower == "url")
				{
					return AttachmentType.URL;
				}
				if (lower == "htm")
				{
					return AttachmentType.HTML;
				}
				if (lower == "html")
				{
					return AttachmentType.HTML;
				}
				return AttachmentType.Miscellaneous;
			}
		}

		public Attachment()
		{
		}

		public int CompareTo(Attachment rhs)
		{
			string str = FileName.Name(this.fName);
			return str.CompareTo(FileName.Name(rhs.Name));
		}

		public Attachment Copy()
		{
			Attachment attachment = new Attachment()
			{
				ID = this.fID,
				Name = this.fName,
				Contents = new byte[(int)this.fContents.Length]
			};
			for (int i = 0; i != (int)this.fContents.Length; i++)
			{
				attachment.Contents[i] = this.fContents[i];
			}
			return attachment;
		}
	}
}