using Masterplan;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class HandoutForm : Form
	{
		private Button CloseBtn;

		private SplitContainer Splitter;

		private ListView ItemList;

		private ColumnHeader ItemHdr;

		private ToolStrip ItemToolbar;

		private WebBrowser Browser;

		private ToolStrip BrowserToolbar;

		private ToolStripButton UpBtn;

		private ToolStripButton DownBtn;

		private ToolStripButton ExportBtn;

		private Panel BrowserPanel;

		private SplitContainer ItemSplitter;

		private ListView SourceList;

		private ColumnHeader SourceHdr;

		private ToolStrip SourceToolbar;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton PlayerViewBtn;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton DMInfoBtn;

		private ToolStripButton AddBtn;

		private ToolStripButton AddAllBtn;

		private ToolStripButton RemoveBtn;

		private ToolStripButton ClearBtn;

		private List<object> fItems = new List<object>();

		private List<Type> fTypes = new List<Type>();

		private bool fShowDMInfo;

		public object SelectedItem
		{
			get
			{
				if (this.ItemList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.ItemList.SelectedItems[0].Tag;
			}
		}

		public object SelectedSource
		{
			get
			{
				if (this.SourceList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.SourceList.SelectedItems[0].Tag;
			}
		}

		public HandoutForm()
		{
			this.InitializeComponent();
			this.Browser.DocumentText = "";
			this.fTypes.Add(typeof(Background));
			this.fTypes.Add(typeof(EncyclopediaEntry));
			this.fTypes.Add(typeof(Race));
			this.fTypes.Add(typeof(Class));
			this.fTypes.Add(typeof(Theme));
			this.fTypes.Add(typeof(ParagonPath));
			this.fTypes.Add(typeof(EpicDestiny));
			this.fTypes.Add(typeof(PlayerBackground));
			this.fTypes.Add(typeof(Feat));
			this.fTypes.Add(typeof(Weapon));
			this.fTypes.Add(typeof(Artifact));
			this.fTypes.Add(typeof(Ritual));
			this.fTypes.Add(typeof(CreatureLore));
			this.fTypes.Add(typeof(Disease));
			this.fTypes.Add(typeof(Poison));
			this.update_source_list();
			this.update_item_list();
			this.update_handout();
			Application.Idle += new EventHandler(this.Application_Idle);
		}

		private void AddAllBtn_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in this.SourceList.Items)
			{
				object tag = item.Tag;
				if (!this.fItems.Contains(tag))
				{
					this.fItems.Add(tag);
				}
				else
				{
					return;
				}
			}
			this.update_source_list();
			this.update_item_list();
			this.update_handout();
		}

		public void AddBackgroundEntries()
		{
			foreach (ListViewItem item in this.SourceList.Items)
			{
				object tag = item.Tag;
				if (!(tag is Background))
				{
					continue;
				}
				this.fItems.Add(tag);
			}
			this.update_source_list();
			this.update_item_list();
			this.update_handout();
		}

		private void AddBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSource == null)
			{
				return;
			}
			if (this.fItems.Contains(this.SelectedSource))
			{
				return;
			}
			this.fItems.Add(this.SelectedSource);
			this.update_source_list();
			this.update_item_list();
			this.update_handout();
		}

		public void AddEncyclopediaEntries()
		{
			foreach (ListViewItem item in this.SourceList.Items)
			{
				object tag = item.Tag;
				if (!(tag is EncyclopediaEntry))
				{
					continue;
				}
				this.fItems.Add(tag);
			}
			this.update_source_list();
			this.update_item_list();
			this.update_handout();
		}

		public void AddRulesEntries()
		{
			foreach (ListViewItem item in this.SourceList.Items)
			{
				object tag = item.Tag;
				if (!(tag is IPlayerOption))
				{
					continue;
				}
				this.fItems.Add(tag);
			}
			this.update_source_list();
			this.update_item_list();
			this.update_handout();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.AddBtn.Enabled = this.SelectedSource != null;
			this.AddAllBtn.Enabled = this.SourceList.ShowGroups;
			this.RemoveBtn.Enabled = this.SelectedItem != null;
			this.ClearBtn.Enabled = this.fItems.Count != 0;
			this.UpBtn.Enabled = (this.SelectedItem == null ? false : this.fItems.IndexOf(this.SelectedItem) != 0);
			this.DownBtn.Enabled = (this.SelectedItem == null ? false : this.fItems.IndexOf(this.SelectedItem) != this.fItems.Count - 1);
			this.ExportBtn.Enabled = this.fItems.Count != 0;
			this.PlayerViewBtn.Enabled = this.fItems.Count != 0;
			bool flag = false;
			foreach (object fItem in this.fItems)
			{
				if (!(fItem is EncyclopediaEntry) || !((fItem as EncyclopediaEntry).DMInfo != ""))
				{
					continue;
				}
				flag = true;
				break;
			}
			this.DMInfoBtn.Enabled = flag;
			this.DMInfoBtn.Checked = this.fShowDMInfo;
		}

		private void DMInfoBtn_Click(object sender, EventArgs e)
		{
			this.fShowDMInfo = !this.fShowDMInfo;
			this.update_handout();
		}

		private void DownBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedItem == null)
			{
				return;
			}
			int selectedItem = this.fItems.IndexOf(this.SelectedItem);
			if (selectedItem == this.fItems.Count - 1)
			{
				return;
			}
			object item = this.fItems[selectedItem + 1];
			this.fItems[selectedItem + 1] = this.SelectedItem;
			this.fItems[selectedItem] = item;
			this.update_item_list();
			this.update_handout();
			this.ItemList.SelectedIndices.Add(selectedItem + 1);
		}

		private void ExportBtn_Click(object sender, EventArgs e)
		{
			if (this.fItems.Count == 0)
			{
				return;
			}
			SaveFileDialog saveFileDialog = new SaveFileDialog()
			{
				Filter = Program.HTMLFilter
			};
			if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				File.WriteAllText(saveFileDialog.FileName, this.Browser.DocumentText);
				Process.Start(saveFileDialog.FileName);
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(HandoutForm));
			this.CloseBtn = new Button();
			this.Splitter = new SplitContainer();
			this.ItemSplitter = new SplitContainer();
			this.SourceList = new ListView();
			this.SourceHdr = new ColumnHeader();
			this.SourceToolbar = new ToolStrip();
			this.ItemList = new ListView();
			this.ItemHdr = new ColumnHeader();
			this.ItemToolbar = new ToolStrip();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.UpBtn = new ToolStripButton();
			this.DownBtn = new ToolStripButton();
			this.BrowserPanel = new Panel();
			this.Browser = new WebBrowser();
			this.BrowserToolbar = new ToolStrip();
			this.ExportBtn = new ToolStripButton();
			this.PlayerViewBtn = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.DMInfoBtn = new ToolStripButton();
			this.AddAllBtn = new ToolStripButton();
			this.AddBtn = new ToolStripButton();
			this.RemoveBtn = new ToolStripButton();
			this.ClearBtn = new ToolStripButton();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.ItemSplitter.Panel1.SuspendLayout();
			this.ItemSplitter.Panel2.SuspendLayout();
			this.ItemSplitter.SuspendLayout();
			this.SourceToolbar.SuspendLayout();
			this.ItemToolbar.SuspendLayout();
			this.BrowserPanel.SuspendLayout();
			this.BrowserToolbar.SuspendLayout();
			base.SuspendLayout();
			this.CloseBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CloseBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.CloseBtn.Location = new Point(796, 389);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new System.Drawing.Size(75, 23);
			this.CloseBtn.TabIndex = 0;
			this.CloseBtn.Text = "Close";
			this.CloseBtn.UseVisualStyleBackColor = true;
			this.Splitter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Splitter.FixedPanel = FixedPanel.Panel1;
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.ItemSplitter);
			this.Splitter.Panel2.Controls.Add(this.BrowserPanel);
			this.Splitter.Panel2.Controls.Add(this.BrowserToolbar);
			this.Splitter.Size = new System.Drawing.Size(859, 371);
			this.Splitter.SplitterDistance = 494;
			this.Splitter.TabIndex = 1;
			this.ItemSplitter.Dock = DockStyle.Fill;
			this.ItemSplitter.Location = new Point(0, 0);
			this.ItemSplitter.Name = "ItemSplitter";
			this.ItemSplitter.Panel1.Controls.Add(this.SourceList);
			this.ItemSplitter.Panel1.Controls.Add(this.SourceToolbar);
			this.ItemSplitter.Panel2.Controls.Add(this.ItemList);
			this.ItemSplitter.Panel2.Controls.Add(this.ItemToolbar);
			this.ItemSplitter.Size = new System.Drawing.Size(494, 371);
			this.ItemSplitter.SplitterDistance = 242;
			this.ItemSplitter.TabIndex = 2;
			this.SourceList.AllowDrop = true;
			this.SourceList.Columns.AddRange(new ColumnHeader[] { this.SourceHdr });
			this.SourceList.Dock = DockStyle.Fill;
			this.SourceList.FullRowSelect = true;
			this.SourceList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.SourceList.HideSelection = false;
			this.SourceList.Location = new Point(0, 25);
			this.SourceList.MultiSelect = false;
			this.SourceList.Name = "SourceList";
			this.SourceList.Size = new System.Drawing.Size(242, 346);
			this.SourceList.TabIndex = 2;
			this.SourceList.UseCompatibleStateImageBehavior = false;
			this.SourceList.View = View.Details;
			this.SourceList.DoubleClick += new EventHandler(this.AddBtn_Click);
			this.SourceList.DragDrop += new DragEventHandler(this.SourceList_DragDrop);
			this.SourceList.ItemDrag += new ItemDragEventHandler(this.SourceList_ItemDrag);
			this.SourceList.DragOver += new DragEventHandler(this.SourceList_DragOver);
			this.SourceHdr.Text = "Item";
			this.SourceHdr.Width = 200;
			ToolStripItemCollection items = this.SourceToolbar.Items;
			ToolStripItem[] addBtn = new ToolStripItem[] { this.AddBtn, this.AddAllBtn };
			items.AddRange(addBtn);
			this.SourceToolbar.Location = new Point(0, 0);
			this.SourceToolbar.Name = "SourceToolbar";
			this.SourceToolbar.Size = new System.Drawing.Size(242, 25);
			this.SourceToolbar.TabIndex = 1;
			this.SourceToolbar.Text = "toolStrip2";
			this.ItemList.AllowDrop = true;
			this.ItemList.Columns.AddRange(new ColumnHeader[] { this.ItemHdr });
			this.ItemList.Dock = DockStyle.Fill;
			this.ItemList.FullRowSelect = true;
			this.ItemList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.ItemList.HideSelection = false;
			this.ItemList.Location = new Point(0, 25);
			this.ItemList.MultiSelect = false;
			this.ItemList.Name = "ItemList";
			this.ItemList.Size = new System.Drawing.Size(248, 346);
			this.ItemList.TabIndex = 1;
			this.ItemList.UseCompatibleStateImageBehavior = false;
			this.ItemList.View = View.Details;
			this.ItemList.DragDrop += new DragEventHandler(this.ItemList_DragDrop);
			this.ItemList.ItemDrag += new ItemDragEventHandler(this.ItemList_ItemDrag);
			this.ItemList.DragOver += new DragEventHandler(this.ItemList_DragOver);
			this.ItemHdr.Text = "Item";
			this.ItemHdr.Width = 200;
			ToolStripItemCollection toolStripItemCollections = this.ItemToolbar.Items;
			ToolStripItem[] removeBtn = new ToolStripItem[] { this.RemoveBtn, this.ClearBtn, this.toolStripSeparator1, this.UpBtn, this.DownBtn };
			toolStripItemCollections.AddRange(removeBtn);
			this.ItemToolbar.Location = new Point(0, 0);
			this.ItemToolbar.Name = "ItemToolbar";
			this.ItemToolbar.Size = new System.Drawing.Size(248, 25);
			this.ItemToolbar.TabIndex = 0;
			this.ItemToolbar.Text = "toolStrip2";
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.UpBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.UpBtn.Image = (Image)componentResourceManager.GetObject("UpBtn.Image");
			this.UpBtn.ImageTransparentColor = Color.Magenta;
			this.UpBtn.Name = "UpBtn";
			this.UpBtn.Size = new System.Drawing.Size(59, 22);
			this.UpBtn.Text = "Move Up";
			this.UpBtn.Click += new EventHandler(this.UpBtn_Click);
			this.DownBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DownBtn.Image = (Image)componentResourceManager.GetObject("DownBtn.Image");
			this.DownBtn.ImageTransparentColor = Color.Magenta;
			this.DownBtn.Name = "DownBtn";
			this.DownBtn.Size = new System.Drawing.Size(75, 22);
			this.DownBtn.Text = "Move Down";
			this.DownBtn.Click += new EventHandler(this.DownBtn_Click);
			this.BrowserPanel.BorderStyle = BorderStyle.FixedSingle;
			this.BrowserPanel.Controls.Add(this.Browser);
			this.BrowserPanel.Dock = DockStyle.Fill;
			this.BrowserPanel.Location = new Point(0, 25);
			this.BrowserPanel.Name = "BrowserPanel";
			this.BrowserPanel.Size = new System.Drawing.Size(361, 346);
			this.BrowserPanel.TabIndex = 2;
			this.Browser.Dock = DockStyle.Fill;
			this.Browser.IsWebBrowserContextMenuEnabled = false;
			this.Browser.Location = new Point(0, 0);
			this.Browser.MinimumSize = new System.Drawing.Size(20, 20);
			this.Browser.Name = "Browser";
			this.Browser.ScriptErrorsSuppressed = true;
			this.Browser.Size = new System.Drawing.Size(359, 344);
			this.Browser.TabIndex = 1;
			this.Browser.WebBrowserShortcutsEnabled = false;
			ToolStripItemCollection items1 = this.BrowserToolbar.Items;
			ToolStripItem[] exportBtn = new ToolStripItem[] { this.ExportBtn, this.PlayerViewBtn, this.toolStripSeparator2, this.DMInfoBtn };
			items1.AddRange(exportBtn);
			this.BrowserToolbar.Location = new Point(0, 0);
			this.BrowserToolbar.Name = "BrowserToolbar";
			this.BrowserToolbar.Size = new System.Drawing.Size(361, 25);
			this.BrowserToolbar.TabIndex = 0;
			this.BrowserToolbar.Text = "toolStrip1";
			this.ExportBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ExportBtn.Image = (Image)componentResourceManager.GetObject("ExportBtn.Image");
			this.ExportBtn.ImageTransparentColor = Color.Magenta;
			this.ExportBtn.Name = "ExportBtn";
			this.ExportBtn.Size = new System.Drawing.Size(44, 22);
			this.ExportBtn.Text = "Export";
			this.ExportBtn.Click += new EventHandler(this.ExportBtn_Click);
			this.PlayerViewBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PlayerViewBtn.Image = (Image)componentResourceManager.GetObject("PlayerViewBtn.Image");
			this.PlayerViewBtn.ImageTransparentColor = Color.Magenta;
			this.PlayerViewBtn.Name = "PlayerViewBtn";
			this.PlayerViewBtn.Size = new System.Drawing.Size(114, 22);
			this.PlayerViewBtn.Text = "Send to Player View";
			this.PlayerViewBtn.Click += new EventHandler(this.PlayerViewBtn_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			this.DMInfoBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DMInfoBtn.Image = (Image)componentResourceManager.GetObject("DMInfoBtn.Image");
			this.DMInfoBtn.ImageTransparentColor = Color.Magenta;
			this.DMInfoBtn.Name = "DMInfoBtn";
			this.DMInfoBtn.Size = new System.Drawing.Size(86, 22);
			this.DMInfoBtn.Text = "Show DM Info";
			this.DMInfoBtn.Click += new EventHandler(this.DMInfoBtn_Click);
			this.AddAllBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AddAllBtn.Image = (Image)componentResourceManager.GetObject("AddAllBtn.Image");
			this.AddAllBtn.ImageTransparentColor = Color.Magenta;
			this.AddAllBtn.Name = "AddAllBtn";
			this.AddAllBtn.Size = new System.Drawing.Size(50, 22);
			this.AddAllBtn.Text = "Add All";
			this.AddAllBtn.Click += new EventHandler(this.AddAllBtn_Click);
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
			this.ClearBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ClearBtn.Image = (Image)componentResourceManager.GetObject("ClearBtn.Image");
			this.ClearBtn.ImageTransparentColor = Color.Magenta;
			this.ClearBtn.Name = "ClearBtn";
			this.ClearBtn.Size = new System.Drawing.Size(38, 22);
			this.ClearBtn.Text = "Clear";
			this.ClearBtn.Click += new EventHandler(this.RemoveAll_Click);
			base.AcceptButton = this.CloseBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(883, 424);
			base.Controls.Add(this.Splitter);
			base.Controls.Add(this.CloseBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "HandoutForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Export Handout";
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.Panel2.PerformLayout();
			this.Splitter.ResumeLayout(false);
			this.ItemSplitter.Panel1.ResumeLayout(false);
			this.ItemSplitter.Panel1.PerformLayout();
			this.ItemSplitter.Panel2.ResumeLayout(false);
			this.ItemSplitter.Panel2.PerformLayout();
			this.ItemSplitter.ResumeLayout(false);
			this.SourceToolbar.ResumeLayout(false);
			this.SourceToolbar.PerformLayout();
			this.ItemToolbar.ResumeLayout(false);
			this.ItemToolbar.PerformLayout();
			this.BrowserPanel.ResumeLayout(false);
			this.BrowserToolbar.ResumeLayout(false);
			this.BrowserToolbar.PerformLayout();
			base.ResumeLayout(false);
		}

		private void ItemList_DragDrop(object sender, DragEventArgs e)
		{
			foreach (Type fType in this.fTypes)
			{
				object data = e.Data.GetData(fType);
				if (data == null || this.fItems.Contains(data))
				{
					continue;
				}
				this.fItems.Add(data);
				this.update_source_list();
				this.update_item_list();
				this.update_handout();
				return;
			}
		}

		private void ItemList_DragOver(object sender, DragEventArgs e)
		{
			foreach (Type fType in this.fTypes)
			{
				object data = e.Data.GetData(fType);
				if (data == null || this.fItems.Contains(data))
				{
					continue;
				}
				e.Effect = DragDropEffects.Move;
				return;
			}
		}

		private void ItemList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedItem == null)
			{
				return;
			}
			base.DoDragDrop(this.SelectedItem, DragDropEffects.Move);
		}

		private void PlayerViewBtn_Click(object sender, EventArgs e)
		{
			if (Session.PlayerView == null)
			{
				Session.PlayerView = new PlayerViewForm(this);
			}
			Session.PlayerView.ShowHandout(this.fItems, this.fShowDMInfo);
		}

		private void RemoveAll_Click(object sender, EventArgs e)
		{
			this.fItems.Clear();
			this.update_source_list();
			this.update_item_list();
			this.update_handout();
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedItem == null)
			{
				return;
			}
			this.fItems.Remove(this.SelectedItem);
			this.update_source_list();
			this.update_item_list();
			this.update_handout();
		}

		private void SourceList_DragDrop(object sender, DragEventArgs e)
		{
			foreach (Type fType in this.fTypes)
			{
				object data = e.Data.GetData(fType);
				if (data == null || !this.fItems.Contains(data))
				{
					continue;
				}
				this.fItems.Remove(data);
				this.update_source_list();
				this.update_item_list();
				this.update_handout();
				return;
			}
		}

		private void SourceList_DragOver(object sender, DragEventArgs e)
		{
			foreach (Type fType in this.fTypes)
			{
				object data = e.Data.GetData(fType);
				if (data == null || !this.fItems.Contains(data))
				{
					continue;
				}
				e.Effect = DragDropEffects.Move;
				return;
			}
		}

		private void SourceList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedSource == null)
			{
				return;
			}
			base.DoDragDrop(this.SelectedSource, DragDropEffects.Move);
		}

		private void UpBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedItem == null)
			{
				return;
			}
			int selectedItem = this.fItems.IndexOf(this.SelectedItem);
			if (selectedItem == 0)
			{
				return;
			}
			object item = this.fItems[selectedItem - 1];
			this.fItems[selectedItem - 1] = this.SelectedItem;
			this.fItems[selectedItem] = item;
			this.update_item_list();
			this.update_handout();
			this.ItemList.SelectedIndices.Add(selectedItem - 1);
		}

		private void update_handout()
		{
			this.Browser.Document.OpenNew(true);
			this.Browser.Document.Write(HTML.Handout(this.fItems, DisplaySize.Small, this.fShowDMInfo));
		}

		private void update_item_list()
		{
			this.ItemList.Items.Clear();
			foreach (object fItem in this.fItems)
			{
				ListViewItem listViewItem = this.ItemList.Items.Add(fItem.ToString());
				listViewItem.Tag = fItem;
			}
			if (this.ItemList.Items.Count == 0)
			{
				ListViewItem grayText = this.ItemList.Items.Add("(no items)");
				grayText.ForeColor = SystemColors.GrayText;
			}
		}

		private void update_source_list()
		{
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			foreach (EncyclopediaEntry entry in Session.Project.Encyclopedia.Entries)
			{
				if (entry.Category == null || !(entry.Category != ""))
				{
					continue;
				}
				binarySearchTree.Add(entry.Category);
			}
			List<string> sortedList = binarySearchTree.SortedList;
			sortedList.Insert(0, "Background Items");
			sortedList.Add("Miscellaneous");
			sortedList.Add("Player Options");
			this.SourceList.Groups.Clear();
			foreach (string str in sortedList)
			{
				this.SourceList.Groups.Add(str, str);
			}
			this.SourceList.ShowGroups = true;
			this.SourceList.Items.Clear();
			foreach (Background background in Session.Project.Backgrounds)
			{
				if (this.fItems.Contains(background) || background.Details == "")
				{
					continue;
				}
				ListViewItem item = this.SourceList.Items.Add(background.Title);
				item.Tag = background;
				item.Group = this.SourceList.Groups["Background Items"];
			}
			foreach (EncyclopediaEntry encyclopediaEntry in Session.Project.Encyclopedia.Entries)
			{
				if (this.fItems.Contains(encyclopediaEntry) || encyclopediaEntry.Details == "")
				{
					continue;
				}
				ListViewItem listViewItem = this.SourceList.Items.Add(encyclopediaEntry.Name);
				listViewItem.Tag = encyclopediaEntry;
				if (encyclopediaEntry.Category == null || !(encyclopediaEntry.Category != ""))
				{
					listViewItem.Group = this.SourceList.Groups["Miscellaneous"];
				}
				else
				{
					listViewItem.Group = this.SourceList.Groups[encyclopediaEntry.Category];
				}
			}
			foreach (IPlayerOption playerOption in Session.Project.PlayerOptions)
			{
				if (this.fItems.Contains(playerOption))
				{
					continue;
				}
				ListViewItem item1 = this.SourceList.Items.Add(playerOption.Name);
				item1.Tag = playerOption;
				item1.Group = this.SourceList.Groups["Player Options"];
			}
			if (this.SourceList.Items.Count == 0)
			{
				this.SourceList.ShowGroups = false;
				ListViewItem grayText = this.SourceList.Items.Add("(no items)");
				grayText.ForeColor = SystemColors.GrayText;
			}
		}
	}
}