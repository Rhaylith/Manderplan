using Masterplan;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class ArtifactDetailsForm : Form
	{
		private IContainer components;

		private WebBrowser Browser;

		private ToolStrip Toolbar;

		private ToolStripButton PlayerViewBtn;

		private ToolStripDropDownButton ExportMenu;

		private ToolStripMenuItem ExportHTML;

		private Artifact fArtifact;

		public ArtifactDetailsForm(Artifact artifact)
		{
			this.InitializeComponent();
			this.fArtifact = artifact.Copy();
			this.Browser.DocumentText = HTML.Artifact(this.fArtifact, DisplaySize.Small, false, true);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void ExportHTML_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog()
			{
				FileName = this.fArtifact.Name,
				Filter = Program.HTMLFilter
			};
			if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				File.WriteAllText(saveFileDialog.FileName, this.Browser.DocumentText);
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ArtifactDetailsForm));
			this.Browser = new WebBrowser();
			this.Toolbar = new ToolStrip();
			this.ExportMenu = new ToolStripDropDownButton();
			this.ExportHTML = new ToolStripMenuItem();
			this.PlayerViewBtn = new ToolStripButton();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.Browser.Dock = DockStyle.Fill;
			this.Browser.Location = new Point(0, 25);
			this.Browser.MinimumSize = new System.Drawing.Size(20, 20);
			this.Browser.Name = "Browser";
			this.Browser.Size = new System.Drawing.Size(372, 337);
			this.Browser.TabIndex = 1;
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] exportMenu = new ToolStripItem[] { this.ExportMenu, this.PlayerViewBtn };
			items.AddRange(exportMenu);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(372, 25);
			this.Toolbar.TabIndex = 4;
			this.Toolbar.Text = "toolStrip1";
			this.ExportMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ExportMenu.DropDownItems.AddRange(new ToolStripItem[] { this.ExportHTML });
			this.ExportMenu.Image = (Image)componentResourceManager.GetObject("ExportMenu.Image");
			this.ExportMenu.ImageTransparentColor = Color.Magenta;
			this.ExportMenu.Name = "ExportMenu";
			this.ExportMenu.Size = new System.Drawing.Size(53, 22);
			this.ExportMenu.Text = "Export";
			this.ExportHTML.Name = "ExportHTML";
			this.ExportHTML.Size = new System.Drawing.Size(157, 22);
			this.ExportHTML.Text = "Export to HTML";
			this.ExportHTML.Click += new EventHandler(this.ExportHTML_Click);
			this.PlayerViewBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PlayerViewBtn.Image = (Image)componentResourceManager.GetObject("PlayerViewBtn.Image");
			this.PlayerViewBtn.ImageTransparentColor = Color.Magenta;
			this.PlayerViewBtn.Name = "PlayerViewBtn";
			this.PlayerViewBtn.Size = new System.Drawing.Size(114, 22);
			this.PlayerViewBtn.Text = "Send to Player View";
			this.PlayerViewBtn.Click += new EventHandler(this.PlayerViewBtn_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(372, 362);
			base.Controls.Add(this.Browser);
			base.Controls.Add(this.Toolbar);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ArtifactDetailsForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Artifact Details";
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void PlayerViewBtn_Click(object sender, EventArgs e)
		{
			if (Session.PlayerView == null)
			{
				Session.PlayerView = new PlayerViewForm(this);
			}
			Session.PlayerView.ShowArtifact(this.fArtifact);
		}
	}
}