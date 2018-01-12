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
	internal class CreatureTemplateSelectForm : Form
	{
		private Button OKBtn;

		private ListView CreatureList;

		private Button CancelBtn;

		private ColumnHeader NameHdr;

		private ColumnHeader InfoHdr;

		private SplitContainer Splitter;

		private Panel BrowserPanel;

		private WebBrowser Browser;

		private Panel NamePanel;

		private TextBox NameBox;

		private Label NameLbl;

		public CreatureTemplate Template
		{
			get
			{
				if (this.CreatureList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.CreatureList.SelectedItems[0].Tag as CreatureTemplate;
			}
		}

		public CreatureTemplateSelectForm()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.update_list();
			this.Browser.DocumentText = "";
			this.CreatureList_SelectedIndexChanged(null, null);
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = this.Template != null;
		}

		private void CreatureList_SelectedIndexChanged(object sender, EventArgs e)
		{
			string str = "";
			if (this.Template != null)
			{
				str = HTML.CreatureTemplate(this.Template, DisplaySize.Small, false);
			}
			else
			{
				List<string> strs = new List<string>();
				strs.AddRange(HTML.GetHead("", "", DisplaySize.Small));
				strs.Add("<BODY>");
				strs.Add("<P class=instruction>");
				strs.Add("(select a template from the list to see its details here)");
				strs.Add("</P>");
				strs.Add("</BODY>");
				strs.Add("</HTML>");
				str = HTML.Concatenate(strs);
			}
			this.Browser.Document.OpenNew(true);
			this.Browser.Document.Write(str);
		}

		private void InitializeComponent()
		{
			ListViewGroup listViewGroup = new ListViewGroup("Functional Templates", HorizontalAlignment.Left);
			ListViewGroup listViewGroup1 = new ListViewGroup("Class Templates", HorizontalAlignment.Left);
			this.OKBtn = new Button();
			this.CreatureList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.InfoHdr = new ColumnHeader();
			this.CancelBtn = new Button();
			this.Splitter = new SplitContainer();
			this.NamePanel = new Panel();
			this.NameBox = new TextBox();
			this.NameLbl = new Label();
			this.BrowserPanel = new Panel();
			this.Browser = new WebBrowser();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.NamePanel.SuspendLayout();
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
			ColumnHeader[] nameHdr = new ColumnHeader[] { this.NameHdr, this.InfoHdr };
			this.CreatureList.Columns.AddRange(nameHdr);
			this.CreatureList.Dock = DockStyle.Fill;
			this.CreatureList.FullRowSelect = true;
			listViewGroup.Header = "Functional Templates";
			listViewGroup.Name = "listViewGroup1";
			listViewGroup1.Header = "Class Templates";
			listViewGroup1.Name = "listViewGroup2";
			this.CreatureList.Groups.AddRange(new ListViewGroup[] { listViewGroup, listViewGroup1 });
			this.CreatureList.HideSelection = false;
			this.CreatureList.Location = new Point(0, 27);
			this.CreatureList.MultiSelect = false;
			this.CreatureList.Name = "CreatureList";
			this.CreatureList.Size = new System.Drawing.Size(330, 309);
			this.CreatureList.TabIndex = 0;
			this.CreatureList.UseCompatibleStateImageBehavior = false;
			this.CreatureList.View = View.Details;
			this.CreatureList.SelectedIndexChanged += new EventHandler(this.CreatureList_SelectedIndexChanged);
			this.CreatureList.DoubleClick += new EventHandler(this.TileList_DoubleClick);
			this.NameHdr.Text = "Template";
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
			this.Splitter.Panel1.Controls.Add(this.CreatureList);
			this.Splitter.Panel1.Controls.Add(this.NamePanel);
			this.Splitter.Panel2.Controls.Add(this.BrowserPanel);
			this.Splitter.Size = new System.Drawing.Size(693, 336);
			this.Splitter.SplitterDistance = 330;
			this.Splitter.TabIndex = 0;
			this.NamePanel.Controls.Add(this.NameBox);
			this.NamePanel.Controls.Add(this.NameLbl);
			this.NamePanel.Dock = DockStyle.Top;
			this.NamePanel.Location = new Point(0, 0);
			this.NamePanel.Name = "NamePanel";
			this.NamePanel.Size = new System.Drawing.Size(330, 27);
			this.NamePanel.TabIndex = 0;
			this.NameBox.Location = new Point(47, 3);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(280, 20);
			this.NameBox.TabIndex = 1;
			this.NameBox.TextChanged += new EventHandler(this.NameBox_TextChanged);
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(3, 6);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new System.Drawing.Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
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
			base.Name = "CreatureTemplateSelectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Select a Template";
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			this.NamePanel.ResumeLayout(false);
			this.NamePanel.PerformLayout();
			this.BrowserPanel.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private bool match(CreatureTemplate ct, string query)
		{
			string[] strArrays = query.ToLower().Split(new char[0]);
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				if (!this.match_token(ct, strArrays[i]))
				{
					return false;
				}
			}
			return true;
		}

		private bool match_token(CreatureTemplate ct, string token)
		{
			if (ct.Name.ToLower().Contains(token))
			{
				return true;
			}
			if (ct.Info.ToLower().Contains(token))
			{
				return true;
			}
			return false;
		}

		private void NameBox_TextChanged(object sender, EventArgs e)
		{
			this.update_list();
			this.CreatureList_SelectedIndexChanged(null, null);
		}

		private void TileList_DoubleClick(object sender, EventArgs e)
		{
			if (this.Template != null)
			{
				base.DialogResult = System.Windows.Forms.DialogResult.OK;
				base.Close();
			}
		}

		private void update_list()
		{
			this.CreatureList.Items.Clear();
			foreach (CreatureTemplate template in Session.Templates)
			{
				if (!this.match(template, this.NameBox.Text))
				{
					continue;
				}
				ListViewItem item = this.CreatureList.Items.Add(template.Name);
				item.SubItems.Add(template.Info);
				item.Tag = template;
				switch (template.Type)
				{
					case CreatureTemplateType.Functional:
					{
						item.Group = this.CreatureList.Groups[0];
						continue;
					}
					case CreatureTemplateType.Class:
					{
						item.Group = this.CreatureList.Groups[1];
						continue;
					}
					default:
					{
						continue;
					}
				}
			}
		}
	}
}