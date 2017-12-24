using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Masterplan.Tools
{
	internal class BlankMap
	{
		public BlankMap()
		{
		}

		public static void Print()
		{
			PrintDialog printDialog = new PrintDialog()
			{
				AllowPrintToFile = false
			};
			if (printDialog.ShowDialog() == DialogResult.OK)
			{
				PrintDocument printDocument = new PrintDocument()
				{
					DocumentName = "Blank Grid",
					PrinterSettings = printDialog.PrinterSettings
				};
				for (int i = 0; i != printDialog.PrinterSettings.Copies; i++)
				{
					printDocument.PrintPage += new PrintPageEventHandler(BlankMap.print_blank_page);
					printDocument.Print();
				}
			}
		}

		private static void print_blank_page(object sender, PrintPageEventArgs e)
		{
			int width = e.PageSettings.PaperSize.Width / 100;
			int height = e.PageSettings.PaperSize.Height / 100;
			int num = e.PageBounds.Width / width;
			int height1 = e.PageBounds.Height / height;
			int num1 = Math.Min(num, height1);
			int num2 = width * num1 + 1;
			int num3 = height * num1 + 1;
			Bitmap bitmap = new Bitmap(num2, num3);
			for (int i = 0; i != num2; i++)
			{
				for (int j = 0; j != num3; j++)
				{
					if (i % num1 == 0 || j % num1 == 0)
					{
						bitmap.SetPixel(i, j, Color.DarkGray);
					}
				}
			}
			int width1 = (e.PageBounds.Width - num2) / 2;
			int height2 = (e.PageBounds.Height - num3) / 2;
			Rectangle rectangle = new Rectangle(width1, height2, num2, num3);
			e.Graphics.DrawRectangle(Pens.Black, rectangle);
			e.Graphics.DrawImage(bitmap, rectangle);
		}
	}
}