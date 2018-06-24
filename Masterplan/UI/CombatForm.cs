using Masterplan;
using Masterplan.Controls;
using Masterplan.Commands;
using Masterplan.Commands.Combat;
using Masterplan.Data;
using Masterplan.Data.Combat;
using Masterplan.Events;
using Masterplan.Extensibility;
using Masterplan.Properties;
using Masterplan.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Resources;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Utils;

using Microsoft.VisualStudio.Profiler;

namespace Masterplan.UI
{
	internal class CombatForm : Form
	{
        private CombatState combatState;

		private Encounter fEncounter;

		private int fPartyLevel = Session.Project.Party.Level;

		private bool fCombatStarted;

		private CombatData fCurrentActor;

		private int fCurrentRound = 1;

		private int fRemovedCreatureXP;

		private List<OngoingCondition> fEffects = new List<OngoingCondition>();

		private EncounterLog fLog = new EncounterLog();

		private bool fUpdatingList;

		private bool fPromptOnClose = true;

		private StringFormat fLeft = new StringFormat();

		private StringFormat fRight = new StringFormat();

		private IContainer components;

		private ToolStrip Toolbar;

		private SplitContainer MapSplitter;

		private CombatForm.CombatListControl CombatList;

		private ColumnHeader NameHdr;

		private ColumnHeader InitHdr;

		private ColumnHeader HPHdr;

		private ToolTip MapTooltip;

		private ToolStripButton DetailsBtn;

		private ToolStripSeparator toolStripSeparator1;

		private SplitContainer ListSplitter;

		private StatusStrip Statusbar;

		private ToolStripStatusLabel XPLbl;

		private System.Windows.Forms.ContextMenuStrip MapContext;

		private ToolStripMenuItem MapDetails;

		private ToolStripMenuItem MapVisible;

		private ToolStripSeparator toolStripMenuItem1;

		private ToolStripButton DamageBtn;

		private ToolStripMenuItem MapDamage;

		private ToolStripSeparator toolStripSeparator2;

		private System.Windows.Forms.ContextMenuStrip ListContext;

		private ToolStripMenuItem ListDetails;

		private ToolStripMenuItem ListDamage;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem ListVisible;

		public Masterplan.Controls.MapView MapView;

		private ToolStripDropDownButton CombatantsBtn;

		private ToolStripMenuItem CombatantsAdd;

		private ToolStripMenuItem CombatantsRemove;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripMenuItem CombatantsAddToken;

		private TrackBar ZoomGauge;

		private ToolStripDropDownButton MapMenu;

		private ToolStripMenuItem MapReset;

		private ToolStripMenuItem MapNavigate;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripMenuItem MapExport;

		private WebBrowser Preview;

		private Panel PreviewPanel;

        private ToolStripButton UndoBtn;
        private ToolStripButton RedoBtn;

        private ToolStripButton NextInitBtn;
        private ToolStripButton PrevInitBtn;

		private ToolStripMenuItem ShowMap;

		private ToolStripSeparator toolStripSeparator8;

		private ToolStripDropDownButton PlayerViewMapMenu;

		private ToolStripMenuItem PlayerViewMap;

		private ToolStripMenuItem PlayerLabels;
        private ToolStripMenuItem PlayerLabelsRequireKnowledge;

        private ToolStripMenuItem PlayerViewShowVisibility;
        private ToolStripMenuItem PlayerViewUseDarkScheme;

        private ToolStripMenuItem MapFog;

		private ToolStripMenuItem MapFogAllCreatures;

		private ToolStripMenuItem MapFogVisibleCreatures;

		private ToolStripMenuItem MapFogHideCreatures;

		private ToolStripSeparator toolStripSeparator10;

		private ToolStripSeparator toolStripSeparator9;

		private ToolStripMenuItem PlayerViewFog;

		private ToolStripMenuItem PlayerFogAll;

		private ToolStripMenuItem PlayerFogVisible;

		private ToolStripMenuItem PlayerFogNone;

		private ToolStripMenuItem MapGrid;

		private ToolStripMenuItem PlayerViewGrid;

		private ToolStripMenuItem MapPrint;

		private ToolStripMenuItem MapLOS;

		private ToolStripMenuItem PlayerViewLOS;

		private ToolStripSeparator toolStripSeparator12;

		private ToolStripMenuItem CombatantsHideAll;

		private ToolStripMenuItem CombatantsShowAll;

		private ToolStripDropDownButton OptionsMenu;

		private ToolStripSeparator toolStripSeparator13;

		private ToolStripMenuItem OneColumn;

		private ToolStripMenuItem TwoColumns;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripMenuItem ToolsAutoRemove;

		private ToolStripStatusLabel LevelLbl;

		private ToolStripMenuItem CombatantsAddOverlay;

		private ToolStripSeparator toolStripSeparator14;

		private ToolStripSeparator toolStripMenuItem2;

		private ToolStripMenuItem PlayerHealth;

		private ToolStripMenuItem MapHealth;

		private ToolStripSeparator toolStripSeparator15;

		private ToolStripSeparator toolStripSeparator16;

		private ToolStripSeparator toolStripSeparator17;

		private Panel MainPanel;

		private Button CloseBtn;

		private ToolStripButton DelayBtn;

		private ToolStripSeparator toolStripSeparator18;

		private ToolStripMenuItem ListDelay;

		private ToolStripMenuItem MapDelay;

		private ToolStripStatusLabel RoundLbl;

		private ToolStripSeparator toolStripSeparator20;

		private ToolStripMenuItem MapRight;

		private ToolStripMenuItem MapBelow;

		private ToolStripSeparator toolStripSeparator21;

		private ToolStripMenuItem OptionsLandscape;

		private ToolStripMenuItem OptionsPortrait;

		private ToolStripMenuItem MapDrawing;

		private ToolStripSeparator toolStripSeparator19;

		private ToolStripMenuItem MapClearDrawings;

		private Button PauseBtn;

		private ToolStripDropDownButton EffectMenu;

		private ToolStripMenuItem ListCondition;

		private ToolStripMenuItem MapAddEffect;

		private ToolStripMenuItem effectToolStripMenuItem;

		private ToolStripMenuItem effectToolStripMenuItem1;

		private ToolStripMenuItem effectToolStripMenuItem2;

		private ToolStripMenuItem OptionsIPlay4e;

		private ToolStripMenuItem ListRemoveEffect;

		private ToolStripMenuItem effectToolStripMenuItem3;

		private ToolStripMenuItem MapRemoveEffect;

		private ToolStripMenuItem effectToolStripMenuItem4;

		private ToolStripSeparator toolStripSeparator22;

		private ToolStripMenuItem MapContextDrawing;

		private ToolStripMenuItem MapContextClearDrawings;

		private ToolStripSeparator toolStripSeparator24;

		private ToolStripMenuItem MapContextOverlay;

		private ToolStripMenuItem MapGridLabels;

		private ToolStripMenuItem PlayerViewGridLabels;

		private ToolStripMenuItem ListHeal;

		private ToolStripMenuItem MapHeal;

		private ToolStripButton HealBtn;

		private ToolStripMenuItem MapPictureTokens;

		private ToolStripMenuItem PlayerPictureTokens;

		private ToolStripDropDownButton ToolsMenu;

		private ToolStripMenuItem ToolsEffects;

		private ToolStripMenuItem ToolsLinks;

		private ToolStripSeparator toolStripSeparator11;

		private ToolStripMenuItem ToolsAddIns;

		private ToolStripMenuItem addinsToolStripMenuItem;

		private ToolStripMenuItem ListCreateCopy;

		private ToolStripMenuItem MapCreateCopy;

		private ToolStripMenuItem PlayerViewInitList;

		private ToolStripMenuItem MapSetPicture;

		private ToolStripDropDownButton PlayerViewNoMapMenu;

		private ToolStripMenuItem PlayerViewNoMapShowInitiativeList;

		private ToolStripMenuItem MapConditions;

		private ToolStripMenuItem PlayerConditions;

		private TabControl Pages;

		private TabPage CombatantsPage;

		private TabPage TemplatesPage;

		private ListView TemplateList;

		private ColumnHeader TemplateHdr;

		private Button InfoBtn;

		private Masterplan.Controls.InitiativePanel InitiativePanel;

		private ToolStripMenuItem OptionsShowInit;

		private ToolStripMenuItem PlayerViewNoMapShowLabels;

		private ColumnHeader DefHdr;

		private ToolStripMenuItem ListRemove;

		private ToolStripMenuItem MapRemove;

		private ToolStripMenuItem ListRemoveMap;

		private ToolStripMenuItem ListRemoveCombat;

		private ToolStripMenuItem MapRemoveMap;

		private ToolStripMenuItem MapRemoveCombat;

		private Button DieRollerBtn;

		private ColumnHeader EffectsHdr;

		private ToolStripSeparator toolStripSeparator23;

		private ToolStripMenuItem ToolsColumns;

		private ToolStripMenuItem ToolsColumnsInit;

		private ToolStripMenuItem ToolsColumnsHP;

		private ToolStripMenuItem ToolsColumnsDefences;

		private ToolStripMenuItem ToolsColumnsConditions;

		private TabPage LogPage;

		private WebBrowser LogBrowser;

		private ToolStripSeparator toolStripSeparator25;

		private ToolStripMenuItem MapContextLOS;

		private Button ReportBtn;

		private ToolStripSeparator toolStripSeparator26;

		private ToolStripMenuItem CombatantsWaves;

        // HACK!
        public static bool TerrainLayersNeedRefresh = false;

		public WebBrowser PlayerInitiative
		{
			get
			{
				WebBrowser webBrowser;
				if (Session.PlayerView == null)
				{
					return null;
				}
				if (Session.PlayerView.Controls.Count == 0)
				{
					return null;
				}
				SplitContainer item = Session.PlayerView.Controls[0] as SplitContainer;
				if (item == null)
				{
					return null;
				}
				if (item.Panel2Collapsed)
				{
					return null;
				}
				if (item.Panel2.Controls.Count == 0)
				{
					return null;
				}
				IEnumerator enumerator = item.Panel2.Controls.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						WebBrowser current = (Control)enumerator.Current as WebBrowser;
						if (current == null)
						{
							continue;
						}
						webBrowser = current;
						return webBrowser;
					}
					return null;
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
		}

		public Masterplan.Controls.MapView PlayerMap
		{
			get
			{
				if (Session.PlayerView == null)
				{
					return null;
				}
				if (Session.PlayerView.Controls.Count == 0)
				{
					return null;
				}
				SplitContainer item = Session.PlayerView.Controls[0] as SplitContainer;
				if (item == null)
				{
					return null;
				}
				if (item.Panel1Collapsed)
				{
					return null;
				}
				if (item.Panel1.Controls.Count == 0)
				{
					return null;
				}
				return item.Panel1.Controls[0] as Masterplan.Controls.MapView;
			}
		}

