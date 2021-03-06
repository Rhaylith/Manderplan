using Masterplan;
using Masterplan.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class CreatureProfileForm : Form
	{
		private ICreature fCreature;

		private IRole fRole;

		private Label NameLbl;

		private TextBox NameBox;

		private Label LevelLbl;

		private NumericUpDown LevelBox;

		private Label RoleLbl;

		private Button RoleBtn;

		private Button OKBtn;

		private Button CancelBtn;

		private ComboBox CatBox;

		private Label CatLbl;

		private ComboBox SizeBox;

		private Label SizeLbl;

		private TextBox KeywordBox;

		private Label KeywordLbl;

		private ComboBox TypeBox;

		private Label TypeLbl;

		private ComboBox OriginBox;

		private Label OriginLbl;

		private ComboBox TemplateBox;

		private Label TemplateLbl;

		public ICreature Creature
		{
			get
			{
				return this.fCreature;
			}
		}

		public CreatureProfileForm(ICreature creature)
		{
			this.InitializeComponent();
			this.fCreature = creature;
			this.fRole = creature.Role;
			foreach (CreatureSize value in Enum.GetValues(typeof(CreatureSize)))
			{
				this.SizeBox.Items.Add(value);
			}
			foreach (CreatureOrigin creatureOrigin in Enum.GetValues(typeof(CreatureOrigin)))
			{
				this.OriginBox.Items.Add(creatureOrigin);
			}
			foreach (CreatureType creatureType in Enum.GetValues(typeof(CreatureType)))
			{
				this.TypeBox.Items.Add(creatureType);
			}
			if (this.fCreature is NPC)
			{
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
				this.RoleLbl.Enabled = false;
				this.RoleBtn.Enabled = false;
				this.CatLbl.Enabled = false;
				this.CatBox.Enabled = false;
			}
			else if (!(this.fCreature is CustomCreature))
			{
				this.TemplateLbl.Enabled = false;
				this.TemplateBox.Enabled = false;
				List<string> strs = new List<string>();
				foreach (Masterplan.Data.Creature creature1 in Session.Creatures)
				{
					string category = creature1.Category;
					if (!(category != "") || strs.Contains(category))
					{
						continue;
					}
					strs.Add(category);
				}
				foreach (string str in strs)
				{
					this.CatBox.Items.Add(str);
				}
			}
			else
			{
				this.TemplateLbl.Enabled = false;
				this.TemplateBox.Enabled = false;
				this.CatLbl.Enabled = false;
				this.CatBox.Enabled = false;
			}
			this.NameBox.Text = this.fCreature.Name;
			this.LevelBox.Value = this.fCreature.Level;
			this.SizeBox.SelectedItem = this.fCreature.Size;
			this.OriginBox.SelectedItem = this.fCreature.Origin;
			this.TypeBox.SelectedItem = this.fCreature.Type;
			this.KeywordBox.Text = this.fCreature.Keywords;
			if (!(this.fCreature is NPC))
			{
				if (this.fCreature is CustomCreature)
				{
					this.CatBox.Text = "Custom Creature";
					return;
				}
				this.RoleBtn.Text = this.fCreature.Role.ToString();
				this.CatBox.Text = this.fCreature.Category;
				return;
			}
			NPC nPC = this.fCreature as NPC;
			CreatureTemplate creatureTemplate2 = Session.FindTemplate(nPC.TemplateID, SearchType.Global);
			if (creatureTemplate2 == null)
			{
				this.TemplateBox.SelectedIndex = 0;
			}
			else
			{
				this.TemplateBox.SelectedItem = creatureTemplate2;
			}
			this.CatBox.Text = "NPC";
		}

		private void InitializeComponent()
		{
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.LevelLbl = new Label();
			this.LevelBox = new NumericUpDown();
			this.RoleLbl = new Label();
			this.RoleBtn = new Button();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.CatBox = new ComboBox();
			this.CatLbl = new Label();
			this.SizeBox = new ComboBox();
			this.SizeLbl = new Label();
			this.KeywordBox = new TextBox();
			this.KeywordLbl = new Label();
			this.TypeBox = new ComboBox();
			this.TypeLbl = new Label();
			this.OriginBox = new ComboBox();
			this.OriginLbl = new Label();
			this.TemplateBox = new ComboBox();
			this.TemplateLbl = new Label();
			((ISupportInitialize)this.LevelBox).BeginInit();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new System.Drawing.Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.NameBox.Location = new Point(74, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(214, 20);
			this.NameBox.TabIndex = 1;
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(12, 40);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new System.Drawing.Size(36, 13);
			this.LevelLbl.TabIndex = 2;
			this.LevelLbl.Text = "Level:";
			this.LevelBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.LevelBox.Location = new Point(74, 38);
			NumericUpDown levelBox = this.LevelBox;
			int[] numArray = new int[] { 40, 0, 0, 0 };
			levelBox.Maximum = new decimal(numArray);
			int[] numArray1 = new int[] { 1, 0, 0, 0 };
			this.LevelBox.Minimum = new decimal(numArray1);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.Size = new System.Drawing.Size(214, 20);
			this.LevelBox.TabIndex = 3;
			int[] numArray2 = new int[] { 1, 0, 0, 0 };
			this.LevelBox.Value = new decimal(numArray2);
			this.RoleLbl.AutoSize = true;
			this.RoleLbl.Location = new Point(12, 69);
			this.RoleLbl.Name = "RoleLbl";
			this.RoleLbl.Size = new System.Drawing.Size(32, 13);
			this.RoleLbl.TabIndex = 4;
			this.RoleLbl.Text = "Role:";
			this.RoleBtn.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.RoleBtn.Location = new Point(74, 64);
			this.RoleBtn.Name = "RoleBtn";
			this.RoleBtn.Size = new System.Drawing.Size(214, 23);
			this.RoleBtn.TabIndex = 5;
			this.RoleBtn.Text = "[role]";
			this.RoleBtn.UseVisualStyleBackColor = true;
			this.RoleBtn.Click += new EventHandler(this.RoleBtn_Click);
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(132, 265);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 16;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(213, 265);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 17;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.CatBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.CatBox.AutoCompleteMode = AutoCompleteMode.Append;
			this.CatBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.CatBox.FormattingEnabled = true;
			this.CatBox.Location = new Point(74, 227);
			this.CatBox.Name = "CatBox";
			this.CatBox.Size = new System.Drawing.Size(214, 21);
			this.CatBox.Sorted = true;
			this.CatBox.TabIndex = 15;
			this.CatLbl.AutoSize = true;
			this.CatLbl.Location = new Point(12, 230);
			this.CatLbl.Name = "CatLbl";
			this.CatLbl.Size = new System.Drawing.Size(52, 13);
			this.CatLbl.TabIndex = 14;
			this.CatLbl.Text = "Category:";
			this.SizeBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.SizeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.SizeBox.Location = new Point(74, 120);
			this.SizeBox.Name = "SizeBox";
			this.SizeBox.Size = new System.Drawing.Size(214, 21);
			this.SizeBox.TabIndex = 7;
			this.SizeLbl.AutoSize = true;
			this.SizeLbl.Location = new Point(12, 123);
			this.SizeLbl.Name = "SizeLbl";
			this.SizeLbl.Size = new System.Drawing.Size(30, 13);
			this.SizeLbl.TabIndex = 6;
			this.SizeLbl.Text = "Size:";
			this.KeywordBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.KeywordBox.Location = new Point(74, 201);
			this.KeywordBox.Name = "KeywordBox";
			this.KeywordBox.Size = new System.Drawing.Size(214, 20);
			this.KeywordBox.TabIndex = 13;
			this.KeywordLbl.AutoSize = true;
			this.KeywordLbl.Location = new Point(12, 204);
			this.KeywordLbl.Name = "KeywordLbl";
			this.KeywordLbl.Size = new System.Drawing.Size(56, 13);
			this.KeywordLbl.TabIndex = 12;
			this.KeywordLbl.Text = "Keywords:";
			this.TypeBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.TypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.TypeBox.FormattingEnabled = true;
			this.TypeBox.Location = new Point(74, 174);
			this.TypeBox.Name = "TypeBox";
			this.TypeBox.Size = new System.Drawing.Size(214, 21);
			this.TypeBox.TabIndex = 11;
			this.TypeLbl.AutoSize = true;
			this.TypeLbl.Location = new Point(12, 177);
			this.TypeLbl.Name = "TypeLbl";
			this.TypeLbl.Size = new System.Drawing.Size(34, 13);
			this.TypeLbl.TabIndex = 10;
			this.TypeLbl.Text = "Type:";
			this.OriginBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.OriginBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.OriginBox.FormattingEnabled = true;
			this.OriginBox.Location = new Point(74, 147);
			this.OriginBox.Name = "OriginBox";
			this.OriginBox.Size = new System.Drawing.Size(214, 21);
			this.OriginBox.TabIndex = 9;
			this.OriginLbl.AutoSize = true;
			this.OriginLbl.Location = new Point(12, 150);
			this.OriginLbl.Name = "OriginLbl";
			this.OriginLbl.Size = new System.Drawing.Size(37, 13);
			this.OriginLbl.TabIndex = 8;
			this.OriginLbl.Text = "Origin:";
			this.TemplateBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.TemplateBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.TemplateBox.FormattingEnabled = true;
			this.TemplateBox.Location = new Point(74, 93);
			this.TemplateBox.Name = "TemplateBox";
			this.TemplateBox.Size = new System.Drawing.Size(214, 21);
			this.TemplateBox.TabIndex = 19;
			this.TemplateBox.SelectedIndexChanged += new EventHandler(this.TemplateBox_SelectedIndexChanged);
			this.TemplateLbl.AutoSize = true;
			this.TemplateLbl.Location = new Point(12, 96);
			this.TemplateLbl.Name = "TemplateLbl";
			this.TemplateLbl.Size = new System.Drawing.Size(54, 13);
			this.TemplateLbl.TabIndex = 18;
			this.TemplateLbl.Text = "Template:";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(300, 300);
			base.Controls.Add(this.TemplateBox);
			base.Controls.Add(this.TemplateLbl);
			base.Controls.Add(this.SizeBox);
			base.Controls.Add(this.SizeLbl);
			base.Controls.Add(this.KeywordBox);
			base.Controls.Add(this.KeywordLbl);
			base.Controls.Add(this.TypeBox);
			base.Controls.Add(this.TypeLbl);
			base.Controls.Add(this.OriginBox);
			base.Controls.Add(this.OriginLbl);
			base.Controls.Add(this.CatBox);
			base.Controls.Add(this.CatLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.RoleBtn);
			base.Controls.Add(this.RoleLbl);
			base.Controls.Add(this.LevelBox);
			base.Controls.Add(this.LevelLbl);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CreatureProfileForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Creature";
			((ISupportInitialize)this.LevelBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fCreature.Name = this.NameBox.Text;
			this.fCreature.Level = (int)this.LevelBox.Value;
			this.fCreature.Size = (CreatureSize)this.SizeBox.SelectedItem;
			this.fCreature.Origin = (CreatureOrigin)this.OriginBox.SelectedItem;
			this.fCreature.Type = (CreatureType)this.TypeBox.SelectedItem;
			this.fCreature.Keywords = this.KeywordBox.Text;
			if (this.fCreature is NPC)
			{
				CreatureTemplate selectedItem = this.TemplateBox.SelectedItem as CreatureTemplate;
				(this.fCreature as NPC).TemplateID = (selectedItem != null ? selectedItem.ID : Guid.Empty);
				return;
			}
			this.fCreature.Role = this.fRole;
			this.fCreature.Category = this.CatBox.Text;
			if (this.fCreature.Role is Minion)
			{
				this.fCreature.HP = 1;
			}
		}

		private void RoleBtn_Click(object sender, EventArgs e)
		{
			RoleForm roleForm = new RoleForm(this.fCreature.Role, ThreatType.Creature);
			if (roleForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fRole = roleForm.Role;
				this.RoleBtn.Text = this.fRole.ToString();
			}
		}

		private void TemplateBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			CreatureTemplate selectedItem = this.TemplateBox.SelectedItem as CreatureTemplate;
			if (selectedItem != null)
			{
				this.RoleBtn.Text = selectedItem.Role.ToString();
			}
		}
	}
}