using Masterplan;
using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class NPCForm : Form
	{
		private Masterplan.Data.NPC fNPC;

		private bool fUpdating;

		private Button OKBtn;

		private Button CancelBtn;

		private Label NameLbl;

		private TextBox NameBox;

		private Label LevelLbl;

		private NumericUpDown LevelBox;

		private Label TemplateLbl;

		private Label StrLbl;

		private NumericUpDown StrBox;

		private NumericUpDown ConBox;

		private Label ConLbl;

		private NumericUpDown DexBox;

		private Label DexLbl;

		private NumericUpDown IntBox;

		private Label IntLbl;

		private NumericUpDown WisBox;

		private Label WisLbl;

		private NumericUpDown ChaBox;

		private Label ChaLbl;

		private Label InitLbl;

		private NumericUpDown InitModBox;

		private TextBox InitBox;

		private TextBox HPBox;

		private NumericUpDown HPModBox;

		private Label HPLbl;

		private TextBox ACBox;

		private NumericUpDown ACModBox;

		private Label ACLbl;

		private TextBox FortBox;

		private NumericUpDown FortModBox;

		private Label FortLbl;

		private TextBox RefBox;

		private NumericUpDown RefModBox;

		private Label RefLbl;

		private TextBox WillBox;

		private NumericUpDown WillModBox;

		private Label WillLbl;

		private TextBox ChaModBox;

		private TextBox WisModBox;

		private TextBox IntModBox;

		private TextBox DexModBox;

		private TextBox ConModBox;

		private TextBox StrModBox;

		private TabControl Pages;

		private TabPage CombatPage;

		private TabPage AbilitiesPage;

		private TabPage DetailsPage;

		private TabPage DamagePage;

		private ListView DamageList;

		private ColumnHeader DmgModHdr;

		private ToolStrip DamageToolbar;

		private ToolStripButton AddDmgBtn;

		private ToolStripButton RemoveDmgBtn;

		private ToolStripButton EditDmgBtn;

		private ComboBox TemplateBox;

		private TextBox CostBox;

		private Label CostLbl;

		private TabPage PowersPage;

		private ListView PowerList;

		private ColumnHeader NameHdr;

		private ToolStrip PowerToolbar;

		private ToolStripButton PowerRemoveBtn;

		private ToolStripButton PowerEditBtn;

		private TabPage AuraPage;

		private ListView AuraList;

		private ColumnHeader AuraHdr;

		private ToolStrip AuraToolbar;

		private ToolStripButton AuraAddBtn;

		private ToolStripButton AuraRemoveBtn;

		private ToolStripButton AuraEditBtn;

		private ListView DetailsList;

		private ColumnHeader AttributeHdr;

		private ColumnHeader ValueHdr;

		private ToolStrip DetailsToolbar;

		private ToolStripButton DetailsEditBtn;

		private TabPage PicturePage;

		private PictureBox PortraitBox;

		private ToolStrip PortraitToolbar;

		private ToolStripButton PortraitBrowse;

		private ToolStripButton PortraitClear;

		private Button InfoBtn;

		private Label InfoLbl;

		private ToolStripSplitButton PowerAddBtn;

		private ToolStripMenuItem PowerBrowse;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton PowerUpBtn;

		private ToolStripButton PowerDownBtn;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton RegenBtn;

		private ToolStripLabel ClearRegenLbl;

		public Masterplan.Data.NPC NPC
		{
			get
			{
				return this.fNPC;
			}
		}

		public Aura SelectedAura
		{
			get
			{
				if (this.AuraList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.AuraList.SelectedItems[0].Tag as Aura;
			}
		}

		public DamageModifier SelectedDamageMod
		{
			get
			{
				if (this.DamageList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.DamageList.SelectedItems[0].Tag as DamageModifier;
			}
		}

		public DetailsField SelectedField
		{
			get
			{
				if (this.DetailsList.SelectedItems.Count == 0)
				{
					return DetailsField.None;
				}
				return (DetailsField)this.DetailsList.SelectedItems[0].Tag;
			}
		}

		public CreaturePower SelectedPower
		{
			get
			{
				if (this.PowerList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.PowerList.SelectedItems[0].Tag as CreaturePower;
			}
		}

		public NPCForm(Masterplan.Data.NPC cc, bool unused)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			List<Guid> guids = new List<Guid>();
			foreach (CreatureTemplate template in Session.Templates)
			{
				if (template.Type != CreatureTemplateType.Class)
				{
					continue;
				}
				guids.Add(template.ID);
			}
			foreach (CreatureTemplate creatureTemplate in Session.Project.Library.Templates)
			{
				if (creatureTemplate.Type != CreatureTemplateType.Class || guids.Contains(creatureTemplate.ID))
				{
					continue;
				}
				guids.Add(creatureTemplate.ID);
			}
			foreach (Guid guid in guids)
			{
				CreatureTemplate creatureTemplate1 = Session.FindTemplate(guid, SearchType.Global);
				this.TemplateBox.Items.Add(creatureTemplate1);
			}
			this.fNPC = cc.Copy();
			this.fUpdating = true;
			this.NameBox.Text = this.fNPC.Name;
			this.LevelBox.Value = this.fNPC.Level;
			this.InfoBtn.Text = this.fNPC.Phenotype;
			CreatureTemplate creatureTemplate2 = Session.FindTemplate(this.fNPC.TemplateID, SearchType.Global);
			if (creatureTemplate2 == null)
			{
				this.TemplateBox.SelectedIndex = 0;
			}
			else
			{
				this.TemplateBox.SelectedItem = creatureTemplate2;
			}
			this.StrBox.Value = this.fNPC.Strength.Score;
			this.ConBox.Value = this.fNPC.Constitution.Score;
			this.DexBox.Value = this.fNPC.Dexterity.Score;
			this.IntBox.Value = this.fNPC.Intelligence.Score;
			this.WisBox.Value = this.fNPC.Wisdom.Score;
			this.ChaBox.Value = this.fNPC.Charisma.Score;
			this.InitModBox.Value = this.fNPC.InitiativeModifier;
			this.HPModBox.Value = this.fNPC.HPModifier;
			this.ACModBox.Value = this.fNPC.ACModifier;
			this.FortModBox.Value = this.fNPC.FortitudeModifier;
			this.RefModBox.Value = this.fNPC.ReflexModifier;
			this.WillModBox.Value = this.fNPC.WillModifier;
			this.fUpdating = false;
			this.update_fields();
			this.update_powers_list();
			this.update_aura_list();
			this.update_damage_list();
			this.update_details();
			if (this.fNPC.Image != null)
			{
				this.PortraitBox.Image = this.fNPC.Image;
			}
		}

		private void add_power(CreaturePower power)
		{
			this.fNPC.CreaturePowers.Add(power);
			this.update_powers_list();
		}

		private void AddDmgBtn_Click(object sender, EventArgs e)
		{
			DamageModifierForm damageModifierForm = new DamageModifierForm(new DamageModifier());
			if (damageModifierForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fNPC.DamageModifiers.Add(damageModifierForm.Modifier);
				this.update_damage_list();
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.PowerRemoveBtn.Enabled = this.SelectedPower != null;
			this.PowerEditBtn.Enabled = this.SelectedPower != null;
			this.PowerUpBtn.Enabled = (this.SelectedPower == null ? false : this.fNPC.CreaturePowers.IndexOf(this.SelectedPower) != 0);
			this.PowerDownBtn.Enabled = (this.SelectedPower == null ? false : this.fNPC.CreaturePowers.IndexOf(this.SelectedPower) != this.fNPC.CreaturePowers.Count - 1);
			this.AuraRemoveBtn.Enabled = this.SelectedAura != null;
			this.AuraEditBtn.Enabled = this.SelectedAura != null;
			this.RemoveDmgBtn.Enabled = this.SelectedDamageMod != null;
			this.EditDmgBtn.Enabled = this.SelectedDamageMod != null;
			this.ClearRegenLbl.Visible = this.fNPC.Regeneration != null;
			this.DetailsEditBtn.Enabled = this.SelectedField != DetailsField.None;
		}

		private void AuraAddBtn_Click(object sender, EventArgs e)
		{
			AuraForm auraForm = new AuraForm(new Aura()
			{
				Name = "New Aura"
			});
			if (auraForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fNPC.Auras.Add(auraForm.Aura);
				this.update_aura_list();
			}
		}

		private void AuraEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedAura != null)
			{
				int aura = this.fNPC.Auras.IndexOf(this.SelectedAura);
				AuraForm auraForm = new AuraForm(this.SelectedAura);
				if (auraForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fNPC.Auras[aura] = auraForm.Aura;
					this.update_aura_list();
				}
			}
		}

		private void AuraRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedAura != null)
			{
				this.fNPC.Auras.Remove(this.SelectedAura);
				this.update_aura_list();
			}
		}

		private void ClearRegenLbl_Click(object sender, EventArgs e)
		{
			this.fNPC.Regeneration = null;
		}

		private void DetailsEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedField != DetailsField.None && (new DetailsForm(this.fNPC, this.SelectedField, null)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.update_details();
			}
		}

		private void EditDmgBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedDamageMod != null)
			{
				int modifier = this.fNPC.DamageModifiers.IndexOf(this.SelectedDamageMod);
				DamageModifierForm damageModifierForm = new DamageModifierForm(this.SelectedDamageMod);
				if (damageModifierForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fNPC.DamageModifiers[modifier] = damageModifierForm.Modifier;
					this.update_damage_list();
				}
			}
		}

		private void image_changed()
		{
			this.PortraitBox.Image = this.fNPC.Image;
		}

		private void InfoBtn_Click(object sender, EventArgs e)
		{
			CreatureClassForm creatureClassForm = new CreatureClassForm(this.fNPC);
			if (creatureClassForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fNPC.Size = creatureClassForm.CreatureSize;
				this.fNPC.Origin = creatureClassForm.Origin;
				this.fNPC.Type = creatureClassForm.Type;
				this.fNPC.Keywords = creatureClassForm.Keywords;
				this.InfoBtn.Text = this.fNPC.Phenotype;
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(NPCForm));
			ListViewGroup listViewGroup = new ListViewGroup("Immunities", HorizontalAlignment.Left);
			ListViewGroup listViewGroup1 = new ListViewGroup("Resistances", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Vulnerabilities", HorizontalAlignment.Left);
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.LevelLbl = new Label();
			this.LevelBox = new NumericUpDown();
			this.TemplateLbl = new Label();
			this.StrLbl = new Label();
			this.StrBox = new NumericUpDown();
			this.ConBox = new NumericUpDown();
			this.ConLbl = new Label();
			this.DexBox = new NumericUpDown();
			this.DexLbl = new Label();
			this.IntBox = new NumericUpDown();
			this.IntLbl = new Label();
			this.WisBox = new NumericUpDown();
			this.WisLbl = new Label();
			this.ChaBox = new NumericUpDown();
			this.ChaLbl = new Label();
			this.InitLbl = new Label();
			this.InitModBox = new NumericUpDown();
			this.InitBox = new TextBox();
			this.HPBox = new TextBox();
			this.HPModBox = new NumericUpDown();
			this.HPLbl = new Label();
			this.ACBox = new TextBox();
			this.ACModBox = new NumericUpDown();
			this.ACLbl = new Label();
			this.FortBox = new TextBox();
			this.FortModBox = new NumericUpDown();
			this.FortLbl = new Label();
			this.RefBox = new TextBox();
			this.RefModBox = new NumericUpDown();
			this.RefLbl = new Label();
			this.WillBox = new TextBox();
			this.WillModBox = new NumericUpDown();
			this.WillLbl = new Label();
			this.ChaModBox = new TextBox();
			this.WisModBox = new TextBox();
			this.IntModBox = new TextBox();
			this.DexModBox = new TextBox();
			this.ConModBox = new TextBox();
			this.StrModBox = new TextBox();
			this.Pages = new TabControl();
			this.AbilitiesPage = new TabPage();
			this.CostBox = new TextBox();
			this.CostLbl = new Label();
			this.CombatPage = new TabPage();
			this.PowersPage = new TabPage();
			this.PowerList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.PowerToolbar = new ToolStrip();
			this.PowerAddBtn = new ToolStripSplitButton();
			this.PowerBrowse = new ToolStripMenuItem();
			this.PowerRemoveBtn = new ToolStripButton();
			this.PowerEditBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.PowerUpBtn = new ToolStripButton();
			this.PowerDownBtn = new ToolStripButton();
			this.AuraPage = new TabPage();
			this.AuraList = new ListView();
			this.AuraHdr = new ColumnHeader();
			this.AuraToolbar = new ToolStrip();
			this.AuraAddBtn = new ToolStripButton();
			this.AuraRemoveBtn = new ToolStripButton();
			this.AuraEditBtn = new ToolStripButton();
			this.DamagePage = new TabPage();
			this.DamageList = new ListView();
			this.DmgModHdr = new ColumnHeader();
			this.DamageToolbar = new ToolStrip();
			this.AddDmgBtn = new ToolStripButton();
			this.RemoveDmgBtn = new ToolStripButton();
			this.EditDmgBtn = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.RegenBtn = new ToolStripButton();
			this.ClearRegenLbl = new ToolStripLabel();
			this.DetailsPage = new TabPage();
			this.DetailsList = new ListView();
			this.AttributeHdr = new ColumnHeader();
			this.ValueHdr = new ColumnHeader();
			this.DetailsToolbar = new ToolStrip();
			this.DetailsEditBtn = new ToolStripButton();
			this.PicturePage = new TabPage();
			this.PortraitBox = new PictureBox();
			this.PortraitToolbar = new ToolStrip();
			this.PortraitBrowse = new ToolStripButton();
			this.PortraitClear = new ToolStripButton();
			this.TemplateBox = new ComboBox();
			this.InfoBtn = new Button();
			this.InfoLbl = new Label();
			((ISupportInitialize)this.LevelBox).BeginInit();
			((ISupportInitialize)this.StrBox).BeginInit();
			((ISupportInitialize)this.ConBox).BeginInit();
			((ISupportInitialize)this.DexBox).BeginInit();
			((ISupportInitialize)this.IntBox).BeginInit();
			((ISupportInitialize)this.WisBox).BeginInit();
			((ISupportInitialize)this.ChaBox).BeginInit();
			((ISupportInitialize)this.InitModBox).BeginInit();
			((ISupportInitialize)this.HPModBox).BeginInit();
			((ISupportInitialize)this.ACModBox).BeginInit();
			((ISupportInitialize)this.FortModBox).BeginInit();
			((ISupportInitialize)this.RefModBox).BeginInit();
			((ISupportInitialize)this.WillModBox).BeginInit();
			this.Pages.SuspendLayout();
			this.AbilitiesPage.SuspendLayout();
			this.CombatPage.SuspendLayout();
			this.PowersPage.SuspendLayout();
			this.PowerToolbar.SuspendLayout();
			this.AuraPage.SuspendLayout();
			this.AuraToolbar.SuspendLayout();
			this.DamagePage.SuspendLayout();
			this.DamageToolbar.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			this.DetailsToolbar.SuspendLayout();
			this.PicturePage.SuspendLayout();
			((ISupportInitialize)this.PortraitBox).BeginInit();
			this.PortraitToolbar.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(241, 345);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 11;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(322, 345);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 12;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new System.Drawing.Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.NameBox.Location = new Point(72, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(325, 20);
			this.NameBox.TabIndex = 1;
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(12, 40);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new System.Drawing.Size(36, 13);
			this.LevelLbl.TabIndex = 2;
			this.LevelLbl.Text = "Level:";
			this.LevelBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.LevelBox.Location = new Point(72, 38);
			NumericUpDown levelBox = this.LevelBox;
			int[] numArray = new int[] { 40, 0, 0, 0 };
			levelBox.Maximum = new decimal(numArray);
			NumericUpDown num = this.LevelBox;
			int[] numArray1 = new int[] { 1, 0, 0, 0 };
			num.Minimum = new decimal(numArray1);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.Size = new System.Drawing.Size(325, 20);
			this.LevelBox.TabIndex = 3;
			NumericUpDown numericUpDown = this.LevelBox;
			int[] numArray2 = new int[] { 1, 0, 0, 0 };
			numericUpDown.Value = new decimal(numArray2);
			this.LevelBox.ValueChanged += new EventHandler(this.ParameterChanged);
			this.TemplateLbl.AutoSize = true;
			this.TemplateLbl.Location = new Point(12, 67);
			this.TemplateLbl.Name = "TemplateLbl";
			this.TemplateLbl.Size = new System.Drawing.Size(54, 13);
			this.TemplateLbl.TabIndex = 4;
			this.TemplateLbl.Text = "Template:";
			this.StrLbl.AutoSize = true;
			this.StrLbl.Location = new Point(6, 9);
			this.StrLbl.Name = "StrLbl";
			this.StrLbl.Size = new System.Drawing.Size(50, 13);
			this.StrLbl.TabIndex = 0;
			this.StrLbl.Text = "Strength:";
			this.StrBox.Location = new Point(77, 6);
			this.StrBox.Name = "StrBox";
			this.StrBox.Size = new System.Drawing.Size(100, 20);
			this.StrBox.TabIndex = 1;
			this.StrBox.ValueChanged += new EventHandler(this.ParameterChanged);
			this.ConBox.Location = new Point(77, 32);
			this.ConBox.Name = "ConBox";
			this.ConBox.Size = new System.Drawing.Size(100, 20);
			this.ConBox.TabIndex = 4;
			this.ConBox.ValueChanged += new EventHandler(this.ParameterChanged);
			this.ConLbl.AutoSize = true;
			this.ConLbl.Location = new Point(6, 35);
			this.ConLbl.Name = "ConLbl";
			this.ConLbl.Size = new System.Drawing.Size(65, 13);
			this.ConLbl.TabIndex = 3;
			this.ConLbl.Text = "Constitution:";
			this.DexBox.Location = new Point(77, 58);
			this.DexBox.Name = "DexBox";
			this.DexBox.Size = new System.Drawing.Size(100, 20);
			this.DexBox.TabIndex = 7;
			this.DexBox.ValueChanged += new EventHandler(this.ParameterChanged);
			this.DexLbl.AutoSize = true;
			this.DexLbl.Location = new Point(6, 61);
			this.DexLbl.Name = "DexLbl";
			this.DexLbl.Size = new System.Drawing.Size(51, 13);
			this.DexLbl.TabIndex = 6;
			this.DexLbl.Text = "Dexterity:";
			this.IntBox.Location = new Point(77, 84);
			this.IntBox.Name = "IntBox";
			this.IntBox.Size = new System.Drawing.Size(100, 20);
			this.IntBox.TabIndex = 10;
			this.IntBox.ValueChanged += new EventHandler(this.ParameterChanged);
			this.IntLbl.AutoSize = true;
			this.IntLbl.Location = new Point(6, 87);
			this.IntLbl.Name = "IntLbl";
			this.IntLbl.Size = new System.Drawing.Size(64, 13);
			this.IntLbl.TabIndex = 9;
			this.IntLbl.Text = "Intelligence:";
			this.WisBox.Location = new Point(77, 110);
			this.WisBox.Name = "WisBox";
			this.WisBox.Size = new System.Drawing.Size(100, 20);
			this.WisBox.TabIndex = 13;
			this.WisBox.ValueChanged += new EventHandler(this.ParameterChanged);
			this.WisLbl.AutoSize = true;
			this.WisLbl.Location = new Point(6, 113);
			this.WisLbl.Name = "WisLbl";
			this.WisLbl.Size = new System.Drawing.Size(48, 13);
			this.WisLbl.TabIndex = 12;
			this.WisLbl.Text = "Wisdom:";
			this.ChaBox.Location = new Point(77, 136);
			this.ChaBox.Name = "ChaBox";
			this.ChaBox.Size = new System.Drawing.Size(100, 20);
			this.ChaBox.TabIndex = 16;
			this.ChaBox.ValueChanged += new EventHandler(this.ParameterChanged);
			this.ChaLbl.AutoSize = true;
			this.ChaLbl.Location = new Point(6, 139);
			this.ChaLbl.Name = "ChaLbl";
			this.ChaLbl.Size = new System.Drawing.Size(53, 13);
			this.ChaLbl.TabIndex = 15;
			this.ChaLbl.Text = "Charisma:";
			this.InitLbl.AutoSize = true;
			this.InitLbl.Location = new Point(6, 9);
			this.InitLbl.Name = "InitLbl";
			this.InitLbl.Size = new System.Drawing.Size(49, 13);
			this.InitLbl.TabIndex = 0;
			this.InitLbl.Text = "Initiative:";
			this.InitModBox.Location = new Point(77, 6);
			NumericUpDown initModBox = this.InitModBox;
			int[] numArray3 = new int[] { 1000, 0, 0, 0 };
			initModBox.Maximum = new decimal(numArray3);
			NumericUpDown initModBox1 = this.InitModBox;
			int[] numArray4 = new int[] { 1000, 0, 0, -2147483648 };
			initModBox1.Minimum = new decimal(numArray4);
			this.InitModBox.Name = "InitModBox";
			this.InitModBox.Size = new System.Drawing.Size(100, 20);
			this.InitModBox.TabIndex = 1;
			this.InitModBox.ValueChanged += new EventHandler(this.ParameterChanged);
			this.InitBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.InitBox.Location = new Point(183, 6);
			this.InitBox.Name = "InitBox";
			this.InitBox.ReadOnly = true;
			this.InitBox.Size = new System.Drawing.Size(188, 20);
			this.InitBox.TabIndex = 2;
			this.InitBox.TabStop = false;
			this.InitBox.Text = "[init]";
			this.InitBox.TextAlign = HorizontalAlignment.Center;
			this.HPBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.HPBox.Location = new Point(183, 32);
			this.HPBox.Name = "HPBox";
			this.HPBox.ReadOnly = true;
			this.HPBox.Size = new System.Drawing.Size(188, 20);
			this.HPBox.TabIndex = 8;
			this.HPBox.TabStop = false;
			this.HPBox.Text = "[hp]";
			this.HPBox.TextAlign = HorizontalAlignment.Center;
			this.HPModBox.Location = new Point(77, 32);
			NumericUpDown hPModBox = this.HPModBox;
			int[] numArray5 = new int[] { 1000, 0, 0, 0 };
			hPModBox.Maximum = new decimal(numArray5);
			NumericUpDown hPModBox1 = this.HPModBox;
			int[] numArray6 = new int[] { 1000, 0, 0, -2147483648 };
			hPModBox1.Minimum = new decimal(numArray6);
			this.HPModBox.Name = "HPModBox";
			this.HPModBox.Size = new System.Drawing.Size(100, 20);
			this.HPModBox.TabIndex = 7;
			this.HPModBox.ValueChanged += new EventHandler(this.ParameterChanged);
			this.HPLbl.AutoSize = true;
			this.HPLbl.Location = new Point(6, 35);
			this.HPLbl.Name = "HPLbl";
			this.HPLbl.Size = new System.Drawing.Size(55, 13);
			this.HPLbl.TabIndex = 6;
			this.HPLbl.Text = "Hit Points:";
			this.ACBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.ACBox.Location = new Point(183, 58);
			this.ACBox.Name = "ACBox";
			this.ACBox.ReadOnly = true;
			this.ACBox.Size = new System.Drawing.Size(188, 20);
			this.ACBox.TabIndex = 11;
			this.ACBox.TabStop = false;
			this.ACBox.Text = "[ac]";
			this.ACBox.TextAlign = HorizontalAlignment.Center;
			this.ACModBox.Location = new Point(77, 58);
			NumericUpDown aCModBox = this.ACModBox;
			int[] numArray7 = new int[] { 1000, 0, 0, 0 };
			aCModBox.Maximum = new decimal(numArray7);
			NumericUpDown aCModBox1 = this.ACModBox;
			int[] numArray8 = new int[] { 1000, 0, 0, -2147483648 };
			aCModBox1.Minimum = new decimal(numArray8);
			this.ACModBox.Name = "ACModBox";
			this.ACModBox.Size = new System.Drawing.Size(100, 20);
			this.ACModBox.TabIndex = 10;
			this.ACModBox.ValueChanged += new EventHandler(this.ParameterChanged);
			this.ACLbl.AutoSize = true;
			this.ACLbl.Location = new Point(6, 61);
			this.ACLbl.Name = "ACLbl";
			this.ACLbl.Size = new System.Drawing.Size(24, 13);
			this.ACLbl.TabIndex = 9;
			this.ACLbl.Text = "AC:";
			this.FortBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.FortBox.Location = new Point(183, 84);
			this.FortBox.Name = "FortBox";
			this.FortBox.ReadOnly = true;
			this.FortBox.Size = new System.Drawing.Size(188, 20);
			this.FortBox.TabIndex = 14;
			this.FortBox.TabStop = false;
			this.FortBox.Text = "[fort]";
			this.FortBox.TextAlign = HorizontalAlignment.Center;
			this.FortModBox.Location = new Point(77, 84);
			NumericUpDown fortModBox = this.FortModBox;
			int[] numArray9 = new int[] { 1000, 0, 0, 0 };
			fortModBox.Maximum = new decimal(numArray9);
			NumericUpDown fortModBox1 = this.FortModBox;
			int[] numArray10 = new int[] { 1000, 0, 0, -2147483648 };
			fortModBox1.Minimum = new decimal(numArray10);
			this.FortModBox.Name = "FortModBox";
			this.FortModBox.Size = new System.Drawing.Size(100, 20);
			this.FortModBox.TabIndex = 13;
			this.FortModBox.ValueChanged += new EventHandler(this.ParameterChanged);
			this.FortLbl.AutoSize = true;
			this.FortLbl.Location = new Point(6, 87);
			this.FortLbl.Name = "FortLbl";
			this.FortLbl.Size = new System.Drawing.Size(51, 13);
			this.FortLbl.TabIndex = 12;
			this.FortLbl.Text = "Fortitude:";
			this.RefBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.RefBox.Location = new Point(183, 110);
			this.RefBox.Name = "RefBox";
			this.RefBox.ReadOnly = true;
			this.RefBox.Size = new System.Drawing.Size(188, 20);
			this.RefBox.TabIndex = 17;
			this.RefBox.TabStop = false;
			this.RefBox.Text = "[ref]";
			this.RefBox.TextAlign = HorizontalAlignment.Center;
			this.RefModBox.Location = new Point(77, 110);
			NumericUpDown refModBox = this.RefModBox;
			int[] numArray11 = new int[] { 1000, 0, 0, 0 };
			refModBox.Maximum = new decimal(numArray11);
			NumericUpDown refModBox1 = this.RefModBox;
			int[] numArray12 = new int[] { 1000, 0, 0, -2147483648 };
			refModBox1.Minimum = new decimal(numArray12);
			this.RefModBox.Name = "RefModBox";
			this.RefModBox.Size = new System.Drawing.Size(100, 20);
			this.RefModBox.TabIndex = 16;
			this.RefModBox.ValueChanged += new EventHandler(this.ParameterChanged);
			this.RefLbl.AutoSize = true;
			this.RefLbl.Location = new Point(6, 113);
			this.RefLbl.Name = "RefLbl";
			this.RefLbl.Size = new System.Drawing.Size(40, 13);
			this.RefLbl.TabIndex = 15;
			this.RefLbl.Text = "Reflex:";
			this.WillBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.WillBox.Location = new Point(183, 136);
			this.WillBox.Name = "WillBox";
			this.WillBox.ReadOnly = true;
			this.WillBox.Size = new System.Drawing.Size(188, 20);
			this.WillBox.TabIndex = 20;
			this.WillBox.TabStop = false;
			this.WillBox.Text = "[will]";
			this.WillBox.TextAlign = HorizontalAlignment.Center;
			this.WillModBox.Location = new Point(77, 136);
			NumericUpDown willModBox = this.WillModBox;
			int[] numArray13 = new int[] { 1000, 0, 0, 0 };
			willModBox.Maximum = new decimal(numArray13);
			NumericUpDown willModBox1 = this.WillModBox;
			int[] numArray14 = new int[] { 1000, 0, 0, -2147483648 };
			willModBox1.Minimum = new decimal(numArray14);
			this.WillModBox.Name = "WillModBox";
			this.WillModBox.Size = new System.Drawing.Size(100, 20);
			this.WillModBox.TabIndex = 19;
			this.WillModBox.ValueChanged += new EventHandler(this.ParameterChanged);
			this.WillLbl.AutoSize = true;
			this.WillLbl.Location = new Point(6, 139);
			this.WillLbl.Name = "WillLbl";
			this.WillLbl.Size = new System.Drawing.Size(27, 13);
			this.WillLbl.TabIndex = 18;
			this.WillLbl.Text = "Will:";
			this.ChaModBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.ChaModBox.Location = new Point(183, 136);
			this.ChaModBox.Name = "ChaModBox";
			this.ChaModBox.ReadOnly = true;
			this.ChaModBox.Size = new System.Drawing.Size(188, 20);
			this.ChaModBox.TabIndex = 17;
			this.ChaModBox.TabStop = false;
			this.ChaModBox.Text = "[cha]";
			this.ChaModBox.TextAlign = HorizontalAlignment.Center;
			this.WisModBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.WisModBox.Location = new Point(183, 110);
			this.WisModBox.Name = "WisModBox";
			this.WisModBox.ReadOnly = true;
			this.WisModBox.Size = new System.Drawing.Size(188, 20);
			this.WisModBox.TabIndex = 14;
			this.WisModBox.TabStop = false;
			this.WisModBox.Text = "[wis]";
			this.WisModBox.TextAlign = HorizontalAlignment.Center;
			this.IntModBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.IntModBox.Location = new Point(183, 84);
			this.IntModBox.Name = "IntModBox";
			this.IntModBox.ReadOnly = true;
			this.IntModBox.Size = new System.Drawing.Size(188, 20);
			this.IntModBox.TabIndex = 11;
			this.IntModBox.TabStop = false;
			this.IntModBox.Text = "[int]";
			this.IntModBox.TextAlign = HorizontalAlignment.Center;
			this.DexModBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.DexModBox.Location = new Point(183, 58);
			this.DexModBox.Name = "DexModBox";
			this.DexModBox.ReadOnly = true;
			this.DexModBox.Size = new System.Drawing.Size(188, 20);
			this.DexModBox.TabIndex = 8;
			this.DexModBox.TabStop = false;
			this.DexModBox.Text = "[dex]";
			this.DexModBox.TextAlign = HorizontalAlignment.Center;
			this.ConModBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.ConModBox.Location = new Point(183, 32);
			this.ConModBox.Name = "ConModBox";
			this.ConModBox.ReadOnly = true;
			this.ConModBox.Size = new System.Drawing.Size(188, 20);
			this.ConModBox.TabIndex = 5;
			this.ConModBox.TabStop = false;
			this.ConModBox.Text = "[con]";
			this.ConModBox.TextAlign = HorizontalAlignment.Center;
			this.StrModBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.StrModBox.Location = new Point(183, 6);
			this.StrModBox.Name = "StrModBox";
			this.StrModBox.ReadOnly = true;
			this.StrModBox.Size = new System.Drawing.Size(188, 20);
			this.StrModBox.TabIndex = 2;
			this.StrModBox.TabStop = false;
			this.StrModBox.Text = "[str]";
			this.StrModBox.TextAlign = HorizontalAlignment.Center;
			this.Pages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Pages.Controls.Add(this.AbilitiesPage);
			this.Pages.Controls.Add(this.CombatPage);
			this.Pages.Controls.Add(this.PowersPage);
			this.Pages.Controls.Add(this.AuraPage);
			this.Pages.Controls.Add(this.DamagePage);
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Controls.Add(this.PicturePage);
			this.Pages.Location = new Point(12, 120);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new System.Drawing.Size(385, 219);
			this.Pages.TabIndex = 10;
			this.AbilitiesPage.Controls.Add(this.CostBox);
			this.AbilitiesPage.Controls.Add(this.CostLbl);
			this.AbilitiesPage.Controls.Add(this.StrModBox);
			this.AbilitiesPage.Controls.Add(this.StrBox);
			this.AbilitiesPage.Controls.Add(this.ChaModBox);
			this.AbilitiesPage.Controls.Add(this.ChaBox);
			this.AbilitiesPage.Controls.Add(this.StrLbl);
			this.AbilitiesPage.Controls.Add(this.ChaLbl);
			this.AbilitiesPage.Controls.Add(this.WisModBox);
			this.AbilitiesPage.Controls.Add(this.WisBox);
			this.AbilitiesPage.Controls.Add(this.WisLbl);
			this.AbilitiesPage.Controls.Add(this.IntModBox);
			this.AbilitiesPage.Controls.Add(this.IntBox);
			this.AbilitiesPage.Controls.Add(this.ConLbl);
			this.AbilitiesPage.Controls.Add(this.IntLbl);
			this.AbilitiesPage.Controls.Add(this.DexModBox);
			this.AbilitiesPage.Controls.Add(this.DexBox);
			this.AbilitiesPage.Controls.Add(this.ConBox);
			this.AbilitiesPage.Controls.Add(this.DexLbl);
			this.AbilitiesPage.Controls.Add(this.ConModBox);
			this.AbilitiesPage.Location = new Point(4, 22);
			this.AbilitiesPage.Name = "AbilitiesPage";
			this.AbilitiesPage.Padding = new System.Windows.Forms.Padding(3);
			this.AbilitiesPage.Size = new System.Drawing.Size(377, 193);
			this.AbilitiesPage.TabIndex = 2;
			this.AbilitiesPage.Text = "Abilities";
			this.AbilitiesPage.UseVisualStyleBackColor = true;
			this.CostBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.CostBox.Location = new Point(77, 162);
			this.CostBox.Name = "CostBox";
			this.CostBox.ReadOnly = true;
			this.CostBox.Size = new System.Drawing.Size(294, 20);
			this.CostBox.TabIndex = 19;
			this.CostBox.TabStop = false;
			this.CostBox.Text = "[cost]";
			this.CostBox.TextAlign = HorizontalAlignment.Center;
			this.CostLbl.AutoSize = true;
			this.CostLbl.Location = new Point(6, 165);
			this.CostLbl.Name = "CostLbl";
			this.CostLbl.Size = new System.Drawing.Size(31, 13);
			this.CostLbl.TabIndex = 18;
			this.CostLbl.Text = "Cost:";
			this.CombatPage.Controls.Add(this.InitBox);
			this.CombatPage.Controls.Add(this.InitLbl);
			this.CombatPage.Controls.Add(this.FortLbl);
			this.CombatPage.Controls.Add(this.WillBox);
			this.CombatPage.Controls.Add(this.FortModBox);
			this.CombatPage.Controls.Add(this.InitModBox);
			this.CombatPage.Controls.Add(this.ACBox);
			this.CombatPage.Controls.Add(this.WillModBox);
			this.CombatPage.Controls.Add(this.FortBox);
			this.CombatPage.Controls.Add(this.HPLbl);
			this.CombatPage.Controls.Add(this.ACModBox);
			this.CombatPage.Controls.Add(this.WillLbl);
			this.CombatPage.Controls.Add(this.RefLbl);
			this.CombatPage.Controls.Add(this.HPModBox);
			this.CombatPage.Controls.Add(this.ACLbl);
			this.CombatPage.Controls.Add(this.RefBox);
			this.CombatPage.Controls.Add(this.RefModBox);
			this.CombatPage.Controls.Add(this.HPBox);
			this.CombatPage.Location = new Point(4, 22);
			this.CombatPage.Name = "CombatPage";
			this.CombatPage.Padding = new System.Windows.Forms.Padding(3);
			this.CombatPage.Size = new System.Drawing.Size(377, 193);
			this.CombatPage.TabIndex = 0;
			this.CombatPage.Text = "Combat Statistics";
			this.CombatPage.UseVisualStyleBackColor = true;
			this.PowersPage.Controls.Add(this.PowerList);
			this.PowersPage.Controls.Add(this.PowerToolbar);
			this.PowersPage.Location = new Point(4, 22);
			this.PowersPage.Name = "PowersPage";
			this.PowersPage.Padding = new System.Windows.Forms.Padding(3);
			this.PowersPage.Size = new System.Drawing.Size(377, 193);
			this.PowersPage.TabIndex = 5;
			this.PowersPage.Text = "Powers";
			this.PowersPage.UseVisualStyleBackColor = true;
			this.PowerList.Columns.AddRange(new ColumnHeader[] { this.NameHdr });
			this.PowerList.Dock = DockStyle.Fill;
			this.PowerList.FullRowSelect = true;
			this.PowerList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.PowerList.HideSelection = false;
			this.PowerList.Location = new Point(3, 28);
			this.PowerList.MultiSelect = false;
			this.PowerList.Name = "PowerList";
			this.PowerList.Size = new System.Drawing.Size(371, 162);
			this.PowerList.TabIndex = 3;
			this.PowerList.UseCompatibleStateImageBehavior = false;
			this.PowerList.View = View.Details;
			this.PowerList.DoubleClick += new EventHandler(this.PowerEditBtn_Click);
			this.NameHdr.Text = "Power Name";
			this.NameHdr.Width = 200;
			ToolStripItemCollection items = this.PowerToolbar.Items;
			ToolStripItem[] powerAddBtn = new ToolStripItem[] { this.PowerAddBtn, this.PowerRemoveBtn, this.PowerEditBtn, this.toolStripSeparator1, this.PowerUpBtn, this.PowerDownBtn };
			items.AddRange(powerAddBtn);
			this.PowerToolbar.Location = new Point(3, 3);
			this.PowerToolbar.Name = "PowerToolbar";
			this.PowerToolbar.Size = new System.Drawing.Size(371, 25);
			this.PowerToolbar.TabIndex = 2;
			this.PowerToolbar.Text = "toolStrip1";
			this.PowerAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PowerAddBtn.DropDownItems.AddRange(new ToolStripItem[] { this.PowerBrowse });
			this.PowerAddBtn.Image = (Image)componentResourceManager.GetObject("PowerAddBtn.Image");
			this.PowerAddBtn.ImageTransparentColor = Color.Magenta;
			this.PowerAddBtn.Name = "PowerAddBtn";
			this.PowerAddBtn.Size = new System.Drawing.Size(45, 22);
			this.PowerAddBtn.Text = "Add";
			this.PowerAddBtn.ButtonClick += new EventHandler(this.PowerAddBtn_Click);
			this.PowerBrowse.Name = "PowerBrowse";
			this.PowerBrowse.Size = new System.Drawing.Size(121, 22);
			this.PowerBrowse.Text = "Browse...";
			this.PowerBrowse.Click += new EventHandler(this.SelectPowerBtn_Click);
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
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.PowerUpBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PowerUpBtn.Image = (Image)componentResourceManager.GetObject("PowerUpBtn.Image");
			this.PowerUpBtn.ImageTransparentColor = Color.Magenta;
			this.PowerUpBtn.Name = "PowerUpBtn";
			this.PowerUpBtn.Size = new System.Drawing.Size(59, 22);
			this.PowerUpBtn.Text = "Move Up";
			this.PowerUpBtn.Click += new EventHandler(this.PowerUpBtn_Click);
			this.PowerDownBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PowerDownBtn.Image = (Image)componentResourceManager.GetObject("PowerDownBtn.Image");
			this.PowerDownBtn.ImageTransparentColor = Color.Magenta;
			this.PowerDownBtn.Name = "PowerDownBtn";
			this.PowerDownBtn.Size = new System.Drawing.Size(75, 22);
			this.PowerDownBtn.Text = "Move Down";
			this.PowerDownBtn.Click += new EventHandler(this.PowerDownBtn_Click);
			this.AuraPage.Controls.Add(this.AuraList);
			this.AuraPage.Controls.Add(this.AuraToolbar);
			this.AuraPage.Location = new Point(4, 22);
			this.AuraPage.Name = "AuraPage";
			this.AuraPage.Padding = new System.Windows.Forms.Padding(3);
			this.AuraPage.Size = new System.Drawing.Size(377, 193);
			this.AuraPage.TabIndex = 6;
			this.AuraPage.Text = "Auras";
			this.AuraPage.UseVisualStyleBackColor = true;
			this.AuraList.Columns.AddRange(new ColumnHeader[] { this.AuraHdr });
			this.AuraList.Dock = DockStyle.Fill;
			this.AuraList.FullRowSelect = true;
			this.AuraList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.AuraList.HideSelection = false;
			this.AuraList.Location = new Point(3, 28);
			this.AuraList.MultiSelect = false;
			this.AuraList.Name = "AuraList";
			this.AuraList.Size = new System.Drawing.Size(371, 162);
			this.AuraList.TabIndex = 5;
			this.AuraList.UseCompatibleStateImageBehavior = false;
			this.AuraList.View = View.Details;
			this.AuraList.DoubleClick += new EventHandler(this.AuraEditBtn_Click);
			this.AuraHdr.Text = "Aura Name";
			this.AuraHdr.Width = 200;
			ToolStripItemCollection toolStripItemCollections = this.AuraToolbar.Items;
			ToolStripItem[] auraAddBtn = new ToolStripItem[] { this.AuraAddBtn, this.AuraRemoveBtn, this.AuraEditBtn };
			toolStripItemCollections.AddRange(auraAddBtn);
			this.AuraToolbar.Location = new Point(3, 3);
			this.AuraToolbar.Name = "AuraToolbar";
			this.AuraToolbar.Size = new System.Drawing.Size(371, 25);
			this.AuraToolbar.TabIndex = 4;
			this.AuraToolbar.Text = "toolStrip1";
			this.AuraAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AuraAddBtn.Image = (Image)componentResourceManager.GetObject("AuraAddBtn.Image");
			this.AuraAddBtn.ImageTransparentColor = Color.Magenta;
			this.AuraAddBtn.Name = "AuraAddBtn";
			this.AuraAddBtn.Size = new System.Drawing.Size(33, 22);
			this.AuraAddBtn.Text = "Add";
			this.AuraAddBtn.Click += new EventHandler(this.AuraAddBtn_Click);
			this.AuraRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AuraRemoveBtn.Image = (Image)componentResourceManager.GetObject("AuraRemoveBtn.Image");
			this.AuraRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.AuraRemoveBtn.Name = "AuraRemoveBtn";
			this.AuraRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.AuraRemoveBtn.Text = "Remove";
			this.AuraRemoveBtn.Click += new EventHandler(this.AuraRemoveBtn_Click);
			this.AuraEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AuraEditBtn.Image = (Image)componentResourceManager.GetObject("AuraEditBtn.Image");
			this.AuraEditBtn.ImageTransparentColor = Color.Magenta;
			this.AuraEditBtn.Name = "AuraEditBtn";
			this.AuraEditBtn.Size = new System.Drawing.Size(31, 22);
			this.AuraEditBtn.Text = "Edit";
			this.AuraEditBtn.Click += new EventHandler(this.AuraEditBtn_Click);
			this.DamagePage.Controls.Add(this.DamageList);
			this.DamagePage.Controls.Add(this.DamageToolbar);
			this.DamagePage.Location = new Point(4, 22);
			this.DamagePage.Name = "DamagePage";
			this.DamagePage.Padding = new System.Windows.Forms.Padding(3);
			this.DamagePage.Size = new System.Drawing.Size(377, 193);
			this.DamagePage.TabIndex = 4;
			this.DamagePage.Text = "Damage";
			this.DamagePage.UseVisualStyleBackColor = true;
			this.DamageList.Columns.AddRange(new ColumnHeader[] { this.DmgModHdr });
			this.DamageList.Dock = DockStyle.Fill;
			this.DamageList.FullRowSelect = true;
			listViewGroup.Header = "Immunities";
			listViewGroup.Name = "ImmGrp";
			listViewGroup1.Header = "Resistances";
			listViewGroup1.Name = "ResGrp";
			listViewGroup2.Header = "Vulnerabilities";
			listViewGroup2.Name = "VulnGrp";
			this.DamageList.Groups.AddRange(new ListViewGroup[] { listViewGroup, listViewGroup1, listViewGroup2 });
			this.DamageList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.DamageList.HideSelection = false;
			this.DamageList.Location = new Point(3, 28);
			this.DamageList.MultiSelect = false;
			this.DamageList.Name = "DamageList";
			this.DamageList.Size = new System.Drawing.Size(371, 162);
			this.DamageList.TabIndex = 3;
			this.DamageList.UseCompatibleStateImageBehavior = false;
			this.DamageList.View = View.Details;
			this.DamageList.DoubleClick += new EventHandler(this.EditDmgBtn_Click);
			this.DmgModHdr.Text = "Damage Modifier";
			this.DmgModHdr.Width = 200;
			ToolStripItemCollection items1 = this.DamageToolbar.Items;
			ToolStripItem[] addDmgBtn = new ToolStripItem[] { this.AddDmgBtn, this.RemoveDmgBtn, this.EditDmgBtn, this.toolStripSeparator2, this.RegenBtn, this.ClearRegenLbl };
			items1.AddRange(addDmgBtn);
			this.DamageToolbar.Location = new Point(3, 3);
			this.DamageToolbar.Name = "DamageToolbar";
			this.DamageToolbar.Size = new System.Drawing.Size(371, 25);
			this.DamageToolbar.TabIndex = 2;
			this.DamageToolbar.Text = "toolStrip1";
			this.AddDmgBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AddDmgBtn.Image = (Image)componentResourceManager.GetObject("AddDmgBtn.Image");
			this.AddDmgBtn.ImageTransparentColor = Color.Magenta;
			this.AddDmgBtn.Name = "AddDmgBtn";
			this.AddDmgBtn.Size = new System.Drawing.Size(33, 22);
			this.AddDmgBtn.Text = "Add";
			this.AddDmgBtn.Click += new EventHandler(this.AddDmgBtn_Click);
			this.RemoveDmgBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveDmgBtn.Image = (Image)componentResourceManager.GetObject("RemoveDmgBtn.Image");
			this.RemoveDmgBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveDmgBtn.Name = "RemoveDmgBtn";
			this.RemoveDmgBtn.Size = new System.Drawing.Size(54, 22);
			this.RemoveDmgBtn.Text = "Remove";
			this.RemoveDmgBtn.Click += new EventHandler(this.RemoveDmgBtn_Click);
			this.EditDmgBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditDmgBtn.Image = (Image)componentResourceManager.GetObject("EditDmgBtn.Image");
			this.EditDmgBtn.ImageTransparentColor = Color.Magenta;
			this.EditDmgBtn.Name = "EditDmgBtn";
			this.EditDmgBtn.Size = new System.Drawing.Size(31, 22);
			this.EditDmgBtn.Text = "Edit";
			this.EditDmgBtn.Click += new EventHandler(this.EditDmgBtn_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			this.RegenBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RegenBtn.Image = (Image)componentResourceManager.GetObject("RegenBtn.Image");
			this.RegenBtn.ImageTransparentColor = Color.Magenta;
			this.RegenBtn.Name = "RegenBtn";
			this.RegenBtn.Size = new System.Drawing.Size(81, 22);
			this.RegenBtn.Text = "Regeneration";
			this.RegenBtn.Click += new EventHandler(this.RegenBtn_Click);
			this.ClearRegenLbl.IsLink = true;
			this.ClearRegenLbl.Name = "ClearRegenLbl";
			this.ClearRegenLbl.Size = new System.Drawing.Size(88, 22);
			this.ClearRegenLbl.Text = "(remove regen)";
			this.ClearRegenLbl.Click += new EventHandler(this.ClearRegenLbl_Click);
			this.DetailsPage.Controls.Add(this.DetailsList);
			this.DetailsPage.Controls.Add(this.DetailsToolbar);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new System.Windows.Forms.Padding(3);
			this.DetailsPage.Size = new System.Drawing.Size(377, 193);
			this.DetailsPage.TabIndex = 3;
			this.DetailsPage.Text = "Details";
			this.DetailsPage.UseVisualStyleBackColor = true;
			ListView.ColumnHeaderCollection columns = this.DetailsList.Columns;
			ColumnHeader[] attributeHdr = new ColumnHeader[] { this.AttributeHdr, this.ValueHdr };
			columns.AddRange(attributeHdr);
			this.DetailsList.Dock = DockStyle.Fill;
			this.DetailsList.FullRowSelect = true;
			this.DetailsList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.DetailsList.HideSelection = false;
			this.DetailsList.Location = new Point(3, 28);
			this.DetailsList.MultiSelect = false;
			this.DetailsList.Name = "DetailsList";
			this.DetailsList.Size = new System.Drawing.Size(371, 162);
			this.DetailsList.TabIndex = 3;
			this.DetailsList.UseCompatibleStateImageBehavior = false;
			this.DetailsList.View = View.Details;
			this.DetailsList.DoubleClick += new EventHandler(this.DetailsEditBtn_Click);
			this.AttributeHdr.Text = "Field";
			this.AttributeHdr.Width = 100;
			this.ValueHdr.Text = "Details";
			this.ValueHdr.Width = 200;
			this.DetailsToolbar.Items.AddRange(new ToolStripItem[] { this.DetailsEditBtn });
			this.DetailsToolbar.Location = new Point(3, 3);
			this.DetailsToolbar.Name = "DetailsToolbar";
			this.DetailsToolbar.Size = new System.Drawing.Size(371, 25);
			this.DetailsToolbar.TabIndex = 2;
			this.DetailsToolbar.Text = "toolStrip1";
			this.DetailsEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DetailsEditBtn.Image = (Image)componentResourceManager.GetObject("DetailsEditBtn.Image");
			this.DetailsEditBtn.ImageTransparentColor = Color.Magenta;
			this.DetailsEditBtn.Name = "DetailsEditBtn";
			this.DetailsEditBtn.Size = new System.Drawing.Size(31, 22);
			this.DetailsEditBtn.Text = "Edit";
			this.DetailsEditBtn.Click += new EventHandler(this.DetailsEditBtn_Click);
			this.PicturePage.Controls.Add(this.PortraitBox);
			this.PicturePage.Controls.Add(this.PortraitToolbar);
			this.PicturePage.Location = new Point(4, 22);
			this.PicturePage.Name = "PicturePage";
			this.PicturePage.Padding = new System.Windows.Forms.Padding(3);
			this.PicturePage.Size = new System.Drawing.Size(377, 193);
			this.PicturePage.TabIndex = 7;
			this.PicturePage.Text = "Picture";
			this.PicturePage.UseVisualStyleBackColor = true;
			this.PortraitBox.Dock = DockStyle.Fill;
			this.PortraitBox.Location = new Point(3, 28);
			this.PortraitBox.Name = "PortraitBox";
			this.PortraitBox.Size = new System.Drawing.Size(371, 162);
			this.PortraitBox.SizeMode = PictureBoxSizeMode.Zoom;
			this.PortraitBox.TabIndex = 3;
			this.PortraitBox.TabStop = false;
			ToolStripItemCollection toolStripItemCollections1 = this.PortraitToolbar.Items;
			ToolStripItem[] portraitBrowse = new ToolStripItem[] { this.PortraitBrowse, this.PortraitClear };
			toolStripItemCollections1.AddRange(portraitBrowse);
			this.PortraitToolbar.Location = new Point(3, 3);
			this.PortraitToolbar.Name = "PortraitToolbar";
			this.PortraitToolbar.Size = new System.Drawing.Size(371, 25);
			this.PortraitToolbar.TabIndex = 2;
			this.PortraitToolbar.Text = "toolStrip1";
			this.PortraitBrowse.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PortraitBrowse.Image = (Image)componentResourceManager.GetObject("PortraitBrowse.Image");
			this.PortraitBrowse.ImageTransparentColor = Color.Magenta;
			this.PortraitBrowse.Name = "PortraitBrowse";
			this.PortraitBrowse.Size = new System.Drawing.Size(49, 22);
			this.PortraitBrowse.Text = "Browse";
			this.PortraitBrowse.Click += new EventHandler(this.PortraitBrowse_Click);
			this.PortraitClear.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PortraitClear.Image = (Image)componentResourceManager.GetObject("PortraitClear.Image");
			this.PortraitClear.ImageTransparentColor = Color.Magenta;
			this.PortraitClear.Name = "PortraitClear";
			this.PortraitClear.Size = new System.Drawing.Size(38, 22);
			this.PortraitClear.Text = "Clear";
			this.PortraitClear.Click += new EventHandler(this.PortraitClear_Click);
			this.TemplateBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.TemplateBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.TemplateBox.FormattingEnabled = true;
			this.TemplateBox.Location = new Point(72, 64);
			this.TemplateBox.Name = "TemplateBox";
			this.TemplateBox.Size = new System.Drawing.Size(325, 21);
			this.TemplateBox.Sorted = true;
			this.TemplateBox.TabIndex = 5;
			this.TemplateBox.SelectedIndexChanged += new EventHandler(this.TemplateBox_SelectedIndexChanged);
			this.InfoBtn.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.InfoBtn.Location = new Point(72, 91);
			this.InfoBtn.Name = "InfoBtn";
			this.InfoBtn.Size = new System.Drawing.Size(325, 23);
			this.InfoBtn.TabIndex = 9;
			this.InfoBtn.Text = "[info]";
			this.InfoBtn.UseVisualStyleBackColor = true;
			this.InfoBtn.Click += new EventHandler(this.InfoBtn_Click);
			this.InfoLbl.AutoSize = true;
			this.InfoLbl.Location = new Point(12, 96);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new System.Drawing.Size(28, 13);
			this.InfoLbl.TabIndex = 8;
			this.InfoLbl.Text = "Info:";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(409, 380);
			base.Controls.Add(this.InfoBtn);
			base.Controls.Add(this.InfoLbl);
			base.Controls.Add(this.TemplateBox);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.TemplateLbl);
			base.Controls.Add(this.LevelBox);
			base.Controls.Add(this.LevelLbl);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "NPCForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "NPC";
			((ISupportInitialize)this.LevelBox).EndInit();
			((ISupportInitialize)this.StrBox).EndInit();
			((ISupportInitialize)this.ConBox).EndInit();
			((ISupportInitialize)this.DexBox).EndInit();
			((ISupportInitialize)this.IntBox).EndInit();
			((ISupportInitialize)this.WisBox).EndInit();
			((ISupportInitialize)this.ChaBox).EndInit();
			((ISupportInitialize)this.InitModBox).EndInit();
			((ISupportInitialize)this.HPModBox).EndInit();
			((ISupportInitialize)this.ACModBox).EndInit();
			((ISupportInitialize)this.FortModBox).EndInit();
			((ISupportInitialize)this.RefModBox).EndInit();
			((ISupportInitialize)this.WillModBox).EndInit();
			this.Pages.ResumeLayout(false);
			this.AbilitiesPage.ResumeLayout(false);
			this.AbilitiesPage.PerformLayout();
			this.CombatPage.ResumeLayout(false);
			this.CombatPage.PerformLayout();
			this.PowersPage.ResumeLayout(false);
			this.PowersPage.PerformLayout();
			this.PowerToolbar.ResumeLayout(false);
			this.PowerToolbar.PerformLayout();
			this.AuraPage.ResumeLayout(false);
			this.AuraPage.PerformLayout();
			this.AuraToolbar.ResumeLayout(false);
			this.AuraToolbar.PerformLayout();
			this.DamagePage.ResumeLayout(false);
			this.DamagePage.PerformLayout();
			this.DamageToolbar.ResumeLayout(false);
			this.DamageToolbar.PerformLayout();
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			this.DetailsToolbar.ResumeLayout(false);
			this.DetailsToolbar.PerformLayout();
			this.PicturePage.ResumeLayout(false);
			this.PicturePage.PerformLayout();
			((ISupportInitialize)this.PortraitBox).EndInit();
			this.PortraitToolbar.ResumeLayout(false);
			this.PortraitToolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fNPC.Name = this.NameBox.Text;
		}

		private void ParameterChanged(object sender, EventArgs e)
		{
			this.update_fields();
		}

		private void PortraitBrowse_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Filter = Program.ImageFilter
			};
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fNPC.Image = Image.FromFile(openFileDialog.FileName);
				this.image_changed();
			}
		}

		private void PortraitClear_Click(object sender, EventArgs e)
		{
			this.fNPC.Image = null;
			this.image_changed();
		}

		private void PowerAddBtn_Click(object sender, EventArgs e)
		{
			CreaturePower creaturePower = new CreaturePower()
			{
				Name = "New Power"
			};
			PowerBuilderForm powerBuilderForm = new PowerBuilderForm(creaturePower, this.fNPC, false);
			if (powerBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fNPC.CreaturePowers.Add(powerBuilderForm.Power);
				this.update_powers_list();
			}
		}

		private void PowerDownBtn_Click(object sender, EventArgs e)
		{
			int selectedPower = this.fNPC.CreaturePowers.IndexOf(this.SelectedPower);
			CreaturePower item = this.fNPC.CreaturePowers[selectedPower + 1];
			this.fNPC.CreaturePowers[selectedPower + 1] = this.SelectedPower;
			this.fNPC.CreaturePowers[selectedPower] = item;
			this.update_powers_list();
			this.PowerList.Items[selectedPower + 1].Selected = true;
		}

		private void PowerEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedPower != null)
			{
				int power = this.fNPC.CreaturePowers.IndexOf(this.SelectedPower);
				PowerBuilderForm powerBuilderForm = new PowerBuilderForm(this.SelectedPower, this.fNPC, false);
				if (powerBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fNPC.CreaturePowers[power] = powerBuilderForm.Power;
					this.update_powers_list();
				}
			}
		}

		private void PowerRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedPower != null)
			{
				this.fNPC.CreaturePowers.Remove(this.SelectedPower);
				this.update_powers_list();
			}
		}

		private void PowerUpBtn_Click(object sender, EventArgs e)
		{
			int selectedPower = this.fNPC.CreaturePowers.IndexOf(this.SelectedPower);
			CreaturePower item = this.fNPC.CreaturePowers[selectedPower - 1];
			this.fNPC.CreaturePowers[selectedPower - 1] = this.SelectedPower;
			this.fNPC.CreaturePowers[selectedPower] = item;
			this.update_powers_list();
			this.PowerList.Items[selectedPower - 1].Selected = true;
		}

		private void RegenBtn_Click(object sender, EventArgs e)
		{
			Regeneration regeneration = this.fNPC.Regeneration ?? new Regeneration();
			RegenerationForm regenerationForm = new RegenerationForm(regeneration);
			if (regenerationForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fNPC.Regeneration = regenerationForm.Regeneration;
			}
		}

		private void RemoveDmgBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedDamageMod != null)
			{
				this.fNPC.DamageModifiers.Remove(this.SelectedDamageMod);
				this.update_damage_list();
			}
		}

		private void SelectPowerBtn_Click(object sender, EventArgs e)
		{
			PowerBrowserForm powerBrowserForm = new PowerBrowserForm(this.NameBox.Text, (int)this.LevelBox.Value, this.fNPC.Role, new PowerCallback(this.add_power));
			powerBrowserForm.ShowDialog();
		}

		private void TemplateBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			CreatureTemplate selectedItem = this.TemplateBox.SelectedItem as CreatureTemplate;
			this.fNPC.TemplateID = (selectedItem != null ? selectedItem.ID : Guid.Empty);
			this.update_fields();
		}

		private void update_aura_list()
		{
			this.AuraList.Items.Clear();
			foreach (Aura aura in this.fNPC.Auras)
			{
				ListViewItem listViewItem = this.AuraList.Items.Add(aura.Name);
				listViewItem.Tag = aura;
			}
			if (this.AuraList.Items.Count == 0)
			{
				ListViewItem grayText = this.AuraList.Items.Add("(no auras)");
				grayText.ForeColor = SystemColors.GrayText;
			}
		}

		private void update_damage_list()
		{
			this.DamageList.Items.Clear();
			this.DamageList.ShowGroups = true;
			foreach (DamageModifier damageModifier in this.fNPC.DamageModifiers)
			{
				ListViewItem item = this.DamageList.Items.Add(damageModifier.ToString());
				item.Tag = damageModifier;
				if (damageModifier.Value == 0)
				{
					item.Group = this.DamageList.Groups[0];
				}
				if (damageModifier.Value < 0)
				{
					item.Group = this.DamageList.Groups[1];
				}
				if (damageModifier.Value <= 0)
				{
					continue;
				}
				item.Group = this.DamageList.Groups[2];
			}
			if (this.fNPC.DamageModifiers.Count == 0)
			{
				this.DamageList.ShowGroups = false;
				ListViewItem grayText = this.DamageList.Items.Add("(none)");
				grayText.ForeColor = SystemColors.GrayText;
			}
		}

		private void update_details()
		{
			this.DetailsList.Items.Clear();
			ListViewItem listViewItem = this.DetailsList.Items.Add("Senses");
			listViewItem.SubItems.Add(this.fNPC.Senses);
			listViewItem.Tag = DetailsField.Senses;
			ListViewItem listViewItem1 = this.DetailsList.Items.Add("Movement");
			listViewItem1.SubItems.Add(this.fNPC.Movement);
			listViewItem1.Tag = DetailsField.Movement;
			ListViewItem listViewItem2 = this.DetailsList.Items.Add("Resist");
			listViewItem2.SubItems.Add(this.fNPC.Resist);
			listViewItem2.Tag = DetailsField.Resist;
			ListViewItem listViewItem3 = this.DetailsList.Items.Add("Vulnerable");
			listViewItem3.SubItems.Add(this.fNPC.Vulnerable);
			listViewItem3.Tag = DetailsField.Vulnerable;
			ListViewItem listViewItem4 = this.DetailsList.Items.Add("Immune");
			listViewItem4.SubItems.Add(this.fNPC.Immune);
			listViewItem4.Tag = DetailsField.Immune;
			ListViewItem listViewItem5 = this.DetailsList.Items.Add("Alignment");
			listViewItem5.SubItems.Add(this.fNPC.Alignment);
			listViewItem5.Tag = DetailsField.Alignment;
			ListViewItem listViewItem6 = this.DetailsList.Items.Add("Languages");
			listViewItem6.SubItems.Add(this.fNPC.Languages);
			listViewItem6.Tag = DetailsField.Languages;
			ListViewItem listViewItem7 = this.DetailsList.Items.Add("Skills");
			listViewItem7.SubItems.Add(this.fNPC.Skills);
			listViewItem7.Tag = DetailsField.Skills;
			ListViewItem listViewItem8 = this.DetailsList.Items.Add("Equipment");
			listViewItem8.SubItems.Add(this.fNPC.Equipment);
			listViewItem8.Tag = DetailsField.Equipment;
			ListViewItem listViewItem9 = this.DetailsList.Items.Add("Description");
			listViewItem9.SubItems.Add(this.fNPC.Details);
			listViewItem9.Tag = DetailsField.Description;
			ListViewItem listViewItem10 = this.DetailsList.Items.Add("Tactics");
			listViewItem10.SubItems.Add(this.fNPC.Tactics);
			listViewItem10.Tag = DetailsField.Tactics;
		}

		private void update_fields()
		{
			if (this.fUpdating)
			{
				return;
			}
			this.fNPC.Level = (int)this.LevelBox.Value;
			this.fNPC.Strength.Score = (int)this.StrBox.Value;
			this.fNPC.Constitution.Score = (int)this.ConBox.Value;
			this.fNPC.Dexterity.Score = (int)this.DexBox.Value;
			this.fNPC.Intelligence.Score = (int)this.IntBox.Value;
			this.fNPC.Wisdom.Score = (int)this.WisBox.Value;
			this.fNPC.Charisma.Score = (int)this.ChaBox.Value;
			int abilityCost = this.fNPC.AbilityCost;
			this.CostBox.Text = (abilityCost != -1 ? abilityCost.ToString() : "(n/a)");
			this.fNPC.InitiativeModifier = (int)this.InitModBox.Value;
			this.fNPC.HPModifier = (int)this.HPModBox.Value;
			this.fNPC.ACModifier = (int)this.ACModBox.Value;
			this.fNPC.FortitudeModifier = (int)this.FortModBox.Value;
			this.fNPC.ReflexModifier = (int)this.RefModBox.Value;
			this.fNPC.WillModifier = (int)this.WillModBox.Value;
			int level = this.fNPC.Level / 2;
			int modifier = this.fNPC.Strength.Modifier;
			this.StrModBox.Text = string.Concat(modifier, " / ", modifier + level);
			int num = this.fNPC.Constitution.Modifier;
			this.ConModBox.Text = string.Concat(num, " / ", num + level);
			int modifier1 = this.fNPC.Dexterity.Modifier;
			this.DexModBox.Text = string.Concat(modifier1, " / ", modifier1 + level);
			int num1 = this.fNPC.Intelligence.Modifier;
			this.IntModBox.Text = string.Concat(num1, " / ", num1 + level);
			int modifier2 = this.fNPC.Wisdom.Modifier;
			this.WisModBox.Text = string.Concat(modifier2, " / ", modifier2 + level);
			int num2 = this.fNPC.Charisma.Modifier;
			this.ChaModBox.Text = string.Concat(num2, " / ", num2 + level);
			this.InitBox.Text = this.fNPC.Initiative.ToString();
			this.HPBox.Text = this.fNPC.HP.ToString();
			this.ACBox.Text = this.fNPC.AC.ToString();
			this.FortBox.Text = this.fNPC.Fortitude.ToString();
			this.RefBox.Text = this.fNPC.Reflex.ToString();
			this.WillBox.Text = this.fNPC.Will.ToString();
		}

		private void update_powers_list()
		{
			this.PowerList.Items.Clear();
			foreach (CreaturePower creaturePower in this.fNPC.CreaturePowers)
			{
				ListViewItem listViewItem = this.PowerList.Items.Add(creaturePower.Name);
				listViewItem.Tag = creaturePower;
			}
			if (this.PowerList.Items.Count == 0)
			{
				ListViewItem grayText = this.PowerList.Items.Add("(no powers)");
				grayText.ForeColor = SystemColors.GrayText;
			}
		}
	}
}