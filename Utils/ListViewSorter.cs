using System;
using System.Collections;
using System.Windows.Forms;

namespace Utils
{
	public class ListViewSorter : IComparer
	{
		private int fColumn;

		private bool fAscending = true;

		public bool Ascending
		{
			get
			{
				return this.fAscending;
			}
			set
			{
				this.fAscending = value;
			}
		}

		public int Column
		{
			get
			{
				return this.fColumn;
			}
			set
			{
				this.fColumn = value;
			}
		}

		public ListViewSorter()
		{
		}

		public int Compare(object x, object y)
		{
			int num;
			ListViewItem listViewItem = x as ListViewItem;
			ListViewItem listViewItem1 = y as ListViewItem;
			if (listViewItem == null || listViewItem1 == null)
			{
				throw new ArgumentException();
			}
			string text = listViewItem.SubItems[this.Column].Text;
			string str = listViewItem1.SubItems[this.Column].Text;
			try
			{
				int num1 = int.Parse(text);
				int num2 = int.Parse(str);
				num = num1.CompareTo(num2) * (this.fAscending ? 1 : -1);
				return num;
			}
			catch
			{
			}
			try
			{
				float single = float.Parse(text);
				float single1 = float.Parse(str);
				num = single.CompareTo(single1) * (this.fAscending ? 1 : -1);
				return num;
			}
			catch
			{
			}
			try
			{
				DateTime dateTime = DateTime.Parse(text);
				DateTime dateTime1 = DateTime.Parse(str);
				num = dateTime.CompareTo(dateTime1) * (this.fAscending ? 1 : -1);
			}
			catch
			{
				return text.CompareTo(str) * (this.fAscending ? 1 : -1);
			}
			return num;
		}

		public void SetColumn(int col)
		{
			if (this.fColumn == col)
			{
				this.fAscending = !this.fAscending;
				return;
			}
			this.fColumn = col;
			this.fAscending = true;
		}

		public static void Sort(ListView list, int column)
		{
			ListViewSorter listViewItemSorter = list.ListViewItemSorter as ListViewSorter;
			if (listViewItemSorter != null)
			{
				listViewItemSorter.SetColumn(column);
				list.Sort();
			}
		}
	}
}