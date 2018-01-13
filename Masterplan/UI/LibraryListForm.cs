using Masterplan;
using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class LibraryListForm : Form
	{
		private IContainer components;

		private SplitContainer Splitter;

		private ToolStrip LibraryToolbar;

		private ToolStripButton LibraryRemoveBtn;

		private ToolStripButton LibraryEditBtn;

		private ListView CreatureList;

		private ToolStrip CreatureToolbar;

		private ToolStripButton OppRemoveBtn;

		private ToolStripButton OppEditBtn;

		private ColumnHeader CreatureNameHdr;

		private ColumnHeader CreatureInfoHdr;

		private TabControl Pages;

		private TabPage CreaturesPage;

		private TabPage TemplatesPage;

		private ToolStrip TemplateToolbar;

		private ToolStripButton TemplateRemoveBtn;

		private ToolStripButton TemplateEditBtn;

		private ListView TemplateList;

		private ColumnHeader TemplateNameHdr;

		private TabPage TilesPage;

		private ListView TileList;

		private ColumnHeader TileSetNameHdr;

		private ToolStrip TileToolbar;

		private ToolStripButton TileRemoveBtn;

		private ToolStripButton TileEditBtn;

		private ColumnHeader TemplateInfoHdr;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton CreatureCutBtn;

		private ToolStripButton CreatureCopyBtn;

		private ToolStripButton CreaturePasteBtn;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton TemplateCutBtn;

		private ToolStripButton TemplateCopyBtn;

		private ToolStripButton TemplatePasteBtn;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton TileCutBtn;

		private ToolStripButton TileCopyBtn;

		private ToolStripButton TilePasteBtn;

		private ToolStripSeparator toolStripSeparator4;

		private TabPage TrapsPage;

		private ListView TrapList;

		private ColumnHeader TrapNameHdr;

		private ColumnHeader TrapInfoHdr;

		private ToolStrip TrapToolbar;

		private ToolStripButton TrapRemoveBtn;

		private ToolStripButton TrapEditBtn;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripButton TrapCutBtn;

		private ToolStripButton TrapCopyBtn;

		private ToolStripButton TrapPasteBtn;

		private TabPage ChallengePage;

		private ListView ChallengeList;

		private ColumnHeader ChallengeNameHdr;

		private ColumnHeader ChallengeInfoHdr;

		private ToolStrip ChallengeToolbar;

		private ToolStripButton ChallengeRemoveBtn;

		private ToolStripButton ChallengeEditBtn;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripButton ChallengeCutBtn;

		private ToolStripButton ChallengeCopyBtn;

		private ToolStripButton ChallengePasteBtn;

		private System.Windows.Forms.ContextMenuStrip CreatureContext;

		private ToolStripMenuItem CreatureContextRemove;

		private ToolStripMenuItem CreatureContextCategory;

		private System.Windows.Forms.ContextMenuStrip TileContext;

		private ToolStripMenuItem TileContextRemove;

		private ToolStripMenuItem TileContextCategory;

		private ToolStripMenuItem TilePlain;

		private ToolStripMenuItem TileDoorway;

		private ToolStripMenuItem TileStairway;

		private ToolStripMenuItem TileFeature;

		private ToolStripMenuItem TileSpecial;

		private System.Windows.Forms.ContextMenuStrip TemplateContext;

		private ToolStripMenuItem TemplateContextRemove;

		private ToolStripMenuItem TemplateContextType;

		private ToolStripMenuItem TemplateFunctional;

		private ToolStripMenuItem TemplateClass;

		private System.Windows.Forms.ContextMenuStrip TrapContext;

		private ToolStripMenuItem TrapContextRemove;

		private ToolStripMenuItem TrapContextType;

		private ToolStripMenuItem TrapTrap;

		private ToolStripMenuItem TrapHazard;

		private System.Windows.Forms.ContextMenuStrip ChallengeContext;

		private ToolStripMenuItem ChallengeContextRemove;

		private ToolStripButton CreatureStatBlockBtn;

		private ToolStripSeparator toolStripSeparator8;

		private ToolStripButton TrapStatBlockBtn;

		private ToolStripSeparator toolStripSeparator9;

		private ToolStripButton ChallengeStatBlockBtn;

		private ToolStripSeparator toolStripSeparator10;

		private ToolStrip CreatureSearchToolbar;

		private ToolStripLabel SearchLbl;

		private ToolStripTextBox SearchBox;

		private ToolStripSeparator toolStripSeparator11;

		private ToolStripButton CategorisedBtn;

		private ToolStripButton UncategorisedBtn;

		private TabPage MagicItemsPage;

		private ListView MagicItemList;

		private ColumnHeader MagicItemHdr;

		private ToolStrip MagicItemToolbar;

		private System.Windows.Forms.ContextMenuStrip MagicItemContext;

		private ToolStripMenuItem MagicItemContextRemove;

		private SplitContainer splitContainer1;

		private ListView MagicItemVersionList;

		private ColumnHeader MagicItemInfoHdr;

		private ToolStrip MagicItemVersionToolbar;

		private ToolStripButton MagicItemRemoveBtn;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripButton MagicItemEditBtn;

		private ToolStripButton MagicItemCutBtn;

		private ToolStripButton MagicItemCopyBtn;

		private ToolStripButton MagicItemPasteBtn;

		private ToolStripSeparator toolStripSeparator12;

		private ToolStripButton MagicItemStatBlockBtn;

		private ToolStripSeparator toolStripSeparator13;

		private ToolStripSeparator toolStripSeparator14;

		private TreeView LibraryTree;

		private ToolStripSeparator toolStripSeparator15;

		private ToolStripMenuItem TileMap;

		private ToolStripSeparator toolStripSeparator16;

		private ToolStripMenuItem TileContextSize;

		private Button CompendiumBtn;

		private Button HelpBtn;

		private ToolStripSeparator toolStripSeparator17;

		private ToolStripButton LibraryMergeBtn;

		private ToolStripDropDownButton FileMenu;

		private ToolStripMenuItem FileNew;

		private ToolStripMenuItem FileClose;

		private LibraryHelpPanel HelpPanel;

		private ToolStripMenuItem FileOpen;

		private ToolStripDropDownButton TemplateAddBtn;

		private ToolStripMenuItem addTemplateToolStripMenuItem;

		private ToolStripMenuItem TemplateAddTheme;

		private ToolStripDropDownButton TileAddBtn;

		private ToolStripMenuItem addTileToolStripMenuItem;

		private ToolStripMenuItem TileAddFolder;

		private ToolStripSeparator toolStripSeparator18;

		private ToolStripButton TemplateStatBlock;

		private ToolStripDropDownButton CreatureAddBtn;

		private ToolStripMenuItem CreatureAddSingle;

		private ToolStripDropDownButton CreatureTools;

		private ToolStripMenuItem CreatureToolsDemographics;

		private ToolStripMenuItem CreatureToolsPowerStatistics;

		private ToolStripMenuItem CreatureToolsFilterList;

		private ToolStripDropDownButton TrapTools;

		private ToolStripMenuItem TrapToolsDemographics;

		private ToolStripMenuItem CreatureToolsExport;

		private ToolStripSeparator toolStripSeparator19;

		private ToolStripMenuItem CreatureImport;

		private ToolStripSeparator toolStripSeparator20;

		private ToolStripMenuItem TemplateImport;

		private ToolStripSeparator toolStripSeparator21;

		private ToolStripDropDownButton TemplateTools;

		private ToolStripMenuItem TemplateToolsExport;

		private ToolStripMenuItem TrapToolsExport;

		private ToolStripDropDownButton TrapAdd;

		private ToolStripMenuItem TrapAddAdd;

		private ToolStripMenuItem TrapAddImport;

		private ToolStripDropDownButton ChallengeAdd;

		private ToolStripMenuItem ChallengeAddAdd;

		private ToolStripMenuItem ChallengeAddImport;

		private ToolStripSeparator toolStripSeparator22;

		private ToolStripDropDownButton ChallengeTools;

		private ToolStripMenuItem ChallengeToolsExport;

		private ToolStripDropDownButton MagicItemAdd;

		private ToolStripMenuItem MagicItemAddAdd;

		private ToolStripMenuItem MagicItemAddImport;

		private ToolStripDropDownButton MagicItemTools;

		private ToolStripMenuItem MagicItemToolsDemographics;

		private ToolStripMenuItem MagicItemToolsExport;

		private ToolStripSeparator toolStripSeparator24;

		private ToolStripMenuItem TileAddImport;

		private ToolStripSeparator toolStripSeparator23;

		private ToolStripDropDownButton TileTools;

		private ToolStripMenuItem TileToolsExport;

		private ToolStripSeparator toolStripSeparator25;

		private ToolStripSeparator toolStripSeparator26;

		private ToolStripSeparator toolStripSeparator27;

		private TabPage TerrainPowersPage;

		private ListView TerrainPowerList;

		private ColumnHeader TPNameHdr;

		private ColumnHeader TPInfoHdr;

		private ToolStrip TerrainPowerToolbar;

		private ToolStripDropDownButton TPAdd;

		private ToolStripMenuItem TPAddTerrainPower;

		private ToolStripSeparator toolStripSeparator28;

		private ToolStripMenuItem TPAddImport;

		private ToolStripButton TPRemoveBtn;

		private ToolStripButton TPEditBtn;

		private ToolStripSeparator toolStripSeparator29;

		private ToolStripButton TPCutBtn;

		private ToolStripButton TPCopyBtn;

		private ToolStripButton TPPasteBtn;

		private ToolStripSeparator toolStripSeparator30;

		private ToolStripDropDownButton TPTools;

		private ToolStripMenuItem TPToolsExport;

		private System.Windows.Forms.ContextMenuStrip TPContext;

		private ToolStripMenuItem TPContextRemove;

		private TabPage ArtifactPage;

		private ListView ArtifactList;

		private ColumnHeader ArtifactHdr;

		private ColumnHeader ArtifactInfoHdr;

		private ToolStrip ArtifactToolbar;

		private ToolStripDropDownButton ArtifactAdd;

		private ToolStripMenuItem ArtifactAddAdd;

		private ToolStripSeparator toolStripSeparator31;

		private ToolStripMenuItem ArtifactAddImport;

		private ToolStripButton ArtifactRemove;

		private ToolStripButton ArtifactEdit;

		private ToolStripSeparator toolStripSeparator32;

		private ToolStripButton ArtifactCut;

		private ToolStripButton ArtifactCopy;

		private ToolStripButton ArtifactPaste;

		private ToolStripSeparator toolStripSeparator33;

		private ToolStripDropDownButton ArtifactTools;

		private ToolStripMenuItem ArtifactToolsExport;

		private System.Windows.Forms.ContextMenuStrip ArtifactContext;

		private ToolStripMenuItem ArtifactContextRemove;

		private ToolStripButton ArtifactStatBlockBtn;

		private ToolStripSeparator toolStripSeparator34;

		private ToolStripButton TPStatBlockBtn;

		private ToolStripSeparator toolStripSeparator35;

		private Dictionary<Library, bool> fModified = new Dictionary<Library, bool>();

		private List<TabPage> fCleanPages = new List<TabPage>();

		private bool fShowCategorised = true;

		private bool fShowUncategorised = true;

		public List<Artifact> SelectedArtifacts
		{
			get
			{
				List<Artifact> artifacts = new List<Artifact>();
				foreach (ListViewItem selectedItem in this.ArtifactList.SelectedItems)
				{
					Artifact tag = selectedItem.Tag as Artifact;
					if (tag == null)
					{
						continue;
					}
					artifacts.Add(tag);
				}
				return artifacts;
			}
		}

		public List<SkillChallenge> SelectedChallenges
		{
			get
			{
				List<SkillChallenge> skillChallenges = new List<SkillChallenge>();
				foreach (ListViewItem selectedItem in this.ChallengeList.SelectedItems)
				{
					SkillChallenge tag = selectedItem.Tag as SkillChallenge;
					if (tag == null)
					{
						continue;
					}
					skillChallenges.Add(tag);
				}
				return skillChallenges;
			}
		}

		public List<Creature> SelectedCreatures
		{
			get
			{
				List<Creature> creatures = new List<Creature>();
				foreach (ListViewItem selectedItem in this.CreatureList.SelectedItems)
				{
					Creature tag = selectedItem.Tag as Creature;
					if (tag == null)
					{
						continue;
					}
					creatures.Add(tag);
				}
				return creatures;
			}
		}

		public Library SelectedLibrary
		{
			get
			{
				if (this.LibraryTree.SelectedNode == null)
				{
					return null;
				}
				return this.LibraryTree.SelectedNode.Tag as Library;
			}
			set
			{
				List<TreeNode> treeNodes = new List<TreeNode>();
				foreach (TreeNode node in this.LibraryTree.Nodes)
				{
					this.get_nodes(node, treeNodes);
				}
				foreach (TreeNode treeNode in treeNodes)
				{
					if (treeNode.Tag != value)
					{
						continue;
					}
					this.LibraryTree.SelectedNode = treeNode;
					break;
				}
			}
		}

		public List<MagicItem> SelectedMagicItems
		{
			get
			{
				List<MagicItem> magicItems = new List<MagicItem>();
				foreach (ListViewItem selectedItem in this.MagicItemVersionList.SelectedItems)
				{
					MagicItem tag = selectedItem.Tag as MagicItem;
					if (tag == null)
					{
						continue;
					}
					magicItems.Add(tag);
				}
				return magicItems;
			}
		}

		public string SelectedMagicItemSet
		{
			get
			{
				if (this.MagicItemList.SelectedItems.Count == 0)
				{
					return "";
				}
				return this.MagicItemList.SelectedItems[0].Text;
			}
		}

		public List<CreatureTemplate> SelectedTemplates
		{
			get
			{
				List<CreatureTemplate> creatureTemplates = new List<CreatureTemplate>();
				foreach (ListViewItem selectedItem in this.TemplateList.SelectedItems)
				{
					CreatureTemplate tag = selectedItem.Tag as CreatureTemplate;
					if (tag == null)
					{
						continue;
					}
					creatureTemplates.Add(tag);
				}
				return creatureTemplates;
			}
		}

		public List<TerrainPower> SelectedTerrainPowers
		{
			get
			{
				List<TerrainPower> terrainPowers = new List<TerrainPower>();
				foreach (ListViewItem selectedItem in this.TerrainPowerList.SelectedItems)
				{
					TerrainPower tag = selectedItem.Tag as TerrainPower;
					if (tag == null)
					{
						continue;
					}
					terrainPowers.Add(tag);
				}
				return terrainPowers;
			}
		}

		public List<MonsterTheme> SelectedThemes
		{
			get
			{
				List<MonsterTheme> monsterThemes = new List<MonsterTheme>();
				foreach (ListViewItem selectedItem in this.TemplateList.SelectedItems)
				{
					MonsterTheme tag = selectedItem.Tag as MonsterTheme;
					if (tag == null)
					{
						continue;
					}
					monsterThemes.Add(tag);
				}
				return monsterThemes;
			}
		}

		public List<Tile> SelectedTiles
		{
			get
			{
				List<Tile> tiles = new List<Tile>();
				foreach (ListViewItem selectedItem in this.TileList.SelectedItems)
				{
					Tile tag = selectedItem.Tag as Tile;
					if (tag == null)
					{
						continue;
					}
					tiles.Add(tag);
				}
				return tiles;
			}
		}

		public List<Trap> SelectedTraps
		{
			get
			{
				List<Trap> traps = new List<Trap>();
				foreach (ListViewItem selectedItem in this.TrapList.SelectedItems)
				{
					Trap tag = selectedItem.Tag as Trap;
					if (tag == null)
					{
						continue;
					}
					traps.Add(tag);
				}
				return traps;
			}
		}

		public LibraryListForm()
		{
			this.InitializeComponent();
			this.CreatureSearchToolbar.Visible = false;
			this.CompendiumBtn.Visible = Program.IsBeta;
			foreach (Library library in Session.Libraries)
			{
				this.fModified[library] = false;
			}
			Application.Idle += new EventHandler(this.Application_Idle);
			this.update_libraries();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			bool flag;
			this.HelpBtn.Text = (this.HelpPanel.Visible ? "Hide Help" : "Show Help");
			if (this.SelectedLibrary == null || Session.Project == null || this.SelectedLibrary != Session.Project.Library)
			{
				this.LibraryRemoveBtn.Enabled = this.SelectedLibrary != null;
				this.LibraryEditBtn.Enabled = this.SelectedLibrary != null;
				this.CreatureAddBtn.Enabled = this.SelectedLibrary != null;
				this.OppRemoveBtn.Enabled = this.SelectedCreatures.Count != 0;
				this.OppEditBtn.Enabled = this.SelectedCreatures.Count == 1;
				this.CreatureCopyBtn.Enabled = this.SelectedCreatures.Count != 0;
				this.CreatureCutBtn.Enabled = this.SelectedCreatures.Count != 0;
				this.CreaturePasteBtn.Enabled = (this.SelectedLibrary == null ? false : Clipboard.ContainsData(typeof(List<Creature>).ToString()));
				this.CreatureStatBlockBtn.Enabled = this.SelectedCreatures.Count == 1;
				this.CreatureToolsExport.Enabled = this.SelectedCreatures.Count == 1;
				this.TemplateAddBtn.Enabled = this.SelectedLibrary != null;
				this.TemplateRemoveBtn.Enabled = (this.SelectedTemplates.Count != 0 ? true : this.SelectedThemes.Count != 0);
				this.TemplateEditBtn.Enabled = (this.SelectedTemplates.Count != 0 ? true : this.SelectedThemes.Count != 0);
				this.TemplateCopyBtn.Enabled = (this.SelectedTemplates.Count != 0 ? true : this.SelectedThemes.Count != 0);
				this.TemplateCutBtn.Enabled = (this.SelectedTemplates.Count != 0 ? true : this.SelectedThemes.Count != 0);
				ToolStripButton templatePasteBtn = this.TemplatePasteBtn;
				if (this.SelectedLibrary == null)
				{
					flag = false;
				}
				else
				{
					flag = (Clipboard.ContainsData(typeof(List<CreatureTemplate>).ToString()) ? true : Clipboard.ContainsData(typeof(List<MonsterTheme>).ToString()));
				}
				templatePasteBtn.Enabled = flag;
				this.TemplateStatBlock.Enabled = this.SelectedTemplates.Count == 1;
				this.TemplateToolsExport.Enabled = (this.SelectedTemplates.Count == 1 ? true : this.SelectedThemes.Count == 1);
				this.TrapAdd.Enabled = this.SelectedLibrary != null;
				this.TrapRemoveBtn.Enabled = this.SelectedTraps.Count != 0;
				this.TrapEditBtn.Enabled = this.SelectedTraps.Count == 1;
				this.TrapCopyBtn.Enabled = this.SelectedTraps.Count != 0;
				this.TrapCutBtn.Enabled = this.SelectedTraps.Count != 0;
				this.TrapPasteBtn.Enabled = (this.SelectedLibrary == null ? false : Clipboard.ContainsData(typeof(List<Trap>).ToString()));
				this.TrapStatBlockBtn.Enabled = this.SelectedTraps.Count == 1;
				this.TrapToolsExport.Enabled = this.SelectedTraps.Count == 1;
				this.ChallengeAdd.Enabled = this.SelectedLibrary != null;
				this.ChallengeRemoveBtn.Enabled = this.SelectedChallenges.Count != 0;
				this.ChallengeEditBtn.Enabled = this.SelectedChallenges.Count == 1;
				this.ChallengeCopyBtn.Enabled = this.SelectedChallenges.Count != 0;
				this.ChallengeCutBtn.Enabled = this.SelectedChallenges.Count != 0;
				this.ChallengePasteBtn.Enabled = (this.SelectedLibrary == null ? false : Clipboard.ContainsData(typeof(List<SkillChallenge>).ToString()));
				this.ChallengeStatBlockBtn.Enabled = this.SelectedChallenges.Count == 1;
				this.ChallengeToolsExport.Enabled = this.SelectedChallenges.Count == 1;
				this.MagicItemAdd.Enabled = this.SelectedLibrary != null;
				this.MagicItemRemoveBtn.Enabled = this.SelectedMagicItems.Count != 0;
				this.MagicItemEditBtn.Enabled = this.SelectedMagicItems.Count == 1;
				this.MagicItemCopyBtn.Enabled = this.SelectedMagicItems.Count != 0;
				this.MagicItemCutBtn.Enabled = this.SelectedMagicItems.Count != 0;
				this.MagicItemPasteBtn.Enabled = (this.SelectedLibrary == null ? false : Clipboard.ContainsData(typeof(List<MagicItem>).ToString()));
				this.MagicItemStatBlockBtn.Enabled = this.SelectedMagicItems.Count == 1;
				this.MagicItemToolsExport.Enabled = this.SelectedMagicItemSet != "";
				this.TileAddBtn.Enabled = this.SelectedLibrary != null;
				this.TileRemoveBtn.Enabled = this.SelectedTiles.Count != 0;
				this.TileEditBtn.Enabled = this.SelectedTiles.Count == 1;
				this.TileCopyBtn.Enabled = this.SelectedTiles.Count != 0;
				this.TileCutBtn.Enabled = this.SelectedTiles.Count != 0;
				this.TilePasteBtn.Enabled = (this.SelectedLibrary == null ? false : Clipboard.ContainsData(typeof(List<Tile>).ToString()));
				this.TileToolsExport.Enabled = this.SelectedTiles.Count == 1;
				this.TPAdd.Enabled = this.SelectedLibrary != null;
				this.TPRemoveBtn.Enabled = this.SelectedTerrainPowers.Count != 0;
				this.TPEditBtn.Enabled = this.SelectedTerrainPowers.Count == 1;
				this.TPCopyBtn.Enabled = this.SelectedTerrainPowers.Count != 0;
				this.TPCutBtn.Enabled = this.SelectedTerrainPowers.Count != 0;
				this.TPPasteBtn.Enabled = (this.SelectedLibrary == null ? false : Clipboard.ContainsData(typeof(List<TerrainPower>).ToString()));
				this.TPStatBlockBtn.Enabled = this.SelectedTerrainPowers.Count == 1;
				this.TPToolsExport.Enabled = this.SelectedTerrainPowers.Count == 1;
				this.ArtifactAdd.Enabled = this.SelectedLibrary != null;
				this.ArtifactRemove.Enabled = this.SelectedArtifacts.Count != 0;
				this.ArtifactEdit.Enabled = this.SelectedArtifacts.Count == 1;
				this.ArtifactCopy.Enabled = this.SelectedArtifacts.Count != 0;
				this.ArtifactCut.Enabled = this.SelectedArtifacts.Count != 0;
				this.ArtifactPaste.Enabled = (this.SelectedLibrary == null ? false : Clipboard.ContainsData(typeof(List<Artifact>).ToString()));
				this.ArtifactStatBlockBtn.Enabled = this.SelectedArtifacts.Count == 1;
				this.ArtifactToolsExport.Enabled = this.SelectedArtifacts.Count == 1;
			}
			else
			{
				this.LibraryRemoveBtn.Enabled = false;
				this.LibraryEditBtn.Enabled = false;
				this.CreatureAddBtn.Enabled = false;
				this.OppRemoveBtn.Enabled = false;
				this.OppEditBtn.Enabled = false;
				this.CreatureCopyBtn.Enabled = this.SelectedCreatures.Count != 0;
				this.CreatureCutBtn.Enabled = false;
				this.CreaturePasteBtn.Enabled = false;
				this.CreatureStatBlockBtn.Enabled = this.SelectedCreatures.Count == 1;
				this.CreatureToolsExport.Enabled = this.SelectedCreatures.Count == 1;
				this.TemplateAddBtn.Enabled = false;
				this.TemplateRemoveBtn.Enabled = false;
				this.TemplateEditBtn.Enabled = false;
				this.TemplateCopyBtn.Enabled = (this.SelectedTemplates.Count != 0 ? true : this.SelectedThemes.Count != 0);
				this.TemplateCutBtn.Enabled = false;
				this.TemplatePasteBtn.Enabled = false;
				this.TemplateStatBlock.Enabled = (this.SelectedTemplates.Count == 1 ? true : this.SelectedThemes.Count == 1);
				this.TemplateToolsExport.Enabled = (this.SelectedTemplates.Count == 1 ? true : this.SelectedThemes.Count == 1);
				this.TrapAdd.Enabled = false;
				this.TrapRemoveBtn.Enabled = false;
				this.TrapEditBtn.Enabled = false;
				this.TrapCopyBtn.Enabled = this.SelectedTraps.Count != 0;
				this.TrapCutBtn.Enabled = false;
				this.TrapPasteBtn.Enabled = false;
				this.TrapStatBlockBtn.Enabled = this.SelectedTraps.Count == 1;
				this.TrapToolsExport.Enabled = this.SelectedTraps.Count == 1;
				this.ChallengeAdd.Enabled = false;
				this.ChallengeRemoveBtn.Enabled = false;
				this.ChallengeEditBtn.Enabled = false;
				this.ChallengeCopyBtn.Enabled = this.SelectedChallenges.Count != 0;
				this.ChallengeCutBtn.Enabled = false;
				this.ChallengePasteBtn.Enabled = false;
				this.ChallengeStatBlockBtn.Enabled = this.SelectedChallenges.Count == 1;
				this.ChallengeToolsExport.Enabled = this.SelectedChallenges.Count == 1;
				this.MagicItemAdd.Enabled = false;
				this.MagicItemRemoveBtn.Enabled = false;
				this.MagicItemEditBtn.Enabled = false;
				this.MagicItemCopyBtn.Enabled = this.SelectedMagicItems.Count != 0;
				this.MagicItemCutBtn.Enabled = false;
				this.MagicItemPasteBtn.Enabled = false;
				this.MagicItemStatBlockBtn.Enabled = this.SelectedMagicItems.Count == 1;
				this.MagicItemToolsExport.Enabled = this.SelectedMagicItemSet != "";
				this.TileAddBtn.Enabled = false;
				this.TileRemoveBtn.Enabled = false;
				this.TileEditBtn.Enabled = false;
				this.TileCopyBtn.Enabled = this.SelectedTiles.Count != 0;
				this.TileCutBtn.Enabled = false;
				this.TilePasteBtn.Enabled = false;
				this.TileToolsExport.Enabled = this.SelectedTiles.Count == 1;
				this.TPAdd.Enabled = false;
				this.TPRemoveBtn.Enabled = false;
				this.TPEditBtn.Enabled = false;
				this.TPCopyBtn.Enabled = this.SelectedTerrainPowers.Count != 0;
				this.TPCutBtn.Enabled = false;
				this.TPPasteBtn.Enabled = false;
				this.TPStatBlockBtn.Enabled = this.SelectedTerrainPowers.Count == 1;
				this.TPToolsExport.Enabled = this.SelectedTerrainPowers.Count == 1;
				this.ArtifactAdd.Enabled = false;
				this.ArtifactRemove.Enabled = false;
				this.ArtifactEdit.Enabled = false;
				this.ArtifactCopy.Enabled = this.SelectedArtifacts.Count != 0;
				this.ArtifactCut.Enabled = false;
				this.ArtifactPaste.Enabled = false;
				this.ArtifactStatBlockBtn.Enabled = this.SelectedArtifacts.Count == 1;
				this.ArtifactToolsExport.Enabled = this.SelectedArtifacts.Count == 1;
			}
			this.CreatureToolsFilterList.Checked = this.CreatureSearchToolbar.Visible;
			this.CategorisedBtn.Checked = this.fShowCategorised;
			this.UncategorisedBtn.Checked = this.fShowUncategorised;
			this.CreatureContextRemove.Enabled = this.SelectedCreatures.Count != 0;
			this.CreatureContextCategory.Enabled = this.SelectedCreatures.Count != 0;
			this.TemplateContextRemove.Enabled = this.SelectedTemplates.Count != 0;
			this.TemplateContextType.Enabled = this.SelectedTemplates.Count != 0;
			this.TrapContextRemove.Enabled = this.SelectedTraps.Count != 0;
			this.TrapContextType.Enabled = this.SelectedTraps.Count != 0;
			this.ChallengeContextRemove.Enabled = this.SelectedChallenges.Count != 0;
			this.MagicItemContextRemove.Enabled = this.SelectedMagicItems.Count != 0;
			this.TileContextRemove.Enabled = this.SelectedTiles.Count != 0;
			this.TileContextCategory.Enabled = this.SelectedTiles.Count != 0;
			this.TileContextSize.Enabled = this.SelectedTiles.Count != 0;
			this.TPContextRemove.Enabled = this.SelectedTerrainPowers.Count != 0;
			this.ArtifactContextRemove.Enabled = this.SelectedArtifacts.Count != 0;
		}

		private void ArtifactAddAdd_Click(object sender, EventArgs e)
		{
			ArtifactBuilderForm artifactBuilderForm = new ArtifactBuilderForm(new Artifact()
			{
				Name = "New Artifact"
			});
			if (artifactBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.SelectedLibrary.Artifacts.Add(artifactBuilderForm.Artifact);
				this.fModified[this.SelectedLibrary] = true;
				this.update_content(true);
			}
		}

		private void ArtifactAddImport_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary != null)
			{
				if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
				{
					return;
				}
				OpenFileDialog openFileDialog = new OpenFileDialog()
				{
					Filter = Program.ArtifactFilter,
					Multiselect = true
				};
				if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					string[] fileNames = openFileDialog.FileNames;
					for (int i = 0; i < (int)fileNames.Length; i++)
					{
						Artifact artifact = Serialisation<Artifact>.Load(fileNames[i], SerialisationMode.Binary);
						if (artifact != null)
						{
							this.SelectedLibrary.Artifacts.Add(artifact);
							this.fModified[this.SelectedLibrary] = true;
							this.update_content(true);
						}
					}
				}
			}
		}

		private void ArtifactCopy_Click(object sender, EventArgs e)
		{
			if (this.SelectedArtifacts != null)
			{
				Clipboard.SetData(typeof(List<Artifact>).ToString(), this.SelectedArtifacts);
			}
		}

		private void ArtifactCut_Click(object sender, EventArgs e)
		{
			this.ArtifactCopy_Click(sender, e);
			this.ArtifactRemove_Click(sender, e);
		}

		private void ArtifactEdit_Click(object sender, EventArgs e)
		{
			if (this.SelectedArtifacts.Count == 1)
			{
				Library artifact = Session.FindLibrary(this.SelectedArtifacts[0]);
				if (artifact == null)
				{
					return;
				}
				int num = artifact.Artifacts.IndexOf(this.SelectedArtifacts[0]);
				ArtifactBuilderForm artifactBuilderForm = new ArtifactBuilderForm(this.SelectedArtifacts[0]);
				if (artifactBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					artifact.Artifacts[num] = artifactBuilderForm.Artifact;
					this.fModified[artifact] = true;
					this.update_content(true);
				}
			}
		}

		private void ArtifactList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedArtifacts.Count != 0)
			{
				base.DoDragDrop(this.SelectedArtifacts, DragDropEffects.Move);
			}
		}

		private void ArtifactPaste_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary == null)
			{
				return;
			}
			if (Clipboard.ContainsData(typeof(List<Artifact>).ToString()))
			{
				foreach (Artifact datum in Clipboard.GetData(typeof(List<Artifact>).ToString()) as List<Artifact>)
				{
					Artifact artifact = datum.Copy();
					artifact.ID = Guid.NewGuid();
					this.SelectedLibrary.Artifacts.Add(artifact);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_content(true);
			}
		}

		private void ArtifactRemove_Click(object sender, EventArgs e)
		{
			if (this.SelectedArtifacts.Count != 0)
			{
				if (MessageBox.Show("Are you sure you want to delete your selection?", "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
				{
					return;
				}
				foreach (Artifact selectedArtifact in this.SelectedArtifacts)
				{
					Library selectedLibrary = this.SelectedLibrary ?? Session.FindLibrary(selectedArtifact);
					selectedLibrary.Artifacts.Remove(selectedArtifact);
					this.fModified[selectedLibrary] = true;
				}
				this.update_content(true);
			}
		}

		private void ArtifactStatBlockBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedArtifacts.Count != 1)
			{
				return;
			}
			ArtifactDetailsForm artifactDetailsForm = new ArtifactDetailsForm(this.SelectedArtifacts[0]);
			artifactDetailsForm.ShowDialog();
		}

		private void ArtifactToolsExport_Click(object sender, EventArgs e)
		{
			if (this.SelectedArtifacts.Count == 1)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog()
				{
					Title = "Export",
					Filter = Program.ArtifactFilter,
					FileName = this.SelectedArtifacts[0].Name
				};
				if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Serialisation<Artifact>.Save(saveFileDialog.FileName, this.SelectedArtifacts[0], SerialisationMode.Binary);
				}
			}
		}

		private void ChallengeAddBtn_Click(object sender, EventArgs e)
		{
			SkillChallengeBuilderForm skillChallengeBuilderForm = new SkillChallengeBuilderForm(new SkillChallenge()
			{
				Name = "New Skill Challenge"
			});
			if (skillChallengeBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.SelectedLibrary.SkillChallenges.Add(skillChallengeBuilderForm.SkillChallenge);
				this.fModified[this.SelectedLibrary] = true;
				this.update_content(true);
			}
		}

		private void ChallengeAddImport_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary != null)
			{
				if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
				{
					return;
				}
				OpenFileDialog openFileDialog = new OpenFileDialog()
				{
					Filter = Program.SkillChallengeFilter,
					Multiselect = true
				};
				if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					string[] fileNames = openFileDialog.FileNames;
					for (int i = 0; i < (int)fileNames.Length; i++)
					{
						SkillChallenge skillChallenge = Serialisation<SkillChallenge>.Load(fileNames[i], SerialisationMode.Binary);
						if (skillChallenge != null)
						{
							this.SelectedLibrary.SkillChallenges.Add(skillChallenge);
							this.fModified[this.SelectedLibrary] = true;
							this.update_content(true);
						}
					}
				}
			}
		}

		private void ChallengeContextRemove_Click(object sender, EventArgs e)
		{
			this.ChallengeRemoveBtn_Click(sender, e);
		}

		private void ChallengeCopyBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedChallenges != null)
			{
				Clipboard.SetData(typeof(List<SkillChallenge>).ToString(), this.SelectedChallenges);
			}
		}

		private void ChallengeCutBtn_Click(object sender, EventArgs e)
		{
			this.ChallengeCopyBtn_Click(sender, e);
			this.ChallengeRemoveBtn_Click(sender, e);
		}

		private void ChallengeEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedChallenges.Count == 1)
			{
				Library skillChallenge = Session.FindLibrary(this.SelectedChallenges[0]);
				if (skillChallenge == null)
				{
					return;
				}
				int num = skillChallenge.SkillChallenges.IndexOf(this.SelectedChallenges[0]);
				SkillChallengeBuilderForm skillChallengeBuilderForm = new SkillChallengeBuilderForm(this.SelectedChallenges[0]);
				if (skillChallengeBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					skillChallenge.SkillChallenges[num] = skillChallengeBuilderForm.SkillChallenge;
					this.fModified[skillChallenge] = true;
					this.update_content(true);
				}
			}
		}

		private void ChallengeList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedChallenges.Count != 0)
			{
				base.DoDragDrop(this.SelectedChallenges, DragDropEffects.Move);
			}
		}

		private void ChallengePasteBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary == null)
			{
				return;
			}
			if (Clipboard.ContainsData(typeof(List<SkillChallenge>).ToString()))
			{
				foreach (SkillChallenge datum in Clipboard.GetData(typeof(List<SkillChallenge>).ToString()) as List<SkillChallenge>)
				{
					SkillChallenge skillChallenge = datum.Copy() as SkillChallenge;
					skillChallenge.ID = Guid.NewGuid();
					this.SelectedLibrary.SkillChallenges.Add(skillChallenge);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_content(true);
			}
		}

		private void ChallengeRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedChallenges.Count != 0)
			{
				if (MessageBox.Show("Are you sure you want to delete your selection?", "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
				{
					return;
				}
				foreach (SkillChallenge selectedChallenge in this.SelectedChallenges)
				{
					Library selectedLibrary = this.SelectedLibrary ?? Session.FindLibrary(selectedChallenge);
					selectedLibrary.SkillChallenges.Remove(selectedChallenge);
					this.fModified[selectedLibrary] = true;
				}
				this.update_content(true);
			}
		}

		private void ChallengeStatBlockBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedChallenges.Count != 1)
			{
				return;
			}
			SkillChallengeDetailsForm skillChallengeDetailsForm = new SkillChallengeDetailsForm(this.SelectedChallenges[0]);
			skillChallengeDetailsForm.ShowDialog();
		}

		private void ChallengeToolsExport_Click(object sender, EventArgs e)
		{
			if (this.SelectedChallenges.Count == 1)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog()
				{
					Title = "Export",
					Filter = Program.SkillChallengeFilter,
					FileName = this.SelectedChallenges[0].Name
				};
				if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Serialisation<SkillChallenge>.Save(saveFileDialog.FileName, this.SelectedChallenges[0], SerialisationMode.Binary);
				}
			}
		}

		private void CompendiumBtn_Click(object sender, EventArgs e)
		{
			if ((new CompendiumForm()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.update_content(true);
			}
		}

		private void CreatureAddBtn_Click(object sender, EventArgs e)
		{
			CreatureBuilderForm creatureBuilderForm = new CreatureBuilderForm(new Creature()
			{
				Name = "New Creature"
			});
			if (creatureBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.SelectedLibrary.Creatures.Add(creatureBuilderForm.Creature as Creature);
				this.fModified[this.SelectedLibrary] = true;
				this.update_content(true);
			}
		}

		private void CreatureContextCategory_Click(object sender, EventArgs e)
		{
			List<string> strs = new List<string>();
			foreach (Creature creature in Session.Creatures)
			{
				if (creature.Category == null || creature.Category == "" || strs.Contains(creature.Category))
				{
					continue;
				}
				strs.Add(creature.Category);
			}
			List<string> strs1 = new List<string>();
			foreach (Creature selectedCreature in this.SelectedCreatures)
			{
				if (selectedCreature.Category == null || selectedCreature.Category == "" || strs1.Contains(selectedCreature.Category))
				{
					continue;
				}
				strs1.Add(selectedCreature.Category);
			}
			string item = "";
			if (strs1.Count == 1)
			{
				item = strs1[0];
			}
			if (strs1.Count == 0)
			{
				if (this.SelectedCreatures.Count != 1)
				{
					Dictionary<string, int> strs2 = new Dictionary<string, int>();
					for (int i = 0; i != this.SelectedCreatures.Count; i++)
					{
						string name = this.SelectedCreatures[i].Name;
						for (int j = i + 1; j != this.SelectedCreatures.Count; j++)
						{
							string str = this.SelectedCreatures[j].Name;
							string str1 = StringHelper.LongestCommonToken(name, str);
							if (str1.Length >= 3)
							{
								if (!strs2.ContainsKey(str1))
								{
									strs2[str1] = 0;
								}
								Dictionary<string, int> item1 = strs2;
								Dictionary<string, int> strs3 = item1;
								string str2 = str1;
								item1[str2] = strs3[str2] + 1;
							}
						}
					}
					if (strs2.Keys.Count != 0)
					{
						foreach (string key in strs2.Keys)
						{
							if (strs2[key] <= (strs2.ContainsKey(item) ? strs2[item] : 0))
							{
								continue;
							}
							item = key;
						}
						if (!strs.Contains(item))
						{
							strs.Add(item);
						}
					}
				}
				else
				{
					item = this.SelectedCreatures[0].Name;
				}
			}
			CategoryForm categoryForm = new CategoryForm(strs, item);
			if (categoryForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				foreach (Creature category in this.SelectedCreatures)
				{
					category.Category = categoryForm.Category;
					Library library = Session.FindLibrary(category);
					if (library == null)
					{
						continue;
					}
					this.fModified[library] = true;
				}
				this.update_creatures();
			}
		}

		private void CreatureContextRemove_Click(object sender, EventArgs e)
		{
			this.OppRemoveBtn_Click(sender, e);
		}

		private void CreatureCopyBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCreatures.Count != 0)
			{
				Clipboard.SetData(typeof(List<Creature>).ToString(), this.SelectedCreatures);
			}
		}

		private void CreatureCutBtn_Click(object sender, EventArgs e)
		{
			this.CreatureCopyBtn_Click(sender, e);
			this.OppRemoveBtn_Click(sender, e);
		}

		private void CreatureDemoBtn_Click(object sender, EventArgs e)
		{
			try
			{
				(new DemographicsForm(this.SelectedLibrary, DemographicsSource.Creatures)).ShowDialog();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void CreatureFilterCategorised_Click(object sender, EventArgs e)
		{
			this.fShowCategorised = !this.fShowCategorised;
			this.update_content(true);
		}

		private void CreatureFilterUncategorised_Click(object sender, EventArgs e)
		{
			this.fShowUncategorised = !this.fShowUncategorised;
			this.update_content(true);
		}

		private void CreatureImport_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary != null)
			{
				if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
				{
					return;
				}
				OpenFileDialog openFileDialog = new OpenFileDialog()
				{
					Filter = Program.CreatureAndMonsterFilter,
					Multiselect = true
				};
				if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					string[] fileNames = openFileDialog.FileNames;
					for (int i = 0; i < (int)fileNames.Length; i++)
					{
						string str = fileNames[i];
						Creature d = null;
						if (str.EndsWith("creature"))
						{
							d = Serialisation<Creature>.Load(str, SerialisationMode.Binary);
						}
						if (str.EndsWith("monster"))
						{
							d = AppImport.ImportCreature(File.ReadAllText(str));
						}
						if (d != null)
						{
							Creature creature = this.SelectedLibrary.FindCreature(d.Name, d.Level);
							if (creature != null)
							{
								d.ID = creature.ID;
								d.Category = creature.Category;
								this.SelectedLibrary.Creatures.Remove(creature);
							}
							this.SelectedLibrary.Creatures.Add(d);
							this.fModified[this.SelectedLibrary] = true;
							this.update_content(true);
						}
					}
				}
			}
		}

		private void CreaturePasteBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary == null)
			{
				return;
			}
			if (Clipboard.ContainsData(typeof(List<Creature>).ToString()))
			{
				foreach (Creature datum in Clipboard.GetData(typeof(List<Creature>).ToString()) as List<Creature>)
				{
					Creature creature = datum.Copy();
					creature.ID = Guid.NewGuid();
					this.SelectedLibrary.Creatures.Add(creature);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_content(true);
			}
		}

		private void CreatureStatBlockBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCreatures.Count != 1)
			{
				return;
			}
			EncounterCard encounterCard = new EncounterCard()
			{
				CreatureID = this.SelectedCreatures[0].ID
			};
			(new CreatureDetailsForm(encounterCard)).ShowDialog();
		}

		private void CreatureToolsExport_Click(object sender, EventArgs e)
		{
			if (this.SelectedCreatures.Count == 1)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog()
				{
					Title = "Export",
					Filter = Program.CreatureFilter,
					FileName = this.SelectedCreatures[0].Name
				};
				if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Serialisation<Creature>.Save(saveFileDialog.FileName, this.SelectedCreatures[0], SerialisationMode.Binary);
				}
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

		private void FileClose_Click(object sender, EventArgs e)
		{
			if (Session.Project != null)
			{
				Session.Project.PopulateProjectLibrary();
			}
			foreach (Library library in Session.Libraries)
			{
				library.Creatures.Clear();
				library.Templates.Clear();
				library.Themes.Clear();
				library.Traps.Clear();
				library.SkillChallenges.Clear();
				library.MagicItems.Clear();
				library.Tiles.Clear();
				library.TerrainPowers.Clear();
				library.Artifacts.Clear();
			}
			Session.Libraries.Clear();
			if (Session.Project != null)
			{
				Session.Project.SimplifyProjectLibrary();
			}
			this.update_libraries();
			this.update_content(true);
			GC.Collect();
		}

		private void FileNew_Click(object sender, EventArgs e)
		{
			LibraryForm libraryForm = new LibraryForm(new Library()
			{
				Name = "New Library"
			});
			if (libraryForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Libraries.Add(libraryForm.Library);
				Session.Libraries.Sort();
				this.save(libraryForm.Library);
				this.fModified[libraryForm.Library] = true;
				this.update_libraries();
				this.SelectedLibrary = libraryForm.Library;
				this.update_content(true);
			}
		}

		private void FileOpen_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Filter = Program.LibraryFilter,
				Multiselect = true
			};
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				string libraryFolder = Session.LibraryFolder;
				List<string> strs = new List<string>();
				string[] fileNames = openFileDialog.FileNames;
				for (int i = 0; i < (int)fileNames.Length; i++)
				{
					string str = fileNames[i];
					if (!FileName.Directory(str).Contains(libraryFolder))
					{
						strs.Add(str);
					}
				}
				if (strs.Count != 0 && MessageBox.Show(string.Concat(string.Concat("Do you want to move these libraries into the Libraries folder?", Environment.NewLine), "They will then be loaded automatically the next time Masterplan starts up."), "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
				{
					foreach (string str1 in strs)
					{
						string str2 = string.Concat(libraryFolder, FileName.Name(str1), ".library");
						File.Copy(str1, str2);
					}
				}
				string[] strArrays = openFileDialog.FileNames;
				for (int j = 0; j < (int)strArrays.Length; j++)
				{
					string str3 = strArrays[j];
					if (!strs.Contains(str3))
					{
						Library library = Session.LoadLibrary(str3);
						this.fModified[library] = false;
					}
				}
				foreach (string str4 in strs)
				{
					Library library1 = Session.LoadLibrary(str4);
					this.fModified[library1] = false;
				}
				if (Session.Project != null)
				{
					Session.Project.SimplifyProjectLibrary();
				}
				Session.Libraries.Sort();
				this.update_libraries();
				this.update_content(true);
			}
		}

		private void FilterBtn_Click(object sender, EventArgs e)
		{
			this.CreatureSearchToolbar.Visible = !this.CreatureSearchToolbar.Visible;
			this.update_content(true);
		}

		private void get_nodes(TreeNode tn, List<TreeNode> nodes)
		{
			nodes.Add(tn);
			foreach (TreeNode node in tn.Nodes)
			{
				this.get_nodes(node, nodes);
			}
		}

		private void HelpBtn_Click(object sender, EventArgs e)
		{
			this.show_help(!this.HelpPanel.Visible);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(LibraryListForm));
			ListViewGroup listViewGroup = new ListViewGroup("Functional Templates", HorizontalAlignment.Left);
			ListViewGroup listViewGroup1 = new ListViewGroup("Class Templates", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Themes", HorizontalAlignment.Left);
			ListViewGroup listViewGroup3 = new ListViewGroup("Traps", HorizontalAlignment.Left);
			ListViewGroup listViewGroup4 = new ListViewGroup("Hazards", HorizontalAlignment.Left);
			ListViewGroup listViewGroup5 = new ListViewGroup("Traps", HorizontalAlignment.Left);
			ListViewGroup listViewGroup6 = new ListViewGroup("Hazards", HorizontalAlignment.Left);
			ListViewGroup listViewGroup7 = new ListViewGroup("Heroic Tier", HorizontalAlignment.Left);
			ListViewGroup listViewGroup8 = new ListViewGroup("Paragon Tier", HorizontalAlignment.Left);
			ListViewGroup listViewGroup9 = new ListViewGroup("Epic Tier", HorizontalAlignment.Left);
			ListViewGroup listViewGroup10 = new ListViewGroup("Traps", HorizontalAlignment.Left);
			ListViewGroup listViewGroup11 = new ListViewGroup("Hazards", HorizontalAlignment.Left);
			ListViewGroup listViewGroup12 = new ListViewGroup("Traps", HorizontalAlignment.Left);
			ListViewGroup listViewGroup13 = new ListViewGroup("Hazards", HorizontalAlignment.Left);
			this.Splitter = new SplitContainer();
			this.LibraryTree = new TreeView();
			this.LibraryToolbar = new ToolStrip();
			this.FileMenu = new ToolStripDropDownButton();
			this.FileNew = new ToolStripMenuItem();
			this.FileOpen = new ToolStripMenuItem();
			this.FileClose = new ToolStripMenuItem();
			this.LibraryRemoveBtn = new ToolStripButton();
			this.LibraryEditBtn = new ToolStripButton();
			this.toolStripSeparator17 = new ToolStripSeparator();
			this.LibraryMergeBtn = new ToolStripButton();
			this.CompendiumBtn = new Button();
			this.HelpBtn = new Button();
			this.Pages = new TabControl();
			this.CreaturesPage = new TabPage();
			this.CreatureList = new ListView();
			this.CreatureNameHdr = new ColumnHeader();
			this.CreatureInfoHdr = new ColumnHeader();
			this.CreatureContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.CreatureContextRemove = new ToolStripMenuItem();
			this.CreatureContextCategory = new ToolStripMenuItem();
			this.CreatureSearchToolbar = new ToolStrip();
			this.SearchLbl = new ToolStripLabel();
			this.SearchBox = new ToolStripTextBox();
			this.toolStripSeparator11 = new ToolStripSeparator();
			this.CategorisedBtn = new ToolStripButton();
			this.UncategorisedBtn = new ToolStripButton();
			this.CreatureToolbar = new ToolStrip();
			this.CreatureAddBtn = new ToolStripDropDownButton();
			this.CreatureAddSingle = new ToolStripMenuItem();
			this.toolStripSeparator19 = new ToolStripSeparator();
			this.CreatureImport = new ToolStripMenuItem();
			this.OppRemoveBtn = new ToolStripButton();
			this.OppEditBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.CreatureCutBtn = new ToolStripButton();
			this.CreatureCopyBtn = new ToolStripButton();
			this.CreaturePasteBtn = new ToolStripButton();
			this.toolStripSeparator4 = new ToolStripSeparator();
			this.CreatureStatBlockBtn = new ToolStripButton();
			this.toolStripSeparator10 = new ToolStripSeparator();
			this.CreatureTools = new ToolStripDropDownButton();
			this.CreatureToolsDemographics = new ToolStripMenuItem();
			this.CreatureToolsPowerStatistics = new ToolStripMenuItem();
			this.CreatureToolsFilterList = new ToolStripMenuItem();
			this.CreatureToolsExport = new ToolStripMenuItem();
			this.TemplatesPage = new TabPage();
			this.TemplateList = new ListView();
			this.TemplateNameHdr = new ColumnHeader();
			this.TemplateInfoHdr = new ColumnHeader();
			this.TemplateContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.TemplateContextRemove = new ToolStripMenuItem();
			this.TemplateContextType = new ToolStripMenuItem();
			this.TemplateFunctional = new ToolStripMenuItem();
			this.TemplateClass = new ToolStripMenuItem();
			this.TemplateToolbar = new ToolStrip();
			this.TemplateAddBtn = new ToolStripDropDownButton();
			this.addTemplateToolStripMenuItem = new ToolStripMenuItem();
			this.TemplateAddTheme = new ToolStripMenuItem();
			this.toolStripSeparator20 = new ToolStripSeparator();
			this.TemplateImport = new ToolStripMenuItem();
			this.TemplateRemoveBtn = new ToolStripButton();
			this.TemplateEditBtn = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.TemplateCutBtn = new ToolStripButton();
			this.TemplateCopyBtn = new ToolStripButton();
			this.TemplatePasteBtn = new ToolStripButton();
			this.toolStripSeparator18 = new ToolStripSeparator();
			this.TemplateStatBlock = new ToolStripButton();
			this.toolStripSeparator21 = new ToolStripSeparator();
			this.TemplateTools = new ToolStripDropDownButton();
			this.TemplateToolsExport = new ToolStripMenuItem();
			this.TrapsPage = new TabPage();
			this.TrapList = new ListView();
			this.TrapNameHdr = new ColumnHeader();
			this.TrapInfoHdr = new ColumnHeader();
			this.TrapContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.TrapContextRemove = new ToolStripMenuItem();
			this.TrapContextType = new ToolStripMenuItem();
			this.TrapTrap = new ToolStripMenuItem();
			this.TrapHazard = new ToolStripMenuItem();
			this.TrapToolbar = new ToolStrip();
			this.TrapAdd = new ToolStripDropDownButton();
			this.TrapAddAdd = new ToolStripMenuItem();
			this.toolStripSeparator25 = new ToolStripSeparator();
			this.TrapAddImport = new ToolStripMenuItem();
			this.TrapRemoveBtn = new ToolStripButton();
			this.TrapEditBtn = new ToolStripButton();
			this.toolStripSeparator6 = new ToolStripSeparator();
			this.TrapCutBtn = new ToolStripButton();
			this.TrapCopyBtn = new ToolStripButton();
			this.TrapPasteBtn = new ToolStripButton();
			this.toolStripSeparator8 = new ToolStripSeparator();
			this.TrapStatBlockBtn = new ToolStripButton();
			this.toolStripSeparator13 = new ToolStripSeparator();
			this.TrapTools = new ToolStripDropDownButton();
			this.TrapToolsDemographics = new ToolStripMenuItem();
			this.TrapToolsExport = new ToolStripMenuItem();
			this.ChallengePage = new TabPage();
			this.ChallengeList = new ListView();
			this.ChallengeNameHdr = new ColumnHeader();
			this.ChallengeInfoHdr = new ColumnHeader();
			this.ChallengeToolbar = new ToolStrip();
			this.ChallengeAdd = new ToolStripDropDownButton();
			this.ChallengeAddAdd = new ToolStripMenuItem();
			this.toolStripSeparator26 = new ToolStripSeparator();
			this.ChallengeAddImport = new ToolStripMenuItem();
			this.ChallengeRemoveBtn = new ToolStripButton();
			this.ChallengeEditBtn = new ToolStripButton();
			this.toolStripSeparator7 = new ToolStripSeparator();
			this.ChallengeCutBtn = new ToolStripButton();
			this.ChallengeCopyBtn = new ToolStripButton();
			this.ChallengePasteBtn = new ToolStripButton();
			this.toolStripSeparator9 = new ToolStripSeparator();
			this.ChallengeStatBlockBtn = new ToolStripButton();
			this.toolStripSeparator22 = new ToolStripSeparator();
			this.ChallengeTools = new ToolStripDropDownButton();
			this.ChallengeToolsExport = new ToolStripMenuItem();
			this.MagicItemsPage = new TabPage();
			this.splitContainer1 = new SplitContainer();
			this.MagicItemList = new ListView();
			this.MagicItemHdr = new ColumnHeader();
			this.MagicItemContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.MagicItemContextRemove = new ToolStripMenuItem();
			this.MagicItemToolbar = new ToolStrip();
			this.MagicItemAdd = new ToolStripDropDownButton();
			this.MagicItemAddAdd = new ToolStripMenuItem();
			this.toolStripSeparator27 = new ToolStripSeparator();
			this.MagicItemAddImport = new ToolStripMenuItem();
			this.toolStripSeparator14 = new ToolStripSeparator();
			this.MagicItemTools = new ToolStripDropDownButton();
			this.MagicItemToolsDemographics = new ToolStripMenuItem();
			this.MagicItemToolsExport = new ToolStripMenuItem();
			this.MagicItemVersionList = new ListView();
			this.MagicItemInfoHdr = new ColumnHeader();
			this.MagicItemVersionToolbar = new ToolStrip();
			this.MagicItemRemoveBtn = new ToolStripButton();
			this.toolStripSeparator5 = new ToolStripSeparator();
			this.MagicItemEditBtn = new ToolStripButton();
			this.MagicItemCutBtn = new ToolStripButton();
			this.MagicItemCopyBtn = new ToolStripButton();
			this.MagicItemPasteBtn = new ToolStripButton();
			this.toolStripSeparator12 = new ToolStripSeparator();
			this.MagicItemStatBlockBtn = new ToolStripButton();
			this.TilesPage = new TabPage();
			this.TileList = new ListView();
			this.TileSetNameHdr = new ColumnHeader();
			this.TileContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.TileContextRemove = new ToolStripMenuItem();
			this.TileContextCategory = new ToolStripMenuItem();
			this.TilePlain = new ToolStripMenuItem();
			this.TileDoorway = new ToolStripMenuItem();
			this.TileStairway = new ToolStripMenuItem();
			this.TileFeature = new ToolStripMenuItem();
			this.toolStripSeparator15 = new ToolStripSeparator();
			this.TileSpecial = new ToolStripMenuItem();
			this.toolStripSeparator16 = new ToolStripSeparator();
			this.TileMap = new ToolStripMenuItem();
			this.TileContextSize = new ToolStripMenuItem();
			this.TileToolbar = new ToolStrip();
			this.TileAddBtn = new ToolStripDropDownButton();
			this.addTileToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator24 = new ToolStripSeparator();
			this.TileAddImport = new ToolStripMenuItem();
			this.TileAddFolder = new ToolStripMenuItem();
			this.TileRemoveBtn = new ToolStripButton();
			this.TileEditBtn = new ToolStripButton();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.TileCutBtn = new ToolStripButton();
			this.TileCopyBtn = new ToolStripButton();
			this.TilePasteBtn = new ToolStripButton();
			this.toolStripSeparator23 = new ToolStripSeparator();
			this.TileTools = new ToolStripDropDownButton();
			this.TileToolsExport = new ToolStripMenuItem();
			this.TerrainPowersPage = new TabPage();
			this.TerrainPowerList = new ListView();
			this.TPNameHdr = new ColumnHeader();
			this.TPInfoHdr = new ColumnHeader();
			this.TPContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.TPContextRemove = new ToolStripMenuItem();
			this.TerrainPowerToolbar = new ToolStrip();
			this.TPAdd = new ToolStripDropDownButton();
			this.TPAddTerrainPower = new ToolStripMenuItem();
			this.toolStripSeparator28 = new ToolStripSeparator();
			this.TPAddImport = new ToolStripMenuItem();
			this.TPRemoveBtn = new ToolStripButton();
			this.TPEditBtn = new ToolStripButton();
			this.toolStripSeparator29 = new ToolStripSeparator();
			this.TPCutBtn = new ToolStripButton();
			this.TPCopyBtn = new ToolStripButton();
			this.TPPasteBtn = new ToolStripButton();
			this.toolStripSeparator30 = new ToolStripSeparator();
			this.TPStatBlockBtn = new ToolStripButton();
			this.toolStripSeparator35 = new ToolStripSeparator();
			this.TPTools = new ToolStripDropDownButton();
			this.TPToolsExport = new ToolStripMenuItem();
			this.ArtifactPage = new TabPage();
			this.ArtifactList = new ListView();
			this.ArtifactHdr = new ColumnHeader();
			this.ArtifactInfoHdr = new ColumnHeader();
			this.ArtifactContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ArtifactContextRemove = new ToolStripMenuItem();
			this.ArtifactToolbar = new ToolStrip();
			this.ArtifactAdd = new ToolStripDropDownButton();
			this.ArtifactAddAdd = new ToolStripMenuItem();
			this.toolStripSeparator31 = new ToolStripSeparator();
			this.ArtifactAddImport = new ToolStripMenuItem();
			this.ArtifactRemove = new ToolStripButton();
			this.ArtifactEdit = new ToolStripButton();
			this.toolStripSeparator32 = new ToolStripSeparator();
			this.ArtifactCut = new ToolStripButton();
			this.ArtifactCopy = new ToolStripButton();
			this.ArtifactPaste = new ToolStripButton();
			this.toolStripSeparator33 = new ToolStripSeparator();
			this.ArtifactStatBlockBtn = new ToolStripButton();
			this.toolStripSeparator34 = new ToolStripSeparator();
			this.ArtifactTools = new ToolStripDropDownButton();
			this.ArtifactToolsExport = new ToolStripMenuItem();
			this.HelpPanel = new LibraryHelpPanel();
			this.ChallengeContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ChallengeContextRemove = new ToolStripMenuItem();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.LibraryToolbar.SuspendLayout();
			this.Pages.SuspendLayout();
			this.CreaturesPage.SuspendLayout();
			this.CreatureContext.SuspendLayout();
			this.CreatureSearchToolbar.SuspendLayout();
			this.CreatureToolbar.SuspendLayout();
			this.TemplatesPage.SuspendLayout();
			this.TemplateContext.SuspendLayout();
			this.TemplateToolbar.SuspendLayout();
			this.TrapsPage.SuspendLayout();
			this.TrapContext.SuspendLayout();
			this.TrapToolbar.SuspendLayout();
			this.ChallengePage.SuspendLayout();
			this.ChallengeToolbar.SuspendLayout();
			this.MagicItemsPage.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.MagicItemContext.SuspendLayout();
			this.MagicItemToolbar.SuspendLayout();
			this.MagicItemVersionToolbar.SuspendLayout();
			this.TilesPage.SuspendLayout();
			this.TileContext.SuspendLayout();
			this.TileToolbar.SuspendLayout();
			this.TerrainPowersPage.SuspendLayout();
			this.TPContext.SuspendLayout();
			this.TerrainPowerToolbar.SuspendLayout();
			this.ArtifactPage.SuspendLayout();
			this.ArtifactContext.SuspendLayout();
			this.ArtifactToolbar.SuspendLayout();
			this.ChallengeContext.SuspendLayout();
			base.SuspendLayout();
			this.Splitter.Dock = DockStyle.Fill;
			this.Splitter.FixedPanel = FixedPanel.Panel1;
			this.Splitter.Location = new Point(0, 0);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.LibraryTree);
			this.Splitter.Panel1.Controls.Add(this.LibraryToolbar);
			this.Splitter.Panel1.Controls.Add(this.CompendiumBtn);
			this.Splitter.Panel1.Controls.Add(this.HelpBtn);
			this.Splitter.Panel2.Controls.Add(this.Pages);
			this.Splitter.Panel2.Controls.Add(this.HelpPanel);
			this.Splitter.Size = new System.Drawing.Size(879, 431);
			this.Splitter.SplitterDistance = 249;
			this.Splitter.TabIndex = 0;
			this.LibraryTree.AllowDrop = true;
			this.LibraryTree.Dock = DockStyle.Fill;
			this.LibraryTree.FullRowSelect = true;
			this.LibraryTree.HideSelection = false;
			this.LibraryTree.Location = new Point(0, 25);
			this.LibraryTree.Name = "LibraryTree";
			this.LibraryTree.ShowPlusMinus = false;
			this.LibraryTree.ShowRootLines = false;
			this.LibraryTree.Size = new System.Drawing.Size(249, 360);
			this.LibraryTree.TabIndex = 1;
			this.LibraryTree.DoubleClick += new EventHandler(this.LibraryEditBtn_Click);
			this.LibraryTree.DragDrop += new DragEventHandler(this.LibraryList_DragDrop);
			this.LibraryTree.AfterSelect += new TreeViewEventHandler(this.LibraryTree_AfterSelect);
			this.LibraryTree.ItemDrag += new ItemDragEventHandler(this.LibraryList_ItemDrag);
			this.LibraryTree.DragOver += new DragEventHandler(this.LibraryList_DragOver);
			ToolStripItem[] fileMenu = new ToolStripItem[] { this.FileMenu, this.LibraryRemoveBtn, this.LibraryEditBtn, this.toolStripSeparator17, this.LibraryMergeBtn };
			this.LibraryToolbar.Items.AddRange(fileMenu);
			this.LibraryToolbar.Location = new Point(0, 0);
			this.LibraryToolbar.Name = "LibraryToolbar";
			this.LibraryToolbar.Size = new System.Drawing.Size(249, 25);
			this.LibraryToolbar.TabIndex = 0;
			this.LibraryToolbar.Text = "toolStrip1";
			this.FileMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItem[] fileNew = new ToolStripItem[] { this.FileNew, this.FileOpen, this.FileClose };
			this.FileMenu.DropDownItems.AddRange(fileNew);
			this.FileMenu.Image = (Image)resources.GetObject("FileMenu.Image");
			this.FileMenu.ImageTransparentColor = Color.Magenta;
			this.FileMenu.Name = "FileMenu";
			this.FileMenu.Size = new System.Drawing.Size(38, 22);
			this.FileMenu.Text = "File";
			this.FileNew.Name = "FileNew";
			this.FileNew.Size = new System.Drawing.Size(183, 22);
			this.FileNew.Text = "Create New Library...";
			this.FileNew.Click += new EventHandler(this.FileNew_Click);
			this.FileOpen.Name = "FileOpen";
			this.FileOpen.Size = new System.Drawing.Size(183, 22);
			this.FileOpen.Text = "Open Library...";
			this.FileOpen.Click += new EventHandler(this.FileOpen_Click);
			this.FileClose.Name = "FileClose";
			this.FileClose.Size = new System.Drawing.Size(183, 22);
			this.FileClose.Text = "Close All Libraries";
			this.FileClose.Click += new EventHandler(this.FileClose_Click);
			this.LibraryRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LibraryRemoveBtn.Image = (Image)resources.GetObject("LibraryRemoveBtn.Image");
			this.LibraryRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.LibraryRemoveBtn.Name = "LibraryRemoveBtn";
			this.LibraryRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.LibraryRemoveBtn.Text = "Remove";
			this.LibraryRemoveBtn.Click += new EventHandler(this.LibraryRemoveBtn_Click);
			this.LibraryEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LibraryEditBtn.Image = (Image)resources.GetObject("LibraryEditBtn.Image");
			this.LibraryEditBtn.ImageTransparentColor = Color.Magenta;
			this.LibraryEditBtn.Name = "LibraryEditBtn";
			this.LibraryEditBtn.Size = new System.Drawing.Size(31, 22);
			this.LibraryEditBtn.Text = "Edit";
			this.LibraryEditBtn.Click += new EventHandler(this.LibraryEditBtn_Click);
			this.toolStripSeparator17.Name = "toolStripSeparator17";
			this.toolStripSeparator17.Size = new System.Drawing.Size(6, 25);
			this.LibraryMergeBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LibraryMergeBtn.Image = (Image)resources.GetObject("LibraryMergeBtn.Image");
			this.LibraryMergeBtn.ImageTransparentColor = Color.Magenta;
			this.LibraryMergeBtn.Name = "LibraryMergeBtn";
			this.LibraryMergeBtn.Size = new System.Drawing.Size(45, 22);
			this.LibraryMergeBtn.Text = "Merge";
			this.LibraryMergeBtn.Click += new EventHandler(this.LibraryMergeBtn_Click);
			this.CompendiumBtn.Dock = DockStyle.Bottom;
			this.CompendiumBtn.Location = new Point(0, 385);
			this.CompendiumBtn.Name = "CompendiumBtn";
			this.CompendiumBtn.Size = new System.Drawing.Size(249, 23);
			this.CompendiumBtn.TabIndex = 2;
			this.CompendiumBtn.Text = "Download Compendium Items";
			this.CompendiumBtn.UseVisualStyleBackColor = true;
			this.CompendiumBtn.Click += new EventHandler(this.CompendiumBtn_Click);
			this.HelpBtn.Dock = DockStyle.Bottom;
			this.HelpBtn.Location = new Point(0, 408);
			this.HelpBtn.Name = "HelpBtn";
			this.HelpBtn.Size = new System.Drawing.Size(249, 23);
			this.HelpBtn.TabIndex = 3;
			this.HelpBtn.Text = "Show Help";
			this.HelpBtn.UseVisualStyleBackColor = true;
			this.HelpBtn.Click += new EventHandler(this.HelpBtn_Click);
			this.Pages.Controls.Add(this.CreaturesPage);
			this.Pages.Controls.Add(this.TemplatesPage);
			this.Pages.Controls.Add(this.TrapsPage);
			this.Pages.Controls.Add(this.ChallengePage);
			this.Pages.Controls.Add(this.MagicItemsPage);
			this.Pages.Controls.Add(this.TilesPage);
			this.Pages.Controls.Add(this.TerrainPowersPage);
			this.Pages.Controls.Add(this.ArtifactPage);
			this.Pages.Dock = DockStyle.Fill;
			this.Pages.Location = new Point(0, 0);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new System.Drawing.Size(626, 272);
			this.Pages.TabIndex = 2;
			this.Pages.SelectedIndexChanged += new EventHandler(this.Pages_SelectedIndexChanged);
			this.CreaturesPage.Controls.Add(this.CreatureList);
			this.CreaturesPage.Controls.Add(this.CreatureSearchToolbar);
			this.CreaturesPage.Controls.Add(this.CreatureToolbar);
			this.CreaturesPage.Location = new Point(4, 22);
			this.CreaturesPage.Name = "CreaturesPage";
			this.CreaturesPage.Padding = new System.Windows.Forms.Padding(3);
			this.CreaturesPage.Size = new System.Drawing.Size(618, 246);
			this.CreaturesPage.TabIndex = 0;
			this.CreaturesPage.Text = "Creatures";
			this.CreaturesPage.UseVisualStyleBackColor = true;
			ColumnHeader[] creatureNameHdr = new ColumnHeader[] { this.CreatureNameHdr, this.CreatureInfoHdr };
			this.CreatureList.Columns.AddRange(creatureNameHdr);
			this.CreatureList.ContextMenuStrip = this.CreatureContext;
			this.CreatureList.Dock = DockStyle.Fill;
			this.CreatureList.FullRowSelect = true;
			this.CreatureList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.CreatureList.HideSelection = false;
			this.CreatureList.Location = new Point(3, 53);
			this.CreatureList.Name = "CreatureList";
			this.CreatureList.Size = new System.Drawing.Size(612, 190);
			this.CreatureList.Sorting = SortOrder.Ascending;
			this.CreatureList.TabIndex = 1;
			this.CreatureList.UseCompatibleStateImageBehavior = false;
			this.CreatureList.View = View.Details;
			this.CreatureList.DoubleClick += new EventHandler(this.OppEditBtn_Click);
			this.CreatureList.ItemDrag += new ItemDragEventHandler(this.OppList_ItemDrag);
			this.CreatureNameHdr.Text = "Creature";
			this.CreatureNameHdr.Width = 300;
			this.CreatureInfoHdr.Text = "Info";
			this.CreatureInfoHdr.Width = 150;
			ToolStripItem[] creatureContextRemove = new ToolStripItem[] { this.CreatureContextRemove, this.CreatureContextCategory };
			this.CreatureContext.Items.AddRange(creatureContextRemove);
			this.CreatureContext.Name = "CreatureContext";
			this.CreatureContext.Size = new System.Drawing.Size(151, 48);
			this.CreatureContextRemove.Name = "CreatureContextRemove";
			this.CreatureContextRemove.Size = new System.Drawing.Size(150, 22);
			this.CreatureContextRemove.Text = "Remove";
			this.CreatureContextRemove.Click += new EventHandler(this.CreatureContextRemove_Click);
			this.CreatureContextCategory.Name = "CreatureContextCategory";
			this.CreatureContextCategory.Size = new System.Drawing.Size(150, 22);
			this.CreatureContextCategory.Text = "Set Category...";
			this.CreatureContextCategory.Click += new EventHandler(this.CreatureContextCategory_Click);
			ToolStripItem[] searchLbl = new ToolStripItem[] { this.SearchLbl, this.SearchBox, this.toolStripSeparator11, this.CategorisedBtn, this.UncategorisedBtn };
			this.CreatureSearchToolbar.Items.AddRange(searchLbl);
			this.CreatureSearchToolbar.Location = new Point(3, 28);
			this.CreatureSearchToolbar.Name = "CreatureSearchToolbar";
			this.CreatureSearchToolbar.Size = new System.Drawing.Size(612, 25);
			this.CreatureSearchToolbar.TabIndex = 2;
			this.CreatureSearchToolbar.Text = "toolStrip1";
			this.SearchLbl.Name = "SearchLbl";
			this.SearchLbl.Size = new System.Drawing.Size(45, 22);
			this.SearchLbl.Text = "Search:";
			this.SearchBox.BorderStyle = BorderStyle.FixedSingle;
			this.SearchBox.Name = "SearchBox";
			this.SearchBox.Size = new System.Drawing.Size(150, 25);
			this.SearchBox.TextChanged += new EventHandler(this.SearchBox_TextChanged);
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
			this.CategorisedBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.CategorisedBtn.Image = (Image)resources.GetObject("CategorisedBtn.Image");
			this.CategorisedBtn.ImageTransparentColor = Color.Magenta;
			this.CategorisedBtn.Name = "CategorisedBtn";
			this.CategorisedBtn.Size = new System.Drawing.Size(74, 22);
			this.CategorisedBtn.Text = "Categorised";
			this.CategorisedBtn.Click += new EventHandler(this.CreatureFilterCategorised_Click);
			this.UncategorisedBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.UncategorisedBtn.Image = (Image)resources.GetObject("UncategorisedBtn.Image");
			this.UncategorisedBtn.ImageTransparentColor = Color.Magenta;
			this.UncategorisedBtn.Name = "UncategorisedBtn";
			this.UncategorisedBtn.Size = new System.Drawing.Size(87, 22);
			this.UncategorisedBtn.Text = "Uncategorised";
			this.UncategorisedBtn.Click += new EventHandler(this.CreatureFilterUncategorised_Click);
			ToolStripItem[] creatureAddBtn = new ToolStripItem[] { this.CreatureAddBtn, this.OppRemoveBtn, this.OppEditBtn, this.toolStripSeparator1, this.CreatureCutBtn, this.CreatureCopyBtn, this.CreaturePasteBtn, this.toolStripSeparator4, this.CreatureStatBlockBtn, this.toolStripSeparator10, this.CreatureTools };
			this.CreatureToolbar.Items.AddRange(creatureAddBtn);
			this.CreatureToolbar.Location = new Point(3, 3);
			this.CreatureToolbar.Name = "CreatureToolbar";
			this.CreatureToolbar.Size = new System.Drawing.Size(612, 25);
			this.CreatureToolbar.TabIndex = 0;
			this.CreatureToolbar.Text = "toolStrip2";
			this.CreatureAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItem[] creatureAddSingle = new ToolStripItem[] { this.CreatureAddSingle, this.toolStripSeparator19, this.CreatureImport };
			this.CreatureAddBtn.DropDownItems.AddRange(creatureAddSingle);
			this.CreatureAddBtn.Image = (Image)resources.GetObject("CreatureAddBtn.Image");
			this.CreatureAddBtn.ImageTransparentColor = Color.Magenta;
			this.CreatureAddBtn.Name = "CreatureAddBtn";
			this.CreatureAddBtn.Size = new System.Drawing.Size(42, 22);
			this.CreatureAddBtn.Text = "Add";
			this.CreatureAddSingle.Name = "CreatureAddSingle";
			this.CreatureAddSingle.Size = new System.Drawing.Size(162, 22);
			this.CreatureAddSingle.Text = "Add a Creature...";
			this.CreatureAddSingle.Click += new EventHandler(this.CreatureAddBtn_Click);
			this.toolStripSeparator19.Name = "toolStripSeparator19";
			this.toolStripSeparator19.Size = new System.Drawing.Size(159, 6);
			this.CreatureImport.Name = "CreatureImport";
			this.CreatureImport.Size = new System.Drawing.Size(162, 22);
			this.CreatureImport.Text = "Import...";
			this.CreatureImport.Click += new EventHandler(this.CreatureImport_Click);
			this.OppRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.OppRemoveBtn.Image = (Image)resources.GetObject("OppRemoveBtn.Image");
			this.OppRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.OppRemoveBtn.Name = "OppRemoveBtn";
			this.OppRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.OppRemoveBtn.Text = "Remove";
			this.OppRemoveBtn.Click += new EventHandler(this.OppRemoveBtn_Click);
			this.OppEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.OppEditBtn.Image = (Image)resources.GetObject("OppEditBtn.Image");
			this.OppEditBtn.ImageTransparentColor = Color.Magenta;
			this.OppEditBtn.Name = "OppEditBtn";
			this.OppEditBtn.Size = new System.Drawing.Size(31, 22);
			this.OppEditBtn.Text = "Edit";
			this.OppEditBtn.Click += new EventHandler(this.OppEditBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.CreatureCutBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.CreatureCutBtn.Image = (Image)resources.GetObject("CreatureCutBtn.Image");
			this.CreatureCutBtn.ImageTransparentColor = Color.Magenta;
			this.CreatureCutBtn.Name = "CreatureCutBtn";
			this.CreatureCutBtn.Size = new System.Drawing.Size(30, 22);
			this.CreatureCutBtn.Text = "Cut";
			this.CreatureCutBtn.Click += new EventHandler(this.CreatureCutBtn_Click);
			this.CreatureCopyBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.CreatureCopyBtn.Image = (Image)resources.GetObject("CreatureCopyBtn.Image");
			this.CreatureCopyBtn.ImageTransparentColor = Color.Magenta;
			this.CreatureCopyBtn.Name = "CreatureCopyBtn";
			this.CreatureCopyBtn.Size = new System.Drawing.Size(39, 22);
			this.CreatureCopyBtn.Text = "Copy";
			this.CreatureCopyBtn.Click += new EventHandler(this.CreatureCopyBtn_Click);
			this.CreaturePasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.CreaturePasteBtn.Image = (Image)resources.GetObject("CreaturePasteBtn.Image");
			this.CreaturePasteBtn.ImageTransparentColor = Color.Magenta;
			this.CreaturePasteBtn.Name = "CreaturePasteBtn";
			this.CreaturePasteBtn.Size = new System.Drawing.Size(39, 22);
			this.CreaturePasteBtn.Text = "Paste";
			this.CreaturePasteBtn.Click += new EventHandler(this.CreaturePasteBtn_Click);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			this.CreatureStatBlockBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.CreatureStatBlockBtn.Image = (Image)resources.GetObject("CreatureStatBlockBtn.Image");
			this.CreatureStatBlockBtn.ImageTransparentColor = Color.Magenta;
			this.CreatureStatBlockBtn.Name = "CreatureStatBlockBtn";
			this.CreatureStatBlockBtn.Size = new System.Drawing.Size(63, 22);
			this.CreatureStatBlockBtn.Text = "Stat Block";
			this.CreatureStatBlockBtn.Click += new EventHandler(this.CreatureStatBlockBtn_Click);
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
			this.CreatureTools.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItem[] creatureToolsDemographics = new ToolStripItem[] { this.CreatureToolsDemographics, this.CreatureToolsPowerStatistics, this.CreatureToolsFilterList, this.CreatureToolsExport };
			this.CreatureTools.DropDownItems.AddRange(creatureToolsDemographics);
			this.CreatureTools.Image = (Image)resources.GetObject("CreatureTools.Image");
			this.CreatureTools.ImageTransparentColor = Color.Magenta;
			this.CreatureTools.Name = "CreatureTools";
			this.CreatureTools.Size = new System.Drawing.Size(49, 22);
			this.CreatureTools.Text = "Tools";
			this.CreatureToolsDemographics.Name = "CreatureToolsDemographics";
			this.CreatureToolsDemographics.Size = new System.Drawing.Size(165, 22);
			this.CreatureToolsDemographics.Text = "Demographics";
			this.CreatureToolsDemographics.Click += new EventHandler(this.CreatureDemoBtn_Click);
			this.CreatureToolsPowerStatistics.Name = "CreatureToolsPowerStatistics";
			this.CreatureToolsPowerStatistics.Size = new System.Drawing.Size(165, 22);
			this.CreatureToolsPowerStatistics.Text = "Power Statistics...";
			this.CreatureToolsPowerStatistics.Click += new EventHandler(this.PowerStatsBtn_Click);
			this.CreatureToolsFilterList.Name = "CreatureToolsFilterList";
			this.CreatureToolsFilterList.Size = new System.Drawing.Size(165, 22);
			this.CreatureToolsFilterList.Text = "Filter List";
			this.CreatureToolsFilterList.Click += new EventHandler(this.FilterBtn_Click);
			this.CreatureToolsExport.Name = "CreatureToolsExport";
			this.CreatureToolsExport.Size = new System.Drawing.Size(165, 22);
			this.CreatureToolsExport.Text = "Export...";
			this.CreatureToolsExport.Click += new EventHandler(this.CreatureToolsExport_Click);
			this.TemplatesPage.Controls.Add(this.TemplateList);
			this.TemplatesPage.Controls.Add(this.TemplateToolbar);
			this.TemplatesPage.Location = new Point(4, 22);
			this.TemplatesPage.Name = "TemplatesPage";
			this.TemplatesPage.Padding = new System.Windows.Forms.Padding(3);
			this.TemplatesPage.Size = new System.Drawing.Size(618, 246);
			this.TemplatesPage.TabIndex = 1;
			this.TemplatesPage.Text = "Templates";
			this.TemplatesPage.UseVisualStyleBackColor = true;
			ColumnHeader[] templateNameHdr = new ColumnHeader[] { this.TemplateNameHdr, this.TemplateInfoHdr };
			this.TemplateList.Columns.AddRange(templateNameHdr);
			this.TemplateList.ContextMenuStrip = this.TemplateContext;
			this.TemplateList.Dock = DockStyle.Fill;
			this.TemplateList.FullRowSelect = true;
			listViewGroup.Header = "Functional Templates";
			listViewGroup.Name = "FunctionalGroup";
			listViewGroup1.Header = "Class Templates";
			listViewGroup1.Name = "ClassGroup";
			listViewGroup2.Header = "Themes";
			listViewGroup2.Name = "ThemeGroup";
			ListViewGroup[] listViewGroupArray = new ListViewGroup[] { listViewGroup, listViewGroup1, listViewGroup2 };
			this.TemplateList.Groups.AddRange(listViewGroupArray);
			this.TemplateList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.TemplateList.HideSelection = false;
			this.TemplateList.Location = new Point(3, 28);
			this.TemplateList.Name = "TemplateList";
			this.TemplateList.Size = new System.Drawing.Size(612, 215);
			this.TemplateList.Sorting = SortOrder.Ascending;
			this.TemplateList.TabIndex = 2;
			this.TemplateList.UseCompatibleStateImageBehavior = false;
			this.TemplateList.View = View.Details;
			this.TemplateList.DoubleClick += new EventHandler(this.TemplateEditBtn_Click);
			this.TemplateList.ItemDrag += new ItemDragEventHandler(this.TemplateList_ItemDrag);
			this.TemplateNameHdr.Text = "Template";
			this.TemplateNameHdr.Width = 300;
			this.TemplateInfoHdr.Text = "Role";
			this.TemplateInfoHdr.Width = 150;
			ToolStripItem[] templateContextRemove = new ToolStripItem[] { this.TemplateContextRemove, this.TemplateContextType };
			this.TemplateContext.Items.AddRange(templateContextRemove);
			this.TemplateContext.Name = "TemplateContext";
			this.TemplateContext.Size = new System.Drawing.Size(118, 48);
			this.TemplateContextRemove.Name = "TemplateContextRemove";
			this.TemplateContextRemove.Size = new System.Drawing.Size(117, 22);
			this.TemplateContextRemove.Text = "Remove";
			this.TemplateContextRemove.Click += new EventHandler(this.TemplateContextRemove_Click);
			ToolStripItem[] templateFunctional = new ToolStripItem[] { this.TemplateFunctional, this.TemplateClass };
			this.TemplateContextType.DropDownItems.AddRange(templateFunctional);
			this.TemplateContextType.Name = "TemplateContextType";
			this.TemplateContextType.Size = new System.Drawing.Size(117, 22);
			this.TemplateContextType.Text = "Type";
			this.TemplateFunctional.Name = "TemplateFunctional";
			this.TemplateFunctional.Size = new System.Drawing.Size(130, 22);
			this.TemplateFunctional.Text = "Functional";
			this.TemplateFunctional.Click += new EventHandler(this.TemplateFunctional_Click);
			this.TemplateClass.Name = "TemplateClass";
			this.TemplateClass.Size = new System.Drawing.Size(130, 22);
			this.TemplateClass.Text = "Class";
			this.TemplateClass.Click += new EventHandler(this.TemplateClass_Click);
			ToolStripItem[] templateAddBtn = new ToolStripItem[] { this.TemplateAddBtn, this.TemplateRemoveBtn, this.TemplateEditBtn, this.toolStripSeparator2, this.TemplateCutBtn, this.TemplateCopyBtn, this.TemplatePasteBtn, this.toolStripSeparator18, this.TemplateStatBlock, this.toolStripSeparator21, this.TemplateTools };
			this.TemplateToolbar.Items.AddRange(templateAddBtn);
			this.TemplateToolbar.Location = new Point(3, 3);
			this.TemplateToolbar.Name = "TemplateToolbar";
			this.TemplateToolbar.Size = new System.Drawing.Size(612, 25);
			this.TemplateToolbar.TabIndex = 1;
			this.TemplateToolbar.Text = "toolStrip2";
			this.TemplateAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItem[] templateAddTheme = new ToolStripItem[] { this.addTemplateToolStripMenuItem, this.TemplateAddTheme, this.toolStripSeparator20, this.TemplateImport };
			this.TemplateAddBtn.DropDownItems.AddRange(templateAddTheme);
			this.TemplateAddBtn.Image = (Image)resources.GetObject("TemplateAddBtn.Image");
			this.TemplateAddBtn.ImageTransparentColor = Color.Magenta;
			this.TemplateAddBtn.Name = "TemplateAddBtn";
			this.TemplateAddBtn.Size = new System.Drawing.Size(42, 22);
			this.TemplateAddBtn.Text = "Add";
			this.addTemplateToolStripMenuItem.Name = "addTemplateToolStripMenuItem";
			this.addTemplateToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.addTemplateToolStripMenuItem.Text = "Add a Template...";
			this.addTemplateToolStripMenuItem.Click += new EventHandler(this.TemplateAddBtn_Click);
			this.TemplateAddTheme.Name = "TemplateAddTheme";
			this.TemplateAddTheme.Size = new System.Drawing.Size(167, 22);
			this.TemplateAddTheme.Text = "Add a Theme...";
			this.TemplateAddTheme.Click += new EventHandler(this.TemplateAddTheme_Click);
			this.toolStripSeparator20.Name = "toolStripSeparator20";
			this.toolStripSeparator20.Size = new System.Drawing.Size(164, 6);
			this.TemplateImport.Name = "TemplateImport";
			this.TemplateImport.Size = new System.Drawing.Size(167, 22);
			this.TemplateImport.Text = "Import...";
			this.TemplateImport.Click += new EventHandler(this.TemplateImport_Click);
			this.TemplateRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TemplateRemoveBtn.Image = (Image)resources.GetObject("TemplateRemoveBtn.Image");
			this.TemplateRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.TemplateRemoveBtn.Name = "TemplateRemoveBtn";
			this.TemplateRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.TemplateRemoveBtn.Text = "Remove";
			this.TemplateRemoveBtn.Click += new EventHandler(this.TemplateRemoveBtn_Click);
			this.TemplateEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TemplateEditBtn.Image = (Image)resources.GetObject("TemplateEditBtn.Image");
			this.TemplateEditBtn.ImageTransparentColor = Color.Magenta;
			this.TemplateEditBtn.Name = "TemplateEditBtn";
			this.TemplateEditBtn.Size = new System.Drawing.Size(31, 22);
			this.TemplateEditBtn.Text = "Edit";
			this.TemplateEditBtn.Click += new EventHandler(this.TemplateEditBtn_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			this.TemplateCutBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TemplateCutBtn.Image = (Image)resources.GetObject("TemplateCutBtn.Image");
			this.TemplateCutBtn.ImageTransparentColor = Color.Magenta;
			this.TemplateCutBtn.Name = "TemplateCutBtn";
			this.TemplateCutBtn.Size = new System.Drawing.Size(30, 22);
			this.TemplateCutBtn.Text = "Cut";
			this.TemplateCutBtn.Click += new EventHandler(this.TemplateCutBtn_Click);
			this.TemplateCopyBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TemplateCopyBtn.Image = (Image)resources.GetObject("TemplateCopyBtn.Image");
			this.TemplateCopyBtn.ImageTransparentColor = Color.Magenta;
			this.TemplateCopyBtn.Name = "TemplateCopyBtn";
			this.TemplateCopyBtn.Size = new System.Drawing.Size(39, 22);
			this.TemplateCopyBtn.Text = "Copy";
			this.TemplateCopyBtn.Click += new EventHandler(this.TemplateCopyBtn_Click);
			this.TemplatePasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TemplatePasteBtn.Image = (Image)resources.GetObject("TemplatePasteBtn.Image");
			this.TemplatePasteBtn.ImageTransparentColor = Color.Magenta;
			this.TemplatePasteBtn.Name = "TemplatePasteBtn";
			this.TemplatePasteBtn.Size = new System.Drawing.Size(39, 22);
			this.TemplatePasteBtn.Text = "Paste";
			this.TemplatePasteBtn.Click += new EventHandler(this.TemplatePasteBtn_Click);
			this.toolStripSeparator18.Name = "toolStripSeparator18";
			this.toolStripSeparator18.Size = new System.Drawing.Size(6, 25);
			this.TemplateStatBlock.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TemplateStatBlock.Image = (Image)resources.GetObject("TemplateStatBlock.Image");
			this.TemplateStatBlock.ImageTransparentColor = Color.Magenta;
			this.TemplateStatBlock.Name = "TemplateStatBlock";
			this.TemplateStatBlock.Size = new System.Drawing.Size(63, 22);
			this.TemplateStatBlock.Text = "Stat Block";
			this.TemplateStatBlock.Click += new EventHandler(this.TemplateStatBlock_Click);
			this.toolStripSeparator21.Name = "toolStripSeparator21";
			this.toolStripSeparator21.Size = new System.Drawing.Size(6, 25);
			this.TemplateTools.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TemplateTools.DropDownItems.AddRange(new ToolStripItem[] { this.TemplateToolsExport });
			this.TemplateTools.Image = (Image)resources.GetObject("TemplateTools.Image");
			this.TemplateTools.ImageTransparentColor = Color.Magenta;
			this.TemplateTools.Name = "TemplateTools";
			this.TemplateTools.Size = new System.Drawing.Size(49, 22);
			this.TemplateTools.Text = "Tools";
			this.TemplateToolsExport.Name = "TemplateToolsExport";
			this.TemplateToolsExport.Size = new System.Drawing.Size(116, 22);
			this.TemplateToolsExport.Text = "Export...";
			this.TemplateToolsExport.Click += new EventHandler(this.TemplateToolsExport_Click);
			this.TrapsPage.Controls.Add(this.TrapList);
			this.TrapsPage.Controls.Add(this.TrapToolbar);
			this.TrapsPage.Location = new Point(4, 22);
			this.TrapsPage.Name = "TrapsPage";
			this.TrapsPage.Padding = new System.Windows.Forms.Padding(3);
			this.TrapsPage.Size = new System.Drawing.Size(618, 246);
			this.TrapsPage.TabIndex = 3;
			this.TrapsPage.Text = "Traps / Hazards";
			this.TrapsPage.UseVisualStyleBackColor = true;
			ColumnHeader[] trapNameHdr = new ColumnHeader[] { this.TrapNameHdr, this.TrapInfoHdr };
			this.TrapList.Columns.AddRange(trapNameHdr);
			this.TrapList.ContextMenuStrip = this.TrapContext;
			this.TrapList.Dock = DockStyle.Fill;
			this.TrapList.FullRowSelect = true;
			listViewGroup3.Header = "Traps";
			listViewGroup3.Name = "TrapGroup";
			listViewGroup4.Header = "Hazards";
			listViewGroup4.Name = "HazardGroup";
			this.TrapList.Groups.AddRange(new ListViewGroup[] { listViewGroup3, listViewGroup4 });
			this.TrapList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.TrapList.HideSelection = false;
			this.TrapList.Location = new Point(3, 28);
			this.TrapList.Name = "TrapList";
			this.TrapList.Size = new System.Drawing.Size(612, 215);
			this.TrapList.Sorting = SortOrder.Ascending;
			this.TrapList.TabIndex = 4;
			this.TrapList.UseCompatibleStateImageBehavior = false;
			this.TrapList.View = View.Details;
			this.TrapList.DoubleClick += new EventHandler(this.TrapEditBtn_Click);
			this.TrapList.ItemDrag += new ItemDragEventHandler(this.TrapList_ItemDrag);
			this.TrapNameHdr.Text = "Trap";
			this.TrapNameHdr.Width = 300;
			this.TrapInfoHdr.Text = "Role";
			this.TrapInfoHdr.Width = 150;
			ToolStripItem[] trapContextRemove = new ToolStripItem[] { this.TrapContextRemove, this.TrapContextType };
			this.TrapContext.Items.AddRange(trapContextRemove);
			this.TrapContext.Name = "TrapContext";
			this.TrapContext.Size = new System.Drawing.Size(118, 48);
			this.TrapContextRemove.Name = "TrapContextRemove";
			this.TrapContextRemove.Size = new System.Drawing.Size(117, 22);
			this.TrapContextRemove.Text = "Remove";
			this.TrapContextRemove.Click += new EventHandler(this.TrapContextRemove_Click);
			ToolStripItem[] trapTrap = new ToolStripItem[] { this.TrapTrap, this.TrapHazard };
			this.TrapContextType.DropDownItems.AddRange(trapTrap);
			this.TrapContextType.Name = "TrapContextType";
			this.TrapContextType.Size = new System.Drawing.Size(117, 22);
			this.TrapContextType.Text = "Type";
			this.TrapTrap.Name = "TrapTrap";
			this.TrapTrap.Size = new System.Drawing.Size(111, 22);
			this.TrapTrap.Text = "Trap";
			this.TrapTrap.Click += new EventHandler(this.TrapTrap_Click);
			this.TrapHazard.Name = "TrapHazard";
			this.TrapHazard.Size = new System.Drawing.Size(111, 22);
			this.TrapHazard.Text = "Hazard";
			this.TrapHazard.Click += new EventHandler(this.TrapHazard_Click);
			ToolStripItem[] trapAdd = new ToolStripItem[] { this.TrapAdd, this.TrapRemoveBtn, this.TrapEditBtn, this.toolStripSeparator6, this.TrapCutBtn, this.TrapCopyBtn, this.TrapPasteBtn, this.toolStripSeparator8, this.TrapStatBlockBtn, this.toolStripSeparator13, this.TrapTools };
			this.TrapToolbar.Items.AddRange(trapAdd);
			this.TrapToolbar.Location = new Point(3, 3);
			this.TrapToolbar.Name = "TrapToolbar";
			this.TrapToolbar.Size = new System.Drawing.Size(612, 25);
			this.TrapToolbar.TabIndex = 3;
			this.TrapToolbar.Text = "toolStrip2";
			this.TrapAdd.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItem[] trapAddAdd = new ToolStripItem[] { this.TrapAddAdd, this.toolStripSeparator25, this.TrapAddImport };
			this.TrapAdd.DropDownItems.AddRange(trapAddAdd);
			this.TrapAdd.Image = (Image)resources.GetObject("TrapAdd.Image");
			this.TrapAdd.ImageTransparentColor = Color.Magenta;
			this.TrapAdd.Name = "TrapAdd";
			this.TrapAdd.Size = new System.Drawing.Size(42, 22);
			this.TrapAdd.Text = "Add";
			this.TrapAddAdd.Name = "TrapAddAdd";
			this.TrapAddAdd.Size = new System.Drawing.Size(141, 22);
			this.TrapAddAdd.Text = "Add a Trap...";
			this.TrapAddAdd.Click += new EventHandler(this.TrapAddBtn_Click);
			this.toolStripSeparator25.Name = "toolStripSeparator25";
			this.toolStripSeparator25.Size = new System.Drawing.Size(138, 6);
			this.TrapAddImport.Name = "TrapAddImport";
			this.TrapAddImport.Size = new System.Drawing.Size(141, 22);
			this.TrapAddImport.Text = "Import...";
			this.TrapAddImport.Click += new EventHandler(this.TrapAddImport_Click);
			this.TrapRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TrapRemoveBtn.Image = (Image)resources.GetObject("TrapRemoveBtn.Image");
			this.TrapRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.TrapRemoveBtn.Name = "TrapRemoveBtn";
			this.TrapRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.TrapRemoveBtn.Text = "Remove";
			this.TrapRemoveBtn.Click += new EventHandler(this.TrapRemoveBtn_Click);
			this.TrapEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TrapEditBtn.Image = (Image)resources.GetObject("TrapEditBtn.Image");
			this.TrapEditBtn.ImageTransparentColor = Color.Magenta;
			this.TrapEditBtn.Name = "TrapEditBtn";
			this.TrapEditBtn.Size = new System.Drawing.Size(31, 22);
			this.TrapEditBtn.Text = "Edit";
			this.TrapEditBtn.Click += new EventHandler(this.TrapEditBtn_Click);
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
			this.TrapCutBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TrapCutBtn.Image = (Image)resources.GetObject("TrapCutBtn.Image");
			this.TrapCutBtn.ImageTransparentColor = Color.Magenta;
			this.TrapCutBtn.Name = "TrapCutBtn";
			this.TrapCutBtn.Size = new System.Drawing.Size(30, 22);
			this.TrapCutBtn.Text = "Cut";
			this.TrapCutBtn.Click += new EventHandler(this.TrapCutBtn_Click);
			this.TrapCopyBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TrapCopyBtn.Image = (Image)resources.GetObject("TrapCopyBtn.Image");
			this.TrapCopyBtn.ImageTransparentColor = Color.Magenta;
			this.TrapCopyBtn.Name = "TrapCopyBtn";
			this.TrapCopyBtn.Size = new System.Drawing.Size(39, 22);
			this.TrapCopyBtn.Text = "Copy";
			this.TrapCopyBtn.Click += new EventHandler(this.TrapCopyBtn_Click);
			this.TrapPasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TrapPasteBtn.Image = (Image)resources.GetObject("TrapPasteBtn.Image");
			this.TrapPasteBtn.ImageTransparentColor = Color.Magenta;
			this.TrapPasteBtn.Name = "TrapPasteBtn";
			this.TrapPasteBtn.Size = new System.Drawing.Size(39, 22);
			this.TrapPasteBtn.Text = "Paste";
			this.TrapPasteBtn.Click += new EventHandler(this.TrapPasteBtn_Click);
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
			this.TrapStatBlockBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TrapStatBlockBtn.Image = (Image)resources.GetObject("TrapStatBlockBtn.Image");
			this.TrapStatBlockBtn.ImageTransparentColor = Color.Magenta;
			this.TrapStatBlockBtn.Name = "TrapStatBlockBtn";
			this.TrapStatBlockBtn.Size = new System.Drawing.Size(63, 22);
			this.TrapStatBlockBtn.Text = "Stat Block";
			this.TrapStatBlockBtn.Click += new EventHandler(this.TrapStatBlockBtn_Click);
			this.toolStripSeparator13.Name = "toolStripSeparator13";
			this.toolStripSeparator13.Size = new System.Drawing.Size(6, 25);
			this.TrapTools.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItem[] trapToolsDemographics = new ToolStripItem[] { this.TrapToolsDemographics, this.TrapToolsExport };
			this.TrapTools.DropDownItems.AddRange(trapToolsDemographics);
			this.TrapTools.Image = (Image)resources.GetObject("TrapTools.Image");
			this.TrapTools.ImageTransparentColor = Color.Magenta;
			this.TrapTools.Name = "TrapTools";
			this.TrapTools.Size = new System.Drawing.Size(49, 22);
			this.TrapTools.Text = "Tools";
			this.TrapToolsDemographics.Name = "TrapToolsDemographics";
			this.TrapToolsDemographics.Size = new System.Drawing.Size(151, 22);
			this.TrapToolsDemographics.Text = "Demographics";
			this.TrapToolsDemographics.Click += new EventHandler(this.TrapDemoBtn_Click);
			this.TrapToolsExport.Name = "TrapToolsExport";
			this.TrapToolsExport.Size = new System.Drawing.Size(151, 22);
			this.TrapToolsExport.Text = "Export...";
			this.TrapToolsExport.Click += new EventHandler(this.TrapToolsExport_Click);
			this.ChallengePage.Controls.Add(this.ChallengeList);
			this.ChallengePage.Controls.Add(this.ChallengeToolbar);
			this.ChallengePage.Location = new Point(4, 22);
			this.ChallengePage.Name = "ChallengePage";
			this.ChallengePage.Padding = new System.Windows.Forms.Padding(3);
			this.ChallengePage.Size = new System.Drawing.Size(618, 246);
			this.ChallengePage.TabIndex = 4;
			this.ChallengePage.Text = "Skill Challenges";
			this.ChallengePage.UseVisualStyleBackColor = true;
			ColumnHeader[] challengeNameHdr = new ColumnHeader[] { this.ChallengeNameHdr, this.ChallengeInfoHdr };
			this.ChallengeList.Columns.AddRange(challengeNameHdr);
			this.ChallengeList.Dock = DockStyle.Fill;
			this.ChallengeList.FullRowSelect = true;
			listViewGroup5.Header = "Traps";
			listViewGroup5.Name = "TrapGroup";
			listViewGroup6.Header = "Hazards";
			listViewGroup6.Name = "HazardGroup";
			this.ChallengeList.Groups.AddRange(new ListViewGroup[] { listViewGroup5, listViewGroup6 });
			this.ChallengeList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.ChallengeList.HideSelection = false;
			this.ChallengeList.Location = new Point(3, 28);
			this.ChallengeList.Name = "ChallengeList";
			this.ChallengeList.Size = new System.Drawing.Size(612, 215);
			this.ChallengeList.Sorting = SortOrder.Ascending;
			this.ChallengeList.TabIndex = 6;
			this.ChallengeList.UseCompatibleStateImageBehavior = false;
			this.ChallengeList.View = View.Details;
			this.ChallengeList.DoubleClick += new EventHandler(this.ChallengeEditBtn_Click);
			this.ChallengeList.ItemDrag += new ItemDragEventHandler(this.ChallengeList_ItemDrag);
			this.ChallengeNameHdr.Text = "Challenge";
			this.ChallengeNameHdr.Width = 300;
			this.ChallengeInfoHdr.Text = "Complexity";
			this.ChallengeInfoHdr.Width = 150;
			ToolStripItem[] challengeAdd = new ToolStripItem[] { this.ChallengeAdd, this.ChallengeRemoveBtn, this.ChallengeEditBtn, this.toolStripSeparator7, this.ChallengeCutBtn, this.ChallengeCopyBtn, this.ChallengePasteBtn, this.toolStripSeparator9, this.ChallengeStatBlockBtn, this.toolStripSeparator22, this.ChallengeTools };
			this.ChallengeToolbar.Items.AddRange(challengeAdd);
			this.ChallengeToolbar.Location = new Point(3, 3);
			this.ChallengeToolbar.Name = "ChallengeToolbar";
			this.ChallengeToolbar.Size = new System.Drawing.Size(612, 25);
			this.ChallengeToolbar.TabIndex = 5;
			this.ChallengeToolbar.Text = "toolStrip2";
			this.ChallengeAdd.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItem[] challengeAddAdd = new ToolStripItem[] { this.ChallengeAddAdd, this.toolStripSeparator26, this.ChallengeAddImport };
			this.ChallengeAdd.DropDownItems.AddRange(challengeAddAdd);
			this.ChallengeAdd.Image = (Image)resources.GetObject("ChallengeAdd.Image");
			this.ChallengeAdd.ImageTransparentColor = Color.Magenta;
			this.ChallengeAdd.Name = "ChallengeAdd";
			this.ChallengeAdd.Size = new System.Drawing.Size(42, 22);
			this.ChallengeAdd.Text = "Add";
			this.ChallengeAddAdd.Name = "ChallengeAddAdd";
			this.ChallengeAddAdd.Size = new System.Drawing.Size(194, 22);
			this.ChallengeAddAdd.Text = "Add a Skill Challenge...";
			this.ChallengeAddAdd.Click += new EventHandler(this.ChallengeAddBtn_Click);
			this.toolStripSeparator26.Name = "toolStripSeparator26";
			this.toolStripSeparator26.Size = new System.Drawing.Size(191, 6);
			this.ChallengeAddImport.Name = "ChallengeAddImport";
			this.ChallengeAddImport.Size = new System.Drawing.Size(194, 22);
			this.ChallengeAddImport.Text = "Import...";
			this.ChallengeAddImport.Click += new EventHandler(this.ChallengeAddImport_Click);
			this.ChallengeRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChallengeRemoveBtn.Image = (Image)resources.GetObject("ChallengeRemoveBtn.Image");
			this.ChallengeRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.ChallengeRemoveBtn.Name = "ChallengeRemoveBtn";
			this.ChallengeRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.ChallengeRemoveBtn.Text = "Remove";
			this.ChallengeRemoveBtn.Click += new EventHandler(this.ChallengeRemoveBtn_Click);
			this.ChallengeEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChallengeEditBtn.Image = (Image)resources.GetObject("ChallengeEditBtn.Image");
			this.ChallengeEditBtn.ImageTransparentColor = Color.Magenta;
			this.ChallengeEditBtn.Name = "ChallengeEditBtn";
			this.ChallengeEditBtn.Size = new System.Drawing.Size(31, 22);
			this.ChallengeEditBtn.Text = "Edit";
			this.ChallengeEditBtn.Click += new EventHandler(this.ChallengeEditBtn_Click);
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
			this.ChallengeCutBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChallengeCutBtn.Image = (Image)resources.GetObject("ChallengeCutBtn.Image");
			this.ChallengeCutBtn.ImageTransparentColor = Color.Magenta;
			this.ChallengeCutBtn.Name = "ChallengeCutBtn";
			this.ChallengeCutBtn.Size = new System.Drawing.Size(30, 22);
			this.ChallengeCutBtn.Text = "Cut";
			this.ChallengeCutBtn.Click += new EventHandler(this.ChallengeCutBtn_Click);
			this.ChallengeCopyBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChallengeCopyBtn.Image = (Image)resources.GetObject("ChallengeCopyBtn.Image");
			this.ChallengeCopyBtn.ImageTransparentColor = Color.Magenta;
			this.ChallengeCopyBtn.Name = "ChallengeCopyBtn";
			this.ChallengeCopyBtn.Size = new System.Drawing.Size(39, 22);
			this.ChallengeCopyBtn.Text = "Copy";
			this.ChallengeCopyBtn.Click += new EventHandler(this.ChallengeCopyBtn_Click);
			this.ChallengePasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChallengePasteBtn.Image = (Image)resources.GetObject("ChallengePasteBtn.Image");
			this.ChallengePasteBtn.ImageTransparentColor = Color.Magenta;
			this.ChallengePasteBtn.Name = "ChallengePasteBtn";
			this.ChallengePasteBtn.Size = new System.Drawing.Size(39, 22);
			this.ChallengePasteBtn.Text = "Paste";
			this.ChallengePasteBtn.Click += new EventHandler(this.ChallengePasteBtn_Click);
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
			this.ChallengeStatBlockBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChallengeStatBlockBtn.Image = (Image)resources.GetObject("ChallengeStatBlockBtn.Image");
			this.ChallengeStatBlockBtn.ImageTransparentColor = Color.Magenta;
			this.ChallengeStatBlockBtn.Name = "ChallengeStatBlockBtn";
			this.ChallengeStatBlockBtn.Size = new System.Drawing.Size(63, 22);
			this.ChallengeStatBlockBtn.Text = "Stat Block";
			this.ChallengeStatBlockBtn.Click += new EventHandler(this.ChallengeStatBlockBtn_Click);
			this.toolStripSeparator22.Name = "toolStripSeparator22";
			this.toolStripSeparator22.Size = new System.Drawing.Size(6, 25);
			this.ChallengeTools.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChallengeTools.DropDownItems.AddRange(new ToolStripItem[] { this.ChallengeToolsExport });
			this.ChallengeTools.Image = (Image)resources.GetObject("ChallengeTools.Image");
			this.ChallengeTools.ImageTransparentColor = Color.Magenta;
			this.ChallengeTools.Name = "ChallengeTools";
			this.ChallengeTools.Size = new System.Drawing.Size(49, 22);
			this.ChallengeTools.Text = "Tools";
			this.ChallengeToolsExport.Name = "ChallengeToolsExport";
			this.ChallengeToolsExport.Size = new System.Drawing.Size(116, 22);
			this.ChallengeToolsExport.Text = "Export...";
			this.ChallengeToolsExport.Click += new EventHandler(this.ChallengeToolsExport_Click);
			this.MagicItemsPage.Controls.Add(this.splitContainer1);
			this.MagicItemsPage.Location = new Point(4, 22);
			this.MagicItemsPage.Name = "MagicItemsPage";
			this.MagicItemsPage.Padding = new System.Windows.Forms.Padding(3);
			this.MagicItemsPage.Size = new System.Drawing.Size(618, 246);
			this.MagicItemsPage.TabIndex = 6;
			this.MagicItemsPage.Text = "Magic Items";
			this.MagicItemsPage.UseVisualStyleBackColor = true;
			this.splitContainer1.Dock = DockStyle.Fill;
			this.splitContainer1.Location = new Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Panel1.Controls.Add(this.MagicItemList);
			this.splitContainer1.Panel1.Controls.Add(this.MagicItemToolbar);
			this.splitContainer1.Panel2.Controls.Add(this.MagicItemVersionList);
			this.splitContainer1.Panel2.Controls.Add(this.MagicItemVersionToolbar);
			this.splitContainer1.Size = new System.Drawing.Size(612, 240);
			this.splitContainer1.SplitterDistance = 309;
			this.splitContainer1.TabIndex = 7;
			this.MagicItemList.Columns.AddRange(new ColumnHeader[] { this.MagicItemHdr });
			this.MagicItemList.ContextMenuStrip = this.MagicItemContext;
			this.MagicItemList.Dock = DockStyle.Fill;
			this.MagicItemList.FullRowSelect = true;
			this.MagicItemList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.MagicItemList.HideSelection = false;
			this.MagicItemList.Location = new Point(0, 25);
			this.MagicItemList.MultiSelect = false;
			this.MagicItemList.Name = "MagicItemList";
			this.MagicItemList.Size = new System.Drawing.Size(309, 215);
			this.MagicItemList.Sorting = SortOrder.Ascending;
			this.MagicItemList.TabIndex = 6;
			this.MagicItemList.UseCompatibleStateImageBehavior = false;
			this.MagicItemList.View = View.Details;
			this.MagicItemList.SelectedIndexChanged += new EventHandler(this.MagicItemList_SelectedIndexChanged);
			this.MagicItemHdr.Text = "Magic Item";
			this.MagicItemHdr.Width = 273;
			this.MagicItemContext.Items.AddRange(new ToolStripItem[] { this.MagicItemContextRemove });
			this.MagicItemContext.Name = "ChallengeContext";
			this.MagicItemContext.Size = new System.Drawing.Size(118, 26);
			this.MagicItemContextRemove.Name = "MagicItemContextRemove";
			this.MagicItemContextRemove.Size = new System.Drawing.Size(117, 22);
			this.MagicItemContextRemove.Text = "Remove";
			this.MagicItemContextRemove.Click += new EventHandler(this.MagicItemContextRemove_Click);
			ToolStripItem[] magicItemAdd = new ToolStripItem[] { this.MagicItemAdd, this.toolStripSeparator14, this.MagicItemTools };
			this.MagicItemToolbar.Items.AddRange(magicItemAdd);
			this.MagicItemToolbar.Location = new Point(0, 0);
			this.MagicItemToolbar.Name = "MagicItemToolbar";
			this.MagicItemToolbar.Size = new System.Drawing.Size(309, 25);
			this.MagicItemToolbar.TabIndex = 5;
			this.MagicItemToolbar.Text = "toolStrip2";
			this.MagicItemAdd.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItem[] magicItemAddAdd = new ToolStripItem[] { this.MagicItemAddAdd, this.toolStripSeparator27, this.MagicItemAddImport };
			this.MagicItemAdd.DropDownItems.AddRange(magicItemAddAdd);
			this.MagicItemAdd.Image = (Image)resources.GetObject("MagicItemAdd.Image");
			this.MagicItemAdd.ImageTransparentColor = Color.Magenta;
			this.MagicItemAdd.Name = "MagicItemAdd";
			this.MagicItemAdd.Size = new System.Drawing.Size(42, 22);
			this.MagicItemAdd.Text = "Add";
			this.MagicItemAddAdd.Name = "MagicItemAddAdd";
			this.MagicItemAddAdd.Size = new System.Drawing.Size(177, 22);
			this.MagicItemAddAdd.Text = "Add a Magic Item...";
			this.MagicItemAddAdd.Click += new EventHandler(this.MagicItemAddBtn_Click);
			this.toolStripSeparator27.Name = "toolStripSeparator27";
			this.toolStripSeparator27.Size = new System.Drawing.Size(174, 6);
			this.MagicItemAddImport.Name = "MagicItemAddImport";
			this.MagicItemAddImport.Size = new System.Drawing.Size(177, 22);
			this.MagicItemAddImport.Text = "Import...";
			this.MagicItemAddImport.Click += new EventHandler(this.MagicItemAddImport_Click);
			this.toolStripSeparator14.Name = "toolStripSeparator14";
			this.toolStripSeparator14.Size = new System.Drawing.Size(6, 25);
			this.MagicItemTools.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItem[] magicItemToolsDemographics = new ToolStripItem[] { this.MagicItemToolsDemographics, this.MagicItemToolsExport };
			this.MagicItemTools.DropDownItems.AddRange(magicItemToolsDemographics);
			this.MagicItemTools.Image = (Image)resources.GetObject("MagicItemTools.Image");
			this.MagicItemTools.ImageTransparentColor = Color.Magenta;
			this.MagicItemTools.Name = "MagicItemTools";
			this.MagicItemTools.Size = new System.Drawing.Size(49, 22);
			this.MagicItemTools.Text = "Tools";
			this.MagicItemToolsDemographics.Name = "MagicItemToolsDemographics";
			this.MagicItemToolsDemographics.Size = new System.Drawing.Size(151, 22);
			this.MagicItemToolsDemographics.Text = "Demographics";
			this.MagicItemToolsDemographics.Click += new EventHandler(this.MagicItemDemoBtn_Click);
			this.MagicItemToolsExport.Name = "MagicItemToolsExport";
			this.MagicItemToolsExport.Size = new System.Drawing.Size(151, 22);
			this.MagicItemToolsExport.Text = "Export...";
			this.MagicItemToolsExport.Click += new EventHandler(this.MagicItemsToolsExport_Click);
			this.MagicItemVersionList.Columns.AddRange(new ColumnHeader[] { this.MagicItemInfoHdr });
			this.MagicItemVersionList.Dock = DockStyle.Fill;
			this.MagicItemVersionList.FullRowSelect = true;
			listViewGroup7.Header = "Heroic Tier";
			listViewGroup7.Name = "listViewGroup1";
			listViewGroup8.Header = "Paragon Tier";
			listViewGroup8.Name = "listViewGroup2";
			listViewGroup9.Header = "Epic Tier";
			listViewGroup9.Name = "listViewGroup3";
			this.MagicItemVersionList.Groups.AddRange(new ListViewGroup[] { listViewGroup7, listViewGroup8, listViewGroup9 });
			this.MagicItemVersionList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.MagicItemVersionList.HideSelection = false;
			this.MagicItemVersionList.Location = new Point(0, 25);
			this.MagicItemVersionList.Name = "MagicItemVersionList";
			this.MagicItemVersionList.Size = new System.Drawing.Size(299, 215);
			this.MagicItemVersionList.TabIndex = 1;
			this.MagicItemVersionList.UseCompatibleStateImageBehavior = false;
			this.MagicItemVersionList.View = View.Details;
			this.MagicItemVersionList.DoubleClick += new EventHandler(this.MagicItemEditBtn_Click);
			this.MagicItemVersionList.ItemDrag += new ItemDragEventHandler(this.MagicItemList_ItemDrag);
			this.MagicItemInfoHdr.Text = "Version";
			this.MagicItemInfoHdr.Width = 250;
			ToolStripItem[] magicItemRemoveBtn = new ToolStripItem[] { this.MagicItemRemoveBtn, this.toolStripSeparator5, this.MagicItemEditBtn, this.MagicItemCutBtn, this.MagicItemCopyBtn, this.MagicItemPasteBtn, this.toolStripSeparator12, this.MagicItemStatBlockBtn };
			this.MagicItemVersionToolbar.Items.AddRange(magicItemRemoveBtn);
			this.MagicItemVersionToolbar.Location = new Point(0, 0);
			this.MagicItemVersionToolbar.Name = "MagicItemVersionToolbar";
			this.MagicItemVersionToolbar.Size = new System.Drawing.Size(299, 25);
			this.MagicItemVersionToolbar.TabIndex = 0;
			this.MagicItemVersionToolbar.Text = "toolStrip1";
			this.MagicItemRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MagicItemRemoveBtn.Image = (Image)resources.GetObject("MagicItemRemoveBtn.Image");
			this.MagicItemRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.MagicItemRemoveBtn.Name = "MagicItemRemoveBtn";
			this.MagicItemRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.MagicItemRemoveBtn.Text = "Remove";
			this.MagicItemRemoveBtn.Click += new EventHandler(this.MagicItemRemoveBtn_Click);
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			this.MagicItemEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MagicItemEditBtn.Image = (Image)resources.GetObject("MagicItemEditBtn.Image");
			this.MagicItemEditBtn.ImageTransparentColor = Color.Magenta;
			this.MagicItemEditBtn.Name = "MagicItemEditBtn";
			this.MagicItemEditBtn.Size = new System.Drawing.Size(31, 22);
			this.MagicItemEditBtn.Text = "Edit";
			this.MagicItemEditBtn.Click += new EventHandler(this.MagicItemEditBtn_Click);
			this.MagicItemCutBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MagicItemCutBtn.Image = (Image)resources.GetObject("MagicItemCutBtn.Image");
			this.MagicItemCutBtn.ImageTransparentColor = Color.Magenta;
			this.MagicItemCutBtn.Name = "MagicItemCutBtn";
			this.MagicItemCutBtn.Size = new System.Drawing.Size(30, 22);
			this.MagicItemCutBtn.Text = "Cut";
			this.MagicItemCutBtn.Click += new EventHandler(this.MagicItemCutBtn_Click);
			this.MagicItemCopyBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MagicItemCopyBtn.Image = (Image)resources.GetObject("MagicItemCopyBtn.Image");
			this.MagicItemCopyBtn.ImageTransparentColor = Color.Magenta;
			this.MagicItemCopyBtn.Name = "MagicItemCopyBtn";
			this.MagicItemCopyBtn.Size = new System.Drawing.Size(39, 22);
			this.MagicItemCopyBtn.Text = "Copy";
			this.MagicItemCopyBtn.Click += new EventHandler(this.MagicItemCopyBtn_Click);
			this.MagicItemPasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MagicItemPasteBtn.Image = (Image)resources.GetObject("MagicItemPasteBtn.Image");
			this.MagicItemPasteBtn.ImageTransparentColor = Color.Magenta;
			this.MagicItemPasteBtn.Name = "MagicItemPasteBtn";
			this.MagicItemPasteBtn.Size = new System.Drawing.Size(39, 22);
			this.MagicItemPasteBtn.Text = "Paste";
			this.MagicItemPasteBtn.Click += new EventHandler(this.MagicItemPasteBtn_Click);
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
			this.MagicItemStatBlockBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MagicItemStatBlockBtn.Image = (Image)resources.GetObject("MagicItemStatBlockBtn.Image");
			this.MagicItemStatBlockBtn.ImageTransparentColor = Color.Magenta;
			this.MagicItemStatBlockBtn.Name = "MagicItemStatBlockBtn";
			this.MagicItemStatBlockBtn.Size = new System.Drawing.Size(63, 22);
			this.MagicItemStatBlockBtn.Text = "Stat Block";
			this.MagicItemStatBlockBtn.Click += new EventHandler(this.MagicItemStatBlockBtn_Click);
			this.TilesPage.Controls.Add(this.TileList);
			this.TilesPage.Controls.Add(this.TileToolbar);
			this.TilesPage.Location = new Point(4, 22);
			this.TilesPage.Name = "TilesPage";
			this.TilesPage.Padding = new System.Windows.Forms.Padding(3);
			this.TilesPage.Size = new System.Drawing.Size(618, 246);
			this.TilesPage.TabIndex = 2;
			this.TilesPage.Text = "Map Tiles";
			this.TilesPage.UseVisualStyleBackColor = true;
			this.TileList.Columns.AddRange(new ColumnHeader[] { this.TileSetNameHdr });
			this.TileList.ContextMenuStrip = this.TileContext;
			this.TileList.Dock = DockStyle.Fill;
			this.TileList.FullRowSelect = true;
			this.TileList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.TileList.HideSelection = false;
			this.TileList.Location = new Point(3, 28);
			this.TileList.Name = "TileList";
			this.TileList.Size = new System.Drawing.Size(612, 215);
			this.TileList.Sorting = SortOrder.Ascending;
			this.TileList.TabIndex = 4;
			this.TileList.UseCompatibleStateImageBehavior = false;
			this.TileList.DoubleClick += new EventHandler(this.TileSetEditBtn_Click);
			this.TileList.ItemDrag += new ItemDragEventHandler(this.TileSetView_ItemDrag);
			this.TileSetNameHdr.Text = "Tile Set";
			this.TileSetNameHdr.Width = 299;
			ToolStripItem[] tileContextRemove = new ToolStripItem[] { this.TileContextRemove, this.TileContextCategory, this.TileContextSize };
			this.TileContext.Items.AddRange(tileContextRemove);
			this.TileContext.Name = "TileContext";
			this.TileContext.Size = new System.Drawing.Size(142, 70);
			this.TileContextRemove.Name = "TileContextRemove";
			this.TileContextRemove.Size = new System.Drawing.Size(141, 22);
			this.TileContextRemove.Text = "Remove";
			this.TileContextRemove.Click += new EventHandler(this.TileContextRemove_Click);
			ToolStripItem[] tilePlain = new ToolStripItem[] { this.TilePlain, this.TileDoorway, this.TileStairway, this.TileFeature, this.toolStripSeparator15, this.TileSpecial, this.toolStripSeparator16, this.TileMap };
			this.TileContextCategory.DropDownItems.AddRange(tilePlain);
			this.TileContextCategory.Name = "TileContextCategory";
			this.TileContextCategory.Size = new System.Drawing.Size(141, 22);
			this.TileContextCategory.Text = "Set Category";
			this.TilePlain.Name = "TilePlain";
			this.TilePlain.Size = new System.Drawing.Size(130, 22);
			this.TilePlain.Text = "Plain Floor";
			this.TilePlain.Click += new EventHandler(this.TilePlain_Click);
			this.TileDoorway.Name = "TileDoorway";
			this.TileDoorway.Size = new System.Drawing.Size(130, 22);
			this.TileDoorway.Text = "Doorway";
			this.TileDoorway.Click += new EventHandler(this.TileDoorway_Click);
			this.TileStairway.Name = "TileStairway";
			this.TileStairway.Size = new System.Drawing.Size(130, 22);
			this.TileStairway.Text = "Stairway";
			this.TileStairway.Click += new EventHandler(this.TileStairway_Click);
			this.TileFeature.Name = "TileFeature";
			this.TileFeature.Size = new System.Drawing.Size(130, 22);
			this.TileFeature.Text = "Feature";
			this.TileFeature.Click += new EventHandler(this.TileFeature_Click);
			this.toolStripSeparator15.Name = "toolStripSeparator15";
			this.toolStripSeparator15.Size = new System.Drawing.Size(127, 6);
			this.TileSpecial.Name = "TileSpecial";
			this.TileSpecial.Size = new System.Drawing.Size(130, 22);
			this.TileSpecial.Text = "Special";
			this.TileSpecial.Click += new EventHandler(this.TileSpecial_Click);
			this.toolStripSeparator16.Name = "toolStripSeparator16";
			this.toolStripSeparator16.Size = new System.Drawing.Size(127, 6);
			this.TileMap.Name = "TileMap";
			this.TileMap.Size = new System.Drawing.Size(130, 22);
			this.TileMap.Text = "Full Map";
			this.TileMap.Click += new EventHandler(this.TileMap_Click);
			this.TileContextSize.Name = "TileContextSize";
			this.TileContextSize.Size = new System.Drawing.Size(141, 22);
			this.TileContextSize.Text = "Set Size...";
			this.TileContextSize.Click += new EventHandler(this.TileContextSize_Click);
			ToolStripItem[] tileAddBtn = new ToolStripItem[] { this.TileAddBtn, this.TileRemoveBtn, this.TileEditBtn, this.toolStripSeparator3, this.TileCutBtn, this.TileCopyBtn, this.TilePasteBtn, this.toolStripSeparator23, this.TileTools };
			this.TileToolbar.Items.AddRange(tileAddBtn);
			this.TileToolbar.Location = new Point(3, 3);
			this.TileToolbar.Name = "TileToolbar";
			this.TileToolbar.Size = new System.Drawing.Size(612, 25);
			this.TileToolbar.TabIndex = 3;
			this.TileToolbar.Text = "toolStrip2";
			this.TileAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItem[] tileAddImport = new ToolStripItem[] { this.addTileToolStripMenuItem, this.toolStripSeparator24, this.TileAddImport, this.TileAddFolder };
			this.TileAddBtn.DropDownItems.AddRange(tileAddImport);
			this.TileAddBtn.Image = (Image)resources.GetObject("TileAddBtn.Image");
			this.TileAddBtn.ImageTransparentColor = Color.Magenta;
			this.TileAddBtn.Name = "TileAddBtn";
			this.TileAddBtn.Size = new System.Drawing.Size(42, 22);
			this.TileAddBtn.Text = "Add";
			this.addTileToolStripMenuItem.Name = "addTileToolStripMenuItem";
			this.addTileToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.addTileToolStripMenuItem.Text = "Add a Tile...";
			this.addTileToolStripMenuItem.Click += new EventHandler(this.TileAddBtn_Click);
			this.toolStripSeparator24.Name = "toolStripSeparator24";
			this.toolStripSeparator24.Size = new System.Drawing.Size(161, 6);
			this.TileAddImport.Name = "TileAddImport";
			this.TileAddImport.Size = new System.Drawing.Size(164, 22);
			this.TileAddImport.Text = "Import...";
			this.TileAddImport.Click += new EventHandler(this.TileAddImport_Click);
			this.TileAddFolder.Name = "TileAddFolder";
			this.TileAddFolder.Size = new System.Drawing.Size(164, 22);
			this.TileAddFolder.Text = "Import a Folder...";
			this.TileAddFolder.Click += new EventHandler(this.TileAddFolderBtn_Click);
			this.TileRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TileRemoveBtn.Image = (Image)resources.GetObject("TileRemoveBtn.Image");
			this.TileRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.TileRemoveBtn.Name = "TileRemoveBtn";
			this.TileRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.TileRemoveBtn.Text = "Remove";
			this.TileRemoveBtn.Click += new EventHandler(this.TileSetRemoveBtn_Click);
			this.TileEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TileEditBtn.Image = (Image)resources.GetObject("TileEditBtn.Image");
			this.TileEditBtn.ImageTransparentColor = Color.Magenta;
			this.TileEditBtn.Name = "TileEditBtn";
			this.TileEditBtn.Size = new System.Drawing.Size(31, 22);
			this.TileEditBtn.Text = "Edit";
			this.TileEditBtn.Click += new EventHandler(this.TileSetEditBtn_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			this.TileCutBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TileCutBtn.Image = (Image)resources.GetObject("TileCutBtn.Image");
			this.TileCutBtn.ImageTransparentColor = Color.Magenta;
			this.TileCutBtn.Name = "TileCutBtn";
			this.TileCutBtn.Size = new System.Drawing.Size(30, 22);
			this.TileCutBtn.Text = "Cut";
			this.TileCutBtn.Click += new EventHandler(this.TileCutBtn_Click);
			this.TileCopyBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TileCopyBtn.Image = (Image)resources.GetObject("TileCopyBtn.Image");
			this.TileCopyBtn.ImageTransparentColor = Color.Magenta;
			this.TileCopyBtn.Name = "TileCopyBtn";
			this.TileCopyBtn.Size = new System.Drawing.Size(39, 22);
			this.TileCopyBtn.Text = "Copy";
			this.TileCopyBtn.Click += new EventHandler(this.TileCopyBtn_Click);
			this.TilePasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TilePasteBtn.Image = (Image)resources.GetObject("TilePasteBtn.Image");
			this.TilePasteBtn.ImageTransparentColor = Color.Magenta;
			this.TilePasteBtn.Name = "TilePasteBtn";
			this.TilePasteBtn.Size = new System.Drawing.Size(39, 22);
			this.TilePasteBtn.Text = "Paste";
			this.TilePasteBtn.Click += new EventHandler(this.TilePasteBtn_Click);
			this.toolStripSeparator23.Name = "toolStripSeparator23";
			this.toolStripSeparator23.Size = new System.Drawing.Size(6, 25);
			this.TileTools.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TileTools.DropDownItems.AddRange(new ToolStripItem[] { this.TileToolsExport });
			this.TileTools.Image = (Image)resources.GetObject("TileTools.Image");
			this.TileTools.ImageTransparentColor = Color.Magenta;
			this.TileTools.Name = "TileTools";
			this.TileTools.Size = new System.Drawing.Size(49, 22);
			this.TileTools.Text = "Tools";
			this.TileToolsExport.Name = "TileToolsExport";
			this.TileToolsExport.Size = new System.Drawing.Size(116, 22);
			this.TileToolsExport.Text = "Export...";
			this.TileToolsExport.Click += new EventHandler(this.TileToolsExport_Click);
			this.TerrainPowersPage.Controls.Add(this.TerrainPowerList);
			this.TerrainPowersPage.Controls.Add(this.TerrainPowerToolbar);
			this.TerrainPowersPage.Location = new Point(4, 22);
			this.TerrainPowersPage.Name = "TerrainPowersPage";
			this.TerrainPowersPage.Padding = new System.Windows.Forms.Padding(3);
			this.TerrainPowersPage.Size = new System.Drawing.Size(618, 246);
			this.TerrainPowersPage.TabIndex = 7;
			this.TerrainPowersPage.Text = "Terrain Powers";
			this.TerrainPowersPage.UseVisualStyleBackColor = true;
			ColumnHeader[] tPNameHdr = new ColumnHeader[] { this.TPNameHdr, this.TPInfoHdr };
			this.TerrainPowerList.Columns.AddRange(tPNameHdr);
			this.TerrainPowerList.ContextMenuStrip = this.TPContext;
			this.TerrainPowerList.Dock = DockStyle.Fill;
			this.TerrainPowerList.FullRowSelect = true;
			listViewGroup10.Header = "Traps";
			listViewGroup10.Name = "TrapGroup";
			listViewGroup11.Header = "Hazards";
			listViewGroup11.Name = "HazardGroup";
			this.TerrainPowerList.Groups.AddRange(new ListViewGroup[] { listViewGroup10, listViewGroup11 });
			this.TerrainPowerList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.TerrainPowerList.HideSelection = false;
			this.TerrainPowerList.Location = new Point(3, 28);
			this.TerrainPowerList.Name = "TerrainPowerList";
			this.TerrainPowerList.Size = new System.Drawing.Size(612, 215);
			this.TerrainPowerList.Sorting = SortOrder.Ascending;
			this.TerrainPowerList.TabIndex = 6;
			this.TerrainPowerList.UseCompatibleStateImageBehavior = false;
			this.TerrainPowerList.View = View.Details;
			this.TerrainPowerList.DoubleClick += new EventHandler(this.TPEditBtn_Click);
			this.TerrainPowerList.ItemDrag += new ItemDragEventHandler(this.TPList_ItemDrag);
			this.TPNameHdr.Text = "Terrain Power";
			this.TPNameHdr.Width = 300;
			this.TPInfoHdr.Text = "Info";
			this.TPInfoHdr.Width = 150;
			this.TPContext.Items.AddRange(new ToolStripItem[] { this.TPContextRemove });
			this.TPContext.Name = "ChallengeContext";
			this.TPContext.Size = new System.Drawing.Size(118, 26);
			this.TPContextRemove.Name = "TPContextRemove";
			this.TPContextRemove.Size = new System.Drawing.Size(117, 22);
			this.TPContextRemove.Text = "Remove";
			this.TPContextRemove.Click += new EventHandler(this.TPContextRemove_Click);
			ToolStripItem[] tPAdd = new ToolStripItem[] { this.TPAdd, this.TPRemoveBtn, this.TPEditBtn, this.toolStripSeparator29, this.TPCutBtn, this.TPCopyBtn, this.TPPasteBtn, this.toolStripSeparator30, this.TPStatBlockBtn, this.toolStripSeparator35, this.TPTools };
			this.TerrainPowerToolbar.Items.AddRange(tPAdd);
			this.TerrainPowerToolbar.Location = new Point(3, 3);
			this.TerrainPowerToolbar.Name = "TerrainPowerToolbar";
			this.TerrainPowerToolbar.Size = new System.Drawing.Size(612, 25);
			this.TerrainPowerToolbar.TabIndex = 5;
			this.TerrainPowerToolbar.Text = "toolStrip2";
			this.TPAdd.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItem[] tPAddTerrainPower = new ToolStripItem[] { this.TPAddTerrainPower, this.toolStripSeparator28, this.TPAddImport };
			this.TPAdd.DropDownItems.AddRange(tPAddTerrainPower);
			this.TPAdd.Image = (Image)resources.GetObject("TPAdd.Image");
			this.TPAdd.ImageTransparentColor = Color.Magenta;
			this.TPAdd.Name = "TPAdd";
			this.TPAdd.Size = new System.Drawing.Size(42, 22);
			this.TPAdd.Text = "Add";
			this.TPAddTerrainPower.Name = "TPAddTerrainPower";
			this.TPAddTerrainPower.Size = new System.Drawing.Size(190, 22);
			this.TPAddTerrainPower.Text = "Add a Terrain Power...";
			this.TPAddTerrainPower.Click += new EventHandler(this.TPAddBtn_Click);
			this.toolStripSeparator28.Name = "toolStripSeparator28";
			this.toolStripSeparator28.Size = new System.Drawing.Size(187, 6);
			this.TPAddImport.Name = "TPAddImport";
			this.TPAddImport.Size = new System.Drawing.Size(190, 22);
			this.TPAddImport.Text = "Import...";
			this.TPAddImport.Click += new EventHandler(this.TPAddImport_Click);
			this.TPRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TPRemoveBtn.Image = (Image)resources.GetObject("TPRemoveBtn.Image");
			this.TPRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.TPRemoveBtn.Name = "TPRemoveBtn";
			this.TPRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.TPRemoveBtn.Text = "Remove";
			this.TPRemoveBtn.Click += new EventHandler(this.TPRemoveBtn_Click);
			this.TPEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TPEditBtn.Image = (Image)resources.GetObject("TPEditBtn.Image");
			this.TPEditBtn.ImageTransparentColor = Color.Magenta;
			this.TPEditBtn.Name = "TPEditBtn";
			this.TPEditBtn.Size = new System.Drawing.Size(31, 22);
			this.TPEditBtn.Text = "Edit";
			this.TPEditBtn.Click += new EventHandler(this.TPEditBtn_Click);
			this.toolStripSeparator29.Name = "toolStripSeparator29";
			this.toolStripSeparator29.Size = new System.Drawing.Size(6, 25);
			this.TPCutBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TPCutBtn.Image = (Image)resources.GetObject("TPCutBtn.Image");
			this.TPCutBtn.ImageTransparentColor = Color.Magenta;
			this.TPCutBtn.Name = "TPCutBtn";
			this.TPCutBtn.Size = new System.Drawing.Size(30, 22);
			this.TPCutBtn.Text = "Cut";
			this.TPCutBtn.Click += new EventHandler(this.TPCutBtn_Click);
			this.TPCopyBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TPCopyBtn.Image = (Image)resources.GetObject("TPCopyBtn.Image");
			this.TPCopyBtn.ImageTransparentColor = Color.Magenta;
			this.TPCopyBtn.Name = "TPCopyBtn";
			this.TPCopyBtn.Size = new System.Drawing.Size(39, 22);
			this.TPCopyBtn.Text = "Copy";
			this.TPCopyBtn.Click += new EventHandler(this.TPCopyBtn_Click);
			this.TPPasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TPPasteBtn.Image = (Image)resources.GetObject("TPPasteBtn.Image");
			this.TPPasteBtn.ImageTransparentColor = Color.Magenta;
			this.TPPasteBtn.Name = "TPPasteBtn";
			this.TPPasteBtn.Size = new System.Drawing.Size(39, 22);
			this.TPPasteBtn.Text = "Paste";
			this.TPPasteBtn.Click += new EventHandler(this.TPPasteBtn_Click);
			this.toolStripSeparator30.Name = "toolStripSeparator30";
			this.toolStripSeparator30.Size = new System.Drawing.Size(6, 25);
			this.TPStatBlockBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TPStatBlockBtn.Image = (Image)resources.GetObject("TPStatBlockBtn.Image");
			this.TPStatBlockBtn.ImageTransparentColor = Color.Magenta;
			this.TPStatBlockBtn.Name = "TPStatBlockBtn";
			this.TPStatBlockBtn.Size = new System.Drawing.Size(63, 22);
			this.TPStatBlockBtn.Text = "Stat Block";
			this.TPStatBlockBtn.Click += new EventHandler(this.TPStatBlockBtn_Click);
			this.toolStripSeparator35.Name = "toolStripSeparator35";
			this.toolStripSeparator35.Size = new System.Drawing.Size(6, 25);
			this.TPTools.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItem[] fileMenu1 = new ToolStripItem[] { this.TPToolsExport };
			this.TPTools.DropDownItems.AddRange(fileMenu1);
			this.TPTools.Image = (Image)resources.GetObject("TPTools.Image");
			this.TPTools.ImageTransparentColor = Color.Magenta;
			this.TPTools.Name = "TPTools";
			this.TPTools.Size = new System.Drawing.Size(49, 22);
			this.TPTools.Text = "Tools";
			this.TPToolsExport.Name = "TPToolsExport";
			this.TPToolsExport.Size = new System.Drawing.Size(116, 22);
			this.TPToolsExport.Text = "Export...";
			this.TPToolsExport.Click += new EventHandler(this.TPToolsExport_Click);
			this.ArtifactPage.Controls.Add(this.ArtifactList);
			this.ArtifactPage.Controls.Add(this.ArtifactToolbar);
			this.ArtifactPage.Location = new Point(4, 22);
			this.ArtifactPage.Name = "ArtifactPage";
			this.ArtifactPage.Padding = new System.Windows.Forms.Padding(3);
			this.ArtifactPage.Size = new System.Drawing.Size(618, 246);
			this.ArtifactPage.TabIndex = 8;
			this.ArtifactPage.Text = "Artifacts";
			this.ArtifactPage.UseVisualStyleBackColor = true;
			ColumnHeader[] artifactHdr = new ColumnHeader[] { this.ArtifactHdr, this.ArtifactInfoHdr };
			this.ArtifactList.Columns.AddRange(artifactHdr);
			this.ArtifactList.ContextMenuStrip = this.ArtifactContext;
			this.ArtifactList.Dock = DockStyle.Fill;
			this.ArtifactList.FullRowSelect = true;
			listViewGroup12.Header = "Traps";
			listViewGroup12.Name = "TrapGroup";
			listViewGroup13.Header = "Hazards";
			listViewGroup13.Name = "HazardGroup";
			ListViewGroup[] listViewGroupArray1 = new ListViewGroup[] { listViewGroup12, listViewGroup13 };
			this.ArtifactList.Groups.AddRange(listViewGroupArray1);
			this.ArtifactList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.ArtifactList.HideSelection = false;
			this.ArtifactList.Location = new Point(3, 28);
			this.ArtifactList.Name = "ArtifactList";
			this.ArtifactList.Size = new System.Drawing.Size(612, 215);
			this.ArtifactList.Sorting = SortOrder.Ascending;
			this.ArtifactList.TabIndex = 6;
			this.ArtifactList.UseCompatibleStateImageBehavior = false;
			this.ArtifactList.View = View.Details;
			this.ArtifactList.DoubleClick += new EventHandler(this.ArtifactEdit_Click);
			this.ArtifactList.ItemDrag += new ItemDragEventHandler(this.ArtifactList_ItemDrag);
			this.ArtifactHdr.Text = "Artifact";
			this.ArtifactHdr.Width = 300;
			this.ArtifactInfoHdr.Text = "Info";
			this.ArtifactInfoHdr.Width = 150;
			ToolStripItem[] fileMenu2 = new ToolStripItem[] { this.ArtifactContextRemove };
			this.ArtifactContext.Items.AddRange(fileMenu2);
			this.ArtifactContext.Name = "ChallengeContext";
			this.ArtifactContext.Size = new System.Drawing.Size(118, 26);
			this.ArtifactContextRemove.Name = "ArtifactContextRemove";
			this.ArtifactContextRemove.Size = new System.Drawing.Size(117, 22);
			this.ArtifactContextRemove.Text = "Remove";
			this.ArtifactContextRemove.Click += new EventHandler(this.ArtifactRemove_Click);
			ToolStripItem[] fileMenu3 = new ToolStripItem[] { this.ArtifactAdd, this.ArtifactRemove, this.ArtifactEdit, this.toolStripSeparator32, this.ArtifactCut, this.ArtifactCopy, this.ArtifactPaste, this.toolStripSeparator33, this.ArtifactStatBlockBtn, this.toolStripSeparator34, this.ArtifactTools };
			this.ArtifactToolbar.Items.AddRange(fileMenu3);
			this.ArtifactToolbar.Location = new Point(3, 3);
			this.ArtifactToolbar.Name = "ArtifactToolbar";
			this.ArtifactToolbar.Size = new System.Drawing.Size(612, 25);
			this.ArtifactToolbar.TabIndex = 5;
			this.ArtifactToolbar.Text = "toolStrip2";
			this.ArtifactAdd.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItem[] fileMenu4 = new ToolStripItem[] { this.ArtifactAddAdd, this.toolStripSeparator31, this.ArtifactAddImport };
			this.ArtifactAdd.DropDownItems.AddRange(fileMenu4);
			this.ArtifactAdd.Image = (Image)resources.GetObject("ArtifactAdd.Image");
			this.ArtifactAdd.ImageTransparentColor = Color.Magenta;
			this.ArtifactAdd.Name = "ArtifactAdd";
			this.ArtifactAdd.Size = new System.Drawing.Size(42, 22);
			this.ArtifactAdd.Text = "Add";
			this.ArtifactAddAdd.Name = "ArtifactAddAdd";
			this.ArtifactAddAdd.Size = new System.Drawing.Size(163, 22);
			this.ArtifactAddAdd.Text = "Add an Artifact...";
			this.ArtifactAddAdd.Click += new EventHandler(this.ArtifactAddAdd_Click);
			this.toolStripSeparator31.Name = "toolStripSeparator31";
			this.toolStripSeparator31.Size = new System.Drawing.Size(160, 6);
			this.ArtifactAddImport.Name = "ArtifactAddImport";
			this.ArtifactAddImport.Size = new System.Drawing.Size(163, 22);
			this.ArtifactAddImport.Text = "Import...";
			this.ArtifactAddImport.Click += new EventHandler(this.ArtifactAddImport_Click);
			this.ArtifactRemove.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ArtifactRemove.Image = (Image)resources.GetObject("ArtifactRemove.Image");
			this.ArtifactRemove.ImageTransparentColor = Color.Magenta;
			this.ArtifactRemove.Name = "ArtifactRemove";
			this.ArtifactRemove.Size = new System.Drawing.Size(54, 22);
			this.ArtifactRemove.Text = "Remove";
			this.ArtifactRemove.Click += new EventHandler(this.ArtifactRemove_Click);
			this.ArtifactEdit.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ArtifactEdit.Image = (Image)resources.GetObject("ArtifactEdit.Image");
			this.ArtifactEdit.ImageTransparentColor = Color.Magenta;
			this.ArtifactEdit.Name = "ArtifactEdit";
			this.ArtifactEdit.Size = new System.Drawing.Size(31, 22);
			this.ArtifactEdit.Text = "Edit";
			this.ArtifactEdit.Click += new EventHandler(this.ArtifactEdit_Click);
			this.toolStripSeparator32.Name = "toolStripSeparator32";
			this.toolStripSeparator32.Size = new System.Drawing.Size(6, 25);
			this.ArtifactCut.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ArtifactCut.Image = (Image)resources.GetObject("ArtifactCut.Image");
			this.ArtifactCut.ImageTransparentColor = Color.Magenta;
			this.ArtifactCut.Name = "ArtifactCut";
			this.ArtifactCut.Size = new System.Drawing.Size(30, 22);
			this.ArtifactCut.Text = "Cut";
			this.ArtifactCut.Click += new EventHandler(this.ArtifactCut_Click);
			this.ArtifactCopy.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ArtifactCopy.Image = (Image)resources.GetObject("ArtifactCopy.Image");
			this.ArtifactCopy.ImageTransparentColor = Color.Magenta;
			this.ArtifactCopy.Name = "ArtifactCopy";
			this.ArtifactCopy.Size = new System.Drawing.Size(39, 22);
			this.ArtifactCopy.Text = "Copy";
			this.ArtifactCopy.Click += new EventHandler(this.ArtifactCopy_Click);
			this.ArtifactPaste.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ArtifactPaste.Image = (Image)resources.GetObject("ArtifactPaste.Image");
			this.ArtifactPaste.ImageTransparentColor = Color.Magenta;
			this.ArtifactPaste.Name = "ArtifactPaste";
			this.ArtifactPaste.Size = new System.Drawing.Size(39, 22);
			this.ArtifactPaste.Text = "Paste";
			this.ArtifactPaste.Click += new EventHandler(this.ArtifactPaste_Click);
			this.toolStripSeparator33.Name = "toolStripSeparator33";
			this.toolStripSeparator33.Size = new System.Drawing.Size(6, 25);
			this.ArtifactStatBlockBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ArtifactStatBlockBtn.Image = (Image)resources.GetObject("ArtifactStatBlockBtn.Image");
			this.ArtifactStatBlockBtn.ImageTransparentColor = Color.Magenta;
			this.ArtifactStatBlockBtn.Name = "ArtifactStatBlockBtn";
			this.ArtifactStatBlockBtn.Size = new System.Drawing.Size(63, 22);
			this.ArtifactStatBlockBtn.Text = "Stat Block";
			this.ArtifactStatBlockBtn.Click += new EventHandler(this.ArtifactStatBlockBtn_Click);
			this.toolStripSeparator34.Name = "toolStripSeparator34";
			this.toolStripSeparator34.Size = new System.Drawing.Size(6, 25);
			this.ArtifactTools.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItem[] fileMenu5 = new ToolStripItem[] { this.ArtifactToolsExport };
			this.ArtifactTools.DropDownItems.AddRange(fileMenu5);
			this.ArtifactTools.Image = (Image)resources.GetObject("ArtifactTools.Image");
			this.ArtifactTools.ImageTransparentColor = Color.Magenta;
			this.ArtifactTools.Name = "ArtifactTools";
			this.ArtifactTools.Size = new System.Drawing.Size(49, 22);
			this.ArtifactTools.Text = "Tools";
			this.ArtifactToolsExport.Name = "ArtifactToolsExport";
			this.ArtifactToolsExport.Size = new System.Drawing.Size(116, 22);
			this.ArtifactToolsExport.Text = "Export...";
			this.ArtifactToolsExport.Click += new EventHandler(this.ArtifactToolsExport_Click);
			this.HelpPanel.BorderStyle = BorderStyle.FixedSingle;
			this.HelpPanel.Dock = DockStyle.Bottom;
			this.HelpPanel.Location = new Point(0, 272);
			this.HelpPanel.Name = "HelpPanel";
			this.HelpPanel.Size = new System.Drawing.Size(626, 159);
			this.HelpPanel.TabIndex = 3;
			this.HelpPanel.Visible = false;
			ToolStripItem[] fileMenu6 = new ToolStripItem[] { this.ChallengeContextRemove };
			this.ChallengeContext.Items.AddRange(fileMenu6);
			this.ChallengeContext.Name = "ChallengeContext";
			this.ChallengeContext.Size = new System.Drawing.Size(118, 26);
			this.ChallengeContextRemove.Name = "ChallengeContextRemove";
			this.ChallengeContextRemove.Size = new System.Drawing.Size(117, 22);
			this.ChallengeContextRemove.Text = "Remove";
			this.ChallengeContextRemove.Click += new EventHandler(this.ChallengeContextRemove_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(879, 431);
			base.Controls.Add(this.Splitter);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MinimizeBox = false;
			base.Name = "LibraryListForm";
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Libraries";
			base.FormClosed += new FormClosedEventHandler(this.LibrariesForm_FormClosed);
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel1.PerformLayout();
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			this.LibraryToolbar.ResumeLayout(false);
			this.LibraryToolbar.PerformLayout();
			this.Pages.ResumeLayout(false);
			this.CreaturesPage.ResumeLayout(false);
			this.CreaturesPage.PerformLayout();
			this.CreatureContext.ResumeLayout(false);
			this.CreatureSearchToolbar.ResumeLayout(false);
			this.CreatureSearchToolbar.PerformLayout();
			this.CreatureToolbar.ResumeLayout(false);
			this.CreatureToolbar.PerformLayout();
			this.TemplatesPage.ResumeLayout(false);
			this.TemplatesPage.PerformLayout();
			this.TemplateContext.ResumeLayout(false);
			this.TemplateToolbar.ResumeLayout(false);
			this.TemplateToolbar.PerformLayout();
			this.TrapsPage.ResumeLayout(false);
			this.TrapsPage.PerformLayout();
			this.TrapContext.ResumeLayout(false);
			this.TrapToolbar.ResumeLayout(false);
			this.TrapToolbar.PerformLayout();
			this.ChallengePage.ResumeLayout(false);
			this.ChallengePage.PerformLayout();
			this.ChallengeToolbar.ResumeLayout(false);
			this.ChallengeToolbar.PerformLayout();
			this.MagicItemsPage.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			this.splitContainer1.ResumeLayout(false);
			this.MagicItemContext.ResumeLayout(false);
			this.MagicItemToolbar.ResumeLayout(false);
			this.MagicItemToolbar.PerformLayout();
			this.MagicItemVersionToolbar.ResumeLayout(false);
			this.MagicItemVersionToolbar.PerformLayout();
			this.TilesPage.ResumeLayout(false);
			this.TilesPage.PerformLayout();
			this.TileContext.ResumeLayout(false);
			this.TileToolbar.ResumeLayout(false);
			this.TileToolbar.PerformLayout();
			this.TerrainPowersPage.ResumeLayout(false);
			this.TerrainPowersPage.PerformLayout();
			this.TPContext.ResumeLayout(false);
			this.TerrainPowerToolbar.ResumeLayout(false);
			this.TerrainPowerToolbar.PerformLayout();
			this.ArtifactPage.ResumeLayout(false);
			this.ArtifactPage.PerformLayout();
			this.ArtifactContext.ResumeLayout(false);
			this.ArtifactToolbar.ResumeLayout(false);
			this.ArtifactToolbar.PerformLayout();
			this.ChallengeContext.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void LibrariesForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			foreach (Library library in Session.Libraries)
			{
				if (this.fModified.ContainsKey(library) && !this.fModified[library])
				{
					continue;
				}
				this.save(library);
			}
		}

		private void LibraryEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary != null)
			{
				int library = Session.Libraries.IndexOf(this.SelectedLibrary);
				string libraryFilename = Session.GetLibraryFilename(this.SelectedLibrary);
				if (!File.Exists(libraryFilename))
				{
					MessageBox.Show("This library cannot be renamed.", "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
				LibraryForm libraryForm = new LibraryForm(this.SelectedLibrary);
				if (libraryForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Libraries[library] = libraryForm.Library;
					Session.Libraries.Sort();
					string str = Session.GetLibraryFilename(libraryForm.Library);
					if (libraryFilename != str)
					{
						(new FileInfo(libraryFilename)).MoveTo(str);
					}
					this.fModified[libraryForm.Library] = true;
					this.update_libraries();
					this.update_content(true);
				}
			}
		}

		private void LibraryList_DragDrop(object sender, DragEventArgs e)
		{
			Point client = this.LibraryTree.PointToClient(System.Windows.Forms.Cursor.Position);
			TreeNode nodeAt = this.LibraryTree.GetNodeAt(client);
			this.LibraryTree.SelectedNode = nodeAt;
			if (this.SelectedLibrary == null)
			{
				return;
			}
			Library data = e.Data.GetData(typeof(Library)) as Library;
			if (data != null)
			{
				this.SelectedLibrary.Import(data);
				this.fModified[this.SelectedLibrary] = true;
				Session.DeleteLibrary(data);
				this.update_content(true);
			}
			List<Creature> creatures = e.Data.GetData(typeof(List<Creature>)) as List<Creature>;
			if (creatures != null)
			{
				foreach (Creature datum in creatures)
				{
					foreach (Library library in Session.Libraries)
					{
						if (!library.Creatures.Contains(datum))
						{
							continue;
						}
						library.Creatures.Remove(datum);
						this.fModified[library] = true;
					}
					this.SelectedLibrary.Creatures.Add(datum);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_creatures();
			}
			List<CreatureTemplate> creatureTemplates = e.Data.GetData(typeof(List<CreatureTemplate>)) as List<CreatureTemplate>;
			if (creatureTemplates != null)
			{
				foreach (CreatureTemplate creatureTemplate in creatureTemplates)
				{
					foreach (Library library1 in Session.Libraries)
					{
						if (!library1.Templates.Contains(creatureTemplate))
						{
							continue;
						}
						library1.Templates.Remove(creatureTemplate);
						this.fModified[library1] = true;
					}
					this.SelectedLibrary.Templates.Add(creatureTemplate);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_templates();
			}
			List<Trap> traps = e.Data.GetData(typeof(List<Trap>)) as List<Trap>;
			if (traps != null)
			{
				foreach (Trap trap in traps)
				{
					foreach (Library library2 in Session.Libraries)
					{
						if (!library2.Traps.Contains(trap))
						{
							continue;
						}
						library2.Traps.Remove(trap);
						this.fModified[library2] = true;
					}
					this.SelectedLibrary.Traps.Add(trap);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_traps();
			}
			List<SkillChallenge> skillChallenges = e.Data.GetData(typeof(List<SkillChallenge>)) as List<SkillChallenge>;
			if (skillChallenges != null)
			{
				foreach (SkillChallenge skillChallenge in skillChallenges)
				{
					foreach (Library library3 in Session.Libraries)
					{
						if (!library3.SkillChallenges.Contains(skillChallenge))
						{
							continue;
						}
						library3.SkillChallenges.Remove(skillChallenge);
						this.fModified[library3] = true;
					}
					this.SelectedLibrary.SkillChallenges.Add(skillChallenge);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_challenges();
			}
			List<MagicItem> magicItems = e.Data.GetData(typeof(List<MagicItem>)) as List<MagicItem>;
			if (magicItems != null)
			{
				foreach (MagicItem magicItem in magicItems)
				{
					foreach (Library library4 in Session.Libraries)
					{
						if (!library4.MagicItems.Contains(magicItem))
						{
							continue;
						}
						library4.MagicItems.Remove(magicItem);
						this.fModified[library4] = true;
					}
					this.SelectedLibrary.MagicItems.Add(magicItem);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_magic_item_sets();
			}
			List<Tile> tiles = e.Data.GetData(typeof(List<Tile>)) as List<Tile>;
			if (tiles != null)
			{
				foreach (Tile tile in tiles)
				{
					foreach (Library library5 in Session.Libraries)
					{
						if (!library5.Tiles.Contains(tile))
						{
							continue;
						}
						library5.Tiles.Remove(tile);
						this.fModified[library5] = true;
					}
					this.SelectedLibrary.Tiles.Add(tile);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_tiles();
			}
			List<TerrainPower> terrainPowers = e.Data.GetData(typeof(List<TerrainPower>)) as List<TerrainPower>;
			if (terrainPowers != null)
			{
				foreach (TerrainPower terrainPower in terrainPowers)
				{
					foreach (Library library6 in Session.Libraries)
					{
						if (!library6.TerrainPowers.Contains(terrainPower))
						{
							continue;
						}
						library6.TerrainPowers.Remove(terrainPower);
						this.fModified[library6] = true;
					}
					this.SelectedLibrary.TerrainPowers.Add(terrainPower);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_terrain_powers();
			}
			List<Artifact> artifacts = e.Data.GetData(typeof(List<Artifact>)) as List<Artifact>;
			if (artifacts != null)
			{
				foreach (Artifact artifact in artifacts)
				{
					foreach (Library library7 in Session.Libraries)
					{
						if (!library7.Artifacts.Contains(artifact))
						{
							continue;
						}
						library7.Artifacts.Remove(artifact);
						this.fModified[library7] = true;
					}
					this.SelectedLibrary.Artifacts.Add(artifact);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_artifacts();
			}
		}

		private void LibraryList_DragOver(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.None;
			Point client = this.LibraryTree.PointToClient(System.Windows.Forms.Cursor.Position);
			TreeNode nodeAt = this.LibraryTree.GetNodeAt(client);
			this.LibraryTree.SelectedNode = nodeAt;
			if (this.SelectedLibrary == null)
			{
				return;
			}
			if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
			{
				return;
			}
			Library data = e.Data.GetData(typeof(Library)) as Library;
			if (data != null)
			{
				if (data != this.SelectedLibrary)
				{
					e.Effect = DragDropEffects.Move;
				}
				return;
			}
			List<Creature> creatures = e.Data.GetData(typeof(List<Creature>)) as List<Creature>;
			if (creatures != null)
			{
				bool flag = false;
				foreach (Creature datum in creatures)
				{
					if (this.SelectedLibrary.Creatures.Contains(datum))
					{
						continue;
					}
					flag = true;
					break;
				}
				if (flag)
				{
					e.Effect = DragDropEffects.Move;
				}
				return;
			}
			List<CreatureTemplate> creatureTemplates = e.Data.GetData(typeof(List<CreatureTemplate>)) as List<CreatureTemplate>;
			if (creatureTemplates != null)
			{
				bool flag1 = false;
				foreach (CreatureTemplate creatureTemplate in creatureTemplates)
				{
					if (this.SelectedLibrary.Templates.Contains(creatureTemplate))
					{
						continue;
					}
					flag1 = true;
					break;
				}
				if (flag1)
				{
					e.Effect = DragDropEffects.Move;
				}
				return;
			}
			List<Trap> traps = e.Data.GetData(typeof(List<Trap>)) as List<Trap>;
			if (traps != null)
			{
				bool flag2 = false;
				foreach (Trap trap in traps)
				{
					if (this.SelectedLibrary.Traps.Contains(trap))
					{
						continue;
					}
					flag2 = true;
					break;
				}
				if (flag2)
				{
					e.Effect = DragDropEffects.Move;
				}
				return;
			}
			List<SkillChallenge> skillChallenges = e.Data.GetData(typeof(List<SkillChallenge>)) as List<SkillChallenge>;
			if (skillChallenges != null)
			{
				bool flag3 = false;
				foreach (SkillChallenge skillChallenge in skillChallenges)
				{
					if (this.SelectedLibrary.SkillChallenges.Contains(skillChallenge))
					{
						continue;
					}
					flag3 = true;
					break;
				}
				if (flag3)
				{
					e.Effect = DragDropEffects.Move;
				}
				return;
			}
			List<MagicItem> magicItems = e.Data.GetData(typeof(List<MagicItem>)) as List<MagicItem>;
			if (skillChallenges != null)
			{
				bool flag4 = false;
				foreach (MagicItem magicItem in magicItems)
				{
					if (this.SelectedLibrary.MagicItems.Contains(magicItem))
					{
						continue;
					}
					flag4 = true;
					break;
				}
				if (flag4)
				{
					e.Effect = DragDropEffects.Move;
				}
				return;
			}
			List<Tile> tiles = e.Data.GetData(typeof(List<Tile>)) as List<Tile>;
			if (tiles != null)
			{
				bool flag5 = false;
				foreach (Tile tile in tiles)
				{
					if (this.SelectedLibrary.Tiles.Contains(tile))
					{
						continue;
					}
					flag5 = true;
					break;
				}
				if (flag5)
				{
					e.Effect = DragDropEffects.Move;
				}
				return;
			}
			List<TerrainPower> terrainPowers = e.Data.GetData(typeof(List<TerrainPower>)) as List<TerrainPower>;
			if (terrainPowers != null)
			{
				bool flag6 = false;
				foreach (TerrainPower terrainPower in terrainPowers)
				{
					if (this.SelectedLibrary.TerrainPowers.Contains(terrainPower))
					{
						continue;
					}
					flag6 = true;
					break;
				}
				if (flag6)
				{
					e.Effect = DragDropEffects.Move;
				}
				return;
			}
			List<Artifact> artifacts = e.Data.GetData(typeof(List<Artifact>)) as List<Artifact>;
			if (artifacts != null)
			{
				bool flag7 = false;
				foreach (Artifact artifact in artifacts)
				{
					if (this.SelectedLibrary.Artifacts.Contains(artifact))
					{
						continue;
					}
					flag7 = true;
					break;
				}
				if (flag7)
				{
					e.Effect = DragDropEffects.Move;
				}
			}
		}

		private void LibraryList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			Library selectedLibrary = this.SelectedLibrary;
			if (selectedLibrary == null)
			{
				return;
			}
			if (Session.Project != null && Session.Project.Library == selectedLibrary)
			{
				return;
			}
			base.DoDragDrop(selectedLibrary, DragDropEffects.Move);
		}

		private void LibraryMergeBtn_Click(object sender, EventArgs e)
		{
			MergeLibrariesForm mergeLibrariesForm = new MergeLibrariesForm();
			if (mergeLibrariesForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Library library = new Library()
				{
					Name = mergeLibrariesForm.LibraryName,
					SecurityData = Program.SecurityData
				};
				foreach (Library selectedLibrary in mergeLibrariesForm.SelectedLibraries)
				{
					library.Import(selectedLibrary);
					Session.DeleteLibrary(selectedLibrary);
				}
				Session.Libraries.Add(library);
				Session.Libraries.Sort();
				this.save(library);
				this.update_libraries();
				this.SelectedLibrary = library;
				this.update_content(true);
			}
		}

		private void LibraryRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary != null)
			{
				if (MessageBox.Show("You are about to delete a library; are you sure you want to do this?", "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
				{
					return;
				}
				Session.DeleteLibrary(this.SelectedLibrary);
				this.update_libraries();
				this.SelectedLibrary = null;
				this.update_content(true);
			}
		}

		private void LibraryTree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			this.update_content(true);
		}

		private void MagicItemAddBtn_Click(object sender, EventArgs e)
		{
			MagicItemBuilderForm magicItemBuilderForm = new MagicItemBuilderForm(new MagicItem()
			{
				Name = "New Magic Item"
			});
			if (magicItemBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.SelectedLibrary.MagicItems.Add(magicItemBuilderForm.MagicItem);
				this.fModified[this.SelectedLibrary] = true;
				this.update_content(true);
			}
		}

		private void MagicItemAddImport_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary != null)
			{
				if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
				{
					return;
				}
				OpenFileDialog openFileDialog = new OpenFileDialog()
				{
					Filter = Program.MagicItemFilter,
					Multiselect = true
				};
				if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					string[] fileNames = openFileDialog.FileNames;
					for (int i = 0; i < (int)fileNames.Length; i++)
					{
						foreach (MagicItem d in Serialisation<List<MagicItem>>.Load(fileNames[i], SerialisationMode.Binary))
						{
							if (d == null)
							{
								continue;
							}
							MagicItem magicItem = this.SelectedLibrary.FindMagicItem(d.Name, d.Level);
							if (magicItem != null)
							{
								d.ID = magicItem.ID;
								this.SelectedLibrary.MagicItems.Remove(magicItem);
							}
							this.SelectedLibrary.MagicItems.Add(d);
							this.fModified[this.SelectedLibrary] = true;
							this.update_content(true);
						}
					}
				}
			}
		}

		private void MagicItemContextRemove_Click(object sender, EventArgs e)
		{
			this.MagicItemRemoveBtn_Click(sender, e);
		}

		private void MagicItemCopyBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedMagicItems.Count != 0)
			{
				Clipboard.SetData(typeof(List<MagicItem>).ToString(), this.SelectedMagicItems);
			}
		}

		private void MagicItemCutBtn_Click(object sender, EventArgs e)
		{
			this.MagicItemCopyBtn_Click(sender, e);
			this.MagicItemRemoveBtn_Click(sender, e);
		}

		private void MagicItemDemoBtn_Click(object sender, EventArgs e)
		{
			try
			{
				(new DemographicsForm(this.SelectedLibrary, DemographicsSource.MagicItems)).ShowDialog();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void MagicItemEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedMagicItems.Count == 1)
			{
				Library magicItem = Session.FindLibrary(this.SelectedMagicItems[0]);
				if (magicItem == null)
				{
					return;
				}
				int num = magicItem.MagicItems.IndexOf(this.SelectedMagicItems[0]);
				MagicItemBuilderForm magicItemBuilderForm = new MagicItemBuilderForm(this.SelectedMagicItems[0]);
				if (magicItemBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					magicItem.MagicItems[num] = magicItemBuilderForm.MagicItem;
					this.fModified[magicItem] = true;
					this.update_content(true);
				}
			}
		}

		private void MagicItemList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedMagicItems.Count != 0)
			{
				base.DoDragDrop(this.SelectedMagicItems, DragDropEffects.Move);
			}
		}

		private void MagicItemList_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.update_magic_item_versions();
		}

		private void MagicItemPasteBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary == null)
			{
				return;
			}
			if (Clipboard.ContainsData(typeof(List<MagicItem>).ToString()))
			{
				foreach (MagicItem datum in Clipboard.GetData(typeof(List<MagicItem>).ToString()) as List<MagicItem>)
				{
					MagicItem magicItem = datum.Copy();
					magicItem.ID = Guid.NewGuid();
					this.SelectedLibrary.MagicItems.Add(magicItem);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_content(true);
			}
		}

		private void MagicItemRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedMagicItems.Count != 0)
			{
				if (MessageBox.Show("Are you sure you want to delete your selection?", "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
				{
					return;
				}
				foreach (MagicItem selectedMagicItem in this.SelectedMagicItems)
				{
					Library selectedLibrary = this.SelectedLibrary ?? Session.FindLibrary(selectedMagicItem);
					selectedLibrary.MagicItems.Remove(selectedMagicItem);
					this.fModified[selectedLibrary] = true;
				}
				this.update_content(true);
			}
		}

		private void MagicItemStatBlockBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedMagicItems.Count != 1)
			{
				return;
			}
			MagicItemDetailsForm magicItemDetailsForm = new MagicItemDetailsForm(this.SelectedMagicItems[0]);
			magicItemDetailsForm.ShowDialog();
		}

		private void MagicItemsToolsExport_Click(object sender, EventArgs e)
		{
			if (this.SelectedMagicItemSet != "")
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog()
				{
					Title = "Export",
					Filter = Program.MagicItemFilter,
					FileName = this.SelectedMagicItemSet
				};
				if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					List<MagicItem> magicItems = new List<MagicItem>();
					if (this.SelectedLibrary == null)
					{
						foreach (Library library in Session.Libraries)
						{
							magicItems.AddRange(library.MagicItems);
						}
					}
					else
					{
						magicItems.AddRange(this.SelectedLibrary.MagicItems);
					}
					List<MagicItem> magicItems1 = new List<MagicItem>();
					foreach (MagicItem magicItem in magicItems)
					{
						if (magicItem.Name != this.SelectedMagicItemSet)
						{
							continue;
						}
						magicItems1.Add(magicItem);
					}
					Serialisation<List<MagicItem>>.Save(saveFileDialog.FileName, magicItems1, SerialisationMode.Binary);
				}
			}
		}

		private void OppEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCreatures.Count == 1)
			{
				Library creature = Session.FindLibrary(this.SelectedCreatures[0]);
				if (creature == null)
				{
					return;
				}
				int num = creature.Creatures.IndexOf(this.SelectedCreatures[0]);
				CreatureBuilderForm creatureBuilderForm = new CreatureBuilderForm(this.SelectedCreatures[0]);
				if (creatureBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					creature.Creatures[num] = creatureBuilderForm.Creature as Creature;
					this.fModified[creature] = true;
					this.update_content(true);
				}
			}
		}

		private void OppList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedCreatures.Count != 0)
			{
				base.DoDragDrop(this.SelectedCreatures, DragDropEffects.Move);
			}
		}

		private void OppRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCreatures.Count != 0)
			{
				if (MessageBox.Show("Are you sure you want to delete your selection?", "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
				{
					return;
				}
				foreach (Creature selectedCreature in this.SelectedCreatures)
				{
					Library selectedLibrary = this.SelectedLibrary ?? Session.FindLibrary(selectedCreature);
					selectedLibrary.Creatures.Remove(selectedCreature);
					this.fModified[selectedLibrary] = true;
				}
				this.update_content(true);
			}
		}

		private void Pages_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.update_content(false);
		}

		private void PowerStatsBtn_Click(object sender, EventArgs e)
		{
			List<CreaturePower> creaturePowers = new List<CreaturePower>();
			int count = 0;
			if (this.SelectedLibrary != null)
			{
				count = this.SelectedLibrary.Creatures.Count;
				foreach (ICreature creature in this.SelectedLibrary.Creatures)
				{
					if (creature == null)
					{
						continue;
					}
					creaturePowers.AddRange(creature.CreaturePowers);
				}
				if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
				{
					count += Session.Project.CustomCreatures.Count;
					foreach (ICreature customCreature in Session.Project.CustomCreatures)
					{
						if (customCreature == null)
						{
							continue;
						}
						creaturePowers.AddRange(customCreature.CreaturePowers);
					}
				}
			}
			else
			{
				foreach (Library library in Session.Libraries)
				{
					count += library.Creatures.Count;
					foreach (Creature creature1 in library.Creatures)
					{
						creaturePowers.AddRange(creature1.CreaturePowers);
					}
				}
			}
			(new PowerStatisticsForm(creaturePowers, count)).ShowDialog();
		}

		private void save(Library lib)
		{
			GC.Collect();
			string libraryFilename = Session.GetLibraryFilename(lib);
			Serialisation<Library>.Save(libraryFilename, lib, SerialisationMode.Binary);
		}

		private void SearchBox_TextChanged(object sender, EventArgs e)
		{
			this.update_content(true);
		}

		private void show_help(bool show)
		{
			this.HelpPanel.Visible = show;
		}

		private void TemplateAddBtn_Click(object sender, EventArgs e)
		{
			CreatureTemplateBuilderForm creatureTemplateBuilderForm = new CreatureTemplateBuilderForm(new CreatureTemplate()
			{
				Name = "New Template"
			});
			if (creatureTemplateBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.SelectedLibrary.Templates.Add(creatureTemplateBuilderForm.Template);
				this.fModified[this.SelectedLibrary] = true;
				this.update_content(true);
			}
		}

		private void TemplateAddTheme_Click(object sender, EventArgs e)
		{
			MonsterThemeForm monsterThemeForm = new MonsterThemeForm(new MonsterTheme()
			{
				Name = "New Theme"
			});
			if (monsterThemeForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.SelectedLibrary.Themes.Add(monsterThemeForm.Theme);
				this.fModified[this.SelectedLibrary] = true;
				this.update_content(true);
			}
		}

		private void TemplateClass_Click(object sender, EventArgs e)
		{
			foreach (CreatureTemplate selectedTemplate in this.SelectedTemplates)
			{
				selectedTemplate.Type = CreatureTemplateType.Class;
				Library library = Session.FindLibrary(selectedTemplate);
				if (library == null)
				{
					continue;
				}
				this.fModified[library] = true;
			}
			this.update_templates();
		}

		private void TemplateContextRemove_Click(object sender, EventArgs e)
		{
			this.TemplateRemoveBtn_Click(sender, e);
		}

		private void TemplateCopyBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTemplates.Count != 0)
			{
				Clipboard.SetData(typeof(List<CreatureTemplate>).ToString(), this.SelectedTemplates);
			}
			if (this.SelectedThemes.Count != 0)
			{
				Clipboard.SetData(typeof(List<MonsterTheme>).ToString(), this.SelectedThemes);
			}
		}

		private void TemplateCutBtn_Click(object sender, EventArgs e)
		{
			this.TemplateCopyBtn_Click(sender, e);
			this.TemplateRemoveBtn_Click(sender, e);
		}

		private void TemplateEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTemplates.Count == 1)
			{
				Library template = Session.FindLibrary(this.SelectedTemplates[0]);
				if (template == null)
				{
					return;
				}
				int num = template.Templates.IndexOf(this.SelectedTemplates[0]);
				CreatureTemplateBuilderForm creatureTemplateBuilderForm = new CreatureTemplateBuilderForm(this.SelectedTemplates[0]);
				if (creatureTemplateBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					template.Templates[num] = creatureTemplateBuilderForm.Template;
					this.fModified[template] = true;
					this.update_content(true);
				}
			}
			if (this.SelectedThemes.Count == 1)
			{
				Library theme = Session.FindLibrary(this.SelectedThemes[0]);
				if (theme == null)
				{
					return;
				}
				int num1 = theme.Themes.IndexOf(this.SelectedThemes[0]);
				MonsterThemeForm monsterThemeForm = new MonsterThemeForm(this.SelectedThemes[0]);
				if (monsterThemeForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					theme.Themes[num1] = monsterThemeForm.Theme;
					this.fModified[theme] = true;
					this.update_content(true);
				}
			}
		}

		private void TemplateFunctional_Click(object sender, EventArgs e)
		{
			foreach (CreatureTemplate selectedTemplate in this.SelectedTemplates)
			{
				selectedTemplate.Type = CreatureTemplateType.Functional;
				Library library = Session.FindLibrary(selectedTemplate);
				if (library == null)
				{
					continue;
				}
				this.fModified[library] = true;
			}
			this.update_templates();
		}

		private void TemplateImport_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary != null)
			{
				if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
				{
					return;
				}
				OpenFileDialog openFileDialog = new OpenFileDialog()
				{
					Filter = Program.CreatureTemplateAndThemeFilter,
					Multiselect = true
				};
				if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					string[] fileNames = openFileDialog.FileNames;
					for (int i = 0; i < (int)fileNames.Length; i++)
					{
						string str = fileNames[i];
						if (str.EndsWith("creaturetemplate"))
						{
							CreatureTemplate creatureTemplate = Serialisation<CreatureTemplate>.Load(str, SerialisationMode.Binary);
							if (creatureTemplate != null)
							{
								this.SelectedLibrary.Templates.Add(creatureTemplate);
								this.fModified[this.SelectedLibrary] = true;
								this.update_content(true);
							}
						}
						if (str.EndsWith("theme"))
						{
							MonsterTheme monsterTheme = Serialisation<MonsterTheme>.Load(str, SerialisationMode.Binary);
							if (monsterTheme != null)
							{
								this.SelectedLibrary.Themes.Add(monsterTheme);
								this.fModified[this.SelectedLibrary] = true;
								this.update_content(true);
							}
						}
					}
				}
			}
		}

		private void TemplateList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedTemplates.Count != 0)
			{
				base.DoDragDrop(this.SelectedTemplates, DragDropEffects.Move);
			}
		}

		private void TemplatePasteBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary == null)
			{
				return;
			}
			if (Clipboard.ContainsData(typeof(List<CreatureTemplate>).ToString()))
			{
				foreach (CreatureTemplate datum in Clipboard.GetData(typeof(List<CreatureTemplate>).ToString()) as List<CreatureTemplate>)
				{
					CreatureTemplate creatureTemplate = datum.Copy();
					creatureTemplate.ID = Guid.NewGuid();
					this.SelectedLibrary.Templates.Add(creatureTemplate);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_content(true);
			}
			if (Clipboard.ContainsData(typeof(List<MonsterTheme>).ToString()))
			{
				foreach (MonsterTheme monsterTheme in Clipboard.GetData(typeof(List<MonsterTheme>).ToString()) as List<MonsterTheme>)
				{
					MonsterTheme monsterTheme1 = monsterTheme.Copy();
					monsterTheme1.ID = Guid.NewGuid();
					this.SelectedLibrary.Themes.Add(monsterTheme1);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_content(true);
			}
		}

		private void TemplateRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTemplates.Count != 0)
			{
				if (MessageBox.Show("Are you sure you want to delete your selection?", "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
				{
					return;
				}
				foreach (CreatureTemplate selectedTemplate in this.SelectedTemplates)
				{
					Library selectedLibrary = this.SelectedLibrary ?? Session.FindLibrary(selectedTemplate);
					selectedLibrary.Templates.Remove(selectedTemplate);
					this.fModified[selectedLibrary] = true;
				}
				this.update_content(true);
			}
			if (this.SelectedThemes.Count != 0)
			{
				foreach (MonsterTheme selectedTheme in this.SelectedThemes)
				{
					Library library = this.SelectedLibrary ?? Session.FindLibrary(selectedTheme);
					library.Themes.Remove(selectedTheme);
					this.fModified[library] = true;
				}
				this.update_content(true);
			}
		}

		private void TemplateStatBlock_Click(object sender, EventArgs e)
		{
			if (this.SelectedTemplates.Count != 1)
			{
				return;
			}
			CreatureTemplateDetailsForm creatureTemplateDetailsForm = new CreatureTemplateDetailsForm(this.SelectedTemplates[0]);
			creatureTemplateDetailsForm.ShowDialog();
		}

		private void TemplateToolsExport_Click(object sender, EventArgs e)
		{
			if (this.SelectedTemplates.Count == 1)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog()
				{
					Title = "Export",
					Filter = Program.CreatureTemplateFilter,
					FileName = this.SelectedTemplates[0].Name
				};
				if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Serialisation<CreatureTemplate>.Save(saveFileDialog.FileName, this.SelectedTemplates[0], SerialisationMode.Binary);
					return;
				}
			}
			else if (this.SelectedThemes.Count == 1)
			{
				SaveFileDialog saveFileDialog1 = new SaveFileDialog()
				{
					Title = "Export",
					Filter = Program.ThemeFilter,
					FileName = this.SelectedThemes[0].Name
				};
				if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Serialisation<MonsterTheme>.Save(saveFileDialog1.FileName, this.SelectedThemes[0], SerialisationMode.Binary);
				}
			}
		}

		private void TileAddBtn_Click(object sender, EventArgs e)
		{
			TileForm tileForm = new TileForm(new Tile());
			if (tileForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.SelectedLibrary.Tiles.Add(tileForm.Tile);
				this.fModified[this.SelectedLibrary] = true;
				this.update_content(true);
			}
		}

		private void TileAddFolderBtn_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
			{
				Description = "Choose the folder to open.",
				ShowNewFolderButton = false
			};
			if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(folderBrowserDialog.SelectedPath);
				List<string> strs = new List<string>()
				{
					"jpg",
					"jpeg",
					"bmp",
					"gif",
					"png",
					"tga"
				};
				List<FileInfo> fileInfos = new List<FileInfo>();
				foreach (string str in strs)
				{
					fileInfos.AddRange(directoryInfo.GetFiles(string.Concat("*.", str)));
				}
				int width = 2147483647;
				int height = 2147483647;
				foreach (FileInfo fileInfo in fileInfos)
				{
					Image image = Image.FromFile(fileInfo.FullName);
					if (image.Width < width)
					{
						width = image.Width;
					}
					if (image.Height >= height)
					{
						continue;
					}
					height = image.Height;
				}
				int num = Math.Min(width, height);
				foreach (FileInfo fileInfo1 in fileInfos)
				{
                    Tile tile = new Tile();
                    tile.Image = Image.FromFile(fileInfo1.FullName);
                    tile.Size = new System.Drawing.Size(tile.Image.Width / num, tile.Image.Height / num);

					Program.SetResolution(tile.Image);
					this.SelectedLibrary.Tiles.Add(tile);
				}
				this.fModified[this.SelectedLibrary] = true;
				this.update_content(true);
			}
		}

		private void TileAddImport_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary != null)
			{
				if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
				{
					return;
				}
				OpenFileDialog openFileDialog = new OpenFileDialog()
				{
					Filter = Program.MapTileFilter,
					Multiselect = true
				};
				if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					string[] fileNames = openFileDialog.FileNames;
					for (int i = 0; i < (int)fileNames.Length; i++)
					{
						Tile tile = Serialisation<Tile>.Load(fileNames[i], SerialisationMode.Binary);
						if (tile != null)
						{
							this.SelectedLibrary.Tiles.Add(tile);
							this.fModified[this.SelectedLibrary] = true;
							this.update_content(true);
						}
					}
				}
			}
		}

		private void TileContextRemove_Click(object sender, EventArgs e)
		{
			this.TileSetRemoveBtn_Click(sender, e);
		}

		private void TileContextSize_Click(object sender, EventArgs e)
		{
			TileSizeForm tileSizeForm = new TileSizeForm(this.SelectedTiles);
			if (tileSizeForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				foreach (Tile selectedTile in this.SelectedTiles)
				{
					selectedTile.Size = tileSizeForm.TileSize;
					Library library = Session.FindLibrary(selectedTile);
					if (library == null)
					{
						continue;
					}
					this.fModified[library] = true;
				}
				this.update_tiles();
			}
		}

		private void TileCopyBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTiles.Count != 0)
			{
				Clipboard.SetData(typeof(List<Tile>).ToString(), this.SelectedTiles);
			}
		}

		private void TileCutBtn_Click(object sender, EventArgs e)
		{
			this.TileCopyBtn_Click(sender, e);
			this.TileSetRemoveBtn_Click(sender, e);
		}

		private void TileDoorway_Click(object sender, EventArgs e)
		{
			foreach (Tile selectedTile in this.SelectedTiles)
			{
				selectedTile.Category = TileCategory.Doorway;
				Library library = Session.FindLibrary(selectedTile);
				if (library == null)
				{
					continue;
				}
				this.fModified[library] = true;
			}
			this.update_tiles();
		}

		private void TileFeature_Click(object sender, EventArgs e)
		{
			foreach (Tile selectedTile in this.SelectedTiles)
			{
				selectedTile.Category = TileCategory.Feature;
				Library library = Session.FindLibrary(selectedTile);
				if (library == null)
				{
					continue;
				}
				this.fModified[library] = true;
			}
			this.update_tiles();
		}

		private void TileMap_Click(object sender, EventArgs e)
		{
			foreach (Tile selectedTile in this.SelectedTiles)
			{
				selectedTile.Category = TileCategory.Map;
				Library library = Session.FindLibrary(selectedTile);
				if (library == null)
				{
					continue;
				}
				this.fModified[library] = true;
			}
			this.update_tiles();
		}

		private void TilePasteBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary == null)
			{
				return;
			}
			if (Clipboard.ContainsData(typeof(List<Tile>).ToString()))
			{
				foreach (Tile datum in Clipboard.GetData(typeof(List<Tile>).ToString()) as List<Tile>)
				{
					Tile tile = datum.Copy();
					tile.ID = Guid.NewGuid();
					this.SelectedLibrary.Tiles.Add(tile);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_content(true);
			}
		}

		private void TilePlain_Click(object sender, EventArgs e)
		{
			foreach (Tile selectedTile in this.SelectedTiles)
			{
				selectedTile.Category = TileCategory.Plain;
				Library library = Session.FindLibrary(selectedTile);
				if (library == null)
				{
					continue;
				}
				this.fModified[library] = true;
			}
			this.update_tiles();
		}

		private void TileSetEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTiles.Count == 1)
			{
				Library tile = Session.FindLibrary(this.SelectedTiles[0]);
				if (tile == null)
				{
					return;
				}
				int num = tile.Tiles.IndexOf(this.SelectedTiles[0]);
				TileForm tileForm = new TileForm(this.SelectedTiles[0]);
				if (tileForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					tile.Tiles[num] = tileForm.Tile;
					this.fModified[tile] = true;
					this.update_content(true);
				}
			}
		}

		private void TileSetRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTiles.Count != 0)
			{
				if (MessageBox.Show("Are you sure you want to delete your selection?", "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
				{
					return;
				}
				foreach (Tile selectedTile in this.SelectedTiles)
				{
					Library selectedLibrary = this.SelectedLibrary ?? Session.FindLibrary(selectedTile);
					selectedLibrary.Tiles.Remove(selectedTile);
					this.fModified[selectedLibrary] = true;
				}
				this.update_content(true);
			}
		}

		private void TileSetView_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedTiles.Count != 0)
			{
				base.DoDragDrop(this.SelectedTiles, DragDropEffects.Move);
			}
		}

		private void TileSpecial_Click(object sender, EventArgs e)
		{
			foreach (Tile selectedTile in this.SelectedTiles)
			{
				selectedTile.Category = TileCategory.Special;
				Library library = Session.FindLibrary(selectedTile);
				if (library == null)
				{
					continue;
				}
				this.fModified[library] = true;
			}
			this.update_tiles();
		}

		private void TileStairway_Click(object sender, EventArgs e)
		{
			foreach (Tile selectedTile in this.SelectedTiles)
			{
				selectedTile.Category = TileCategory.Stairway;
				Library library = Session.FindLibrary(selectedTile);
				if (library == null)
				{
					continue;
				}
				this.fModified[library] = true;
			}
			this.update_tiles();
		}

		private void TileToolsExport_Click(object sender, EventArgs e)
		{
			if (this.SelectedTiles.Count == 1)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog()
				{
					Title = "Export",
					Filter = Program.MapTileFilter,
					FileName = this.SelectedTiles[0].ToString()
				};
				if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Serialisation<Tile>.Save(saveFileDialog.FileName, this.SelectedTiles[0], SerialisationMode.Binary);
				}
			}
		}

		private void TPAddBtn_Click(object sender, EventArgs e)
		{
			TerrainPowerForm terrainPowerForm = new TerrainPowerForm(new TerrainPower()
			{
				Name = "New Terrain Power"
			});
			if (terrainPowerForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.SelectedLibrary.TerrainPowers.Add(terrainPowerForm.Power);
				this.fModified[this.SelectedLibrary] = true;
				this.update_content(true);
			}
		}

		private void TPAddImport_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary != null)
			{
				if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
				{
					return;
				}
				OpenFileDialog openFileDialog = new OpenFileDialog()
				{
					Filter = Program.TerrainPowerFilter,
					Multiselect = true
				};
				if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					string[] fileNames = openFileDialog.FileNames;
					for (int i = 0; i < (int)fileNames.Length; i++)
					{
						TerrainPower terrainPower = Serialisation<TerrainPower>.Load(fileNames[i], SerialisationMode.Binary);
						if (terrainPower != null)
						{
							this.SelectedLibrary.TerrainPowers.Add(terrainPower);
							this.fModified[this.SelectedLibrary] = true;
							this.update_content(true);
						}
					}
				}
			}
		}

		private void TPContextRemove_Click(object sender, EventArgs e)
		{
			this.TPRemoveBtn_Click(sender, e);
		}

		private void TPCopyBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTerrainPowers != null)
			{
				Clipboard.SetData(typeof(List<TerrainPower>).ToString(), this.SelectedTerrainPowers);
			}
		}

		private void TPCutBtn_Click(object sender, EventArgs e)
		{
			this.TPCopyBtn_Click(sender, e);
			this.TPRemoveBtn_Click(sender, e);
		}

		private void TPEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTerrainPowers.Count == 1)
			{
				Library power = Session.FindLibrary(this.SelectedTerrainPowers[0]);
				if (power == null)
				{
					return;
				}
				int num = power.TerrainPowers.IndexOf(this.SelectedTerrainPowers[0]);
				TerrainPowerForm terrainPowerForm = new TerrainPowerForm(this.SelectedTerrainPowers[0]);
				if (terrainPowerForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					power.TerrainPowers[num] = terrainPowerForm.Power;
					this.fModified[power] = true;
					this.update_content(true);
				}
			}
		}

		private void TPList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedTerrainPowers.Count != 0)
			{
				base.DoDragDrop(this.SelectedTerrainPowers, DragDropEffects.Move);
			}
		}

		private void TPPasteBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary == null)
			{
				return;
			}
			if (Clipboard.ContainsData(typeof(List<TerrainPower>).ToString()))
			{
				foreach (TerrainPower datum in Clipboard.GetData(typeof(List<TerrainPower>).ToString()) as List<TerrainPower>)
				{
					TerrainPower terrainPower = datum.Copy();
					terrainPower.ID = Guid.NewGuid();
					this.SelectedLibrary.TerrainPowers.Add(terrainPower);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_content(true);
			}
		}

		private void TPRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTerrainPowers.Count != 0)
			{
				if (MessageBox.Show("Are you sure you want to delete your selection?", "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
				{
					return;
				}
				foreach (TerrainPower selectedTerrainPower in this.SelectedTerrainPowers)
				{
					Library selectedLibrary = this.SelectedLibrary ?? Session.FindLibrary(selectedTerrainPower);
					selectedLibrary.TerrainPowers.Remove(selectedTerrainPower);
					this.fModified[selectedLibrary] = true;
				}
				this.update_content(true);
			}
		}

		private void TPStatBlockBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTerrainPowers.Count != 1)
			{
				return;
			}
			TerrainPowerDetailsForm terrainPowerDetailsForm = new TerrainPowerDetailsForm(this.SelectedTerrainPowers[0]);
			terrainPowerDetailsForm.ShowDialog();
		}

		private void TPToolsExport_Click(object sender, EventArgs e)
		{
			if (this.SelectedTerrainPowers.Count == 1)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog()
				{
					Title = "Export",
					Filter = Program.TerrainPowerFilter,
					FileName = this.SelectedTerrainPowers[0].Name
				};
				if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Serialisation<TerrainPower>.Save(saveFileDialog.FileName, this.SelectedTerrainPowers[0], SerialisationMode.Binary);
				}
			}
		}

		private void TrapAddBtn_Click(object sender, EventArgs e)
		{
			Trap trap = new Trap()
			{
				Name = "New Trap"
			};
			trap.Attacks.Add(new TrapAttack());
			TrapBuilderForm trapBuilderForm = new TrapBuilderForm(trap);
			if (trapBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.SelectedLibrary.Traps.Add(trapBuilderForm.Trap);
				this.fModified[this.SelectedLibrary] = true;
				this.update_content(true);
			}
		}

		private void TrapAddImport_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary != null)
			{
				if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
				{
					return;
				}
				OpenFileDialog openFileDialog = new OpenFileDialog()
				{
					Filter = Program.TrapFilter,
					Multiselect = true
				};
				if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					string[] fileNames = openFileDialog.FileNames;
					for (int i = 0; i < (int)fileNames.Length; i++)
					{
						Trap d = Serialisation<Trap>.Load(fileNames[i], SerialisationMode.Binary);
						if (d != null)
						{
							Trap trap = this.SelectedLibrary.FindTrap(d.Name, d.Level, d.Role.ToString());
							if (trap != null)
							{
								d.ID = trap.ID;
								this.SelectedLibrary.Traps.Remove(trap);
							}
							this.SelectedLibrary.Traps.Add(d);
							this.fModified[this.SelectedLibrary] = true;
							this.update_content(true);
						}
					}
				}
			}
		}

		private void TrapContextRemove_Click(object sender, EventArgs e)
		{
			this.TrapRemoveBtn_Click(sender, e);
		}

		private void TrapCopyBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTraps.Count != 0)
			{
				Clipboard.SetData(typeof(List<Trap>).ToString(), this.SelectedTraps);
			}
		}

		private void TrapCutBtn_Click(object sender, EventArgs e)
		{
			this.TrapCopyBtn_Click(sender, e);
			this.TrapRemoveBtn_Click(sender, e);
		}

		private void TrapDemoBtn_Click(object sender, EventArgs e)
		{
			try
			{
				(new DemographicsForm(this.SelectedLibrary, DemographicsSource.Traps)).ShowDialog();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void TrapEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTraps.Count == 1)
			{
				Library trap = Session.FindLibrary(this.SelectedTraps[0]);
				if (trap == null)
				{
					return;
				}
				int num = trap.Traps.IndexOf(this.SelectedTraps[0]);
				TrapBuilderForm trapBuilderForm = new TrapBuilderForm(this.SelectedTraps[0]);
				if (trapBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					trap.Traps[num] = trapBuilderForm.Trap;
					this.fModified[trap] = true;
					this.update_content(true);
				}
			}
		}

		private void TrapHazard_Click(object sender, EventArgs e)
		{
			foreach (Trap selectedTrap in this.SelectedTraps)
			{
				selectedTrap.Type = TrapType.Hazard;
				Library library = Session.FindLibrary(selectedTrap);
				if (library == null)
				{
					continue;
				}
				this.fModified[library] = true;
			}
			this.update_traps();
		}

		private void TrapList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedTraps.Count != 0)
			{
				base.DoDragDrop(this.SelectedTraps, DragDropEffects.Move);
			}
		}

		private void TrapPasteBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary == null)
			{
				return;
			}
			if (Clipboard.ContainsData(typeof(List<Trap>).ToString()))
			{
				foreach (Trap datum in Clipboard.GetData(typeof(List<Trap>).ToString()) as List<Trap>)
				{
					Trap trap = datum.Copy();
					trap.ID = Guid.NewGuid();
					this.SelectedLibrary.Traps.Add(trap);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_content(true);
			}
		}

		private void TrapRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTraps.Count != 0)
			{
				if (MessageBox.Show("Are you sure you want to delete your selection?", "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
				{
					return;
				}
				foreach (Trap selectedTrap in this.SelectedTraps)
				{
					Library selectedLibrary = this.SelectedLibrary ?? Session.FindLibrary(selectedTrap);
					selectedLibrary.Traps.Remove(selectedTrap);
					this.fModified[selectedLibrary] = true;
				}
				this.update_content(true);
			}
		}

		private void TrapStatBlockBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTraps.Count != 1)
			{
				return;
			}
			TrapDetailsForm trapDetailsForm = new TrapDetailsForm(this.SelectedTraps[0]);
			trapDetailsForm.ShowDialog();
		}

		private void TrapToolsExport_Click(object sender, EventArgs e)
		{
			if (this.SelectedTraps.Count == 1)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog()
				{
					Title = "Export",
					Filter = Program.TrapFilter,
					FileName = this.SelectedTraps[0].Name
				};
				if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Serialisation<Trap>.Save(saveFileDialog.FileName, this.SelectedTraps[0], SerialisationMode.Binary);
				}
			}
		}

		private void TrapTrap_Click(object sender, EventArgs e)
		{
			foreach (Trap selectedTrap in this.SelectedTraps)
			{
				selectedTrap.Type = TrapType.Trap;
				Library library = Session.FindLibrary(selectedTrap);
				if (library == null)
				{
					continue;
				}
				this.fModified[library] = true;
			}
			this.update_traps();
		}

		private void update_artifacts()
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			this.ArtifactList.BeginUpdate();
			ListState state = ListState.GetState(this.ArtifactList);
			List<Artifact> artifacts = new List<Artifact>();
			if (this.SelectedLibrary == null)
			{
				foreach (Library library in Session.Libraries)
				{
					artifacts.AddRange(library.Artifacts);
				}
			}
			else
			{
				artifacts.AddRange(this.SelectedLibrary.Artifacts);
				if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
				{
					foreach (Parcel treasureParcel in Session.Project.TreasureParcels)
					{
						if (treasureParcel.ArtifactID == Guid.Empty)
						{
							continue;
						}
						Artifact artifact = Session.FindArtifact(treasureParcel.ArtifactID, SearchType.Global);
						if (artifact == null)
						{
							continue;
						}
						artifacts.Add(artifact);
					}
					foreach (PlotPoint allPlotPoint in Session.Project.AllPlotPoints)
					{
						foreach (Parcel parcel in allPlotPoint.Parcels)
						{
							if (parcel.ArtifactID == Guid.Empty)
							{
								continue;
							}
							Artifact artifact1 = Session.FindArtifact(parcel.ArtifactID, SearchType.Global);
							if (artifact1 == null)
							{
								continue;
							}
							artifacts.Add(artifact1);
						}
					}
				}
			}
			this.ArtifactList.Items.Clear();
			this.ArtifactList.ShowGroups = false;
			foreach (Artifact artifact2 in artifacts)
			{
				if (artifact2 == null)
				{
					continue;
				}
				ListViewItem listViewItem = this.ArtifactList.Items.Add(artifact2.Name);
				listViewItem.SubItems.Add(artifact2.Tier.ToString());
				listViewItem.Tag = artifact2;
			}
			if (this.ArtifactList.Items.Count == 0)
			{
				ListViewItem grayText = this.ArtifactList.Items.Add("(no artifacts)");
				grayText.ForeColor = SystemColors.GrayText;
			}
			this.ArtifactList.Sort();
			ListState.SetState(this.ArtifactList, state);
			this.ArtifactList.EndUpdate();
			System.Windows.Forms.Cursor.Current = Cursors.Default;
		}

		private void update_challenges()
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			this.ChallengeList.BeginUpdate();
			ListState state = ListState.GetState(this.ChallengeList);
			List<SkillChallenge> skillChallenges = new List<SkillChallenge>();
			if (this.SelectedLibrary == null)
			{
				foreach (Library library in Session.Libraries)
				{
					skillChallenges.AddRange(library.SkillChallenges);
				}
			}
			else
			{
				skillChallenges.AddRange(this.SelectedLibrary.SkillChallenges);
				if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
				{
					foreach (PlotPoint allPlotPoint in Session.Project.AllPlotPoints)
					{
						if (allPlotPoint.Element is Encounter)
						{
							skillChallenges.AddRange((allPlotPoint.Element as Encounter).SkillChallenges);
						}
						if (!(allPlotPoint.Element is SkillChallenge))
						{
							continue;
						}
						skillChallenges.Add(allPlotPoint.Element as SkillChallenge);
					}
				}
			}
			this.ChallengeList.Items.Clear();
			this.ChallengeList.ShowGroups = false;
			foreach (SkillChallenge skillChallenge in skillChallenges)
			{
				if (skillChallenge == null)
				{
					continue;
				}
				ListViewItem listViewItem = this.ChallengeList.Items.Add(skillChallenge.Name);
				listViewItem.SubItems.Add(skillChallenge.Info);
				listViewItem.Tag = skillChallenge;
			}
			if (this.ChallengeList.Items.Count == 0)
			{
				ListViewItem grayText = this.ChallengeList.Items.Add("(no skill challenges)");
				grayText.ForeColor = SystemColors.GrayText;
			}
			this.ChallengeList.Sort();
			ListState.SetState(this.ChallengeList, state);
			this.ChallengeList.EndUpdate();
			System.Windows.Forms.Cursor.Current = Cursors.Default;
		}

		private void update_content(bool force_refresh)
		{
			if (force_refresh)
			{
				this.fCleanPages.Clear();
			}
			if (this.Pages.SelectedTab == this.CreaturesPage && !this.fCleanPages.Contains(this.CreaturesPage))
			{
				this.update_creatures();
				this.fCleanPages.Add(this.CreaturesPage);
			}
			if (this.Pages.SelectedTab == this.TemplatesPage && !this.fCleanPages.Contains(this.TemplatesPage))
			{
				this.update_templates();
				this.fCleanPages.Add(this.TemplatesPage);
			}
			if (this.Pages.SelectedTab == this.TrapsPage && !this.fCleanPages.Contains(this.TrapsPage))
			{
				this.update_traps();
				this.fCleanPages.Add(this.TrapsPage);
			}
			if (this.Pages.SelectedTab == this.ChallengePage && !this.fCleanPages.Contains(this.ChallengePage))
			{
				this.update_challenges();
				this.fCleanPages.Add(this.ChallengePage);
			}
			if (this.Pages.SelectedTab == this.MagicItemsPage && !this.fCleanPages.Contains(this.MagicItemsPage))
			{
				this.update_magic_item_sets();
				this.update_magic_item_versions();
				this.fCleanPages.Add(this.MagicItemsPage);
			}
			if (this.Pages.SelectedTab == this.TilesPage && !this.fCleanPages.Contains(this.TilesPage))
			{
				this.update_tiles();
				this.fCleanPages.Add(this.TilesPage);
			}
			if (this.Pages.SelectedTab == this.TerrainPowersPage && !this.fCleanPages.Contains(this.TerrainPowersPage))
			{
				this.update_terrain_powers();
				this.fCleanPages.Add(this.TerrainPowersPage);
			}
			if (this.Pages.SelectedTab == this.ArtifactPage && !this.fCleanPages.Contains(this.ArtifactPage))
			{
				this.update_artifacts();
				this.fCleanPages.Add(this.ArtifactPage);
			}
		}

		private void update_creatures()
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			this.CreatureList.BeginUpdate();
			ListState state = ListState.GetState(this.CreatureList);
			List<Creature> creatures = new List<Creature>();
			if (this.SelectedLibrary == null)
			{
				creatures.AddRange(Session.Creatures);
			}
			else
			{
				creatures.AddRange(this.SelectedLibrary.Creatures);
				if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
				{
					foreach (CustomCreature customCreature in Session.Project.CustomCreatures)
					{
						creatures.Add(new Creature(customCreature));
					}
					foreach (NPC nPC in Session.Project.NPCs)
					{
						creatures.Add(new Creature(nPC));
					}
				}
			}
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			foreach (Creature creature in creatures)
			{
				if (creature == null || !(creature.Category != ""))
				{
					continue;
				}
				binarySearchTree.Add(creature.Category);
			}
			List<string> sortedList = binarySearchTree.SortedList;
			sortedList.Insert(0, "Custom Creatures");
			sortedList.Insert(1, "NPCs");
			sortedList.Add("Miscellaneous Creatures");
			this.CreatureList.Groups.Clear();
			foreach (string str in sortedList)
			{
				this.CreatureList.Groups.Add(str, str);
			}
			this.CreatureList.ShowGroups = true;
			List<ListViewItem> listViewItems = new List<ListViewItem>();
			foreach (Creature creature1 in creatures)
			{
				if (creature1 == null)
				{
					continue;
				}
				if (this.CreatureSearchToolbar.Visible)
				{
					if (this.SearchBox.Text != "")
					{
						string lower = this.SearchBox.Text.ToLower();
						bool flag = creature1.Name.ToLower().Contains(lower);
						if (!flag && creature1.Category != null && creature1.Category.ToLower().Contains(lower))
						{
							flag = true;
						}
						if (!flag)
						{
							continue;
						}
					}
					bool flag1 = false;
					bool flag2 = (creature1.Category == null ? false : creature1.Category != "");
					if (this.fShowCategorised && flag2)
					{
						flag1 = true;
					}
					if (this.fShowUncategorised && !flag2)
					{
						flag1 = true;
					}
					if (!flag1)
					{
						continue;
					}
				}
				ListViewItem listViewItem = new ListViewItem(creature1.Name);
				listViewItem.SubItems.Add(creature1.Info);
				listViewItem.Tag = creature1;
				if (creature1.Category == "")
				{
					listViewItem.Group = this.CreatureList.Groups["Miscellaneous Creatures"];
				}
				else
				{
					listViewItem.Group = this.CreatureList.Groups[creature1.Category];
				}
				listViewItems.Add(listViewItem);
			}
			this.CreatureList.Items.Clear();
			this.CreatureList.Items.AddRange(listViewItems.ToArray());
			if (this.CreatureList.Items.Count == 0)
			{
				this.CreatureList.ShowGroups = false;
				ListViewItem grayText = this.CreatureList.Items.Add("(no creatures)");
				grayText.ForeColor = SystemColors.GrayText;
			}
			this.CreatureList.Sort();
			ListState.SetState(this.CreatureList, state);
			this.CreatureList.EndUpdate();
			System.Windows.Forms.Cursor.Current = Cursors.Default;
		}

		private void update_libraries()
		{
			this.LibraryTree.Nodes.Clear();
			if (Session.Libraries.Count != 0)
			{
				TreeNode treeNode = this.LibraryTree.Nodes.Add("All Libraries");
				treeNode.ImageIndex = 0;
				foreach (Library library in Session.Libraries)
				{
					treeNode.Nodes.Add(library.Name).Tag = library;
				}
				treeNode.Expand();
			}
			else if (Session.Project == null)
			{
				TreeNode grayText = this.LibraryTree.Nodes.Add("(no libraries installed)");
				grayText.ForeColor = SystemColors.GrayText;
				this.show_help(true);
			}
			if (Session.Project != null)
			{
				TreeNode library1 = this.LibraryTree.Nodes.Add(Session.Project.Name);
				library1.Tag = Session.Project.Library;
			}
		}

		private void update_magic_item_sets()
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			this.MagicItemList.BeginUpdate();
			ListState state = ListState.GetState(this.MagicItemList);
			List<MagicItem> magicItems = new List<MagicItem>();
			if (this.SelectedLibrary == null)
			{
				foreach (Library library in Session.Libraries)
				{
					magicItems.AddRange(library.MagicItems);
				}
			}
			else
			{
				magicItems.AddRange(this.SelectedLibrary.MagicItems);
			}
			Dictionary<string, BinarySearchTree<string>> strs = new Dictionary<string, BinarySearchTree<string>>();
			foreach (MagicItem magicItem in magicItems)
			{
				string type = magicItem.Type;
				if (type == "")
				{
					type = "Miscallaneous Items";
				}
				if (!strs.ContainsKey(type))
				{
					strs[type] = new BinarySearchTree<string>();
				}
				strs[type].Add(magicItem.Name);
			}
			List<string> strs1 = new List<string>();
			strs1.AddRange(strs.Keys);
			strs1.Sort();
			int num = strs1.IndexOf("Miscellaneous Items");
			if (num != -1)
			{
				strs1.RemoveAt(num);
				strs1.Add("Miscellaneous Items");
			}
			this.MagicItemList.Groups.Clear();
			this.MagicItemList.ShowGroups = true;
			List<ListViewItem> listViewItems = new List<ListViewItem>();
			foreach (string str in strs1)
			{
				ListViewGroup listViewGroup = this.MagicItemList.Groups.Add(str, str);
				foreach (string sortedList in strs[str].SortedList)
				{
					listViewItems.Add(new ListViewItem(sortedList)
					{
						Group = listViewGroup
					});
				}
			}
			this.MagicItemList.Items.Clear();
			this.MagicItemList.Items.AddRange(listViewItems.ToArray());
			if (this.MagicItemList.Items.Count == 0)
			{
				this.MagicItemList.ShowGroups = false;
				ListViewItem grayText = this.MagicItemList.Items.Add("(no magic items)");
				grayText.ForeColor = SystemColors.GrayText;
			}
			ListState.SetState(this.MagicItemList, state);
			this.MagicItemList.EndUpdate();
			System.Windows.Forms.Cursor.Current = Cursors.Default;
		}

		private void update_magic_item_versions()
		{
			if (this.SelectedMagicItemSet == "")
			{
				this.MagicItemVersionList.Enabled = false;
				this.MagicItemVersionList.ShowGroups = false;
				this.MagicItemVersionList.Items.Clear();
			}
			else
			{
				this.MagicItemVersionList.Enabled = true;
				this.MagicItemVersionList.ShowGroups = true;
				this.MagicItemVersionList.Items.Clear();
				List<MagicItem> magicItems = new List<MagicItem>();
				if (this.SelectedLibrary == null)
				{
					foreach (Library library in Session.Libraries)
					{
						magicItems.AddRange(library.MagicItems);
					}
				}
				else
				{
					magicItems.AddRange(this.SelectedLibrary.MagicItems);
				}
				foreach (MagicItem magicItem in magicItems)
				{
					if (magicItem.Name != this.SelectedMagicItemSet)
					{
						continue;
					}
					ListViewItem item = this.MagicItemVersionList.Items.Add(string.Concat("Level ", magicItem.Level));
					item.Tag = magicItem;
					if (magicItem.Level < 11)
					{
						item.Group = this.MagicItemVersionList.Groups[0];
					}
					else if (magicItem.Level >= 21)
					{
						item.Group = this.MagicItemVersionList.Groups[2];
					}
					else
					{
						item.Group = this.MagicItemVersionList.Groups[1];
					}
				}
			}
		}

		private void update_templates()
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			this.TemplateList.BeginUpdate();
			ListState state = ListState.GetState(this.TemplateList);
			List<CreatureTemplate> creatureTemplates = new List<CreatureTemplate>();
			List<MonsterTheme> monsterThemes = new List<MonsterTheme>();
			if (this.SelectedLibrary == null)
			{
				foreach (Library library in Session.Libraries)
				{
					creatureTemplates.AddRange(library.Templates);
					monsterThemes.AddRange(library.Themes);
				}
			}
			else
			{
				creatureTemplates.AddRange(this.SelectedLibrary.Templates);
				monsterThemes.AddRange(this.SelectedLibrary.Themes);
			}
			this.TemplateList.Items.Clear();
			this.TemplateList.ShowGroups = true;
			foreach (CreatureTemplate creatureTemplate in creatureTemplates)
			{
				if (creatureTemplate == null)
				{
					continue;
				}
				ListViewItem item = this.TemplateList.Items.Add(creatureTemplate.Name);
				item.SubItems.Add(creatureTemplate.Info);
				item.Tag = creatureTemplate;
				item.Group = this.TemplateList.Groups[(creatureTemplate.Type == CreatureTemplateType.Functional ? 0 : 1)];
			}
			foreach (MonsterTheme monsterTheme in monsterThemes)
			{
				if (monsterTheme == null)
				{
					continue;
				}
				ListViewItem listViewItem = this.TemplateList.Items.Add(monsterTheme.Name);
				listViewItem.Tag = monsterTheme;
				listViewItem.Group = this.TemplateList.Groups[2];
			}
			if (this.TemplateList.Items.Count == 0)
			{
				this.TemplateList.ShowGroups = false;
				ListViewItem grayText = this.TemplateList.Items.Add("(no templates or themes)");
				grayText.ForeColor = SystemColors.GrayText;
			}
			this.TemplateList.Sort();
			ListState.SetState(this.TemplateList, state);
			this.TemplateList.EndUpdate();
			System.Windows.Forms.Cursor.Current = Cursors.Default;
		}

		private void update_terrain_powers()
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			this.TerrainPowerList.BeginUpdate();
			ListState state = ListState.GetState(this.TerrainPowerList);
			List<TerrainPower> terrainPowers = new List<TerrainPower>();
			if (this.SelectedLibrary == null)
			{
				foreach (Library library in Session.Libraries)
				{
					terrainPowers.AddRange(library.TerrainPowers);
				}
			}
			else
			{
				terrainPowers.AddRange(this.SelectedLibrary.TerrainPowers);
				if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
				{
					foreach (PlotPoint allPlotPoint in Session.Project.AllPlotPoints)
					{
						if (!(allPlotPoint.Element is Encounter))
						{
							continue;
						}
						foreach (CustomToken customToken in (allPlotPoint.Element as Encounter).CustomTokens)
						{
							if (customToken.TerrainPower == null)
							{
								continue;
							}
							terrainPowers.Add(customToken.TerrainPower);
						}
					}
				}
			}
			this.TerrainPowerList.Items.Clear();
			this.TerrainPowerList.ShowGroups = false;
			foreach (TerrainPower terrainPower in terrainPowers)
			{
				if (terrainPower == null)
				{
					continue;
				}
				ListViewItem listViewItem = this.TerrainPowerList.Items.Add(terrainPower.Name);
				listViewItem.SubItems.Add(terrainPower.Type.ToString());
				listViewItem.Tag = terrainPower;
			}
			if (this.TerrainPowerList.Items.Count == 0)
			{
				ListViewItem grayText = this.TerrainPowerList.Items.Add("(no terrain powers)");
				grayText.ForeColor = SystemColors.GrayText;
			}
			this.TerrainPowerList.Sort();
			ListState.SetState(this.TerrainPowerList, state);
			this.TerrainPowerList.EndUpdate();
			System.Windows.Forms.Cursor.Current = Cursors.Default;
		}

		private void update_tiles()
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			this.TileList.BeginUpdate();
			List<Tile> tiles = new List<Tile>();
			if (this.SelectedLibrary == null)
			{
				foreach (Library library in Session.Libraries)
				{
					tiles.AddRange(library.Tiles);
				}
			}
			else
			{
				tiles.AddRange(this.SelectedLibrary.Tiles);
			}
			this.TileList.Groups.Clear();
			this.TileList.ShowGroups = true;
			foreach (TileCategory value in Enum.GetValues(typeof(TileCategory)))
			{
				this.TileList.Groups.Add(value.ToString(), value.ToString());
			}
			this.TileList.Items.Clear();
			List<ListViewItem> listViewItems = new List<ListViewItem>();
			this.TileList.LargeImageList = new ImageList()
			{
				ColorDepth = ColorDepth.Depth32Bit,
				ImageSize = new System.Drawing.Size(32, 32)
			};
			foreach (Tile tile in tiles)
			{
				ListViewItem listViewItem = new ListViewItem(tile.ToString())
				{
					Tag = tile,
					Group = this.TileList.Groups[tile.Category.ToString()]
				};
				Image image = (tile.Image != null ? tile.Image : tile.BlankImage);
				Image bitmap = new Bitmap(32, 32);
				Graphics graphic = Graphics.FromImage(bitmap);
				if (tile.Size.Width <= tile.Size.Height)
				{
					System.Drawing.Size size = tile.Size;
					System.Drawing.Size size1 = tile.Size;
					int width = size.Width * 32 / size1.Height;
					Rectangle rectangle = new Rectangle((32 - width) / 2, 0, width, 32);
					graphic.DrawImage(image, rectangle);
				}
				else
				{
					System.Drawing.Size size2 = tile.Size;
					System.Drawing.Size size3 = tile.Size;
					int height = size2.Height * 32 / size3.Width;
					Rectangle rectangle1 = new Rectangle(0, (32 - height) / 2, 32, height);
					graphic.DrawImage(image, rectangle1);
				}
				this.TileList.LargeImageList.Images.Add(bitmap);
				listViewItem.ImageIndex = this.TileList.LargeImageList.Images.Count - 1;
				listViewItems.Add(listViewItem);
			}
			this.TileList.Items.AddRange(listViewItems.ToArray());
			if (this.TileList.Items.Count == 0)
			{
				this.TileList.ShowGroups = false;
				ListViewItem grayText = this.TileList.Items.Add("(no tiles)");
				grayText.ForeColor = SystemColors.GrayText;
			}
			this.TileList.EndUpdate();
			System.Windows.Forms.Cursor.Current = Cursors.Default;
		}

		private void update_traps()
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			this.TrapList.BeginUpdate();
			ListState state = ListState.GetState(this.TrapList);
			List<Trap> traps = new List<Trap>();
			if (this.SelectedLibrary == null)
			{
				foreach (Library library in Session.Libraries)
				{
					traps.AddRange(library.Traps);
				}
			}
			else
			{
				traps.AddRange(this.SelectedLibrary.Traps);
				if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
				{
					foreach (PlotPoint allPlotPoint in Session.Project.AllPlotPoints)
					{
						if (allPlotPoint.Element is Encounter)
						{
							traps.AddRange((allPlotPoint.Element as Encounter).Traps);
						}
						if (!(allPlotPoint.Element is Trap))
						{
							continue;
						}
						traps.Add(allPlotPoint.Element as Trap);
					}
				}
			}
			this.TrapList.Items.Clear();
			this.TrapList.ShowGroups = true;
			List<ListViewItem> listViewItems = new List<ListViewItem>();
			foreach (Trap trap in traps)
			{
				if (trap == null)
				{
					continue;
				}
				ListViewItem listViewItem = new ListViewItem(trap.Name);
				listViewItem.SubItems.Add(trap.Info);
				listViewItem.Tag = trap;
				listViewItem.Group = this.TrapList.Groups[(trap.Type == TrapType.Trap ? 0 : 1)];
				listViewItems.Add(listViewItem);
			}
			this.TrapList.Items.AddRange(listViewItems.ToArray());
			if (this.TrapList.Items.Count == 0)
			{
				this.TrapList.ShowGroups = false;
				ListViewItem grayText = this.TrapList.Items.Add("(no traps)");
				grayText.ForeColor = SystemColors.GrayText;
			}
			this.TrapList.Sort();
			ListState.SetState(this.TrapList, state);
			this.TrapList.EndUpdate();
			System.Windows.Forms.Cursor.Current = Cursors.Default;
		}
	}
}