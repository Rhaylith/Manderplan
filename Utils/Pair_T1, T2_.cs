using System;

namespace Utils
{
	[Serializable]
	public class Pair<T1, T2> : IComparable<Pair<T1, T2>>
	{
		private T1 fFirst;

		private T2 fSecond;

		public T1 First
		{
			get
			{
				return this.fFirst;
			}
			set
			{
				this.fFirst = value;
			}
		}

		public T2 Second
		{
			get
			{
				return this.fSecond;
			}
			set
			{
				this.fSecond = value;
			}
		}

		public Pair()
		{
		}

		public Pair(T1 first, T2 second)
		{
			this.First = first;
			this.Second = second;
		}

		public int CompareTo(Pair<T1, T2> rhs)
		{
			string str = this.fFirst.ToString();
			return str.CompareTo(rhs.First.ToString());
		}

		public override string ToString()
		{
			return string.Concat(this.fFirst, ", ", this.fSecond);
		}
	}
}