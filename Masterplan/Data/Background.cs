using System;

namespace Masterplan.Data
{
	[Serializable]
	public class Background
	{
		private Guid fID = Guid.NewGuid();

		private string fTitle = "";

		private string fDetails = "";

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

		public string Title
		{
			get
			{
				return this.fTitle;
			}
			set
			{
				this.fTitle = value;
			}
		}

		public Background()
		{
		}

		public Background(string title)
		{
			this.fTitle = title;
		}

		public Background Copy()
		{
			Background background = new Background()
			{
				ID = this.fID,
				Title = this.fTitle,
				Details = this.fDetails
			};
			return background;
		}

		public override string ToString()
		{
			return this.fTitle;
		}
	}
}