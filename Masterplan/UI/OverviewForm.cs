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
	internal class OverviewForm : Form
	{
		private ToolStrip Toolbar;

		private ListView ItemList;

		private ColumnHeader PointHdr;

		private ColumnHeader InfoHdr;

		private ToolStripButton EncounterBtn;

		private ToolStripButton ChallengeBtn;

		private ToolStripButton TreasureBtn;

		private ToolStripButton TrapBtn;

		private Panel MainPanel;

		private Button CloseBtn;

		private OverviewType fType;

		private List<PlotPoint> fPoints = new List<PlotPoint>();

		public Pair<PlotPoint, SkillChallenge> SelectedChallenge
		{
			get
			{
				if (this.ItemList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.ItemList.SelectedItems[0].Tag as Pair<PlotPoint, SkillChallenge>;
			}
		}

		public Pair<PlotPoint, Encounter> SelectedEncounter
		{
			get
			{
				if (this.ItemList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.ItemList.SelectedItems[0].Tag as Pair<PlotPoint, Encounter>;
			}
		}

		public Pair<PlotPoint, Parcel> SelectedParcel
		{
			get
			{
				if (this.ItemList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.ItemList.SelectedItems[0].Tag as Pair<PlotPoint, Parcel>;
			}
		}

		public Pair<PlotPoint, Trap> SelectedTrap
		{
			get
			{
				if (this.ItemList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.ItemList.SelectedItems[0].Tag as Pair<PlotPoint, Trap>;
			}
		}

		public OverviewForm()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.add_points(null);
			this.update_list();
		}

		private void add_points(Plot plot)
		{
			List<PlotPoint> plotPoints = (plot != null ? plot.Points : Session.Project.Plot.Points);
			this.fPoints.AddRange(plotPoints);
			foreach (PlotPoint plotPoint in plotPoints)
			{
				this.add_points(plotPoint.Subplot);
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.EncounterBtn.Checked = this.fType == OverviewType.Encounters;
			this.TrapBtn.Checked = this.fType == OverviewType.Traps;
			this.ChallengeBtn.Checked = this.fType == OverviewType.SkillChallenges;
			this.TreasureBtn.Checked = this.fType == OverviewType.Treasure;
		}

		private void ChallengeBtn_Click(object sender, EventArgs e)
		{
			this.fType = OverviewType.SkillChallenges;
			this.update_list();
		}

        private void EncounterBtn_Click(object sender, EventArgs e)
		{
			this.fType = OverviewType.Encounters;
			this.update_list();
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(OverviewForm));
			this.Toolbar = new ToolStrip();
			this.EncounterBtn = new ToolStripButton();
			this.TrapBtn = new ToolStripButton();
			this.ChallengeBtn = new ToolStripButton();
			this.TreasureBtn = new ToolStripButton();
			this.ItemList = new ListView();
			this.PointHdr = new ColumnHeader();
			this.InfoHdr = new ColumnHeader();
			this.MainPanel = new Panel();
			this.CloseBtn = new Button();
			this.Toolbar.SuspendLayout();
			this.MainPanel.SuspendLayout();
			base.SuspendLayout();
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] encounterBtn = new ToolStripItem[] { this.EncounterBtn, this.TrapBtn, this.ChallengeBtn, this.TreasureBtn };
			items.AddRange(encounterBtn);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(513, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.EncounterBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EncounterBtn.Image = (Image)componentResourceManager.GetObject("EncounterBtn.Image");
			this.EncounterBtn.ImageTransparentColor = Color.Magenta;
			this.EncounterBtn.Name = "EncounterBtn";
			this.EncounterBtn.Size = new System.Drawing.Size(70, 22);
			this.EncounterBtn.Text = "Encounters";
			this.EncounterBtn.Click += new EventHandler(this.EncounterBtn_Click);
			this.TrapBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TrapBtn.Image = (Image)componentResourceManager.GetObject("TrapBtn.Image");
			this.TrapBtn.ImageTransparentColor = Color.Magenta;
			this.TrapBtn.Name = "TrapBtn";
			this.TrapBtn.Size = new System.Drawing.Size(40, 22);
			this.TrapBtn.Text = "Traps";
			this.TrapBtn.Click += new EventHandler(this.TrapBtn_Click);
			this.ChallengeBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChallengeBtn.Image = (Image)componentResourceManager.GetObject("ChallengeBtn.Image");
			this.ChallengeBtn.ImageTransparentColor = Color.Magenta;
			this.ChallengeBtn.Name = "ChallengeBtn";
			this.ChallengeBtn.Size = new System.Drawing.Size(93, 22);
			this.ChallengeBtn.Text = "Skill Challenges";
			this.ChallengeBtn.Click += new EventHandler(this.ChallengeBtn_Click);
			this.TreasureBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TreasureBtn.Image = (Image)componentResourceManager.GetObject("TreasureBtn.Image");
			this.TreasureBtn.ImageTransparentColor = Color.Magenta;
			this.TreasureBtn.Name = "TreasureBtn";
			this.TreasureBtn.Size = new System.Drawing.Size(56, 22);
			this.TreasureBtn.Text = "Treasure";
			this.TreasureBtn.Click += new EventHandler(this.TreasureBtn_Click);
			ListView.ColumnHeaderCollection columns = this.ItemList.Columns;
			ColumnHeader[] pointHdr = new ColumnHeader[] { this.PointHdr, this.InfoHdr };
			columns.AddRange(pointHdr);
			this.ItemList.Dock = DockStyle.Fill;
			this.ItemList.FullRowSelect = true;
			this.ItemList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.ItemList.HideSelection = false;
			this.ItemList.Location = new Point(0, 25);
			this.ItemList.MultiSelect = false;
			this.ItemList.Name = "ItemList";
			this.ItemList.Size = new System.Drawing.Size(513, 203);
			this.ItemList.TabIndex = 1;
			this.ItemList.UseCompatibleStateImageBehavior = false;
			this.ItemList.View = View.Details;
			this.ItemList.DoubleClick += new EventHandler(this.ItemList_DoubleClick);
			this.PointHdr.Text = "Point";
			this.PointHdr.Width = 100;
			this.InfoHdr.Text = "Information";
			this.InfoHdr.Width = 384;
			this.MainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.MainPanel.Controls.Add(this.ItemList);
			this.MainPanel.Controls.Add(this.Toolbar);
			this.MainPanel.Location = new Point(12, 12);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new System.Drawing.Size(513, 228);
			this.MainPanel.TabIndex = 2;
			this.CloseBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CloseBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.CloseBtn.Location = new Point(450, 246);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new System.Drawing.Size(75, 23);
			this.CloseBtn.TabIndex = 3;
			this.CloseBtn.Text = "Close";
			this.CloseBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.CloseBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(537, 281);
			base.Controls.Add(this.CloseBtn);
			base.Controls.Add(this.MainPanel);
			base.MinimizeBox = false;
			base.Name = "OverviewForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Project Overview";
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.MainPanel.ResumeLayout(false);
			this.MainPanel.PerformLayout();
			base.ResumeLayout(false);
		}

		private void ItemList_DoubleClick(object sender, EventArgs e)
		{
			switch (this.fType)
			{
				case OverviewType.Encounters:
				{
					if (this.SelectedEncounter == null)
					{
						break;
					}
					int partyLevel = Workspace.GetPartyLevel(this.SelectedEncounter.First);
					EncounterBuilderForm encounterBuilderForm = new EncounterBuilderForm(this.SelectedEncounter.Second, partyLevel, false);
					if (encounterBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.SelectedEncounter.First.Element = encounterBuilderForm.Encounter;
						Session.Modified = true;
						this.update_list();
					}
					return;
				}
				case OverviewType.Traps:
				{
					if (this.SelectedTrap == null)
					{
						break;
					}
					if (this.SelectedTrap.First.Element is TrapElement)
					{
						TrapElement element = this.SelectedTrap.First.Element as TrapElement;
						TrapBuilderForm trapBuilderForm = new TrapBuilderForm(this.SelectedTrap.Second);
						if (trapBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						{
							element.Trap = trapBuilderForm.Trap;
							Session.Modified = true;
							this.update_list();
						}
						return;
					}
					if (!(this.SelectedTrap.First.Element is Encounter))
					{
						break;
					}
					Encounter trap = this.SelectedTrap.First.Element as Encounter;
					int num = trap.Traps.IndexOf(this.SelectedTrap.Second);
					TrapBuilderForm trapBuilderForm1 = new TrapBuilderForm(this.SelectedTrap.Second);
					if (trapBuilderForm1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						trap.Traps[num] = trapBuilderForm1.Trap;
						Session.Modified = true;
						this.update_list();
					}
					return;
				}
				case OverviewType.SkillChallenges:
				{
					if (this.SelectedChallenge == null)
					{
						break;
					}
					if (this.SelectedChallenge.First.Element is SkillChallenge)
					{
						SkillChallenge name = this.SelectedChallenge.First.Element as SkillChallenge;
						SkillChallengeBuilderForm skillChallengeBuilderForm = new SkillChallengeBuilderForm(this.SelectedChallenge.Second);
						if (skillChallengeBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						{
							name.Name = skillChallengeBuilderForm.SkillChallenge.Name;
							name.Level = skillChallengeBuilderForm.SkillChallenge.Level;
							name.Complexity = skillChallengeBuilderForm.SkillChallenge.Complexity;
							name.Success = skillChallengeBuilderForm.SkillChallenge.Success;
							name.Failure = skillChallengeBuilderForm.SkillChallenge.Failure;
							name.Skills.Clear();
							foreach (SkillChallengeData skill in skillChallengeBuilderForm.SkillChallenge.Skills)
							{
								name.Skills.Add(skill.Copy());
							}
							Session.Modified = true;
							this.update_list();
						}
						return;
					}
					if (!(this.SelectedChallenge.First.Element is Encounter))
					{
						break;
					}
					Encounter skillChallenge = this.SelectedChallenge.First.Element as Encounter;
					int num1 = skillChallenge.SkillChallenges.IndexOf(this.SelectedChallenge.Second);
					SkillChallengeBuilderForm skillChallengeBuilderForm1 = new SkillChallengeBuilderForm(this.SelectedChallenge.Second);
					if (skillChallengeBuilderForm1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						skillChallenge.SkillChallenges[num1] = skillChallengeBuilderForm1.SkillChallenge;
						Session.Modified = true;
						this.update_list();
					}
					return;
				}
				case OverviewType.Treasure:
				{
					if (this.SelectedParcel == null)
					{
						break;
					}
					int parcel = this.SelectedParcel.First.Parcels.IndexOf(this.SelectedParcel.Second);
					ParcelForm parcelForm = new ParcelForm(this.SelectedParcel.Second);
					if (parcelForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
					{
						break;
					}
					this.SelectedParcel.First.Parcels[parcel] = parcelForm.Parcel;
					Session.Modified = true;
					this.update_list();
					break;
				}
				default:
				{
					return;
				}
			}
		}

		private void TrapBtn_Click(object sender, EventArgs e)
		{
			this.fType = OverviewType.Traps;
			this.update_list();
		}

		private void TreasureBtn_Click(object sender, EventArgs e)
		{
			this.fType = OverviewType.Treasure;
			this.update_list();
		}

		private void update_list()
		{
			this.ItemList.Items.Clear();
			switch (this.fType)
			{
				case OverviewType.Encounters:
				{
					List<PlotPoint>.Enumerator enumerator = this.fPoints.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							PlotPoint current = enumerator.Current;
							if (current.Element == null || !(current.Element is Encounter))
							{
								continue;
							}
							Encounter element = current.Element as Encounter;
							string str = "";
							List<EncounterSlot>.Enumerator enumerator1 = element.AllSlots.GetEnumerator();
							try
							{
								while (enumerator1.MoveNext())
								{
									EncounterSlot encounterSlot = enumerator1.Current;
									if (str != "")
									{
										str = string.Concat(str, ", ");
									}
									str = string.Concat(str, encounterSlot.Card.Title);
									if (encounterSlot.CombatData.Count == 1)
									{
										continue;
									}
									object obj = str;
									object[] count = new object[] { obj, " (x", encounterSlot.CombatData.Count, ")" };
									str = string.Concat(count);
								}
							}
							finally
							{
								((IDisposable)enumerator1).Dispose();
							}
							List<Trap>.Enumerator enumerator2 = element.Traps.GetEnumerator();
							try
							{
								while (enumerator2.MoveNext())
								{
									Trap trap = enumerator2.Current;
									if (str != "")
									{
										str = string.Concat(str, ", ");
									}
									str = string.Concat(str, trap.Name);
								}
							}
							finally
							{
								((IDisposable)enumerator2).Dispose();
							}
							ListViewItem pair = this.ItemList.Items.Add(current.Name);
							pair.SubItems.Add(string.Concat(element.GetXP(), " XP; ", str));
							pair.Tag = new Pair<PlotPoint, Encounter>(current, element);
						}
						break;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
				}
				case OverviewType.Traps:
				{
					List<PlotPoint>.Enumerator enumerator3 = this.fPoints.GetEnumerator();
					try
					{
						while (enumerator3.MoveNext())
						{
							PlotPoint plotPoint = enumerator3.Current;
							if (plotPoint.Element == null)
							{
								continue;
							}
							if (plotPoint.Element is TrapElement)
							{
								TrapElement trapElement = plotPoint.Element as TrapElement;
								ListViewItem listViewItem = this.ItemList.Items.Add(plotPoint.Name);
								listViewItem.SubItems.Add(string.Concat(Experience.GetCreatureXP(trapElement.Trap.Level), " XP; ", trapElement.Trap.Name));
								listViewItem.Tag = new Pair<PlotPoint, Trap>(plotPoint, trapElement.Trap);
							}
							if (!(plotPoint.Element is Encounter))
							{
								continue;
							}
							List<Trap>.Enumerator enumerator4 = (plotPoint.Element as Encounter).Traps.GetEnumerator();
							try
							{
								while (enumerator4.MoveNext())
								{
									Trap current1 = enumerator4.Current;
									ListViewItem pair1 = this.ItemList.Items.Add(plotPoint.Name);
									pair1.SubItems.Add(string.Concat(Experience.GetCreatureXP(current1.Level), " XP; ", current1.Name));
									pair1.Tag = new Pair<PlotPoint, Trap>(plotPoint, current1);
								}
							}
							finally
							{
								((IDisposable)enumerator4).Dispose();
							}
						}
						break;
					}
					finally
					{
						((IDisposable)enumerator3).Dispose();
					}
				}
				case OverviewType.SkillChallenges:
				{
					List<PlotPoint>.Enumerator enumerator5 = this.fPoints.GetEnumerator();
					try
					{
						while (enumerator5.MoveNext())
						{
							PlotPoint plotPoint1 = enumerator5.Current;
							if (plotPoint1.Element == null)
							{
								continue;
							}
							if (plotPoint1.Element is SkillChallenge)
							{
								SkillChallenge skillChallenge = plotPoint1.Element as SkillChallenge;
								ListViewItem listViewItem1 = this.ItemList.Items.Add(plotPoint1.Name);
								listViewItem1.SubItems.Add(string.Concat(skillChallenge.GetXP(), " XP"));
								listViewItem1.Tag = new Pair<PlotPoint, SkillChallenge>(plotPoint1, skillChallenge);
							}
							if (!(plotPoint1.Element is Encounter))
							{
								continue;
							}
							List<SkillChallenge>.Enumerator enumerator6 = (plotPoint1.Element as Encounter).SkillChallenges.GetEnumerator();
							try
							{
								while (enumerator6.MoveNext())
								{
									SkillChallenge skillChallenge1 = enumerator6.Current;
									ListViewItem pair2 = this.ItemList.Items.Add(plotPoint1.Name);
									pair2.SubItems.Add(string.Concat(skillChallenge1.GetXP(), " XP"));
									pair2.Tag = new Pair<PlotPoint, SkillChallenge>(plotPoint1, skillChallenge1);
								}
							}
							finally
							{
								((IDisposable)enumerator6).Dispose();
							}
						}
						break;
					}
					finally
					{
						((IDisposable)enumerator5).Dispose();
					}
				}
				case OverviewType.Treasure:
				{
					List<PlotPoint>.Enumerator enumerator7 = this.fPoints.GetEnumerator();
					try
					{
						while (enumerator7.MoveNext())
						{
							PlotPoint current2 = enumerator7.Current;
							List<Parcel>.Enumerator enumerator8 = current2.Parcels.GetEnumerator();
							try
							{
								while (enumerator8.MoveNext())
								{
									Parcel parcel = enumerator8.Current;
									string str1 = (parcel.Name != "" ? parcel.Name : "(undefined parcel)");
									ListViewItem listViewItem2 = this.ItemList.Items.Add(current2.Name);
									listViewItem2.SubItems.Add(string.Concat(str1, ": ", parcel.Details));
									listViewItem2.Tag = new Pair<PlotPoint, Parcel>(current2, parcel);
								}
							}
							finally
							{
								((IDisposable)enumerator8).Dispose();
							}
						}
						break;
					}
					finally
					{
						((IDisposable)enumerator7).Dispose();
					}
				}
			}
			if (this.ItemList.Items.Count == 0)
			{
				ListViewItem grayText = this.ItemList.Items.Add("(no items)");
				grayText.ForeColor = SystemColors.GrayText;
			}
			this.ItemList.Sort();
		}
	}
}