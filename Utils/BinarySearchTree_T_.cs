using System;
using System.Collections.Generic;

namespace Utils
{
	public class BinarySearchTree<T>
	where T : IComparable<T>
	{
		private T fData;

		private BinarySearchTree<T> fLeft;

		private BinarySearchTree<T> fRight;

		public int Count
		{
			get
			{
				if (this.fData == null)
				{
					return 0;
				}
				int count = 1;
				if (this.fLeft != null)
				{
					count += this.fLeft.Count;
				}
				if (this.fRight != null)
				{
					count += this.fRight.Count;
				}
				return count;
			}
		}

		public List<T> SortedList
		{
			get
			{
				List<T> ts = new List<T>();
				if (this.fData != null)
				{
					if (this.fLeft != null)
					{
						ts.AddRange(this.fLeft.SortedList);
					}
					ts.Add(this.fData);
					if (this.fRight != null)
					{
						ts.AddRange(this.fRight.SortedList);
					}
				}
				return ts;
			}
		}

		public BinarySearchTree()
		{
		}

		public BinarySearchTree(T item)
		{
			this.fData = item;
		}

		public BinarySearchTree(IEnumerable<T> list)
		{
			this.Add(list);
		}

		public void Add(T item)
		{
			if (this.fData == null)
			{
				this.fData = item;
				return;
			}
			int num = this.fData.CompareTo(item);
			if (num > 0)
			{
				if (this.fLeft != null)
				{
					this.fLeft.Add(item);
				}
				else
				{
					this.fLeft = new BinarySearchTree<T>(item);
				}
			}
			if (num < 0)
			{
				if (this.fRight == null)
				{
					this.fRight = new BinarySearchTree<T>(item);
					return;
				}
				this.fRight.Add(item);
			}
		}

		public void Add(IEnumerable<T> list)
		{
			foreach (T t in list)
			{
				this.Add(t);
			}
		}

		public bool Contains(T item)
		{
			if (this.fData == null)
			{
				return false;
			}
			int num = this.fData.CompareTo(item);
			if (num > 0)
			{
				if (this.fLeft == null)
				{
					return false;
				}
				return this.fLeft.Contains(item);
			}
			if (num >= 0)
			{
				return true;
			}
			if (this.fRight == null)
			{
				return false;
			}
			return this.fRight.Contains(item);
		}
	}
}