using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class TokenLinkListForm : Form
	{
		private List<TokenLink> fLinks;

		private ListView EffectList;

		private ColumnHeader LinkHdr;

		private ToolStrip Toolbar;

		private ToolStripButton RemoveBtn;

		private ToolStripButton EditBtn;

		private ColumnHeader TokenHdr;

		public TokenLink SelectedLink
		{
			get
			{
				if (this.EffectList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.EffectList.SelectedItems[0].Tag as TokenLink;
			}
		}

		public TokenLinkListForm(List<TokenLink> links)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fLinks = links;
			this.update_list();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveBtn.Enabled = this.SelectedLink != null;
			this.EditBtn.Enabled = this.SelectedLink != null;
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLink != null)
			{
				int link = this.fLinks.IndexOf(this.SelectedLink);
				TokenLinkForm tokenLinkForm = new TokenLinkForm(this.SelectedLink);
				if (tokenLinkForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fLinks[link] = tokenLinkForm.Link;
					this.update_list();
				}
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(TokenLinkListForm));
			this.Toolbar = new ToolStrip();
			this.RemoveBtn = new ToolStripButton();
			this.EditBtn = new ToolStripButton();
			this.EffectList = new ListView();
			this.LinkHdr = new ColumnHeader();
			this.TokenHdr = new ColumnHeader();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] removeBtn = new ToolStripItem[] { this.RemoveBtn, this.EditBtn };
			items.AddRange(removeBtn);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(429, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
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
			ListView.ColumnHeaderCollection columns = this.EffectList.Columns;
			ColumnHeader[] tokenHdr = new ColumnHeader[] { this.TokenHdr, this.LinkHdr };
			columns.AddRange(tokenHdr);
			this.EffectList.Dock = DockStyle.Fill;
			this.EffectList.FullRowSelect = true;
			this.EffectList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.EffectList.HideSelection = false;
			this.EffectList.Location = new Point(0, 25);
			this.EffectList.MultiSelect = false;
			this.EffectList.Name = "EffectList";
			this.EffectList.Size = new System.Drawing.Size(429, 172);
			this.EffectList.TabIndex = 1;
			this.EffectList.UseCompatibleStateImageBehavior = false;
			this.EffectList.View = View.Details;
			this.EffectList.DoubleClick += new EventHandler(this.EditBtn_Click);
			this.LinkHdr.Text = "Link";
			this.LinkHdr.Width = 150;
			this.TokenHdr.Text = "Tokens";
			this.TokenHdr.Width = 250;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(429, 197);
			base.Controls.Add(this.EffectList);
			base.Controls.Add(this.Toolbar);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TokenLinkListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Token Links";
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLink != null)
			{
				this.fLinks.Remove(this.SelectedLink);
				this.update_list();
			}
		}

		private void update_list()
		{
			this.EffectList.Items.Clear();
			foreach (TokenLink fLink in this.fLinks)
			{
				string str = "";
				foreach (IToken token in fLink.Tokens)
				{
					string displayName = "";
					if (token is CreatureToken)
					{
						displayName = (token as CreatureToken).Data.DisplayName;
					}
					if (token is Hero)
					{
						displayName = (token as Hero).Name;
					}
					if (token is CustomToken)
					{
						displayName = (token as CustomToken).Name;
					}
					if (displayName == "")
					{
						displayName = "(unknown map token)";
					}
					if (str != "")
					{
						str = string.Concat(str, ", ");
					}
					str = string.Concat(str, displayName);
				}
				ListViewItem listViewItem = this.EffectList.Items.Add(str);
				listViewItem.SubItems.Add(fLink.Text);
				listViewItem.Tag = fLink;
			}
		}
	}
}