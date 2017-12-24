using Masterplan;
using Masterplan.Data;
using Masterplan.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class SkillChallengePanel : UserControl
	{
		private IContainer components;

		private ToolStrip Toolbar;

		private ToolStripButton EditBtn;

		private ListView SkillList;

		private ColumnHeader InfoHdr;

		private ToolStripButton ChooseBtn;

		private ToolStripButton LocationBtn;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton AddLibraryBtn;

		private ToolStripSeparator toolStripSeparator2;

		private SkillChallenge fChallenge;

		private int fPartyLevel = Session.Project.Party.Level;

		public SkillChallenge Challenge
		{
			get
			{
				return this.fChallenge;
			}
			set
			{
				this.fChallenge = value;
				this.update_view();
			}
		}

		public int PartyLevel
		{
			get
			{
				return this.fPartyLevel;
			}
			set
			{
				this.fPartyLevel = value;
				this.update_view();
			}
		}

		public SkillChallengeData SelectedSkill
		{
			get
			{
				if (this.SkillList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.SkillList.SelectedItems[0].Tag as SkillChallengeData;
			}
		}

		public SkillChallengePanel()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
		}

		private void AddLibraryBtn_Click(object sender, EventArgs e)
		{
			LibrarySelectForm librarySelectForm = new LibrarySelectForm();
			if (librarySelectForm.ShowDialog() == DialogResult.OK)
			{
				Library selectedLibrary = librarySelectForm.SelectedLibrary;
				selectedLibrary.SkillChallenges.Add(this.fChallenge.Copy() as SkillChallenge);
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.ChooseBtn.Enabled = Session.SkillChallenges.Count != 0;
		}

		private void ChooseBtn_Click(object sender, EventArgs e)
		{
			SkillChallengeSelectForm skillChallengeSelectForm = new SkillChallengeSelectForm();
			if (skillChallengeSelectForm.ShowDialog() == DialogResult.OK)
			{
				this.fChallenge.Name = skillChallengeSelectForm.SkillChallenge.Name;
				this.fChallenge.Complexity = skillChallengeSelectForm.SkillChallenge.Complexity;
				this.fChallenge.Success = skillChallengeSelectForm.SkillChallenge.Success;
				this.fChallenge.Failure = skillChallengeSelectForm.SkillChallenge.Failure;
				this.fChallenge.Skills.Clear();
				foreach (SkillChallengeData skill in skillChallengeSelectForm.SkillChallenge.Skills)
				{
					this.fChallenge.Skills.Add(skill.Copy());
				}
				this.fChallenge.Level = this.fPartyLevel;
				this.update_view();
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

		public void Edit()
		{
			this.EditBtn_Click(null, null);
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			SkillChallengeBuilderForm skillChallengeBuilderForm = new SkillChallengeBuilderForm(this.fChallenge);
			if (skillChallengeBuilderForm.ShowDialog() == DialogResult.OK)
			{
				this.fChallenge.Name = skillChallengeBuilderForm.SkillChallenge.Name;
				this.fChallenge.Complexity = skillChallengeBuilderForm.SkillChallenge.Complexity;
				this.fChallenge.Level = skillChallengeBuilderForm.SkillChallenge.Level;
				this.fChallenge.Success = skillChallengeBuilderForm.SkillChallenge.Success;
				this.fChallenge.Failure = skillChallengeBuilderForm.SkillChallenge.Failure;
				this.fChallenge.Notes = skillChallengeBuilderForm.SkillChallenge.Notes;
				this.fChallenge.Skills.Clear();
				foreach (SkillChallengeData skill in skillChallengeBuilderForm.SkillChallenge.Skills)
				{
					this.fChallenge.Skills.Add(skill.Copy());
				}
				this.update_view();
			}
		}

		private void InitializeComponent()
		{
			ListViewGroup listViewGroup = new ListViewGroup("Info", HorizontalAlignment.Left);
			ListViewGroup listViewGroup1 = new ListViewGroup("Primary Skills", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Other Skills", HorizontalAlignment.Left);
			ListViewGroup listViewGroup3 = new ListViewGroup("Automatic Failure", HorizontalAlignment.Left);
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(SkillChallengePanel));
			this.Toolbar = new ToolStrip();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.SkillList = new ListView();
			this.InfoHdr = new ColumnHeader();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.EditBtn = new ToolStripButton();
			this.LocationBtn = new ToolStripButton();
			this.ChooseBtn = new ToolStripButton();
			this.AddLibraryBtn = new ToolStripButton();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] editBtn = new ToolStripItem[] { this.EditBtn, this.LocationBtn, this.toolStripSeparator1, this.ChooseBtn, this.toolStripSeparator2, this.AddLibraryBtn };
			items.AddRange(editBtn);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(520, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.SkillList.Columns.AddRange(new ColumnHeader[] { this.InfoHdr });
			this.SkillList.Dock = DockStyle.Fill;
			this.SkillList.FullRowSelect = true;
			listViewGroup.Header = "Info";
			listViewGroup.Name = "InfoHdr";
			listViewGroup1.Header = "Primary Skills";
			listViewGroup1.Name = "PrimaryHdr";
			listViewGroup2.Header = "Other Skills";
			listViewGroup2.Name = "SecondaryHdr";
			listViewGroup3.Header = "Automatic Failure";
			listViewGroup3.Name = "listViewGroup1";
			ListViewGroupCollection groups = this.SkillList.Groups;
			ListViewGroup[] listViewGroupArray = new ListViewGroup[] { listViewGroup, listViewGroup1, listViewGroup2, listViewGroup3 };
			groups.AddRange(listViewGroupArray);
			this.SkillList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.SkillList.HideSelection = false;
			this.SkillList.Location = new Point(0, 25);
			this.SkillList.MultiSelect = false;
			this.SkillList.Name = "SkillList";
			this.SkillList.Size = new System.Drawing.Size(520, 155);
			this.SkillList.TabIndex = 1;
			this.SkillList.UseCompatibleStateImageBehavior = false;
			this.SkillList.View = View.Details;
			this.SkillList.DoubleClick += new EventHandler(this.SkillList_DoubleClick);
			this.InfoHdr.Text = "Info";
			this.InfoHdr.Width = 445;
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			this.EditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditBtn.Image = (Image)componentResourceManager.GetObject("EditBtn.Image");
			this.EditBtn.ImageTransparentColor = Color.Magenta;
			this.EditBtn.Name = "EditBtn";
			this.EditBtn.Size = new System.Drawing.Size(128, 22);
			this.EditBtn.Text = "Skill Challenge Builder";
			this.EditBtn.Click += new EventHandler(this.EditBtn_Click);
			this.LocationBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LocationBtn.Image = (Image)componentResourceManager.GetObject("LocationBtn.Image");
			this.LocationBtn.ImageTransparentColor = Color.Magenta;
			this.LocationBtn.Name = "LocationBtn";
			this.LocationBtn.Size = new System.Drawing.Size(103, 22);
			this.LocationBtn.Text = "Set Map Location";
			this.LocationBtn.Click += new EventHandler(this.LocationBtn_Click);
			this.ChooseBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChooseBtn.Image = (Image)componentResourceManager.GetObject("ChooseBtn.Image");
			this.ChooseBtn.ImageTransparentColor = Color.Magenta;
			this.ChooseBtn.Name = "ChooseBtn";
			this.ChooseBtn.Size = new System.Drawing.Size(136, 22);
			this.ChooseBtn.Text = "Use Standard Challenge";
			this.ChooseBtn.Click += new EventHandler(this.ChooseBtn_Click);
			this.AddLibraryBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AddLibraryBtn.Image = (Image)componentResourceManager.GetObject("AddLibraryBtn.Image");
			this.AddLibraryBtn.ImageTransparentColor = Color.Magenta;
			this.AddLibraryBtn.Name = "AddLibraryBtn";
			this.AddLibraryBtn.Size = new System.Drawing.Size(86, 22);
			this.AddLibraryBtn.Text = "Add to Library";
			this.AddLibraryBtn.Click += new EventHandler(this.AddLibraryBtn_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.SkillList);
			base.Controls.Add(this.Toolbar);
			base.Name = "SkillChallengePanel";
			base.Size = new System.Drawing.Size(520, 180);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void LocationBtn_Click(object sender, EventArgs e)
		{
			MapAreaSelectForm mapAreaSelectForm = new MapAreaSelectForm(this.fChallenge.MapID, this.fChallenge.MapAreaID);
			if (mapAreaSelectForm.ShowDialog() == DialogResult.OK)
			{
				this.fChallenge.MapID = (mapAreaSelectForm.Map != null ? mapAreaSelectForm.Map.ID : Guid.Empty);
				this.fChallenge.MapAreaID = (mapAreaSelectForm.MapArea != null ? mapAreaSelectForm.MapArea.ID : Guid.Empty);
				this.update_view();
			}
		}

		private void SkillList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedSkill != null)
			{
				int skillData = this.fChallenge.Skills.IndexOf(this.SelectedSkill);
				SkillChallengeSkillForm skillChallengeSkillForm = new SkillChallengeSkillForm(this.SelectedSkill);
				if (skillChallengeSkillForm.ShowDialog() == DialogResult.OK)
				{
					this.fChallenge.Skills[skillData] = skillChallengeSkillForm.SkillData;
					this.update_view();
				}
			}
		}

		private void update_view()
		{
			this.SkillList.Items.Clear();
			ListView.ListViewItemCollection items = this.SkillList.Items;
			object[] name = new object[] { this.fChallenge.Name, ": ", this.fChallenge.GetXP(), " XP" };
			ListViewItem item = items.Add(string.Concat(name));
			item.Group = this.SkillList.Groups[0];
			ListViewItem listViewItem = this.SkillList.Items.Add(this.fChallenge.Info);
			listViewItem.Group = this.SkillList.Groups[0];
			if (this.fChallenge.MapID != Guid.Empty)
			{
				Map map = Session.Project.FindTacticalMap(this.fChallenge.MapID);
				if (map != null)
				{
					MapArea mapArea = map.FindArea(this.fChallenge.MapAreaID);
					if (mapArea != null)
					{
						string str = string.Concat("Location: ", map.Name);
						if (mapArea != null)
						{
							str = string.Concat(str, " (", mapArea.Name, ")");
						}
						ListViewItem item1 = this.SkillList.Items.Add(str);
						item1.Group = this.SkillList.Groups[0];
					}
				}
			}
			foreach (SkillChallengeData skill in this.fChallenge.Skills)
			{
				string str1 = string.Concat(skill.Difficulty.ToString().ToLower(), " DCs");
				if (skill.DCModifier != 0)
				{
					str1 = (skill.DCModifier <= 0 ? string.Concat(str1, " ", skill.DCModifier) : string.Concat(str1, " +", skill.DCModifier));
				}
				string str2 = string.Concat(skill.SkillName, " (", str1, ")");
				if (skill.Details != "")
				{
					str2 = string.Concat(str2, ": ", skill.Details);
				}
				ListViewItem red = this.SkillList.Items.Add(str2);
				red.Tag = skill;
				switch (skill.Type)
				{
					case SkillType.Primary:
					{
						red.Group = this.SkillList.Groups[1];
						break;
					}
					case SkillType.Secondary:
					{
						red.Group = this.SkillList.Groups[2];
						break;
					}
					case SkillType.AutoFail:
					{
						red.Group = this.SkillList.Groups[3];
						break;
					}
				}
				if (skill.Difficulty != Difficulty.Trivial && skill.Difficulty != Difficulty.Extreme)
				{
					continue;
				}
				red.ForeColor = Color.Red;
			}
			if (this.SkillList.Groups[1].Items.Count == 0)
			{
				ListViewItem grayText = this.SkillList.Items.Add("(none)");
				grayText.Group = this.SkillList.Groups[1];
				grayText.ForeColor = SystemColors.GrayText;
			}
			if (this.SkillList.Groups[2].Items.Count == 0)
			{
				ListViewItem grayText1 = this.SkillList.Items.Add("(none)");
				grayText1.Group = this.SkillList.Groups[2];
				grayText1.ForeColor = SystemColors.GrayText;
			}
			this.SkillList.Sort();
		}
	}
}