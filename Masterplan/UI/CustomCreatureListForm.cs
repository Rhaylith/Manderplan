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
	internal class CustomCreatureListForm : Form
	{
		private ToolStrip Toolbar;

		private ListView CreatureList;

		private ColumnHeader NameHdr;

		private ToolStripButton RemoveBtn;

		private ToolStripButton EditBtn;

		private ColumnHeader InfoHdr;

		private ColumnHeader StatsHdr;

		private StatusStrip Statusbar;

		private ToolStripStatusLabel InfoLbl;

		private ToolStripDropDownButton AddBtn;

		private ToolStripMenuItem AddNPC;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton StatBlockBtn;

		private Panel MainPanel;

		private Button CloseBtn;

		private ToolStripButton EncEntryBtn;

		private ToolStripMenuItem AddCreature;

		public CustomCreature SelectedCreature
		{
			get
			{
				if (this.CreatureList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.CreatureList.SelectedItems[0].Tag as CustomCreature;
			}
		}

		public NPC SelectedNPC
		{
			get
			{
				if (this.CreatureList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.CreatureList.SelectedItems[0].Tag as NPC;
			}
		}

		public CustomCreatureListForm()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.update_creatures();
		}

		private void AddCreature_Click(object sender, EventArgs e)
		{
			CreatureBuilderForm creatureBuilderForm = new CreatureBuilderForm(new CustomCreature()
			{
				Name = "New Creature"
			});
			if (creatureBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Project.CustomCreatures.Add(creatureBuilderForm.Creature as CustomCreature);
				Session.Modified = true;
				this.update_creatures();
			}
		}

		private void AddNPC_Click(object sender, EventArgs e)
		{
			if (!this.class_templates_exist())
			{
				string str = string.Concat("NPCs require class templates; you have no class templates defined.", Environment.NewLine);
				str = string.Concat(str, "You can define templates in the Libraries screen.");
				MessageBox.Show(str, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				NPC nPC = new NPC()
				{
					Name = "New NPC"
				};
				foreach (CreatureTemplate template in Session.Templates)
				{
					if (template.Type != CreatureTemplateType.Class)
					{
						continue;
					}
					nPC.TemplateID = template.ID;
					break;
				}
				CreatureBuilderForm creatureBuilderForm = new CreatureBuilderForm(nPC);
				if (creatureBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.NPCs.Add(creatureBuilderForm.Creature as NPC);
					Session.Modified = true;
					this.update_creatures();
					return;
				}
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveBtn.Enabled = (this.SelectedCreature != null ? true : this.SelectedNPC != null);
			this.EditBtn.Enabled = (this.SelectedCreature != null ? true : this.SelectedNPC != null);
			this.StatBlockBtn.Enabled = (this.SelectedCreature != null ? true : this.SelectedNPC != null);
			this.EncEntryBtn.Enabled = (this.SelectedCreature != null ? true : this.SelectedNPC != null);
		}

		private bool class_templates_exist()
		{
			bool flag;
			List<Library>.Enumerator enumerator = Session.Libraries.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					List<CreatureTemplate>.Enumerator enumerator1 = enumerator.Current.Templates.GetEnumerator();
					try
					{
						while (enumerator1.MoveNext())
						{
							if (enumerator1.Current.Type != CreatureTemplateType.Class)
							{
								continue;
							}
							flag = true;
							return flag;
						}
					}
					finally
					{
						((IDisposable)enumerator1).Dispose();
					}
				}
				return false;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		private void CustomCreatureListForm_Shown(object sender, EventArgs e)
		{
			this.CreatureList.Invalidate();
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCreature != null)
			{
				int creature = Session.Project.CustomCreatures.IndexOf(this.SelectedCreature);
				CreatureBuilderForm creatureBuilderForm = new CreatureBuilderForm(this.SelectedCreature);
				if (creatureBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.CustomCreatures[creature] = creatureBuilderForm.Creature as CustomCreature;
					Session.Modified = true;
					this.update_creatures();
				}
			}
			if (this.SelectedNPC != null)
			{
				int num = Session.Project.NPCs.IndexOf(this.SelectedNPC);
				CreatureBuilderForm creatureBuilderForm1 = new CreatureBuilderForm(this.SelectedNPC);
				if (creatureBuilderForm1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.NPCs[num] = creatureBuilderForm1.Creature as NPC;
					Session.Modified = true;
					this.update_creatures();
				}
			}
		}

		private void EncEntryBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCreature == null && this.SelectedNPC == null)
			{
				return;
			}
			Guid guid = (this.SelectedNPC != null ? this.SelectedNPC.ID : this.SelectedCreature.ID);
			string str = (this.SelectedNPC != null ? this.SelectedNPC.Name : this.SelectedCreature.Name);
			string str1 = (this.SelectedNPC != null ? "People" : "Creatures");
			EncyclopediaEntry encyclopediaEntry = Session.Project.Encyclopedia.FindEntryForAttachment(guid);
			if (encyclopediaEntry == null)
			{
				if (MessageBox.Show(string.Concat(string.Concat(string.Concat("There is no encyclopedia entry associated with ", str, "."), Environment.NewLine), "Would you like to create one now?"), "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
				{
					return;
				}
				encyclopediaEntry = new EncyclopediaEntry()
				{
					Name = str,
					AttachmentID = guid,
					Category = str1
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

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(CustomCreatureListForm));
			ListViewGroup listViewGroup = new ListViewGroup("Custom Creatures", HorizontalAlignment.Left);
			ListViewGroup listViewGroup1 = new ListViewGroup("NPCs", HorizontalAlignment.Left);
			this.Toolbar = new ToolStrip();
			this.AddBtn = new ToolStripDropDownButton();
			this.AddCreature = new ToolStripMenuItem();
			this.AddNPC = new ToolStripMenuItem();
			this.RemoveBtn = new ToolStripButton();
			this.EditBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.StatBlockBtn = new ToolStripButton();
			this.EncEntryBtn = new ToolStripButton();
			this.CreatureList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.InfoHdr = new ColumnHeader();
			this.StatsHdr = new ColumnHeader();
			this.Statusbar = new StatusStrip();
			this.InfoLbl = new ToolStripStatusLabel();
			this.MainPanel = new Panel();
			this.CloseBtn = new Button();
			this.Toolbar.SuspendLayout();
			this.Statusbar.SuspendLayout();
			this.MainPanel.SuspendLayout();
			base.SuspendLayout();
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] addBtn = new ToolStripItem[] { this.AddBtn, this.RemoveBtn, this.EditBtn, this.toolStripSeparator1, this.StatBlockBtn, this.EncEntryBtn };
			items.AddRange(addBtn);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(776, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.AddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems = this.AddBtn.DropDownItems;
			ToolStripItem[] addCreature = new ToolStripItem[] { this.AddCreature, this.AddNPC };
			dropDownItems.AddRange(addCreature);
			this.AddBtn.Image = (Image)componentResourceManager.GetObject("AddBtn.Image");
			this.AddBtn.ImageTransparentColor = Color.Magenta;
			this.AddBtn.Name = "AddBtn";
			this.AddBtn.Size = new System.Drawing.Size(42, 22);
			this.AddBtn.Text = "Add";
			this.AddCreature.Name = "AddCreature";
			this.AddCreature.Size = new System.Drawing.Size(155, 22);
			this.AddCreature.Text = "New Creature...";
			this.AddCreature.Click += new EventHandler(this.AddCreature_Click);
			this.AddNPC.Name = "AddNPC";
			this.AddNPC.Size = new System.Drawing.Size(155, 22);
			this.AddNPC.Text = "New NPC...";
			this.AddNPC.Click += new EventHandler(this.AddNPC_Click);
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
			this.StatBlockBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.StatBlockBtn.Image = (Image)componentResourceManager.GetObject("StatBlockBtn.Image");
			this.StatBlockBtn.ImageTransparentColor = Color.Magenta;
			this.StatBlockBtn.Name = "StatBlockBtn";
			this.StatBlockBtn.Size = new System.Drawing.Size(63, 22);
			this.StatBlockBtn.Text = "Stat Block";
			this.StatBlockBtn.Click += new EventHandler(this.StatBlockBtn_Click);
			this.EncEntryBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EncEntryBtn.Image = (Image)componentResourceManager.GetObject("EncEntryBtn.Image");
			this.EncEntryBtn.ImageTransparentColor = Color.Magenta;
			this.EncEntryBtn.Name = "EncEntryBtn";
			this.EncEntryBtn.Size = new System.Drawing.Size(111, 22);
			this.EncEntryBtn.Text = "Encyclopedia Entry";
			this.EncEntryBtn.Click += new EventHandler(this.EncEntryBtn_Click);
			ListView.ColumnHeaderCollection columns = this.CreatureList.Columns;
			ColumnHeader[] nameHdr = new ColumnHeader[] { this.NameHdr, this.InfoHdr, this.StatsHdr };
			columns.AddRange(nameHdr);
			this.CreatureList.Dock = DockStyle.Fill;
			this.CreatureList.FullRowSelect = true;
			listViewGroup.Header = "Custom Creatures";
			listViewGroup.Name = "CustomGroup";
			listViewGroup1.Header = "NPCs";
			listViewGroup1.Name = "NPCGroup";
			this.CreatureList.Groups.AddRange(new ListViewGroup[] { listViewGroup, listViewGroup1 });
			this.CreatureList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.CreatureList.HideSelection = false;
			this.CreatureList.Location = new Point(0, 25);
			this.CreatureList.MultiSelect = false;
			this.CreatureList.Name = "CreatureList";
			this.CreatureList.Size = new System.Drawing.Size(776, 219);
			this.CreatureList.Sorting = SortOrder.Ascending;
			this.CreatureList.TabIndex = 1;
			this.CreatureList.UseCompatibleStateImageBehavior = false;
			this.CreatureList.View = View.Details;
			this.CreatureList.DoubleClick += new EventHandler(this.EditBtn_Click);
			this.NameHdr.Text = "Creature";
			this.NameHdr.Width = 287;
			this.InfoHdr.Text = "Information";
			this.InfoHdr.Width = 150;
			this.StatsHdr.Text = "Statistics";
			this.StatsHdr.Width = 311;
			this.Statusbar.Items.AddRange(new ToolStripItem[] { this.InfoLbl });
			this.Statusbar.Location = new Point(0, 244);
			this.Statusbar.Name = "Statusbar";
			this.Statusbar.Size = new System.Drawing.Size(776, 22);
			this.Statusbar.SizingGrip = false;
			this.Statusbar.TabIndex = 2;
			this.Statusbar.Text = "statusStrip1";
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new System.Drawing.Size(696, 17);
			this.InfoLbl.Text = "This screen is for adding NPCs and unusual creatures to this project only. For reusable creatures, go to Libraries on the Tools menu.";
			this.MainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.MainPanel.Controls.Add(this.CreatureList);
			this.MainPanel.Controls.Add(this.Toolbar);
			this.MainPanel.Controls.Add(this.Statusbar);
			this.MainPanel.Location = new Point(12, 12);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new System.Drawing.Size(776, 266);
			this.MainPanel.TabIndex = 3;
			this.CloseBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CloseBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.CloseBtn.Location = new Point(713, 284);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new System.Drawing.Size(75, 23);
			this.CloseBtn.TabIndex = 4;
			this.CloseBtn.Text = "Close";
			this.CloseBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.CloseBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(800, 319);
			base.Controls.Add(this.CloseBtn);
			base.Controls.Add(this.MainPanel);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CustomCreatureListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Custom Creatures and NPCs";
			base.Shown += new EventHandler(this.CustomCreatureListForm_Shown);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.Statusbar.ResumeLayout(false);
			this.Statusbar.PerformLayout();
			this.MainPanel.ResumeLayout(false);
			this.MainPanel.PerformLayout();
			base.ResumeLayout(false);
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCreature != null)
			{
				if (MessageBox.Show("Are you sure you want to delete this creature?", "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
				{
					return;
				}
				Session.Project.CustomCreatures.Remove(this.SelectedCreature);
				Session.Modified = true;
				this.update_creatures();
			}
			if (this.SelectedNPC != null)
			{
				if (MessageBox.Show("Are you sure you want to delete this NPC?", "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
				{
					return;
				}
				Session.Project.NPCs.Remove(this.SelectedNPC);
				Session.Modified = true;
				this.update_creatures();
			}
		}

		private void StatBlockBtn_Click(object sender, EventArgs e)
		{
			EncounterCard encounterCard = null;
			if (this.SelectedCreature != null)
			{
				encounterCard = new EncounterCard()
				{
					CreatureID = this.SelectedCreature.ID
				};
			}
			if (this.SelectedNPC != null)
			{
				encounterCard = new EncounterCard()
				{
					CreatureID = this.SelectedNPC.ID
				};
			}
			(new CreatureDetailsForm(encounterCard)).ShowDialog();
		}

		private void update_creatures()
		{
			this.CreatureList.Items.Clear();
			foreach (CustomCreature customCreature in Session.Project.CustomCreatures)
			{
				if (customCreature != null)
				{
					ListViewItem item = this.CreatureList.Items.Add(customCreature.Name);
					ListViewItem.ListViewSubItemCollection subItems = item.SubItems;
					object[] level = new object[] { "Level ", customCreature.Level, " ", customCreature.Role };
					subItems.Add(string.Concat(level));
					ListViewItem.ListViewSubItemCollection listViewSubItemCollections = item.SubItems;
					object[] hP = new object[] { customCreature.HP, " HP; AC ", customCreature.AC, ", Fort ", customCreature.Fortitude, ", Ref ", customCreature.Reflex, ", Will ", customCreature.Will };
					listViewSubItemCollections.Add(string.Concat(hP));
					item.Group = this.CreatureList.Groups[0];
					item.Tag = customCreature;
				}
				else
				{
					return;
				}
			}
			foreach (NPC nPC in Session.Project.NPCs)
			{
				if (nPC != null)
				{
					ListViewItem listViewItem = this.CreatureList.Items.Add(nPC.Name);
					ListViewItem.ListViewSubItemCollection subItems1 = listViewItem.SubItems;
					object[] objArray = new object[] { "Level ", nPC.Level, " ", nPC.Role };
					subItems1.Add(string.Concat(objArray));
					ListViewItem.ListViewSubItemCollection listViewSubItemCollections1 = listViewItem.SubItems;
					object[] hP1 = new object[] { nPC.HP, " HP; AC ", nPC.AC, ", Fort ", nPC.Fortitude, ", Ref ", nPC.Reflex, ", Will ", nPC.Will };
					listViewSubItemCollections1.Add(string.Concat(hP1));
					listViewItem.Group = this.CreatureList.Groups[1];
					listViewItem.Tag = nPC;
				}
				else
				{
					return;
				}
			}
			if (this.CreatureList.Groups[0].Items.Count == 0)
			{
				ListViewItem grayText = this.CreatureList.Items.Add("(no custom creatures)");
				grayText.Group = this.CreatureList.Groups[0];
				grayText.ForeColor = SystemColors.GrayText;
			}
			if (this.CreatureList.Groups[1].Items.Count == 0)
			{
				ListViewItem item1 = this.CreatureList.Items.Add("(no NPCs)");
				item1.Group = this.CreatureList.Groups[1];
				item1.ForeColor = SystemColors.GrayText;
			}
			this.CreatureList.Sort();
		}
	}
}