using Masterplan;
using Masterplan.Tools;
using Masterplan.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Utils;

namespace Masterplan.Controls
{
	internal class DicePanel : UserControl
	{
		private const int DIE_SIZE = 32;

		private StringFormat fCentred = new StringFormat();

		private List<Pair<int, int>> fDice = new List<Pair<int, int>>();

		private int fConstant;

		private bool fUpdating;

		private IContainer components;

		private ToolStrip DiceToolbar;

		private ToolStripButton RollBtn;

		private ToolStripButton ClearBtn;

		private Label DiceLbl;

		private ListView DiceList;

		private ListView DiceSourceList;

		private Panel ResultPanel;

		private ToolStripButton OddsBtn;

		private TextBox ExpressionBox;

		public DiceExpression Expression
		{
			get
			{
				return DiceExpression.Parse(this.ExpressionBox.Text);
			}
			set
			{
				this.ExpressionBox.Text = (value != null ? value.ToString() : "");
			}
		}

		public Pair<int, int> SelectedDie
		{
			get
			{
				if (this.DiceSourceList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.DiceSourceList.SelectedItems[0].Tag as Pair<int, int>;
			}
		}

		public Pair<int, int> SelectedRoll
		{
			get
			{
				if (this.DiceList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.DiceList.SelectedItems[0].Tag as Pair<int, int>;
			}
		}

		public DicePanel()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fCentred.Alignment = StringAlignment.Center;
			this.fCentred.LineAlignment = StringAlignment.Center;
		}

		private void add_die(int sides)
		{
			int num = Session.Dice(1, sides);
			this.fDice.Add(new Pair<int, int>(sides, num));
			this.fDice.Sort(new DicePanel.DiceSorter());
			this.update_dice_rolls();
			this.update_dice_result();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RollBtn.Enabled = this.fDice.Count != 0;
			this.ClearBtn.Enabled = this.fDice.Count != 0;
			this.OddsBtn.Enabled = this.fDice.Count != 0;
		}

		private void ClearBtn_Click(object sender, EventArgs e)
		{
			this.fDice.Clear();
			this.fConstant = 0;
			this.update_dice_rolls();
			this.update_dice_result();
		}

		private void DiceList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedRoll != null)
			{
				this.SelectedRoll.Second = Session.Dice(1, this.SelectedRoll.First);
				this.update_dice_rolls();
				this.update_dice_result();
			}
		}

