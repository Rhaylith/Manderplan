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
	internal class CreatureMultipleSelectForm : Form
	{
		private Button OKBtn;

		private ListView CreatureList;

		private Button CancelBtn;

		private ColumnHeader NameHdr;

		private ColumnHeader InfoHdr;

		private SplitContainer Splitter;

		private Panel BrowserPanel;

		private WebBrowser Browser;

		private Panel NamePanel;

		private TextBox NameBox;

		private Label NameLbl;

		private Label DragLbl;

		private List<ICreature> fCreatures = new List<ICreature>();

		public ICreature SelectedCreature
		{
			get
			{
				if (this.CreatureList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.CreatureList.SelectedItems[0].Tag as ICreature;
			}
		}

		public List<ICreature> SelectedCreatures
		{
			get
			{
				return this.fCreatures;
			}
		}

		public CreatureMultipleSelectForm()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.update_list();
			this.Browser.DocumentText = "";
			this.update_stats();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = this.SelectedCreatures.Count >= 2;
		}

		private void Browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.Scheme == "remove")
			{
				Guid guid = new Guid(e.Url.LocalPath);
				ICreature creature = this.find_creature(guid);
				if (creature != null)
				{
					e.Cancel = true;
					this.fCreatures.Remove(creature);
					this.update_list();
					this.update_stats();
				}
			}
		}

		private void CreatureList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedCreature != null)
			{
				this.DragLbl.BackColor = SystemColors.Highlight;
				this.DragLbl.ForeColor = SystemColors.HighlightText;
				if (base.DoDragDrop(this.SelectedCreature, DragDropEffects.Move) == DragDropEffects.Move)
				{
					this.fCreatures.Add(this.SelectedCreature);
					this.update_list();
					this.update_stats();
				}
				this.DragLbl.BackColor = SystemColors.Control;
				this.DragLbl.ForeColor = SystemColors.ControlText;
			}
		}

		private void CreatureList_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void DragLbl_DragDrop(object sender, DragEventArgs e)
		{
			if (this.has_creature(e.Data))
			{
				e.Effect = DragDropEffects.Move;
			}
		}

		private void DragLbl_DragOver(object sender, DragEventArgs e)
		{
			if (this.has_creature(e.Data))
			{
				e.Effect = DragDropEffects.Move;
			}
		}

		private ICreature find_creature(Guid id)
		{
			ICreature creature;
			List<ICreature>.Enumerator enumerator = this.fCreatures.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					ICreature current = enumerator.Current;
					if (current.ID != id)
					{
						continue;
					}
					creature = current;
					return creature;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		private bool has_creature(IDataObject data)
		{
			if (data.GetData(typeof(Creature)) is Creature)
			{
				return true;
			}
			if (data.GetData(typeof(CustomCreature)) is CustomCreature)
			{
				return true;
			}
			if (data.GetData(typeof(NPC)) is NPC)
			{
				return true;
			}
			return false;
		}

		private void InitializeComponent()
		{
			this.OKBtn = new Button();
			this.CreatureList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.InfoHdr = new ColumnHeader();
			this.CancelBtn = new Button();
			this.Splitter = new SplitContainer();
			this.NamePanel = new Panel();
			this.NameBox = new TextBox();
			this.NameLbl = new Label();
			this.BrowserPanel = new Panel();
			this.Browser = new WebBrowser();
			this.DragLbl = new Label();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.NamePanel.SuspendLayout();
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
			ColumnHeader[] nameHdr = new ColumnHeader[] { this.NameHdr, this.InfoHdr };
			this.CreatureList.Columns.AddRange(nameHdr);
			this.CreatureList.Dock = DockStyle.Fill;
			this.CreatureList.FullRowSelect = true;
			this.CreatureList.HideSelection = false;
			this.CreatureList.Location = new Point(0, 27);
			this.CreatureList.Name = "CreatureList";
			this.CreatureList.Size = new System.Drawing.Size(330, 309);
			this.CreatureList.TabIndex = 0;
			this.CreatureList.UseCompatibleStateImageBehavior = false;
			this.CreatureList.View = View.Details;
			this.CreatureList.SelectedIndexChanged += new EventHandler(this.CreatureList_SelectedIndexChanged);
			this.CreatureList.DoubleClick += new EventHandler(this.TileList_DoubleClick);
			this.CreatureList.ItemDrag += new ItemDragEventHandler(this.CreatureList_ItemDrag);
			this.NameHdr.Text = "Creature";
			this.NameHdr.Width = 150;
			this.InfoHdr.Text = "Info";
			this.InfoHdr.Width = 150;
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
			this.Splitter.Panel1.Controls.Add(this.CreatureList);
			this.Splitter.Panel1.Controls.Add(this.NamePanel);
			this.Splitter.Panel2.Controls.Add(this.BrowserPanel);
			this.Splitter.Size = new System.Drawing.Size(693, 336);
			this.Splitter.SplitterDistance = 330;
			this.Splitter.TabIndex = 0;
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
			this.BrowserPanel.BorderStyle = BorderStyle.FixedSingle;
			this.BrowserPanel.Controls.Add(this.Browser);
			this.BrowserPanel.Controls.Add(this.DragLbl);
			this.BrowserPanel.Dock = DockStyle.Fill;
			this.BrowserPanel.Location = new Point(0, 0);
			this.BrowserPanel.Name = "BrowserPanel";
			this.BrowserPanel.Size = new System.Drawing.Size(359, 336);
			this.BrowserPanel.TabIndex = 0;
			this.Browser.AllowWebBrowserDrop = false;
			this.Browser.Dock = DockStyle.Fill;
			this.Browser.IsWebBrowserContextMenuEnabled = false;
			this.Browser.Location = new Point(0, 51);
			this.Browser.MinimumSize = new System.Drawing.Size(20, 20);
			this.Browser.Name = "Browser";
			this.Browser.ScriptErrorsSuppressed = true;
			this.Browser.Size = new System.Drawing.Size(357, 283);
			this.Browser.TabIndex = 0;
			this.Browser.WebBrowserShortcutsEnabled = false;
			this.Browser.Navigating += new WebBrowserNavigatingEventHandler(this.Browser_Navigating);
			this.DragLbl.AllowDrop = true;
			this.DragLbl.Dock = DockStyle.Top;
			this.DragLbl.Location = new Point(0, 0);
			this.DragLbl.Name = "DragLbl";
			this.DragLbl.Size = new System.Drawing.Size(357, 51);
			this.DragLbl.TabIndex = 1;
			this.DragLbl.Text = "Drag creatures here from the list at the left";
			this.DragLbl.TextAlign = ContentAlignment.MiddleCenter;
			this.DragLbl.DragOver += new DragEventHandler(this.DragLbl_DragOver);
			this.DragLbl.DragDrop += new DragEventHandler(this.DragLbl_DragDrop);
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
			base.Name = "CreatureMultipleSelectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Select Multiple Creatures";
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			this.NamePanel.ResumeLayout(false);
			this.NamePanel.PerformLayout();
			this.BrowserPanel.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private bool match(ICreature creature, string query)
		{
			string[] strArrays = query.ToLower().Split(new char[0]);
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				if (!this.match_token(creature, strArrays[i]))
				{
					return false;
				}
			}
			return true;
		}

		private bool match_token(ICreature creature, string token)
		{
			if (creature.Name.ToLower().Contains(token))
			{
				return true;
			}
			if (creature.Info.ToLower().Contains(token))
			{
				return true;
			}
			return false;
		}

		private void NameBox_TextChanged(object sender, EventArgs e)
		{
			this.update_list();
		}

		private void TileList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedCreature != null)
			{
				this.fCreatures.Add(this.SelectedCreature);
				this.update_list();
				this.update_stats();
			}
		}

		private void update_list()
		{
			this.CreatureList.BeginUpdate();
			this.CreatureList.Groups.Clear();
			this.CreatureList.Items.Clear();
			List<Creature> creatures = Session.Creatures;
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			foreach (Creature creature in creatures)
			{
				binarySearchTree.Add(creature.Category);
			}
			List<string> sortedList = binarySearchTree.SortedList;
			sortedList.Add("Miscellaneous Creatures");
			foreach (string str in sortedList)
			{
				this.CreatureList.Groups.Add(str, str);
			}
			List<ListViewItem> listViewItems = new List<ListViewItem>();
			foreach (Creature creature1 in creatures)
			{
				if (!this.match(creature1, this.NameBox.Text) || this.fCreatures.Contains(creature1))
				{
					continue;
				}
				ListViewItem listViewItem = new ListViewItem(creature1.Name);
				listViewItem.SubItems.Add(creature1.Info);
				listViewItem.Tag = creature1;
				if (creature1.Category == null || !(creature1.Category != ""))
				{
					listViewItem.Group = this.CreatureList.Groups["Miscellaneous Creatures"];
				}
				else
				{
					listViewItem.Group = this.CreatureList.Groups[creature1.Category];
				}
				listViewItems.Add(listViewItem);
			}
			this.CreatureList.Items.AddRange(listViewItems.ToArray());
			this.CreatureList.EndUpdate();
		}

		private void update_stats()
		{
			List<string> head = HTML.GetHead("", "", DisplaySize.Small);
			head.Add("<BODY>");
			if (this.fCreatures.Count == 0)
			{
				head.Add("<P class=instruction>");
				head.Add("You have not yet selected any creatures; to select a creature, drag it from the list at the left onto the box above");
				head.Add("</P>");
			}
			else
			{
				head.Add("<P class=table>");
				head.Add("<TABLE>");
				head.Add("<TR class=heading>");
				head.Add("<TD colspan=3><B>Selected Creatures</B></TD>");
				head.Add("</TR>");
				foreach (ICreature fCreature in this.fCreatures)
				{
					head.Add("<TR class=header>");
					head.Add(string.Concat("<TD colspan=2>", fCreature.Name, "</TD>"));
					head.Add(string.Concat("<TD align=center><A href=remove:", fCreature.ID, ">remove</A></TD>"));
					head.Add("</TR>");
				}
				head.Add("</TABLE>");
				head.Add("</P>");
			}
			foreach (ICreature creature in this.fCreatures)
			{
				EncounterCard encounterCard = new EncounterCard(creature);
				head.Add("<P class=table>");
				head.AddRange(encounterCard.AsText(null, CardMode.View, false));
				head.Add("</P>");
			}
			head.Add("</BODY>");
			head.Add("</HTML>");
			string str = HTML.Concatenate(head);
			this.Browser.Document.OpenNew(true);
			this.Browser.Document.Write(str);
		}
	}
}