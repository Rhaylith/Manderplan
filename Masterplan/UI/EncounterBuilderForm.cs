using Masterplan;
using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Events;
using Masterplan.Tools;
using Masterplan.Tools.Generators;
using Masterplan.Wizards;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Utils;
using Utils.Wizards;

namespace Masterplan.UI
{
	internal class EncounterBuilderForm : Form
	{
		private Masterplan.Data.Encounter fEncounter = new Masterplan.Data.Encounter();

		private int fPartyLevel = Session.Project.Party.Level;

		private int fPartySize = Session.Project.Party.Size;

		private bool fAddingThreats;

		private ListMode fMode;

		private IContainer components;

		private ListView DifficultyList;

		private ColumnHeader DiffHdr;

		private ColumnHeader DiffXPHdr;

		private ListView SlotList;

		private ToolStrip EncToolbar;

		private ListView SourceItemList;

		private ToolStrip ThreatToolbar;

		private ColumnHeader ThreatHdr;

		private ColumnHeader CountHdr;

		private ColumnHeader NameHdr;

		private ColumnHeader InfoHdr;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton AddBtn;

		private ToolStripButton RemoveBtn;

		private ColumnHeader XPHdr;

		private ColumnHeader ThreatInfoHdr;

		private TabPage ThreatsPage;

		private TabPage MapPage;

		private Masterplan.Controls.MapView MapView;

		private ToolStrip MapToolbar;

		private ColumnHeader DiffLevels;

		private ToolStripDropDownButton ViewMenu;

		private ToolStripMenuItem ViewTemplates;

		private SplitContainer HSplitter;

		private SplitContainer VSplitter;

		private ToolStripSeparator toolStripSeparator6;

		private SplitContainer MapSplitter;

		private ListView MapThreatList;

		private ColumnHeader ThreatNameHdr;

		private ToolTip Tooltip;

		public TabControl Pages;

		private ToolStripMenuItem ViewNPCs;

		private StatusStrip XPStatusbar;

		private ToolStripStatusLabel XPLbl;

		private ToolStripStatusLabel DiffLbl;

		private ToolStripStatusLabel CountLbl;

		private System.Windows.Forms.ContextMenuStrip MapContextMenu;

		private ToolStripMenuItem MapContextView;

		private ToolStripSeparator toolStripMenuItem4;

		private ToolStripMenuItem MapContextRemove;

		private ToolStripMenuItem MapContextVisible;

		private ToolStripDropDownButton MapToolsMenu;

		private ToolStripMenuItem MapToolsLOS;

		private ToolStripSeparator toolStripMenuItem5;

		private ToolStripMenuItem MapToolsPrint;

		private ToolStripMenuItem MapToolsScreenshot;

		private ToolStripDropDownButton MapCreaturesMenu;

		private ToolStripMenuItem MapCreaturesRemove;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripMenuItem MapCreaturesShowAll;

		private ToolStripMenuItem MapCreaturesHideAll;

		private ToolStripSeparator toolStripSeparator8;

		private ToolStripMenuItem CreaturesAddCustom;

		private ToolStripMenuItem CreaturesAddOverlay;

		private ToolStripMenuItem MapContextRemoveEncounter;

		private ToolStripSeparator toolStripSeparator5;

		private EncounterGauge XPGauge;

		private ToolStripStatusLabel LevelLbl;

		private TabPage NotesPage;

		private ToolStrip NoteToolbar;

		private ToolStripButton NoteAddBtn;

		private ToolStripButton NoteRemoveBtn;

		private ToolStripButton NoteEditBtn;

		private ToolStripSeparator toolStripSeparator21;

		private ToolStripButton NoteUpBtn;

		private ToolStripButton NoteDownBtn;

		private SplitContainer NoteSplitter;

		private ListView NoteList;

		private ColumnHeader NoteHdr;

		private Panel BackgroundPanel;

		private WebBrowser NoteDetails;

		private ToolStripButton MapBtn;

		private Button OKBtn;

		private Button CancelBtn;

		private ToolStripDropDownButton ToolsMenu;

		private ToolStripMenuItem ToolsUseTemplate;

		private ToolStripMenuItem ToolsUseDeck;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripSplitButton AutoBuildBtn;

		private ToolStripMenuItem AutoBuildAdvanced;

		private ToolStripMenuItem MapToolsGridlines;

		private ToolStripSeparator toolStripSeparator12;

		private ToolStripMenuItem ViewGroups;

		private ToolStripMenuItem MapToolsGridLabels;

		private ToolStripMenuItem ToolsExport;

		private ToolStripMenuItem MapToolsPictureTokens;

		private ToolStripMenuItem ToolsApplyTheme;

		private ToolStripSeparator toolStripSeparator13;

		private ToolStripSeparator toolStripSeparator15;

		private ToolStripMenuItem MapContextSetPicture;

		private ToolStripMenuItem MapContextCopy;

		private System.Windows.Forms.ContextMenuStrip ThreatContextMenu;

		private ToolStripMenuItem EditStatBlock;

		private ToolStripMenuItem EditApplyTheme;

		private ToolStripSeparator toolStripSeparator14;

		private ToolStripMenuItem EditRemoveTemplate;

		private ToolStripMenuItem EditRemoveLevelAdj;

		private ToolStripMenuItem EditClearTheme;

		private ToolStripSeparator toolStripSeparator16;

		private ToolStripMenuItem EditSetFaction;

		private ToolStripMenuItem EditSwap;

		private ToolStripMenuItem SwapStandard;

		private ToolStripSeparator toolStripMenuItem2;

		private ToolStripMenuItem SwapElite;

		private ToolStripMenuItem SwapSolo;

		private ToolStripSeparator toolStripSeparator11;

		private ToolStripMenuItem SwapMinions;

		private ToolStripMenuItem decksToolStripMenuItem;

		private StatusStrip HintStatusbar;

		private ToolStripStatusLabel HintLbl;

		private ToolStripMenuItem ToolsClearAll;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripDropDownButton AddMenu;

		private ToolStripMenuItem ToolsAddCreature;

		private ToolStripMenuItem ToolsAddTrap;

		private ToolStripMenuItem ToolsAddChallenge;

		private ToolStripSeparator toolStripSeparator4;

		private Button InfoBtn;

		private Button DieRollerBtn;

		private Masterplan.Controls.FilterPanel FilterPanel;

		private ToolStripButton CreaturesBtn;

		private ToolStripButton TrapsBtn;

		private ToolStripButton ChallengesBtn;

		private ToolStripStatusLabel PartyLbl;

		private ToolStripSplitButton StatBlockBtn;

		private ToolStripMenuItem StatBlockEdit;

		private ToolStripMenuItem EditSetWave;

		private ToolStripSeparator toolStripSeparator9;

		public Masterplan.Data.Encounter Encounter
		{
			get
			{
				return this.fEncounter;
			}
		}

