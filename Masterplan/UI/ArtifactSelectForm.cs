using Masterplan;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class ArtifactSelectForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private SplitContainer Splitter;

		private ListView ItemList;

		private ColumnHeader NameHdr;

		private ColumnHeader InfoHdr;

		private Panel BrowserPanel;

		private WebBrowser Browser;

		private Panel NamePanel;

		private TextBox NameBox;

		private Label NameLbl;

		public Masterplan.Data.Artifact Artifact
		{
			get
			{
				if (this.ItemList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.ItemList.SelectedItems[0].Tag as Masterplan.Data.Artifact;
			}
		}

		public ArtifactSelectForm()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.Browser.DocumentText = "";
			this.ItemList_SelectedIndexChanged(null, null);
			this.update_list();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = this.Artifact != null;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.Splitter = new SplitContainer();
			this.ItemList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.InfoHdr = new ColumnHeader();
			this.BrowserPanel = new Panel();
			this.Browser = new WebBrowser();
			this.NamePanel = new Panel();
			this.NameBox = new TextBox();
			this.NameLbl = new Label();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.BrowserPanel.SuspendLayout();
			this.NamePanel.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(549, 354);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 0;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(630, 354);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 1;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.Splitter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.ItemList);
			this.Splitter.Panel1.Controls.Add(this.NamePanel);
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
			this.ItemList.Location = new Point(0, 27);
			this.ItemList.MultiSelect = false;
			this.ItemList.Name = "ItemList";
			this.ItemList.Size = new System.Drawing.Size(330, 309);
			this.ItemList.Sorting = SortOrder.Ascending;
			this.ItemList.TabIndex = 1;
			this.ItemList.UseCompatibleStateImageBehavior = false;
			this.ItemList.View = View.Details;
			this.ItemList.SelectedIndexChanged += new EventHandler(this.ItemList_SelectedIndexChanged);
			this.ItemList.DoubleClick += new EventHandler(this.ItemList_DoubleClick);
			this.NameHdr.Text = "Artifact";
			this.NameHdr.Width = 150;
			this.InfoHdr.Text = "Info";
			this.InfoHdr.Width = 150;
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
			this.NamePanel.Controls.Add(this.NameBox);
			this.NamePanel.Controls.Add(this.NameLbl);
			this.NamePanel.Dock = DockStyle.Top;
			this.NamePanel.Location = new Point(0, 0);
			this.NamePanel.Name = "NamePanel";
			this.NamePanel.Size = new System.Drawing.Size(330, 27);
			this.NamePanel.TabIndex = 0;
			this.NameBox.Location = new Point(47, 3);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(280, 20);
			this.NameBox.TabIndex = 1;
			this.NameBox.TextChanged += new EventHandler(this.NameBox_TextChanged);
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(3, 6);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new System.Drawing.Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
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
			base.Name = "ArtifactSelectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Select an Artifact";
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			this.BrowserPanel.ResumeLayout(false);
			this.NamePanel.ResumeLayout(false);
			this.NamePanel.PerformLayout();
			base.ResumeLayout(false);
		}

		private void ItemList_DoubleClick(object sender, EventArgs e)
		{
			if (this.Artifact != null)
			{
				base.DialogResult = System.Windows.Forms.DialogResult.OK;
				base.Close();
			}
		}

		private void ItemList_SelectedIndexChanged(object sender, EventArgs e)
		{
			string str = HTML.Artifact(this.Artifact, DisplaySize.Small, false, true);
			this.Browser.Document.OpenNew(true);
			this.Browser.Document.Write(str);
		}

		private bool match(Masterplan.Data.Artifact item, string query)
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

		private bool match_token(Masterplan.Data.Artifact item, string token)
		{
			if (item.Name.ToLower().Contains(token))
			{
				return true;
			}
			return false;
		}

		private void NameBox_TextChanged(object sender, EventArgs e)
		{
			this.update_list();
		}

		private void update_list()
		{
			List<Masterplan.Data.Artifact> artifacts = new List<Masterplan.Data.Artifact>();
			foreach (Masterplan.Data.Artifact artifact in Session.Artifacts)
			{
				if (!this.match(artifact, this.NameBox.Text))
				{
					continue;
				}
				artifacts.Add(artifact);
			}
			ListViewGroup listViewGroup = this.ItemList.Groups.Add("Heroic Tier", "Heroic Tier");
			ListViewGroup listViewGroup1 = this.ItemList.Groups.Add("Paragon Tier", "Paragon Tier");
			ListViewGroup listViewGroup2 = this.ItemList.Groups.Add("Epic Tier", "Epic Tier");
			List<ListViewItem> listViewItems = new List<ListViewItem>();
			foreach (Masterplan.Data.Artifact artifact1 in artifacts)
			{
				ListViewItem listViewItem = new ListViewItem(artifact1.Name);
				listViewItem.SubItems.Add(string.Concat(artifact1.Tier, " Tier"));
				listViewItem.Tag = artifact1;
				switch (artifact1.Tier)
				{
					case Tier.Heroic:
					{
						listViewItem.Group = listViewGroup;
						break;
					}
					case Tier.Paragon:
					{
						listViewItem.Group = listViewGroup1;
						break;
					}
					case Tier.Epic:
					{
						listViewItem.Group = listViewGroup2;
						break;
					}
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