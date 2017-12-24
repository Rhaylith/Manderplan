using System;

namespace Masterplan.Data
{
	[Serializable]
	public class Aura
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fKeywords = "";

		private string fDetails = "";

		private bool fExtractedData;

		private int fRadius = -2147483648;

		private string fDescription = "";

		internal string Description
		{
			get
			{
				if (!this.fExtractedData)
				{
					this.extract();
				}
				return this.fDescription;
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
				this.extract();
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

		public string Keywords
		{
			get
			{
				return this.fKeywords;
			}
			set
			{
				this.fKeywords = value;
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

		internal int Radius
		{
			get
			{
				if (!this.fExtractedData)
				{
					this.extract();
				}
				return this.fRadius;
			}
		}

		public Aura()
		{
		}

		public Aura Copy()
		{
			Aura aura = new Aura()
			{
				ID = this.fID,
				Name = this.fName,
				Keywords = this.fKeywords,
				Details = this.fDetails
			};
			return aura;
		}

		private void extract()
		{
			string str = "";
			bool flag = false;
			int num = 0;
			while (num != this.fDetails.Length)
			{
				char chr = this.fDetails[num];
				flag = char.IsDigit(chr);
				if (flag || !(str != ""))
				{
					if (flag)
					{
						str = string.Concat(str, chr);
					}
					num++;
				}
				else
				{
					this.fDescription = this.fDetails.Substring(num);
					break;
				}
			}
			int num1 = 1;
			try
			{
				num1 = int.Parse(str);
			}
			catch
			{
				num1 = 1;
			}
			if (this.fDescription != null)
			{
				if (this.fDescription.StartsWith(":"))
				{
					this.fDescription = this.fDescription.Substring(1);
				}
				this.fDescription = this.fDescription.Trim();
			}
			else
			{
				this.fDescription = "";
			}
			this.fRadius = num1;
			this.fExtractedData = true;
		}
	}
}