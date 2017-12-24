using Masterplan;
using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class EncyclopediaImageForm : Form
	{
		private IContainer components;

		private Label NameLbl;

		private TextBox NameBox;

		private System.Windows.Forms.Panel Panel;

		private ToolStrip Toolbar;

		private ToolStripButton BrowseBtn;

		private Button OKBtn;

		private Button CancelBtn;

		private System.Windows.Forms.PictureBox PictureBox;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton PlayerViewBtn;

		private ToolStripButton PasteBtn;

		private EncyclopediaImage fImage;

		public EncyclopediaImage Image
		{
			get
			{
				return this.fImage;
			}
		}

		public EncyclopediaImageForm(EncyclopediaImage img)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fImage = img.Copy();
			this.NameBox.Text = this.fImage.Name;
			this.PictureBox.Image = this.fImage.Image;
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.PasteBtn.Enabled = Clipboard.ContainsImage();
			this.PlayerViewBtn.Enabled = this.fImage.Image != null;
		}

		private void BrowseBtn_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Filter = Program.ImageFilter
			};
			if (openFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
			{
				return;
			}
			this.fImage.Image = System.Drawing.Image.FromFile(openFileDialog.FileName);
			Program.SetResolution(this.fImage.Image);
			this.PictureBox.Image = this.fImage.Image;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(EncyclopediaImageForm));
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.Panel = new System.Windows.Forms.Panel();
			this.PictureBox = new System.Windows.Forms.PictureBox();
			this.Toolbar = new ToolStrip();
			this.BrowseBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.PlayerViewBtn = new ToolStripButton();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.PasteBtn = new ToolStripButton();
			this.Panel.SuspendLayout();
			((ISupportInitialize)this.PictureBox).BeginInit();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new System.Drawing.Size(74, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Picture Name:";
			this.NameBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.NameBox.Location = new Point(92, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(303, 20);
			this.NameBox.TabIndex = 1;
			this.Panel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Panel.BorderStyle = BorderStyle.FixedSingle;
			this.Panel.Controls.Add(this.PictureBox);
			this.Panel.Controls.Add(this.Toolbar);
			this.Panel.Location = new Point(12, 38);
			this.Panel.Name = "Panel";
			this.Panel.Size = new System.Drawing.Size(383, 305);
			this.Panel.TabIndex = 2;
			this.PictureBox.Dock = DockStyle.Fill;
			this.PictureBox.Location = new Point(0, 25);
			this.PictureBox.Name = "PictureBox";
			this.PictureBox.Size = new System.Drawing.Size(381, 278);
			this.PictureBox.SizeMode = PictureBoxSizeMode.Zoom;
			this.PictureBox.TabIndex = 1;
			this.PictureBox.TabStop = false;
			this.PictureBox.DoubleClick += new EventHandler(this.BrowseBtn_Click);
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] browseBtn = new ToolStripItem[] { this.BrowseBtn, this.PasteBtn, this.toolStripSeparator1, this.PlayerViewBtn };
			items.AddRange(browseBtn);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(381, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.BrowseBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.BrowseBtn.Image = (System.Drawing.Image)componentResourceManager.GetObject("BrowseBtn.Image");
			this.BrowseBtn.ImageTransparentColor = Color.Magenta;
			this.BrowseBtn.Name = "BrowseBtn";
			this.BrowseBtn.Size = new System.Drawing.Size(82, 22);
			this.BrowseBtn.Text = "Select Picture";
			this.BrowseBtn.Click += new EventHandler(this.BrowseBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.PlayerViewBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PlayerViewBtn.Image = (System.Drawing.Image)componentResourceManager.GetObject("PlayerViewBtn.Image");
			this.PlayerViewBtn.ImageTransparentColor = Color.Magenta;
			this.PlayerViewBtn.Name = "PlayerViewBtn";
			this.PlayerViewBtn.Size = new System.Drawing.Size(114, 22);
			this.PlayerViewBtn.Text = "Send to Player View";
			this.PlayerViewBtn.Click += new EventHandler(this.PlayerViewBtn_Click);
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(239, 349);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(320, 349);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.PasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PasteBtn.Image = (System.Drawing.Image)componentResourceManager.GetObject("PasteBtn.Image");
			this.PasteBtn.ImageTransparentColor = Color.Magenta;
			this.PasteBtn.Name = "PasteBtn";
			this.PasteBtn.Size = new System.Drawing.Size(79, 22);
			this.PasteBtn.Text = "Paste Picture";
			this.PasteBtn.Click += new EventHandler(this.PasteBtn_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(407, 384);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.Panel);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MinimizeBox = false;
			base.Name = "EncyclopediaImageForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Encyclopedia Picture";
			this.Panel.ResumeLayout(false);
			this.Panel.PerformLayout();
			((ISupportInitialize)this.PictureBox).EndInit();
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fImage.Name = this.NameBox.Text;
		}

		private void PasteBtn_Click(object sender, EventArgs e)
		{
			if (Clipboard.ContainsImage())
			{
				this.fImage.Image = Clipboard.GetImage();
				Program.SetResolution(this.fImage.Image);
				this.PictureBox.Image = this.fImage.Image;
			}
		}

		private void PlayerViewBtn_Click(object sender, EventArgs e)
		{
			if (Session.PlayerView == null)
			{
				Session.PlayerView = new PlayerViewForm(this);
			}
			Session.PlayerView.ShowImage(this.fImage.Image);
		}
	}
}