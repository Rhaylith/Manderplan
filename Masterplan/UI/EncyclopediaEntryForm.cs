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
using Utils.Controls;

namespace Masterplan.UI
{
	internal class EncyclopediaEntryForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label TitleLbl;

		private TextBox TitleBox;

		private DefaultTextBox DetailsBox;

		private TabControl Pages;

		private TabPage DetailsPage;

		private TabPage LinksPage;

		private ListView EntryList;

		private ColumnHeader EntryHdr;

		private StatusStrip PlayerStatusbar;

		private ToolStripStatusLabel toolStripStatusLabel1;

		private Label CatLbl;

		private ComboBox CatBox;

		private TabPage DMPage;

		private DefaultTextBox DMBox;

		private StatusStrip DMStatusBar;

		private ToolStripStatusLabel toolStripStatusLabel2;

		private TabPage ImagesTab;

		private ToolStrip PictureToolbar;

		private ToolStripButton AddBtn;

		private ToolStripButton RemoveBtn;

		private ToolStripButton EditBtn;

		private ListView PictureList;

		private EncyclopediaEntry fEntry;

		public EncyclopediaEntry Entry
		{
			get
			{
				return this.fEntry;
			}
		}

		public EncyclopediaImage SelectedImage
		{
			get
			{
				if (this.PictureList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.PictureList.SelectedItems[0].Tag as EncyclopediaImage;
			}
		}

		public EncyclopediaEntryForm(EncyclopediaEntry entry)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			binarySearchTree.Add("People");
			binarySearchTree.Add("Places");
			binarySearchTree.Add("Things");
			binarySearchTree.Add("History");
			binarySearchTree.Add("Culture");
			binarySearchTree.Add("Geography");
			binarySearchTree.Add("Organisations");
			foreach (EncyclopediaEntry encyclopediaEntry in Session.Project.Encyclopedia.Entries)
			{
				if (encyclopediaEntry.Category == null || !(encyclopediaEntry.Category != ""))
				{
					continue;
				}
				binarySearchTree.Add(encyclopediaEntry.Category);
			}
			this.CatBox.Items.AddRange(binarySearchTree.SortedList.ToArray());
			this.fEntry = entry.Copy();
			this.TitleBox.Text = this.fEntry.Name;
			this.CatBox.Text = this.fEntry.Category;
			this.DetailsBox.Text = this.fEntry.Details;
			this.DMBox.Text = this.fEntry.DMInfo;
			foreach (EncyclopediaEntry encyclopediaEntry1 in Session.Project.Encyclopedia.Entries)
			{
				if (encyclopediaEntry1.ID == this.fEntry.ID)
				{
					continue;
				}
				ListViewItem listViewItem = this.EntryList.Items.Add(encyclopediaEntry1.Name);
				listViewItem.Tag = encyclopediaEntry1;
				listViewItem.Checked = Session.Project.Encyclopedia.FindLink(this.fEntry.ID, encyclopediaEntry1.ID) != null;
			}
			if (this.EntryList.Items.Count == 0)
			{
				ListViewItem grayText = this.EntryList.Items.Add("(no entries)");
				grayText.ForeColor = SystemColors.GrayText;
				this.EntryList.CheckBoxes = false;
			}
			this.update_pictures();
		}

