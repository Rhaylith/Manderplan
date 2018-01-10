using Masterplan;
using Masterplan.Data;
using Masterplan.Tools;
using Masterplan.Tools.Generators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class ParcelForm : Form
	{
		private Masterplan.Data.Parcel fParcel;

		private Label NameLbl;

		private TextBox NameBox;

		private Button OKBtn;

		private Button CancelBtn;

		private TextBox DetailsBox;

		private ToolStrip Toolbar;

		private WebBrowser Browser;

		private ToolStripDropDownButton ChangeTo;

		private ToolStripMenuItem ChangeToMundaneParcel;

		private ToolStripMenuItem ChangeToMagicItem;

		private ToolStripMenuItem ChangeToArtifact;

		private ToolStripButton SelectBtn;

		private Panel MainPanel;

		private Panel DetailsPanel;

		private ToolStripButton RandomiseBtn;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripSeparator toolStripSeparator2;

		public Masterplan.Data.Parcel Parcel
		{
			get
			{
				return this.fParcel;
			}
		}

		public ParcelForm(Masterplan.Data.Parcel p)
		{
			this.InitializeComponent();
			this.fParcel = p.Copy();
			this.set_controls();
		}

		private void ChangeToArtifact_Click(object sender, EventArgs e)
		{
			ArtifactSelectForm artifactSelectForm = new ArtifactSelectForm();
			if (artifactSelectForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fParcel.SetAsArtifact(artifactSelectForm.Artifact);
				this.NameBox.Text = this.fParcel.Name;
				this.DetailsBox.Text = this.fParcel.Details;
				this.set_controls();
			}
		}

		private void ChangeToMagicItem_Click(object sender, EventArgs e)
		{
			MagicItemSelectForm magicItemSelectForm = new MagicItemSelectForm(this.fParcel.FindItemLevel());
			if (magicItemSelectForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fParcel.SetAsMagicItem(magicItemSelectForm.MagicItem);
				this.NameBox.Text = this.fParcel.Name;
				this.DetailsBox.Text = this.fParcel.Details;
				this.set_controls();
			}
		}

		private void ChangeToMundaneParcel_Click(object sender, EventArgs e)
		{
			this.fParcel.MagicItemID = Guid.Empty;
			this.fParcel.ArtifactID = Guid.Empty;
			this.fParcel.Name = "";
			this.fParcel.Details = "";
			this.set_controls();
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ParcelForm));
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.DetailsBox = new TextBox();
			this.Toolbar = new ToolStrip();
			this.ChangeTo = new ToolStripDropDownButton();
			this.ChangeToMundaneParcel = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.ChangeToMagicItem = new ToolStripMenuItem();
			this.ChangeToArtifact = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.SelectBtn = new ToolStripButton();
			this.RandomiseBtn = new ToolStripButton();
			this.Browser = new WebBrowser();
			this.MainPanel = new Panel();
			this.DetailsPanel = new Panel();
			this.Toolbar.SuspendLayout();
			this.MainPanel.SuspendLayout();
			this.DetailsPanel.SuspendLayout();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(3, 6);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new System.Drawing.Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.NameBox.Location = new Point(47, 3);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(297, 20);
			this.NameBox.TabIndex = 1;
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(203, 359);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 5;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(284, 359);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 6;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.DetailsBox.AcceptsReturn = true;
			this.DetailsBox.AcceptsTab = true;
			this.DetailsBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.DetailsBox.Location = new Point(3, 29);
			this.DetailsBox.Multiline = true;
			this.DetailsBox.Name = "DetailsBox";
			this.DetailsBox.ScrollBars = ScrollBars.Vertical;
			this.DetailsBox.Size = new System.Drawing.Size(341, 284);
			this.DetailsBox.TabIndex = 0;
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] changeTo = new ToolStripItem[] { this.ChangeTo, this.toolStripSeparator1, this.SelectBtn, this.RandomiseBtn };
			items.AddRange(changeTo);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(347, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.ChangeTo.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems = this.ChangeTo.DropDownItems;
			ToolStripItem[] changeToMundaneParcel = new ToolStripItem[] { this.ChangeToMundaneParcel, this.toolStripSeparator2, this.ChangeToMagicItem, this.ChangeToArtifact };
			dropDownItems.AddRange(changeToMundaneParcel);
			this.ChangeTo.Image = (Image)componentResourceManager.GetObject("ChangeTo.Image");
			this.ChangeTo.ImageTransparentColor = Color.Magenta;
			this.ChangeTo.Name = "ChangeTo";
			this.ChangeTo.Size = new System.Drawing.Size(78, 22);
			this.ChangeTo.Text = "Change To";
			this.ChangeToMundaneParcel.Name = "ChangeToMundaneParcel";
			this.ChangeToMundaneParcel.Size = new System.Drawing.Size(160, 22);
			this.ChangeToMundaneParcel.Text = "Mundane Parcel";
			this.ChangeToMundaneParcel.Click += new EventHandler(this.ChangeToMundaneParcel_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(157, 6);
			this.ChangeToMagicItem.Name = "ChangeToMagicItem";
			this.ChangeToMagicItem.Size = new System.Drawing.Size(160, 22);
			this.ChangeToMagicItem.Text = "Magic Item...";
			this.ChangeToMagicItem.Click += new EventHandler(this.ChangeToMagicItem_Click);
			this.ChangeToArtifact.Name = "ChangeToArtifact";
			this.ChangeToArtifact.Size = new System.Drawing.Size(160, 22);
			this.ChangeToArtifact.Text = "Artifact...";
			this.ChangeToArtifact.Click += new EventHandler(this.ChangeToArtifact_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.SelectBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SelectBtn.Image = (Image)componentResourceManager.GetObject("SelectBtn.Image");
			this.SelectBtn.ImageTransparentColor = Color.Magenta;
			this.SelectBtn.Name = "SelectBtn";
			this.SelectBtn.Size = new System.Drawing.Size(42, 22);
			this.SelectBtn.Text = "Select";
			this.SelectBtn.Click += new EventHandler(this.SelectBtn_Click);
			this.RandomiseBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RandomiseBtn.Image = (Image)componentResourceManager.GetObject("RandomiseBtn.Image");
			this.RandomiseBtn.ImageTransparentColor = Color.Magenta;
			this.RandomiseBtn.Name = "RandomiseBtn";
			this.RandomiseBtn.Size = new System.Drawing.Size(70, 22);
			this.RandomiseBtn.Text = "Randomise";
			this.RandomiseBtn.Click += new EventHandler(this.RandomiseBtn_Click);
			this.Browser.Dock = DockStyle.Fill;
			this.Browser.Location = new Point(0, 25);
			this.Browser.MinimumSize = new System.Drawing.Size(20, 20);
			this.Browser.Name = "Browser";
			this.Browser.Size = new System.Drawing.Size(347, 316);
			this.Browser.TabIndex = 1;
			this.MainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.MainPanel.Controls.Add(this.Browser);
			this.MainPanel.Controls.Add(this.DetailsPanel);
			this.MainPanel.Controls.Add(this.Toolbar);
			this.MainPanel.Location = new Point(12, 12);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new System.Drawing.Size(347, 341);
			this.MainPanel.TabIndex = 9;
			this.DetailsPanel.Controls.Add(this.DetailsBox);
			this.DetailsPanel.Controls.Add(this.NameBox);
			this.DetailsPanel.Controls.Add(this.NameLbl);
			this.DetailsPanel.Dock = DockStyle.Fill;
			this.DetailsPanel.Location = new Point(0, 25);
			this.DetailsPanel.Name = "DetailsPanel";
			this.DetailsPanel.Size = new System.Drawing.Size(347, 316);
			this.DetailsPanel.TabIndex = 2;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(371, 394);
			base.Controls.Add(this.MainPanel);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ParcelForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Treasure Parcel";
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.MainPanel.ResumeLayout(false);
			this.MainPanel.PerformLayout();
			this.DetailsPanel.ResumeLayout(false);
			this.DetailsPanel.PerformLayout();
			base.ResumeLayout(false);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			if (this.fParcel.MagicItemID == Guid.Empty && this.fParcel.ArtifactID == Guid.Empty)
			{
				this.fParcel.Name = this.NameBox.Text;
				this.fParcel.Details = this.DetailsBox.Text;
			}
		}

		private void RandomiseBtn_Click(object sender, EventArgs e)
		{
			if (this.fParcel.MagicItemID != Guid.Empty)
			{
				MagicItem magicItem = Treasure.RandomMagicItem(this.fParcel.FindItemLevel());
				if (magicItem != null)
				{
					this.fParcel.SetAsMagicItem(magicItem);
				}
				this.set_controls();
				return;
			}
			if (this.fParcel.ArtifactID != Guid.Empty)
			{
				Artifact artifact = Treasure.RandomArtifact(this.fParcel.FindItemTier());
				if (artifact != null)
				{
					this.fParcel.SetAsArtifact(artifact);
				}
				this.set_controls();
				return;
			}
			int value = this.fParcel.Value;
			if (value == 0)
			{
				value = Treasure.GetItemValue(Session.Project.Party.Level);
			}
			this.fParcel = Treasure.CreateParcel(value, false);
			this.NameBox.Text = this.fParcel.Name;
			this.DetailsBox.Text = this.fParcel.Details;
			this.set_controls();
		}

		private void SelectBtn_Click(object sender, EventArgs e)
		{
			if (this.fParcel.MagicItemID != Guid.Empty)
			{
				this.ChangeToMagicItem_Click(this, e);
				return;
			}
			if (this.fParcel.ArtifactID != Guid.Empty)
			{
				this.ChangeToArtifact_Click(this, e);
			}
		}

		private void set_controls()
		{
			bool magicItemID = this.fParcel.MagicItemID != Guid.Empty;
			bool artifactID = this.fParcel.ArtifactID != Guid.Empty;
			bool flag = (magicItemID ? false : !artifactID);
			this.ChangeToMundaneParcel.Enabled = !flag;
			this.ChangeToMagicItem.Enabled = (magicItemID ? false : Session.MagicItems.Count != 0);
			this.ChangeToArtifact.Enabled = (artifactID ? false : Session.Artifacts.Count != 0);
			this.Browser.Visible = !flag;
			this.DetailsPanel.Visible = flag;
			this.SelectBtn.Enabled = (magicItemID ? true : artifactID);
			if (flag)
			{
				this.NameBox.Text = this.fParcel.Name;
				this.DetailsBox.Text = this.fParcel.Details;
				return;
			}
			MagicItem magicItem = Session.FindMagicItem(this.fParcel.MagicItemID, SearchType.Global);
			if (magicItem != null)
			{
				string str = HTML.MagicItem(magicItem, DisplaySize.Small, false, true);
				this.Browser.DocumentText = str;
			}
			Artifact artifact = Session.FindArtifact(this.fParcel.ArtifactID, SearchType.Global);
			if (artifact != null)
			{
				string str1 = HTML.Artifact(artifact, DisplaySize.Small, false, true);
				this.Browser.DocumentText = str1;
			}
		}
	}
}