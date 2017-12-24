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
	internal class CreatureTemplateBuilderForm : Form
	{
		private IContainer components;

		private ToolStrip Toolbar;

		private Panel BtnPnl;

		private Button CancelBtn;

		private Button OKBtn;

		private WebBrowser StatBlockBrowser;

		private ToolStripDropDownButton OptionsMenu;

		private ToolStripMenuItem OptionsVariant;

		private ToolStripDropDownButton FileMenu;

		private ToolStripMenuItem FileExport;

		private CreatureTemplate fTemplate;

		public CreatureTemplate Template
		{
			get
			{
				return this.fTemplate;
			}
		}

		public CreatureTemplateBuilderForm(CreatureTemplate template)
		{
			this.InitializeComponent();
			this.fTemplate = template.Copy();
			this.update_statblock();
		}

		private void add_power(CreaturePower power)
		{
			this.fTemplate.CreaturePowers.Add(power);
			this.update_statblock();
		}

		private void Browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.Scheme == "build")
			{
				if (e.Url.LocalPath == "profile")
				{
					e.Cancel = true;
					CreatureTemplateProfileForm creatureTemplateProfileForm = new CreatureTemplateProfileForm(this.fTemplate);
					if (creatureTemplateProfileForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fTemplate.Name = creatureTemplateProfileForm.Template.Name;
						this.fTemplate.Type = creatureTemplateProfileForm.Template.Type;
						this.fTemplate.Role = creatureTemplateProfileForm.Template.Role;
						this.fTemplate.Leader = creatureTemplateProfileForm.Template.Leader;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "combat")
				{
					e.Cancel = true;
					CreatureTemplateStatsForm creatureTemplateStatsForm = new CreatureTemplateStatsForm(this.fTemplate);
					if (creatureTemplateStatsForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fTemplate.HP = creatureTemplateStatsForm.Template.HP;
						this.fTemplate.Initiative = creatureTemplateStatsForm.Template.Initiative;
						this.fTemplate.AC = creatureTemplateStatsForm.Template.AC;
						this.fTemplate.Fortitude = creatureTemplateStatsForm.Template.Fortitude;
						this.fTemplate.Reflex = creatureTemplateStatsForm.Template.Reflex;
						this.fTemplate.Will = creatureTemplateStatsForm.Template.Will;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "damage")
				{
					e.Cancel = true;
					if ((new DamageModListForm(this.fTemplate)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "senses")
				{
					e.Cancel = true;
					DetailsForm detailsForm = new DetailsForm(this.fTemplate.Senses, "Senses", "");
					if (detailsForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fTemplate.Senses = detailsForm.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "movement")
				{
					e.Cancel = true;
					DetailsForm detailsForm1 = new DetailsForm(this.fTemplate.Movement, "Movement", "");
					if (detailsForm1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fTemplate.Movement = detailsForm1.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "tactics")
				{
					e.Cancel = true;
					DetailsForm detailsForm2 = new DetailsForm(this.fTemplate.Tactics, "Tactics", "");
					if (detailsForm2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fTemplate.Tactics = detailsForm2.Details;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "power")
			{
				if (e.Url.LocalPath == "addpower")
				{
					e.Cancel = true;
					CreaturePower creaturePower = new CreaturePower()
					{
						Name = "New Power",
						Action = new PowerAction()
					};
					bool type = this.fTemplate.Type == CreatureTemplateType.Functional;
					PowerBuilderForm powerBuilderForm = new PowerBuilderForm(creaturePower, null, type);
					if (powerBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fTemplate.CreaturePowers.Add(powerBuilderForm.Power);
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "addtrait")
				{
					e.Cancel = true;
					CreaturePower creaturePower1 = new CreaturePower()
					{
						Name = "New Trait",
						Action = null
					};
					bool flag = this.fTemplate.Type == CreatureTemplateType.Functional;
					PowerBuilderForm powerBuilderForm1 = new PowerBuilderForm(creaturePower1, null, flag);
					if (powerBuilderForm1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fTemplate.CreaturePowers.Add(powerBuilderForm1.Power);
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "addaura")
				{
					e.Cancel = true;
					Aura aura = new Aura()
					{
						Name = "New Aura",
						Details = "1"
					};
					AuraForm auraForm = new AuraForm(aura);
					if (auraForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fTemplate.Auras.Add(auraForm.Aura);
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "regenedit")
				{
					e.Cancel = true;
					Regeneration regeneration = this.fTemplate.Regeneration ?? new Regeneration(5, "");
					RegenerationForm regenerationForm = new RegenerationForm(regeneration);
					if (regenerationForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fTemplate.Regeneration = regenerationForm.Regeneration;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "regenremove")
				{
					e.Cancel = true;
					this.fTemplate.Regeneration = null;
					this.update_statblock();
				}
			}
			if (e.Url.Scheme == "poweredit")
			{
				CreaturePower creaturePower2 = this.find_power(new Guid(e.Url.LocalPath));
				if (creaturePower2 != null)
				{
					e.Cancel = true;
					int power = this.fTemplate.CreaturePowers.IndexOf(creaturePower2);
					bool type1 = this.fTemplate.Type == CreatureTemplateType.Functional;
					PowerBuilderForm powerBuilderForm2 = new PowerBuilderForm(creaturePower2, null, type1);
					if (powerBuilderForm2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fTemplate.CreaturePowers[power] = powerBuilderForm2.Power;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "powerremove")
			{
				CreaturePower creaturePower3 = this.find_power(new Guid(e.Url.LocalPath));
				if (creaturePower3 != null)
				{
					e.Cancel = true;
					this.fTemplate.CreaturePowers.Remove(creaturePower3);
					this.update_statblock();
				}
			}
			if (e.Url.Scheme == "auraedit")
			{
				Aura aura1 = this.find_aura(new Guid(e.Url.LocalPath));
				if (aura1 != null)
				{
					e.Cancel = true;
					int num = this.fTemplate.Auras.IndexOf(aura1);
					AuraForm auraForm1 = new AuraForm(aura1);
					if (auraForm1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fTemplate.Auras[num] = auraForm1.Aura;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "auraremove")
			{
				Aura aura2 = this.find_aura(new Guid(e.Url.LocalPath));
				if (aura2 != null)
				{
					e.Cancel = true;
					this.fTemplate.Auras.Remove(aura2);
					this.update_statblock();
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void FileExport_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog()
			{
				Title = "Export Creature Template",
				FileName = this.fTemplate.Name,
				Filter = Program.CreatureTemplateFilter
			};
			if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK && !Serialisation<CreatureTemplate>.Save(saveFileDialog.FileName, this.fTemplate, SerialisationMode.Binary))
			{
				MessageBox.Show("The creature template could not be exported.", "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private Aura find_aura(Guid id)
		{
			Aura aura;
			List<Aura>.Enumerator enumerator = this.fTemplate.Auras.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Aura current = enumerator.Current;
					if (current.ID != id)
					{
						continue;
					}
					aura = current;
					return aura;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return aura;
		}

		private CreaturePower find_power(Guid id)
		{
			CreaturePower creaturePower;
			List<CreaturePower>.Enumerator enumerator = this.fTemplate.CreaturePowers.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					CreaturePower current = enumerator.Current;
					if (current.ID != id)
					{
						continue;
					}
					creaturePower = current;
					return creaturePower;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return creaturePower;
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(CreatureTemplateBuilderForm));
			this.Toolbar = new ToolStrip();
			this.FileMenu = new ToolStripDropDownButton();
			this.FileExport = new ToolStripMenuItem();
			this.OptionsMenu = new ToolStripDropDownButton();
			this.OptionsVariant = new ToolStripMenuItem();
			this.BtnPnl = new Panel();
			this.CancelBtn = new Button();
			this.OKBtn = new Button();
			this.StatBlockBrowser = new WebBrowser();
			this.Toolbar.SuspendLayout();
			this.BtnPnl.SuspendLayout();
			base.SuspendLayout();
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] fileMenu = new ToolStripItem[] { this.FileMenu, this.OptionsMenu };
			items.AddRange(fileMenu);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(384, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.FileMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.FileMenu.DropDownItems.AddRange(new ToolStripItem[] { this.FileExport });
			this.FileMenu.Image = (Image)componentResourceManager.GetObject("FileMenu.Image");
			this.FileMenu.ImageTransparentColor = Color.Magenta;
			this.FileMenu.Name = "FileMenu";
			this.FileMenu.Size = new System.Drawing.Size(38, 22);
			this.FileMenu.Text = "File";
			this.FileExport.Name = "FileExport";
			this.FileExport.Size = new System.Drawing.Size(152, 22);
			this.FileExport.Text = "Export...";
			this.FileExport.Click += new EventHandler(this.FileExport_Click);
			this.OptionsMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.OptionsMenu.DropDownItems.AddRange(new ToolStripItem[] { this.OptionsVariant });
			this.OptionsMenu.Image = (Image)componentResourceManager.GetObject("OptionsMenu.Image");
			this.OptionsMenu.ImageTransparentColor = Color.Magenta;
			this.OptionsMenu.Name = "OptionsMenu";
			this.OptionsMenu.Size = new System.Drawing.Size(62, 22);
			this.OptionsMenu.Text = "Options";
			this.OptionsVariant.Name = "OptionsVariant";
			this.OptionsVariant.Size = new System.Drawing.Size(218, 22);
			this.OptionsVariant.Text = "Copy an Existing Creature...";
			this.OptionsVariant.Click += new EventHandler(this.OptionsVariant_Click);
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
			base.Name = "CreatureTemplateBuilderForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Creature Template Builder";
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.BtnPnl.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OptionsVariant_Click(object sender, EventArgs e)
		{
		}

		private void update_statblock()
		{
			this.StatBlockBrowser.DocumentText = HTML.CreatureTemplate(this.fTemplate, DisplaySize.Small, true);
		}
	}
}