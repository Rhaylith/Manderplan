using Masterplan;
using Masterplan.Controls;
using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class CombatDataForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label InitLbl;

		private NumericUpDown InitBox;

		private Label DamageLbl;

		private NumericUpDown DamageBox;

		private TextBox HPBox;

		private Panel ConditionPanel;

		private ListView EffectList;

		private ToolStrip Toolbar;

		private ColumnHeader EffectHdr;

		private ToolStripButton RemoveBtn;

		private ToolStripButton EditBtn;

		private ToolStripButton AddBtn;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton SavesBtn;

		private ToolStripButton DmgBtn;

		private NumericUpDown TempHPBox;

		private Label TempHPLbl;

		private HitPointGauge HPGauge;

		private Label LabelLbl;

		private TextBox LabelBox;

		private ColumnHeader EffectDurationHdr;

		private NumericUpDown AltitudeBox;

		private Label AltitudeLbl;

		private CombatData fData;

		private EncounterCard fCard;

		private Encounter fEncounter;

		private CombatData fCurrentActor;

		private int fCurrentRound = -2147483648;

		public CombatData Data
		{
			get
			{
				return this.fData;
			}
		}

		public OngoingCondition SelectedCondition
		{
			get
			{
				if (this.EffectList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.EffectList.SelectedItems[0].Tag as OngoingCondition;
			}
		}

		public CombatDataForm(CombatData data, EncounterCard card, Encounter enc, CombatData current_actor, int current_round, bool allow_name_edit)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.EffectList_SizeChanged(null, null);
			this.fData = data.Copy();
			this.fCard = card;
			this.fEncounter = enc;
			this.fCurrentActor = current_actor;
			this.fCurrentRound = current_round;
			if (this.fData.Initiative == -2147483648)
			{
				this.fData.Initiative = 0;
			}
			this.Text = this.fData.DisplayName;
			this.LabelBox.Text = this.fData.DisplayName;
			if (!allow_name_edit)
			{
				this.LabelBox.Enabled = false;
			}
			this.update_hp();
			this.InitBox.Value = this.fData.Initiative;
			this.AltitudeBox.Value = this.fData.Altitude;
			this.update_effects();
		}

		private void AddBtn_Click(object sender, EventArgs e)
		{
			OngoingCondition ongoingCondition = new OngoingCondition();
			EffectForm effectForm = new EffectForm(ongoingCondition, this.fEncounter, this.fCurrentActor, this.fCurrentRound);
			if (effectForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fData.Conditions.Add(effectForm.Effect);
				this.update_effects();
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			bool flag = false;
			foreach (OngoingCondition condition in this.fData.Conditions)
			{
				if (condition.Type != OngoingType.Damage || condition.Value <= 0)
				{
					continue;
				}
				flag = true;
				break;
			}
			this.RemoveBtn.Enabled = this.SelectedCondition != null;
			this.EditBtn.Enabled = this.SelectedCondition != null;
			this.SavesBtn.Enabled = this.fData.Conditions.Count > 0;
			this.DmgBtn.Enabled = flag;
		}

		private void DamageBox_ValueChanged(object sender, EventArgs e)
		{
			this.fData.Damage = (int)this.DamageBox.Value;
			this.update_hp();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void DmgBtn_Click(object sender, EventArgs e)
		{
			if ((new OngoingDamageForm(this.fData, this.fCard, this.fEncounter)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.update_hp();
			}
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCondition != null)
			{
				int effect = this.fData.Conditions.IndexOf(this.SelectedCondition);
				EffectForm effectForm = new EffectForm(this.SelectedCondition, this.fEncounter, this.fCurrentActor, this.fCurrentRound);
				if (effectForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fData.Conditions[effect] = effectForm.Effect;
					this.update_effects();
				}
			}
		}

		private void EffectList_SizeChanged(object sender, EventArgs e)
		{
			int width = this.EffectList.Width - (SystemInformation.VerticalScrollBarWidth + 6);
			ListView effectList = this.EffectList;
			System.Drawing.Size tileSize = this.EffectList.TileSize;
			effectList.TileSize = new System.Drawing.Size(width, tileSize.Height);
		}

		private void InitializeComponent()
		{
			ListViewGroup listViewGroup = new ListViewGroup("Ongoing Conditions", HorizontalAlignment.Left);
			ListViewGroup listViewGroup1 = new ListViewGroup("Ongoing Damage", HorizontalAlignment.Left);
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(CombatDataForm));
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.InitLbl = new Label();
			this.InitBox = new NumericUpDown();
			this.DamageLbl = new Label();
			this.DamageBox = new NumericUpDown();
			this.HPBox = new TextBox();
			this.ConditionPanel = new Panel();
			this.EffectList = new ListView();
			this.EffectHdr = new ColumnHeader();
			this.EffectDurationHdr = new ColumnHeader();
			this.Toolbar = new ToolStrip();
			this.AddBtn = new ToolStripButton();
			this.RemoveBtn = new ToolStripButton();
			this.EditBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.DmgBtn = new ToolStripButton();
			this.SavesBtn = new ToolStripButton();
			this.TempHPBox = new NumericUpDown();
			this.TempHPLbl = new Label();
			this.LabelLbl = new Label();
			this.LabelBox = new TextBox();
			this.HPGauge = new HitPointGauge();
			this.AltitudeBox = new NumericUpDown();
			this.AltitudeLbl = new Label();
			((ISupportInitialize)this.InitBox).BeginInit();
			((ISupportInitialize)this.DamageBox).BeginInit();
			this.ConditionPanel.SuspendLayout();
			this.Toolbar.SuspendLayout();
			((ISupportInitialize)this.TempHPBox).BeginInit();
			((ISupportInitialize)this.AltitudeBox).BeginInit();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(231, 351);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 13;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(312, 351);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 14;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.InitLbl.AutoSize = true;
			this.InitLbl.Location = new Point(12, 143);
			this.InitLbl.Name = "InitLbl";
			this.InitLbl.Size = new System.Drawing.Size(49, 13);
			this.InitLbl.TabIndex = 7;
			this.InitLbl.Text = "Initiative:";
			this.InitBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.InitBox.Location = new Point(73, 141);
			NumericUpDown initBox = this.InitBox;
			int[] numArray = new int[] { 100, 0, 0, -2147483648 };
			initBox.Minimum = new decimal(numArray);
			this.InitBox.Name = "InitBox";
			this.InitBox.Size = new System.Drawing.Size(276, 20);
			this.InitBox.TabIndex = 8;
			this.DamageLbl.AutoSize = true;
			this.DamageLbl.Location = new Point(12, 53);
			this.DamageLbl.Name = "DamageLbl";
			this.DamageLbl.Size = new System.Drawing.Size(50, 13);
			this.DamageLbl.TabIndex = 2;
			this.DamageLbl.Text = "Damage:";
			this.DamageBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.DamageBox.Location = new Point(73, 51);
			NumericUpDown damageBox = this.DamageBox;
			int[] numArray1 = new int[] { 1000, 0, 0, 0 };
			damageBox.Maximum = new decimal(numArray1);
			this.DamageBox.Name = "DamageBox";
			this.DamageBox.Size = new System.Drawing.Size(276, 20);
			this.DamageBox.TabIndex = 3;
			this.DamageBox.ValueChanged += new EventHandler(this.DamageBox_ValueChanged);
			this.HPBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.HPBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.HPBox.Location = new Point(73, 103);
			this.HPBox.Name = "HPBox";
			this.HPBox.ReadOnly = true;
			this.HPBox.Size = new System.Drawing.Size(276, 20);
			this.HPBox.TabIndex = 6;
			this.HPBox.TabStop = false;
			this.HPBox.TextAlign = HorizontalAlignment.Center;
			this.ConditionPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.ConditionPanel.BorderStyle = BorderStyle.FixedSingle;
			this.ConditionPanel.Controls.Add(this.EffectList);
			this.ConditionPanel.Controls.Add(this.Toolbar);
			this.ConditionPanel.Location = new Point(12, 193);
			this.ConditionPanel.Name = "ConditionPanel";
			this.ConditionPanel.Size = new System.Drawing.Size(337, 152);
			this.ConditionPanel.TabIndex = 11;
			this.EffectList.BorderStyle = BorderStyle.None;
			ListView.ColumnHeaderCollection columns = this.EffectList.Columns;
			ColumnHeader[] effectHdr = new ColumnHeader[] { this.EffectHdr, this.EffectDurationHdr };
			columns.AddRange(effectHdr);
			this.EffectList.Dock = DockStyle.Fill;
			this.EffectList.FullRowSelect = true;
			listViewGroup.Header = "Ongoing Conditions";
			listViewGroup.Name = "ConditionHdr";
			listViewGroup1.Header = "Ongoing Damage";
			listViewGroup1.Name = "DmgHdr";
			this.EffectList.Groups.AddRange(new ListViewGroup[] { listViewGroup, listViewGroup1 });
			this.EffectList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.EffectList.HideSelection = false;
			this.EffectList.Location = new Point(0, 25);
			this.EffectList.MultiSelect = false;
			this.EffectList.Name = "EffectList";
			this.EffectList.Size = new System.Drawing.Size(335, 125);
			this.EffectList.TabIndex = 1;
			this.EffectList.TileSize = new System.Drawing.Size(200, 30);
			this.EffectList.UseCompatibleStateImageBehavior = false;
			this.EffectList.View = View.Tile;
			this.EffectList.SizeChanged += new EventHandler(this.EffectList_SizeChanged);
			this.EffectList.DoubleClick += new EventHandler(this.EditBtn_Click);
			this.EffectHdr.Text = "Effect";
			this.EffectHdr.Width = 120;
			this.EffectDurationHdr.Text = "Duration";
			this.EffectDurationHdr.Width = 141;
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] addBtn = new ToolStripItem[] { this.AddBtn, this.RemoveBtn, this.EditBtn, this.toolStripSeparator1, this.DmgBtn, this.SavesBtn };
			items.AddRange(addBtn);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(335, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.AddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AddBtn.Image = (Image)componentResourceManager.GetObject("AddBtn.Image");
			this.AddBtn.ImageTransparentColor = Color.Magenta;
			this.AddBtn.Name = "AddBtn";
			this.AddBtn.Size = new System.Drawing.Size(33, 22);
			this.AddBtn.Text = "Add";
			this.AddBtn.Click += new EventHandler(this.AddBtn_Click);
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
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.DmgBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DmgBtn.Image = (Image)componentResourceManager.GetObject("DmgBtn.Image");
			this.DmgBtn.ImageTransparentColor = Color.Magenta;
			this.DmgBtn.Name = "DmgBtn";
			this.DmgBtn.Size = new System.Drawing.Size(105, 22);
			this.DmgBtn.Text = "Ongoing Damage";
			this.DmgBtn.Click += new EventHandler(this.DmgBtn_Click);
			this.SavesBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SavesBtn.Image = (Image)componentResourceManager.GetObject("SavesBtn.Image");
			this.SavesBtn.ImageTransparentColor = Color.Magenta;
			this.SavesBtn.Name = "SavesBtn";
			this.SavesBtn.Size = new System.Drawing.Size(40, 22);
			this.SavesBtn.Text = "Saves";
			this.SavesBtn.Click += new EventHandler(this.SavesBtn_Click);
			this.TempHPBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.TempHPBox.Location = new Point(73, 77);
			NumericUpDown tempHPBox = this.TempHPBox;
			int[] numArray2 = new int[] { 1000, 0, 0, 0 };
			tempHPBox.Maximum = new decimal(numArray2);
			this.TempHPBox.Name = "TempHPBox";
			this.TempHPBox.Size = new System.Drawing.Size(276, 20);
			this.TempHPBox.TabIndex = 5;
			this.TempHPBox.ValueChanged += new EventHandler(this.TempHPBox_ValueChanged);
			this.TempHPLbl.AutoSize = true;
			this.TempHPLbl.Location = new Point(12, 79);
			this.TempHPLbl.Name = "TempHPLbl";
			this.TempHPLbl.Size = new System.Drawing.Size(55, 13);
			this.TempHPLbl.TabIndex = 4;
			this.TempHPLbl.Text = "Temp HP:";
			this.LabelLbl.AutoSize = true;
			this.LabelLbl.Location = new Point(12, 15);
			this.LabelLbl.Name = "LabelLbl";
			this.LabelLbl.Size = new System.Drawing.Size(36, 13);
			this.LabelLbl.TabIndex = 0;
			this.LabelLbl.Text = "Label:";
			this.LabelBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.LabelBox.Location = new Point(73, 12);
			this.LabelBox.Name = "LabelBox";
			this.LabelBox.Size = new System.Drawing.Size(314, 20);
			this.LabelBox.TabIndex = 1;
			this.HPGauge.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
			this.HPGauge.Damage = 0;
			this.HPGauge.FullHP = 0;
			this.HPGauge.Location = new Point(355, 51);
			this.HPGauge.Name = "HPGauge";
			this.HPGauge.Size = new System.Drawing.Size(32, 294);
			this.HPGauge.TabIndex = 12;
			this.HPGauge.TempHP = 0;
			this.AltitudeBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.AltitudeBox.Location = new Point(73, 167);
			NumericUpDown altitudeBox = this.AltitudeBox;
			int[] numArray3 = new int[] { 100, 0, 0, -2147483648 };
			altitudeBox.Minimum = new decimal(numArray3);
			this.AltitudeBox.Name = "AltitudeBox";
			this.AltitudeBox.Size = new System.Drawing.Size(276, 20);
			this.AltitudeBox.TabIndex = 10;
			this.AltitudeLbl.AutoSize = true;
			this.AltitudeLbl.Location = new Point(12, 169);
			this.AltitudeLbl.Name = "AltitudeLbl";
			this.AltitudeLbl.Size = new System.Drawing.Size(45, 13);
			this.AltitudeLbl.TabIndex = 9;
			this.AltitudeLbl.Text = "Altitude:";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(399, 386);
			base.Controls.Add(this.AltitudeBox);
			base.Controls.Add(this.AltitudeLbl);
			base.Controls.Add(this.LabelBox);
			base.Controls.Add(this.LabelLbl);
			base.Controls.Add(this.HPGauge);
			base.Controls.Add(this.TempHPBox);
			base.Controls.Add(this.TempHPLbl);
			base.Controls.Add(this.ConditionPanel);
			base.Controls.Add(this.HPBox);
			base.Controls.Add(this.DamageBox);
			base.Controls.Add(this.DamageLbl);
			base.Controls.Add(this.InitBox);
			base.Controls.Add(this.InitLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CombatDataForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Combatant";
			((ISupportInitialize)this.InitBox).EndInit();
			((ISupportInitialize)this.DamageBox).EndInit();
			this.ConditionPanel.ResumeLayout(false);
			this.ConditionPanel.PerformLayout();
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			((ISupportInitialize)this.TempHPBox).EndInit();
			((ISupportInitialize)this.AltitudeBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fData.DisplayName = this.LabelBox.Text;
			this.fData.Initiative = (int)this.InitBox.Value;
			this.fData.Altitude = (int)this.AltitudeBox.Value;
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCondition != null)
			{
				this.fData.Conditions.Remove(this.SelectedCondition);
				this.update_effects();
			}
		}

		private void SavesBtn_Click(object sender, EventArgs e)
		{
			if ((new SavingThrowForm(this.fData, this.fCard, this.fEncounter)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.update_effects();
			}
		}

		private void TempHPBox_ValueChanged(object sender, EventArgs e)
		{
			this.fData.TempHP = (int)this.TempHPBox.Value;
			this.update_hp();
		}

		private void update_effects()
		{
			this.EffectList.Items.Clear();
			this.EffectList.ShowGroups = true;
			foreach (OngoingCondition condition in this.fData.Conditions)
			{
				string str = condition.ToString();
				string duration = condition.GetDuration(this.fEncounter);
				if (duration == "")
				{
					duration = "until the end of the encounter";
				}
				ListViewItem item = this.EffectList.Items.Add(str);
				item.SubItems.Add(duration);
				item.Tag = condition;
				item.Group = this.EffectList.Groups[(condition.Type == OngoingType.Condition ? 0 : 1)];
			}
			if (this.fData.Conditions.Count == 0)
			{
				this.EffectList.ShowGroups = false;
				ListViewItem grayText = this.EffectList.Items.Add("(no ongoing effects)");
				grayText.ForeColor = SystemColors.GrayText;
			}
		}

		private void update_hp()
		{
			this.DamageBox.Value = this.fData.Damage;
			this.TempHPBox.Value = this.fData.TempHP;
			int hP = 0;
			if (this.fCard == null)
			{
				foreach (Hero hero in Session.Project.Heroes)
				{
					if (this.fData.DisplayName != hero.Name)
					{
						continue;
					}
					hP = hero.HP;
				}
			}
			else
			{
				hP = this.fCard.HP;
			}
			int damage = hP - this.fData.Damage;
			this.HPBox.Text = string.Concat(damage, " HP");
			if (this.fData.TempHP > 0)
			{
				TextBox hPBox = this.HPBox;
				object text = hPBox.Text;
				object[] tempHP = new object[] { text, "; ", this.fData.TempHP, " temp HP" };
				hPBox.Text = string.Concat(tempHP);
			}
			if (damage + this.fData.TempHP <= 0)
			{
				TextBox textBox = this.HPBox;
				textBox.Text = string.Concat(textBox.Text, " (dead)");
			}
			else if (damage <= hP / 2)
			{
				TextBox hPBox1 = this.HPBox;
				hPBox1.Text = string.Concat(hPBox1.Text, " (bloodied)");
			}
			this.HPGauge.FullHP = hP;
			this.HPGauge.Damage = this.fData.Damage;
			this.HPGauge.TempHP = this.fData.TempHP;
		}
	}
}