		public ICreature SelectedCreature
		{
			get
			{
				if (this.SourceItemList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.SourceItemList.SelectedItems[0].Tag as ICreature;
			}
		}

		public IToken SelectedMapThreat
		{
			get
			{
				if (this.MapThreatList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.MapThreatList.SelectedItems[0].Tag as IToken;
			}
		}

		private EncounterNote SelectedNote
		{
			get
			{
				if (this.NoteList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.NoteList.SelectedItems[0].Tag as EncounterNote;
			}
			set
			{
				this.NoteList.SelectedItems.Clear();
				if (value != null)
				{
					foreach (ListViewItem item in this.NoteList.Items)
					{
						EncounterNote tag = item.Tag as EncounterNote;
						if (tag == null || !(tag.ID == value.ID))
						{
							continue;
						}
						item.Selected = true;
					}
				}
				this.update_selected_note();
			}
		}

		public NPC SelectedNPC
		{
			get
			{
				if (this.SourceItemList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.SourceItemList.SelectedItems[0].Tag as NPC;
			}
		}

		public SkillChallenge SelectedSkillChallenge
		{
			get
			{
				if (this.SourceItemList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.SourceItemList.SelectedItems[0].Tag as SkillChallenge;
			}
		}

		public EncounterSlot SelectedSlot
		{
			get
			{
				if (this.SlotList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.SlotList.SelectedItems[0].Tag as EncounterSlot;
			}
		}

		public SkillChallenge SelectedSlotSkillChallenge
		{
			get
			{
				if (this.SlotList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.SlotList.SelectedItems[0].Tag as SkillChallenge;
			}
		}

		public Trap SelectedSlotTrap
		{
			get
			{
				if (this.SlotList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.SlotList.SelectedItems[0].Tag as Trap;
			}
		}

		public CreatureTemplate SelectedTemplate
		{
			get
			{
				if (this.SourceItemList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.SourceItemList.SelectedItems[0].Tag as CreatureTemplate;
			}
		}

		public Trap SelectedTrap
		{
			get
			{
				if (this.SourceItemList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.SourceItemList.SelectedItems[0].Tag as Trap;
			}
		}

		public EncounterBuilderForm(Masterplan.Data.Encounter enc, int party_level, bool adding_threats)
		{
			Map map;
			MapArea mapArea;
			this.InitializeComponent();
			this.fMode = ListMode.Creatures;
			this.fEncounter = enc.Copy() as Masterplan.Data.Encounter;
			this.fPartyLevel = party_level;
			this.fAddingThreats = adding_threats;
			this.SourceItemList.ListViewItemSorter = new EncounterBuilderForm.SourceSorter();
			this.NoteDetails.DocumentText = "";
			this.ToolsUseDeck.Visible = Session.Project.Decks.Count != 0;
			this.FilterPanel.Mode = this.fMode;
			this.FilterPanel.PartyLevel = this.fPartyLevel;
			this.FilterPanel.FilterByPartyLevel();
			Application.Idle += new EventHandler(this.Application_Idle);
			if (!this.fAddingThreats)
			{
				if (this.fEncounter.MapID != Guid.Empty)
				{
					map = Session.Project.FindTacticalMap(this.fEncounter.MapID);
				}
				else
				{
					map = null;
				}
				Map map1 = map;
				if (map1 != null)
				{
					this.MapView.Map = map1;
					this.MapView.Encounter = this.fEncounter;
					if (this.fEncounter.MapAreaID != Guid.Empty)
					{
						mapArea = map1.FindArea(this.fEncounter.MapAreaID);
					}
					else
					{
						mapArea = null;
					}
					MapArea mapArea1 = mapArea;
					this.MapView.Viewpoint = (mapArea1 != null ? mapArea1.Region : Rectangle.Empty);
				}
				this.update_difficulty_list();
				this.update_mapthreats();
				this.update_notes();
				this.update_selected_note();
			}
			else
			{
				this.Pages.TabPages.Remove(this.MapPage);
				this.Pages.TabPages.Remove(this.NotesPage);
				this.VSplitter.Panel2Collapsed = true;
			}
			this.update_source_list();
			this.update_encounter();
			this.update_party_label();
		}

		private void add_challenge(SkillChallenge sc)
		{
			SkillChallenge skillChallenge = sc.Copy() as SkillChallenge;
			skillChallenge.Level = this.fPartyLevel;
			this.fEncounter.SkillChallenges.Add(skillChallenge);
			this.update_encounter();
		}

		private ListViewItem add_challenge_to_list(SkillChallenge sc)
		{
			Difficulty difficulty;
			if (sc == null)
			{
				return null;
			}
			if (!this.FilterPanel.AllowItem(sc, out difficulty))
			{
				return null;
			}
			ListViewItem listViewItem = new ListViewItem(sc.Name);
			ListViewItem.ListViewSubItem grayText = listViewItem.SubItems.Add(sc.Info);
			listViewItem.Tag = sc;
			listViewItem.UseItemStyleForSubItems = false;
			grayText.ForeColor = SystemColors.GrayText;
			return listViewItem;
		}

		private ListViewItem add_creature_to_list(ICreature c)
		{
			Difficulty difficulty;
			if (c == null)
			{
				return null;
			}
			if (!this.FilterPanel.AllowItem(c, out difficulty))
			{
				return null;
			}
			ListViewItem listViewItem = new ListViewItem(c.Name);
			ListViewItem.ListViewSubItem grayText = listViewItem.SubItems.Add(c.Info);
			listViewItem.Tag = c;
			listViewItem.UseItemStyleForSubItems = false;
			grayText.ForeColor = SystemColors.GrayText;
			Difficulty difficulty1 = difficulty;
			if (difficulty1 == Difficulty.Trivial)
			{
				listViewItem.ForeColor = Color.Green;
			}
			else if (difficulty1 == Difficulty.Extreme)
			{
				listViewItem.ForeColor = Color.Maroon;
			}
			if (c is CustomCreature)
			{
				listViewItem.Group = this.SourceItemList.Groups["Custom Creatures"];
			}
			else if (c.Category == null || !(c.Category != ""))
			{
				listViewItem.Group = this.SourceItemList.Groups["Miscellaneous Creatures"];
			}
			else
			{
				listViewItem.Group = this.SourceItemList.Groups[c.Category];
			}
			return listViewItem;
		}

		private ListViewItem add_npc_to_list(NPC npc, ListViewGroup group)
		{
			Difficulty difficulty;
			if (npc == null)
			{
				return null;
			}
			if (!this.FilterPanel.AllowItem(npc, out difficulty))
			{
				return null;
			}
			ListViewItem listViewItem = new ListViewItem(npc.Name);
			ListViewItem.ListViewSubItem grayText = listViewItem.SubItems.Add(npc.Info);
			listViewItem.Group = group;
			listViewItem.Tag = npc;
			listViewItem.UseItemStyleForSubItems = false;
			grayText.ForeColor = SystemColors.GrayText;
			if (difficulty == Difficulty.Trivial)
			{
				listViewItem.ForeColor = Color.Green;
			}
			if (difficulty == Difficulty.Extreme)
			{
				listViewItem.ForeColor = Color.Red;
			}
			return listViewItem;
		}

		private void add_opponent(ICreature creature)
		{
			EncounterSlot encounterSlot = null;
			foreach (EncounterSlot slot in this.fEncounter.Slots)
			{
				if (!(slot.Card.CreatureID == creature.ID) || slot.Card.TemplateIDs.Count != 0)
				{
					continue;
				}
				encounterSlot = slot;
				break;
			}
			if (encounterSlot == null)
			{
				encounterSlot = new EncounterSlot();
				encounterSlot.Card.CreatureID = creature.ID;
				this.fEncounter.Slots.Add(encounterSlot);
			}
			CombatData combatDatum = new CombatData()
			{
				DisplayName = encounterSlot.Card.Title
			};
			encounterSlot.CombatData.Add(combatDatum);
			this.update_encounter();
			this.update_mapthreats();
		}

		private void add_template(CreatureTemplate template, EncounterSlot slot)
		{
			slot.Card.TemplateIDs.Add(template.ID);
			this.update_encounter();
			this.update_mapthreats();
		}

		private ListViewItem add_template_to_list(CreatureTemplate ct, ListViewGroup group)
		{
			Difficulty difficulty;
			if (ct == null)
			{
				return null;
			}
			if (!this.FilterPanel.AllowItem(ct, out difficulty))
			{
				return null;
			}
			ListViewItem listViewItem = new ListViewItem(ct.Name);
			ListViewItem.ListViewSubItem grayText = listViewItem.SubItems.Add(ct.Info);
			listViewItem.Group = group;
			listViewItem.Tag = ct;
			listViewItem.UseItemStyleForSubItems = false;
			grayText.ForeColor = SystemColors.GrayText;
			return listViewItem;
		}

		private void add_trap(Trap trap)
		{
			this.fEncounter.Traps.Add(trap.Copy());
			this.update_encounter();
		}

		private ListViewItem add_trap_to_list(Trap trap, ListViewGroup lvg)
		{
			Difficulty difficulty;
			if (trap == null)
			{
				return null;
			}
			if (!this.FilterPanel.AllowItem(trap, out difficulty))
			{
				return null;
			}
			ListViewItem listViewItem = new ListViewItem(trap.Name);
			ListViewItem.ListViewSubItem grayText = listViewItem.SubItems.Add(trap.Info);
			listViewItem.Group = lvg;
			listViewItem.Tag = trap;
			listViewItem.UseItemStyleForSubItems = false;
			grayText.ForeColor = SystemColors.GrayText;
			if (difficulty == Difficulty.Trivial)
			{
				listViewItem.ForeColor = Color.Green;
			}
			if (difficulty == Difficulty.Extreme)
			{
				listViewItem.ForeColor = Color.Red;
			}
			return listViewItem;
		}

		private void AddBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSlot != null)
			{
				if ((Control.ModifierKeys & Keys.Shift) != Keys.Shift)
				{
					CombatData combatDatum = new CombatData();
					this.SelectedSlot.CombatData.Add(combatDatum);
					this.update_encounter();
					this.update_mapthreats();
				}
				else
				{
					EncounterCard card = this.SelectedSlot.Card;
					card.LevelAdjustment = card.LevelAdjustment + 1;
					this.update_encounter();
				}
			}
			if (this.SelectedSlotTrap != null)
			{
				Trap trap = this.SelectedSlotTrap.Copy();
				trap.ID = Guid.NewGuid();
				this.fEncounter.Traps.Add(trap);
				this.update_encounter();
			}
			if (this.SelectedSlotSkillChallenge != null)
			{
				SkillChallenge skillChallenge = this.SelectedSlotSkillChallenge.Copy() as SkillChallenge;
				skillChallenge.ID = Guid.NewGuid();
				this.fEncounter.SkillChallenges.Add(skillChallenge);
				this.update_encounter();
			}
		}

		private void AddToken_Click(object sender, EventArgs e)
		{
			try
			{
				CustomToken customToken = new CustomToken()
				{
					Name = "Custom Map Token",
					Type = CustomTokenType.Token
				};
				CustomTokenForm customTokenForm = new CustomTokenForm(customToken);
				if (customTokenForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fEncounter.CustomTokens.Add(customTokenForm.Token);
					this.update_encounter();
					this.update_mapthreats();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private bool allow_template_drop(EncounterSlot slot, CreatureTemplate template)
		{
			if (slot.Card.TemplateIDs.Contains(template.ID))
			{
				return false;
			}
			ICreature creature = Session.FindCreature(slot.Card.CreatureID, SearchType.Global);
			if (creature.Role is Minion)
			{
				return false;
			}
			ComplexRole role = creature.Role as ComplexRole;
			int count = slot.Card.TemplateIDs.Count;
			switch (role.Flag)
			{
				case RoleFlag.Elite:
				{
					count++;
					break;
				}
				case RoleFlag.Solo:
				{
					count += 2;
					break;
				}
			}
			return count < 2;
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			try
			{
				if (this.Pages.SelectedTab == this.ThreatsPage)
				{
					bool flag = (this.SelectedSlot != null || this.SelectedSlotTrap != null ? true : this.SelectedSlotSkillChallenge != null);
					this.AddBtn.Enabled = flag;
					this.AddBtn.Visible = (this.SelectedSlot != null ? true : this.SelectedSlotTrap != null);
					this.RemoveBtn.Enabled = flag;
					this.StatBlockBtn.Enabled = flag;
					if (this.SelectedSlotTrap != null || this.SelectedSlotSkillChallenge != null)
					{
						this.RemoveBtn.Text = "Remove";
					}
					else
					{
						this.RemoveBtn.Text = "-";
					}
					this.CreaturesBtn.Visible = Session.Creatures.Count != 0;
					this.TrapsBtn.Visible = Session.Traps.Count != 0;
					this.ChallengesBtn.Visible = Session.SkillChallenges.Count != 0;
					this.CreaturesBtn.Checked = this.fMode == ListMode.Creatures;
					this.TrapsBtn.Checked = this.fMode == ListMode.Traps;
					this.ChallengesBtn.Checked = this.fMode == ListMode.SkillChallenges;
				}
				if (this.Pages.SelectedTab == this.MapPage)
				{
					this.MapToolsLOS.Checked = this.MapView.LineOfSight;
					this.MapToolsGridlines.Checked = this.MapView.ShowGrid != MapGridMode.None;
					this.MapToolsGridLabels.Checked = this.MapView.ShowGridLabels;
					this.MapToolsPictureTokens.Checked = this.MapView.ShowPictureTokens;
					this.MapToolsPrint.Enabled = this.MapView.Map != null;
					this.MapToolsScreenshot.Enabled = this.MapView.Map != null;
					this.MapSplitter.Panel2Collapsed = (this.MapView.Map == null ? true : this.MapThreatList.Items.Count == 0);
					this.MapContextView.Enabled = this.MapView.SelectedTokens.Count == 1;
					this.MapContextSetPicture.Enabled = this.MapView.SelectedTokens.Count == 1;
					this.MapContextRemove.Enabled = this.MapView.SelectedTokens.Count != 0;
					this.MapContextRemoveEncounter.Enabled = this.MapView.SelectedTokens.Count != 0;
					this.MapContextCopy.Enabled = (this.MapView.SelectedTokens.Count != 1 ? false : this.MapView.SelectedTokens[0] is CustomToken);
					if (this.MapView.SelectedTokens.Count != 1)
					{
						this.MapContextVisible.Enabled = false;
						this.MapContextVisible.Checked = false;
					}
					else
					{
						this.MapContextVisible.Enabled = true;
						IToken item = this.MapView.SelectedTokens[0];
						if (item is CreatureToken)
						{
							this.MapContextVisible.Checked = (item as CreatureToken).Data.Visible;
						}
						if (item is CustomToken)
						{
							this.MapContextVisible.Checked = (item as CustomToken).Data.Visible;
						}
					}
				}
				if (this.Pages.SelectedTab == this.NotesPage)
				{
					this.NoteRemoveBtn.Enabled = this.SelectedNote != null;
					this.NoteEditBtn.Enabled = this.SelectedNote != null;
					this.NoteUpBtn.Enabled = (this.SelectedNote == null ? false : this.fEncounter.Notes.IndexOf(this.SelectedNote) != 0);
					this.NoteDownBtn.Enabled = (this.SelectedNote == null ? false : this.fEncounter.Notes.IndexOf(this.SelectedNote) != this.fEncounter.Notes.Count - 1);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void autobuild(bool advanced)
		{
			AutoBuildData autoBuildDatum = null;
			if (!advanced)
			{
				autoBuildDatum = new AutoBuildData();
			}
			else
			{
				AutoBuildForm autoBuildForm = new AutoBuildForm(AutoBuildForm.Mode.Encounter);
				if (autoBuildForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
				{
					return;
				}
				autoBuildDatum = autoBuildForm.Data;
			}
			autoBuildDatum.Level = this.fPartyLevel;
			bool flag = EncounterBuilder.Build(autoBuildDatum, this.fEncounter, false);
			this.update_encounter();
			this.update_mapthreats();
			if (!flag)
			{
				MessageBox.Show("AutoBuild was unable to find enough creatures of the appropriate type to build an encounter.", "Encounter Builder", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		private void AutoBuildAdvanced_Click(object sender, EventArgs e)
		{
			this.autobuild(true);
		}

		private void AutoBuildBtn_Click(object sender, EventArgs e)
		{
			this.autobuild(false);
		}

		private Creature choose_creature(List<Creature> creatures, string category)
		{
			CreatureSelectForm creatureSelectForm = new CreatureSelectForm(creatures);
			if (creatureSelectForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
			{
				return null;
			}
			return Session.FindCreature(creatureSelectForm.Creature.CreatureID, SearchType.Global) as Creature;
		}

		private void count_slot_as(object sender, EventArgs e)
		{
			if (this.SelectedSlot != null)
			{
				EncounterSlotType tag = (EncounterSlotType)(sender as ToolStripMenuItem).Tag;
				this.SelectedSlot.Type = tag;
				this.update_encounter();
			}
		}

		private void CreaturesAddOverlay_Click(object sender, EventArgs e)
		{
			try
			{
				CustomToken customToken = new CustomToken()
				{
					Name = "Custom Overlay",
					Type = CustomTokenType.Overlay
				};
				CustomOverlayForm customOverlayForm = new CustomOverlayForm(customToken);
				if (customOverlayForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fEncounter.CustomTokens.Add(customOverlayForm.Token);
					this.update_encounter();
					this.update_mapthreats();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void DieRollerBtn_Click(object sender, EventArgs e)
		{
			(new DieRollerForm()).ShowDialog();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void EditApplyTheme_Click(object sender, EventArgs e)
		{
			if (this.SelectedSlot == null)
			{
				return;
			}
			ThemeForm themeForm = new ThemeForm(this.SelectedSlot.Card);
			if (themeForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.SelectedSlot.Card = themeForm.Card;
				this.update_encounter();
				this.update_mapthreats();
			}
		}

		private void EditClearTheme_Click(object sender, EventArgs e)
		{
			if (this.SelectedSlot == null)
			{
				return;
			}
			this.SelectedSlot.Card.ThemeID = Guid.Empty;
			this.SelectedSlot.Card.ThemeAttackPowerID = Guid.Empty;
			this.SelectedSlot.Card.ThemeUtilityPowerID = Guid.Empty;
			this.update_encounter();
			this.update_mapthreats();
		}

		private void EditRemoveLevelAdj_Click(object sender, EventArgs e)
		{
			if (this.SelectedSlot != null && this.SelectedSlot.Card.LevelAdjustment != 0)
			{
				this.SelectedSlot.Card.LevelAdjustment = 0;
				this.update_encounter();
				this.update_mapthreats();
			}
		}

		private void EditRemoveTemplate_Click(object sender, EventArgs e)
		{
			if (this.SelectedSlot != null && this.SelectedSlot.Card.TemplateIDs.Count != 0)
			{
				this.SelectedSlot.Card.TemplateIDs.Clear();
				this.update_encounter();
				this.update_mapthreats();
			}
		}

		private void EditStatBlock_Click(object sender, EventArgs e)
		{
			if (this.SelectedSlot != null)
			{
				Guid creatureID = this.SelectedSlot.Card.CreatureID;
				CustomCreature customCreature = Session.Project.FindCustomCreature(creatureID);
				NPC nPC = Session.Project.FindNPC(creatureID);
				if (customCreature != null)
				{
					int creature = Session.Project.CustomCreatures.IndexOf(customCreature);
					CreatureBuilderForm creatureBuilderForm = new CreatureBuilderForm(customCreature);
					if (creatureBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.SelectedSlot.SetDefaultDisplayNames();
						Session.Project.CustomCreatures[creature] = creatureBuilderForm.Creature as CustomCreature;
						Session.Modified = true;
						this.update_encounter();
						this.update_source_list();
						this.update_mapthreats();
					}
				}
				else if (nPC == null)
				{
					switch (MessageBox.Show(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat("You're about to edit a creature's stat block. Do you want to change this creature globally?", Environment.NewLine), Environment.NewLine), "Press Yes to apply your changes to this creature, everywhere it appears, even in other projects. Select this option if you're correcting an error in the creature's stat block."), Environment.NewLine), Environment.NewLine), "Press No to apply your changes to a copy of this creature. Select this option if you're modifying or re-skinning the creature for this encounter only, leaving other encounters as they are."), "Masterplan", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk))
					{
						case System.Windows.Forms.DialogResult.Yes:
						{
							Creature creature1 = Session.FindCreature(creatureID, SearchType.Global) as Creature;
							Library library = Session.FindLibrary(creature1);
							int num = library.Creatures.IndexOf(creature1);
							CreatureBuilderForm creatureBuilderForm1 = new CreatureBuilderForm(creature1);
							if (creatureBuilderForm1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
							{
								break;
							}
							this.SelectedSlot.SetDefaultDisplayNames();
							library.Creatures[num] = creatureBuilderForm1.Creature as Creature;
							string libraryFilename = Session.GetLibraryFilename(library);
							Serialisation<Library>.Save(libraryFilename, library, SerialisationMode.Binary);
							this.update_encounter();
							this.update_source_list();
							this.update_mapthreats();
							break;
						}
						case System.Windows.Forms.DialogResult.No:
						{
							CustomCreature customCreature1 = new CustomCreature(Session.FindCreature(creatureID, SearchType.Global));
							CreatureHelper.AdjustCreatureLevel(customCreature1, this.SelectedSlot.Card.LevelAdjustment);
							customCreature1.ID = Guid.NewGuid();
							CreatureBuilderForm creatureBuilderForm2 = new CreatureBuilderForm(customCreature1);
							if (creatureBuilderForm2.ShowDialog() != System.Windows.Forms.DialogResult.OK)
							{
								break;
							}
							Session.Project.CustomCreatures.Add(creatureBuilderForm2.Creature as CustomCreature);
							Session.Modified = true;
							this.SelectedSlot.Card.CreatureID = creatureBuilderForm2.Creature.ID;
							this.SelectedSlot.Card.LevelAdjustment = 0;
							this.SelectedSlot.SetDefaultDisplayNames();
							this.update_encounter();
							this.update_source_list();
							this.update_mapthreats();
							break;
						}
					}
				}
				else
				{
					int num1 = Session.Project.NPCs.IndexOf(nPC);
					CreatureBuilderForm creatureBuilderForm3 = new CreatureBuilderForm(nPC);
					if (creatureBuilderForm3.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.SelectedSlot.SetDefaultDisplayNames();
						Session.Project.NPCs[num1] = creatureBuilderForm3.Creature as NPC;
						Session.Modified = true;
						this.update_encounter();
						this.update_source_list();
						this.update_mapthreats();
					}
				}
			}
			if (this.SelectedSlotTrap != null)
			{
				int trap = this.fEncounter.Traps.IndexOf(this.SelectedSlotTrap);
				TrapBuilderForm trapBuilderForm = new TrapBuilderForm(this.SelectedSlotTrap);
				if (trapBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					trapBuilderForm.Trap.ID = Guid.NewGuid();
					this.fEncounter.Traps[trap] = trapBuilderForm.Trap;
					this.update_encounter();
				}
			}
			if (this.SelectedSlotSkillChallenge != null)
			{
				int skillChallenge = this.fEncounter.SkillChallenges.IndexOf(this.SelectedSlotSkillChallenge);
				SkillChallengeBuilderForm skillChallengeBuilderForm = new SkillChallengeBuilderForm(this.SelectedSlotSkillChallenge);
				if (skillChallengeBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					skillChallengeBuilderForm.SkillChallenge.ID = Guid.NewGuid();
					this.fEncounter.SkillChallenges[skillChallenge] = skillChallengeBuilderForm.SkillChallenge;
					this.update_encounter();
				}
			}
		}

		private void ExportBtn_Click(object sender, EventArgs e)
		{
			if (this.MapView.Map != null)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog()
				{
					FileName = this.MapView.Name,
					Filter = "Bitmap Image|*.bmp|JPEG Image|*.jpg|GIF Image|*.gif|PNG Image|*.png"
				};
				if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					ImageFormat bmp = ImageFormat.Bmp;
					switch (saveFileDialog.FilterIndex)
					{
						case 1:
						{
							bmp = ImageFormat.Bmp;
							break;
						}
						case 2:
						{
							bmp = ImageFormat.Jpeg;
							break;
						}
						case 3:
						{
							bmp = ImageFormat.Gif;
							break;
						}
						case 4:
						{
							bmp = ImageFormat.Png;
							break;
						}
					}
					Bitmap bitmap = Screenshot.Map(this.MapView);
					bitmap.Save(saveFileDialog.FileName, bmp);
				}
			}
		}

		private void FilterPanel_FilterChanged(object sender, EventArgs e)
		{
			this.update_source_list();
		}

		private List<Creature> find_creatures(RoleFlag flag, int level, List<RoleType> roles)
		{
			List<Creature> creatures = new List<Creature>();
			foreach (Library library in Session.Libraries)
			{
				foreach (Creature creature in library.Creatures)
				{
					if (creature.Role is Minion)
					{
						continue;
					}
					ComplexRole role = creature.Role as ComplexRole;
					if (role.Flag != flag || creature.Level != level || roles.Count != 0 && !roles.Contains(role.Type))
					{
						continue;
					}
					creatures.Add(creature);
				}
			}
			return creatures;
		}

		private List<Creature> find_minions(int level)
		{
			List<Creature> creatures = new List<Creature>();
			foreach (Library library in Session.Libraries)
			{
				foreach (Creature creature in library.Creatures)
				{
					if (!(creature.Role is Minion) || creature.Level != level)
					{
						continue;
					}
					creatures.Add(creature);
				}
			}
			return creatures;
		}

		private void InfoBtn_Click(object sender, EventArgs e)
		{
			InfoForm infoForm = new InfoForm()
			{
				Level = this.fPartyLevel
			};
			infoForm.ShowDialog();
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(EncounterBuilderForm));
			this.DifficultyList = new ListView();
			this.DiffHdr = new ColumnHeader();
			this.DiffXPHdr = new ColumnHeader();
			this.DiffLevels = new ColumnHeader();
			this.SlotList = new ListView();
			this.ThreatHdr = new ColumnHeader();
			this.ThreatInfoHdr = new ColumnHeader();
			this.CountHdr = new ColumnHeader();
			this.XPHdr = new ColumnHeader();
			this.ThreatContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.EditStatBlock = new ToolStripMenuItem();
			this.EditApplyTheme = new ToolStripMenuItem();
			this.toolStripSeparator14 = new ToolStripSeparator();
			this.EditRemoveTemplate = new ToolStripMenuItem();
			this.EditRemoveLevelAdj = new ToolStripMenuItem();
			this.EditClearTheme = new ToolStripMenuItem();
			this.toolStripSeparator16 = new ToolStripSeparator();
			this.EditSetFaction = new ToolStripMenuItem();
			this.EditSetWave = new ToolStripMenuItem();
			this.toolStripSeparator9 = new ToolStripSeparator();
			this.EditSwap = new ToolStripMenuItem();
			this.SwapStandard = new ToolStripMenuItem();
			this.toolStripMenuItem2 = new ToolStripSeparator();
			this.SwapElite = new ToolStripMenuItem();
			this.SwapSolo = new ToolStripMenuItem();
			this.toolStripSeparator11 = new ToolStripSeparator();
			this.SwapMinions = new ToolStripMenuItem();
			this.EncToolbar = new ToolStrip();
			this.AddBtn = new ToolStripButton();
			this.RemoveBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.StatBlockBtn = new ToolStripSplitButton();
			this.StatBlockEdit = new ToolStripMenuItem();
			this.toolStripSeparator4 = new ToolStripSeparator();
			this.AddMenu = new ToolStripDropDownButton();
			this.ToolsAddCreature = new ToolStripMenuItem();
			this.ToolsAddTrap = new ToolStripMenuItem();
			this.ToolsAddChallenge = new ToolStripMenuItem();
			this.ToolsMenu = new ToolStripDropDownButton();
			this.ToolsClearAll = new ToolStripMenuItem();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.ToolsUseTemplate = new ToolStripMenuItem();
			this.ToolsUseDeck = new ToolStripMenuItem();
			this.decksToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.ToolsApplyTheme = new ToolStripMenuItem();
			this.toolStripSeparator13 = new ToolStripSeparator();
			this.ToolsExport = new ToolStripMenuItem();
			this.AutoBuildBtn = new ToolStripSplitButton();
			this.AutoBuildAdvanced = new ToolStripMenuItem();
			this.SourceItemList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.InfoHdr = new ColumnHeader();
			this.ThreatToolbar = new ToolStrip();
			this.CreaturesBtn = new ToolStripButton();
			this.TrapsBtn = new ToolStripButton();
			this.ChallengesBtn = new ToolStripButton();
			this.ViewMenu = new ToolStripDropDownButton();
			this.ViewTemplates = new ToolStripMenuItem();
			this.ViewNPCs = new ToolStripMenuItem();
			this.toolStripSeparator12 = new ToolStripSeparator();
			this.ViewGroups = new ToolStripMenuItem();
			this.Pages = new TabControl();
			this.ThreatsPage = new TabPage();
			this.HSplitter = new SplitContainer();
			this.VSplitter = new SplitContainer();
			this.HintStatusbar = new StatusStrip();
			this.HintLbl = new ToolStripStatusLabel();
			this.XPStatusbar = new StatusStrip();
			this.XPLbl = new ToolStripStatusLabel();
			this.LevelLbl = new ToolStripStatusLabel();
			this.DiffLbl = new ToolStripStatusLabel();
			this.CountLbl = new ToolStripStatusLabel();
			this.PartyLbl = new ToolStripStatusLabel();
			this.MapPage = new TabPage();
			this.MapSplitter = new SplitContainer();
			this.MapContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.MapContextView = new ToolStripMenuItem();
			this.toolStripSeparator15 = new ToolStripSeparator();
			this.MapContextSetPicture = new ToolStripMenuItem();
			this.toolStripMenuItem4 = new ToolStripSeparator();
			this.MapContextRemove = new ToolStripMenuItem();
			this.MapContextRemoveEncounter = new ToolStripMenuItem();
			this.MapContextCopy = new ToolStripMenuItem();
			this.toolStripSeparator5 = new ToolStripSeparator();
			this.MapContextVisible = new ToolStripMenuItem();
			this.MapThreatList = new ListView();
			this.ThreatNameHdr = new ColumnHeader();
			this.MapToolbar = new ToolStrip();
			this.MapBtn = new ToolStripButton();
			this.toolStripSeparator6 = new ToolStripSeparator();
			this.MapToolsMenu = new ToolStripDropDownButton();
			this.MapToolsLOS = new ToolStripMenuItem();
			this.MapToolsGridlines = new ToolStripMenuItem();
			this.MapToolsGridLabels = new ToolStripMenuItem();
			this.MapToolsPictureTokens = new ToolStripMenuItem();
			this.toolStripMenuItem5 = new ToolStripSeparator();
			this.MapToolsPrint = new ToolStripMenuItem();
			this.MapToolsScreenshot = new ToolStripMenuItem();
			this.MapCreaturesMenu = new ToolStripDropDownButton();
			this.MapCreaturesRemove = new ToolStripMenuItem();
			this.toolStripSeparator7 = new ToolStripSeparator();
			this.MapCreaturesShowAll = new ToolStripMenuItem();
			this.MapCreaturesHideAll = new ToolStripMenuItem();
			this.toolStripSeparator8 = new ToolStripSeparator();
			this.CreaturesAddCustom = new ToolStripMenuItem();
			this.CreaturesAddOverlay = new ToolStripMenuItem();
			this.NotesPage = new TabPage();
			this.NoteSplitter = new SplitContainer();
			this.NoteList = new ListView();
			this.NoteHdr = new ColumnHeader();
			this.BackgroundPanel = new Panel();
			this.NoteDetails = new WebBrowser();
			this.NoteToolbar = new ToolStrip();
			this.NoteAddBtn = new ToolStripButton();
			this.NoteRemoveBtn = new ToolStripButton();
			this.NoteEditBtn = new ToolStripButton();
			this.toolStripSeparator21 = new ToolStripSeparator();
			this.NoteUpBtn = new ToolStripButton();
			this.NoteDownBtn = new ToolStripButton();
			this.Tooltip = new ToolTip(this.components);
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.InfoBtn = new Button();
			this.DieRollerBtn = new Button();
			this.XPGauge = new EncounterGauge();
			this.FilterPanel = new Masterplan.Controls.FilterPanel();
			this.MapView = new Masterplan.Controls.MapView();
			this.ThreatContextMenu.SuspendLayout();
			this.EncToolbar.SuspendLayout();
			this.ThreatToolbar.SuspendLayout();
			this.Pages.SuspendLayout();
			this.ThreatsPage.SuspendLayout();
			this.HSplitter.Panel1.SuspendLayout();
			this.HSplitter.Panel2.SuspendLayout();
			this.HSplitter.SuspendLayout();
			this.VSplitter.Panel1.SuspendLayout();
			this.VSplitter.Panel2.SuspendLayout();
			this.VSplitter.SuspendLayout();
			this.HintStatusbar.SuspendLayout();
			this.XPStatusbar.SuspendLayout();
			this.MapPage.SuspendLayout();
			this.MapSplitter.Panel1.SuspendLayout();
			this.MapSplitter.Panel2.SuspendLayout();
			this.MapSplitter.SuspendLayout();
			this.MapContextMenu.SuspendLayout();
			this.MapToolbar.SuspendLayout();
			this.NotesPage.SuspendLayout();
			this.NoteSplitter.Panel1.SuspendLayout();
			this.NoteSplitter.Panel2.SuspendLayout();
			this.NoteSplitter.SuspendLayout();
			this.BackgroundPanel.SuspendLayout();
			this.NoteToolbar.SuspendLayout();
			base.SuspendLayout();
			ColumnHeader[] diffHdr = new ColumnHeader[] { this.DiffHdr, this.DiffXPHdr, this.DiffLevels };
			this.DifficultyList.Columns.AddRange(diffHdr);
			this.DifficultyList.Dock = DockStyle.Fill;
			this.DifficultyList.Enabled = false;
			this.DifficultyList.FullRowSelect = true;
			this.DifficultyList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.DifficultyList.HideSelection = false;
			this.DifficultyList.Location = new Point(0, 0);
			this.DifficultyList.MultiSelect = false;
			this.DifficultyList.Name = "DifficultyList";
			this.DifficultyList.Size = new System.Drawing.Size(472, 86);
			this.DifficultyList.TabIndex = 0;
			this.DifficultyList.UseCompatibleStateImageBehavior = false;
			this.DifficultyList.View = View.Details;
			this.DiffHdr.Text = "Difficulty";
			this.DiffHdr.Width = 204;
			this.DiffXPHdr.Text = "XP Range";
			this.DiffXPHdr.TextAlign = HorizontalAlignment.Center;
			this.DiffXPHdr.Width = 120;
			this.DiffLevels.Text = "Creature Levels";
			this.DiffLevels.TextAlign = HorizontalAlignment.Center;
			this.DiffLevels.Width = 120;
			this.SlotList.AllowDrop = true;
			ColumnHeader[] threatHdr = new ColumnHeader[] { this.ThreatHdr, this.ThreatInfoHdr, this.CountHdr, this.XPHdr };
			this.SlotList.Columns.AddRange(threatHdr);
			this.SlotList.ContextMenuStrip = this.ThreatContextMenu;
			this.SlotList.Dock = DockStyle.Fill;
			this.SlotList.FullRowSelect = true;
			this.SlotList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.SlotList.HideSelection = false;
			this.SlotList.Location = new Point(0, 25);
			this.SlotList.MultiSelect = false;
			this.SlotList.Name = "SlotList";
			this.SlotList.Size = new System.Drawing.Size(472, 206);
			this.SlotList.Sorting = SortOrder.Ascending;
			this.SlotList.TabIndex = 1;
			this.SlotList.UseCompatibleStateImageBehavior = false;
			this.SlotList.View = View.Details;
			this.SlotList.DoubleClick += new EventHandler(this.SlotList_DoubleClick);
			this.SlotList.DragDrop += new DragEventHandler(this.SlotList_DragDrop);
			this.SlotList.KeyDown += new KeyEventHandler(this.SlotList_KeyDown);
			this.SlotList.DragOver += new DragEventHandler(this.SlotList_DragOver);
			this.ThreatHdr.Text = "Threat";
			this.ThreatHdr.Width = 207;
			this.ThreatInfoHdr.Text = "Info";
			this.ThreatInfoHdr.Width = 120;
			this.CountHdr.Text = "Count";
			this.CountHdr.TextAlign = HorizontalAlignment.Right;
			this.XPHdr.Text = "XP";
			this.XPHdr.TextAlign = HorizontalAlignment.Right;
			ToolStripItem[] editStatBlock = new ToolStripItem[] { this.EditStatBlock, this.EditApplyTheme, this.toolStripSeparator14, this.EditRemoveTemplate, this.EditRemoveLevelAdj, this.EditClearTheme, this.toolStripSeparator16, this.EditSetFaction, this.EditSetWave, this.toolStripSeparator9, this.EditSwap };
			this.ThreatContextMenu.Items.AddRange(editStatBlock);
			this.ThreatContextMenu.Name = "ThreatContextMenu";
			this.ThreatContextMenu.Size = new System.Drawing.Size(213, 198);
			this.ThreatContextMenu.Opening += new CancelEventHandler(this.ThreatContextMenu_Opening);
			this.EditStatBlock.Name = "EditStatBlock";
			this.EditStatBlock.Size = new System.Drawing.Size(212, 22);
			this.EditStatBlock.Text = "Edit Stat Block...";
			this.EditStatBlock.Click += new EventHandler(this.EditStatBlock_Click);
			this.EditApplyTheme.Name = "EditApplyTheme";
			this.EditApplyTheme.Size = new System.Drawing.Size(212, 22);
			this.EditApplyTheme.Text = "Apply Theme...";
			this.EditApplyTheme.Click += new EventHandler(this.EditApplyTheme_Click);
			this.toolStripSeparator14.Name = "toolStripSeparator14";
			this.toolStripSeparator14.Size = new System.Drawing.Size(209, 6);
			this.EditRemoveTemplate.Name = "EditRemoveTemplate";
			this.EditRemoveTemplate.Size = new System.Drawing.Size(212, 22);
			this.EditRemoveTemplate.Text = "Remove Template";
			this.EditRemoveTemplate.Click += new EventHandler(this.EditRemoveTemplate_Click);
			this.EditRemoveLevelAdj.Name = "EditRemoveLevelAdj";
			this.EditRemoveLevelAdj.Size = new System.Drawing.Size(212, 22);
			this.EditRemoveLevelAdj.Text = "Remove Level Adjustment";
			this.EditRemoveLevelAdj.Click += new EventHandler(this.EditRemoveLevelAdj_Click);
			this.EditClearTheme.Name = "EditClearTheme";
			this.EditClearTheme.Size = new System.Drawing.Size(212, 22);
			this.EditClearTheme.Text = "Remove Theme";
			this.EditClearTheme.Click += new EventHandler(this.EditClearTheme_Click);
			this.toolStripSeparator16.Name = "toolStripSeparator16";
			this.toolStripSeparator16.Size = new System.Drawing.Size(209, 6);
			this.EditSetFaction.Name = "EditSetFaction";
			this.EditSetFaction.Size = new System.Drawing.Size(212, 22);
			this.EditSetFaction.Text = "Set Faction";
			this.EditSetWave.Name = "EditSetWave";
			this.EditSetWave.Size = new System.Drawing.Size(212, 22);
			this.EditSetWave.Text = "Set Wave";
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(209, 6);
			ToolStripItem[] swapStandard = new ToolStripItem[] { this.SwapStandard, this.toolStripMenuItem2, this.SwapElite, this.SwapSolo, this.toolStripSeparator11, this.SwapMinions };
			this.EditSwap.DropDownItems.AddRange(swapStandard);
			this.EditSwap.Name = "EditSwap";
			this.EditSwap.Size = new System.Drawing.Size(212, 22);
			this.EditSwap.Text = "Swap For";
			this.SwapStandard.Name = "SwapStandard";
			this.SwapStandard.Size = new System.Drawing.Size(169, 22);
			this.SwapStandard.Text = "Standard Creature";
			this.SwapStandard.Click += new EventHandler(this.SwapStandard_Click);
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(166, 6);
			this.SwapElite.Name = "SwapElite";
			this.SwapElite.Size = new System.Drawing.Size(169, 22);
			this.SwapElite.Text = "Elite Creature";
			this.SwapElite.Click += new EventHandler(this.SwapElite_Click);
			this.SwapSolo.Name = "SwapSolo";
			this.SwapSolo.Size = new System.Drawing.Size(169, 22);
			this.SwapSolo.Text = "Solo Creature";
			this.SwapSolo.Click += new EventHandler(this.SwapSolo_Click);
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new System.Drawing.Size(166, 6);
			this.SwapMinions.Name = "SwapMinions";
			this.SwapMinions.Size = new System.Drawing.Size(169, 22);
			this.SwapMinions.Text = "Minions";
			this.SwapMinions.Click += new EventHandler(this.SwapMinions_Click);
			ToolStripItem[] addBtn = new ToolStripItem[] { this.AddBtn, this.RemoveBtn, this.toolStripSeparator1, this.StatBlockBtn, this.toolStripSeparator4, this.AddMenu, this.ToolsMenu, this.AutoBuildBtn };
			this.EncToolbar.Items.AddRange(addBtn);
			this.EncToolbar.Location = new Point(0, 0);
			this.EncToolbar.Name = "EncToolbar";
			this.EncToolbar.Size = new System.Drawing.Size(472, 25);
			this.EncToolbar.TabIndex = 0;
			this.EncToolbar.Text = "toolStrip1";
			this.AddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AddBtn.Image = (Image)resources.GetObject("AddBtn.Image");
			this.AddBtn.ImageTransparentColor = Color.Magenta;
			this.AddBtn.Name = "AddBtn";
			this.AddBtn.Size = new System.Drawing.Size(23, 22);
			this.AddBtn.Text = "+";
			this.AddBtn.ToolTipText = "Adjust number (hold shift to adjust level)";
			this.AddBtn.Click += new EventHandler(this.AddBtn_Click);
			this.RemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveBtn.Image = (Image)resources.GetObject("RemoveBtn.Image");
			this.RemoveBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new System.Drawing.Size(23, 22);
			this.RemoveBtn.Text = "-";
			this.RemoveBtn.ToolTipText = "Adjust number (hold shift to adjust level)";
			this.RemoveBtn.Click += new EventHandler(this.RemoveBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.StatBlockBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.StatBlockBtn.DropDownItems.AddRange(new ToolStripItem[] { this.StatBlockEdit });
			this.StatBlockBtn.Image = (Image)resources.GetObject("StatBlockBtn.Image");
			this.StatBlockBtn.ImageTransparentColor = Color.Magenta;
			this.StatBlockBtn.Name = "StatBlockBtn";
			this.StatBlockBtn.Size = new System.Drawing.Size(75, 22);
			this.StatBlockBtn.Text = "Stat Block";
			this.StatBlockBtn.ButtonClick += new EventHandler(this.StatBlockBtn_Click);
			this.StatBlockEdit.Name = "StatBlockEdit";
			this.StatBlockEdit.Size = new System.Drawing.Size(103, 22);
			this.StatBlockEdit.Text = "Edit...";
			this.StatBlockEdit.Click += new EventHandler(this.EditStatBlock_Click);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			this.AddMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItem[] toolsAddCreature = new ToolStripItem[] { this.ToolsAddCreature, this.ToolsAddTrap, this.ToolsAddChallenge };
			this.AddMenu.DropDownItems.AddRange(toolsAddCreature);
			this.AddMenu.Image = (Image)resources.GetObject("AddMenu.Image");
			this.AddMenu.ImageTransparentColor = Color.Magenta;
			this.AddMenu.Name = "AddMenu";
			this.AddMenu.Size = new System.Drawing.Size(42, 22);
			this.AddMenu.Text = "Add";
			this.ToolsAddCreature.Name = "ToolsAddCreature";
			this.ToolsAddCreature.ShortcutKeys = Keys.LButton | Keys.MButton | Keys.XButton1 | Keys.Back | Keys.Tab | Keys.Clear | Keys.Return | Keys.Enter | Keys.A | Keys.D | Keys.E | Keys.H | Keys.I | Keys.L | Keys.M | Keys.Control;
			this.ToolsAddCreature.Size = new System.Drawing.Size(270, 22);
			this.ToolsAddCreature.Text = "Add Custom Creature...";
			this.ToolsAddCreature.Click += new EventHandler(this.ToolsAddCreature_Click);
			this.ToolsAddTrap.Name = "ToolsAddTrap";
			this.ToolsAddTrap.ShortcutKeys = Keys.MButton | Keys.ShiftKey | Keys.Capital | Keys.CapsLock | Keys.D | Keys.P | Keys.T | Keys.Control;
			this.ToolsAddTrap.Size = new System.Drawing.Size(270, 22);
			this.ToolsAddTrap.Text = "Add Custom Trap...";
			this.ToolsAddTrap.Click += new EventHandler(this.ToolsAddTrap_Click);
			this.ToolsAddChallenge.Name = "ToolsAddChallenge";
			this.ToolsAddChallenge.ShortcutKeys = Keys.LButton | Keys.RButton | Keys.Cancel | Keys.ShiftKey | Keys.ControlKey | Keys.Menu | Keys.Pause | Keys.A | Keys.B | Keys.C | Keys.P | Keys.Q | Keys.R | Keys.S | Keys.Control;
			this.ToolsAddChallenge.Size = new System.Drawing.Size(270, 22);
			this.ToolsAddChallenge.Text = "Add Custom Skill Challenge...";
			this.ToolsAddChallenge.Click += new EventHandler(this.ToolsAddChallenge_Click);
			this.ToolsMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItem[] toolsClearAll = new ToolStripItem[] { this.ToolsClearAll, this.toolStripSeparator3, this.ToolsUseTemplate, this.ToolsUseDeck, this.toolStripSeparator2, this.ToolsApplyTheme, this.toolStripSeparator13, this.ToolsExport };
			this.ToolsMenu.DropDownItems.AddRange(toolsClearAll);
			this.ToolsMenu.Image = (Image)resources.GetObject("ToolsMenu.Image");
			this.ToolsMenu.ImageTransparentColor = Color.Magenta;
			this.ToolsMenu.Name = "ToolsMenu";
			this.ToolsMenu.Size = new System.Drawing.Size(49, 22);
			this.ToolsMenu.Text = "Tools";
			this.ToolsMenu.DropDownOpening += new EventHandler(this.ToolsMenu_DropDownOpening);
			this.ToolsClearAll.Name = "ToolsClearAll";
			this.ToolsClearAll.Size = new System.Drawing.Size(212, 22);
			this.ToolsClearAll.Text = "Clear All";
			this.ToolsClearAll.Click += new EventHandler(this.ToolsClearAll_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(209, 6);
			this.ToolsUseTemplate.Name = "ToolsUseTemplate";
			this.ToolsUseTemplate.Size = new System.Drawing.Size(212, 22);
			this.ToolsUseTemplate.Text = "Use Encounter Template...";
			this.ToolsUseTemplate.Click += new EventHandler(this.ToolsUseTemplate_Click);
			this.ToolsUseDeck.DropDownItems.AddRange(new ToolStripItem[] { this.decksToolStripMenuItem });
			this.ToolsUseDeck.Name = "ToolsUseDeck";
			this.ToolsUseDeck.Size = new System.Drawing.Size(212, 22);
			this.ToolsUseDeck.Text = "Use Encounter Deck";
			this.decksToolStripMenuItem.Name = "decksToolStripMenuItem";
			this.decksToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.decksToolStripMenuItem.Text = "(decks)";
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(209, 6);
			this.ToolsApplyTheme.Name = "ToolsApplyTheme";
			this.ToolsApplyTheme.Size = new System.Drawing.Size(212, 22);
			this.ToolsApplyTheme.Text = "Apply Theme to All...";
			this.ToolsApplyTheme.Click += new EventHandler(this.ToolsApplyTheme_Click);
			this.toolStripSeparator13.Name = "toolStripSeparator13";
			this.toolStripSeparator13.Size = new System.Drawing.Size(209, 6);
			this.ToolsExport.Name = "ToolsExport";
			this.ToolsExport.Size = new System.Drawing.Size(212, 22);
			this.ToolsExport.Text = "Export Encounter File...";
			this.ToolsExport.Click += new EventHandler(this.ToolsExport_Click);
			this.AutoBuildBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AutoBuildBtn.DropDownItems.AddRange(new ToolStripItem[] { this.AutoBuildAdvanced });
			this.AutoBuildBtn.Image = (Image)resources.GetObject("AutoBuildBtn.Image");
			this.AutoBuildBtn.ImageTransparentColor = Color.Magenta;
			this.AutoBuildBtn.Name = "AutoBuildBtn";
			this.AutoBuildBtn.Size = new System.Drawing.Size(76, 22);
			this.AutoBuildBtn.Text = "AutoBuild";
			this.AutoBuildBtn.ButtonClick += new EventHandler(this.AutoBuildBtn_Click);
			this.AutoBuildAdvanced.Name = "AutoBuildAdvanced";
			this.AutoBuildAdvanced.Size = new System.Drawing.Size(136, 22);
			this.AutoBuildAdvanced.Text = "Advanced...";
			this.AutoBuildAdvanced.Click += new EventHandler(this.AutoBuildAdvanced_Click);
			ColumnHeader[] nameHdr = new ColumnHeader[] { this.NameHdr, this.InfoHdr };
			this.SourceItemList.Columns.AddRange(nameHdr);
			this.SourceItemList.Dock = DockStyle.Fill;
			this.SourceItemList.FullRowSelect = true;
			this.SourceItemList.HideSelection = false;
			this.SourceItemList.Location = new Point(0, 49);
			this.SourceItemList.MultiSelect = false;
			this.SourceItemList.Name = "SourceItemList";
			this.SourceItemList.Size = new System.Drawing.Size(320, 314);
			this.SourceItemList.Sorting = SortOrder.Ascending;
			this.SourceItemList.TabIndex = 2;
			this.SourceItemList.UseCompatibleStateImageBehavior = false;
			this.SourceItemList.View = View.Details;
			this.SourceItemList.DoubleClick += new EventHandler(this.ThreatList_DoubleClick);
			this.SourceItemList.ColumnClick += new ColumnClickEventHandler(this.SourceItemList_ColumnClick);
			this.SourceItemList.ItemDrag += new ItemDragEventHandler(this.OpponentList_ItemDrag);
			this.NameHdr.Text = "Creatures";
			this.NameHdr.Width = 139;
			this.InfoHdr.Text = "Info";
			this.InfoHdr.Width = 140;
			ToolStripItem[] creaturesBtn = new ToolStripItem[] { this.CreaturesBtn, this.TrapsBtn, this.ChallengesBtn, this.ViewMenu };
			this.ThreatToolbar.Items.AddRange(creaturesBtn);
			this.ThreatToolbar.Location = new Point(0, 0);
			this.ThreatToolbar.Name = "ThreatToolbar";
			this.ThreatToolbar.Size = new System.Drawing.Size(320, 25);
			this.ThreatToolbar.TabIndex = 0;
			this.ThreatToolbar.Text = "toolStrip2";
			this.CreaturesBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.CreaturesBtn.Image = (Image)resources.GetObject("CreaturesBtn.Image");
			this.CreaturesBtn.ImageTransparentColor = Color.Magenta;
			this.CreaturesBtn.Name = "CreaturesBtn";
			this.CreaturesBtn.Size = new System.Drawing.Size(61, 22);
			this.CreaturesBtn.Text = "Creatures";
			this.CreaturesBtn.Click += new EventHandler(this.ViewCreatures_Click);
			this.TrapsBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TrapsBtn.Image = (Image)resources.GetObject("TrapsBtn.Image");
			this.TrapsBtn.ImageTransparentColor = Color.Magenta;
			this.TrapsBtn.Name = "TrapsBtn";
			this.TrapsBtn.Size = new System.Drawing.Size(93, 22);
			this.TrapsBtn.Text = "Traps / Hazards";
			this.TrapsBtn.Click += new EventHandler(this.ViewTraps_Click);
			this.ChallengesBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChallengesBtn.Image = (Image)resources.GetObject("ChallengesBtn.Image");
			this.ChallengesBtn.ImageTransparentColor = Color.Magenta;
			this.ChallengesBtn.Name = "ChallengesBtn";
			this.ChallengesBtn.Size = new System.Drawing.Size(93, 22);
			this.ChallengesBtn.Text = "Skill Challenges";
			this.ChallengesBtn.Click += new EventHandler(this.ViewChallenges_Click);
			this.ViewMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItem[] viewTemplates = new ToolStripItem[] { this.ViewTemplates, this.ViewNPCs, this.toolStripSeparator12, this.ViewGroups };
			this.ViewMenu.DropDownItems.AddRange(viewTemplates);
			this.ViewMenu.Image = (Image)resources.GetObject("ViewMenu.Image");
			this.ViewMenu.ImageTransparentColor = Color.Magenta;
			this.ViewMenu.Name = "ViewMenu";
			this.ViewMenu.Size = new System.Drawing.Size(48, 22);
			this.ViewMenu.Text = "More";
			this.ViewMenu.DropDownOpening += new EventHandler(this.ViewMenu_DropDownOpening);
			this.ViewTemplates.Font = new System.Drawing.Font("Segoe UI", 9f);
			this.ViewTemplates.Name = "ViewTemplates";
			this.ViewTemplates.Size = new System.Drawing.Size(177, 22);
			this.ViewTemplates.Text = "Creature Templates";
			this.ViewTemplates.Click += new EventHandler(this.ViewTemplates_Click);
			this.ViewNPCs.Font = new System.Drawing.Font("Segoe UI", 9f);
			this.ViewNPCs.Name = "ViewNPCs";
			this.ViewNPCs.Size = new System.Drawing.Size(177, 22);
			this.ViewNPCs.Text = "NPCs";
			this.ViewNPCs.Click += new EventHandler(this.ViewNPCs_Click);
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new System.Drawing.Size(174, 6);
			this.ViewGroups.Font = new System.Drawing.Font("Segoe UI", 9f);
			this.ViewGroups.Name = "ViewGroups";
			this.ViewGroups.Size = new System.Drawing.Size(177, 22);
			this.ViewGroups.Text = "Show in Groups";
			this.ViewGroups.Click += new EventHandler(this.ViewGroups_Click);
			this.Pages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Pages.Controls.Add(this.ThreatsPage);
			this.Pages.Controls.Add(this.MapPage);
			this.Pages.Controls.Add(this.NotesPage);
			this.Pages.Location = new Point(12, 12);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new System.Drawing.Size(810, 395);
			this.Pages.TabIndex = 0;
			this.ThreatsPage.Controls.Add(this.HSplitter);
			this.ThreatsPage.Location = new Point(4, 22);
			this.ThreatsPage.Name = "ThreatsPage";
			this.ThreatsPage.Padding = new System.Windows.Forms.Padding(3);
			this.ThreatsPage.Size = new System.Drawing.Size(802, 369);
			this.ThreatsPage.TabIndex = 0;
			this.ThreatsPage.Text = "Threats";
			this.ThreatsPage.UseVisualStyleBackColor = true;
			this.HSplitter.Dock = DockStyle.Fill;
			this.HSplitter.FixedPanel = FixedPanel.Panel2;
			this.HSplitter.Location = new Point(3, 3);
			this.HSplitter.Name = "HSplitter";
			this.HSplitter.Panel1.Controls.Add(this.VSplitter);
			this.HSplitter.Panel2.Controls.Add(this.SourceItemList);
			this.HSplitter.Panel2.Controls.Add(this.FilterPanel);
			this.HSplitter.Panel2.Controls.Add(this.ThreatToolbar);
			this.HSplitter.Size = new System.Drawing.Size(796, 363);
			this.HSplitter.SplitterDistance = 472;
			this.HSplitter.TabIndex = 2;
			this.VSplitter.Dock = DockStyle.Fill;
			this.VSplitter.FixedPanel = FixedPanel.Panel2;
			this.VSplitter.Location = new Point(0, 0);
			this.VSplitter.Name = "VSplitter";
			this.VSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.VSplitter.Panel1.Controls.Add(this.HintStatusbar);
			this.VSplitter.Panel1.Controls.Add(this.SlotList);
			this.VSplitter.Panel1.Controls.Add(this.EncToolbar);
			this.VSplitter.Panel2.Controls.Add(this.DifficultyList);
			this.VSplitter.Panel2.Controls.Add(this.XPGauge);
			this.VSplitter.Panel2.Controls.Add(this.XPStatusbar);
			this.VSplitter.Size = new System.Drawing.Size(472, 363);
			this.VSplitter.SplitterDistance = 231;
			this.VSplitter.TabIndex = 0;
			this.HintStatusbar.Items.AddRange(new ToolStripItem[] { this.HintLbl });
			this.HintStatusbar.Location = new Point(0, 209);
			this.HintStatusbar.Name = "HintStatusbar";
			this.HintStatusbar.Size = new System.Drawing.Size(472, 22);
			this.HintStatusbar.SizingGrip = false;
			this.HintStatusbar.TabIndex = 2;
			this.HintStatusbar.Text = "statusStrip1";
			this.HintLbl.Name = "HintLbl";
			this.HintLbl.Size = new System.Drawing.Size(415, 17);
			this.HintLbl.Text = "Drag items from the right into this list; double-click to view; right-click to edit";
			ToolStripItem[] xPLbl = new ToolStripItem[] { this.XPLbl, this.LevelLbl, this.DiffLbl, this.CountLbl, this.PartyLbl };
			this.XPStatusbar.Items.AddRange(xPLbl);
			this.XPStatusbar.Location = new Point(0, 106);
			this.XPStatusbar.Name = "XPStatusbar";
			this.XPStatusbar.Size = new System.Drawing.Size(472, 22);
			this.XPStatusbar.SizingGrip = false;
			this.XPStatusbar.TabIndex = 1;
			this.XPStatusbar.Text = "statusStrip1";
			this.XPLbl.Name = "XPLbl";
			this.XPLbl.Size = new System.Drawing.Size(29, 17);
			this.XPLbl.Text = "[XP]";
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new System.Drawing.Size(39, 17);
			this.LevelLbl.Text = "[level]";
			this.DiffLbl.Name = "DiffLbl";
			this.DiffLbl.Size = new System.Drawing.Size(33, 17);
			this.DiffLbl.Text = "[diff]";
			this.CountLbl.Name = "CountLbl";
			this.CountLbl.Size = new System.Drawing.Size(46, 17);
			this.CountLbl.Text = "[count]";
			this.PartyLbl.IsLink = true;
			this.PartyLbl.Name = "PartyLbl";
			this.PartyLbl.Size = new System.Drawing.Size(310, 17);
			this.PartyLbl.Spring = true;
			this.PartyLbl.Text = "Change Party";
			this.PartyLbl.TextAlign = ContentAlignment.MiddleRight;
			this.PartyLbl.Click += new EventHandler(this.PartyLbl_Click);
			this.MapPage.Controls.Add(this.MapSplitter);
			this.MapPage.Controls.Add(this.MapToolbar);
			this.MapPage.Location = new Point(4, 22);
			this.MapPage.Name = "MapPage";
			this.MapPage.Padding = new System.Windows.Forms.Padding(3);
			this.MapPage.Size = new System.Drawing.Size(802, 369);
			this.MapPage.TabIndex = 1;
			this.MapPage.Text = "Encounter Map";
			this.MapPage.UseVisualStyleBackColor = true;
			this.MapSplitter.Dock = DockStyle.Fill;
			this.MapSplitter.FixedPanel = FixedPanel.Panel2;
			this.MapSplitter.Location = new Point(3, 28);
			this.MapSplitter.Name = "MapSplitter";
			this.MapSplitter.Panel1.Controls.Add(this.MapView);
			this.MapSplitter.Panel2.Controls.Add(this.MapThreatList);
			this.MapSplitter.Size = new System.Drawing.Size(796, 338);
			this.MapSplitter.SplitterDistance = 586;
			this.MapSplitter.TabIndex = 2;
			ToolStripItem[] mapContextView = new ToolStripItem[] { this.MapContextView, this.toolStripSeparator15, this.MapContextSetPicture, this.toolStripMenuItem4, this.MapContextRemove, this.MapContextRemoveEncounter, this.MapContextCopy, this.toolStripSeparator5, this.MapContextVisible };
			this.MapContextMenu.Items.AddRange(mapContextView);
			this.MapContextMenu.Name = "MapContextMenu";
			this.MapContextMenu.Size = new System.Drawing.Size(204, 154);
			this.MapContextView.Name = "MapContextView";
			this.MapContextView.Size = new System.Drawing.Size(203, 22);
			this.MapContextView.Text = "View Stat Block";
			this.MapContextView.Click += new EventHandler(this.MapContextView_Click);
			this.toolStripSeparator15.Name = "toolStripSeparator15";
			this.toolStripSeparator15.Size = new System.Drawing.Size(200, 6);
			this.MapContextSetPicture.Name = "MapContextSetPicture";
			this.MapContextSetPicture.Size = new System.Drawing.Size(203, 22);
			this.MapContextSetPicture.Text = "Set Picture...";
			this.MapContextSetPicture.Click += new EventHandler(this.MapContextSetPicture_Click);
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(200, 6);
			this.MapContextRemove.Name = "MapContextRemove";
			this.MapContextRemove.Size = new System.Drawing.Size(203, 22);
			this.MapContextRemove.Text = "Remove from Map";
			this.MapContextRemove.Click += new EventHandler(this.MapContextRemove_Click);
			this.MapContextRemoveEncounter.Name = "MapContextRemoveEncounter";
			this.MapContextRemoveEncounter.Size = new System.Drawing.Size(203, 22);
			this.MapContextRemoveEncounter.Text = "Remove from Encounter";
			this.MapContextRemoveEncounter.Click += new EventHandler(this.MapContextRemoveEncounter_Click);
			this.MapContextCopy.Name = "MapContextCopy";
			this.MapContextCopy.Size = new System.Drawing.Size(203, 22);
			this.MapContextCopy.Text = "Create Copy";
			this.MapContextCopy.Click += new EventHandler(this.MapContextCopy_Click);
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(200, 6);
			this.MapContextVisible.Name = "MapContextVisible";
			this.MapContextVisible.Size = new System.Drawing.Size(203, 22);
			this.MapContextVisible.Text = "Visible";
			this.MapContextVisible.Click += new EventHandler(this.MapContextVisible_Click);
			this.MapThreatList.Columns.AddRange(new ColumnHeader[] { this.ThreatNameHdr });
			this.MapThreatList.Dock = DockStyle.Fill;
			this.MapThreatList.FullRowSelect = true;
			this.MapThreatList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.MapThreatList.HideSelection = false;
			this.MapThreatList.Location = new Point(0, 0);
			this.MapThreatList.MultiSelect = false;
			this.MapThreatList.Name = "MapThreatList";
			this.MapThreatList.Size = new System.Drawing.Size(206, 338);
			this.MapThreatList.TabIndex = 1;
			this.MapThreatList.UseCompatibleStateImageBehavior = false;
			this.MapThreatList.View = View.Details;
			this.MapThreatList.DoubleClick += new EventHandler(this.MapThreatList_DoubleClick);
			this.MapThreatList.ItemDrag += new ItemDragEventHandler(this.MapThreatList_ItemDrag);
			this.ThreatNameHdr.Text = "Creature";
			this.ThreatNameHdr.Width = 171;
			ToolStripItem[] mapBtn = new ToolStripItem[] { this.MapBtn, this.toolStripSeparator6, this.MapToolsMenu, this.MapCreaturesMenu };
			this.MapToolbar.Items.AddRange(mapBtn);
			this.MapToolbar.Location = new Point(3, 3);
			this.MapToolbar.Name = "MapToolbar";
			this.MapToolbar.Size = new System.Drawing.Size(796, 25);
			this.MapToolbar.TabIndex = 0;
			this.MapToolbar.Text = "toolStrip1";
			this.MapBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MapBtn.Image = (Image)resources.GetObject("MapBtn.Image");
			this.MapBtn.ImageTransparentColor = Color.Magenta;
			this.MapBtn.Name = "MapBtn";
			this.MapBtn.Size = new System.Drawing.Size(69, 22);
			this.MapBtn.Text = "Select Map";
			this.MapBtn.Click += new EventHandler(this.MapBtn_Click);
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
			this.MapToolsMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItem[] mapToolsLOS = new ToolStripItem[] { this.MapToolsLOS, this.MapToolsGridlines, this.MapToolsGridLabels, this.MapToolsPictureTokens, this.toolStripMenuItem5, this.MapToolsPrint, this.MapToolsScreenshot };
			this.MapToolsMenu.DropDownItems.AddRange(mapToolsLOS);
			this.MapToolsMenu.Image = (Image)resources.GetObject("MapToolsMenu.Image");
			this.MapToolsMenu.ImageTransparentColor = Color.Magenta;
			this.MapToolsMenu.Name = "MapToolsMenu";
			this.MapToolsMenu.Size = new System.Drawing.Size(49, 22);
			this.MapToolsMenu.Text = "Tools";
			this.MapToolsLOS.Name = "MapToolsLOS";
			this.MapToolsLOS.Size = new System.Drawing.Size(168, 22);
			this.MapToolsLOS.Text = "Line of Sight";
			this.MapToolsLOS.Click += new EventHandler(this.MapToolsLOS_Click);
			this.MapToolsGridlines.Name = "MapToolsGridlines";
			this.MapToolsGridlines.Size = new System.Drawing.Size(168, 22);
			this.MapToolsGridlines.Text = "Gridlines";
			this.MapToolsGridlines.Click += new EventHandler(this.MapToolsGridlines_Click);
			this.MapToolsGridLabels.Name = "MapToolsGridLabels";
			this.MapToolsGridLabels.Size = new System.Drawing.Size(168, 22);
			this.MapToolsGridLabels.Text = "Grid Labels";
			this.MapToolsGridLabels.Click += new EventHandler(this.MapToolsGridLabels_Click);
			this.MapToolsPictureTokens.Name = "MapToolsPictureTokens";
			this.MapToolsPictureTokens.Size = new System.Drawing.Size(168, 22);
			this.MapToolsPictureTokens.Text = "Picture Tokens";
			this.MapToolsPictureTokens.Click += new EventHandler(this.MapToolsPictureTokens_Click);
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(165, 6);
			this.MapToolsPrint.Name = "MapToolsPrint";
			this.MapToolsPrint.Size = new System.Drawing.Size(168, 22);
			this.MapToolsPrint.Text = "Print";
			this.MapToolsPrint.Click += new EventHandler(this.PrintBtn_Click);
			this.MapToolsScreenshot.Name = "MapToolsScreenshot";
			this.MapToolsScreenshot.Size = new System.Drawing.Size(168, 22);
			this.MapToolsScreenshot.Text = "Export Screenshot";
			this.MapToolsScreenshot.Click += new EventHandler(this.ExportBtn_Click);
			this.MapCreaturesMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItem[] mapCreaturesRemove = new ToolStripItem[] { this.MapCreaturesRemove, this.toolStripSeparator7, this.MapCreaturesShowAll, this.MapCreaturesHideAll, this.toolStripSeparator8, this.CreaturesAddCustom, this.CreaturesAddOverlay };
			this.MapCreaturesMenu.DropDownItems.AddRange(mapCreaturesRemove);
			this.MapCreaturesMenu.Image = (Image)resources.GetObject("MapCreaturesMenu.Image");
			this.MapCreaturesMenu.ImageTransparentColor = Color.Magenta;
			this.MapCreaturesMenu.Name = "MapCreaturesMenu";
			this.MapCreaturesMenu.Size = new System.Drawing.Size(85, 22);
			this.MapCreaturesMenu.Text = "Map Tokens";
			this.MapCreaturesRemove.Name = "MapCreaturesRemove";
			this.MapCreaturesRemove.Size = new System.Drawing.Size(193, 22);
			this.MapCreaturesRemove.Text = "Remove All";
			this.MapCreaturesRemove.Click += new EventHandler(this.MapCreaturesRemoveAll_Click);
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(190, 6);
			this.MapCreaturesShowAll.Name = "MapCreaturesShowAll";
			this.MapCreaturesShowAll.Size = new System.Drawing.Size(193, 22);
			this.MapCreaturesShowAll.Text = "Show All";
			this.MapCreaturesShowAll.Click += new EventHandler(this.MapCreaturesShowAll_Click);
			this.MapCreaturesHideAll.Name = "MapCreaturesHideAll";
			this.MapCreaturesHideAll.Size = new System.Drawing.Size(193, 22);
			this.MapCreaturesHideAll.Text = "Hide All";
			this.MapCreaturesHideAll.Click += new EventHandler(this.MapCreaturesHideAll_Click);
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(190, 6);
			this.CreaturesAddCustom.Name = "CreaturesAddCustom";
			this.CreaturesAddCustom.Size = new System.Drawing.Size(193, 22);
			this.CreaturesAddCustom.Text = "Add Custom Token...";
			this.CreaturesAddCustom.Click += new EventHandler(this.AddToken_Click);
			this.CreaturesAddOverlay.Name = "CreaturesAddOverlay";
			this.CreaturesAddOverlay.Size = new System.Drawing.Size(193, 22);
			this.CreaturesAddOverlay.Text = "Add Custom Overlay...";
			this.CreaturesAddOverlay.Click += new EventHandler(this.CreaturesAddOverlay_Click);
			this.NotesPage.Controls.Add(this.NoteSplitter);
			this.NotesPage.Controls.Add(this.NoteToolbar);
			this.NotesPage.Location = new Point(4, 22);
			this.NotesPage.Name = "NotesPage";
			this.NotesPage.Padding = new System.Windows.Forms.Padding(3);
			this.NotesPage.Size = new System.Drawing.Size(802, 369);
			this.NotesPage.TabIndex = 2;
			this.NotesPage.Text = "Notes";
			this.NotesPage.UseVisualStyleBackColor = true;
			this.NoteSplitter.Dock = DockStyle.Fill;
			this.NoteSplitter.FixedPanel = FixedPanel.Panel1;
			this.NoteSplitter.Location = new Point(3, 28);
			this.NoteSplitter.Name = "NoteSplitter";
			this.NoteSplitter.Panel1.Controls.Add(this.NoteList);
			this.NoteSplitter.Panel2.Controls.Add(this.BackgroundPanel);
			this.NoteSplitter.Size = new System.Drawing.Size(796, 338);
			this.NoteSplitter.SplitterDistance = 180;
			this.NoteSplitter.TabIndex = 2;
			this.NoteList.Columns.AddRange(new ColumnHeader[] { this.NoteHdr });
			this.NoteList.Dock = DockStyle.Fill;
			this.NoteList.FullRowSelect = true;
			this.NoteList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.NoteList.HideSelection = false;
			this.NoteList.Location = new Point(0, 0);
			this.NoteList.MultiSelect = false;
			this.NoteList.Name = "NoteList";
			this.NoteList.Size = new System.Drawing.Size(180, 338);
			this.NoteList.TabIndex = 0;
			this.NoteList.UseCompatibleStateImageBehavior = false;
			this.NoteList.View = View.Details;
			this.NoteList.SelectedIndexChanged += new EventHandler(this.NoteList_SelectedIndexChanged);
			this.NoteList.DoubleClick += new EventHandler(this.NoteEditBtn_Click);
			this.NoteHdr.Text = "Notes";
			this.NoteHdr.Width = 150;
			this.BackgroundPanel.BorderStyle = BorderStyle.FixedSingle;
			this.BackgroundPanel.Controls.Add(this.NoteDetails);
			this.BackgroundPanel.Dock = DockStyle.Fill;
			this.BackgroundPanel.Location = new Point(0, 0);
			this.BackgroundPanel.Name = "BackgroundPanel";
			this.BackgroundPanel.Size = new System.Drawing.Size(612, 338);
			this.BackgroundPanel.TabIndex = 0;
			this.NoteDetails.Dock = DockStyle.Fill;
			this.NoteDetails.IsWebBrowserContextMenuEnabled = false;
			this.NoteDetails.Location = new Point(0, 0);
			this.NoteDetails.MinimumSize = new System.Drawing.Size(20, 20);
			this.NoteDetails.Name = "NoteDetails";
			this.NoteDetails.Size = new System.Drawing.Size(610, 336);
			this.NoteDetails.TabIndex = 0;
			this.NoteDetails.Navigating += new WebBrowserNavigatingEventHandler(this.NoteDetails_Navigating);
			ToolStripItem[] noteAddBtn = new ToolStripItem[] { this.NoteAddBtn, this.NoteRemoveBtn, this.NoteEditBtn, this.toolStripSeparator21, this.NoteUpBtn, this.NoteDownBtn };
			this.NoteToolbar.Items.AddRange(noteAddBtn);
			this.NoteToolbar.Location = new Point(3, 3);
			this.NoteToolbar.Name = "NoteToolbar";
			this.NoteToolbar.Size = new System.Drawing.Size(796, 25);
			this.NoteToolbar.TabIndex = 1;
			this.NoteToolbar.Text = "toolStrip1";
			this.NoteAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NoteAddBtn.Image = (Image)resources.GetObject("NoteAddBtn.Image");
			this.NoteAddBtn.ImageTransparentColor = Color.Magenta;
			this.NoteAddBtn.Name = "NoteAddBtn";
			this.NoteAddBtn.Size = new System.Drawing.Size(33, 22);
			this.NoteAddBtn.Text = "Add";
			this.NoteAddBtn.Click += new EventHandler(this.NoteAddBtn_Click);
			this.NoteRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NoteRemoveBtn.Image = (Image)resources.GetObject("NoteRemoveBtn.Image");
			this.NoteRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.NoteRemoveBtn.Name = "NoteRemoveBtn";
			this.NoteRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.NoteRemoveBtn.Text = "Remove";
			this.NoteRemoveBtn.Click += new EventHandler(this.NoteRemoveBtn_Click);
			this.NoteEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NoteEditBtn.Image = (Image)resources.GetObject("NoteEditBtn.Image");
			this.NoteEditBtn.ImageTransparentColor = Color.Magenta;
			this.NoteEditBtn.Name = "NoteEditBtn";
			this.NoteEditBtn.Size = new System.Drawing.Size(31, 22);
			this.NoteEditBtn.Text = "Edit";
			this.NoteEditBtn.Click += new EventHandler(this.NoteEditBtn_Click);
			this.toolStripSeparator21.Name = "toolStripSeparator21";
			this.toolStripSeparator21.Size = new System.Drawing.Size(6, 25);
			this.NoteUpBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NoteUpBtn.Image = (Image)resources.GetObject("NoteUpBtn.Image");
			this.NoteUpBtn.ImageTransparentColor = Color.Magenta;
			this.NoteUpBtn.Name = "NoteUpBtn";
			this.NoteUpBtn.Size = new System.Drawing.Size(59, 22);
			this.NoteUpBtn.Text = "Move Up";
			this.NoteUpBtn.Click += new EventHandler(this.NoteUpBtn_Click);
			this.NoteDownBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NoteDownBtn.Image = (Image)resources.GetObject("NoteDownBtn.Image");
			this.NoteDownBtn.ImageTransparentColor = Color.Magenta;
			this.NoteDownBtn.Name = "NoteDownBtn";
			this.NoteDownBtn.Size = new System.Drawing.Size(75, 22);
			this.NoteDownBtn.Text = "Move Down";
			this.NoteDownBtn.Click += new EventHandler(this.NoteDownBtn_Click);
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(666, 413);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(747, 413);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.InfoBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.InfoBtn.Location = new Point(12, 413);
			this.InfoBtn.Name = "InfoBtn";
			this.InfoBtn.Size = new System.Drawing.Size(75, 23);
			this.InfoBtn.TabIndex = 1;
			this.InfoBtn.Text = "Information";
			this.InfoBtn.UseVisualStyleBackColor = true;
			this.InfoBtn.Click += new EventHandler(this.InfoBtn_Click);
			this.DieRollerBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.DieRollerBtn.Location = new Point(93, 413);
			this.DieRollerBtn.Name = "DieRollerBtn";
			this.DieRollerBtn.Size = new System.Drawing.Size(75, 23);
			this.DieRollerBtn.TabIndex = 2;
			this.DieRollerBtn.Text = "Die Roller";
			this.DieRollerBtn.UseVisualStyleBackColor = true;
			this.DieRollerBtn.Click += new EventHandler(this.DieRollerBtn_Click);
			this.XPGauge.Dock = DockStyle.Bottom;
			this.XPGauge.Location = new Point(0, 86);
			this.XPGauge.Name = "XPGauge";
			this.XPGauge.Party = null;
			this.XPGauge.Size = new System.Drawing.Size(472, 20);
			this.XPGauge.TabIndex = 1;
			this.XPGauge.XP = 0;
			this.FilterPanel.AutoSize = true;
			this.FilterPanel.BorderStyle = BorderStyle.FixedSingle;
			this.FilterPanel.Dock = DockStyle.Top;
			this.FilterPanel.Location = new Point(0, 25);
			this.FilterPanel.Mode = ListMode.Creatures;
			this.FilterPanel.Name = "FilterPanel";
			this.FilterPanel.PartyLevel = 1;
			this.FilterPanel.Size = new System.Drawing.Size(320, 24);
			this.FilterPanel.TabIndex = 0;
			this.FilterPanel.FilterChanged += new EventHandler(this.FilterPanel_FilterChanged);
			this.MapView.AllowDrawing = false;
			this.MapView.AllowDrop = true;
			this.MapView.AllowLinkCreation = false;
			this.MapView.AllowScrolling = false;
			this.MapView.BackgroundMap = null;
			this.MapView.BorderSize = 1;
			this.MapView.Caption = "";
			this.MapView.ContextMenuStrip = this.MapContextMenu;
			this.MapView.Cursor = Cursors.Default;
			this.MapView.Dock = DockStyle.Fill;
			this.MapView.Encounter = null;
			this.MapView.FrameType = MapDisplayType.Dimmed;
			this.MapView.HighlightAreas = false;
			this.MapView.HoverToken = null;
			this.MapView.HoverTokenLink = null;
			this.MapView.LineOfSight = false;
			this.MapView.Location = new Point(0, 0);
			this.MapView.Map = null;
			this.MapView.Mode = MapViewMode.Thumbnail;
			this.MapView.Name = "MapView";
			this.MapView.Plot = null;
			this.MapView.ScalingFactor = 1;
			this.MapView.SelectedArea = null;
			this.MapView.SelectedTiles = null;
			this.MapView.Selection = new Rectangle(0, 0, 0, 0);
			this.MapView.ShowAllWaves = true;
			this.MapView.ShowAuras = true;
			this.MapView.ShowConditions = true;
			this.MapView.ShowCreatureLabels = true;
			this.MapView.ShowCreatures = CreatureViewMode.All;
			this.MapView.ShowGrid = MapGridMode.None;
			this.MapView.ShowGridLabels = false;
			this.MapView.ShowHealthBars = false;
			this.MapView.ShowPictureTokens = true;
			this.MapView.Size = new System.Drawing.Size(586, 338);
			this.MapView.TabIndex = 1;
			this.MapView.Tactical = true;
			this.MapView.TokenLinks = null;
			this.MapView.Viewpoint = new Rectangle(0, 0, 0, 0);
			this.MapView.TokenActivated += new TokenEventHandler(this.MapView_TokenActivated);
			this.MapView.ItemDropped += new ItemDroppedEventHandler(this.MapView_ItemDropped);
			this.MapView.DoubleClick += new EventHandler(this.MapView_DoubleClick);
			this.MapView.SelectedTokensChanged += new EventHandler(this.MapView_SelectedTokensChanged);
			this.MapView.HoverTokenChanged += new EventHandler(this.MapView_HoverTokenChanged);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(834, 448);
			base.Controls.Add(this.DieRollerBtn);
			base.Controls.Add(this.InfoBtn);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.Pages);
			base.MinimizeBox = false;
			base.Name = "EncounterBuilderForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Encounter Builder";
			this.ThreatContextMenu.ResumeLayout(false);
			this.EncToolbar.ResumeLayout(false);
			this.EncToolbar.PerformLayout();
			this.ThreatToolbar.ResumeLayout(false);
			this.ThreatToolbar.PerformLayout();
			this.Pages.ResumeLayout(false);
			this.ThreatsPage.ResumeLayout(false);
			this.HSplitter.Panel1.ResumeLayout(false);
			this.HSplitter.Panel2.ResumeLayout(false);
			this.HSplitter.Panel2.PerformLayout();
			this.HSplitter.ResumeLayout(false);
			this.VSplitter.Panel1.ResumeLayout(false);
			this.VSplitter.Panel1.PerformLayout();
			this.VSplitter.Panel2.ResumeLayout(false);
			this.VSplitter.Panel2.PerformLayout();
			this.VSplitter.ResumeLayout(false);
			this.HintStatusbar.ResumeLayout(false);
			this.HintStatusbar.PerformLayout();
			this.XPStatusbar.ResumeLayout(false);
			this.XPStatusbar.PerformLayout();
			this.MapPage.ResumeLayout(false);
			this.MapPage.PerformLayout();
			this.MapSplitter.Panel1.ResumeLayout(false);
			this.MapSplitter.Panel2.ResumeLayout(false);
			this.MapSplitter.ResumeLayout(false);
			this.MapContextMenu.ResumeLayout(false);
			this.MapToolbar.ResumeLayout(false);
			this.MapToolbar.PerformLayout();
			this.NotesPage.ResumeLayout(false);
			this.NotesPage.PerformLayout();
			this.NoteSplitter.Panel1.ResumeLayout(false);
			this.NoteSplitter.Panel2.ResumeLayout(false);
			this.NoteSplitter.ResumeLayout(false);
			this.BackgroundPanel.ResumeLayout(false);
			this.NoteToolbar.ResumeLayout(false);
			this.NoteToolbar.PerformLayout();
			base.ResumeLayout(false);
		}

		private void MapBtn_Click(object sender, EventArgs e)
		{
			MapAreaSelectForm mapAreaSelectForm = new MapAreaSelectForm(this.fEncounter.MapID, this.fEncounter.MapAreaID);
			if (mapAreaSelectForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Guid guid = (mapAreaSelectForm.Map != null ? mapAreaSelectForm.Map.ID : Guid.Empty);
				Guid guid1 = (mapAreaSelectForm.MapArea != null ? mapAreaSelectForm.MapArea.ID : Guid.Empty);
				if (guid == this.fEncounter.MapID && guid1 == this.fEncounter.MapAreaID)
				{
					return;
				}
				foreach (EncounterSlot slot in this.fEncounter.Slots)
				{
					foreach (CombatData combatDatum in slot.CombatData)
					{
						combatDatum.Location = CombatData.NoPoint;
					}
				}
				foreach (CustomToken customToken in this.fEncounter.CustomTokens)
				{
					customToken.Data.Location = CombatData.NoPoint;
				}
				this.fEncounter.MapID = guid;
				this.fEncounter.MapAreaID = guid1;
				this.MapView.Map = mapAreaSelectForm.Map;
				this.MapView.Viewpoint = (mapAreaSelectForm.MapArea != null ? mapAreaSelectForm.MapArea.Region : Rectangle.Empty);
				this.MapView.Encounter = this.fEncounter;
				this.update_mapthreats();
			}
		}

		private void MapContextCopy_Click(object sender, EventArgs e)
		{
			if (this.MapView.SelectedTokens.Count != 1)
			{
				return;
			}
			CustomToken item = this.MapView.SelectedTokens[0] as CustomToken;
			if (item != null)
			{
				CustomToken noPoint = item.Copy();
				noPoint.ID = Guid.NewGuid();
				noPoint.Data.Location = CombatData.NoPoint;
				this.fEncounter.CustomTokens.Add(noPoint);
			}
			this.update_mapthreats();
		}

		private void MapContextRemove_Click(object sender, EventArgs e)
		{
			foreach (IToken selectedToken in this.MapView.SelectedTokens)
			{
				CreatureToken noPoint = selectedToken as CreatureToken;
				if (noPoint != null)
				{
					noPoint.Data.Location = CombatData.NoPoint;
				}
				CustomToken customToken = selectedToken as CustomToken;
				if (customToken == null)
				{
					continue;
				}
				customToken.Data.Location = CombatData.NoPoint;
			}
			this.update_mapthreats();
		}

		private void MapContextRemoveEncounter_Click(object sender, EventArgs e)
		{
			foreach (IToken selectedToken in this.MapView.SelectedTokens)
			{
				CreatureToken creatureToken = selectedToken as CreatureToken;
				if (creatureToken != null)
				{
					EncounterSlot encounterSlot = this.fEncounter.FindSlot(creatureToken.SlotID);
					encounterSlot.CombatData.Remove(creatureToken.Data);
					if (encounterSlot.CombatData.Count == 0)
					{
						this.fEncounter.Slots.Remove(encounterSlot);
					}
				}
				CustomToken customToken = selectedToken as CustomToken;
				if (customToken == null)
				{
					continue;
				}
				this.fEncounter.CustomTokens.Remove(customToken);
			}
			this.update_encounter();
			this.update_mapthreats();
		}

		private void MapContextSetPicture_Click(object sender, EventArgs e)
		{
			if (this.MapView.SelectedTokens.Count != 1)
			{
				return;
			}
			CreatureToken item = this.MapView.SelectedTokens[0] as CreatureToken;
			if (item != null)
			{
				EncounterSlot encounterSlot = this.fEncounter.FindSlot(item.SlotID);
				ICreature creature = Session.FindCreature(encounterSlot.Card.CreatureID, SearchType.Global);
				if (creature != null)
				{
					OpenFileDialog openFileDialog = new OpenFileDialog()
					{
						Filter = Program.ImageFilter
					};
					if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						creature.Image = Image.FromFile(openFileDialog.FileName);
						Program.SetResolution(creature.Image);
						if (!(creature is Creature))
						{
							Session.Modified = true;
						}
						else
						{
							Library library = Session.FindLibrary(creature as Creature);
							if (library != null)
							{
								string libraryFilename = Session.GetLibraryFilename(library);
								Serialisation<Library>.Save(libraryFilename, library, SerialisationMode.Binary);
							}
						}
						this.MapView.Invalidate();
					}
				}
			}
		}

		private void MapContextView_Click(object sender, EventArgs e)
		{
			if (this.MapView.SelectedTokens.Count != 1)
			{
				return;
			}
			CreatureToken item = this.MapView.SelectedTokens[0] as CreatureToken;
			if (item != null)
			{
				EncounterSlot encounterSlot = this.fEncounter.FindSlot(item.SlotID);
				(new CreatureDetailsForm(encounterSlot.Card)).ShowDialog();
			}
			CustomToken customToken = this.MapView.SelectedTokens[0] as CustomToken;
			if (customToken != null)
			{
				int token = this.fEncounter.CustomTokens.IndexOf(customToken);
				switch (customToken.Type)
				{
					case CustomTokenType.Token:
					{
						CustomTokenForm customTokenForm = new CustomTokenForm(customToken);
						if (customTokenForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
						{
							break;
						}
						this.fEncounter.CustomTokens[token] = customTokenForm.Token;
						this.update_encounter();
						this.update_mapthreats();
						return;
					}
					case CustomTokenType.Overlay:
					{
						CustomOverlayForm customOverlayForm = new CustomOverlayForm(customToken);
						if (customOverlayForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
						{
							break;
						}
						this.fEncounter.CustomTokens[token] = customOverlayForm.Token;
						this.update_encounter();
						this.update_mapthreats();
						break;
					}
					default:
					{
						return;
					}
				}
			}
		}

		private void MapContextVisible_Click(object sender, EventArgs e)
		{
			foreach (IToken selectedToken in this.MapView.SelectedTokens)
			{
				CreatureToken visible = selectedToken as CreatureToken;
				if (visible != null)
				{
					visible.Data.Visible = !visible.Data.Visible;
					this.MapView.Invalidate();
				}
				CustomToken customToken = selectedToken as CustomToken;
				if (customToken == null)
				{
					continue;
				}
				customToken.Data.Visible = !customToken.Data.Visible;
				this.MapView.Invalidate();
			}
		}

		private void MapCreaturesHideAll_Click(object sender, EventArgs e)
		{
			foreach (EncounterSlot slot in this.fEncounter.Slots)
			{
				foreach (CombatData combatDatum in slot.CombatData)
				{
					combatDatum.Visible = false;
				}
			}
			foreach (CustomToken customToken in this.fEncounter.CustomTokens)
			{
				customToken.Data.Visible = false;
			}
			this.MapView.MapChanged();
		}

		private void MapCreaturesRemoveAll_Click(object sender, EventArgs e)
		{
			foreach (EncounterSlot slot in this.fEncounter.Slots)
			{
				foreach (CombatData combatDatum in slot.CombatData)
				{
					combatDatum.Location = CombatData.NoPoint;
				}
			}
			foreach (CustomToken customToken in this.fEncounter.CustomTokens)
			{
				customToken.Data.Location = CombatData.NoPoint;
			}
			this.MapView.MapChanged();
			this.update_mapthreats();
		}

		private void MapCreaturesShowAll_Click(object sender, EventArgs e)
		{
			foreach (EncounterSlot slot in this.fEncounter.Slots)
			{
				foreach (CombatData combatDatum in slot.CombatData)
				{
					combatDatum.Visible = true;
				}
			}
			foreach (CustomToken customToken in this.fEncounter.CustomTokens)
			{
				customToken.Data.Visible = true;
			}
			this.MapView.MapChanged();
		}

		private void MapThreatList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedMapThreat != null)
			{
				CreatureToken selectedMapThreat = this.SelectedMapThreat as CreatureToken;
				if (selectedMapThreat != null)
				{
					EncounterSlot encounterSlot = this.fEncounter.FindSlot(selectedMapThreat.SlotID);
					(new CreatureDetailsForm(encounterSlot.Card)).ShowDialog();
				}
				CustomToken customToken = this.SelectedMapThreat as CustomToken;
				if (customToken != null)
				{
					int token = this.fEncounter.CustomTokens.IndexOf(customToken);
					switch (customToken.Type)
					{
						case CustomTokenType.Token:
						{
							CustomTokenForm customTokenForm = new CustomTokenForm(customToken);
							if (customTokenForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
							{
								break;
							}
							this.fEncounter.CustomTokens[token] = customTokenForm.Token;
							this.update_encounter();
							this.update_mapthreats();
							return;
						}
						case CustomTokenType.Overlay:
						{
							CustomOverlayForm customOverlayForm = new CustomOverlayForm(customToken);
							if (customOverlayForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
							{
								break;
							}
							this.fEncounter.CustomTokens[token] = customOverlayForm.Token;
							this.update_encounter();
							this.update_mapthreats();
							break;
						}
						default:
						{
							return;
						}
					}
				}
			}
		}

		private void MapThreatList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedMapThreat != null)
			{
				base.DoDragDrop(this.SelectedMapThreat, DragDropEffects.Move);
			}
		}

		private void MapToolsGridLabels_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.ShowGridLabels = !this.MapView.ShowGridLabels;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void MapToolsGridlines_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.ShowGrid = (this.MapView.ShowGrid == MapGridMode.None ? MapGridMode.Overlay : MapGridMode.None);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void MapToolsLOS_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.LineOfSight = !this.MapView.LineOfSight;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void MapToolsPictureTokens_Click(object sender, EventArgs e)
		{
			this.MapView.ShowPictureTokens = !this.MapView.ShowPictureTokens;
		}

		private void MapView_DoubleClick(object sender, EventArgs e)
		{
			if (this.fEncounter.MapID == Guid.Empty)
			{
				this.MapBtn_Click(sender, e);
			}
		}

		private void MapView_HoverTokenChanged(object sender, EventArgs e)
		{
			string title = "";
			string info = "";
			CreatureToken hoverToken = this.MapView.HoverToken as CreatureToken;
			if (hoverToken != null)
			{
				EncounterSlot encounterSlot = this.fEncounter.FindSlot(hoverToken.SlotID);
				title = encounterSlot.Card.Title;
				info = encounterSlot.Card.Info;
				info = string.Concat(info, Environment.NewLine);
				info = string.Concat(info, "Double-click for more details");
			}
			CustomToken customToken = this.MapView.HoverToken as CustomToken;
			if (customToken != null)
			{
				title = customToken.Name;
				info = "Double-click to edit";
			}
			this.Tooltip.ToolTipTitle = title;
			this.Tooltip.ToolTipIcon = ToolTipIcon.Info;
			this.Tooltip.SetToolTip(this.MapView, info);
		}

		private void MapView_ItemDropped(CombatData data, Point location)
		{
            data.Location = location;
			this.update_mapthreats();
		}

		private void MapView_SelectedTokensChanged(object sender, EventArgs e)
		{
		}

		private void MapView_TokenActivated(object sender, TokenEventArgs e)
		{
			CreatureToken token = e.Token as CreatureToken;
			if (token != null)
			{
				EncounterSlot encounterSlot = this.fEncounter.FindSlot(token.SlotID);
				(new CreatureDetailsForm(encounterSlot.Card)).ShowDialog();
			}
			CustomToken customToken = e.Token as CustomToken;
			if (customToken != null)
			{
				int num = this.fEncounter.CustomTokens.IndexOf(customToken);
				if (num != -1)
				{
					switch (customToken.Type)
					{
						case CustomTokenType.Token:
						{
							CustomTokenForm customTokenForm = new CustomTokenForm(customToken);
							if (customTokenForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
							{
								break;
							}
							this.fEncounter.CustomTokens[num] = customTokenForm.Token;
							this.update_encounter();
							this.update_mapthreats();
							return;
						}
						case CustomTokenType.Overlay:
						{
							CustomOverlayForm customOverlayForm = new CustomOverlayForm(customToken);
							if (customOverlayForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
							{
								break;
							}
							this.fEncounter.CustomTokens[num] = customOverlayForm.Token;
							this.update_encounter();
							this.update_mapthreats();
							break;
						}
						default:
						{
							return;
						}
					}
				}
			}
		}

		private void NoteAddBtn_Click(object sender, EventArgs e)
		{
			try
			{
				EncounterNoteForm encounterNoteForm = new EncounterNoteForm(new EncounterNote("New Note"));
				if (encounterNoteForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fEncounter.Notes.Add(encounterNoteForm.Note);
					this.update_notes();
					this.SelectedNote = encounterNoteForm.Note;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void NoteDetails_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			try
			{
				if (e.Url.Scheme == "note")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "edit")
					{
						this.NoteEditBtn_Click(sender, e);
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void NoteDownBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedNote != null && this.fEncounter.Notes.IndexOf(this.SelectedNote) != this.fEncounter.Notes.Count - 1)
				{
					int selectedNote = this.fEncounter.Notes.IndexOf(this.SelectedNote);
					EncounterNote item = this.fEncounter.Notes[selectedNote + 1];
					this.fEncounter.Notes[selectedNote + 1] = this.SelectedNote;
					this.fEncounter.Notes[selectedNote] = item;
					this.update_notes();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void NoteEditBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedNote != null)
				{
					int note = this.fEncounter.Notes.IndexOf(this.SelectedNote);
					EncounterNoteForm encounterNoteForm = new EncounterNoteForm(this.SelectedNote);
					if (encounterNoteForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fEncounter.Notes[note] = encounterNoteForm.Note;
						this.update_notes();
						this.SelectedNote = encounterNoteForm.Note;
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void NoteList_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				this.update_selected_note();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void NoteRemoveBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedNote != null)
				{
					if (MessageBox.Show("Remove encounter note: are you sure?", "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.No)
					{
						this.fEncounter.Notes.Remove(this.SelectedNote);
						this.update_notes();
						this.SelectedNote = null;
					}
					else
					{
						return;
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void NoteUpBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedNote != null && this.fEncounter.Notes.IndexOf(this.SelectedNote) != 0)
				{
					int selectedNote = this.fEncounter.Notes.IndexOf(this.SelectedNote);
					EncounterNote item = this.fEncounter.Notes[selectedNote - 1];
					this.fEncounter.Notes[selectedNote - 1] = this.SelectedNote;
					this.fEncounter.Notes[selectedNote] = item;
					this.update_notes();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void OpponentList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedCreature != null)
			{
				base.DoDragDrop(this.SelectedCreature, DragDropEffects.All);
			}
			if (this.SelectedTemplate != null)
			{
				base.DoDragDrop(this.SelectedTemplate, DragDropEffects.All);
			}
			if (this.SelectedNPC != null)
			{
				base.DoDragDrop(this.SelectedNPC, DragDropEffects.All);
			}
			if (this.SelectedTrap != null)
			{
				base.DoDragDrop(this.SelectedTrap, DragDropEffects.All);
			}
			if (this.SelectedSkillChallenge != null)
			{
				base.DoDragDrop(this.SelectedSkillChallenge, DragDropEffects.All);
			}
		}

		private void PartyLbl_Click(object sender, EventArgs e)
		{
			PartyForm partyForm = new PartyForm(new Party(this.fPartySize, this.fPartyLevel));
			if (partyForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fPartySize = partyForm.Party.Size;
				this.fPartyLevel = partyForm.Party.Level;
				this.update_difficulty_list();
				this.update_encounter();
				this.update_party_label();
			}
		}

		private void perform_swap(Creature creature, int count, EncounterSlot old_slot)
		{
			EncounterSlot encounterSlot = new EncounterSlot();
			encounterSlot.Card.CreatureID = creature.ID;
			for (int i = 0; i != count; i++)
			{
				CombatData combatDatum = new CombatData();
				encounterSlot.CombatData.Add(combatDatum);
			}
			this.fEncounter.Slots.Remove(old_slot);
			this.fEncounter.Slots.Add(encounterSlot);
			this.update_encounter();
			this.update_mapthreats();
		}

		private void PrintBtn_Click(object sender, EventArgs e)
		{
			(new MapPrintingForm(this.MapView)).ShowDialog();
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSlot != null)
			{
				if ((Control.ModifierKeys & Keys.Shift) != Keys.Shift)
				{
					this.SelectedSlot.CombatData.RemoveAt(this.SelectedSlot.CombatData.Count - 1);
					if (this.SelectedSlot.CombatData.Count == 0)
					{
						this.fEncounter.Slots.Remove(this.SelectedSlot);
					}
					this.update_encounter();
					this.update_mapthreats();
				}
				else if (this.SelectedSlot.Card.Level > 1)
				{
					EncounterCard card = this.SelectedSlot.Card;
					card.LevelAdjustment = card.LevelAdjustment - 1;
					this.update_encounter();
				}
			}
			if (this.SelectedSlotTrap != null)
			{
				this.fEncounter.Traps.Remove(this.SelectedSlotTrap);
				this.update_encounter();
			}
			if (this.SelectedSlotSkillChallenge != null)
			{
				this.fEncounter.SkillChallenges.Remove(this.SelectedSlotSkillChallenge);
				this.update_encounter();
			}
		}

		private void SlotList_DoubleClick(object sender, EventArgs e)
		{
			this.StatBlockBtn_Click(sender, e);
		}

		private void SlotList_DragDrop(object sender, DragEventArgs e)
		{
			Creature data = e.Data.GetData(typeof(Creature)) as Creature;
			if (data != null)
			{
				this.add_opponent(data);
			}
			CustomCreature customCreature = e.Data.GetData(typeof(CustomCreature)) as CustomCreature;
			if (customCreature != null)
			{
				this.add_opponent(customCreature);
			}
			NPC nPC = e.Data.GetData(typeof(NPC)) as NPC;
			if (nPC != null)
			{
				this.add_opponent(nPC);
			}
			Trap trap = e.Data.GetData(typeof(Trap)) as Trap;
			if (trap != null)
			{
				this.add_trap(trap);
			}
			SkillChallenge skillChallenge = e.Data.GetData(typeof(SkillChallenge)) as SkillChallenge;
			if (skillChallenge != null)
			{
				this.add_challenge(skillChallenge);
			}
			CreatureTemplate creatureTemplate = e.Data.GetData(typeof(CreatureTemplate)) as CreatureTemplate;
			if (creatureTemplate != null && this.SelectedSlot != null && this.allow_template_drop(this.SelectedSlot, creatureTemplate))
			{
				this.add_template(creatureTemplate, this.SelectedSlot);
			}
		}

		private void SlotList_DragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetData(typeof(Creature)) is Creature)
			{
				e.Effect = DragDropEffects.Copy;
			}
			if (e.Data.GetData(typeof(CustomCreature)) is CustomCreature)
			{
				e.Effect = DragDropEffects.Copy;
			}
			if (e.Data.GetData(typeof(NPC)) is NPC)
			{
				e.Effect = DragDropEffects.Copy;
			}
			if (e.Data.GetData(typeof(Trap)) is Trap)
			{
				e.Effect = DragDropEffects.Copy;
			}
			if (e.Data.GetData(typeof(SkillChallenge)) is SkillChallenge)
			{
				e.Effect = DragDropEffects.Copy;
			}
			CreatureTemplate data = e.Data.GetData(typeof(CreatureTemplate)) as CreatureTemplate;
			if (data != null)
			{
				Point client = this.SlotList.PointToClient(System.Windows.Forms.Cursor.Position);
				ListViewItem itemAt = this.SlotList.GetItemAt(client.X, client.Y);
				itemAt.Selected = true;
				EncounterSlot tag = itemAt.Tag as EncounterSlot;
				if (tag != null && this.allow_template_drop(tag, data))
				{
					e.Effect = DragDropEffects.Copy;
					return;
				}
				e.Effect = DragDropEffects.None;
			}
		}

		private void SlotList_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				if (this.SelectedSlot != null)
				{
					this.fEncounter.Slots.Remove(this.SelectedSlot);
					this.update_encounter();
					e.Handled = true;
					return;
				}
				if (this.SelectedSlotSkillChallenge != null)
				{
					this.fEncounter.SkillChallenges.Remove(this.SelectedSlotSkillChallenge);
					this.update_encounter();
					e.Handled = true;
					return;
				}
				if (this.SelectedSlotTrap != null)
				{
					this.fEncounter.Traps.Remove(this.SelectedSlotTrap);
					this.update_encounter();
					e.Handled = true;
				}
			}
		}

		private void SourceItemList_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			(this.SourceItemList.ListViewItemSorter as EncounterBuilderForm.SourceSorter).Set(e.Column);
			this.SourceItemList.Sort();
		}

		private void StatBlockBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSlot != null)
			{
				(new CreatureDetailsForm(this.SelectedSlot.Card)).ShowDialog();
			}
			if (this.SelectedSlotTrap != null)
			{
				(new TrapDetailsForm(this.SelectedSlotTrap)).ShowDialog();
			}
			if (this.SelectedSlotSkillChallenge != null)
			{
				(new SkillChallengeDetailsForm(this.SelectedSlotSkillChallenge)).ShowDialog();
			}
		}

		private void SwapElite_Click(object sender, EventArgs e)
		{
			EncounterCard card = this.SelectedSlot.Card;
			ICreature creature = Session.FindCreature(card.CreatureID, SearchType.Global);
			int count = 1;
			if (!(creature.Role is Minion))
			{
				switch (card.Flag)
				{
					case RoleFlag.Standard:
					{
						count = this.SelectedSlot.CombatData.Count / 2;
						break;
					}
					case RoleFlag.Elite:
					{
						count = this.SelectedSlot.CombatData.Count;
						break;
					}
					case RoleFlag.Solo:
					{
						count = this.SelectedSlot.CombatData.Count * 5 / 2;
						break;
					}
				}
			}
			else
			{
				count = this.SelectedSlot.CombatData.Count / 8;
			}
			List<Creature> creatures = this.find_creatures(RoleFlag.Elite, card.Level, card.Roles);
			if (creatures.Count == 0)
			{
				MessageBox.Show("There are no creatures of this type.", "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			Creature creature1 = this.choose_creature(creatures, creature.Category);
			if (creature1 == null)
			{
				return;
			}
			this.perform_swap(creature1, count, this.SelectedSlot);
		}

		private void SwapMinions_Click(object sender, EventArgs e)
		{
			EncounterCard card = this.SelectedSlot.Card;
			ICreature creature = Session.FindCreature(card.CreatureID, SearchType.Global);
			int count = 1;
			if (!(creature.Role is Minion))
			{
				switch (card.Flag)
				{
					case RoleFlag.Standard:
					{
						count = this.SelectedSlot.CombatData.Count * 4;
						break;
					}
					case RoleFlag.Elite:
					{
						count = this.SelectedSlot.CombatData.Count * 8;
						break;
					}
					case RoleFlag.Solo:
					{
						count = this.SelectedSlot.CombatData.Count * 20;
						break;
					}
				}
			}
			else
			{
				count = this.SelectedSlot.CombatData.Count / 4;
			}
			List<Creature> creatures = this.find_minions(card.Level);
			if (creatures.Count == 0)
			{
				MessageBox.Show("There are no creatures of this type.", "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			Creature creature1 = this.choose_creature(creatures, creature.Category);
			if (creature1 == null)
			{
				return;
			}
			this.perform_swap(creature1, count, this.SelectedSlot);
		}

		private void SwapSolo_Click(object sender, EventArgs e)
		{
			EncounterCard card = this.SelectedSlot.Card;
			ICreature creature = Session.FindCreature(card.CreatureID, SearchType.Global);
			int count = 1;
			if (!(creature.Role is Minion))
			{
				switch (card.Flag)
				{
					case RoleFlag.Standard:
					{
						count = this.SelectedSlot.CombatData.Count / 5;
						break;
					}
					case RoleFlag.Elite:
					{
						count = this.SelectedSlot.CombatData.Count * 2 / 5;
						break;
					}
					case RoleFlag.Solo:
					{
						count = this.SelectedSlot.CombatData.Count;
						break;
					}
				}
			}
			else
			{
				count = this.SelectedSlot.CombatData.Count / 20;
			}
			List<Creature> creatures = this.find_creatures(RoleFlag.Solo, card.Level, card.Roles);
			if (creatures.Count == 0)
			{
				MessageBox.Show("There are no creatures of this type.", "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			Creature creature1 = this.choose_creature(creatures, creature.Category);
			if (creature1 == null)
			{
				return;
			}
			this.perform_swap(creature1, count, this.SelectedSlot);
		}

		private void SwapStandard_Click(object sender, EventArgs e)
		{
			EncounterCard card = this.SelectedSlot.Card;
			ICreature creature = Session.FindCreature(card.CreatureID, SearchType.Global);
			int count = 1;
			if (!(creature.Role is Minion))
			{
				switch (card.Flag)
				{
					case RoleFlag.Standard:
					{
						count = this.SelectedSlot.CombatData.Count;
						break;
					}
					case RoleFlag.Elite:
					{
						count = this.SelectedSlot.CombatData.Count * 2;
						break;
					}
					case RoleFlag.Solo:
					{
						count = this.SelectedSlot.CombatData.Count * 5;
						break;
					}
				}
			}
			else
			{
				count = this.SelectedSlot.CombatData.Count / 4;
			}
			List<Creature> creatures = this.find_creatures(RoleFlag.Standard, card.Level, card.Roles);
			if (creatures.Count == 0)
			{
				MessageBox.Show("There are no creatures of this type.", "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			Creature creature1 = this.choose_creature(creatures, creature.Category);
			if (creature1 == null)
			{
				return;
			}
			this.perform_swap(creature1, count, this.SelectedSlot);
		}

		private void ThreatContextMenu_Opening(object sender, CancelEventArgs e)
		{
			this.EditStatBlock.Enabled = (this.SelectedSlot != null || this.SelectedSlotTrap != null ? true : this.SelectedSlotSkillChallenge != null);
			this.EditSetFaction.Enabled = this.SelectedSlot != null;
			this.EditSetFaction.DropDownItems.Clear();
			foreach (EncounterSlotType value in Enum.GetValues(typeof(EncounterSlotType)))
			{
				ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(value.ToString())
				{
					Tag = value,
					Enabled = this.SelectedSlot != null,
					Checked = (this.SelectedSlot == null ? false : this.SelectedSlot.Type == value)
				};
				toolStripMenuItem.Click += new EventHandler(this.count_slot_as);
				this.EditSetFaction.DropDownItems.Add(toolStripMenuItem);
			}
			this.EditRemoveTemplate.Enabled = (this.SelectedSlot == null ? false : this.SelectedSlot.Card.TemplateIDs.Count != 0);
			this.EditRemoveLevelAdj.Enabled = (this.SelectedSlot == null ? false : this.SelectedSlot.Card.LevelAdjustment != 0);
			this.EditSwap.Enabled = this.SelectedSlot != null;
			this.EditSetWave.Enabled = this.SelectedSlot != null;
			this.EditSetWave.DropDownItems.Clear();
			ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem("Initial Wave")
			{
				Tag = this.fEncounter,
				Enabled = this.SelectedSlot != null,
				Checked = (this.SelectedSlot == null ? false : this.fEncounter.Slots.Contains(this.SelectedSlot))
			};
			toolStripMenuItem1.Click += new EventHandler(this.wave_initial);
			this.EditSetWave.DropDownItems.Add(toolStripMenuItem1);
			foreach (EncounterWave wafe in this.fEncounter.Waves)
			{
				ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(wafe.Name)
				{
					Tag = wafe,
					Enabled = this.SelectedSlot != null,
					Checked = (this.SelectedSlot == null ? false : this.fEncounter.FindWave(this.SelectedSlot) == wafe)
				};
				toolStripMenuItem2.Click += new EventHandler(this.wave_subsequent);
				this.EditSetWave.DropDownItems.Add(toolStripMenuItem2);
			}
			ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem("New Wave...")
			{
				Tag = null,
				Enabled = this.SelectedSlot != null,
				Checked = false
			};
			toolStripMenuItem3.Click += new EventHandler(this.wave_new);
			this.EditSetWave.DropDownItems.Add(toolStripMenuItem3);
			if (this.SelectedSlot != null)
			{
				ICreature creature = Session.FindCreature(this.SelectedSlot.Card.CreatureID, SearchType.Global);
				if (creature == null)
				{
					this.SwapStandard.Enabled = false;
					this.SwapElite.Enabled = false;
					this.SwapSolo.Enabled = false;
					this.SwapMinions.Enabled = false;
				}
				else if (!(creature.Role is Minion))
				{
					RoleFlag flag = this.SelectedSlot.Card.Flag;
					this.SwapStandard.Enabled = true;
					this.SwapElite.Enabled = (this.SelectedSlot.CombatData.Count < 2 ? false : this.SelectedSlot.CombatData.Count % 2 == 0);
					this.SwapSolo.Enabled = (this.SelectedSlot.CombatData.Count < 5 ? false : this.SelectedSlot.CombatData.Count % 5 == 0);
					this.SwapMinions.Enabled = true;
				}
				else
				{
					this.SwapStandard.Enabled = (this.SelectedSlot.CombatData.Count < 4 ? false : this.SelectedSlot.CombatData.Count % 4 == 0);
					this.SwapElite.Enabled = (this.SelectedSlot.CombatData.Count < 8 ? false : this.SelectedSlot.CombatData.Count % 8 == 0);
					this.SwapSolo.Enabled = (this.SelectedSlot.CombatData.Count < 20 ? false : this.SelectedSlot.CombatData.Count % 20 == 0);
					this.SwapMinions.Enabled = false;
				}
			}
			else
			{
				this.SwapStandard.Enabled = false;
				this.SwapElite.Enabled = false;
				this.SwapSolo.Enabled = false;
				this.SwapMinions.Enabled = false;
			}
			if (this.SelectedSlot == null)
			{
				this.EditApplyTheme.Enabled = false;
				this.EditClearTheme.Enabled = false;
				return;
			}
			this.EditApplyTheme.Enabled = this.SelectedSlot.Card != null;
			this.EditClearTheme.Enabled = (this.SelectedSlot.Card == null ? false : this.SelectedSlot.Card.ThemeID != Guid.Empty);
		}

		private void ThreatList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedCreature != null)
			{
				EncounterCard encounterCard = new EncounterCard()
				{
					CreatureID = this.SelectedCreature.ID
				};
				(new CreatureDetailsForm(encounterCard)).ShowDialog();
			}
			if (this.SelectedTemplate != null)
			{
				(new CreatureTemplateDetailsForm(this.SelectedTemplate)).ShowDialog();
			}
			if (this.SelectedTrap != null)
			{
				(new TrapDetailsForm(this.SelectedTrap)).ShowDialog();
			}
			if (this.SelectedSkillChallenge != null)
			{
				(new SkillChallengeDetailsForm(this.SelectedSkillChallenge)).ShowDialog();
			}
		}

		private void ToolsAddChallenge_Click(object sender, EventArgs e)
		{
			try
			{
				SkillChallenge skillChallenge = new SkillChallenge()
				{
					Name = "Custom Skill Challenge",
					Level = this.fPartyLevel
				};
				SkillChallengeBuilderForm skillChallengeBuilderForm = new SkillChallengeBuilderForm(skillChallenge);
				if (skillChallengeBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fEncounter.SkillChallenges.Add(skillChallengeBuilderForm.SkillChallenge);
					this.update_encounter();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ToolsAddCreature_Click(object sender, EventArgs e)
		{
			try
			{
				CustomCreature customCreature = new CustomCreature()
				{
					Name = "Custom Creature",
					Level = this.fPartyLevel
				};
				CreatureBuilderForm creatureBuilderForm = new CreatureBuilderForm(customCreature);
				if (creatureBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.CustomCreatures.Add(creatureBuilderForm.Creature as CustomCreature);
					Session.Modified = true;
					this.add_opponent(creatureBuilderForm.Creature);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ToolsAddTrap_Click(object sender, EventArgs e)
		{
			try
			{
				Trap trap = new Trap()
				{
					Name = "Custom Trap",
					Level = this.fPartyLevel
				};
				trap.Attacks.Add(new TrapAttack());
				TrapBuilderForm trapBuilderForm = new TrapBuilderForm(trap);
				if (trapBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fEncounter.Traps.Add(trapBuilderForm.Trap);
					this.update_encounter();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ToolsApplyTheme_Click(object sender, EventArgs e)
		{
			MonsterThemeSelectForm monsterThemeSelectForm = new MonsterThemeSelectForm();
			if (monsterThemeSelectForm.ShowDialog() == System.Windows.Forms.DialogResult.OK && monsterThemeSelectForm.MonsterTheme != null)
			{
				foreach (EncounterSlot slot in this.fEncounter.Slots)
				{
					slot.Card.ThemeID = monsterThemeSelectForm.MonsterTheme.ID;
					slot.Card.ThemeAttackPowerID = Guid.Empty;
					slot.Card.ThemeUtilityPowerID = Guid.Empty;
					List<ThemePowerData> themePowerDatas = monsterThemeSelectForm.MonsterTheme.ListPowers(slot.Card.Roles, PowerType.Attack);
					if (themePowerDatas.Count != 0)
					{
						int num = Session.Random.Next() % themePowerDatas.Count;
						ThemePowerData item = themePowerDatas[num];
						slot.Card.ThemeAttackPowerID = item.Power.ID;
					}
					List<ThemePowerData> themePowerDatas1 = monsterThemeSelectForm.MonsterTheme.ListPowers(slot.Card.Roles, PowerType.Utility);
					if (themePowerDatas1.Count == 0)
					{
						continue;
					}
					int num1 = Session.Random.Next() % themePowerDatas1.Count;
					ThemePowerData themePowerDatum = themePowerDatas1[num1];
					slot.Card.ThemeUtilityPowerID = themePowerDatum.Power.ID;
				}
				this.update_encounter();
				this.update_mapthreats();
			}
		}

		private void ToolsClearAll_Click(object sender, EventArgs e)
		{
			this.fEncounter.Slots.Clear();
			this.fEncounter.Traps.Clear();
			this.fEncounter.SkillChallenges.Clear();
			this.update_encounter();
			this.update_mapthreats();
		}

		private void ToolsExport_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog()
			{
				FileName = "Encounter",
				Filter = Program.EncounterFilter
			};
			if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				string str = EncounterExporter.ExportXML(this.fEncounter);
				File.WriteAllText(saveFileDialog.FileName, str);
			}
		}

		private void ToolsMenu_DropDownOpening(object sender, EventArgs e)
		{
			this.ToolsUseDeck.DropDownItems.Clear();
			foreach (EncounterDeck deck in Session.Project.Decks)
			{
				if (deck.Cards.Count == 0)
				{
					continue;
				}
				object[] name = new object[] { deck.Name, " (", deck.Cards.Count, " cards)" };
				ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(string.Concat(name))
				{
					Tag = deck
				};
				toolStripMenuItem.Click += new EventHandler(this.use_deck);
				this.ToolsUseDeck.DropDownItems.Add(toolStripMenuItem);
			}
			if (this.ToolsUseDeck.DropDownItems.Count == 0)
			{
				ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem("(no decks)")
				{
					ForeColor = SystemColors.GrayText
				};
				this.ToolsUseDeck.DropDownItems.Add(toolStripMenuItem1);
			}
		}

		private void ToolsUseTemplate_Click(object sender, EventArgs e)
		{
			List<Pair<EncounterTemplateGroup, EncounterTemplate>> pairs = EncounterBuilder.FindTemplates(this.fEncounter, this.fPartyLevel, true);
			if (pairs.Count != 0)
			{
				if ((new EncounterTemplateWizard(pairs, this.fEncounter, this.fPartyLevel)).Show() == System.Windows.Forms.DialogResult.OK)
				{
					this.update_encounter();
					this.update_mapthreats();
				}
				return;
			}
			string str = string.Concat("There are no encounter templates which match the creatures already in the encounter.", Environment.NewLine);
			str = string.Concat(str, "This does not mean there is a problem with your encounter.");
			MessageBox.Show(str, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		private void update_difficulty_list()
		{
			int creatureXP = this.fPartySize * (Experience.GetCreatureXP(this.fPartyLevel - 3) + Experience.GetCreatureXP(this.fPartyLevel - 2)) / 2;
			int num = this.fPartySize * (Experience.GetCreatureXP(this.fPartyLevel - 1) + Experience.GetCreatureXP(this.fPartyLevel)) / 2;
			int creatureXP1 = this.fPartySize * (Experience.GetCreatureXP(this.fPartyLevel + 1) + Experience.GetCreatureXP(this.fPartyLevel + 2)) / 2;
			int num1 = this.fPartySize * (Experience.GetCreatureXP(this.fPartyLevel + 4) + Experience.GetCreatureXP(this.fPartyLevel + 5)) / 2;
			creatureXP = Math.Max(1, creatureXP);
			num = Math.Max(1, num);
			creatureXP1 = Math.Max(1, creatureXP1);
			num1 = Math.Max(1, num1);
			this.DifficultyList.Items.Clear();
			ListViewItem listViewItem = this.DifficultyList.Items.Add("Easy");
			listViewItem.SubItems.Add(string.Concat(creatureXP, " - ", num));
			int num2 = Math.Max(1, this.fPartyLevel - 4);
			listViewItem.SubItems.Add(string.Concat(num2, " - ", this.fPartyLevel + 3));
			listViewItem.Tag = Difficulty.Easy;
			ListViewItem listViewItem1 = this.DifficultyList.Items.Add("Moderate");
			listViewItem1.SubItems.Add(string.Concat(num, " - ", creatureXP1));
			int num3 = Math.Max(1, this.fPartyLevel - 3);
			listViewItem1.SubItems.Add(string.Concat(num3, " - ", this.fPartyLevel + 3));
			listViewItem1.Tag = Difficulty.Moderate;
			ListViewItem listViewItem2 = this.DifficultyList.Items.Add("Hard");
			listViewItem2.SubItems.Add(string.Concat(creatureXP1, " - ", num1));
			int num4 = Math.Max(1, this.fPartyLevel - 3);
			listViewItem2.SubItems.Add(string.Concat(num4, " - ", this.fPartyLevel + 5));
			listViewItem2.Tag = Difficulty.Hard;
			this.XPGauge.Party = new Party(this.fPartySize, this.fPartyLevel);
		}

		private void update_encounter()
		{
			this.SlotList.BeginUpdate();
			ListState state = ListState.GetState(this.SlotList);
			this.SlotList.Groups.Clear();
			this.SlotList.Items.Clear();
			this.SlotList.ShowGroups = (this.fEncounter.Count != 0 || this.fEncounter.Traps.Count != 0 ? true : this.fEncounter.SkillChallenges.Count != 0);
			if (this.fEncounter.Count == 0)
			{
				this.SlotList.Groups.Add("Creatures", "Creatures");
				ListViewItem grayText = this.SlotList.Items.Add("(none)");
				grayText.ForeColor = SystemColors.GrayText;
				grayText.Group = this.SlotList.Groups["Creatures"];
			}
			else
			{
				foreach (EncounterSlot allSlot in this.fEncounter.AllSlots)
				{
					allSlot.SetDefaultDisplayNames();
				}
				this.SlotList.Groups.Add("Combatants", "Combatants");
				foreach (EncounterWave wafe in this.fEncounter.Waves)
				{
					this.SlotList.Groups.Add(wafe.Name, wafe.Name);
				}
				foreach (EncounterSlot encounterSlot in this.fEncounter.AllSlots)
				{
					string title = encounterSlot.Card.Title;
					ICreature creature = Session.FindCreature(encounterSlot.Card.CreatureID, SearchType.Global);
					ListViewItem item = this.SlotList.Items.Add(title);
					item.SubItems.Add(encounterSlot.Card.Info);
					ListViewItem.ListViewSubItemCollection subItems = item.SubItems;
					int count = encounterSlot.CombatData.Count;
					subItems.Add(count.ToString());
					item.SubItems.Add(encounterSlot.XP.ToString());
					item.Tag = encounterSlot;
					if (creature != null)
					{
						EncounterWave encounterWave = this.fEncounter.FindWave(encounterSlot);
						item.Group = this.SlotList.Groups[(encounterWave == null ? "Combatants" : encounterWave.Name)];
					}
					if (creature != null)
					{
						Difficulty threatDifficulty = AI.GetThreatDifficulty(creature.Level + encounterSlot.Card.LevelAdjustment, this.fPartyLevel);
						if (threatDifficulty == Difficulty.Trivial)
						{
							item.ForeColor = Color.Green;
						}
						if (threatDifficulty != Difficulty.Extreme)
						{
							continue;
						}
						item.ForeColor = Color.Red;
					}
					else
					{
						item.ForeColor = Color.Red;
					}
				}
			}
			if (this.fEncounter.Traps.Count != 0)
			{
				this.SlotList.Groups.Add("Traps / Hazards", "Traps / Hazards");
				foreach (Trap trap in this.fEncounter.Traps)
				{
					ListViewItem green = this.SlotList.Items.Add(trap.Name);
					green.SubItems.Add(trap.Info);
					green.SubItems.Add("");
					green.SubItems.Add(trap.XP.ToString());
					green.Tag = trap;
					green.Group = this.SlotList.Groups["Traps / Hazards"];
					Difficulty difficulty = AI.GetThreatDifficulty(trap.Level, this.fPartyLevel);
					if (difficulty == Difficulty.Trivial)
					{
						green.ForeColor = Color.Green;
					}
					if (difficulty != Difficulty.Extreme)
					{
						continue;
					}
					green.ForeColor = Color.Red;
				}
			}
			if (this.fEncounter.SkillChallenges.Count != 0)
			{
				this.SlotList.Groups.Add("Skill Challenges", "Skill Challenges");
				foreach (SkillChallenge skillChallenge in this.fEncounter.SkillChallenges)
				{
					ListViewItem red = this.SlotList.Items.Add(skillChallenge.Name);
					red.SubItems.Add(skillChallenge.Info);
					red.SubItems.Add("");
					ListViewItem.ListViewSubItemCollection listViewSubItemCollections = red.SubItems;
					int xP = skillChallenge.GetXP();
					listViewSubItemCollections.Add(xP.ToString());
					red.Tag = skillChallenge;
					red.Group = this.SlotList.Groups["Skill Challenges"];
					Difficulty difficulty1 = skillChallenge.GetDifficulty(this.fPartyLevel, this.fPartySize);
					if (difficulty1 == Difficulty.Trivial)
					{
						red.ForeColor = Color.Green;
					}
					if (difficulty1 != Difficulty.Extreme)
					{
						continue;
					}
					red.ForeColor = Color.Red;
				}
			}
			ListState.SetState(this.SlotList, state);
			this.SlotList.EndUpdate();
			Difficulty difficulty2 = this.fEncounter.GetDifficulty(this.fPartyLevel, this.fPartySize);
			foreach (ListViewItem listViewItem in this.DifficultyList.Items)
			{
				Difficulty tag = (Difficulty)listViewItem.Tag;
				listViewItem.BackColor = (difficulty2 == tag ? Color.Gray : SystemColors.Window);
				listViewItem.Font = (difficulty2 == tag ? new System.Drawing.Font(this.Font, this.Font.Style | FontStyle.Bold) : this.Font);
			}
			int num = this.fEncounter.GetXP();
			this.XPGauge.XP = num;
			this.XPLbl.Text = string.Concat("XP: ", num);
			int creatureLevel = Experience.GetCreatureLevel(num / this.fPartySize);
			this.LevelLbl.Text = string.Concat("Level: ", Math.Max(creatureLevel, 1));
			this.DiffLbl.Text = string.Concat("Difficulty: ", this.fEncounter.GetDifficulty(this.fPartyLevel, this.fPartySize));
			this.CountLbl.Text = string.Concat("Opponents: ", this.fEncounter.Count);
		}

		private void update_mapthreats()
		{
			this.MapThreatList.Items.Clear();
			this.MapThreatList.Groups.Clear();
			this.SlotList.Groups.Add("Combatants", "Combatants");
			foreach (EncounterWave wafe in this.fEncounter.Waves)
			{
				this.SlotList.Groups.Add(wafe.Name, wafe.Name);
			}
			this.SlotList.Groups.Add("Custom Tokens / Overlays", "Custom Tokens / Overlays");
			foreach (EncounterSlot allSlot in this.fEncounter.AllSlots)
			{
				foreach (CombatData combatDatum in allSlot.CombatData)
				{
					if (combatDatum.Location != CombatData.NoPoint)
					{
						continue;
					}
					ListViewItem creatureToken = this.MapThreatList.Items.Add(combatDatum.DisplayName);
					creatureToken.Tag = new CreatureToken(allSlot.ID, combatDatum);
					EncounterWave encounterWave = this.fEncounter.FindWave(allSlot);
					creatureToken.Group = this.MapThreatList.Groups[(encounterWave == null ? "Combatants" : encounterWave.Name)];
				}
			}
			foreach (CustomToken customToken in this.fEncounter.CustomTokens)
			{
				if (customToken.Data.Location != CombatData.NoPoint)
				{
					continue;
				}
				ListViewItem item = this.MapThreatList.Items.Add(customToken.Name);
				item.Tag = customToken;
				item.Group = this.MapThreatList.Groups["Custom Tokens / Overlays"];
			}
			if (this.MapThreatList.Items.Count == 0)
			{
				this.MapView.Caption = "";
				return;
			}
			this.MapView.Caption = "Drag creatures from the list to place them on the map";
		}

		private void update_notes()
		{
			try
			{
				EncounterNote selectedNote = this.SelectedNote;
				this.NoteList.Items.Clear();
				foreach (EncounterNote note in this.fEncounter.Notes)
				{
					ListViewItem grayText = this.NoteList.Items.Add(note.Title);
					grayText.Tag = note;
					if (note.Contents == "")
					{
						grayText.ForeColor = SystemColors.GrayText;
					}
					if (note != selectedNote)
					{
						continue;
					}
					grayText.Selected = true;
				}
				if (this.NoteList.Items.Count == 0)
				{
					ListViewItem listViewItem = this.NoteList.Items.Add("(no notes)");
					listViewItem.ForeColor = SystemColors.GrayText;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void update_party_label()
		{
			this.PartyLbl.Text = string.Concat(this.fPartySize, " PCs at level ", this.fPartyLevel);
		}

		private void update_selected_note()
		{
			try
			{
				this.NoteDetails.Document.OpenNew(true);
				this.NoteDetails.Document.Write(HTML.EncounterNote(this.SelectedNote, DisplaySize.Small));
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void update_source_list()
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			this.SourceItemList.BeginUpdate();
			try
			{
				this.SourceItemList.Items.Clear();
				this.SourceItemList.Groups.Clear();
				this.SourceItemList.ShowGroups = true;
				switch (this.fMode)
				{
					case ListMode.Creatures:
					{
						List<Creature> creatures = Session.Creatures;
						BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
						foreach (Creature creature in creatures)
						{
							if (creature.Category == null || !(creature.Category != ""))
							{
								continue;
							}
							binarySearchTree.Add(creature.Category);
						}
						List<string> sortedList = binarySearchTree.SortedList;
						sortedList.Insert(0, "Custom Creatures");
						sortedList.Add("Miscellaneous Creatures");
						foreach (string str in sortedList)
						{
							this.SourceItemList.Groups.Add(str, str);
						}
						List<ListViewItem> listViewItems = new List<ListViewItem>();
						foreach (CustomCreature customCreature in Session.Project.CustomCreatures)
						{
							ListViewItem listViewItem = this.add_creature_to_list(customCreature);
							if (listViewItem == null)
							{
								continue;
							}
							listViewItems.Add(listViewItem);
						}
						foreach (Creature creature1 in creatures)
						{
							ListViewItem listViewItem1 = this.add_creature_to_list(creature1);
							if (listViewItem1 == null)
							{
								continue;
							}
							listViewItems.Add(listViewItem1);
						}
						this.SourceItemList.Items.AddRange(listViewItems.ToArray());
						if (this.SourceItemList.Items.Count != 0)
						{
							break;
						}
						this.SourceItemList.ShowGroups = false;
						ListViewItem grayText = this.SourceItemList.Items.Add("(no creatures)");
						grayText.ForeColor = SystemColors.GrayText;
						break;
					}
					case ListMode.Templates:
					{
						List<CreatureTemplate> templates = Session.Templates;
						ListViewGroup listViewGroup = this.SourceItemList.Groups.Add("Functional Templates", "Functional Templates");
						ListViewGroup listViewGroup1 = this.SourceItemList.Groups.Add("Class Templates", "Class Templates");
						List<ListViewItem> listViewItems1 = new List<ListViewItem>();
						foreach (CreatureTemplate template in templates)
						{
							ListViewItem listViewItem2 = this.add_template_to_list(template, (template.Type == CreatureTemplateType.Functional ? listViewGroup : listViewGroup1));
							if (listViewItem2 == null)
							{
								continue;
							}
							listViewItems1.Add(listViewItem2);
						}
						this.SourceItemList.Items.AddRange(listViewItems1.ToArray());
						if (this.SourceItemList.Items.Count != 0)
						{
							break;
						}
						this.SourceItemList.ShowGroups = false;
						ListViewItem grayText1 = this.SourceItemList.Items.Add("(no templates)");
						grayText1.ForeColor = SystemColors.GrayText;
						break;
					}
					case ListMode.NPCs:
					{
						ListViewGroup listViewGroup2 = this.SourceItemList.Groups.Add("NPCs", "NPCs");
						List<ListViewItem> listViewItems2 = new List<ListViewItem>();
						foreach (NPC nPC in Session.Project.NPCs)
						{
							ListViewItem listViewItem3 = this.add_npc_to_list(nPC, listViewGroup2);
							if (listViewItem3 == null)
							{
								continue;
							}
							listViewItems2.Add(listViewItem3);
						}
						this.SourceItemList.Items.AddRange(listViewItems2.ToArray());
						if (this.SourceItemList.Items.Count != 0)
						{
							break;
						}
						this.SourceItemList.ShowGroups = false;
						ListViewItem grayText2 = this.SourceItemList.Items.Add("(no npcs)");
						grayText2.ForeColor = SystemColors.GrayText;
						break;
					}
					case ListMode.Traps:
					{
						List<Trap> traps = Session.Traps;
						ListViewGroup listViewGroup3 = this.SourceItemList.Groups.Add("Traps", "Traps");
						ListViewGroup listViewGroup4 = this.SourceItemList.Groups.Add("Hazards", "Hazards");
						ListViewGroup listViewGroup5 = this.SourceItemList.Groups.Add("Terrain", "Terrain");
						List<ListViewItem> listViewItems3 = new List<ListViewItem>();
						foreach (Trap trap in traps)
						{
							ListViewGroup listViewGroup6 = null;
							switch (trap.Type)
							{
								case TrapType.Trap:
								{
									listViewGroup6 = listViewGroup3;
									break;
								}
								case TrapType.Hazard:
								{
									listViewGroup6 = listViewGroup4;
									break;
								}
								case TrapType.Terrain:
								{
									listViewGroup6 = listViewGroup5;
									break;
								}
							}
							ListViewItem listViewItem4 = this.add_trap_to_list(trap, listViewGroup6);
							if (listViewItem4 == null)
							{
								continue;
							}
							listViewItems3.Add(listViewItem4);
						}
						this.SourceItemList.Items.AddRange(listViewItems3.ToArray());
						if (this.SourceItemList.Items.Count != 0)
						{
							break;
						}
						this.SourceItemList.ShowGroups = false;
						ListViewItem grayText3 = this.SourceItemList.Items.Add("(no traps)");
						grayText3.ForeColor = SystemColors.GrayText;
						break;
					}
					case ListMode.SkillChallenges:
					{
						List<SkillChallenge> skillChallenges = Session.SkillChallenges;
						List<ListViewItem> listViewItems4 = new List<ListViewItem>();
						foreach (SkillChallenge skillChallenge in skillChallenges)
						{
							ListViewItem listViewItem5 = this.add_challenge_to_list(skillChallenge);
							if (listViewItem5 == null)
							{
								continue;
							}
							listViewItems4.Add(listViewItem5);
						}
						this.SourceItemList.Items.AddRange(listViewItems4.ToArray());
						if (this.SourceItemList.Items.Count != 0)
						{
							break;
						}
						this.SourceItemList.ShowGroups = false;
						ListViewItem grayText4 = this.SourceItemList.Items.Add("(no skill challenges)");
						grayText4.ForeColor = SystemColors.GrayText;
						break;
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
			this.SourceItemList.EndUpdate();
			System.Windows.Forms.Cursor.Current = Cursors.Default;
		}

		private void use_deck(object sender, EventArgs e)
		{
			EncounterDeck tag = (sender as ToolStripMenuItem).Tag as EncounterDeck;
			tag.DrawEncounter(this.fEncounter);
			if (tag.Cards.Count == 0)
			{
				Session.Project.Decks.Remove(tag);
			}
			this.update_encounter();
			this.update_mapthreats();
		}

		private void ViewChallenges_Click(object sender, EventArgs e)
		{
			this.fMode = ListMode.SkillChallenges;
			this.FilterPanel.Mode = ListMode.SkillChallenges;
			this.update_source_list();
		}

		private void ViewCreatures_Click(object sender, EventArgs e)
		{
			this.fMode = ListMode.Creatures;
			this.FilterPanel.Mode = ListMode.Creatures;
			this.update_source_list();
		}

		private void ViewGroups_Click(object sender, EventArgs e)
		{
			this.SourceItemList.ShowGroups = !this.SourceItemList.ShowGroups;
		}

		private void ViewMenu_DropDownOpening(object sender, EventArgs e)
		{
			this.ViewTemplates.Enabled = Session.Templates.Count != 0;
			this.ViewNPCs.Enabled = Session.Project.NPCs.Count != 0;
			this.ViewNPCs.Checked = this.fMode == ListMode.NPCs;
			this.ViewTemplates.Checked = this.fMode == ListMode.Templates;
			this.ViewGroups.Checked = this.SourceItemList.ShowGroups;
		}

		private void ViewNPCs_Click(object sender, EventArgs e)
		{
			this.fMode = ListMode.NPCs;
			this.FilterPanel.Mode = ListMode.NPCs;
			this.update_source_list();
		}

		private void ViewTemplates_Click(object sender, EventArgs e)
		{
			this.fMode = ListMode.Templates;
			this.FilterPanel.Mode = ListMode.Templates;
			this.update_source_list();
		}

		private void ViewTraps_Click(object sender, EventArgs e)
		{
			this.fMode = ListMode.Traps;
			this.FilterPanel.Mode = ListMode.Traps;
			this.update_source_list();
		}

		private void wave_initial(object sender, EventArgs e)
		{
			EncounterWave encounterWave = this.fEncounter.FindWave(this.SelectedSlot);
			if (encounterWave != null)
			{
				encounterWave.Slots.Remove(this.SelectedSlot);
				this.fEncounter.Slots.Add(this.SelectedSlot);
			}
			this.update_encounter();
			this.update_mapthreats();
		}

		private void wave_new(object sender, EventArgs e)
		{
			EncounterWave encounterWave = new EncounterWave()
			{
				Name = string.Concat("Wave ", this.fEncounter.Waves.Count + 2)
			};
			this.fEncounter.Waves.Add(encounterWave);
			EncounterWave encounterWave1 = this.fEncounter.FindWave(this.SelectedSlot);
			if (encounterWave1 != null)
			{
				encounterWave1.Slots.Remove(this.SelectedSlot);
			}
			else
			{
				this.fEncounter.Slots.Remove(this.SelectedSlot);
			}
			encounterWave.Slots.Add(this.SelectedSlot);
			this.update_encounter();
			this.update_mapthreats();
		}

		private void wave_subsequent(object sender, EventArgs e)
		{
			EncounterWave tag = (sender as ToolStripMenuItem).Tag as EncounterWave;
			if (tag != null)
			{
				EncounterWave encounterWave = this.fEncounter.FindWave(this.SelectedSlot);
				if (encounterWave != null)
				{
					encounterWave.Slots.Remove(this.SelectedSlot);
				}
				else
				{
					this.fEncounter.Slots.Remove(this.SelectedSlot);
				}
				tag.Slots.Add(this.SelectedSlot);
			}
			this.update_encounter();
			this.update_mapthreats();
		}

		private class SourceSorter : IComparer
		{
			private bool fAscending;

			private int fColumn;

			public SourceSorter()
			{
			}

			public int Compare(object x, object y)
			{
				ListViewItem listViewItem = x as ListViewItem;
				ListViewItem listViewItem1 = y as ListViewItem;
				int num = 0;
				if (this.fColumn == 1)
				{
					if (listViewItem.Tag is ICreature)
					{
						ICreature tag = listViewItem.Tag as ICreature;
						ICreature creature = listViewItem1.Tag as ICreature;
						num = tag.Level.CompareTo(creature.Level);
					}
					if (listViewItem.Tag is Trap)
					{
						Trap trap = listViewItem.Tag as Trap;
						Trap tag1 = listViewItem1.Tag as Trap;
						num = trap.Level.CompareTo(tag1.Level);
					}
				}
				if (num == 0)
				{
					ListViewItem.ListViewSubItem item = listViewItem.SubItems[this.fColumn];
					ListViewItem.ListViewSubItem listViewSubItem = listViewItem1.SubItems[this.fColumn];
					num = item.Text.CompareTo(listViewSubItem.Text);
				}
				if (!this.fAscending)
				{
					num *= -1;
				}
				return num;
			}

			public void Set(int column)
			{
				if (this.fColumn == column)
				{
					this.fAscending = !this.fAscending;
				}
				this.fColumn = column;
			}
		}
	}
}