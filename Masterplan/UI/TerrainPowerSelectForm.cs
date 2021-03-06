using Masterplan;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class TerrainPowerSelectForm : Form
	{
		private Button OKBtn;

		private ListView ChallengeList;

		private Button CancelBtn;

		private ColumnHeader NameHdr;

		private ColumnHeader InfoHdr;

		private SplitContainer Splitter;

		private Panel BrowserPanel;

		private WebBrowser Browser;

		public Masterplan.Data.TerrainPower TerrainPower
		{
			get
			{
				if (this.ChallengeList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.ChallengeList.SelectedItems[0].Tag as Masterplan.Data.TerrainPower;
			}
		}

		public TerrainPowerSelectForm()
		{
			this.InitializeComponent();
			foreach (Masterplan.Data.TerrainPower terrainPower in Session.TerrainPowers)
			{
				ListViewItem listViewItem = this.ChallengeList.Items.Add(terrainPower.Name);
				listViewItem.SubItems.Add(terrainPower.Name);
				listViewItem.Tag = terrainPower;
			}
			Application.Idle += new EventHandler(this.Application_Idle);
			this.Browser.DocumentText = "";
			this.ChallengeList_SelectedIndexChanged(null, null);
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = this.TerrainPower != null;
		}

		private void ChallengeList_SelectedIndexChanged(object sender, EventArgs e)
		{
			string str = HTML.TerrainPower(this.TerrainPower, DisplaySize.Small);
			this.Browser.Document.OpenNew(true);
			this.Browser.Document.Write(str);
		}

		private void InitializeComponent()
		{
			this.OKBtn = new Button();
			this.ChallengeList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.InfoHdr = new ColumnHeader();
			this.CancelBtn = new Button();
			this.Splitter = new SplitContainer();
			this.BrowserPanel = new Panel();
			this.Browser = new WebBrowser();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.BrowserPanel.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(549, 354);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 1;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			ListView.ColumnHeaderCollection columns = this.ChallengeList.Columns;
			ColumnHeader[] nameHdr = new ColumnHeader[] { this.NameHdr, this.InfoHdr };
			columns.AddRange(nameHdr);
			this.ChallengeList.Dock = DockStyle.Fill;
			this.ChallengeList.FullRowSelect = true;
			this.ChallengeList.HideSelection = false;
			this.ChallengeList.Location = new Point(0, 0);
			this.ChallengeList.MultiSelect = false;
			this.ChallengeList.Name = "ChallengeList";
			this.ChallengeList.Size = new System.Drawing.Size(330, 336);
			this.ChallengeList.Sorting = SortOrder.Ascending;
			this.ChallengeList.TabIndex = 0;
			this.ChallengeList.UseCompatibleStateImageBehavior = false;
			this.ChallengeList.View = View.Details;
			this.ChallengeList.SelectedIndexChanged += new EventHandler(this.ChallengeList_SelectedIndexChanged);
			this.ChallengeList.DoubleClick += new EventHandler(this.TileList_DoubleClick);
			this.NameHdr.Text = "Skill Challenge";
			this.NameHdr.Width = 150;
			this.InfoHdr.Text = "Info";
			this.InfoHdr.Width = 150;
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(630, 354);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 2;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.Splitter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.ChallengeList);
			this.Splitter.Panel2.Controls.Add(this.BrowserPanel);
			this.Splitter.Size = new System.Drawing.Size(693, 336);
			this.Splitter.SplitterDistance = 330;
			this.Splitter.TabIndex = 3;
			this.BrowserPanel.BorderStyle = BorderStyle.FixedSingle;
			this.BrowserPanel.Controls.Add(this.Browser);
			this.BrowserPanel.Dock = DockStyle.Fill;
			this.BrowserPanel.Location = new Point(0, 0);
			this.BrowserPanel.Name = "BrowserPanel";
			this.BrowserPanel.Size = new System.Drawing.Size(359, 336);
			this.BrowserPanel.TabIndex = 0;
			this.Browser.AllowWebBrowserDrop = false;
			this.Browser.Dock = DockStyle.Fill;
			this.Browser.IsWebBrowserContextMenuEnabled = false;
			this.Browser.Location = new Point(0, 0);
			this.Browser.MinimumSize = new System.Drawing.Size(20, 20);
			this.Browser.Name = "Browser";
			this.Browser.ScriptErrorsSuppressed = true;
			this.Browser.Size = new System.Drawing.Size(357, 334);
			this.Browser.TabIndex = 0;
			this.Browser.WebBrowserShortcutsEnabled = false;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(717, 389);
			base.Controls.Add(this.Splitter);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SkillChallengeSelectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Select a Skill Challenge";
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			this.BrowserPanel.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void TileList_DoubleClick(object sender, EventArgs e)
		{
			if (this.TerrainPower != null)
			{
				base.DialogResult = System.Windows.Forms.DialogResult.OK;
				base.Close();
			}
		}
	}
}