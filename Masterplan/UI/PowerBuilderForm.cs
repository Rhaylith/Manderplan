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
	internal class PowerBuilderForm : Form
	{
		private CreaturePower fPower;

		private ICreature fCreature;

		private bool fFromFunctionalTemplate;

		private List<string> fExamples = new List<string>();

		private ToolStrip Toolbar;

		private Panel BtnPnl;

		private Button CancelBtn;

		private Button OKBtn;

		private WebBrowser StatBlockBrowser;

		private ToolStripButton PowerBrowserBtn;

		public CreaturePower Power
		{
			get
			{
				return this.fPower;
			}
		}

		public PowerBuilderForm(CreaturePower power, ICreature creature, bool functional_template)
		{
			this.InitializeComponent();
			this.fPower = power;
			this.fCreature = creature;
			this.fFromFunctionalTemplate = functional_template;
			this.refresh_examples();
			this.update_statblock();
		}

		private void Browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			IRole role;
			if (e.Url.Scheme == "power")
			{
				if (e.Url.LocalPath == "info")
				{
					e.Cancel = true;
					PowerInfoForm powerInfoForm = new PowerInfoForm(this.fPower);
					if (powerInfoForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fPower.Name = powerInfoForm.PowerName;
						this.fPower.Keywords = powerInfoForm.PowerKeywords;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "action")
				{
					e.Cancel = true;
					PowerActionForm powerActionForm = new PowerActionForm(this.fPower.Action);
					if (powerActionForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fPower.Action = powerActionForm.Action;
						this.refresh_examples();
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "prerequisite")
				{
					e.Cancel = true;
					DetailsForm detailsForm = new DetailsForm(this.fPower.Condition, "Power Prerequisite", null);
					if (detailsForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fPower.Condition = detailsForm.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "range")
				{
					e.Cancel = true;
					PowerRangeForm powerRangeForm = new PowerRangeForm(this.fPower);
					if (powerRangeForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fPower.Range = powerRangeForm.PowerRange;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "attack")
				{
					e.Cancel = true;
					PowerAttack attack = this.fPower.Attack ?? new PowerAttack();
					int num = (this.fCreature != null ? this.fCreature.Level : 0);
					if (this.fCreature != null)
					{
						role = this.fCreature.Role;
					}
					else
					{
						role = null;
					}
					PowerAttackForm powerAttackForm = new PowerAttackForm(attack, this.fFromFunctionalTemplate, num, role);
					if (powerAttackForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fPower.Attack = powerAttackForm.Attack;
						this.refresh_examples();
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "clearattack")
				{
					e.Cancel = true;
					this.fPower.Attack = null;
					this.refresh_examples();
					this.update_statblock();
				}
				if (e.Url.LocalPath == "details")
				{
					e.Cancel = true;
					PowerDetailsForm powerDetailsForm = new PowerDetailsForm(this.fPower.Details, this.fCreature);
					if (powerDetailsForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fPower.Details = powerDetailsForm.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "desc")
				{
					e.Cancel = true;
					DetailsForm detailsForm1 = new DetailsForm(this.fPower.Description, "Power Description", null);
					if (detailsForm1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fPower.Description = detailsForm1.Details;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "details")
			{
				if (e.Url.LocalPath == "refresh")
				{
					e.Cancel = true;
					this.refresh_examples();
					this.update_statblock();
				}
				try
				{
					int num1 = int.Parse(e.Url.LocalPath);
					e.Cancel = true;
					this.fPower.Details = this.fExamples[num1];
					this.fExamples.RemoveAt(num1);
					if (this.fExamples.Count == 0)
					{
						this.refresh_examples();
					}
					this.update_statblock();
				}
				catch
				{
				}
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(PowerBuilderForm));
			this.Toolbar = new ToolStrip();
			this.PowerBrowserBtn = new ToolStripButton();
			this.BtnPnl = new Panel();
			this.CancelBtn = new Button();
			this.OKBtn = new Button();
			this.StatBlockBrowser = new WebBrowser();
			this.Toolbar.SuspendLayout();
			this.BtnPnl.SuspendLayout();
			base.SuspendLayout();
			this.Toolbar.Items.AddRange(new ToolStripItem[] { this.PowerBrowserBtn });
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(664, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.PowerBrowserBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PowerBrowserBtn.Image = (Image)componentResourceManager.GetObject("PowerBrowserBtn.Image");
			this.PowerBrowserBtn.ImageTransparentColor = Color.Magenta;
			this.PowerBrowserBtn.Name = "PowerBrowserBtn";
			this.PowerBrowserBtn.Size = new System.Drawing.Size(89, 22);
			this.PowerBrowserBtn.Text = "Power Browser";
			this.PowerBrowserBtn.Click += new EventHandler(this.PowerBrowserBtn_Click);
			this.BtnPnl.Controls.Add(this.CancelBtn);
			this.BtnPnl.Controls.Add(this.OKBtn);
			this.BtnPnl.Dock = DockStyle.Bottom;
			this.BtnPnl.Location = new Point(0, 333);
			this.BtnPnl.Name = "BtnPnl";
			this.BtnPnl.Size = new System.Drawing.Size(664, 35);
			this.BtnPnl.TabIndex = 2;
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(577, 6);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 1;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(496, 6);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 0;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.StatBlockBrowser.AllowWebBrowserDrop = false;
			this.StatBlockBrowser.Dock = DockStyle.Fill;
			this.StatBlockBrowser.IsWebBrowserContextMenuEnabled = false;
			this.StatBlockBrowser.Location = new Point(0, 25);
			this.StatBlockBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.StatBlockBrowser.Name = "StatBlockBrowser";
			this.StatBlockBrowser.ScriptErrorsSuppressed = true;
			this.StatBlockBrowser.Size = new System.Drawing.Size(664, 308);
			this.StatBlockBrowser.TabIndex = 2;
			this.StatBlockBrowser.WebBrowserShortcutsEnabled = false;
			this.StatBlockBrowser.Navigating += new WebBrowserNavigatingEventHandler(this.Browser_Navigating);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(664, 368);
			base.Controls.Add(this.StatBlockBrowser);
			base.Controls.Add(this.BtnPnl);
			base.Controls.Add(this.Toolbar);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PowerBuilderForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Power Builder";
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.BtnPnl.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void PowerBrowserBtn_Click(object sender, EventArgs e)
		{
			IRole role;
			int num = (this.fCreature != null ? this.fCreature.Level : 0);
			if (this.fCreature != null)
			{
				role = this.fCreature.Role;
			}
			else
			{
				role = null;
			}
			PowerBrowserForm powerBrowserForm = new PowerBrowserForm(null, num, role, null);
			if (powerBrowserForm.ShowDialog() == System.Windows.Forms.DialogResult.OK && powerBrowserForm.SelectedPower != null)
			{
				this.fPower = powerBrowserForm.SelectedPower.Copy();
				this.fPower.ID = Guid.NewGuid();
				this.update_statblock();
			}
		}

		private void refresh_examples()
		{
			this.fExamples.Clear();
			List<ICreature> creatures = new List<ICreature>();
			foreach (Creature creature in Session.Creatures)
			{
				if (creature == null || creature.Level != this.fCreature.Level || !(creature.Role.ToString() == this.fCreature.Role.ToString()))
				{
					continue;
				}
				creatures.Add(creature);
			}
			if (Session.Project != null)
			{
				foreach (CustomCreature customCreature in Session.Project.CustomCreatures)
				{
					if (customCreature == null || customCreature.Level != this.fCreature.Level || !(customCreature.Role.ToString() == this.fCreature.Role.ToString()))
					{
						continue;
					}
					creatures.Add(customCreature);
				}
			}
			List<string> strs = new List<string>();
			foreach (ICreature creature1 in creatures)
			{
				foreach (CreaturePower creaturePower in creature1.CreaturePowers)
				{
					if (this.fPower.Category != creaturePower.Category || creaturePower.Details == "")
					{
						continue;
					}
					strs.Add(creaturePower.Details);
				}
			}
			if (strs.Count != 0)
			{
				for (int i = 0; i != 5; i++)
				{
					if (strs.Count == 0)
					{
						return;
					}
					int num = Session.Random.Next(strs.Count);
					string item = strs[num];
					strs.RemoveAt(num);
					this.fExamples.Add(item);
				}
			}
		}

		private void update_statblock()
		{
			IRole role;
			int num = (this.fCreature != null ? this.fCreature.Level : 0);
			if (this.fCreature != null)
			{
				role = this.fCreature.Role;
			}
			else
			{
				role = null;
			}
			IRole role1 = role;
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<TABLE class=clear>");
			head.Add("<TR class=clear>");
			head.Add("<TD class=clear>");
			head.Add("<P class=table>");
			head.Add("<TABLE>");
			head.AddRange(this.fPower.AsHTML(null, CardMode.Build, this.fFromFunctionalTemplate));
			head.Add("</TABLE>");
			head.Add("</P>");
			head.Add("</TD>");
			head.Add("<TD class=clear>");
			head.Add("<P class=table>");
			head.Add("<TABLE>");
			head.Add("<TR class=heading>");
			head.Add("<TD colspan=2><B>Power Advice</B></TD>");
			head.Add("</TR>");
			head.Add("<TR class=shaded>");
			head.Add("<TD colspan=2><B>Attack Bonus</B></TD>");
			head.Add("</TR>");
			head.Add("<TR>");
			head.Add("<TD>Attack vs Armour Class</TD>");
			head.Add(string.Concat("<TD align=center>+", Statistics.AttackBonus(DefenceType.AC, num, role1), "</TD>"));
			head.Add("</TR>");
			head.Add("<TR>");
			head.Add("<TD>Attack vs Other Defence</TD>");
			head.Add(string.Concat("<TD align=center>+", Statistics.AttackBonus(DefenceType.Fortitude, num, role1), "</TD>"));
			head.Add("</TR>");
			if (role1 != null)
			{
				head.Add("<TR class=shaded>");
				head.Add("<TD colspan=2><B>Damage</B></TD>");
				head.Add("</TR>");
				if (!(role1 is Minion))
				{
					head.Add("<TR>");
					head.Add("<TD>Damage vs Single Targets</TD>");
					head.Add(string.Concat("<TD align=center>", Statistics.Damage(num, DamageExpressionType.Normal), "</TD>"));
					head.Add("</TR>");
					head.Add("<TR>");
					head.Add("<TD>Damage vs Multiple Targets</TD>");
					head.Add(string.Concat("<TD align=center>", Statistics.Damage(num, DamageExpressionType.Multiple), "</TD>"));
					head.Add("</TR>");
				}
				else
				{
					head.Add("<TR>");
					head.Add("<TD>Minion Damage</TD>");
					head.Add(string.Concat("<TD align=center>", Statistics.Damage(num, DamageExpressionType.Minion), "</TD>"));
					head.Add("</TR>");
				}
				if (this.fExamples.Count != 0)
				{
					head.Add("<TR class=shaded>");
					head.Add("<TD><B>Example Power Details</B></TD>");
					head.Add("<TD align=center><A href=details:refresh>(refresh)</A></TD>");
					head.Add("</TR>");
					foreach (string fExample in this.fExamples)
					{
						int num1 = this.fExamples.IndexOf(fExample);
						head.Add("<TR>");
						object[] objArray = new object[] { "<TD colspan=2>", fExample, " <A href=details:", num1, ">(use this)</A></TD>" };
						head.Add(string.Concat(objArray));
						head.Add("</TR>");
					}
				}
			}
			head.Add("</TABLE>");
			head.Add("</P>");
			head.Add("</TD>");
			head.Add("</TR>");
			head.Add("</TABLE>");
			head.Add("</BODY>");
			head.Add("</HTML>");
			this.StatBlockBrowser.DocumentText = HTML.Concatenate(head);
		}
	}
}