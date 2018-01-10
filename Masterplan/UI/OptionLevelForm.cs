using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class OptionLevelForm : Form
	{
		private LevelData fLevel;

		private TabControl Pages;

		private Button OKBtn;

		private Button CancelBtn;

		private TabPage FeaturesPage;

		private TabPage PowersPage;

		private ListView FeatureList;

		private ColumnHeader FeatureHdr;

		private ToolStrip FeatureToolbar;

		private ToolStripButton FeatureAddBtn;

		private ToolStripButton FeatureRemoveBtn;

		private ToolStripButton FeatureEditBtn;

		private ListView PowerList;

		private ColumnHeader PowerHdr;

		private ToolStrip PowerToolbar;

		private ToolStripButton PowerAddBtn;

		private ToolStripButton PowerRemoveBtn;

		private ToolStripButton PowerEditBtn;

		public LevelData Level
		{
			get
			{
				return this.fLevel;
			}
		}

		public Feature SelectedFeature
		{
			get
			{
				if (this.FeatureList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.FeatureList.SelectedItems[0].Tag as Feature;
			}
		}

		public PlayerPower SelectedPower
		{
			get
			{
				if (this.PowerList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.PowerList.SelectedItems[0].Tag as PlayerPower;
			}
		}

		public OptionLevelForm(LevelData level, bool show_features)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fLevel = level.Copy();
			this.Text = string.Concat("Level ", this.fLevel.Level);
			if (!show_features)
			{
				this.Pages.TabPages.Remove(this.FeaturesPage);
			}
			this.update_features();
			this.update_powers();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.FeatureRemoveBtn.Enabled = this.SelectedFeature != null;
			this.FeatureEditBtn.Enabled = this.SelectedFeature != null;
			this.PowerRemoveBtn.Enabled = this.SelectedPower != null;
			this.PowerEditBtn.Enabled = this.SelectedPower != null;
		}

		private void FeatureAddBtn_Click(object sender, EventArgs e)
		{
			OptionFeatureForm optionFeatureForm = new OptionFeatureForm(new Feature()
			{
				Name = "New Feature"
			});
			if (optionFeatureForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fLevel.Features.Add(optionFeatureForm.Feature);
				this.update_features();
			}
		}

		private void FeatureEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedFeature != null)
			{
				int feature = this.fLevel.Features.IndexOf(this.SelectedFeature);
				OptionFeatureForm optionFeatureForm = new OptionFeatureForm(this.SelectedFeature);
				if (optionFeatureForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fLevel.Features[feature] = optionFeatureForm.Feature;
					this.update_features();
				}
			}
		}

		private void FeatureRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedFeature != null)
			{
				this.fLevel.Features.Remove(this.SelectedFeature);
				this.update_features();
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(OptionLevelForm));
			this.Pages = new TabControl();
			this.FeaturesPage = new TabPage();
			this.FeatureList = new ListView();
			this.FeatureHdr = new ColumnHeader();
			this.FeatureToolbar = new ToolStrip();
			this.FeatureAddBtn = new ToolStripButton();
			this.FeatureRemoveBtn = new ToolStripButton();
			this.FeatureEditBtn = new ToolStripButton();
			this.PowersPage = new TabPage();
			this.PowerList = new ListView();
			this.PowerHdr = new ColumnHeader();
			this.PowerToolbar = new ToolStrip();
			this.PowerAddBtn = new ToolStripButton();
			this.PowerRemoveBtn = new ToolStripButton();
			this.PowerEditBtn = new ToolStripButton();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.Pages.SuspendLayout();
			this.FeaturesPage.SuspendLayout();
			this.FeatureToolbar.SuspendLayout();
			this.PowersPage.SuspendLayout();
			this.PowerToolbar.SuspendLayout();
			base.SuspendLayout();
			this.Pages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Pages.Controls.Add(this.FeaturesPage);
			this.Pages.Controls.Add(this.PowersPage);
			this.Pages.Location = new Point(12, 12);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new System.Drawing.Size(349, 200);
			this.Pages.TabIndex = 2;
			this.FeaturesPage.Controls.Add(this.FeatureList);
			this.FeaturesPage.Controls.Add(this.FeatureToolbar);
			this.FeaturesPage.Location = new Point(4, 22);
			this.FeaturesPage.Name = "FeaturesPage";
			this.FeaturesPage.Padding = new System.Windows.Forms.Padding(3);
			this.FeaturesPage.Size = new System.Drawing.Size(341, 174);
			this.FeaturesPage.TabIndex = 2;
			this.FeaturesPage.Text = "Features";
			this.FeaturesPage.UseVisualStyleBackColor = true;
			this.FeatureList.Columns.AddRange(new ColumnHeader[] { this.FeatureHdr });
			this.FeatureList.Dock = DockStyle.Fill;
			this.FeatureList.FullRowSelect = true;
			this.FeatureList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.FeatureList.HideSelection = false;
			this.FeatureList.Location = new Point(3, 28);
			this.FeatureList.MultiSelect = false;
			this.FeatureList.Name = "FeatureList";
			this.FeatureList.Size = new System.Drawing.Size(335, 143);
			this.FeatureList.TabIndex = 1;
			this.FeatureList.UseCompatibleStateImageBehavior = false;
			this.FeatureList.View = View.Details;
			this.FeatureList.DoubleClick += new EventHandler(this.FeatureEditBtn_Click);
			this.FeatureHdr.Text = "Feature";
			this.FeatureHdr.Width = 300;
			ToolStripItemCollection items = this.FeatureToolbar.Items;
			ToolStripItem[] featureAddBtn = new ToolStripItem[] { this.FeatureAddBtn, this.FeatureRemoveBtn, this.FeatureEditBtn };
			items.AddRange(featureAddBtn);
			this.FeatureToolbar.Location = new Point(3, 3);
			this.FeatureToolbar.Name = "FeatureToolbar";
			this.FeatureToolbar.Size = new System.Drawing.Size(335, 25);
			this.FeatureToolbar.TabIndex = 0;
			this.FeatureToolbar.Text = "toolStrip1";
			this.FeatureAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.FeatureAddBtn.Image = (Image)componentResourceManager.GetObject("FeatureAddBtn.Image");
			this.FeatureAddBtn.ImageTransparentColor = Color.Magenta;
			this.FeatureAddBtn.Name = "FeatureAddBtn";
			this.FeatureAddBtn.Size = new System.Drawing.Size(33, 22);
			this.FeatureAddBtn.Text = "Add";
			this.FeatureAddBtn.Click += new EventHandler(this.FeatureAddBtn_Click);
			this.FeatureRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.FeatureRemoveBtn.Image = (Image)componentResourceManager.GetObject("FeatureRemoveBtn.Image");
			this.FeatureRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.FeatureRemoveBtn.Name = "FeatureRemoveBtn";
			this.FeatureRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.FeatureRemoveBtn.Text = "Remove";
			this.FeatureRemoveBtn.Click += new EventHandler(this.FeatureRemoveBtn_Click);
			this.FeatureEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.FeatureEditBtn.Image = (Image)componentResourceManager.GetObject("FeatureEditBtn.Image");
			this.FeatureEditBtn.ImageTransparentColor = Color.Magenta;
			this.FeatureEditBtn.Name = "FeatureEditBtn";
			this.FeatureEditBtn.Size = new System.Drawing.Size(31, 22);
			this.FeatureEditBtn.Text = "Edit";
			this.FeatureEditBtn.Click += new EventHandler(this.FeatureEditBtn_Click);
			this.PowersPage.Controls.Add(this.PowerList);
			this.PowersPage.Controls.Add(this.PowerToolbar);
			this.PowersPage.Location = new Point(4, 22);
			this.PowersPage.Name = "PowersPage";
			this.PowersPage.Padding = new System.Windows.Forms.Padding(3);
			this.PowersPage.Size = new System.Drawing.Size(341, 222);
			this.PowersPage.TabIndex = 3;
			this.PowersPage.Text = "Powers";
			this.PowersPage.UseVisualStyleBackColor = true;
			this.PowerList.Columns.AddRange(new ColumnHeader[] { this.PowerHdr });
			this.PowerList.Dock = DockStyle.Fill;
			this.PowerList.FullRowSelect = true;
			this.PowerList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.PowerList.HideSelection = false;
			this.PowerList.Location = new Point(3, 28);
			this.PowerList.MultiSelect = false;
			this.PowerList.Name = "PowerList";
			this.PowerList.Size = new System.Drawing.Size(335, 191);
			this.PowerList.TabIndex = 2;
			this.PowerList.UseCompatibleStateImageBehavior = false;
			this.PowerList.View = View.Details;
			this.PowerList.DoubleClick += new EventHandler(this.PowerEditBtn_Click);
			this.PowerHdr.Text = "Feature";
			this.PowerHdr.Width = 300;
			ToolStripItemCollection toolStripItemCollections = this.PowerToolbar.Items;
			ToolStripItem[] powerAddBtn = new ToolStripItem[] { this.PowerAddBtn, this.PowerRemoveBtn, this.PowerEditBtn };
			toolStripItemCollections.AddRange(powerAddBtn);
			this.PowerToolbar.Location = new Point(3, 3);
			this.PowerToolbar.Name = "PowerToolbar";
			this.PowerToolbar.Size = new System.Drawing.Size(335, 25);
			this.PowerToolbar.TabIndex = 1;
			this.PowerToolbar.Text = "toolStrip2";
			this.PowerAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PowerAddBtn.Image = (Image)componentResourceManager.GetObject("PowerAddBtn.Image");
			this.PowerAddBtn.ImageTransparentColor = Color.Magenta;
			this.PowerAddBtn.Name = "PowerAddBtn";
			this.PowerAddBtn.Size = new System.Drawing.Size(33, 22);
			this.PowerAddBtn.Text = "Add";
			this.PowerAddBtn.Click += new EventHandler(this.PowerAddBtn_Click);
			this.PowerRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PowerRemoveBtn.Image = (Image)componentResourceManager.GetObject("PowerRemoveBtn.Image");
			this.PowerRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.PowerRemoveBtn.Name = "PowerRemoveBtn";
			this.PowerRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.PowerRemoveBtn.Text = "Remove";
			this.PowerRemoveBtn.Click += new EventHandler(this.PowerRemoveBtn_Click);
			this.PowerEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PowerEditBtn.Image = (Image)componentResourceManager.GetObject("PowerEditBtn.Image");
			this.PowerEditBtn.ImageTransparentColor = Color.Magenta;
			this.PowerEditBtn.Name = "PowerEditBtn";
			this.PowerEditBtn.Size = new System.Drawing.Size(31, 22);
			this.PowerEditBtn.Text = "Edit";
			this.PowerEditBtn.Click += new EventHandler(this.PowerEditBtn_Click);
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(205, 218);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(286, 218);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(373, 253);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.Pages);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "OptionLevelForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Level";
			this.Pages.ResumeLayout(false);
			this.FeaturesPage.ResumeLayout(false);
			this.FeaturesPage.PerformLayout();
			this.FeatureToolbar.ResumeLayout(false);
			this.FeatureToolbar.PerformLayout();
			this.PowersPage.ResumeLayout(false);
			this.PowersPage.PerformLayout();
			this.PowerToolbar.ResumeLayout(false);
			this.PowerToolbar.PerformLayout();
			base.ResumeLayout(false);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
		}

		private void PowerAddBtn_Click(object sender, EventArgs e)
		{
			OptionPowerForm optionPowerForm = new OptionPowerForm(new PlayerPower()
			{
				Name = "New Power"
			});
			if (optionPowerForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fLevel.Powers.Add(optionPowerForm.Power);
				this.update_powers();
			}
		}

		private void PowerEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedPower != null)
			{
				int power = this.fLevel.Powers.IndexOf(this.SelectedPower);
				OptionPowerForm optionPowerForm = new OptionPowerForm(this.SelectedPower);
				if (optionPowerForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fLevel.Powers[power] = optionPowerForm.Power;
					this.update_powers();
				}
			}
		}

		private void PowerRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedPower != null)
			{
				this.fLevel.Powers.Remove(this.SelectedPower);
				this.update_powers();
			}
		}

		private void update_features()
		{
			this.FeatureList.Items.Clear();
			foreach (Feature feature in this.fLevel.Features)
			{
				ListViewItem listViewItem = this.FeatureList.Items.Add(feature.Name);
				listViewItem.Tag = feature;
			}
			if (this.fLevel.Features.Count == 0)
			{
				ListViewItem grayText = this.FeatureList.Items.Add("(none)");
				grayText.ForeColor = SystemColors.GrayText;
			}
		}

		private void update_powers()
		{
			this.PowerList.Items.Clear();
			foreach (PlayerPower power in this.fLevel.Powers)
			{
				ListViewItem listViewItem = this.PowerList.Items.Add(power.Name);
				listViewItem.Tag = power;
			}
			if (this.fLevel.Powers.Count == 0)
			{
				ListViewItem grayText = this.PowerList.Items.Add("(none)");
				grayText.ForeColor = SystemColors.GrayText;
			}
		}
	}
}