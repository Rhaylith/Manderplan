using System;

namespace Masterplan.Data
{
	[Serializable]
	public class MagicItemSection
	{
		private string fHeader = "";

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

		public string Header
		{
			get
			{
				return this.fHeader;
			}
			set
			{
				this.fHeader = value;
			}
		}

		public MagicItemSection()
		{
		}

		public MagicItemSection Copy()
		{
			MagicItemSection magicItemSection = new MagicItemSection()
			{
				Header = this.fHeader,
				Details = this.fDetails
			};
			return magicItemSection;
		}

		public override string ToString()
		{
			if (this.fDetails == "")
			{
				return this.fHeader;
			}
			return string.Concat(this.fHeader, ": ", this.fDetails);
		}
	}
}