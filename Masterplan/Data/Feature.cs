using System;

namespace Masterplan.Data
{
	[Serializable]
	public class Feature
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

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

		public Feature()
		{
		}

		public Feature Copy()
		{
			Feature feature = new Feature()
			{
				ID = this.fID,
				Name = this.fName,
				Details = this.fDetails
			};
			return feature;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}