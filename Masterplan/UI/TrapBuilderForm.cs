using Masterplan;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class TrapBuilderForm : Form
	{
		private Masterplan.Data.Trap fTrap;

		private Panel BtnPnl;

		private Button CancelBtn;

		private Button OKBtn;

		private WebBrowser StatBlockBrowser;

		private ToolStrip Toolbar;

		private ToolStripDropDownButton OptionsMenu;

		private ToolStripMenuItem OptionsCopy;

		private ToolStripButton LevelDownBtn;

		private ToolStripButton LevelUpBtn;

		private ToolStripLabel LevelLbl;

		private ToolStripDropDownButton FileMenu;

		private ToolStripMenuItem FileExport;

		public Masterplan.Data.Trap Trap
		{
			get
			{
				return this.fTrap;
			}
		}

		public TrapBuilderForm(Masterplan.Data.Trap trap)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fTrap = trap.Copy();
			this.update_statblock();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.LevelDownBtn.Enabled = this.fTrap.Level > 1;
		}

		private void Browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.Scheme == "build")
			{
				if (e.Url.LocalPath == "profile")
				{
					e.Cancel = true;
					TrapProfileForm trapProfileForm = new TrapProfileForm(this.fTrap);
					if (trapProfileForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fTrap.Name = trapProfileForm.Trap.Name;
						this.fTrap.Type = trapProfileForm.Trap.Type;
						this.fTrap.Level = trapProfileForm.Trap.Level;
						this.fTrap.Role = trapProfileForm.Trap.Role;
						this.fTrap.Initiative = trapProfileForm.Trap.Initiative;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "readaloud")
				{
					e.Cancel = true;
					DetailsForm detailsForm = new DetailsForm(this.fTrap.ReadAloud, "Read-Aloud Text", null);
					if (detailsForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fTrap.ReadAloud = detailsForm.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "desc")
				{
					e.Cancel = true;
					DetailsForm detailsForm1 = new DetailsForm(this.fTrap.Description, "Description", null);
					if (detailsForm1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fTrap.Description = detailsForm1.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "details")
				{
					e.Cancel = true;
					DetailsForm detailsForm2 = new DetailsForm(this.fTrap.Details, "Details", null);
					if (detailsForm2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fTrap.Details = detailsForm2.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "addskill")
				{
					e.Cancel = true;
					TrapSkillData trapSkillDatum = new TrapSkillData()
					{
						SkillName = "Perception",
						DC = AI.GetSkillDC(Difficulty.Moderate, this.fTrap.Level)
					};
					TrapSkillForm trapSkillForm = new TrapSkillForm(trapSkillDatum, this.fTrap.Level);
					if (trapSkillForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fTrap.Skills.Add(trapSkillForm.SkillData);
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "addattack")
				{
					e.Cancel = true;
					TrapAttack trapAttack = new TrapAttack()
					{
						Name = "Attack"
					};
					this.fTrap.Attacks.Add(trapAttack);
					this.update_statblock();
				}
				if (e.Url.LocalPath == "addcm")
				{
					e.Cancel = true;
					TrapCountermeasureForm trapCountermeasureForm = new TrapCountermeasureForm("", this.fTrap.Level);
					if (trapCountermeasureForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fTrap.Countermeasures.Add(trapCountermeasureForm.Countermeasure);
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "trigger")
				{
					e.Cancel = true;
					DetailsForm detailsForm3 = new DetailsForm(this.fTrap.Trigger, "Trigger", null);
					if (detailsForm3.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fTrap.Trigger = detailsForm3.Details;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "attackaction")
			{
				e.Cancel = true;
				Guid guid = new Guid(e.Url.LocalPath);
				TrapAttack name = this.fTrap.FindAttack(guid);
				if (name != null)
				{
					TrapActionForm trapActionForm = new TrapActionForm(name);
					if (trapActionForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						name.Name = trapActionForm.Attack.Name;
						name.Action = trapActionForm.Attack.Action;
						name.Range = trapActionForm.Attack.Range;
						name.Target = trapActionForm.Attack.Target;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "attackremove")
			{
				e.Cancel = true;
				Guid guid1 = new Guid(e.Url.LocalPath);
				TrapAttack trapAttack1 = this.fTrap.FindAttack(guid1);
				if (trapAttack1 != null)
				{
					this.fTrap.Attacks.Remove(trapAttack1);
					this.update_statblock();
				}
			}
			if (e.Url.Scheme == "attackattack")
			{
				e.Cancel = true;
				Guid guid2 = new Guid(e.Url.LocalPath);
				TrapAttack attack = this.fTrap.FindAttack(guid2);
				if (attack != null)
				{
					PowerAttackForm powerAttackForm = new PowerAttackForm(attack.Attack, false, this.fTrap.Level, this.fTrap.Role);
					if (powerAttackForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						attack.Attack = powerAttackForm.Attack;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "attackhit")
			{
				e.Cancel = true;
				Guid guid3 = new Guid(e.Url.LocalPath);
				TrapAttack details = this.fTrap.FindAttack(guid3);
				if (details != null)
				{
					DetailsForm detailsForm4 = new DetailsForm(details.OnHit, "On Hit", null);
					if (detailsForm4.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						details.OnHit = detailsForm4.Details;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "attackmiss")
			{
				e.Cancel = true;
				Guid guid4 = new Guid(e.Url.LocalPath);
				TrapAttack details1 = this.fTrap.FindAttack(guid4);
				if (details1 != null)
				{
					DetailsForm detailsForm5 = new DetailsForm(details1.OnMiss, "On Miss", null);
					if (detailsForm5.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						details1.OnMiss = detailsForm5.Details;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "attackeffect")
			{
				e.Cancel = true;
				Guid guid5 = new Guid(e.Url.LocalPath);
				TrapAttack details2 = this.fTrap.FindAttack(guid5);
				if (details2 != null)
				{
					DetailsForm detailsForm6 = new DetailsForm(details2.Effect, "Effect", null);
					if (detailsForm6.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						details2.Effect = detailsForm6.Details;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "attacknotes")
			{
				e.Cancel = true;
				Guid guid6 = new Guid(e.Url.LocalPath);
				TrapAttack trapAttack2 = this.fTrap.FindAttack(guid6);
				if (trapAttack2 != null)
				{
					DetailsForm detailsForm7 = new DetailsForm(trapAttack2.Notes, "Notes", null);
					if (detailsForm7.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						trapAttack2.Notes = detailsForm7.Details;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "skill")
			{
				e.Cancel = true;
				Guid guid7 = new Guid(e.Url.LocalPath);
				TrapSkillData trapSkillDatum1 = this.fTrap.FindSkill(guid7);
				if (trapSkillDatum1 != null)
				{
					int skillData = this.fTrap.Skills.IndexOf(trapSkillDatum1);
					TrapSkillForm trapSkillForm1 = new TrapSkillForm(trapSkillDatum1, this.fTrap.Level);
					if (trapSkillForm1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fTrap.Skills[skillData] = trapSkillForm1.SkillData;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "skillremove")
			{
				e.Cancel = true;
				Guid guid8 = new Guid(e.Url.LocalPath);
				TrapSkillData trapSkillDatum2 = this.fTrap.FindSkill(guid8);
				if (trapSkillDatum2 != null)
				{
					this.fTrap.Skills.Remove(trapSkillDatum2);
					this.update_statblock();
				}
			}
			if (e.Url.Scheme == "cm")
			{
				e.Cancel = true;
				int countermeasure = int.Parse(e.Url.LocalPath);
				string item = this.fTrap.Countermeasures[countermeasure];
				TrapCountermeasureForm trapCountermeasureForm1 = new TrapCountermeasureForm(item, this.fTrap.Level);
				if (trapCountermeasureForm1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fTrap.Countermeasures[countermeasure] = trapCountermeasureForm1.Countermeasure;
					this.update_statblock();
				}
			}
		}

		private void FileExport_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog()
			{
				Title = "Export Trap",
				FileName = this.fTrap.Name,
				Filter = Program.TrapFilter
			};
			if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK && !Serialisation<Masterplan.Data.Trap>.Save(saveFileDialog.FileName, this.fTrap, SerialisationMode.Binary))
			{
				MessageBox.Show("The trap could not be exported.", "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private List<string> get_advice()
		{
			int num = 2;
			int level = this.fTrap.Level + 5;
			int level1 = this.fTrap.Level + 3;
			bool flag = false;
			if (this.fTrap.Role is ComplexRole)
			{
				ComplexRole role = this.fTrap.Role as ComplexRole;
				if (role.Flag == RoleFlag.Elite || role.Flag == RoleFlag.Solo)
				{
					flag = true;
				}
			}
			if (flag)
			{
				num += 2;
				level += 2;
				level1 += 2;
			}
			List<string> strs = new List<string>()
			{
				"<P class=table>",
				"<TABLE>",
				"<TR class=heading>",
				"<TD colspan=2><B>Initiative Advice</B></TD>",
				"</TR>",
				"<TR>",
				"<TD>Initiative</TD>",
				string.Concat("<TD align=center>+", num, "</TD>"),
				"</TR>",
				"<TR class=heading>",
				"<TD colspan=2><B>Attack Advice</B></TD>",
				"</TR>",
				"<TR>",
				"<TD>Attack vs Armour Class</TD>",
				string.Concat("<TD align=center>+", level, "</TD>"),
				"</TR>",
				"<TR>",
				"<TD>Attack vs Other Defence</TD>",
				string.Concat("<TD align=center>+", level1, "</TD>"),
				"</TR>",
				"<TR class=heading>",
				"<TD colspan=2><B>Damage Advice</B></TD>",
				"</TR>"
			};
			if (!(this.fTrap.Role is Minion))
			{
				strs.Add("<TR>");
				strs.Add("<TD>Damage vs Single Targets</TD>");
				strs.Add(string.Concat("<TD align=center>", Statistics.Damage(this.fTrap.Level, DamageExpressionType.Normal), "</TD>"));
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD>Damage vs Multiple Targets</TD>");
				strs.Add(string.Concat("<TD align=center>", Statistics.Damage(this.fTrap.Level, DamageExpressionType.Multiple), "</TD>"));
				strs.Add("</TR>");
			}
			else
			{
				strs.Add("<TR>");
				strs.Add("<TD>Minion Damage</TD>");
				strs.Add(string.Concat("<TD align=center>", Statistics.Damage(this.fTrap.Level, DamageExpressionType.Minion), "</TD>"));
				strs.Add("</TR>");
			}
			strs.Add("<TR class=heading>");
			strs.Add("<TD colspan=2><B>Skill Advice</B></TD>");
			strs.Add("</TR>");
			strs.Add("<TR>");
			strs.Add("<TD>Easy DC</TD>");
			strs.Add(string.Concat("<TD align=center>", AI.GetSkillDC(Difficulty.Easy, this.fTrap.Level), "</TD>"));
			strs.Add("</TR>");
			strs.Add("<TR>");
			strs.Add("<TD>Moderate DC</TD>");
			strs.Add(string.Concat("<TD align=center>", AI.GetSkillDC(Difficulty.Moderate, this.fTrap.Level), "</TD>"));
			strs.Add("</TR>");
			strs.Add("<TR>");
			strs.Add("<TD>Hard DC</TD>");
			strs.Add(string.Concat("<TD align=center>", AI.GetSkillDC(Difficulty.Hard, this.fTrap.Level), "</TD>"));
			strs.Add("</TR>");
			strs.Add("</TABLE>");
			strs.Add("</P>");
			return strs;
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(TrapBuilderForm));
			this.BtnPnl = new Panel();
			this.CancelBtn = new Button();
			this.OKBtn = new Button();
			this.StatBlockBrowser = new WebBrowser();
			this.Toolbar = new ToolStrip();
			this.FileMenu = new ToolStripDropDownButton();
			this.FileExport = new ToolStripMenuItem();
			this.OptionsMenu = new ToolStripDropDownButton();
			this.OptionsCopy = new ToolStripMenuItem();
			this.LevelDownBtn = new ToolStripButton();
			this.LevelUpBtn = new ToolStripButton();
			this.LevelLbl = new ToolStripLabel();
			this.BtnPnl.SuspendLayout();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.BtnPnl.Controls.Add(this.CancelBtn);
			this.BtnPnl.Controls.Add(this.OKBtn);
			this.BtnPnl.Dock = DockStyle.Bottom;
			this.BtnPnl.Location = new Point(0, 443);
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
			this.StatBlockBrowser.Size = new System.Drawing.Size(664, 418);
			this.StatBlockBrowser.TabIndex = 2;
			this.StatBlockBrowser.WebBrowserShortcutsEnabled = false;
			this.StatBlockBrowser.Navigating += new WebBrowserNavigatingEventHandler(this.Browser_Navigating);
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] fileMenu = new ToolStripItem[] { this.FileMenu, this.OptionsMenu, this.LevelDownBtn, this.LevelUpBtn, this.LevelLbl };
			items.AddRange(fileMenu);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(664, 25);
			this.Toolbar.TabIndex = 3;
			this.Toolbar.Text = "Toolbar";
			this.FileMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.FileMenu.DropDownItems.AddRange(new ToolStripItem[] { this.FileExport });
			this.FileMenu.Image = (Image)componentResourceManager.GetObject("FileMenu.Image");
			this.FileMenu.ImageTransparentColor = Color.Magenta;
			this.FileMenu.Name = "FileMenu";
			this.FileMenu.Size = new System.Drawing.Size(38, 22);
			this.FileMenu.Text = "File";
			this.FileExport.Name = "FileExport";
			this.FileExport.Size = new System.Drawing.Size(152, 22);
			this.FileExport.Text = "Export...";
			this.FileExport.Click += new EventHandler(this.FileExport_Click);
			this.OptionsMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.OptionsMenu.DropDownItems.AddRange(new ToolStripItem[] { this.OptionsCopy });
			this.OptionsMenu.Image = (Image)componentResourceManager.GetObject("OptionsMenu.Image");
			this.OptionsMenu.ImageTransparentColor = Color.Magenta;
			this.OptionsMenu.Name = "OptionsMenu";
			this.OptionsMenu.Size = new System.Drawing.Size(62, 22);
			this.OptionsMenu.Text = "Options";
			this.OptionsCopy.Name = "OptionsCopy";
			this.OptionsCopy.Size = new System.Drawing.Size(197, 22);
			this.OptionsCopy.Text = "Copy an Existing Trap...";
			this.OptionsCopy.Click += new EventHandler(this.OptionsCopy_Click);
			this.LevelDownBtn.Alignment = ToolStripItemAlignment.Right;
			this.LevelDownBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LevelDownBtn.Image = (Image)componentResourceManager.GetObject("LevelDownBtn.Image");
			this.LevelDownBtn.ImageTransparentColor = Color.Magenta;
			this.LevelDownBtn.Name = "LevelDownBtn";
			this.LevelDownBtn.Size = new System.Drawing.Size(23, 22);
			this.LevelDownBtn.Text = "-";
			this.LevelDownBtn.ToolTipText = "Level Down";
			this.LevelDownBtn.Click += new EventHandler(this.LevelDownBtn_Click);
			this.LevelUpBtn.Alignment = ToolStripItemAlignment.Right;
			this.LevelUpBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LevelUpBtn.Image = (Image)componentResourceManager.GetObject("LevelUpBtn.Image");
			this.LevelUpBtn.ImageTransparentColor = Color.Magenta;
			this.LevelUpBtn.Name = "LevelUpBtn";
			this.LevelUpBtn.Size = new System.Drawing.Size(23, 22);
			this.LevelUpBtn.Text = "+";
			this.LevelUpBtn.ToolTipText = "Level Up";
			this.LevelUpBtn.Click += new EventHandler(this.LevelUpBtn_Click);
			this.LevelLbl.Alignment = ToolStripItemAlignment.Right;
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new System.Drawing.Size(37, 22);
			this.LevelLbl.Text = "Level:";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(664, 478);
			base.Controls.Add(this.StatBlockBrowser);
			base.Controls.Add(this.Toolbar);
			base.Controls.Add(this.BtnPnl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TrapBuilderForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Trap / Hazard Builder";
			this.BtnPnl.ResumeLayout(false);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void LevelDownBtn_Click(object sender, EventArgs e)
		{
			this.fTrap.AdjustLevel(-1);
			this.update_statblock();
		}

		private void LevelUpBtn_Click(object sender, EventArgs e)
		{
			this.fTrap.AdjustLevel(1);
			this.update_statblock();
		}

		private void OptionsCopy_Click(object sender, EventArgs e)
		{
			TrapSelectForm trapSelectForm = new TrapSelectForm();
			if (trapSelectForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fTrap = trapSelectForm.Trap.Copy();
				this.update_statblock();
			}
		}

		private void update_statblock()
		{
			List<string> head = HTML.GetHead("Trap", "", DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<TABLE class=clear>");
			head.Add("<TR class=clear>");
			head.Add("<TD class=clear>");
			head.Add("<P class=table>");
			head.Add(HTML.Trap(this.fTrap, null, false, false, true, DisplaySize.Small));
			head.Add("</P>");
			head.Add("</TD>");
			head.Add("<TD class=clear>");
			head.AddRange(this.get_advice());
			head.Add("</TD>");
			head.Add("</TR>");
			head.Add("</TABLE>");
			head.Add("</BODY>");
			head.Add("</HTML>");
			this.StatBlockBrowser.DocumentText = HTML.Concatenate(head);
		}
	}
}