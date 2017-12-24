using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Utils;
using Utils.Graphics;

namespace Masterplan.Controls
{
	internal class CardDeck : UserControl
	{
		private IContainer components;

		private List<Pair<EncounterCard, int>> fCards;

		private StringFormat fCentred = new StringFormat();

		private StringFormat fTitle = new StringFormat();

		private StringFormat fInfo = new StringFormat();

		private float fRadius = 10f;

		private int fVisibleCards;

		private List<Pair<RectangleF, EncounterCard>> fRegions;

		private EncounterCard fHoverCard;

		public EncounterCard TopCard
		{
			get
			{
				if (this.fCards == null || this.fCards.Count == 0)
				{
					return null;
				}
				return this.fCards[0].First;
			}
		}

		public CardDeck()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.fCentred.Alignment = StringAlignment.Center;
			this.fCentred.LineAlignment = StringAlignment.Center;
			this.fCentred.Trimming = StringTrimming.EllipsisWord;
			this.fTitle.Alignment = StringAlignment.Near;
			this.fTitle.LineAlignment = StringAlignment.Center;
			this.fTitle.Trimming = StringTrimming.Character;
			this.fInfo.Alignment = StringAlignment.Far;
			this.fInfo.LineAlignment = StringAlignment.Center;
			this.fInfo.Trimming = StringTrimming.Character;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void draw_card(EncounterCard card, int count, bool topmost, RectangleF rect, Graphics g)
		{
			int num = (card != null ? 255 : 100);
			GraphicsPath graphicsPath = RoundedRectangle.Create(rect, this.fRadius, RoundedRectangle.RectangleCorners.TopLeft | RoundedRectangle.RectangleCorners.TopRight);
			using (Brush solidBrush = new SolidBrush(Color.FromArgb(num, 54, 79, 39)))
			{
				g.FillPath(solidBrush, graphicsPath);
			}
			g.DrawPath(Pens.White, graphicsPath);
			float height = (float)this.Font.Height * 1.5f;
			RectangleF rectangleF = new RectangleF(rect.X + this.fRadius, rect.Y, rect.Width - 2f * this.fRadius, height);
			if (card == null)
			{
				int num1 = this.fCards.Count - this.fVisibleCards;
				g.DrawString(string.Concat("(", num1, " more cards)"), this.Font, Brushes.White, rectangleF, this.fCentred);
			}
			else
			{
				string title = card.Title;
				if (count > 1)
				{
					object[] objArray = new object[] { "(", count, "x) ", title };
					title = string.Concat(objArray);
				}
				using (Brush brush = new SolidBrush((card != this.fHoverCard ? Color.White : Color.PaleGreen)))
				{
					using (System.Drawing.Font font = new System.Drawing.Font(this.Font, this.Font.Style | FontStyle.Bold))
					{
						g.DrawString(title, font, brush, rectangleF, this.fTitle);
					}
					g.DrawString(card.Info, this.Font, brush, rectangleF, this.fInfo);
				}
				if (topmost)
				{
					float single = this.fRadius * 0.2f;
					RectangleF rectangleF1 = new RectangleF(rect.X + single, rect.Y + rectangleF.Height, rect.Width - 2f * single, rect.Height - rectangleF.Height);
					using (Brush solidBrush1 = new SolidBrush(Color.FromArgb(225, 231, 197)))
					{
						g.FillRectangle(solidBrush1, rectangleF1);
					}
					string str = "Click on a card to move it to the front of the deck.";
					g.DrawString(str, this.Font, Brushes.Black, rectangleF1, this.fCentred);
					return;
				}
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		}

		protected void OnDeckOrderChanged()
		{
			if (this.DeckOrderChanged != null)
			{
				this.DeckOrderChanged(this, new EventArgs());
			}
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			if (this.fHoverCard == null)
			{
				return;
			}
			EncounterCard encounterCard = this.fHoverCard;
			this.fHoverCard = null;
			while (this.fCards[0].First != encounterCard)
			{
				Pair<EncounterCard, int> item = this.fCards[0];
				this.fCards.RemoveAt(0);
				this.fCards.Add(item);
				this.Refresh();
			}
			this.OnDeckOrderChanged();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			this.fHoverCard = null;
			base.Invalidate();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (this.fRegions == null)
			{
				return;
			}
			EncounterCard second = null;
			foreach (Pair<RectangleF, EncounterCard> fRegion in this.fRegions)
			{
				if (fRegion.First.Top > (float)e.Location.Y || fRegion.First.Bottom < (float)e.Location.Y)
				{
					continue;
				}
				second = fRegion.Second;
			}
			this.fHoverCard = second;
			base.Invalidate();
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if (e.Delta <= 0)
			{
				Pair<EncounterCard, int> item = this.fCards[this.fCards.Count - 1];
				this.fCards.RemoveAt(this.fCards.Count - 1);
				this.fCards.Insert(0, item);
			}
			else
			{
				Pair<EncounterCard, int> pair = this.fCards[0];
				this.fCards.RemoveAt(0);
				this.fCards.Add(pair);
			}
			this.fHoverCard = null;
			this.Refresh();
			this.OnDeckOrderChanged();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.FillRectangle(Brushes.Transparent, base.ClientRectangle);
			if (this.fCards == null || this.fCards.Count == 0)
			{
				e.Graphics.DrawString("(no cards)", this.Font, Brushes.Black, base.ClientRectangle, this.fCentred);
				return;
			}
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			float x = (float)base.ClientRectangle.X;
			float y = (float)base.ClientRectangle.Y;
			float width = (float)(base.ClientRectangle.Width - 1);
			Rectangle clientRectangle = base.ClientRectangle;
			RectangleF rectangleF = new RectangleF(x, y, width, (float)(clientRectangle.Height - 1));
			this.fRegions = new List<Pair<RectangleF, EncounterCard>>();
			float height = (float)this.Font.Height * 1.8f;
			float single = height * 0.2f;
			float height1 = (float)base.Height - 4f * this.fRadius;
			int num = (int)(height1 / height);
			this.fVisibleCards = Math.Min(num, this.fCards.Count);
			if (this.fVisibleCards + 1 == this.fCards.Count)
			{
				this.fVisibleCards++;
			}
			bool count = this.fCards.Count > this.fVisibleCards;
			int num1 = (count ? this.fVisibleCards + 1 : this.fVisibleCards);
			if (count)
			{
				float single1 = single * (float)this.fVisibleCards;
				float x1 = rectangleF.X + single1;
				float y1 = rectangleF.Y;
				float width1 = rectangleF.Width - single * (float)(num1 - 1);
				float height2 = rectangleF.Height - y1;
				RectangleF rectangleF1 = new RectangleF(x1, y1, width1, height2);
				this.draw_card(null, 0, false, rectangleF1, e.Graphics);
			}
			for (int i = this.fVisibleCards - 1; i >= 0; i--)
			{
				float single2 = single * (float)i;
				float single3 = height * (float)(num1 - i - 1);
				float x2 = rectangleF.X + single2;
				float y2 = rectangleF.Y + single3;
				float width2 = rectangleF.Width - single * (float)(num1 - 1);
				float height3 = rectangleF.Height - y2;
				RectangleF rectangleF2 = new RectangleF(x2, y2, width2, height3);
				Pair<EncounterCard, int> item = this.fCards[i];
				bool flag = i == 0;
				this.draw_card(item.First, item.Second, flag, rectangleF2, e.Graphics);
				this.fRegions.Add(new Pair<RectangleF, EncounterCard>(rectangleF2, item.First));
			}
		}

		public void SetCards(List<EncounterCard> cards)
		{
			this.fCards = new List<Pair<EncounterCard, int>>();
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			foreach (EncounterCard card in cards)
			{
				binarySearchTree.Add(card.Title);
			}
			foreach (string sortedList in binarySearchTree.SortedList)
			{
				Pair<EncounterCard, int> pair = new Pair<EncounterCard, int>();
				foreach (EncounterCard encounterCard in cards)
				{
					if (encounterCard.Title != sortedList)
					{
						continue;
					}
					pair.First = encounterCard;
					Pair<EncounterCard, int> second = pair;
					second.Second = second.Second + 1;
				}
				this.fCards.Add(pair);
			}
			base.Invalidate();
		}

		public event EventHandler DeckOrderChanged;
	}
}