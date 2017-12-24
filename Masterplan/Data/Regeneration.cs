using System;

namespace Masterplan.Data
{
	[Serializable]
	public class Regeneration
	{
		private int fValue = 2;

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

		public int Value
		{
			get
			{
				return this.fValue;
			}
			set
			{
				this.fValue = value;
			}
		}

		public Regeneration()
		{
		}

		public Regeneration(int value, string details)
		{
			this.fValue = value;
			this.fDetails = details;
		}

		public Regeneration Copy()
		{
			Regeneration regeneration = new Regeneration()
			{
				Value = this.fValue,
				Details = this.fDetails
			};
			return regeneration;
		}

		public override string ToString()
		{
			string str = this.fValue.ToString();
			if (this.fDetails != "")
			{
				bool flag = str != "";
				if (flag)
				{
					str = string.Concat(str, " (");
				}
				str = string.Concat(str, this.fDetails);
				if (flag)
				{
					str = string.Concat(str, ")");
				}
			}
			return str;
		}
	}
}