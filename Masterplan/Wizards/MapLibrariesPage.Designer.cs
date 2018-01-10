using Masterplan;
using Masterplan.Data;
using Masterplan.Tools.Generators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Utils.Wizards;

namespace Masterplan.Wizards
{
	internal class MapLibrariesPage : UserControl, IWizardPage
	{
		private MapBuilderData fData;

		private Label InfoLbl;

		private ListView LibraryList;

		private ColumnHeader LibHdr;

		private ToolStrip Toolbar;

		private ToolStripButton SelectAllBtn;

		private ToolStripButton DeselectAllBtn;

		private LinkLabel InfoLinkLbl;

		public bool AllowBack
		{
			get
			{
				if (this.fData.DelveOnly)
				{
					return false;
				}
				return true;
			}
		}

		public bool AllowFinish
		{
			get
			{
				return this.LibraryList.CheckedItems.Count != 0;
			}
		}

		public bool AllowNext
		{
			get
			{
				return this.LibraryList.CheckedItems.Count != 0;
			}
		}

		public MapLibrariesPage()
		{
			this.InitializeComponent();
		}

		private void DeselectAllBtn_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in this.LibraryList.Items)
			{
				item.Checked = false;
			}
		}

		private void InfoLinkLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			string str = string.Concat("In order to be used with AutoBuild, map tiles need to be categorised (as doors, stairs, etc), so that they can be placed intelligently.", Environment.NewLine);
			str = string.Concat(str, Environment.NewLine);
			str = string.Concat(str, "Libraries which do not have categorised tiles cannot be used, and so are not shown in the list.");
			str = string.Concat(str, Environment.NewLine);
			str = string.Concat(str, Environment.NewLine);
			str = string.Concat(str, "You can set tile categories in the Libraries screen.");
			MessageBox.Show(this, str, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MapLibrariesPage));
			this.InfoLbl = new Label();
			this.LibraryList = new ListView();
			this.LibHdr = new ColumnHeader();
			this.Toolbar = new ToolStrip();
			this.SelectAllBtn = new ToolStripButton();
			this.DeselectAllBtn = new ToolStripButton();
			this.InfoLinkLbl = new LinkLabel();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.InfoLbl.Dock = DockStyle.Top;
			this.InfoLbl.Location = new Point(0, 0);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new System.Drawing.Size(372, 40);
			this.InfoLbl.TabIndex = 1;
			this.InfoLbl.Text = "Select the libraries you want to use to create the map.";
			this.LibraryList.CheckBoxes = true;
			this.LibraryList.Columns.AddRange(new ColumnHeader[] { this.LibHdr });
			this.LibraryList.Dock = DockStyle.Fill;
			this.LibraryList.FullRowSelect = true;
			this.LibraryList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.LibraryList.HideSelection = false;
			this.LibraryList.Location = new Point(0, 65);
			this.LibraryList.MultiSelect = false;
			this.LibraryList.Name = "LibraryList";
			this.LibraryList.Size = new System.Drawing.Size(372, 158);
			this.LibraryList.TabIndex = 2;
			this.LibraryList.UseCompatibleStateImageBehavior = false;
			this.LibraryList.View = View.Details;
			this.LibHdr.Text = "Library";
			this.LibHdr.Width = 300;
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] selectAllBtn = new ToolStripItem[] { this.SelectAllBtn, this.DeselectAllBtn };
			items.AddRange(selectAllBtn);
			this.Toolbar.Location = new Point(0, 40);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(372, 25);
			this.Toolbar.TabIndex = 3;
			this.Toolbar.Text = "toolStrip1";
			this.SelectAllBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SelectAllBtn.Image = (Image)componentResourceManager.GetObject("SelectAllBtn.Image");
			this.SelectAllBtn.ImageTransparentColor = Color.Magenta;
			this.SelectAllBtn.Name = "SelectAllBtn";
			this.SelectAllBtn.Size = new System.Drawing.Size(59, 22);
			this.SelectAllBtn.Text = "Select All";
			this.SelectAllBtn.Click += new EventHandler(this.SelectAllBtn_Click);
			this.DeselectAllBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DeselectAllBtn.Image = (Image)componentResourceManager.GetObject("DeselectAllBtn.Image");
			this.DeselectAllBtn.ImageTransparentColor = Color.Magenta;
			this.DeselectAllBtn.Name = "DeselectAllBtn";
			this.DeselectAllBtn.Size = new System.Drawing.Size(72, 22);
			this.DeselectAllBtn.Text = "Deselect All";
			this.DeselectAllBtn.Click += new EventHandler(this.DeselectAllBtn_Click);
			this.InfoLinkLbl.Dock = DockStyle.Bottom;
			this.InfoLinkLbl.Location = new Point(0, 223);
			this.InfoLinkLbl.Name = "InfoLinkLbl";
			this.InfoLinkLbl.Size = new System.Drawing.Size(372, 23);
			this.InfoLinkLbl.TabIndex = 4;
			this.InfoLinkLbl.TabStop = true;
			this.InfoLinkLbl.Text = "Why are my libraries not shown?";
			this.InfoLinkLbl.TextAlign = ContentAlignment.MiddleLeft;
			this.InfoLinkLbl.LinkClicked += new LinkLabelLinkClickedEventHandler(this.InfoLinkLbl_LinkClicked);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.LibraryList);
			base.Controls.Add(this.Toolbar);
			base.Controls.Add(this.InfoLinkLbl);
			base.Controls.Add(this.InfoLbl);
			base.Name = "MapLibrariesPage";
			base.Size = new System.Drawing.Size(372, 246);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public bool OnBack()
		{
			return true;
		}

		public bool OnFinish()
		{
			this.set_selected_libraries();
			return true;
		}

		public bool OnNext()
		{
			this.set_selected_libraries();
			return true;
		}

		public void OnShown(object data)
		{
			if (this.fData == null)
			{
				this.fData = data as MapBuilderData;
				this.LibraryList.Items.Clear();
				foreach (Library library in Session.Libraries)
				{
					if (!library.ShowInAutoBuild)
					{
						continue;
					}
					ListViewItem listViewItem = this.LibraryList.Items.Add(library.Name);
					listViewItem.Checked = this.fData.Libraries.Contains(library);
					listViewItem.Tag = library;
				}
				if (this.LibraryList.Items.Count == 0)
				{
					ListViewItem grayText = this.LibraryList.Items.Add("(no libraries)");
					grayText.ForeColor = SystemColors.GrayText;
					this.LibraryList.CheckBoxes = false;
				}
			}
		}

		private void SelectAllBtn_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in this.LibraryList.Items)
			{
				item.Checked = true;
			}
		}

		private void set_selected_libraries()
		{
			this.fData.Libraries.Clear();
			foreach (ListViewItem checkedItem in this.LibraryList.CheckedItems)
			{
				Library tag = checkedItem.Tag as Library;
				this.fData.Libraries.Add(tag);
			}
		}
	}
}