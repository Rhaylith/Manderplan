using Masterplan;
using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class DeckListForm : Form
	{
		private IContainer components;

		private ToolStrip Toolbar;

		private ListView DeckList;

		private ColumnHeader NameHdr;

		private ToolStripButton AddBtn;

		private ToolStripButton RemoveBtn;

		private ColumnHeader LevelHdr;

		private ColumnHeader CardsHdr;

		private ToolStripButton EditBtn;

		private Panel MainPanel;

		private Button CloseBtn;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton ViewBtn;

		private ToolStripSplitButton RunBtn;

		private ToolStripMenuItem RunMap;

		public EncounterDeck SelectedDeck
		{
			get
			{
				if (this.DeckList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.DeckList.SelectedItems[0].Tag as EncounterDeck;
			}
		}

		public DeckListForm()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.update_decks();
		}

		private void AddBtn_Click(object sender, EventArgs e)
		{
			EncounterDeck encounterDeck = new EncounterDeck()
			{
				Name = "New Deck",
				Level = Session.Project.Party.Level
			};
			DeckBuilderForm deckBuilderForm = new DeckBuilderForm(encounterDeck);
			if (deckBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Project.Decks.Add(deckBuilderForm.Deck);
				Session.Modified = true;
				this.update_decks();
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveBtn.Enabled = this.SelectedDeck != null;
			this.EditBtn.Enabled = this.SelectedDeck != null;
			this.ViewBtn.Enabled = (this.SelectedDeck == null ? false : this.SelectedDeck.Cards.Count != 0);
			this.RunBtn.Enabled = (this.SelectedDeck == null ? false : this.SelectedDeck.Cards.Count != 0);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedDeck != null)
			{
				int deck = Session.Project.Decks.IndexOf(this.SelectedDeck);
				DeckBuilderForm deckBuilderForm = new DeckBuilderForm(this.SelectedDeck);
				if (deckBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.Decks[deck] = deckBuilderForm.Deck;
					Session.Modified = true;
					this.update_decks();
				}
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(DeckListForm));
			this.Toolbar = new ToolStrip();
			this.AddBtn = new ToolStripButton();
			this.RemoveBtn = new ToolStripButton();
			this.EditBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.ViewBtn = new ToolStripButton();
			this.RunBtn = new ToolStripSplitButton();
			this.RunMap = new ToolStripMenuItem();
			this.DeckList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.LevelHdr = new ColumnHeader();
			this.CardsHdr = new ColumnHeader();
			this.MainPanel = new Panel();
			this.CloseBtn = new Button();
			this.Toolbar.SuspendLayout();
			this.MainPanel.SuspendLayout();
			base.SuspendLayout();
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] addBtn = new ToolStripItem[] { this.AddBtn, this.RemoveBtn, this.EditBtn, this.toolStripSeparator1, this.ViewBtn, this.RunBtn };
			items.AddRange(addBtn);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(378, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.AddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AddBtn.Image = (Image)componentResourceManager.GetObject("AddBtn.Image");
			this.AddBtn.ImageTransparentColor = Color.Magenta;
			this.AddBtn.Name = "AddBtn";
			this.AddBtn.Size = new System.Drawing.Size(33, 22);
			this.AddBtn.Text = "Add";
			this.AddBtn.Click += new EventHandler(this.AddBtn_Click);
			this.RemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveBtn.Image = (Image)componentResourceManager.GetObject("RemoveBtn.Image");
			this.RemoveBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.RemoveBtn.Text = "Remove";
			this.RemoveBtn.Click += new EventHandler(this.RemoveBtn_Click);
			this.EditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditBtn.Image = (Image)componentResourceManager.GetObject("EditBtn.Image");
			this.EditBtn.ImageTransparentColor = Color.Magenta;
			this.EditBtn.Name = "EditBtn";
			this.EditBtn.Size = new System.Drawing.Size(31, 22);
			this.EditBtn.Text = "Edit";
			this.EditBtn.Click += new EventHandler(this.EditBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.ViewBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ViewBtn.Image = (Image)componentResourceManager.GetObject("ViewBtn.Image");
			this.ViewBtn.ImageTransparentColor = Color.Magenta;
			this.ViewBtn.Name = "ViewBtn";
			this.ViewBtn.Size = new System.Drawing.Size(69, 22);
			this.ViewBtn.Text = "View Cards";
			this.ViewBtn.Click += new EventHandler(this.ViewBtn_Click);
			this.RunBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RunBtn.DropDownItems.AddRange(new ToolStripItem[] { this.RunMap });
			this.RunBtn.Image = (Image)componentResourceManager.GetObject("RunBtn.Image");
			this.RunBtn.ImageTransparentColor = Color.Magenta;
			this.RunBtn.Name = "RunBtn";
			this.RunBtn.Size = new System.Drawing.Size(101, 22);
			this.RunBtn.Text = "Run Encounter";
			this.RunBtn.ButtonClick += new EventHandler(this.RunBtn_Click);
			this.RunMap.Name = "RunMap";
			this.RunMap.Size = new System.Drawing.Size(150, 22);
			this.RunMap.Text = "Choose Map...";
			this.RunMap.Click += new EventHandler(this.RunMap_Click);
			ListView.ColumnHeaderCollection columns = this.DeckList.Columns;
			ColumnHeader[] nameHdr = new ColumnHeader[] { this.NameHdr, this.LevelHdr, this.CardsHdr };
			columns.AddRange(nameHdr);
			this.DeckList.Dock = DockStyle.Fill;
			this.DeckList.FullRowSelect = true;
			this.DeckList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.DeckList.HideSelection = false;
			this.DeckList.Location = new Point(0, 25);
			this.DeckList.MultiSelect = false;
			this.DeckList.Name = "DeckList";
			this.DeckList.Size = new System.Drawing.Size(378, 255);
			this.DeckList.TabIndex = 1;
			this.DeckList.UseCompatibleStateImageBehavior = false;
			this.DeckList.View = View.Details;
			this.DeckList.DoubleClick += new EventHandler(this.ViewBtn_Click);
			this.NameHdr.Text = "Deck";
			this.NameHdr.Width = 225;
			this.LevelHdr.Text = "Level";
			this.LevelHdr.TextAlign = HorizontalAlignment.Right;
			this.CardsHdr.Text = "Cards";
			this.CardsHdr.TextAlign = HorizontalAlignment.Right;
			this.MainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.MainPanel.Controls.Add(this.DeckList);
			this.MainPanel.Controls.Add(this.Toolbar);
			this.MainPanel.Location = new Point(12, 12);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new System.Drawing.Size(378, 280);
			this.MainPanel.TabIndex = 2;
			this.CloseBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CloseBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.CloseBtn.Location = new Point(315, 298);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new System.Drawing.Size(75, 23);
			this.CloseBtn.TabIndex = 3;
			this.CloseBtn.Text = "Close";
			this.CloseBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.CloseBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(402, 333);
			base.Controls.Add(this.CloseBtn);
			base.Controls.Add(this.MainPanel);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DeckListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Encounter Decks";
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.MainPanel.ResumeLayout(false);
			this.MainPanel.PerformLayout();
			base.ResumeLayout(false);
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedDeck != null)
			{
				Session.Project.Decks.Remove(this.SelectedDeck);
				Session.Modified = true;
				this.update_decks();
			}
		}

		private void run_encounter(EncounterDeck deck, bool choose_map)
		{
			MapAreaSelectForm mapAreaSelectForm = null;
			if (choose_map)
			{
				mapAreaSelectForm = new MapAreaSelectForm(Guid.Empty, Guid.Empty);
				if (mapAreaSelectForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
				{
					return;
				}
			}
			Encounter encounter = new Encounter();
			bool flag = deck.DrawEncounter(encounter);
			this.update_decks();
			if (!flag)
			{
				MessageBox.Show("An encounter could not be built from this deck; check that there are enough cards remaining.", "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			CombatState combatState = new CombatState()
			{
				Encounter = encounter,
				PartyLevel = Session.Project.Party.Level
			};
			if (mapAreaSelectForm != null && mapAreaSelectForm.Map != null)
			{
				combatState.Encounter.MapID = mapAreaSelectForm.Map.ID;
				if (mapAreaSelectForm.MapArea != null)
				{
					combatState.Encounter.MapAreaID = mapAreaSelectForm.MapArea.ID;
				}
			}
			(new CombatForm(combatState)).Show();
		}

		private void RunBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedDeck != null)
			{
				this.run_encounter(this.SelectedDeck, false);
			}
		}

		private void RunMap_Click(object sender, EventArgs e)
		{
			if (this.SelectedDeck != null)
			{
				this.run_encounter(this.SelectedDeck, true);
			}
		}

		private void update_decks()
		{
			this.DeckList.Items.Clear();
			foreach (EncounterDeck deck in Session.Project.Decks)
			{
				int num = 0;
				foreach (EncounterCard card in deck.Cards)
				{
					if (card.Drawn)
					{
						continue;
					}
					num++;
				}
				string str = deck.Cards.Count.ToString();
				if (num != deck.Cards.Count)
				{
					str = string.Concat(num, " / ", deck.Cards.Count);
				}
				ListViewItem listViewItem = this.DeckList.Items.Add(deck.Name);
				listViewItem.SubItems.Add(deck.Level.ToString());
				listViewItem.SubItems.Add(str);
				listViewItem.Tag = deck;
			}
			if (this.DeckList.Items.Count == 0)
			{
				ListViewItem grayText = this.DeckList.Items.Add("(no decks)");
				grayText.ForeColor = SystemColors.GrayText;
			}
		}

		private void ViewBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedDeck != null)
			{
				(new DeckViewForm(this.SelectedDeck.Cards)).ShowDialog();
			}
		}
	}
}