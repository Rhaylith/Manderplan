using Masterplan;
using Masterplan.Controls;
using Masterplan.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Resources;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class RegionalMapListForm : Form
	{
		private ListView MapList;

		private ColumnHeader MapHdr;

		private ToolStrip ListToolbar;

		private ToolStripButton RemoveBtn;

		private ToolStripButton EditBtn;

		private SplitContainer Splitter;

		private ToolStrip MapToolbar;

		private ToolStripDropDownButton ToolsMenu;

		private ToolStripMenuItem ToolsScreenshot;

		private ToolStripMenuItem ToolsPlayerView;

		private Button CloseBtn;

		private RegionalMapPanel MapPanel;

		private ToolStripSplitButton AddBtn;

		private ToolStripMenuItem AddImportProject;

		private ToolStripDropDownButton LocationMenu;

		private ToolStripMenuItem LocationEntry;

		public RegionalMap SelectedMap
		{
			get
			{
				if (this.MapList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.MapList.SelectedItems[0].Tag as RegionalMap;
			}
			set
			{
				this.MapList.SelectedItems.Clear();
				foreach (ListViewItem item in this.MapList.Items)
				{
					if (item.Tag != value)
					{
						continue;
					}
					item.Selected = true;
				}
			}
		}

		public RegionalMapListForm()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.update_maps();
		}

		private void AddBtn_Click(object sender, EventArgs e)
		{
			RegionalMap regionalMap = new RegionalMap()
			{
				Name = "New Map"
			};
			RegionalMapForm regionalMapForm = new RegionalMapForm(regionalMap, null);
			if (regionalMapForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Project.RegionalMaps.Add(regionalMapForm.Map);
				Session.Modified = true;
				this.update_maps();
				this.SelectedMap = regionalMapForm.Map;
			}
		}

		private void AddImportProject_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Filter = Program.ProjectFilter
			};
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Project project = Serialisation<Project>.Load(openFileDialog.FileName, SerialisationMode.Binary);
				if (project != null)
				{
					RegionalMapSelectForm regionalMapSelectForm = new RegionalMapSelectForm(project.RegionalMaps, null, true);
					if (regionalMapSelectForm.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
					{
						return;
					}
					Session.Project.RegionalMaps.AddRange(regionalMapSelectForm.Maps);
					Session.Modified = true;
					this.update_maps();
				}
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveBtn.Enabled = this.SelectedMap != null;
			this.EditBtn.Enabled = this.SelectedMap != null;
			this.LocationMenu.Enabled = this.MapPanel.SelectedLocation != null;
			this.ToolsMenu.Enabled = this.SelectedMap != null;
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedMap != null)
			{
				int map = Session.Project.RegionalMaps.IndexOf(this.SelectedMap);
				RegionalMapForm regionalMapForm = new RegionalMapForm(this.SelectedMap, null);
				if (regionalMapForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.RegionalMaps[map] = regionalMapForm.Map;
					Session.Modified = true;
					this.update_maps();
					this.SelectedMap = regionalMapForm.Map;
				}
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(RegionalMapListForm));
			this.MapList = new ListView();
			this.MapHdr = new ColumnHeader();
			this.ListToolbar = new ToolStrip();
			this.AddBtn = new ToolStripSplitButton();
			this.AddImportProject = new ToolStripMenuItem();
			this.RemoveBtn = new ToolStripButton();
			this.EditBtn = new ToolStripButton();
			this.Splitter = new SplitContainer();
			this.MapPanel = new RegionalMapPanel();
			this.MapToolbar = new ToolStrip();
			this.LocationMenu = new ToolStripDropDownButton();
			this.LocationEntry = new ToolStripMenuItem();
			this.ToolsMenu = new ToolStripDropDownButton();
			this.ToolsScreenshot = new ToolStripMenuItem();
			this.ToolsPlayerView = new ToolStripMenuItem();
			this.CloseBtn = new Button();
			this.ListToolbar.SuspendLayout();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.MapToolbar.SuspendLayout();
			base.SuspendLayout();
			this.MapList.Columns.AddRange(new ColumnHeader[] { this.MapHdr });
			this.MapList.Dock = DockStyle.Fill;
			this.MapList.FullRowSelect = true;
			this.MapList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.MapList.HideSelection = false;
			this.MapList.Location = new Point(0, 25);
			this.MapList.MultiSelect = false;
			this.MapList.Name = "MapList";
			this.MapList.Size = new System.Drawing.Size(205, 374);
			this.MapList.TabIndex = 1;
			this.MapList.UseCompatibleStateImageBehavior = false;
			this.MapList.View = View.Details;
			this.MapList.SelectedIndexChanged += new EventHandler(this.MapList_SelectedIndexChanged);
			this.MapList.DoubleClick += new EventHandler(this.EditBtn_Click);
			this.MapHdr.Text = "Map";
			this.MapHdr.Width = 172;
			ToolStripItemCollection items = this.ListToolbar.Items;
			ToolStripItem[] addBtn = new ToolStripItem[] { this.AddBtn, this.RemoveBtn, this.EditBtn };
			items.AddRange(addBtn);
			this.ListToolbar.Location = new Point(0, 0);
			this.ListToolbar.Name = "ListToolbar";
			this.ListToolbar.Size = new System.Drawing.Size(205, 25);
			this.ListToolbar.TabIndex = 0;
			this.ListToolbar.Text = "toolStrip1";
			this.AddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AddBtn.DropDownItems.AddRange(new ToolStripItem[] { this.AddImportProject });
			this.AddBtn.Image = (Image)componentResourceManager.GetObject("AddBtn.Image");
			this.AddBtn.ImageTransparentColor = Color.Magenta;
			this.AddBtn.Name = "AddBtn";
			this.AddBtn.Size = new System.Drawing.Size(45, 22);
			this.AddBtn.Text = "Add";
			this.AddBtn.ButtonClick += new EventHandler(this.AddBtn_Click);
			this.AddImportProject.Name = "AddImportProject";
			this.AddImportProject.Size = new System.Drawing.Size(209, 22);
			this.AddImportProject.Text = "Import from Project File...";
			this.AddImportProject.Click += new EventHandler(this.AddImportProject_Click);
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
			this.Splitter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Splitter.FixedPanel = FixedPanel.Panel1;
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.MapList);
			this.Splitter.Panel1.Controls.Add(this.ListToolbar);
			this.Splitter.Panel2.Controls.Add(this.MapPanel);
			this.Splitter.Panel2.Controls.Add(this.MapToolbar);
			this.Splitter.Size = new System.Drawing.Size(726, 399);
			this.Splitter.SplitterDistance = 205;
			this.Splitter.TabIndex = 11;
			this.MapPanel.AllowEditing = false;
			this.MapPanel.BorderStyle = BorderStyle.FixedSingle;
			this.MapPanel.Dock = DockStyle.Fill;
			this.MapPanel.HighlightedLocation = null;
			this.MapPanel.Location = new Point(0, 25);
			this.MapPanel.Map = null;
			this.MapPanel.Mode = MapViewMode.Thumbnail;
			this.MapPanel.Name = "MapPanel";
			this.MapPanel.Plot = null;
			this.MapPanel.SelectedLocation = null;
			this.MapPanel.ShowLocations = true;
			this.MapPanel.Size = new System.Drawing.Size(517, 374);
			this.MapPanel.TabIndex = 2;
			this.MapPanel.DoubleClick += new EventHandler(this.LocationEntry_Click);
			ToolStripItemCollection toolStripItemCollections = this.MapToolbar.Items;
			ToolStripItem[] locationMenu = new ToolStripItem[] { this.LocationMenu, this.ToolsMenu };
			toolStripItemCollections.AddRange(locationMenu);
			this.MapToolbar.Location = new Point(0, 0);
			this.MapToolbar.Name = "MapToolbar";
			this.MapToolbar.Size = new System.Drawing.Size(517, 25);
			this.MapToolbar.TabIndex = 1;
			this.MapToolbar.Text = "toolStrip1";
			this.LocationMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LocationMenu.DropDownItems.AddRange(new ToolStripItem[] { this.LocationEntry });
			this.LocationMenu.Image = (Image)componentResourceManager.GetObject("LocationMenu.Image");
			this.LocationMenu.ImageTransparentColor = Color.Magenta;
			this.LocationMenu.Name = "LocationMenu";
			this.LocationMenu.Size = new System.Drawing.Size(66, 22);
			this.LocationMenu.Text = "Location";
			this.LocationEntry.Name = "LocationEntry";
			this.LocationEntry.Size = new System.Drawing.Size(183, 22);
			this.LocationEntry.Text = "Encyclopedia Entry...";
			this.LocationEntry.Click += new EventHandler(this.LocationEntry_Click);
			this.ToolsMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems = this.ToolsMenu.DropDownItems;
			ToolStripItem[] toolsScreenshot = new ToolStripItem[] { this.ToolsScreenshot, this.ToolsPlayerView };
			dropDownItems.AddRange(toolsScreenshot);
			this.ToolsMenu.Image = (Image)componentResourceManager.GetObject("ToolsMenu.Image");
			this.ToolsMenu.ImageTransparentColor = Color.Magenta;
			this.ToolsMenu.Name = "ToolsMenu";
			this.ToolsMenu.Size = new System.Drawing.Size(49, 22);
			this.ToolsMenu.Text = "Tools";
			this.ToolsScreenshot.Name = "ToolsScreenshot";
			this.ToolsScreenshot.Size = new System.Drawing.Size(177, 22);
			this.ToolsScreenshot.Text = "Export Map";
			this.ToolsScreenshot.Click += new EventHandler(this.ToolsExport_Click);
			this.ToolsPlayerView.Name = "ToolsPlayerView";
			this.ToolsPlayerView.Size = new System.Drawing.Size(177, 22);
			this.ToolsPlayerView.Text = "Send to Player View";
			this.ToolsPlayerView.Click += new EventHandler(this.ToolsPlayerView_Click);
			this.CloseBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CloseBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.CloseBtn.Location = new Point(663, 417);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new System.Drawing.Size(75, 23);
			this.CloseBtn.TabIndex = 12;
			this.CloseBtn.Text = "Close";
			this.CloseBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.CloseBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(750, 452);
			base.Controls.Add(this.CloseBtn);
			base.Controls.Add(this.Splitter);
			base.MinimizeBox = false;
			base.Name = "RegionalMapListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Regional Maps";
			this.ListToolbar.ResumeLayout(false);
			this.ListToolbar.PerformLayout();
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel1.PerformLayout();
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.Panel2.PerformLayout();
			this.Splitter.ResumeLayout(false);
			this.MapToolbar.ResumeLayout(false);
			this.MapToolbar.PerformLayout();
			base.ResumeLayout(false);
		}

		private void LocationEntry_Click(object sender, EventArgs e)
		{
			if (this.MapPanel.SelectedLocation == null)
			{
				return;
			}
			EncyclopediaEntry encyclopediaEntry = Session.Project.Encyclopedia.FindEntryForAttachment(this.MapPanel.SelectedLocation.ID);
			if (encyclopediaEntry == null)
			{
				if (MessageBox.Show(string.Concat(string.Concat("There is no encyclopedia entry associated with this location.", Environment.NewLine), "Would you like to create one now?"), "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
				{
					return;
				}
				encyclopediaEntry = new EncyclopediaEntry()
				{
					Name = this.MapPanel.SelectedLocation.Name,
					AttachmentID = this.MapPanel.SelectedLocation.ID,
					Category = this.MapPanel.SelectedLocation.Category
				};
				if (encyclopediaEntry.Category == "")
				{
					encyclopediaEntry.Category = "Places";
				}
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

		private void MapList_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.MapPanel.Map = this.SelectedMap;
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedMap != null)
			{
				if (MessageBox.Show("Are you sure you want to delete this map?", "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
				{
					return;
				}
				Session.Project.RegionalMaps.Remove(this.SelectedMap);
				Session.Modified = true;
				this.update_maps();
			}
		}

		private void ToolsExport_Click(object sender, EventArgs e)
		{
			if (this.SelectedMap != null)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog()
				{
					FileName = this.SelectedMap.Name,
					Filter = "Bitmap Image |*.bmp|JPEG Image|*.jpg|GIF Image|*.gif|PNG Image|*.png"
				};
				if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					ImageFormat bmp = ImageFormat.Bmp;
					switch (saveFileDialog.FilterIndex)
					{
						case 1:
						{
							bmp = ImageFormat.Bmp;
							break;
						}
						case 2:
						{
							bmp = ImageFormat.Jpeg;
							break;
						}
						case 3:
						{
							bmp = ImageFormat.Gif;
							break;
						}
						case 4:
						{
							bmp = ImageFormat.Png;
							break;
						}
					}
					this.MapPanel.Map.Image.Save(saveFileDialog.FileName, bmp);
				}
			}
		}

		private void ToolsPlayerView_Click(object sender, EventArgs e)
		{
			if (this.SelectedMap != null)
			{
				if (Session.PlayerView == null)
				{
					Session.PlayerView = new PlayerViewForm(this);
				}
				Session.PlayerView.ShowRegionalMap(this.MapPanel);
			}
		}

		private void update_maps()
		{
			this.MapList.Items.Clear();
			foreach (RegionalMap regionalMap in Session.Project.RegionalMaps)
			{
				ListViewItem listViewItem = this.MapList.Items.Add(regionalMap.Name);
				listViewItem.Tag = regionalMap;
			}
			if (this.MapList.Items.Count == 0)
			{
				ListViewItem grayText = this.MapList.Items.Add("(no maps)");
				grayText.ForeColor = SystemColors.GrayText;
			}
		}
	}
}