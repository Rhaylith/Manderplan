using Masterplan;
using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Events;
using Masterplan.Extensibility;
using Masterplan.Tools;
using Masterplan.Tools.Generators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Utils;
using Utils.Forms;

namespace Masterplan.UI
{
	internal class MainForm : Form
	{
		private IContainer components;

		private ToolStrip WorkspaceToolbar;

		private ToolStripButton RemoveBtn;

		private System.Windows.Forms.ContextMenuStrip PointMenu;

		private ToolStripMenuItem ContextAdd;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem ContextRemove;

		private ToolStripMenuItem ContextEdit;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripMenuItem ContextExplore;

		private MenuStrip MainMenu;

		private ToolStripMenuItem FileMenu;

		private ToolStripMenuItem FileNew;

		private ToolStripSeparator toolStripMenuItem1;

		private ToolStripMenuItem FileOpen;

		private ToolStripSeparator toolStripMenuItem2;

		private ToolStripMenuItem FileSave;

		private ToolStripMenuItem FileSaveAs;

		private ToolStripSeparator toolStripMenuItem3;

		private ToolStripMenuItem FileExit;

		private SplitContainer PreviewSplitter;

		private ToolStripButton PlotCutBtn;

		private ToolStripButton PlotCopyBtn;

		private ToolStripButton PlotPasteBtn;

		private ToolStripMenuItem ProjectMenu;

		private ToolStripMenuItem HelpMenu;

		private ToolStripMenuItem HelpAbout;

		private ToolStripMenuItem HelpFeedback;

		private ToolStripMenuItem ProjectProject;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripButton SearchBtn;

		private ToolStrip WorkspaceSearchBar;

		private ToolStripLabel PlotSearchLbl;

		private ToolStripTextBox PlotSearchBox;

		private ToolStripMenuItem ContextAddBetween;

		private ToolStripMenuItem ProjectTacticalMaps;

		private ToolStripSeparator toolStripSeparator10;

		private ToolStripMenuItem ProjectDecks;

		private SplitContainer PreviewInfoSplitter;

		private ToolStripMenuItem ProjectOverview;

		private ToolStripMenuItem ProjectCustomCreatures;

		private ToolStripMenuItem HelpManual;

		private ToolStripSeparator toolStripSeparator12;

		private ToolStripMenuItem ProjectPlayers;

		private ToolStripMenuItem ProjectCalendars;

		private ToolStripMenuItem HelpWebsite;

		private ToolStripSeparator toolStripSeparator13;

		private SplitContainer NavigationSplitter;

		private TreeView NavigationTree;

		private ToolStripSeparator toolStripSeparator9;

		private ToolStripDropDownButton ViewMenu;

		private ToolStripMenuItem ViewDefault;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripMenuItem ViewEncounters;

		private ToolStripMenuItem ViewChallenges;

		private ToolStripMenuItem ViewQuests;

		private ToolStripMenuItem ViewParcels;

		private ToolStripSeparator toolStripSeparator8;

		private ToolStripMenuItem ViewHighlighting;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripMenuItem ViewLevelling;

		private ToolStripSeparator toolStripSeparator11;

		private ToolStripMenuItem ViewPreview;

		private ToolStripMenuItem ViewNavigation;

		private StatusStrip BreadcrumbBar;

		private TabControl Pages;

		private TabPage WorkspacePage;

		private Panel PlotPanel;

		private TabPage EncyclopediaPage;

		private SplitContainer EncyclopediaSplitter;

		private ListView EntryList;

		private ColumnHeader EntryHdr;

		private Panel EntryPanel;

		private ToolStrip EncyclopediaToolbar;

		private ToolStripButton EncRemoveBtn;

		private ToolStripButton EncEditBtn;

		private ToolStripSeparator toolStripSeparator15;

		private ToolStripLabel EncSearchLbl;

		private ToolStripTextBox EncSearchBox;

		private ToolStripLabel EncClearLbl;

		private ToolStripLabel PlotClearBtn;

		private TabPage JotterPage;

		private ToolStrip JotterToolbar;

		private SplitContainer JotterSplitter;

		private ListView NoteList;

		private TextBox NoteBox;

		private ToolStripButton NoteAddBtn;

		private ToolStripButton NoteRemoveBtn;

		private ColumnHeader NoteHdr;

		private ToolStripSeparator toolStripSeparator16;

		private ToolStripLabel NoteSearchLbl;

		private ToolStripTextBox NoteSearchBox;

		private ToolStripLabel NoteClearLbl;

		private ToolStripButton EncCutBtn;

		private ToolStripButton EncCopyBtn;

		private ToolStripButton EncPasteBtn;

		private ToolStripSeparator toolStripSeparator17;

		private ToolStripSeparator toolStripSeparator18;

		private ToolStripButton NoteCutBtn;

		private ToolStripButton NoteCopyBtn;

		private ToolStripButton NotePasteBtn;

		private WebBrowser EntryDetails;

		private Panel PreviewPanel;

		private ToolStripMenuItem ContextMoveTo;

		private ToolStripSeparator toolStripSeparator20;

		private ToolStripMenuItem ViewTooltips;

		private ToolStripMenuItem ViewTraps;

		private ToolStripMenuItem ToolsMenu;

		private ToolStripMenuItem ToolsExportProject;

		private ToolStripSeparator toolStripMenuItem4;

		private ToolStripMenuItem ToolsLibraries;

		private ToolStripSeparator toolStripMenuItem5;

		private ToolStripMenuItem ToolsAddIns;

		private ToolStripMenuItem addinsToolStripMenuItem;

		private TabPage AttachmentsPage;

		private ListView AttachmentList;

		private ColumnHeader AttachmentHdr;

		private ToolStrip AttachmentToolbar;

		private ToolStripButton AttachmentRemoveBtn;

		private ToolStripSeparator toolStripSeparator19;

		private ToolStripButton AttachmentPlayerView;

		private TabPage BackgroundPage;

		private SplitContainer splitContainer1;

		private ListView BackgroundList;

		private ToolStrip BackgroundToolbar;

		private ToolStripButton BackgroundAddBtn;

		private ToolStripButton BackgroundRemoveBtn;

		private ToolStripButton BackgroundEditBtn;

		private ToolStripSeparator toolStripSeparator21;

		private ToolStripButton EncPlayerView;

		private ToolStripSeparator toolStripSeparator22;

		private ColumnHeader InfoHdr;

		private Panel BackgroundPanel;

		private WebBrowser BackgroundDetails;

		private ToolStripSeparator toolStripSeparator23;

		private ToolStripButton BackgroundUpBtn;

		private ToolStripButton BackgroundDownBtn;

		private ToolStripDropDownButton AttachmentExtract;

		private ToolStripMenuItem AttachmentExtractSimple;

		private ToolStripMenuItem AttachmentExtractAndRun;

		private ColumnHeader AttachmentSizeHdr;

		private ToolStripSeparator toolStripSeparator24;

		private ToolStripMenuItem ProjectParcels;

		private ToolStripDropDownButton BackgroundPlayerView;

		private ToolStripMenuItem BackgroundPlayerViewSelected;

		private ToolStripMenuItem BackgroundPlayerViewAll;

		private ToolStripMenuItem PlayerViewMenu;

		private ToolStripMenuItem PlayerViewShow;

		private ToolStripMenuItem PlayerViewClear;

		private ToolStripSeparator toolStripMenuItem7;

		private ToolStripMenuItem PlayerViewOtherDisplay;

		private ToolStripMenuItem ToolsImportProject;

		private ToolStripSeparator toolStripSeparator25;

		private ToolStripMenuItem ToolsExportHandout;

		private ToolStrip PreviewToolbar;

		private ToolStripDropDownButton FlowchartMenu;

		private ToolStripMenuItem FlowchartPrint;

		private ToolStripMenuItem FlowchartExport;

		private ToolStripButton EditBtn;

		private ToolStripDropDownButton PlotPointMenu;

		private ToolStripMenuItem PlotPointPlayerView;

		private ToolStripButton ExploreBtn;

		private ToolStripMenuItem PlotPointExportHTML;

		private ToolStripSplitButton AddBtn;

		private ToolStripMenuItem AddEncounter;

		private ToolStripMenuItem AddChallenge;

		private ToolStripMenuItem AddTrap;

		private ToolStripMenuItem AddQuest;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem ProjectEncounters;

		private ToolStripSeparator toolStripSeparator14;

		private ToolStripMenuItem PlayerViewTextSize;

		private ToolStripMenuItem TextSizeSmall;

		private ToolStripMenuItem TextSizeLarge;

		private ToolStripButton AttachmentImportBtn;

		private ToolStripMenuItem TextSizeMedium;

		private ToolStripSeparator toolStripSeparator27;

		private ToolStripSeparator toolStripSeparator28;

		private ToolStripMenuItem ContextState;

		private ToolStripMenuItem ContextStateNormal;

		private ToolStripMenuItem ContextStateCompleted;

		private ToolStripMenuItem ContextStateSkipped;

		private ToolStripMenuItem ContextDisconnectAll;

		private ToolStripMenuItem ContextDisconnect;

		private ToolStripSeparator toolStripSeparator29;

		private ToolStripSeparator toolStripSeparator30;

		private ToolStripMenuItem ProjectPassword;

		private ToolStripMenuItem FlowchartAllXP;

		private TabPage RulesPage;

		private SplitContainer RulesSplitter;

		private ListView RulesList;

		private ColumnHeader RulesHdr;

		private ToolStrip RulesToolbar;

		private ToolStripDropDownButton RulesAddBtn;

		private ToolStripMenuItem AddRace;

		private ToolStripSeparator toolStripSeparator31;

		private ToolStripMenuItem AddClass;

		private ToolStripMenuItem AddParagonPath;

		private ToolStripMenuItem AddEpicDestiny;

		private ToolStripSeparator toolStripSeparator32;

		private ToolStripMenuItem AddBackground;

		private ToolStripMenuItem AddFeat;

		private ToolStripMenuItem AddWeapon;

		private ToolStripMenuItem AddRitual;

		private Panel RulesBrowserPanel;

		private WebBrowser RulesBrowser;

		private ToolStripSeparator toolStripSeparator33;

		private WebBrowser Preview;

		private ToolStripMenuItem ProjectCampaignSettings;

		private ToolStripMenuItem PlotPointExportFile;

		private ToolStripSeparator toolStripSeparator35;

		internal Masterplan.Controls.PlotView PlotView;

		private ToolStripMenuItem ProjectRegionalMaps;

		private ToolStripSeparator toolStripSeparator37;

		private ToolStripButton NoteCategoryBtn;

		private ToolStripSeparator toolStripSeparator38;

		private ToolStripSeparator toolStripSeparator39;

		private ToolStripMenuItem AddCreatureLore;

		private ToolStripMenuItem AddDisease;

		private ToolStripMenuItem AddPoison;

		private ToolStripDropDownButton EncShareBtn;

		private ToolStripMenuItem EncShareExport;

		private ToolStripMenuItem EncShareImport;

		private ToolStripSeparator toolStripMenuItem6;

		private ToolStripMenuItem EncSharePublish;

		private ToolStripSeparator toolStripSeparator40;

		private ToolStripSeparator toolStripMenuItem8;

		private ToolStripMenuItem HelpFacebook;

		private ToolStripMenuItem HelpTwitter;

		private ToolStripMenuItem AddTheme;

		private ToolStripDropDownButton RulesShareBtn;

		private ToolStripMenuItem RulesShareExport;

		private ToolStripMenuItem RulesShareImport;

		private ToolStripSeparator toolStripMenuItem9;

		private ToolStripMenuItem RulesSharePublish;

		private ToolStripSeparator toolStripSeparator41;

		private ToolStripMenuItem FileAdvanced;

		private ToolStripMenuItem AdvancedDelve;

		private ToolStripSeparator toolStripSeparator42;

		private ToolStrip EncEntryToolbar;

		private ToolStripButton RulesRemoveBtn;

		private ToolStripButton RulesEditBtn;

		private ToolStripSeparator toolStripSeparator36;

		private ToolStripButton RulesPlayerViewBtn;

		private ToolStripSeparator toolStripSeparator43;

		private ToolStripButton RuleEncyclopediaBtn;

		private TabPage ReferencePage;

		private SplitContainer ReferenceSplitter;

		private TabControl ReferencePages;

		private TabPage PartyPage;

		private WebBrowser PartyBrowser;

		private TabPage ToolsPage;

		private WebBrowser GeneratorBrowser;

		private ToolStrip GeneratorToolbar;

		private ToolStripButton NPCBtn;

		private ToolStripButton RoomBtn;

		private ToolStripButton TreasureBtn;

		private Panel ToolBrowserPanel;

		private ToolStripButton BookTitleBtn;

		private ToolStripButton ExoticNameBtn;

		private ToolStripButton PotionBtn;

		private ToolStripLabel toolStripLabel1;

		private ToolStripSeparator toolStripSeparator26;

		private ToolStripSeparator toolStripSeparator44;

		private ToolStripMenuItem ViewLinks;

		private ToolStripMenuItem ViewLinksCurved;

		private ToolStripMenuItem ViewLinksAngled;

		private ToolStripMenuItem ViewLinksStraight;

		private ToolStripButton ElfNameBtn;

		private ToolStripButton DwarfNameBtn;

		private ToolStripButton HalflingNameBtn;

		private ToolStripSeparator toolStripSeparator45;

		private ToolStripSeparator toolStripSeparator46;

		private ToolStripButton DwarfTextBtn;

		private ToolStripButton ElfTextBtn;

		private ToolStripButton PrimordialTextBtn;

		private SplitContainer EncyclopediaEntrySplitter;

		private ListView EntryImageList;

		private TabPage CompendiumPage;

		private WebBrowser CompendiumBrowser;

		private ToolStripMenuItem HelpTutorials;

		private ToolStripSeparator toolStripSeparator47;

		private ToolStripSeparator toolStripSeparator34;

		private ToolStripMenuItem ToolsIssues;

		private Masterplan.Controls.InfoPanel InfoPanel;

		private ToolStrip ReferenceToolbar;

		private ToolStripButton DieRollerBtn;

		private ToolStripMenuItem ToolsExportLoot;

		private ToolStripMenuItem AdvancedSample;

		private ToolStripDropDownButton AdvancedBtn;

		private ToolStripMenuItem PlotAdvancedTreasure;

		private ToolStripMenuItem PlotAdvancedIssues;

		private ToolStripMenuItem PlotAdvancedDifficulty;

		private ToolStripSeparator toolStripSeparator48;

		private ToolStripDropDownButton BackgroundShareBtn;

		private ToolStripMenuItem BackgroundShareExport;

		private ToolStripMenuItem BackgroundShareImport;

		private ToolStripSeparator toolStripMenuItem10;

		private ToolStripMenuItem BackgroundSharePublish;

		private ToolStripDropDownButton EncAddBtn;

		private ToolStripMenuItem EncAddEntry;

		private ToolStripMenuItem EncAddGroup;

		private ToolStripMenuItem ToolsPowerStats;

		private ToolStripMenuItem ToolsTileChecklist;

		private ToolStripMenuItem ToolsMiniChecklist;

		private ToolStripSeparator toolStripSeparator49;

		private WelcomePanel fWelcome;

		private ExtensibilityManager fExtensibility;

		private bool fUpdating;

		private MainForm.ViewType fView;

		private MapView fDelveView;

		private RegionalMapPanel fMapView;

		private string fDownloadedFile = "";

		private string fPartyBreakdownSecondary = "";

		public List<Attachment> SelectedAttachments
		{
			get
			{
				List<Attachment> attachments = new List<Attachment>();
				foreach (ListViewItem selectedItem in this.AttachmentList.SelectedItems)
				{
					Attachment tag = selectedItem.Tag as Attachment;
					if (tag == null)
					{
						continue;
					}
					attachments.Add(tag);
				}
				return attachments;
			}
		}

		public Background SelectedBackground
		{
			get
			{
				if (this.BackgroundList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.BackgroundList.SelectedItems[0].Tag as Background;
			}
			set
			{
				this.BackgroundList.SelectedItems.Clear();
				if (value != null)
				{
					foreach (ListViewItem item in this.BackgroundList.Items)
					{
						Background tag = item.Tag as Background;
						if (tag == null || !(tag.ID == value.ID))
						{
							continue;
						}
						item.Selected = true;
					}
				}
				this.update_background_item();
			}
		}

		public EncyclopediaImage SelectedEncyclopediaImage
		{
			get
			{
				if (this.EntryImageList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.EntryImageList.SelectedItems[0].Tag as EncyclopediaImage;
			}
		}

		public IEncyclopediaItem SelectedEncyclopediaItem
		{
			get
			{
				if (this.EntryList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.EntryList.SelectedItems[0].Tag as IEncyclopediaItem;
			}
			set
			{
				this.EntryList.SelectedItems.Clear();
				if (value != null)
				{
					foreach (ListViewItem item in this.EntryList.Items)
					{
						IEncyclopediaItem tag = item.Tag as IEncyclopediaItem;
						if (tag == null || !(tag.ID == value.ID))
						{
							continue;
						}
						item.Selected = true;
					}
				}
				this.update_encyclopedia_entry();
			}
		}

		public IIssue SelectedIssue
		{
			get
			{
				if (this.NoteList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.NoteList.SelectedItems[0].Tag as IIssue;
			}
		}

		public Note SelectedNote
		{
			get
			{
				if (this.NoteList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.NoteList.SelectedItems[0].Tag as Note;
			}
			set
			{
				this.NoteList.SelectedItems.Clear();
				if (value != null)
				{
					foreach (ListViewItem item in this.NoteList.Items)
					{
						Note tag = item.Tag as Note;
						if (tag == null || !(tag.ID == value.ID))
						{
							continue;
						}
						item.Selected = true;
					}
				}
			}
		}

		public IPlayerOption SelectedRule
		{
			get
			{
				if (this.RulesList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.RulesList.SelectedItems[0].Tag as IPlayerOption;
			}
			set
			{
				this.RulesList.SelectedItems.Clear();
				if (value != null)
				{
					foreach (ListViewItem item in this.RulesList.Items)
					{
						IPlayerOption tag = item.Tag as IPlayerOption;
						if (tag == null || !(tag.ID == value.ID))
						{
							continue;
						}
						item.Selected = true;
					}
				}
			}
		}

		public MainForm()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			try
			{
				this.Preview.DocumentText = "";
				this.BackgroundDetails.DocumentText = "";
				this.EntryDetails.DocumentText = "";
				this.RulesBrowser.DocumentText = "";
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
			try
			{
				this.fExtensibility = new ExtensibilityManager(this);
				foreach (IAddIn addIn in this.fExtensibility.AddIns)
				{
					foreach (IPage page in addIn.Pages)
					{
						TabPage tabPage = new TabPage(page.Name);
						this.Pages.TabPages.Add(tabPage);
						tabPage.Controls.Add(page.Control);
						page.Control.Dock = DockStyle.Fill;
					}
					foreach (IPage quickReferencePage in addIn.QuickReferencePages)
					{
						TabPage tabPage1 = new TabPage()
						{
							Text = quickReferencePage.Name
						};
						tabPage1.Controls.Add(quickReferencePage.Control);
						quickReferencePage.Control.Dock = DockStyle.Fill;
						this.ReferencePages.TabPages.Add(tabPage1);
					}
				}
			}
			catch (Exception exception1)
			{
				LogSystem.Trace(exception1);
			}
			try
			{
				if (Session.Project != null)
				{
					this.PlotView.Plot = Session.Project.Plot;
				}
				else
				{
					base.Controls.Clear();
					this.fWelcome = new WelcomePanel(Session.Preferences.ShowHeadlines)
					{
						Dock = DockStyle.Fill
					};
					this.fWelcome.NewProjectClicked += new EventHandler(this.Welcome_NewProjectClicked);
					this.fWelcome.OpenProjectClicked += new EventHandler(this.Welcome_OpenProjectClicked);
					this.fWelcome.OpenLastProjectClicked += new EventHandler(this.Welcome_OpenLastProjectClicked);
					this.fWelcome.DelveClicked += new EventHandler(this.Welcome_DelveClicked);
					this.fWelcome.PremadeClicked += new EventHandler(this.Welcome_PremadeClicked);
					base.Controls.Add(this.fWelcome);
					base.Controls.Add(this.MainMenu);
				}
			}
			catch (Exception exception2)
			{
				LogSystem.Trace(exception2);
			}
			try
			{
				this.NavigationSplitter.Panel1Collapsed = !Session.Preferences.ShowNavigation;
				this.PreviewSplitter.Panel2Collapsed = !Session.Preferences.ShowPreview;
				this.PlotView.LinkStyle = Session.Preferences.LinkStyle;
				this.WorkspaceSearchBar.Visible = false;
				this.update_encyclopedia_templates();
			}
			catch (Exception exception3)
			{
				LogSystem.Trace(exception3);
			}
			try
			{
				if (Session.Preferences.Maximised)
				{
					base.WindowState = FormWindowState.Maximized;
				}
				else if (!(Session.Preferences.Size != System.Drawing.Size.Empty) || !(Session.Preferences.Location != Point.Empty))
				{
					base.StartPosition = FormStartPosition.CenterScreen;
				}
				else
				{
					base.StartPosition = FormStartPosition.Manual;
					int width = base.Width;
					System.Drawing.Size size = Session.Preferences.Size;
					int num = Math.Max(width, size.Width);
					int height = base.Height;
					System.Drawing.Size size1 = Session.Preferences.Size;
					int num1 = Math.Max(height, size1.Height);
					base.Size = new System.Drawing.Size(num, num1);
					int left = base.Left;
					Point location = Session.Preferences.Location;
					int num2 = Math.Max(left, location.X);
					int top = base.Top;
					Point point = Session.Preferences.Location;
					int num3 = Math.Max(top, point.Y);
					base.Location = new Point(num2, num3);
				}
			}
			catch (Exception exception4)
			{
				LogSystem.Trace(exception4);
			}
			this.update_title();
			this.UpdateView();
		}

		private void add_attachment(string filename)
		{
			try
			{
				FileInfo fileInfo = new FileInfo(filename);
				Attachment attachment = new Attachment()
				{
					Name = fileInfo.Name,
					Contents = File.ReadAllBytes(filename)
				};
				Attachment attachment1 = Session.Project.FindAttachment(attachment.Name);
				if (attachment1 != null)
				{
					string str = string.Concat("An attachment with this name already exists.", Environment.NewLine);
					str = string.Concat(str, "Do you want to replace it?");
					System.Windows.Forms.DialogResult dialogResult = MessageBox.Show(str, "Masterplan", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
					{
						return;
					}
					else
					{
						switch (dialogResult)
						{
							case System.Windows.Forms.DialogResult.Yes:
							{
								Session.Project.Attachments.Remove(attachment1);
								break;
							}
							case System.Windows.Forms.DialogResult.No:
							{
								int num = 1;
								while (Session.Project.FindAttachment(attachment.Name) != null)
								{
									num++;
									object[] objArray = new object[] { FileName.Name(filename), " ", num, ".", FileName.Extension(filename) };
									attachment.Name = string.Concat(objArray);
								}
								break;
							}
						}
					}
				}
				Session.Project.Attachments.Add(attachment);
				Session.Project.Attachments.Sort();
				Session.Modified = true;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void add_between(object sender, EventArgs e)
		{
			try
			{
				Pair<PlotPoint, PlotPoint> tag = (sender as ToolStripMenuItem).Tag as Pair<PlotPoint, PlotPoint>;
				this.add_point(tag.First, tag.Second);
				this.UpdateView();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void add_breadcrumb(PlotPoint pp, bool link)
		{
			try
			{
				ToolStripLabel toolStripLabel = new ToolStripLabel((pp != null ? pp.Name : Session.Project.Name))
				{
					IsLink = link,
					Tag = pp
				};
				toolStripLabel.Click += new EventHandler(this.Breadcrumb_Click);
				this.BreadcrumbBar.Items.Add(toolStripLabel);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void add_in_command_clicked(object sender, EventArgs e)
		{
			try
			{
				((sender as ToolStripMenuItem).Tag as ICommand).Execute();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void add_navigation_node(PlotPoint pp, TreeNode parent)
		{
			try
			{
				string str = (pp != null ? pp.Name : Session.Project.Name);
				TreeNode treeNode = ((parent != null ? parent.Nodes : this.NavigationTree.Nodes)).Add(str);
				Plot plot = (pp != null ? pp.Subplot : Session.Project.Plot);
				treeNode.Tag = plot;
				if (this.PlotView.Plot == plot)
				{
					this.NavigationTree.SelectedNode = treeNode;
				}
				foreach (PlotPoint plotPoint in (pp != null ? pp.Subplot.Points : Session.Project.Plot.Points))
				{
					if (plotPoint.Subplot.Points.Count == 0 && plotPoint.Subplot != this.PlotView.Plot)
					{
						continue;
					}
					this.add_navigation_node(plotPoint, treeNode);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void add_point(PlotPoint lhs, PlotPoint rhs)
		{
			try
			{
				PlotPoint plotPoint = new PlotPoint("New Point");
				PlotPointForm plotPointForm = new PlotPointForm(plotPoint, this.PlotView.Plot, false);
				if (plotPointForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.PlotView.Plot.Points.Add(plotPointForm.PlotPoint);
					this.PlotView.RecalculateLayout();
					if (lhs != null && rhs != null)
					{
						lhs.Links.Remove(rhs.ID);
					}
					if (lhs != null)
					{
						lhs.Links.Add(plotPointForm.PlotPoint.ID);
					}
					if (rhs != null)
					{
						plotPointForm.PlotPoint.Links.Add(rhs.ID);
					}
					Session.Modified = true;
					this.UpdateView();
					this.PlotView.SelectedPoint = plotPointForm.PlotPoint;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void AddBackground_Click(object sender, EventArgs e)
		{
			OptionBackgroundForm optionBackgroundForm = new OptionBackgroundForm(new PlayerBackground()
			{
				Name = "New Background"
			});
			if (optionBackgroundForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionBackgroundForm.Background);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void AddBtn_Click(object sender, EventArgs e)
		{
			try
			{
				this.add_point(null, null);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void AddChallenge_Click(object sender, EventArgs e)
		{
			try
			{
				SkillChallenge skillChallenge = new SkillChallenge()
				{
					Name = "Unnamed Skill Challenge",
					Level = Session.Project.Party.Level
				};
				PlotPoint plotPoint = new PlotPoint("New Skill Challenge Point")
				{
					Element = skillChallenge
				};
				PlotPointForm plotPointForm = new PlotPointForm(plotPoint, this.PlotView.Plot, true);
				if (plotPointForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.PlotView.Plot.Points.Add(plotPointForm.PlotPoint);
					this.PlotView.RecalculateLayout();
					Session.Modified = true;
					this.UpdateView();
					this.PlotView.SelectedPoint = plotPointForm.PlotPoint;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void AddClass_Click(object sender, EventArgs e)
		{
			Class @class = new Class()
			{
				Name = "New Class"
			};
			for (int i = 1; i <= 30; i++)
			{
				LevelData levelDatum = new LevelData()
				{
					Level = i
				};
				@class.Levels.Add(levelDatum);
			}
			OptionClassForm optionClassForm = new OptionClassForm(@class);
			if (optionClassForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionClassForm.Class);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void AddCreatureLore_Click(object sender, EventArgs e)
		{
			CreatureLore creatureLore = new CreatureLore()
			{
				Name = "Creature",
				SkillName = "Nature"
			};
			OptionCreatureLoreForm optionCreatureLoreForm = new OptionCreatureLoreForm(creatureLore);
			if (optionCreatureLoreForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionCreatureLoreForm.CreatureLore);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void AddDisease_Click(object sender, EventArgs e)
		{
			OptionDiseaseForm optionDiseaseForm = new OptionDiseaseForm(new Disease()
			{
				Name = "New Disease"
			});
			if (optionDiseaseForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionDiseaseForm.Disease);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void AddEncounter_Click(object sender, EventArgs e)
		{
			try
			{
				Encounter encounter = new Encounter();
				encounter.SetStandardEncounterNotes();
				PlotPoint plotPoint = new PlotPoint("New Encounter Point")
				{
					Element = encounter
				};
				PlotPointForm plotPointForm = new PlotPointForm(plotPoint, this.PlotView.Plot, true);
				if (plotPointForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.PlotView.Plot.Points.Add(plotPointForm.PlotPoint);
					this.PlotView.RecalculateLayout();
					Session.Modified = true;
					this.UpdateView();
					this.PlotView.SelectedPoint = plotPointForm.PlotPoint;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void AddEpicDestiny_Click(object sender, EventArgs e)
		{
			EpicDestiny epicDestiny = new EpicDestiny()
			{
				Name = "New Epic Destiny"
			};
			for (int i = 21; i <= 30; i++)
			{
				LevelData levelDatum = new LevelData()
				{
					Level = i
				};
				epicDestiny.Levels.Add(levelDatum);
			}
			OptionEpicDestinyForm optionEpicDestinyForm = new OptionEpicDestinyForm(epicDestiny);
			if (optionEpicDestinyForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionEpicDestinyForm.EpicDestiny);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void AddFeat_Click(object sender, EventArgs e)
		{
			OptionFeatForm optionFeatForm = new OptionFeatForm(new Feat()
			{
				Name = "New Feat"
			});
			if (optionFeatForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionFeatForm.Feat);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void AddParagonPath_Click(object sender, EventArgs e)
		{
			ParagonPath paragonPath = new ParagonPath()
			{
				Name = "New Paragon Path"
			};
			for (int i = 11; i <= 20; i++)
			{
				LevelData levelDatum = new LevelData()
				{
					Level = i
				};
				paragonPath.Levels.Add(levelDatum);
			}
			OptionParagonPathForm optionParagonPathForm = new OptionParagonPathForm(paragonPath);
			if (optionParagonPathForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionParagonPathForm.ParagonPath);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void AddPoison_Click(object sender, EventArgs e)
		{
			OptionPoisonForm optionPoisonForm = new OptionPoisonForm(new Poison()
			{
				Name = "New Poison"
			});
			if (optionPoisonForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionPoisonForm.Poison);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void AddQuest_Click(object sender, EventArgs e)
		{
			try
			{
				PlotPoint plotPoint = new PlotPoint("New Quest Point")
				{
					Element = new Quest()
				};
				PlotPointForm plotPointForm = new PlotPointForm(plotPoint, this.PlotView.Plot, true);
				if (plotPointForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.PlotView.Plot.Points.Add(plotPointForm.PlotPoint);
					this.PlotView.RecalculateLayout();
					Session.Modified = true;
					this.UpdateView();
					this.PlotView.SelectedPoint = plotPointForm.PlotPoint;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void AddRace_Click(object sender, EventArgs e)
		{
			OptionRaceForm optionRaceForm = new OptionRaceForm(new Race()
			{
				Name = "New Race"
			});
			if (optionRaceForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionRaceForm.Race);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void AddRitual_Click(object sender, EventArgs e)
		{
			OptionRitualForm optionRitualForm = new OptionRitualForm(new Ritual()
			{
				Name = "New Ritual"
			});
			if (optionRitualForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionRitualForm.Ritual);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void AddTheme_Click(object sender, EventArgs e)
		{
			Theme theme = new Theme()
			{
				Name = "New Theme"
			};
			for (int i = 1; i <= 10; i++)
			{
				LevelData levelDatum = new LevelData()
				{
					Level = i
				};
				theme.Levels.Add(levelDatum);
			}
			OptionThemeForm optionThemeForm = new OptionThemeForm(theme);
			if (optionThemeForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionThemeForm.Theme);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void AddTrap_Click(object sender, EventArgs e)
		{
			try
			{
				TrapElement trapElement = new TrapElement();
				trapElement.Trap.Name = "Unnamed Trap";
				trapElement.Trap.Level = Session.Project.Party.Level;
				PlotPoint plotPoint = new PlotPoint("New Trap / Hazard Point")
				{
					Element = trapElement
				};
				PlotPointForm plotPointForm = new PlotPointForm(plotPoint, this.PlotView.Plot, true);
				if (plotPointForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.PlotView.Plot.Points.Add(plotPointForm.PlotPoint);
					this.PlotView.RecalculateLayout();
					Session.Modified = true;
					this.UpdateView();
					this.PlotView.SelectedPoint = plotPointForm.PlotPoint;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void AddWeapon_Click(object sender, EventArgs e)
		{
			OptionWeaponForm optionWeaponForm = new OptionWeaponForm(new Weapon()
			{
				Name = "New Weapon"
			});
			if (optionWeaponForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionWeaponForm.Weapon);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void AdvancedDelve_Click(object sender, EventArgs e)
		{
			try
			{
				Project project = Session.Project;
				string fileName = Session.FileName;
				MainForm.ViewType viewType = this.fView;
				if (!this.create_delve())
				{
					Session.Project = project;
					Session.FileName = fileName;
					this.fView = viewType;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void AdvancedSample_Click(object sender, EventArgs e)
		{
			try
			{
				Project project = Session.Project;
				string fileName = Session.FileName;
				this.Welcome_PremadeClicked(sender, e);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			try
			{
				if (this.Pages.SelectedTab == this.WorkspacePage)
				{
					PlotPoint selectedPoint = this.get_selected_point();
					this.RemoveBtn.Enabled = selectedPoint != null;
					this.PlotCutBtn.Enabled = selectedPoint != null;
					this.PlotCopyBtn.Enabled = selectedPoint != null;
					this.PlotPasteBtn.Enabled = (Clipboard.ContainsData(typeof(PlotPoint).ToString()) ? true : Clipboard.ContainsText());
					this.SearchBtn.Checked = this.WorkspaceSearchBar.Visible;
					this.PlotClearBtn.Visible = this.PlotSearchBox.Text != "";
					this.EditBtn.Enabled = selectedPoint != null;
					this.ExploreBtn.Enabled = selectedPoint != null;
					this.PlotPointMenu.Enabled = selectedPoint != null;
					this.PlotPointPlayerView.Enabled = (selectedPoint == null ? false : selectedPoint.ReadAloud != "");
					this.PlotPointExportHTML.Enabled = selectedPoint != null;
					this.ContextRemove.Enabled = this.RemoveBtn.Enabled;
					this.ContextEdit.Enabled = this.EditBtn.Enabled;
					this.ContextExplore.Enabled = this.EditBtn.Enabled;
					this.ContextState.Enabled = selectedPoint != null;
					this.FlowchartAllXP.Checked = Session.Preferences.AllXP;
				}
				if (this.Pages.SelectedTab == this.BackgroundPage)
				{
					this.BackgroundRemoveBtn.Enabled = this.SelectedBackground != null;
					this.BackgroundEditBtn.Enabled = this.SelectedBackground != null;
					this.BackgroundUpBtn.Enabled = (this.SelectedBackground == null ? false : Session.Project.Backgrounds.IndexOf(this.SelectedBackground) != 0);
					this.BackgroundDownBtn.Enabled = (this.SelectedBackground == null ? false : Session.Project.Backgrounds.IndexOf(this.SelectedBackground) != Session.Project.Backgrounds.Count - 1);
					this.BackgroundPlayerViewSelected.Enabled = (this.SelectedBackground == null ? false : this.SelectedBackground.Details != "");
					this.BackgroundPlayerViewAll.Enabled = (Session.Project == null ? false : Session.Project.Backgrounds.Count != 0);
				}
				if (this.Pages.SelectedTab == this.EncyclopediaPage)
				{
					this.EncAddGroup.Enabled = (Session.Project == null ? false : Session.Project.Encyclopedia.Entries.Count != 0);
					this.EncRemoveBtn.Enabled = this.SelectedEncyclopediaItem != null;
					this.EncEditBtn.Enabled = this.SelectedEncyclopediaItem != null;
					this.EncCutBtn.Enabled = (this.SelectedEncyclopediaItem == null ? false : this.SelectedEncyclopediaItem is EncyclopediaEntry);
					this.EncCopyBtn.Enabled = (this.SelectedEncyclopediaItem == null ? false : this.SelectedEncyclopediaItem is EncyclopediaEntry);
					this.EncPasteBtn.Enabled = (Clipboard.ContainsData(typeof(EncyclopediaEntry).ToString()) ? true : Clipboard.ContainsText());
					this.EncPlayerView.Enabled = this.SelectedEncyclopediaItem != null;
					this.EncShareExport.Enabled = (Session.Project == null ? false : Session.Project.Encyclopedia.Entries.Count != 0);
					this.EncSharePublish.Enabled = (Session.Project == null ? false : Session.Project.Encyclopedia.Entries.Count != 0);
					this.EncClearLbl.Visible = this.EncSearchBox.Text != "";
				}
				if (this.Pages.SelectedTab == this.RulesPage)
				{
					this.RulesRemoveBtn.Enabled = this.SelectedRule != null;
					this.RulesEditBtn.Enabled = this.SelectedRule != null;
					this.RulesPlayerViewBtn.Enabled = this.SelectedRule != null;
					this.RuleEncyclopediaBtn.Enabled = this.SelectedRule != null;
					this.RulesShareExport.Enabled = (Session.Project == null ? false : Session.Project.PlayerOptions.Count != 0);
					this.RulesSharePublish.Enabled = (Session.Project == null ? false : Session.Project.PlayerOptions.Count != 0);
				}
				if (this.Pages.SelectedTab == this.AttachmentsPage)
				{
					this.AttachmentImportBtn.Enabled = true;
					this.AttachmentRemoveBtn.Enabled = this.SelectedAttachments.Count != 0;
					this.AttachmentExtract.Enabled = this.SelectedAttachments.Count != 0;
					this.AttachmentPlayerView.Enabled = (this.SelectedAttachments.Count != 1 ? false : this.SelectedAttachments[0].Type != AttachmentType.Miscellaneous);
				}
				if (this.Pages.SelectedTab == this.JotterPage)
				{
					this.NoteRemoveBtn.Enabled = this.SelectedNote != null;
					this.NoteCategoryBtn.Enabled = this.SelectedNote != null;
					this.NoteCutBtn.Enabled = this.SelectedNote != null;
					this.NoteCopyBtn.Enabled = this.SelectedNote != null;
					this.NotePasteBtn.Enabled = (Clipboard.ContainsData(typeof(Note).ToString()) ? true : Clipboard.ContainsText());
					this.NoteClearLbl.Visible = this.NoteSearchBox.Text != "";
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void AttachmentExtractAndRun_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (Attachment selectedAttachment in this.SelectedAttachments)
				{
					this.extract_attachment(selectedAttachment, true);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void AttachmentExtractSimple_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (Attachment selectedAttachment in this.SelectedAttachments)
				{
					this.extract_attachment(selectedAttachment, false);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void AttachmentImportBtn_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog()
				{
					Filter = "All Files|*.*",
					Multiselect = true
				};
				if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					string[] fileNames = openFileDialog.FileNames;
					for (int i = 0; i < (int)fileNames.Length; i++)
					{
						this.add_attachment(fileNames[i]);
					}
					this.update_attachments();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void AttachmentList_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				string[] data = e.Data.GetData("FileDrop") as string[];
				if (data != null)
				{
					string[] strArrays = data;
					for (int i = 0; i < (int)strArrays.Length; i++)
					{
						this.add_attachment(strArrays[i]);
					}
					this.update_attachments();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void AttachmentList_DragOver(object sender, DragEventArgs e)
		{
			try
			{
				if (e.Data.GetData("FileDrop") is string[])
				{
					e.Effect = DragDropEffects.All;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void AttachmentRemoveBtn_Click(object sender, EventArgs e)
		{
			try
			{
				List<Attachment> selectedAttachments = this.SelectedAttachments;
				if (selectedAttachments.Count != 0)
				{
					if (MessageBox.Show(string.Concat(string.Concat("You are about to remove one or more attachments from this project.", Environment.NewLine), "Are you sure you want to do this?"), "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
					{
						foreach (Attachment selectedAttachment in selectedAttachments)
						{
							Session.Project.Attachments.Remove(selectedAttachment);
						}
						Session.Modified = true;
						this.update_attachments();
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

		private void AttachmentSendBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedAttachments.Count == 1)
				{
					Attachment item = this.SelectedAttachments[0];
					if (item.Type != AttachmentType.Miscellaneous)
					{
						if (Session.PlayerView == null)
						{
							Session.PlayerView = new PlayerViewForm(this);
						}
						if (item.Type == AttachmentType.PlainText)
						{
							Session.PlayerView.ShowPlainText(item);
						}
						if (item.Type == AttachmentType.RichText)
						{
							Session.PlayerView.ShowRichText(item);
						}
						if (item.Type == AttachmentType.Image)
						{
							Session.PlayerView.ShowImage(item);
						}
						if (item.Type == AttachmentType.URL)
						{
							Session.PlayerView.ShowWebPage(item);
						}
						if (item.Type == AttachmentType.HTML)
						{
							Session.PlayerView.ShowWebPage(item);
						}
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void BackgroundAddBtn_Click(object sender, EventArgs e)
		{
			try
			{
				BackgroundForm backgroundForm = new BackgroundForm(new Background("New Background Item"));
				if (backgroundForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.Backgrounds.Add(backgroundForm.Background);
					Session.Modified = true;
					this.update_background_list();
					this.SelectedBackground = backgroundForm.Background;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void BackgroundDetails_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			try
			{
				if (e.Url.Scheme == "background")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "edit")
					{
						this.BackgroundEditBtn_Click(sender, e);
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void BackgroundDownBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedBackground != null && Session.Project.Backgrounds.IndexOf(this.SelectedBackground) != Session.Project.Backgrounds.Count - 1)
				{
					int selectedBackground = Session.Project.Backgrounds.IndexOf(this.SelectedBackground);
					Background item = Session.Project.Backgrounds[selectedBackground + 1];
					Session.Project.Backgrounds[selectedBackground + 1] = this.SelectedBackground;
					Session.Project.Backgrounds[selectedBackground] = item;
					Session.Modified = true;
					this.update_background_list();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void BackgroundEditBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedBackground != null)
				{
					int background = Session.Project.Backgrounds.IndexOf(this.SelectedBackground);
					BackgroundForm backgroundForm = new BackgroundForm(this.SelectedBackground);
					if (backgroundForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						Session.Project.Backgrounds[background] = backgroundForm.Background;
						Session.Modified = true;
						this.update_background_list();
						this.SelectedBackground = backgroundForm.Background;
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void BackgroundList_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				this.update_background_item();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void BackgroundPlayerViewAll_Click(object sender, EventArgs e)
		{
			try
			{
				if (Session.PlayerView == null)
				{
					Session.PlayerView = new PlayerViewForm(this);
				}
				Session.PlayerView.ShowBackground(Session.Project.Backgrounds);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void BackgroundPlayerViewSelected_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedBackground != null)
				{
					if (Session.PlayerView == null)
					{
						Session.PlayerView = new PlayerViewForm(this);
					}
					Session.PlayerView.ShowBackground(this.SelectedBackground);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void BackgroundRemoveBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedBackground != null)
				{
					if (MessageBox.Show("Are you sure you want to delete this background?", "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.No)
					{
						Session.Project.Backgrounds.Remove(this.SelectedBackground);
						Session.Modified = true;
						this.update_background_list();
						this.SelectedBackground = null;
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

		private void BackgroundShareExport_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog()
			{
				Filter = Program.BackgroundFilter,
				FileName = Session.Project.Name
			};
			if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Serialisation<List<Background>>.Save(saveFileDialog.FileName, Session.Project.Backgrounds, SerialisationMode.XML);
			}
		}

		private void BackgroundShareImport_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Filter = Program.BackgroundFilter
			};
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				List<Background> backgrounds = Serialisation<List<Background>>.Load(openFileDialog.FileName, SerialisationMode.XML);
				Session.Project.Backgrounds.AddRange(backgrounds);
				Session.Modified = true;
				this.UpdateView();
			}
		}

		private void BackgroundSharePublish_Click(object sender, EventArgs e)
		{
			HandoutForm handoutForm = new HandoutForm();
			handoutForm.AddBackgroundEntries();
			handoutForm.ShowDialog();
		}

		private void BackgroundUpBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedBackground != null && Session.Project.Backgrounds.IndexOf(this.SelectedBackground) != 0)
				{
					int selectedBackground = Session.Project.Backgrounds.IndexOf(this.SelectedBackground);
					Background item = Session.Project.Backgrounds[selectedBackground - 1];
					Session.Project.Backgrounds[selectedBackground - 1] = this.SelectedBackground;
					Session.Project.Backgrounds[selectedBackground] = item;
					Session.Modified = true;
					this.update_background_list();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void BookTitleBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<H3>Book Titles</H3>");
			for (int i = 0; i != 10; i++)
			{
				head.Add(string.Concat("<P>", Book.Title(), "</P>"));
			}
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void Breadcrumb_Click(object sender, EventArgs e)
		{
			try
			{
				PlotPoint tag = (sender as ToolStripLabel).Tag as PlotPoint;
				if (tag != null)
				{
					this.PlotView.Plot = tag.Subplot;
					this.UpdateView();
				}
				else
				{
					this.PlotView.Plot = Session.Project.Plot;
					this.UpdateView();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private bool check_modified()
		{
			bool flag;
			try
			{
				if (Session.Modified)
				{
					System.Windows.Forms.DialogResult dialogResult = MessageBox.Show("The project has been modified.\nDo you want to save it now?", "Masterplan", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
					{
						flag = false;
						return flag;
					}
					else
					{
						switch (dialogResult)
						{
							case System.Windows.Forms.DialogResult.Yes:
							{
								if (Session.FileName == "")
								{
									SaveFileDialog saveFileDialog = new SaveFileDialog()
									{
										Filter = Program.ProjectFilter,
										FileName = Session.Project.Name
									};
									if (saveFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
									{
										flag = false;
										return flag;
									}
									else
									{
										GC.Collect();
										Session.Project.PopulateProjectLibrary();
										bool flag1 = Serialisation<Project>.Save(saveFileDialog.FileName, Session.Project, SerialisationMode.Binary);
										Session.Project.SimplifyProjectLibrary();
										if (flag1)
										{
											Session.FileName = saveFileDialog.FileName;
											Session.Modified = false;
											break;
										}
										else
										{
											flag = false;
											return flag;
										}
									}
								}
								else
								{
									GC.Collect();
									Session.Project.PopulateProjectLibrary();
									bool flag2 = Serialisation<Project>.Save(Session.FileName, Session.Project, SerialisationMode.Binary);
									Session.Project.SimplifyProjectLibrary();
									if (flag2)
									{
										Session.Modified = false;
										break;
									}
									else
									{
										flag = false;
										return flag;
									}
								}
							}
						}
					}
				}
				return true;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
				return true;
			}
			return flag;
		}

		private void ClearBtn_Click(object sender, EventArgs e)
		{
			try
			{
				this.PlotSearchBox.Text = "";
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ContextAdd_Click(object sender, EventArgs e)
		{
			try
			{
				this.add_point(this.PlotView.SelectedPoint, null);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ContextDisconnectAll_Click(object sender, EventArgs e)
		{
			try
			{
				this.PlotView.SelectedPoint.Links.Clear();
				Guid d = this.PlotView.SelectedPoint.ID;
				foreach (PlotPoint point in this.PlotView.Plot.Points)
				{
					while (point.Links.Contains(d))
					{
						point.Links.Remove(d);
					}
				}
				this.PlotView.RecalculateLayout();
				Session.Modified = true;
				this.update_workspace();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ContextStateCompleted_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlotView.SelectedPoint != null)
				{
					foreach (PlotPoint subtree in this.PlotView.SelectedPoint.Subtree)
					{
						subtree.State = PlotPointState.Completed;
					}
					Session.Modified = true;
					this.update_workspace();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ContextStateNormal_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlotView.SelectedPoint != null)
				{
					foreach (PlotPoint subtree in this.PlotView.SelectedPoint.Subtree)
					{
						subtree.State = PlotPointState.Normal;
					}
					Session.Modified = true;
					this.update_workspace();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ContextStateSkipped_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlotView.SelectedPoint != null)
				{
					foreach (PlotPoint subtree in this.PlotView.SelectedPoint.Subtree)
					{
						subtree.State = PlotPointState.Skipped;
					}
					Session.Modified = true;
					this.update_workspace();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void CopyBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlotView.SelectedPoint != null)
				{
					Clipboard.SetData(typeof(PlotPoint).ToString(), this.PlotView.SelectedPoint.Copy());
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private bool create_delve()
		{
			this.FileNew_Click(null, null);
			if (Session.Project == null)
			{
				return false;
			}
			Map map = new Map()
			{
				Name = "Random Dungeon"
			};
			MapBuilderForm mapBuilderForm = new MapBuilderForm(map, true);
			if (mapBuilderForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
			{
				return false;
			}
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			map = mapBuilderForm.Map;
			PlotPoint plotPoint = DelveBuilder.AutoBuild(map, new AutoBuildData());
			if (plotPoint == null)
			{
				return false;
			}
			Session.Project.Maps.Add(map);
			foreach (PlotPoint point in plotPoint.Subplot.Points)
			{
				Session.Project.Plot.Points.Add(point);
			}
			Session.Modified = true;
			this.UpdateView();
			this.delve_view(map);
			System.Windows.Forms.Cursor.Current = Cursors.Default;
			return true;
		}

		private EncyclopediaEntry create_entry(string name, string content)
		{
			try
			{
				EncyclopediaEntry encyclopediaEntry = new EncyclopediaEntry()
				{
					Name = name,
					Details = content
				};
				EncyclopediaEntryForm encyclopediaEntryForm = new EncyclopediaEntryForm(encyclopediaEntry);
				if (encyclopediaEntryForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.Encyclopedia.Entries.Add(encyclopediaEntryForm.Entry);
					Session.Project.Encyclopedia.Entries.Sort();
					Session.Modified = true;
					return encyclopediaEntryForm.Entry;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
			return null;
		}

		private void CutBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlotView.SelectedPoint != null)
				{
					Clipboard.SetData(typeof(PlotPoint).ToString(), this.PlotView.SelectedPoint.Copy());
					this.PlotView.Plot.RemovePoint(this.PlotView.SelectedPoint);
					this.PlotView.RecalculateLayout();
					this.PlotView.SelectedPoint = null;
					Session.Modified = true;
					this.PlotView.Invalidate();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void delve_view(Map map)
		{
			if (map == null)
			{
				return;
			}
			foreach (Control control in this.PreviewSplitter.Panel1.Controls)
			{
				control.Visible = false;
			}
			MapView mapView = new MapView()
			{
				Map = map,
				Plot = this.PlotView.Plot,
				Mode = MapViewMode.Thumbnail,
				HighlightAreas = true,
				LineOfSight = false,
				BorderSize = 1,
				BorderStyle = BorderStyle.FixedSingle,
				Dock = DockStyle.Fill
			};
			this.PreviewSplitter.Panel1.Controls.Add(mapView);
			mapView.AreaSelected += new MapAreaEventHandler(this.select_maparea);
			mapView.DoubleClick += new EventHandler(this.edit_maparea);
			this.fDelveView = mapView;
			this.fView = MainForm.ViewType.Delve;
			this.update_preview();
		}

		private void delve_view_edit()
		{
			Map map = this.fDelveView.Map;
			int num = Session.Project.Maps.IndexOf(map);
			MapBuilderForm mapBuilderForm = new MapBuilderForm(map, false);
			if (mapBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Project.Maps[num] = mapBuilderForm.Map;
				Session.Modified = true;
				this.fDelveView.Map = mapBuilderForm.Map;
			}
		}

		private void DieRollerBtn_Click(object sender, EventArgs e)
		{
			(new DieRollerForm()).ShowDialog();
		}

		private void disconnect_points(object sender, EventArgs e)
		{
			try
			{
				Pair<PlotPoint, PlotPoint> tag = (sender as ToolStripMenuItem).Tag as Pair<PlotPoint, PlotPoint>;
				Guid d = tag.Second.ID;
				while (tag.First.Links.Contains(d))
				{
					tag.First.Links.Remove(d);
				}
				this.PlotView.RecalculateLayout();
				Session.Modified = true;
				this.update_workspace();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
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

		private void DwarfNameBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<H3>Dwarvish Names</H3>");
			head.Add("<P class=instruction>Click on any name to create an encyclopedia entry for it.</P>");
			head.Add("<P class=table>");
			head.Add("<TABLE>");
			head.Add("<TR class=heading>");
			head.Add("<TD><B>Male</B></TD>");
			head.Add("<TD><B>Female</B></TD>");
			head.Add("</TR>");
			for (int i = 0; i != 10; i++)
			{
				string str = DwarfName.MaleName();
				string str1 = DwarfName.FemaleName();
				head.Add("<TR>");
				head.Add("<TD>");
				string[] strArrays = new string[] { "<P><A href=entry:", str.Replace(" ", "%20"), ">", str, "</A></P>" };
				head.Add(string.Concat(strArrays));
				head.Add("</TD>");
				head.Add("<TD>");
				string[] strArrays1 = new string[] { "<P><A href=entry:", str1.Replace(" ", "%20"), ">", str1, "</A></P>" };
				head.Add(string.Concat(strArrays1));
				head.Add("</TD>");
				head.Add("</TR>");
			}
			head.Add("</TABLE>");
			head.Add("</P>");
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void DwarfTextBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			int num = Session.Dice(1, 6);
			for (int i = 0; i != num; i++)
			{
				head.Add(string.Concat("<P>", DwarfName.Sentence(), "</P>"));
			}
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		private bool edit_element(object sender, EventArgs e)
		{
			bool flag;
			try
			{
				PlotPoint selectedPoint = this.get_selected_point();
				if (selectedPoint != null)
				{
					int partyLevel = Workspace.GetPartyLevel(selectedPoint);
					Encounter element = selectedPoint.Element as Encounter;
					if (element != null)
					{
						EncounterBuilderForm encounterBuilderForm = new EncounterBuilderForm(element, partyLevel, false);
						if (encounterBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						{
							selectedPoint.Element = encounterBuilderForm.Encounter;
							Session.Modified = true;
							this.UpdateView();
							flag = true;
							return flag;
						}
					}
					SkillChallenge skillChallenge = selectedPoint.Element as SkillChallenge;
					if (skillChallenge != null)
					{
						SkillChallengeBuilderForm skillChallengeBuilderForm = new SkillChallengeBuilderForm(skillChallenge);
						if (skillChallengeBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						{
							selectedPoint.Element = skillChallengeBuilderForm.SkillChallenge;
							Session.Modified = true;
							this.UpdateView();
							flag = true;
							return flag;
						}
					}
					TrapElement trap = selectedPoint.Element as TrapElement;
					if (trap != null)
					{
						TrapBuilderForm trapBuilderForm = new TrapBuilderForm(trap.Trap);
						if (trapBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						{
							trap.Trap = trapBuilderForm.Trap;
							Session.Modified = true;
							this.UpdateView();
							flag = true;
							return flag;
						}
					}
				}
				return false;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
				return false;
			}
			return flag;
		}

		private void edit_map_area(Map map, MapArea map_area, MapView mapview)
		{
			try
			{
				if (map != null && map_area != null)
				{
					int area = map.Areas.IndexOf(map_area);
					MapAreaForm mapAreaForm = new MapAreaForm(map_area, map);
					if (mapAreaForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						map.Areas[area] = mapAreaForm.Area;
						Session.Modified = true;
						if (mapview != null)
						{
							mapview.SelectedArea = mapAreaForm.Area;
						}
						this.UpdateView();
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void edit_map_location(RegionalMap map, MapLocation loc, RegionalMapPanel mappanel)
		{
			try
			{
				if (map != null && loc != null)
				{
					int mapLocation = map.Locations.IndexOf(loc);
					MapLocationForm mapLocationForm = new MapLocationForm(loc);
					if (mapLocationForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						map.Locations[mapLocation] = mapLocationForm.MapLocation;
						Session.Modified = true;
						if (mappanel != null)
						{
							mappanel.SelectedLocation = mapLocationForm.MapLocation;
						}
						this.UpdateView();
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void edit_maparea(object sender, EventArgs e)
		{
			if (this.fDelveView.SelectedArea != null)
			{
				int area = this.fDelveView.Map.Areas.IndexOf(this.fDelveView.SelectedArea);
				MapAreaForm mapAreaForm = new MapAreaForm(this.fDelveView.SelectedArea, this.fDelveView.Map);
				if (mapAreaForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fDelveView.Map.Areas[area] = mapAreaForm.Area;
					Session.Modified = true;
					this.fDelveView.MapChanged();
				}
			}
		}

		private void edit_maplocation(object sender, EventArgs e)
		{
			if (this.fMapView.SelectedLocation != null)
			{
				int mapLocation = this.fMapView.Map.Locations.IndexOf(this.fMapView.SelectedLocation);
				MapLocationForm mapLocationForm = new MapLocationForm(this.fMapView.SelectedLocation);
				if (mapLocationForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fMapView.Map.Locations[mapLocation] = mapLocationForm.MapLocation;
					Session.Modified = true;
					this.fMapView.Invalidate();
				}
			}
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			try
			{
				PlotPoint selectedPoint = this.get_selected_point();
				if (selectedPoint != null)
				{
					int plotPoint = this.PlotView.Plot.Points.IndexOf(selectedPoint);
					Plot plot = Session.Project.FindParent(selectedPoint);
					PlotPointForm plotPointForm = new PlotPointForm(selectedPoint, plot, false);
					if (plotPointForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						plot.Points[plotPoint] = plotPointForm.PlotPoint;
						Session.Modified = true;
						this.set_selected_point(plotPointForm.PlotPoint);
						this.PlotView.RecalculateLayout();
						this.UpdateView();
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ElfNameBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<H3>Elvish Names</H3>");
			head.Add("<P class=instruction>Click on any name to create an encyclopedia entry for it.</P>");
			head.Add("<P class=table>");
			head.Add("<TABLE>");
			head.Add("<TR class=heading>");
			head.Add("<TD><B>Male</B></TD>");
			head.Add("<TD><B>Female</B></TD>");
			head.Add("</TR>");
			for (int i = 0; i != 10; i++)
			{
				string str = ElfName.MaleName();
				string str1 = ElfName.FemaleName();
				head.Add("<TR>");
				head.Add("<TD>");
				string[] strArrays = new string[] { "<P><A href=entry:", str.Replace(" ", "%20"), ">", str, "</A></P>" };
				head.Add(string.Concat(strArrays));
				head.Add("</TD>");
				head.Add("<TD>");
				string[] strArrays1 = new string[] { "<P><A href=entry:", str1.Replace(" ", "%20"), ">", str1, "</A></P>" };
				head.Add(string.Concat(strArrays1));
				head.Add("</TD>");
				head.Add("</TR>");
			}
			head.Add("</TABLE>");
			head.Add("</P>");
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void ElfTextBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			int num = Session.Dice(1, 6);
			for (int i = 0; i != num; i++)
			{
				head.Add(string.Concat("<P>", ElfName.Sentence(), "</P>"));
			}
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void EncAddEntry_Click(object sender, EventArgs e)
		{
			try
			{
				EncyclopediaEntry encyclopediaEntry = this.create_entry("New Entry", "");
				if (encyclopediaEntry != null)
				{
					this.UpdateView();
					this.SelectedEncyclopediaItem = encyclopediaEntry;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void EncAddGroup_Click(object sender, EventArgs e)
		{
			try
			{
				EncyclopediaGroup encyclopediaGroup = new EncyclopediaGroup()
				{
					Name = "New Group"
				};
				EncyclopediaGroupForm encyclopediaGroupForm = new EncyclopediaGroupForm(encyclopediaGroup);
				if (encyclopediaGroupForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.Encyclopedia.Groups.Add(encyclopediaGroupForm.Group);
					Session.Modified = true;
					this.UpdateView();
					this.SelectedEncyclopediaItem = encyclopediaGroup;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void EncClearLbl_Click(object sender, EventArgs e)
		{
			try
			{
				this.EncSearchBox.Text = "";
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void EncCopyBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedEncyclopediaItem != null && this.SelectedEncyclopediaItem is EncyclopediaEntry)
				{
					EncyclopediaEntry selectedEncyclopediaItem = this.SelectedEncyclopediaItem as EncyclopediaEntry;
					Clipboard.SetData(typeof(EncyclopediaEntry).ToString(), selectedEncyclopediaItem.Copy());
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void EncCutBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedEncyclopediaItem != null && this.SelectedEncyclopediaItem is EncyclopediaEntry)
				{
					EncyclopediaEntry selectedEncyclopediaItem = this.SelectedEncyclopediaItem as EncyclopediaEntry;
					Clipboard.SetData(typeof(EncyclopediaEntry).ToString(), selectedEncyclopediaItem.Copy());
					Session.Project.Encyclopedia.Entries.Remove(selectedEncyclopediaItem);
					Session.Modified = true;
					this.update_encyclopedia_list();
					this.SelectedEncyclopediaItem = null;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void EncEditBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedEncyclopediaItem != null && this.SelectedEncyclopediaItem is EncyclopediaEntry)
				{
					int entry = Session.Project.Encyclopedia.Entries.IndexOf(this.SelectedEncyclopediaItem as EncyclopediaEntry);
					EncyclopediaEntryForm encyclopediaEntryForm = new EncyclopediaEntryForm(this.SelectedEncyclopediaItem as EncyclopediaEntry);
					if (encyclopediaEntryForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						Session.Project.Encyclopedia.Entries[entry] = encyclopediaEntryForm.Entry;
						Session.Modified = true;
						this.UpdateView();
						this.SelectedEncyclopediaItem = encyclopediaEntryForm.Entry;
					}
				}
				if (this.SelectedEncyclopediaItem != null && this.SelectedEncyclopediaItem is EncyclopediaGroup)
				{
					int group = Session.Project.Encyclopedia.Groups.IndexOf(this.SelectedEncyclopediaItem as EncyclopediaGroup);
					EncyclopediaGroupForm encyclopediaGroupForm = new EncyclopediaGroupForm(this.SelectedEncyclopediaItem as EncyclopediaGroup);
					if (encyclopediaGroupForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						Session.Project.Encyclopedia.Groups[group] = encyclopediaGroupForm.Group;
						Session.Modified = true;
						this.UpdateView();
						this.SelectedEncyclopediaItem = encyclopediaGroupForm.Group;
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void EncPasteBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (Clipboard.ContainsData(typeof(EncyclopediaEntry).ToString()))
				{
					EncyclopediaEntry data = Clipboard.GetData(typeof(EncyclopediaEntry).ToString()) as EncyclopediaEntry;
					if (data != null)
					{
						if (Session.Project.Encyclopedia.FindEntry(data.ID) != null)
						{
							Guid d = data.ID;
							data.ID = Guid.NewGuid();
							List<EncyclopediaLink> encyclopediaLinks = new List<EncyclopediaLink>();
							foreach (EncyclopediaLink link in Session.Project.Encyclopedia.Links)
							{
								if (!link.EntryIDs.Contains(d))
								{
									continue;
								}
								EncyclopediaLink encyclopediaLink = link.Copy();
								int num = encyclopediaLink.EntryIDs.IndexOf(d);
								encyclopediaLink.EntryIDs[num] = data.ID;
								encyclopediaLinks.Add(encyclopediaLink);
							}
							Session.Project.Encyclopedia.Links.AddRange(encyclopediaLinks);
						}
						Session.Project.Encyclopedia.Entries.Add(data);
						Session.Modified = true;
						this.update_encyclopedia_list();
						this.SelectedEncyclopediaItem = data;
					}
				}
				else if (Clipboard.ContainsText())
				{
					string text = Clipboard.GetText();
					EncyclopediaEntry encyclopediaEntry = new EncyclopediaEntry()
					{
						Name = string.Concat(text.Trim().Substring(0, 12), "..."),
						Details = text
					};
					Session.Project.Encyclopedia.Entries.Add(encyclopediaEntry);
					Session.Modified = true;
					this.update_encyclopedia_list();
					this.SelectedEncyclopediaItem = encyclopediaEntry;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void EncPlayerView_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedEncyclopediaItem != null)
				{
					if (Session.PlayerView == null)
					{
						Session.PlayerView = new PlayerViewForm(this);
					}
					Session.PlayerView.ShowEncyclopediaItem(this.SelectedEncyclopediaItem);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void EncRemoveBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedEncyclopediaItem != null && this.SelectedEncyclopediaItem is EncyclopediaEntry)
				{
					if (MessageBox.Show("Are you sure you want to delete this encyclopedia entry?", "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.No)
					{
						Session.Project.Encyclopedia.Entries.Remove(this.SelectedEncyclopediaItem as EncyclopediaEntry);
						List<EncyclopediaLink> encyclopediaLinks = new List<EncyclopediaLink>();
						foreach (EncyclopediaLink link in Session.Project.Encyclopedia.Links)
						{
							if (!link.EntryIDs.Contains(this.SelectedEncyclopediaItem.ID))
							{
								continue;
							}
							encyclopediaLinks.Add(link);
						}
						foreach (EncyclopediaLink encyclopediaLink in encyclopediaLinks)
						{
							Session.Project.Encyclopedia.Links.Remove(encyclopediaLink);
						}
						foreach (EncyclopediaGroup group in Session.Project.Encyclopedia.Groups)
						{
							if (!group.EntryIDs.Contains(this.SelectedEncyclopediaItem.ID))
							{
								continue;
							}
							group.EntryIDs.Remove(this.SelectedEncyclopediaItem.ID);
						}
						Session.Modified = true;
						this.update_encyclopedia_list();
						this.SelectedEncyclopediaItem = null;
					}
					else
					{
						return;
					}
				}
				if (this.SelectedEncyclopediaItem != null && this.SelectedEncyclopediaItem is EncyclopediaGroup)
				{
					if (MessageBox.Show("Are you sure you want to delete this encyclopedia group?", "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.No)
					{
						Session.Project.Encyclopedia.Groups.Remove(this.SelectedEncyclopediaItem as EncyclopediaGroup);
						this.UpdateView();
						this.SelectedEncyclopediaItem = null;
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

		private void EncSearchBox_TextChanged(object sender, EventArgs e)
		{
			try
			{
				this.update_encyclopedia_list();
				if (this.EntryList.Items.Count == 0)
				{
					this.SelectedEncyclopediaItem = null;
				}
				else
				{
					this.SelectedEncyclopediaItem = this.EntryList.Items[0].Tag as EncyclopediaEntry;
				}
				this.EncSearchBox.Focus();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void EncShareExport_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog()
			{
				Filter = Program.EncyclopediaFilter,
				FileName = Session.Project.Name
			};
			if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Serialisation<Encyclopedia>.Save(saveFileDialog.FileName, Session.Project.Encyclopedia, SerialisationMode.XML);
			}
		}

		private void EncShareImport_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Filter = Program.EncyclopediaFilter
			};
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Encyclopedia encyclopedium = Serialisation<Encyclopedia>.Load(openFileDialog.FileName, SerialisationMode.XML);
				Session.Project.Encyclopedia.Import(encyclopedium);
				Session.Modified = true;
				this.UpdateView();
			}
		}

		private void EncSharePublish_Click(object sender, EventArgs e)
		{
			HandoutForm handoutForm = new HandoutForm();
			handoutForm.AddEncyclopediaEntries();
			handoutForm.ShowDialog();
		}

		private void encyclopedia_template(object sender, EventArgs e)
		{
			try
			{
				if (sender is ToolStripMenuItem)
				{
					string tag = (sender as ToolStripMenuItem).Tag as string;
					string str = FileName.Name(tag);
					EncyclopediaEntry encyclopediaEntry = this.create_entry(str, File.ReadAllText(tag));
					if (encyclopediaEntry != null)
					{
						this.UpdateView();
						this.SelectedEncyclopediaItem = encyclopediaEntry;
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void EntryDetails_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			try
			{
				if (e.Url.Scheme == "entry")
				{
					e.Cancel = true;
					if (e.Url.LocalPath != "edit")
					{
						Guid guid = new Guid(e.Url.LocalPath);
						this.SelectedEncyclopediaItem = Session.Project.Encyclopedia.FindEntry(guid);
					}
					else
					{
						this.EncEditBtn_Click(sender, e);
					}
				}
				if (e.Url.Scheme == "missing")
				{
					e.Cancel = true;
					EncyclopediaEntry encyclopediaEntry = this.create_entry(e.Url.LocalPath, "");
					if (encyclopediaEntry != null)
					{
						this.update_encyclopedia_list();
						this.SelectedEncyclopediaItem = encyclopediaEntry;
					}
				}
				if (e.Url.Scheme == "group")
				{
					e.Cancel = true;
					if (e.Url.LocalPath != "edit")
					{
						Guid guid1 = new Guid(e.Url.LocalPath);
						this.SelectedEncyclopediaItem = Session.Project.Encyclopedia.FindGroup(guid1);
					}
					else
					{
						this.EncEditBtn_Click(sender, e);
					}
				}
				if (e.Url.Scheme == "map")
				{
					e.Cancel = true;
					Guid guid2 = new Guid(e.Url.LocalPath);
					foreach (RegionalMap regionalMap in Session.Project.RegionalMaps)
					{
						MapLocation mapLocation = regionalMap.FindLocation(guid2);
						if (mapLocation == null)
						{
							continue;
						}
						(new RegionalMapForm(regionalMap, mapLocation)).ShowDialog();
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void EntryImageList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedEncyclopediaImage != null)
			{
				EncyclopediaEntry selectedEncyclopediaItem = this.SelectedEncyclopediaItem as EncyclopediaEntry;
				if (selectedEncyclopediaItem == null)
				{
					return;
				}
				int num = selectedEncyclopediaItem.Images.IndexOf(this.SelectedEncyclopediaImage);
				EncyclopediaImageForm encyclopediaImageForm = new EncyclopediaImageForm(this.SelectedEncyclopediaImage);
				if (encyclopediaImageForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					selectedEncyclopediaItem.Images[num] = encyclopediaImageForm.Image;
					this.update_encyclopedia_images();
					Session.Modified = true;
				}
			}
		}

		private void EntryList_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				this.update_encyclopedia_entry();
				this.EntryList.Focus();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ExoticNameBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<H3>Exotic Names</H3>");
			head.Add("<P class=instruction>Click on any name to create an encyclopedia entry for it.</P>");
			head.Add("<P class=table>");
			head.Add("<TABLE>");
			head.Add("<TR class=heading>");
			head.Add("<TD colspan=2><B>Names</B></TD>");
			head.Add("</TR>");
			for (int i = 0; i != 10; i++)
			{
				string str = ExoticName.FullName();
				string str1 = ExoticName.FullName();
				head.Add("<TR>");
				head.Add("<TD>");
				string[] strArrays = new string[] { "<P><A href=entry:", str.Replace(" ", "%20"), ">", str, "</A></P>" };
				head.Add(string.Concat(strArrays));
				head.Add("</TD>");
				head.Add("<TD>");
				string[] strArrays1 = new string[] { "<P><A href=entry:", str1.Replace(" ", "%20"), ">", str1, "</A></P>" };
				head.Add(string.Concat(strArrays1));
				head.Add("</TD>");
				head.Add("</TR>");
			}
			head.Add("</TABLE>");
			head.Add("</P>");
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void ExploreBtn_Click(object sender, EventArgs e)
		{
			try
			{
				PlotPoint selectedPoint = this.get_selected_point();
				if (selectedPoint != null)
				{
					if (this.fView != MainForm.ViewType.Flowchart)
					{
						this.flowchart_view();
					}
					this.PlotView.Plot = selectedPoint.Subplot;
					this.UpdateView();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void extract_attachment(Attachment att, bool run)
		{
			string i;
			object[] objArray = null;
			try
			{
				string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				if (!folderPath.EndsWith("\\"))
				{
					folderPath = string.Concat(folderPath, "\\");
				}
				string str = string.Concat(folderPath, att.Name);
				int num = 1;
				for (i = str; File.Exists(i); i = string.Concat(objArray))
				{
					num++;
					objArray = new object[] { folderPath, FileName.Name(str), " ", num, ".", FileName.Extension(str) };
				}
				File.WriteAllBytes(i, att.Contents);
				if (run)
				{
					Process.Start(i);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void FileExit_Click(object sender, EventArgs e)
		{
			try
			{
				base.Close();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void FileMenu_DropDownOpening(object sender, EventArgs e)
		{
			try
			{
				this.FileSave.Enabled = Session.Project != null;
				this.FileSaveAs.Enabled = Session.Project != null;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void FileNew_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.fView != MainForm.ViewType.Flowchart)
				{
					this.flowchart_view();
				}
				if (this.check_modified())
				{
					Project project = new Project()
					{
						Name = (sender != null ? "Untitled Campaign" : "Random Delve"),
						Author = Environment.UserName
					};
					if ((new ProjectForm(project)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						Session.Project = project;
						Session.Project.SetStandardBackgroundItems();
						Session.Project.TreasureParcels.AddRange(Treasure.CreateParcelSet(Session.Project.Party.Level, Session.Project.Party.Size, true));
						Session.FileName = "";
						this.PlotView.Plot = Session.Project.Plot;
						this.update_title();
						this.UpdateView();
						if (base.Controls.Contains(this.fWelcome))
						{
							base.Controls.Clear();
							this.fWelcome = null;
							base.Controls.Add(this.Pages);
							base.Controls.Add(this.MainMenu);
							this.Pages.Focus();
						}
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void FileOpen_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.fView != MainForm.ViewType.Flowchart)
				{
					this.flowchart_view();
				}
				if (this.check_modified())
				{
					OpenFileDialog openFileDialog = new OpenFileDialog()
					{
						Filter = Program.ProjectFilter,
						FileName = Session.FileName
					};
					if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.open_file(openFileDialog.FileName);
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void FileSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (Session.FileName != "")
				{
					GC.Collect();
					Session.Project.PopulateProjectLibrary();
					if (!Serialisation<Project>.Save(Session.FileName, Session.Project, SerialisationMode.Binary))
					{
						MessageBox.Show("The file could not be saved; check the filename and drive permissions and try again.", "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else
					{
						Session.Modified = false;
					}
					Session.Project.SimplifyProjectLibrary();
				}
				else
				{
					this.FileSaveAs_Click(sender, e);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void FileSaveAs_Click(object sender, EventArgs e)
		{
			try
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog()
				{
					Filter = Program.ProjectFilter,
					FileName = FileName.TrimInvalidCharacters(Session.Project.Name)
				};
				if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					GC.Collect();
					Session.Project.PopulateProjectLibrary();
					if (!Serialisation<Project>.Save(saveFileDialog.FileName, Session.Project, SerialisationMode.Binary))
					{
						MessageBox.Show("The file could not be saved; check the filename and drive permissions and try again.", "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else
					{
						Session.FileName = saveFileDialog.FileName;
						Session.Modified = false;
					}
					Session.Project.SimplifyProjectLibrary();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void flowchart_view()
		{
			List<Control> controls = new List<Control>();
			foreach (Control control in this.PreviewSplitter.Panel1.Controls)
			{
				if (!control.Visible)
				{
					continue;
				}
				controls.Add(control);
			}
			foreach (Control control1 in controls)
			{
				this.PreviewSplitter.Panel1.Controls.Remove(control1);
			}
			foreach (Control control2 in this.PreviewSplitter.Panel1.Controls)
			{
				control2.Visible = true;
			}
			this.fView = MainForm.ViewType.Flowchart;
			this.update_preview();
		}

		private void FlowchartAllXP_Click(object sender, EventArgs e)
		{
			Session.Preferences.AllXP = !Session.Preferences.AllXP;
			this.update_workspace();
			this.update_preview();
		}

		private void FlowchartExport_Click(object sender, EventArgs e)
		{
			try
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog()
				{
					FileName = Session.Project.Name,
					Filter = "Bitmap Image |*.bmp|JPEG Image|*.jpg|GIF Image|*.gif|PNG Image|*.png"
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
					Bitmap bitmap = Screenshot.Plot(this.PlotView.Plot, new System.Drawing.Size(800, 600));
					bitmap.Save(saveFileDialog.FileName, bmp);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void FlowchartPrint_Click(object sender, EventArgs e)
		{
			try
			{
				PrintDialog printDialog = new PrintDialog();
				if (printDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					PrintDocument printDocument = new PrintDocument()
					{
						DocumentName = Session.Project.Name,
						PrinterSettings = printDialog.PrinterSettings
					};
					printDocument.PrintPage += new PrintPageEventHandler(this.print_page);
					printDocument.Print();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void GeneratorBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.Scheme == "entry")
			{
				e.Cancel = true;
				string localPath = e.Url.LocalPath;
				EncyclopediaEntry encyclopediaEntry = Session.Project.Encyclopedia.FindEntry(localPath) ?? new EncyclopediaEntry()
				{
					Name = localPath,
					Category = "People"
				};
				Session.Project.Encyclopedia.Entries.IndexOf(encyclopediaEntry);
				EncyclopediaEntryForm encyclopediaEntryForm = new EncyclopediaEntryForm(encyclopediaEntry);
				if (encyclopediaEntryForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.Encyclopedia.Entries.Add(encyclopediaEntryForm.Entry);
					Session.Modified = true;
					this.update_encyclopedia_list();
				}
			}
			if (e.Url.Scheme == "parcel")
			{
				e.Cancel = true;
				string str = e.Url.LocalPath;
				Parcel parcel = new Parcel()
				{
					Name = "Item",
					Details = str
				};
				Session.Project.TreasureParcels.Add(parcel);
				Session.Modified = true;
				(new ParcelListForm()).ShowDialog();
			}
		}

		private PlotPoint get_selected_point()
		{
			PlotPoint plotPoint;
			switch (this.fView)
			{
				case MainForm.ViewType.Flowchart:
				{
					return this.PlotView.SelectedPoint;
				}
				case MainForm.ViewType.Delve:
				{
					MapView mapView = null;
					foreach (Control control in this.PreviewSplitter.Panel1.Controls)
					{
						if (!(control is MapView))
						{
							continue;
						}
						mapView = control as MapView;
					}
					if (mapView == null)
					{
						break;
					}
					MapArea selectedArea = mapView.SelectedArea;
					if (selectedArea == null)
					{
						break;
					}
					List<PlotPoint>.Enumerator enumerator = this.PlotView.Plot.Points.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							PlotPoint current = enumerator.Current;
							if (current.Element == null)
							{
								continue;
							}
							if (current.Element is Encounter)
							{
								Encounter element = current.Element as Encounter;
								if (element.MapID == mapView.Map.ID && element.MapAreaID == selectedArea.ID)
								{
									plotPoint = current;
									return plotPoint;
								}
							}
							if (current.Element is TrapElement)
							{
								TrapElement trapElement = current.Element as TrapElement;
								if (trapElement.MapID == mapView.Map.ID && trapElement.MapAreaID == selectedArea.ID)
								{
									plotPoint = current;
									return plotPoint;
								}
							}
							if (current.Element is SkillChallenge)
							{
								SkillChallenge skillChallenge = current.Element as SkillChallenge;
								if (skillChallenge.MapID == mapView.Map.ID && skillChallenge.MapAreaID == selectedArea.ID)
								{
									plotPoint = current;
									return plotPoint;
								}
							}
							if (!(current.Element is MapElement))
							{
								continue;
							}
							MapElement mapElement = current.Element as MapElement;
							if (!(mapElement.MapID == mapView.Map.ID) || !(mapElement.MapAreaID == selectedArea.ID))
							{
								continue;
							}
							plotPoint = current;
							return plotPoint;
						}
						break;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					break;
				}
				case MainForm.ViewType.Map:
				{
					RegionalMapPanel regionalMapPanel = null;
					foreach (Control control1 in this.PreviewSplitter.Panel1.Controls)
					{
						if (!(control1 is RegionalMapPanel))
						{
							continue;
						}
						regionalMapPanel = control1 as RegionalMapPanel;
					}
					if (regionalMapPanel == null)
					{
						break;
					}
					if (regionalMapPanel.SelectedLocation == null)
					{
						return null;
					}
					List<PlotPoint>.Enumerator enumerator1 = this.PlotView.Plot.Points.GetEnumerator();
					try
					{
						while (enumerator1.MoveNext())
						{
							PlotPoint current1 = enumerator1.Current;
							if (!(current1.RegionalMapID == regionalMapPanel.Map.ID) || !(current1.MapLocationID == regionalMapPanel.SelectedLocation.ID))
							{
								continue;
							}
							plotPoint = current1;
							return plotPoint;
						}
						break;
					}
					finally
					{
						((IDisposable)enumerator1).Dispose();
					}
					break;
				}
			}
			return null;
		}

		private void HalflingNameBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<H3>Halfling Names</H3>");
			head.Add("<P class=instruction>Click on any name to create an encyclopedia entry for it.</P>");
			head.Add("<P class=table>");
			head.Add("<TABLE>");
			head.Add("<TR class=heading>");
			head.Add("<TD><B>Male</B></TD>");
			head.Add("<TD><B>Female</B></TD>");
			head.Add("</TR>");
			for (int i = 0; i != 10; i++)
			{
				string str = HalflingName.MaleName();
				string str1 = HalflingName.FemaleName();
				head.Add("<TR>");
				head.Add("<TD>");
				string[] strArrays = new string[] { "<P><A href=entry:", str.Replace(" ", "%20"), ">", str, "</A></P>" };
				head.Add(string.Concat(strArrays));
				head.Add("</TD>");
				head.Add("<TD>");
				string[] strArrays1 = new string[] { "<P><A href=entry:", str1.Replace(" ", "%20"), ">", str1, "</A></P>" };
				head.Add(string.Concat(strArrays1));
				head.Add("</TD>");
				head.Add("</TR>");
			}
			head.Add("</TABLE>");
			head.Add("</P>");
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void HelpAbout_Click(object sender, EventArgs e)
		{
			try
			{
				(new AboutBox()).ShowDialog();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void HelpFacebook_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start("http://www.facebook.com/masterplanstudio/");
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void HelpFeedback_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start("mailto:masterplan@habitualindolence.net?subject=Masterplan Feedback");
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void HelpManual_Click(object sender, EventArgs e)
		{
			try
			{
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				string str = string.Concat(FileName.Directory(entryAssembly.FullName), "Manual.pdf");
				if (File.Exists(str))
				{
					Process.Start(str);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void HelpTutorials_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start("http://www.habitualindolence.net/masterplan/tutorials.htm");
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void HelpTwitter_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start("http://www.twitter.com/Masterplan_4E/");
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void HelpWebsite_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start("http://www.habitualindolence.net/masterplan/");
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MainForm));
			ListViewGroup listViewGroup = new ListViewGroup("Races", HorizontalAlignment.Left);
			ListViewGroup listViewGroup1 = new ListViewGroup("Classes", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Themes", HorizontalAlignment.Left);
			ListViewGroup listViewGroup3 = new ListViewGroup("Paragon Paths", HorizontalAlignment.Left);
			ListViewGroup listViewGroup4 = new ListViewGroup("Epic Destinies", HorizontalAlignment.Left);
			ListViewGroup listViewGroup5 = new ListViewGroup("Backgrounds", HorizontalAlignment.Left);
			ListViewGroup listViewGroup6 = new ListViewGroup("Feats (heroic tier)", HorizontalAlignment.Left);
			ListViewGroup listViewGroup7 = new ListViewGroup("Feats (paragon tier)", HorizontalAlignment.Left);
			ListViewGroup listViewGroup8 = new ListViewGroup("Feats (epic tier)", HorizontalAlignment.Left);
			ListViewGroup listViewGroup9 = new ListViewGroup("Weapons", HorizontalAlignment.Left);
			ListViewGroup listViewGroup10 = new ListViewGroup("Rituals", HorizontalAlignment.Left);
			ListViewGroup listViewGroup11 = new ListViewGroup("Creature Lore", HorizontalAlignment.Left);
			ListViewGroup listViewGroup12 = new ListViewGroup("Diseases", HorizontalAlignment.Left);
			ListViewGroup listViewGroup13 = new ListViewGroup("Poisons", HorizontalAlignment.Left);
			ListViewGroup listViewGroup14 = new ListViewGroup("Issues", HorizontalAlignment.Left);
			ListViewGroup listViewGroup15 = new ListViewGroup("Information", HorizontalAlignment.Left);
			ListViewGroup listViewGroup16 = new ListViewGroup("Notes", HorizontalAlignment.Left);
			this.WorkspaceToolbar = new ToolStrip();
			this.AddBtn = new ToolStripSplitButton();
			this.AddEncounter = new ToolStripMenuItem();
			this.AddChallenge = new ToolStripMenuItem();
			this.AddTrap = new ToolStripMenuItem();
			this.AddQuest = new ToolStripMenuItem();
			this.RemoveBtn = new ToolStripButton();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.PlotCutBtn = new ToolStripButton();
			this.PlotCopyBtn = new ToolStripButton();
			this.PlotPasteBtn = new ToolStripButton();
			this.toolStripSeparator5 = new ToolStripSeparator();
			this.SearchBtn = new ToolStripButton();
			this.toolStripSeparator9 = new ToolStripSeparator();
			this.ViewMenu = new ToolStripDropDownButton();
			this.ViewDefault = new ToolStripMenuItem();
			this.toolStripSeparator7 = new ToolStripSeparator();
			this.ViewEncounters = new ToolStripMenuItem();
			this.ViewTraps = new ToolStripMenuItem();
			this.ViewChallenges = new ToolStripMenuItem();
			this.ViewQuests = new ToolStripMenuItem();
			this.ViewParcels = new ToolStripMenuItem();
			this.toolStripSeparator8 = new ToolStripSeparator();
			this.ViewHighlighting = new ToolStripMenuItem();
			this.toolStripSeparator6 = new ToolStripSeparator();
			this.ViewLinks = new ToolStripMenuItem();
			this.ViewLinksCurved = new ToolStripMenuItem();
			this.ViewLinksAngled = new ToolStripMenuItem();
			this.ViewLinksStraight = new ToolStripMenuItem();
			this.ViewLevelling = new ToolStripMenuItem();
			this.ViewTooltips = new ToolStripMenuItem();
			this.toolStripSeparator11 = new ToolStripSeparator();
			this.ViewNavigation = new ToolStripMenuItem();
			this.ViewPreview = new ToolStripMenuItem();
			this.FlowchartMenu = new ToolStripDropDownButton();
			this.FlowchartPrint = new ToolStripMenuItem();
			this.FlowchartExport = new ToolStripMenuItem();
			this.toolStripSeparator27 = new ToolStripSeparator();
			this.FlowchartAllXP = new ToolStripMenuItem();
			this.AdvancedBtn = new ToolStripDropDownButton();
			this.PlotAdvancedTreasure = new ToolStripMenuItem();
			this.PlotAdvancedIssues = new ToolStripMenuItem();
			this.PlotAdvancedDifficulty = new ToolStripMenuItem();
			this.PointMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ContextAdd = new ToolStripMenuItem();
			this.ContextAddBetween = new ToolStripMenuItem();
			this.toolStripSeparator28 = new ToolStripSeparator();
			this.ContextDisconnectAll = new ToolStripMenuItem();
			this.ContextDisconnect = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.ContextMoveTo = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.ContextState = new ToolStripMenuItem();
			this.ContextStateNormal = new ToolStripMenuItem();
			this.ContextStateCompleted = new ToolStripMenuItem();
			this.ContextStateSkipped = new ToolStripMenuItem();
			this.toolStripSeparator20 = new ToolStripSeparator();
			this.ContextEdit = new ToolStripMenuItem();
			this.ContextRemove = new ToolStripMenuItem();
			this.toolStripSeparator29 = new ToolStripSeparator();
			this.ContextExplore = new ToolStripMenuItem();
			this.MainMenu = new MenuStrip();
			this.FileMenu = new ToolStripMenuItem();
			this.FileNew = new ToolStripMenuItem();
			this.toolStripMenuItem1 = new ToolStripSeparator();
			this.FileOpen = new ToolStripMenuItem();
			this.toolStripMenuItem2 = new ToolStripSeparator();
			this.FileSave = new ToolStripMenuItem();
			this.FileSaveAs = new ToolStripMenuItem();
			this.toolStripMenuItem3 = new ToolStripSeparator();
			this.FileAdvanced = new ToolStripMenuItem();
			this.AdvancedDelve = new ToolStripMenuItem();
			this.AdvancedSample = new ToolStripMenuItem();
			this.toolStripSeparator42 = new ToolStripSeparator();
			this.FileExit = new ToolStripMenuItem();
			this.ProjectMenu = new ToolStripMenuItem();
			this.ProjectProject = new ToolStripMenuItem();
			this.ProjectOverview = new ToolStripMenuItem();
			this.ProjectCampaignSettings = new ToolStripMenuItem();
			this.toolStripSeparator30 = new ToolStripSeparator();
			this.ProjectPassword = new ToolStripMenuItem();
			this.toolStripSeparator10 = new ToolStripSeparator();
			this.ProjectTacticalMaps = new ToolStripMenuItem();
			this.ProjectRegionalMaps = new ToolStripMenuItem();
			this.toolStripSeparator4 = new ToolStripSeparator();
			this.ProjectPlayers = new ToolStripMenuItem();
			this.ProjectParcels = new ToolStripMenuItem();
			this.ProjectDecks = new ToolStripMenuItem();
			this.ProjectCustomCreatures = new ToolStripMenuItem();
			this.ProjectCalendars = new ToolStripMenuItem();
			this.toolStripSeparator37 = new ToolStripSeparator();
			this.ProjectEncounters = new ToolStripMenuItem();
			this.PlayerViewMenu = new ToolStripMenuItem();
			this.PlayerViewShow = new ToolStripMenuItem();
			this.PlayerViewClear = new ToolStripMenuItem();
			this.toolStripMenuItem7 = new ToolStripSeparator();
			this.PlayerViewOtherDisplay = new ToolStripMenuItem();
			this.toolStripSeparator14 = new ToolStripSeparator();
			this.PlayerViewTextSize = new ToolStripMenuItem();
			this.TextSizeSmall = new ToolStripMenuItem();
			this.TextSizeMedium = new ToolStripMenuItem();
			this.TextSizeLarge = new ToolStripMenuItem();
			this.ToolsMenu = new ToolStripMenuItem();
			this.ToolsImportProject = new ToolStripMenuItem();
			this.toolStripSeparator25 = new ToolStripSeparator();
			this.ToolsExportProject = new ToolStripMenuItem();
			this.ToolsExportHandout = new ToolStripMenuItem();
			this.ToolsExportLoot = new ToolStripMenuItem();
			this.toolStripSeparator34 = new ToolStripSeparator();
			this.ToolsTileChecklist = new ToolStripMenuItem();
			this.ToolsMiniChecklist = new ToolStripMenuItem();
			this.toolStripSeparator49 = new ToolStripSeparator();
			this.ToolsIssues = new ToolStripMenuItem();
			this.ToolsPowerStats = new ToolStripMenuItem();
			this.toolStripMenuItem4 = new ToolStripSeparator();
			this.ToolsLibraries = new ToolStripMenuItem();
			this.toolStripMenuItem5 = new ToolStripSeparator();
			this.ToolsAddIns = new ToolStripMenuItem();
			this.addinsToolStripMenuItem = new ToolStripMenuItem();
			this.HelpMenu = new ToolStripMenuItem();
			this.HelpManual = new ToolStripMenuItem();
			this.toolStripSeparator12 = new ToolStripSeparator();
			this.HelpFeedback = new ToolStripMenuItem();
			this.toolStripMenuItem8 = new ToolStripSeparator();
			this.HelpTutorials = new ToolStripMenuItem();
			this.toolStripSeparator47 = new ToolStripSeparator();
			this.HelpWebsite = new ToolStripMenuItem();
			this.HelpFacebook = new ToolStripMenuItem();
			this.HelpTwitter = new ToolStripMenuItem();
			this.toolStripSeparator13 = new ToolStripSeparator();
			this.HelpAbout = new ToolStripMenuItem();
			this.PreviewSplitter = new SplitContainer();
			this.NavigationSplitter = new SplitContainer();
			this.NavigationTree = new TreeView();
			this.PlotPanel = new Panel();
			this.PlotView = new Masterplan.Controls.PlotView();
			this.BreadcrumbBar = new StatusStrip();
			this.WorkspaceSearchBar = new ToolStrip();
			this.PlotSearchLbl = new ToolStripLabel();
			this.PlotSearchBox = new ToolStripTextBox();
			this.PlotClearBtn = new ToolStripLabel();
			this.PreviewInfoSplitter = new SplitContainer();
			this.PreviewPanel = new Panel();
			this.Preview = new WebBrowser();
			this.PreviewToolbar = new ToolStrip();
			this.EditBtn = new ToolStripButton();
			this.ExploreBtn = new ToolStripButton();
			this.toolStripSeparator41 = new ToolStripSeparator();
			this.PlotPointMenu = new ToolStripDropDownButton();
			this.PlotPointPlayerView = new ToolStripMenuItem();
			this.toolStripSeparator35 = new ToolStripSeparator();
			this.PlotPointExportHTML = new ToolStripMenuItem();
			this.PlotPointExportFile = new ToolStripMenuItem();
			this.Pages = new TabControl();
			this.WorkspacePage = new TabPage();
			this.BackgroundPage = new TabPage();
			this.splitContainer1 = new SplitContainer();
			this.BackgroundList = new ListView();
			this.InfoHdr = new ColumnHeader();
			this.BackgroundPanel = new Panel();
			this.BackgroundDetails = new WebBrowser();
			this.BackgroundToolbar = new ToolStrip();
			this.BackgroundAddBtn = new ToolStripButton();
			this.BackgroundRemoveBtn = new ToolStripButton();
			this.BackgroundEditBtn = new ToolStripButton();
			this.toolStripSeparator21 = new ToolStripSeparator();
			this.BackgroundUpBtn = new ToolStripButton();
			this.BackgroundDownBtn = new ToolStripButton();
			this.toolStripSeparator23 = new ToolStripSeparator();
			this.BackgroundPlayerView = new ToolStripDropDownButton();
			this.BackgroundPlayerViewSelected = new ToolStripMenuItem();
			this.BackgroundPlayerViewAll = new ToolStripMenuItem();
			this.toolStripSeparator48 = new ToolStripSeparator();
			this.BackgroundShareBtn = new ToolStripDropDownButton();
			this.BackgroundShareExport = new ToolStripMenuItem();
			this.BackgroundShareImport = new ToolStripMenuItem();
			this.toolStripMenuItem10 = new ToolStripSeparator();
			this.BackgroundSharePublish = new ToolStripMenuItem();
			this.EncyclopediaPage = new TabPage();
			this.EncyclopediaSplitter = new SplitContainer();
			this.EntryList = new ListView();
			this.EntryHdr = new ColumnHeader();
			this.EncyclopediaEntrySplitter = new SplitContainer();
			this.EntryPanel = new Panel();
			this.EntryDetails = new WebBrowser();
			this.EntryImageList = new ListView();
			this.EncyclopediaToolbar = new ToolStrip();
			this.EncAddBtn = new ToolStripDropDownButton();
			this.EncAddEntry = new ToolStripMenuItem();
			this.EncAddGroup = new ToolStripMenuItem();
			this.EncRemoveBtn = new ToolStripButton();
			this.EncEditBtn = new ToolStripButton();
			this.toolStripSeparator15 = new ToolStripSeparator();
			this.EncCutBtn = new ToolStripButton();
			this.EncCopyBtn = new ToolStripButton();
			this.EncPasteBtn = new ToolStripButton();
			this.toolStripSeparator17 = new ToolStripSeparator();
			this.EncPlayerView = new ToolStripButton();
			this.toolStripSeparator40 = new ToolStripSeparator();
			this.EncShareBtn = new ToolStripDropDownButton();
			this.EncShareExport = new ToolStripMenuItem();
			this.EncShareImport = new ToolStripMenuItem();
			this.toolStripMenuItem6 = new ToolStripSeparator();
			this.EncSharePublish = new ToolStripMenuItem();
			this.toolStripSeparator22 = new ToolStripSeparator();
			this.EncSearchLbl = new ToolStripLabel();
			this.EncSearchBox = new ToolStripTextBox();
			this.EncClearLbl = new ToolStripLabel();
			this.RulesPage = new TabPage();
			this.RulesSplitter = new SplitContainer();
			this.RulesList = new ListView();
			this.RulesHdr = new ColumnHeader();
			this.RulesToolbar = new ToolStrip();
			this.RulesAddBtn = new ToolStripDropDownButton();
			this.AddRace = new ToolStripMenuItem();
			this.toolStripSeparator31 = new ToolStripSeparator();
			this.AddClass = new ToolStripMenuItem();
			this.AddTheme = new ToolStripMenuItem();
			this.AddParagonPath = new ToolStripMenuItem();
			this.AddEpicDestiny = new ToolStripMenuItem();
			this.toolStripSeparator32 = new ToolStripSeparator();
			this.AddBackground = new ToolStripMenuItem();
			this.AddFeat = new ToolStripMenuItem();
			this.AddWeapon = new ToolStripMenuItem();
			this.AddRitual = new ToolStripMenuItem();
			this.toolStripSeparator39 = new ToolStripSeparator();
			this.AddCreatureLore = new ToolStripMenuItem();
			this.AddDisease = new ToolStripMenuItem();
			this.AddPoison = new ToolStripMenuItem();
			this.toolStripSeparator33 = new ToolStripSeparator();
			this.RulesShareBtn = new ToolStripDropDownButton();
			this.RulesShareExport = new ToolStripMenuItem();
			this.RulesShareImport = new ToolStripMenuItem();
			this.toolStripMenuItem9 = new ToolStripSeparator();
			this.RulesSharePublish = new ToolStripMenuItem();
			this.RulesBrowserPanel = new Panel();
			this.RulesBrowser = new WebBrowser();
			this.EncEntryToolbar = new ToolStrip();
			this.RulesRemoveBtn = new ToolStripButton();
			this.RulesEditBtn = new ToolStripButton();
			this.toolStripSeparator43 = new ToolStripSeparator();
			this.RuleEncyclopediaBtn = new ToolStripButton();
			this.toolStripSeparator36 = new ToolStripSeparator();
			this.RulesPlayerViewBtn = new ToolStripButton();
			this.AttachmentsPage = new TabPage();
			this.AttachmentList = new ListView();
			this.AttachmentHdr = new ColumnHeader();
			this.AttachmentSizeHdr = new ColumnHeader();
			this.AttachmentToolbar = new ToolStrip();
			this.AttachmentImportBtn = new ToolStripButton();
			this.AttachmentRemoveBtn = new ToolStripButton();
			this.toolStripSeparator19 = new ToolStripSeparator();
			this.AttachmentExtract = new ToolStripDropDownButton();
			this.AttachmentExtractSimple = new ToolStripMenuItem();
			this.AttachmentExtractAndRun = new ToolStripMenuItem();
			this.toolStripSeparator24 = new ToolStripSeparator();
			this.AttachmentPlayerView = new ToolStripButton();
			this.JotterPage = new TabPage();
			this.JotterSplitter = new SplitContainer();
			this.NoteList = new ListView();
			this.NoteHdr = new ColumnHeader();
			this.NoteBox = new TextBox();
			this.JotterToolbar = new ToolStrip();
			this.NoteAddBtn = new ToolStripButton();
			this.NoteRemoveBtn = new ToolStripButton();
			this.toolStripSeparator16 = new ToolStripSeparator();
			this.NoteCategoryBtn = new ToolStripButton();
			this.toolStripSeparator38 = new ToolStripSeparator();
			this.NoteCutBtn = new ToolStripButton();
			this.NoteCopyBtn = new ToolStripButton();
			this.NotePasteBtn = new ToolStripButton();
			this.toolStripSeparator18 = new ToolStripSeparator();
			this.NoteSearchLbl = new ToolStripLabel();
			this.NoteSearchBox = new ToolStripTextBox();
			this.NoteClearLbl = new ToolStripLabel();
			this.ReferencePage = new TabPage();
			this.ReferenceSplitter = new SplitContainer();
			this.ReferencePages = new TabControl();
			this.PartyPage = new TabPage();
			this.PartyBrowser = new WebBrowser();
			this.ToolsPage = new TabPage();
			this.ToolBrowserPanel = new Panel();
			this.GeneratorBrowser = new WebBrowser();
			this.GeneratorToolbar = new ToolStrip();
			this.toolStripLabel1 = new ToolStripLabel();
			this.toolStripSeparator26 = new ToolStripSeparator();
			this.ElfNameBtn = new ToolStripButton();
			this.DwarfNameBtn = new ToolStripButton();
			this.HalflingNameBtn = new ToolStripButton();
			this.ExoticNameBtn = new ToolStripButton();
			this.toolStripSeparator44 = new ToolStripSeparator();
			this.TreasureBtn = new ToolStripButton();
			this.BookTitleBtn = new ToolStripButton();
			this.PotionBtn = new ToolStripButton();
			this.toolStripSeparator45 = new ToolStripSeparator();
			this.NPCBtn = new ToolStripButton();
			this.RoomBtn = new ToolStripButton();
			this.toolStripSeparator46 = new ToolStripSeparator();
			this.ElfTextBtn = new ToolStripButton();
			this.DwarfTextBtn = new ToolStripButton();
			this.PrimordialTextBtn = new ToolStripButton();
			this.CompendiumPage = new TabPage();
			this.CompendiumBrowser = new WebBrowser();
			this.InfoPanel = new Masterplan.Controls.InfoPanel();
			this.ReferenceToolbar = new ToolStrip();
			this.DieRollerBtn = new ToolStripButton();
			this.WorkspaceToolbar.SuspendLayout();
			this.PointMenu.SuspendLayout();
			this.MainMenu.SuspendLayout();
			this.PreviewSplitter.Panel1.SuspendLayout();
			this.PreviewSplitter.Panel2.SuspendLayout();
			this.PreviewSplitter.SuspendLayout();
			this.NavigationSplitter.Panel1.SuspendLayout();
			this.NavigationSplitter.Panel2.SuspendLayout();
			this.NavigationSplitter.SuspendLayout();
			this.PlotPanel.SuspendLayout();
			this.WorkspaceSearchBar.SuspendLayout();
			this.PreviewInfoSplitter.Panel1.SuspendLayout();
			this.PreviewInfoSplitter.SuspendLayout();
			this.PreviewPanel.SuspendLayout();
			this.PreviewToolbar.SuspendLayout();
			this.Pages.SuspendLayout();
			this.WorkspacePage.SuspendLayout();
			this.BackgroundPage.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.BackgroundPanel.SuspendLayout();
			this.BackgroundToolbar.SuspendLayout();
			this.EncyclopediaPage.SuspendLayout();
			this.EncyclopediaSplitter.Panel1.SuspendLayout();
			this.EncyclopediaSplitter.Panel2.SuspendLayout();
			this.EncyclopediaSplitter.SuspendLayout();
			this.EncyclopediaEntrySplitter.Panel1.SuspendLayout();
			this.EncyclopediaEntrySplitter.Panel2.SuspendLayout();
			this.EncyclopediaEntrySplitter.SuspendLayout();
			this.EntryPanel.SuspendLayout();
			this.EncyclopediaToolbar.SuspendLayout();
			this.RulesPage.SuspendLayout();
			this.RulesSplitter.Panel1.SuspendLayout();
			this.RulesSplitter.Panel2.SuspendLayout();
			this.RulesSplitter.SuspendLayout();
			this.RulesToolbar.SuspendLayout();
			this.RulesBrowserPanel.SuspendLayout();
			this.EncEntryToolbar.SuspendLayout();
			this.AttachmentsPage.SuspendLayout();
			this.AttachmentToolbar.SuspendLayout();
			this.JotterPage.SuspendLayout();
			this.JotterSplitter.Panel1.SuspendLayout();
			this.JotterSplitter.Panel2.SuspendLayout();
			this.JotterSplitter.SuspendLayout();
			this.JotterToolbar.SuspendLayout();
			this.ReferencePage.SuspendLayout();
			this.ReferenceSplitter.Panel1.SuspendLayout();
			this.ReferenceSplitter.Panel2.SuspendLayout();
			this.ReferenceSplitter.SuspendLayout();
			this.ReferencePages.SuspendLayout();
			this.PartyPage.SuspendLayout();
			this.ToolsPage.SuspendLayout();
			this.ToolBrowserPanel.SuspendLayout();
			this.GeneratorToolbar.SuspendLayout();
			this.CompendiumPage.SuspendLayout();
			this.ReferenceToolbar.SuspendLayout();
			base.SuspendLayout();
			ToolStripItemCollection items = this.WorkspaceToolbar.Items;
			ToolStripItem[] addBtn = new ToolStripItem[] { this.AddBtn, this.RemoveBtn, this.toolStripSeparator3, this.PlotCutBtn, this.PlotCopyBtn, this.PlotPasteBtn, this.toolStripSeparator5, this.SearchBtn, this.toolStripSeparator9, this.ViewMenu, this.FlowchartMenu, this.AdvancedBtn };
			items.AddRange(addBtn);
			this.WorkspaceToolbar.Location = new Point(0, 0);
			this.WorkspaceToolbar.Name = "WorkspaceToolbar";
			this.WorkspaceToolbar.Size = new System.Drawing.Size(508, 25);
			this.WorkspaceToolbar.TabIndex = 1;
			this.WorkspaceToolbar.Text = "toolStrip1";
			this.AddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems = this.AddBtn.DropDownItems;
			ToolStripItem[] addEncounter = new ToolStripItem[] { this.AddEncounter, this.AddChallenge, this.AddTrap, this.AddQuest };
			dropDownItems.AddRange(addEncounter);
			this.AddBtn.Image = (Image)componentResourceManager.GetObject("AddBtn.Image");
			this.AddBtn.ImageTransparentColor = Color.Magenta;
			this.AddBtn.Name = "AddBtn";
			this.AddBtn.Size = new System.Drawing.Size(45, 22);
			this.AddBtn.Text = "Add";
			this.AddBtn.ButtonClick += new EventHandler(this.AddBtn_Click);
			this.AddEncounter.Name = "AddEncounter";
			this.AddEncounter.Size = new System.Drawing.Size(160, 22);
			this.AddEncounter.Text = "Encounter...";
			this.AddEncounter.Click += new EventHandler(this.AddEncounter_Click);
			this.AddChallenge.Name = "AddChallenge";
			this.AddChallenge.Size = new System.Drawing.Size(160, 22);
			this.AddChallenge.Text = "Skill Challenge...";
			this.AddChallenge.Click += new EventHandler(this.AddChallenge_Click);
			this.AddTrap.Name = "AddTrap";
			this.AddTrap.Size = new System.Drawing.Size(160, 22);
			this.AddTrap.Text = "Trap / Hazard...";
			this.AddTrap.Click += new EventHandler(this.AddTrap_Click);
			this.AddQuest.Name = "AddQuest";
			this.AddQuest.Size = new System.Drawing.Size(160, 22);
			this.AddQuest.Text = "Quest...";
			this.AddQuest.Click += new EventHandler(this.AddQuest_Click);
			this.RemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveBtn.Image = (Image)componentResourceManager.GetObject("RemoveBtn.Image");
			this.RemoveBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.RemoveBtn.Text = "Remove";
			this.RemoveBtn.Click += new EventHandler(this.RemoveBtn_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			this.PlotCutBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PlotCutBtn.Image = (Image)componentResourceManager.GetObject("PlotCutBtn.Image");
			this.PlotCutBtn.ImageTransparentColor = Color.Magenta;
			this.PlotCutBtn.Name = "PlotCutBtn";
			this.PlotCutBtn.Size = new System.Drawing.Size(30, 22);
			this.PlotCutBtn.Text = "Cut";
			this.PlotCutBtn.Click += new EventHandler(this.CutBtn_Click);
			this.PlotCopyBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PlotCopyBtn.Image = (Image)componentResourceManager.GetObject("PlotCopyBtn.Image");
			this.PlotCopyBtn.ImageTransparentColor = Color.Magenta;
			this.PlotCopyBtn.Name = "PlotCopyBtn";
			this.PlotCopyBtn.Size = new System.Drawing.Size(39, 22);
			this.PlotCopyBtn.Text = "Copy";
			this.PlotCopyBtn.Click += new EventHandler(this.CopyBtn_Click);
			this.PlotPasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PlotPasteBtn.Image = (Image)componentResourceManager.GetObject("PlotPasteBtn.Image");
			this.PlotPasteBtn.ImageTransparentColor = Color.Magenta;
			this.PlotPasteBtn.Name = "PlotPasteBtn";
			this.PlotPasteBtn.Size = new System.Drawing.Size(39, 22);
			this.PlotPasteBtn.Text = "Paste";
			this.PlotPasteBtn.Click += new EventHandler(this.PasteBtn_Click);
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			this.SearchBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SearchBtn.Image = (Image)componentResourceManager.GetObject("SearchBtn.Image");
			this.SearchBtn.ImageTransparentColor = Color.Magenta;
			this.SearchBtn.Name = "SearchBtn";
			this.SearchBtn.Size = new System.Drawing.Size(46, 22);
			this.SearchBtn.Text = "Search";
			this.SearchBtn.Click += new EventHandler(this.SearchBtn_Click);
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
			ToolStripItemCollection toolStripItemCollections = this.ViewMenu.DropDownItems;
			ToolStripItem[] viewDefault = new ToolStripItem[] { this.ViewDefault, this.toolStripSeparator7, this.ViewEncounters, this.ViewTraps, this.ViewChallenges, this.ViewQuests, this.ViewParcels, this.toolStripSeparator8, this.ViewHighlighting, this.toolStripSeparator6, this.ViewLinks, this.ViewLevelling, this.ViewTooltips, this.toolStripSeparator11, this.ViewNavigation, this.ViewPreview };
			toolStripItemCollections.AddRange(viewDefault);
			this.ViewMenu.Name = "ViewMenu";
			this.ViewMenu.Size = new System.Drawing.Size(45, 22);
			this.ViewMenu.Text = "View";
			this.ViewMenu.DropDownOpening += new EventHandler(this.ViewMenu_DropDownOpening);
			this.ViewDefault.Name = "ViewDefault";
			this.ViewDefault.Size = new System.Drawing.Size(191, 22);
			this.ViewDefault.Text = "Default View";
			this.ViewDefault.Click += new EventHandler(this.ViewDefault_Click);
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(188, 6);
			this.ViewEncounters.Name = "ViewEncounters";
			this.ViewEncounters.Size = new System.Drawing.Size(191, 22);
			this.ViewEncounters.Text = "Show Encounters";
			this.ViewEncounters.Click += new EventHandler(this.ViewEncounters_Click);
			this.ViewTraps.Name = "ViewTraps";
			this.ViewTraps.Size = new System.Drawing.Size(191, 22);
			this.ViewTraps.Text = "Show Traps / Hazards";
			this.ViewTraps.Click += new EventHandler(this.ViewTraps_Click);
			this.ViewChallenges.Name = "ViewChallenges";
			this.ViewChallenges.Size = new System.Drawing.Size(191, 22);
			this.ViewChallenges.Text = "Show Skill Challenges";
			this.ViewChallenges.Click += new EventHandler(this.ViewChallenges_Click);
			this.ViewQuests.Name = "ViewQuests";
			this.ViewQuests.Size = new System.Drawing.Size(191, 22);
			this.ViewQuests.Text = "Show Quests";
			this.ViewQuests.Click += new EventHandler(this.ViewQuests_Click);
			this.ViewParcels.Name = "ViewParcels";
			this.ViewParcels.Size = new System.Drawing.Size(191, 22);
			this.ViewParcels.Text = "Show Treasure Parcels";
			this.ViewParcels.Click += new EventHandler(this.ViewParcels_Click);
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(188, 6);
			this.ViewHighlighting.Name = "ViewHighlighting";
			this.ViewHighlighting.Size = new System.Drawing.Size(191, 22);
			this.ViewHighlighting.Text = "Highlighting";
			this.ViewHighlighting.Click += new EventHandler(this.ViewHighlighting_Click);
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(188, 6);
			ToolStripItemCollection dropDownItems1 = this.ViewLinks.DropDownItems;
			ToolStripItem[] viewLinksCurved = new ToolStripItem[] { this.ViewLinksCurved, this.ViewLinksAngled, this.ViewLinksStraight };
			dropDownItems1.AddRange(viewLinksCurved);
			this.ViewLinks.Name = "ViewLinks";
			this.ViewLinks.Size = new System.Drawing.Size(191, 22);
			this.ViewLinks.Text = "Show Links";
			this.ViewLinks.DropDownOpening += new EventHandler(this.ViewLinks_DropDownOpening);
			this.ViewLinksCurved.Name = "ViewLinksCurved";
			this.ViewLinksCurved.Size = new System.Drawing.Size(115, 22);
			this.ViewLinksCurved.Text = "Curved";
			this.ViewLinksCurved.Click += new EventHandler(this.ViewLinksCurved_Click);
			this.ViewLinksAngled.Name = "ViewLinksAngled";
			this.ViewLinksAngled.Size = new System.Drawing.Size(115, 22);
			this.ViewLinksAngled.Text = "Angled";
			this.ViewLinksAngled.Click += new EventHandler(this.ViewLinksAngled_Click);
			this.ViewLinksStraight.Name = "ViewLinksStraight";
			this.ViewLinksStraight.Size = new System.Drawing.Size(115, 22);
			this.ViewLinksStraight.Text = "Straight";
			this.ViewLinksStraight.Click += new EventHandler(this.ViewLinksStraight_Click);
			this.ViewLevelling.Name = "ViewLevelling";
			this.ViewLevelling.Size = new System.Drawing.Size(191, 22);
			this.ViewLevelling.Text = "Show Levelling";
			this.ViewLevelling.Click += new EventHandler(this.ViewLevelling_Click);
			this.ViewTooltips.Name = "ViewTooltips";
			this.ViewTooltips.Size = new System.Drawing.Size(191, 22);
			this.ViewTooltips.Text = "Show Tooltips";
			this.ViewTooltips.Click += new EventHandler(this.ViewTooltips_Click);
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new System.Drawing.Size(188, 6);
			this.ViewNavigation.Name = "ViewNavigation";
			this.ViewNavigation.Size = new System.Drawing.Size(191, 22);
			this.ViewNavigation.Text = "Show Navigation";
			this.ViewNavigation.Click += new EventHandler(this.ViewNavigation_Click);
			this.ViewPreview.Name = "ViewPreview";
			this.ViewPreview.Size = new System.Drawing.Size(191, 22);
			this.ViewPreview.Text = "Show Preview";
			this.ViewPreview.Click += new EventHandler(this.ViewPreview_Click);
			this.FlowchartMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection toolStripItemCollections1 = this.FlowchartMenu.DropDownItems;
			ToolStripItem[] flowchartPrint = new ToolStripItem[] { this.FlowchartPrint, this.FlowchartExport, this.toolStripSeparator27, this.FlowchartAllXP };
			toolStripItemCollections1.AddRange(flowchartPrint);
			this.FlowchartMenu.Image = (Image)componentResourceManager.GetObject("FlowchartMenu.Image");
			this.FlowchartMenu.ImageTransparentColor = Color.Magenta;
			this.FlowchartMenu.Name = "FlowchartMenu";
			this.FlowchartMenu.Size = new System.Drawing.Size(72, 22);
			this.FlowchartMenu.Text = "Flowchart";
			this.FlowchartPrint.Name = "FlowchartPrint";
			this.FlowchartPrint.Size = new System.Drawing.Size(196, 22);
			this.FlowchartPrint.Text = "Print...";
			this.FlowchartPrint.Click += new EventHandler(this.FlowchartPrint_Click);
			this.FlowchartExport.Name = "FlowchartExport";
			this.FlowchartExport.Size = new System.Drawing.Size(196, 22);
			this.FlowchartExport.Text = "Export...";
			this.FlowchartExport.Click += new EventHandler(this.FlowchartExport_Click);
			this.toolStripSeparator27.Name = "toolStripSeparator27";
			this.toolStripSeparator27.Size = new System.Drawing.Size(193, 6);
			this.FlowchartAllXP.Name = "FlowchartAllXP";
			this.FlowchartAllXP.Size = new System.Drawing.Size(196, 22);
			this.FlowchartAllXP.Text = "Maximum Available XP";
			this.FlowchartAllXP.Click += new EventHandler(this.FlowchartAllXP_Click);
			this.AdvancedBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems2 = this.AdvancedBtn.DropDownItems;
			ToolStripItem[] plotAdvancedTreasure = new ToolStripItem[] { this.PlotAdvancedTreasure, this.PlotAdvancedIssues, this.PlotAdvancedDifficulty };
			dropDownItems2.AddRange(plotAdvancedTreasure);
			this.AdvancedBtn.Image = (Image)componentResourceManager.GetObject("AdvancedBtn.Image");
			this.AdvancedBtn.ImageTransparentColor = Color.Magenta;
			this.AdvancedBtn.Name = "AdvancedBtn";
			this.AdvancedBtn.Size = new System.Drawing.Size(73, 22);
			this.AdvancedBtn.Text = "Advanced";
			this.PlotAdvancedTreasure.Name = "PlotAdvancedTreasure";
			this.PlotAdvancedTreasure.Size = new System.Drawing.Size(185, 22);
			this.PlotAdvancedTreasure.Text = "Export Treasure List...";
			this.PlotAdvancedTreasure.Click += new EventHandler(this.PlotAdvancedTreasure_Click);
			this.PlotAdvancedIssues.Name = "PlotAdvancedIssues";
			this.PlotAdvancedIssues.Size = new System.Drawing.Size(185, 22);
			this.PlotAdvancedIssues.Text = "Plot Design Issues";
			this.PlotAdvancedIssues.Click += new EventHandler(this.PlotAdvancedIssues_Click);
			this.PlotAdvancedDifficulty.Name = "PlotAdvancedDifficulty";
			this.PlotAdvancedDifficulty.Size = new System.Drawing.Size(185, 22);
			this.PlotAdvancedDifficulty.Text = "Adjust Difficulty...";
			this.PlotAdvancedDifficulty.Click += new EventHandler(this.PlotAdvancedDifficulty_Click);
			ToolStripItemCollection items1 = this.PointMenu.Items;
			ToolStripItem[] contextAdd = new ToolStripItem[] { this.ContextAdd, this.ContextAddBetween, this.toolStripSeparator28, this.ContextDisconnectAll, this.ContextDisconnect, this.toolStripSeparator1, this.ContextMoveTo, this.toolStripSeparator2, this.ContextState, this.toolStripSeparator20, this.ContextEdit, this.ContextRemove, this.toolStripSeparator29, this.ContextExplore };
			items1.AddRange(contextAdd);
			this.PointMenu.Name = "PointMenu";
			this.PointMenu.Size = new System.Drawing.Size(166, 232);
			this.PointMenu.Opening += new CancelEventHandler(this.PointMenu_Opening);
			this.ContextAdd.Name = "ContextAdd";
			this.ContextAdd.Size = new System.Drawing.Size(165, 22);
			this.ContextAdd.Text = "Add Point...";
			this.ContextAdd.Click += new EventHandler(this.ContextAdd_Click);
			this.ContextAddBetween.Name = "ContextAddBetween";
			this.ContextAddBetween.Size = new System.Drawing.Size(165, 22);
			this.ContextAddBetween.Text = "Add Point";
			this.toolStripSeparator28.Name = "toolStripSeparator28";
			this.toolStripSeparator28.Size = new System.Drawing.Size(162, 6);
			this.ContextDisconnectAll.Name = "ContextDisconnectAll";
			this.ContextDisconnectAll.Size = new System.Drawing.Size(165, 22);
			this.ContextDisconnectAll.Text = "Disconnect Point";
			this.ContextDisconnectAll.Click += new EventHandler(this.ContextDisconnectAll_Click);
			this.ContextDisconnect.Name = "ContextDisconnect";
			this.ContextDisconnect.Size = new System.Drawing.Size(165, 22);
			this.ContextDisconnect.Text = "Disconnect From";
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(162, 6);
			this.ContextMoveTo.Name = "ContextMoveTo";
			this.ContextMoveTo.Size = new System.Drawing.Size(165, 22);
			this.ContextMoveTo.Text = "Move To Subplot";
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(162, 6);
			ToolStripItemCollection toolStripItemCollections2 = this.ContextState.DropDownItems;
			ToolStripItem[] contextStateNormal = new ToolStripItem[] { this.ContextStateNormal, this.ContextStateCompleted, this.ContextStateSkipped };
			toolStripItemCollections2.AddRange(contextStateNormal);
			this.ContextState.Name = "ContextState";
			this.ContextState.Size = new System.Drawing.Size(165, 22);
			this.ContextState.Text = "State";
			this.ContextStateNormal.Name = "ContextStateNormal";
			this.ContextStateNormal.Size = new System.Drawing.Size(133, 22);
			this.ContextStateNormal.Text = "Normal";
			this.ContextStateNormal.Click += new EventHandler(this.ContextStateNormal_Click);
			this.ContextStateCompleted.Name = "ContextStateCompleted";
			this.ContextStateCompleted.Size = new System.Drawing.Size(133, 22);
			this.ContextStateCompleted.Text = "Completed";
			this.ContextStateCompleted.Click += new EventHandler(this.ContextStateCompleted_Click);
			this.ContextStateSkipped.Name = "ContextStateSkipped";
			this.ContextStateSkipped.Size = new System.Drawing.Size(133, 22);
			this.ContextStateSkipped.Text = "Skipped";
			this.ContextStateSkipped.Click += new EventHandler(this.ContextStateSkipped_Click);
			this.toolStripSeparator20.Name = "toolStripSeparator20";
			this.toolStripSeparator20.Size = new System.Drawing.Size(162, 6);
			this.ContextEdit.Name = "ContextEdit";
			this.ContextEdit.Size = new System.Drawing.Size(165, 22);
			this.ContextEdit.Text = "Edit";
			this.ContextEdit.Click += new EventHandler(this.EditBtn_Click);
			this.ContextRemove.Name = "ContextRemove";
			this.ContextRemove.Size = new System.Drawing.Size(165, 22);
			this.ContextRemove.Text = "Remove";
			this.ContextRemove.Click += new EventHandler(this.RemoveBtn_Click);
			this.toolStripSeparator29.Name = "toolStripSeparator29";
			this.toolStripSeparator29.Size = new System.Drawing.Size(162, 6);
			this.ContextExplore.Name = "ContextExplore";
			this.ContextExplore.Size = new System.Drawing.Size(165, 22);
			this.ContextExplore.Text = "Explore Subplot";
			this.ContextExplore.Click += new EventHandler(this.ExploreBtn_Click);
			ToolStripItemCollection items2 = this.MainMenu.Items;
			ToolStripItem[] fileMenu = new ToolStripItem[] { this.FileMenu, this.ProjectMenu, this.PlayerViewMenu, this.ToolsMenu, this.HelpMenu };
			items2.AddRange(fileMenu);
			this.MainMenu.Location = new Point(0, 0);
			this.MainMenu.Name = "MainMenu";
			this.MainMenu.Size = new System.Drawing.Size(864, 24);
			this.MainMenu.TabIndex = 4;
			this.MainMenu.Text = "menuStrip1";
			ToolStripItemCollection dropDownItems3 = this.FileMenu.DropDownItems;
			ToolStripItem[] fileNew = new ToolStripItem[] { this.FileNew, this.toolStripMenuItem1, this.FileOpen, this.toolStripMenuItem2, this.FileSave, this.FileSaveAs, this.toolStripMenuItem3, this.FileAdvanced, this.toolStripSeparator42, this.FileExit };
			dropDownItems3.AddRange(fileNew);
			this.FileMenu.Name = "FileMenu";
			this.FileMenu.Size = new System.Drawing.Size(37, 20);
			this.FileMenu.Text = "File";
			this.FileMenu.DropDownOpening += new EventHandler(this.FileMenu_DropDownOpening);
			this.FileNew.Name = "FileNew";
			this.FileNew.ShortcutKeys = Keys.RButton | Keys.MButton | Keys.XButton2 | Keys.Back | Keys.LineFeed | Keys.Clear | Keys.B | Keys.D | Keys.F | Keys.H | Keys.J | Keys.L | Keys.N | Keys.Control;
			this.FileNew.Size = new System.Drawing.Size(195, 22);
			this.FileNew.Text = "New Project...";
			this.FileNew.Click += new EventHandler(this.FileNew_Click);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(192, 6);
			this.FileOpen.Name = "FileOpen";
			this.FileOpen.ShortcutKeys = Keys.LButton | Keys.RButton | Keys.Cancel | Keys.MButton | Keys.XButton1 | Keys.XButton2 | Keys.Back | Keys.Tab | Keys.LineFeed | Keys.Clear | Keys.Return | Keys.Enter | Keys.A | Keys.B | Keys.C | Keys.D | Keys.E | Keys.F | Keys.G | Keys.H | Keys.I | Keys.J | Keys.K | Keys.L | Keys.M | Keys.N | Keys.O | Keys.Control;
			this.FileOpen.Size = new System.Drawing.Size(195, 22);
			this.FileOpen.Text = "Open Project...";
			this.FileOpen.Click += new EventHandler(this.FileOpen_Click);
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(192, 6);
			this.FileSave.Name = "FileSave";
			this.FileSave.ShortcutKeys = Keys.LButton | Keys.RButton | Keys.Cancel | Keys.ShiftKey | Keys.ControlKey | Keys.Menu | Keys.Pause | Keys.A | Keys.B | Keys.C | Keys.P | Keys.Q | Keys.R | Keys.S | Keys.Control;
			this.FileSave.Size = new System.Drawing.Size(195, 22);
			this.FileSave.Text = "Save Project";
			this.FileSave.Click += new EventHandler(this.FileSave_Click);
			this.FileSaveAs.Name = "FileSaveAs";
			this.FileSaveAs.Size = new System.Drawing.Size(195, 22);
			this.FileSaveAs.Text = "Save Project As...";
			this.FileSaveAs.Click += new EventHandler(this.FileSaveAs_Click);
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(192, 6);
			ToolStripItemCollection toolStripItemCollections3 = this.FileAdvanced.DropDownItems;
			ToolStripItem[] advancedDelve = new ToolStripItem[] { this.AdvancedDelve, this.AdvancedSample };
			toolStripItemCollections3.AddRange(advancedDelve);
			this.FileAdvanced.Name = "FileAdvanced";
			this.FileAdvanced.Size = new System.Drawing.Size(195, 22);
			this.FileAdvanced.Text = "Advanced";
			this.AdvancedDelve.Name = "AdvancedDelve";
			this.AdvancedDelve.Size = new System.Drawing.Size(245, 22);
			this.AdvancedDelve.Text = "Create a Dungeon Delve...";
			this.AdvancedDelve.Click += new EventHandler(this.AdvancedDelve_Click);
			this.AdvancedSample.Name = "AdvancedSample";
			this.AdvancedSample.Size = new System.Drawing.Size(245, 22);
			this.AdvancedSample.Text = "Download a Premade Adventure";
			this.AdvancedSample.Click += new EventHandler(this.AdvancedSample_Click);
			this.toolStripSeparator42.Name = "toolStripSeparator42";
			this.toolStripSeparator42.Size = new System.Drawing.Size(192, 6);
			this.FileExit.Name = "FileExit";
			this.FileExit.Size = new System.Drawing.Size(195, 22);
			this.FileExit.Text = "Exit";
			this.FileExit.Click += new EventHandler(this.FileExit_Click);
			ToolStripItemCollection dropDownItems4 = this.ProjectMenu.DropDownItems;
			ToolStripItem[] projectProject = new ToolStripItem[] { this.ProjectProject, this.ProjectOverview, this.ProjectCampaignSettings, this.toolStripSeparator30, this.ProjectPassword, this.toolStripSeparator10, this.ProjectTacticalMaps, this.ProjectRegionalMaps, this.toolStripSeparator4, this.ProjectPlayers, this.ProjectParcels, this.ProjectDecks, this.ProjectCustomCreatures, this.ProjectCalendars, this.toolStripSeparator37, this.ProjectEncounters };
			dropDownItems4.AddRange(projectProject);
			this.ProjectMenu.Name = "ProjectMenu";
			this.ProjectMenu.Size = new System.Drawing.Size(56, 20);
			this.ProjectMenu.Text = "Project";
			this.ProjectMenu.DropDownOpening += new EventHandler(this.ProjectMenu_DropDownOpening);
			this.ProjectProject.Name = "ProjectProject";
			this.ProjectProject.ShortcutKeys = Keys.ShiftKey | Keys.P | Keys.Control;
			this.ProjectProject.Size = new System.Drawing.Size(243, 22);
			this.ProjectProject.Text = "Project Properties";
			this.ProjectProject.Click += new EventHandler(this.ProjectProject_Click);
			this.ProjectOverview.Name = "ProjectOverview";
			this.ProjectOverview.Size = new System.Drawing.Size(243, 22);
			this.ProjectOverview.Text = "Project Overview";
			this.ProjectOverview.Click += new EventHandler(this.ProjectOverview_Click);
			this.ProjectCampaignSettings.Name = "ProjectCampaignSettings";
			this.ProjectCampaignSettings.Size = new System.Drawing.Size(243, 22);
			this.ProjectCampaignSettings.Text = "Campaign Settings";
			this.ProjectCampaignSettings.Click += new EventHandler(this.ProjectCampaignSettings_Click);
			this.toolStripSeparator30.Name = "toolStripSeparator30";
			this.toolStripSeparator30.Size = new System.Drawing.Size(240, 6);
			this.ProjectPassword.Name = "ProjectPassword";
			this.ProjectPassword.Size = new System.Drawing.Size(243, 22);
			this.ProjectPassword.Text = "Password Protection";
			this.ProjectPassword.Click += new EventHandler(this.ProjectPassword_Click);
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(240, 6);
			this.ProjectTacticalMaps.Name = "ProjectTacticalMaps";
			this.ProjectTacticalMaps.ShortcutKeys = Keys.F2;
			this.ProjectTacticalMaps.Size = new System.Drawing.Size(243, 22);
			this.ProjectTacticalMaps.Text = "Tactical Maps";
			this.ProjectTacticalMaps.Click += new EventHandler(this.ProjectTacticalMaps_Click);
			this.ProjectRegionalMaps.Name = "ProjectRegionalMaps";
			this.ProjectRegionalMaps.ShortcutKeys = Keys.F3;
			this.ProjectRegionalMaps.Size = new System.Drawing.Size(243, 22);
			this.ProjectRegionalMaps.Text = "Regional Maps";
			this.ProjectRegionalMaps.Click += new EventHandler(this.ProjectRegionalMaps_Click);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(240, 6);
			this.ProjectPlayers.Name = "ProjectPlayers";
			this.ProjectPlayers.ShortcutKeys = Keys.F4;
			this.ProjectPlayers.Size = new System.Drawing.Size(243, 22);
			this.ProjectPlayers.Text = "Player Characters";
			this.ProjectPlayers.Click += new EventHandler(this.ProjectPlayers_Click);
			this.ProjectParcels.Name = "ProjectParcels";
			this.ProjectParcels.ShortcutKeys = Keys.F5;
			this.ProjectParcels.Size = new System.Drawing.Size(243, 22);
			this.ProjectParcels.Text = "Treasure Parcels";
			this.ProjectParcels.Click += new EventHandler(this.ProjectParcels_Click);
			this.ProjectDecks.Name = "ProjectDecks";
			this.ProjectDecks.ShortcutKeys = Keys.F6;
			this.ProjectDecks.Size = new System.Drawing.Size(243, 22);
			this.ProjectDecks.Text = "Encounter Decks";
			this.ProjectDecks.Click += new EventHandler(this.ProjectDecks_Click);
			this.ProjectCustomCreatures.Name = "ProjectCustomCreatures";
			this.ProjectCustomCreatures.ShortcutKeys = Keys.F7;
			this.ProjectCustomCreatures.Size = new System.Drawing.Size(243, 22);
			this.ProjectCustomCreatures.Text = "Custom Creatures and NPCs";
			this.ProjectCustomCreatures.Click += new EventHandler(this.ProjectCustomCreatures_Click);
			this.ProjectCalendars.Name = "ProjectCalendars";
			this.ProjectCalendars.ShortcutKeys = Keys.F8;
			this.ProjectCalendars.Size = new System.Drawing.Size(243, 22);
			this.ProjectCalendars.Text = "Calendars";
			this.ProjectCalendars.Click += new EventHandler(this.ProjectCalendars_Click);
			this.toolStripSeparator37.Name = "toolStripSeparator37";
			this.toolStripSeparator37.Size = new System.Drawing.Size(240, 6);
			this.ProjectEncounters.Name = "ProjectEncounters";
			this.ProjectEncounters.Size = new System.Drawing.Size(243, 22);
			this.ProjectEncounters.Text = "Paused Encounters";
			this.ProjectEncounters.Click += new EventHandler(this.ProjectEncounters_Click);
			ToolStripItemCollection toolStripItemCollections4 = this.PlayerViewMenu.DropDownItems;
			ToolStripItem[] playerViewShow = new ToolStripItem[] { this.PlayerViewShow, this.PlayerViewClear, this.toolStripMenuItem7, this.PlayerViewOtherDisplay, this.toolStripSeparator14, this.PlayerViewTextSize };
			toolStripItemCollections4.AddRange(playerViewShow);
			this.PlayerViewMenu.Name = "PlayerViewMenu";
			this.PlayerViewMenu.Size = new System.Drawing.Size(79, 20);
			this.PlayerViewMenu.Text = "Player View";
			this.PlayerViewMenu.DropDownOpening += new EventHandler(this.PlayerViewMenu_DropDownOpening);
			this.PlayerViewShow.Name = "PlayerViewShow";
			this.PlayerViewShow.Size = new System.Drawing.Size(194, 22);
			this.PlayerViewShow.Text = "Show";
			this.PlayerViewShow.Click += new EventHandler(this.ToolsPlayerView_Click);
			this.PlayerViewClear.Name = "PlayerViewClear";
			this.PlayerViewClear.Size = new System.Drawing.Size(194, 22);
			this.PlayerViewClear.Text = "Clear";
			this.PlayerViewClear.Click += new EventHandler(this.ToolsPlayerViewClear_Click);
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			this.toolStripMenuItem7.Size = new System.Drawing.Size(191, 6);
			this.PlayerViewOtherDisplay.Name = "PlayerViewOtherDisplay";
			this.PlayerViewOtherDisplay.Size = new System.Drawing.Size(194, 22);
			this.PlayerViewOtherDisplay.Text = "Show on Other Display";
			this.PlayerViewOtherDisplay.Click += new EventHandler(this.ToolsPlayerViewSecondary_Click);
			this.toolStripSeparator14.Name = "toolStripSeparator14";
			this.toolStripSeparator14.Size = new System.Drawing.Size(191, 6);
			ToolStripItemCollection dropDownItems5 = this.PlayerViewTextSize.DropDownItems;
			ToolStripItem[] textSizeSmall = new ToolStripItem[] { this.TextSizeSmall, this.TextSizeMedium, this.TextSizeLarge };
			dropDownItems5.AddRange(textSizeSmall);
			this.PlayerViewTextSize.Name = "PlayerViewTextSize";
			this.PlayerViewTextSize.Size = new System.Drawing.Size(194, 22);
			this.PlayerViewTextSize.Text = "Text Size";
			this.TextSizeSmall.Name = "TextSizeSmall";
			this.TextSizeSmall.Size = new System.Drawing.Size(119, 22);
			this.TextSizeSmall.Text = "Small";
			this.TextSizeSmall.Click += new EventHandler(this.TextSizeSmall_Click);
			this.TextSizeMedium.Name = "TextSizeMedium";
			this.TextSizeMedium.Size = new System.Drawing.Size(119, 22);
			this.TextSizeMedium.Text = "Medium";
			this.TextSizeMedium.Click += new EventHandler(this.TextSizeMedium_Click);
			this.TextSizeLarge.Name = "TextSizeLarge";
			this.TextSizeLarge.Size = new System.Drawing.Size(119, 22);
			this.TextSizeLarge.Text = "Large";
			this.TextSizeLarge.Click += new EventHandler(this.TextSizeLarge_Click);
			ToolStripItemCollection toolStripItemCollections5 = this.ToolsMenu.DropDownItems;
			ToolStripItem[] toolsImportProject = new ToolStripItem[] { this.ToolsImportProject, this.toolStripSeparator25, this.ToolsExportProject, this.ToolsExportHandout, this.ToolsExportLoot, this.toolStripSeparator34, this.ToolsTileChecklist, this.ToolsMiniChecklist, this.toolStripSeparator49, this.ToolsIssues, this.ToolsPowerStats, this.toolStripMenuItem4, this.ToolsLibraries, this.toolStripMenuItem5, this.ToolsAddIns };
			toolStripItemCollections5.AddRange(toolsImportProject);
			this.ToolsMenu.Name = "ToolsMenu";
			this.ToolsMenu.Size = new System.Drawing.Size(48, 20);
			this.ToolsMenu.Text = "Tools";
			this.ToolsMenu.DropDownOpening += new EventHandler(this.ToolsMenu_DropDownOpening);
			this.ToolsImportProject.Name = "ToolsImportProject";
			this.ToolsImportProject.Size = new System.Drawing.Size(204, 22);
			this.ToolsImportProject.Text = "Import Project...";
			this.ToolsImportProject.Click += new EventHandler(this.ToolsImportProject_Click);
			this.toolStripSeparator25.Name = "toolStripSeparator25";
			this.toolStripSeparator25.Size = new System.Drawing.Size(201, 6);
			this.ToolsExportProject.Name = "ToolsExportProject";
			this.ToolsExportProject.Size = new System.Drawing.Size(204, 22);
			this.ToolsExportProject.Text = "Export Project...";
			this.ToolsExportProject.Click += new EventHandler(this.ToolsExportProject_Click);
			this.ToolsExportHandout.Name = "ToolsExportHandout";
			this.ToolsExportHandout.Size = new System.Drawing.Size(204, 22);
			this.ToolsExportHandout.Text = "Export Handout...";
			this.ToolsExportHandout.Click += new EventHandler(this.ToolsExportHandout_Click);
			this.ToolsExportLoot.Name = "ToolsExportLoot";
			this.ToolsExportLoot.Size = new System.Drawing.Size(204, 22);
			this.ToolsExportLoot.Text = "Export Treasure List...";
			this.ToolsExportLoot.Click += new EventHandler(this.ToolsExportLoot_Click);
			this.toolStripSeparator34.Name = "toolStripSeparator34";
			this.toolStripSeparator34.Size = new System.Drawing.Size(201, 6);
			this.ToolsTileChecklist.Name = "ToolsTileChecklist";
			this.ToolsTileChecklist.Size = new System.Drawing.Size(204, 22);
			this.ToolsTileChecklist.Text = "Map Tile Checklist...";
			this.ToolsTileChecklist.Click += new EventHandler(this.ToolsTileChecklist_Click);
			this.ToolsMiniChecklist.Name = "ToolsMiniChecklist";
			this.ToolsMiniChecklist.Size = new System.Drawing.Size(204, 22);
			this.ToolsMiniChecklist.Text = "Miniature Checklist...";
			this.ToolsMiniChecklist.Click += new EventHandler(this.ToolsMiniChecklist_Click);
			this.toolStripSeparator49.Name = "toolStripSeparator49";
			this.toolStripSeparator49.Size = new System.Drawing.Size(201, 6);
			this.ToolsIssues.Name = "ToolsIssues";
			this.ToolsIssues.Size = new System.Drawing.Size(204, 22);
			this.ToolsIssues.Text = "Plot Design Issues";
			this.ToolsIssues.Click += new EventHandler(this.ToolsIssues_Click);
			this.ToolsPowerStats.Name = "ToolsPowerStats";
			this.ToolsPowerStats.Size = new System.Drawing.Size(204, 22);
			this.ToolsPowerStats.Text = "Creature Power Statistics";
			this.ToolsPowerStats.Click += new EventHandler(this.ToolsPowerStats_Click);
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(201, 6);
			this.ToolsLibraries.Name = "ToolsLibraries";
			this.ToolsLibraries.ShortcutKeys = Keys.MButton | Keys.Back | Keys.Clear | Keys.D | Keys.H | Keys.L | Keys.Control;
			this.ToolsLibraries.Size = new System.Drawing.Size(204, 22);
			this.ToolsLibraries.Text = "Libraries";
			this.ToolsLibraries.Click += new EventHandler(this.ToolsLibraries_Click);
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(201, 6);
			this.ToolsAddIns.DropDownItems.AddRange(new ToolStripItem[] { this.addinsToolStripMenuItem });
			this.ToolsAddIns.Name = "ToolsAddIns";
			this.ToolsAddIns.Size = new System.Drawing.Size(204, 22);
			this.ToolsAddIns.Text = "Add-Ins";
			this.addinsToolStripMenuItem.Name = "addinsToolStripMenuItem";
			this.addinsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.addinsToolStripMenuItem.Text = "[add-ins]";
			ToolStripItemCollection dropDownItems6 = this.HelpMenu.DropDownItems;
			ToolStripItem[] helpManual = new ToolStripItem[] { this.HelpManual, this.toolStripSeparator12, this.HelpFeedback, this.toolStripMenuItem8, this.HelpTutorials, this.toolStripSeparator47, this.HelpWebsite, this.HelpFacebook, this.HelpTwitter, this.toolStripSeparator13, this.HelpAbout };
			dropDownItems6.AddRange(helpManual);
			this.HelpMenu.Name = "HelpMenu";
			this.HelpMenu.Size = new System.Drawing.Size(44, 20);
			this.HelpMenu.Text = "Help";
			this.HelpManual.Name = "HelpManual";
			this.HelpManual.ShortcutKeys = Keys.F1;
			this.HelpManual.Size = new System.Drawing.Size(204, 22);
			this.HelpManual.Text = "Manual";
			this.HelpManual.Click += new EventHandler(this.HelpManual_Click);
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new System.Drawing.Size(201, 6);
			this.HelpFeedback.Name = "HelpFeedback";
			this.HelpFeedback.Size = new System.Drawing.Size(204, 22);
			this.HelpFeedback.Text = "Send Feedback";
			this.HelpFeedback.Click += new EventHandler(this.HelpFeedback_Click);
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			this.toolStripMenuItem8.Size = new System.Drawing.Size(201, 6);
			this.HelpTutorials.Name = "HelpTutorials";
			this.HelpTutorials.Size = new System.Drawing.Size(204, 22);
			this.HelpTutorials.Text = "Tutorials";
			this.HelpTutorials.Click += new EventHandler(this.HelpTutorials_Click);
			this.toolStripSeparator47.Name = "toolStripSeparator47";
			this.toolStripSeparator47.Size = new System.Drawing.Size(201, 6);
			this.HelpWebsite.Name = "HelpWebsite";
			this.HelpWebsite.Size = new System.Drawing.Size(204, 22);
			this.HelpWebsite.Text = "Masterplan Website";
			this.HelpWebsite.Click += new EventHandler(this.HelpWebsite_Click);
			this.HelpFacebook.Name = "HelpFacebook";
			this.HelpFacebook.Size = new System.Drawing.Size(204, 22);
			this.HelpFacebook.Text = "Masterplan on Facebook";
			this.HelpFacebook.Click += new EventHandler(this.HelpFacebook_Click);
			this.HelpTwitter.Name = "HelpTwitter";
			this.HelpTwitter.Size = new System.Drawing.Size(204, 22);
			this.HelpTwitter.Text = "Masterplan on Twitter";
			this.HelpTwitter.Click += new EventHandler(this.HelpTwitter_Click);
			this.toolStripSeparator13.Name = "toolStripSeparator13";
			this.toolStripSeparator13.Size = new System.Drawing.Size(201, 6);
			this.HelpAbout.Name = "HelpAbout";
			this.HelpAbout.Size = new System.Drawing.Size(204, 22);
			this.HelpAbout.Text = "About";
			this.HelpAbout.Click += new EventHandler(this.HelpAbout_Click);
			this.PreviewSplitter.Dock = DockStyle.Fill;
			this.PreviewSplitter.FixedPanel = FixedPanel.Panel2;
			this.PreviewSplitter.Location = new Point(0, 0);
			this.PreviewSplitter.Name = "PreviewSplitter";
			this.PreviewSplitter.Panel1.Controls.Add(this.NavigationSplitter);
			this.PreviewSplitter.Panel1.Controls.Add(this.WorkspaceToolbar);
			this.PreviewSplitter.Panel2.Controls.Add(this.PreviewInfoSplitter);
			this.PreviewSplitter.Size = new System.Drawing.Size(856, 410);
			this.PreviewSplitter.SplitterDistance = 508;
			this.PreviewSplitter.TabIndex = 6;
			this.NavigationSplitter.Dock = DockStyle.Fill;
			this.NavigationSplitter.FixedPanel = FixedPanel.Panel1;
			this.NavigationSplitter.Location = new Point(0, 25);
			this.NavigationSplitter.Name = "NavigationSplitter";
			this.NavigationSplitter.Panel1.Controls.Add(this.NavigationTree);
			this.NavigationSplitter.Panel2.Controls.Add(this.PlotPanel);
			this.NavigationSplitter.Panel2.Controls.Add(this.WorkspaceSearchBar);
			this.NavigationSplitter.Size = new System.Drawing.Size(508, 385);
			this.NavigationSplitter.SplitterDistance = 152;
			this.NavigationSplitter.TabIndex = 4;
			this.NavigationTree.AllowDrop = true;
			this.NavigationTree.Dock = DockStyle.Fill;
			this.NavigationTree.HideSelection = false;
			this.NavigationTree.Location = new Point(0, 0);
			this.NavigationTree.Name = "NavigationTree";
			this.NavigationTree.ShowRootLines = false;
			this.NavigationTree.Size = new System.Drawing.Size(152, 385);
			this.NavigationTree.TabIndex = 0;
			this.NavigationTree.DragDrop += new DragEventHandler(this.NavigationTree_DragDrop);
			this.NavigationTree.AfterSelect += new TreeViewEventHandler(this.NavigationTree_AfterSelect);
			this.NavigationTree.DragOver += new DragEventHandler(this.NavigationTree_DragOver);
			this.PlotPanel.BorderStyle = BorderStyle.FixedSingle;
			this.PlotPanel.Controls.Add(this.PlotView);
			this.PlotPanel.Controls.Add(this.BreadcrumbBar);
			this.PlotPanel.Dock = DockStyle.Fill;
			this.PlotPanel.Location = new Point(0, 25);
			this.PlotPanel.Name = "PlotPanel";
			this.PlotPanel.Size = new System.Drawing.Size(352, 360);
			this.PlotPanel.TabIndex = 5;
			this.PlotView.AllowDrop = true;
			this.PlotView.ContextMenuStrip = this.PointMenu;
			this.PlotView.Dock = DockStyle.Fill;
			this.PlotView.Filter = "";
			this.PlotView.LinkStyle = PlotViewLinkStyle.Curved;
			this.PlotView.Location = new Point(0, 0);
			this.PlotView.Mode = PlotViewMode.Normal;
			this.PlotView.Name = "PlotView";
			this.PlotView.Plot = null;
			this.PlotView.SelectedPoint = null;
			this.PlotView.ShowLevels = true;
			this.PlotView.ShowTooltips = true;
			this.PlotView.Size = new System.Drawing.Size(350, 336);
			this.PlotView.TabIndex = 2;
			this.PlotView.PlotLayoutChanged += new EventHandler(this.PlotView_PlotLayoutChanged);
			this.PlotView.DoubleClick += new EventHandler(this.PlotView_DoubleClick);
			this.PlotView.SelectionChanged += new EventHandler(this.PlotView_SelectionChanged);
			this.PlotView.PlotChanged += new EventHandler(this.PlotView_PlotChanged);
			this.BreadcrumbBar.Location = new Point(0, 336);
			this.BreadcrumbBar.Name = "BreadcrumbBar";
			this.BreadcrumbBar.Size = new System.Drawing.Size(350, 22);
			this.BreadcrumbBar.SizingGrip = false;
			this.BreadcrumbBar.TabIndex = 4;
			this.BreadcrumbBar.Text = "statusStrip1";
			ToolStripItemCollection items3 = this.WorkspaceSearchBar.Items;
			ToolStripItem[] plotSearchLbl = new ToolStripItem[] { this.PlotSearchLbl, this.PlotSearchBox, this.PlotClearBtn };
			items3.AddRange(plotSearchLbl);
			this.WorkspaceSearchBar.Location = new Point(0, 0);
			this.WorkspaceSearchBar.Name = "WorkspaceSearchBar";
			this.WorkspaceSearchBar.Size = new System.Drawing.Size(352, 25);
			this.WorkspaceSearchBar.TabIndex = 3;
			this.WorkspaceSearchBar.Text = "toolStrip1";
			this.PlotSearchLbl.Name = "PlotSearchLbl";
			this.PlotSearchLbl.Size = new System.Drawing.Size(63, 22);
			this.PlotSearchLbl.Text = "Search for:";
			this.PlotSearchBox.BorderStyle = BorderStyle.FixedSingle;
			this.PlotSearchBox.Name = "PlotSearchBox";
			this.PlotSearchBox.Size = new System.Drawing.Size(200, 25);
			this.PlotSearchBox.TextChanged += new EventHandler(this.SearchBox_TextChanged);
			this.PlotClearBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PlotClearBtn.Image = (Image)componentResourceManager.GetObject("PlotClearBtn.Image");
			this.PlotClearBtn.ImageTransparentColor = Color.Magenta;
			this.PlotClearBtn.IsLink = true;
			this.PlotClearBtn.Name = "PlotClearBtn";
			this.PlotClearBtn.Size = new System.Drawing.Size(34, 22);
			this.PlotClearBtn.Text = "Clear";
			this.PlotClearBtn.Click += new EventHandler(this.ClearBtn_Click);
			this.PreviewInfoSplitter.Dock = DockStyle.Fill;
			this.PreviewInfoSplitter.FixedPanel = FixedPanel.Panel2;
			this.PreviewInfoSplitter.Location = new Point(0, 0);
			this.PreviewInfoSplitter.Name = "PreviewInfoSplitter";
			this.PreviewInfoSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.PreviewInfoSplitter.Panel1.Controls.Add(this.PreviewPanel);
			this.PreviewInfoSplitter.Panel1.Controls.Add(this.PreviewToolbar);
			this.PreviewInfoSplitter.Size = new System.Drawing.Size(344, 410);
			this.PreviewInfoSplitter.SplitterDistance = 227;
			this.PreviewInfoSplitter.TabIndex = 1;
			this.PreviewPanel.BorderStyle = BorderStyle.FixedSingle;
			this.PreviewPanel.Controls.Add(this.Preview);
			this.PreviewPanel.Dock = DockStyle.Fill;
			this.PreviewPanel.Location = new Point(0, 25);
			this.PreviewPanel.Name = "PreviewPanel";
			this.PreviewPanel.Size = new System.Drawing.Size(344, 202);
			this.PreviewPanel.TabIndex = 1;
			this.Preview.AllowWebBrowserDrop = false;
			this.Preview.Dock = DockStyle.Fill;
			this.Preview.IsWebBrowserContextMenuEnabled = false;
			this.Preview.Location = new Point(0, 0);
			this.Preview.MinimumSize = new System.Drawing.Size(20, 20);
			this.Preview.Name = "Preview";
			this.Preview.ScriptErrorsSuppressed = true;
			this.Preview.Size = new System.Drawing.Size(342, 200);
			this.Preview.TabIndex = 0;
			this.Preview.Navigating += new WebBrowserNavigatingEventHandler(this.Preview_Navigating);
			ToolStripItemCollection items4 = this.PreviewToolbar.Items;
			ToolStripItem[] editBtn = new ToolStripItem[] { this.EditBtn, this.ExploreBtn, this.toolStripSeparator41, this.PlotPointMenu };
			items4.AddRange(editBtn);
			this.PreviewToolbar.Location = new Point(0, 0);
			this.PreviewToolbar.Name = "PreviewToolbar";
			this.PreviewToolbar.Size = new System.Drawing.Size(344, 25);
			this.PreviewToolbar.TabIndex = 1;
			this.PreviewToolbar.Text = "toolStrip1";
			this.EditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditBtn.Image = (Image)componentResourceManager.GetObject("EditBtn.Image");
			this.EditBtn.ImageTransparentColor = Color.Magenta;
			this.EditBtn.Name = "EditBtn";
			this.EditBtn.Size = new System.Drawing.Size(86, 22);
			this.EditBtn.Text = "Edit Plot Point";
			this.EditBtn.Click += new EventHandler(this.EditBtn_Click);
			this.ExploreBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ExploreBtn.Image = (Image)componentResourceManager.GetObject("ExploreBtn.Image");
			this.ExploreBtn.ImageTransparentColor = Color.Magenta;
			this.ExploreBtn.Name = "ExploreBtn";
			this.ExploreBtn.Size = new System.Drawing.Size(93, 22);
			this.ExploreBtn.Text = "Explore Subplot";
			this.ExploreBtn.Click += new EventHandler(this.ExploreBtn_Click);
			this.toolStripSeparator41.Name = "toolStripSeparator41";
			this.toolStripSeparator41.Size = new System.Drawing.Size(6, 25);
			this.PlotPointMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection toolStripItemCollections6 = this.PlotPointMenu.DropDownItems;
			ToolStripItem[] plotPointPlayerView = new ToolStripItem[] { this.PlotPointPlayerView, this.toolStripSeparator35, this.PlotPointExportHTML, this.PlotPointExportFile };
			toolStripItemCollections6.AddRange(plotPointPlayerView);
			this.PlotPointMenu.Image = (Image)componentResourceManager.GetObject("PlotPointMenu.Image");
			this.PlotPointMenu.ImageTransparentColor = Color.Magenta;
			this.PlotPointMenu.Name = "PlotPointMenu";
			this.PlotPointMenu.Size = new System.Drawing.Size(49, 22);
			this.PlotPointMenu.Text = "Share";
			this.PlotPointPlayerView.Name = "PlotPointPlayerView";
			this.PlotPointPlayerView.Size = new System.Drawing.Size(177, 22);
			this.PlotPointPlayerView.Text = "Send to Player View";
			this.PlotPointPlayerView.Click += new EventHandler(this.PlotPointPlayerView_Click);
			this.toolStripSeparator35.Name = "toolStripSeparator35";
			this.toolStripSeparator35.Size = new System.Drawing.Size(174, 6);
			this.PlotPointExportHTML.Name = "PlotPointExportHTML";
			this.PlotPointExportHTML.Size = new System.Drawing.Size(177, 22);
			this.PlotPointExportHTML.Text = "Export to HTML...";
			this.PlotPointExportHTML.Click += new EventHandler(this.PlotPointExportHTML_Click);
			this.PlotPointExportFile.Name = "PlotPointExportFile";
			this.PlotPointExportFile.Size = new System.Drawing.Size(177, 22);
			this.PlotPointExportFile.Text = "Export to File...";
			this.PlotPointExportFile.Click += new EventHandler(this.PlotPointExportFile_Click);
			this.Pages.Controls.Add(this.WorkspacePage);
			this.Pages.Controls.Add(this.BackgroundPage);
			this.Pages.Controls.Add(this.EncyclopediaPage);
			this.Pages.Controls.Add(this.RulesPage);
			this.Pages.Controls.Add(this.AttachmentsPage);
			this.Pages.Controls.Add(this.JotterPage);
			this.Pages.Controls.Add(this.ReferencePage);
			this.Pages.Dock = DockStyle.Fill;
			this.Pages.Location = new Point(0, 24);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new System.Drawing.Size(864, 436);
			this.Pages.TabIndex = 5;
			this.WorkspacePage.Controls.Add(this.PreviewSplitter);
			this.WorkspacePage.Location = new Point(4, 22);
			this.WorkspacePage.Name = "WorkspacePage";
			this.WorkspacePage.Size = new System.Drawing.Size(856, 410);
			this.WorkspacePage.TabIndex = 0;
			this.WorkspacePage.Text = "Plot Workspace";
			this.WorkspacePage.UseVisualStyleBackColor = true;
			this.BackgroundPage.Controls.Add(this.splitContainer1);
			this.BackgroundPage.Controls.Add(this.BackgroundToolbar);
			this.BackgroundPage.Location = new Point(4, 22);
			this.BackgroundPage.Name = "BackgroundPage";
			this.BackgroundPage.Size = new System.Drawing.Size(856, 410);
			this.BackgroundPage.TabIndex = 4;
			this.BackgroundPage.Text = "Background";
			this.BackgroundPage.UseVisualStyleBackColor = true;
			this.splitContainer1.Dock = DockStyle.Fill;
			this.splitContainer1.FixedPanel = FixedPanel.Panel1;
			this.splitContainer1.Location = new Point(0, 25);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Panel1.Controls.Add(this.BackgroundList);
			this.splitContainer1.Panel2.Controls.Add(this.BackgroundPanel);
			this.splitContainer1.Size = new System.Drawing.Size(856, 385);
			this.splitContainer1.SplitterDistance = 180;
			this.splitContainer1.TabIndex = 1;
			this.BackgroundList.Columns.AddRange(new ColumnHeader[] { this.InfoHdr });
			this.BackgroundList.Dock = DockStyle.Fill;
			this.BackgroundList.FullRowSelect = true;
			this.BackgroundList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.BackgroundList.HideSelection = false;
			this.BackgroundList.Location = new Point(0, 0);
			this.BackgroundList.MultiSelect = false;
			this.BackgroundList.Name = "BackgroundList";
			this.BackgroundList.Size = new System.Drawing.Size(180, 385);
			this.BackgroundList.TabIndex = 0;
			this.BackgroundList.UseCompatibleStateImageBehavior = false;
			this.BackgroundList.View = View.Details;
			this.BackgroundList.SelectedIndexChanged += new EventHandler(this.BackgroundList_SelectedIndexChanged);
			this.BackgroundList.DoubleClick += new EventHandler(this.BackgroundEditBtn_Click);
			this.InfoHdr.Text = "Information";
			this.InfoHdr.Width = 150;
			this.BackgroundPanel.BorderStyle = BorderStyle.FixedSingle;
			this.BackgroundPanel.Controls.Add(this.BackgroundDetails);
			this.BackgroundPanel.Dock = DockStyle.Fill;
			this.BackgroundPanel.Location = new Point(0, 0);
			this.BackgroundPanel.Name = "BackgroundPanel";
			this.BackgroundPanel.Size = new System.Drawing.Size(672, 385);
			this.BackgroundPanel.TabIndex = 0;
			this.BackgroundDetails.Dock = DockStyle.Fill;
			this.BackgroundDetails.IsWebBrowserContextMenuEnabled = false;
			this.BackgroundDetails.Location = new Point(0, 0);
			this.BackgroundDetails.MinimumSize = new System.Drawing.Size(20, 20);
			this.BackgroundDetails.Name = "BackgroundDetails";
			this.BackgroundDetails.Size = new System.Drawing.Size(670, 383);
			this.BackgroundDetails.TabIndex = 0;
			this.BackgroundDetails.Navigating += new WebBrowserNavigatingEventHandler(this.BackgroundDetails_Navigating);
			ToolStripItemCollection items5 = this.BackgroundToolbar.Items;
			ToolStripItem[] backgroundAddBtn = new ToolStripItem[] { this.BackgroundAddBtn, this.BackgroundRemoveBtn, this.BackgroundEditBtn, this.toolStripSeparator21, this.BackgroundUpBtn, this.BackgroundDownBtn, this.toolStripSeparator23, this.BackgroundPlayerView, this.toolStripSeparator48, this.BackgroundShareBtn };
			items5.AddRange(backgroundAddBtn);
			this.BackgroundToolbar.Location = new Point(0, 0);
			this.BackgroundToolbar.Name = "BackgroundToolbar";
			this.BackgroundToolbar.Size = new System.Drawing.Size(856, 25);
			this.BackgroundToolbar.TabIndex = 0;
			this.BackgroundToolbar.Text = "toolStrip1";
			this.BackgroundAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.BackgroundAddBtn.Image = (Image)componentResourceManager.GetObject("BackgroundAddBtn.Image");
			this.BackgroundAddBtn.ImageTransparentColor = Color.Magenta;
			this.BackgroundAddBtn.Name = "BackgroundAddBtn";
			this.BackgroundAddBtn.Size = new System.Drawing.Size(33, 22);
			this.BackgroundAddBtn.Text = "Add";
			this.BackgroundAddBtn.Click += new EventHandler(this.BackgroundAddBtn_Click);
			this.BackgroundRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.BackgroundRemoveBtn.Image = (Image)componentResourceManager.GetObject("BackgroundRemoveBtn.Image");
			this.BackgroundRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.BackgroundRemoveBtn.Name = "BackgroundRemoveBtn";
			this.BackgroundRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.BackgroundRemoveBtn.Text = "Remove";
			this.BackgroundRemoveBtn.Click += new EventHandler(this.BackgroundRemoveBtn_Click);
			this.BackgroundEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.BackgroundEditBtn.Image = (Image)componentResourceManager.GetObject("BackgroundEditBtn.Image");
			this.BackgroundEditBtn.ImageTransparentColor = Color.Magenta;
			this.BackgroundEditBtn.Name = "BackgroundEditBtn";
			this.BackgroundEditBtn.Size = new System.Drawing.Size(31, 22);
			this.BackgroundEditBtn.Text = "Edit";
			this.BackgroundEditBtn.Click += new EventHandler(this.BackgroundEditBtn_Click);
			this.toolStripSeparator21.Name = "toolStripSeparator21";
			this.toolStripSeparator21.Size = new System.Drawing.Size(6, 25);
			this.BackgroundUpBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.BackgroundUpBtn.Image = (Image)componentResourceManager.GetObject("BackgroundUpBtn.Image");
			this.BackgroundUpBtn.ImageTransparentColor = Color.Magenta;
			this.BackgroundUpBtn.Name = "BackgroundUpBtn";
			this.BackgroundUpBtn.Size = new System.Drawing.Size(59, 22);
			this.BackgroundUpBtn.Text = "Move Up";
			this.BackgroundUpBtn.Click += new EventHandler(this.BackgroundUpBtn_Click);
			this.BackgroundDownBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.BackgroundDownBtn.Image = (Image)componentResourceManager.GetObject("BackgroundDownBtn.Image");
			this.BackgroundDownBtn.ImageTransparentColor = Color.Magenta;
			this.BackgroundDownBtn.Name = "BackgroundDownBtn";
			this.BackgroundDownBtn.Size = new System.Drawing.Size(75, 22);
			this.BackgroundDownBtn.Text = "Move Down";
			this.BackgroundDownBtn.Click += new EventHandler(this.BackgroundDownBtn_Click);
			this.toolStripSeparator23.Name = "toolStripSeparator23";
			this.toolStripSeparator23.Size = new System.Drawing.Size(6, 25);
			this.BackgroundPlayerView.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems7 = this.BackgroundPlayerView.DropDownItems;
			ToolStripItem[] backgroundPlayerViewSelected = new ToolStripItem[] { this.BackgroundPlayerViewSelected, this.BackgroundPlayerViewAll };
			dropDownItems7.AddRange(backgroundPlayerViewSelected);
			this.BackgroundPlayerView.Image = (Image)componentResourceManager.GetObject("BackgroundPlayerView.Image");
			this.BackgroundPlayerView.ImageTransparentColor = Color.Magenta;
			this.BackgroundPlayerView.Name = "BackgroundPlayerView";
			this.BackgroundPlayerView.Size = new System.Drawing.Size(123, 22);
			this.BackgroundPlayerView.Text = "Send to Player View";
			this.BackgroundPlayerViewSelected.Name = "BackgroundPlayerViewSelected";
			this.BackgroundPlayerViewSelected.Size = new System.Drawing.Size(145, 22);
			this.BackgroundPlayerViewSelected.Text = "Selected Item";
			this.BackgroundPlayerViewSelected.Click += new EventHandler(this.BackgroundPlayerViewSelected_Click);
			this.BackgroundPlayerViewAll.Name = "BackgroundPlayerViewAll";
			this.BackgroundPlayerViewAll.Size = new System.Drawing.Size(145, 22);
			this.BackgroundPlayerViewAll.Text = "All Items";
			this.BackgroundPlayerViewAll.Click += new EventHandler(this.BackgroundPlayerViewAll_Click);
			this.toolStripSeparator48.Name = "toolStripSeparator48";
			this.toolStripSeparator48.Size = new System.Drawing.Size(6, 25);
			this.BackgroundShareBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection toolStripItemCollections7 = this.BackgroundShareBtn.DropDownItems;
			ToolStripItem[] backgroundShareExport = new ToolStripItem[] { this.BackgroundShareExport, this.BackgroundShareImport, this.toolStripMenuItem10, this.BackgroundSharePublish };
			toolStripItemCollections7.AddRange(backgroundShareExport);
			this.BackgroundShareBtn.Image = (Image)componentResourceManager.GetObject("BackgroundShareBtn.Image");
			this.BackgroundShareBtn.ImageTransparentColor = Color.Magenta;
			this.BackgroundShareBtn.Name = "BackgroundShareBtn";
			this.BackgroundShareBtn.Size = new System.Drawing.Size(49, 22);
			this.BackgroundShareBtn.Text = "Share";
			this.BackgroundShareExport.Name = "BackgroundShareExport";
			this.BackgroundShareExport.Size = new System.Drawing.Size(122, 22);
			this.BackgroundShareExport.Text = "Export...";
			this.BackgroundShareExport.Click += new EventHandler(this.BackgroundShareExport_Click);
			this.BackgroundShareImport.Name = "BackgroundShareImport";
			this.BackgroundShareImport.Size = new System.Drawing.Size(122, 22);
			this.BackgroundShareImport.Text = "Import...";
			this.BackgroundShareImport.Click += new EventHandler(this.BackgroundShareImport_Click);
			this.toolStripMenuItem10.Name = "toolStripMenuItem10";
			this.toolStripMenuItem10.Size = new System.Drawing.Size(119, 6);
			this.BackgroundSharePublish.Name = "BackgroundSharePublish";
			this.BackgroundSharePublish.Size = new System.Drawing.Size(122, 22);
			this.BackgroundSharePublish.Text = "Publish...";
			this.BackgroundSharePublish.Click += new EventHandler(this.BackgroundSharePublish_Click);
			this.EncyclopediaPage.Controls.Add(this.EncyclopediaSplitter);
			this.EncyclopediaPage.Controls.Add(this.EncyclopediaToolbar);
			this.EncyclopediaPage.Location = new Point(4, 22);
			this.EncyclopediaPage.Name = "EncyclopediaPage";
			this.EncyclopediaPage.Size = new System.Drawing.Size(856, 410);
			this.EncyclopediaPage.TabIndex = 1;
			this.EncyclopediaPage.Text = "Encyclopedia";
			this.EncyclopediaPage.UseVisualStyleBackColor = true;
			this.EncyclopediaSplitter.Dock = DockStyle.Fill;
			this.EncyclopediaSplitter.FixedPanel = FixedPanel.Panel1;
			this.EncyclopediaSplitter.Location = new Point(0, 25);
			this.EncyclopediaSplitter.Name = "EncyclopediaSplitter";
			this.EncyclopediaSplitter.Panel1.Controls.Add(this.EntryList);
			this.EncyclopediaSplitter.Panel2.Controls.Add(this.EncyclopediaEntrySplitter);
			this.EncyclopediaSplitter.Size = new System.Drawing.Size(856, 385);
			this.EncyclopediaSplitter.SplitterDistance = 255;
			this.EncyclopediaSplitter.TabIndex = 3;
			this.EntryList.Columns.AddRange(new ColumnHeader[] { this.EntryHdr });
			this.EntryList.Dock = DockStyle.Fill;
			this.EntryList.FullRowSelect = true;
			this.EntryList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.EntryList.HideSelection = false;
			this.EntryList.Location = new Point(0, 0);
			this.EntryList.MultiSelect = false;
			this.EntryList.Name = "EntryList";
			this.EntryList.Size = new System.Drawing.Size(255, 385);
			this.EntryList.Sorting = SortOrder.Ascending;
			this.EntryList.TabIndex = 0;
			this.EntryList.UseCompatibleStateImageBehavior = false;
			this.EntryList.View = View.Details;
			this.EntryList.SelectedIndexChanged += new EventHandler(this.EntryList_SelectedIndexChanged);
			this.EntryList.DoubleClick += new EventHandler(this.EncEditBtn_Click);
			this.EntryHdr.Text = "Entries";
			this.EntryHdr.Width = 221;
			this.EncyclopediaEntrySplitter.Dock = DockStyle.Fill;
			this.EncyclopediaEntrySplitter.FixedPanel = FixedPanel.Panel2;
			this.EncyclopediaEntrySplitter.Location = new Point(0, 0);
			this.EncyclopediaEntrySplitter.Name = "EncyclopediaEntrySplitter";
			this.EncyclopediaEntrySplitter.Panel1.Controls.Add(this.EntryPanel);
			this.EncyclopediaEntrySplitter.Panel2.Controls.Add(this.EntryImageList);
			this.EncyclopediaEntrySplitter.Size = new System.Drawing.Size(597, 385);
			this.EncyclopediaEntrySplitter.SplitterDistance = 465;
			this.EncyclopediaEntrySplitter.TabIndex = 5;
			this.EntryPanel.BorderStyle = BorderStyle.FixedSingle;
			this.EntryPanel.Controls.Add(this.EntryDetails);
			this.EntryPanel.Dock = DockStyle.Fill;
			this.EntryPanel.Location = new Point(0, 0);
			this.EntryPanel.Name = "EntryPanel";
			this.EntryPanel.Size = new System.Drawing.Size(465, 385);
			this.EntryPanel.TabIndex = 0;
			this.EntryDetails.Dock = DockStyle.Fill;
			this.EntryDetails.IsWebBrowserContextMenuEnabled = false;
			this.EntryDetails.Location = new Point(0, 0);
			this.EntryDetails.MinimumSize = new System.Drawing.Size(20, 20);
			this.EntryDetails.Name = "EntryDetails";
			this.EntryDetails.ScriptErrorsSuppressed = true;
			this.EntryDetails.Size = new System.Drawing.Size(463, 383);
			this.EntryDetails.TabIndex = 4;
			this.EntryDetails.WebBrowserShortcutsEnabled = false;
			this.EntryDetails.Navigating += new WebBrowserNavigatingEventHandler(this.EntryDetails_Navigating);
			this.EntryImageList.Dock = DockStyle.Fill;
			this.EntryImageList.Location = new Point(0, 0);
			this.EntryImageList.Name = "EntryImageList";
			this.EntryImageList.Size = new System.Drawing.Size(128, 385);
			this.EntryImageList.TabIndex = 0;
			this.EntryImageList.UseCompatibleStateImageBehavior = false;
			this.EntryImageList.DoubleClick += new EventHandler(this.EntryImageList_DoubleClick);
			ToolStripItemCollection items6 = this.EncyclopediaToolbar.Items;
			ToolStripItem[] encAddBtn = new ToolStripItem[] { this.EncAddBtn, this.EncRemoveBtn, this.EncEditBtn, this.toolStripSeparator15, this.EncCutBtn, this.EncCopyBtn, this.EncPasteBtn, this.toolStripSeparator17, this.EncPlayerView, this.toolStripSeparator40, this.EncShareBtn, this.toolStripSeparator22, this.EncSearchLbl, this.EncSearchBox, this.EncClearLbl };
			items6.AddRange(encAddBtn);
			this.EncyclopediaToolbar.Location = new Point(0, 0);
			this.EncyclopediaToolbar.Name = "EncyclopediaToolbar";
			this.EncyclopediaToolbar.Size = new System.Drawing.Size(856, 25);
			this.EncyclopediaToolbar.TabIndex = 2;
			this.EncyclopediaToolbar.Text = "toolStrip1";
			this.EncAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems8 = this.EncAddBtn.DropDownItems;
			ToolStripItem[] encAddEntry = new ToolStripItem[] { this.EncAddEntry, this.EncAddGroup };
			dropDownItems8.AddRange(encAddEntry);
			this.EncAddBtn.Image = (Image)componentResourceManager.GetObject("EncAddBtn.Image");
			this.EncAddBtn.ImageTransparentColor = Color.Magenta;
			this.EncAddBtn.Name = "EncAddBtn";
			this.EncAddBtn.Size = new System.Drawing.Size(42, 22);
			this.EncAddBtn.Text = "Add";
			this.EncAddEntry.Name = "EncAddEntry";
			this.EncAddEntry.Size = new System.Drawing.Size(142, 22);
			this.EncAddEntry.Text = "Add an Entry";
			this.EncAddEntry.Click += new EventHandler(this.EncAddEntry_Click);
			this.EncAddGroup.Name = "EncAddGroup";
			this.EncAddGroup.Size = new System.Drawing.Size(142, 22);
			this.EncAddGroup.Text = "Add a Group";
			this.EncAddGroup.Click += new EventHandler(this.EncAddGroup_Click);
			this.EncRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EncRemoveBtn.Image = (Image)componentResourceManager.GetObject("EncRemoveBtn.Image");
			this.EncRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.EncRemoveBtn.Name = "EncRemoveBtn";
			this.EncRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.EncRemoveBtn.Text = "Remove";
			this.EncRemoveBtn.Click += new EventHandler(this.EncRemoveBtn_Click);
			this.EncEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EncEditBtn.Image = (Image)componentResourceManager.GetObject("EncEditBtn.Image");
			this.EncEditBtn.ImageTransparentColor = Color.Magenta;
			this.EncEditBtn.Name = "EncEditBtn";
			this.EncEditBtn.Size = new System.Drawing.Size(31, 22);
			this.EncEditBtn.Text = "Edit";
			this.EncEditBtn.Click += new EventHandler(this.EncEditBtn_Click);
			this.toolStripSeparator15.Name = "toolStripSeparator15";
			this.toolStripSeparator15.Size = new System.Drawing.Size(6, 25);
			this.EncCutBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EncCutBtn.Image = (Image)componentResourceManager.GetObject("EncCutBtn.Image");
			this.EncCutBtn.ImageTransparentColor = Color.Magenta;
			this.EncCutBtn.Name = "EncCutBtn";
			this.EncCutBtn.Size = new System.Drawing.Size(30, 22);
			this.EncCutBtn.Text = "Cut";
			this.EncCutBtn.Click += new EventHandler(this.EncCutBtn_Click);
			this.EncCopyBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EncCopyBtn.Image = (Image)componentResourceManager.GetObject("EncCopyBtn.Image");
			this.EncCopyBtn.ImageTransparentColor = Color.Magenta;
			this.EncCopyBtn.Name = "EncCopyBtn";
			this.EncCopyBtn.Size = new System.Drawing.Size(39, 22);
			this.EncCopyBtn.Text = "Copy";
			this.EncCopyBtn.Click += new EventHandler(this.EncCopyBtn_Click);
			this.EncPasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EncPasteBtn.Image = (Image)componentResourceManager.GetObject("EncPasteBtn.Image");
			this.EncPasteBtn.ImageTransparentColor = Color.Magenta;
			this.EncPasteBtn.Name = "EncPasteBtn";
			this.EncPasteBtn.Size = new System.Drawing.Size(39, 22);
			this.EncPasteBtn.Text = "Paste";
			this.EncPasteBtn.Click += new EventHandler(this.EncPasteBtn_Click);
			this.toolStripSeparator17.Name = "toolStripSeparator17";
			this.toolStripSeparator17.Size = new System.Drawing.Size(6, 25);
			this.EncPlayerView.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EncPlayerView.Image = (Image)componentResourceManager.GetObject("EncPlayerView.Image");
			this.EncPlayerView.ImageTransparentColor = Color.Magenta;
			this.EncPlayerView.Name = "EncPlayerView";
			this.EncPlayerView.Size = new System.Drawing.Size(114, 22);
			this.EncPlayerView.Text = "Send to Player View";
			this.EncPlayerView.Click += new EventHandler(this.EncPlayerView_Click);
			this.toolStripSeparator40.Name = "toolStripSeparator40";
			this.toolStripSeparator40.Size = new System.Drawing.Size(6, 25);
			this.EncShareBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection toolStripItemCollections8 = this.EncShareBtn.DropDownItems;
			ToolStripItem[] encShareExport = new ToolStripItem[] { this.EncShareExport, this.EncShareImport, this.toolStripMenuItem6, this.EncSharePublish };
			toolStripItemCollections8.AddRange(encShareExport);
			this.EncShareBtn.Image = (Image)componentResourceManager.GetObject("EncShareBtn.Image");
			this.EncShareBtn.ImageTransparentColor = Color.Magenta;
			this.EncShareBtn.Name = "EncShareBtn";
			this.EncShareBtn.Size = new System.Drawing.Size(49, 22);
			this.EncShareBtn.Text = "Share";
			this.EncShareExport.Name = "EncShareExport";
			this.EncShareExport.Size = new System.Drawing.Size(122, 22);
			this.EncShareExport.Text = "Export...";
			this.EncShareExport.Click += new EventHandler(this.EncShareExport_Click);
			this.EncShareImport.Name = "EncShareImport";
			this.EncShareImport.Size = new System.Drawing.Size(122, 22);
			this.EncShareImport.Text = "Import...";
			this.EncShareImport.Click += new EventHandler(this.EncShareImport_Click);
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new System.Drawing.Size(119, 6);
			this.EncSharePublish.Name = "EncSharePublish";
			this.EncSharePublish.Size = new System.Drawing.Size(122, 22);
			this.EncSharePublish.Text = "Publish...";
			this.EncSharePublish.Click += new EventHandler(this.EncSharePublish_Click);
			this.toolStripSeparator22.Name = "toolStripSeparator22";
			this.toolStripSeparator22.Size = new System.Drawing.Size(6, 25);
			this.EncSearchLbl.Name = "EncSearchLbl";
			this.EncSearchLbl.Size = new System.Drawing.Size(45, 22);
			this.EncSearchLbl.Text = "Search:";
			this.EncSearchBox.BorderStyle = BorderStyle.FixedSingle;
			this.EncSearchBox.Name = "EncSearchBox";
			this.EncSearchBox.Size = new System.Drawing.Size(150, 25);
			this.EncSearchBox.TextChanged += new EventHandler(this.EncSearchBox_TextChanged);
			this.EncClearLbl.IsLink = true;
			this.EncClearLbl.Name = "EncClearLbl";
			this.EncClearLbl.Size = new System.Drawing.Size(34, 22);
			this.EncClearLbl.Text = "Clear";
			this.EncClearLbl.Click += new EventHandler(this.EncClearLbl_Click);
			this.RulesPage.Controls.Add(this.RulesSplitter);
			this.RulesPage.Location = new Point(4, 22);
			this.RulesPage.Name = "RulesPage";
			this.RulesPage.Size = new System.Drawing.Size(856, 410);
			this.RulesPage.TabIndex = 5;
			this.RulesPage.Text = "Campaign Rules";
			this.RulesPage.UseVisualStyleBackColor = true;
			this.RulesSplitter.Dock = DockStyle.Fill;
			this.RulesSplitter.FixedPanel = FixedPanel.Panel1;
			this.RulesSplitter.Location = new Point(0, 0);
			this.RulesSplitter.Name = "RulesSplitter";
			this.RulesSplitter.Panel1.Controls.Add(this.RulesList);
			this.RulesSplitter.Panel1.Controls.Add(this.RulesToolbar);
			this.RulesSplitter.Panel2.Controls.Add(this.RulesBrowserPanel);
			this.RulesSplitter.Panel2.Controls.Add(this.EncEntryToolbar);
			this.RulesSplitter.Size = new System.Drawing.Size(856, 410);
			this.RulesSplitter.SplitterDistance = 231;
			this.RulesSplitter.TabIndex = 1;
			this.RulesList.Columns.AddRange(new ColumnHeader[] { this.RulesHdr });
			this.RulesList.Dock = DockStyle.Fill;
			this.RulesList.FullRowSelect = true;
			listViewGroup.Header = "Races";
			listViewGroup.Name = "listViewGroup1";
			listViewGroup1.Header = "Classes";
			listViewGroup1.Name = "listViewGroup9";
			listViewGroup2.Header = "Themes";
			listViewGroup2.Name = "listViewGroup14";
			listViewGroup3.Header = "Paragon Paths";
			listViewGroup3.Name = "listViewGroup2";
			listViewGroup4.Header = "Epic Destinies";
			listViewGroup4.Name = "listViewGroup3";
			listViewGroup5.Header = "Backgrounds";
			listViewGroup5.Name = "listViewGroup4";
			listViewGroup6.Header = "Feats (heroic tier)";
			listViewGroup6.Name = "listViewGroup5";
			listViewGroup7.Header = "Feats (paragon tier)";
			listViewGroup7.Name = "listViewGroup6";
			listViewGroup8.Header = "Feats (epic tier)";
			listViewGroup8.Name = "listViewGroup7";
			listViewGroup9.Header = "Weapons";
			listViewGroup9.Name = "listViewGroup10";
			listViewGroup10.Header = "Rituals";
			listViewGroup10.Name = "listViewGroup8";
			listViewGroup11.Header = "Creature Lore";
			listViewGroup11.Name = "listViewGroup11";
			listViewGroup12.Header = "Diseases";
			listViewGroup12.Name = "listViewGroup12";
			listViewGroup13.Header = "Poisons";
			listViewGroup13.Name = "listViewGroup13";
			ListViewGroupCollection groups = this.RulesList.Groups;
			ListViewGroup[] listViewGroupArray = new ListViewGroup[] { listViewGroup, listViewGroup1, listViewGroup2, listViewGroup3, listViewGroup4, listViewGroup5, listViewGroup6, listViewGroup7, listViewGroup8, listViewGroup9, listViewGroup10, listViewGroup11, listViewGroup12, listViewGroup13 };
			groups.AddRange(listViewGroupArray);
			this.RulesList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.RulesList.HideSelection = false;
			this.RulesList.Location = new Point(0, 25);
			this.RulesList.MultiSelect = false;
			this.RulesList.Name = "RulesList";
			this.RulesList.Size = new System.Drawing.Size(231, 385);
			this.RulesList.Sorting = SortOrder.Ascending;
			this.RulesList.TabIndex = 1;
			this.RulesList.UseCompatibleStateImageBehavior = false;
			this.RulesList.View = View.Details;
			this.RulesList.SelectedIndexChanged += new EventHandler(this.RulesList_SelectedIndexChanged);
			this.RulesList.DoubleClick += new EventHandler(this.RulesEditBtn_Click);
			this.RulesHdr.Text = "Rules Elements";
			this.RulesHdr.Width = 193;
			ToolStripItemCollection items7 = this.RulesToolbar.Items;
			ToolStripItem[] rulesAddBtn = new ToolStripItem[] { this.RulesAddBtn, this.toolStripSeparator33, this.RulesShareBtn };
			items7.AddRange(rulesAddBtn);
			this.RulesToolbar.Location = new Point(0, 0);
			this.RulesToolbar.Name = "RulesToolbar";
			this.RulesToolbar.Size = new System.Drawing.Size(231, 25);
			this.RulesToolbar.TabIndex = 0;
			this.RulesToolbar.Text = "toolStrip1";
			this.RulesAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems9 = this.RulesAddBtn.DropDownItems;
			ToolStripItem[] addRace = new ToolStripItem[] { this.AddRace, this.toolStripSeparator31, this.AddClass, this.AddTheme, this.AddParagonPath, this.AddEpicDestiny, this.toolStripSeparator32, this.AddBackground, this.AddFeat, this.AddWeapon, this.AddRitual, this.toolStripSeparator39, this.AddCreatureLore, this.AddDisease, this.AddPoison };
			dropDownItems9.AddRange(addRace);
			this.RulesAddBtn.Image = (Image)componentResourceManager.GetObject("RulesAddBtn.Image");
			this.RulesAddBtn.ImageTransparentColor = Color.Magenta;
			this.RulesAddBtn.Name = "RulesAddBtn";
			this.RulesAddBtn.Size = new System.Drawing.Size(42, 22);
			this.RulesAddBtn.Text = "Add";
			this.AddRace.Name = "AddRace";
			this.AddRace.Size = new System.Drawing.Size(145, 22);
			this.AddRace.Text = "Race";
			this.AddRace.Click += new EventHandler(this.AddRace_Click);
			this.toolStripSeparator31.Name = "toolStripSeparator31";
			this.toolStripSeparator31.Size = new System.Drawing.Size(142, 6);
			this.AddClass.Name = "AddClass";
			this.AddClass.Size = new System.Drawing.Size(145, 22);
			this.AddClass.Text = "Class";
			this.AddClass.Click += new EventHandler(this.AddClass_Click);
			this.AddTheme.Name = "AddTheme";
			this.AddTheme.Size = new System.Drawing.Size(145, 22);
			this.AddTheme.Text = "Theme";
			this.AddTheme.Click += new EventHandler(this.AddTheme_Click);
			this.AddParagonPath.Name = "AddParagonPath";
			this.AddParagonPath.Size = new System.Drawing.Size(145, 22);
			this.AddParagonPath.Text = "Paragon Path";
			this.AddParagonPath.Click += new EventHandler(this.AddParagonPath_Click);
			this.AddEpicDestiny.Name = "AddEpicDestiny";
			this.AddEpicDestiny.Size = new System.Drawing.Size(145, 22);
			this.AddEpicDestiny.Text = "Epic Destiny";
			this.AddEpicDestiny.Click += new EventHandler(this.AddEpicDestiny_Click);
			this.toolStripSeparator32.Name = "toolStripSeparator32";
			this.toolStripSeparator32.Size = new System.Drawing.Size(142, 6);
			this.AddBackground.Name = "AddBackground";
			this.AddBackground.Size = new System.Drawing.Size(145, 22);
			this.AddBackground.Text = "Background";
			this.AddBackground.Click += new EventHandler(this.AddBackground_Click);
			this.AddFeat.Name = "AddFeat";
			this.AddFeat.Size = new System.Drawing.Size(145, 22);
			this.AddFeat.Text = "Feat";
			this.AddFeat.Click += new EventHandler(this.AddFeat_Click);
			this.AddWeapon.Name = "AddWeapon";
			this.AddWeapon.Size = new System.Drawing.Size(145, 22);
			this.AddWeapon.Text = "Weapon";
			this.AddWeapon.Click += new EventHandler(this.AddWeapon_Click);
			this.AddRitual.Name = "AddRitual";
			this.AddRitual.Size = new System.Drawing.Size(145, 22);
			this.AddRitual.Text = "Ritual";
			this.AddRitual.Click += new EventHandler(this.AddRitual_Click);
			this.toolStripSeparator39.Name = "toolStripSeparator39";
			this.toolStripSeparator39.Size = new System.Drawing.Size(142, 6);
			this.AddCreatureLore.Name = "AddCreatureLore";
			this.AddCreatureLore.Size = new System.Drawing.Size(145, 22);
			this.AddCreatureLore.Text = "Creature Lore";
			this.AddCreatureLore.Click += new EventHandler(this.AddCreatureLore_Click);
			this.AddDisease.Name = "AddDisease";
			this.AddDisease.Size = new System.Drawing.Size(145, 22);
			this.AddDisease.Text = "Disease";
			this.AddDisease.Click += new EventHandler(this.AddDisease_Click);
			this.AddPoison.Name = "AddPoison";
			this.AddPoison.Size = new System.Drawing.Size(145, 22);
			this.AddPoison.Text = "Poison";
			this.AddPoison.Click += new EventHandler(this.AddPoison_Click);
			this.toolStripSeparator33.Name = "toolStripSeparator33";
			this.toolStripSeparator33.Size = new System.Drawing.Size(6, 25);
			this.RulesShareBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection toolStripItemCollections9 = this.RulesShareBtn.DropDownItems;
			ToolStripItem[] rulesShareExport = new ToolStripItem[] { this.RulesShareExport, this.RulesShareImport, this.toolStripMenuItem9, this.RulesSharePublish };
			toolStripItemCollections9.AddRange(rulesShareExport);
			this.RulesShareBtn.Image = (Image)componentResourceManager.GetObject("RulesShareBtn.Image");
			this.RulesShareBtn.ImageTransparentColor = Color.Magenta;
			this.RulesShareBtn.Name = "RulesShareBtn";
			this.RulesShareBtn.Size = new System.Drawing.Size(49, 22);
			this.RulesShareBtn.Text = "Share";
			this.RulesShareExport.Name = "RulesShareExport";
			this.RulesShareExport.Size = new System.Drawing.Size(122, 22);
			this.RulesShareExport.Text = "Export...";
			this.RulesShareExport.Click += new EventHandler(this.RulesShareExport_Click);
			this.RulesShareImport.Name = "RulesShareImport";
			this.RulesShareImport.Size = new System.Drawing.Size(122, 22);
			this.RulesShareImport.Text = "Import...";
			this.RulesShareImport.Click += new EventHandler(this.RulesShareImport_Click);
			this.toolStripMenuItem9.Name = "toolStripMenuItem9";
			this.toolStripMenuItem9.Size = new System.Drawing.Size(119, 6);
			this.RulesSharePublish.Name = "RulesSharePublish";
			this.RulesSharePublish.Size = new System.Drawing.Size(122, 22);
			this.RulesSharePublish.Text = "Publish...";
			this.RulesSharePublish.Click += new EventHandler(this.RulesSharePublish_Click);
			this.RulesBrowserPanel.BorderStyle = BorderStyle.FixedSingle;
			this.RulesBrowserPanel.Controls.Add(this.RulesBrowser);
			this.RulesBrowserPanel.Dock = DockStyle.Fill;
			this.RulesBrowserPanel.Location = new Point(0, 25);
			this.RulesBrowserPanel.Name = "RulesBrowserPanel";
			this.RulesBrowserPanel.Size = new System.Drawing.Size(621, 385);
			this.RulesBrowserPanel.TabIndex = 0;
			this.RulesBrowser.Dock = DockStyle.Fill;
			this.RulesBrowser.IsWebBrowserContextMenuEnabled = false;
			this.RulesBrowser.Location = new Point(0, 0);
			this.RulesBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.RulesBrowser.Name = "RulesBrowser";
			this.RulesBrowser.ScriptErrorsSuppressed = true;
			this.RulesBrowser.Size = new System.Drawing.Size(619, 383);
			this.RulesBrowser.TabIndex = 1;
			this.RulesBrowser.WebBrowserShortcutsEnabled = false;
			ToolStripItemCollection items8 = this.EncEntryToolbar.Items;
			ToolStripItem[] rulesRemoveBtn = new ToolStripItem[] { this.RulesRemoveBtn, this.RulesEditBtn, this.toolStripSeparator43, this.RuleEncyclopediaBtn, this.toolStripSeparator36, this.RulesPlayerViewBtn };
			items8.AddRange(rulesRemoveBtn);
			this.EncEntryToolbar.Location = new Point(0, 0);
			this.EncEntryToolbar.Name = "EncEntryToolbar";
			this.EncEntryToolbar.Size = new System.Drawing.Size(621, 25);
			this.EncEntryToolbar.TabIndex = 2;
			this.EncEntryToolbar.Text = "toolStrip1";
			this.RulesRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RulesRemoveBtn.Image = (Image)componentResourceManager.GetObject("RulesRemoveBtn.Image");
			this.RulesRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.RulesRemoveBtn.Name = "RulesRemoveBtn";
			this.RulesRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.RulesRemoveBtn.Text = "Remove";
			this.RulesRemoveBtn.Click += new EventHandler(this.RulesRemoveBtn_Click);
			this.RulesEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RulesEditBtn.Image = (Image)componentResourceManager.GetObject("RulesEditBtn.Image");
			this.RulesEditBtn.ImageTransparentColor = Color.Magenta;
			this.RulesEditBtn.Name = "RulesEditBtn";
			this.RulesEditBtn.Size = new System.Drawing.Size(31, 22);
			this.RulesEditBtn.Text = "Edit";
			this.RulesEditBtn.Click += new EventHandler(this.RulesEditBtn_Click);
			this.toolStripSeparator43.Name = "toolStripSeparator43";
			this.toolStripSeparator43.Size = new System.Drawing.Size(6, 25);
			this.RuleEncyclopediaBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RuleEncyclopediaBtn.Image = (Image)componentResourceManager.GetObject("RuleEncyclopediaBtn.Image");
			this.RuleEncyclopediaBtn.ImageTransparentColor = Color.Magenta;
			this.RuleEncyclopediaBtn.Name = "RuleEncyclopediaBtn";
			this.RuleEncyclopediaBtn.Size = new System.Drawing.Size(111, 22);
			this.RuleEncyclopediaBtn.Text = "Encyclopedia Entry";
			this.RuleEncyclopediaBtn.Click += new EventHandler(this.RuleEncyclopediaBtn_Click);
			this.toolStripSeparator36.Name = "toolStripSeparator36";
			this.toolStripSeparator36.Size = new System.Drawing.Size(6, 25);
			this.RulesPlayerViewBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RulesPlayerViewBtn.Image = (Image)componentResourceManager.GetObject("RulesPlayerViewBtn.Image");
			this.RulesPlayerViewBtn.ImageTransparentColor = Color.Magenta;
			this.RulesPlayerViewBtn.Name = "RulesPlayerViewBtn";
			this.RulesPlayerViewBtn.Size = new System.Drawing.Size(114, 22);
			this.RulesPlayerViewBtn.Text = "Send to Player View";
			this.RulesPlayerViewBtn.Click += new EventHandler(this.RulesPlayerViewBtn_Click);
			this.AttachmentsPage.Controls.Add(this.AttachmentList);
			this.AttachmentsPage.Controls.Add(this.AttachmentToolbar);
			this.AttachmentsPage.Location = new Point(4, 22);
			this.AttachmentsPage.Name = "AttachmentsPage";
			this.AttachmentsPage.Size = new System.Drawing.Size(856, 410);
			this.AttachmentsPage.TabIndex = 3;
			this.AttachmentsPage.Text = "Attachments";
			this.AttachmentsPage.UseVisualStyleBackColor = true;
			this.AttachmentList.AllowDrop = true;
			ListView.ColumnHeaderCollection columns = this.AttachmentList.Columns;
			ColumnHeader[] attachmentHdr = new ColumnHeader[] { this.AttachmentHdr, this.AttachmentSizeHdr };
			columns.AddRange(attachmentHdr);
			this.AttachmentList.Dock = DockStyle.Fill;
			this.AttachmentList.FullRowSelect = true;
			this.AttachmentList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.AttachmentList.HideSelection = false;
			this.AttachmentList.Location = new Point(0, 25);
			this.AttachmentList.Name = "AttachmentList";
			this.AttachmentList.Size = new System.Drawing.Size(856, 385);
			this.AttachmentList.TabIndex = 1;
			this.AttachmentList.UseCompatibleStateImageBehavior = false;
			this.AttachmentList.View = View.Details;
			this.AttachmentList.DoubleClick += new EventHandler(this.AttachmentExtractAndRun_Click);
			this.AttachmentList.DragDrop += new DragEventHandler(this.AttachmentList_DragDrop);
			this.AttachmentList.DragOver += new DragEventHandler(this.AttachmentList_DragOver);
			this.AttachmentHdr.Text = "Attachment";
			this.AttachmentHdr.Width = 500;
			this.AttachmentSizeHdr.Text = "Size";
			this.AttachmentSizeHdr.TextAlign = HorizontalAlignment.Right;
			this.AttachmentSizeHdr.Width = 100;
			ToolStripItemCollection items9 = this.AttachmentToolbar.Items;
			ToolStripItem[] attachmentImportBtn = new ToolStripItem[] { this.AttachmentImportBtn, this.AttachmentRemoveBtn, this.toolStripSeparator19, this.AttachmentExtract, this.toolStripSeparator24, this.AttachmentPlayerView };
			items9.AddRange(attachmentImportBtn);
			this.AttachmentToolbar.Location = new Point(0, 0);
			this.AttachmentToolbar.Name = "AttachmentToolbar";
			this.AttachmentToolbar.Size = new System.Drawing.Size(856, 25);
			this.AttachmentToolbar.TabIndex = 0;
			this.AttachmentToolbar.Text = "toolStrip1";
			this.AttachmentImportBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AttachmentImportBtn.Image = (Image)componentResourceManager.GetObject("AttachmentImportBtn.Image");
			this.AttachmentImportBtn.ImageTransparentColor = Color.Magenta;
			this.AttachmentImportBtn.Name = "AttachmentImportBtn";
			this.AttachmentImportBtn.Size = new System.Drawing.Size(47, 22);
			this.AttachmentImportBtn.Text = "Import";
			this.AttachmentImportBtn.Click += new EventHandler(this.AttachmentImportBtn_Click);
			this.AttachmentRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AttachmentRemoveBtn.Image = (Image)componentResourceManager.GetObject("AttachmentRemoveBtn.Image");
			this.AttachmentRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.AttachmentRemoveBtn.Name = "AttachmentRemoveBtn";
			this.AttachmentRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.AttachmentRemoveBtn.Text = "Remove";
			this.AttachmentRemoveBtn.Click += new EventHandler(this.AttachmentRemoveBtn_Click);
			this.toolStripSeparator19.Name = "toolStripSeparator19";
			this.toolStripSeparator19.Size = new System.Drawing.Size(6, 25);
			this.AttachmentExtract.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems10 = this.AttachmentExtract.DropDownItems;
			ToolStripItem[] attachmentExtractSimple = new ToolStripItem[] { this.AttachmentExtractSimple, this.AttachmentExtractAndRun };
			dropDownItems10.AddRange(attachmentExtractSimple);
			this.AttachmentExtract.Image = (Image)componentResourceManager.GetObject("AttachmentExtract.Image");
			this.AttachmentExtract.ImageTransparentColor = Color.Magenta;
			this.AttachmentExtract.Name = "AttachmentExtract";
			this.AttachmentExtract.Size = new System.Drawing.Size(55, 22);
			this.AttachmentExtract.Text = "Extract";
			this.AttachmentExtractSimple.Name = "AttachmentExtractSimple";
			this.AttachmentExtractSimple.Size = new System.Drawing.Size(224, 22);
			this.AttachmentExtractSimple.Text = "Extract to Desktop";
			this.AttachmentExtractSimple.Click += new EventHandler(this.AttachmentExtractSimple_Click);
			this.AttachmentExtractAndRun.Name = "AttachmentExtractAndRun";
			this.AttachmentExtractAndRun.Size = new System.Drawing.Size(224, 22);
			this.AttachmentExtractAndRun.Text = "Extract to Desktop and Open";
			this.AttachmentExtractAndRun.Click += new EventHandler(this.AttachmentExtractAndRun_Click);
			this.toolStripSeparator24.Name = "toolStripSeparator24";
			this.toolStripSeparator24.Size = new System.Drawing.Size(6, 25);
			this.AttachmentPlayerView.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AttachmentPlayerView.Image = (Image)componentResourceManager.GetObject("AttachmentPlayerView.Image");
			this.AttachmentPlayerView.ImageTransparentColor = Color.Magenta;
			this.AttachmentPlayerView.Name = "AttachmentPlayerView";
			this.AttachmentPlayerView.Size = new System.Drawing.Size(114, 22);
			this.AttachmentPlayerView.Text = "Send to Player View";
			this.AttachmentPlayerView.Click += new EventHandler(this.AttachmentSendBtn_Click);
			this.JotterPage.Controls.Add(this.JotterSplitter);
			this.JotterPage.Controls.Add(this.JotterToolbar);
			this.JotterPage.Location = new Point(4, 22);
			this.JotterPage.Name = "JotterPage";
			this.JotterPage.Size = new System.Drawing.Size(856, 410);
			this.JotterPage.TabIndex = 2;
			this.JotterPage.Text = "Jotter";
			this.JotterPage.UseVisualStyleBackColor = true;
			this.JotterSplitter.Dock = DockStyle.Fill;
			this.JotterSplitter.FixedPanel = FixedPanel.Panel1;
			this.JotterSplitter.Location = new Point(0, 25);
			this.JotterSplitter.Name = "JotterSplitter";
			this.JotterSplitter.Panel1.Controls.Add(this.NoteList);
			this.JotterSplitter.Panel2.Controls.Add(this.NoteBox);
			this.JotterSplitter.Size = new System.Drawing.Size(856, 385);
			this.JotterSplitter.SplitterDistance = 180;
			this.JotterSplitter.TabIndex = 1;
			this.NoteList.Columns.AddRange(new ColumnHeader[] { this.NoteHdr });
			this.NoteList.Dock = DockStyle.Fill;
			this.NoteList.FullRowSelect = true;
			listViewGroup14.Header = "Issues";
			listViewGroup14.Name = "IssueGroup";
			listViewGroup15.Header = "Information";
			listViewGroup15.Name = "InfoGroup";
			listViewGroup16.Header = "Notes";
			listViewGroup16.Name = "NoteGroup";
			this.NoteList.Groups.AddRange(new ListViewGroup[] { listViewGroup14, listViewGroup15, listViewGroup16 });
			this.NoteList.HeaderStyle = ColumnHeaderStyle.None;
			this.NoteList.HideSelection = false;
			this.NoteList.Location = new Point(0, 0);
			this.NoteList.MultiSelect = false;
			this.NoteList.Name = "NoteList";
			this.NoteList.Size = new System.Drawing.Size(180, 385);
			this.NoteList.Sorting = SortOrder.Ascending;
			this.NoteList.TabIndex = 0;
			this.NoteList.UseCompatibleStateImageBehavior = false;
			this.NoteList.View = View.Details;
			this.NoteList.SelectedIndexChanged += new EventHandler(this.NoteList_SelectedIndexChanged);
			this.NoteHdr.Text = "Notes";
			this.NoteHdr.Width = 150;
			this.NoteBox.BorderStyle = BorderStyle.FixedSingle;
			this.NoteBox.Dock = DockStyle.Fill;
			this.NoteBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.NoteBox.Location = new Point(0, 0);
			this.NoteBox.Multiline = true;
			this.NoteBox.Name = "NoteBox";
			this.NoteBox.ScrollBars = ScrollBars.Vertical;
			this.NoteBox.Size = new System.Drawing.Size(672, 385);
			this.NoteBox.TabIndex = 0;
			this.NoteBox.TextChanged += new EventHandler(this.NoteBox_TextChanged);
			ToolStripItemCollection items10 = this.JotterToolbar.Items;
			ToolStripItem[] noteAddBtn = new ToolStripItem[] { this.NoteAddBtn, this.NoteRemoveBtn, this.toolStripSeparator16, this.NoteCategoryBtn, this.toolStripSeparator38, this.NoteCutBtn, this.NoteCopyBtn, this.NotePasteBtn, this.toolStripSeparator18, this.NoteSearchLbl, this.NoteSearchBox, this.NoteClearLbl };
			items10.AddRange(noteAddBtn);
			this.JotterToolbar.Location = new Point(0, 0);
			this.JotterToolbar.Name = "JotterToolbar";
			this.JotterToolbar.Size = new System.Drawing.Size(856, 25);
			this.JotterToolbar.TabIndex = 0;
			this.JotterToolbar.Text = "toolStrip1";
			this.NoteAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NoteAddBtn.Image = (Image)componentResourceManager.GetObject("NoteAddBtn.Image");
			this.NoteAddBtn.ImageTransparentColor = Color.Magenta;
			this.NoteAddBtn.Name = "NoteAddBtn";
			this.NoteAddBtn.Size = new System.Drawing.Size(62, 22);
			this.NoteAddBtn.Text = "Add Note";
			this.NoteAddBtn.Click += new EventHandler(this.NoteAddBtn_Click);
			this.NoteRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NoteRemoveBtn.Image = (Image)componentResourceManager.GetObject("NoteRemoveBtn.Image");
			this.NoteRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.NoteRemoveBtn.Name = "NoteRemoveBtn";
			this.NoteRemoveBtn.Size = new System.Drawing.Size(83, 22);
			this.NoteRemoveBtn.Text = "Remove Note";
			this.NoteRemoveBtn.Click += new EventHandler(this.NoteRemoveBtn_Click);
			this.toolStripSeparator16.Name = "toolStripSeparator16";
			this.toolStripSeparator16.Size = new System.Drawing.Size(6, 25);
			this.NoteCategoryBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NoteCategoryBtn.Image = (Image)componentResourceManager.GetObject("NoteCategoryBtn.Image");
			this.NoteCategoryBtn.ImageTransparentColor = Color.Magenta;
			this.NoteCategoryBtn.Name = "NoteCategoryBtn";
			this.NoteCategoryBtn.Size = new System.Drawing.Size(78, 22);
			this.NoteCategoryBtn.Text = "Set Category";
			this.NoteCategoryBtn.Click += new EventHandler(this.NoteCategoryBtn_Click);
			this.toolStripSeparator38.Name = "toolStripSeparator38";
			this.toolStripSeparator38.Size = new System.Drawing.Size(6, 25);
			this.NoteCutBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NoteCutBtn.Image = (Image)componentResourceManager.GetObject("NoteCutBtn.Image");
			this.NoteCutBtn.ImageTransparentColor = Color.Magenta;
			this.NoteCutBtn.Name = "NoteCutBtn";
			this.NoteCutBtn.Size = new System.Drawing.Size(30, 22);
			this.NoteCutBtn.Text = "Cut";
			this.NoteCutBtn.Click += new EventHandler(this.NoteCutBtn_Click);
			this.NoteCopyBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NoteCopyBtn.Image = (Image)componentResourceManager.GetObject("NoteCopyBtn.Image");
			this.NoteCopyBtn.ImageTransparentColor = Color.Magenta;
			this.NoteCopyBtn.Name = "NoteCopyBtn";
			this.NoteCopyBtn.Size = new System.Drawing.Size(39, 22);
			this.NoteCopyBtn.Text = "Copy";
			this.NoteCopyBtn.Click += new EventHandler(this.NoteCopyBtn_Click);
			this.NotePasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NotePasteBtn.Image = (Image)componentResourceManager.GetObject("NotePasteBtn.Image");
			this.NotePasteBtn.ImageTransparentColor = Color.Magenta;
			this.NotePasteBtn.Name = "NotePasteBtn";
			this.NotePasteBtn.Size = new System.Drawing.Size(39, 22);
			this.NotePasteBtn.Text = "Paste";
			this.NotePasteBtn.Click += new EventHandler(this.NotePasteBtn_Click);
			this.toolStripSeparator18.Name = "toolStripSeparator18";
			this.toolStripSeparator18.Size = new System.Drawing.Size(6, 25);
			this.NoteSearchLbl.Name = "NoteSearchLbl";
			this.NoteSearchLbl.Size = new System.Drawing.Size(45, 22);
			this.NoteSearchLbl.Text = "Search:";
			this.NoteSearchBox.BorderStyle = BorderStyle.FixedSingle;
			this.NoteSearchBox.Name = "NoteSearchBox";
			this.NoteSearchBox.Size = new System.Drawing.Size(150, 25);
			this.NoteSearchBox.TextChanged += new EventHandler(this.NoteSearchBox_TextChanged);
			this.NoteClearLbl.IsLink = true;
			this.NoteClearLbl.Name = "NoteClearLbl";
			this.NoteClearLbl.Size = new System.Drawing.Size(34, 22);
			this.NoteClearLbl.Text = "Clear";
			this.NoteClearLbl.Click += new EventHandler(this.NoteClearLbl_Click);
			this.ReferencePage.Controls.Add(this.ReferenceSplitter);
			this.ReferencePage.Location = new Point(4, 22);
			this.ReferencePage.Name = "ReferencePage";
			this.ReferencePage.Size = new System.Drawing.Size(856, 410);
			this.ReferencePage.TabIndex = 6;
			this.ReferencePage.Text = "In-Session Reference";
			this.ReferencePage.UseVisualStyleBackColor = true;
			this.ReferenceSplitter.Dock = DockStyle.Fill;
			this.ReferenceSplitter.FixedPanel = FixedPanel.Panel2;
			this.ReferenceSplitter.Location = new Point(0, 0);
			this.ReferenceSplitter.Name = "ReferenceSplitter";
			this.ReferenceSplitter.Panel1.Controls.Add(this.ReferencePages);
			this.ReferenceSplitter.Panel2.Controls.Add(this.InfoPanel);
			this.ReferenceSplitter.Panel2.Controls.Add(this.ReferenceToolbar);
			this.ReferenceSplitter.Size = new System.Drawing.Size(856, 410);
			this.ReferenceSplitter.SplitterDistance = 594;
			this.ReferenceSplitter.TabIndex = 1;
			this.ReferencePages.Alignment = TabAlignment.Left;
			this.ReferencePages.Controls.Add(this.PartyPage);
			this.ReferencePages.Controls.Add(this.ToolsPage);
			this.ReferencePages.Controls.Add(this.CompendiumPage);
			this.ReferencePages.Dock = DockStyle.Fill;
			this.ReferencePages.Location = new Point(0, 0);
			this.ReferencePages.Multiline = true;
			this.ReferencePages.Name = "ReferencePages";
			this.ReferencePages.SelectedIndex = 0;
			this.ReferencePages.Size = new System.Drawing.Size(594, 410);
			this.ReferencePages.TabIndex = 0;
			this.ReferencePages.SelectedIndexChanged += new EventHandler(this.ReferencePages_SelectedIndexChanged);
			this.PartyPage.Controls.Add(this.PartyBrowser);
			this.PartyPage.Location = new Point(23, 4);
			this.PartyPage.Name = "PartyPage";
			this.PartyPage.Size = new System.Drawing.Size(567, 402);
			this.PartyPage.TabIndex = 0;
			this.PartyPage.Text = "Party Breakdown";
			this.PartyPage.UseVisualStyleBackColor = true;
			this.PartyBrowser.AllowWebBrowserDrop = false;
			this.PartyBrowser.Dock = DockStyle.Fill;
			this.PartyBrowser.IsWebBrowserContextMenuEnabled = false;
			this.PartyBrowser.Location = new Point(0, 0);
			this.PartyBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.PartyBrowser.Name = "PartyBrowser";
			this.PartyBrowser.ScriptErrorsSuppressed = true;
			this.PartyBrowser.Size = new System.Drawing.Size(567, 402);
			this.PartyBrowser.TabIndex = 0;
			this.PartyBrowser.WebBrowserShortcutsEnabled = false;
			this.PartyBrowser.Navigating += new WebBrowserNavigatingEventHandler(this.PartyBrowser_Navigating);
			this.ToolsPage.Controls.Add(this.ToolBrowserPanel);
			this.ToolsPage.Controls.Add(this.GeneratorToolbar);
			this.ToolsPage.Location = new Point(23, 4);
			this.ToolsPage.Name = "ToolsPage";
			this.ToolsPage.Size = new System.Drawing.Size(567, 402);
			this.ToolsPage.TabIndex = 1;
			this.ToolsPage.Text = "Random Generators";
			this.ToolsPage.UseVisualStyleBackColor = true;
			this.ToolBrowserPanel.BorderStyle = BorderStyle.FixedSingle;
			this.ToolBrowserPanel.Controls.Add(this.GeneratorBrowser);
			this.ToolBrowserPanel.Dock = DockStyle.Fill;
			this.ToolBrowserPanel.Location = new Point(107, 0);
			this.ToolBrowserPanel.Name = "ToolBrowserPanel";
			this.ToolBrowserPanel.Size = new System.Drawing.Size(460, 402);
			this.ToolBrowserPanel.TabIndex = 3;
			this.GeneratorBrowser.AllowWebBrowserDrop = false;
			this.GeneratorBrowser.Dock = DockStyle.Fill;
			this.GeneratorBrowser.IsWebBrowserContextMenuEnabled = false;
			this.GeneratorBrowser.Location = new Point(0, 0);
			this.GeneratorBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.GeneratorBrowser.Name = "GeneratorBrowser";
			this.GeneratorBrowser.ScriptErrorsSuppressed = true;
			this.GeneratorBrowser.Size = new System.Drawing.Size(458, 400);
			this.GeneratorBrowser.TabIndex = 1;
			this.GeneratorBrowser.Navigating += new WebBrowserNavigatingEventHandler(this.GeneratorBrowser_Navigating);
			this.GeneratorToolbar.Dock = DockStyle.Left;
			this.GeneratorToolbar.GripStyle = ToolStripGripStyle.Hidden;
			ToolStripItemCollection toolStripItemCollections10 = this.GeneratorToolbar.Items;
			ToolStripItem[] elfNameBtn = new ToolStripItem[] { this.toolStripLabel1, this.toolStripSeparator26, this.ElfNameBtn, this.DwarfNameBtn, this.HalflingNameBtn, this.ExoticNameBtn, this.toolStripSeparator44, this.TreasureBtn, this.BookTitleBtn, this.PotionBtn, this.toolStripSeparator45, this.NPCBtn, this.RoomBtn, this.toolStripSeparator46, this.ElfTextBtn, this.DwarfTextBtn, this.PrimordialTextBtn };
			toolStripItemCollections10.AddRange(elfNameBtn);
			this.GeneratorToolbar.Location = new Point(0, 0);
			this.GeneratorToolbar.Name = "GeneratorToolbar";
			this.GeneratorToolbar.ShowItemToolTips = false;
			this.GeneratorToolbar.Size = new System.Drawing.Size(107, 402);
			this.GeneratorToolbar.TabIndex = 2;
			this.GeneratorToolbar.Text = "toolStrip1";
			this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9f, FontStyle.Bold);
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(104, 15);
			this.toolStripLabel1.Text = "Generators";
			this.toolStripSeparator26.Name = "toolStripSeparator26";
			this.toolStripSeparator26.Size = new System.Drawing.Size(104, 6);
			this.ElfNameBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ElfNameBtn.Image = (Image)componentResourceManager.GetObject("ElfNameBtn.Image");
			this.ElfNameBtn.ImageTransparentColor = Color.Magenta;
			this.ElfNameBtn.Name = "ElfNameBtn";
			this.ElfNameBtn.Size = new System.Drawing.Size(104, 19);
			this.ElfNameBtn.Text = "Elvish Names";
			this.ElfNameBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.ElfNameBtn.Click += new EventHandler(this.ElfNameBtn_Click);
			this.DwarfNameBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DwarfNameBtn.Image = (Image)componentResourceManager.GetObject("DwarfNameBtn.Image");
			this.DwarfNameBtn.ImageTransparentColor = Color.Magenta;
			this.DwarfNameBtn.Name = "DwarfNameBtn";
			this.DwarfNameBtn.Size = new System.Drawing.Size(104, 19);
			this.DwarfNameBtn.Text = "Dwarvish Names";
			this.DwarfNameBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.DwarfNameBtn.Click += new EventHandler(this.DwarfNameBtn_Click);
			this.HalflingNameBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.HalflingNameBtn.Image = (Image)componentResourceManager.GetObject("HalflingNameBtn.Image");
			this.HalflingNameBtn.ImageTransparentColor = Color.Magenta;
			this.HalflingNameBtn.Name = "HalflingNameBtn";
			this.HalflingNameBtn.Size = new System.Drawing.Size(104, 19);
			this.HalflingNameBtn.Text = "Halfling Names";
			this.HalflingNameBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.HalflingNameBtn.Click += new EventHandler(this.HalflingNameBtn_Click);
			this.ExoticNameBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ExoticNameBtn.Image = (Image)componentResourceManager.GetObject("ExoticNameBtn.Image");
			this.ExoticNameBtn.ImageTransparentColor = Color.Magenta;
			this.ExoticNameBtn.Name = "ExoticNameBtn";
			this.ExoticNameBtn.Size = new System.Drawing.Size(104, 19);
			this.ExoticNameBtn.Text = "Exotic Names";
			this.ExoticNameBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.ExoticNameBtn.Click += new EventHandler(this.ExoticNameBtn_Click);
			this.toolStripSeparator44.Name = "toolStripSeparator44";
			this.toolStripSeparator44.Size = new System.Drawing.Size(104, 6);
			this.TreasureBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TreasureBtn.Image = (Image)componentResourceManager.GetObject("TreasureBtn.Image");
			this.TreasureBtn.ImageTransparentColor = Color.Magenta;
			this.TreasureBtn.Name = "TreasureBtn";
			this.TreasureBtn.Size = new System.Drawing.Size(104, 19);
			this.TreasureBtn.Text = "Art Objects";
			this.TreasureBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.TreasureBtn.Click += new EventHandler(this.TreasureBtn_Click);
			this.BookTitleBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.BookTitleBtn.Image = (Image)componentResourceManager.GetObject("BookTitleBtn.Image");
			this.BookTitleBtn.ImageTransparentColor = Color.Magenta;
			this.BookTitleBtn.Name = "BookTitleBtn";
			this.BookTitleBtn.Size = new System.Drawing.Size(104, 19);
			this.BookTitleBtn.Text = "Book Titles";
			this.BookTitleBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.BookTitleBtn.Click += new EventHandler(this.BookTitleBtn_Click);
			this.PotionBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PotionBtn.Image = (Image)componentResourceManager.GetObject("PotionBtn.Image");
			this.PotionBtn.ImageTransparentColor = Color.Magenta;
			this.PotionBtn.Name = "PotionBtn";
			this.PotionBtn.Size = new System.Drawing.Size(104, 19);
			this.PotionBtn.Text = "Potions";
			this.PotionBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.PotionBtn.Click += new EventHandler(this.PotionBtn_Click);
			this.toolStripSeparator45.Name = "toolStripSeparator45";
			this.toolStripSeparator45.Size = new System.Drawing.Size(104, 6);
			this.NPCBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NPCBtn.Image = (Image)componentResourceManager.GetObject("NPCBtn.Image");
			this.NPCBtn.ImageTransparentColor = Color.Magenta;
			this.NPCBtn.Name = "NPCBtn";
			this.NPCBtn.Size = new System.Drawing.Size(104, 19);
			this.NPCBtn.Text = "NPC Description";
			this.NPCBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.NPCBtn.Click += new EventHandler(this.NPCBtn_Click);
			this.RoomBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RoomBtn.Image = (Image)componentResourceManager.GetObject("RoomBtn.Image");
			this.RoomBtn.ImageTransparentColor = Color.Magenta;
			this.RoomBtn.Name = "RoomBtn";
			this.RoomBtn.Size = new System.Drawing.Size(104, 19);
			this.RoomBtn.Text = "Room Description";
			this.RoomBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.RoomBtn.Click += new EventHandler(this.RoomBtn_Click);
			this.toolStripSeparator46.Name = "toolStripSeparator46";
			this.toolStripSeparator46.Size = new System.Drawing.Size(104, 6);
			this.ElfTextBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ElfTextBtn.Image = (Image)componentResourceManager.GetObject("ElfTextBtn.Image");
			this.ElfTextBtn.ImageTransparentColor = Color.Magenta;
			this.ElfTextBtn.Name = "ElfTextBtn";
			this.ElfTextBtn.Size = new System.Drawing.Size(104, 19);
			this.ElfTextBtn.Text = "Elvish Text";
			this.ElfTextBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.ElfTextBtn.Click += new EventHandler(this.ElfTextBtn_Click);
			this.DwarfTextBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DwarfTextBtn.Image = (Image)componentResourceManager.GetObject("DwarfTextBtn.Image");
			this.DwarfTextBtn.ImageTransparentColor = Color.Magenta;
			this.DwarfTextBtn.Name = "DwarfTextBtn";
			this.DwarfTextBtn.Size = new System.Drawing.Size(104, 19);
			this.DwarfTextBtn.Text = "Dwarvish Text";
			this.DwarfTextBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.DwarfTextBtn.Click += new EventHandler(this.DwarfTextBtn_Click);
			this.PrimordialTextBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PrimordialTextBtn.Image = (Image)componentResourceManager.GetObject("PrimordialTextBtn.Image");
			this.PrimordialTextBtn.ImageTransparentColor = Color.Magenta;
			this.PrimordialTextBtn.Name = "PrimordialTextBtn";
			this.PrimordialTextBtn.Size = new System.Drawing.Size(104, 19);
			this.PrimordialTextBtn.Text = "Primordial Text";
			this.PrimordialTextBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.PrimordialTextBtn.Click += new EventHandler(this.PrimordialTextBtn_Click);
			this.CompendiumPage.Controls.Add(this.CompendiumBrowser);
			this.CompendiumPage.Location = new Point(23, 4);
			this.CompendiumPage.Name = "CompendiumPage";
			this.CompendiumPage.Padding = new System.Windows.Forms.Padding(3);
			this.CompendiumPage.Size = new System.Drawing.Size(567, 402);
			this.CompendiumPage.TabIndex = 2;
			this.CompendiumPage.Text = "Compendium";
			this.CompendiumPage.UseVisualStyleBackColor = true;
			this.CompendiumBrowser.AllowWebBrowserDrop = false;
			this.CompendiumBrowser.Dock = DockStyle.Fill;
			this.CompendiumBrowser.Location = new Point(3, 3);
			this.CompendiumBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.CompendiumBrowser.Name = "CompendiumBrowser";
			this.CompendiumBrowser.ScriptErrorsSuppressed = true;
			this.CompendiumBrowser.Size = new System.Drawing.Size(561, 396);
			this.CompendiumBrowser.TabIndex = 0;
			this.InfoPanel.Dock = DockStyle.Fill;
			this.InfoPanel.Level = 1;
			this.InfoPanel.Location = new Point(0, 25);
			this.InfoPanel.Name = "InfoPanel";
			this.InfoPanel.Size = new System.Drawing.Size(258, 385);
			this.InfoPanel.TabIndex = 0;
			this.ReferenceToolbar.Items.AddRange(new ToolStripItem[] { this.DieRollerBtn });
			this.ReferenceToolbar.Location = new Point(0, 0);
			this.ReferenceToolbar.Name = "ReferenceToolbar";
			this.ReferenceToolbar.Size = new System.Drawing.Size(258, 25);
			this.ReferenceToolbar.TabIndex = 1;
			this.ReferenceToolbar.Text = "toolStrip1";
			this.DieRollerBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DieRollerBtn.Image = (Image)componentResourceManager.GetObject("DieRollerBtn.Image");
			this.DieRollerBtn.ImageTransparentColor = Color.Magenta;
			this.DieRollerBtn.Name = "DieRollerBtn";
			this.DieRollerBtn.Size = new System.Drawing.Size(61, 22);
			this.DieRollerBtn.Text = "Die Roller";
			this.DieRollerBtn.Click += new EventHandler(this.DieRollerBtn_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(864, 460);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.MainMenu);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MainMenuStrip = this.MainMenu;
			base.Name = "MainForm";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Masterplan";
			base.Shown += new EventHandler(this.MainForm_Shown);
			base.Layout += new LayoutEventHandler(this.MainForm_Layout);
			base.FormClosing += new FormClosingEventHandler(this.MainForm_FormClosing);
			this.WorkspaceToolbar.ResumeLayout(false);
			this.WorkspaceToolbar.PerformLayout();
			this.PointMenu.ResumeLayout(false);
			this.MainMenu.ResumeLayout(false);
			this.MainMenu.PerformLayout();
			this.PreviewSplitter.Panel1.ResumeLayout(false);
			this.PreviewSplitter.Panel1.PerformLayout();
			this.PreviewSplitter.Panel2.ResumeLayout(false);
			this.PreviewSplitter.ResumeLayout(false);
			this.NavigationSplitter.Panel1.ResumeLayout(false);
			this.NavigationSplitter.Panel2.ResumeLayout(false);
			this.NavigationSplitter.Panel2.PerformLayout();
			this.NavigationSplitter.ResumeLayout(false);
			this.PlotPanel.ResumeLayout(false);
			this.PlotPanel.PerformLayout();
			this.WorkspaceSearchBar.ResumeLayout(false);
			this.WorkspaceSearchBar.PerformLayout();
			this.PreviewInfoSplitter.Panel1.ResumeLayout(false);
			this.PreviewInfoSplitter.Panel1.PerformLayout();
			this.PreviewInfoSplitter.ResumeLayout(false);
			this.PreviewPanel.ResumeLayout(false);
			this.PreviewToolbar.ResumeLayout(false);
			this.PreviewToolbar.PerformLayout();
			this.Pages.ResumeLayout(false);
			this.WorkspacePage.ResumeLayout(false);
			this.BackgroundPage.ResumeLayout(false);
			this.BackgroundPage.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.BackgroundPanel.ResumeLayout(false);
			this.BackgroundToolbar.ResumeLayout(false);
			this.BackgroundToolbar.PerformLayout();
			this.EncyclopediaPage.ResumeLayout(false);
			this.EncyclopediaPage.PerformLayout();
			this.EncyclopediaSplitter.Panel1.ResumeLayout(false);
			this.EncyclopediaSplitter.Panel2.ResumeLayout(false);
			this.EncyclopediaSplitter.ResumeLayout(false);
			this.EncyclopediaEntrySplitter.Panel1.ResumeLayout(false);
			this.EncyclopediaEntrySplitter.Panel2.ResumeLayout(false);
			this.EncyclopediaEntrySplitter.ResumeLayout(false);
			this.EntryPanel.ResumeLayout(false);
			this.EncyclopediaToolbar.ResumeLayout(false);
			this.EncyclopediaToolbar.PerformLayout();
			this.RulesPage.ResumeLayout(false);
			this.RulesSplitter.Panel1.ResumeLayout(false);
			this.RulesSplitter.Panel1.PerformLayout();
			this.RulesSplitter.Panel2.ResumeLayout(false);
			this.RulesSplitter.Panel2.PerformLayout();
			this.RulesSplitter.ResumeLayout(false);
			this.RulesToolbar.ResumeLayout(false);
			this.RulesToolbar.PerformLayout();
			this.RulesBrowserPanel.ResumeLayout(false);
			this.EncEntryToolbar.ResumeLayout(false);
			this.EncEntryToolbar.PerformLayout();
			this.AttachmentsPage.ResumeLayout(false);
			this.AttachmentsPage.PerformLayout();
			this.AttachmentToolbar.ResumeLayout(false);
			this.AttachmentToolbar.PerformLayout();
			this.JotterPage.ResumeLayout(false);
			this.JotterPage.PerformLayout();
			this.JotterSplitter.Panel1.ResumeLayout(false);
			this.JotterSplitter.Panel2.ResumeLayout(false);
			this.JotterSplitter.Panel2.PerformLayout();
			this.JotterSplitter.ResumeLayout(false);
			this.JotterToolbar.ResumeLayout(false);
			this.JotterToolbar.PerformLayout();
			this.ReferencePage.ResumeLayout(false);
			this.ReferenceSplitter.Panel1.ResumeLayout(false);
			this.ReferenceSplitter.Panel2.ResumeLayout(false);
			this.ReferenceSplitter.Panel2.PerformLayout();
			this.ReferenceSplitter.ResumeLayout(false);
			this.ReferencePages.ResumeLayout(false);
			this.PartyPage.ResumeLayout(false);
			this.ToolsPage.ResumeLayout(false);
			this.ToolsPage.PerformLayout();
			this.ToolBrowserPanel.ResumeLayout(false);
			this.GeneratorToolbar.ResumeLayout(false);
			this.GeneratorToolbar.PerformLayout();
			this.CompendiumPage.ResumeLayout(false);
			this.ReferenceToolbar.ResumeLayout(false);
			this.ReferenceToolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				if (!this.check_modified())
				{
					e.Cancel = true;
				}
				if (Session.FileName != "")
				{
					Session.Preferences.LastFile = Session.FileName;
				}
				Session.Preferences.Maximised = base.WindowState == FormWindowState.Maximized;
				if (!Session.Preferences.Maximised)
				{
					Session.Preferences.Maximised = false;
					Session.Preferences.Size = base.Size;
					Session.Preferences.Location = base.Location;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void MainForm_Layout(object sender, LayoutEventArgs e)
		{
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			try
			{
				Session.MainForm = this;
				if (Program.SplashScreen != null)
				{
					Program.SplashScreen.Close();
					Program.SplashScreen = null;
				}
				this.PlotView_SelectionChanged(null, null);
				this.NoteList_SelectedIndexChanged(null, null);
				if (Session.DisabledLibraries != null && Session.DisabledLibraries.Count != 0)
				{
					string str = "Due to copy protection, some libraries were not loaded:";
					str = string.Concat(str, Environment.NewLine);
					List<string> strs = new List<string>(Session.DisabledLibraries);
					int num = Math.Min(strs.Count, 6);
					for (int i = 0; i != num; i++)
					{
						int num1 = Session.Random.Next(strs.Count);
						string item = strs[num1];
						strs.Remove(item);
						str = string.Concat(str, Environment.NewLine);
						str = string.Concat(str, "* ", item);
					}
					if (strs.Count > 0)
					{
						str = string.Concat(str, Environment.NewLine, Environment.NewLine);
						object obj = str;
						object[] count = new object[] { obj, "... and ", strs.Count, " others." };
						str = string.Concat(count);
					}
					MessageBox.Show(str, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				if (Session.Project == null && Session.Creatures.Count == 0 && FileName.Directory(Application.ExecutablePath).Contains("Program Files"))
				{
					string str1 = "You're running Masterplan from the Program Files folder.";
					str1 = string.Concat(str1, Environment.NewLine, Environment.NewLine);
					str1 = string.Concat(str1, "Although Masterplan will run, this is a protected folder, and Masterplan won't be able to save any changes that you make to your libraries.");
					str1 = string.Concat(str1, Environment.NewLine, Environment.NewLine);
					str1 = string.Concat(str1, "If you move Masterplan to a new location (like My Documents or the Desktop), you won't have this problem.");
					MessageBox.Show(str1, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void map_view(RegionalMap map)
		{
			if (map == null)
			{
				return;
			}
			foreach (Control control in this.PreviewSplitter.Panel1.Controls)
			{
				control.Visible = false;
			}
			RegionalMapPanel regionalMapPanel = new RegionalMapPanel()
			{
				Map = map,
				Plot = this.PlotView.Plot,
				Mode = MapViewMode.Thumbnail,
				BorderStyle = BorderStyle.FixedSingle,
				Dock = DockStyle.Fill
			};
			this.PreviewSplitter.Panel1.Controls.Add(regionalMapPanel);
			regionalMapPanel.SelectedLocationModified += new EventHandler(this.select_maplocation);
			regionalMapPanel.DoubleClick += new EventHandler(this.edit_maplocation);
			this.fMapView = regionalMapPanel;
			this.fView = MainForm.ViewType.Map;
			this.update_preview();
		}

		private void map_view_edit()
		{
			RegionalMap map = this.fMapView.Map;
			int num = Session.Project.RegionalMaps.IndexOf(map);
			RegionalMapForm regionalMapForm = new RegionalMapForm(map, null);
			if (regionalMapForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Project.RegionalMaps[num] = regionalMapForm.Map;
				Session.Modified = true;
				this.fMapView.Map = regionalMapForm.Map;
			}
		}

		private bool match(EncyclopediaEntry entry, string[] tokens)
		{
			bool flag;
			try
			{
				string[] strArrays = tokens;
				int num = 0;
				while (num < (int)strArrays.Length)
				{
					if (this.match(entry, strArrays[num]))
					{
						num++;
					}
					else
					{
						flag = false;
						return flag;
					}
				}
				flag = true;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
				return false;
			}
			return flag;
		}

		private bool match(EncyclopediaEntry entry, string token)
		{
			bool flag;
			try
			{
				if (!entry.Name.ToLower().Contains(token))
				{
					flag = (!entry.Details.ToLower().Contains(token) ? false : true);
				}
				else
				{
					flag = true;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
				return false;
			}
			return flag;
		}

		private bool match(Note n, string[] tokens)
		{
			bool flag;
			try
			{
				string[] strArrays = tokens;
				int num = 0;
				while (num < (int)strArrays.Length)
				{
					if (this.match(n, strArrays[num]))
					{
						num++;
					}
					else
					{
						flag = false;
						return flag;
					}
				}
				flag = true;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
				return false;
			}
			return flag;
		}

		private bool match(Note n, string token)
		{
			bool flag;
			try
			{
				flag = n.Content.ToLower().Contains(token);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
				return false;
			}
			return flag;
		}

		private void move_to_subplot(object sender, EventArgs e)
		{
			try
			{
				Pair<PlotPoint, PlotPoint> tag = (sender as ToolStripMenuItem).Tag as Pair<PlotPoint, PlotPoint>;
				this.PlotView.Plot.RemovePoint(tag.Second);
				tag.First.Subplot.Points.Add(tag.Second);
				Session.Modified = true;
				this.PlotView.RecalculateLayout();
				this.UpdateView();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void NavigationTree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			try
			{
				if (!this.fUpdating)
				{
					Plot tag = e.Node.Tag as Plot;
					if (this.PlotView.Plot != tag)
					{
						this.PlotView.Plot = tag;
						this.UpdateView();
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void NavigationTree_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				PlotPoint data = e.Data.GetData(typeof(PlotPoint)) as PlotPoint;
				if (data != null)
				{
					Point client = this.NavigationTree.PointToClient(System.Windows.Forms.Cursor.Position);
					TreeNode nodeAt = this.NavigationTree.GetNodeAt(client);
					if (nodeAt != null)
					{
						Plot tag = nodeAt.Tag as Plot;
						this.NavigationTree.SelectedNode = nodeAt;
						if (tag.Points.Contains(data))
						{
							return;
						}
						else if (data != Session.Project.FindParent(tag))
						{
							Plot plot = Session.Project.FindParent(data);
							plot.RemovePoint(data);
							data.Links.Clear();
							plot.Points.Remove(data);
							tag.Points.Add(data);
							Session.Modified = true;
							this.UpdateView();
						}
						else
						{
							return;
						}
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

		private void NavigationTree_DragOver(object sender, DragEventArgs e)
		{
			try
			{
				e.Effect = DragDropEffects.None;
				PlotPoint data = e.Data.GetData(typeof(PlotPoint)) as PlotPoint;
				if (data != null)
				{
					Point client = this.NavigationTree.PointToClient(System.Windows.Forms.Cursor.Position);
					TreeNode nodeAt = this.NavigationTree.GetNodeAt(client);
					if (nodeAt != null)
					{
						Plot tag = nodeAt.Tag as Plot;
						if (tag.Points.Contains(data))
						{
							return;
						}
						else if (data != Session.Project.FindParent(tag))
						{
							this.NavigationTree.SelectedNode = nodeAt;
							e.Effect = DragDropEffects.Move;
						}
						else
						{
							return;
						}
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

		private void NoteAddBtn_Click(object sender, EventArgs e)
		{
			try
			{
				Note note = new Note();
				Session.Project.Notes.Add(note);
				Session.Modified = true;
				this.update_notes();
				this.SelectedNote = note;
				this.NoteBox.Focus();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void NoteBox_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (!this.fUpdating)
				{
					if (this.SelectedNote != null)
					{
						this.SelectedNote.Content = this.NoteBox.Text;
						this.NoteList.SelectedItems[0].Text = this.SelectedNote.Name;
						this.NoteList.SelectedItems[0].ForeColor = (this.SelectedNote.Content != "" ? SystemColors.WindowText : SystemColors.GrayText);
						this.NoteList.Sort();
						Session.Modified = true;
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void NoteCategoryBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedNote == null)
			{
				return;
			}
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			foreach (Note note in Session.Project.Notes)
			{
				if (note.Category == "")
				{
					continue;
				}
				binarySearchTree.Add(note.Category);
			}
			CategoryForm categoryForm = new CategoryForm(binarySearchTree.SortedList, this.SelectedNote.Category);
			if (categoryForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.SelectedNote.Category = categoryForm.Category;
				Session.Modified = true;
				this.update_notes();
			}
		}

		private void NoteClearLbl_Click(object sender, EventArgs e)
		{
			try
			{
				this.NoteSearchBox.Text = "";
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void NoteCopyBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedNote != null)
				{
					Clipboard.SetData(typeof(Note).ToString(), this.SelectedNote.Copy());
				}
				else if (this.NoteBox.SelectedText != "")
				{
					this.NoteBox.Copy();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void NoteCutBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedNote != null)
				{
					Clipboard.SetData(typeof(Note).ToString(), this.SelectedNote.Copy());
					Session.Project.Notes.Remove(this.SelectedNote);
					Session.Modified = true;
					this.update_notes();
					this.SelectedNote = null;
				}
				else if (this.NoteBox.SelectedText != "")
				{
					this.NoteBox.Cut();
					Session.Modified = true;
					this.update_notes();
					this.SelectedNote = null;
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
				this.fUpdating = true;
				this.NoteBox.Text = "(no note selected)";
				this.NoteBox.Enabled = false;
				this.NoteBox.ReadOnly = true;
				if (this.SelectedNote != null)
				{
					this.NoteBox.Text = this.SelectedNote.Content;
					this.NoteBox.Enabled = true;
					this.NoteBox.ReadOnly = false;
				}
				if (this.SelectedIssue != null)
				{
					this.NoteBox.Text = this.SelectedIssue.ToString();
					this.NoteBox.Enabled = true;
				}
				this.fUpdating = false;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void NotePasteBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (Clipboard.ContainsData(typeof(Note).ToString()))
				{
					Note data = Clipboard.GetData(typeof(Note).ToString()) as Note;
					if (data != null)
					{
						if (Session.Project.FindNote(data.ID) != null)
						{
							data.ID = Guid.NewGuid();
						}
						Session.Project.Notes.Add(data);
						Session.Modified = true;
						this.update_notes();
						this.SelectedNote = data;
					}
				}
				else if (Clipboard.ContainsText())
				{
					Clipboard.GetText();
					if (!this.NoteBox.Focused || this.SelectedNote == null)
					{
						Note note = new Note()
						{
							Content = Clipboard.GetText()
						};
						Session.Project.Notes.Add(note);
						Session.Modified = true;
						this.update_notes();
						this.SelectedNote = note;
					}
					else
					{
						this.NoteBox.Paste();
						Session.Modified = true;
						this.update_notes();
						this.NoteBox.Focus();
					}
				}
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
					if (MessageBox.Show("Are you sure you want to delete this note?", "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.No)
					{
						Session.Project.Notes.Remove(this.SelectedNote);
						Session.Modified = true;
						this.update_notes();
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

		private void NoteSearchBox_TextChanged(object sender, EventArgs e)
		{
			try
			{
				this.update_notes();
				if (this.NoteList.Groups[1].Items.Count == 0)
				{
					this.SelectedNote = null;
				}
				else
				{
					Note tag = this.NoteList.Groups[1].Items[0].Tag as Note;
					this.SelectedNote = tag;
				}
				this.NoteSearchBox.Focus();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void NPCBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<H3>NPC Description</H3>");
			head.Add(string.Concat("<P>", NPCBuilder.Description(), "</P>"));
			head.Add("<P class=table>");
			head.Add("<TABLE>");
			head.Add("<TR class=heading>");
			head.Add("<TD colspan=3>");
			head.Add("<B>NPC Details</B>");
			head.Add("</TD>");
			head.Add("</TR>");
			string str = NPCBuilder.Physical();
			if (str != "")
			{
				head.Add("<TR>");
				head.Add("<TD>");
				head.Add("<B>Physical Traits</B>");
				head.Add("</TD>");
				head.Add("<TD colspan=2>");
				head.Add(str);
				head.Add("</TD>");
				head.Add("</TR>");
			}
			string str1 = NPCBuilder.Personality();
			if (str1 != "")
			{
				head.Add("<TR>");
				head.Add("<TD>");
				head.Add("<B>Personality</B>");
				head.Add("</TD>");
				head.Add("<TD colspan=2>");
				head.Add(str1);
				head.Add("</TD>");
				head.Add("</TR>");
			}
			string str2 = NPCBuilder.Speech();
			if (str2 != "")
			{
				head.Add("<TR>");
				head.Add("<TD>");
				head.Add("<B>Speech</B>");
				head.Add("</TD>");
				head.Add("<TD colspan=2>");
				head.Add(str2);
				head.Add("</TD>");
				head.Add("</TR>");
			}
			head.Add("</TABLE>");
			head.Add("</P>");
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void open_file(string filename)
		{
			GC.Collect();
			Project project = Serialisation<Project>.Load(filename, SerialisationMode.Binary);
			if (project == null)
			{
				project = Session.LoadBackup(filename);
			}
			else
			{
				Session.CreateBackup(filename);
			}
			if (project == null)
			{
				string str = string.Concat("The file '", FileName.Name(filename), "' could not be opened.");
				MessageBox.Show(str, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else if (Session.CheckPassword(project))
			{
				Session.Project = project;
				Session.FileName = filename;
				Session.Modified = false;
				Session.Project.Update();
				Session.Project.SimplifyProjectLibrary();
				this.PlotView.Plot = Session.Project.Plot;
				this.update_title();
				this.UpdateView();
				if (base.Controls.Contains(this.fWelcome))
				{
					base.Controls.Clear();
					this.fWelcome = null;
					base.Controls.Add(this.Pages);
					base.Controls.Add(this.MainMenu);
					this.Pages.Focus();
					return;
				}
			}
		}

		private void PartyBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.Scheme == "party" && e.Url.LocalPath == "edit")
			{
				e.Cancel = true;
				this.ProjectPlayers_Click(sender, e);
			}
			if (e.Url.Scheme == "show")
			{
				e.Cancel = true;
				this.fPartyBreakdownSecondary = e.Url.LocalPath;
				this.update_party();
			}
		}

		private void PasteBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (Clipboard.ContainsData(typeof(PlotPoint).ToString()))
				{
					PlotPoint data = Clipboard.GetData(typeof(PlotPoint).ToString()) as PlotPoint;
					if (data != null)
					{
						if (this.PlotView.Plot.FindPoint(data.ID) != null)
						{
							data = data.Copy();
							data.Links.Clear();
							data.ID = Guid.NewGuid();
						}
						List<Guid> guids = new List<Guid>();
						foreach (Guid link in data.Links)
						{
							if (this.PlotView.Plot.FindPoint(link) != null)
							{
								continue;
							}
							guids.Add(link);
						}
						foreach (Guid guid in guids)
						{
							data.Links.Remove(guid);
						}
						this.PlotView.Plot.Points.Add(data);
						this.PlotView.RecalculateLayout();
						if (this.PlotView.SelectedPoint != null)
						{
							this.PlotView.SelectedPoint.Links.Add(data.ID);
						}
						Session.Modified = true;
						this.PlotView.SelectedPoint = data;
						this.PlotView.Invalidate();
					}
				}
				else if (Clipboard.ContainsText())
				{
					string text = Clipboard.GetText();
					PlotPoint plotPoint = new PlotPoint()
					{
						Name = string.Concat(text.Trim().Substring(0, 12), "..."),
						Details = text
					};
					this.PlotView.Plot.Points.Add(plotPoint);
					this.PlotView.RecalculateLayout();
					if (this.PlotView.SelectedPoint != null)
					{
						this.PlotView.SelectedPoint.Links.Add(plotPoint.ID);
					}
					Session.Modified = true;
					this.PlotView.SelectedPoint = plotPoint;
					this.PlotView.Invalidate();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void PlayerViewMenu_DropDownOpening(object sender, EventArgs e)
		{
			try
			{
				this.PlayerViewShow.Enabled = Session.Project != null;
				this.PlayerViewShow.Checked = Session.PlayerView != null;
				this.PlayerViewClear.Enabled = (Session.PlayerView == null ? false : Session.PlayerView.Mode != PlayerViewMode.Blank);
				this.PlayerViewOtherDisplay.Enabled = (int)Screen.AllScreens.Length > 1;
				this.PlayerViewOtherDisplay.Checked = ((int)Screen.AllScreens.Length <= 1 ? false : PlayerViewForm.UseOtherDisplay);
				this.TextSizeSmall.Checked = PlayerViewForm.DisplaySize == DisplaySize.Small;
				this.TextSizeMedium.Checked = PlayerViewForm.DisplaySize == DisplaySize.Medium;
				this.TextSizeLarge.Checked = PlayerViewForm.DisplaySize == DisplaySize.Large;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void PlotAdvancedDifficulty_Click(object sender, EventArgs e)
		{
			LevelAdjustmentForm levelAdjustmentForm = new LevelAdjustmentForm();
			if (levelAdjustmentForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				int levelAdjustment = levelAdjustmentForm.LevelAdjustment;
				foreach (PlotPoint allPlotPoint in this.PlotView.Plot.AllPlotPoints)
				{
					if (allPlotPoint.Element is Encounter)
					{
						Encounter element = allPlotPoint.Element as Encounter;
						foreach (EncounterSlot slot in element.Slots)
						{
							EncounterCard card = slot.Card;
							card.LevelAdjustment = card.LevelAdjustment + levelAdjustment;
						}
						foreach (Trap trap in element.Traps)
						{
							trap.AdjustLevel(levelAdjustment);
						}
						foreach (SkillChallenge skillChallenge in element.SkillChallenges)
						{
							SkillChallenge level = skillChallenge;
							level.Level = level.Level + levelAdjustment;
							skillChallenge.Level = Math.Max(1, skillChallenge.Level);
						}
					}
					if (allPlotPoint.Element is Trap)
					{
						(allPlotPoint.Element as Trap).AdjustLevel(levelAdjustment);
					}
					if (allPlotPoint.Element is SkillChallenge)
					{
						SkillChallenge element1 = allPlotPoint.Element as SkillChallenge;
						SkillChallenge level1 = element1;
						level1.Level = level1.Level + levelAdjustment;
						element1.Level = Math.Max(1, element1.Level);
					}
					if (!(allPlotPoint.Element is Quest))
					{
						continue;
					}
					Quest quest = allPlotPoint.Element as Quest;
					Quest quest1 = quest;
					quest1.Level = quest1.Level + levelAdjustment;
					quest.Level = Math.Max(1, quest.Level);
				}
				Session.Modified = true;
				this.PlotView.Invalidate();
			}
		}

		private void PlotAdvancedIssues_Click(object sender, EventArgs e)
		{
			(new IssuesForm(this.PlotView.Plot)).ShowDialog();
		}

		private void PlotAdvancedTreasure_Click(object sender, EventArgs e)
		{
			(new TreasureListForm(this.PlotView.Plot)).ShowDialog();
		}

		private void PlotPointExportFile_Click(object sender, EventArgs e)
		{
			try
			{
				PlotPoint selectedPoint = this.get_selected_point();
				if (selectedPoint != null)
				{
					Project project = new Project()
					{
						Name = selectedPoint.Name
					};
					project.Party.Size = Session.Project.Party.Size;
					project.Party.Level = Workspace.GetPartyLevel(selectedPoint);
					project.Plot.Points.Add(selectedPoint.Copy());
					foreach (PlotPoint allPlotPoint in project.AllPlotPoints)
					{
						allPlotPoint.EncyclopediaEntryIDs.Clear();
					}
					foreach (Guid guid in project.Plot.FindTacticalMaps())
					{
						Map map = Session.Project.FindTacticalMap(guid);
						if (map == null)
						{
							continue;
						}
						project.Maps.Add(map.Copy());
					}
					foreach (Guid guid1 in project.Plot.FindRegionalMaps())
					{
						RegionalMap regionalMap = Session.Project.FindRegionalMap(guid1);
						if (regionalMap == null)
						{
							continue;
						}
						project.RegionalMaps.Add(regionalMap.Copy());
					}
					GC.Collect();
					project.PopulateProjectLibrary();
					SaveFileDialog saveFileDialog = new SaveFileDialog()
					{
						FileName = selectedPoint.Name,
						Filter = Program.ProjectFilter
					};
					if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						Serialisation<Project>.Save(saveFileDialog.FileName, project, SerialisationMode.Binary);
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void PlotPointExportHTML_Click(object sender, EventArgs e)
		{
			try
			{
				PlotPoint selectedPoint = this.get_selected_point();
				if (selectedPoint != null)
				{
					SaveFileDialog saveFileDialog = new SaveFileDialog()
					{
						FileName = selectedPoint.Name,
						Filter = Program.HTMLFilter
					};
					if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						int partyLevel = Workspace.GetPartyLevel(selectedPoint);
						File.WriteAllText(saveFileDialog.FileName, HTML.PlotPoint(selectedPoint, this.PlotView.Plot, partyLevel, false, this.fView, DisplaySize.Small));
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void PlotPointPlayerView_Click(object sender, EventArgs e)
		{
			try
			{
				PlotPoint selectedPoint = this.get_selected_point();
				if (selectedPoint != null)
				{
					if (Session.PlayerView == null)
					{
						Session.PlayerView = new PlayerViewForm(this);
					}
					Session.PlayerView.ShowPlotPoint(selectedPoint);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void PlotView_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				if (this.PlotView.SelectedPoint == null)
				{
					this.AddBtn_Click(sender, e);
				}
				else if ((Control.ModifierKeys & Keys.Shift) != Keys.Shift)
				{
					this.EditBtn_Click(sender, e);
				}
				else
				{
					this.ExploreBtn_Click(sender, e);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void PlotView_PlotChanged(object sender, EventArgs e)
		{
			this.UpdateView();
		}

		private void PlotView_PlotLayoutChanged(object sender, EventArgs e)
		{
			Session.Modified = true;
		}

		private void PlotView_SelectionChanged(object sender, EventArgs e)
		{
			try
			{
				this.update_preview();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void PointMenu_Opening(object sender, CancelEventArgs e)
		{
			try
			{
				this.ContextAddBetween.DropDownItems.Clear();
				this.ContextDisconnect.DropDownItems.Clear();
				if (this.PlotView.SelectedPoint != null)
				{
					foreach (PlotPoint point in this.PlotView.Plot.Points)
					{
						if (!point.Links.Contains(this.PlotView.SelectedPoint.ID))
						{
							continue;
						}
						ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(string.Concat("After \"", point.Name, "\""));
						toolStripMenuItem.Click += new EventHandler(this.add_between);
						toolStripMenuItem.Tag = new Pair<PlotPoint, PlotPoint>(point, this.PlotView.SelectedPoint);
						this.ContextAddBetween.DropDownItems.Add(toolStripMenuItem);
						ToolStripMenuItem pair = new ToolStripMenuItem(point.Name);
						pair.Click += new EventHandler(this.disconnect_points);
						pair.Tag = new Pair<PlotPoint, PlotPoint>(point, this.PlotView.SelectedPoint);
						this.ContextDisconnect.DropDownItems.Add(pair);
					}
					foreach (Guid link in this.PlotView.SelectedPoint.Links)
					{
						PlotPoint plotPoint = this.PlotView.Plot.FindPoint(link);
						ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem(string.Concat("Before \"", plotPoint.Name, "\""));
						toolStripMenuItem1.Click += new EventHandler(this.add_between);
						toolStripMenuItem1.Tag = new Pair<PlotPoint, PlotPoint>(this.PlotView.SelectedPoint, plotPoint);
						this.ContextAddBetween.DropDownItems.Add(toolStripMenuItem1);
						ToolStripMenuItem pair1 = new ToolStripMenuItem(plotPoint.Name);
						pair1.Click += new EventHandler(this.disconnect_points);
						pair1.Tag = new Pair<PlotPoint, PlotPoint>(this.PlotView.SelectedPoint, plotPoint);
						this.ContextDisconnect.DropDownItems.Add(pair1);
					}
				}
				this.ContextAddBetween.Enabled = this.ContextAddBetween.DropDownItems.Count != 0;
				this.ContextDisconnect.Enabled = this.ContextDisconnect.DropDownItems.Count != 0;
				this.ContextDisconnectAll.Enabled = this.ContextDisconnect.Enabled;
				this.ContextMoveTo.DropDownItems.Clear();
				if (this.PlotView.SelectedPoint != null)
				{
					foreach (PlotPoint point1 in this.PlotView.Plot.Points)
					{
						if (!point1.Links.Contains(this.PlotView.SelectedPoint.ID))
						{
							continue;
						}
						ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(point1.Name);
						toolStripMenuItem2.Click += new EventHandler(this.move_to_subplot);
						toolStripMenuItem2.Tag = new Pair<PlotPoint, PlotPoint>(point1, this.PlotView.SelectedPoint);
						this.ContextMoveTo.DropDownItems.Add(toolStripMenuItem2);
					}
					this.ContextStateNormal.Checked = this.PlotView.SelectedPoint.State == PlotPointState.Normal;
					this.ContextStateCompleted.Checked = this.PlotView.SelectedPoint.State == PlotPointState.Completed;
					this.ContextStateSkipped.Checked = this.PlotView.SelectedPoint.State == PlotPointState.Skipped;
				}
				this.ContextMoveTo.Enabled = this.ContextMoveTo.DropDownItems.Count != 0;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void PotionBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<H3>Potions</H3>");
			for (int i = 0; i != 10; i++)
			{
				head.Add(string.Concat("<P>", Potion.Description(), "</P>"));
			}
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void Preview_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			try
			{
				if (e.Url.Scheme == "plot")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "add")
					{
						this.AddBtn_Click(sender, e);
					}
					if (e.Url.LocalPath == "encounter")
					{
						if (this.PlotView.SelectedPoint != null)
						{
							this.PlotView.SelectedPoint.Element = new Encounter();
							if (!this.edit_element(null, null))
							{
								this.PlotView.SelectedPoint.Element = null;
							}
						}
						else
						{
							this.AddEncounter_Click(sender, e);
						}
					}
					if (e.Url.LocalPath == "challenge")
					{
						if (this.PlotView.SelectedPoint != null)
						{
							SkillChallenge skillChallenge = new SkillChallenge()
							{
								Level = Session.Project.Party.Level
							};
							this.PlotView.SelectedPoint.Element = skillChallenge;
							if (!this.edit_element(null, null))
							{
								this.PlotView.SelectedPoint.Element = null;
							}
						}
						else
						{
							this.AddChallenge_Click(sender, e);
						}
					}
					if (e.Url.LocalPath == "edit")
					{
						this.EditBtn_Click(sender, e);
					}
					if (e.Url.LocalPath == "explore")
					{
						this.ExploreBtn_Click(sender, e);
					}
					if (e.Url.LocalPath == "properties")
					{
						this.ProjectProject_Click(sender, e);
					}
					if (e.Url.LocalPath == "up")
					{
						PlotPoint plotPoint = Session.Project.FindParent(this.PlotView.Plot);
						if (plotPoint != null)
						{
							Plot plot = Session.Project.FindParent(plotPoint);
							if (plot != null)
							{
								if (this.fView != MainForm.ViewType.Flowchart)
								{
									this.flowchart_view();
								}
								this.PlotView.Plot = plot;
								this.PlotView.SelectedPoint = plotPoint;
								this.UpdateView();
							}
						}
					}
					if (e.Url.LocalPath == "goals")
					{
						GoalListForm goalListForm = new GoalListForm(this.PlotView.Plot.Goals);
						if (goalListForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						{
							this.PlotView.Plot.Goals = goalListForm.Goals;
							Session.Modified = true;
							if (goalListForm.CreatePlot)
							{
								this.PlotView.Plot.Points.Clear();
								GoalBuilder.Build(this.PlotView.Plot);
								this.PlotView.RecalculateLayout();
							}
							this.UpdateView();
						}
					}
					if (e.Url.LocalPath == "5x5")
					{
						if (this.PlotView.Plot.FiveByFive.Columns.Count == 0)
						{
							this.PlotView.Plot.FiveByFive.Initialise();
						}
						FiveByFiveForm fiveByFiveForm = new FiveByFiveForm(this.PlotView.Plot.FiveByFive);
						if (fiveByFiveForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						{
							this.PlotView.Plot.FiveByFive = fiveByFiveForm.FiveByFive;
							Session.Modified = true;
							if (fiveByFiveForm.CreatePlot)
							{
								this.PlotView.Plot.Points.Clear();
								FiveByFive.Build(this.PlotView.Plot);
								this.PlotView.RecalculateLayout();
							}
							this.UpdateView();
						}
					}
					if (e.Url.LocalPath == "element")
					{
						this.edit_element(sender, e);
					}
					if (e.Url.LocalPath == "run")
					{
						this.run_encounter(sender, e);
					}
					if (e.Url.LocalPath == "maparea")
					{
						PlotPoint selectedPoint = this.get_selected_point();
						Map map = null;
						MapArea mapArea = null;
						selectedPoint.GetTacticalMapArea(ref map, ref mapArea);
						this.edit_map_area(map, mapArea, null);
					}
					if (e.Url.LocalPath == "maploc")
					{
						PlotPoint selectedPoint1 = this.get_selected_point();
						RegionalMap regionalMap = null;
						MapLocation mapLocation = null;
						selectedPoint1.GetRegionalMapArea(ref regionalMap, ref mapLocation, Session.Project);
						this.show_map_location(regionalMap, mapLocation);
					}
				}
				if (e.Url.Scheme == "entry")
				{
					e.Cancel = true;
					Guid guid = new Guid(e.Url.LocalPath);
					EncyclopediaEntry encyclopediaEntry = Session.Project.Encyclopedia.FindEntry(guid);
					if (encyclopediaEntry != null)
					{
						(new EncyclopediaEntryDetailsForm(encyclopediaEntry)).ShowDialog();
					}
				}
				if (e.Url.Scheme == "item")
				{
					e.Cancel = true;
					Guid guid1 = new Guid(e.Url.LocalPath);
					MagicItem magicItem = Session.FindMagicItem(guid1, SearchType.Global);
					if (magicItem != null)
					{
						(new MagicItemDetailsForm(magicItem)).ShowDialog();
					}
				}
				if (e.Url.Scheme == "delveview")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "select")
					{
						MapSelectForm mapSelectForm = new MapSelectForm(Session.Project.Maps, null, false);
						if (mapSelectForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						{
							this.delve_view(mapSelectForm.Map);
						}
					}
					else if (e.Url.LocalPath == "off")
					{
						this.flowchart_view();
					}
					else if (e.Url.LocalPath == "edit")
					{
						this.delve_view_edit();
					}
					else if (e.Url.LocalPath == "build")
					{
						Map map1 = new Map()
						{
							Name = "New Map"
						};
						MapBuilderForm mapBuilderForm = new MapBuilderForm(map1, false);
						if (mapBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						{
							Session.Project.Maps.Add(mapBuilderForm.Map);
							this.delve_view(mapBuilderForm.Map);
						}
					}
					else if (e.Url.LocalPath != "playerview")
					{
						Guid guid2 = new Guid(e.Url.LocalPath);
						Map map2 = Session.Project.FindTacticalMap(guid2);
						if (map2 != null)
						{
							this.delve_view(map2);
						}
					}
					else
					{
						MapView mapView = new MapView()
						{
							Map = this.fDelveView.Map,
							Plot = this.PlotView.Plot,
							Mode = MapViewMode.PlayerView,
							LineOfSight = false,
							BorderSize = 1,
							HighlightAreas = false
						};
						bool flag = false;
						int num = 2147483647;
						int num1 = -2147483648;
						int num2 = 2147483647;
						int num3 = -2147483648;
						foreach (MapArea area in this.fDelveView.Map.Areas)
						{
							PlotPoint plotPoint1 = this.PlotView.Plot.FindPointForMapArea(this.fDelveView.Map, area);
							if (plotPoint1 == null || plotPoint1.State != PlotPointState.Completed)
							{
								continue;
							}
							flag = true;
							Rectangle region = area.Region;
							num = Math.Min(num, region.Left);
							region = area.Region;
							num1 = Math.Max(num1, region.Right);
							region = area.Region;
							num2 = Math.Min(num2, region.Top);
							region = area.Region;
							num3 = Math.Max(num3, region.Bottom);
						}
						if (flag)
						{
							mapView.Viewpoint = new Rectangle(num, num2, num1 - num, num3 - num2);
						}
						if (Session.PlayerView == null)
						{
							Session.PlayerView = new PlayerViewForm(this);
						}
						Session.PlayerView.ShowTacticalMap(mapView, null);
					}
				}
				if (e.Url.Scheme == "mapview")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "select")
					{
						RegionalMapSelectForm regionalMapSelectForm = new RegionalMapSelectForm(Session.Project.RegionalMaps, null, false);
						if (regionalMapSelectForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						{
							this.map_view(regionalMapSelectForm.Map);
						}
					}
					else if (e.Url.LocalPath == "off")
					{
						this.flowchart_view();
					}
					else if (e.Url.LocalPath == "edit")
					{
						this.map_view_edit();
					}
					else if (e.Url.LocalPath == "build")
					{
						RegionalMap regionalMap1 = new RegionalMap()
						{
							Name = "New Map"
						};
						RegionalMapForm regionalMapForm = new RegionalMapForm(regionalMap1, null);
						if (regionalMapForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						{
							Session.Project.RegionalMaps.Add(regionalMapForm.Map);
							this.map_view(regionalMapForm.Map);
						}
					}
					else if (e.Url.LocalPath != "playerview")
					{
						Guid guid3 = new Guid(e.Url.LocalPath);
						RegionalMap regionalMap2 = Session.Project.FindRegionalMap(guid3);
						if (regionalMap2 != null)
						{
							this.map_view(regionalMap2);
						}
					}
					else
					{
						RegionalMapPanel regionalMapPanel = new RegionalMapPanel()
						{
							Map = this.fMapView.Map,
							Plot = this.PlotView.Plot,
							Mode = MapViewMode.PlayerView
						};
						if (Session.PlayerView == null)
						{
							Session.PlayerView = new PlayerViewForm(this);
						}
						Session.PlayerView.ShowRegionalMap(regionalMapPanel);
					}
				}
				if (e.Url.Scheme == "maparea")
				{
					e.Cancel = true;
					MapView mapView1 = null;
					foreach (Control control in this.PreviewSplitter.Panel1.Controls)
					{
						if (!(control is MapView))
						{
							continue;
						}
						mapView1 = control as MapView;
						break;
					}
					if (mapView1 == null || mapView1.SelectedArea == null)
					{
						return;
					}
					else
					{
						if (e.Url.LocalPath == "edit")
						{
							this.edit_map_area(mapView1.Map, mapView1.SelectedArea, mapView1);
						}
						if (e.Url.LocalPath == "create")
						{
							PlotPoint plotPoint2 = new PlotPoint(mapView1.SelectedArea.Name)
							{
								Element = new MapElement(mapView1.Map.ID, mapView1.SelectedArea.ID)
							};
							PlotPointForm plotPointForm = new PlotPointForm(plotPoint2, this.PlotView.Plot, false);
							if (plotPointForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
							{
								this.PlotView.Plot.Points.Add(plotPointForm.PlotPoint);
								this.UpdateView();
								Session.Modified = true;
							}
						}
						if (e.Url.LocalPath == "encounter")
						{
							Encounter encounter = new Encounter()
							{
								MapID = mapView1.Map.ID,
								MapAreaID = mapView1.SelectedArea.ID
							};
							encounter.SetStandardEncounterNotes();
							PlotPoint plotPoint3 = new PlotPoint(mapView1.SelectedArea.Name)
							{
								Element = encounter
							};
							PlotPointForm plotPointForm1 = new PlotPointForm(plotPoint3, this.PlotView.Plot, true);
							if (plotPointForm1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
							{
								this.PlotView.Plot.Points.Add(plotPointForm1.PlotPoint);
								this.UpdateView();
								Session.Modified = true;
							}
						}
						if (e.Url.LocalPath == "trap")
						{
							TrapElement trapElement = new TrapElement();
							trapElement.Trap.Name = mapView1.SelectedArea.Name;
							trapElement.MapID = mapView1.Map.ID;
							trapElement.MapAreaID = mapView1.SelectedArea.ID;
							PlotPoint plotPoint4 = new PlotPoint(mapView1.SelectedArea.Name)
							{
								Element = trapElement
							};
							PlotPointForm plotPointForm2 = new PlotPointForm(plotPoint4, this.PlotView.Plot, true);
							if (plotPointForm2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
							{
								this.PlotView.Plot.Points.Add(plotPointForm2.PlotPoint);
								this.UpdateView();
								Session.Modified = true;
							}
						}
						if (e.Url.LocalPath == "challenge")
						{
							SkillChallenge skillChallenge1 = new SkillChallenge()
							{
								Name = mapView1.SelectedArea.Name,
								MapID = mapView1.Map.ID,
								MapAreaID = mapView1.SelectedArea.ID,
								Level = Session.Project.Party.Level
							};
							PlotPoint plotPoint5 = new PlotPoint(mapView1.SelectedArea.Name)
							{
								Element = skillChallenge1
							};
							PlotPointForm plotPointForm3 = new PlotPointForm(plotPoint5, this.PlotView.Plot, true);
							if (plotPointForm3.ShowDialog() == System.Windows.Forms.DialogResult.OK)
							{
								this.PlotView.Plot.Points.Add(plotPointForm3.PlotPoint);
								this.UpdateView();
								Session.Modified = true;
							}
						}
					}
				}
				if (e.Url.Scheme == "maploc")
				{
					e.Cancel = true;
					RegionalMapPanel regionalMapPanel1 = null;
					foreach (Control control1 in this.PreviewSplitter.Panel1.Controls)
					{
						if (!(control1 is RegionalMapPanel))
						{
							continue;
						}
						regionalMapPanel1 = control1 as RegionalMapPanel;
						break;
					}
					if (regionalMapPanel1 == null || regionalMapPanel1.SelectedLocation == null)
					{
						return;
					}
					else
					{
						if (e.Url.LocalPath == "edit")
						{
							this.edit_map_location(regionalMapPanel1.Map, regionalMapPanel1.SelectedLocation, regionalMapPanel1);
						}
						if (e.Url.LocalPath == "create")
						{
							PlotPoint plotPoint6 = new PlotPoint(regionalMapPanel1.SelectedLocation.Name)
							{
								RegionalMapID = regionalMapPanel1.Map.ID,
								MapLocationID = regionalMapPanel1.SelectedLocation.ID
							};
							PlotPointForm plotPointForm4 = new PlotPointForm(plotPoint6, this.PlotView.Plot, false);
							if (plotPointForm4.ShowDialog() == System.Windows.Forms.DialogResult.OK)
							{
								this.PlotView.Plot.Points.Add(plotPointForm4.PlotPoint);
								this.UpdateView();
								Session.Modified = true;
							}
						}
						if (e.Url.LocalPath == "encounter")
						{
							Encounter encounter1 = new Encounter();
							encounter1.SetStandardEncounterNotes();
							PlotPoint plotPoint7 = new PlotPoint(regionalMapPanel1.SelectedLocation.Name)
							{
								RegionalMapID = regionalMapPanel1.Map.ID,
								MapLocationID = regionalMapPanel1.SelectedLocation.ID,
								Element = encounter1
							};
							PlotPointForm plotPointForm5 = new PlotPointForm(plotPoint7, this.PlotView.Plot, true);
							if (plotPointForm5.ShowDialog() == System.Windows.Forms.DialogResult.OK)
							{
								this.PlotView.Plot.Points.Add(plotPointForm5.PlotPoint);
								this.UpdateView();
								Session.Modified = true;
							}
						}
						if (e.Url.LocalPath == "trap")
						{
							TrapElement name = new TrapElement();
							name.Trap.Name = regionalMapPanel1.SelectedLocation.Name;
							PlotPoint plotPoint8 = new PlotPoint(regionalMapPanel1.SelectedLocation.Name)
							{
								RegionalMapID = regionalMapPanel1.Map.ID,
								MapLocationID = regionalMapPanel1.SelectedLocation.ID,
								Element = name
							};
							PlotPointForm plotPointForm6 = new PlotPointForm(plotPoint8, this.PlotView.Plot, true);
							if (plotPointForm6.ShowDialog() == System.Windows.Forms.DialogResult.OK)
							{
								this.PlotView.Plot.Points.Add(plotPointForm6.PlotPoint);
								this.UpdateView();
								Session.Modified = true;
							}
						}
						if (e.Url.LocalPath == "challenge")
						{
							SkillChallenge skillChallenge2 = new SkillChallenge()
							{
								Name = regionalMapPanel1.SelectedLocation.Name,
								Level = Session.Project.Party.Level
							};
							PlotPoint plotPoint9 = new PlotPoint(regionalMapPanel1.SelectedLocation.Name)
							{
								RegionalMapID = regionalMapPanel1.Map.ID,
								MapLocationID = regionalMapPanel1.SelectedLocation.ID,
								Element = skillChallenge2
							};
							PlotPointForm plotPointForm7 = new PlotPointForm(plotPoint9, this.PlotView.Plot, true);
							if (plotPointForm7.ShowDialog() == System.Windows.Forms.DialogResult.OK)
							{
								this.PlotView.Plot.Points.Add(plotPointForm7.PlotPoint);
								this.UpdateView();
								Session.Modified = true;
							}
						}
					}
				}
				if (e.Url.Scheme == "sc")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "reset")
					{
						SkillChallenge element = this.get_selected_point().Element as SkillChallenge;
						if (element != null)
						{
							foreach (SkillChallengeData skill in element.Skills)
							{
								skill.Results.Successes = 0;
								skill.Results.Fails = 0;
								Session.Modified = true;
								this.UpdateView();
							}
						}
					}
				}
				if (e.Url.Scheme == "success")
				{
					e.Cancel = true;
					SkillChallenge element1 = this.get_selected_point().Element as SkillChallenge;
					if (element1 != null)
					{
						SkillChallengeResult results = element1.FindSkill(e.Url.LocalPath).Results;
						results.Successes = results.Successes + 1;
						Session.Modified = true;
						this.UpdateView();
					}
				}
				if (e.Url.Scheme == "failure")
				{
					e.Cancel = true;
					SkillChallenge element2 = this.get_selected_point().Element as SkillChallenge;
					if (element2 != null)
					{
						SkillChallengeResult fails = element2.FindSkill(e.Url.LocalPath).Results;
						fails.Fails = fails.Fails + 1;
						Session.Modified = true;
						this.UpdateView();
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void PrimordialTextBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			int num = Session.Dice(1, 6);
			for (int i = 0; i != num; i++)
			{
				head.Add(string.Concat("<P>", ExoticName.Sentence(), "</P>"));
			}
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void print_page(object sender, PrintPageEventArgs e)
		{
			try
			{
				Bitmap bitmap = Screenshot.Plot(this.PlotView.Plot, e.MarginBounds.Size);
				e.Graphics.DrawImage(bitmap, e.MarginBounds);
				e.HasMorePages = false;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys key)
		{
			if (Session.Project != null)
			{
				if (this.Pages.SelectedTab == this.WorkspacePage)
				{
					if (key == (Keys.LButton | Keys.A | Keys.Control))
					{
						this.AddBtn_Click(null, null);
						return true;
					}
					if (key == (Keys.Back | Keys.ShiftKey | Keys.FinalMode | Keys.H | Keys.P | Keys.X | Keys.Control))
					{
						this.CutBtn_Click(null, null);
						return true;
					}
					if (key == (Keys.LButton | Keys.RButton | Keys.Cancel | Keys.A | Keys.B | Keys.C | Keys.Control))
					{
						this.CopyBtn_Click(null, null);
						return true;
					}
					if (key == (Keys.RButton | Keys.MButton | Keys.XButton2 | Keys.ShiftKey | Keys.Menu | Keys.Capital | Keys.CapsLock | Keys.B | Keys.D | Keys.F | Keys.P | Keys.R | Keys.T | Keys.V | Keys.Control))
					{
						this.PasteBtn_Click(null, null);
						return true;
					}
					if (this.PlotView.Navigate(key))
					{
						return true;
					}
				}
				if (this.Pages.SelectedTab == this.BackgroundPage && key == (Keys.LButton | Keys.A | Keys.Control))
				{
					this.BackgroundAddBtn_Click(null, null);
					return true;
				}
				if (this.Pages.SelectedTab == this.EncyclopediaPage)
				{
					if (key == (Keys.LButton | Keys.A | Keys.Control))
					{
						this.EncAddEntry_Click(null, null);
						return true;
					}
					if (key == (Keys.Back | Keys.ShiftKey | Keys.FinalMode | Keys.H | Keys.P | Keys.X | Keys.Control))
					{
						this.EncCutBtn_Click(null, null);
						return true;
					}
					if (key == (Keys.LButton | Keys.RButton | Keys.Cancel | Keys.A | Keys.B | Keys.C | Keys.Control))
					{
						this.EncCopyBtn_Click(null, null);
						return true;
					}
					if (key == (Keys.RButton | Keys.MButton | Keys.XButton2 | Keys.ShiftKey | Keys.Menu | Keys.Capital | Keys.CapsLock | Keys.B | Keys.D | Keys.F | Keys.P | Keys.R | Keys.T | Keys.V | Keys.Control))
					{
						this.EncPasteBtn_Click(null, null);
						return true;
					}
				}
				if (this.Pages.SelectedTab == this.AttachmentsPage && key == (Keys.LButton | Keys.A | Keys.Control))
				{
					this.AttachmentImportBtn_Click(null, null);
					return true;
				}
				if (this.Pages.SelectedTab == this.JotterPage)
				{
					if (key == (Keys.Back | Keys.ShiftKey | Keys.FinalMode | Keys.H | Keys.P | Keys.X | Keys.Control))
					{
						this.NoteCutBtn_Click(null, null);
						return true;
					}
					if (key == (Keys.LButton | Keys.RButton | Keys.Cancel | Keys.A | Keys.B | Keys.C | Keys.Control))
					{
						this.NoteCopyBtn_Click(null, null);
						return true;
					}
					if (key == (Keys.RButton | Keys.MButton | Keys.XButton2 | Keys.ShiftKey | Keys.Menu | Keys.Capital | Keys.CapsLock | Keys.B | Keys.D | Keys.F | Keys.P | Keys.R | Keys.T | Keys.V | Keys.Control))
					{
						this.NotePasteBtn_Click(null, null);
						return true;
					}
				}
			}
			return base.ProcessCmdKey(ref msg, key);
		}

		private void ProjectCalendars_Click(object sender, EventArgs e)
		{
			try
			{
				(new CalendarListForm()).ShowDialog();
				this.UpdateView();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ProjectCampaignSettings_Click(object sender, EventArgs e)
		{
			try
			{
				if (Session.Project != null)
				{
					if ((new CampaignSettingsForm(Session.Project.CampaignSettings)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						Session.Modified = true;
						this.UpdateView();
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ProjectCustomCreatures_Click(object sender, EventArgs e)
		{
			try
			{
				(new CustomCreatureListForm()).ShowDialog();
				this.UpdateView();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ProjectDecks_Click(object sender, EventArgs e)
		{
			try
			{
				(new DeckListForm()).ShowDialog();
				this.UpdateView();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ProjectEncounters_Click(object sender, EventArgs e)
		{
			try
			{
				(new PausedCombatListForm()).ShowDialog();
				foreach (Form openForm in Application.OpenForms)
				{
					if (!(openForm is CombatForm))
					{
						continue;
					}
					openForm.Activate();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ProjectMenu_DropDownOpening(object sender, EventArgs e)
		{
			try
			{
				this.ProjectProject.Enabled = Session.Project != null;
				this.ProjectOverview.Enabled = Session.Project != null;
				this.ProjectCampaignSettings.Enabled = Session.Project != null;
				this.ProjectPassword.Enabled = Session.Project != null;
				this.ProjectTacticalMaps.Enabled = Session.Project != null;
				this.ProjectRegionalMaps.Enabled = Session.Project != null;
				this.ProjectPlayers.Enabled = Session.Project != null;
				this.ProjectParcels.Enabled = Session.Project != null;
				this.ProjectDecks.Enabled = Session.Project != null;
				this.ProjectCustomCreatures.Enabled = Session.Project != null;
				this.ProjectCalendars.Enabled = Session.Project != null;
				this.ProjectEncounters.Enabled = (Session.Project == null ? false : Session.Project.SavedCombats.Count != 0);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ProjectOverview_Click(object sender, EventArgs e)
		{
			try
			{
				(new OverviewForm()).ShowDialog();
				this.UpdateView();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ProjectParcels_Click(object sender, EventArgs e)
		{
			try
			{
				(new ParcelListForm()).ShowDialog();
				this.UpdateView();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ProjectPassword_Click(object sender, EventArgs e)
		{
			if (Session.CheckPassword(Session.Project))
			{
				PasswordSetForm passwordSetForm = new PasswordSetForm();
				if (passwordSetForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.Password = passwordSetForm.Password;
					Session.Project.PasswordHint = passwordSetForm.PasswordHint;
					Session.Modified = true;
				}
			}
		}

		private void ProjectPlayers_Click(object sender, EventArgs e)
		{
			try
			{
				(new HeroListForm()).ShowDialog();
				this.UpdateView();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ProjectProject_Click(object sender, EventArgs e)
		{
			try
			{
				if (Session.Project != null)
				{
					if ((new ProjectForm(Session.Project)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						Session.Modified = true;
						this.update_title();
						this.UpdateView();
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ProjectRegionalMaps_Click(object sender, EventArgs e)
		{
			try
			{
				(new RegionalMapListForm()).ShowDialog();
				this.UpdateView();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ProjectTacticalMaps_Click(object sender, EventArgs e)
		{
			try
			{
				(new MapListForm()).ShowDialog();
				this.UpdateView();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ReferencePages_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.ReferencePages.SelectedTab == this.CompendiumPage && this.CompendiumBrowser.Url == null)
			{
				List<string> strs = new List<string>();
				strs.AddRange(HTML.GetHead(null, null, DisplaySize.Small));
				strs.Add("<BODY>");
				strs.Add("<P class=instruction>");
				strs.Add("No PC details have been entered; click <A href=\"party:edit\">here</A> to do this now.");
				strs.Add("</P>");
				strs.Add("<P class=instruction>");
				strs.Add("When PCs have been entered, you will see a useful breakdown of their defences, passive skills and known languages here.");
				strs.Add("</P>");
				strs.Add("</BODY>");
				strs.Add("</HTML>");
				this.CompendiumBrowser.DocumentText = HTML.Concatenate(strs);
				this.CompendiumBrowser.Navigate("http://www.wizards.com/dndinsider/compendium/database.aspx");
			}
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlotView.SelectedPoint != null)
				{
					System.Windows.Forms.DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this plot point?", "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (dialogResult != System.Windows.Forms.DialogResult.No)
					{
						if (this.PlotView.SelectedPoint.Subplot.Points.Count != 0)
						{
							string str = string.Concat("This plot point has a subplot.", Environment.NewLine);
							str = string.Concat(str, "Do you want to keep the subplot points?");
							dialogResult = MessageBox.Show(str, "Masterplan", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
							if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
							{
								return;
							}
							else if (dialogResult == System.Windows.Forms.DialogResult.Yes)
							{
								foreach (PlotPoint point in this.PlotView.SelectedPoint.Subplot.Points)
								{
									this.PlotView.Plot.Points.Add(point);
								}
							}
						}
						this.PlotView.Plot.RemovePoint(this.PlotView.SelectedPoint);
						this.PlotView.RecalculateLayout();
						this.PlotView.SelectedPoint = null;
						Session.Modified = true;
						this.UpdateView();
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

		private void RoomBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			head.Add(string.Concat("<H3>", RoomBuilder.Name(), "</H3>"));
			head.Add(string.Concat("<P>", RoomBuilder.Details(), "</P>"));
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void RuleEncyclopediaBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedRule == null)
			{
				return;
			}
			EncyclopediaEntry encyclopediaEntry = Session.Project.Encyclopedia.FindEntryForAttachment(this.SelectedRule.ID);
			if (encyclopediaEntry == null)
			{
				if (MessageBox.Show(string.Concat(string.Concat("There is no encyclopedia entry associated with this item.", Environment.NewLine), "Would you like to create one now?"), "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
				{
					return;
				}
				encyclopediaEntry = new EncyclopediaEntry()
				{
					Name = this.SelectedRule.Name,
					AttachmentID = this.SelectedRule.ID,
					Category = ""
				};
				Session.Project.Encyclopedia.Entries.Add(encyclopediaEntry);
				Session.Modified = true;
			}
			int entry = Session.Project.Encyclopedia.Entries.IndexOf(encyclopediaEntry);
			EncyclopediaEntryForm encyclopediaEntryForm = new EncyclopediaEntryForm(encyclopediaEntry);
			if (encyclopediaEntryForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Project.Encyclopedia.Entries[entry] = encyclopediaEntryForm.Entry;
				Session.Modified = true;
				this.UpdateView();
				this.Pages.SelectedTab = this.EncyclopediaPage;
				this.SelectedEncyclopediaItem = encyclopediaEntryForm.Entry;
			}
		}

		private void RulesEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedRule == null)
			{
				return;
			}
			int race = Session.Project.PlayerOptions.IndexOf(this.SelectedRule);
			if (this.SelectedRule is Race)
			{
				OptionRaceForm optionRaceForm = new OptionRaceForm(this.SelectedRule as Race);
				if (optionRaceForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.PlayerOptions[race] = optionRaceForm.Race;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
			if (this.SelectedRule is Class)
			{
				OptionClassForm optionClassForm = new OptionClassForm(this.SelectedRule as Class);
				if (optionClassForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.PlayerOptions[race] = optionClassForm.Class;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
			if (this.SelectedRule is Theme)
			{
				OptionThemeForm optionThemeForm = new OptionThemeForm(this.SelectedRule as Theme);
				if (optionThemeForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.PlayerOptions[race] = optionThemeForm.Theme;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
			if (this.SelectedRule is ParagonPath)
			{
				OptionParagonPathForm optionParagonPathForm = new OptionParagonPathForm(this.SelectedRule as ParagonPath);
				if (optionParagonPathForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.PlayerOptions[race] = optionParagonPathForm.ParagonPath;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
			if (this.SelectedRule is EpicDestiny)
			{
				OptionEpicDestinyForm optionEpicDestinyForm = new OptionEpicDestinyForm(this.SelectedRule as EpicDestiny);
				if (optionEpicDestinyForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.PlayerOptions[race] = optionEpicDestinyForm.EpicDestiny;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
			if (this.SelectedRule is PlayerBackground)
			{
				OptionBackgroundForm optionBackgroundForm = new OptionBackgroundForm(this.SelectedRule as PlayerBackground);
				if (optionBackgroundForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.PlayerOptions[race] = optionBackgroundForm.Background;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
			if (this.SelectedRule is Feat)
			{
				OptionFeatForm optionFeatForm = new OptionFeatForm(this.SelectedRule as Feat);
				if (optionFeatForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.PlayerOptions[race] = optionFeatForm.Feat;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
			if (this.SelectedRule is Weapon)
			{
				OptionWeaponForm optionWeaponForm = new OptionWeaponForm(this.SelectedRule as Weapon);
				if (optionWeaponForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.PlayerOptions[race] = optionWeaponForm.Weapon;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
			if (this.SelectedRule is Ritual)
			{
				OptionRitualForm optionRitualForm = new OptionRitualForm(this.SelectedRule as Ritual);
				if (optionRitualForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.PlayerOptions[race] = optionRitualForm.Ritual;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
			if (this.SelectedRule is CreatureLore)
			{
				OptionCreatureLoreForm optionCreatureLoreForm = new OptionCreatureLoreForm(this.SelectedRule as CreatureLore);
				if (optionCreatureLoreForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.PlayerOptions[race] = optionCreatureLoreForm.CreatureLore;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
			if (this.SelectedRule is Disease)
			{
				OptionDiseaseForm optionDiseaseForm = new OptionDiseaseForm(this.SelectedRule as Disease);
				if (optionDiseaseForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.PlayerOptions[race] = optionDiseaseForm.Disease;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
			if (this.SelectedRule is Poison)
			{
				OptionPoisonForm optionPoisonForm = new OptionPoisonForm(this.SelectedRule as Poison);
				if (optionPoisonForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.PlayerOptions[race] = optionPoisonForm.Poison;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
		}

		private void RulesList_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.update_selected_rule();
		}

		private void RulesPlayerViewBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedRule != null)
			{
				if (Session.PlayerView == null)
				{
					Session.PlayerView = new PlayerViewForm(this);
				}
				Session.PlayerView.ShowPlayerOption(this.SelectedRule);
			}
		}

		private void RulesRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedRule != null)
			{
				Session.Project.PlayerOptions.Remove(this.SelectedRule);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void RulesShareExport_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog()
			{
				Filter = Program.RulesFilter,
				FileName = Session.Project.Name
			};
			if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Serialisation<List<IPlayerOption>>.Save(saveFileDialog.FileName, Session.Project.PlayerOptions, SerialisationMode.Binary);
			}
		}

		private void RulesShareImport_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Filter = Program.RulesFilter
			};
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				List<IPlayerOption> playerOptions = Serialisation<List<IPlayerOption>>.Load(openFileDialog.FileName, SerialisationMode.Binary);
				Session.Project.PlayerOptions.AddRange(playerOptions);
				this.UpdateView();
			}
		}

		private void RulesSharePublish_Click(object sender, EventArgs e)
		{
			HandoutForm handoutForm = new HandoutForm();
			handoutForm.AddRulesEntries();
			handoutForm.ShowDialog();
		}

		private void run_encounter(object sender, EventArgs e)
		{
			try
			{
				PlotPoint selectedPoint = this.get_selected_point();
				if (selectedPoint != null)
				{
					Encounter element = selectedPoint.Element as Encounter;
					if (element != null)
					{
						CombatState combatState = new CombatState()
						{
							Encounter = element,
							PartyLevel = Workspace.GetPartyLevel(selectedPoint)
						};
						(new CombatForm(combatState)).Show();
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void SearchBox_TextChanged(object sender, EventArgs e)
		{
			try
			{
				this.PlotView.Filter = this.PlotSearchBox.Text;
				this.PlotSearchBox.Focus();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void SearchBtn_Click(object sender, EventArgs e)
		{
			try
			{
				this.WorkspaceSearchBar.Visible = !this.WorkspaceSearchBar.Visible;
				if (!this.WorkspaceSearchBar.Visible)
				{
					this.PlotSearchBox.Text = "";
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void select_maparea(object sender, MapAreaEventArgs e)
		{
			this.update_preview();
		}

		private void select_maplocation(object sender, EventArgs e)
		{
			this.update_preview();
		}

		private void set_rmap_preview(RegionalMap map, MapLocation loc)
		{
			try
			{
				RegionalMapPanel regionalMapPanel = new RegionalMapPanel()
				{
					Mode = MapViewMode.Plain,
					Map = map,
					HighlightedLocation = loc
				};
				regionalMapPanel.DoubleClick += new EventHandler(this.show_rmap);
				regionalMapPanel.BorderStyle = BorderStyle.Fixed3D;
				regionalMapPanel.Dock = DockStyle.Fill;
				this.PreviewInfoSplitter.Panel2.Controls.Add(regionalMapPanel);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void set_selected_point(PlotPoint point)
		{
			switch (this.fView)
			{
				case MainForm.ViewType.Flowchart:
				{
					this.PlotView.SelectedPoint = point;
					return;
				}
				case MainForm.ViewType.Delve:
				{
					MapView mapView = null;
					foreach (Control control in this.PreviewSplitter.Panel1.Controls)
					{
						if (!(control is MapView))
						{
							continue;
						}
						mapView = control as MapView;
					}
					if (mapView == null)
					{
						break;
					}
					mapView.SelectedArea = null;
					if (point.Element == null)
					{
						break;
					}
					if (point.Element is Encounter)
					{
						Encounter element = point.Element as Encounter;
						if (element.MapID == mapView.Map.ID)
						{
							mapView.SelectedArea = mapView.Map.FindArea(element.MapAreaID);
						}
					}
					if (point.Element is TrapElement)
					{
						TrapElement trapElement = point.Element as TrapElement;
						if (trapElement.MapID == mapView.Map.ID)
						{
							mapView.SelectedArea = mapView.Map.FindArea(trapElement.MapAreaID);
						}
					}
					if (point.Element is SkillChallenge)
					{
						SkillChallenge skillChallenge = point.Element as SkillChallenge;
						if (skillChallenge.MapID == mapView.Map.ID)
						{
							mapView.SelectedArea = mapView.Map.FindArea(skillChallenge.MapAreaID);
						}
					}
					if (!(point.Element is MapElement))
					{
						break;
					}
					MapElement mapElement = point.Element as MapElement;
					if (mapElement.MapID != mapView.Map.ID)
					{
						break;
					}
					mapView.SelectedArea = mapView.Map.FindArea(mapElement.MapAreaID);
					return;
				}
				case MainForm.ViewType.Map:
				{
					RegionalMapPanel regionalMapPanel = null;
					foreach (Control control1 in this.PreviewSplitter.Panel1.Controls)
					{
						if (!(control1 is RegionalMapPanel))
						{
							continue;
						}
						regionalMapPanel = control1 as RegionalMapPanel;
					}
					if (regionalMapPanel == null)
					{
						break;
					}
					regionalMapPanel.SelectedLocation = null;
					if (point.RegionalMapID == regionalMapPanel.Map.ID)
					{
						break;
					}
					regionalMapPanel.SelectedLocation = regionalMapPanel.Map.FindLocation(point.MapLocationID);
					break;
				}
				default:
				{
					return;
				}
			}
		}

		private void set_subplot_preview(Plot p)
		{
			try
			{
				Masterplan.Controls.PlotView plotView = new Masterplan.Controls.PlotView()
				{
					Plot = p,
					Mode = PlotViewMode.Plain,
					LinkStyle = Session.Preferences.LinkStyle
				};
				plotView.DoubleClick += new EventHandler(this.ExploreBtn_Click);
				plotView.BorderStyle = BorderStyle.Fixed3D;
				plotView.Dock = DockStyle.Fill;
				this.PreviewInfoSplitter.Panel2.Controls.Add(plotView);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void set_tmap_preview(Guid map_id, Guid area_id, Encounter enc)
		{
			try
			{
				Map map = Session.Project.FindTacticalMap(map_id);
				if (map != null)
				{
					MapView mapView = new MapView()
					{
						Mode = MapViewMode.Plain,
						FrameType = MapDisplayType.Dimmed,
						LineOfSight = false,
						ShowGrid = MapGridMode.None,
						BorderSize = 1,
						Map = map
					};
					if (area_id != Guid.Empty)
					{
						MapArea mapArea = map.FindArea(area_id);
						if (mapArea != null)
						{
							mapView.Viewpoint = mapArea.Region;
						}
					}
					if (enc == null)
					{
						mapView.DoubleClick += new EventHandler(this.show_tmap);
					}
					else
					{
						mapView.Encounter = enc;
						mapView.DoubleClick += new EventHandler(this.run_encounter);
					}
					mapView.BorderStyle = BorderStyle.Fixed3D;
					mapView.Dock = DockStyle.Fill;
					this.PreviewInfoSplitter.Panel2.Controls.Add(mapView);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void show_map_location(RegionalMap map, MapLocation loc)
		{
			(new RegionalMapForm(map, loc)).ShowDialog();
		}

		private void show_rmap(object sender, EventArgs e)
		{
			RegionalMapPanel regionalMapPanel = sender as RegionalMapPanel;
			if (Session.PlayerView == null)
			{
				Session.PlayerView = new PlayerViewForm(this);
			}
			Session.PlayerView.ShowRegionalMap(regionalMapPanel);
		}

		private void show_tmap(object sender, EventArgs e)
		{
			MapView mapView = sender as MapView;
			if (Session.PlayerView == null)
			{
				Session.PlayerView = new PlayerViewForm(this);
			}
			Session.PlayerView.ShowTacticalMap(mapView, null);
		}

		private void TextSizeLarge_Click(object sender, EventArgs e)
		{
			try
			{
				PlayerViewForm.DisplaySize = DisplaySize.Large;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void TextSizeMedium_Click(object sender, EventArgs e)
		{
			try
			{
				PlayerViewForm.DisplaySize = DisplaySize.Medium;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void TextSizeSmall_Click(object sender, EventArgs e)
		{
			try
			{
				PlayerViewForm.DisplaySize = DisplaySize.Small;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ToolsExportHandout_Click(object sender, EventArgs e)
		{
			try
			{
				(new HandoutForm()).ShowDialog();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ToolsExportLoot_Click(object sender, EventArgs e)
		{
			try
			{
				(new TreasureListForm(Session.Project.Plot)).ShowDialog();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ToolsExportProject_Click(object sender, EventArgs e)
		{
			try
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog()
				{
					Filter = Program.HTMLFilter,
					FileName = FileName.TrimInvalidCharacters(Session.Project.Name)
				};
				if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					if (!(new HTML()).ExportProject(saveFileDialog.FileName))
					{
						MessageBox.Show("The file could not be saved; check the filename and drive permissions and try again.", "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else
					{
						Process.Start(saveFileDialog.FileName);
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ToolsImportProject_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog()
				{
					Filter = Program.ProjectFilter
				};
				if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					GC.Collect();
					Project project = Serialisation<Project>.Load(openFileDialog.FileName, SerialisationMode.Binary);
					if (project != null)
					{
						Session.Project.PopulateProjectLibrary();
						Session.Project.Import(project);
						Session.Project.SimplifyProjectLibrary();
						Session.Modified = true;
						this.PlotView.RecalculateLayout();
						this.UpdateView();
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ToolsIssues_Click(object sender, EventArgs e)
		{
			try
			{
				(new IssuesForm(Session.Project.Plot)).ShowDialog();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ToolsLibraries_Click(object sender, EventArgs e)
		{
			try
			{
				(new LibraryListForm()).ShowDialog();
				this.UpdateView();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ToolsMenu_DropDownOpening(object sender, EventArgs e)
		{
			try
			{
				this.ToolsImportProject.Enabled = Session.Project != null;
				this.ToolsExportProject.Enabled = Session.Project != null;
				this.ToolsExportHandout.Enabled = Session.Project != null;
				this.ToolsExportLoot.Enabled = Session.Project != null;
				this.ToolsIssues.Enabled = Session.Project != null;
				this.ToolsTileChecklist.Enabled = Session.Project != null;
				this.ToolsMiniChecklist.Enabled = Session.Project != null;
				this.ToolsAddIns.DropDownItems.Clear();
				foreach (IAddIn addIn in Session.AddIns)
				{
					ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(addIn.Name)
					{
						ToolTipText = TextHelper.Wrap(addIn.Description),
						Tag = addIn
					};
					this.ToolsAddIns.DropDownItems.Add(toolStripMenuItem);
					foreach (ICommand command in addIn.Commands)
					{
						ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem(command.Name)
						{
							ToolTipText = TextHelper.Wrap(command.Description),
							Enabled = command.Available,
							Checked = command.Active
						};
						toolStripMenuItem1.Click += new EventHandler(this.add_in_command_clicked);
						toolStripMenuItem1.Tag = command;
						toolStripMenuItem.DropDownItems.Add(toolStripMenuItem1);
					}
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
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ToolsMiniChecklist_Click(object sender, EventArgs e)
		{
			try
			{
				if (Session.Project != null)
				{
					(new MiniChecklistForm()).ShowDialog();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ToolsPlayerView_Click(object sender, EventArgs e)
		{
			try
			{
				if (Session.PlayerView != null)
				{
					Session.PlayerView.Close();
				}
				else
				{
					Session.PlayerView = new PlayerViewForm(this);
					Session.PlayerView.ShowDefault();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ToolsPlayerViewClear_Click(object sender, EventArgs e)
		{
			try
			{
				if (Session.PlayerView != null)
				{
					Session.PlayerView.ShowDefault();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ToolsPlayerViewSecondary_Click(object sender, EventArgs e)
		{
			try
			{
				PlayerViewForm.UseOtherDisplay = !PlayerViewForm.UseOtherDisplay;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ToolsPowerStats_Click(object sender, EventArgs e)
		{
			try
			{
				List<ICreature> creatures = new List<ICreature>();
				foreach (ICreature creature in Session.Creatures)
				{
					creatures.Add(creature);
				}
				if (Session.Project != null)
				{
					foreach (ICreature customCreature in Session.Project.CustomCreatures)
					{
						creatures.Add(customCreature);
					}
					foreach (ICreature nPC in Session.Project.NPCs)
					{
						creatures.Add(nPC);
					}
				}
				List<CreaturePower> creaturePowers = new List<CreaturePower>();
				foreach (ICreature creature1 in creatures)
				{
					if (creature1 == null)
					{
						continue;
					}
					creaturePowers.AddRange(creature1.CreaturePowers);
				}
				(new PowerStatisticsForm(creaturePowers, creatures.Count)).ShowDialog();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ToolsTileChecklist_Click(object sender, EventArgs e)
		{
			try
			{
				if (Session.Project != null)
				{
					(new TileChecklistForm()).ShowDialog();
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void TreasureBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<H3>Art Objects</H3>");
			head.Add("<P class=instruction>Click on any item to make it available as a treasure parcel.</P>");
			head.Add("<P class=table>");
			head.Add("<TABLE>");
			head.Add("<TR class=heading>");
			head.Add("<TD><B>Items</B></TD>");
			head.Add("</TR>");
			for (int i = 0; i != 10; i++)
			{
				string str = Treasure.ArtObject();
				head.Add("<TR>");
				head.Add("<TD>");
				string[] strArrays = new string[] { "<P><A href=parcel:", str.Replace(" ", "%20"), ">", str, "</A></P>" };
				head.Add(string.Concat(strArrays));
				head.Add("</TD>");
				head.Add("</TR>");
			}
			head.Add("</TABLE>");
			head.Add("</P>");
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void update_attachments()
		{
			try
			{
				if (Session.Project == null)
				{
					ListViewItem grayText = this.AttachmentList.Items.Add("(no project)");
					grayText.ForeColor = SystemColors.GrayText;
				}
				else
				{
					BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
					foreach (Attachment attachment in Session.Project.Attachments)
					{
						string str = string.Concat(FileName.Extension(attachment.Name).ToUpper(), " Files");
						binarySearchTree.Add(str);
					}
					List<string> sortedList = binarySearchTree.SortedList;
					this.AttachmentList.Groups.Clear();
					foreach (string str1 in sortedList)
					{
						this.AttachmentList.Groups.Add(str1, str1);
					}
					this.AttachmentList.Items.Clear();
					foreach (Attachment attachment1 in Session.Project.Attachments)
					{
						int length = (int)attachment1.Contents.Length;
						string str2 = string.Concat(length, " B");
						float single = (float)length / 1024f;
						if (single >= 1f)
						{
							str2 = string.Concat(single.ToString("F1"), " KB");
						}
						float single1 = (float)single / 1024f;
						if (single1 >= 1f)
						{
							str2 = string.Concat(single1.ToString("F1"), " MB");
						}
						float single2 = (float)single1 / 1024f;
						if (single2 >= 1f)
						{
							str2 = string.Concat(single2.ToString("F1"), " GB");
						}
						string str3 = string.Concat(FileName.Extension(attachment1.Name).ToUpper(), " Files");
						ListViewItem item = this.AttachmentList.Items.Add(attachment1.Name);
						item.SubItems.Add(str2);
						item.Group = this.AttachmentList.Groups[str3];
						item.Tag = attachment1;
					}
					if (Session.Project.Attachments.Count == 0)
					{
						ListViewItem listViewItem = this.AttachmentList.Items.Add("(no attachments)");
						listViewItem.ForeColor = SystemColors.GrayText;
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void update_background_item()
		{
			try
			{
				this.BackgroundDetails.Document.OpenNew(true);
				this.BackgroundDetails.Document.Write(HTML.Background(this.SelectedBackground, DisplaySize.Small));
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void update_background_list()
		{
			try
			{
				Background selectedBackground = this.SelectedBackground;
				this.BackgroundList.Items.Clear();
				if (Session.Project == null)
				{
					ListViewItem grayText = this.BackgroundList.Items.Add("(no project)");
					grayText.ForeColor = SystemColors.GrayText;
				}
				else
				{
					foreach (Background background in Session.Project.Backgrounds)
					{
						ListViewItem listViewItem = this.BackgroundList.Items.Add(background.Title);
						listViewItem.Tag = background;
						if (background.Details == "")
						{
							listViewItem.ForeColor = SystemColors.GrayText;
						}
						if (background != selectedBackground)
						{
							continue;
						}
						listViewItem.Selected = true;
					}
					if (Session.Project.Backgrounds.Count == 0)
					{
						ListViewItem grayText1 = this.BackgroundList.Items.Add("(no backgrounds)");
						grayText1.ForeColor = SystemColors.GrayText;
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void update_breadcrumbs()
		{
			Plot plot;
			try
			{
				this.BreadcrumbBar.Items.Clear();
				if (Session.Project != null)
				{
					List<PlotPoint> plotPoints = new List<PlotPoint>();
					Plot plot1 = this.PlotView.Plot;
					while (plot1 != null)
					{
						PlotPoint plotPoint = Session.Project.FindParent(plot1);
						if (plotPoint != null)
						{
							plot = Session.Project.FindParent(plotPoint);
						}
						else
						{
							plot = null;
						}
						plot1 = plot;
						plotPoints.Add(plotPoint);
					}
					plotPoints.Reverse();
					foreach (PlotPoint plotPoint1 in plotPoints)
					{
						bool flag = plotPoints.IndexOf(plotPoint1) != plotPoints.Count - 1;
						this.add_breadcrumb(plotPoint1, flag);
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void update_encyclopedia_entry()
		{
			Encyclopedia encyclopedia;
			try
			{
				if (Session.Project != null)
				{
					encyclopedia = Session.Project.Encyclopedia;
				}
				else
				{
					encyclopedia = null;
				}
				Encyclopedia encyclopedium = encyclopedia;
				string str = "";
				if (this.SelectedEncyclopediaItem == null)
				{
					str = HTML.EncyclopediaEntry(null, encyclopedium, DisplaySize.Small, true, true, true, false);
				}
				else
				{
					if (this.SelectedEncyclopediaItem is EncyclopediaEntry)
					{
						str = HTML.EncyclopediaEntry(this.SelectedEncyclopediaItem as EncyclopediaEntry, encyclopedium, DisplaySize.Small, true, true, true, false);
					}
					if (this.SelectedEncyclopediaItem is EncyclopediaGroup)
					{
						str = HTML.EncyclopediaGroup(this.SelectedEncyclopediaItem as EncyclopediaGroup, encyclopedium, DisplaySize.Small, true, true);
					}
				}
				this.EntryDetails.Document.OpenNew(true);
				this.EntryDetails.Document.Write(str);
				this.update_encyclopedia_images();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void update_encyclopedia_images()
		{
			try
			{
				EncyclopediaEntry selectedEncyclopediaItem = null;
				if (this.SelectedEncyclopediaItem != null && this.SelectedEncyclopediaItem is EncyclopediaEntry)
				{
					selectedEncyclopediaItem = this.SelectedEncyclopediaItem as EncyclopediaEntry;
				}
				bool count = false;
				if (selectedEncyclopediaItem != null)
				{
					count = selectedEncyclopediaItem.Images.Count > 0;
				}
				if (!count)
				{
					this.EntryImageList.Items.Clear();
					this.EntryImageList.LargeImageList = null;
					this.EncyclopediaEntrySplitter.Panel2Collapsed = true;
				}
				else
				{
					this.EntryImageList.Items.Clear();
					this.EntryImageList.LargeImageList = null;
					ImageList imageList = new ImageList()
					{
						ImageSize = new System.Drawing.Size(64, 64),
						ColorDepth = ColorDepth.Depth32Bit
					};
					this.EntryImageList.LargeImageList = imageList;
					foreach (EncyclopediaImage image in selectedEncyclopediaItem.Images)
					{
						if (image.Image == null)
						{
							continue;
						}
						ListViewItem listViewItem = this.EntryImageList.Items.Add(image.Name);
						listViewItem.Tag = image;
						Image bitmap = new Bitmap(64, 64);
						Graphics graphic = Graphics.FromImage(bitmap);
						if (image.Image.Size.Width <= image.Image.Size.Height)
						{
							System.Drawing.Size size = image.Image.Size;
							System.Drawing.Size size1 = image.Image.Size;
							int width = size.Width * 64 / size1.Height;
							Rectangle rectangle = new Rectangle((64 - width) / 2, 0, width, 64);
							graphic.DrawImage(image.Image, rectangle);
						}
						else
						{
							System.Drawing.Size size2 = image.Image.Size;
							System.Drawing.Size size3 = image.Image.Size;
							int height = size2.Height * 64 / size3.Width;
							Rectangle rectangle1 = new Rectangle(0, (64 - height) / 2, 64, height);
							graphic.DrawImage(image.Image, rectangle1);
						}
						imageList.Images.Add(bitmap);
						listViewItem.ImageIndex = imageList.Images.Count - 1;
					}
					this.EncyclopediaEntrySplitter.Panel2Collapsed = false;
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void update_encyclopedia_list()
		{
			try
			{
				string[] strArrays = null;
				string[] strArrays1 = this.EncSearchBox.Text.ToLower().Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
				this.EntryList.BeginUpdate();
				if (Session.Project == null)
				{
					ListViewItem grayText = this.EntryList.Items.Add("(no project)");
					grayText.ForeColor = SystemColors.GrayText;
				}
				else
				{
					this.EntryList.ShowGroups = true;
					BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
					foreach (EncyclopediaEntry entry in Session.Project.Encyclopedia.Entries)
					{
						if (entry.Category == null || !(entry.Category != ""))
						{
							continue;
						}
						binarySearchTree.Add(entry.Category);
					}
					List<string> sortedList = binarySearchTree.SortedList;
					sortedList.Insert(0, "Groups");
					sortedList.Add("Miscellaneous Entries");
					this.EntryList.Groups.Clear();
					foreach (string str in sortedList)
					{
						this.EntryList.Groups.Add(str, str);
					}
					List<ListViewItem> listViewItems = new List<ListViewItem>();
					if ((int)strArrays1.Length == 0)
					{
						List<EncyclopediaGroup> encyclopediaGroups = new List<EncyclopediaGroup>();
						encyclopediaGroups.AddRange(Session.Project.Encyclopedia.Groups);
						encyclopediaGroups.Sort();
						foreach (EncyclopediaGroup encyclopediaGroup in encyclopediaGroups)
						{
							ListViewItem listViewItem = new ListViewItem(encyclopediaGroup.Name)
							{
								Tag = encyclopediaGroup,
								Group = this.EntryList.Groups["Groups"]
							};
							listViewItems.Add(listViewItem);
						}
					}
					foreach (EncyclopediaEntry encyclopediaEntry in Session.Project.Encyclopedia.Entries)
					{
						if (!this.match(encyclopediaEntry, strArrays1))
						{
							continue;
						}
						ListViewItem item = new ListViewItem(encyclopediaEntry.Name)
						{
							Tag = encyclopediaEntry
						};
						if (encyclopediaEntry.Category == null || !(encyclopediaEntry.Category != ""))
						{
							item.Group = this.EntryList.Groups["Miscellaneous Entries"];
						}
						else
						{
							item.Group = this.EntryList.Groups[encyclopediaEntry.Category];
						}
						if (encyclopediaEntry.Details == "" && encyclopediaEntry.DMInfo == "")
						{
							item.ForeColor = SystemColors.GrayText;
						}
						listViewItems.Add(item);
					}
					if (listViewItems.Count == 0)
					{
						this.EntryList.ShowGroups = false;
						ListViewItem listViewItem1 = new ListViewItem((this.EncSearchBox.Text == "" ? "(no entries)" : "(no matching entries)"))
						{
							ForeColor = SystemColors.GrayText
						};
						listViewItems.Add(listViewItem1);
					}
					this.EntryList.Items.Clear();
					this.EntryList.Items.AddRange(listViewItems.ToArray());
				}
				this.EntryList.EndUpdate();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void update_encyclopedia_templates()
		{
			try
			{
				string str = string.Concat(Application.StartupPath, "\\Encyclopedia");
				if (Directory.Exists(str))
				{
					List<string> strs = new List<string>();
					strs.AddRange(Directory.GetFiles(str, "*.txt"));
					strs.AddRange(Directory.GetFiles(str, "*.htm"));
					strs.AddRange(Directory.GetFiles(str, "*.html"));
					if (strs.Count > 0)
					{
						this.EncAddBtn.DropDownItems.Add(new ToolStripSeparator());
						foreach (string str1 in strs)
						{
							ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(FileName.Name(str1))
							{
								Tag = str1
							};
							toolStripMenuItem.Click += new EventHandler(this.encyclopedia_template);
							this.EncAddBtn.DropDownItems.Add(toolStripMenuItem);
						}
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void update_navigation()
		{
			try
			{
				this.NavigationTree.BeginUpdate();
				this.NavigationTree.Nodes.Clear();
				if (Session.Project != null)
				{
					this.add_navigation_node(null, null);
					this.NavigationTree.ExpandAll();
				}
				this.NavigationTree.EndUpdate();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void update_notes()
		{
			try
			{
				this.NoteList.BeginUpdate();
				Note selectedNote = this.SelectedNote;
				this.NoteList.Items.Clear();
				this.NoteBox.Text = "";
				BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
				if (Session.Project != null)
				{
					foreach (Note note in Session.Project.Notes)
					{
						if (note.Category == "")
						{
							continue;
						}
						binarySearchTree.Add(note.Category);
					}
				}
				List<string> sortedList = binarySearchTree.SortedList;
				sortedList.Add("Notes");
				this.NoteList.Groups.Clear();
				foreach (string str in sortedList)
				{
					this.NoteList.Groups.Add(str, str);
				}
				string[] strArrays = this.NoteSearchBox.Text.ToLower().Split(new char[0]);
				if (Session.Project != null)
				{
					foreach (Note note1 in Session.Project.Notes)
					{
						if (!this.match(note1, strArrays))
						{
							continue;
						}
						ListViewItem item = this.NoteList.Items.Add(note1.Name);
						item.Tag = note1;
						if (note1.Category != "")
						{
							item.Group = this.NoteList.Groups[note1.Category];
						}
						else
						{
							item.Group = this.NoteList.Groups["Notes"];
						}
						if (note1.Content == "")
						{
							item.ForeColor = SystemColors.GrayText;
						}
						if (note1 != selectedNote)
						{
							continue;
						}
						item.Selected = true;
					}
				}
				if (this.NoteList.Groups["Notes"].Items.Count == 0)
				{
					ListViewItem grayText = this.NoteList.Items.Add((this.NoteSearchBox.Text == "" ? "(no notes)" : "(no matching notes)"));
					grayText.ForeColor = SystemColors.GrayText;
					grayText.Group = this.NoteList.Groups["Notes"];
				}
				this.NoteList.Sort();
				this.NoteList.EndUpdate();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void update_party()
		{
			if (this.PartyBrowser.Document == null)
			{
				this.PartyBrowser.DocumentText = "";
			}
			this.PartyBrowser.Document.OpenNew(true);
			this.PartyBrowser.Document.Write(HTML.PCs(this.fPartyBreakdownSecondary, DisplaySize.Small));
		}

		private void update_preview()
		{
			try
			{
				this.Preview.Document.OpenNew(true);
				bool flag = false;
				PlotPoint selectedPoint = this.get_selected_point();
				Map map = null;
				MapArea selectedArea = null;
				MapLocation selectedLocation = null;
				if (selectedPoint == null && this.fView == MainForm.ViewType.Delve)
				{
					MapView mapView = null;
					foreach (Control control in this.PreviewSplitter.Panel1.Controls)
					{
						if (!(control is MapView))
						{
							continue;
						}
						mapView = control as MapView;
						break;
					}
					if (mapView != null)
					{
						map = mapView.Map;
						selectedArea = mapView.SelectedArea;
					}
				}
				if (selectedPoint == null && this.fView == MainForm.ViewType.Map)
				{
					RegionalMapPanel regionalMapPanel = null;
					foreach (Control control1 in this.PreviewSplitter.Panel1.Controls)
					{
						if (!(control1 is RegionalMapPanel))
						{
							continue;
						}
						regionalMapPanel = control1 as RegionalMapPanel;
						break;
					}
					if (regionalMapPanel != null)
					{
						RegionalMap regionalMap = regionalMapPanel.Map;
						selectedLocation = regionalMapPanel.SelectedLocation;
					}
				}
				if (selectedArea != null)
				{
					this.Preview.Document.Write(HTML.MapArea(selectedArea, DisplaySize.Small));
				}
				else if (selectedLocation == null)
				{
					int num = (selectedPoint != null ? Workspace.GetPartyLevel(selectedPoint) : 0);
					this.Preview.Document.Write(HTML.PlotPoint(selectedPoint, this.PlotView.Plot, num, true, this.fView, DisplaySize.Small));
				}
				else
				{
					this.Preview.Document.Write(HTML.MapLocation(selectedLocation, DisplaySize.Small));
				}
				this.PreviewInfoSplitter.Panel2.Controls.Clear();
				if (selectedPoint != null)
				{
					if (selectedPoint.Element is Encounter)
					{
						Encounter element = selectedPoint.Element as Encounter;
						if (element.MapID != Guid.Empty)
						{
							this.set_tmap_preview(element.MapID, element.MapAreaID, element);
							flag = true;
						}
					}
					if (selectedPoint.Element is MapElement)
					{
						MapElement mapElement = selectedPoint.Element as MapElement;
						if (mapElement.MapID != Guid.Empty)
						{
							this.set_tmap_preview(mapElement.MapID, mapElement.MapAreaID, null);
							flag = true;
						}
					}
					if (!flag)
					{
						RegionalMap regionalMap1 = null;
						MapLocation mapLocation = null;
						selectedPoint.GetRegionalMapArea(ref regionalMap1, ref mapLocation, Session.Project);
						if (mapLocation != null)
						{
							this.set_rmap_preview(regionalMap1, mapLocation);
							flag = true;
						}
					}
					if (!flag && selectedPoint.Subplot.Points.Count > 0)
					{
						this.set_subplot_preview(selectedPoint.Subplot);
						flag = true;
					}
				}
				else if (selectedArea != null)
				{
					this.set_tmap_preview(map.ID, selectedArea.ID, null);
					flag = true;
				}
				if (!flag)
				{
					this.PreviewInfoSplitter.Panel2.Controls.Clear();
				}
				this.PreviewInfoSplitter.Panel2Collapsed = !flag;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void update_reference()
		{
			if (Session.Project != null)
			{
				this.InfoPanel.Level = Session.Project.Party.Level;
			}
			this.update_party();
			if (this.GeneratorBrowser.DocumentText == "")
			{
				List<string> strs = new List<string>();
				strs.AddRange(HTML.GetHead(null, null, DisplaySize.Small));
				strs.Add("<BODY>");
				strs.Add("<P class=instruction>");
				strs.Add("Use the buttons to the left to generate random names etc.");
				strs.Add("</P>");
				strs.Add("</BODY>");
				this.GeneratorBrowser.DocumentText = HTML.Concatenate(strs);
			}
			foreach (IAddIn addIn in this.fExtensibility.AddIns)
			{
				foreach (IPage quickReferencePage in addIn.QuickReferencePages)
				{
					quickReferencePage.UpdateView();
				}
			}
		}

		private void update_rules_list()
		{
			this.RulesList.Items.Clear();
			this.RulesList.ShowGroups = true;
			if (Session.Project != null)
			{
				foreach (IPlayerOption playerOption in Session.Project.PlayerOptions)
				{
					int num = 0;
					if (playerOption is Race)
					{
						num = 0;
					}
					if (playerOption is Class)
					{
						num = 1;
					}
					if (playerOption is Theme)
					{
						num = 2;
					}
					if (playerOption is ParagonPath)
					{
						num = 3;
					}
					if (playerOption is EpicDestiny)
					{
						num = 4;
					}
					if (playerOption is PlayerBackground)
					{
						num = 5;
					}
					if (playerOption is Feat)
					{
						switch ((playerOption as Feat).Tier)
						{
							case Tier.Heroic:
							{
								num = 6;
								break;
							}
							case Tier.Paragon:
							{
								num = 7;
								break;
							}
							case Tier.Epic:
							{
								num = 8;
								break;
							}
						}
					}
					if (playerOption is Weapon)
					{
						num = 9;
					}
					if (playerOption is Ritual)
					{
						num = 10;
					}
					if (playerOption is CreatureLore)
					{
						num = 11;
					}
					if (playerOption is Disease)
					{
						num = 12;
					}
					if (playerOption is Poison)
					{
						num = 13;
					}
					ListViewItem item = this.RulesList.Items.Add(playerOption.Name);
					item.Tag = playerOption;
					item.Group = this.RulesList.Groups[num];
				}
				if (this.RulesList.Items.Count == 0)
				{
					this.RulesList.ShowGroups = false;
					ListViewItem grayText = this.RulesList.Items.Add("(none)");
					grayText.ForeColor = SystemColors.GrayText;
				}
			}
		}

		private void update_selected_rule()
		{
			if (this.SelectedRule != null)
			{
				this.RulesBrowser.Document.OpenNew(true);
				this.RulesBrowser.Document.Write(HTML.PlayerOption(this.SelectedRule, DisplaySize.Small));
				return;
			}
			List<string> strs = new List<string>()
			{
				"<HTML>"
			};
			strs.AddRange(HTML.GetHead(null, null, DisplaySize.Small));
			strs.Add("<BODY>");
			strs.Add("<P class=instruction>On this page you can create and manage campaign-specific rules elements.</P>");
			strs.Add("</BODY>");
			strs.Add("</HTML>");
			this.RulesBrowser.Document.OpenNew(true);
			this.RulesBrowser.Document.Write(HTML.Concatenate(strs));
		}

		private void update_title()
		{
			try
			{
				string str = "Masterplan";
				if (Session.Project != null)
				{
					str = string.Concat(Session.Project.Name, " - Masterplan");
				}
				this.Text = str;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void update_workspace()
		{
			try
			{
				this.update_navigation();
				this.update_preview();
				this.update_breadcrumbs();
				this.PlotView.Invalidate();
				if (this.fView == MainForm.ViewType.Delve)
				{
					MapView mapView = null;
					foreach (Control control in this.PreviewSplitter.Panel1.Controls)
					{
						if (!(control is MapView) || !control.Visible)
						{
							continue;
						}
						mapView = control as MapView;
						break;
					}
					if (mapView != null)
					{
						mapView.MapChanged();
					}
				}
				if (this.fView == MainForm.ViewType.Map)
				{
					RegionalMapPanel regionalMapPanel = null;
					foreach (Control control1 in this.PreviewSplitter.Panel1.Controls)
					{
						if (!(control1 is RegionalMapPanel) || !control1.Visible)
						{
							continue;
						}
						regionalMapPanel = control1 as RegionalMapPanel;
						break;
					}
					if (regionalMapPanel != null)
					{
						regionalMapPanel.Invalidate();
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		public void UpdateView()
		{
			try
			{
				this.fUpdating = true;
				this.update_workspace();
				this.update_background_list();
				this.update_background_item();
				this.update_encyclopedia_list();
				this.update_encyclopedia_entry();
				this.update_rules_list();
				this.update_selected_rule();
				this.update_attachments();
				this.update_notes();
				this.update_reference();
				foreach (IAddIn addIn in this.fExtensibility.AddIns)
				{
					foreach (IPage page in addIn.Pages)
					{
						page.UpdateView();
					}
				}
				if (this.fView == MainForm.ViewType.Delve)
				{
					foreach (Control control in this.PreviewSplitter.Panel1.Controls)
					{
						if (!(control is MapView))
						{
							continue;
						}
						MapView mapView = control as MapView;
						mapView.Map = Session.Project.FindTacticalMap(mapView.Map.ID);
						break;
					}
				}
				if (this.fView == MainForm.ViewType.Map)
				{
					foreach (Control control1 in this.PreviewSplitter.Panel1.Controls)
					{
						if (!(control1 is RegionalMapPanel))
						{
							continue;
						}
						RegionalMapPanel regionalMapPanel = control1 as RegionalMapPanel;
						regionalMapPanel.Map = Session.Project.FindRegionalMap(regionalMapPanel.Map.ID);
						break;
					}
				}
				this.fUpdating = false;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ViewChallenges_Click(object sender, EventArgs e)
		{
			try
			{
				this.PlotView.Mode = PlotViewMode.HighlightChallenge;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ViewDefault_Click(object sender, EventArgs e)
		{
			try
			{
				this.PlotView.Mode = PlotViewMode.Normal;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ViewEncounters_Click(object sender, EventArgs e)
		{
			try
			{
				this.PlotView.Mode = PlotViewMode.HighlightEncounter;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ViewHighlighting_Click(object sender, EventArgs e)
		{
			try
			{
				this.PlotView.Mode = PlotViewMode.HighlightSelected;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ViewLevelling_Click(object sender, EventArgs e)
		{
			try
			{
				this.PlotView.ShowLevels = !this.PlotView.ShowLevels;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ViewLinks_DropDownOpening(object sender, EventArgs e)
		{
			this.ViewLinksCurved.Checked = this.PlotView.LinkStyle == PlotViewLinkStyle.Curved;
			this.ViewLinksAngled.Checked = this.PlotView.LinkStyle == PlotViewLinkStyle.Angled;
			this.ViewLinksStraight.Checked = this.PlotView.LinkStyle == PlotViewLinkStyle.Straight;
		}

		private void ViewLinksAngled_Click(object sender, EventArgs e)
		{
			this.PlotView.LinkStyle = PlotViewLinkStyle.Angled;
			Session.Preferences.LinkStyle = PlotViewLinkStyle.Angled;
		}

		private void ViewLinksCurved_Click(object sender, EventArgs e)
		{
			this.PlotView.LinkStyle = PlotViewLinkStyle.Curved;
			Session.Preferences.LinkStyle = PlotViewLinkStyle.Curved;
		}

		private void ViewLinksStraight_Click(object sender, EventArgs e)
		{
			this.PlotView.LinkStyle = PlotViewLinkStyle.Straight;
			Session.Preferences.LinkStyle = PlotViewLinkStyle.Straight;
		}

		private void ViewMenu_DropDownOpening(object sender, EventArgs e)
		{
			this.ViewDefault.Checked = this.PlotView.Mode == PlotViewMode.Normal;
			this.ViewHighlighting.Checked = this.PlotView.Mode == PlotViewMode.HighlightSelected;
			this.ViewEncounters.Checked = this.PlotView.Mode == PlotViewMode.HighlightEncounter;
			this.ViewTraps.Checked = this.PlotView.Mode == PlotViewMode.HighlightTrap;
			this.ViewChallenges.Checked = this.PlotView.Mode == PlotViewMode.HighlightChallenge;
			this.ViewQuests.Checked = this.PlotView.Mode == PlotViewMode.HighlightQuest;
			this.ViewParcels.Checked = this.PlotView.Mode == PlotViewMode.HighlightParcel;
			this.ViewLevelling.Checked = this.PlotView.ShowLevels;
			this.ViewTooltips.Checked = this.PlotView.ShowTooltips;
			this.ViewPreview.Checked = !this.PreviewSplitter.Panel2Collapsed;
			this.ViewNavigation.Checked = !this.NavigationSplitter.Panel1Collapsed;
		}

		private void ViewNavigation_Click(object sender, EventArgs e)
		{
			try
			{
				this.NavigationSplitter.Panel1Collapsed = !this.NavigationSplitter.Panel1Collapsed;
				Session.Preferences.ShowNavigation = !Session.Preferences.ShowNavigation;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ViewParcels_Click(object sender, EventArgs e)
		{
			try
			{
				this.PlotView.Mode = PlotViewMode.HighlightParcel;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ViewPreview_Click(object sender, EventArgs e)
		{
			try
			{
				this.PreviewSplitter.Panel2Collapsed = !this.PreviewSplitter.Panel2Collapsed;
				Session.Preferences.ShowPreview = !Session.Preferences.ShowPreview;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ViewQuests_Click(object sender, EventArgs e)
		{
			try
			{
				this.PlotView.Mode = PlotViewMode.HighlightQuest;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ViewTooltips_Click(object sender, EventArgs e)
		{
			try
			{
				this.PlotView.ShowTooltips = !this.PlotView.ShowTooltips;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ViewTraps_Click(object sender, EventArgs e)
		{
			try
			{
				this.PlotView.Mode = PlotViewMode.HighlightTrap;
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			Program.SplashScreen.Progress = Program.SplashScreen.Actions;
			Program.SplashScreen.CurrentAction = "Opening sample adventure...";
			this.open_file(this.fDownloadedFile);
			Program.SplashScreen.Close();
			Program.SplashScreen = null;
			this.fDownloadedFile = "";
		}

		private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			Program.SplashScreen.Progress = e.ProgressPercentage;
		}

		private void Welcome_DelveClicked(object sender, EventArgs e)
		{
			try
			{
				this.AdvancedDelve_Click(null, null);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void Welcome_NewProjectClicked(object sender, EventArgs e)
		{
			try
			{
				this.FileNew_Click(sender, e);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void Welcome_OpenLastProjectClicked(object sender, EventArgs e)
		{
			try
			{
				this.open_file(Session.Preferences.LastFile);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void Welcome_OpenProjectClicked(object sender, EventArgs e)
		{
			try
			{
				this.FileOpen_Click(sender, e);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void Welcome_PremadeClicked(object sender, EventArgs e)
		{
			try
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog()
				{
					Filter = Program.ProjectFilter,
					FileName = "Example"
				};
				if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fDownloadedFile = saveFileDialog.FileName;
					Program.SplashScreen = new ProgressScreen("Downloading example adventure...", 100);
					Program.SplashScreen.Show();
					WebClient webClient = new WebClient();
					webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.wc_DownloadProgressChanged);
					webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.wc_DownloadFileCompleted);
					webClient.DownloadFileAsync(new Uri("http://www.habitualindolence.net/masterplan/downloads/example.masterplan"), this.fDownloadedFile);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		public enum ViewType
		{
			Flowchart,
			Delve,
			Map
		}
	}
}