		public SkillChallenge SelectedChallenge
		{
			get
			{
				if (this.CombatList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.CombatList.SelectedItems[0].Tag as SkillChallenge;
			}
		}

		public List<IToken> SelectedTokens
		{
			get
			{
				List<IToken> tokens = new List<IToken>();
				foreach (ListViewItem selectedItem in this.CombatList.SelectedItems)
				{
					IToken tag = selectedItem.Tag as IToken;
					if (tag == null)
					{
						continue;
					}
					tokens.Add(tag);
				}
				return tokens;
			}
		}

		public Trap SelectedTrap
		{
			get
			{
				if (this.CombatList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.CombatList.SelectedItems[0].Tag as Trap;
			}
		}

		public bool TwoColumnPreview
		{
			get
			{
				if (this.fCurrentActor == null)
				{
					return false;
				}
				return this.Preview.Width > 630;
			}
		}

		public CombatForm(CombatState cs)
		{
			this.InitializeComponent();
			this.Preview.DocumentText = "";
			this.LogBrowser.DocumentText = "";
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fLeft.Alignment = StringAlignment.Near;
			this.fLeft.LineAlignment = StringAlignment.Center;
			this.fRight.Alignment = StringAlignment.Far;
			this.fRight.LineAlignment = StringAlignment.Center;
            this.combatState = cs;
			this.fEncounter = cs.Encounter.Copy() as Encounter;
			this.fPartyLevel = cs.PartyLevel;
			this.fRemovedCreatureXP = cs.RemovedCreatureXP;
			this.fCurrentRound = cs.CurrentRound;
			this.RoundLbl.Text = string.Concat("Round ", this.fCurrentRound);
			if (cs.QuickEffects != null)
			{
				foreach (OngoingCondition quickEffect in cs.QuickEffects)
				{
					this.add_quick_effect(quickEffect);
				}
			}
			if (cs.HeroData == null)
			{
				foreach (Hero hero in Session.Project.Heroes)
				{
					hero.CombatData.Location = CombatData.NoPoint;
				}
			}
			else
			{
				foreach (Hero item in Session.Project.Heroes)
				{
					if (!cs.HeroData.ContainsKey(item.ID))
					{
						continue;
					}
					item.CombatData = cs.HeroData[item.ID];
				}
			}
			foreach (Hero d in Session.Project.Heroes)
			{
				d.CombatData.ID = d.ID;
				d.CombatData.DisplayName = d.Name;
			}
			foreach (Trap trap in this.fEncounter.Traps)
			{
				if (trap.CombatData != null)
				{
					continue;
				}
                trap.CombatData = new CombatData() { DisplayName = trap.Name, ID = trap.ID };
			}
			if (this.fEncounter.MapID == Guid.Empty)
			{
				this.Pages.TabPages.Remove(this.TemplatesPage);
			}
			else
			{
				foreach (Hero hero1 in Session.Project.Heroes)
				{
					foreach (CustomToken token in hero1.Tokens)
					{
						string str = string.Concat(hero1.Name, ": ", token.Name);
						ListViewItem listViewItem = this.TemplateList.Items.Add(str);
						listViewItem.Tag = token;
						listViewItem.Group = this.TemplateList.Groups[0];
					}
				}
				foreach (CreatureSize value in Enum.GetValues(typeof(CreatureSize)))
				{
					CustomToken customToken = new CustomToken()
					{
						Type = CustomTokenType.Token,
						TokenSize = value,
						Colour = Color.Black,
						Name = string.Concat(value, " Token")
					};
					ListViewItem item1 = this.TemplateList.Items.Add(customToken.Name);
					item1.Tag = customToken;
					item1.Group = this.TemplateList.Groups[1];
				}
				for (int i = 2; i <= 10; i++)
				{
					CustomToken transparent = new CustomToken()
					{
						Type = CustomTokenType.Overlay,
						OverlaySize = new System.Drawing.Size(i, i)
					};
					object[] objArray = new object[] { i, " x ", i, " Zone" };
					transparent.Name = string.Concat(objArray);
					transparent.Colour = Color.Transparent;
					ListViewItem listViewItem1 = this.TemplateList.Items.Add(transparent.Name);
					listViewItem1.Tag = transparent;
					listViewItem1.Group = this.TemplateList.Groups[2];
				}
			}
			this.fLog = cs.Log;
			this.fLog.Active = false;
			if (this.fLog.Entries.Count != 0)
			{
				this.fLog.Active = true;
				this.fLog.AddResumeEntry();
			}
			this.update_log();
			if (cs.CurrentActor != Guid.Empty)
			{
				this.fCombatStarted = true;
				Hero hero2 = Session.Project.FindHero(cs.CurrentActor);
                if (hero2 != null)
                {
                    this.fCurrentActor = hero2.CombatData;
                }
                else
                {
                    Trap trap = this.fEncounter.FindTrap(cs.CurrentActor);
                    if (trap != null)
                    {
                        this.fCurrentActor = trap.CombatData;
                    }
                    else
                    {
                        CombatData combatDatum1 = this.fEncounter.FindCombatData(cs.CurrentActor);
                        if (combatDatum1 != null)
                        {
                            this.fCurrentActor = combatDatum1;
                        }
                    }
				}
			}

			this.CombatList.ListViewItemSorter = new CombatForm.InitiativeSorter(this.fEncounter);
			this.set_map(cs.TokenLinks, cs.Viewpoint, cs.Sketches);
			this.MapMenu.Visible = this.fEncounter.MapID != Guid.Empty;
			this.InitiativePanel.InitiativeScores = this.get_initiatives();
			this.InitiativePanel.CurrentInitiative = this.InitiativePanel.Maximum;
			this.PlayerViewMapMenu.Visible = this.fEncounter.MapID != Guid.Empty;
			this.PlayerViewNoMapMenu.Visible = this.fEncounter.MapID == Guid.Empty;
			if (!Session.Preferences.CombatColumnInitiative)
			{
				this.InitHdr.Width = 0;
			}
			if (!Session.Preferences.CombatColumnHP)
			{
				this.HPHdr.Width = 0;
			}
			if (!Session.Preferences.CombatColumnDefences)
			{
				this.DefHdr.Width = 0;
			}
			if (!Session.Preferences.CombatColumnEffects)
			{
				this.EffectsHdr.Width = 0;
			}
			Screen screen = Screen.FromControl(this);
			if (screen.Bounds.Height > screen.Bounds.Width)
			{
				this.OptionsPortrait_Click(null, null);
			}
			Session.CurrentEncounter = this.fEncounter;
			this.update_list();
			this.update_log();
			this.update_preview_panel();
			this.update_maps();
			this.update_statusbar();

            this.RegisterCommands();
        }

        private void RegisterCommands()
        {
            CommandManager.GetInstance().RegisterListener(typeof(InitiativeAdvanceCommand), this.InitiativeAdvancedHandler);
            CommandManager.GetInstance().RegisterListener(typeof(InitiativePreviousCommand), this.InitiativeAdvancedHandler);
            CommandManager.GetInstance().RegisterListener(typeof(HealEntitiesCommand), this.HealCommandCallback);
            CommandManager.GetInstance().RegisterListener(typeof(DamageEntityCommand), this.DamageCommandCallback);
            CommandManager.GetInstance().RegisterListener(typeof(MoveTokenCommand), this.OnMoveTokenCommand);
            CommandManager.GetInstance().RegisterListener(typeof(RemoveEffectCommand), this.OnRemoveEffectCommand);
            CommandManager.GetInstance().RegisterListener(typeof(AddEffectCommand), this.OnRemoveEffectCommand);
            CommandManager.GetInstance().RegisterListener(typeof(AddRemoveLinkCommand), this.OnAddRemoveLinkCommand);
            CommandManager.GetInstance().RegisterListener(typeof(RemoveFromMapCommand), this.OnRemoveFromMapCommand);
            CommandManager.GetInstance().RegisterListener(typeof(RemoveFromCombatCommand), this.OnRemoveFromMapCommand);
            CommandManager.GetInstance().RegisterListener(typeof(VisibilityToggleCommand), this.HealCommandCallback);
            CommandManager.GetInstance().RegisterListener(typeof(DelayAction), this.UpdateUIForNewTurn);

            // Compound Transactions
            CommandManager.GetInstance().RegisterListener(typeof(BeginningOfTurnUpdates), this.UpdateUIForNewTurn);
            CommandManager.GetInstance().RegisterListener(typeof(EndTurnCommand), this.InitiativeAdvancedHandler);
        }

        private void add_condition_hint(ListViewItem lvi)
		{
			if (lvi.ImageIndex == -1)
			{
				return;
			}
			Image item = this.CombatList.SmallImageList.Images[lvi.ImageIndex];
			Graphics graphic = Graphics.FromImage(item);
			graphic.SmoothingMode = SmoothingMode.AntiAlias;
			graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
			graphic.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			graphic.FillEllipse(Brushes.White, 5, 5, 6, 6);
			graphic.DrawEllipse(Pens.DarkGray, 5, 5, 6, 6);
			this.CombatList.SmallImageList.Images[lvi.ImageIndex] = item;
		}

		private void add_icon(ListViewItem lvi, Color c)
		{
			Image bitmap = new Bitmap(16, 16);
			Graphics graphic = Graphics.FromImage(bitmap);
			graphic.SmoothingMode = SmoothingMode.AntiAlias;
			graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
			graphic.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			graphic.FillEllipse(new SolidBrush(c), 2, 2, 12, 12);
			if (c == Color.White)
			{
				graphic.DrawEllipse(Pens.Black, 2, 2, 12, 12);
			}
			this.CombatList.SmallImageList.Images.Add(bitmap);
			lvi.ImageIndex = this.CombatList.SmallImageList.Images.Count - 1;
		}

    // TODO:  Fix This
		private void add_in_command_clicked(object sender, EventArgs e)
		{
			//try
			//{
			//	((sender as ToolStripMenuItem).Tag as ICommand).Execute();
			//}
			//catch (Exception exception)
			//{
			//	LogSystem.Trace(exception);
			//}
		}

		private void add_initiative_hint(ListViewItem lvi)
		{
			if (lvi.ImageIndex == -1)
			{
				return;
			}
			Image item = this.CombatList.SmallImageList.Images[lvi.ImageIndex];
			Graphics graphic = Graphics.FromImage(item);
			graphic.SmoothingMode = SmoothingMode.AntiAlias;
			graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
			graphic.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			Pen pen = new Pen(Color.Blue, 3f);
			graphic.DrawRectangle(pen, 0, 0, 16, 16);
			this.CombatList.SmallImageList.Images[lvi.ImageIndex] = item;
		}

		private void add_quick_effect(OngoingCondition effect)
		{
			string str = effect.ToString(this.fEncounter, false);
			foreach (OngoingCondition fEffect in this.fEffects)
			{
				if (fEffect.ToString(this.fEncounter, false) != str)
				{
					continue;
				}
				return;
			}
			this.fEffects.Add(effect.Copy());
			this.fEffects.Sort();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			try
			{
				bool flag = false;
				bool flag1 = false;
				if (this.SelectedTokens.Count != 0)
				{
					flag = true;
					flag1 = true;
					foreach (IToken selectedToken in this.SelectedTokens)
					{
						if ((selectedToken is CreatureToken ? false : !(selectedToken is Hero)))
						{
							flag = false;
							flag1 = false;
						}
						if (selectedToken is CreatureToken && !(selectedToken as CreatureToken).Data.Delaying)
						{
							flag1 = false;
						}
						if (!(selectedToken is Hero) || (selectedToken as Hero).CombatData.Delaying)
						{
							continue;
						}
						flag1 = false;
					}
				}
				this.DetailsBtn.Enabled = this.SelectedTokens.Count == 1;
				this.DamageBtn.Enabled = flag;
				this.HealBtn.Enabled = flag;
				this.EffectMenu.Enabled = flag;
                this.UndoBtn.Text = "Undo";
                this.RedoBtn.Text = "Redo";
                this.PrevInitBtn.Text = "Prev Turn";
                this.NextInitBtn.Text = (this.fCombatStarted ? "Next Turn" : "Start Encounter");
				this.DelayBtn.Visible = this.fCombatStarted;
				this.DelayBtn.Enabled = flag;
				this.DelayBtn.Checked = flag1;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void apply_effect(OngoingCondition oc, List<IToken> tokens, bool add_to_quick_list)
		{
			try
			{
				if (oc.Duration == DurationType.BeginningOfTurn || oc.Duration == DurationType.EndOfTurn)
				{
					if (oc.DurationCreatureID == Guid.Empty)
					{
						CombatantSelectForm combatantSelectForm = new CombatantSelectForm(this.fEncounter);
						if (combatantSelectForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						{
							if (combatantSelectForm.SelectedCombatant == null)
							{
								return;
							}
							else
							{
								oc.DurationCreatureID = combatantSelectForm.SelectedCombatant.ID;
							}
						}
					}
					oc.DurationRound = this.fCurrentRound;
					if (this.fCurrentActor != null && oc.DurationCreatureID == this.fCurrentActor.ID)
					{
						OngoingCondition durationRound = oc;
						durationRound.DurationRound = durationRound.DurationRound + 1;
					}
				}

                // Queue these up and run them seperately since the tokens list may be modified by the command
                var commands = new List<AddEffectCommand>();

                foreach (IToken token in tokens)
				{
					CreatureToken creatureToken = token as CreatureToken;
					if (creatureToken != null)
					{
						CombatData data = creatureToken.Data;
                        // TODO:  Why does this need to be a copy?
                        commands.Add(new AddEffectCommand(data, oc.Copy()));
					}
					Hero hero = token as Hero;
					if (hero == null)
					{
						continue;
					}
					CombatData combatData = hero.CombatData;
                    commands.Add(new AddEffectCommand(combatData, oc.Copy()));
				}

                foreach(var command in commands)
                {
                    CommandManager.GetInstance().ExecuteCommand(command);
                }

				if (add_to_quick_list)
				{
					bool flag = false;
					OngoingCondition empty = oc.Copy();
					if (Session.Project.Heroes.Count != 0)
					{
						Hero hero1 = Session.Project.FindHero(this.fCurrentActor.ID);
						HeroSelectForm heroSelectForm = new HeroSelectForm(hero1);
						if (heroSelectForm.ShowDialog() == System.Windows.Forms.DialogResult.OK && heroSelectForm.SelectedHero != null)
						{
							if (empty.DurationCreatureID != heroSelectForm.SelectedHero.ID)
							{
								empty.DurationCreatureID = Guid.Empty;
							}
							heroSelectForm.SelectedHero.Effects.Add(empty);
							Session.Modified = true;
							flag = true;
						}
					}
					if (!flag)
					{
						this.add_quick_effect(empty);
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void apply_effect_from_map(object sender, EventArgs e)
		{
			OngoingCondition ongoingCondition = new OngoingCondition();
			EffectForm effectForm = new EffectForm(ongoingCondition, this.fEncounter, this.fCurrentActor, this.fCurrentRound);
			if (effectForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.apply_effect(effectForm.Effect, this.MapView.SelectedTokens, true);
			}
		}

		private void apply_effect_from_toolbar(object sender, EventArgs e)
		{
			OngoingCondition ongoingCondition = new OngoingCondition();
			EffectForm effectForm = new EffectForm(ongoingCondition, this.fEncounter, this.fCurrentActor, this.fCurrentRound);
			if (effectForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.apply_effect(effectForm.Effect, this.SelectedTokens, true);
			}
		}

		private void apply_quick_effect_from_map(object sender, EventArgs e)
		{
			OngoingCondition tag = (sender as ToolStripItem).Tag as OngoingCondition;
			if (tag == null)
			{
				return;
			}
			this.apply_effect(tag.Copy(), this.MapView.SelectedTokens, false);
		}

		private void apply_quick_effect_from_toolbar(object sender, EventArgs e)
		{
			OngoingCondition tag = (sender as ToolStripItem).Tag as OngoingCondition;
			if (tag == null)
			{
				return;
			}
			this.apply_effect(tag.Copy(), this.SelectedTokens, false);
		}

		private void cancelled_scrolling()
		{
			Masterplan.Controls.MapView mapView;
			string str;
			if (Session.Preferences.PlayerViewMap)
			{
				mapView = this.MapView;
			}
			else
			{
				mapView = null;
			}
			Masterplan.Controls.MapView mapView1 = mapView;
			if (Session.Preferences.PlayerViewInitiative)
			{
				str = this.InitiativeView();
			}
			else
			{
				str = null;
			}
			Session.PlayerView.ShowTacticalMap(mapView1, str);
			this.PlayerMap.ScalingFactor = this.MapView.ScalingFactor;
		}

		private void CloseBtn_Click(object sender, EventArgs e)
		{
			this.fPromptOnClose = false;
			base.Close();
		}

		private void CombatantsAdd_Click(object sender, EventArgs e)
		{
			try
			{
				EncounterBuilderForm encounterBuilderForm = new EncounterBuilderForm(new Encounter(), this.fPartyLevel, true);
				if (encounterBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					foreach (EncounterSlot slot in encounterBuilderForm.Encounter.Slots)
					{
						this.fEncounter.Slots.Add(slot);
						if (!this.fCombatStarted)
						{
							continue;
						}
						this.roll_initiative();
					}
					foreach (Trap trap in encounterBuilderForm.Encounter.Traps)
					{
						if (trap.Initiative != Int32.MinValue)
						{
							trap.CombatData = new CombatData();
							if (this.fCombatStarted)
							{
								this.roll_initiative();
							}
						}
						this.fEncounter.Traps.Add(trap);
					}
					foreach (SkillChallenge skillChallenge in encounterBuilderForm.Encounter.SkillChallenges)
					{
						this.fEncounter.SkillChallenges.Add(skillChallenge);
					}
					this.update_list();
					this.update_preview_panel();
					this.update_statusbar();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void CombatantsAddCustom_Click(object sender, EventArgs e)
		{
			try
			{
				CustomToken customToken = new CustomToken()
				{
					Name = "Custom Token",
					Type = CustomTokenType.Token
				};
				CustomTokenForm customTokenForm = new CustomTokenForm(customToken);
				if (customTokenForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fEncounter.CustomTokens.Add(customTokenForm.Token);
					this.update_list();
					this.update_maps();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void CombatantsAddOverlay_Click(object sender, EventArgs e)
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
					this.update_list();
					this.update_maps();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void CombatantsBtn_DropDownOpening(object sender, EventArgs e)
		{
			this.CombatantsAddToken.Visible = this.fEncounter.MapID != Guid.Empty;
			this.CombatantsAddOverlay.Visible = this.fEncounter.MapID != Guid.Empty;
			this.CombatantsRemove.Enabled = this.SelectedTokens.Count != 0;
			this.CombatantsWaves.DropDownItems.Clear();
			foreach (EncounterWave wafe in this.fEncounter.Waves)
			{
				if (wafe.Count == 0)
				{
					continue;
				}
				ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(wafe.Name)
				{
					Checked = wafe.Active,
					Tag = wafe
				};
				toolStripMenuItem.Click += new EventHandler(this.wave_activated);
				this.CombatantsWaves.DropDownItems.Add(toolStripMenuItem);
			}
			if (this.CombatantsWaves.DropDownItems.Count == 0)
			{
				ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem("(none set)")
				{
					Enabled = false
				};
				this.CombatantsWaves.DropDownItems.Add(toolStripMenuItem1);
			}
		}

		private void CombatantsEffects_Click(object sender, EventArgs e)
		{
			EffectListForm effectListForm = new EffectListForm(this.fEncounter, this.fCurrentActor, this.fCurrentRound);
			effectListForm.ShowDialog();
			this.update_list();
			this.update_preview_panel();
			this.update_maps();
		}

		private void CombatantsHideAll_Click(object sender, EventArgs e)
		{
			this.show_or_hide_all(false);
		}

		private void CombatantsLinks_Click(object sender, EventArgs e)
		{
			(new TokenLinkListForm(this.MapView.TokenLinks)).ShowDialog();
			this.update_list();
			this.update_preview_panel();
			this.update_maps();
		}

		private void CombatantsRemove_Click(object sender, EventArgs e)
		{
			if (this.SelectedTokens.Count != 0)
			{
				this.remove_from_combat(this.SelectedTokens);
			}
		}

		private void CombatantsShowAll_Click(object sender, EventArgs e)
		{
			this.show_or_hide_all(true);
		}

		private void CombatForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				if (this.fPromptOnClose)
				{
					bool flag = false;
					foreach (EncounterSlot allSlot in this.fEncounter.AllSlots)
					{
						int hP = allSlot.Card.HP;
						foreach (CombatData combatDatum in allSlot.CombatData)
						{
							if (combatDatum.Initiative == -2147483648 || hP + combatDatum.TempHP - combatDatum.Damage <= 0)
							{
								continue;
							}
							flag = true;
						}
					}
					if (flag && MessageBox.Show("There are creatures remaining; are you sure you want to end the encounter?", "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.No)
					{
						e.Cancel = true;
						return;
					}
				}
				if (this.PlayerMap != null || this.PlayerInitiative != null)
				{
					Session.PlayerView.ShowDefault();
				}
				Session.CurrentEncounter = null;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void CombatForm_Shown(object sender, EventArgs e)
		{
			try
			{
				if (!Session.Preferences.CombatMapRight)
				{
					this.MapSplitter.SplitterDistance = this.MapSplitter.Height / 2;
				}
				if (this.fCurrentActor == null)
				{
					foreach (Hero hero in Session.Project.Heroes)
					{
						hero.CombatData.Reset(false);
					}
					this.update_list();
					this.update_maps();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void CombatList_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedTokens.Count == 1)
				{
					this.edit_token(this.SelectedTokens[0]);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void CombatList_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
		{
			e.DrawDefault = true;
		}

		private void CombatList_DrawItem(object sender, DrawListViewItemEventArgs e)
		{
		}

		private void CombatList_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
		{
			Brush highlightText;
			Brush highlight;
			e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			if (e.Item.Selected)
			{
				highlightText = SystemBrushes.HighlightText;
			}
			else
			{
				highlightText = new SolidBrush(e.Item.ForeColor);
			}
			Brush brush = highlightText;
			if (e.Item.Selected)
			{
				highlight = SystemBrushes.Highlight;
			}
			else
			{
				highlight = new SolidBrush(e.Item.BackColor);
			}
			Brush brush1 = highlight;
			StringFormat stringFormat = (e.Header.TextAlign == HorizontalAlignment.Left ? this.fLeft : this.fRight);
			e.Graphics.FillRectangle(brush1, e.Bounds);
			if (e.ColumnIndex == 0)
			{
				CreatureState state = CreatureState.Defeated;
				int hP = 0;
				int damage = 0;
				int tempHP = 0;
				if (e.Item.Tag is CreatureToken)
				{
					CombatData data = (e.Item.Tag as CreatureToken).Data;
					EncounterSlot encounterSlot = this.fEncounter.FindSlot(data);
					state = encounterSlot.GetState(data);
					hP = encounterSlot.Card.HP;
					damage = hP - data.Damage;
					tempHP = data.TempHP;
				}
				if (e.Item.Tag is Hero)
				{
					Hero tag = e.Item.Tag as Hero;
					CombatData combatData = tag.CombatData;
					state = CreatureState.Active;
					hP = tag.HP;
					damage = hP - combatData.Damage;
					tempHP = combatData.TempHP;
				}
				if (e.Item.Tag is SkillChallenge)
				{
					SkillChallenge skillChallenge = e.Item.Tag as SkillChallenge;
					if (skillChallenge.Results.Fails >= 3)
					{
						state = CreatureState.Bloodied;
						damage = 3;
						hP = 3;
					}
					else if (skillChallenge.Results.Successes < skillChallenge.Successes)
					{
						state = CreatureState.Active;
						hP = skillChallenge.Successes;
						damage = skillChallenge.Successes - skillChallenge.Results.Successes;
					}
					else
					{
						state = CreatureState.Defeated;
						damage = skillChallenge.Successes;
						hP = skillChallenge.Successes;
					}
				}
				if (damage < 0)
				{
					damage = 0;
				}
				if (damage > hP)
				{
					damage = hP;
				}
				if (hP <= 1 || state == CreatureState.Defeated)
				{
					Graphics graphics = e.Graphics;
					Pen darkGray = Pens.DarkGray;
					int left = e.Bounds.Left;
					int bottom = e.Bounds.Bottom;
					int right = e.Bounds.Right;
					Rectangle bounds = e.Bounds;
					graphics.DrawLine(darkGray, left, bottom, right, bounds.Bottom);
				}
				else
				{
					int width = e.Bounds.Width - 1;
					int height = e.Bounds.Height / 4;
					int x = e.Bounds.X;
					Rectangle rectangle = e.Bounds;
					Rectangle rectangle1 = new Rectangle(x, rectangle.Bottom - height, width, height);
					Color color = (state == CreatureState.Bloodied ? Color.Red : Color.DarkGray);
					Brush linearGradientBrush = new LinearGradientBrush(rectangle1, Color.White, Color.FromArgb(10, color), LinearGradientMode.Vertical);
					e.Graphics.FillRectangle(linearGradientBrush, rectangle1);
					e.Graphics.DrawRectangle(Pens.DarkGray, rectangle1);
					int num = width * damage / (hP + tempHP);
					Rectangle rectangle2 = new Rectangle(rectangle1.X, rectangle1.Y, num, height);
					Brush linearGradientBrush1 = new LinearGradientBrush(rectangle2, Color.Transparent, color, LinearGradientMode.Vertical);
					e.Graphics.FillRectangle(linearGradientBrush1, rectangle2);
					if (tempHP > 0)
					{
						int num1 = width * tempHP / (hP + tempHP);
						Rectangle rectangle3 = new Rectangle(rectangle2.Right, rectangle2.Y, num1, height);
						Brush linearGradientBrush2 = new LinearGradientBrush(rectangle3, Color.Transparent, Color.Blue, LinearGradientMode.Vertical);
						e.Graphics.FillRectangle(linearGradientBrush2, rectangle3);
					}
				}
			}
			e.Graphics.DrawString(e.SubItem.Text, e.Item.Font, brush, e.Bounds, stringFormat);
		}

		private void CombatList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			try
			{
				if (this.SelectedTokens.Count == 1)
				{
					IToken item = this.SelectedTokens[0];
					if (item is CreatureToken)
					{
						CreatureToken creatureToken = item as CreatureToken;
						if (creatureToken.Data.Location == CombatData.NoPoint)
						{
							base.DoDragDrop(creatureToken, DragDropEffects.Move);
							this.update_list();
							this.update_preview_panel();
							this.update_maps();
						}
					}
					if (item is Hero)
					{
						Hero hero = item as Hero;
						if (hero.CombatData.Location == CombatData.NoPoint)
						{
							base.DoDragDrop(hero, DragDropEffects.Move);
							if (hero.CombatData.Location != CombatData.NoPoint)
							{
								this.update_list();
								this.update_preview_panel();
								this.update_maps();
							}
						}
					}
					if (item is CustomToken)
					{
						CustomToken customToken = item as CustomToken;
						if (customToken.Data.Location == CombatData.NoPoint)
						{
							base.DoDragDrop(customToken, DragDropEffects.Move);
							this.update_list();
							this.update_preview_panel();
							this.update_maps();
						}
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void CombatList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			try
			{
				if (!this.fUpdatingList)
				{
					if (this.SelectedTokens.Count != 0)
					{
						this.MapView.SelectTokens(this.SelectedTokens, false);
						if (this.PlayerMap != null)
						{
							this.PlayerMap.SelectTokens(this.SelectedTokens, false);
						}
						this.update_preview_panel();
					}
					else
					{
						this.MapView.SelectTokens(null, false);
						if (this.PlayerMap != null)
						{
							this.PlayerMap.SelectTokens(null, false);
						}
						this.update_preview_panel();
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void CombatList_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void copy_custom_token()
		{
			foreach (IToken selectedToken in this.SelectedTokens)
			{
				if (!(selectedToken is CustomToken))
				{
					continue;
				}
				CustomToken noPoint = (selectedToken as CustomToken).Copy();
				noPoint.ID = Guid.NewGuid();
				noPoint.Data.Location = CombatData.NoPoint;
				this.fEncounter.CustomTokens.Add(noPoint);
			}
			this.update_list();
		}

		private void DamageBtn_Click(object sender, EventArgs e)
		{
			try
			{
				this.do_damage(this.SelectedTokens);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void DelayBtn_Click(object sender, EventArgs e)
		{
			try
			{
				this.set_delay(this.SelectedTokens);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void DetailsBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedTokens.Count == 1)
				{
					this.edit_token(this.SelectedTokens[0]);
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

		private void do_damage(List<IToken> tokens)
		{
			List<Pair<CombatData, EncounterCard>> pairs = new List<Pair<CombatData, EncounterCard>>();
			foreach (IToken token in tokens)
			{
				CombatData data = null;
				EncounterCard card = null;
				if (token is CreatureToken)
				{
					CreatureToken creatureToken = token as CreatureToken;
					data = creatureToken.Data;
					card = this.fEncounter.FindSlot(creatureToken.SlotID).Card;
				}
				if (token is Hero)
				{
					data = (token as Hero).CombatData;
				}
				pairs.Add(new Pair<CombatData, EncounterCard>(data, card));
			}
			Dictionary<CombatData, int> combatDatas = new Dictionary<CombatData, int>();
			Dictionary<CombatData, CreatureState> _state = new Dictionary<CombatData, CreatureState>();
			foreach (Pair<CombatData, EncounterCard> pair in pairs)
			{
				combatDatas[pair.First] = pair.First.Damage;
			}
			foreach (Pair<CombatData, EncounterCard> pair1 in pairs)
			{
				_state[pair1.First] = this.get_state(pair1.First);
			}
			DamageForm damageForm = new DamageForm(pairs, 0, this.fEncounter);
			if (damageForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
                CommandManager.GetInstance().ExecuteCommand(damageForm.DamageCommand);
			}
		}

        private void RemoveDeadEnemies()
        {
            // THIS IS A PROBLEM!  If we already have a compound command open then this will add commands to it
            if (Session.Preferences.CreatureAutoRemove)
            {
                foreach (EncounterSlot allSlot in this.fEncounter.AllSlots)
                {
                    foreach (CombatData combatDatum in allSlot.CombatData)
                    {
                        if (allSlot.GetState(combatDatum) == CreatureState.Defeated)
                        {
                            var command = new RemoveFromMapCommand(combatDatum);
                            command.EffectsToRemove = remove_all_effects_caused_by(combatDatum.ID);
                            command.LinksToRemove = remove_links(combatDatum.Location);
                            command.RemoveFromInitiative = true;
                            CommandManager.GetInstance().ExecuteCommand(command);
                        }
                    }
                }
            }
        }

        private void DamageCommandCallback()
        {
            // Check to see if anyone died.
            this.RemoveDeadEnemies();

            // Callback of Damage Command
            this.update_list();
            this.update_log();
            this.update_preview_panel();
            this.update_maps();
        }

        private void HealCommandCallback()
        {
            this.update_list();
            this.update_log();
            this.update_preview_panel();
            this.update_maps();
        }

        private void do_heal(List<IToken> tokens)
		{
			List<Pair<CombatData, EncounterCard>> pairs = new List<Pair<CombatData, EncounterCard>>();
			foreach (IToken token in tokens)
			{
				CombatData data = null;
				EncounterCard card = null;
				if (token is CreatureToken)
				{
					CreatureToken creatureToken = token as CreatureToken;
					data = creatureToken.Data;
					card = this.fEncounter.FindSlot(creatureToken.SlotID).Card;
				}
				if (token is Hero)
				{
					data = (token as Hero).CombatData;
				}
				pairs.Add(new Pair<CombatData, EncounterCard>(data, card));
			}
			Dictionary<CombatData, int> combatDatas = new Dictionary<CombatData, int>();
			Dictionary<CombatData, CreatureState> _state = new Dictionary<CombatData, CreatureState>();
			foreach (Pair<CombatData, EncounterCard> pair in pairs)
			{
				combatDatas[pair.First] = pair.First.Damage;
			}
			foreach (Pair<CombatData, EncounterCard> pair1 in pairs)
			{
				_state[pair1.First] = this.get_state(pair1.First);
			}
            HealForm form = new HealForm(pairs);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
                CommandManager.GetInstance().ExecuteCommand(form.HealCommand);
				//foreach (Pair<CombatData, EncounterCard> pair2 in pairs)
				//{
				//	int damage = pair2.First.Damage - combatDatas[pair2.First];
				//	if (damage != 0)
				//	{
				//		this.fLog.AddDamageEntry(pair2.First.ID, damage, null);
				//	}
				//	CreatureState creatureState = this.get_state(pair2.First);
				//	if (creatureState == _state[pair2.First])
				//	{
				//		continue;
				//	}
				//	this.fLog.AddStateEntry(pair2.First.ID, creatureState);
				//}
			}
		}

		private bool edit_initiative(Hero hero)
		{
			int initiative = 0;
			initiative = hero.CombatData.Initiative;
			InitiativeForm initiativeForm = new InitiativeForm(hero.InitBonus, initiative);
			if (initiativeForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
			{
				return false;
			}
			hero.CombatData.Initiative = initiativeForm.Score;
			this.update_list();
			this.update_preview_panel();
			this.update_maps();
			this.update_statusbar();
			List<int> _initiatives = this.get_initiatives();
			this.InitiativePanel.InitiativeScores = _initiatives;
			int item = _initiatives[0];
			return true;
		}

		private void edit_token(IToken token)
		{
			if (token is CreatureToken)
			{
				CreatureToken creatureToken = token as CreatureToken;
				EncounterSlot data = this.fEncounter.FindSlot(creatureToken.SlotID);
				int num = data.CombatData.IndexOf(creatureToken.Data);
				int damage = creatureToken.Data.Damage;
				CreatureState state = data.GetState(creatureToken.Data);
				List<string> strs = new List<string>();
				foreach (OngoingCondition condition in creatureToken.Data.Conditions)
				{
					strs.Add(condition.ToString(this.fEncounter, false));
				}
				CombatDataForm combatDataForm = new CombatDataForm(creatureToken.Data, data.Card, this.fEncounter, this.fCurrentActor, this.fCurrentRound, true);
				if (combatDataForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
                    if (combatDataForm.InitBox.Value != creatureToken.Data.Initiative)
                    {
                        // initiative changed
                        this.combatState.InitiativeList.UpdateInitiative(creatureToken.Data, (int)combatDataForm.InitBox.Value);
                    }

					data.CombatData[num] = combatDataForm.Data;
					if (damage != combatDataForm.Data.Damage)
					{
						damage = combatDataForm.Data.Damage - damage;
						this.fLog.AddDamageEntry(combatDataForm.Data.ID, damage, null);
					}
					if (data.GetState(combatDataForm.Data) != state)
					{
						state = data.GetState(combatDataForm.Data);
						this.fLog.AddStateEntry(combatDataForm.Data.ID, state);
					}
					List<string> strs1 = new List<string>();
					foreach (OngoingCondition ongoingCondition in combatDataForm.Data.Conditions)
					{
						strs1.Add(ongoingCondition.ToString(this.fEncounter, false));
					}
					foreach (string str in strs)
					{
						if (strs1.Contains(str))
						{
							continue;
						}
						this.fLog.AddEffectEntry(combatDataForm.Data.ID, str, false);
					}
					foreach (string str1 in strs1)
					{
						if (strs.Contains(str1))
						{
							continue;
						}
						this.fLog.AddEffectEntry(combatDataForm.Data.ID, str1, true);
					}

					this.update_list();
					this.update_log();
					this.update_preview_panel();
					this.update_maps();
					this.InitiativePanel.InitiativeScores = this.get_initiatives();
				}
			}
			if (token is Hero)
			{
				Hero hero = token as Hero;
				if (hero.CombatData.Initiative != -2147483648)
				{
					CombatData combatData = hero.CombatData;
					int damage1 = combatData.Damage;
					CreatureState creatureState = hero.GetState(combatData.Damage);
					List<string> strs2 = new List<string>();
					foreach (OngoingCondition condition1 in combatData.Conditions)
					{
						strs2.Add(condition1.ToString(this.fEncounter, false));
					}
					CombatDataForm combatDataForm1 = new CombatDataForm(combatData, null, this.fEncounter, this.fCurrentActor, this.fCurrentRound, false);
					if (combatDataForm1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
                        if (combatDataForm1.InitBox.Value != combatData.Initiative)
                        {
                            // initiative changed
                            this.combatState.InitiativeList.UpdateInitiative(combatData, (int)combatDataForm1.InitBox.Value);
                        }

                        hero.CombatData = combatDataForm1.Data;
						if (damage1 != combatDataForm1.Data.Damage)
						{
							damage1 = combatDataForm1.Data.Damage - damage1;
							this.fLog.AddDamageEntry(combatDataForm1.Data.ID, damage1, null);
						}
						if (hero.GetState(combatDataForm1.Data.Damage) != creatureState)
						{
							creatureState = hero.GetState(combatDataForm1.Data.Damage);
							this.fLog.AddStateEntry(combatDataForm1.Data.ID, creatureState);
						}
						List<string> strs3 = new List<string>();
						foreach (OngoingCondition ongoingCondition1 in combatDataForm1.Data.Conditions)
						{
							strs3.Add(ongoingCondition1.ToString(this.fEncounter, false));
						}
						foreach (string str2 in strs2)
						{
							if (strs3.Contains(str2))
							{
								continue;
							}
							this.fLog.AddEffectEntry(combatDataForm1.Data.ID, str2, false);
						}
						foreach (string str3 in strs3)
						{
							if (strs2.Contains(str3))
							{
								continue;
							}
							this.fLog.AddEffectEntry(combatDataForm1.Data.ID, str3, true);
						}
						this.update_list();
						this.update_log();
						this.update_preview_panel();
						this.update_maps();
						this.InitiativePanel.InitiativeScores = this.get_initiatives();
					}
				}
				else
				{
					this.edit_initiative(hero);
				}
			}
			if (token is CustomToken)
			{
				CustomToken customToken = token as CustomToken;
				int num1 = this.fEncounter.CustomTokens.IndexOf(customToken);
				if (num1 != -1)
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
							this.fEncounter.CustomTokens[num1] = customTokenForm.Token;
							this.update_list();
							this.update_preview_panel();
							this.update_maps();
							return;
						}
						case CustomTokenType.Overlay:
						{
                                bool refreshTerrain = customToken.IsTerrainLayer;
                                CustomOverlayForm customOverlayForm = new CustomOverlayForm(customToken);
                                if (customOverlayForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                                {
                                    break;
                                }
                                this.fEncounter.CustomTokens[num1] = customOverlayForm.Token;
                            refreshTerrain |= customOverlayForm.Token.IsTerrainLayer;
                            if (refreshTerrain)
                            {
                                    CombatForm.TerrainLayersNeedRefresh = true;
                                    this.RebuildTerrainLayersAllMaps();
                            }
							this.update_list();
							this.update_preview_panel();
							this.update_maps();
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

		private void EffectMenu_DropDownOpening(object sender, EventArgs e)
		{
			this.update_effects_list(this.EffectMenu, true);
		}

		public string EncounterLogView(bool player_view)
		{
			List<string> strs = new List<string>();
			if (!player_view)
			{
				strs.AddRange(HTML.GetHead("Encounter Log", "", DisplaySize.Small));
				strs.Add("<BODY>");
			}
			if (this.fLog != null)
			{
				strs.Add("<P class=table>");
				strs.Add("<TABLE class=wide>");
				strs.Add("<TR class=encounterlog>");
				strs.Add("<TD colspan=2>");
				strs.Add("<B>Encounter Log</B>");
				strs.Add("</TD>");
				strs.Add("<TD align=right>");
				strs.Add(string.Concat("<B>Round ", this.fCurrentRound, "</B>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (!this.fLog.Active)
				{
					strs.Add("<TR class=warning>");
					strs.Add("<TD colspan=3>");
					strs.Add("The log is not yet active as the encounter has not started.");
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				foreach (RoundLog round in this.fLog.CreateReport(this.fEncounter, !player_view).Rounds)
				{
					strs.Add("<TR class=shaded>");
					if (!player_view)
					{
						strs.Add("<TD colspan=3>");
					}
					else
					{
						strs.Add("<TD class=pvlogentry colspan=3>");
					}
					strs.Add(string.Concat("<B>Round ", round.Round, "</B>"));
					strs.Add("</TD>");
					strs.Add("</TR>");
					if (round.Count == 0)
					{
						strs.Add("<TR>");
						if (!player_view)
						{
							strs.Add("<TD align=center colspan=3>");
						}
						else
						{
							strs.Add("<TD class=pvlogentry align=center colspan=3>");
						}
						strs.Add("(nothing)");
						strs.Add("</TD>");
						strs.Add("</TR>");
					}
					bool flag = (!player_view ? true : Session.Preferences.PlayerViewCreatureLabels);
					foreach (TurnLog turn in round.Turns)
					{
						if (turn.Entries.Count == 0)
						{
							continue;
						}
						strs.Add("<TR>");
						if (!player_view)
						{
							strs.Add("<TD colspan=2>");
						}
						else
						{
							strs.Add("<TD class=pvlogentry colspan=3>");
						}
						strs.Add(string.Concat("<B>", EncounterLog.GetName(turn.ID, this.fEncounter, flag), "</B>"));
						strs.Add("</TD>");
						if (!player_view)
						{
							strs.Add("<TD align=right>");
							strs.Add(turn.Start.ToString("h:mm:ss"));
							strs.Add("</TD>");
						}
						strs.Add("</TR>");
						foreach (IEncounterLogEntry entry in turn.Entries)
						{
							strs.Add("<TR>");
							if (!player_view)
							{
								strs.Add("<TD class=indent colspan=3>");
							}
							else
							{
								strs.Add("<TD class=pvlogindent colspan=3>");
							}
							strs.Add(entry.Description(this.fEncounter, flag));
							strs.Add("</TD>");
							strs.Add("</TR>");
						}
					}
				}
				strs.Add("</TABLE>");
				strs.Add("</P>");
			}
			if (!player_view)
			{
				strs.Add("</BODY>");
				strs.Add("</HTML>");
			}
			return HTML.Concatenate(strs);
		}

		private ListViewItem get_combatant(Guid id)
		{
			ListViewItem listViewItem;
			IEnumerator enumerator = this.CombatList.Items.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					ListViewItem current = (ListViewItem)enumerator.Current;
					CreatureToken tag = current.Tag as CreatureToken;
					if (tag == null || !(tag.Data.ID == id))
					{
						Hero hero = current.Tag as Hero;
						if (hero == null || !(hero.ID == id))
						{
							Trap trap = current.Tag as Trap;
							if (trap == null || !(trap.ID == id))
							{
								continue;
							}
							listViewItem = current;
							return listViewItem;
						}
						else
						{
							listViewItem = current;
							return listViewItem;
						}
					}
					else
					{
						listViewItem = current;
						return listViewItem;
					}
				}
				return null;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		private string get_conditions(CombatData cd)
		{
			string str = "";
			bool flag = false;
			foreach (OngoingCondition condition in cd.Conditions)
			{
				if (condition.Type != OngoingType.Damage)
				{
					continue;
				}
				flag = true;
				break;
			}
			if (flag)
			{
				if (str != "")
				{
					str = string.Concat(str, "; ");
				}
				str = string.Concat(str, "Damage");
			}
			foreach (OngoingCondition ongoingCondition in cd.Conditions)
			{
				if (ongoingCondition.Type == OngoingType.Damage)
				{
					continue;
				}
				if (str != "")
				{
					str = string.Concat(str, "; ");
				}
				switch (ongoingCondition.Type)
				{
					case OngoingType.Condition:
					{
						str = string.Concat(str, ongoingCondition.Data);
						continue;
					}
					case OngoingType.DefenceModifier:
					{
						str = string.Concat(str, ongoingCondition.ToString(this.fEncounter, false));
						continue;
					}
					default:
					{
						continue;
					}
				}
			}
			return str;
		}

		private string get_info(CreatureToken token)
		{
			string str = "";
			EncounterSlot encounterSlot = this.fEncounter.FindSlot(token.SlotID);
			foreach (string str1 in encounterSlot.Card.AsText(token.Data, CardMode.Text, true))
			{
				if (str != "")
				{
					str = string.Concat(str, Environment.NewLine);
				}
				str = string.Concat(str, str1);
			}
			if (token.Data.Conditions.Count != 0)
			{
				str = string.Concat(str, Environment.NewLine);
				foreach (OngoingCondition condition in token.Data.Conditions)
				{
					str = string.Concat(str, Environment.NewLine);
					str = string.Concat(str, condition.ToString(this.fEncounter, false));
				}
			}
			return str;
		}

		private string get_info(Hero hero)
		{
			string str = string.Concat(hero.Race, " ", hero.Class);
			if (hero.Player != "")
			{
				str = string.Concat(str, Environment.NewLine);
				str = string.Concat(str, "Player: ", hero.Player);
			}
			CombatData combatData = hero.CombatData;
			if (combatData != null && combatData.Conditions.Count != 0)
			{
				str = string.Concat(str, Environment.NewLine);
				foreach (OngoingCondition condition in combatData.Conditions)
				{
					str = string.Concat(str, Environment.NewLine);
					str = string.Concat(str, condition.ToString(this.fEncounter, false));
				}
			}
			return str;
		}

		private string get_info(CustomToken token)
		{
			if (token.Details == "")
			{
				return "(no details)";
			}
			return token.Details;
		}

		private List<int> get_initiatives()
		{
            return this.combatState.InitiativeList.GetAsList();
		}

		private Point get_location(IToken token)
		{
			if (token is CreatureToken)
			{
				return (token as CreatureToken).Data.Location;
			}
			if (token is Hero)
			{
				return (token as Hero).CombatData.Location;
			}
			if (!(token is CustomToken))
			{
				return CombatData.NoPoint;
			}
			return (token as CustomToken).Data.Location;
		}

		private CombatData get_next_actor(CombatData current_actor)
		{
            this.combatState.InitiativeList.AdvanceNextTurn();
            return this.combatState.InitiativeList.CurrentActor;
		}

		private CreatureState get_state(CombatData cd)
		{
			EncounterSlot encounterSlot = this.fEncounter.FindSlot(cd);
			if (encounterSlot != null)
			{
				return encounterSlot.GetState(cd);
			}
			Hero hero = Session.Project.FindHero(cd.ID);
			if (hero != null)
			{
				return hero.GetState(cd.Damage);
			}
			if (this.fEncounter.FindTrap(cd.ID) != null)
			{
				return CreatureState.Active;
			}
			return CreatureState.Active;
		}

		private void handle_ended_effects(CombatData actor, bool beginning_of_turn)
		{
			if (actor == null)
			{
				return;
			}
			DurationType durationType = (beginning_of_turn ? DurationType.BeginningOfTurn : DurationType.EndOfTurn);
			List<Pair<CombatData, OngoingCondition>> pairs = new List<Pair<CombatData, OngoingCondition>>();
			foreach (EncounterSlot allSlot in this.fEncounter.AllSlots)
			{
				foreach (CombatData combatDatum in allSlot.CombatData)
				{
					foreach (OngoingCondition condition in combatDatum.Conditions)
					{
						if (condition.Duration != durationType || condition.DurationRound > this.fCurrentRound || !(actor.ID == condition.DurationCreatureID))
						{
							continue;
						}
						pairs.Add(new Pair<CombatData, OngoingCondition>(combatDatum, condition));
					}
				}
			}
			foreach (Hero hero in Session.Project.Heroes)
			{
				CombatData combatData = hero.CombatData;
				foreach (OngoingCondition ongoingCondition in combatData.Conditions)
				{
					if (ongoingCondition.Duration != durationType || ongoingCondition.DurationRound > this.fCurrentRound || !(actor.ID == ongoingCondition.DurationCreatureID))
					{
						continue;
					}
					pairs.Add(new Pair<CombatData, OngoingCondition>(combatData, ongoingCondition));
				}
			}
			foreach (Trap trap in this.fEncounter.Traps)
			{
                CombatData item = trap.CombatData;
				foreach (OngoingCondition condition1 in item.Conditions)
				{
					if (condition1.Duration != durationType || condition1.DurationRound > this.fCurrentRound || !(actor.ID == condition1.DurationCreatureID))
					{
						continue;
					}
					pairs.Add(new Pair<CombatData, OngoingCondition>(item, condition1));
				}
			}
			if (pairs.Count > 0)
			{
				(new EndedEffectsForm(pairs, this.fEncounter)).ShowDialog();
			}
		}

		private void handle_ongoing_damage(CombatData actor)
		{
			if (actor == null)
			{
				return;
			}
			List<OngoingCondition> ongoingConditions = new List<OngoingCondition>();
			foreach (OngoingCondition condition in actor.Conditions)
			{
				if (condition.Type != OngoingType.Damage || condition.Value <= 0)
				{
					continue;
				}
				ongoingConditions.Add(condition);
			}
			if (ongoingConditions.Count == 0)
			{
				return;
			}
			EncounterCard card = null;
			EncounterSlot encounterSlot = this.fEncounter.FindSlot(actor);
			if (encounterSlot != null)
			{
				card = encounterSlot.Card;
			}
			int damage = this.fCurrentActor.Damage;
			CreatureState state = CreatureState.Active;
			if (encounterSlot != null)
			{
				state = encounterSlot.GetState(actor);
			}
			if (encounterSlot == null)
			{
				Hero hero = Session.Project.FindHero(actor.ID);
				state = hero.GetState(damage);
			}
			if ((new OngoingDamageForm(actor, card, this.fEncounter)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				if (actor.Damage != damage)
				{
					this.fLog.AddDamageEntry(this.fCurrentActor.ID, this.fCurrentActor.Damage - damage, null);
				}
				if (encounterSlot == null)
				{
					Hero hero1 = Session.Project.FindHero(actor.ID);
					if (hero1.GetState(actor.Damage) != state)
					{
						this.fLog.AddStateEntry(actor.ID, hero1.GetState(actor.Damage));
					}
				}
				else if (encounterSlot.GetState(actor) != state)
				{
					this.fLog.AddStateEntry(actor.ID, encounterSlot.GetState(actor));
				}
			}
		}

		private void handle_recharge(CombatData actor)
		{
			if (actor == null)
			{
				return;
			}
			EncounterSlot encounterSlot = this.fEncounter.FindSlot(actor);
			if (encounterSlot == null)
			{
				return;
			}
			List<CreaturePower> creaturePowers = encounterSlot.Card.CreaturePowers;
			List<CreaturePower> creaturePowers1 = new List<CreaturePower>();
			foreach (Guid usedPower in this.fCurrentActor.UsedPowers)
			{
				foreach (CreaturePower creaturePower in creaturePowers)
				{
					if (!(creaturePower.ID == usedPower) || creaturePower.Action == null || !(creaturePower.Action.Recharge != ""))
					{
						continue;
					}
					creaturePowers1.Add(creaturePower);
				}
			}
			if (creaturePowers1.Count == 0)
			{
				return;
			}

            (new RechargeForm(actor, encounterSlot.Card)).ShowDialog();
		}

		private void handle_regen(CombatData actor)
		{
			if (actor == null)
			{
				return;
			}
			if (actor.Damage <= 0)
			{
				return;
			}
			EncounterSlot encounterSlot = this.fEncounter.FindSlot(actor);
			if (encounterSlot == null)
			{
				return;
			}
			Regeneration regeneration = new Regeneration()
			{
				Value = 0
			};
			if (encounterSlot.Card.Regeneration != null)
			{
				regeneration.Value = encounterSlot.Card.Regeneration.Value;
				regeneration.Details = encounterSlot.Card.Regeneration.Details;
			}
			foreach (OngoingCondition condition in actor.Conditions)
			{
				if (condition.Type != OngoingType.Regeneration)
				{
					continue;
				}
				regeneration.Value = Math.Max(regeneration.Value, condition.Regeneration.Value);
				if (condition.Regeneration.Details == "")
				{
					continue;
				}
				if (regeneration.Details != "")
				{
					Regeneration regeneration1 = regeneration;
					regeneration1.Details = string.Concat(regeneration1.Details, Environment.NewLine);
				}
				Regeneration regeneration2 = regeneration;
				regeneration2.Details = string.Concat(regeneration2.Details, condition.Regeneration.Details);
			}
			if (regeneration.Value == 0)
			{
				return;
			}
			string str = string.Concat(actor.DisplayName, " has regeneration:");
			str = string.Concat(str, Environment.NewLine, Environment.NewLine);
			object obj = str;
			object[] value = new object[] { obj, "Value: ", regeneration.Value, Environment.NewLine };
			str = string.Concat(value);
			if (regeneration.Details != "")
			{
				str = string.Concat(str, regeneration.Details, Environment.NewLine);
			}
			str = string.Concat(str, Environment.NewLine);
			str = string.Concat(str, "Do you want to apply it now?");
			if (MessageBox.Show(str, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.Yes)
			{
                Commands.CommandManager.GetInstance().ExecuteCommand(new Commands.Combat.HealEntitiesCommand(new List<CombatData>() { actor }, regeneration.Value, false));
			}
		}

		private void handle_saves()
		{
			if (this.fCurrentActor == null)
			{
				return;
			}
			if (this.fCurrentActor.Delaying)
			{
				return;
			}
			List<OngoingCondition> ongoingConditions = new List<OngoingCondition>();
			foreach (OngoingCondition condition in this.fCurrentActor.Conditions)
			{
				if (condition.Duration != DurationType.SaveEnds)
				{
					continue;
				}
				ongoingConditions.Add(condition);
			}
			if (ongoingConditions.Count == 0)
			{
				return;
			}
			EncounterCard card = null;
			EncounterSlot encounterSlot = this.fEncounter.FindSlot(this.fCurrentActor);
			if (encounterSlot != null)
			{
				card = encounterSlot.Card;
			}

            (new SavingThrowForm(this.fCurrentActor, card, this.fEncounter)).ShowDialog();
			
		}

		private void HealBtn_Click(object sender, EventArgs e)
		{
			try
			{
				this.do_heal(this.SelectedTokens);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void highlight_current_actor()
		{
			this.MapView.BoxedTokens.Clear();
			if (this.fCurrentActor != null)
			{
				Hero hero = Session.Project.FindHero(this.fCurrentActor.ID);
				if (hero == null)
				{
					EncounterSlot encounterSlot = this.fEncounter.FindSlot(this.fCurrentActor);
					if (encounterSlot != null)
					{
						CreatureToken creatureToken = new CreatureToken(encounterSlot.ID, this.fCurrentActor);
						this.MapView.BoxedTokens.Add(creatureToken);
					}
				}
				else
				{
					this.MapView.BoxedTokens.Add(hero);
				}
				this.MapView.MapChanged();
			}
		}

		private string html_encounter_overview()
		{
			List<string> strs = new List<string>()
			{
				"<P class=instruction>Select a combatant from the list to see its stat block here.</P>",
				"<P class=instruction></P>"
			};
			List<EncounterCard> encounterCards = new List<EncounterCard>();
			List<EncounterCard> encounterCards1 = new List<EncounterCard>();
			List<EncounterCard> encounterCards2 = new List<EncounterCard>();
			foreach (EncounterSlot allSlot in this.fEncounter.AllSlots)
			{
				if (allSlot.Card.Auras.Count != 0)
				{
					encounterCards.Add(allSlot.Card);
				}
				if (allSlot.Card.Tactics != "")
				{
					encounterCards1.Add(allSlot.Card);
				}
				bool flag = false;
				foreach (CreaturePower creaturePower in allSlot.Card.CreaturePowers)
				{
					if (creaturePower.Action == null || !(creaturePower.Action.Trigger != ""))
					{
						continue;
					}
					flag = true;
				}
				if (!flag)
				{
					continue;
				}
				encounterCards2.Add(allSlot.Card);
			}
			if (encounterCards.Count != 0 || encounterCards1.Count != 0 || encounterCards2.Count != 0)
			{
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				strs.Add("<TR class=heading>");
				strs.Add("<TD>");
				strs.Add("<B>Remember</B>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (encounterCards.Count != 0)
				{
					strs.Add("<TR class=shaded>");
					strs.Add("<TD>");
					strs.Add("<B>Auras</B>");
					strs.Add("</TD>");
					strs.Add("</TR>");
					foreach (EncounterCard encounterCard in encounterCards)
					{
						strs.Add("<TR>");
						strs.Add("<TD>");
						strs.Add(string.Concat("<B>", encounterCard.Title, "</B>"));
						strs.Add("</TD>");
						strs.Add("</TR>");
						foreach (Aura aura in encounterCard.Auras)
						{
							strs.Add("<TR>");
							strs.Add("<TD class=indent>");
							strs.Add(string.Concat("<B>", aura.Name, "</B>: ", aura.Details));
							strs.Add("</TD>");
							strs.Add("</TR>");
						}
					}
				}
				if (encounterCards1.Count != 0)
				{
					strs.Add("<TR class=shaded>");
					strs.Add("<TD>");
					strs.Add("<B>Tactics</B>");
					strs.Add("</TD>");
					strs.Add("</TR>");
					foreach (EncounterCard encounterCard1 in encounterCards1)
					{
						strs.Add("<TR>");
						strs.Add("<TD class=indent>");
						strs.Add(string.Concat("<B>", encounterCard1.Title, "</B>: ", encounterCard1.Tactics));
						strs.Add("</TD>");
						strs.Add("</TR>");
					}
				}
				if (encounterCards2.Count != 0)
				{
					strs.Add("<TR class=shaded>");
					strs.Add("<TD>");
					strs.Add("<B>Triggered Powers</B>");
					strs.Add("</TD>");
					strs.Add("</TR>");
					foreach (EncounterCard encounterCard2 in encounterCards2)
					{
						strs.Add("<TR>");
						strs.Add("<TD>");
						strs.Add(string.Concat("<B>", encounterCard2.Title, "</B>:"));
						strs.Add("</TD>");
						strs.Add("</TR>");
						foreach (CreaturePower creaturePower1 in encounterCard2.CreaturePowers)
						{
							if (creaturePower1.Action == null || creaturePower1.Action.Trigger == "")
							{
								continue;
							}
							strs.Add("<TR>");
							strs.Add("<TD class=indent>");
							strs.Add(string.Concat("<B>", creaturePower1.Name, "</B>: ", creaturePower1.Action.Trigger));
							strs.Add("</TD>");
							strs.Add("</TR>");
						}
					}
				}
				strs.Add("</TABLE>");
				strs.Add("</P>");
			}
			if (this.fEncounter.MapAreaID != Guid.Empty)
			{
				MapArea mapArea = this.MapView.Map.FindArea(this.fEncounter.MapAreaID);
				if (mapArea != null && mapArea.Details != "")
				{
					strs.Add(string.Concat("<P class=encounter_note><B>", HTML.Process(mapArea.Name, true), "</B>:</P>"));
					strs.Add(string.Concat("<P class=encounter_note>", HTML.Process(mapArea.Details, true), "</P>"));
				}
			}
			foreach (EncounterNote note in this.fEncounter.Notes)
			{
				if (note.Contents == "")
				{
					continue;
				}
				strs.Add(string.Concat("<P class=encounter_note><B>", HTML.Process(note.Title, true), "</B>:</P>"));
				strs.Add(string.Concat("<P class=encounter_note>", HTML.Process(note.Contents, false), "</P>"));
			}
			return HTML.Concatenate(strs);
		}

		private string html_encounter_start()
		{
			List<string> strs = new List<string>()
			{
				"<P class=table>",
				"<TABLE>",
				"<TR class=heading>",
				"<TD>",
				"<B>Starting the Encounter</B>",
				"</TD>",
				"</TR>"
			};
			string str = "automatically";
			string str1 = "manually";
			string str2 = "individually";
			string str3 = "in groups";
			string str4 = "calculated automatically";
			string str5 = "entered manually";
			string str6 = " (grouped by type)";
			strs.Add("<TR class=shaded>");
			strs.Add("<TD>");
			strs.Add("<B>How do you want to roll initiative?</B>");
			strs.Add("</TD>");
			strs.Add("</TR>");
			if (Session.Project.Heroes.Count != 0)
			{
				string str7 = "";
				string str8 = str;
				string str9 = str1;
				switch (Session.Preferences.HeroInitiativeMode)
				{
					case InitiativeMode.AutoGroup:
					case InitiativeMode.AutoIndividual:
					{
						str7 = str4;
						str9 = string.Concat("<A href=heroinit:manual>", str9, "</A>");
						break;
					}
					case InitiativeMode.ManualIndividual:
					case InitiativeMode.ManualGroup:
					{
						str7 = str5;
						str8 = string.Concat("<A href=heroinit:auto>", str8, "</A>");
						break;
					}
				}
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add(string.Concat("For <B>PCs</B>: ", str7));
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD class=indent>");
				strs.Add(string.Concat(str8, " / ", str9));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			if (this.fEncounter.Count != 0)
			{
				string str10 = "";
				string str11 = str;
				string str12 = str1;
				string str13 = str2;
				string str14 = str3;
				switch (Session.Preferences.InitiativeMode)
				{
					case InitiativeMode.AutoGroup:
					{
						str10 = string.Concat(str4, str6);
						str12 = string.Concat("<A href=creatureinit:manual>", str12, "</A>");
						str13 = string.Concat("<A href=creatureinit:individual>", str13, "</A>");
						break;
					}
					case InitiativeMode.AutoIndividual:
					{
						str10 = str4;
						str12 = string.Concat("<A href=creatureinit:manual>", str12, "</A>");
						str14 = string.Concat("<A href=creatureinit:group>", str14, "</A>");
						break;
					}
					case InitiativeMode.ManualIndividual:
					{
						str10 = str5;
						str11 = string.Concat("<A href=creatureinit:auto>", str11, "</A>");
						str14 = string.Concat("<A href=creatureinit:group>", str14, "</A>");
						break;
					}
					case InitiativeMode.ManualGroup:
					{
						str10 = string.Concat(str5, str6);
						str11 = string.Concat("<A href=creatureinit:auto>", str11, "</A>");
						str13 = string.Concat("<A href=creatureinit:individual>", str13, "</A>");
						break;
					}
				}
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add(string.Concat("For <B>creatures</B>: ", str10));
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD class=indent>");
				strs.Add(string.Concat(str11, " / ", str12));
				strs.Add("</TD>");
				strs.Add("</TR>");
				bool flag = false;
				foreach (EncounterSlot allSlot in this.fEncounter.AllSlots)
				{
					if (allSlot.CombatData.Count <= 1)
					{
						continue;
					}
					flag = true;
					break;
				}
				if (flag)
				{
					strs.Add("<TR>");
					strs.Add("<TD class=indent>");
					strs.Add(string.Concat(str13, " / ", str14));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
			}
			bool flag1 = false;
			foreach (Trap trap in this.fEncounter.Traps)
			{
				if (trap.Initiative == -2147483648)
				{
					continue;
				}
				flag1 = true;
				break;
			}
			if (flag1)
			{
				string str15 = "";
				string str16 = str;
				string str17 = str1;
				switch (Session.Preferences.TrapInitiativeMode)
				{
					case InitiativeMode.AutoGroup:
					case InitiativeMode.AutoIndividual:
					{
						str15 = str4;
						str17 = string.Concat("<A href=trapinit:manual>", str17, "</A>");
						break;
					}
					case InitiativeMode.ManualIndividual:
					case InitiativeMode.ManualGroup:
					{
						str15 = str5;
						str16 = string.Concat("<A href=trapinit:auto>", str16, "</A>");
						break;
					}
				}
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add(string.Concat("For <B>traps</B>: ", str15));
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD class=indent>");
				strs.Add(string.Concat(str16, " / ", str17));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			strs.Add("<TR class=shaded>");
			strs.Add("<TD>");
			strs.Add("<B>Preparing for the encounter</B>");
			strs.Add("</TD>");
			strs.Add("</TR>");
			strs.Add("<TR>");
			strs.Add("<TD>");
			strs.Add("<A href=combat:hp>Update PC hit points</A>");
			strs.Add("- if they've healed or taken damage since their last encounter");
			strs.Add("</TD>");
			strs.Add("</TR>");
			strs.Add("<TR>");
			strs.Add("<TD>");
			strs.Add("<A href=combat:rename>Rename combatants</A>");
			strs.Add("- if you need to indicate which mini is which creature");
			strs.Add("</TD>");
			strs.Add("</TR>");
			if (this.fEncounter.MapID != Guid.Empty)
			{
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add("Place PCs on the map - drag PCs from the list into their starting positions on the map");
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			bool flag2 = false;
			foreach (Hero hero in Session.Project.Heroes)
			{
				if (hero.Key == null || !(hero.Key != ""))
				{
					continue;
				}
				flag2 = true;
				break;
			}
			if (flag2)
			{
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add("<A href=combat:sync>Update iPlay4E characters</A>");
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			strs.Add("<TR class=shaded>");
			strs.Add("<TD>");
			strs.Add("<B>Everything ready?</B>");
			strs.Add("</TD>");
			strs.Add("</TR>");
			strs.Add("<TR>");
			strs.Add("<TD>");
			strs.Add("<A href=combat:start>Click here to roll initiative and start the encounter!</A>");
			strs.Add("</TD>");
			strs.Add("</TR>");
			strs.Add("</TABLE>");
			strs.Add("</P>");
			return HTML.Concatenate(strs);
		}

		private string html_skill_challenge()
		{
			return HTML.SkillChallenge(this.SelectedChallenge, true, false, DisplaySize.Small);
		}

		private string html_token(IToken token, bool full)
		{
			string str = "";
			if (token is Hero)
			{
				Hero hero = token as Hero;
				CombatData combatData = hero.CombatData;
				str = (!this.TwoColumnPreview || combatData != this.fCurrentActor ? HTML.StatBlock(hero, this.fEncounter, false, false, false, DisplaySize.Small) : "");
			}
			if (token is CreatureToken)
			{
				CreatureToken creatureToken = token as CreatureToken;
				EncounterSlot encounterSlot = this.fEncounter.FindSlot(creatureToken.SlotID);
				CombatData data = creatureToken.Data;
				str = (!this.TwoColumnPreview || data != this.fCurrentActor ? HTML.StatBlock(encounterSlot.Card, creatureToken.Data, this.fEncounter, false, false, full, CardMode.Combat, DisplaySize.Small) : "");
			}
			if (token is CustomToken)
			{
				CustomToken customToken = token as CustomToken;
				str = HTML.CustomMapToken(customToken, (this.fEncounter.MapID == Guid.Empty ? false : customToken.Data.Location == CombatData.NoPoint), false, DisplaySize.Small);
			}
			return str;
		}

		private string html_tokens(List<IToken> tokens)
		{
			string str = "";
			if (tokens.Count != 1)
			{
				List<string> strs = new List<string>();
				foreach (IToken token in tokens)
				{
					strs.Add(this.html_token(token, false));
				}
				str = HTML.Concatenate(strs);
			}
			else
			{
				str = this.html_token(tokens[0], true);
			}
			return str;
		}

		private string html_trap()
		{
			CombatData item = null;
            Trap trap = this.fEncounter.FindTrap(this.SelectedTrap.ID);
			if (trap != null)
			{
                item = trap.CombatData;
				if (this.TwoColumnPreview && item == this.fCurrentActor)
				{
					return "";
				}
			}
			return HTML.Trap(this.SelectedTrap, item, false, false, false, DisplaySize.Small);
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(CombatForm));
			ListViewGroup listViewGroup = new ListViewGroup("Combatants", HorizontalAlignment.Left);
			ListViewGroup listViewGroup1 = new ListViewGroup("Delayed / Readied", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Traps", HorizontalAlignment.Left);
			ListViewGroup listViewGroup3 = new ListViewGroup("Skill Challenges", HorizontalAlignment.Left);
			ListViewGroup listViewGroup4 = new ListViewGroup("Custom Tokens and Overlays", HorizontalAlignment.Left);
			ListViewGroup listViewGroup5 = new ListViewGroup("Not In Play", HorizontalAlignment.Left);
			ListViewGroup listViewGroup6 = new ListViewGroup("Defeated", HorizontalAlignment.Left);
			ListViewGroup listViewGroup7 = new ListViewGroup("Predefined", HorizontalAlignment.Left);
			ListViewGroup listViewGroup8 = new ListViewGroup("Custom Tokens", HorizontalAlignment.Left);
			ListViewGroup listViewGroup9 = new ListViewGroup("Custom Overlays", HorizontalAlignment.Left);
            ListViewGroup listViewGroup10 = new ListViewGroup("Terrain Layers", HorizontalAlignment.Left);
            this.Toolbar = new ToolStrip();
			this.DetailsBtn = new ToolStripButton();
			this.DamageBtn = new ToolStripButton();
			this.HealBtn = new ToolStripButton();
			this.EffectMenu = new ToolStripDropDownButton();
			this.effectToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator18 = new ToolStripSeparator();
            this.UndoBtn = new ToolStripButton();
            this.RedoBtn = new ToolStripButton();
            this.PrevInitBtn = new ToolStripButton();
			this.NextInitBtn = new ToolStripButton();
			this.DelayBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.CombatantsBtn = new ToolStripDropDownButton();
			this.CombatantsAdd = new ToolStripMenuItem();
			this.CombatantsAddToken = new ToolStripMenuItem();
			this.CombatantsAddOverlay = new ToolStripMenuItem();
			this.toolStripSeparator6 = new ToolStripSeparator();
			this.CombatantsRemove = new ToolStripMenuItem();
			this.toolStripSeparator12 = new ToolStripSeparator();
			this.CombatantsHideAll = new ToolStripMenuItem();
			this.CombatantsShowAll = new ToolStripMenuItem();
			this.toolStripSeparator26 = new ToolStripSeparator();
			this.CombatantsWaves = new ToolStripMenuItem();
			this.MapMenu = new ToolStripDropDownButton();
			this.ShowMap = new ToolStripMenuItem();
			this.toolStripSeparator10 = new ToolStripSeparator();
			this.MapFog = new ToolStripMenuItem();
			this.MapFogAllCreatures = new ToolStripMenuItem();
			this.MapFogVisibleCreatures = new ToolStripMenuItem();
			this.MapFogHideCreatures = new ToolStripMenuItem();
			this.toolStripSeparator15 = new ToolStripSeparator();
			this.MapLOS = new ToolStripMenuItem();
			this.MapGrid = new ToolStripMenuItem();
			this.MapGridLabels = new ToolStripMenuItem();
			this.MapHealth = new ToolStripMenuItem();
			this.MapConditions = new ToolStripMenuItem();
			this.MapPictureTokens = new ToolStripMenuItem();
			this.toolStripSeparator7 = new ToolStripSeparator();
			this.MapNavigate = new ToolStripMenuItem();
			this.MapReset = new ToolStripMenuItem();
			this.toolStripSeparator8 = new ToolStripSeparator();
			this.MapDrawing = new ToolStripMenuItem();
			this.MapClearDrawings = new ToolStripMenuItem();
			this.toolStripSeparator19 = new ToolStripSeparator();
			this.MapPrint = new ToolStripMenuItem();
			this.MapExport = new ToolStripMenuItem();
			this.PlayerViewMapMenu = new ToolStripDropDownButton();
			this.PlayerViewMap = new ToolStripMenuItem();
			this.PlayerViewInitList = new ToolStripMenuItem();
			this.toolStripSeparator9 = new ToolStripSeparator();
			this.PlayerViewFog = new ToolStripMenuItem();
			this.PlayerFogAll = new ToolStripMenuItem();
			this.PlayerFogVisible = new ToolStripMenuItem();
			this.PlayerFogNone = new ToolStripMenuItem();
			this.toolStripSeparator16 = new ToolStripSeparator();
			this.PlayerViewLOS = new ToolStripMenuItem();
			this.PlayerViewGrid = new ToolStripMenuItem();
			this.PlayerViewGridLabels = new ToolStripMenuItem();
			this.PlayerHealth = new ToolStripMenuItem();
			this.PlayerConditions = new ToolStripMenuItem();
			this.PlayerPictureTokens = new ToolStripMenuItem();
			this.toolStripSeparator17 = new ToolStripSeparator();
			this.PlayerLabels = new ToolStripMenuItem();
            this.PlayerLabelsRequireKnowledge = new ToolStripMenuItem();
            this.PlayerViewShowVisibility = new ToolStripMenuItem();
            this.PlayerViewUseDarkScheme = new ToolStripMenuItem();
            this.PlayerViewNoMapMenu = new ToolStripDropDownButton();
			this.PlayerViewNoMapShowInitiativeList = new ToolStripMenuItem();
			this.PlayerViewNoMapShowLabels = new ToolStripMenuItem();
			this.ToolsMenu = new ToolStripDropDownButton();
			this.ToolsEffects = new ToolStripMenuItem();
			this.ToolsLinks = new ToolStripMenuItem();
			this.toolStripSeparator11 = new ToolStripSeparator();
			this.ToolsAddIns = new ToolStripMenuItem();
			this.addinsToolStripMenuItem = new ToolStripMenuItem();
			this.OptionsMenu = new ToolStripDropDownButton();
			this.OptionsShowInit = new ToolStripMenuItem();
			this.toolStripSeparator13 = new ToolStripSeparator();
			this.OneColumn = new ToolStripMenuItem();
			this.TwoColumns = new ToolStripMenuItem();
			this.toolStripSeparator20 = new ToolStripSeparator();
			this.MapRight = new ToolStripMenuItem();
			this.MapBelow = new ToolStripMenuItem();
			this.toolStripSeparator21 = new ToolStripSeparator();
			this.OptionsLandscape = new ToolStripMenuItem();
			this.OptionsPortrait = new ToolStripMenuItem();
			this.toolStripSeparator5 = new ToolStripSeparator();
			this.ToolsAutoRemove = new ToolStripMenuItem();
			this.OptionsIPlay4e = new ToolStripMenuItem();
			this.toolStripSeparator23 = new ToolStripSeparator();
			this.ToolsColumns = new ToolStripMenuItem();
			this.ToolsColumnsInit = new ToolStripMenuItem();
			this.ToolsColumnsHP = new ToolStripMenuItem();
			this.ToolsColumnsDefences = new ToolStripMenuItem();
			this.ToolsColumnsConditions = new ToolStripMenuItem();
			this.MapSplitter = new SplitContainer();
			this.Pages = new TabControl();
			this.CombatantsPage = new TabPage();
			this.ListSplitter = new SplitContainer();
			this.CombatList = new CombatForm.CombatListControl();
			this.NameHdr = new ColumnHeader();
			this.InitHdr = new ColumnHeader();
			this.HPHdr = new ColumnHeader();
			this.DefHdr = new ColumnHeader();
			this.EffectsHdr = new ColumnHeader();
			this.ListContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ListDetails = new ToolStripMenuItem();
			this.toolStripSeparator14 = new ToolStripSeparator();
			this.ListDamage = new ToolStripMenuItem();
			this.ListHeal = new ToolStripMenuItem();
			this.ListCondition = new ToolStripMenuItem();
			this.effectToolStripMenuItem1 = new ToolStripMenuItem();
			this.ListRemoveEffect = new ToolStripMenuItem();
			this.effectToolStripMenuItem3 = new ToolStripMenuItem();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.ListRemove = new ToolStripMenuItem();
			this.ListRemoveMap = new ToolStripMenuItem();
			this.ListRemoveCombat = new ToolStripMenuItem();
			this.ListCreateCopy = new ToolStripMenuItem();
			this.toolStripSeparator4 = new ToolStripSeparator();
			this.ListVisible = new ToolStripMenuItem();
			this.ListDelay = new ToolStripMenuItem();
			this.PreviewPanel = new Panel();
			this.Preview = new WebBrowser();
			this.TemplatesPage = new TabPage();
			this.TemplateList = new ListView();
			this.TemplateHdr = new ColumnHeader();
			this.LogPage = new TabPage();
			this.LogBrowser = new WebBrowser();
			this.MapView = new Masterplan.Controls.MapView();
			this.MapContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.MapDetails = new ToolStripMenuItem();
			this.toolStripMenuItem2 = new ToolStripSeparator();
			this.MapDamage = new ToolStripMenuItem();
			this.MapHeal = new ToolStripMenuItem();
			this.MapAddEffect = new ToolStripMenuItem();
			this.effectToolStripMenuItem2 = new ToolStripMenuItem();
			this.MapRemoveEffect = new ToolStripMenuItem();
			this.effectToolStripMenuItem4 = new ToolStripMenuItem();
			this.MapSetPicture = new ToolStripMenuItem();
			this.toolStripMenuItem1 = new ToolStripSeparator();
			this.MapRemove = new ToolStripMenuItem();
			this.MapRemoveMap = new ToolStripMenuItem();
			this.MapRemoveCombat = new ToolStripMenuItem();
			this.MapCreateCopy = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.MapVisible = new ToolStripMenuItem();
			this.MapDelay = new ToolStripMenuItem();
			this.toolStripSeparator22 = new ToolStripSeparator();
			this.MapContextDrawing = new ToolStripMenuItem();
			this.MapContextClearDrawings = new ToolStripMenuItem();
			this.toolStripSeparator25 = new ToolStripSeparator();
			this.MapContextLOS = new ToolStripMenuItem();
			this.toolStripSeparator24 = new ToolStripSeparator();
			this.MapContextOverlay = new ToolStripMenuItem();
			this.ZoomGauge = new TrackBar();
			this.MapTooltip = new ToolTip(this.components);
			this.Statusbar = new StatusStrip();
			this.RoundLbl = new ToolStripStatusLabel();
			this.XPLbl = new ToolStripStatusLabel();
			this.LevelLbl = new ToolStripStatusLabel();
			this.MainPanel = new Panel();
			this.InitiativePanel = new Masterplan.Controls.InitiativePanel();
			this.CloseBtn = new Button();
			this.PauseBtn = new Button();
			this.InfoBtn = new Button();
			this.DieRollerBtn = new Button();
			this.ReportBtn = new Button();
			this.Toolbar.SuspendLayout();
			this.MapSplitter.Panel1.SuspendLayout();
			this.MapSplitter.Panel2.SuspendLayout();
			this.MapSplitter.SuspendLayout();
			this.Pages.SuspendLayout();
			this.CombatantsPage.SuspendLayout();
			this.ListSplitter.Panel1.SuspendLayout();
			this.ListSplitter.Panel2.SuspendLayout();
			this.ListSplitter.SuspendLayout();
			this.ListContext.SuspendLayout();
			this.PreviewPanel.SuspendLayout();
			this.TemplatesPage.SuspendLayout();
			this.LogPage.SuspendLayout();
			this.MapContext.SuspendLayout();
			((ISupportInitialize)this.ZoomGauge).BeginInit();
			this.Statusbar.SuspendLayout();
			this.MainPanel.SuspendLayout();
			base.SuspendLayout();
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] detailsBtn = new ToolStripItem[] { this.DetailsBtn, this.DamageBtn, this.HealBtn, this.EffectMenu, this.toolStripSeparator18, this.UndoBtn, this.RedoBtn, this.PrevInitBtn, this.NextInitBtn, this.DelayBtn, this.toolStripSeparator1, this.CombatantsBtn, this.MapMenu, this.PlayerViewMapMenu, this.PlayerViewNoMapMenu, this.ToolsMenu, this.OptionsMenu };
			items.AddRange(detailsBtn);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(850, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.DetailsBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DetailsBtn.Image = (Image)componentResourceManager.GetObject("DetailsBtn.Image");
			this.DetailsBtn.ImageTransparentColor = Color.Magenta;
			this.DetailsBtn.Name = "DetailsBtn";
			this.DetailsBtn.Size = new System.Drawing.Size(46, 22);
			this.DetailsBtn.Text = "Details";
			this.DetailsBtn.Click += new EventHandler(this.DetailsBtn_Click);
			this.DamageBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DamageBtn.Image = (Image)componentResourceManager.GetObject("DamageBtn.Image");
			this.DamageBtn.ImageTransparentColor = Color.Magenta;
			this.DamageBtn.Name = "DamageBtn";
			this.DamageBtn.Size = new System.Drawing.Size(55, 22);
			this.DamageBtn.Text = "Damage";
			this.DamageBtn.Click += new EventHandler(this.DamageBtn_Click);
			this.HealBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.HealBtn.Image = (Image)componentResourceManager.GetObject("HealBtn.Image");
			this.HealBtn.ImageTransparentColor = Color.Magenta;
			this.HealBtn.Name = "HealBtn";
			this.HealBtn.Size = new System.Drawing.Size(35, 22);
			this.HealBtn.Text = "Heal";
			this.HealBtn.Click += new EventHandler(this.HealBtn_Click);
			this.EffectMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EffectMenu.DropDownItems.AddRange(new ToolStripItem[] { this.effectToolStripMenuItem });
			this.EffectMenu.Image = (Image)componentResourceManager.GetObject("EffectMenu.Image");
			this.EffectMenu.ImageTransparentColor = Color.Magenta;
			this.EffectMenu.Name = "EffectMenu";
			this.EffectMenu.Size = new System.Drawing.Size(75, 22);
			this.EffectMenu.Text = "Add Effect";
			this.EffectMenu.DropDownOpening += new EventHandler(this.EffectMenu_DropDownOpening);
			this.effectToolStripMenuItem.Name = "effectToolStripMenuItem";
			this.effectToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.effectToolStripMenuItem.Text = "[effect]";
			this.toolStripSeparator18.Name = "toolStripSeparator18";
			this.toolStripSeparator18.Size = new System.Drawing.Size(6, 25);

            this.UndoBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.UndoBtn.Image = (Image)componentResourceManager.GetObject("UndoBtn.Image");
            this.UndoBtn.ImageTransparentColor = Color.Magenta;
            this.UndoBtn.Name = "UndoBtn";
            this.UndoBtn.Size = new System.Drawing.Size(63, 22);
            this.UndoBtn.Text = "Undo";
            this.UndoBtn.Click += new EventHandler(this.UndoBtn_Click);

            this.RedoBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.RedoBtn.Image = (Image)componentResourceManager.GetObject("RedoBtn.Image");
            this.RedoBtn.ImageTransparentColor = Color.Magenta;
            this.RedoBtn.Name = "RedoBtn";
            this.RedoBtn.Size = new System.Drawing.Size(63, 22);
            this.RedoBtn.Text = "Redo";
            this.RedoBtn.Click += new EventHandler(this.RedoBtn_Click);


            this.PrevInitBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.PrevInitBtn.Image = (Image)componentResourceManager.GetObject("PrevInitBtn.Image");
            this.PrevInitBtn.ImageTransparentColor = Color.Magenta;
            this.PrevInitBtn.Name = "PrevInitBtn";
            this.PrevInitBtn.Size = new System.Drawing.Size(63, 22);
            this.PrevInitBtn.Text = "Prev Turn";
            this.PrevInitBtn.Click += new EventHandler(this.PrevInitBtn_Click);

            this.NextInitBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NextInitBtn.Image = (Image)componentResourceManager.GetObject("NextInitBtn.Image");
			this.NextInitBtn.ImageTransparentColor = Color.Magenta;
			this.NextInitBtn.Name = "NextInitBtn";
			this.NextInitBtn.Size = new System.Drawing.Size(63, 22);
			this.NextInitBtn.Text = "Next Turn";
			this.NextInitBtn.Click += new EventHandler(this.NextInitBtn_Click);
			this.DelayBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DelayBtn.Image = (Image)componentResourceManager.GetObject("DelayBtn.Image");
			this.DelayBtn.ImageTransparentColor = Color.Magenta;
			this.DelayBtn.Name = "DelayBtn";
			this.DelayBtn.Size = new System.Drawing.Size(78, 22);
			this.DelayBtn.Text = "Delay Action";
			this.DelayBtn.Click += new EventHandler(this.DelayBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.CombatantsBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems = this.CombatantsBtn.DropDownItems;
			ToolStripItem[] combatantsAdd = new ToolStripItem[] { this.CombatantsAdd, this.CombatantsAddToken, this.CombatantsAddOverlay, this.toolStripSeparator6, this.CombatantsWaves, this.toolStripSeparator26, this.CombatantsRemove, this.toolStripSeparator12, this.CombatantsHideAll, this.CombatantsShowAll };
			dropDownItems.AddRange(combatantsAdd);
			this.CombatantsBtn.Image = (Image)componentResourceManager.GetObject("CombatantsBtn.Image");
			this.CombatantsBtn.ImageTransparentColor = Color.Magenta;
			this.CombatantsBtn.Name = "CombatantsBtn";
			this.CombatantsBtn.Size = new System.Drawing.Size(85, 22);
			this.CombatantsBtn.Text = "Combatants";
			this.CombatantsBtn.DropDownOpening += new EventHandler(this.CombatantsBtn_DropDownOpening);
			this.CombatantsAdd.Name = "CombatantsAdd";
			this.CombatantsAdd.Size = new System.Drawing.Size(175, 22);
			this.CombatantsAdd.Text = "Add Combatant...";
			this.CombatantsAdd.Click += new EventHandler(this.CombatantsAdd_Click);
			this.CombatantsAddToken.Name = "CombatantsAddToken";
			this.CombatantsAddToken.Size = new System.Drawing.Size(175, 22);
			this.CombatantsAddToken.Text = "Add Map Token...";
			this.CombatantsAddToken.Click += new EventHandler(this.CombatantsAddCustom_Click);
			this.CombatantsAddOverlay.Name = "CombatantsAddOverlay";
			this.CombatantsAddOverlay.Size = new System.Drawing.Size(175, 22);
			this.CombatantsAddOverlay.Text = "Add Map Overlay...";
			this.CombatantsAddOverlay.Click += new EventHandler(this.CombatantsAddOverlay_Click);
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(172, 6);
			this.CombatantsRemove.Name = "CombatantsRemove";
			this.CombatantsRemove.Size = new System.Drawing.Size(175, 22);
			this.CombatantsRemove.Text = "Remove Selected";
			this.CombatantsRemove.Click += new EventHandler(this.CombatantsRemove_Click);
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new System.Drawing.Size(172, 6);
			this.CombatantsHideAll.Name = "CombatantsHideAll";
			this.CombatantsHideAll.Size = new System.Drawing.Size(175, 22);
			this.CombatantsHideAll.Text = "Hide All";
			this.CombatantsHideAll.Click += new EventHandler(this.CombatantsHideAll_Click);
			this.CombatantsShowAll.Name = "CombatantsShowAll";
			this.CombatantsShowAll.Size = new System.Drawing.Size(175, 22);
			this.CombatantsShowAll.Text = "Show All";
			this.CombatantsShowAll.Click += new EventHandler(this.CombatantsShowAll_Click);
			this.toolStripSeparator26.Name = "toolStripSeparator26";
			this.toolStripSeparator26.Size = new System.Drawing.Size(172, 6);
			this.CombatantsWaves.Name = "CombatantsWaves";
			this.CombatantsWaves.Size = new System.Drawing.Size(175, 22);
			this.CombatantsWaves.Text = "Waves";
			this.MapMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection toolStripItemCollections = this.MapMenu.DropDownItems;
			ToolStripItem[] showMap = new ToolStripItem[] { this.ShowMap, this.toolStripSeparator10, this.MapFog, this.toolStripSeparator15, this.MapLOS, this.MapGrid, this.MapGridLabels, this.MapHealth, this.MapConditions, this.MapPictureTokens, this.toolStripSeparator7, this.MapNavigate, this.MapReset, this.toolStripSeparator8, this.MapDrawing, this.MapClearDrawings, this.toolStripSeparator19, this.MapPrint, this.MapExport };
			toolStripItemCollections.AddRange(showMap);
			this.MapMenu.Image = (Image)componentResourceManager.GetObject("MapMenu.Image");
			this.MapMenu.ImageTransparentColor = Color.Magenta;
			this.MapMenu.Name = "MapMenu";
			this.MapMenu.Size = new System.Drawing.Size(44, 22);
			this.MapMenu.Text = "Map";
			this.MapMenu.DropDownOpening += new EventHandler(this.MapMenu_DropDownOpening);
			this.ShowMap.Name = "ShowMap";
			this.ShowMap.Size = new System.Drawing.Size(184, 22);
			this.ShowMap.Text = "Show Map";
			this.ShowMap.Click += new EventHandler(this.ShowMap_Click);
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(181, 6);
			this.MapFog.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems1 = this.MapFog.DropDownItems;
			ToolStripItem[] mapFogAllCreatures = new ToolStripItem[] { this.MapFogAllCreatures, this.MapFogVisibleCreatures, this.MapFogHideCreatures };
			dropDownItems1.AddRange(mapFogAllCreatures);
			this.MapFog.Image = (Image)componentResourceManager.GetObject("MapFog.Image");
			this.MapFog.ImageTransparentColor = Color.Magenta;
			this.MapFog.Name = "MapFog";
			this.MapFog.Size = new System.Drawing.Size(184, 22);
			this.MapFog.Text = "Fog of War";
			this.MapFogAllCreatures.Name = "MapFogAllCreatures";
			this.MapFogAllCreatures.Size = new System.Drawing.Size(221, 22);
			this.MapFogAllCreatures.Text = "Show All Creatures";
			this.MapFogAllCreatures.Click += new EventHandler(this.MapFogAllCreatures_Click);
			this.MapFogVisibleCreatures.Name = "MapFogVisibleCreatures";
			this.MapFogVisibleCreatures.Size = new System.Drawing.Size(221, 22);
			this.MapFogVisibleCreatures.Text = "Show Visible Creatures Only";
			this.MapFogVisibleCreatures.Click += new EventHandler(this.MapFogVisibleCreatures_Click);
			this.MapFogHideCreatures.Name = "MapFogHideCreatures";
			this.MapFogHideCreatures.Size = new System.Drawing.Size(221, 22);
			this.MapFogHideCreatures.Text = "Hide All Creatures";
			this.MapFogHideCreatures.Click += new EventHandler(this.MapFogHideCreatures_Click);
			this.toolStripSeparator15.Name = "toolStripSeparator15";
			this.toolStripSeparator15.Size = new System.Drawing.Size(181, 6);
			this.MapLOS.Name = "MapLOS";
			this.MapLOS.Size = new System.Drawing.Size(184, 22);
			this.MapLOS.Text = "Show Line of Sight";
			this.MapLOS.Click += new EventHandler(this.MapLOS_Click);
			this.MapGrid.Name = "MapGrid";
			this.MapGrid.Size = new System.Drawing.Size(184, 22);
			this.MapGrid.Text = "Show Grid";
			this.MapGrid.Click += new EventHandler(this.MapGrid_Click);
			this.MapGridLabels.Name = "MapGridLabels";
			this.MapGridLabels.Size = new System.Drawing.Size(184, 22);
			this.MapGridLabels.Text = "Show Grid Labels";
			this.MapGridLabels.Click += new EventHandler(this.MapGridLabels_Click);
			this.MapHealth.Name = "MapHealth";
			this.MapHealth.Size = new System.Drawing.Size(184, 22);
			this.MapHealth.Text = "Show Health Bars";
			this.MapHealth.Click += new EventHandler(this.MapHealth_Click);
			this.MapConditions.Name = "MapConditions";
			this.MapConditions.Size = new System.Drawing.Size(184, 22);
			this.MapConditions.Text = "Show Conditions";
			this.MapConditions.Click += new EventHandler(this.MapConditions_Click);
			this.MapPictureTokens.Name = "MapPictureTokens";
			this.MapPictureTokens.Size = new System.Drawing.Size(184, 22);
			this.MapPictureTokens.Text = "Show Picture Tokens";
			this.MapPictureTokens.Click += new EventHandler(this.MapPictureTokens_Click);
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(181, 6);
			this.MapNavigate.Name = "MapNavigate";
			this.MapNavigate.Size = new System.Drawing.Size(184, 22);
			this.MapNavigate.Text = "Scroll and Zoom";
			this.MapNavigate.Click += new EventHandler(this.MapNavigate_Click);
			this.MapReset.Name = "MapReset";
			this.MapReset.Size = new System.Drawing.Size(184, 22);
			this.MapReset.Text = "Reset View";
			this.MapReset.Click += new EventHandler(this.MapReset_Click);
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(181, 6);
			this.MapDrawing.Name = "MapDrawing";
			this.MapDrawing.Size = new System.Drawing.Size(184, 22);
			this.MapDrawing.Text = "Allow Drawing";
			this.MapDrawing.Click += new EventHandler(this.MapDrawing_Click);
			this.MapClearDrawings.Name = "MapClearDrawings";
			this.MapClearDrawings.Size = new System.Drawing.Size(184, 22);
			this.MapClearDrawings.Text = "Clear Drawings";
			this.MapClearDrawings.Click += new EventHandler(this.MapClearDrawings_Click);
			this.toolStripSeparator19.Name = "toolStripSeparator19";
			this.toolStripSeparator19.Size = new System.Drawing.Size(181, 6);
			this.MapPrint.Name = "MapPrint";
			this.MapPrint.Size = new System.Drawing.Size(184, 22);
			this.MapPrint.Text = "Print";
			this.MapPrint.Click += new EventHandler(this.MapPrint_Click);
			this.MapExport.Name = "MapExport";
			this.MapExport.Size = new System.Drawing.Size(184, 22);
			this.MapExport.Text = "Export Screenshot";
			this.MapExport.Click += new EventHandler(this.MapExport_Click);
			this.PlayerViewMapMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection toolStripItemCollections1 = this.PlayerViewMapMenu.DropDownItems;
			ToolStripItem[] playerViewMap = new ToolStripItem[] { this.PlayerViewMap, this.PlayerViewInitList, this.toolStripSeparator9, this.PlayerViewFog, this.toolStripSeparator16, this.PlayerViewLOS, this.PlayerViewGrid, this.PlayerViewGridLabels, this.PlayerHealth, this.PlayerConditions, this.PlayerPictureTokens, this.toolStripSeparator17, this.PlayerLabels, this.PlayerLabelsRequireKnowledge, this.PlayerViewShowVisibility, this.PlayerViewUseDarkScheme};
			toolStripItemCollections1.AddRange(playerViewMap);
			this.PlayerViewMapMenu.Image = (Image)componentResourceManager.GetObject("PlayerViewMapMenu.Image");
			this.PlayerViewMapMenu.ImageTransparentColor = Color.Magenta;
			this.PlayerViewMapMenu.Name = "PlayerViewMapMenu";
			this.PlayerViewMapMenu.Size = new System.Drawing.Size(80, 22);
			this.PlayerViewMapMenu.Text = "Player View";
			this.PlayerViewMapMenu.DropDownOpening += new EventHandler(this.PlayerViewMapMenu_DropDownOpening);
			this.PlayerViewMap.Name = "PlayerViewMap";
			this.PlayerViewMap.Size = new System.Drawing.Size(215, 22);
			this.PlayerViewMap.Text = "Show Map";
			this.PlayerViewMap.Click += new EventHandler(this.PlayerViewMap_Click);
			this.PlayerViewInitList.Name = "PlayerViewInitList";
			this.PlayerViewInitList.Size = new System.Drawing.Size(215, 22);
			this.PlayerViewInitList.Text = "Show Initiative List";
			this.PlayerViewInitList.Click += new EventHandler(this.PlayerViewInitList_Click);
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(212, 6);
			this.PlayerViewFog.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems2 = this.PlayerViewFog.DropDownItems;
			ToolStripItem[] playerFogAll = new ToolStripItem[] { this.PlayerFogAll, this.PlayerFogVisible, this.PlayerFogNone };
			dropDownItems2.AddRange(playerFogAll);
			this.PlayerViewFog.Image = (Image)componentResourceManager.GetObject("PlayerViewFog.Image");
			this.PlayerViewFog.ImageTransparentColor = Color.Magenta;
			this.PlayerViewFog.Name = "PlayerViewFog";
			this.PlayerViewFog.Size = new System.Drawing.Size(215, 22);
			this.PlayerViewFog.Text = "Fog of War";
			this.PlayerFogAll.Name = "PlayerFogAll";
			this.PlayerFogAll.Size = new System.Drawing.Size(221, 22);
			this.PlayerFogAll.Text = "Show All Creatures";
			this.PlayerFogAll.Click += new EventHandler(this.PlayerFogAll_Click);
			this.PlayerFogVisible.Name = "PlayerFogVisible";
			this.PlayerFogVisible.Size = new System.Drawing.Size(221, 22);
			this.PlayerFogVisible.Text = "Show Visible Creatures Only";
			this.PlayerFogVisible.Click += new EventHandler(this.PlayerFogVisible_Click);
			this.PlayerFogNone.Name = "PlayerFogNone";
			this.PlayerFogNone.Size = new System.Drawing.Size(221, 22);
			this.PlayerFogNone.Text = "Hide All Creatures";
			this.PlayerFogNone.Click += new EventHandler(this.PlayerFogNone_Click);
			this.toolStripSeparator16.Name = "toolStripSeparator16";
			this.toolStripSeparator16.Size = new System.Drawing.Size(212, 6);
			this.PlayerViewLOS.Name = "PlayerViewLOS";
			this.PlayerViewLOS.Size = new System.Drawing.Size(215, 22);
			this.PlayerViewLOS.Text = "Show Line of Sight";
			this.PlayerViewLOS.Click += new EventHandler(this.PlayerViewLOS_Click);
			this.PlayerViewGrid.Name = "PlayerViewGrid";
			this.PlayerViewGrid.Size = new System.Drawing.Size(215, 22);
			this.PlayerViewGrid.Text = "Show Grid";
			this.PlayerViewGrid.Click += new EventHandler(this.PlayerViewGrid_Click);
			this.PlayerViewGridLabels.Name = "PlayerViewGridLabels";
			this.PlayerViewGridLabels.Size = new System.Drawing.Size(215, 22);
			this.PlayerViewGridLabels.Text = "Show Grid Labels";
			this.PlayerViewGridLabels.Click += new EventHandler(this.PlayerViewGridLabels_Click);
			this.PlayerHealth.Name = "PlayerHealth";
			this.PlayerHealth.Size = new System.Drawing.Size(215, 22);
			this.PlayerHealth.Text = "Show Health Bars";
			this.PlayerHealth.Click += new EventHandler(this.PlayerHealth_Click);
			this.PlayerConditions.Name = "PlayerConditions";
			this.PlayerConditions.Size = new System.Drawing.Size(215, 22);
			this.PlayerConditions.Text = "Show Conditions";
			this.PlayerConditions.Click += new EventHandler(this.PlayerConditions_Click);
			this.PlayerPictureTokens.Name = "PlayerPictureTokens";
			this.PlayerPictureTokens.Size = new System.Drawing.Size(215, 22);
			this.PlayerPictureTokens.Text = "Show Picture Tokens";
			this.PlayerPictureTokens.Click += new EventHandler(this.PlayerPictureTokens_Click);
			this.toolStripSeparator17.Name = "toolStripSeparator17";
			this.toolStripSeparator17.Size = new System.Drawing.Size(212, 6);
			this.PlayerLabels.Name = "PlayerLabels";
			this.PlayerLabels.Size = new System.Drawing.Size(215, 22);
			this.PlayerLabels.Text = "Show Detailed Information";
			this.PlayerLabels.Click += new EventHandler(this.PlayerLabels_Click);
            this.PlayerLabelsRequireKnowledge.Name = "PlayerLabelsRequireKnowledge";
            this.PlayerLabelsRequireKnowledge.Size = new System.Drawing.Size(215, 22);
            this.PlayerLabelsRequireKnowledge.Text = "Require Knowledge Checks For Info";
            this.PlayerLabelsRequireKnowledge.Click += new EventHandler(this.PlayerLabelsRequireKnowledge_Click);

            this.PlayerViewShowVisibility.Name = "PlayerViewVisibility";
            this.PlayerViewShowVisibility.Size = new System.Drawing.Size(215, 22);
            this.PlayerViewShowVisibility.Text = "Render Player Visibility";
            this.PlayerViewShowVisibility.Click += new EventHandler(this.PlayerViewVisibility_Click);

            this.PlayerViewUseDarkScheme.Name = "PlayerViewUseDarkScheme";
            this.PlayerViewUseDarkScheme.Size = new System.Drawing.Size(215, 22);
            this.PlayerViewUseDarkScheme.Text = "Use Dark Scheme For Visibility";
            this.PlayerViewUseDarkScheme.Click += new EventHandler(this.PlayerViewUseDarkScheme_Click);


            this.PlayerViewNoMapMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection toolStripItemCollections2 = this.PlayerViewNoMapMenu.DropDownItems;
			ToolStripItem[] playerViewNoMapShowInitiativeList = new ToolStripItem[] { this.PlayerViewNoMapShowInitiativeList, this.PlayerViewNoMapShowLabels };
			toolStripItemCollections2.AddRange(playerViewNoMapShowInitiativeList);
			this.PlayerViewNoMapMenu.Image = (Image)componentResourceManager.GetObject("PlayerViewNoMapMenu.Image");
			this.PlayerViewNoMapMenu.ImageTransparentColor = Color.Magenta;
			this.PlayerViewNoMapMenu.Name = "PlayerViewNoMapMenu";
			this.PlayerViewNoMapMenu.Size = new System.Drawing.Size(80, 22);
			this.PlayerViewNoMapMenu.Text = "Player View";
			this.PlayerViewNoMapMenu.DropDownOpening += new EventHandler(this.PlayerViewNoMapMenu_DropDownOpening);
			this.PlayerViewNoMapShowInitiativeList.Name = "PlayerViewNoMapShowInitiativeList";
			this.PlayerViewNoMapShowInitiativeList.Size = new System.Drawing.Size(215, 22);
			this.PlayerViewNoMapShowInitiativeList.Text = "Show Initiative List";
			this.PlayerViewNoMapShowInitiativeList.Click += new EventHandler(this.PlayerViewNoMapShowInitiativeList_Click);
			this.PlayerViewNoMapShowLabels.Name = "PlayerViewNoMapShowLabels";
			this.PlayerViewNoMapShowLabels.Size = new System.Drawing.Size(215, 22);
			this.PlayerViewNoMapShowLabels.Text = "Show Detailed Information";
			this.PlayerViewNoMapShowLabels.Click += new EventHandler(this.PlayerViewNoMapShowLabels_Click);
			this.ToolsMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems3 = this.ToolsMenu.DropDownItems;
			ToolStripItem[] toolsEffects = new ToolStripItem[] { this.ToolsEffects, this.ToolsLinks, this.toolStripSeparator11, this.ToolsAddIns };
			dropDownItems3.AddRange(toolsEffects);
			this.ToolsMenu.Image = (Image)componentResourceManager.GetObject("ToolsMenu.Image");
			this.ToolsMenu.ImageTransparentColor = Color.Magenta;
			this.ToolsMenu.Name = "ToolsMenu";
			this.ToolsMenu.Size = new System.Drawing.Size(49, 22);
			this.ToolsMenu.Text = "Tools";
			this.ToolsMenu.Click += new EventHandler(this.ToolsMenu_DopDownOpening);
			this.ToolsEffects.Name = "ToolsEffects";
			this.ToolsEffects.Size = new System.Drawing.Size(159, 22);
			this.ToolsEffects.Text = "Ongoing Effects";
			this.ToolsEffects.Click += new EventHandler(this.CombatantsEffects_Click);
			this.ToolsLinks.Name = "ToolsLinks";
			this.ToolsLinks.Size = new System.Drawing.Size(159, 22);
			this.ToolsLinks.Text = "Token Links";
			this.ToolsLinks.Click += new EventHandler(this.CombatantsLinks_Click);
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new System.Drawing.Size(156, 6);
			this.ToolsAddIns.DropDownItems.AddRange(new ToolStripItem[] { this.addinsToolStripMenuItem });
			this.ToolsAddIns.Name = "ToolsAddIns";
			this.ToolsAddIns.Size = new System.Drawing.Size(159, 22);
			this.ToolsAddIns.Text = "Add-Ins";
			this.addinsToolStripMenuItem.Name = "addinsToolStripMenuItem";
			this.addinsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.addinsToolStripMenuItem.Text = "[add-ins]";
			this.OptionsMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection toolStripItemCollections3 = this.OptionsMenu.DropDownItems;
			ToolStripItem[] optionsShowInit = new ToolStripItem[] { this.OptionsShowInit, this.toolStripSeparator13, this.OneColumn, this.TwoColumns, this.toolStripSeparator20, this.MapRight, this.MapBelow, this.toolStripSeparator21, this.OptionsLandscape, this.OptionsPortrait, this.toolStripSeparator5, this.ToolsAutoRemove, this.OptionsIPlay4e, this.toolStripSeparator23, this.ToolsColumns };
			toolStripItemCollections3.AddRange(optionsShowInit);
			this.OptionsMenu.Image = (Image)componentResourceManager.GetObject("OptionsMenu.Image");
			this.OptionsMenu.ImageTransparentColor = Color.Magenta;
			this.OptionsMenu.Name = "OptionsMenu";
			this.OptionsMenu.Size = new System.Drawing.Size(62, 22);
			this.OptionsMenu.Text = "Options";
			this.OptionsMenu.DropDownOpening += new EventHandler(this.OptionsMenu_DropDownOpening);
			this.OptionsShowInit.Name = "OptionsShowInit";
			this.OptionsShowInit.Size = new System.Drawing.Size(229, 22);
			this.OptionsShowInit.Text = "Show Initiative Gauge";
			this.OptionsShowInit.Click += new EventHandler(this.OptionsShowInit_Click);
			this.toolStripSeparator13.Name = "toolStripSeparator13";
			this.toolStripSeparator13.Size = new System.Drawing.Size(226, 6);
			this.OneColumn.Name = "OneColumn";
			this.OneColumn.Size = new System.Drawing.Size(229, 22);
			this.OneColumn.Text = "One Column";
			this.OneColumn.Click += new EventHandler(this.OneColumn_Click);
			this.TwoColumns.Name = "TwoColumns";
			this.TwoColumns.Size = new System.Drawing.Size(229, 22);
			this.TwoColumns.Text = "Two Columns";
			this.TwoColumns.Click += new EventHandler(this.TwoColumns_Click);
			this.toolStripSeparator20.Name = "toolStripSeparator20";
			this.toolStripSeparator20.Size = new System.Drawing.Size(226, 6);
			this.MapRight.Name = "MapRight";
			this.MapRight.Size = new System.Drawing.Size(229, 22);
			this.MapRight.Text = "Map at Right";
			this.MapRight.Click += new EventHandler(this.OptionsMapRight_Click);
			this.MapBelow.Name = "MapBelow";
			this.MapBelow.Size = new System.Drawing.Size(229, 22);
			this.MapBelow.Text = "Map Below";
			this.MapBelow.Click += new EventHandler(this.OptionsMapBelow_Click);
			this.toolStripSeparator21.Name = "toolStripSeparator21";
			this.toolStripSeparator21.Size = new System.Drawing.Size(226, 6);
			this.OptionsLandscape.Name = "OptionsLandscape";
			this.OptionsLandscape.Size = new System.Drawing.Size(229, 22);
			this.OptionsLandscape.Text = "Landscape";
			this.OptionsLandscape.Click += new EventHandler(this.OptionsLandscape_Click);
			this.OptionsPortrait.Name = "OptionsPortrait";
			this.OptionsPortrait.Size = new System.Drawing.Size(229, 22);
			this.OptionsPortrait.Text = "Portrait";
			this.OptionsPortrait.Click += new EventHandler(this.OptionsPortrait_Click);
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(226, 6);
			this.ToolsAutoRemove.Name = "ToolsAutoRemove";
			this.ToolsAutoRemove.Size = new System.Drawing.Size(229, 22);
			this.ToolsAutoRemove.Text = "Remove Defeated Opponents";
			this.ToolsAutoRemove.Click += new EventHandler(this.ToolsAutoRemove_Click);
			this.OptionsIPlay4e.Name = "OptionsIPlay4e";
			this.OptionsIPlay4e.Size = new System.Drawing.Size(229, 22);
			this.OptionsIPlay4e.Text = "iPlay4e Integration";
			this.OptionsIPlay4e.Click += new EventHandler(this.OptionsIPlay4e_Click);
			this.toolStripSeparator23.Name = "toolStripSeparator23";
			this.toolStripSeparator23.Size = new System.Drawing.Size(226, 6);
			ToolStripItemCollection dropDownItems4 = this.ToolsColumns.DropDownItems;
			ToolStripItem[] toolsColumnsInit = new ToolStripItem[] { this.ToolsColumnsInit, this.ToolsColumnsHP, this.ToolsColumnsDefences, this.ToolsColumnsConditions };
			dropDownItems4.AddRange(toolsColumnsInit);
			this.ToolsColumns.Name = "ToolsColumns";
			this.ToolsColumns.Size = new System.Drawing.Size(229, 22);
			this.ToolsColumns.Text = "Columns";
			this.ToolsColumns.DropDownOpening += new EventHandler(this.ToolsColumns_DropDownOpening);
			this.ToolsColumnsInit.Name = "ToolsColumnsInit";
			this.ToolsColumnsInit.Size = new System.Drawing.Size(126, 22);
			this.ToolsColumnsInit.Text = "Initiative";
			this.ToolsColumnsInit.Click += new EventHandler(this.ToolsColumnsInit_Click);
			this.ToolsColumnsHP.Name = "ToolsColumnsHP";
			this.ToolsColumnsHP.Size = new System.Drawing.Size(126, 22);
			this.ToolsColumnsHP.Text = "Hit Points";
			this.ToolsColumnsHP.Click += new EventHandler(this.ToolsColumnsHP_Click);
			this.ToolsColumnsDefences.Name = "ToolsColumnsDefences";
			this.ToolsColumnsDefences.Size = new System.Drawing.Size(126, 22);
			this.ToolsColumnsDefences.Text = "Defences";
			this.ToolsColumnsDefences.Click += new EventHandler(this.ToolsColumnsDefences_Click);
			this.ToolsColumnsConditions.Name = "ToolsColumnsConditions";
			this.ToolsColumnsConditions.Size = new System.Drawing.Size(126, 22);
			this.ToolsColumnsConditions.Text = "Effects";
			this.ToolsColumnsConditions.Click += new EventHandler(this.ToolsColumnsConditions_Click);
			this.MapSplitter.Dock = DockStyle.Fill;
			this.MapSplitter.FixedPanel = FixedPanel.Panel1;
			this.MapSplitter.Location = new Point(0, 0);
			this.MapSplitter.Name = "MapSplitter";
			this.MapSplitter.Panel1.Controls.Add(this.Pages);
			this.MapSplitter.Panel2.Controls.Add(this.MapView);
			this.MapSplitter.Panel2.Controls.Add(this.ZoomGauge);
			this.MapSplitter.Size = new System.Drawing.Size(786, 362);
			this.MapSplitter.SplitterDistance = 368;
			this.MapSplitter.TabIndex = 1;
			this.Pages.Controls.Add(this.CombatantsPage);
			this.Pages.Controls.Add(this.TemplatesPage);
			this.Pages.Controls.Add(this.LogPage);
			this.Pages.Dock = DockStyle.Fill;
			this.Pages.Location = new Point(0, 0);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new System.Drawing.Size(368, 362);
			this.Pages.TabIndex = 2;
			this.CombatantsPage.Controls.Add(this.ListSplitter);
			this.CombatantsPage.Location = new Point(4, 22);
			this.CombatantsPage.Name = "CombatantsPage";
			this.CombatantsPage.Padding = new System.Windows.Forms.Padding(3);
			this.CombatantsPage.Size = new System.Drawing.Size(360, 336);
			this.CombatantsPage.TabIndex = 0;
			this.CombatantsPage.Text = "Combatants";
			this.CombatantsPage.UseVisualStyleBackColor = true;
			this.ListSplitter.Dock = DockStyle.Fill;
			this.ListSplitter.Location = new Point(3, 3);
			this.ListSplitter.Name = "ListSplitter";
			this.ListSplitter.Orientation = Orientation.Horizontal;
			this.ListSplitter.Panel1.Controls.Add(this.CombatList);
			this.ListSplitter.Panel2.Controls.Add(this.PreviewPanel);
			this.ListSplitter.Size = new System.Drawing.Size(354, 330);
			this.ListSplitter.SplitterDistance = 159;
			this.ListSplitter.TabIndex = 1;
			this.ListSplitter.Resize += new EventHandler(this.ListSplitter_Resize);
			this.ListSplitter.SplitterMoved += new SplitterEventHandler(this.ListSplitter_SplitterMoved);
			ListView.ColumnHeaderCollection columns = this.CombatList.Columns;
			ColumnHeader[] nameHdr = new ColumnHeader[] { this.NameHdr, this.InitHdr, this.HPHdr, this.DefHdr, this.EffectsHdr };
			columns.AddRange(nameHdr);
			this.CombatList.ContextMenuStrip = this.ListContext;
			this.CombatList.Dock = DockStyle.Fill;
			this.CombatList.FullRowSelect = true;
			listViewGroup.Header = "Combatants";
			listViewGroup.Name = "listViewGroup1";
			listViewGroup1.Header = "Delayed / Readied";
			listViewGroup1.Name = "listViewGroup5";
			listViewGroup2.Header = "Traps";
			listViewGroup2.Name = "listViewGroup3";
			listViewGroup3.Header = "Skill Challenges";
			listViewGroup3.Name = "listViewGroup4";
			listViewGroup4.Header = "Custom Tokens and Overlays";
			listViewGroup4.Name = "listViewGroup6";
			listViewGroup5.Header = "Not In Play";
			listViewGroup5.Name = "listViewGroup2";
			listViewGroup6.Header = "Defeated";
			listViewGroup6.Name = "listViewGroup7";
            listViewGroup10.Header = "Terrain Layers";
            listViewGroup10.Name = "listViewGroup10";
            ListViewGroupCollection groups = this.CombatList.Groups;
			ListViewGroup[] listViewGroupArray = new ListViewGroup[] { listViewGroup, listViewGroup1, listViewGroup2, listViewGroup3, listViewGroup4, listViewGroup5, listViewGroup6, listViewGroup10};
			groups.AddRange(listViewGroupArray);
			this.CombatList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.CombatList.HideSelection = false;
			this.CombatList.Location = new Point(0, 0);
			this.CombatList.Name = "CombatList";
			this.CombatList.OwnerDraw = true;
			this.CombatList.Size = new System.Drawing.Size(354, 159);
			this.CombatList.TabIndex = 0;
			this.CombatList.TileSize = new System.Drawing.Size(300, 45);
			this.CombatList.UseCompatibleStateImageBehavior = false;
			this.CombatList.View = View.Details;
			this.CombatList.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(this.CombatList_DrawColumnHeader);
			this.CombatList.DrawItem += new DrawListViewItemEventHandler(this.CombatList_DrawItem);
			this.CombatList.SelectedIndexChanged += new EventHandler(this.CombatList_SelectedIndexChanged);
			this.CombatList.DoubleClick += new EventHandler(this.CombatList_DoubleClick);
			this.CombatList.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(this.CombatList_ItemSelectionChanged);
			this.CombatList.ItemDrag += new ItemDragEventHandler(this.CombatList_ItemDrag);
			this.CombatList.DrawSubItem += new DrawListViewSubItemEventHandler(this.CombatList_DrawSubItem);
			this.NameHdr.Text = "Name";
			this.NameHdr.Width = 185;
			this.InitHdr.Text = "Init";
			this.InitHdr.TextAlign = HorizontalAlignment.Right;
			this.HPHdr.Text = "HP";
			this.HPHdr.TextAlign = HorizontalAlignment.Right;
			this.DefHdr.Text = "Defences";
			this.DefHdr.Width = 200;
			this.EffectsHdr.Text = "Effects";
			this.EffectsHdr.Width = 175;
			ToolStripItemCollection items1 = this.ListContext.Items;
			ToolStripItem[] listDetails = new ToolStripItem[] { this.ListDetails, this.toolStripSeparator14, this.ListDamage, this.ListHeal, this.ListCondition, this.ListRemoveEffect, this.toolStripSeparator3, this.ListRemove, this.ListCreateCopy, this.toolStripSeparator4, this.ListVisible, this.ListDelay };
			items1.AddRange(listDetails);
			this.ListContext.Name = "MapContext";
			this.ListContext.Size = new System.Drawing.Size(185, 220);
			this.ListContext.Opening += new CancelEventHandler(this.ListContext_Opening);
			this.ListDetails.Name = "ListDetails";
			this.ListDetails.Size = new System.Drawing.Size(184, 22);
			this.ListDetails.Text = "Details";
			this.ListDetails.Click += new EventHandler(this.ListDetails_Click);
			this.toolStripSeparator14.Name = "toolStripSeparator14";
			this.toolStripSeparator14.Size = new System.Drawing.Size(181, 6);
			this.ListDamage.Name = "ListDamage";
			this.ListDamage.Size = new System.Drawing.Size(184, 22);
			this.ListDamage.Text = "Damage...";
			this.ListDamage.Click += new EventHandler(this.ListDamage_Click);
			this.ListHeal.Name = "ListHeal";
			this.ListHeal.Size = new System.Drawing.Size(184, 22);
			this.ListHeal.Text = "Heal...";
			this.ListHeal.Click += new EventHandler(this.ListHeal_Click);
			this.ListCondition.DropDownItems.AddRange(new ToolStripItem[] { this.effectToolStripMenuItem1 });
			this.ListCondition.Name = "ListCondition";
			this.ListCondition.Size = new System.Drawing.Size(184, 22);
			this.ListCondition.Text = "Add Effect";
			this.ListCondition.DropDownOpening += new EventHandler(this.ListCondition_DropDownOpening);
			this.effectToolStripMenuItem1.Name = "effectToolStripMenuItem1";
			this.effectToolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
			this.effectToolStripMenuItem1.Text = "[effect]";
			this.ListRemoveEffect.DropDownItems.AddRange(new ToolStripItem[] { this.effectToolStripMenuItem3 });
			this.ListRemoveEffect.Name = "ListRemoveEffect";
			this.ListRemoveEffect.Size = new System.Drawing.Size(184, 22);
			this.ListRemoveEffect.Text = "Remove Effect";
			this.ListRemoveEffect.DropDownOpening += new EventHandler(this.ListRemoveEffect_DropDownOpening);
			this.effectToolStripMenuItem3.Name = "effectToolStripMenuItem3";
			this.effectToolStripMenuItem3.Size = new System.Drawing.Size(112, 22);
			this.effectToolStripMenuItem3.Text = "[effect]";
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(181, 6);
			ToolStripItemCollection toolStripItemCollections4 = this.ListRemove.DropDownItems;
			ToolStripItem[] listRemoveMap = new ToolStripItem[] { this.ListRemoveMap, this.ListRemoveCombat };
			toolStripItemCollections4.AddRange(listRemoveMap);
			this.ListRemove.Name = "ListRemove";
			this.ListRemove.Size = new System.Drawing.Size(184, 22);
			this.ListRemove.Text = "Remove";
			this.ListRemoveMap.Name = "ListRemoveMap";
			this.ListRemoveMap.Size = new System.Drawing.Size(192, 22);
			this.ListRemoveMap.Text = "Remove from Map";
			this.ListRemoveMap.Click += new EventHandler(this.ListRemoveMap_Click);
			this.ListRemoveCombat.Name = "ListRemoveCombat";
			this.ListRemoveCombat.Size = new System.Drawing.Size(192, 22);
			this.ListRemoveCombat.Text = "Remove from Combat";
			this.ListRemoveCombat.Click += new EventHandler(this.ListRemoveCombat_Click);
			this.ListCreateCopy.Name = "ListCreateCopy";
			this.ListCreateCopy.Size = new System.Drawing.Size(184, 22);
			this.ListCreateCopy.Text = "Create Duplicate";
			this.ListCreateCopy.Click += new EventHandler(this.ListCreateCopy_Click);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(181, 6);
			this.ListVisible.Name = "ListVisible";
			this.ListVisible.Size = new System.Drawing.Size(184, 22);
			this.ListVisible.Text = "Visible";
			this.ListVisible.Click += new EventHandler(this.ListVisible_Click);
			this.ListDelay.Name = "ListDelay";
			this.ListDelay.Size = new System.Drawing.Size(184, 22);
			this.ListDelay.Text = "Delay / Ready Action";
			this.ListDelay.Click += new EventHandler(this.ListDelay_Click);
			this.PreviewPanel.BorderStyle = BorderStyle.Fixed3D;
			this.PreviewPanel.Controls.Add(this.Preview);
			this.PreviewPanel.Dock = DockStyle.Fill;
			this.PreviewPanel.Location = new Point(0, 0);
			this.PreviewPanel.Name = "PreviewPanel";
			this.PreviewPanel.Size = new System.Drawing.Size(354, 167);
			this.PreviewPanel.TabIndex = 1;
			this.Preview.Dock = DockStyle.Fill;
			this.Preview.IsWebBrowserContextMenuEnabled = false;
			this.Preview.Location = new Point(0, 0);
			this.Preview.MinimumSize = new System.Drawing.Size(20, 20);
			this.Preview.Name = "Preview";
			this.Preview.ScriptErrorsSuppressed = true;
			this.Preview.Size = new System.Drawing.Size(350, 163);
			this.Preview.TabIndex = 0;
			this.Preview.WebBrowserShortcutsEnabled = false;
			this.Preview.Navigating += new WebBrowserNavigatingEventHandler(this.Preview_Navigating);
			this.TemplatesPage.Controls.Add(this.TemplateList);
			this.TemplatesPage.Location = new Point(4, 22);
			this.TemplatesPage.Name = "TemplatesPage";
			this.TemplatesPage.Padding = new System.Windows.Forms.Padding(3);
			this.TemplatesPage.Size = new System.Drawing.Size(360, 336);
			this.TemplatesPage.TabIndex = 1;
			this.TemplatesPage.Text = "Tokens and Overlays";
			this.TemplatesPage.UseVisualStyleBackColor = true;
			this.TemplateList.Columns.AddRange(new ColumnHeader[] { this.TemplateHdr });
			this.TemplateList.Dock = DockStyle.Fill;
			this.TemplateList.FullRowSelect = true;
			listViewGroup7.Header = "Predefined";
			listViewGroup7.Name = "listViewGroup3";
			listViewGroup8.Header = "Custom Tokens";
			listViewGroup8.Name = "listViewGroup1";
			listViewGroup9.Header = "Custom Overlays";
			listViewGroup9.Name = "listViewGroup2";
            this.TemplateList.Groups.AddRange(new ListViewGroup[] { listViewGroup7, listViewGroup8, listViewGroup9 });
			this.TemplateList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.TemplateList.HideSelection = false;
			this.TemplateList.Location = new Point(3, 3);
			this.TemplateList.MultiSelect = false;
			this.TemplateList.Name = "TemplateList";
			this.TemplateList.Size = new System.Drawing.Size(354, 330);
			this.TemplateList.TabIndex = 0;
			this.TemplateList.UseCompatibleStateImageBehavior = false;
			this.TemplateList.View = View.Details;
			this.TemplateList.ItemDrag += new ItemDragEventHandler(this.TemplateList_ItemDrag);
			this.TemplateHdr.Text = "Templates";
			this.TemplateHdr.Width = 283;
			this.LogPage.Controls.Add(this.LogBrowser);
			this.LogPage.Location = new Point(4, 22);
			this.LogPage.Name = "LogPage";
			this.LogPage.Padding = new System.Windows.Forms.Padding(3);
			this.LogPage.Size = new System.Drawing.Size(360, 336);
			this.LogPage.TabIndex = 2;
			this.LogPage.Text = "Encounter Log";
			this.LogPage.UseVisualStyleBackColor = true;
			this.LogBrowser.Dock = DockStyle.Fill;
			this.LogBrowser.IsWebBrowserContextMenuEnabled = false;
			this.LogBrowser.Location = new Point(3, 3);
			this.LogBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.LogBrowser.Name = "LogBrowser";
			this.LogBrowser.ScriptErrorsSuppressed = true;
			this.LogBrowser.Size = new System.Drawing.Size(354, 330);
			this.LogBrowser.TabIndex = 1;
			this.LogBrowser.WebBrowserShortcutsEnabled = false;
			this.MapView.AllowDrawing = false;
			this.MapView.AllowDrop = true;
			this.MapView.AllowLinkCreation = true;
			this.MapView.AllowScrolling = false;
			this.MapView.BackgroundMap = null;
			this.MapView.BorderSize = 0;
			this.MapView.BorderStyle = BorderStyle.Fixed3D;
			this.MapView.Caption = "";
			this.MapView.ContextMenuStrip = this.MapContext;
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
			this.MapView.ShowAllWaves = false;
			this.MapView.ShowAuras = true;
			this.MapView.ShowConditions = true;
			this.MapView.ShowCreatureLabels = true;
			this.MapView.ShowCreatures = CreatureViewMode.All;
			this.MapView.ShowGrid = MapGridMode.None;
			this.MapView.ShowGridLabels = false;
			this.MapView.ShowHealthBars = false;
			this.MapView.ShowPictureTokens = true;
			this.MapView.Size = new System.Drawing.Size(414, 317);
			this.MapView.TabIndex = 0;
			this.MapView.Tactical = true;
			this.MapView.TokenLinks = null;
			this.MapView.Viewpoint = new Rectangle(0, 0, 0, 0);
			this.MapView.TokenDragged += new DraggedTokenEventHandler(this.MapView_TokenDragged);
			this.MapView.CancelledScrolling += new EventHandler(this.MapView_CancelledScrolling);
			this.MapView.TokenActivated += new TokenEventHandler(this.MapView_TokenActivated);
			this.MapView.CreateTokenLink += new CreateTokenLinkEventHandler(this.MapView_CreateTokenLink);
			this.MapView.EditTokenLink += new TokenLinkEventHandler(this.MapView_EditTokenLink);
			this.MapView.MouseZoomed += new MouseEventHandler(this.MapView_MouseZoomed);
			this.MapView.SelectedTokensChanged += new EventHandler(this.MapView_SelectedTokensChanged);
			this.MapView.HoverTokenChanged += new EventHandler(this.MapView_HoverTokenChanged);
			this.MapView.ItemMoved += new MovementEventHandler(this.MapView_ItemMoved);
            this.MapView.ItemDropped += new ItemDroppedEventHandler(this.MapView_ItemDropped);
            this.MapView.SketchCreated += new MapSketchEventHandler(this.MapView_SketchCreated);
			ToolStripItemCollection items2 = this.MapContext.Items;
			ToolStripItem[] mapDetails = new ToolStripItem[] { this.MapDetails, this.toolStripMenuItem2, this.MapDamage, this.MapHeal, this.MapAddEffect, this.MapRemoveEffect, this.MapSetPicture, this.toolStripMenuItem1, this.MapRemove, this.MapCreateCopy, this.toolStripSeparator2, this.MapVisible, this.MapDelay, this.toolStripSeparator22, this.MapContextDrawing, this.MapContextClearDrawings, this.toolStripSeparator25, this.MapContextLOS, this.toolStripSeparator24, this.MapContextOverlay };
			items2.AddRange(mapDetails);
			this.MapContext.Name = "MapContext";
			this.MapContext.Size = new System.Drawing.Size(185, 348);
			this.MapContext.Opening += new CancelEventHandler(this.MapContext_Opening);
			this.MapDetails.Name = "MapDetails";
			this.MapDetails.Size = new System.Drawing.Size(184, 22);
			this.MapDetails.Text = "Details";
			this.MapDetails.Click += new EventHandler(this.MapDetails_Click);
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(181, 6);
			this.MapDamage.Name = "MapDamage";
			this.MapDamage.Size = new System.Drawing.Size(184, 22);
			this.MapDamage.Text = "Damage...";
			this.MapDamage.Click += new EventHandler(this.MapDamage_Click);
			this.MapHeal.Name = "MapHeal";
			this.MapHeal.Size = new System.Drawing.Size(184, 22);
			this.MapHeal.Text = "Heal...";
			this.MapHeal.Click += new EventHandler(this.MapHeal_Click);
			this.MapAddEffect.DropDownItems.AddRange(new ToolStripItem[] { this.effectToolStripMenuItem2 });
			this.MapAddEffect.Name = "MapAddEffect";
			this.MapAddEffect.Size = new System.Drawing.Size(184, 22);
			this.MapAddEffect.Text = "Add Effect";
			this.MapAddEffect.DropDownOpening += new EventHandler(this.MapCondition_DropDownOpening);
			this.effectToolStripMenuItem2.Name = "effectToolStripMenuItem2";
			this.effectToolStripMenuItem2.Size = new System.Drawing.Size(112, 22);
			this.effectToolStripMenuItem2.Text = "[effect]";
			this.MapRemoveEffect.DropDownItems.AddRange(new ToolStripItem[] { this.effectToolStripMenuItem4 });
			this.MapRemoveEffect.Name = "MapRemoveEffect";
			this.MapRemoveEffect.Size = new System.Drawing.Size(184, 22);
			this.MapRemoveEffect.Text = "Remove Effect";
			this.MapRemoveEffect.DropDownOpening += new EventHandler(this.MapRemoveEffect_DropDownOpening);
			this.effectToolStripMenuItem4.Name = "effectToolStripMenuItem4";
			this.effectToolStripMenuItem4.Size = new System.Drawing.Size(112, 22);
			this.effectToolStripMenuItem4.Text = "[effect]";
			this.MapSetPicture.Name = "MapSetPicture";
			this.MapSetPicture.Size = new System.Drawing.Size(184, 22);
			this.MapSetPicture.Text = "Set Picture...";
			this.MapSetPicture.Click += new EventHandler(this.MapSetPicture_Click);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(181, 6);
			ToolStripItemCollection dropDownItems5 = this.MapRemove.DropDownItems;
			ToolStripItem[] mapRemoveMap = new ToolStripItem[] { this.MapRemoveMap, this.MapRemoveCombat };
			dropDownItems5.AddRange(mapRemoveMap);
			this.MapRemove.Name = "MapRemove";
			this.MapRemove.Size = new System.Drawing.Size(184, 22);
			this.MapRemove.Text = "Remove";
			this.MapRemoveMap.Name = "MapRemoveMap";
			this.MapRemoveMap.Size = new System.Drawing.Size(192, 22);
			this.MapRemoveMap.Text = "Remove from Map";
			this.MapRemoveMap.Click += new EventHandler(this.MapRemoveMap_Click);
			this.MapRemoveCombat.Name = "MapRemoveCombat";
			this.MapRemoveCombat.Size = new System.Drawing.Size(192, 22);
			this.MapRemoveCombat.Text = "Remove from Combat";
			this.MapRemoveCombat.Click += new EventHandler(this.MapRemoveCombat_Click);
			this.MapCreateCopy.Name = "MapCreateCopy";
			this.MapCreateCopy.Size = new System.Drawing.Size(184, 22);
			this.MapCreateCopy.Text = "Create Duplicate";
			this.MapCreateCopy.Click += new EventHandler(this.MapCreateCopy_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(181, 6);
			this.MapVisible.Name = "MapVisible";
			this.MapVisible.Size = new System.Drawing.Size(184, 22);
			this.MapVisible.Text = "Visible";
			this.MapVisible.Click += new EventHandler(this.MapVisible_Click);
			this.MapDelay.Name = "MapDelay";
			this.MapDelay.Size = new System.Drawing.Size(184, 22);
			this.MapDelay.Text = "Delay / Ready Action";
			this.MapDelay.Click += new EventHandler(this.MapDelay_Click);
			this.toolStripSeparator22.Name = "toolStripSeparator22";
			this.toolStripSeparator22.Size = new System.Drawing.Size(181, 6);
			this.MapContextDrawing.Name = "MapContextDrawing";
			this.MapContextDrawing.Size = new System.Drawing.Size(184, 22);
			this.MapContextDrawing.Text = "Allow Drawing";
			this.MapContextDrawing.Click += new EventHandler(this.MapDrawing_Click);
			this.MapContextClearDrawings.Name = "MapContextClearDrawings";
			this.MapContextClearDrawings.Size = new System.Drawing.Size(184, 22);
			this.MapContextClearDrawings.Text = "Clear Drawings";
			this.MapContextClearDrawings.Click += new EventHandler(this.MapClearDrawings_Click);
			this.toolStripSeparator25.Name = "toolStripSeparator25";
			this.toolStripSeparator25.Size = new System.Drawing.Size(181, 6);
			this.MapContextLOS.Name = "MapContextLOS";
			this.MapContextLOS.Size = new System.Drawing.Size(184, 22);
			this.MapContextLOS.Text = "Line of Sight";
			this.MapContextLOS.Click += new EventHandler(this.MapLOS_Click);
			this.toolStripSeparator24.Name = "toolStripSeparator24";
			this.toolStripSeparator24.Size = new System.Drawing.Size(181, 6);
			this.MapContextOverlay.Name = "MapContextOverlay";
			this.MapContextOverlay.Size = new System.Drawing.Size(184, 22);
			this.MapContextOverlay.Text = "Add Overlay...";
			this.MapContextOverlay.Click += new EventHandler(this.MapContextOverlay_Click);
			this.ZoomGauge.Dock = DockStyle.Bottom;
			this.ZoomGauge.Location = new Point(0, 317);
			this.ZoomGauge.Maximum = 100;
			this.ZoomGauge.Name = "ZoomGauge";
			this.ZoomGauge.Size = new System.Drawing.Size(414, 45);
			this.ZoomGauge.TabIndex = 1;
			this.ZoomGauge.TickFrequency = 10;
			this.ZoomGauge.TickStyle = TickStyle.Both;
			this.ZoomGauge.Value = 50;
			this.ZoomGauge.Visible = false;
			this.ZoomGauge.Scroll += new EventHandler(this.ZoomGauge_Scroll);
			this.MapTooltip.ToolTipIcon = ToolTipIcon.Info;
			ToolStripItemCollection items3 = this.Statusbar.Items;
			ToolStripItem[] roundLbl = new ToolStripItem[] { this.RoundLbl, this.XPLbl, this.LevelLbl };
			items3.AddRange(roundLbl);
			this.Statusbar.Location = new Point(0, 362);
			this.Statusbar.Name = "Statusbar";
			this.Statusbar.Size = new System.Drawing.Size(826, 22);
			this.Statusbar.SizingGrip = false;
			this.Statusbar.TabIndex = 0;
			this.Statusbar.Text = "statusStrip1";
			this.RoundLbl.Font = new System.Drawing.Font("Segoe UI", 9f, FontStyle.Bold);
			this.RoundLbl.Name = "RoundLbl";
			this.RoundLbl.Size = new System.Drawing.Size(48, 17);
			this.RoundLbl.Text = "[round]";
			this.XPLbl.Name = "XPLbl";
			this.XPLbl.Size = new System.Drawing.Size(27, 17);
			this.XPLbl.Text = "[xp]";
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new System.Drawing.Size(39, 17);
			this.LevelLbl.Text = "[level]";
			this.MainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.MainPanel.Controls.Add(this.MapSplitter);
			this.MainPanel.Controls.Add(this.InitiativePanel);
			this.MainPanel.Controls.Add(this.Statusbar);
			this.MainPanel.Location = new Point(12, 28);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new System.Drawing.Size(826, 384);
			this.MainPanel.TabIndex = 1;
			this.InitiativePanel.BorderStyle = BorderStyle.Fixed3D;
			this.InitiativePanel.CurrentInitiative = 0;
			this.InitiativePanel.Dock = DockStyle.Right;
			this.InitiativePanel.InitiativeScores = (List<int>)componentResourceManager.GetObject("InitiativePanel.InitiativeScores");
			this.InitiativePanel.Location = new Point(786, 0);
			this.InitiativePanel.Name = "InitiativePanel";
			this.InitiativePanel.Size = new System.Drawing.Size(40, 362);
			this.InitiativePanel.TabIndex = 2;
			this.InitiativePanel.InitiativeChanged += new EventHandler(this.InitiativePanel_InitiativeChanged);
			this.CloseBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CloseBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.CloseBtn.Location = new Point(718, 418);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new System.Drawing.Size(120, 23);
			this.CloseBtn.TabIndex = 6;
			this.CloseBtn.Text = "End Encounter";
			this.CloseBtn.UseVisualStyleBackColor = true;
			this.CloseBtn.Click += new EventHandler(this.CloseBtn_Click);
			this.PauseBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.PauseBtn.Location = new Point(592, 418);
			this.PauseBtn.Name = "PauseBtn";
			this.PauseBtn.Size = new System.Drawing.Size(120, 23);
			this.PauseBtn.TabIndex = 5;
			this.PauseBtn.Text = "Pause Encounter";
			this.PauseBtn.UseVisualStyleBackColor = true;
			this.PauseBtn.Click += new EventHandler(this.PauseBtn_Click);
			this.InfoBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.InfoBtn.Location = new Point(12, 418);
			this.InfoBtn.Name = "InfoBtn";
			this.InfoBtn.Size = new System.Drawing.Size(75, 23);
			this.InfoBtn.TabIndex = 2;
			this.InfoBtn.Text = "Information";
			this.InfoBtn.UseVisualStyleBackColor = true;
			this.InfoBtn.Click += new EventHandler(this.InfoBtn_Click);
			this.DieRollerBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.DieRollerBtn.Location = new Point(93, 418);
			this.DieRollerBtn.Name = "DieRollerBtn";
			this.DieRollerBtn.Size = new System.Drawing.Size(75, 23);
			this.DieRollerBtn.TabIndex = 3;
			this.DieRollerBtn.Text = "Die Roller";
			this.DieRollerBtn.UseVisualStyleBackColor = true;
			this.DieRollerBtn.Click += new EventHandler(this.DieRollerBtn_Click);
			this.ReportBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.ReportBtn.Location = new Point(174, 418);
			this.ReportBtn.Name = "ReportBtn";
			this.ReportBtn.Size = new System.Drawing.Size(75, 23);
			this.ReportBtn.TabIndex = 4;
			this.ReportBtn.Text = "Report";
			this.ReportBtn.UseVisualStyleBackColor = true;
			this.ReportBtn.Click += new EventHandler(this.ReportBtn_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(850, 453);
			base.Controls.Add(this.ReportBtn);
			base.Controls.Add(this.DieRollerBtn);
			base.Controls.Add(this.Toolbar);
			base.Controls.Add(this.InfoBtn);
			base.Controls.Add(this.MainPanel);
			base.Controls.Add(this.CloseBtn);
			base.Controls.Add(this.PauseBtn);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "CombatForm";
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Combat Encounter";
			base.Shown += new EventHandler(this.CombatForm_Shown);
			base.FormClosing += new FormClosingEventHandler(this.CombatForm_FormClosing);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.MapSplitter.Panel1.ResumeLayout(false);
			this.MapSplitter.Panel2.ResumeLayout(false);
			this.MapSplitter.Panel2.PerformLayout();
			this.MapSplitter.ResumeLayout(false);
			this.Pages.ResumeLayout(false);
			this.CombatantsPage.ResumeLayout(false);
			this.ListSplitter.Panel1.ResumeLayout(false);
			this.ListSplitter.Panel2.ResumeLayout(false);
			this.ListSplitter.ResumeLayout(false);
			this.ListContext.ResumeLayout(false);
			this.PreviewPanel.ResumeLayout(false);
			this.TemplatesPage.ResumeLayout(false);
			this.LogPage.ResumeLayout(false);
			this.MapContext.ResumeLayout(false);
			((ISupportInitialize)this.ZoomGauge).EndInit();
			this.Statusbar.ResumeLayout(false);
			this.Statusbar.PerformLayout();
			this.MainPanel.ResumeLayout(false);
			this.MainPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void InitiativePanel_InitiativeChanged(object sender, EventArgs e)
		{
			try
			{
				Guid d = this.fCurrentActor.ID;
				this.fCurrentActor = null;
				this.fCurrentActor = this.get_next_actor(null);
				if (this.fCurrentActor.ID != d)
				{
					this.fLog.AddStartTurnEntry(this.fCurrentActor.ID);
				}
				this.update_list();
				this.update_log();
				this.update_preview_panel();
				this.highlight_current_actor();
                this.update_maps();
            }
            catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		public string InitiativeView()
		{
			List<ListViewItem> listViewItems = new List<ListViewItem>();
			foreach (ListViewItem item in this.CombatList.Groups[0].Items)
			{
				listViewItems.Add(item);
			}
			listViewItems.Sort(this.CombatList.ListViewItemSorter as IComparer<ListViewItem>);
			List<string> strs = new List<string>();
			List<string> strs1 = new List<string>();
			bool flag = false;
			strs.AddRange(HTML.GetHead(null, null, PlayerViewForm.DisplaySize));
			strs.Add("<BODY bgcolor=black>");
			strs.Add("<P class=table>");
			strs.Add("<TABLE class=initiative>");
			foreach (ListViewItem listViewItem in listViewItems)
			{
				CombatData data = null;
				string displayName = "";
                if (listViewItem.Tag is Hero)
                {
                    Hero hero = listViewItem.Tag as Hero;
                    data = hero.CombatData;
                    displayName = hero.Name;
                }
                else
                {
                    if (listViewItem.Tag is CreatureToken)
                    {
                        CreatureToken tag = listViewItem.Tag as CreatureToken;
                        data = tag.Data;

                        bool showLabel = Session.Preferences.PlayerViewCreatureLabels;
                        
                        if (showLabel && Session.Preferences.PlayerViewCreatureLabelsRequireKnowledge)
                        {
                            EncounterSlot encounterSlot = this.fEncounter.FindSlot(tag.SlotID);
                            showLabel = encounterSlot.KnowledgeKnown;
                        }

                        if (showLabel)
                        { 
                            displayName = data.DisplayName;
                        }

                        if (displayName == "")
                        {
                            displayName = "Scary Monster";
                        }

                    }
                    if (listViewItem.Tag is Trap)
                    {
                        Trap trap = listViewItem.Tag as Trap;
                        if (trap.Initiative != Int32.MinValue)
                        {
                            data = trap.CombatData;
                            displayName = data.DisplayName;
                            if (!Session.Preferences.PlayerViewCreatureLabels)
                            {
                                displayName = trap.Type.ToString();
                            }
                        }
                    }
                    if (listViewItem.Tag is CustomToken)
                    {
                        data = (listViewItem.Tag as CustomToken).Data;
                        displayName = data.DisplayName;
                    }
                }
				if (data == null || !data.Visible || data.Initiative == -2147483648)
				{
					continue;
				}

                string abbreviation = String.IsNullOrEmpty(data.DisplayName) ? TextHelper.Abbreviation(displayName) : TextHelper.Abbreviation(data.DisplayName);
                displayName = string.Format("{0} - {1}", abbreviation, displayName);

                string str = "white";
				if (data == this.fCurrentActor)
				{
					flag = true;
					displayName = string.Concat("<B>", displayName, "</B>");
				}
				EncounterSlot encounterSlot1 = this.fEncounter.FindSlot(data);
				if (encounterSlot1 != null)
				{
					switch (encounterSlot1.GetState(data))
					{
						case CreatureState.Bloodied:
						{
							str = "red";
							break;
						}
						case CreatureState.Defeated:
						{
							str = "darkgrey";
							displayName = string.Concat("<S>", displayName, "</S>");
							break;
						}
					}
				}
				string[] strArrays = new string[] { "<FONT color=", str, ">", displayName, "</FONT>" };
				string str1 = string.Concat(strArrays);
				if (data.Conditions.Count != 0)
				{
					string str2 = "";
					foreach (OngoingCondition condition in data.Conditions)
					{
						if (str2 != "")
						{
							str2 = string.Concat(str2, "; ");
						}
						str2 = string.Concat(str2, condition.ToString(this.fEncounter, true));
					}
					str1 = string.Concat(str1, "<BR><FONT color=grey>", str2, "</FONT>");
				}
				List<string> strs2 = (flag ? strs : strs1);
				strs2.Add("<TR>");
				strs2.Add(string.Concat("<TD align=center bgcolor=black width=50><FONT color=lightgrey>", data.Initiative, "</FONT></TD>"));
				strs2.Add(string.Concat("<TD bgcolor=black>", str1, "</TD>"));
				strs2.Add("</TR>");
			}
			strs.AddRange(strs1);
			strs.Add("</TABLE>");
			strs.Add("</P>");
			strs.Add("<HR>");
			strs.Add(this.EncounterLogView(true));
			strs.Add("</BODY>");
			strs.Add("</HTML>");
			return HTML.Concatenate(strs);
		}

		private void list_splitter_changed()
		{
			try
			{
				if (base.Visible)
				{
					this.update_preview_panel();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ListCondition_DropDownOpening(object sender, EventArgs e)
		{
			this.update_effects_list(this.ListCondition, true);
		}

		private void ListContext_Opening(object sender, CancelEventArgs e)
		{
			try
			{
				bool flag = false;
				bool flag1 = false;
				bool flag2 = false;
				bool flag3 = false;
				if (this.SelectedTokens.Count != 0)
				{
					flag = true;
					flag1 = true;
					flag2 = true;
					foreach (IToken selectedToken in this.SelectedTokens)
					{
						if ((selectedToken is CreatureToken ? false : !(selectedToken is Hero)))
						{
							flag = false;
						}
						if (selectedToken is CreatureToken)
						{
							CreatureToken creatureToken = selectedToken as CreatureToken;
							if (!creatureToken.Data.Delaying)
							{
								flag1 = false;
							}
							if (this.MapView.Map != null && creatureToken.Data.Location == CombatData.NoPoint)
							{
								flag2 = false;
							}
						}
						if (selectedToken is Hero)
						{
							CombatData combatData = (selectedToken as Hero).CombatData;
							if (!combatData.Delaying)
							{
								flag1 = false;
							}
							if (this.MapView.Map != null && combatData.Location == CombatData.NoPoint)
							{
								flag2 = false;
							}
						}
						if (!(selectedToken is CustomToken))
						{
							continue;
						}
						CustomToken customToken = selectedToken as CustomToken;
						flag3 = true;
						if (this.MapView.Map != null && customToken.Data.Location == CombatData.NoPoint)
						{
							flag2 = false;
						}
						if (customToken.CreatureID == Guid.Empty)
						{
							continue;
						}
						flag2 = false;
					}
				}
				bool flag4 = false;
				foreach (IToken token in this.SelectedTokens)
				{
					if (token is Hero)
					{
						continue;
					}
					flag4 = true;
				}
				this.ListDetails.Enabled = this.SelectedTokens.Count == 1;
				this.ListDamage.Enabled = flag;
				this.ListHeal.Enabled = flag;
				this.ListCondition.Enabled = flag;
				this.ListRemoveEffect.Enabled = this.SelectedTokens.Count == 1;
				this.ListRemoveMap.Enabled = flag2;
				this.ListRemoveCombat.Enabled = this.SelectedTokens.Count != 0;
				this.ListCreateCopy.Enabled = flag3;
				this.ListVisible.Enabled = flag4;
				if (!this.ListVisible.Enabled || this.SelectedTokens.Count != 1)
				{
					this.ListVisible.Checked = false;
				}
				else
				{
					if (this.SelectedTokens[0] is CreatureToken)
					{
						CreatureToken item = this.SelectedTokens[0] as CreatureToken;
						this.ListVisible.Checked = item.Data.Visible;
					}
					if (this.SelectedTokens[0] is CustomToken)
					{
						CustomToken item1 = this.SelectedTokens[0] as CustomToken;
						this.ListVisible.Checked = item1.Data.Visible;
					}
				}
				this.ListDelay.Enabled = flag;
				this.ListDelay.Checked = flag1;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ListCreateCopy_Click(object sender, EventArgs e)
		{
			this.copy_custom_token();
		}

		private void ListDamage_Click(object sender, EventArgs e)
		{
			try
			{
				this.do_damage(this.SelectedTokens);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ListDelay_Click(object sender, EventArgs e)
		{
			this.set_delay(this.SelectedTokens);
		}

		private void ListDetails_Click(object sender, EventArgs e)
		{
			try
			{
				this.edit_token(this.SelectedTokens[0]);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ListHeal_Click(object sender, EventArgs e)
		{
			try
			{
				this.do_heal(this.SelectedTokens);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ListRemoveCombat_Click(object sender, EventArgs e)
		{
			this.remove_from_combat(this.SelectedTokens);
		}

		private void ListRemoveEffect_DropDownOpening(object sender, EventArgs e)
		{
			this.update_remove_effect_list(this.ListRemoveEffect, true);
		}

		private void ListRemoveMap_Click(object sender, EventArgs e)
		{
			this.remove_from_map(this.SelectedTokens);
		}

		private void ListSplitter_Resize(object sender, EventArgs e)
		{
			this.list_splitter_changed();
		}

		private void ListSplitter_SplitterMoved(object sender, SplitterEventArgs e)
		{
			this.list_splitter_changed();
		}

		private void ListVisible_Click(object sender, EventArgs e)
		{
			this.toggle_visibility(this.SelectedTokens);
		}

		private void MapClearDrawings_Click(object sender, EventArgs e)
		{
			this.MapView.Sketches.Clear();
			this.MapView.Invalidate();
			if (this.PlayerMap != null)
			{
				this.PlayerMap.Sketches.Clear();
				this.PlayerMap.Invalidate();
			}
		}

		private void MapCondition_DropDownOpening(object sender, EventArgs e)
		{
			this.update_effects_list(this.MapAddEffect, false);
		}

		private void MapConditions_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.ShowConditions = !this.MapView.ShowConditions;
				Session.Preferences.CombatConditionBadges = this.MapView.ShowConditions;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void MapContext_Opening(object sender, CancelEventArgs e)
		{
			try
			{
				bool flag = false;
				bool flag1 = false;
				bool flag2 = false;
				bool flag3 = false;
				if (this.MapView.SelectedTokens.Count != 0)
				{
					flag = true;
					flag1 = true;
					flag2 = true;
					foreach (IToken selectedToken in this.MapView.SelectedTokens)
					{
						if ((selectedToken is CreatureToken ? false : !(selectedToken is Hero)))
						{
							flag = false;
						}
						if (selectedToken is CreatureToken)
						{
							CreatureToken creatureToken = selectedToken as CreatureToken;
							if (!creatureToken.Data.Delaying)
							{
								flag1 = false;
							}
							if (creatureToken.Data.Location == CombatData.NoPoint)
							{
								flag2 = false;
							}
						}
						if (selectedToken is Hero)
						{
							CombatData combatData = (selectedToken as Hero).CombatData;
							if (!combatData.Delaying)
							{
								flag1 = false;
							}
							if (combatData.Location == CombatData.NoPoint)
							{
								flag2 = false;
							}
						}
						if (!(selectedToken is CustomToken))
						{
							continue;
						}
						CustomToken customToken = selectedToken as CustomToken;
						flag3 = true;
						if (customToken.Data.Location != CombatData.NoPoint)
						{
							continue;
						}
						flag2 = false;
					}
				}
				bool flag4 = false;
				foreach (IToken token in this.MapView.SelectedTokens)
				{
					if (token is Hero)
					{
						continue;
					}
					flag4 = true;
				}
				this.MapDetails.Enabled = this.MapView.SelectedTokens.Count == 1;
				this.MapDamage.Enabled = flag;
				this.MapHeal.Enabled = flag;
				this.MapAddEffect.Enabled = flag;
				this.MapRemoveEffect.Enabled = flag;
				this.MapRemoveMap.Enabled = flag2;
				this.MapRemoveCombat.Enabled = this.MapView.SelectedTokens.Count != 0;
				this.MapCreateCopy.Enabled = flag3;
				this.MapVisible.Enabled = flag4;
				if (!this.MapVisible.Enabled || this.MapView.SelectedTokens.Count != 1)
				{
					this.MapVisible.Checked = false;
				}
				else
				{
					if (this.MapView.SelectedTokens[0] is CreatureToken)
					{
						CreatureToken item = this.MapView.SelectedTokens[0] as CreatureToken;
						this.MapVisible.Checked = item.Data.Visible;
					}
					if (this.MapView.SelectedTokens[0] is CustomToken)
					{
						CustomToken item1 = this.MapView.SelectedTokens[0] as CustomToken;
						this.MapVisible.Checked = item1.Data.Visible;
					}
				}
				this.MapDelay.Enabled = flag;
				this.MapDelay.Checked = flag1;
				this.MapContextDrawing.Checked = this.MapView.AllowDrawing;
				this.MapContextClearDrawings.Enabled = this.MapView.Sketches.Count != 0;
				this.MapContextLOS.Checked = this.MapView.LineOfSight;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void MapContextOverlay_Click(object sender, EventArgs e)
		{
			CustomToken customToken = new CustomToken()
			{
				Name = "New Overlay",
				Type = CustomTokenType.Overlay
			};
			if (this.MapView.SelectedTokens.Count == 1)
			{
				IToken item = this.MapView.SelectedTokens[0];
				CreatureToken creatureToken = item as CreatureToken;
				if (creatureToken != null)
				{
					customToken.Name = string.Concat("Zone: ", creatureToken.Data.DisplayName);
					customToken.CreatureID = creatureToken.Data.ID;
					customToken.Colour = Color.Red;
				}
				Hero hero = item as Hero;
				if (hero != null)
				{
					customToken.Name = string.Concat(hero.Name, " zone");
					customToken.CreatureID = hero.ID;
					customToken.Colour = Color.DarkGreen;
				}
			}
			CustomOverlayForm customOverlayForm = new CustomOverlayForm(customToken);
			if (customOverlayForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				customToken = customOverlayForm.Token;
				if (customToken.CreatureID == Guid.Empty)
				{
					Point point = new Point(this.MapContext.Left, this.MapContext.Top);
					Point client = this.MapView.PointToClient(point);
					Point squareAtPoint = this.MapView.LayoutData.GetSquareAtPoint(client);
					int x = squareAtPoint.X;
					System.Drawing.Size overlaySize = customToken.OverlaySize;
					int width = x - (overlaySize.Width - 1) / 2;
					int y = squareAtPoint.Y;
					System.Drawing.Size size = customToken.OverlaySize;
					int height = y - (size.Height - 1) / 2;
					customToken.Data.Location = new Point(width, height);
				}
				this.fEncounter.CustomTokens.Add(customToken);
				this.update_list();
				this.update_maps();
			}
		}

		private void MapCreateCopy_Click(object sender, EventArgs e)
		{
			this.copy_custom_token();
		}

		private void MapDamage_Click(object sender, EventArgs e)
		{
			try
			{
				this.do_damage(this.MapView.SelectedTokens);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void MapDelay_Click(object sender, EventArgs e)
		{
			this.set_delay(this.MapView.SelectedTokens);
		}

		private void MapDetails_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.MapView.SelectedTokens.Count != 0)
				{
					this.edit_token(this.MapView.SelectedTokens[0]);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void MapDrawing_Click(object sender, EventArgs e)
		{
			this.MapView.AllowDrawing = !this.MapView.AllowDrawing;
			if (this.PlayerMap != null)
			{
				this.PlayerMap.AllowDrawing = this.MapView.AllowDrawing;
			}
		}

		private void MapExport_Click(object sender, EventArgs e)
		{
			try
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog()
				{
					FileName = this.MapView.Map.Name
				};
				if (this.fEncounter.MapAreaID != Guid.Empty)
				{
					MapArea mapArea = this.MapView.Map.FindArea(this.fEncounter.MapAreaID);
					SaveFileDialog saveFileDialog1 = saveFileDialog;
					saveFileDialog1.FileName = string.Concat(saveFileDialog1.FileName, " - ", mapArea.Name);
				}
				saveFileDialog.Filter = "Bitmap Image |*.bmp|JPEG Image|*.jpg|GIF Image|*.gif|PNG Image|*.png";
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
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void MapFogAllCreatures_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.ShowCreatures = CreatureViewMode.All;
				Session.Preferences.CombatFog = CreatureViewMode.All;
				this.update_list();
				this.update_preview_panel();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void MapFogHideCreatures_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.ShowCreatures = CreatureViewMode.None;
				Session.Preferences.CombatFog = CreatureViewMode.None;
				this.update_list();
				this.update_preview_panel();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void MapFogVisibleCreatures_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.ShowCreatures = CreatureViewMode.Visible;
				Session.Preferences.CombatFog = CreatureViewMode.Visible;
				this.update_list();
				this.update_preview_panel();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void MapGrid_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.ShowGrid = (this.MapView.ShowGrid == MapGridMode.None ? MapGridMode.Overlay : MapGridMode.None);
				Session.Preferences.CombatGrid = this.MapView.ShowGrid == MapGridMode.Overlay;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void MapGridLabels_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.ShowGridLabels = !this.MapView.ShowGridLabels;
				Session.Preferences.CombatGridLabels = this.MapView.ShowGridLabels;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void MapHeal_Click(object sender, EventArgs e)
		{
			try
			{
				this.do_heal(this.MapView.SelectedTokens);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void MapHealth_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.ShowHealthBars = !this.MapView.ShowHealthBars;
				Session.Preferences.CombatHealthBars = this.MapView.ShowHealthBars;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void MapLOS_Click(object sender, EventArgs e)
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

		private void MapMenu_DropDownOpening(object sender, EventArgs e)
		{
			this.ShowMap.Checked = !this.MapSplitter.Panel2Collapsed;
			this.MapLOS.Checked = this.MapView.LineOfSight;
			this.MapGrid.Checked = this.MapView.ShowGrid != MapGridMode.None;
			this.MapGridLabels.Checked = this.MapView.ShowGridLabels;
			this.MapHealth.Checked = this.MapView.ShowHealthBars;
			this.MapConditions.Checked = this.MapView.ShowConditions;
			this.MapPictureTokens.Checked = this.MapView.ShowPictureTokens;
			this.MapNavigate.Checked = this.MapView.AllowScrolling;
			this.MapFogAllCreatures.Checked = this.MapView.ShowCreatures == CreatureViewMode.All;
			this.MapFogVisibleCreatures.Checked = this.MapView.ShowCreatures == CreatureViewMode.Visible;
			this.MapFogHideCreatures.Checked = this.MapView.ShowCreatures == CreatureViewMode.None;
			this.MapDrawing.Checked = this.MapView.AllowDrawing;
			this.MapClearDrawings.Enabled = this.MapView.Sketches.Count != 0;
		}

		private void MapNavigate_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.AllowScrolling = !this.MapView.AllowScrolling;
				this.ZoomGauge.Visible = this.MapView.AllowScrolling;
				if (Session.PlayerView != null)
				{
					if (this.MapView.AllowScrolling)
					{
						Session.Preferences.PlayerViewMap = this.PlayerMap != null;
						Session.Preferences.PlayerViewInitiative = this.PlayerInitiative != null;
						Session.PlayerView.ShowMessage("DM is editing the map; please wait");
					}
					else
					{
						this.cancelled_scrolling();
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void MapPictureTokens_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.ShowPictureTokens = !this.MapView.ShowPictureTokens;
				Session.Preferences.CombatPictureTokens = this.MapView.ShowPictureTokens;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void MapPrint_Click(object sender, EventArgs e)
		{
			try
			{
				(new MapPrintingForm(this.MapView)).ShowDialog();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void MapRemoveCombat_Click(object sender, EventArgs e)
		{
			this.remove_from_combat(this.MapView.SelectedTokens);
		}

		private void MapRemoveEffect_DropDownOpening(object sender, EventArgs e)
		{
			this.update_remove_effect_list(this.MapRemoveEffect, false);
		}

		private void MapRemoveMap_Click(object sender, EventArgs e)
		{
			this.remove_from_map(this.MapView.SelectedTokens);
		}

		private void MapReset_Click(object sender, EventArgs e)
		{
			try
			{
				this.ZoomGauge.Value = 50;
				this.MapView.ScalingFactor = 1;
				if (this.fEncounter.MapAreaID == Guid.Empty)
				{
					this.MapView.Viewpoint = Rectangle.Empty;
				}
				else
				{
					MapArea mapArea = this.MapView.Map.FindArea(this.fEncounter.MapAreaID);
					this.MapView.Viewpoint = mapArea.Region;
				}
				if (this.PlayerMap != null)
				{
					this.PlayerMap.Viewpoint = this.MapView.Viewpoint;
				}

                this.RebuildAllViews();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void MapSetPicture_Click(object sender, EventArgs e)
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
						this.update_list();
					}
				}
			}
			Hero hero = this.MapView.SelectedTokens[0] as Hero;
			if (hero != null)
			{
				OpenFileDialog openFileDialog1 = new OpenFileDialog()
				{
					Filter = Program.ImageFilter
				};
				if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					hero.Portrait = Image.FromFile(openFileDialog1.FileName);
					Program.SetResolution(hero.Portrait);
					Session.Modified = true;
					this.update_list();
				}
			}
		}

		private void MapView_CancelledScrolling(object sender, EventArgs e)
		{
			this.cancelled_scrolling();
		}

		private TokenLink MapView_CreateTokenLink(object sender, TokenListEventArgs e)
		{
			TokenLink link;
			try
			{
				TokenLink tokenLink = new TokenLink();
				tokenLink.Tokens.AddRange(e.Tokens);
				TokenLinkForm tokenLinkForm = new TokenLinkForm(tokenLink);
				if (tokenLinkForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
				{
					link = null;
				}
				else
				{
					link = tokenLinkForm.Link;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
				return null;
			}
			return link;
		}

		private TokenLink MapView_EditTokenLink(object sender, TokenLinkEventArgs e)
		{
			TokenLinkForm tokenLinkForm = new TokenLinkForm(e.Link);
			if (tokenLinkForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
			{
				return null;
			}
			return tokenLinkForm.Link;
		}

		private void MapView_HoverTokenChanged(object sender, EventArgs e)
		{
			try
			{
				this.set_tooltip(this.MapView.HoverToken, this.MapView);
				if (this.PlayerMap != null)
				{
					this.PlayerMap.HoverToken = this.MapView.HoverToken;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

        private void OnMoveTokenCommand()
        {
            // Clear any drag events
            this.PlayerMap?.SetDragInfo(CombatData.NoPoint, CombatData.NoPoint);

            this.RecalculateVisibilityAllMaps();
            this.update_maps();
            foreach (IToken selectedToken in this.MapView.SelectedTokens)
            {
                Guid empty = Guid.Empty;
                CreatureToken creatureToken = selectedToken as CreatureToken;
                if (creatureToken != null)
                {
                    empty = creatureToken.Data.ID;
                }
                Hero hero = selectedToken as Hero;
                if (hero != null)
                {
                    empty = hero.ID;
                }
                //TODO: Fix this
                //this.fLog.AddMoveEntry(empty, e.Distance, "");
            }
            this.update_log();
        }

        private void OnRemoveEffectCommand()
        {
            this.update_list();
            this.update_log();
            this.update_preview_panel();
            this.MapView.MapChanged();
        }

        private void OnAddRemoveLinkCommand()
        {
            this.update_maps();
        }

        private void OnRemoveFromMapCommand()
        {
            this.update_list();
            this.update_preview_panel();
            if (CombatForm.TerrainLayersNeedRefresh)
            {
                this.RebuildTerrainLayersAllMaps();
            }
            this.RecalculateVisibilityAllMaps();
            this.update_maps();
        }

        private void MapView_ItemDropped(CombatData data, Point point)
        {
            MoveTokenCommand command = new MoveTokenCommand(data, data.Location, point);
            CommandManager.GetInstance().ExecuteCommand(command);
        }

        private void MapView_ItemMoved(IToken token, Point start, Point end)
		{
            MoveTokenCommand command = new MoveTokenCommand(CombatData.FromToken(token), start, end);
            CommandManager.GetInstance().ExecuteCommand(command);
		}

		private void MapView_MouseZoomed(object sender, MouseEventArgs e)
		{
			this.ZoomGauge.Visible = true;
			TrackBar zoomGauge = this.ZoomGauge;
			zoomGauge.Value = zoomGauge.Value - Math.Sign(e.Delta);
			this.ZoomGauge_Scroll(sender, e);
		}

		private void MapView_SelectedTokensChanged(object sender, EventArgs e)
		{
			try
			{
				this.fUpdatingList = true;
				this.CombatList.SelectedItems.Clear();
				foreach (IToken selectedToken in this.MapView.SelectedTokens)
				{
					this.select_token(selectedToken);
				}
				this.fUpdatingList = false;
				this.update_preview_panel();
				if (this.PlayerMap != null)
				{
					this.PlayerMap.SelectTokens(this.MapView.SelectedTokens, false);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void MapView_SketchCreated(object sender, MapSketchEventArgs e)
		{
			if (this.PlayerMap != null)
			{
				this.PlayerMap.Sketches.Add(e.Sketch);
				this.PlayerMap.Invalidate();
			}
		}

		private void MapView_TokenActivated(object sender, TokenEventArgs e)
		{
			try
			{
				if (e.Token is CreatureToken || e.Token is Hero)
				{
					this.do_damage(new List<IToken>()
					{
						e.Token
					});
				}
				if (e.Token is CustomToken)
				{
					this.edit_token(e.Token);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void MapView_TokenDragged(object sender, DraggedTokenEventArgs e)
		{
			if (this.PlayerMap != null)
			{
				this.PlayerMap.SetDragInfo(e.OldLocation, e.NewLocation);
                //this.PlayerMap.RecalculateVisibility();
                this.PlayerMap.Redraw();
			}
		}

		private void MapVisible_Click(object sender, EventArgs e)
		{
			this.toggle_visibility(this.MapView.SelectedTokens);
		}

        private void RunBeginningOFTurnUpdates(CombatData nextTurnActor)
        {
            this.handle_regen(nextTurnActor);
            this.handle_ended_effects(nextTurnActor, true);
            this.handle_ongoing_damage(nextTurnActor);
            this.handle_recharge(nextTurnActor);
        }

        private void UpdateUIForNewTurn()
        {
            // Check if anyone died from the previous turn (i.e. ongoing effects)
            this.RemoveDeadEnemies();

            this.fCurrentActor = this.combatState.InitiativeList.CurrentActor;
            Hero hero = Session.Project.FindHero(this.fCurrentActor.ID);
            bool isHero = hero != null;
            if (isHero && this.PlayerMap != null)
            {
                this.PlayerMap.SetNewVisibilitySource(this.fCurrentActor, isHero);
            }

            this.MapView.SetNewVisibilitySource(this.fCurrentActor, isHero);
            
            this.fLog.AddStartTurnEntry(this.fCurrentActor.ID);
            if (this.fCurrentActor.Initiative > this.InitiativePanel.CurrentInitiative)
            {
                this.fCurrentRound++;
                this.RoundLbl.Text = string.Concat("Round: ", this.fCurrentRound);
                this.fLog.AddStartRoundEntry(this.fCurrentRound);
            }

            this.InitiativePanel.CurrentInitiative = this.fCurrentActor.Initiative;
            if (this.fCurrentActor != null && !this.TwoColumnPreview)
            {
                this.select_current_actor();
            }
            this.update_list();
            this.update_log();
            this.update_preview_panel();
            this.highlight_current_actor();
            this.update_maps();
        }

        private void UndoBtn_Click(object sender, EventArgs e)
        {
            CommandManager.GetInstance().UndoLastCommand();
        }

        private void RedoBtn_Click(object sender, EventArgs e)
        {
            CommandManager.GetInstance().RedoNextCommand();
        }

        private void PrevInitBtn_Click(object sender, EventArgs e)
        {
            //InitiativePreviousCommand command = new InitiativePreviousCommand(this.combatState.InitiativeList);
            CommandManager.GetInstance().BackupOneTurn();
            this.UpdateUIForNewTurn();
        }

        private void InitiativeAdvancedHandler()
        {
        }

        private void NextInitBtn_Click(object sender, EventArgs e)
		{
            DataCollection.StartProfile(ProfileLevel.Global, DataCollection.CurrentId);
            try
            {
				if (!this.fCombatStarted)
				{
					this.start_combat();
				}
				else if (this.get_initiatives().Count != 0)
				{
                    CommandManager cmdManager = CommandManager.GetInstance();

                    if (cmdManager.HasFutureTurns)
                    {
                        cmdManager.ForwardOneTurn();
                        this.UpdateUIForNewTurn();
                    }
                    else
                    {
                        cmdManager.NewTurn();
                        cmdManager.BeginCompoundCommand<BeginningOfTurnUpdates>();
                        this.handle_ended_effects(this.fCurrentActor, false);
                        this.handle_saves();

                        cmdManager.ExecuteCommand(new InitiativeAdvanceCommand(this.combatState.InitiativeList));
                        this.RunBeginningOFTurnUpdates(this.combatState.InitiativeList.PeekNextActor());
                        cmdManager.EndAndExecuteCompoundCommand();
                    }
                }
            }
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void OneColumn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.ListSplitter.Orientation != Orientation.Horizontal)
				{
					if (this.fEncounter.MapID == Guid.Empty)
					{
						Session.Preferences.CombatTwoColumnsNoMap = false;
					}
					else
					{
						Session.Preferences.CombatTwoColumns = false;
					}
					this.ListSplitter.Orientation = Orientation.Horizontal;
					this.ListSplitter.SplitterDistance = this.ListSplitter.Height / 2;
					this.MapSplitter.SplitterDistance = 350;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void OptionsIPlay4e_Click(object sender, EventArgs e)
		{
			Session.Preferences.iPlay4E = !Session.Preferences.iPlay4E;
		}

		private void OptionsLandscape_Click(object sender, EventArgs e)
		{
			base.SuspendLayout();
			this.OneColumn_Click(sender, e);
			this.OptionsMapRight_Click(sender, e);
			base.ResumeLayout();
		}

		private void OptionsMapBelow_Click(object sender, EventArgs e)
		{
			if (this.MapSplitter.Orientation == Orientation.Horizontal)
			{
				return;
			}
			this.MapSplitter.Orientation = Orientation.Horizontal;
			this.MapSplitter.SplitterDistance = this.MapSplitter.Height / 2;
			this.MapSplitter.FixedPanel = FixedPanel.None;
			Session.Preferences.CombatMapRight = false;
		}

		private void OptionsMapRight_Click(object sender, EventArgs e)
		{
			if (this.MapSplitter.Orientation == Orientation.Vertical)
			{
				return;
			}
			bool orientation = this.ListSplitter.Orientation == Orientation.Horizontal;
			this.MapSplitter.Orientation = Orientation.Vertical;
			this.MapSplitter.SplitterDistance = (orientation ? 355 : 700);
			this.MapSplitter.FixedPanel = FixedPanel.Panel1;
			Session.Preferences.CombatMapRight = true;
		}

		private void OptionsMenu_DropDownOpening(object sender, EventArgs e)
		{
			bool panel2Collapsed = !this.MapSplitter.Panel2Collapsed;
			this.OptionsShowInit.Checked = this.InitiativePanel.Visible;
			this.OneColumn.Checked = this.ListSplitter.Orientation == Orientation.Horizontal;
			this.TwoColumns.Checked = this.ListSplitter.Orientation == Orientation.Vertical;
			this.OneColumn.Enabled = panel2Collapsed;
			this.TwoColumns.Enabled = panel2Collapsed;
			this.MapRight.Enabled = panel2Collapsed;
			this.MapBelow.Enabled = panel2Collapsed;
			this.MapRight.Checked = this.MapSplitter.Orientation == Orientation.Vertical;
			this.MapBelow.Checked = this.MapSplitter.Orientation == Orientation.Horizontal;
			this.OptionsLandscape.Enabled = panel2Collapsed;
			this.OptionsPortrait.Enabled = panel2Collapsed;
			this.OptionsLandscape.Checked = (!this.OneColumn.Checked ? false : this.MapRight.Checked);
			this.OptionsPortrait.Checked = (!this.TwoColumns.Checked ? false : this.MapBelow.Checked);
			this.ToolsAutoRemove.Checked = Session.Preferences.CreatureAutoRemove;
			this.OptionsIPlay4e.Checked = Session.Preferences.iPlay4E;
		}

		private void OptionsPortrait_Click(object sender, EventArgs e)
		{
			base.SuspendLayout();
			this.TwoColumns_Click(sender, e);
			this.OptionsMapBelow_Click(sender, e);
			base.ResumeLayout();
		}

		private void OptionsShowInit_Click(object sender, EventArgs e)
		{
			this.InitiativePanel.Visible = !this.InitiativePanel.Visible;
		}

		private void PauseBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (MessageBox.Show(string.Concat(string.Concat(string.Concat("Would you like to be able to resume this encounter later?", Environment.NewLine), Environment.NewLine), "If you click Yes, the encounter can be restarted by selecting Paused Encounters from the Project menu."), "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
				{
					CombatState combatState = new CombatState();
					this.fLog.AddPauseEntry();
					Dictionary<Guid, CombatData> guids = new Dictionary<Guid, CombatData>();
					foreach (Hero hero in Session.Project.Heroes)
					{
						guids[hero.ID] = hero.CombatData;
					}
					combatState.Encounter = this.fEncounter;
					combatState.CurrentRound = this.fCurrentRound;
					combatState.PartyLevel = this.fPartyLevel;
					combatState.HeroData = guids;
					combatState.TokenLinks = this.MapView.TokenLinks;
					combatState.RemovedCreatureXP = this.fRemovedCreatureXP;
					combatState.Viewpoint = this.MapView.Viewpoint;
					combatState.Log = this.fLog;
					if (this.fCurrentActor != null)
					{
						combatState.CurrentActor = this.fCurrentActor.ID;
					}
					foreach (MapSketch sketch in this.MapView.Sketches)
					{
						combatState.Sketches.Add(sketch.Copy());
					}
					foreach (OngoingCondition fEffect in this.fEffects)
					{
						combatState.QuickEffects.Add(fEffect.Copy());
					}
					Session.Project.SavedCombats.Add(combatState);
					Session.Modified = true;
					foreach (Form openForm in Application.OpenForms)
					{
						PausedCombatListForm pausedCombatListForm = openForm as PausedCombatListForm;
						if (pausedCombatListForm == null)
						{
							continue;
						}
						pausedCombatListForm.UpdateEncounters();
					}
					this.fPromptOnClose = false;
					base.Close();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void PlayerConditions_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlayerMap != null)
				{
					this.PlayerMap.ShowConditions = !this.PlayerMap.ShowConditions;
					Session.Preferences.PlayerViewConditionBadges = this.PlayerMap.ShowConditions;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void PlayerFogAll_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlayerMap != null)
				{
					this.PlayerMap.ShowCreatures = CreatureViewMode.All;
					Session.Preferences.PlayerViewFog = CreatureViewMode.All;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void PlayerFogNone_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlayerMap != null)
				{
					this.PlayerMap.ShowCreatures = CreatureViewMode.None;
					Session.Preferences.PlayerViewFog = CreatureViewMode.None;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void PlayerFogVisible_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlayerMap != null)
				{
					this.PlayerMap.ShowCreatures = CreatureViewMode.Visible;
					Session.Preferences.PlayerViewFog = CreatureViewMode.Visible;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void PlayerHealth_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlayerMap != null)
				{
					this.PlayerMap.ShowHealthBars = !this.PlayerMap.ShowHealthBars;
					Session.Preferences.PlayerViewHealthBars = this.PlayerMap.ShowHealthBars;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

        private void PlayerViewUseDarkScheme_Click(object sender, EventArgs e)
        {
            if (this.PlayerMap != null)
            {
                this.PlayerMap.UseDarkScheme = !this.PlayerMap.UseDarkScheme;
                this.PlayerMap.Redraw();
            }
        }

        private void PlayerViewVisibility_Click(object sender, EventArgs e)
        {
            if (this.PlayerMap != null)
            {
                this.PlayerMap.ShouldRenderVisibility = !this.PlayerMap.ShouldRenderVisibility;
                this.PlayerMap.RecalculateVisibility();
                this.PlayerMap.Redraw();
            }
        }

        private void PlayerLabels_Click(object sender, EventArgs e)
		{
			try
			{
				Session.Preferences.PlayerViewCreatureLabels = !Session.Preferences.PlayerViewCreatureLabels;
				if (this.PlayerMap != null)
				{
					this.PlayerMap.ShowCreatureLabels = !this.PlayerMap.ShowCreatureLabels;
				}
				if (this.PlayerInitiative != null)
				{
					this.PlayerInitiative.DocumentText = this.InitiativeView();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

        private void PlayerLabelsRequireKnowledge_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Preferences.PlayerViewCreatureLabelsRequireKnowledge = !Session.Preferences.PlayerViewCreatureLabelsRequireKnowledge;
                if (this.PlayerMap != null)
                {
                    this.PlayerMap.CreatureLabelsRequireKnowledge = !this.PlayerMap.CreatureLabelsRequireKnowledge;
                }
                if (this.PlayerInitiative != null)
                {
                    this.PlayerInitiative.DocumentText = this.InitiativeView();
                }
            }
            catch (Exception exception)
            {
                LogSystem.Trace(exception);
            }
        }

        private void PlayerPictureTokens_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlayerMap != null)
				{
					this.PlayerMap.ShowPictureTokens = !this.PlayerMap.ShowPictureTokens;
					Session.Preferences.PlayerViewPictureTokens = this.PlayerMap.ShowPictureTokens;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void PlayerViewGrid_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlayerMap != null)
				{
					this.PlayerMap.ShowGrid = (this.PlayerMap.ShowGrid == MapGridMode.None ? MapGridMode.Overlay : MapGridMode.None);
					Session.Preferences.PlayerViewGrid = this.PlayerMap.ShowGrid == MapGridMode.Overlay;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void PlayerViewGridLabels_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlayerMap != null)
				{
					this.PlayerMap.ShowGridLabels = !this.PlayerMap.ShowGridLabels;
					Session.Preferences.PlayerViewGridLabels = this.PlayerMap.ShowGridLabels;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void PlayerViewInitList_Click(object sender, EventArgs e)
		{
			try
			{
				this.show_player_view(this.PlayerMap != null, this.PlayerInitiative == null);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void PlayerViewLOS_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlayerMap != null)
				{
					this.PlayerMap.LineOfSight = !this.PlayerMap.LineOfSight;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void PlayerViewMap_Click(object sender, EventArgs e)
		{
			try
			{
				this.show_player_view(this.PlayerMap == null, this.PlayerInitiative != null);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void PlayerViewMapMenu_DropDownOpening(object sender, EventArgs e)
		{
			bool flag;
			this.PlayerViewMap.Checked = this.PlayerMap != null;
			this.PlayerViewInitList.Checked = this.PlayerInitiative != null;
			this.PlayerViewLOS.Enabled = this.PlayerMap != null;
			this.PlayerViewLOS.Checked = (this.PlayerMap == null ? false : this.PlayerMap.LineOfSight);
			this.PlayerViewGrid.Enabled = this.PlayerMap != null;
			this.PlayerViewGrid.Checked = (this.PlayerMap == null ? false : this.PlayerMap.ShowGrid != MapGridMode.None);
			this.PlayerViewGridLabels.Enabled = this.PlayerMap != null;
			this.PlayerViewGridLabels.Checked = (this.PlayerMap == null ? false : this.PlayerMap.ShowGridLabels);
			this.PlayerHealth.Enabled = this.PlayerMap != null;
			this.PlayerHealth.Checked = (this.PlayerMap == null ? false : this.PlayerMap.ShowHealthBars);
			this.PlayerConditions.Enabled = this.PlayerMap != null;
			this.PlayerConditions.Checked = (this.PlayerMap == null ? false : this.PlayerMap.ShowConditions);
			this.PlayerPictureTokens.Enabled = this.PlayerMap != null;
			this.PlayerPictureTokens.Checked = (this.PlayerMap == null ? false : this.PlayerMap.ShowPictureTokens);
			this.PlayerLabels.Enabled = (this.PlayerMap != null ? true : this.PlayerInitiative != null);
            this.PlayerLabelsRequireKnowledge.Enabled = (this.PlayerMap != null ? true : this.PlayerInitiative != null);

            this.PlayerViewShowVisibility.Enabled = (this.PlayerMap != null ? true : this.PlayerInitiative != null);
            this.PlayerViewShowVisibility.Checked = (this.PlayerMap == null) ? false : this.PlayerMap.ShouldRenderVisibility;

            this.PlayerViewUseDarkScheme.Enabled = (this.PlayerMap != null ? true : this.PlayerInitiative != null);
            this.PlayerViewUseDarkScheme.Checked = (this.PlayerMap == null) ? false : this.PlayerMap.UseDarkScheme;

            ToolStripMenuItem playerLabels = this.PlayerLabels;
			if (this.PlayerMap == null || !this.PlayerMap.ShowCreatureLabels)
			{
				flag = (this.PlayerInitiative == null ? false : Session.Preferences.PlayerViewCreatureLabels);
			}
			else
			{
				flag = true;
			}
			playerLabels.Checked = flag;
            this.PlayerLabelsRequireKnowledge.Checked = Session.Preferences.PlayerViewCreatureLabelsRequireKnowledge;
			this.PlayerViewFog.Enabled = this.PlayerMap != null;
			this.PlayerFogAll.Checked = (this.PlayerMap == null ? false : this.PlayerMap.ShowCreatures == CreatureViewMode.All);
			this.PlayerFogVisible.Checked = (this.PlayerMap == null ? false : this.PlayerMap.ShowCreatures == CreatureViewMode.Visible);
			this.PlayerFogNone.Checked = (this.PlayerMap == null ? false : this.PlayerMap.ShowCreatures == CreatureViewMode.None);
		}

		private void PlayerViewNoMapMenu_DropDownOpening(object sender, EventArgs e)
		{
			this.PlayerViewNoMapShowInitiativeList.Checked = this.PlayerInitiative != null;
			this.PlayerViewNoMapShowLabels.Enabled = this.PlayerInitiative != null;
			this.PlayerViewNoMapShowLabels.Checked = Session.Preferences.PlayerViewCreatureLabels;
		}

		private void PlayerViewNoMapShowInitiativeList_Click(object sender, EventArgs e)
		{
			try
			{
				this.show_player_view(false, this.PlayerInitiative == null);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void PlayerViewNoMapShowLabels_Click(object sender, EventArgs e)
		{
			Session.Preferences.PlayerViewCreatureLabels = !Session.Preferences.PlayerViewCreatureLabels;
			if (this.PlayerInitiative != null)
			{
				this.PlayerInitiative.DocumentText = this.InitiativeView();
			}
		}

		private void Preview_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			string[] strArrays;
			InitiativeMode initiativeMode;
			try
			{
				if (e.Url.Scheme == "power")
				{
					e.Cancel = true;
					string localPath = e.Url.LocalPath;
					strArrays = new string[] { ";" };
					string[] strArrays1 = localPath.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
					Guid guid = new Guid(strArrays1[0]);
					CombatData combatDatum = this.fEncounter.FindCombatData(guid);
					if (combatDatum == null)
					{
						foreach (Trap trap in this.fEncounter.Traps)
						{
							TrapAttack trapAttack = trap.FindAttack(guid);
							if (trapAttack == null)
							{
								continue;
							}
							this.roll_check(trapAttack.Attack.ToString(), trapAttack.Attack.Bonus);
						}
					}
					else
					{
						EncounterSlot encounterSlot = this.fEncounter.FindSlot(combatDatum);
						if (encounterSlot != null)
						{
							List<CreaturePower> creaturePowers = encounterSlot.Card.CreaturePowers;
							Guid guid1 = new Guid(strArrays1[1]);
							CreaturePower creaturePower = encounterSlot.Card.FindPower(guid1);
							if (creaturePower != null)
							{
								if (creaturePower.Attack != null)
								{
									this.roll_attack(creaturePower);
								}
								this.fLog.AddPowerEntry(combatDatum.ID, creaturePower.Name, true);
								this.update_log();
								if (creaturePower.Action != null && !combatDatum.UsedPowers.Contains(creaturePower.ID) && (creaturePower.Action.Use == PowerUseType.Encounter || creaturePower.Action.Use == PowerUseType.Daily))
								{
									string str = "per-encounter";
									if (creaturePower.Action.Use == PowerUseType.Daily)
									{
										str = "daily";
									}
									if (MessageBox.Show(string.Concat("This is a ", str, " power. Do you want to mark it as expended?"), creaturePower.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.Yes)
									{
										combatDatum.UsedPowers.Add(creaturePower.ID);
										this.update_preview_panel();
									}
								}
							}
							else
							{
								return;
							}
						}
					}
				}
				if (e.Url.Scheme == "refresh")
				{
					e.Cancel = true;
					string localPath1 = e.Url.LocalPath;
					strArrays = new string[] { ";" };
					string[] strArrays2 = localPath1.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
					Guid guid2 = new Guid(strArrays2[0]);
					Guid guid3 = new Guid(strArrays2[1]);
					CombatData combatDatum1 = this.fEncounter.FindCombatData(guid2);
					string name = "";
					EncounterSlot encounterSlot1 = this.fEncounter.FindSlot(combatDatum1);
					if (encounterSlot1 != null)
					{
						ICreature creature = Session.FindCreature(encounterSlot1.Card.CreatureID, SearchType.Global);
						if (creature != null)
						{
							foreach (CreaturePower creaturePower1 in creature.CreaturePowers)
							{
								if (creaturePower1.ID != guid3)
								{
									continue;
								}
								name = creaturePower1.Name;
								break;
							}
						}
					}
					if (!combatDatum1.UsedPowers.Contains(guid3))
					{
						combatDatum1.UsedPowers.Add(guid3);
						this.fLog.AddPowerEntry(combatDatum1.ID, name, true);
					}
					else
					{
						combatDatum1.UsedPowers.Remove(guid3);
						this.fLog.AddPowerEntry(combatDatum1.ID, name, false);
					}
					this.update_preview_panel();
					this.update_log();
				}
				if (e.Url.Scheme == "ability")
				{
					e.Cancel = true;
					int num = int.Parse(e.Url.LocalPath);
					this.roll_check("Ability", num);
				}
				if (e.Url.Scheme == "sc")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "reset")
					{
						SkillChallenge selectedChallenge = this.SelectedChallenge;
						if (selectedChallenge != null)
						{
							foreach (SkillChallengeData skill in selectedChallenge.Skills)
							{
								skill.Results.Successes = 0;
								skill.Results.Fails = 0;
								this.update_list();
								this.update_preview_panel();
							}
						}
					}
				}
				if (e.Url.Scheme == "success")
				{
					e.Cancel = true;
					SkillChallenge skillChallenge = this.SelectedChallenge;
					if (skillChallenge != null)
					{
						SkillChallengeResult results = skillChallenge.FindSkill(e.Url.LocalPath).Results;
						results.Successes = results.Successes + 1;
						this.fLog.AddSkillEntry(this.fCurrentActor.ID, e.Url.LocalPath);
						this.fLog.AddSkillChallengeEntry(this.fCurrentActor.ID, true);
						this.update_list();
						this.update_preview_panel();
						this.update_log();
					}
				}
				if (e.Url.Scheme == "failure")
				{
					e.Cancel = true;
					SkillChallenge selectedChallenge1 = this.SelectedChallenge;
					if (selectedChallenge1 != null)
					{
						SkillChallengeResult fails = selectedChallenge1.FindSkill(e.Url.LocalPath).Results;
						fails.Fails = fails.Fails + 1;
						this.fLog.AddSkillEntry(this.fCurrentActor.ID, e.Url.LocalPath);
						this.fLog.AddSkillChallengeEntry(this.fCurrentActor.ID, false);
						this.update_list();
						this.update_preview_panel();
						this.update_log();
					}
				}
				if (e.Url.Scheme == "dmg")
				{
					e.Cancel = true;
					Guid guid4 = new Guid(e.Url.LocalPath);
					List<IToken> tokens = new List<IToken>();
					Hero hero = Session.Project.FindHero(guid4);
					if (hero != null)
					{
						tokens.Add(hero);
					}
					CombatData combatDatum2 = this.fEncounter.FindCombatData(guid4);
					if (combatDatum2 != null)
					{
						EncounterSlot encounterSlot2 = this.fEncounter.FindSlot(combatDatum2);
						tokens.Add(new CreatureToken(encounterSlot2.ID, combatDatum2));
					}
					if (tokens.Count != 0)
					{
						this.do_damage(tokens);
					}
				}
				if (e.Url.Scheme == "kill")
				{
					e.Cancel = true;
					Guid guid5 = new Guid(e.Url.LocalPath);
					CombatData combatDatum3 = this.fEncounter.FindCombatData(guid5);
					if (combatDatum3 != null)
					{
						combatDatum3.Damage = 1;
						this.fLog.AddStateEntry(combatDatum3.ID, CreatureState.Defeated);
						this.update_list();
						this.update_preview_panel();
						this.update_log();
						this.update_maps();
					}
				}
				if (e.Url.Scheme == "revive")
				{
					e.Cancel = true;
					Guid guid6 = new Guid(e.Url.LocalPath);
					CombatData combatDatum4 = this.fEncounter.FindCombatData(guid6);
					if (combatDatum4 != null)
					{
						combatDatum4.Damage = 0;
						this.fLog.AddStateEntry(combatDatum4.ID, CreatureState.Active);
						this.update_list();
						this.update_preview_panel();
						this.update_log();
						this.update_maps();
					}
				}
				if (e.Url.Scheme == "heal")
				{
					e.Cancel = true;
					Guid guid7 = new Guid(e.Url.LocalPath);
					List<IToken> tokens1 = new List<IToken>();
					Hero hero1 = Session.Project.FindHero(guid7);
					if (hero1 != null)
					{
						tokens1.Add(hero1);
					}
					CombatData combatDatum5 = this.fEncounter.FindCombatData(guid7);
					if (combatDatum5 != null)
					{
						EncounterSlot encounterSlot3 = this.fEncounter.FindSlot(combatDatum5);
						tokens1.Add(new CreatureToken(encounterSlot3.ID, combatDatum5));
					}
					if (tokens1.Count != 0)
					{
						this.do_heal(tokens1);
					}
				}
				if (e.Url.Scheme == "init")
				{
					e.Cancel = true;
					Guid guid8 = new Guid(e.Url.LocalPath);
					int initiative = -2147483648;
					CombatData combatData = this.fEncounter.FindCombatData(guid8);
					if (combatData != null)
					{
						EncounterSlot encounterSlot4 = this.fEncounter.FindSlot(combatData);
						if (encounterSlot4 != null)
						{
							initiative = encounterSlot4.Card.Initiative;
						}
					}
					if (combatData == null)
					{
						Hero hero2 = Session.Project.FindHero(guid8);
						if (hero2 != null)
						{
							combatData = hero2.CombatData;
							initiative = hero2.InitBonus;
						}
					}
					if (combatData == null)
					{
						Trap trap1 = this.fEncounter.FindTrap(guid8);
						if (trap1 != null)
						{
							initiative = trap1.Initiative;
						}
					}
					if (combatData != null && initiative != -2147483648)
					{
						InitiativeForm initiativeForm = new InitiativeForm(initiative, combatData.Initiative);
						if (initiativeForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						{
							combatData.Initiative = initiativeForm.Score;
							this.InitiativePanel.InitiativeScores = this.get_initiatives();
							if (this.fCurrentActor != null)
							{
								this.InitiativePanel.CurrentInitiative = this.fCurrentActor.Initiative;
							}
							this.update_list();
							this.update_preview_panel();
							this.update_maps();
						}
					}
				}
				if (e.Url.Scheme == "effect")
				{
					e.Cancel = true;
					string str1 = e.Url.LocalPath;
					strArrays = new string[] { ":" };
					string[] strArrays3 = str1.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
					if ((int)strArrays3.Length == 2)
					{
						Guid guid9 = new Guid(strArrays3[0]);
						int num1 = int.Parse(strArrays3[1]);
						CombatData combatData1 = this.fEncounter.FindCombatData(guid9);
						if (combatData1 == null)
						{
							Hero hero3 = Session.Project.FindHero(guid9);
							if (hero3 != null)
							{
								combatData1 = hero3.CombatData;
							}
						}
						if (combatData1 != null && num1 >= 0 && num1 <= combatData1.Conditions.Count - 1)
						{
							OngoingCondition item = combatData1.Conditions[num1];
                            CommandManager.GetInstance().ExecuteCommand(new RemoveEffectCommand(combatData1, item));
						}
					}
				}
				if (e.Url.Scheme == "addeffect")
				{
					Hero hero4 = Session.Project.FindHero(this.fCurrentActor.ID);
					int num2 = int.Parse(e.Url.LocalPath);
					OngoingCondition ongoingCondition = hero4.Effects[num2];
					this.apply_effect(ongoingCondition.Copy(), this.SelectedTokens, false);
					this.update_list();
					this.update_preview_panel();
					this.update_log();
					this.update_maps();
				}
				if (e.Url.Scheme == "creatureinit")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "auto")
					{
						initiativeMode = Session.Preferences.InitiativeMode;
						switch (initiativeMode)
						{
							case InitiativeMode.ManualIndividual:
							{
								Session.Preferences.InitiativeMode = InitiativeMode.AutoIndividual;
								break;
							}
							case InitiativeMode.ManualGroup:
							{
								Session.Preferences.InitiativeMode = InitiativeMode.AutoGroup;
								break;
							}
						}
						this.update_preview_panel();
					}
					if (e.Url.LocalPath == "manual")
					{
						initiativeMode = Session.Preferences.InitiativeMode;
						switch (initiativeMode)
						{
							case InitiativeMode.AutoGroup:
							{
								Session.Preferences.InitiativeMode = InitiativeMode.ManualGroup;
								break;
							}
							case InitiativeMode.AutoIndividual:
							{
								Session.Preferences.InitiativeMode = InitiativeMode.ManualIndividual;
								break;
							}
						}
						this.update_preview_panel();
					}
					if (e.Url.LocalPath == "group")
					{
						initiativeMode = Session.Preferences.InitiativeMode;
						switch (initiativeMode)
						{
							case InitiativeMode.AutoIndividual:
							{
								Session.Preferences.InitiativeMode = InitiativeMode.AutoGroup;
								break;
							}
							case InitiativeMode.ManualIndividual:
							{
								Session.Preferences.InitiativeMode = InitiativeMode.ManualGroup;
								break;
							}
						}
						this.update_preview_panel();
					}
					if (e.Url.LocalPath == "individual")
					{
						initiativeMode = Session.Preferences.InitiativeMode;
						if (initiativeMode == InitiativeMode.AutoGroup)
						{
							Session.Preferences.InitiativeMode = InitiativeMode.AutoIndividual;
						}
						else if (initiativeMode == InitiativeMode.ManualGroup)
						{
							Session.Preferences.InitiativeMode = InitiativeMode.ManualIndividual;
						}
						this.update_preview_panel();
					}
				}
				if (e.Url.Scheme == "heroinit")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "auto")
					{
						Session.Preferences.HeroInitiativeMode = InitiativeMode.AutoIndividual;
						this.update_preview_panel();
					}
					if (e.Url.LocalPath == "manual")
					{
						Session.Preferences.HeroInitiativeMode = InitiativeMode.ManualIndividual;
						this.update_preview_panel();
					}
				}
				if (e.Url.Scheme == "trapinit")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "auto")
					{
						Session.Preferences.TrapInitiativeMode = InitiativeMode.AutoIndividual;
						this.update_preview_panel();
					}
					if (e.Url.LocalPath == "manual")
					{
						Session.Preferences.TrapInitiativeMode = InitiativeMode.ManualIndividual;
						this.update_preview_panel();
					}
				}
				if (e.Url.Scheme == "combat")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "sync")
					{
						System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
						foreach (Hero hero5 in Session.Project.Heroes)
						{
							if (hero5.Key == null || hero5.Key == "")
							{
								continue;
							}
							AppImport.ImportIPlay4e(hero5);
							Session.Modified = true;
						}
						System.Windows.Forms.Cursor.Current = Cursors.Default;
					}
					if (e.Url.LocalPath == "hp")
					{
						(new GroupHealthForm()).ShowDialog();
						this.update_list();
						this.update_preview_panel();
						this.update_maps();
					}
					if (e.Url.LocalPath == "rename")
					{
						List<CombatData> combatDatas = new List<CombatData>();
						foreach (EncounterSlot allSlot in this.fEncounter.AllSlots)
						{
							combatDatas.AddRange(allSlot.CombatData);
						}
						(new DisplayNameForm(combatDatas, this.fEncounter)).ShowDialog();
						this.update_list();
						this.update_preview_panel();
						this.update_maps();
					}
					if (e.Url.LocalPath == "start")
					{
						this.start_combat();
					}
				}
                if (e.Url.Scheme == "learn")
                {
                    e.Cancel = true;
                    Guid guid = new Guid(e.Url.LocalPath);
                    CombatData combatDatum = this.fEncounter.FindCombatData(guid);
                    if (combatDatum != null)
                    {
                        EncounterSlot encounterSlot = this.fEncounter.FindSlot(combatDatum);
                        encounterSlot.KnowledgeKnown = !encounterSlot.KnowledgeKnown;
                        //tokens1.Add(new CreatureToken(encounterSlot3.ID, combatDatum5));
                    }

                    if (this.PlayerInitiative != null)
                    {
                        this.PlayerInitiative.DocumentText = this.InitiativeView();
                    }

                }
            }
            catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void remove_effect_from_list(object sender, EventArgs e)
		{
			OngoingCondition tag = (sender as ToolStripItem).Tag as OngoingCondition;
			if (tag == null)
			{
				return;
			}
			if (this.SelectedTokens.Count != 1)
			{
				return;
			}
			CombatData data = null;
			CreatureToken item = this.SelectedTokens[0] as CreatureToken;
			if (item != null)
			{
				data = item.Data;
			}
			Hero hero = this.SelectedTokens[0] as Hero;
			if (hero != null)
			{
				data = hero.CombatData;
			}
			if (data == null)
			{
				return;
			}
            CommandManager.GetInstance().ExecuteCommand(new RemoveEffectCommand(data, tag));
		}

		private void remove_effect_from_map(object sender, EventArgs e)
		{
			OngoingCondition tag = (sender as ToolStripItem).Tag as OngoingCondition;
			if (tag == null)
			{
				return;
			}
			if (this.MapView.SelectedTokens.Count != 1)
			{
				return;
			}
			CombatData data = null;
			CreatureToken item = this.MapView.SelectedTokens[0] as CreatureToken;
			if (item != null)
			{
				data = item.Data;
			}
			Hero hero = this.MapView.SelectedTokens[0] as Hero;
			if (hero != null)
			{
				data = hero.CombatData;
			}
			if (data == null)
			{
				return;
			}
            CommandManager.GetInstance().ExecuteCommand(new RemoveEffectCommand(data, tag));
		}

        private List<RemoveEffectCommand> remove_effects(IToken token)
        {          
            Guid empty = Guid.Empty;
            if (token is CreatureToken)
            {
                empty = (token as CreatureToken).Data.ID;
            }
            if (token is Hero)
            {
                empty = (token as Hero).ID;
            }
            return remove_all_effects_caused_by(empty);
        }

        private List<RemoveEffectCommand> remove_all_effects_caused_by(Guid id)
        {
            List<RemoveEffectCommand> removeList = new List<RemoveEffectCommand>();
            if (id != Guid.Empty)
            {
                foreach (Hero hero in Session.Project.Heroes)
                {
                    removeList.AddRange(this.remove_effects_from_data_caused_by_token(id, hero.CombatData));
                }
                foreach (EncounterSlot allSlot in this.fEncounter.AllSlots)
                {
                    foreach (CombatData combatDatum in allSlot.CombatData)
                    {
                        removeList.AddRange(this.remove_effects_from_data_caused_by_token(id, combatDatum));
                    }
                }
            }

            return removeList;
		}

		private List<RemoveEffectCommand> remove_effects_from_data_caused_by_token(Guid token_id, CombatData data)
		{
            List<RemoveEffectCommand> removeList = new List<RemoveEffectCommand>();
			List<OngoingCondition> ongoingConditions = new List<OngoingCondition>();
			foreach (OngoingCondition condition in data.Conditions)
			{
                if (condition.DurationCreatureID == token_id && condition.Duration == DurationType.BeginningOfTurn || condition.Duration == DurationType.EndOfTurn)
                {
                    removeList.Add(new RemoveEffectCommand(data, condition));
                }
			}

            return removeList;
		}

		private void remove_from_combat(List<IToken> tokens)
		{
			try
			{
                // Queue these up and run them seperately since the tokens list may be modified by the command
                var commands = new List<RemoveFromCombatCommand>();
                foreach (IToken token in tokens)
                {
                    RemoveFromCombatCommand command = null;
                    if (token is CreatureToken)
                    {
                        command = new RemoveFromCombatCommand(token, (token as CreatureToken).Data, this.fEncounter);
                        //this.fRemovedCreatureXP += encounterSlot.Card.XP;
                        command.EffectsToRemove.AddRange(this.remove_effects(token));
                        command.LinksToRemove.AddRange(this.remove_links(token));
                    }
                    else if (token is Hero)
                    {
                        command = new RemoveFromCombatCommand(token, (token as Hero).CombatData, this.fEncounter);
                        command.EffectsToRemove.AddRange(this.remove_effects(token));
                        command.LinksToRemove.AddRange(this.remove_links(token));
                    }
                    else if (token is CustomToken)
                    {
                        CustomToken customToken = token as CustomToken;
                        command = new RemoveFromCombatCommand(token, customToken.Data, this.fEncounter);
                        if (customToken.Type == CustomTokenType.Token)
                        {
                            command.LinksToRemove.AddRange(this.remove_links(token));
                        }
                        else if (customToken.Type == CustomTokenType.Overlay && customToken.IsTerrainLayer)
                        {
                            command.RefreshTerrainLayers = true;
                        }
                    }

                    if (command != null)
                    {
                        commands.Add(command);
                    }
                }

                foreach(var command in commands)
                {
                        CommandManager.GetInstance().ExecuteCommand(command);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void remove_from_map(List<IToken> tokens)
		{
			try
			{
                // Queue these up and run them seperately since the tokens list may be modified by the command
                var commands = new List<RemoveFromMapCommand>();

                foreach (IToken token in tokens)
                {
                    RemoveFromMapCommand command = null;
                    if (token is CreatureToken)
                    {
                        command = new RemoveFromMapCommand((token as CreatureToken).Data);
                        command.EffectsToRemove.AddRange(this.remove_effects(token));
                        command.LinksToRemove.AddRange(this.remove_links(token));
                    }
                    else if (token is Hero)
                    {
                        command = new RemoveFromMapCommand((token as Hero).CombatData);
                        command.EffectsToRemove.AddRange(this.remove_effects(token));
                        command.LinksToRemove.AddRange(this.remove_links(token));
                    }
                    else if (token is CustomToken)
                    {
                        CustomToken noPoint = token as CustomToken;
                        command = new RemoveFromMapCommand(noPoint.Data);
                        if (noPoint.Type == CustomTokenType.Token)
                        {
                            command.LinksToRemove.AddRange(this.remove_links(token));
                        }
                        else if (noPoint.Type == CustomTokenType.Overlay && noPoint.IsTerrainLayer)
                        {
                            command.RefreshTerrainLayers = true;
                        }
                    }

                    if (command != null)
                    {
                        commands.Add(command);
                    }
                }
                foreach(var command in commands)
                { 
                        CommandManager.GetInstance().ExecuteCommand(command);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

        private List<AddRemoveLinkCommand> remove_links(IToken token)
        {
            return remove_links(this.get_location(token));
        }

        private List<AddRemoveLinkCommand> remove_links(Point location)
        {
            List<AddRemoveLinkCommand> linksToRemove = new List<AddRemoveLinkCommand>();
            List<TokenLink> tokenLinks = new List<TokenLink>();
			foreach (TokenLink tokenLink in this.MapView.TokenLinks)
			{
				foreach (IToken token1 in tokenLink.Tokens)
				{
					if (this.get_location(token1) != location)
					{
						continue;
					}
                    linksToRemove.Add(new AddRemoveLinkCommand(AddRemoveLinkCommand.AddRemoveOption.Remove, this.MapView.TokenLinks, tokenLink));
					break;
				}
			}
            return linksToRemove;
		}

		private void ReportBtn_Click(object sender, EventArgs e)
		{
			(new EncounterReportForm(this.fLog, this.fEncounter)).ShowDialog();
		}

		private void roll_attack(CreaturePower power)
		{
			(new AttackRollForm(power, this.fEncounter)).ShowDialog();
			this.update_list();
			this.update_log();
			this.update_preview_panel();
			this.update_maps();
		}

		private void roll_check(string name, int mod)
		{
			int num = Session.Dice(1, 20);
			int num1 = num + mod;
			string str = num.ToString();
			if (num == 1 || num == 20)
			{
				str = string.Concat("Natural ", str);
			}
			object[] objArray = new object[] { "Bonus:\t", mod, Environment.NewLine, "Roll:\t", str, Environment.NewLine, Environment.NewLine, "Result:\t", num1 };
			string str1 = string.Concat(objArray);
			MessageBox.Show(str1, name, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		private void roll_initiative()
		{
            this.combatState.InitiativeList.RollInitiative(this.fEncounter);
			if (this.combatState.InitiativeList.ManualInitiativeDictionary.Count != 0)
			{
				(new GroupInitiativeForm(this.combatState.InitiativeList.ManualInitiativeDictionary, this.fEncounter)).ShowDialog();
			}

            foreach(var combatDatas in this.combatState.InitiativeList.ManualInitiativeDictionary.Values)
            {
                foreach(var data in combatDatas)
                {
                    if (data.Initiative != Int32.MinValue)
                    {
                        this.combatState.InitiativeList.UpdateInitiative(data, data.Initiative);
                    }
                }
            }
			this.InitiativePanel.InitiativeScores = this.get_initiatives();
		}

		private void select_current_actor()
		{
			foreach (ListViewItem item in this.CombatList.Items)
			{
				item.Selected = false;
			}
			ListViewItem _combatant = this.get_combatant(this.fCurrentActor.ID);
			if (_combatant != null)
			{
				_combatant.Selected = true;
			}
		}

		private void select_token(IToken token)
		{
			foreach (ListViewItem item in this.CombatList.Items)
			{
				if (token is CreatureToken && item.Tag is CreatureToken)
				{
					CreatureToken creatureToken = token as CreatureToken;
					CreatureToken tag = item.Tag as CreatureToken;
					if (creatureToken.Data == tag.Data)
					{
						item.Selected = true;
					}
				}
				if (token is CustomToken && item.Tag is CustomToken)
				{
					CustomToken customToken = token as CustomToken;
					CustomToken tag1 = item.Tag as CustomToken;
					if (customToken.Data == tag1.Data)
					{
						item.Selected = true;
					}
				}
				if (!(token is Hero) || !(item.Tag is Hero) || token as Hero != item.Tag as Hero)
				{
					continue;
				}
				item.Selected = true;
			}
		}

		private void set_delay(List<IToken> tokens)
		{
			try
			{
				foreach (IToken token in tokens)
				{
					CombatData data = null;
					if (token is CreatureToken)
					{
						data = (token as CreatureToken).Data;
					}
					if (token is Hero)
					{
						data = (token as Hero).CombatData;
					}
					if (data == null)
					{
						continue;
					}

                    CommandManager.GetInstance().ExecuteCommand(new DelayAction(data, this.combatState));
                    //this.fCurrentActor = this.combatState.InitiativeList.CurrentActor;
				}
				//this.update_list();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void set_map(List<TokenLink> token_links, Rectangle viewpoint, List<MapSketch> sketches)
		{
			Map map = Session.Project.FindTacticalMap(this.fEncounter.MapID);
			this.MapView.Map = map;
			if (token_links == null)
			{
				this.MapView.TokenLinks = new List<TokenLink>();
			}
			else
			{
				this.MapView.TokenLinks = token_links;
				foreach (TokenLink tokenLink in this.MapView.TokenLinks)
				{
					foreach (IToken token in tokenLink.Tokens)
					{
						CreatureToken creatureToken = token as CreatureToken;
						if (creatureToken == null)
						{
							continue;
						}
						EncounterSlot encounterSlot = this.fEncounter.FindSlot(creatureToken.SlotID);
						if (encounterSlot == null)
						{
							continue;
						}
						creatureToken.Data = encounterSlot.FindCombatData(creatureToken.Data.Location);
					}
				}
			}
			if (viewpoint != Rectangle.Empty)
			{
				this.MapView.Viewpoint = viewpoint;
			}
			else if (this.fEncounter.MapAreaID != Guid.Empty)
			{
				MapArea mapArea = map.FindArea(this.fEncounter.MapAreaID);
				if (mapArea != null)
				{
					this.MapView.Viewpoint = mapArea.Region;
				}
			}
			foreach (MapSketch sketch in sketches)
			{
				this.MapView.Sketches.Add(sketch.Copy());
			}
			this.MapView.Encounter = this.fEncounter;
			this.MapView.ShowHealthBars = Session.Preferences.CombatHealthBars;
			this.MapView.ShowCreatures = Session.Preferences.CombatFog;
			this.MapView.ShowPictureTokens = Session.Preferences.CombatPictureTokens;
			this.MapView.ShowGrid = (Session.Preferences.CombatGrid ? MapGridMode.Overlay : MapGridMode.None);
			this.MapView.ShowGridLabels = Session.Preferences.CombatGridLabels;
			if (this.fEncounter.MapID == Guid.Empty)
			{
				this.MapSplitter.Panel2Collapsed = true;
				this.CombatList.Groups[5].Header = "Non-Combatants";
			}
			else if (!Session.Preferences.CombatMapRight)
			{
				this.OptionsMapBelow_Click(null, null);
			}
			if (this.fEncounter.MapID != Guid.Empty && Session.Preferences.CombatTwoColumns)
			{
				this.TwoColumns_Click(null, null);
			}
			if (this.fEncounter.MapID == Guid.Empty && Session.Preferences.CombatTwoColumnsNoMap)
			{
				this.TwoColumns_Click(null, null);
			}

            this.RebuildTerrainLayersAllMaps();
		}

		private void set_tooltip(IToken token, Control ctrl)
		{
			string displayName = "";
			string _info = null;
			if (token is CreatureToken)
			{
				CreatureToken creatureToken = token as CreatureToken;
				displayName = creatureToken.Data.DisplayName;
				_info = this.get_info(creatureToken);
			}
			if (token is Hero)
			{
				Hero hero = token as Hero;
				displayName = hero.Name;
				_info = this.get_info(hero);
			}
			if (token is CustomToken)
			{
				CustomToken customToken = token as CustomToken;
				displayName = customToken.Name;
				_info = this.get_info(customToken);
			}
			this.MapTooltip.ToolTipTitle = displayName;
			this.MapTooltip.SetToolTip(ctrl, _info);
		}

		private void show_or_hide_all(bool visible)
		{
			foreach (EncounterSlot allSlot in this.fEncounter.AllSlots)
			{
				foreach (CombatData combatDatum in allSlot.CombatData)
				{
					combatDatum.Visible = visible;
				}
			}
			foreach (CustomToken customToken in this.fEncounter.CustomTokens)
			{
				customToken.Data.Visible = visible;
			}
			this.update_list();
			this.update_preview_panel();
			this.update_maps();
		}

		private void show_player_view(bool map, bool initiative)
		{
			try
			{
				if (map || initiative)
				{
					if (Session.PlayerView == null)
					{
						Session.PlayerView = new PlayerViewForm(this);
					}
					if (this.PlayerMap == null && this.PlayerInitiative == null)
					{
						Session.PlayerView.ShowTacticalMap(this.MapView, this.InitiativeView());
					}
					SplitContainer item = Session.PlayerView.Controls[0] as SplitContainer;
					if (item != null)
					{
						item.Panel1Collapsed = !map;
						item.Panel2Collapsed = !initiative;
					}
				}
				else
				{
					Session.PlayerView.ShowDefault();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ShowMap_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapSplitter.Panel2Collapsed = !this.MapSplitter.Panel2Collapsed;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

        private CombatData FindFirstHeroInitiative()
        {
            CombatData data = null;
            foreach(var hero in Session.Project.Heroes)
            {
                if (data == null)
                {
                    data = hero.CombatData;
                }
                else if(data.Initiative < hero.CombatData.Initiative)
                {
                    // TODO:  handle case of tied initiative and init bonuses
                    data = hero.CombatData;
                }
            }
            return data;
        }
		private void start_combat()
		{
			this.roll_initiative();
            this.combatState.InitiativeList.StartEncounter();
            this.fCurrentActor = this.combatState.InitiativeList.CurrentActor;
			if (this.fCurrentActor != null)
			{
                this.fCombatStarted = true;
                this.InitiativePanel.CurrentInitiative = this.fCurrentActor.Initiative;
                this.select_current_actor();
                this.update_list();

                bool isHero = Session.Project.FindHero(this.fCurrentActor.ID) != null;
                this.MapView.SetNewVisibilitySource(this.fCurrentActor, isHero);
                if (this.PlayerMap != null)
                {
                    CombatData data = isHero ? this.fCurrentActor : this.FindFirstHeroInitiative();
                    this.PlayerMap?.SetNewVisibilitySource(data, isHero);
                }
                this.RecalculateVisibilityAllMaps();

                this.update_maps();
                this.update_statusbar();
                this.update_preview_panel();
                this.highlight_current_actor();
                this.fLog.Active = true;
                this.fLog.AddStartRoundEntry(this.fCurrentRound);
                this.fLog.AddStartTurnEntry(this.fCurrentActor.ID);
                this.update_log();
            }
        }

		private void TemplateList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			CustomToken tag = this.TemplateList.SelectedItems[0].Tag as CustomToken;
			tag = tag.Copy();
			if (tag.Data.Location == CombatData.NoPoint && base.DoDragDrop(tag, DragDropEffects.Move) == DragDropEffects.Move)
			{
				this.fEncounter.CustomTokens.Add(tag);
				this.update_list();
				this.update_preview_panel();
				this.update_maps();
			}
		}

		private void toggle_visibility(List<IToken> tokens)
		{
			try
			{
                List<CombatData> combatDatas = new List<CombatData>();
				foreach (IToken token in tokens)
				{
					if (token is CreatureToken)
					{
                        combatDatas.Add((token as CreatureToken).Data);
					}
                    else if (token is CustomToken)
                    {
                        combatDatas.Add((token as CustomToken).Data);
                    }
				}
                CommandManager.GetInstance().ExecuteCommand(new VisibilityToggleCommand(combatDatas));
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ToolsAutoRemove_Click(object sender, EventArgs e)
		{
			Session.Preferences.CreatureAutoRemove = !Session.Preferences.CreatureAutoRemove;
		}

		private void ToolsColumns_DropDownOpening(object sender, EventArgs e)
		{
			this.ToolsColumnsInit.Checked = this.InitHdr.Width > 0;
			this.ToolsColumnsHP.Checked = this.HPHdr.Width > 0;
			this.ToolsColumnsDefences.Checked = this.DefHdr.Width > 0;
			this.ToolsColumnsConditions.Checked = this.EffectsHdr.Width > 0;
		}

		private void ToolsColumnsConditions_Click(object sender, EventArgs e)
		{
			this.EffectsHdr.Width = (this.EffectsHdr.Width > 0 ? 0 : 175);
			Session.Preferences.CombatColumnEffects = this.EffectsHdr.Width > 0;
		}

		private void ToolsColumnsDefences_Click(object sender, EventArgs e)
		{
			this.DefHdr.Width = (this.DefHdr.Width > 0 ? 0 : 200);
			Session.Preferences.CombatColumnDefences = this.DefHdr.Width > 0;
		}

		private void ToolsColumnsHP_Click(object sender, EventArgs e)
		{
			this.HPHdr.Width = (this.HPHdr.Width > 0 ? 0 : 60);
			Session.Preferences.CombatColumnHP = this.HPHdr.Width > 0;
		}

		private void ToolsColumnsInit_Click(object sender, EventArgs e)
		{
			this.InitHdr.Width = (this.InitHdr.Width > 0 ? 0 : 60);
			Session.Preferences.CombatColumnInitiative = this.InitHdr.Width > 0;
		}

		private void ToolsMenu_DopDownOpening(object sender, EventArgs e)
		{
			this.ToolsAddIns.DropDownItems.Clear();
			foreach (IAddIn addIn in Session.AddIns)
			{
				ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(addIn.Name)
				{
					ToolTipText = TextHelper.Wrap(addIn.Description),
					Tag = addIn
				};
				this.ToolsAddIns.DropDownItems.Add(toolStripMenuItem);

                //TODO:  Fix this
				//foreach (ICommand combatCommand in addIn.CombatCommands)
				//{
				//	ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem(combatCommand.Name)
				//	{
				//		ToolTipText = TextHelper.Wrap(combatCommand.Description),
				//		Enabled = combatCommand.Available,
				//		Checked = combatCommand.Active
				//	};
				//	toolStripMenuItem1.Click += new EventHandler(this.add_in_command_clicked);
				//	toolStripMenuItem1.Tag = combatCommand;
				//	toolStripMenuItem.DropDownItems.Add(toolStripMenuItem1);
				//}
				if (addIn.Commands.Count != 0)
				{
					continue;
				}
				this.ToolsAddIns.DropDownItems.Add("(no commands)").Enabled = false;
			}
			if (Session.AddIns.Count == 0)
			{
				this.ToolsAddIns.DropDownItems.Add("(none)").Enabled = false;
			}
		}

		private void TwoColumns_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.fEncounter.MapID == Guid.Empty)
				{
					Session.Preferences.CombatTwoColumnsNoMap = true;
				}
				else
				{
					Session.Preferences.CombatTwoColumns = true;
				}
				this.ListSplitter.Orientation = Orientation.Vertical;
				if (this.MapSplitter.Panel2Collapsed || this.MapSplitter.Orientation != Orientation.Vertical)
				{
					this.ListSplitter.SplitterDistance = this.ListSplitter.Width / 2;
				}
				else
				{
					this.MapSplitter.SplitterDistance = 700;
					this.ListSplitter.SplitterDistance = 350;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void update_effects_list(ToolStripDropDownItem tsddi, bool use_list_selection)
		{
			tsddi.DropDownItems.Clear();
			ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("Standard Conditions");
			tsddi.DropDownItems.Add(toolStripMenuItem);
			foreach (string condition in Conditions.GetConditions())
			{
				OngoingCondition ongoingCondition = new OngoingCondition()
				{
					Data = condition,
					Duration = DurationType.Encounter
				};
				ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem(ongoingCondition.ToString(this.fEncounter, false))
				{
					Tag = ongoingCondition
				};
				if (!use_list_selection)
				{
					toolStripMenuItem1.Click += new EventHandler(this.apply_quick_effect_from_map);
				}
				else
				{
					toolStripMenuItem1.Click += new EventHandler(this.apply_quick_effect_from_toolbar);
				}
				toolStripMenuItem.DropDownItems.Add(toolStripMenuItem1);
			}
			tsddi.DropDownItems.Add(new ToolStripSeparator());
			bool flag = false;
			foreach (Hero hero in Session.Project.Heroes)
			{
				if (hero.Effects.Count == 0)
				{
					continue;
				}
				ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(hero.Name);
				tsddi.DropDownItems.Add(toolStripMenuItem2);
				foreach (OngoingCondition effect in hero.Effects)
				{
					ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem(effect.ToString(this.fEncounter, false))
					{
						Tag = effect.Copy()
					};
					if (!use_list_selection)
					{
						toolStripMenuItem3.Click += new EventHandler(this.apply_quick_effect_from_map);
					}
					else
					{
						toolStripMenuItem3.Click += new EventHandler(this.apply_quick_effect_from_toolbar);
					}
					toolStripMenuItem2.DropDownItems.Add(toolStripMenuItem3);
					flag = true;
				}
			}
			if (flag)
			{
				tsddi.DropDownItems.Add(new ToolStripSeparator());
			}
			foreach (OngoingCondition fEffect in this.fEffects)
			{
				ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem(fEffect.ToString(this.fEncounter, false))
				{
					Tag = fEffect.Copy()
				};
				if (!use_list_selection)
				{
					toolStripMenuItem4.Click += new EventHandler(this.apply_quick_effect_from_map);
				}
				else
				{
					toolStripMenuItem4.Click += new EventHandler(this.apply_quick_effect_from_toolbar);
				}
				tsddi.DropDownItems.Add(toolStripMenuItem4);
			}
			if (this.fEffects.Count != 0)
			{
				tsddi.DropDownItems.Add(new ToolStripSeparator());
			}
			ToolStripMenuItem toolStripMenuItem5 = new ToolStripMenuItem("Add a New Effect...");
			if (!use_list_selection)
			{
				toolStripMenuItem5.Click += new EventHandler(this.apply_effect_from_map);
			}
			else
			{
				toolStripMenuItem5.Click += new EventHandler(this.apply_effect_from_toolbar);
			}
			tsddi.DropDownItems.Add(toolStripMenuItem5);
		}

		private void update_list()
		{
			object obj;
			object[] tempHP;
			List<CombatData> combatDatas = new List<CombatData>();

			int num = 0;
			int num1 = 1;
			int num2 = 2;
			int num3 = 3;
			int num4 = 4;
			int num5 = 5;
			int num6 = 6;
            int num10 = 7;
			List<IToken> selectedTokens = this.SelectedTokens;
			Trap selectedTrap = this.SelectedTrap;
			SkillChallenge selectedChallenge = this.SelectedChallenge;
			this.CombatList.BeginUpdate();
			this.CombatList.Items.Clear();
			this.CombatList.SmallImageList = new ImageList()
			{
				ImageSize = new System.Drawing.Size(16, 16)
			};
			foreach (EncounterSlot encounterSlot in this.fEncounter.AllSlots)
			{
				EncounterWave encounterWave = this.fEncounter.FindWave(encounterSlot);
				if (encounterWave != null && !encounterWave.Active)
				{
					continue;
				}
				int hP = encounterSlot.Card.HP;
				ICreature creature = Session.FindCreature(encounterSlot.Card.CreatureID, SearchType.Global);
				foreach (CombatData combatDatum1 in encounterSlot.CombatData)
				{
					int damage = hP - combatDatum1.Damage;
					string str = damage.ToString();
					if (combatDatum1.TempHP > 0)
					{
						obj = str;
						tempHP = new object[] { obj, " (+", combatDatum1.TempHP, ")" };
						str = string.Concat(tempHP);
					}
					if (damage != hP)
					{
						str = string.Concat(str, " / ", hP);
					}
					string str1 = combatDatum1.Initiative.ToString();
					if (combatDatum1.Delaying)
					{
						str1 = string.Concat("(", str1, ")");
					}

                    string displayName = string.Format("[{0}] {1}", TextHelper.Abbreviation(combatDatum1.DisplayName), combatDatum1.DisplayName);
					ListViewItem grayText = this.CombatList.Items.Add(displayName);
					grayText.Tag = new CreatureToken(encounterSlot.ID, combatDatum1);
					if (combatDatum1.Initiative == -2147483648)
					{
						grayText.ForeColor = SystemColors.GrayText;
						str1 = "-";
					}
					int aC = encounterSlot.Card.AC;
					int fortitude = encounterSlot.Card.Fortitude;
					int reflex = encounterSlot.Card.Reflex;
					int will = encounterSlot.Card.Will;
					foreach (OngoingCondition condition in combatDatum1.Conditions)
					{
						if (condition.Type != OngoingType.DefenceModifier)
						{
							continue;
						}
						if (condition.Defences.Contains(DefenceType.AC))
						{
							aC += condition.DefenceMod;
						}
						if (condition.Defences.Contains(DefenceType.Fortitude))
						{
							fortitude += condition.DefenceMod;
						}
						if (condition.Defences.Contains(DefenceType.Reflex))
						{
							reflex += condition.DefenceMod;
						}
						if (!condition.Defences.Contains(DefenceType.Will))
						{
							continue;
						}
						will += condition.DefenceMod;
					}
					tempHP = new object[] { "AC ", aC, ", Fort ", fortitude, ", Ref ", reflex, ", Will ", will };
					string str2 = string.Concat(tempHP);
					string _conditions = this.get_conditions(combatDatum1);
					grayText.SubItems.Add(str1);
					grayText.SubItems.Add(str);
					grayText.SubItems.Add(str2);
					grayText.SubItems.Add(_conditions);
					switch (encounterSlot.GetState(combatDatum1))
					{
						case CreatureState.Bloodied:
						{
							grayText.ForeColor = Color.Maroon;
							break;
						}
						case CreatureState.Defeated:
						{
							grayText.ForeColor = SystemColors.GrayText;
							break;
						}
					}
					if (!combatDatum1.Visible)
					{
						grayText.ForeColor = Color.FromArgb(80, grayText.ForeColor);
						ListViewItem listViewItem = grayText;
						listViewItem.Text = string.Concat(listViewItem.Text, " (hidden)");
					}
					if (creature == null || creature.Image == null)
					{
						this.add_icon(grayText, grayText.ForeColor);
					}
					else
					{
						this.CombatList.SmallImageList.Images.Add(new Bitmap(creature.Image, 16, 16));
						grayText.ImageIndex = this.CombatList.SmallImageList.Images.Count - 1;
					}
					if (combatDatum1.Conditions.Count != 0)
					{
						this.add_condition_hint(grayText);
					}
					int num7 = num;
					if (combatDatum1.Initiative == -2147483648)
					{
						num7 = num5;
					}
					if (combatDatum1.Delaying)
					{
						num7 = num1;
					}
					if (this.MapView.Map != null && combatDatum1.Location == CombatData.NoPoint)
					{
						num7 = num5;
					}
					if (encounterSlot.GetState(combatDatum1) == CreatureState.Defeated)
					{
						num7 = num6;
					}
					grayText.Group = this.CombatList.Groups[num7];
					if (combatDatum1 == this.fCurrentActor)
					{
						grayText.Font = new System.Drawing.Font(grayText.Font, grayText.Font.Style | FontStyle.Bold);
						grayText.UseItemStyleForSubItems = false;
						grayText.BackColor = Color.LightBlue;
						this.add_initiative_hint(grayText);
					}
					foreach (IToken selectedToken in selectedTokens)
					{
						CreatureToken creatureToken1 = selectedToken as CreatureToken;
						if (creatureToken1 == null || creatureToken1.Data != combatDatum1)
						{
							continue;
						}
						grayText.Selected = true;
					}
				}
			}
			foreach (Trap trap in this.fEncounter.Traps)
			{
				ListViewItem item = this.CombatList.Items.Add(trap.Name);
				item.Tag = trap;
				this.add_icon(item, Color.White);
				if (trap.Initiative == Int32.MinValue)
				{
					item.SubItems.Add("-");
					item.Group = this.CombatList.Groups[num2];
				}
				else
				{
					CombatData item1 = trap.CombatData;
					if (item1 == null || item1.Initiative == Int32.MinValue)
					{
						item.SubItems.Add("-");
						item.Group = this.CombatList.Groups[num5];
					}
					else
					{
						string str3 = item1.Initiative.ToString();
						item.SubItems.Add(str3);
						item.Group = this.CombatList.Groups[num];
					}
					if (item1 == this.fCurrentActor)
					{
						item.Font = new System.Drawing.Font(item.Font, item.Font.Style | FontStyle.Bold);
						item.UseItemStyleForSubItems = false;
						item.BackColor = Color.LightBlue;
						this.add_initiative_hint(item);
					}
				}
				item.SubItems.Add("-");
				item.SubItems.Add("-");
				item.SubItems.Add("-");
				if (trap != selectedTrap)
				{
					continue;
				}
				item.Selected = true;
			}
			foreach (SkillChallenge skillChallenge in this.fEncounter.SkillChallenges)
			{
				ListViewItem listViewItem1 = this.CombatList.Items.Add(skillChallenge.Name);
				listViewItem1.SubItems.Add("-");
				listViewItem1.SubItems.Add("-");
				listViewItem1.SubItems.Add("-");
				ListViewItem.ListViewSubItemCollection subItems = listViewItem1.SubItems;
				tempHP = new object[] { skillChallenge.Results.Successes, " / ", skillChallenge.Successes, " successes; ", skillChallenge.Results.Fails, " / 3 failures" };
				subItems.Add(string.Concat(tempHP));
				this.add_icon(listViewItem1, Color.White);
				listViewItem1.Tag = skillChallenge;
				listViewItem1.Group = this.CombatList.Groups[num3];
				if (skillChallenge != selectedChallenge)
				{
					continue;
				}
				listViewItem1.Selected = true;
			}
			foreach (Hero hero in Session.Project.Heroes)
			{
				int num8 = num;
				ListViewItem windowText = this.CombatList.Items.Add(hero.Name);
				windowText.Tag = hero;
				CombatData combatData1 = hero.CombatData;
				switch (hero.GetState(combatData1.Damage))
				{
					case CreatureState.Active:
					{
						windowText.ForeColor = SystemColors.WindowText;
						break;
					}
					case CreatureState.Bloodied:
					{
						windowText.ForeColor = Color.Maroon;
						break;
					}
					case CreatureState.Defeated:
					{
						windowText.ForeColor = SystemColors.GrayText;
						break;
					}
				}
				if (hero.Portrait != null)
				{
					this.CombatList.SmallImageList.Images.Add(new Bitmap(hero.Portrait, 16, 16));
					windowText.ImageIndex = this.CombatList.SmallImageList.Images.Count - 1;
				}
				else if (hero.Key == "")
				{
					this.add_icon(windowText, Color.Green);
				}
				else
				{
					this.CombatList.SmallImageList.Images.Add(new Bitmap(Resources.Purpled20, 16, 16));
					windowText.ImageIndex = this.CombatList.SmallImageList.Images.Count - 1;
				}
				if (combatData1.Conditions.Count != 0)
				{
					this.add_condition_hint(windowText);
				}
				string str4 = "";
				int initiative = combatData1.Initiative;
				if (initiative != -2147483648)
				{
					str4 = initiative.ToString();
					if (combatData1.Delaying)
					{
						str4 = string.Concat("(", str4, ")");
						num8 = num1;
					}
					if (combatData1 == this.fCurrentActor)
					{
						windowText.Font = new System.Drawing.Font(windowText.Font, windowText.Font.Style | FontStyle.Bold);
						windowText.UseItemStyleForSubItems = false;
						windowText.BackColor = Color.LightBlue;
						this.add_initiative_hint(windowText);
					}
				}
				else
				{
					windowText.ForeColor = SystemColors.GrayText;
					num8 = num5;
					str4 = "-";
				}
				string str5 = "";
				if (hero.HP == 0)
				{
					str5 = "-";
				}
				else
				{
					int hP1 = hero.HP - combatData1.Damage;
					str5 = hP1.ToString();
					if (combatData1.TempHP > 0)
					{
						obj = str5;
						tempHP = new object[] { obj, " (+", combatData1.TempHP, ")" };
						str5 = string.Concat(tempHP);
					}
					if (hP1 != hero.HP)
					{
						str5 = string.Concat(str5, " / ", hero.HP);
					}
				}
				windowText.SubItems.Add(str4);
				windowText.SubItems.Add(str5);
				if (hero.AC == 0 || hero.Fortitude == 0 || hero.Reflex == 0 || hero.Will == 0)
				{
					windowText.SubItems.Add("-");
				}
				else
				{
					int defenceMod = hero.AC;
					int fortitude1 = hero.Fortitude;
					int reflex1 = hero.Reflex;
					int will1 = hero.Will;
					foreach (OngoingCondition ongoingCondition in combatData1.Conditions)
					{
						if (ongoingCondition.Type != OngoingType.DefenceModifier)
						{
							continue;
						}
						if (ongoingCondition.Defences.Contains(DefenceType.AC))
						{
							defenceMod += ongoingCondition.DefenceMod;
						}
						if (ongoingCondition.Defences.Contains(DefenceType.Fortitude))
						{
							fortitude1 += ongoingCondition.DefenceMod;
						}
						if (ongoingCondition.Defences.Contains(DefenceType.Reflex))
						{
							reflex1 += ongoingCondition.DefenceMod;
						}
						if (!ongoingCondition.Defences.Contains(DefenceType.Will))
						{
							continue;
						}
						will1 += ongoingCondition.DefenceMod;
					}
					tempHP = new object[] { "AC ", defenceMod, ", Fort ", fortitude1, ", Ref ", reflex1, ", Will ", will1 };
					string str6 = string.Concat(tempHP);
					windowText.SubItems.Add(str6);
				}
				windowText.SubItems.Add(this.get_conditions(combatData1));
				if (this.MapView.Map != null && hero.CombatData.Location == CombatData.NoPoint)
				{
					num8 = num5;
				}
				windowText.Group = this.CombatList.Groups[num8];
				if (!selectedTokens.Contains(hero))
				{
					continue;
				}
				windowText.Selected = true;
			}
			foreach (CustomToken customToken in this.fEncounter.CustomTokens)
			{
				ListViewItem grayText1 = this.CombatList.Items.Add(customToken.Name);
				grayText1.Tag = customToken;
				this.add_icon(grayText1, Color.Blue);
				int num9 = num4;
				if (this.MapView.Map != null && customToken.Data.Location == CombatData.NoPoint && customToken.CreatureID == Guid.Empty)
				{
					num9 = num5;
					grayText1.ForeColor = SystemColors.GrayText;
				}
                if (customToken.IsTerrainLayer)
                {
                    num9 = 7;
                }
				grayText1.Group = this.CombatList.Groups[num9];
				if (!selectedTokens.Contains(customToken))
				{
					continue;
				}
				grayText1.Selected = true;
			}
			this.CombatList.Sort();
			this.CombatList.EndUpdate();
			if (this.PlayerInitiative != null)
			{
				this.PlayerInitiative.DocumentText = this.InitiativeView();
			}
		}

		private void update_log()
		{
			//string str = this.EncounterLogView(false);
			//this.LogBrowser.Document.OpenNew(true);
			//this.LogBrowser.Document.Write(str);
		}

        private void RebuildAllViews()
        {
            this.MapView.CompleteRefresh();
            this.PlayerMap?.CompleteRefresh();
        }

        private void RecalculateVisibilityAllMaps()
        {
            this.MapView.RecalculateVisibility();
            this.PlayerMap?.RecalculateVisibility();
        }

        private void RebuildTerrainLayersAllMaps()
        {
            this.MapView.RebuildTerrainLayer();
            this.PlayerMap?.RebuildTerrainLayer();

            CombatForm.TerrainLayersNeedRefresh = false;
        }

		private void update_maps()
		{
            this.MapView.Redraw();
            this.PlayerMap?.Redraw();
		}

		private void update_preview_panel()
		{
			if (Session.Preferences.iPlay4E && this.SelectedTokens.Count == 1)
			{
				Hero item = this.SelectedTokens[0] as Hero;
				if (item != null && item.Key != "")
				{
					this.Preview.Document.OpenNew(true);
					this.Preview.Document.Write(HTML.Text("Loading iPlay4e character, please wait...", true, true, DisplaySize.Small));
					string str = string.Concat("http://iplay4e.appspot.com/view?xsl=jPint&key=", item.Key);
					this.Preview.Navigate(str);
					return;
				}
			}
			string str1 = "";
			str1 = string.Concat(str1, "<HTML>");
			str1 = string.Concat(str1, HTML.Concatenate(HTML.GetHead("", "", DisplaySize.Small)));
			str1 = string.Concat(str1, "<BODY>");
			if (!this.fCombatStarted)
			{
				str1 = string.Concat(str1, this.html_encounter_start());
			}
			else
			{
				List<IToken> selectedTokens = this.SelectedTokens;
				if (this.TwoColumnPreview)
				{
					List<IToken> tokens = new List<IToken>();
					foreach (IToken selectedToken in selectedTokens)
					{
						CreatureToken creatureToken = selectedToken as CreatureToken;
						if (creatureToken != null && creatureToken.Data.ID == this.fCurrentActor.ID)
						{
							tokens.Add(selectedToken);
						}
						Hero hero = selectedToken as Hero;
						if (hero == null || !(hero.ID == this.fCurrentActor.ID))
						{
							continue;
						}
						tokens.Add(selectedToken);
					}
					foreach (IToken token in tokens)
					{
						selectedTokens.Remove(token);
					}
				}
				if (this.TwoColumnPreview)
				{
					str1 = string.Concat(str1, "<P class=table>");
					str1 = string.Concat(str1, "<TABLE class=clear>");
					str1 = string.Concat(str1, "<TR class=clear>");
					str1 = string.Concat(str1, "<TD class=clear>");
					EncounterSlot encounterSlot = this.fEncounter.FindSlot(this.fCurrentActor);
					if (encounterSlot != null)
					{
						str1 = string.Concat(str1, HTML.StatBlock(encounterSlot.Card, this.fCurrentActor, this.fEncounter, false, true, true, CardMode.Combat, DisplaySize.Small));
					}
					Hero hero1 = Session.Project.FindHero(this.fCurrentActor.ID);
					if (hero1 != null)
					{
						bool count = selectedTokens.Count != 0;
						str1 = string.Concat(str1, HTML.StatBlock(hero1, this.fEncounter, false, true, count, DisplaySize.Small));
					}
					Trap trap = this.fEncounter.FindTrap(this.fCurrentActor.ID);
					if (trap != null)
					{
						str1 = string.Concat(str1, HTML.Trap(trap, this.fCurrentActor, false, true, false, DisplaySize.Small));
					}
					str1 = string.Concat(str1, "</TD>");
					str1 = string.Concat(str1, "<TD class=clear>");
				}
				string str2 = "";
				if (selectedTokens.Count != 0)
				{
					str2 = this.html_tokens(selectedTokens);
				}
				else if (this.SelectedTrap != null)
				{
					str2 = this.html_trap();
				}
				else if (this.SelectedChallenge != null)
				{
					str2 = this.html_skill_challenge();
				}
				str1 = (str2 == "" ? string.Concat(str1, this.html_encounter_overview()) : string.Concat(str1, str2));
				if (this.TwoColumnPreview)
				{
					str1 = string.Concat(str1, "</TD>");
					str1 = string.Concat(str1, "</TR>");
					str1 = string.Concat(str1, "</TABLE>");
					str1 = string.Concat(str1, "</P>");
				}
			}
			str1 = string.Concat(str1, "</BODY>");
			str1 = string.Concat(str1, "</HTML>");

            // TODO:  Sometimes window can be disposed?!  Dunno what to do about that at this point, though, but might as well not take everything down with it.
            if (!this.Preview.IsDisposed)
            {
                this.Preview.Document.OpenNew(true);
                this.Preview.Document.Write(str1);
            }
		}

		private void update_remove_effect_list(ToolStripDropDownItem tsddi, bool use_list_selection)
		{
			tsddi.DropDownItems.Clear();
			List<IToken> tokens = (use_list_selection ? this.SelectedTokens : this.MapView.SelectedTokens);
			if (tokens.Count != 1)
			{
				ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("(multiple selection)")
				{
					Enabled = false
				};
				tsddi.DropDownItems.Add(toolStripMenuItem);
				return;
			}
			CombatData data = null;
			CreatureToken item = tokens[0] as CreatureToken;
			if (item != null)
			{
				data = item.Data;
			}
			Hero hero = tokens[0] as Hero;
			if (hero != null)
			{
				data = hero.CombatData;
			}
			if (data != null)
			{
				foreach (OngoingCondition condition in data.Conditions)
				{
					ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem(condition.ToString(this.fEncounter, false))
					{
						Tag = condition
					};
					if (!use_list_selection)
					{
						toolStripMenuItem1.Click += new EventHandler(this.remove_effect_from_map);
					}
					else
					{
						toolStripMenuItem1.Click += new EventHandler(this.remove_effect_from_list);
					}
					tsddi.DropDownItems.Add(toolStripMenuItem1);
				}
			}
			if (tsddi.DropDownItems.Count == 0)
			{
				ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem("(no effects)")
				{
					Enabled = false
				};
				tsddi.DropDownItems.Add(toolStripMenuItem2);
			}
		}

		private void update_statusbar()
		{
			int xP = this.fEncounter.GetXP() + this.fRemovedCreatureXP;
			this.XPLbl.Text = string.Concat(xP, " XP");
			int num = 0;
			foreach (Hero hero in Session.Project.Heroes)
			{
				if (hero.CombatData.Initiative == -2147483648)
				{
					continue;
				}
				num++;
			}
			if (num > 1)
			{
				ToolStripStatusLabel xPLbl = this.XPLbl;
				object text = xPLbl.Text;
				object[] objArray = new object[] { text, " (", xP / num, " XP each)" };
				xPLbl.Text = string.Concat(objArray);
			}
			int level = this.fEncounter.GetLevel(num);
			this.LevelLbl.Text = (level != -1 ? string.Concat("Encounter Level: ", level) : "");
		}

		private void wave_activated(object sender, EventArgs e)
		{
			EncounterWave tag = (sender as ToolStripMenuItem).Tag as EncounterWave;
			tag.Active = !tag.Active;
			this.update_list();
			this.update_maps();
			this.update_statusbar();
		}

		private void ZoomGauge_Scroll(object sender, EventArgs e)
		{
			try
			{
				double num = 10;
				double num1 = 1;
				double num2 = 0.1;
				double value = (double)(this.ZoomGauge.Value - this.ZoomGauge.Minimum) / (double)(this.ZoomGauge.Maximum - this.ZoomGauge.Minimum);
				if (value < 0.5)
				{
					value *= 2;
					this.MapView.ScalingFactor = num2 + value * (num1 - num2);
				}
				else
				{
					value -= 0.5;
					value *= 2;
					this.MapView.ScalingFactor = num1 + value * (num - num1);
				}

                this.RebuildAllViews();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		public class CombatListControl : ListView
		{
			public CombatListControl()
			{
				this.DoubleBuffered = true;
			}
		}

		private class InitiativeSorter : IComparer, IComparer<ListViewItem>
		{
			private Encounter fEncounter;

			public InitiativeSorter(Encounter enc)
			{
				this.fEncounter = enc;
			}

			public int Compare(object x, object y)
			{
				ListViewItem listViewItem = x as ListViewItem;
				ListViewItem listViewItem1 = y as ListViewItem;
				if (listViewItem == null || listViewItem1 == null)
				{
					return 0;
				}
				return this.Compare(listViewItem, listViewItem1);
			}

			public int Compare(ListViewItem lvi_x, ListViewItem lvi_y)
			{
				int _score = this.get_score(lvi_x);
				int num = _score.CompareTo(this.get_score(lvi_y));
				if (num == 0)
				{
					int _bonus = this.get_bonus(lvi_x);
					num = _bonus.CompareTo(this.get_bonus(lvi_y));
				}
				if (num == 0)
				{
					string text = lvi_x.Text;
					num = text.CompareTo(lvi_y.Text) * -1;
				}
				return -num;
			}

			private int get_bonus(ListViewItem lvi)
			{
				int initBonus;
				try
				{
					if (lvi.Tag is Hero)
					{
						initBonus = (lvi.Tag as Hero).InitBonus;
					}
					else if (lvi.Tag is CreatureToken)
					{
						CreatureToken tag = lvi.Tag as CreatureToken;
						EncounterSlot encounterSlot = this.fEncounter.FindSlot(tag.SlotID);
						initBonus = (encounterSlot != null ? encounterSlot.Card.Initiative : 0);
					}
					else if (!(lvi.Tag is Trap))
					{
						return 0;
					}
					else
					{
						Trap trap = lvi.Tag as Trap;
						initBonus = (trap.Initiative != -2147483648 ? trap.Initiative : 0);
					}
				}
				catch (Exception exception)
				{
					LogSystem.Trace(exception);
					return 0;
				}
				return initBonus;
			}

			private int get_score(ListViewItem lvi)
			{
				int initiative;
				try
				{
					if (lvi.Tag is Hero)
					{
						initiative = (lvi.Tag as Hero).CombatData.Initiative;
					}
					else if (lvi.Tag is CreatureToken)
					{
						initiative = (lvi.Tag as CreatureToken).Data.Initiative;
					}
					else if (!(lvi.Tag is Trap))
					{
						return 0;
					}
					else
					{
						Trap tag = lvi.Tag as Trap;
						initiative = (tag.CombatData == null ? Int32.MinValue : tag.CombatData.Initiative);
					}
				}
				catch (Exception exception)
				{
					LogSystem.Trace(exception);
					return 0;
				}
				return initiative;
			}
		}
	}
}