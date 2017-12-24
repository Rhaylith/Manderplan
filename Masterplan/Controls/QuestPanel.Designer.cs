using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.Controls
{
	internal class QuestPanel : UserControl
	{
		private Masterplan.Data.Quest fQuest;

		private bool fUpdating;

		private IContainer components;

		private Label TypeLbl;

		private ComboBox TypeBox;

		private Label LevelLbl;

		private NumericUpDown LevelBox;

		private Label XPLbl;

		private TextBox XPBox;

		private TrackBar XPSlider;

		private Panel MinorPnl;

		private Label MinMaxLbl;

		public Masterplan.Data.Quest Quest
		{
			get
			{
				return this.fQuest;
			}
			set
			{
				this.fQuest = value;
				this.update_view();
			}
		}

		public QuestPanel()
		{
			this.InitializeComponent();
			foreach (QuestType value in Enum.GetValues(typeof(QuestType)))
			{
				this.TypeBox.Items.Add(value);
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

		private void InitializeComponent()
		{
			this.TypeLbl = new Label();
			this.TypeBox = new ComboBox();
			this.LevelLbl = new Label();
			this.LevelBox = new NumericUpDown();
			this.XPLbl = new Label();
			this.XPBox = new TextBox();
			this.XPSlider = new TrackBar();
			this.MinorPnl = new Panel();
			this.MinMaxLbl = new Label();
			((ISupportInitialize)this.LevelBox).BeginInit();
			((ISupportInitialize)this.XPSlider).BeginInit();
			this.MinorPnl.SuspendLayout();
			base.SuspendLayout();
			this.TypeLbl.AutoSize = true;
			this.TypeLbl.Location = new Point(3, 6);
			this.TypeLbl.Name = "TypeLbl";
			this.TypeLbl.Size = new System.Drawing.Size(65, 13);
			this.TypeLbl.TabIndex = 0;
			this.TypeLbl.Text = "Quest Type:";
			this.TypeBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.TypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.TypeBox.FormattingEnabled = true;
			this.TypeBox.Location = new Point(74, 3);
			this.TypeBox.Name = "TypeBox";
			this.TypeBox.Size = new System.Drawing.Size(195, 21);
			this.TypeBox.TabIndex = 1;
			this.TypeBox.SelectedIndexChanged += new EventHandler(this.TypeBox_SelectedIndexChanged);
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(3, 32);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new System.Drawing.Size(36, 13);
			this.LevelLbl.TabIndex = 2;
			this.LevelLbl.Text = "Level:";
			this.LevelBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.LevelBox.Location = new Point(74, 30);
			NumericUpDown levelBox = this.LevelBox;
			int[] numArray = new int[] { 30, 0, 0, 0 };
			levelBox.Maximum = new decimal(numArray);
			NumericUpDown num = this.LevelBox;
			int[] numArray1 = new int[] { 1, 0, 0, 0 };
			num.Minimum = new decimal(numArray1);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.Size = new System.Drawing.Size(195, 20);
			this.LevelBox.TabIndex = 3;
			NumericUpDown numericUpDown = this.LevelBox;
			int[] numArray2 = new int[] { 1, 0, 0, 0 };
			numericUpDown.Value = new decimal(numArray2);
			this.LevelBox.ValueChanged += new EventHandler(this.LevelBox_ValueChanged);
			this.XPLbl.AutoSize = true;
			this.XPLbl.Location = new Point(3, 59);
			this.XPLbl.Name = "XPLbl";
			this.XPLbl.Size = new System.Drawing.Size(54, 13);
			this.XPLbl.TabIndex = 4;
			this.XPLbl.Text = "XP Value:";
			this.XPBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.XPBox.Location = new Point(74, 56);
			this.XPBox.Name = "XPBox";
			this.XPBox.ReadOnly = true;
			this.XPBox.Size = new System.Drawing.Size(195, 20);
			this.XPBox.TabIndex = 5;
			this.XPSlider.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.XPSlider.LargeChange = 100;
			this.XPSlider.Location = new Point(3, 4);
			this.XPSlider.Maximum = 100;
			this.XPSlider.Name = "XPSlider";
			this.XPSlider.Size = new System.Drawing.Size(190, 45);
			this.XPSlider.SmallChange = 50;
			this.XPSlider.TabIndex = 6;
			this.XPSlider.TickFrequency = 50;
			this.XPSlider.Scroll += new EventHandler(this.XPSlider_Scroll);
			this.MinorPnl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.MinorPnl.BackColor = SystemColors.ControlLightLight;
			this.MinorPnl.Controls.Add(this.MinMaxLbl);
			this.MinorPnl.Controls.Add(this.XPSlider);
			this.MinorPnl.Location = new Point(74, 82);
			this.MinorPnl.Name = "MinorPnl";
			this.MinorPnl.Size = new System.Drawing.Size(195, 73);
			this.MinorPnl.TabIndex = 7;
			this.MinMaxLbl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.MinMaxLbl.ForeColor = SystemColors.ControlDarkDark;
			this.MinMaxLbl.Location = new Point(3, 52);
			this.MinMaxLbl.Name = "MinMaxLbl";
			this.MinMaxLbl.Size = new System.Drawing.Size(189, 13);
			this.MinMaxLbl.TabIndex = 7;
			this.MinMaxLbl.Text = "[min] - [max]";
			this.MinMaxLbl.TextAlign = ContentAlignment.MiddleCenter;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.MinorPnl);
			base.Controls.Add(this.XPBox);
			base.Controls.Add(this.XPLbl);
			base.Controls.Add(this.LevelBox);
			base.Controls.Add(this.LevelLbl);
			base.Controls.Add(this.TypeBox);
			base.Controls.Add(this.TypeLbl);
			base.Name = "QuestPanel";
			base.Size = new System.Drawing.Size(272, 160);
			((ISupportInitialize)this.LevelBox).EndInit();
			((ISupportInitialize)this.XPSlider).EndInit();
			this.MinorPnl.ResumeLayout(false);
			this.MinorPnl.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void LevelBox_ValueChanged(object sender, EventArgs e)
		{
			if (this.fUpdating)
			{
				return;
			}
			this.fQuest.Level = (int)this.LevelBox.Value;
			this.update_view();
		}

		private void TypeBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.fUpdating)
			{
				return;
			}
			this.fQuest.Type = (QuestType)this.TypeBox.SelectedItem;
			this.update_view();
		}

		private void update_view()
		{
			this.fUpdating = true;
			this.TypeBox.SelectedItem = this.fQuest.Type;
			this.LevelBox.Value = this.fQuest.Level;
			this.XPSlider.Visible = this.fQuest.Type == QuestType.Minor;
			if (this.XPSlider.Visible)
			{
				Pair<int, int> minorQuestXP = Experience.GetMinorQuestXP(this.fQuest.Level);
				this.XPSlider.SetRange(minorQuestXP.First, minorQuestXP.Second);
				this.MinMaxLbl.Text = string.Concat(minorQuestXP.First, " - ", minorQuestXP.Second);
				if (this.fQuest.XP < minorQuestXP.First)
				{
					this.fQuest.XP = minorQuestXP.First;
				}
				if (this.fQuest.XP > minorQuestXP.Second)
				{
					this.fQuest.XP = minorQuestXP.Second;
				}
				this.XPSlider.Value = this.fQuest.XP;
			}
			this.XPBox.Text = string.Concat(this.fQuest.GetXP(), " XP");
			this.fUpdating = false;
		}

		private void XPSlider_Scroll(object sender, EventArgs e)
		{
			if (this.fUpdating)
			{
				return;
			}
			this.fQuest.XP = this.XPSlider.Value;
			this.update_view();
		}
	}
}