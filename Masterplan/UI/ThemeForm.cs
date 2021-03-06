using Masterplan;
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
	internal class ThemeForm : Form
	{
		private EncounterCard fCard;

		private Button OKBtn;

		private Button CancelBtn;

		private Label AttackLbl;

		private ComboBox AttackBox;

		private Label UtilityLbl;

		private ComboBox UtilityBox;

		private WebBrowser Browser;

		private Panel BrowserPanel;

		private Label ThemeLbl;

		private Label ThemeNameLbl;

		private ToolStrip toolStrip1;

		private ToolStripButton SelectThemeBtn;

		private ToolStripButton CreateThemeBtn;

		private ToolStripButton ClearThemeBtn;

		public EncounterCard Card
		{
			get
			{
				return this.fCard;
			}
		}

		public ThemeForm(EncounterCard card)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.Browser.DocumentText = "";
			this.fCard = card.Copy();
			if (this.fCard.ThemeID == Guid.Empty)
			{
				this.update_selected_theme(null, true);
				return;
			}
			MonsterTheme monsterTheme = Session.FindTheme(this.fCard.ThemeID, SearchType.Global);
			this.update_selected_theme(monsterTheme, false);
			ThemePowerData themePowerDatum = monsterTheme.FindPower(this.fCard.ThemeAttackPowerID);
			this.AttackBox.SelectedItem = themePowerDatum;
			ThemePowerData themePowerDatum1 = monsterTheme.FindPower(this.fCard.ThemeUtilityPowerID);
			this.UtilityBox.SelectedItem = themePowerDatum1;
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.SelectThemeBtn.Enabled = Session.Themes.Count != 0;
			this.ClearThemeBtn.Enabled = this.fCard.ThemeID != Guid.Empty;
		}

		private void AttackBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ThemePowerData selectedItem = this.AttackBox.SelectedItem as ThemePowerData;
			this.fCard.ThemeAttackPowerID = (selectedItem != null ? selectedItem.Power.ID : Guid.Empty);
			this.update_browser();
		}

		private void ClearBtn_Click(object sender, EventArgs e)
		{
			this.update_selected_theme(null, true);
		}

		private void CreateThemeBtn_Click(object sender, EventArgs e)
		{
			MonsterThemeForm monsterThemeForm = new MonsterThemeForm(new MonsterTheme()
			{
				Name = "New Theme"
			});
			if (monsterThemeForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Project.Library.Themes.Add(monsterThemeForm.Theme);
				Session.Modified = true;
				this.update_selected_theme(monsterThemeForm.Theme, true);
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ThemeForm));
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.AttackLbl = new Label();
			this.AttackBox = new ComboBox();
			this.UtilityLbl = new Label();
			this.UtilityBox = new ComboBox();
			this.Browser = new WebBrowser();
			this.BrowserPanel = new Panel();
			this.ThemeLbl = new Label();
			this.ThemeNameLbl = new Label();
			this.toolStrip1 = new ToolStrip();
			this.SelectThemeBtn = new ToolStripButton();
			this.CreateThemeBtn = new ToolStripButton();
			this.ClearThemeBtn = new ToolStripButton();
			this.BrowserPanel.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(202, 379);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 10;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(283, 379);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 11;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.AttackLbl.AutoSize = true;
			this.AttackLbl.Location = new Point(12, 61);
			this.AttackLbl.Name = "AttackLbl";
			this.AttackLbl.Size = new System.Drawing.Size(73, 13);
			this.AttackLbl.TabIndex = 5;
			this.AttackLbl.Text = "Attack power:";
			this.AttackBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.AttackBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.AttackBox.FormattingEnabled = true;
			this.AttackBox.Location = new Point(91, 58);
			this.AttackBox.Name = "AttackBox";
			this.AttackBox.Size = new System.Drawing.Size(267, 21);
			this.AttackBox.TabIndex = 6;
			this.AttackBox.SelectedIndexChanged += new EventHandler(this.AttackBox_SelectedIndexChanged);
			this.UtilityLbl.AutoSize = true;
			this.UtilityLbl.Location = new Point(12, 88);
			this.UtilityLbl.Name = "UtilityLbl";
			this.UtilityLbl.Size = new System.Drawing.Size(67, 13);
			this.UtilityLbl.TabIndex = 7;
			this.UtilityLbl.Text = "Utility power:";
			this.UtilityBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.UtilityBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.UtilityBox.FormattingEnabled = true;
			this.UtilityBox.Location = new Point(91, 85);
			this.UtilityBox.Name = "UtilityBox";
			this.UtilityBox.Size = new System.Drawing.Size(267, 21);
			this.UtilityBox.TabIndex = 8;
			this.UtilityBox.SelectedIndexChanged += new EventHandler(this.UtilityBox_SelectedIndexChanged);
			this.Browser.AllowNavigation = false;
			this.Browser.AllowWebBrowserDrop = false;
			this.Browser.Dock = DockStyle.Fill;
			this.Browser.IsWebBrowserContextMenuEnabled = false;
			this.Browser.Location = new Point(0, 0);
			this.Browser.MinimumSize = new System.Drawing.Size(20, 20);
			this.Browser.Name = "Browser";
			this.Browser.ScriptErrorsSuppressed = true;
			this.Browser.Size = new System.Drawing.Size(344, 259);
			this.Browser.TabIndex = 0;
			this.Browser.WebBrowserShortcutsEnabled = false;
			this.BrowserPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.BrowserPanel.BorderStyle = BorderStyle.FixedSingle;
			this.BrowserPanel.Controls.Add(this.Browser);
			this.BrowserPanel.Location = new Point(12, 112);
			this.BrowserPanel.Name = "BrowserPanel";
			this.BrowserPanel.Size = new System.Drawing.Size(346, 261);
			this.BrowserPanel.TabIndex = 9;
			this.ThemeLbl.AutoSize = true;
			this.ThemeLbl.Location = new Point(12, 32);
			this.ThemeLbl.Name = "ThemeLbl";
			this.ThemeLbl.Size = new System.Drawing.Size(43, 13);
			this.ThemeLbl.TabIndex = 0;
			this.ThemeLbl.Text = "Theme:";
			this.ThemeNameLbl.AutoSize = true;
			this.ThemeNameLbl.Location = new Point(88, 32);
			this.ThemeNameLbl.Name = "ThemeNameLbl";
			this.ThemeNameLbl.Size = new System.Drawing.Size(42, 13);
			this.ThemeNameLbl.TabIndex = 12;
			this.ThemeNameLbl.Text = "[theme]";
			ToolStripItemCollection items = this.toolStrip1.Items;
			ToolStripItem[] selectThemeBtn = new ToolStripItem[] { this.SelectThemeBtn, this.CreateThemeBtn, this.ClearThemeBtn };
			items.AddRange(selectThemeBtn);
			this.toolStrip1.Location = new Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(370, 25);
			this.toolStrip1.TabIndex = 13;
			this.toolStrip1.Text = "toolStrip1";
			this.SelectThemeBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SelectThemeBtn.Image = (Image)componentResourceManager.GetObject("SelectThemeBtn.Image");
			this.SelectThemeBtn.ImageTransparentColor = Color.Magenta;
			this.SelectThemeBtn.Name = "SelectThemeBtn";
			this.SelectThemeBtn.Size = new System.Drawing.Size(82, 22);
			this.SelectThemeBtn.Text = "Select Theme";
			this.SelectThemeBtn.Click += new EventHandler(this.SelectThemeBtn_Click);
			this.CreateThemeBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.CreateThemeBtn.Image = (Image)componentResourceManager.GetObject("CreateThemeBtn.Image");
			this.CreateThemeBtn.ImageTransparentColor = Color.Magenta;
			this.CreateThemeBtn.Name = "CreateThemeBtn";
			this.CreateThemeBtn.Size = new System.Drawing.Size(112, 22);
			this.CreateThemeBtn.Text = "Create New Theme";
			this.CreateThemeBtn.Click += new EventHandler(this.CreateThemeBtn_Click);
			this.ClearThemeBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ClearThemeBtn.Image = (Image)componentResourceManager.GetObject("ClearThemeBtn.Image");
			this.ClearThemeBtn.ImageTransparentColor = Color.Magenta;
			this.ClearThemeBtn.Name = "ClearThemeBtn";
			this.ClearThemeBtn.Size = new System.Drawing.Size(78, 22);
			this.ClearThemeBtn.Text = "Clear Theme";
			this.ClearThemeBtn.Click += new EventHandler(this.ClearBtn_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(370, 411);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.ThemeNameLbl);
			base.Controls.Add(this.BrowserPanel);
			base.Controls.Add(this.UtilityBox);
			base.Controls.Add(this.UtilityLbl);
			base.Controls.Add(this.AttackBox);
			base.Controls.Add(this.AttackLbl);
			base.Controls.Add(this.ThemeLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ThemeForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Set Theme";
			this.BrowserPanel.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
		}

		private void SelectThemeBtn_Click(object sender, EventArgs e)
		{
			MonsterThemeSelectForm monsterThemeSelectForm = new MonsterThemeSelectForm();
			if (monsterThemeSelectForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.update_selected_theme(monsterThemeSelectForm.MonsterTheme, true);
			}
		}

		private void update_browser()
		{
			this.Browser.Document.OpenNew(true);
			this.Browser.Document.Write(HTML.StatBlock(this.fCard, null, null, true, false, true, CardMode.View, DisplaySize.Small));
		}

		private void update_selected_theme(MonsterTheme theme, bool reset_powers)
		{
			if (theme == null)
			{
				this.ThemeNameLbl.Text = "None";
				this.fCard.ThemeID = Guid.Empty;
			}
			else
			{
				this.ThemeNameLbl.Text = theme.Name;
				this.fCard.ThemeID = theme.ID;
			}
			this.AttackBox.Items.Clear();
			this.AttackBox.Items.Add("(no attack power)");
			this.UtilityBox.Items.Clear();
			this.UtilityBox.Items.Add("(no utility power)");
			if (theme != null)
			{
				foreach (ThemePowerData themePowerDatum in theme.ListPowers(this.fCard.Roles, PowerType.Attack))
				{
					this.AttackBox.Items.Add(themePowerDatum);
				}
				foreach (ThemePowerData themePowerDatum1 in theme.ListPowers(this.fCard.Roles, PowerType.Utility))
				{
					this.UtilityBox.Items.Add(themePowerDatum1);
				}
			}
			if (reset_powers)
			{
				this.AttackBox.SelectedIndex = 0;
				this.UtilityBox.SelectedIndex = 0;
			}
			this.AttackBox.Enabled = this.AttackBox.Items.Count > 1;
			this.UtilityBox.Enabled = this.UtilityBox.Items.Count > 1;
			this.update_browser();
		}

		private void UtilityBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ThemePowerData selectedItem = this.UtilityBox.SelectedItem as ThemePowerData;
			this.fCard.ThemeUtilityPowerID = (selectedItem != null ? selectedItem.Power.ID : Guid.Empty);
			this.update_browser();
		}
	}
}