		private void DiceList_DragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetData(typeof(Pair<int, int>)) is Pair<int, int>)
			{
				e.Effect = DragDropEffects.Move;
			}
		}

		private void DiceSourceList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedDie != null)
			{
				this.add_die(this.SelectedDie.First);
			}
		}

		private void DiceSourceList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedDie != null && base.DoDragDrop(this.SelectedDie, DragDropEffects.Move) == DragDropEffects.Move)
			{
				this.add_die(this.SelectedDie.First);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void ExpressionBox_TextChanged(object sender, EventArgs e)
		{
			if (this.fUpdating)
			{
				return;
			}
			DiceExpression diceExpression = DiceExpression.Parse(this.ExpressionBox.Text);
			if (diceExpression != null)
			{
				this.fUpdating = true;
				this.ClearBtn_Click(sender, e);
				this.fConstant = diceExpression.Constant;
				for (int i = 0; i != diceExpression.Throws; i++)
				{
					this.add_die(diceExpression.Sides);
				}
				this.fUpdating = false;
			}
		}

		private Image get_image(int sides, string caption)
		{
			Bitmap bitmap = new Bitmap(32, 32);
			Graphics graphic = Graphics.FromImage(bitmap);
			RectangleF rectangleF = new RectangleF(0f, 0f, 31f, 31f);
			int num = sides;
			switch (num)
			{
				case 4:
				{
					float width = rectangleF.Width / 6f;
					PointF pointF = new PointF(rectangleF.Left, rectangleF.Bottom - width);
					PointF pointF1 = new PointF(rectangleF.Right, rectangleF.Bottom - width);
					PointF pointF2 = new PointF(rectangleF.Left + rectangleF.Width / 2f, rectangleF.Top);
					Brush lightGray = Brushes.LightGray;
					PointF[] pointFArray = new PointF[] { pointF, pointF1, pointF2 };
					graphic.FillPolygon(lightGray, pointFArray);
					Pen gray = Pens.Gray;
					PointF[] pointFArray1 = new PointF[] { pointF, pointF1, pointF2 };
					graphic.DrawPolygon(gray, pointFArray1);
					graphic.DrawString(caption, this.Font, SystemBrushes.WindowText, rectangleF, this.fCentred);
					return bitmap;
				}
				case 5:
				case 7:
				case 9:
				case 11:
				{
					graphic.DrawString(caption, this.Font, SystemBrushes.WindowText, rectangleF, this.fCentred);
					return bitmap;
				}
				case 6:
				{
					float single = rectangleF.Width / 8f;
					RectangleF rectangleF1 = new RectangleF(rectangleF.X + single, rectangleF.Y + single, rectangleF.Width - 2f * single, rectangleF.Height - 2f * single);
					graphic.FillRectangle(Brushes.LightGray, rectangleF1);
					graphic.DrawRectangle(Pens.Gray, rectangleF1.X, rectangleF1.Y, rectangleF1.Width, rectangleF1.Height);
					graphic.DrawString(caption, this.Font, SystemBrushes.WindowText, rectangleF, this.fCentred);
					return bitmap;
				}
				case 8:
				{
					float width1 = rectangleF.Width / 8f;
					PointF pointF3 = new PointF(rectangleF.Left + width1, rectangleF.Top + rectangleF.Height / 2f);
					PointF pointF4 = new PointF(rectangleF.Right - width1, rectangleF.Top + rectangleF.Height / 2f);
					PointF pointF5 = new PointF(rectangleF.Left + rectangleF.Width / 2f, rectangleF.Top);
					PointF pointF6 = new PointF(rectangleF.Left + rectangleF.Width / 2f, rectangleF.Bottom);
					Brush brush = Brushes.LightGray;
					PointF[] pointFArray2 = new PointF[] { pointF3, pointF6, pointF4, pointF5 };
					graphic.FillPolygon(brush, pointFArray2);
					Pen pen = Pens.Gray;
					PointF[] pointFArray3 = new PointF[] { pointF3, pointF6, pointF4, pointF5 };
					graphic.DrawPolygon(pen, pointFArray3);
					graphic.DrawString(caption, this.Font, SystemBrushes.WindowText, rectangleF, this.fCentred);
					return bitmap;
				}
				case 10:
				{
					float left = rectangleF.Left + rectangleF.Width / 2f;
					float top = rectangleF.Top + rectangleF.Height / 2f;
					List<PointF> pointFs = new List<PointF>();
					for (int i = 0; i != 10; i++)
					{
						float single1 = rectangleF.Width / 2f;
						double num1 = (double)i * 6.28318530717959 / 10;
						double num2 = (double)single1 * Math.Sin(num1);
						double num3 = (double)single1 * Math.Cos(num1);
						pointFs.Add(new PointF((float)((double)left + num2), (float)((double)top + num3)));
					}
					graphic.FillPolygon(Brushes.LightGray, pointFs.ToArray());
					graphic.DrawPolygon(Pens.Gray, pointFs.ToArray());
					graphic.DrawString(caption, this.Font, SystemBrushes.WindowText, rectangleF, this.fCentred);
					return bitmap;
				}
				case 12:
				{
					float width2 = rectangleF.Width / 3f;
					PointF pointF7 = new PointF(rectangleF.Left, rectangleF.Top + rectangleF.Height / 2f);
					PointF pointF8 = new PointF(rectangleF.Right, rectangleF.Top + rectangleF.Height / 2f);
					PointF pointF9 = new PointF(rectangleF.Left + width2, rectangleF.Top);
					PointF pointF10 = new PointF(rectangleF.Right - width2, rectangleF.Top);
					PointF pointF11 = new PointF(rectangleF.Left + width2, rectangleF.Bottom);
					PointF pointF12 = new PointF(rectangleF.Right - width2, rectangleF.Bottom);
					Brush lightGray1 = Brushes.LightGray;
					PointF[] pointFArray4 = new PointF[] { pointF7, pointF9, pointF10, pointF8, pointF12, pointF11 };
					graphic.FillPolygon(lightGray1, pointFArray4);
					Pen gray1 = Pens.Gray;
					PointF[] pointFArray5 = new PointF[] { pointF7, pointF9, pointF10, pointF8, pointF12, pointF11 };
					graphic.DrawPolygon(gray1, pointFArray5);
					graphic.DrawString(caption, this.Font, SystemBrushes.WindowText, rectangleF, this.fCentred);
					return bitmap;
				}
				default:
				{
					if (num == 20)
					{
						float single2 = rectangleF.Width / 5f;
						PointF pointF13 = new PointF(rectangleF.Left, rectangleF.Top + single2);
						PointF pointF14 = new PointF(rectangleF.Left, rectangleF.Bottom - single2);
						PointF pointF15 = new PointF(rectangleF.Right, rectangleF.Top + single2);
						PointF pointF16 = new PointF(rectangleF.Right, rectangleF.Bottom - single2);
						PointF pointF17 = new PointF(rectangleF.Left + rectangleF.Width / 2f, rectangleF.Top);
						PointF pointF18 = new PointF(rectangleF.Left + rectangleF.Width / 2f, rectangleF.Bottom);
						Brush brush1 = Brushes.LightGray;
						PointF[] pointFArray6 = new PointF[] { pointF13, pointF14, pointF18, pointF16, pointF15, pointF17 };
						graphic.FillPolygon(brush1, pointFArray6);
						Pen pen1 = Pens.Gray;
						PointF[] pointFArray7 = new PointF[] { pointF13, pointF14, pointF18, pointF16, pointF15, pointF17 };
						graphic.DrawPolygon(pen1, pointFArray7);
						graphic.DrawString(caption, this.Font, SystemBrushes.WindowText, rectangleF, this.fCentred);
						return bitmap;
					}
					else
					{
						graphic.DrawString(caption, this.Font, SystemBrushes.WindowText, rectangleF, this.fCentred);
						return bitmap;
					}
				}
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(DicePanel));
			this.DiceToolbar = new ToolStrip();
			this.RollBtn = new ToolStripButton();
			this.ClearBtn = new ToolStripButton();
			this.OddsBtn = new ToolStripButton();
			this.DiceLbl = new Label();
			this.DiceList = new ListView();
			this.DiceSourceList = new ListView();
			this.ResultPanel = new Panel();
			this.ExpressionBox = new TextBox();
			this.DiceToolbar.SuspendLayout();
			this.ResultPanel.SuspendLayout();
			base.SuspendLayout();
			ToolStripItemCollection items = this.DiceToolbar.Items;
			ToolStripItem[] rollBtn = new ToolStripItem[] { this.RollBtn, this.ClearBtn, this.OddsBtn };
			items.AddRange(rollBtn);
			this.DiceToolbar.Location = new Point(0, 0);
			this.DiceToolbar.Name = "DiceToolbar";
			this.DiceToolbar.Size = new System.Drawing.Size(287, 25);
			this.DiceToolbar.TabIndex = 8;
			this.DiceToolbar.Text = "toolStrip1";
			this.RollBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RollBtn.Image = (Image)componentResourceManager.GetObject("RollBtn.Image");
			this.RollBtn.ImageTransparentColor = Color.Magenta;
			this.RollBtn.Name = "RollBtn";
			this.RollBtn.Size = new System.Drawing.Size(41, 22);
			this.RollBtn.Text = "Reroll";
			this.RollBtn.Click += new EventHandler(this.RollBtn_Click);
			this.ClearBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ClearBtn.Image = (Image)componentResourceManager.GetObject("ClearBtn.Image");
			this.ClearBtn.ImageTransparentColor = Color.Magenta;
			this.ClearBtn.Name = "ClearBtn";
			this.ClearBtn.Size = new System.Drawing.Size(38, 22);
			this.ClearBtn.Text = "Clear";
			this.ClearBtn.Click += new EventHandler(this.ClearBtn_Click);
			this.OddsBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.OddsBtn.Image = (Image)componentResourceManager.GetObject("OddsBtn.Image");
			this.OddsBtn.ImageTransparentColor = Color.Magenta;
			this.OddsBtn.Name = "OddsBtn";
			this.OddsBtn.Size = new System.Drawing.Size(39, 22);
			this.OddsBtn.Text = "Odds";
			this.OddsBtn.Click += new EventHandler(this.OddsBtn_Click);
			this.DiceLbl.Dock = DockStyle.Fill;
			this.DiceLbl.Font = new System.Drawing.Font("Calibri", 30f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.DiceLbl.Location = new Point(0, 0);
			this.DiceLbl.Name = "DiceLbl";
			this.DiceLbl.Size = new System.Drawing.Size(287, 50);
			this.DiceLbl.TabIndex = 7;
			this.DiceLbl.Text = "-";
			this.DiceLbl.TextAlign = ContentAlignment.MiddleCenter;
			this.DiceList.AllowDrop = true;
			this.DiceList.Dock = DockStyle.Fill;
			this.DiceList.Location = new Point(0, 169);
			this.DiceList.Name = "DiceList";
			this.DiceList.Size = new System.Drawing.Size(287, 136);
			this.DiceList.TabIndex = 6;
			this.DiceList.UseCompatibleStateImageBehavior = false;
			this.DiceList.DoubleClick += new EventHandler(this.DiceList_DoubleClick);
			this.DiceList.DragOver += new DragEventHandler(this.DiceList_DragOver);
			this.DiceSourceList.Dock = DockStyle.Top;
			this.DiceSourceList.Location = new Point(0, 25);
			this.DiceSourceList.Name = "DiceSourceList";
			this.DiceSourceList.Size = new System.Drawing.Size(287, 144);
			this.DiceSourceList.TabIndex = 5;
			this.DiceSourceList.UseCompatibleStateImageBehavior = false;
			this.DiceSourceList.DoubleClick += new EventHandler(this.DiceSourceList_DoubleClick);
			this.DiceSourceList.ItemDrag += new ItemDragEventHandler(this.DiceSourceList_ItemDrag);
			this.ResultPanel.Controls.Add(this.DiceLbl);
			this.ResultPanel.Controls.Add(this.ExpressionBox);
			this.ResultPanel.Dock = DockStyle.Bottom;
			this.ResultPanel.Location = new Point(0, 235);
			this.ResultPanel.Name = "ResultPanel";
			this.ResultPanel.Size = new System.Drawing.Size(287, 70);
			this.ResultPanel.TabIndex = 10;
			this.ExpressionBox.Dock = DockStyle.Bottom;
			this.ExpressionBox.Location = new Point(0, 50);
			this.ExpressionBox.Name = "ExpressionBox";
			this.ExpressionBox.Size = new System.Drawing.Size(287, 20);
			this.ExpressionBox.TabIndex = 10;
			this.ExpressionBox.TextAlign = HorizontalAlignment.Center;
			this.ExpressionBox.TextChanged += new EventHandler(this.ExpressionBox_TextChanged);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.ResultPanel);
			base.Controls.Add(this.DiceList);
			base.Controls.Add(this.DiceSourceList);
			base.Controls.Add(this.DiceToolbar);
			base.Name = "DicePanel";
			base.Size = new System.Drawing.Size(287, 305);
			this.DiceToolbar.ResumeLayout(false);
			this.DiceToolbar.PerformLayout();
			this.ResultPanel.ResumeLayout(false);
			this.ResultPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OddsBtn_Click(object sender, EventArgs e)
		{
			List<int> nums = new List<int>();
			foreach (Pair<int, int> pair in this.fDice)
			{
				nums.Add(pair.First);
			}
			OddsForm oddsForm = new OddsForm(nums, this.fConstant, this.ExpressionBox.Text);
			oddsForm.ShowDialog();
		}

		private void RollBtn_Click(object sender, EventArgs e)
		{
			foreach (Pair<int, int> pair in this.fDice)
			{
				pair.Second = Session.Dice(1, pair.First);
			}
			this.fDice.Sort(new DicePanel.DiceSorter());
			this.update_dice_rolls();
			this.update_dice_result();
		}

		private void update_dice_result()
		{
			if (this.fDice.Count == 0)
			{
				this.DiceLbl.ForeColor = SystemColors.GrayText;
				this.DiceLbl.Text = "-";
				return;
			}
			int second = this.fConstant;
			foreach (Pair<int, int> pair in this.fDice)
			{
				second += pair.Second;
			}
			this.DiceLbl.ForeColor = SystemColors.WindowText;
			this.DiceLbl.Text = second.ToString();
		}

		private void update_dice_rolls()
		{
			this.DiceList.Items.Clear();
			this.DiceList.LargeImageList = new ImageList()
			{
				ImageSize = new System.Drawing.Size(32, 32)
			};
			List<int> nums = new List<int>();
			foreach (Pair<int, int> pair in this.fDice)
			{
				ListViewItem count = this.DiceList.Items.Add("");
				count.Tag = pair;
				ImageList.ImageCollection images = this.DiceList.LargeImageList.Images;
				int first = pair.First;
				int second = pair.Second;
				images.Add(this.get_image(first, second.ToString()));
				count.ImageIndex = this.DiceList.LargeImageList.Images.Count - 1;
				nums.Add(pair.First);
			}
			if (!this.fUpdating)
			{
				this.fUpdating = true;
				this.ExpressionBox.Text = (this.fDice.Count != 0 ? DiceStatistics.Expression(nums, this.fConstant) : "");
				this.fUpdating = false;
			}
		}

		private void update_dice_source()
		{
			this.DiceSourceList.Items.Clear();
			List<int> nums = new List<int>()
			{
				4,
				6,
				8,
				10,
				12,
				20
			};
			this.DiceSourceList.LargeImageList = new ImageList()
			{
				ImageSize = new System.Drawing.Size(32, 32)
			};
			foreach (int num in nums)
			{
				string str = string.Concat("d", num);
				ListViewItem pair = this.DiceSourceList.Items.Add("");
				pair.Tag = new Pair<int, int>(num, -1);
				this.DiceSourceList.LargeImageList.Images.Add(this.get_image(num, str));
				pair.ImageIndex = this.DiceSourceList.LargeImageList.Images.Count - 1;
			}
		}

		public void UpdateView()
		{
			this.update_dice_source();
			this.update_dice_rolls();
			this.update_dice_result();
		}

		private class DiceSorter : IComparer<Pair<int, int>>
		{
			public DiceSorter()
			{
			}

			public int Compare(Pair<int, int> lhs, Pair<int, int> rhs)
			{
				int num = lhs.First.CompareTo(rhs.First);
				if (num == 0)
				{
					num = lhs.Second.CompareTo(rhs.Second);
				}
				return num;
			}
		}
	}
}