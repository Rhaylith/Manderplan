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
	internal class EncyclopediaEntryDetailsForm : Form
	{
		private EncyclopediaEntry fEntry;

		private bool fShowDMInfo;

		private WebBrowser Browser;

		private ToolStrip Toolbar;

		private ToolStripButton PlayerViewBtn;

		private ToolStripButton DMBtn;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripDropDownButton ExportMenu;

		private ToolStripMenuItem ExportHTML;

		public EncyclopediaEntryDetailsForm(EncyclopediaEntry entry)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fEntry = entry;
			this.update_entry();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.DMBtn.Checked = this.fShowDMInfo;
		}

		private void Browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.Scheme == "picture")
			{
				e.Cancel = true;
				Guid guid = new Guid(e.Url.LocalPath);
				EncyclopediaImage encyclopediaImage = this.fEntry.FindImage(guid);
				if (encyclopediaImage != null)
				{
					(new EncyclopediaImageForm(encyclopediaImage)).ShowDialog();
				}
			}
		}

		private void DMBtn_Click(object sender, EventArgs e)
		{
			this.fShowDMInfo = !this.fShowDMInfo;
			this.update_entry();
		}

		private void ExportHTML_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog()
			{
				FileName = this.fEntry.Name,
				Filter = Program.HTMLFilter
			};
			if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				File.WriteAllText(saveFileDialog.FileName, this.Browser.DocumentText);
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(EncyclopediaEntryDetailsForm));
			this.Browser = new WebBrowser();
			this.Toolbar = new ToolStrip();
			this.DMBtn = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.ExportMenu = new ToolStripDropDownButton();
			this.ExportHTML = new ToolStripMenuItem();
			this.PlayerViewBtn = new ToolStripButton();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.Browser.Dock = DockStyle.Fill;
			this.Browser.IsWebBrowserContextMenuEnabled = false;
			this.Browser.Location = new Point(0, 25);
			this.Browser.Name = "Browser";
			this.Browser.ScriptErrorsSuppressed = true;
			this.Browser.Size = new System.Drawing.Size(372, 337);
			this.Browser.TabIndex = 2;
			this.Browser.WebBrowserShortcutsEnabled = false;
			this.Browser.Navigating += new WebBrowserNavigatingEventHandler(this.Browser_Navigating);
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] dMBtn = new ToolStripItem[] { this.DMBtn, this.toolStripSeparator2, this.ExportMenu, this.PlayerViewBtn };
			items.AddRange(dMBtn);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(372, 25);
			this.Toolbar.TabIndex = 3;
			this.Toolbar.Text = "toolStrip1";
			this.DMBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DMBtn.Image = (Image)componentResourceManager.GetObject("DMBtn.Image");
			this.DMBtn.ImageTransparentColor = Color.Magenta;
			this.DMBtn.Name = "DMBtn";
			this.DMBtn.Size = new System.Drawing.Size(86, 22);
			this.DMBtn.Text = "Show DM Info";
			this.DMBtn.Click += new EventHandler(this.DMBtn_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
			base.Name = "EncyclopediaEntryDetailsForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Encyclopedia Entry";
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void PlayerViewBtn_Click(object sender, EventArgs e)
		{
			if (this.fEntry != null)
			{
				if (Session.PlayerView == null)
				{
					Session.PlayerView = new PlayerViewForm(this);
				}
				Session.PlayerView.ShowEncyclopediaItem(this.fEntry);
			}
		}

		private void update_entry()
		{
			this.Browser.DocumentText = HTML.EncyclopediaEntry(this.fEntry, Session.Project.Encyclopedia, DisplaySize.Small, this.fShowDMInfo, false, false, true);
		}
	}
}