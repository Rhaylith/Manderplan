using Masterplan;
using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class CustomOverlayForm : Form
	{
		private const string ROUNDED = "Rounded (translucent)";

		private const string BLOCK = "Block (opaque)";

		private Label NameLbl;

		private TextBox NameBox;

		private Button OKBtn;

		private Button CancelBtn;

		private TabControl Pages;

		private TabPage DetailsPage;

		private TextBox DetailsBox;

		private NumericUpDown WidthBox;

		private NumericUpDown HeightBox;

		private Label WidthLbl;

		private Label HeightLbl;

		private TabPage tabPage1;

		private TokenPanel TilePanel;

		private TabPage OptionsPage;

		private ComboBox StyleBox;

		private Label StyleLbl;

		private CheckBox OpaqueBox;

        private CheckBox TerrainLayerBox;

        private CheckBox IsUnSelectableBox;

        private CheckBox DifficultBox;

		private TabPage TerrainPowerPage;

		private ToolStrip TerrainPowerToolbar;

		private ToolStripButton EditBtn;

		private ToolStripButton RemoveBtn;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton SelectBtn;

		private WebBrowser PowerBrowser;

		private CustomToken fToken;

		public CustomToken Token
		{
			get
			{
				return this.fToken;
			}
		}

		public CustomOverlayForm(CustomToken ct)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fToken = ct.Copy();
			this.NameBox.Text = this.fToken.Name;
			this.WidthBox.Value = this.fToken.OverlaySize.Width;
			this.HeightBox.Value = this.fToken.OverlaySize.Height;
			this.update_power();
			this.DetailsBox.Text = this.fToken.Details;
			this.TilePanel.TileSize = this.fToken.OverlaySize;
			this.TilePanel.Image = this.fToken.Image;
			this.TilePanel.Colour = this.fToken.Colour;
			this.StyleBox.Items.Add("Rounded (translucent)");
			this.StyleBox.Items.Add("Block (opaque)");
			switch (this.fToken.OverlayStyle)
			{
				case OverlayStyle.Rounded:
				{
					this.StyleBox.Text = "Rounded (translucent)";
					break;
				}
				case OverlayStyle.Block:
				{
					this.StyleBox.Text = "Block (opaque)";
					break;
				}
			}
			this.DifficultBox.Checked = this.fToken.DifficultTerrain;
			this.OpaqueBox.Checked = this.fToken.Opaque;
            this.TerrainLayerBox.Checked = this.fToken.IsTerrainLayer;
            this.IsUnSelectableBox.Checked = this.fToken.IsUnSelectable;
        }

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveBtn.Enabled = this.fToken.TerrainPower != null;
			this.SelectBtn.Enabled = Session.TerrainPowers.Count != 0;
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			TerrainPower terrainPower = this.fToken.TerrainPower ?? new TerrainPower()
			{
				Name = this.NameBox.Text
			};
			TerrainPowerForm terrainPowerForm = new TerrainPowerForm(terrainPower);
			if (terrainPowerForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fToken.TerrainPower = terrainPowerForm.Power;
				this.NameBox.Text = this.fToken.TerrainPower.Name;
				this.update_power();
			}
		}

		private void HeightBox_ValueChanged(object sender, EventArgs e)
		{
			this.update_tile_size();
		}

		private void InitializeComponent()
		{
			ComponentResourceManager resources = new ComponentResourceManager(typeof(CustomOverlayForm));
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.Pages = new TabControl();
			this.DetailsPage = new TabPage();
			this.DetailsBox = new TextBox();
			this.tabPage1 = new TabPage();
			this.TilePanel = new TokenPanel();
			this.OptionsPage = new TabPage();
			this.StyleBox = new ComboBox();
			this.StyleLbl = new Label();
			this.OpaqueBox = new CheckBox();
            this.TerrainLayerBox = new CheckBox();
            this.IsUnSelectableBox = new CheckBox();
            this.DifficultBox = new CheckBox();
			this.TerrainPowerPage = new TabPage();
			this.PowerBrowser = new WebBrowser();
			this.TerrainPowerToolbar = new ToolStrip();
			this.EditBtn = new ToolStripButton();
			this.RemoveBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.SelectBtn = new ToolStripButton();
			this.WidthBox = new NumericUpDown();
			this.HeightBox = new NumericUpDown();
			this.WidthLbl = new Label();
			this.HeightLbl = new Label();
			this.Pages.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.OptionsPage.SuspendLayout();
			this.TerrainPowerPage.SuspendLayout();
			this.TerrainPowerToolbar.SuspendLayout();
			((ISupportInitialize)this.WidthBox).BeginInit();
			((ISupportInitialize)this.HeightBox).BeginInit();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new System.Drawing.Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.NameBox.Location = new Point(59, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(303, 20);
			this.NameBox.TabIndex = 1;
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(206, 311);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 10;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(287, 311);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 11;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.Pages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Controls.Add(this.tabPage1);
			this.Pages.Controls.Add(this.OptionsPage);
			this.Pages.Controls.Add(this.TerrainPowerPage);
			this.Pages.Location = new Point(15, 90);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new System.Drawing.Size(350, 215);
			this.Pages.TabIndex = 9;
			this.DetailsPage.Controls.Add(this.DetailsBox);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new System.Windows.Forms.Padding(3);
			this.DetailsPage.Size = new System.Drawing.Size(342, 189);
			this.DetailsPage.TabIndex = 0;
			this.DetailsPage.Text = "Details";
			this.DetailsPage.UseVisualStyleBackColor = true;
			this.DetailsBox.AcceptsReturn = true;
			this.DetailsBox.AcceptsTab = true;
			this.DetailsBox.Dock = DockStyle.Fill;
			this.DetailsBox.Location = new Point(3, 3);
			this.DetailsBox.Multiline = true;
			this.DetailsBox.Name = "DetailsBox";
			this.DetailsBox.ScrollBars = ScrollBars.Vertical;
			this.DetailsBox.Size = new System.Drawing.Size(336, 183);
			this.DetailsBox.TabIndex = 0;
			this.tabPage1.Controls.Add(this.TilePanel);
			this.tabPage1.Location = new Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(317, 189);
			this.tabPage1.TabIndex = 1;
			this.tabPage1.Text = "Picture";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.TilePanel.Colour = Color.Blue;
			this.TilePanel.Dock = DockStyle.Fill;
			this.TilePanel.Image = null;
			this.TilePanel.Location = new Point(3, 3);
			this.TilePanel.Name = "TilePanel";
			this.TilePanel.Size = new System.Drawing.Size(311, 183);
			this.TilePanel.TabIndex = 0;
			this.TilePanel.TileSize = new System.Drawing.Size(2, 2);
			this.OptionsPage.Controls.Add(this.StyleBox);
			this.OptionsPage.Controls.Add(this.StyleLbl);
			this.OptionsPage.Controls.Add(this.OpaqueBox);
            this.OptionsPage.Controls.Add(this.TerrainLayerBox);
            this.OptionsPage.Controls.Add(this.IsUnSelectableBox);
            this.OptionsPage.Controls.Add(this.DifficultBox);
			this.OptionsPage.Location = new Point(4, 22);
			this.OptionsPage.Name = "OptionsPage";
			this.OptionsPage.Padding = new System.Windows.Forms.Padding(3);
			this.OptionsPage.Size = new System.Drawing.Size(317, 189);
			this.OptionsPage.TabIndex = 2;
			this.OptionsPage.Text = "Display Options";
			this.OptionsPage.UseVisualStyleBackColor = true;
			this.StyleBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.StyleBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.StyleBox.FormattingEnabled = true;
			this.StyleBox.Location = new Point(45, 6);
			this.StyleBox.Name = "StyleBox";
			this.StyleBox.Size = new System.Drawing.Size(266, 21);
			this.StyleBox.TabIndex = 1;
			this.StyleLbl.AutoSize = true;
			this.StyleLbl.Location = new Point(6, 9);
			this.StyleLbl.Name = "StyleLbl";
			this.StyleLbl.Size = new System.Drawing.Size(33, 13);
			this.StyleLbl.TabIndex = 0;
			this.StyleLbl.Text = "Style:";
			this.OpaqueBox.AutoSize = true;
			this.OpaqueBox.Location = new Point(9, 71);
			this.OpaqueBox.Name = "OpaqueBox";
			this.OpaqueBox.Size = new System.Drawing.Size(173, 17);
			this.OpaqueBox.TabIndex = 3;
			this.OpaqueBox.Text = "This overlay blocks line of sight";
			this.OpaqueBox.UseVisualStyleBackColor = true;

            this.TerrainLayerBox.AutoSize = true;
            this.TerrainLayerBox.Location = new Point(9, 94);
            this.TerrainLayerBox.Name = "TerrainLayerBox";
            this.TerrainLayerBox.Size = new System.Drawing.Size(173, 17);
            this.TerrainLayerBox.TabIndex = 3;
            this.TerrainLayerBox.Text = "This overlay is a terrain layer";
            this.TerrainLayerBox.UseVisualStyleBackColor = true;

            this.IsUnSelectableBox.AutoSize = true;
            this.IsUnSelectableBox.Location = new Point(9, 117);
            this.IsUnSelectableBox.Name = "IsUnSelectableBox";
            this.IsUnSelectableBox.Size = new System.Drawing.Size(173, 17);
            this.IsUnSelectableBox.TabIndex = 3;
            this.IsUnSelectableBox.Text = "This overlay is not selectable in game window";
            this.IsUnSelectableBox.UseVisualStyleBackColor = true;


            this.DifficultBox.AutoSize = true;
			this.DifficultBox.Location = new Point(9, 48);
			this.DifficultBox.Name = "DifficultBox";
			this.DifficultBox.Size = new System.Drawing.Size(153, 17);
			this.DifficultBox.TabIndex = 2;
			this.DifficultBox.Text = "Add difficult terrain markers";
			this.DifficultBox.UseVisualStyleBackColor = true;
			this.TerrainPowerPage.Controls.Add(this.PowerBrowser);
			this.TerrainPowerPage.Controls.Add(this.TerrainPowerToolbar);
			this.TerrainPowerPage.Location = new Point(4, 22);
			this.TerrainPowerPage.Name = "TerrainPowerPage";
			this.TerrainPowerPage.Padding = new System.Windows.Forms.Padding(3);
			this.TerrainPowerPage.Size = new System.Drawing.Size(317, 189);
			this.TerrainPowerPage.TabIndex = 3;
			this.TerrainPowerPage.Text = "Terrain Power";
			this.TerrainPowerPage.UseVisualStyleBackColor = true;
			this.PowerBrowser.Dock = DockStyle.Fill;
			this.PowerBrowser.Location = new Point(3, 28);
			this.PowerBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.PowerBrowser.Name = "PowerBrowser";
			this.PowerBrowser.Size = new System.Drawing.Size(311, 158);
			this.PowerBrowser.TabIndex = 2;
			ToolStripItem[] editBtn = new ToolStripItem[] { this.EditBtn, this.RemoveBtn, this.toolStripSeparator1, this.SelectBtn };
			this.TerrainPowerToolbar.Items.AddRange(editBtn);
			this.TerrainPowerToolbar.Location = new Point(3, 3);
			this.TerrainPowerToolbar.Name = "TerrainPowerToolbar";
			this.TerrainPowerToolbar.Size = new System.Drawing.Size(311, 25);
			this.TerrainPowerToolbar.TabIndex = 1;
			this.TerrainPowerToolbar.Text = "toolStrip1";
			this.EditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditBtn.Image = (Image)resources.GetObject("EditBtn.Image");
			this.EditBtn.ImageTransparentColor = Color.Magenta;
			this.EditBtn.Name = "EditBtn";
			this.EditBtn.Size = new System.Drawing.Size(31, 22);
			this.EditBtn.Text = "Edit";
			this.EditBtn.Click += new EventHandler(this.EditBtn_Click);
			this.RemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveBtn.Image = (Image)resources.GetObject("RemoveBtn.Image");
			this.RemoveBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.RemoveBtn.Text = "Remove";
			this.RemoveBtn.Click += new EventHandler(this.RemoveBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.SelectBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SelectBtn.Image = (Image)resources.GetObject("SelectBtn.Image");
			this.SelectBtn.ImageTransparentColor = Color.Magenta;
			this.SelectBtn.Name = "SelectBtn";
			this.SelectBtn.Size = new System.Drawing.Size(116, 22);
			this.SelectBtn.Text = "Use Standard Power";
			this.SelectBtn.Click += new EventHandler(this.SelectBtn_Click);
			this.WidthBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.WidthBox.Location = new Point(59, 38);
			int[] numArray = new int[] { 1, 0, 0, 0 };
			this.WidthBox.Minimum = new decimal(numArray);
			this.WidthBox.Name = "WidthBox";
			this.WidthBox.Size = new System.Drawing.Size(303, 20);
			this.WidthBox.TabIndex = 3;
			int[] numArray1 = new int[] { 1, 0, 0, 0 };
			this.WidthBox.Value = new decimal(numArray1);
			this.WidthBox.ValueChanged += new EventHandler(this.WidthBox_ValueChanged);
			this.HeightBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.HeightBox.Location = new Point(59, 64);
			int[] numArray2 = new int[] { 1, 0, 0, 0 };
			this.HeightBox.Minimum = new decimal(numArray2);
			this.HeightBox.Name = "HeightBox";
			this.HeightBox.Size = new System.Drawing.Size(303, 20);
			this.HeightBox.TabIndex = 5;
			int[] numArray3 = new int[] { 1, 0, 0, 0 };
			this.HeightBox.Value = new decimal(numArray3);
			this.HeightBox.ValueChanged += new EventHandler(this.HeightBox_ValueChanged);
			this.WidthLbl.AutoSize = true;
			this.WidthLbl.Location = new Point(12, 40);
			this.WidthLbl.Name = "WidthLbl";
			this.WidthLbl.Size = new System.Drawing.Size(38, 13);
			this.WidthLbl.TabIndex = 2;
			this.WidthLbl.Text = "Width:";
			this.HeightLbl.AutoSize = true;
			this.HeightLbl.Location = new Point(12, 66);
			this.HeightLbl.Name = "HeightLbl";
			this.HeightLbl.Size = new System.Drawing.Size(41, 13);
			this.HeightLbl.TabIndex = 4;
			this.HeightLbl.Text = "Height:";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(374, 346);
			base.Controls.Add(this.HeightLbl);
			base.Controls.Add(this.WidthLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.HeightBox);
			base.Controls.Add(this.WidthBox);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CustomOverlayForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Custom Overlay";
			this.Pages.ResumeLayout(false);
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			this.tabPage1.ResumeLayout(false);
			this.OptionsPage.ResumeLayout(false);
			this.OptionsPage.PerformLayout();
			this.TerrainPowerPage.ResumeLayout(false);
			this.TerrainPowerPage.PerformLayout();
			this.TerrainPowerToolbar.ResumeLayout(false);
			this.TerrainPowerToolbar.PerformLayout();
			((ISupportInitialize)this.WidthBox).EndInit();
			((ISupportInitialize)this.HeightBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fToken.Name = this.NameBox.Text;
			this.fToken.OverlaySize = new System.Drawing.Size((int)this.WidthBox.Value, (int)this.HeightBox.Value);
			this.fToken.Details = this.DetailsBox.Text;
			this.fToken.DifficultTerrain = this.DifficultBox.Checked;
			this.fToken.Opaque = this.OpaqueBox.Checked;
            this.fToken.IsTerrainLayer = this.TerrainLayerBox.Checked;
            this.fToken.IsUnSelectable = this.IsUnSelectableBox.Checked;
            this.fToken.Image = this.TilePanel.Image;
			this.fToken.Colour = this.TilePanel.Colour;
			this.fToken.OverlayStyle = (this.StyleBox.Text == "Rounded (translucent)" ? OverlayStyle.Rounded : OverlayStyle.Block);
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			this.fToken.TerrainPower = null;
			this.update_power();
		}

		private void SelectBtn_Click(object sender, EventArgs e)
		{
			TerrainPowerSelectForm terrainPowerSelectForm = new TerrainPowerSelectForm();
			if (terrainPowerSelectForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fToken.TerrainPower = terrainPowerSelectForm.TerrainPower.Copy();
				this.update_power();
			}
		}

		private void update_power()
		{
			this.PowerBrowser.DocumentText = HTML.TerrainPower(this.fToken.TerrainPower, DisplaySize.Small);
		}

		private void update_tile_size()
		{
			this.TilePanel.TileSize = new System.Drawing.Size((int)this.WidthBox.Value, (int)this.HeightBox.Value);
		}

		private void WidthBox_ValueChanged(object sender, EventArgs e)
		{
			this.update_tile_size();
		}
	}
}