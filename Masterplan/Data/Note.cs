using System;

namespace Masterplan.Data
{
	[Serializable]
	public class Note : IComparable<Note>
	{
		private Guid fID = Guid.NewGuid();

		private string fContent = "";

		private string fCategory = "";

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

		public string Content
		{
			get
			{
				return this.fContent;
			}
			set
			{
				this.fContent = value;
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
				string[] newLine = new string[] { Environment.NewLine };
				string[] strArrays = this.fContent.Split(newLine, StringSplitOptions.RemoveEmptyEntries);
				if ((int)strArrays.Length == 0)
				{
					return "(blank note)";
				}
				return strArrays[0];
			}
		}

		public Note()
		{
		}

		public int CompareTo(Note rhs)
		{
			return this.Name.CompareTo(rhs.Name);
		}

		public Note Copy()
		{
			Note note = new Note()
			{
				ID = this.fID,
				Content = this.fContent,
				Category = this.fCategory
			};
			return note;
		}

		public override string ToString()
		{
			return this.fContent;
		}
	}
}