using Masterplan;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class CompendiumForm : Form
	{
		private IContainer components;

		private Button CloseBtn;

		private SplitContainer Splitter;

		private ListView BookList;

		private ColumnHeader BookHdr;

		private ListView ItemList;

		private ColumnHeader ItemHdr;

		private ColumnHeader InfoHdr;

		private List<CompendiumHelper.SourceBook> fBooks = new List<CompendiumHelper.SourceBook>();

		public CompendiumHelper.SourceBook SelectedBook
		{
			get
			{
				if (this.BookList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.BookList.SelectedItems[0].Tag as CompendiumHelper.SourceBook;
			}
		}

		public CompendiumHelper.CompendiumItem SelectedItem
		{
			get
			{
				if (this.ItemList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.ItemList.SelectedItems[0].Tag as CompendiumHelper.CompendiumItem;
			}
		}

		public CompendiumForm()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.update_books();
			this.update_items();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.ItemList.Enabled = this.SelectedBook != null;
		}

		private void BookList_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.update_items();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private Dictionary<string, CompendiumHelper.SourceBook> get_data()
		{
			List<CompendiumHelper.CompendiumItem> creatures = CompendiumHelper.GetCreatures();
			List<CompendiumHelper.CompendiumItem> traps = CompendiumHelper.GetTraps();
			List<CompendiumHelper.CompendiumItem> magicItems = CompendiumHelper.GetMagicItems();
			List<CompendiumHelper.CompendiumItem> compendiumItems = new List<CompendiumHelper.CompendiumItem>();
			if (creatures != null)
			{
				compendiumItems.AddRange(creatures);
			}
			if (traps != null)
			{
				compendiumItems.AddRange(traps);
			}
			if (magicItems != null)
			{
				compendiumItems.AddRange(magicItems);
			}
			Dictionary<string, CompendiumHelper.SourceBook> strs = new Dictionary<string, CompendiumHelper.SourceBook>();
			foreach (CompendiumHelper.CompendiumItem compendiumItem in compendiumItems)
			{
				if (!strs.ContainsKey(compendiumItem.SourceBook))
				{
					CompendiumHelper.SourceBook sourceBook = new CompendiumHelper.SourceBook()
					{
						Name = compendiumItem.SourceBook
					};
					strs[compendiumItem.SourceBook] = sourceBook;
				}
				CompendiumHelper.SourceBook item = strs[compendiumItem.SourceBook];
				if (creatures.Contains(compendiumItem))
				{
					item.Creatures.Add(compendiumItem);
				}
				if (traps.Contains(compendiumItem))
				{
					item.Traps.Add(compendiumItem);
				}
				if (!magicItems.Contains(compendiumItem))
				{
					continue;
				}
				item.MagicItems.Add(compendiumItem);
			}
			return strs;
		}

		private void InitializeComponent()
		{
			ListViewGroup listViewGroup = new ListViewGroup("Books", HorizontalAlignment.Left);
			ListViewGroup listViewGroup1 = new ListViewGroup("Dragon Magazine", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Dungeon Magazine", HorizontalAlignment.Left);
			ListViewGroup listViewGroup3 = new ListViewGroup("RPGA Modules", HorizontalAlignment.Left);
			ListViewGroup listViewGroup4 = new ListViewGroup("Creatures", HorizontalAlignment.Left);
			ListViewGroup listViewGroup5 = new ListViewGroup("Traps / Hazards", HorizontalAlignment.Left);
			ListViewGroup listViewGroup6 = new ListViewGroup("Magic Items", HorizontalAlignment.Left);
			this.CloseBtn = new Button();
			this.Splitter = new SplitContainer();
			this.BookList = new ListView();
			this.BookHdr = new ColumnHeader();
			this.ItemList = new ListView();
			this.ItemHdr = new ColumnHeader();
			this.InfoHdr = new ColumnHeader();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			base.SuspendLayout();
			this.CloseBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CloseBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.CloseBtn.Location = new Point(565, 330);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new System.Drawing.Size(75, 23);
			this.CloseBtn.TabIndex = 2;
			this.CloseBtn.Text = "Close";
			this.CloseBtn.UseVisualStyleBackColor = true;
			this.Splitter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.BookList);
			this.Splitter.Panel2.Controls.Add(this.ItemList);
			this.Splitter.Size = new System.Drawing.Size(628, 312);
			this.Splitter.SplitterDistance = 228;
			this.Splitter.TabIndex = 3;
			this.BookList.Columns.AddRange(new ColumnHeader[] { this.BookHdr });
			this.BookList.Dock = DockStyle.Fill;
			this.BookList.FullRowSelect = true;
			listViewGroup.Header = "Books";
			listViewGroup.Name = "listViewGroup1";
			listViewGroup1.Header = "Dragon Magazine";
			listViewGroup1.Name = "listViewGroup2";
			listViewGroup2.Header = "Dungeon Magazine";
			listViewGroup2.Name = "listViewGroup3";
			listViewGroup3.Header = "RPGA Modules";
			listViewGroup3.Name = "listViewGroup4";
			ListViewGroupCollection groups = this.BookList.Groups;
			ListViewGroup[] listViewGroupArray = new ListViewGroup[] { listViewGroup, listViewGroup1, listViewGroup2, listViewGroup3 };
			groups.AddRange(listViewGroupArray);
			this.BookList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.BookList.HideSelection = false;
			this.BookList.Location = new Point(0, 0);
			this.BookList.MultiSelect = false;
			this.BookList.Name = "BookList";
			this.BookList.Size = new System.Drawing.Size(228, 312);
			this.BookList.Sorting = SortOrder.Ascending;
			this.BookList.TabIndex = 3;
			this.BookList.UseCompatibleStateImageBehavior = false;
			this.BookList.View = View.Details;
			this.BookList.SelectedIndexChanged += new EventHandler(this.BookList_SelectedIndexChanged);
			this.BookHdr.Text = "Source Book";
			this.BookHdr.Width = 200;
			ListView.ColumnHeaderCollection columns = this.ItemList.Columns;
			ColumnHeader[] itemHdr = new ColumnHeader[] { this.ItemHdr, this.InfoHdr };
			columns.AddRange(itemHdr);
			this.ItemList.Dock = DockStyle.Fill;
			this.ItemList.FullRowSelect = true;
			listViewGroup4.Header = "Creatures";
			listViewGroup4.Name = "listViewGroup1";
			listViewGroup5.Header = "Traps / Hazards";
			listViewGroup5.Name = "listViewGroup2";
			listViewGroup6.Header = "Magic Items";
			listViewGroup6.Name = "listViewGroup3";
			this.ItemList.Groups.AddRange(new ListViewGroup[] { listViewGroup4, listViewGroup5, listViewGroup6 });
			this.ItemList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.ItemList.HideSelection = false;
			this.ItemList.Location = new Point(0, 0);
			this.ItemList.MultiSelect = false;
			this.ItemList.Name = "ItemList";
			this.ItemList.Size = new System.Drawing.Size(396, 312);
			this.ItemList.Sorting = SortOrder.Ascending;
			this.ItemList.TabIndex = 1;
			this.ItemList.UseCompatibleStateImageBehavior = false;
			this.ItemList.View = View.Details;
			this.ItemList.DoubleClick += new EventHandler(this.ItemList_DoubleClick);
			this.ItemHdr.Text = "Item";
			this.ItemHdr.Width = 208;
			this.InfoHdr.Text = "Details";
			this.InfoHdr.Width = 158;
			base.AcceptButton = this.CloseBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(652, 365);
			base.Controls.Add(this.Splitter);
			base.Controls.Add(this.CloseBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SourceBookListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Dungeons & Dragons Compendium";
			base.Shown += new EventHandler(this.SourceBookListForm_Shown);
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void ItemList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedItem != null)
			{
				CompendiumItemForm compendiumItemForm = new CompendiumItemForm(this.SelectedItem);
				if (compendiumItemForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Library library = Session.FindLibrary(this.SelectedItem.SourceBook);
					if (library == null)
					{
						library = new Library()
						{
							Name = this.SelectedItem.SourceBook
						};
						Session.Libraries.Add(library);
					}
					switch (this.SelectedItem.Type)
					{
						case CompendiumHelper.ItemType.Creature:
						{
							Creature result = compendiumItemForm.Result as Creature;
							Creature creature = library.FindCreature(result.Name, result.Level);
							if (creature != null)
							{
								result.ID = creature.ID;
								result.Category = creature.Category;
								library.Creatures.Remove(creature);
							}
							library.Creatures.Add(result);
							break;
						}
						case CompendiumHelper.ItemType.Trap:
						{
							Trap d = compendiumItemForm.Result as Trap;
							Trap trap = library.FindTrap(d.Name, d.Level, d.Role.ToString());
							if (trap != null)
							{
								d.ID = trap.ID;
								library.Traps.Remove(trap);
							}
							library.Traps.Add(d);
							break;
						}
						case CompendiumHelper.ItemType.MagicItem:
						{
							MagicItem magicItem = compendiumItemForm.Result as MagicItem;
							MagicItem magicItem1 = library.FindMagicItem(magicItem.Name, magicItem.Level);
							if (magicItem1 != null)
							{
								magicItem.ID = magicItem1.ID;
								library.MagicItems.Remove(magicItem1);
							}
							library.MagicItems.Add(magicItem);
							break;
						}
					}
					string libraryFilename = Session.GetLibraryFilename(library);
					Serialisation<Library>.Save(libraryFilename, library, SerialisationMode.Binary);
				}
			}
		}

		private void SourceBookListForm_Shown(object sender, EventArgs e)
		{
			Application.DoEvents();
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			Dictionary<string, CompendiumHelper.SourceBook> _data = this.get_data();
			this.fBooks.Clear();
			this.fBooks.AddRange(_data.Values);
			this.update_books();
			this.update_items();
			System.Windows.Forms.Cursor.Current = Cursors.Default;
		}

		private void update_books()
		{
			this.BookList.ShowGroups = true;
			List<ListViewItem> listViewItems = new List<ListViewItem>();
			foreach (CompendiumHelper.SourceBook fBook in this.fBooks)
			{
				ListViewItem listViewItem = new ListViewItem(fBook.Name)
				{
					Tag = fBook,
					Group = this.BookList.Groups[0]
				};
				if (fBook.Name.ToLower().StartsWith("dragon magazine"))
				{
					listViewItem.Group = this.BookList.Groups[1];
				}
				if (fBook.Name.ToLower().StartsWith("dungeon magazine"))
				{
					listViewItem.Group = this.BookList.Groups[2];
				}
				if (fBook.Name.ToLower().StartsWith("rpga"))
				{
					listViewItem.Group = this.BookList.Groups[3];
				}
				listViewItems.Add(listViewItem);
			}
			this.BookList.Items.Clear();
			this.BookList.Items.AddRange(listViewItems.ToArray());
			if (this.fBooks.Count == 0)
			{
				this.BookList.ShowGroups = false;
				ListViewItem grayText = this.BookList.Items.Add("(loading)");
				grayText.ForeColor = SystemColors.GrayText;
			}
		}

		private void update_items()
		{
			if (this.SelectedBook == null)
			{
				this.ItemList.Items.Clear();
				return;
			}
			this.ItemList.BeginUpdate();
			List<ListViewItem> listViewItems = new List<ListViewItem>();
			foreach (CompendiumHelper.CompendiumItem creature in this.SelectedBook.Creatures)
			{
				ListViewItem listViewItem = new ListViewItem(creature.Name);
				listViewItem.SubItems.Add(creature.Info);
				listViewItem.Tag = creature;
				listViewItem.Group = this.ItemList.Groups[0];
				listViewItems.Add(listViewItem);
			}
			if (this.SelectedBook.Creatures.Count == 0)
			{
				ListViewItem listViewItem1 = new ListViewItem("(none)")
				{
					ForeColor = SystemColors.GrayText,
					Group = this.ItemList.Groups[0]
				};
				listViewItems.Add(listViewItem1);
			}
			foreach (CompendiumHelper.CompendiumItem trap in this.SelectedBook.Traps)
			{
				ListViewItem item = new ListViewItem(trap.Name);
				item.SubItems.Add(trap.Info);
				item.Tag = trap;
				item.Group = this.ItemList.Groups[1];
				listViewItems.Add(item);
			}
			if (this.SelectedBook.Traps.Count == 0)
			{
				ListViewItem listViewItem2 = new ListViewItem("(none)")
				{
					ForeColor = SystemColors.GrayText,
					Group = this.ItemList.Groups[1]
				};
				listViewItems.Add(listViewItem2);
			}
			foreach (CompendiumHelper.CompendiumItem magicItem in this.SelectedBook.MagicItems)
			{
				ListViewItem item1 = new ListViewItem(magicItem.Name);
				item1.SubItems.Add(magicItem.Info);
				item1.Tag = magicItem;
				item1.Group = this.ItemList.Groups[2];
				listViewItems.Add(item1);
			}
			if (this.SelectedBook.MagicItems.Count == 0)
			{
				ListViewItem listViewItem3 = new ListViewItem("(none)")
				{
					ForeColor = SystemColors.GrayText,
					Group = this.ItemList.Groups[2]
				};
				listViewItems.Add(listViewItem3);
			}
			this.ItemList.Items.Clear();
			this.ItemList.Items.AddRange(listViewItems.ToArray());
			this.ItemList.EndUpdate();
		}
	}
}