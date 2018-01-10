using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class EncyclopediaEntry : IEncyclopediaItem, IComparable<EncyclopediaEntry>
	{
		private string fName = "";

		private Guid fID = Guid.NewGuid();

		private string fCategory = "";

		private Guid fAttachmentID = Guid.Empty;

		private string fDetails = "";

		private string fDM = "";

		private List<EncyclopediaImage> fImages = new List<EncyclopediaImage>();

		public Guid AttachmentID
		{
			get
			{
				return this.fAttachmentID;
			}
			set
			{
				this.fAttachmentID = value;
			}
		}

		public string Category
		{
			get
			{
				return this.fCategory;
			}
			set
			{
				this.fCategory = value;
			}
		}

		public string Details
		{
			get
			{
				return this.fDetails;
			}
			set
			{
				this.fDetails = value;
			}
		}

		public string DMInfo
		{
			get
			{
				return this.fDM;
			}
			set
			{
				this.fDM = value;
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

		public List<EncyclopediaImage> Images
		{
			get
			{
				return this.fImages;
			}
			set
			{
				this.fImages = value;
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

		public EncyclopediaEntry()
		{
		}

		public int CompareTo(EncyclopediaEntry rhs)
		{
			return this.fName.CompareTo(rhs.Name);
		}

		public EncyclopediaEntry Copy()
		{
			EncyclopediaEntry encyclopediaEntry = new EncyclopediaEntry()
			{
				Name = this.fName,
				ID = this.fID,
				Category = this.fCategory,
				AttachmentID = this.fAttachmentID,
				Details = this.fDetails,
				DMInfo = this.fDM
			};
			foreach (EncyclopediaImage fImage in this.fImages)
			{
				encyclopediaEntry.Images.Add(fImage.Copy());
			}
			return encyclopediaEntry;
		}

		public EncyclopediaImage FindImage(Guid id)
		{
			EncyclopediaImage encyclopediaImage;
			List<EncyclopediaImage>.Enumerator enumerator = this.fImages.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					EncyclopediaImage current = enumerator.Current;
					if (current.ID != id)
					{
						continue;
					}
					encyclopediaImage = current;
					return encyclopediaImage;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}