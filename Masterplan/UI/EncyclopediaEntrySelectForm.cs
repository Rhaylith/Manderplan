using Masterplan;
using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class EncyclopediaEntrySelectForm : Form
	{
		private Button OKBtn;

		private ListView EntryList;

		private Button CancelBtn;

		private ColumnHeader NameHdr;

		public Masterplan.Data.EncyclopediaEntry EncyclopediaEntry
		{
			get
			{
				if (this.EntryList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.EntryList.SelectedItems[0].Tag as Masterplan.Data.EncyclopediaEntry;
			}
		}

		public EncyclopediaEntrySelectForm(List<Guid> ignore_ids)
		{
			this.InitializeComponent();
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			foreach (Masterplan.Data.EncyclopediaEntry entry in Session.Project.Encyclopedia.Entries)
			{
				if (entry.Category == null || !(entry.Category != ""))
				{
					continue;
				}
				binarySearchTree.Add(entry.Category);
			}
			List<string> sortedList = binarySearchTree.SortedList;
			sortedList.Insert(0, "Miscellaneous Entries");
			foreach (string str in sortedList)
			{
				this.EntryList.Groups.Add(new ListViewGroup(str, str));
			}
			foreach (Masterplan.Data.EncyclopediaEntry encyclopediaEntry in Session.Project.Encyclopedia.Entries)
			{
				if (ignore_ids.Contains(encyclopediaEntry.ID))
				{
					continue;
				}
				ListViewItem item = this.EntryList.Items.Add(encyclopediaEntry.Name);
				item.Tag = encyclopediaEntry;
				if (encyclopediaEntry.Category == null || !(encyclopediaEntry.Category != ""))
				{
					item.Group = this.EntryList.Groups["Miscellaneous Entries"];
				}
				else
				{
					item.Group = this.EntryList.Groups[encyclopediaEntry.Category];
				}
			}
			if (this.EntryList.Items.Count == 0)
			{
				ListViewItem grayText = this.EntryList.Items.Add("(no entries)");
				grayText.ForeColor = SystemColors.GrayText;
			}
			Application.Idle += new EventHandler(this.Application_Idle);
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = this.EncyclopediaEntry != null;
		}

		private void InitializeComponent()
		{
			this.OKBtn = new Button();
			this.EntryList = new ListView();
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
			this.EntryList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.EntryList.Columns.AddRange(new ColumnHeader[] { this.NameHdr });
			this.EntryList.FullRowSelect = true;
			this.EntryList.HideSelection = false;
			this.EntryList.Location = new Point(12, 12);
			this.EntryList.MultiSelect = false;
			this.EntryList.Name = "EntryList";
			this.EntryList.Size = new System.Drawing.Size(332, 336);
			this.EntryList.Sorting = SortOrder.Ascending;
			this.EntryList.TabIndex = 0;
			this.EntryList.UseCompatibleStateImageBehavior = false;
			this.EntryList.View = View.Details;
			this.EntryList.DoubleClick += new EventHandler(this.TileList_DoubleClick);
			this.NameHdr.Text = "Entry";
			this.NameHdr.Width = 300;
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
			base.Controls.Add(this.EntryList);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "EncyclopediaEntrySelectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Select an Encyclopedia Entry";
			base.ResumeLayout(false);
		}

		private void TileList_DoubleClick(object sender, EventArgs e)
		{
			if (this.EncyclopediaEntry != null)
			{
				base.DialogResult = System.Windows.Forms.DialogResult.OK;
				base.Close();
			}
		}
	}
}