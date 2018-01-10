using Masterplan.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class MapSelectForm : Form
	{
		private Button OKBtn;

		private ListView MapList;

		private Button CancelBtn;

		private ColumnHeader NameHdr;

		public Masterplan.Data.Map Map
		{
			get
			{
				if (this.MapList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.MapList.SelectedItems[0].Tag as Masterplan.Data.Map;
			}
		}

		public List<Masterplan.Data.Map> Maps
		{
			get
			{
				List<Masterplan.Data.Map> maps = new List<Masterplan.Data.Map>();
				foreach (ListViewItem checkedItem in this.MapList.CheckedItems)
				{
					Masterplan.Data.Map tag = checkedItem.Tag as Masterplan.Data.Map;
					if (tag == null)
					{
						continue;
					}
					maps.Add(tag);
				}
				return maps;
			}
		}

		public MapSelectForm(List<Masterplan.Data.Map> maps, List<Guid> exclude, bool multi_select)
		{
			this.InitializeComponent();
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			foreach (Masterplan.Data.Map map in maps)
			{
				if (map.Category == null || !(map.Category != ""))
				{
					continue;
				}
				binarySearchTree.Add(map.Category);
			}
			List<string> sortedList = binarySearchTree.SortedList;
			sortedList.Add("Miscellaneous Maps");
			foreach (string str in sortedList)
			{
				this.MapList.Groups.Add(str, str);
			}
			foreach (Masterplan.Data.Map map1 in maps)
			{
				if (exclude != null && exclude.Contains(map1.ID))
				{
					continue;
				}
				ListViewItem item = this.MapList.Items.Add(map1.Name);
				item.Tag = map1;
				if (map1.Category == null || !(map1.Category != ""))
				{
					item.Group = this.MapList.Groups["Miscellaneous Maps"];
				}
				else
				{
					item.Group = this.MapList.Groups[map1.Category];
				}
			}
			if (multi_select)
			{
				this.MapList.CheckBoxes = true;
			}
			Application.Idle += new EventHandler(this.Application_Idle);
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			if (!this.MapList.CheckBoxes)
			{
				this.OKBtn.Enabled = this.Map != null;
				return;
			}
			this.OKBtn.Enabled = this.MapList.CheckedItems.Count != 0;
		}

		private void InitializeComponent()
		{
			ListViewGroup listViewGroup = new ListViewGroup("Trap", HorizontalAlignment.Left);
			ListViewGroup listViewGroup1 = new ListViewGroup("Hazard", HorizontalAlignment.Left);
			this.OKBtn = new Button();
			this.MapList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.CancelBtn = new Button();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(188, 354);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 1;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.MapList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.MapList.Columns.AddRange(new ColumnHeader[] { this.NameHdr });
			this.MapList.FullRowSelect = true;
			listViewGroup.Header = "Trap";
			listViewGroup.Name = "listViewGroup1";
			listViewGroup1.Header = "Hazard";
			listViewGroup1.Name = "listViewGroup2";
			this.MapList.Groups.AddRange(new ListViewGroup[] { listViewGroup, listViewGroup1 });
			this.MapList.HideSelection = false;
			this.MapList.Location = new Point(12, 12);
			this.MapList.MultiSelect = false;
			this.MapList.Name = "MapList";
			this.MapList.Size = new System.Drawing.Size(332, 336);
			this.MapList.TabIndex = 0;
			this.MapList.UseCompatibleStateImageBehavior = false;
			this.MapList.View = View.Details;
			this.MapList.DoubleClick += new EventHandler(this.TileList_DoubleClick);
			this.NameHdr.Text = "Map";
			this.NameHdr.Width = 281;
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(269, 354);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 2;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(356, 389);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.MapList);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MapSelectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Select a Map";
			base.ResumeLayout(false);
		}

		private void TileList_DoubleClick(object sender, EventArgs e)
		{
			if (this.Map != null)
			{
				base.DialogResult = System.Windows.Forms.DialogResult.OK;
				base.Close();
			}
		}
	}
}