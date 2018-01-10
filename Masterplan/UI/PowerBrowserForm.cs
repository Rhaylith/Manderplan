using Masterplan;
using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class PowerBrowserForm : Form
	{
		private SplitContainer Splitter;

		private ListView CreatureList;

		private ColumnHeader CreatureHdr;

		private WebBrowser PowerDisplay;

		private ToolStrip CreatureListToolbar;

		private ColumnHeader CreatureInfoHdr;

		private Panel DisplayPanel;

		private Button CloseBtn;

		private ToolStrip PowerToolbar;

		private ToolStripDropDownButton ModeBtn;

		private ToolStripMenuItem ModeAll;

		private ToolStripMenuItem ModeSelection;

		private ToolStripButton StatsBtn;

		private Masterplan.Controls.FilterPanel FilterPanel;

		private string fName = "";

		private int fLevel;

		private IRole fRole;

		private bool fShowAll = true;

		private PowerCallback fCallback;

		private List<string> fAddedPowers = new List<string>();

		private List<CreaturePower> fPowers;

		private CreaturePower fSelectedPower;

		public List<ICreature> SelectedCreatures
		{
			get
			{
				List<ICreature> creatures = new List<ICreature>();
				if (!this.fShowAll)
				{
					foreach (ListViewItem selectedItem in this.CreatureList.SelectedItems)
					{
						ICreature tag = selectedItem.Tag as ICreature;
						if (tag == null)
						{
							continue;
						}
						creatures.Add(tag);
					}
				}
				else
				{
					foreach (ListViewItem item in this.CreatureList.Items)
					{
						ICreature creature = item.Tag as ICreature;
						if (creature == null)
						{
							continue;
						}
						creatures.Add(creature);
					}
				}
				return creatures;
			}
		}

		public CreaturePower SelectedPower
		{
			get
			{
				return this.fSelectedPower;
			}
		}

		public List<CreaturePower> ShownPowers
		{
			get
			{
				return this.fPowers;
			}
		}

		public PowerBrowserForm(string name, int level, IRole role, PowerCallback callback)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fName = name;
			this.fLevel = level;
			this.fRole = role;
			this.fCallback = callback;
			if (!this.FilterPanel.SetFilter(this.fLevel, this.fRole))
			{
				this.update_creature_list();
				if (this.SelectedCreatures.Count > 100)
				{
					this.fShowAll = false;
				}
				this.update_powers();
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.ModeAll.Checked = this.fShowAll;
			this.ModeSelection.Checked = !this.fShowAll;
		}

		private void copy_power(CreaturePower power)
		{
			CreaturePower creaturePower = power.Copy();
			creaturePower.ID = Guid.NewGuid();
			if (this.fCallback == null)
			{
				this.fSelectedPower = power;
				base.DialogResult = System.Windows.Forms.DialogResult.OK;
				base.Close();
				return;
			}
			this.fCallback(creaturePower);
			this.fAddedPowers.Add(creaturePower.Name);
			this.update_powers();
		}

		private void CreatureList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.fShowAll)
			{
				this.update_powers();
			}
		}

		private void FilterPanel_FilterChanged(object sender, EventArgs e)
		{
			this.update_creature_list();
			this.update_powers();
		}

		private CreaturePower find_power(Guid id)
		{
			CreaturePower creaturePower;
			List<CreaturePower>.Enumerator enumerator = this.fPowers.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					CreaturePower current = enumerator.Current;
					if (current.ID != id)
					{
						continue;
					}
					creaturePower = current;
					return creaturePower;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(PowerBrowserForm));
			this.Splitter = new SplitContainer();
			this.CreatureList = new ListView();
			this.CreatureHdr = new ColumnHeader();
			this.CreatureInfoHdr = new ColumnHeader();
			this.FilterPanel = new Masterplan.Controls.FilterPanel();
			this.CreatureListToolbar = new ToolStrip();
			this.ModeBtn = new ToolStripDropDownButton();
			this.ModeAll = new ToolStripMenuItem();
			this.ModeSelection = new ToolStripMenuItem();
			this.DisplayPanel = new Panel();
			this.PowerDisplay = new WebBrowser();
			this.PowerToolbar = new ToolStrip();
			this.StatsBtn = new ToolStripButton();
			this.CloseBtn = new Button();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.CreatureListToolbar.SuspendLayout();
			this.DisplayPanel.SuspendLayout();
			this.PowerToolbar.SuspendLayout();
			base.SuspendLayout();
			this.Splitter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Splitter.FixedPanel = FixedPanel.Panel2;
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.CreatureList);
			this.Splitter.Panel1.Controls.Add(this.FilterPanel);
			this.Splitter.Panel1.Controls.Add(this.CreatureListToolbar);
			this.Splitter.Panel2.Controls.Add(this.DisplayPanel);
			this.Splitter.Size = new System.Drawing.Size(734, 377);
			this.Splitter.SplitterDistance = 379;
			this.Splitter.TabIndex = 14;
			ListView.ColumnHeaderCollection columns = this.CreatureList.Columns;
			ColumnHeader[] creatureHdr = new ColumnHeader[] { this.CreatureHdr, this.CreatureInfoHdr };
			columns.AddRange(creatureHdr);
			this.CreatureList.Dock = DockStyle.Fill;
			this.CreatureList.FullRowSelect = true;
			this.CreatureList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.CreatureList.HideSelection = false;
			this.CreatureList.Location = new Point(0, 47);
			this.CreatureList.Name = "CreatureList";
			this.CreatureList.Size = new System.Drawing.Size(379, 330);
			this.CreatureList.Sorting = SortOrder.Ascending;
			this.CreatureList.TabIndex = 2;
			this.CreatureList.UseCompatibleStateImageBehavior = false;
			this.CreatureList.View = View.Details;
			this.CreatureList.SelectedIndexChanged += new EventHandler(this.CreatureList_SelectedIndexChanged);
			this.CreatureHdr.Text = "Creature";
			this.CreatureHdr.Width = 218;
			this.CreatureInfoHdr.Text = "Info";
			this.CreatureInfoHdr.Width = 123;
			this.FilterPanel.AutoSize = true;
			this.FilterPanel.Dock = DockStyle.Top;
			this.FilterPanel.Location = new Point(0, 25);
			this.FilterPanel.Mode = ListMode.Creatures;
			this.FilterPanel.Name = "FilterPanel";
			this.FilterPanel.PartyLevel = 0;
			this.FilterPanel.Size = new System.Drawing.Size(379, 22);
			this.FilterPanel.TabIndex = 17;
			this.FilterPanel.FilterChanged += new EventHandler(this.FilterPanel_FilterChanged);
			this.CreatureListToolbar.Items.AddRange(new ToolStripItem[] { this.ModeBtn });
			this.CreatureListToolbar.Location = new Point(0, 0);
			this.CreatureListToolbar.Name = "CreatureListToolbar";
			this.CreatureListToolbar.Size = new System.Drawing.Size(379, 25);
			this.CreatureListToolbar.TabIndex = 15;
			this.CreatureListToolbar.Text = "toolStrip1";
			this.ModeBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems = this.ModeBtn.DropDownItems;
			ToolStripItem[] modeAll = new ToolStripItem[] { this.ModeAll, this.ModeSelection };
			dropDownItems.AddRange(modeAll);
			this.ModeBtn.Image = (Image)componentResourceManager.GetObject("ModeBtn.Image");
			this.ModeBtn.ImageTransparentColor = Color.Magenta;
			this.ModeBtn.Name = "ModeBtn";
			this.ModeBtn.Size = new System.Drawing.Size(51, 22);
			this.ModeBtn.Text = "Mode";
			this.ModeAll.Name = "ModeAll";
			this.ModeAll.Size = new System.Drawing.Size(290, 22);
			this.ModeAll.Text = "List Powers from All Creatures";
			this.ModeAll.Click += new EventHandler(this.ModeAll_Click);
			this.ModeSelection.Name = "ModeSelection";
			this.ModeSelection.Size = new System.Drawing.Size(290, 22);
			this.ModeSelection.Text = "List Powers from Selected Creatures Only";
			this.ModeSelection.Click += new EventHandler(this.ModeSelection_Click);
			this.DisplayPanel.BorderStyle = BorderStyle.FixedSingle;
			this.DisplayPanel.Controls.Add(this.PowerDisplay);
			this.DisplayPanel.Controls.Add(this.PowerToolbar);
			this.DisplayPanel.Dock = DockStyle.Fill;
			this.DisplayPanel.Location = new Point(0, 0);
			this.DisplayPanel.Name = "DisplayPanel";
			this.DisplayPanel.Size = new System.Drawing.Size(351, 377);
			this.DisplayPanel.TabIndex = 0;
			this.PowerDisplay.Dock = DockStyle.Fill;
			this.PowerDisplay.IsWebBrowserContextMenuEnabled = false;
			this.PowerDisplay.Location = new Point(0, 25);
			this.PowerDisplay.MinimumSize = new System.Drawing.Size(20, 20);
			this.PowerDisplay.Name = "PowerDisplay";
			this.PowerDisplay.ScriptErrorsSuppressed = true;
			this.PowerDisplay.Size = new System.Drawing.Size(349, 350);
			this.PowerDisplay.TabIndex = 2;
			this.PowerDisplay.WebBrowserShortcutsEnabled = false;
			this.PowerDisplay.Navigating += new WebBrowserNavigatingEventHandler(this.PowerDisplay_Navigating);
			this.PowerToolbar.Items.AddRange(new ToolStripItem[] { this.StatsBtn });
			this.PowerToolbar.Location = new Point(0, 0);
			this.PowerToolbar.Name = "PowerToolbar";
			this.PowerToolbar.Size = new System.Drawing.Size(349, 25);
			this.PowerToolbar.TabIndex = 3;
			this.PowerToolbar.Text = "toolStrip1";
			this.StatsBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.StatsBtn.Image = (Image)componentResourceManager.GetObject("StatsBtn.Image");
			this.StatsBtn.ImageTransparentColor = Color.Magenta;
			this.StatsBtn.Name = "StatsBtn";
			this.StatsBtn.Size = new System.Drawing.Size(93, 22);
			this.StatsBtn.Text = "Power Statistics";
			this.StatsBtn.Click += new EventHandler(this.StatsBtn_Click);
			this.CloseBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CloseBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.CloseBtn.Location = new Point(671, 395);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new System.Drawing.Size(75, 23);
			this.CloseBtn.TabIndex = 15;
			this.CloseBtn.Text = "Close";
			this.CloseBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.CloseBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CloseBtn;
			base.ClientSize = new System.Drawing.Size(758, 430);
			base.Controls.Add(this.CloseBtn);
			base.Controls.Add(this.Splitter);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PowerBrowserForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Power Browser";
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel1.PerformLayout();
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			this.CreatureListToolbar.ResumeLayout(false);
			this.CreatureListToolbar.PerformLayout();
			this.DisplayPanel.ResumeLayout(false);
			this.DisplayPanel.PerformLayout();
			this.PowerToolbar.ResumeLayout(false);
			this.PowerToolbar.PerformLayout();
			base.ResumeLayout(false);
		}

		private bool match(Creature c, string token)
		{
			if (c.Name.ToLower().Contains(token.ToLower()))
			{
				return true;
			}
			if (c.Category.ToLower().Contains(token.ToLower()))
			{
				return true;
			}
			if (c.Info.ToLower().Contains(token.ToLower()))
			{
				return true;
			}
			if (c.Phenotype.ToLower().Contains(token.ToLower()))
			{
				return true;
			}
			return false;
		}

		private void ModeAll_Click(object sender, EventArgs e)
		{
			this.fShowAll = true;
			this.update_powers();
		}

		private void ModeSelection_Click(object sender, EventArgs e)
		{
			this.fShowAll = false;
			this.update_powers();
		}

		private void PowerDisplay_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.Scheme == "copy")
			{
				Guid guid = new Guid(e.Url.LocalPath);
				CreaturePower creaturePower = this.find_power(guid);
				if (creaturePower != null)
				{
					e.Cancel = true;
					this.copy_power(creaturePower);
				}
			}
		}

		private bool role_matches(IRole role_a, IRole role_b)
		{
			if (role_a is ComplexRole && role_b is ComplexRole)
			{
				return (role_a as ComplexRole).Type == (role_b as ComplexRole).Type;
			}
			if (!(role_a is Minion) || !(role_b is Minion))
			{
				return false;
			}
			Minion roleA = role_a as Minion;
			Minion roleB = role_b as Minion;
			if (!roleA.HasRole && !roleB.HasRole)
			{
				return true;
			}
			if (!roleA.HasRole || !roleB.HasRole)
			{
				return false;
			}
			return roleA.Type == roleB.Type;
		}

		private void StatsBtn_Click(object sender, EventArgs e)
		{
			PowerStatisticsForm powerStatisticsForm = new PowerStatisticsForm(this.fPowers, this.SelectedCreatures.Count);
			powerStatisticsForm.ShowDialog();
		}

		private void update_creature_list()
		{
			Difficulty difficulty;
			this.CreatureList.BeginUpdate();
			List<ICreature> creatures = new List<ICreature>();
			foreach (ICreature creature in Session.Creatures)
			{
				creatures.Add(creature);
			}
			if (Session.Project != null)
			{
				foreach (ICreature customCreature in Session.Project.CustomCreatures)
				{
					creatures.Add(customCreature);
				}
				foreach (ICreature nPC in Session.Project.NPCs)
				{
					creatures.Add(nPC);
				}
			}
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			foreach (ICreature creature1 in creatures)
			{
				if (creature1.Category == "")
				{
					continue;
				}
				binarySearchTree.Add(creature1.Category);
			}
			List<string> sortedList = binarySearchTree.SortedList;
			sortedList.Insert(0, "Custom Creatures");
			sortedList.Insert(1, "NPCs");
			sortedList.Add("Miscellaneous Creatures");
			this.CreatureList.Groups.Clear();
			foreach (string str in sortedList)
			{
				this.CreatureList.Groups.Add(str, str);
			}
			this.CreatureList.ShowGroups = true;
			List<ListViewItem> listViewItems = new List<ListViewItem>();
			foreach (ICreature creature2 in creatures)
			{
				if (creature2 == null || !this.FilterPanel.AllowItem(creature2, out difficulty))
				{
					continue;
				}
				ListViewItem listViewItem = new ListViewItem(creature2.Name);
				listViewItem.SubItems.Add(creature2.Info);
				listViewItem.Tag = creature2;
				if (creature2.Category == "")
				{
					listViewItem.Group = this.CreatureList.Groups["Miscellaneous Creatures"];
				}
				else
				{
					listViewItem.Group = this.CreatureList.Groups[creature2.Category];
				}
				listViewItems.Add(listViewItem);
			}
			this.CreatureList.Items.Clear();
			this.CreatureList.Items.AddRange(listViewItems.ToArray());
			if (this.CreatureList.Items.Count == 0)
			{
				this.CreatureList.ShowGroups = false;
				ListViewItem grayText = this.CreatureList.Items.Add("(no creatures)");
				grayText.ForeColor = SystemColors.GrayText;
			}
			this.CreatureList.EndUpdate();
		}

		private void update_powers()
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			List<string> strs = new List<string>();
			this.fPowers = new List<CreaturePower>();
			strs.AddRange(HTML.GetHead(null, null, DisplaySize.Small));
			strs.Add("<BODY>");
			List<ICreature> selectedCreatures = this.SelectedCreatures;
			if (selectedCreatures == null || selectedCreatures.Count == 0)
			{
				strs.Add("<P class=instruction>");
				strs.Add("(no creatures selected)");
				strs.Add("</P>");
			}
			else
			{
				strs.Add("<P class=instruction>");
				if (!this.fShowAll)
				{
					strs.Add("The selected creatures have the following powers:");
				}
				else
				{
					strs.Add("These creatures have the following powers:");
				}
				strs.Add("</P>");
				Dictionary<CreaturePowerCategory, List<CreaturePower>> creaturePowerCategories = new Dictionary<CreaturePowerCategory, List<CreaturePower>>();
				Array values = Enum.GetValues(typeof(CreaturePowerCategory));
				foreach (CreaturePowerCategory value in values)
				{
					creaturePowerCategories[value] = new List<CreaturePower>();
				}
				foreach (ICreature selectedCreature in selectedCreatures)
				{
					foreach (CreaturePower creaturePower in selectedCreature.CreaturePowers)
					{
						creaturePowerCategories[creaturePower.Category].Add(creaturePower);
						this.fPowers.Add(creaturePower);
					}
				}
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				foreach (CreaturePowerCategory creaturePowerCategory in values)
				{
					if (creaturePowerCategories[creaturePowerCategory].Count == 0)
					{
						continue;
					}
					creaturePowerCategories[creaturePowerCategory].Sort();
					string str = "";
					switch (creaturePowerCategory)
					{
						case CreaturePowerCategory.Trait:
						{
							str = "Traits";
							break;
						}
						case CreaturePowerCategory.Standard:
						case CreaturePowerCategory.Move:
						case CreaturePowerCategory.Minor:
						case CreaturePowerCategory.Free:
						{
							str = string.Concat(creaturePowerCategory, " Actions");
							break;
						}
						case CreaturePowerCategory.Triggered:
						{
							str = "Triggered Actions";
							break;
						}
						case CreaturePowerCategory.Other:
						{
							str = "Other Actions";
							break;
						}
					}
					strs.Add("<TR class=creature>");
					strs.Add("<TD colspan=3>");
					strs.Add(string.Concat("<B>", str, "</B>"));
					strs.Add("</TD>");
					strs.Add("</TR>");
					foreach (CreaturePower item in creaturePowerCategories[creaturePowerCategory])
					{
						strs.AddRange(item.AsHTML(null, CardMode.View, false));
						strs.Add("<TR>");
						strs.Add("<TD colspan=3 align=center>");
						if (this.fName == null || !(this.fName != ""))
						{
							strs.Add(string.Concat("<A href=copy:", item.ID, ">select this power</A>"));
						}
						else
						{
							object[] d = new object[] { "<A href=copy:", item.ID, ">copy this power into ", this.fName, "</A>" };
							strs.Add(string.Concat(d));
						}
						strs.Add("</TD>");
						strs.Add("</TR>");
					}
				}
				strs.Add("</TABLE>");
				strs.Add("</P>");
			}
			strs.Add("</BODY>");
			strs.Add("</HTML>");
			this.PowerDisplay.DocumentText = HTML.Concatenate(strs);
			System.Windows.Forms.Cursor.Current = Cursors.Default;
		}
	}
}