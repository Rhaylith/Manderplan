using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class DamageForm : Form
	{
		private List<DamageForm.Token> fData;

		private List<DamageType> fTypes = new List<DamageType>();

		private Button OKBtn;

		private Button CancelBtn;

		private Label DmgLbl;

		private NumericUpDown DmgBox;

		private Label ModLbl;

		private TextBox ModBox;

		private Label ValLbl;

		private CheckBox HalveBtn;

		private Label TypeLbl;

		private TextBox ValBox;

		private ToolStrip AmountToolbar;

		private ToolStripButton Dmg1;

		private ToolStripButton Dmg2;

		private ToolStripButton Dmg5;

		private ToolStripButton Dmg10;

		private ToolStripButton Dmg20;

		private ToolStripButton Dmg50;

		private ToolStripButton Dmg100;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton ResetBtn;

		private ToolStrip TypeToolbar;

		private ToolStripButton FireBtn;

		private ToolStripButton ColdBtn;

		private ToolStripButton LightningBtn;

		private ToolStripButton ThunderBtn;

		private ToolStripButton PsychicBtn;

		private ToolStripButton ForceBtn;

		private ToolStripButton AcidBtn;

		private ToolStripButton PoisonBtn;

		private ToolStripButton NecroticBtn;

		private ToolStripButton RadiantBtn;

		private TextBox TypeBox;

        public Masterplan.Commands.Combat.DamageEntityCommand DamageCommand = new Commands.Combat.DamageEntityCommand();

		public List<DamageType> Types
		{
			get
			{
				return this.fTypes;
			}
		}

		public DamageForm(List<Pair<CombatData, EncounterCard>> tokens, int value)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fData = new List<DamageForm.Token>();
			foreach (Pair<CombatData, EncounterCard> token in tokens)
			{
				this.fData.Add(new DamageForm.Token(token.First, token.Second));
			}
			this.DmgBox.Value = value;
			if (this.fData.Count == 1 && this.fData[0].Card != null)
			{
				this.HalveBtn.Checked = this.fData[0].Card.Resist.ToLower().Contains("insubstantial");
			}
			this.update_type();
			this.update_modifier();
			this.update_value();
		}

		private void AcidBtn_Click(object sender, EventArgs e)
		{
			this.add_type(DamageType.Acid);
		}

		private void add_type(DamageType type)
		{
			if (!this.fTypes.Contains(type))
			{
				this.fTypes.Add(type);
				this.fTypes.Sort();
			}
			else
			{
				this.fTypes.Remove(type);
			}
			this.update_type();
			this.update_modifier();
			this.update_value();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.ResetBtn.Enabled = this.DmgBox.Value != new decimal(0);
			this.AcidBtn.Checked = this.fTypes.Contains(DamageType.Acid);
			this.ColdBtn.Checked = this.fTypes.Contains(DamageType.Cold);
			this.FireBtn.Checked = this.fTypes.Contains(DamageType.Fire);
			this.ForceBtn.Checked = this.fTypes.Contains(DamageType.Force);
			this.LightningBtn.Checked = this.fTypes.Contains(DamageType.Lightning);
			this.NecroticBtn.Checked = this.fTypes.Contains(DamageType.Necrotic);
			this.PoisonBtn.Checked = this.fTypes.Contains(DamageType.Poison);
			this.PsychicBtn.Checked = this.fTypes.Contains(DamageType.Psychic);
			this.RadiantBtn.Checked = this.fTypes.Contains(DamageType.Radiant);
			this.ThunderBtn.Checked = this.fTypes.Contains(DamageType.Thunder);
			this.TypeLbl.Enabled = this.fTypes.Count != 0;
			this.TypeBox.Enabled = this.fTypes.Count != 0;
			this.ModLbl.Enabled = this.fTypes.Count != 0;
			this.ModBox.Enabled = this.fTypes.Count != 0;
		}

		private void ColdBtn_Click(object sender, EventArgs e)
		{
			this.add_type(DamageType.Cold);
		}

		private void damage(int n)
		{
			NumericUpDown dmgBox = this.DmgBox;
			dmgBox.Value = dmgBox.Value + n;
		}

		private void DamageForm_Shown(object sender, EventArgs e)
		{
			this.DmgBox.Select(0, 1);
		}

		private void Dmg1_Click(object sender, EventArgs e)
		{
			this.damage(1);
		}

		private void Dmg10_Click(object sender, EventArgs e)
		{
			this.damage(10);
		}

		private void Dmg100_Click(object sender, EventArgs e)
		{
			this.damage(100);
		}

		private void Dmg2_Click(object sender, EventArgs e)
		{
			this.damage(2);
		}

		private void Dmg20_Click(object sender, EventArgs e)
		{
			this.damage(20);
		}

		private void Dmg5_Click(object sender, EventArgs e)
		{
			this.damage(5);
		}

		private void Dmg50_Click(object sender, EventArgs e)
		{
			this.damage(50);
		}

		private void DmgBox_ValueChanged(object sender, EventArgs e)
		{
			this.update_value();
		}

		internal void DoDamage(CombatData data, EncounterCard card, int damage, List<DamageType> types, bool halve_damage)
		{
			int damageModifier = 0;
			if (card != null)
			{
				damageModifier = card.GetDamageModifier(types, data);
			}
			int _value = DamageForm.get_value(damage, damageModifier, halve_damage);
            this.DamageCommand.AddTarget(data, _value);
			//if (data.TempHP > 0)
			//{
			//	int num = Math.Min(data.TempHP, _value);
			//	CombatData tempHP = data;
			//	tempHP.TempHP = tempHP.TempHP - num;
			//	_value -= num;
			//}
			//CombatData combatDatum = data;
			//combatDatum.Damage = combatDatum.Damage + _value;
		}

		private void FireBtn_Click(object sender, EventArgs e)
		{
			this.add_type(DamageType.Fire);
		}

		private void ForceBtn_Click(object sender, EventArgs e)
		{
			this.add_type(DamageType.Force);
		}

		private static int get_value(int initial_value, int modifier, bool halve_damage)
		{
			int initialValue = initial_value;
			if (modifier != 0)
			{
				if (modifier != -2147483648)
				{
					initialValue += modifier;
					initialValue = Math.Max(initialValue, 0);
				}
				else
				{
					initialValue = 0;
				}
			}
			if (halve_damage)
			{
				initialValue /= 2;
			}
			return initialValue;
		}

		private void HalveBtn_CheckedChanged(object sender, EventArgs e)
		{
			this.update_value();
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(DamageForm));
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.DmgLbl = new Label();
			this.DmgBox = new NumericUpDown();
			this.ModLbl = new Label();
			this.ModBox = new TextBox();
			this.ValLbl = new Label();
			this.HalveBtn = new CheckBox();
			this.TypeLbl = new Label();
			this.ValBox = new TextBox();
			this.AmountToolbar = new ToolStrip();
			this.Dmg1 = new ToolStripButton();
			this.Dmg2 = new ToolStripButton();
			this.Dmg5 = new ToolStripButton();
			this.Dmg10 = new ToolStripButton();
			this.Dmg20 = new ToolStripButton();
			this.Dmg50 = new ToolStripButton();
			this.Dmg100 = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.ResetBtn = new ToolStripButton();
			this.TypeToolbar = new ToolStrip();
			this.AcidBtn = new ToolStripButton();
			this.ColdBtn = new ToolStripButton();
			this.FireBtn = new ToolStripButton();
			this.ForceBtn = new ToolStripButton();
			this.LightningBtn = new ToolStripButton();
			this.NecroticBtn = new ToolStripButton();
			this.PoisonBtn = new ToolStripButton();
			this.PsychicBtn = new ToolStripButton();
			this.RadiantBtn = new ToolStripButton();
			this.ThunderBtn = new ToolStripButton();
			this.TypeBox = new TextBox();
			((ISupportInitialize)this.DmgBox).BeginInit();
			this.AmountToolbar.SuspendLayout();
			this.TypeToolbar.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(172, 190);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 11;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(253, 190);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 12;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.DmgLbl.AutoSize = true;
			this.DmgLbl.Location = new Point(66, 30);
			this.DmgLbl.Name = "DmgLbl";
			this.DmgLbl.Size = new System.Drawing.Size(50, 13);
			this.DmgLbl.TabIndex = 2;
			this.DmgLbl.Text = "Damage:";
			this.DmgBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.DmgBox.Location = new Point(134, 28);
			NumericUpDown dmgBox = this.DmgBox;
			int[] numArray = new int[] { 1000, 0, 0, 0 };
			dmgBox.Maximum = new decimal(numArray);
			this.DmgBox.Name = "DmgBox";
			this.DmgBox.Size = new System.Drawing.Size(194, 20);
			this.DmgBox.TabIndex = 3;
			this.DmgBox.ValueChanged += new EventHandler(this.DmgBox_ValueChanged);
			this.ModLbl.AutoSize = true;
			this.ModLbl.Location = new Point(66, 86);
			this.ModLbl.Name = "ModLbl";
			this.ModLbl.Size = new System.Drawing.Size(47, 13);
			this.ModLbl.TabIndex = 6;
			this.ModLbl.Text = "Modifier:";
			this.ModBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.ModBox.Location = new Point(134, 83);
			this.ModBox.Name = "ModBox";
			this.ModBox.ReadOnly = true;
			this.ModBox.Size = new System.Drawing.Size(194, 20);
			this.ModBox.TabIndex = 7;
			this.ValLbl.AutoSize = true;
			this.ValLbl.Location = new Point(66, 135);
			this.ValLbl.Name = "ValLbl";
			this.ValLbl.Size = new System.Drawing.Size(62, 13);
			this.ValLbl.TabIndex = 9;
			this.ValLbl.Text = "Final Value:";
			this.HalveBtn.AutoSize = true;
			this.HalveBtn.Location = new Point(134, 109);
			this.HalveBtn.Name = "HalveBtn";
			this.HalveBtn.Size = new System.Drawing.Size(95, 17);
			this.HalveBtn.TabIndex = 8;
			this.HalveBtn.Text = "Halve damage";
			this.HalveBtn.UseVisualStyleBackColor = true;
			this.HalveBtn.CheckedChanged += new EventHandler(this.HalveBtn_CheckedChanged);
			this.TypeLbl.AutoSize = true;
			this.TypeLbl.Location = new Point(66, 57);
			this.TypeLbl.Name = "TypeLbl";
			this.TypeLbl.Size = new System.Drawing.Size(34, 13);
			this.TypeLbl.TabIndex = 4;
			this.TypeLbl.Text = "Type:";
			this.ValBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.ValBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.ValBox.Location = new Point(134, 132);
			this.ValBox.Name = "ValBox";
			this.ValBox.ReadOnly = true;
			this.ValBox.Size = new System.Drawing.Size(194, 26);
			this.ValBox.TabIndex = 10;
			this.ValBox.Text = "[dmg]";
			this.ValBox.TextAlign = HorizontalAlignment.Center;
			ToolStripItemCollection items = this.AmountToolbar.Items;
			ToolStripItem[] dmg1 = new ToolStripItem[] { this.Dmg1, this.Dmg2, this.Dmg5, this.Dmg10, this.Dmg20, this.Dmg50, this.Dmg100, this.toolStripSeparator1, this.ResetBtn };
			items.AddRange(dmg1);
			this.AmountToolbar.Location = new Point(63, 0);
			this.AmountToolbar.Name = "AmountToolbar";
			this.AmountToolbar.Size = new System.Drawing.Size(277, 25);
			this.AmountToolbar.TabIndex = 0;
			this.AmountToolbar.Text = "toolStrip1";
			this.Dmg1.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.Dmg1.Image = (Image)componentResourceManager.GetObject("Dmg1.Image");
			this.Dmg1.ImageTransparentColor = Color.Magenta;
			this.Dmg1.Name = "Dmg1";
			this.Dmg1.Size = new System.Drawing.Size(25, 22);
			this.Dmg1.Text = "+1";
			this.Dmg1.Click += new EventHandler(this.Dmg1_Click);
			this.Dmg2.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.Dmg2.Image = (Image)componentResourceManager.GetObject("Dmg2.Image");
			this.Dmg2.ImageTransparentColor = Color.Magenta;
			this.Dmg2.Name = "Dmg2";
			this.Dmg2.Size = new System.Drawing.Size(25, 22);
			this.Dmg2.Text = "+2";
			this.Dmg2.Click += new EventHandler(this.Dmg2_Click);
			this.Dmg5.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.Dmg5.Image = (Image)componentResourceManager.GetObject("Dmg5.Image");
			this.Dmg5.ImageTransparentColor = Color.Magenta;
			this.Dmg5.Name = "Dmg5";
			this.Dmg5.Size = new System.Drawing.Size(25, 22);
			this.Dmg5.Text = "+5";
			this.Dmg5.Click += new EventHandler(this.Dmg5_Click);
			this.Dmg10.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.Dmg10.Image = (Image)componentResourceManager.GetObject("Dmg10.Image");
			this.Dmg10.ImageTransparentColor = Color.Magenta;
			this.Dmg10.Name = "Dmg10";
			this.Dmg10.Size = new System.Drawing.Size(31, 22);
			this.Dmg10.Text = "+10";
			this.Dmg10.Click += new EventHandler(this.Dmg10_Click);
			this.Dmg20.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.Dmg20.Image = (Image)componentResourceManager.GetObject("Dmg20.Image");
			this.Dmg20.ImageTransparentColor = Color.Magenta;
			this.Dmg20.Name = "Dmg20";
			this.Dmg20.Size = new System.Drawing.Size(31, 22);
			this.Dmg20.Text = "+20";
			this.Dmg20.Click += new EventHandler(this.Dmg20_Click);
			this.Dmg50.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.Dmg50.Image = (Image)componentResourceManager.GetObject("Dmg50.Image");
			this.Dmg50.ImageTransparentColor = Color.Magenta;
			this.Dmg50.Name = "Dmg50";
			this.Dmg50.Size = new System.Drawing.Size(31, 22);
			this.Dmg50.Text = "+50";
			this.Dmg50.Click += new EventHandler(this.Dmg50_Click);
			this.Dmg100.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.Dmg100.Image = (Image)componentResourceManager.GetObject("Dmg100.Image");
			this.Dmg100.ImageTransparentColor = Color.Magenta;
			this.Dmg100.Name = "Dmg100";
			this.Dmg100.Size = new System.Drawing.Size(37, 22);
			this.Dmg100.Text = "+100";
			this.Dmg100.Click += new EventHandler(this.Dmg100_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.ResetBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ResetBtn.Image = (Image)componentResourceManager.GetObject("ResetBtn.Image");
			this.ResetBtn.ImageTransparentColor = Color.Magenta;
			this.ResetBtn.Name = "ResetBtn";
			this.ResetBtn.Size = new System.Drawing.Size(39, 22);
			this.ResetBtn.Text = "Reset";
			this.ResetBtn.Click += new EventHandler(this.ResetBtn_Click);
			this.TypeToolbar.Dock = DockStyle.Left;
			this.TypeToolbar.GripStyle = ToolStripGripStyle.Hidden;
			ToolStripItemCollection toolStripItemCollections = this.TypeToolbar.Items;
			ToolStripItem[] acidBtn = new ToolStripItem[] { this.AcidBtn, this.ColdBtn, this.FireBtn, this.ForceBtn, this.LightningBtn, this.NecroticBtn, this.PoisonBtn, this.PsychicBtn, this.RadiantBtn, this.ThunderBtn };
			toolStripItemCollections.AddRange(acidBtn);
			this.TypeToolbar.Location = new Point(0, 0);
			this.TypeToolbar.Name = "TypeToolbar";
			this.TypeToolbar.Size = new System.Drawing.Size(63, 225);
			this.TypeToolbar.TabIndex = 1;
			this.TypeToolbar.Text = "toolStrip2";
			this.AcidBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AcidBtn.Image = (Image)componentResourceManager.GetObject("AcidBtn.Image");
			this.AcidBtn.ImageTransparentColor = Color.Magenta;
			this.AcidBtn.Name = "AcidBtn";
			this.AcidBtn.Size = new System.Drawing.Size(60, 19);
			this.AcidBtn.Text = "Acid";
			this.AcidBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.AcidBtn.Click += new EventHandler(this.AcidBtn_Click);
			this.ColdBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ColdBtn.Image = (Image)componentResourceManager.GetObject("ColdBtn.Image");
			this.ColdBtn.ImageTransparentColor = Color.Magenta;
			this.ColdBtn.Name = "ColdBtn";
			this.ColdBtn.Size = new System.Drawing.Size(60, 19);
			this.ColdBtn.Text = "Cold";
			this.ColdBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.ColdBtn.Click += new EventHandler(this.ColdBtn_Click);
			this.FireBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.FireBtn.Image = (Image)componentResourceManager.GetObject("FireBtn.Image");
			this.FireBtn.ImageTransparentColor = Color.Magenta;
			this.FireBtn.Name = "FireBtn";
			this.FireBtn.Size = new System.Drawing.Size(60, 19);
			this.FireBtn.Text = "Fire";
			this.FireBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.FireBtn.Click += new EventHandler(this.FireBtn_Click);
			this.ForceBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ForceBtn.Image = (Image)componentResourceManager.GetObject("ForceBtn.Image");
			this.ForceBtn.ImageTransparentColor = Color.Magenta;
			this.ForceBtn.Name = "ForceBtn";
			this.ForceBtn.Size = new System.Drawing.Size(60, 19);
			this.ForceBtn.Text = "Force";
			this.ForceBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.ForceBtn.Click += new EventHandler(this.ForceBtn_Click);
			this.LightningBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LightningBtn.Image = (Image)componentResourceManager.GetObject("LightningBtn.Image");
			this.LightningBtn.ImageTransparentColor = Color.Magenta;
			this.LightningBtn.Name = "LightningBtn";
			this.LightningBtn.Size = new System.Drawing.Size(60, 19);
			this.LightningBtn.Text = "Lightning";
			this.LightningBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.LightningBtn.Click += new EventHandler(this.LightningBtn_Click);
			this.NecroticBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NecroticBtn.Image = (Image)componentResourceManager.GetObject("NecroticBtn.Image");
			this.NecroticBtn.ImageTransparentColor = Color.Magenta;
			this.NecroticBtn.Name = "NecroticBtn";
			this.NecroticBtn.Size = new System.Drawing.Size(60, 19);
			this.NecroticBtn.Text = "Necrotic";
			this.NecroticBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.NecroticBtn.Click += new EventHandler(this.NecroticBtn_Click);
			this.PoisonBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PoisonBtn.Image = (Image)componentResourceManager.GetObject("PoisonBtn.Image");
			this.PoisonBtn.ImageTransparentColor = Color.Magenta;
			this.PoisonBtn.Name = "PoisonBtn";
			this.PoisonBtn.Size = new System.Drawing.Size(60, 19);
			this.PoisonBtn.Text = "Poison";
			this.PoisonBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.PoisonBtn.Click += new EventHandler(this.PoisonBtn_Click);
			this.PsychicBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PsychicBtn.Image = (Image)componentResourceManager.GetObject("PsychicBtn.Image");
			this.PsychicBtn.ImageTransparentColor = Color.Magenta;
			this.PsychicBtn.Name = "PsychicBtn";
			this.PsychicBtn.Size = new System.Drawing.Size(60, 19);
			this.PsychicBtn.Text = "Psychic";
			this.PsychicBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.PsychicBtn.Click += new EventHandler(this.PsychicBtn_Click);
			this.RadiantBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RadiantBtn.Image = (Image)componentResourceManager.GetObject("RadiantBtn.Image");
			this.RadiantBtn.ImageTransparentColor = Color.Magenta;
			this.RadiantBtn.Name = "RadiantBtn";
			this.RadiantBtn.Size = new System.Drawing.Size(60, 19);
			this.RadiantBtn.Text = "Radiant";
			this.RadiantBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.RadiantBtn.Click += new EventHandler(this.RadiantBtn_Click);
			this.ThunderBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ThunderBtn.Image = (Image)componentResourceManager.GetObject("ThunderBtn.Image");
			this.ThunderBtn.ImageTransparentColor = Color.Magenta;
			this.ThunderBtn.Name = "ThunderBtn";
			this.ThunderBtn.Size = new System.Drawing.Size(60, 19);
			this.ThunderBtn.Text = "Thunder";
			this.ThunderBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.ThunderBtn.Click += new EventHandler(this.ThunderBtn_Click);
			this.TypeBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.TypeBox.Location = new Point(134, 54);
			this.TypeBox.Name = "TypeBox";
			this.TypeBox.ReadOnly = true;
			this.TypeBox.Size = new System.Drawing.Size(194, 20);
			this.TypeBox.TabIndex = 5;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(340, 225);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.TypeBox);
			base.Controls.Add(this.DmgBox);
			base.Controls.Add(this.DmgLbl);
			base.Controls.Add(this.ModLbl);
			base.Controls.Add(this.AmountToolbar);
			base.Controls.Add(this.TypeToolbar);
			base.Controls.Add(this.HalveBtn);
			base.Controls.Add(this.TypeLbl);
			base.Controls.Add(this.ModBox);
			base.Controls.Add(this.ValLbl);
			base.Controls.Add(this.ValBox);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DamageForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Damage";
			base.Shown += new EventHandler(this.DamageForm_Shown);
			((ISupportInitialize)this.DmgBox).EndInit();
			this.AmountToolbar.ResumeLayout(false);
			this.AmountToolbar.PerformLayout();
			this.TypeToolbar.ResumeLayout(false);
			this.TypeToolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void LightningBtn_Click(object sender, EventArgs e)
		{
			this.add_type(DamageType.Lightning);
		}

		private void NecroticBtn_Click(object sender, EventArgs e)
		{
			this.add_type(DamageType.Necrotic);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			foreach (DamageForm.Token fDatum in this.fData)
			{
				this.DoDamage(fDatum.Data, fDatum.Card, (int)this.DmgBox.Value, this.fTypes, this.HalveBtn.Checked);
			}
		}

		private void PoisonBtn_Click(object sender, EventArgs e)
		{
			this.add_type(DamageType.Poison);
		}

		private void PsychicBtn_Click(object sender, EventArgs e)
		{
			this.add_type(DamageType.Psychic);
		}

		private void RadiantBtn_Click(object sender, EventArgs e)
		{
			this.add_type(DamageType.Radiant);
		}

		private void ResetBtn_Click(object sender, EventArgs e)
		{
			this.DmgBox.Value = new decimal(0);
		}

		private void ThunderBtn_Click(object sender, EventArgs e)
		{
			this.add_type(DamageType.Thunder);
		}

		private void update_modifier()
		{
			foreach (DamageForm.Token fDatum in this.fData)
			{
				if (fDatum.Card == null)
				{
					continue;
				}
				fDatum.Modifier = fDatum.Card.GetDamageModifier(this.fTypes, fDatum.Data);
			}
			if (this.fData.Count != 1)
			{
				this.ModBox.Text = "(multiple tokens)";
				return;
			}
			DamageForm.Token item = this.fData[0];
			if (item.Modifier == -2147483648)
			{
				this.ModBox.Text = "Immune";
				return;
			}
			if (item.Modifier > 0)
			{
				this.ModBox.Text = string.Concat("Vulnerable ", item.Modifier);
				return;
			}
			if (item.Modifier >= 0)
			{
				this.ModBox.Text = "(none)";
				return;
			}
			this.ModBox.Text = string.Concat("Resist ", Math.Abs(item.Modifier));
		}

		private void update_type()
		{
			string str = "";
			foreach (DamageType fType in this.fTypes)
			{
				if (str != "")
				{
					str = string.Concat(str, ", ");
				}
				str = string.Concat(str, fType.ToString());
			}
			if (str == "")
			{
				str = "(untyped)";
			}
			this.TypeBox.Text = str;
		}

		private void update_value()
		{
			if (this.fData.Count != 1)
			{
				this.ValBox.Text = "(multiple tokens)";
				return;
			}
			int _value = DamageForm.get_value((int)this.DmgBox.Value, this.fData[0].Modifier, this.HalveBtn.Checked);
			this.ValBox.Text = _value.ToString();
		}

		public class Token
		{
			public CombatData Data;

			public EncounterCard Card;

			public int Modifier;

			public Token(CombatData data, EncounterCard card)
			{
				this.Data = data;
				this.Card = card;
			}
		}
	}
}