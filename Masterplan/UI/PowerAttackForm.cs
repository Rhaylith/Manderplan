using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class PowerAttackForm : Form
	{
		private PowerAttack fAttack;

		private bool fFunctionalTemplate;

		private int fLevel;

		private IRole fRole;

		private Button CancelBtn;

		private Button OKBtn;

		private Label BonusLbl;

		private Label DefenceLbl;

		private ComboBox DefenceBox;

		private NumericUpDown BonusBox;

		private Label InfoLbl;

		private Panel SuggestPnl;

		private Button SuggestBtn;

		private Label SuggestLbl;

		public PowerAttack Attack
		{
			get
			{
				return this.fAttack;
			}
		}

		public PowerAttackForm(PowerAttack attack, bool functional_template, int level, IRole role)
		{
			this.InitializeComponent();
			foreach (DefenceType value in Enum.GetValues(typeof(DefenceType)))
			{
				this.DefenceBox.Items.Add(value);
			}
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fAttack = attack.Copy();
			this.fFunctionalTemplate = functional_template;
			this.fLevel = level;
			this.fRole = role;
			this.BonusBox.Value = this.fAttack.Bonus;
			this.DefenceBox.SelectedItem = this.fAttack.Defence;
			this.set_suggestion();
			if (!this.fFunctionalTemplate)
			{
				this.InfoLbl.Visible = false;
				PowerAttackForm height = this;
				height.Height = height.Height - this.InfoLbl.Height;
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			DefenceType selectedItem = (DefenceType)this.DefenceBox.SelectedItem;
			int num = Statistics.AttackBonus(selectedItem, this.fLevel, this.fRole);
			int value = (int)this.BonusBox.Value;
			this.SuggestBtn.Enabled = value != num;
		}

		private void DefenceBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.set_suggestion();
		}

		private void InitializeComponent()
		{
			this.CancelBtn = new Button();
			this.OKBtn = new Button();
			this.BonusLbl = new Label();
			this.DefenceLbl = new Label();
			this.DefenceBox = new ComboBox();
			this.BonusBox = new NumericUpDown();
			this.InfoLbl = new Label();
			this.SuggestPnl = new Panel();
			this.SuggestBtn = new Button();
			this.SuggestLbl = new Label();
			((ISupportInitialize)this.BonusBox).BeginInit();
			this.SuggestPnl.SuspendLayout();
			base.SuspendLayout();
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(222, 144);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 7;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(141, 144);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 6;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.BonusLbl.AutoSize = true;
			this.BonusLbl.Location = new Point(12, 41);
			this.BonusLbl.Name = "BonusLbl";
			this.BonusLbl.Size = new System.Drawing.Size(37, 13);
			this.BonusLbl.TabIndex = 2;
			this.BonusLbl.Text = "Bonus";
			this.DefenceLbl.AutoSize = true;
			this.DefenceLbl.Location = new Point(12, 15);
			this.DefenceLbl.Name = "DefenceLbl";
			this.DefenceLbl.Size = new System.Drawing.Size(48, 13);
			this.DefenceLbl.TabIndex = 0;
			this.DefenceLbl.Text = "Defence";
			this.DefenceBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.DefenceBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.DefenceBox.FormattingEnabled = true;
			this.DefenceBox.Location = new Point(66, 12);
			this.DefenceBox.Name = "DefenceBox";
			this.DefenceBox.Size = new System.Drawing.Size(231, 21);
			this.DefenceBox.TabIndex = 1;
			this.DefenceBox.SelectedIndexChanged += new EventHandler(this.DefenceBox_SelectedIndexChanged);
			this.BonusBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.BonusBox.Location = new Point(66, 39);
			NumericUpDown bonusBox = this.BonusBox;
			int[] numArray = new int[] { 10, 0, 0, -2147483648 };
			bonusBox.Minimum = new decimal(numArray);
			this.BonusBox.Name = "BonusBox";
			this.BonusBox.Size = new System.Drawing.Size(231, 20);
			this.BonusBox.TabIndex = 3;
			this.InfoLbl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.InfoLbl.Location = new Point(66, 97);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new System.Drawing.Size(231, 44);
			this.InfoLbl.TabIndex = 5;
			this.InfoLbl.Text = "Note: Since this is a functional template power, the creature level will automatically be added to the attack bonus.";
			this.SuggestPnl.Controls.Add(this.SuggestBtn);
			this.SuggestPnl.Controls.Add(this.SuggestLbl);
			this.SuggestPnl.Location = new Point(66, 65);
			this.SuggestPnl.Name = "SuggestPnl";
			this.SuggestPnl.Size = new System.Drawing.Size(231, 29);
			this.SuggestPnl.TabIndex = 4;
			this.SuggestBtn.Location = new Point(135, 3);
			this.SuggestBtn.Name = "SuggestBtn";
			this.SuggestBtn.Size = new System.Drawing.Size(93, 23);
			this.SuggestBtn.TabIndex = 1;
			this.SuggestBtn.Text = "[+x]";
			this.SuggestBtn.UseVisualStyleBackColor = true;
			this.SuggestBtn.Click += new EventHandler(this.SuggestBtn_Click);
			this.SuggestLbl.AutoSize = true;
			this.SuggestLbl.Location = new Point(3, 8);
			this.SuggestLbl.Name = "SuggestLbl";
			this.SuggestLbl.Size = new System.Drawing.Size(126, 13);
			this.SuggestLbl.TabIndex = 0;
			this.SuggestLbl.Text = "Suggested attack bonus:";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(309, 179);
			base.Controls.Add(this.SuggestPnl);
			base.Controls.Add(this.InfoLbl);
			base.Controls.Add(this.DefenceBox);
			base.Controls.Add(this.DefenceLbl);
			base.Controls.Add(this.BonusBox);
			base.Controls.Add(this.BonusLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PowerAttackForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Attack Details";
			((ISupportInitialize)this.BonusBox).EndInit();
			this.SuggestPnl.ResumeLayout(false);
			this.SuggestPnl.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fAttack.Bonus = (int)this.BonusBox.Value;
			this.fAttack.Defence = (DefenceType)this.DefenceBox.SelectedItem;
		}

		private void set_suggestion()
		{
			DefenceType selectedItem = (DefenceType)this.DefenceBox.SelectedItem;
			int num = Statistics.AttackBonus(selectedItem, this.fLevel, this.fRole);
			this.SuggestBtn.Text = (num >= 0 ? string.Concat("+", num) : num.ToString());
			if (this.fFunctionalTemplate)
			{
				this.SuggestBtn.Text = string.Concat("Level ", this.SuggestBtn.Text);
			}
		}

		private void SuggestBtn_Click(object sender, EventArgs e)
		{
			DefenceType selectedItem = (DefenceType)this.DefenceBox.SelectedItem;
			this.BonusBox.Value = Statistics.AttackBonus(selectedItem, this.fLevel, this.fRole);
		}
	}
}