		private void AddBtn_Click(object sender, EventArgs e)
		{
			EncyclopediaImageForm encyclopediaImageForm = new EncyclopediaImageForm(new EncyclopediaImage()
			{
				Name = "(name)"
			});
			if (encyclopediaImageForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fEntry.Images.Add(encyclopediaImageForm.Image);
				this.update_pictures();
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveBtn.Enabled = this.SelectedImage != null;
			this.EditBtn.Enabled = this.SelectedImage != null;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedImage != null)
			{
				int image = this.fEntry.Images.IndexOf(this.SelectedImage);
				EncyclopediaImageForm encyclopediaImageForm = new EncyclopediaImageForm(this.SelectedImage);
				if (encyclopediaImageForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fEntry.Images[image] = encyclopediaImageForm.Image;
					this.update_pictures();
				}
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(EncyclopediaEntryForm));
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.TitleLbl = new Label();
			this.TitleBox = new TextBox();
			this.DetailsBox = new DefaultTextBox();
			this.Pages = new TabControl();
			this.DetailsPage = new TabPage();
			this.PlayerStatusbar = new StatusStrip();
			this.toolStripStatusLabel1 = new ToolStripStatusLabel();
			this.DMPage = new TabPage();
			this.DMBox = new DefaultTextBox();
			this.DMStatusBar = new StatusStrip();
			this.toolStripStatusLabel2 = new ToolStripStatusLabel();
			this.LinksPage = new TabPage();
			this.EntryList = new ListView();
			this.EntryHdr = new ColumnHeader();
			this.ImagesTab = new TabPage();
			this.PictureList = new ListView();
			this.PictureToolbar = new ToolStrip();
			this.AddBtn = new ToolStripButton();
			this.RemoveBtn = new ToolStripButton();
			this.EditBtn = new ToolStripButton();
			this.CatLbl = new Label();
			this.CatBox = new ComboBox();
			this.Pages.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			this.PlayerStatusbar.SuspendLayout();
			this.DMPage.SuspendLayout();
			this.DMStatusBar.SuspendLayout();
			this.LinksPage.SuspendLayout();
			this.ImagesTab.SuspendLayout();
			this.PictureToolbar.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(438, 359);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 5;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(519, 359);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 6;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.TitleLbl.AutoSize = true;
			this.TitleLbl.Location = new Point(12, 15);
			this.TitleLbl.Name = "TitleLbl";
			this.TitleLbl.Size = new System.Drawing.Size(30, 13);
			this.TitleLbl.TabIndex = 0;
			this.TitleLbl.Text = "Title:";
			this.TitleBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.TitleBox.Location = new Point(70, 12);
			this.TitleBox.Name = "TitleBox";
			this.TitleBox.Size = new System.Drawing.Size(524, 20);
			this.TitleBox.TabIndex = 1;
			this.DetailsBox.AcceptsReturn = true;
			this.DetailsBox.AcceptsTab = true;
			this.DetailsBox.DefaultText = "(enter details here)";
			this.DetailsBox.Dock = DockStyle.Fill;
			this.DetailsBox.Location = new Point(3, 25);
			this.DetailsBox.Multiline = true;
			this.DetailsBox.Name = "DetailsBox";
			this.DetailsBox.ScrollBars = ScrollBars.Vertical;
			this.DetailsBox.Size = new System.Drawing.Size(568, 234);
			this.DetailsBox.TabIndex = 1;
			this.DetailsBox.Text = "(enter details here)";
			this.Pages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Controls.Add(this.DMPage);
			this.Pages.Controls.Add(this.LinksPage);
			this.Pages.Controls.Add(this.ImagesTab);
			this.Pages.Location = new Point(12, 65);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new System.Drawing.Size(582, 288);
			this.Pages.TabIndex = 4;
			this.DetailsPage.Controls.Add(this.DetailsBox);
			this.DetailsPage.Controls.Add(this.PlayerStatusbar);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new System.Windows.Forms.Padding(3);
			this.DetailsPage.Size = new System.Drawing.Size(574, 262);
			this.DetailsPage.TabIndex = 0;
			this.DetailsPage.Text = "Public Information";
			this.DetailsPage.UseVisualStyleBackColor = true;
			this.PlayerStatusbar.Dock = DockStyle.Top;
			this.PlayerStatusbar.Items.AddRange(new ToolStripItem[] { this.toolStripStatusLabel1 });
			this.PlayerStatusbar.Location = new Point(3, 3);
			this.PlayerStatusbar.Name = "PlayerStatusbar";
			this.PlayerStatusbar.Size = new System.Drawing.Size(568, 22);
			this.PlayerStatusbar.SizingGrip = false;
			this.PlayerStatusbar.TabIndex = 0;
			this.PlayerStatusbar.Text = "statusStrip1";
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(202, 17);
			this.toolStripStatusLabel1.Text = "Note: HTML tags are supported here.";
			this.DMPage.Controls.Add(this.DMBox);
			this.DMPage.Controls.Add(this.DMStatusBar);
			this.DMPage.Location = new Point(4, 22);
			this.DMPage.Name = "DMPage";
			this.DMPage.Padding = new System.Windows.Forms.Padding(3);
			this.DMPage.Size = new System.Drawing.Size(503, 211);
			this.DMPage.TabIndex = 2;
			this.DMPage.Text = "DM Information";
			this.DMPage.UseVisualStyleBackColor = true;
			this.DMBox.AcceptsReturn = true;
			this.DMBox.AcceptsTab = true;
			this.DMBox.DefaultText = "(enter details here)";
			this.DMBox.Dock = DockStyle.Fill;
			this.DMBox.Location = new Point(3, 25);
			this.DMBox.Multiline = true;
			this.DMBox.Name = "DMBox";
			this.DMBox.ScrollBars = ScrollBars.Vertical;
			this.DMBox.Size = new System.Drawing.Size(497, 183);
			this.DMBox.TabIndex = 3;
			this.DMBox.Text = "(enter details here)";
			this.DMStatusBar.Dock = DockStyle.Top;
			this.DMStatusBar.Items.AddRange(new ToolStripItem[] { this.toolStripStatusLabel2 });
			this.DMStatusBar.Location = new Point(3, 3);
			this.DMStatusBar.Name = "DMStatusBar";
			this.DMStatusBar.Size = new System.Drawing.Size(497, 22);
			this.DMStatusBar.SizingGrip = false;
			this.DMStatusBar.TabIndex = 2;
			this.DMStatusBar.Text = "statusStrip1";
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(202, 17);
			this.toolStripStatusLabel2.Text = "Note: HTML tags are supported here.";
			this.LinksPage.Controls.Add(this.EntryList);
			this.LinksPage.Location = new Point(4, 22);
			this.LinksPage.Name = "LinksPage";
			this.LinksPage.Padding = new System.Windows.Forms.Padding(3);
			this.LinksPage.Size = new System.Drawing.Size(503, 211);
			this.LinksPage.TabIndex = 1;
			this.LinksPage.Text = "See Also";
			this.LinksPage.UseVisualStyleBackColor = true;
			this.EntryList.CheckBoxes = true;
			this.EntryList.Columns.AddRange(new ColumnHeader[] { this.EntryHdr });
			this.EntryList.Dock = DockStyle.Fill;
			this.EntryList.FullRowSelect = true;
			this.EntryList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.EntryList.HideSelection = false;
			this.EntryList.Location = new Point(3, 3);
			this.EntryList.MultiSelect = false;
			this.EntryList.Name = "EntryList";
			this.EntryList.Size = new System.Drawing.Size(497, 205);
			this.EntryList.Sorting = SortOrder.Ascending;
			this.EntryList.TabIndex = 0;
			this.EntryList.UseCompatibleStateImageBehavior = false;
			this.EntryList.View = View.Details;
			this.EntryHdr.Text = "Entry";
			this.EntryHdr.Width = 300;
			this.ImagesTab.Controls.Add(this.PictureList);
			this.ImagesTab.Controls.Add(this.PictureToolbar);
			this.ImagesTab.Location = new Point(4, 22);
			this.ImagesTab.Name = "ImagesTab";
			this.ImagesTab.Padding = new System.Windows.Forms.Padding(3);
			this.ImagesTab.Size = new System.Drawing.Size(503, 211);
			this.ImagesTab.TabIndex = 3;
			this.ImagesTab.Text = "Pictures";
			this.ImagesTab.UseVisualStyleBackColor = true;
			this.PictureList.Dock = DockStyle.Fill;
			this.PictureList.Location = new Point(3, 28);
			this.PictureList.Name = "PictureList";
			this.PictureList.Size = new System.Drawing.Size(497, 180);
			this.PictureList.TabIndex = 1;
			this.PictureList.UseCompatibleStateImageBehavior = false;
			this.PictureList.DoubleClick += new EventHandler(this.EditBtn_Click);
			ToolStripItemCollection items = this.PictureToolbar.Items;
			ToolStripItem[] addBtn = new ToolStripItem[] { this.AddBtn, this.RemoveBtn, this.EditBtn };
			items.AddRange(addBtn);
			this.PictureToolbar.Location = new Point(3, 3);
			this.PictureToolbar.Name = "PictureToolbar";
			this.PictureToolbar.Size = new System.Drawing.Size(497, 25);
			this.PictureToolbar.TabIndex = 0;
			this.PictureToolbar.Text = "toolStrip1";
			this.AddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AddBtn.Image = (Image)componentResourceManager.GetObject("AddBtn.Image");
			this.AddBtn.ImageTransparentColor = Color.Magenta;
			this.AddBtn.Name = "AddBtn";
			this.AddBtn.Size = new System.Drawing.Size(33, 22);
			this.AddBtn.Text = "Add";
			this.AddBtn.Click += new EventHandler(this.AddBtn_Click);
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
			this.CatLbl.AutoSize = true;
			this.CatLbl.Location = new Point(12, 41);
			this.CatLbl.Name = "CatLbl";
			this.CatLbl.Size = new System.Drawing.Size(52, 13);
			this.CatLbl.TabIndex = 2;
			this.CatLbl.Text = "Category:";
			this.CatBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.CatBox.AutoCompleteMode = AutoCompleteMode.Append;
			this.CatBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.CatBox.FormattingEnabled = true;
			this.CatBox.Location = new Point(70, 38);
			this.CatBox.Name = "CatBox";
			this.CatBox.Size = new System.Drawing.Size(524, 21);
			this.CatBox.TabIndex = 3;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(606, 394);
			base.Controls.Add(this.CatBox);
			base.Controls.Add(this.CatLbl);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.TitleBox);
			base.Controls.Add(this.TitleLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MinimizeBox = false;
			base.Name = "EncyclopediaEntryForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Encyclopedia Entry";
			this.Pages.ResumeLayout(false);
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			this.PlayerStatusbar.ResumeLayout(false);
			this.PlayerStatusbar.PerformLayout();
			this.DMPage.ResumeLayout(false);
			this.DMPage.PerformLayout();
			this.DMStatusBar.ResumeLayout(false);
			this.DMStatusBar.PerformLayout();
			this.LinksPage.ResumeLayout(false);
			this.ImagesTab.ResumeLayout(false);
			this.ImagesTab.PerformLayout();
			this.PictureToolbar.ResumeLayout(false);
			this.PictureToolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fEntry.Name = this.TitleBox.Text;
			this.fEntry.Category = this.CatBox.Text;
			this.fEntry.Details = (this.DetailsBox.Text != this.DetailsBox.DefaultText ? this.DetailsBox.Text : "");
			this.fEntry.DMInfo = (this.DMBox.Text != this.DMBox.DefaultText ? this.DMBox.Text : "");
			List<EncyclopediaLink> encyclopediaLinks = new List<EncyclopediaLink>();
			foreach (EncyclopediaLink link in Session.Project.Encyclopedia.Links)
			{
				if (!link.EntryIDs.Contains(this.fEntry.ID))
				{
					continue;
				}
				encyclopediaLinks.Add(link);
			}
			foreach (EncyclopediaLink encyclopediaLink in encyclopediaLinks)
			{
				Session.Project.Encyclopedia.Links.Remove(encyclopediaLink);
			}
			foreach (ListViewItem checkedItem in this.EntryList.CheckedItems)
			{
				EncyclopediaEntry tag = checkedItem.Tag as EncyclopediaEntry;
				EncyclopediaLink encyclopediaLink1 = new EncyclopediaLink();
				encyclopediaLink1.EntryIDs.Add(this.fEntry.ID);
				encyclopediaLink1.EntryIDs.Add(tag.ID);
				Session.Project.Encyclopedia.Links.Add(encyclopediaLink1);
			}
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedImage != null)
			{
				this.fEntry.Images.Remove(this.SelectedImage);
				this.update_pictures();
			}
		}

		private void update_pictures()
		{
			this.PictureList.Items.Clear();
			this.PictureList.LargeImageList = null;
			ImageList imageList = new ImageList()
			{
				ImageSize = new System.Drawing.Size(64, 64),
				ColorDepth = ColorDepth.Depth32Bit
			};
			this.PictureList.LargeImageList = imageList;
			foreach (EncyclopediaImage image in this.fEntry.Images)
			{
				ListViewItem count = this.PictureList.Items.Add(image.Name);
				count.Tag = image;
				Image bitmap = new Bitmap(64, 64);
				Graphics graphic = Graphics.FromImage(bitmap);
				if (image.Image.Size.Width <= image.Image.Size.Height)
				{
					System.Drawing.Size size = image.Image.Size;
					System.Drawing.Size size1 = image.Image.Size;
					int width = size.Width * 64 / size1.Height;
					Rectangle rectangle = new Rectangle((64 - width) / 2, 0, width, 64);
					graphic.DrawImage(image.Image, rectangle);
				}
				else
				{
					System.Drawing.Size size2 = image.Image.Size;
					System.Drawing.Size size3 = image.Image.Size;
					int height = size2.Height * 64 / size3.Width;
					Rectangle rectangle1 = new Rectangle(0, (64 - height) / 2, 64, height);
					graphic.DrawImage(image.Image, rectangle1);
				}
				imageList.Images.Add(bitmap);
				count.ImageIndex = imageList.Images.Count - 1;
			}
			if (this.PictureList.Items.Count == 0)
			{
				ListViewItem grayText = this.PictureList.Items.Add("(no images)");
				grayText.ForeColor = SystemColors.GrayText;
			}
		}
	}
}