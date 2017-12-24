using Masterplan;
using Masterplan.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class TileLibrarySelectForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private ListView LibraryList;

		private Button CancelBtn;

		private ColumnHeader NameHdr;

		private Panel ListPanel;

		private ToolStrip Toolbar;

		private ToolStripButton SelectAllBtn;

		private ToolStripButton DeselectAllBtn;

		public List<Library> Libraries
		{
			get
			{
				List<Library> libraries = new List<Library>();
				foreach (ListViewItem checkedItem in this.LibraryList.CheckedItems)
				{
					Library tag = checkedItem.Tag as Library;
					if (tag == null)
					{
						continue;
					}
					libraries.Add(tag);
				}
				return libraries;
			}
		}

		public TileLibrarySelectForm(List<Library> selected_libraries)
		{
			this.InitializeComponent();
			List<Library> libraries = new List<Library>();
			libraries.AddRange(Session.Libraries);
			libraries.Add(Session.Project.Library);
			foreach (Library library in libraries)
			{
				if (library.Tiles.Count == 0)
				{
					continue;
				}
				ListViewItem listViewItem = this.LibraryList.Items.Add(library.Name);
				listViewItem.Tag = library;
				listViewItem.Checked = selected_libraries.Contains(library);
			}
			Application.Idle += new EventHandler(this.Application_Idle);
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = this.Libraries.Count != 0;
		}

		private void DeselectAllBtn_Click(object sender, EventArgs e)
		{
			this.LibraryList.BeginUpdate();
			foreach (ListViewItem item in this.LibraryList.Items)
			{
				item.Checked = false;
			}
			this.LibraryList.EndUpdate();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(TileLibrarySelectForm));
			this.OKBtn = new Button();
			this.LibraryList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.CancelBtn = new Button();
			this.ListPanel = new Panel();
			this.Toolbar = new ToolStrip();
			this.SelectAllBtn = new ToolStripButton();
			this.DeselectAllBtn = new ToolStripButton();
			this.ListPanel.SuspendLayout();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(192, 312);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 1;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.LibraryList.CheckBoxes = true;
			this.LibraryList.Columns.AddRange(new ColumnHeader[] { this.NameHdr });
			this.LibraryList.Dock = DockStyle.Fill;
			this.LibraryList.FullRowSelect = true;
			this.LibraryList.HideSelection = false;
			this.LibraryList.Location = new Point(0, 25);
			this.LibraryList.MultiSelect = false;
			this.LibraryList.Name = "LibraryList";
			this.LibraryList.Size = new System.Drawing.Size(336, 269);
			this.LibraryList.TabIndex = 0;
			this.LibraryList.UseCompatibleStateImageBehavior = false;
			this.LibraryList.View = View.Details;
			this.NameHdr.Text = "Library";
			this.NameHdr.Width = 300;
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(273, 312);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 2;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.ListPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.ListPanel.Controls.Add(this.LibraryList);
			this.ListPanel.Controls.Add(this.Toolbar);
			this.ListPanel.Location = new Point(12, 12);
			this.ListPanel.Name = "ListPanel";
			this.ListPanel.Size = new System.Drawing.Size(336, 294);
			this.ListPanel.TabIndex = 3;
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] selectAllBtn = new ToolStripItem[] { this.SelectAllBtn, this.DeselectAllBtn };
			items.AddRange(selectAllBtn);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(336, 25);
			this.Toolbar.TabIndex = 1;
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
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(360, 347);
			base.Controls.Add(this.ListPanel);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TileLibrarySelectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Select Tile Libraries";
			this.ListPanel.ResumeLayout(false);
			this.ListPanel.PerformLayout();
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
		}

		private void SelectAllBtn_Click(object sender, EventArgs e)
		{
			this.LibraryList.BeginUpdate();
			foreach (ListViewItem item in this.LibraryList.Items)
			{
				item.Checked = true;
			}
			this.LibraryList.EndUpdate();
		}
	}
}