using Masterplan;
using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class HeroListForm : Form
	{
		private ToolStrip Toolbar;

		private ListView HeroList;

		private ColumnHeader NameHdr;

		private ToolStripButton RemoveBtn;

		private ToolStripButton EditBtn;

		private ColumnHeader CharHdr;

		private TabControl Pages;

		private TabPage DetailsPage;

		private TabPage OverviewPage;

		private BreakdownPanel BreakdownPnl;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton PlayerViewBtn;

		private ColumnHeader InsightHdr;

		private ColumnHeader PercHdr;

		private Button CloseBtn;

		private ToolStripSplitButton AddBtn;

		private ToolStripMenuItem Import_CB;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton StatBlockBtn;

		private ToolStripMenuItem Import_iPlay4e;

		private Button UpdateBtn;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem AddRandomCharacter;

		private ToolStripMenuItem AddRandomParty;

		private ToolStripMenuItem Import_iPlay4e_Party;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton ActiveBtn;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripMenuItem AddSuggest;

		private ToolStripButton EntryBtn;

		private TabPage ParcelPage;

		private ListView ParcelList;

		private ColumnHeader ParcelHdr;

		private ColumnHeader ParcelDetailsHdr;

		private StatusStrip StatusBar;

		private ToolStripStatusLabel PartySizeLbl;

		public Hero SelectedHero
		{
			get
			{
				if (this.HeroList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.HeroList.SelectedItems[0].Tag as Hero;
			}
		}

		public Parcel SelectedParcel
		{
			get
			{
				if (this.ParcelList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.ParcelList.SelectedItems[0].Tag as Parcel;
			}
		}

		public HeroListForm()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.BreakdownPnl.Heroes = Session.Project.Heroes;
			this.update_view();
		}

		private void ActiveBtn_Click(object sender, EventArgs e)
		{
			Hero selectedHero = this.SelectedHero;
			if (selectedHero == null)
			{
				return;
			}
			if (!Session.Project.Heroes.Contains(selectedHero))
			{
				Session.Project.InactiveHeroes.Remove(selectedHero);
				Session.Project.Heroes.Add(selectedHero);
			}
			else
			{
				Session.Project.Heroes.Remove(selectedHero);
				Session.Project.InactiveHeroes.Add(selectedHero);
			}
			Session.Modified = true;
			this.update_view();
		}

		private void add_hero(Hero hero)
		{
			Hero hero1 = Session.Project.FindHero(hero.Name);
			List<Hero> heros = (Session.Project.InactiveHeroes.Contains(hero1) ? Session.Project.InactiveHeroes : Session.Project.Heroes);
			if (hero1 != null)
			{
				hero.ID = hero1.ID;
				hero.Effects.AddRange(hero1.Effects);
				heros.Remove(hero1);
			}
			heros.Add(hero);
			Session.Modified = true;
		}

		private void add_parcel(Parcel parcel)
		{
			if (parcel.HeroID == Guid.Empty)
			{
				return;
			}
			Hero hero = Session.Project.FindHero(parcel.HeroID);
			if (hero == null)
			{
				return;
			}
			ListViewItem item = this.ParcelList.Items.Add(parcel.Name);
			item.SubItems.Add(parcel.Details);
			item.Tag = parcel;
			item.Group = this.ParcelList.Groups[hero.Name];
		}

		private void add_to_list(Hero hero, bool active)
		{
			string str = (hero.Name != "" ? hero.Name : "(unnamed)");
			if (hero.Player != "")
			{
				str = string.Concat(str, " (", hero.Player, ")");
			}
			ListViewItem item = this.HeroList.Items.Add(str);
			item.SubItems.Add(hero.Info);
			item.SubItems.Add(hero.PassiveInsight.ToString());
			item.SubItems.Add(hero.PassivePerception.ToString());
			item.Tag = hero;
			item.Group = this.HeroList.Groups[(active ? 0 : 1)];
		}

		private void AddBtn_Click(object sender, EventArgs e)
		{
			HeroForm heroForm = new HeroForm(new Hero()
			{
				Name = "New Character"
			});
			if (heroForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.add_hero(heroForm.Hero);
				this.update_view();
			}
		}

		private void AddSuggest_Click(object sender, EventArgs e)
		{
			HeroGroup heroGroup = new HeroGroup();
			foreach (Hero hero in Session.Project.Heroes)
			{
				RaceData race = Sourcebook.GetRace(hero.Race);
				ClassData @class = Sourcebook.GetClass(hero.Class);
				heroGroup.Heroes.Add(new HeroData(race, @class));
			}
			HeroData heroDatum = heroGroup.Suggest();
			if (heroDatum != null)
			{
				Hero hero1 = heroDatum.ConvertToHero();
				Session.Project.Heroes.Add(hero1);
			}
			this.update_view();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveBtn.Enabled = this.SelectedHero != null;
			this.EditBtn.Enabled = this.SelectedHero != null;
			this.ActiveBtn.Enabled = this.SelectedHero != null;
			this.ActiveBtn.Checked = (this.SelectedHero == null ? false : Session.Project.Heroes.Contains(this.SelectedHero));
			this.StatBlockBtn.Enabled = this.SelectedHero != null;
			this.EntryBtn.Enabled = this.SelectedHero != null;
		}

		private void edit_hero()
		{
			List<Hero> hero = (Session.Project.Heroes.Contains(this.SelectedHero) ? Session.Project.Heroes : Session.Project.InactiveHeroes);
			int num = hero.IndexOf(this.SelectedHero);
			HeroForm heroForm = new HeroForm(this.SelectedHero);
			if (heroForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				hero[num] = heroForm.Hero;
				Session.Modified = true;
				this.update_view();
			}
		}

		private void edit_iplay4e()
		{
			List<Hero> heros = (Session.Project.Heroes.Contains(this.SelectedHero) ? Session.Project.Heroes : Session.Project.InactiveHeroes);
			int num = heros.IndexOf(this.SelectedHero);
			HeroIPlay4eForm heroIPlay4eForm = new HeroIPlay4eForm(this.SelectedHero.Key, true);
			if (heroIPlay4eForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Hero hero = new Hero()
				{
					Key = heroIPlay4eForm.Key
				};
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
				bool flag = AppImport.ImportIPlay4e(hero);
				System.Windows.Forms.Cursor.Current = Cursors.Default;
				if (flag)
				{
					heros[num] = hero;
					Session.Modified = true;
					this.update_view();
				}
			}
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedHero != null)
			{
				this.edit_hero();
			}
		}

		private void EntryBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedHero == null)
			{
				return;
			}
			EncyclopediaEntry encyclopediaEntry = Session.Project.Encyclopedia.FindEntryForAttachment(this.SelectedHero.ID);
			if (encyclopediaEntry == null)
			{
				if (MessageBox.Show(string.Concat(string.Concat("There is no encyclopedia entry associated with this PC.", Environment.NewLine), "Would you like to create one now?"), "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
				{
					return;
				}
				encyclopediaEntry = new EncyclopediaEntry()
				{
					Name = this.SelectedHero.Name,
					AttachmentID = this.SelectedHero.ID,
					Category = "People"
				};
				Session.Project.Encyclopedia.Entries.Add(encyclopediaEntry);
				Session.Modified = true;
			}
			int entry = Session.Project.Encyclopedia.Entries.IndexOf(encyclopediaEntry);
			EncyclopediaEntryForm encyclopediaEntryForm = new EncyclopediaEntryForm(encyclopediaEntry);
			if (encyclopediaEntryForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Project.Encyclopedia.Entries[entry] = encyclopediaEntryForm.Entry;
				Session.Modified = true;
			}
		}

		private void Import_CB_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Filter = "Character File|*.dnd4e",
				Multiselect = true
			};
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				string[] fileNames = openFileDialog.FileNames;
				for (int i = 0; i < (int)fileNames.Length; i++)
				{
					string str = fileNames[i];
					Hero hero = AppImport.ImportHero(File.ReadAllText(str));
					if (hero == null)
					{
						MessageBox.Show("The character file could not be loaded.", "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					else
					{
						this.add_hero(hero);
						this.update_view();
					}
				}
			}
		}

		private void Import_iPlay4e_Click(object sender, EventArgs e)
		{
			try
			{
				HeroIPlay4eForm heroIPlay4eForm = new HeroIPlay4eForm("", true);
				if (heroIPlay4eForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Hero hero = new Hero()
					{
						Key = heroIPlay4eForm.Key
					};
					System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
					bool flag = AppImport.ImportIPlay4e(hero);
					System.Windows.Forms.Cursor.Current = Cursors.Default;
					if (!flag)
					{
						string str = string.Concat("The character could not be found.", Environment.NewLine);
						str = string.Concat(str, Environment.NewLine);
						str = string.Concat(str, "Make sure:");
						str = string.Concat(str, Environment.NewLine);
						str = string.Concat(str, "* The key is correct");
						str = string.Concat(str, Environment.NewLine);
						str = string.Concat(str, "* The character is public");
						MessageBox.Show(str, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					else
					{
						this.add_hero(hero);
						this.update_view();
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void Import_iPlay4e_Party_Click(object sender, EventArgs e)
		{
			try
			{
				HeroIPlay4eForm heroIPlay4eForm = new HeroIPlay4eForm("", false);
				if (heroIPlay4eForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
					List<Hero> heros = AppImport.ImportParty(heroIPlay4eForm.Key);
					System.Windows.Forms.Cursor.Current = Cursors.Default;
					foreach (Hero hero in heros)
					{
						this.add_hero(hero);
					}
					this.update_view();
					if (heros.Count == 0)
					{
						MessageBox.Show("No characters were found (make sure they are public).", "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(HeroListForm));
			ListViewGroup listViewGroup = new ListViewGroup("PCs", HorizontalAlignment.Left);
			ListViewGroup listViewGroup1 = new ListViewGroup("Inactive PCs", HorizontalAlignment.Left);
			this.Toolbar = new ToolStrip();
			this.AddBtn = new ToolStripSplitButton();
			this.Import_CB = new ToolStripMenuItem();
			this.toolStripSeparator4 = new ToolStripSeparator();
			this.Import_iPlay4e = new ToolStripMenuItem();
			this.Import_iPlay4e_Party = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.AddRandomCharacter = new ToolStripMenuItem();
			this.AddRandomParty = new ToolStripMenuItem();
			this.toolStripSeparator6 = new ToolStripSeparator();
			this.AddSuggest = new ToolStripMenuItem();
			this.RemoveBtn = new ToolStripButton();
			this.EditBtn = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.ActiveBtn = new ToolStripButton();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.PlayerViewBtn = new ToolStripButton();
			this.toolStripSeparator5 = new ToolStripSeparator();
			this.StatBlockBtn = new ToolStripButton();
			this.EntryBtn = new ToolStripButton();
			this.HeroList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.CharHdr = new ColumnHeader();
			this.InsightHdr = new ColumnHeader();
			this.PercHdr = new ColumnHeader();
			this.Pages = new TabControl();
			this.DetailsPage = new TabPage();
			this.OverviewPage = new TabPage();
			this.ParcelPage = new TabPage();
			this.ParcelList = new ListView();
			this.ParcelHdr = new ColumnHeader();
			this.ParcelDetailsHdr = new ColumnHeader();
			this.CloseBtn = new Button();
			this.UpdateBtn = new Button();
			this.StatusBar = new StatusStrip();
			this.PartySizeLbl = new ToolStripStatusLabel();
			this.BreakdownPnl = new BreakdownPanel();
			this.Toolbar.SuspendLayout();
			this.Pages.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			this.OverviewPage.SuspendLayout();
			this.ParcelPage.SuspendLayout();
			this.StatusBar.SuspendLayout();
			base.SuspendLayout();
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] addBtn = new ToolStripItem[] { this.AddBtn, this.RemoveBtn, this.EditBtn, this.toolStripSeparator2, this.ActiveBtn, this.toolStripSeparator3, this.PlayerViewBtn, this.toolStripSeparator5, this.StatBlockBtn, this.EntryBtn };
			items.AddRange(addBtn);
			this.Toolbar.Location = new Point(3, 3);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(562, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.AddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems = this.AddBtn.DropDownItems;
			ToolStripItem[] importCB = new ToolStripItem[] { this.Import_CB, this.toolStripSeparator4, this.Import_iPlay4e, this.Import_iPlay4e_Party, this.toolStripSeparator1, this.AddRandomCharacter, this.AddRandomParty, this.toolStripSeparator6, this.AddSuggest };
			dropDownItems.AddRange(importCB);
			this.AddBtn.Image = (Image)componentResourceManager.GetObject("AddBtn.Image");
			this.AddBtn.ImageTransparentColor = Color.Magenta;
			this.AddBtn.Name = "AddBtn";
			this.AddBtn.Size = new System.Drawing.Size(45, 22);
			this.AddBtn.Text = "Add";
			this.AddBtn.ButtonClick += new EventHandler(this.AddBtn_Click);
			this.Import_CB.Name = "Import_CB";
			this.Import_CB.Size = new System.Drawing.Size(242, 22);
			this.Import_CB.Text = "Import from Character Builder...";
			this.Import_CB.Click += new EventHandler(this.Import_CB_Click);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(239, 6);
			this.Import_iPlay4e.Name = "Import_iPlay4e";
			this.Import_iPlay4e.Size = new System.Drawing.Size(242, 22);
			this.Import_iPlay4e.Text = "Import Character from iPlay4e...";
			this.Import_iPlay4e.Click += new EventHandler(this.Import_iPlay4e_Click);
			this.Import_iPlay4e_Party.Name = "Import_iPlay4e_Party";
			this.Import_iPlay4e_Party.Size = new System.Drawing.Size(242, 22);
			this.Import_iPlay4e_Party.Text = "Import Party from iPlay4e...";
			this.Import_iPlay4e_Party.Click += new EventHandler(this.Import_iPlay4e_Party_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(239, 6);
			this.AddRandomCharacter.Name = "AddRandomCharacter";
			this.AddRandomCharacter.Size = new System.Drawing.Size(242, 22);
			this.AddRandomCharacter.Text = "Random Character";
			this.AddRandomCharacter.Click += new EventHandler(this.RandomPC_Click);
			this.AddRandomParty.Name = "AddRandomParty";
			this.AddRandomParty.Size = new System.Drawing.Size(242, 22);
			this.AddRandomParty.Text = "Random Party";
			this.AddRandomParty.Click += new EventHandler(this.RandomParty_Click);
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(239, 6);
			this.AddSuggest.Name = "AddSuggest";
			this.AddSuggest.Size = new System.Drawing.Size(242, 22);
			this.AddSuggest.Text = "Suggest a Character";
			this.AddSuggest.Click += new EventHandler(this.AddSuggest_Click);
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
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			this.ActiveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ActiveBtn.Image = (Image)componentResourceManager.GetObject("ActiveBtn.Image");
			this.ActiveBtn.ImageTransparentColor = Color.Magenta;
			this.ActiveBtn.Name = "ActiveBtn";
			this.ActiveBtn.Size = new System.Drawing.Size(44, 22);
			this.ActiveBtn.Text = "Active";
			this.ActiveBtn.Click += new EventHandler(this.ActiveBtn_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			this.PlayerViewBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PlayerViewBtn.Image = (Image)componentResourceManager.GetObject("PlayerViewBtn.Image");
			this.PlayerViewBtn.ImageTransparentColor = Color.Magenta;
			this.PlayerViewBtn.Name = "PlayerViewBtn";
			this.PlayerViewBtn.Size = new System.Drawing.Size(114, 22);
			this.PlayerViewBtn.Text = "Send to Player View";
			this.PlayerViewBtn.Click += new EventHandler(this.PlayerViewBtn_Click);
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			this.StatBlockBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.StatBlockBtn.Image = (Image)componentResourceManager.GetObject("StatBlockBtn.Image");
			this.StatBlockBtn.ImageTransparentColor = Color.Magenta;
			this.StatBlockBtn.Name = "StatBlockBtn";
			this.StatBlockBtn.Size = new System.Drawing.Size(63, 22);
			this.StatBlockBtn.Text = "Stat Block";
			this.StatBlockBtn.Click += new EventHandler(this.StatBlockBtn_Click);
			this.EntryBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EntryBtn.Image = (Image)componentResourceManager.GetObject("EntryBtn.Image");
			this.EntryBtn.ImageTransparentColor = Color.Magenta;
			this.EntryBtn.Name = "EntryBtn";
			this.EntryBtn.Size = new System.Drawing.Size(111, 22);
			this.EntryBtn.Text = "Encyclopedia Entry";
			this.EntryBtn.Click += new EventHandler(this.EntryBtn_Click);
			ListView.ColumnHeaderCollection columns = this.HeroList.Columns;
			ColumnHeader[] nameHdr = new ColumnHeader[] { this.NameHdr, this.CharHdr, this.InsightHdr, this.PercHdr };
			columns.AddRange(nameHdr);
			this.HeroList.Dock = DockStyle.Fill;
			this.HeroList.FullRowSelect = true;
			listViewGroup.Header = "PCs";
			listViewGroup.Name = "listViewGroup1";
			listViewGroup1.Header = "Inactive PCs";
			listViewGroup1.Name = "listViewGroup2";
			this.HeroList.Groups.AddRange(new ListViewGroup[] { listViewGroup, listViewGroup1 });
			this.HeroList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.HeroList.HideSelection = false;
			this.HeroList.Location = new Point(3, 28);
			this.HeroList.MultiSelect = false;
			this.HeroList.Name = "HeroList";
			this.HeroList.Size = new System.Drawing.Size(562, 209);
			this.HeroList.Sorting = SortOrder.Ascending;
			this.HeroList.TabIndex = 1;
			this.HeroList.UseCompatibleStateImageBehavior = false;
			this.HeroList.View = View.Details;
			this.HeroList.DoubleClick += new EventHandler(this.EditBtn_Click);
			this.NameHdr.Text = "Name";
			this.NameHdr.Width = 180;
			this.CharHdr.Text = "Character";
			this.CharHdr.Width = 200;
			this.InsightHdr.Text = "Insight";
			this.InsightHdr.TextAlign = HorizontalAlignment.Right;
			this.InsightHdr.Width = 75;
			this.PercHdr.Text = "Perception";
			this.PercHdr.TextAlign = HorizontalAlignment.Right;
			this.PercHdr.Width = 75;
			this.Pages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Controls.Add(this.OverviewPage);
			this.Pages.Controls.Add(this.ParcelPage);
			this.Pages.Location = new Point(12, 12);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new System.Drawing.Size(576, 288);
			this.Pages.TabIndex = 2;
			this.DetailsPage.Controls.Add(this.HeroList);
			this.DetailsPage.Controls.Add(this.StatusBar);
			this.DetailsPage.Controls.Add(this.Toolbar);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new System.Windows.Forms.Padding(3);
			this.DetailsPage.Size = new System.Drawing.Size(568, 262);
			this.DetailsPage.TabIndex = 0;
			this.DetailsPage.Text = "Details";
			this.DetailsPage.UseVisualStyleBackColor = true;
			this.OverviewPage.Controls.Add(this.BreakdownPnl);
			this.OverviewPage.Location = new Point(4, 22);
			this.OverviewPage.Name = "OverviewPage";
			this.OverviewPage.Padding = new System.Windows.Forms.Padding(3);
			this.OverviewPage.Size = new System.Drawing.Size(568, 262);
			this.OverviewPage.TabIndex = 1;
			this.OverviewPage.Text = "Class Role Overview";
			this.OverviewPage.UseVisualStyleBackColor = true;
			this.ParcelPage.Controls.Add(this.ParcelList);
			this.ParcelPage.Location = new Point(4, 22);
			this.ParcelPage.Name = "ParcelPage";
			this.ParcelPage.Padding = new System.Windows.Forms.Padding(3);
			this.ParcelPage.Size = new System.Drawing.Size(568, 262);
			this.ParcelPage.TabIndex = 2;
			this.ParcelPage.Text = "Treasure Parcels";
			this.ParcelPage.UseVisualStyleBackColor = true;
			ListView.ColumnHeaderCollection columnHeaderCollections = this.ParcelList.Columns;
			ColumnHeader[] parcelHdr = new ColumnHeader[] { this.ParcelHdr, this.ParcelDetailsHdr };
			columnHeaderCollections.AddRange(parcelHdr);
			this.ParcelList.Dock = DockStyle.Fill;
			this.ParcelList.FullRowSelect = true;
			this.ParcelList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.ParcelList.HideSelection = false;
			this.ParcelList.Location = new Point(3, 3);
			this.ParcelList.MultiSelect = false;
			this.ParcelList.Name = "ParcelList";
			this.ParcelList.Size = new System.Drawing.Size(562, 256);
			this.ParcelList.TabIndex = 0;
			this.ParcelList.UseCompatibleStateImageBehavior = false;
			this.ParcelList.View = View.Details;
			this.ParcelList.DoubleClick += new EventHandler(this.ParcelList_DoubleClick);
			this.ParcelHdr.Text = "Treasure Parcel";
			this.ParcelHdr.Width = 185;
			this.ParcelDetailsHdr.Text = "Details";
			this.ParcelDetailsHdr.Width = 339;
			this.CloseBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CloseBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.CloseBtn.Location = new Point(513, 306);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new System.Drawing.Size(75, 23);
			this.CloseBtn.TabIndex = 3;
			this.CloseBtn.Text = "Close";
			this.CloseBtn.UseVisualStyleBackColor = true;
			this.UpdateBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.UpdateBtn.Location = new Point(12, 306);
			this.UpdateBtn.Name = "UpdateBtn";
			this.UpdateBtn.Size = new System.Drawing.Size(178, 23);
			this.UpdateBtn.TabIndex = 4;
			this.UpdateBtn.Text = "Update iPlay4e Characters";
			this.UpdateBtn.UseVisualStyleBackColor = true;
			this.UpdateBtn.Click += new EventHandler(this.UpdateBtn_Click);
			this.StatusBar.BackColor = Color.Transparent;
			this.StatusBar.Items.AddRange(new ToolStripItem[] { this.PartySizeLbl });
			this.StatusBar.Location = new Point(3, 237);
			this.StatusBar.Name = "StatusBar";
			this.StatusBar.Size = new System.Drawing.Size(562, 22);
			this.StatusBar.SizingGrip = false;
			this.StatusBar.TabIndex = 2;
			this.PartySizeLbl.IsLink = true;
			this.PartySizeLbl.LinkBehavior = LinkBehavior.HoverUnderline;
			this.PartySizeLbl.Name = "PartySizeLbl";
			this.PartySizeLbl.Size = new System.Drawing.Size(217, 17);
			this.PartySizeLbl.Text = "Your campaign is set up for a party of N";
			this.BreakdownPnl.Dock = DockStyle.Fill;
			this.BreakdownPnl.Heroes = null;
			this.BreakdownPnl.Location = new Point(3, 3);
			this.BreakdownPnl.Name = "BreakdownPnl";
			this.BreakdownPnl.Size = new System.Drawing.Size(562, 256);
			this.BreakdownPnl.TabIndex = 0;
			base.AcceptButton = this.CloseBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(600, 341);
			base.Controls.Add(this.UpdateBtn);
			base.Controls.Add(this.CloseBtn);
			base.Controls.Add(this.Pages);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "HeroListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Player Characters";
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.Pages.ResumeLayout(false);
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			this.OverviewPage.ResumeLayout(false);
			this.ParcelPage.ResumeLayout(false);
			this.StatusBar.ResumeLayout(false);
			this.StatusBar.PerformLayout();
			base.ResumeLayout(false);
		}

		private void ParcelList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedParcel != null)
			{
				ParcelForm parcelForm = new ParcelForm(this.SelectedParcel);
				if (parcelForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.SelectedParcel.Name = parcelForm.Parcel.Name;
					this.SelectedParcel.Details = parcelForm.Parcel.Details;
					this.SelectedParcel.HeroID = parcelForm.Parcel.HeroID;
					this.SelectedParcel.MagicItemID = parcelForm.Parcel.MagicItemID;
					this.SelectedParcel.ArtifactID = parcelForm.Parcel.ArtifactID;
					this.SelectedParcel.Value = parcelForm.Parcel.Value;
					this.update_parcels();
					Session.Modified = true;
				}
			}
		}

		private void PlayerViewBtn_Click(object sender, EventArgs e)
		{
			if (Session.PlayerView == null)
			{
				Session.PlayerView = new PlayerViewForm(this);
			}
			Session.PlayerView.ShowPCs();
		}

		private void RandomParty_Click(object sender, EventArgs e)
		{
			if (Session.Project.Heroes.Count != 0)
			{
				if (MessageBox.Show(string.Concat(string.Concat("This will clear the PC list.", Environment.NewLine), "Are you sure you want to do this?"), "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
				{
					return;
				}
				Session.Project.Heroes.Clear();
			}
			foreach (HeroData hero in HeroGroup.CreateGroup(Session.Project.Party.Size).Heroes)
			{
				if (hero == null)
				{
					continue;
				}
				Hero hero1 = hero.ConvertToHero();
				Session.Project.Heroes.Add(hero1);
			}
			Session.Modified = true;
			this.update_view();
		}

		private void RandomPC_Click(object sender, EventArgs e)
		{
			Hero hero = HeroData.Create().ConvertToHero();
			Session.Project.Heroes.Add(hero);
			Session.Modified = true;
			this.update_view();
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedHero != null)
			{
				if (MessageBox.Show("Are you sure you want to delete this PC?", "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
				{
					return;
				}
				((Session.Project.Heroes.Contains(this.SelectedHero) ? Session.Project.Heroes : Session.Project.InactiveHeroes)).Remove(this.SelectedHero);
				foreach (Parcel allTreasureParcel in Session.Project.AllTreasureParcels)
				{
					if (allTreasureParcel.HeroID != this.SelectedHero.ID)
					{
						continue;
					}
					allTreasureParcel.HeroID = Guid.Empty;
				}
				Session.Modified = true;
				this.update_view();
			}
		}

		private void StatBlockBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedHero != null)
			{
				(new HeroDetailsForm(this.SelectedHero)).ShowDialog();
			}
		}

		private void update_heroes()
		{
			this.HeroList.Items.Clear();
			foreach (Hero hero in Session.Project.Heroes)
			{
				this.add_to_list(hero, true);
			}
			foreach (Hero inactiveHero in Session.Project.InactiveHeroes)
			{
				this.add_to_list(inactiveHero, false);
			}
			if (Session.Project.Heroes.Count == 0)
			{
				ListViewItem grayText = this.HeroList.Items.Add("(no heroes)");
				grayText.ForeColor = SystemColors.GrayText;
				grayText.Group = this.HeroList.Groups[0];
			}
			this.StatusBar.Visible = Session.Project.Heroes.Count > Session.Project.Party.Size;
			this.PartySizeLbl.Text = string.Concat("Your project is set up for a party size of ", Session.Project.Party.Size, "; click here to change it");
			bool flag = false;
			foreach (Hero hero1 in Session.Project.Heroes)
			{
				if (hero1.Key == "")
				{
					continue;
				}
				flag = true;
			}
			this.UpdateBtn.Visible = flag;
		}

		private void update_parcels()
		{
			this.ParcelList.Groups.Clear();
			this.ParcelList.Items.Clear();
			this.ParcelList.ShowGroups = true;
			foreach (Hero hero in Session.Project.Heroes)
			{
				this.ParcelList.Groups.Add(hero.Name, hero.Name);
			}
			foreach (Parcel treasureParcel in Session.Project.TreasureParcels)
			{
				this.add_parcel(treasureParcel);
			}
			foreach (PlotPoint allPlotPoint in Session.Project.AllPlotPoints)
			{
				foreach (Parcel parcel in allPlotPoint.Parcels)
				{
					this.add_parcel(parcel);
				}
			}
			if (this.ParcelList.Items.Count == 0)
			{
				this.ParcelList.ShowGroups = false;
				ListViewItem grayText = this.ParcelList.Items.Add("(none assigned)");
				grayText.ForeColor = SystemColors.GrayText;
			}
		}

		private void update_view()
		{
			this.update_heroes();
			this.update_parcels();
		}

		private void UpdateBtn_Click(object sender, EventArgs e)
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			foreach (Hero hero in Session.Project.Heroes)
			{
				if (hero.Key == null || hero.Key == "")
				{
					continue;
				}
				AppImport.ImportIPlay4e(hero);
			}
			Session.Modified = true;
			this.update_view();
			System.Windows.Forms.Cursor.Current = Cursors.Default;
		}
	}
}