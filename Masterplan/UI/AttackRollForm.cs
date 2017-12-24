using Masterplan;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class AttackRollForm : Form
	{
		private CreaturePower fPower;

		private Encounter fEncounter;

		private bool fAddedCombatant;

		private List<Pair<CombatData, int>> fRolls = new List<Pair<CombatData, int>>();

		private IContainer components;

		private Button OKBtn;

		private WebBrowser PowerBrowser;

		private ListView RollList;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private TabControl Pages;

		private TabPage AttackPage;

		private TabPage DamagePage;

		private Label MissLbl;

		private Label CritLbl;

		private NumericUpDown DamageBox;

		private Label HitLbl;

		private Label DamageExpLbl;

		private Label DamageInfoLbl;

		private ColumnHeader columnHeader4;

		private Label MissValueLbl;

		private Label CritValueLbl;

		private Button RollDamageBtn;

		private SplitContainer Splitter;

		private CheckBox ApplyDamageBox;

		public Pair<CombatData, int> SelectedRoll
		{
			get
			{
				if (this.RollList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.RollList.SelectedItems[0].Tag as Pair<CombatData, int>;
			}
		}

		public AttackRollForm(CreaturePower power, Encounter enc)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fPower = power;
			this.fEncounter = enc;
			this.add_attack_roll(null);
			this.update_damage();
			this.RollDamageBtn_Click(null, null);
		}

		private void add_attack_roll(CombatData cd)
		{
			if (cd != null && this.fRolls.Count == 1 && this.fRolls[0].First == null)
			{
				this.fRolls.Clear();
			}
			int num = Session.Dice(1, 20);
			this.fRolls.Add(new Pair<CombatData, int>(cd, num));
			if (cd != null)
			{
				this.fAddedCombatant = true;
			}
			this.update_list();
			this.update_power();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.ApplyDamageBox.Visible = this.fAddedCombatant;
		}

		private void apply_damage()
		{
			EncounterCard card;
			foreach (ListViewItem item in this.RollList.Items)
			{
				Pair<CombatData, int> tag = item.Tag as Pair<CombatData, int>;
				if (tag.First == null)
				{
					continue;
				}
				int value = 0;
				if (tag.Second == 20)
				{
					value = int.Parse(this.CritValueLbl.Text);
				}
				else if (item.ForeColor == SystemColors.WindowText)
				{
					value = (int)this.DamageBox.Value;
				}
				if (value == 0)
				{
					continue;
				}
				Array values = Enum.GetValues(typeof(DamageType));
				List<DamageType> damageTypes = new List<DamageType>();
				foreach (DamageType damageType in values)
				{
					if (!this.fPower.Details.ToLower().Contains(damageType.ToString().ToLower()))
					{
						continue;
					}
					damageTypes.Add(damageType);
				}
				EncounterSlot encounterSlot = this.fEncounter.FindSlot(tag.First);
				if (encounterSlot != null)
				{
					card = encounterSlot.Card;
				}
				else
				{
					card = null;
				}
				DamageForm.DoDamage(tag.First, card, value, damageTypes, false);
			}
		}

		private void DamageBox_ValueChanged(object sender, EventArgs e)
		{
			int value = (int)this.DamageBox.Value / 2;
			this.MissValueLbl.Text = value.ToString();
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
			this.OKBtn = new Button();
			this.PowerBrowser = new WebBrowser();
			this.RollList = new ListView();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.Pages = new TabControl();
			this.AttackPage = new TabPage();
			this.DamagePage = new TabPage();
			this.RollDamageBtn = new Button();
			this.MissValueLbl = new Label();
			this.CritValueLbl = new Label();
			this.MissLbl = new Label();
			this.CritLbl = new Label();
			this.DamageBox = new NumericUpDown();
			this.HitLbl = new Label();
			this.DamageExpLbl = new Label();
			this.DamageInfoLbl = new Label();
			this.Splitter = new SplitContainer();
			this.ApplyDamageBox = new CheckBox();
			this.Pages.SuspendLayout();
			this.AttackPage.SuspendLayout();
			this.DamagePage.SuspendLayout();
			((ISupportInitialize)this.DamageBox).BeginInit();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(280, 345);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 2;
			this.OKBtn.Text = "Close";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.PowerBrowser.AllowWebBrowserDrop = false;
			this.PowerBrowser.Dock = DockStyle.Fill;
			this.PowerBrowser.IsWebBrowserContextMenuEnabled = false;
			this.PowerBrowser.Location = new Point(0, 0);
			this.PowerBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.PowerBrowser.Name = "PowerBrowser";
			this.PowerBrowser.ScriptErrorsSuppressed = true;
			this.PowerBrowser.Size = new System.Drawing.Size(343, 163);
			this.PowerBrowser.TabIndex = 0;
			this.PowerBrowser.WebBrowserShortcutsEnabled = false;
			this.PowerBrowser.Navigating += new WebBrowserNavigatingEventHandler(this.PowerBrowser_Navigating);
			ListView.ColumnHeaderCollection columns = this.RollList.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader4, this.columnHeader1, this.columnHeader2, this.columnHeader3 };
			columns.AddRange(columnHeaderArray);
			this.RollList.Dock = DockStyle.Fill;
			this.RollList.FullRowSelect = true;
			this.RollList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.RollList.HideSelection = false;
			this.RollList.Location = new Point(3, 3);
			this.RollList.Name = "RollList";
			this.RollList.Size = new System.Drawing.Size(329, 128);
			this.RollList.TabIndex = 0;
			this.RollList.UseCompatibleStateImageBehavior = false;
			this.RollList.View = View.Details;
			this.RollList.DoubleClick += new EventHandler(this.RollList_DoubleClick);
			this.columnHeader4.Text = "Target";
			this.columnHeader4.Width = 120;
			this.columnHeader1.Text = "Roll";
			this.columnHeader1.TextAlign = HorizontalAlignment.Right;
			this.columnHeader2.Text = "Bonus";
			this.columnHeader2.TextAlign = HorizontalAlignment.Right;
			this.columnHeader3.Text = "Total";
			this.columnHeader3.TextAlign = HorizontalAlignment.Right;
			this.Pages.Controls.Add(this.AttackPage);
			this.Pages.Controls.Add(this.DamagePage);
			this.Pages.Dock = DockStyle.Fill;
			this.Pages.Location = new Point(0, 0);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new System.Drawing.Size(343, 160);
			this.Pages.TabIndex = 0;
			this.AttackPage.Controls.Add(this.RollList);
			this.AttackPage.Location = new Point(4, 22);
			this.AttackPage.Name = "AttackPage";
			this.AttackPage.Padding = new System.Windows.Forms.Padding(3);
			this.AttackPage.Size = new System.Drawing.Size(335, 134);
			this.AttackPage.TabIndex = 0;
			this.AttackPage.Text = "Attack Rolls";
			this.AttackPage.UseVisualStyleBackColor = true;
			this.DamagePage.Controls.Add(this.RollDamageBtn);
			this.DamagePage.Controls.Add(this.MissValueLbl);
			this.DamagePage.Controls.Add(this.CritValueLbl);
			this.DamagePage.Controls.Add(this.MissLbl);
			this.DamagePage.Controls.Add(this.CritLbl);
			this.DamagePage.Controls.Add(this.DamageBox);
			this.DamagePage.Controls.Add(this.HitLbl);
			this.DamagePage.Controls.Add(this.DamageExpLbl);
			this.DamagePage.Controls.Add(this.DamageInfoLbl);
			this.DamagePage.Location = new Point(4, 22);
			this.DamagePage.Name = "DamagePage";
			this.DamagePage.Padding = new System.Windows.Forms.Padding(3);
			this.DamagePage.Size = new System.Drawing.Size(335, 134);
			this.DamagePage.TabIndex = 1;
			this.DamagePage.Text = "Damage Rolls";
			this.DamagePage.UseVisualStyleBackColor = true;
			this.RollDamageBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.RollDamageBtn.Location = new Point(254, 33);
			this.RollDamageBtn.Name = "RollDamageBtn";
			this.RollDamageBtn.Size = new System.Drawing.Size(75, 23);
			this.RollDamageBtn.TabIndex = 9;
			this.RollDamageBtn.Text = "Reroll";
			this.RollDamageBtn.UseVisualStyleBackColor = true;
			this.RollDamageBtn.Click += new EventHandler(this.RollDamageBtn_Click);
			this.MissValueLbl.AutoSize = true;
			this.MissValueLbl.Location = new Point(135, 90);
			this.MissValueLbl.Name = "MissValueLbl";
			this.MissValueLbl.Size = new System.Drawing.Size(33, 13);
			this.MissValueLbl.TabIndex = 8;
			this.MissValueLbl.Text = "[miss]";
			this.CritValueLbl.AutoSize = true;
			this.CritValueLbl.Location = new Point(135, 64);
			this.CritValueLbl.Name = "CritValueLbl";
			this.CritValueLbl.Size = new System.Drawing.Size(27, 13);
			this.CritValueLbl.TabIndex = 7;
			this.CritValueLbl.Text = "[crit]";
			this.MissLbl.AutoSize = true;
			this.MissLbl.Location = new Point(6, 90);
			this.MissLbl.Name = "MissLbl";
			this.MissLbl.Size = new System.Drawing.Size(74, 13);
			this.MissLbl.TabIndex = 6;
			this.MissLbl.Text = "On Miss (half):";
			this.CritLbl.AutoSize = true;
			this.CritLbl.Location = new Point(6, 64);
			this.CritLbl.Name = "CritLbl";
			this.CritLbl.Size = new System.Drawing.Size(86, 13);
			this.CritLbl.TabIndex = 4;
			this.CritLbl.Text = "On Critical (max):";
			this.DamageBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.DamageBox.Location = new Point(138, 36);
			this.DamageBox.Name = "DamageBox";
			this.DamageBox.Size = new System.Drawing.Size(110, 20);
			this.DamageBox.TabIndex = 3;
			this.DamageBox.ValueChanged += new EventHandler(this.DamageBox_ValueChanged);
			this.HitLbl.AutoSize = true;
			this.HitLbl.Location = new Point(6, 38);
			this.HitLbl.Name = "HitLbl";
			this.HitLbl.Size = new System.Drawing.Size(40, 13);
			this.HitLbl.TabIndex = 2;
			this.HitLbl.Text = "On Hit:";
			this.DamageExpLbl.AutoSize = true;
			this.DamageExpLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.DamageExpLbl.Location = new Point(135, 13);
			this.DamageExpLbl.Name = "DamageExpLbl";
			this.DamageExpLbl.Size = new System.Drawing.Size(38, 13);
			this.DamageExpLbl.TabIndex = 1;
			this.DamageExpLbl.Text = "[dmg]";
			this.DamageInfoLbl.AutoSize = true;
			this.DamageInfoLbl.Location = new Point(6, 13);
			this.DamageInfoLbl.Name = "DamageInfoLbl";
			this.DamageInfoLbl.Size = new System.Drawing.Size(50, 13);
			this.DamageInfoLbl.TabIndex = 0;
			this.DamageInfoLbl.Text = "Damage:";
			this.Splitter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Orientation = Orientation.Horizontal;
			this.Splitter.Panel1.Controls.Add(this.PowerBrowser);
			this.Splitter.Panel2.Controls.Add(this.Pages);
			this.Splitter.Size = new System.Drawing.Size(343, 327);
			this.Splitter.SplitterDistance = 163;
			this.Splitter.TabIndex = 0;
			this.ApplyDamageBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.ApplyDamageBox.AutoSize = true;
			this.ApplyDamageBox.Location = new Point(12, 349);
			this.ApplyDamageBox.Name = "ApplyDamageBox";
			this.ApplyDamageBox.Size = new System.Drawing.Size(136, 17);
			this.ApplyDamageBox.TabIndex = 1;
			this.ApplyDamageBox.Text = "Apply damage on close";
			this.ApplyDamageBox.UseVisualStyleBackColor = true;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(367, 380);
			base.Controls.Add(this.ApplyDamageBox);
			base.Controls.Add(this.Splitter);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AttackRollForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Attack Roll";
			this.Pages.ResumeLayout(false);
			this.AttackPage.ResumeLayout(false);
			this.DamagePage.ResumeLayout(false);
			this.DamagePage.PerformLayout();
			((ISupportInitialize)this.DamageBox).EndInit();
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			if (this.ApplyDamageBox.Visible && this.ApplyDamageBox.Checked)
			{
				this.apply_damage();
			}
		}

		private void PowerBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.Scheme == "opponent")
			{
				e.Cancel = true;
				Guid guid = new Guid(e.Url.LocalPath);
				this.add_attack_roll(this.fEncounter.FindCombatData(guid));
			}
			if (e.Url.Scheme == "hero")
			{
				e.Cancel = true;
				Guid guid1 = new Guid(e.Url.LocalPath);
				Hero hero = Session.Project.FindHero(guid1);
				if (hero != null)
				{
					this.add_attack_roll(hero.CombatData);
				}
			}
			if (e.Url.Scheme == "target")
			{
				e.Cancel = true;
				this.add_attack_roll(null);
			}
		}

		private bool roll_exists(Guid id)
		{
			bool flag;
			List<Pair<CombatData, int>>.Enumerator enumerator = this.fRolls.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Pair<CombatData, int> current = enumerator.Current;
					if (current.First == null || !(current.First.ID == id))
					{
						continue;
					}
					flag = true;
					return flag;
				}
				return false;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return flag;
		}

		private void RollDamageBtn_Click(object sender, EventArgs e)
		{
			DiceExpression diceExpression = DiceExpression.Parse(this.DamageExpLbl.Text);
			if (diceExpression != null)
			{
				int num = diceExpression.Evaluate();
				this.DamageBox.Value = num;
			}
		}

		private void RollList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedRoll != null)
			{
				int num = Session.Dice(1, 20);
				this.SelectedRoll.Second = num;
				this.update_list();
			}
		}

		private void update_damage()
		{
			string damage = this.fPower.Damage;
			if (damage == "")
			{
				this.Pages.TabPages.Remove(this.DamagePage);
				return;
			}
			DiceExpression diceExpression = DiceExpression.Parse(damage);
			this.DamageExpLbl.Text = damage;
			this.CritValueLbl.Text = diceExpression.Maximum.ToString();
		}

		private void update_list()
		{
			this.RollList.Items.Clear();
			foreach (Pair<CombatData, int> fRoll in this.fRolls)
			{
				int second = fRoll.Second;
				int num = (this.fPower.Attack != null ? this.fPower.Attack.Bonus : 0);
				int num1 = second + num;
				ListViewItem font = this.RollList.Items.Add((fRoll.First != null ? fRoll.First.DisplayName : "Roll"));
				font.SubItems.Add(second.ToString());
				font.SubItems.Add(num.ToString());
				font.SubItems.Add(num1.ToString());
				bool flag = true;
				if (fRoll.First != null && this.fPower.Attack != null)
				{
					int aC = 0;
					Hero hero = Session.Project.FindHero(fRoll.First.ID);
					if (hero == null)
					{
						EncounterSlot encounterSlot = this.fEncounter.FindSlot(fRoll.First);
						switch (this.fPower.Attack.Defence)
						{
							case DefenceType.AC:
							{
								aC = encounterSlot.Card.AC;
								break;
							}
							case DefenceType.Fortitude:
							{
								aC = encounterSlot.Card.Fortitude;
								break;
							}
							case DefenceType.Reflex:
							{
								aC = encounterSlot.Card.Reflex;
								break;
							}
							case DefenceType.Will:
							{
								aC = encounterSlot.Card.Will;
								break;
							}
						}
					}
					else
					{
						switch (this.fPower.Attack.Defence)
						{
							case DefenceType.AC:
							{
								aC = hero.AC;
								break;
							}
							case DefenceType.Fortitude:
							{
								aC = hero.Fortitude;
								break;
							}
							case DefenceType.Reflex:
							{
								aC = hero.Reflex;
								break;
							}
							case DefenceType.Will:
							{
								aC = hero.Will;
								break;
							}
						}
					}
					foreach (OngoingCondition condition in fRoll.First.Conditions)
					{
						if (condition.Type != OngoingType.DefenceModifier || !condition.Defences.Contains(this.fPower.Attack.Defence))
						{
							continue;
						}
						aC += condition.DefenceMod;
					}
					flag = num1 >= aC;
				}
				if (second == 20)
				{
					font.Font = new System.Drawing.Font(font.Font, font.Font.Style | FontStyle.Bold);
				}
				else if (second == 1)
				{
					font.ForeColor = Color.Red;
				}
				else if (!flag)
				{
					font.ForeColor = SystemColors.GrayText;
				}
				font.Tag = fRoll;
			}
		}

		private void update_power()
		{
			List<string> strs = new List<string>();
			strs.AddRange(HTML.GetHead(this.fPower.Name, "", DisplaySize.Small));
			strs.Add("<BODY>");
			strs.Add("<P class=table>");
			strs.Add("<TABLE>");
			strs.AddRange(this.fPower.AsHTML(null, CardMode.View, false));
			strs.Add("</TABLE>");
			strs.Add("</P>");
			strs.Add("<P class=instruction align=left>");
			strs.Add("Click to add an attack roll for:");
			string str = "";
			foreach (Hero hero in Session.Project.Heroes)
			{
				CombatData combatData = hero.CombatData;
				if (this.roll_exists(hero.ID) || hero.GetState(combatData.Damage) == CreatureState.Defeated)
				{
					continue;
				}
				if (str != "")
				{
					str = string.Concat(str, " | ");
				}
				object obj = str;
				object[] d = new object[] { obj, "<A href=hero:", hero.ID, ">", hero.Name, "</A>" };
				str = string.Concat(d);
			}
			if (str != "")
			{
				strs.Add("<BR>");
				strs.Add(str);
			}
			string str1 = "";
			foreach (EncounterSlot slot in this.fEncounter.Slots)
			{
				foreach (CombatData combatDatum in slot.CombatData)
				{
					if (this.roll_exists(combatDatum.ID) || slot.GetState(combatDatum) == CreatureState.Defeated)
					{
						continue;
					}
					if (str1 != "")
					{
						str1 = string.Concat(str1, " | ");
					}
					object obj1 = str1;
					object[] objArray = new object[] { obj1, "<A href=opponent:", combatDatum.ID, ">", combatDatum.DisplayName, "</A>" };
					str1 = string.Concat(objArray);
				}
			}
			if (str1 != "")
			{
				strs.Add("<BR>");
				strs.Add(str1);
			}
			strs.Add("<BR>");
			strs.Add("<A href=target:blank>An unnamed target</A>");
			strs.Add("</P>");
			strs.Add("</BODY>");
			strs.Add("</HTML>");
			this.PowerBrowser.DocumentText = HTML.Concatenate(strs);
		}
	}
}