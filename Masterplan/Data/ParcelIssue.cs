using System;

namespace Masterplan.Data
{
	[Serializable]
	public class ParcelIssue : IIssue
	{
		private Parcel fParcel;

		private PlotPoint fPoint;

		public PlotPoint Point
		{
			get
			{
				return this.fPoint;
			}
		}

		public string Reason
		{
			get
			{
				if (this.fPoint.State != PlotPointState.Normal)
				{
					return "";
				}
				if (this.fParcel.Name != "")
				{
					return "";
				}
				return string.Concat("A treasure parcel in ", this.fPoint.Name, " is undefined.");
			}
		}

		public ParcelIssue(Parcel parcel, PlotPoint pp)
		{
			this.fParcel = parcel;
			this.fPoint = pp;
		}

		public override string ToString()
		{
			return string.Concat(this.fPoint.Name, ": ", this.Reason);
		}
	}
}