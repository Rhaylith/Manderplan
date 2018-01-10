using Masterplan;
using Masterplan.Data;
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
	internal class CategoryListForm : Form
	{
		private Button OKBtn;

		private Button CancelBtn;

		private ListView CatList;

		private ColumnHeader CatHdr;

		private Panel ListPanel;

		private ToolStrip Toolbar;

		private ToolStripButton SelectBtn;

		private ToolStripButton DeselectBtn;

		public List<string> Categories
		{
			get
			{
				if (this.CatList.CheckedItems.Count == this.CatList.Items.Count)
				{
					return null;
				}
				List<string> strs = new List<string>();
				foreach (ListViewItem checkedItem in this.CatList.CheckedItems)
				{
					strs.Add(checkedItem.Text);
				}
				return strs;
			}
		}

		public CategoryListForm(List<string> categories)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			foreach (Creature creature in Session.Creatures)
			{
				if (creature.Category == "")
				{
					continue;
				}
				binarySearchTree.Add(creature.Category);
			}
			List<string> sortedList = binarySearchTree.SortedList;
			List<string> strs = new List<string>();
			foreach (string str in sortedList)
			{
				string str1 = str.Substring(0, 1);
				if (strs.Contains(str1))
				{
					continue;
				}
				strs.Add(str1);
			}
			foreach (string str2 in strs)
			{
				this.CatList.Groups.Add(str2, str2);
			}
			foreach (string str3 in sortedList)
			{
				string str4 = str3.Substring(0, 1);
				ListViewItem item = this.CatList.Items.Add(str3);
				item.Checked = (categories == null ? true : categories.Contains(str3));
				item.Group = this.CatList.Groups[str4];
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = this.CatList.CheckedItems.Count != 0;
		}

		private void DeselectBtn_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in this.CatList.Items)
			{
				item.Checked = false;
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(CategoryListForm));
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.CatList = new ListView();
			this.CatHdr = new ColumnHeader();
			this.ListPanel = new Panel();
			this.Toolbar = new ToolStrip();
			this.SelectBtn = new ToolStripButton();
			this.DeselectBtn = new ToolStripButton();
			this.ListPanel.SuspendLayout();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(93, 295);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 1;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(174, 295);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 2;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.CatList.CheckBoxes = true;
			this.CatList.Columns.AddRange(new ColumnHeader[] { this.CatHdr });
			this.CatList.Dock = DockStyle.Fill;
			this.CatList.FullRowSelect = true;
			this.CatList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.CatList.HideSelection = false;
			this.CatList.Location = new Point(0, 25);
			this.CatList.MultiSelect = false;
			this.CatList.Name = "CatList";
			this.CatList.Size = new System.Drawing.Size(237, 252);
			this.CatList.TabIndex = 1;
			this.CatList.UseCompatibleStateImageBehavior = false;
			this.CatList.View = View.Details;
			this.CatHdr.Text = "Category";
			this.CatHdr.Width = 200;
			this.ListPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.ListPanel.Controls.Add(this.CatList);
			this.ListPanel.Controls.Add(this.Toolbar);
			this.ListPanel.Location = new Point(12, 12);
			this.ListPanel.Name = "ListPanel";
			this.ListPanel.Size = new System.Drawing.Size(237, 277);
			this.ListPanel.TabIndex = 0;
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] selectBtn = new ToolStripItem[] { this.SelectBtn, this.DeselectBtn };
			items.AddRange(selectBtn);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(237, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.SelectBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SelectBtn.Image = (Image)componentResourceManager.GetObject("SelectBtn.Image");
			this.SelectBtn.ImageTransparentColor = Color.Magenta;
			this.SelectBtn.Name = "SelectBtn";
			this.SelectBtn.Size = new System.Drawing.Size(59, 22);
			this.SelectBtn.Text = "Select All";
			this.SelectBtn.Click += new EventHandler(this.SelectBtn_Click);
			this.DeselectBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DeselectBtn.Image = (Image)componentResourceManager.GetObject("DeselectBtn.Image");
			this.DeselectBtn.ImageTransparentColor = Color.Magenta;
			this.DeselectBtn.Name = "DeselectBtn";
			this.DeselectBtn.Size = new System.Drawing.Size(72, 22);
			this.DeselectBtn.Text = "Deselect All";
			this.DeselectBtn.Click += new EventHandler(this.DeselectBtn_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(261, 330);
			base.Controls.Add(this.ListPanel);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CategoryListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Categories";
			this.ListPanel.ResumeLayout(false);
			this.ListPanel.PerformLayout();
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
		}

		private void SelectBtn_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in this.CatList.Items)
			{
				item.Checked = true;
			}
		}
	}
}