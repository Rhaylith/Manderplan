using Masterplan;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class ArtifactBuilderForm : Form
	{
		private Panel BtnPnl;

		private Button CancelBtn;

		private Button OKBtn;

		private WebBrowser StatBlockBrowser;

		private ToolStrip Toolbar;

		private ToolStripDropDownButton FileMenu;

		private ToolStripMenuItem FileImport;

		private ToolStripMenuItem FileExport;

		private Masterplan.Data.Artifact fArtifact;

		public Masterplan.Data.Artifact Artifact
		{
			get
			{
				return this.fArtifact;
			}
		}

		public ArtifactBuilderForm(Masterplan.Data.Artifact artifact)
		{
			this.InitializeComponent();
			this.fArtifact = artifact.Copy();
			this.update_statblock();
		}

		private void Browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.Scheme == "build")
			{
				if (e.Url.LocalPath == "profile")
				{
					e.Cancel = true;
					ArtifactProfileForm artifactProfileForm = new ArtifactProfileForm(this.fArtifact);
					if (artifactProfileForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fArtifact.Name = artifactProfileForm.Artifact.Name;
						this.fArtifact.Tier = artifactProfileForm.Artifact.Tier;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "description")
				{
					e.Cancel = true;
					DetailsForm detailsForm = new DetailsForm(this.fArtifact.Description, "Description", null);
					if (detailsForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fArtifact.Description = detailsForm.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "details")
				{
					e.Cancel = true;
					DetailsForm detailsForm1 = new DetailsForm(this.fArtifact.Details, "Details", null);
					if (detailsForm1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fArtifact.Details = detailsForm1.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "goals")
				{
					e.Cancel = true;
					DetailsForm detailsForm2 = new DetailsForm(this.fArtifact.Goals, "Goals", null);
					if (detailsForm2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fArtifact.Goals = detailsForm2.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "rp")
				{
					e.Cancel = true;
					DetailsForm detailsForm3 = new DetailsForm(this.fArtifact.RoleplayingTips, "Roleplaying", null);
					if (detailsForm3.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fArtifact.RoleplayingTips = detailsForm3.Details;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "section")
			{
				if (e.Url.LocalPath == "new")
				{
					e.Cancel = true;
					MagicItemSectionForm magicItemSectionForm = new MagicItemSectionForm(new MagicItemSection());
					if (magicItemSectionForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fArtifact.Sections.Add(magicItemSectionForm.Section);
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath.Contains(",new"))
				{
					e.Cancel = true;
					try
					{
						string str = e.Url.LocalPath.Substring(0, e.Url.LocalPath.IndexOf(","));
						int num = int.Parse(str);
						ArtifactConcordance item = this.fArtifact.ConcordanceLevels[num];
						MagicItemSectionForm magicItemSectionForm1 = new MagicItemSectionForm(new MagicItemSection());
						if (magicItemSectionForm1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						{
							item.Sections.Add(magicItemSectionForm1.Section);
							this.update_statblock();
						}
					}
					catch
					{
					}
				}
			}
			if (e.Url.Scheme == "sectionedit")
			{
				if (!e.Url.LocalPath.Contains(","))
				{
					e.Cancel = true;
					try
					{
						int section = int.Parse(e.Url.LocalPath);
						MagicItemSectionForm magicItemSectionForm2 = new MagicItemSectionForm(this.fArtifact.Sections[section]);
						if (magicItemSectionForm2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						{
							this.fArtifact.Sections[section] = magicItemSectionForm2.Section;
							this.update_statblock();
						}
					}
					catch
					{
					}
				}
				else
				{
					e.Cancel = true;
					int num1 = e.Url.LocalPath.IndexOf(",");
					string str1 = e.Url.LocalPath.Substring(0, num1);
					string str2 = e.Url.LocalPath.Substring(num1);
					try
					{
						int num2 = int.Parse(str1);
						int section1 = int.Parse(str2);
						ArtifactConcordance artifactConcordance = this.fArtifact.ConcordanceLevels[num2];
						MagicItemSectionForm magicItemSectionForm3 = new MagicItemSectionForm(artifactConcordance.Sections[section1]);
						if (magicItemSectionForm3.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						{
							artifactConcordance.Sections[section1] = magicItemSectionForm3.Section;
							this.update_statblock();
						}
					}
					catch
					{
					}
				}
			}
			if (e.Url.Scheme == "sectionremove")
			{
				if (!e.Url.LocalPath.Contains(","))
				{
					e.Cancel = true;
					try
					{
						int num3 = int.Parse(e.Url.LocalPath);
						this.fArtifact.Sections.RemoveAt(num3);
						this.update_statblock();
					}
					catch
					{
					}
				}
				else
				{
					e.Cancel = true;
					int num4 = e.Url.LocalPath.IndexOf(",");
					string str3 = e.Url.LocalPath.Substring(0, num4);
					string str4 = e.Url.LocalPath.Substring(num4);
					try
					{
						int num5 = int.Parse(str3);
						int num6 = int.Parse(str4);
						this.fArtifact.ConcordanceLevels[num5].Sections.RemoveAt(num6);
						this.update_statblock();
					}
					catch
					{
					}
				}
			}
			if (e.Url.Scheme == "rule")
			{
				e.Cancel = true;
				if (e.Url.LocalPath == "new")
				{
					ArtifactConcordanceForm artifactConcordanceForm = new ArtifactConcordanceForm(new Pair<string, string>("", ""));
					if (artifactConcordanceForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fArtifact.ConcordanceRules.Add(artifactConcordanceForm.Concordance);
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "ruleedit")
			{
				e.Cancel = true;
				try
				{
					int concordance = int.Parse(e.Url.LocalPath);
					ArtifactConcordanceForm artifactConcordanceForm1 = new ArtifactConcordanceForm(this.fArtifact.ConcordanceRules[concordance]);
					if (artifactConcordanceForm1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fArtifact.ConcordanceRules[concordance] = artifactConcordanceForm1.Concordance;
						this.update_statblock();
					}
				}
				catch
				{
				}
			}
			if (e.Url.Scheme == "ruleremove")
			{
				e.Cancel = true;
				try
				{
					int num7 = int.Parse(e.Url.LocalPath);
					this.fArtifact.ConcordanceRules.RemoveAt(num7);
					this.update_statblock();
				}
				catch
				{
				}
			}
			if (e.Url.Scheme == "quote")
			{
				e.Cancel = true;
				try
				{
					int num8 = int.Parse(e.Url.LocalPath);
					ArtifactConcordance details = this.fArtifact.ConcordanceLevels[num8];
					DetailsForm detailsForm4 = new DetailsForm(details.Quote, "Concordance Quote", null);
					if (detailsForm4.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						details.Quote = detailsForm4.Details;
						this.update_statblock();
					}
				}
				catch
				{
				}
			}
			if (e.Url.Scheme == "desc")
			{
				e.Cancel = true;
				try
				{
					int num9 = int.Parse(e.Url.LocalPath);
					ArtifactConcordance item1 = this.fArtifact.ConcordanceLevels[num9];
					DetailsForm detailsForm5 = new DetailsForm(item1.Description, "Concordance Description", null);
					if (detailsForm5.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						item1.Description = detailsForm5.Details;
						this.update_statblock();
					}
				}
				catch
				{
				}
			}
		}

        private void FileExport_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog()
			{
				Title = "Export Artifact",
				FileName = this.fArtifact.Name,
				Filter = Program.ArtifactFilter
			};
			if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK && !Serialisation<Masterplan.Data.Artifact>.Save(saveFileDialog.FileName, this.fArtifact, SerialisationMode.Binary))
			{
				MessageBox.Show("The artifact could not be exported.", "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void FileImport_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Importing an artifact file will clear any changes you have made to the item shown.", "Masterplan", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.Cancel)
			{
				return;
			}
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Title = "Import Artifact",
				Filter = Program.ArtifactFilter
			};
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Masterplan.Data.Artifact artifact = Serialisation<Masterplan.Data.Artifact>.Load(openFileDialog.FileName, SerialisationMode.Binary);
				if (artifact != null)
				{
					this.fArtifact = artifact;
					this.update_statblock();
					return;
				}
				MessageBox.Show("The artifact could not be imported.", "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ArtifactBuilderForm));
			this.BtnPnl = new Panel();
			this.CancelBtn = new Button();
			this.OKBtn = new Button();
			this.StatBlockBrowser = new WebBrowser();
			this.Toolbar = new ToolStrip();
			this.FileMenu = new ToolStripDropDownButton();
			this.FileImport = new ToolStripMenuItem();
			this.FileExport = new ToolStripMenuItem();
			this.BtnPnl.SuspendLayout();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.BtnPnl.Controls.Add(this.CancelBtn);
			this.BtnPnl.Controls.Add(this.OKBtn);
			this.BtnPnl.Dock = DockStyle.Bottom;
			this.BtnPnl.Location = new Point(0, 443);
			this.BtnPnl.Name = "BtnPnl";
			this.BtnPnl.Size = new System.Drawing.Size(384, 35);
			this.BtnPnl.TabIndex = 2;
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(297, 6);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 1;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(216, 6);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 0;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.StatBlockBrowser.AllowWebBrowserDrop = false;
			this.StatBlockBrowser.Dock = DockStyle.Fill;
			this.StatBlockBrowser.IsWebBrowserContextMenuEnabled = false;
			this.StatBlockBrowser.Location = new Point(0, 25);
			this.StatBlockBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.StatBlockBrowser.Name = "StatBlockBrowser";
			this.StatBlockBrowser.ScriptErrorsSuppressed = true;
			this.StatBlockBrowser.Size = new System.Drawing.Size(384, 418);
			this.StatBlockBrowser.TabIndex = 2;
			this.StatBlockBrowser.WebBrowserShortcutsEnabled = false;
			this.StatBlockBrowser.Navigating += new WebBrowserNavigatingEventHandler(this.Browser_Navigating);
			this.Toolbar.Items.AddRange(new ToolStripItem[] { this.FileMenu });
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(384, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.FileMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems = this.FileMenu.DropDownItems;
			ToolStripItem[] fileImport = new ToolStripItem[] { this.FileImport, this.FileExport };
			dropDownItems.AddRange(fileImport);
			this.FileMenu.Image = (Image)componentResourceManager.GetObject("FileMenu.Image");
			this.FileMenu.ImageTransparentColor = Color.Magenta;
			this.FileMenu.Name = "FileMenu";
			this.FileMenu.Size = new System.Drawing.Size(38, 22);
			this.FileMenu.Text = "File";
			this.FileImport.Name = "FileImport";
			this.FileImport.Size = new System.Drawing.Size(119, 22);
			this.FileImport.Text = "Import...";
			this.FileImport.Click += new EventHandler(this.FileImport_Click);
			this.FileExport.Name = "FileExport";
			this.FileExport.Size = new System.Drawing.Size(119, 22);
			this.FileExport.Text = "Export...";
			this.FileExport.Click += new EventHandler(this.FileExport_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(384, 478);
			base.Controls.Add(this.StatBlockBrowser);
			base.Controls.Add(this.BtnPnl);
			base.Controls.Add(this.Toolbar);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ArtifactBuilderForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Artifact Builder";
			this.BtnPnl.ResumeLayout(false);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void update_statblock()
		{
			this.StatBlockBrowser.DocumentText = HTML.Artifact(this.fArtifact, DisplaySize.Small, true, true);
		}
	}
}