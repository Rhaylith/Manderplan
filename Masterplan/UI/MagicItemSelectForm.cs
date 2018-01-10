using Masterplan;
using Masterplan.Controls;
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
	internal class MagicItemSelectForm : Form
	{
		private Button OKBtn;

		private Button CancelBtn;

		private SplitContainer Splitter;

		private ListView ItemList;

		private ColumnHeader NameHdr;

		private ColumnHeader InfoHdr;

		private Panel BrowserPanel;

		private WebBrowser Browser;

		private Masterplan.Controls.LevelRangePanel LevelRangePanel;

		public Masterplan.Data.MagicItem MagicItem
		{
			get
			{
				if (this.ItemList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.ItemList.SelectedItems[0].Tag as Masterplan.Data.MagicItem;
			}
		}

		public MagicItemSelectForm(int level)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			if (level > 0)
			{
				this.LevelRangePanel.SetLevelRange(level, level);
			}
			this.Browser.DocumentText = "";
			this.ItemList_SelectedIndexChanged(null, null);
			this.update_list();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = this.MagicItem != null;
		}

		private void InitializeComponent()
		{
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.Splitter = new SplitContainer();
			this.ItemList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.InfoHdr = new ColumnHeader();
			this.LevelRangePanel = new Masterplan.Controls.LevelRangePanel();
			this.BrowserPanel = new Panel();
			this.Browser = new WebBrowser();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.BrowserPanel.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(549, 354);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 1;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(630, 354);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 2;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.Splitter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.ItemList);
			this.Splitter.Panel1.Controls.Add(this.LevelRangePanel);
			this.Splitter.Panel2.Controls.Add(this.BrowserPanel);
			this.Splitter.Size = new System.Drawing.Size(693, 336);
			this.Splitter.SplitterDistance = 330;
			this.Splitter.TabIndex = 5;
			ListView.ColumnHeaderCollection columns = this.ItemList.Columns;
			ColumnHeader[] nameHdr = new ColumnHeader[] { this.NameHdr, this.InfoHdr };
			columns.AddRange(nameHdr);
			this.ItemList.Dock = DockStyle.Fill;
			this.ItemList.FullRowSelect = true;
			this.ItemList.HideSelection = false;
			this.ItemList.Location = new Point(0, 80);
			this.ItemList.MultiSelect = false;
			this.ItemList.Name = "ItemList";
			this.ItemList.Size = new System.Drawing.Size(330, 256);
			this.ItemList.Sorting = SortOrder.Ascending;
			this.ItemList.TabIndex = 1;
			this.ItemList.UseCompatibleStateImageBehavior = false;
			this.ItemList.View = View.Details;
			this.ItemList.SelectedIndexChanged += new EventHandler(this.ItemList_SelectedIndexChanged);
			this.ItemList.DoubleClick += new EventHandler(this.ItemList_DoubleClick);
			this.NameHdr.Text = "Magic Item";
			this.NameHdr.Width = 150;
			this.InfoHdr.Text = "Info";
			this.InfoHdr.Width = 150;
			this.LevelRangePanel.Dock = DockStyle.Top;
			this.LevelRangePanel.Location = new Point(0, 0);
			this.LevelRangePanel.Name = "LevelRangePanel";
			this.LevelRangePanel.Size = new System.Drawing.Size(330, 80);
			this.LevelRangePanel.TabIndex = 2;
			this.LevelRangePanel.RangeChanged += new EventHandler(this.LevelRangePanel_RangeChanged);
			this.BrowserPanel.BorderStyle = BorderStyle.FixedSingle;
			this.BrowserPanel.Controls.Add(this.Browser);
			this.BrowserPanel.Dock = DockStyle.Fill;
			this.BrowserPanel.Location = new Point(0, 0);
			this.BrowserPanel.Name = "BrowserPanel";
			this.BrowserPanel.Size = new System.Drawing.Size(359, 336);
			this.BrowserPanel.TabIndex = 0;
			this.Browser.AllowWebBrowserDrop = false;
			this.Browser.Dock = DockStyle.Fill;
			this.Browser.IsWebBrowserContextMenuEnabled = false;
			this.Browser.Location = new Point(0, 0);
			this.Browser.MinimumSize = new System.Drawing.Size(20, 20);
			this.Browser.Name = "Browser";
			this.Browser.ScriptErrorsSuppressed = true;
			this.Browser.Size = new System.Drawing.Size(357, 334);
			this.Browser.TabIndex = 0;
			this.Browser.WebBrowserShortcutsEnabled = false;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(717, 389);
			base.Controls.Add(this.Splitter);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MagicItemSelectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Select a Magic Item";
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			this.BrowserPanel.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void ItemList_DoubleClick(object sender, EventArgs e)
		{
			if (this.MagicItem != null)
			{
				base.DialogResult = System.Windows.Forms.DialogResult.OK;
				base.Close();
			}
		}

		private void ItemList_SelectedIndexChanged(object sender, EventArgs e)
		{
			string str = HTML.MagicItem(this.MagicItem, DisplaySize.Small, false, true);
			this.Browser.Document.OpenNew(true);
			this.Browser.Document.Write(str);
		}

		private void LevelRangePanel_RangeChanged(object sender, EventArgs e)
		{
			this.update_list();
		}

		private bool match(Masterplan.Data.MagicItem item, string query)
		{
			string[] strArrays = query.ToLower().Split(new char[0]);
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				if (!this.match_token(item, strArrays[i]))
				{
					return false;
				}
			}
			return true;
		}

		private bool match_token(Masterplan.Data.MagicItem item, string token)
		{
			if (item.Name.ToLower().Contains(token))
			{
				return true;
			}
			return false;
		}

		private void update_list()
		{
			List<Masterplan.Data.MagicItem> magicItems = new List<Masterplan.Data.MagicItem>();
			foreach (Masterplan.Data.MagicItem magicItem in Session.MagicItems)
			{
				if (magicItem.Level < this.LevelRangePanel.MinimumLevel || magicItem.Level > this.LevelRangePanel.MaximumLevel || !this.match(magicItem, this.LevelRangePanel.NameQuery))
				{
					continue;
				}
				magicItems.Add(magicItem);
			}
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			foreach (Masterplan.Data.MagicItem magicItem1 in magicItems)
			{
				if (magicItem1.Type == "")
				{
					continue;
				}
				binarySearchTree.Add(magicItem1.Type);
			}
			List<string> sortedList = binarySearchTree.SortedList;
			sortedList.Add("Miscellaneous Items");
			foreach (string str in sortedList)
			{
				this.ItemList.Groups.Add(str, str);
			}
			List<ListViewItem> listViewItems = new List<ListViewItem>();
			foreach (Masterplan.Data.MagicItem magicItem2 in magicItems)
			{
				ListViewItem listViewItem = new ListViewItem(magicItem2.Name);
				listViewItem.SubItems.Add(magicItem2.Info);
				listViewItem.Tag = magicItem2;
				if (magicItem2.Type == "")
				{
					listViewItem.Group = this.ItemList.Groups["Miscellaneous Items"];
				}
				else
				{
					listViewItem.Group = this.ItemList.Groups[magicItem2.Type];
				}
				listViewItems.Add(listViewItem);
			}
			this.ItemList.BeginUpdate();
			this.ItemList.Items.Clear();
			this.ItemList.Items.AddRange(listViewItems.ToArray());
			this.ItemList.EndUpdate();
		}
	}
}