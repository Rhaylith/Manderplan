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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Races", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Classes", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Themes", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Paragon Paths", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Epic Destinies", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("Backgrounds", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup7 = new System.Windows.Forms.ListViewGroup("Feats (heroic tier)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup8 = new System.Windows.Forms.ListViewGroup("Feats (paragon tier)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup9 = new System.Windows.Forms.ListViewGroup("Feats (epic tier)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup10 = new System.Windows.Forms.ListViewGroup("Weapons", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup11 = new System.Windows.Forms.ListViewGroup("Rituals", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup12 = new System.Windows.Forms.ListViewGroup("Creature Lore", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup13 = new System.Windows.Forms.ListViewGroup("Diseases", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup14 = new System.Windows.Forms.ListViewGroup("Poisons", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup15 = new System.Windows.Forms.ListViewGroup("Issues", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup16 = new System.Windows.Forms.ListViewGroup("Information", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup17 = new System.Windows.Forms.ListViewGroup("Notes", System.Windows.Forms.HorizontalAlignment.Left);
            this.WorkspaceToolbar = new System.Windows.Forms.ToolStrip();
            this.AddBtn = new System.Windows.Forms.ToolStripSplitButton();
            this.AddEncounter = new System.Windows.Forms.ToolStripMenuItem();
            this.AddChallenge = new System.Windows.Forms.ToolStripMenuItem();
            this.AddTrap = new System.Windows.Forms.ToolStripMenuItem();
            this.AddQuest = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.PlotCutBtn = new System.Windows.Forms.ToolStripButton();
            this.PlotCopyBtn = new System.Windows.Forms.ToolStripButton();
            this.PlotPasteBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.SearchBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.ViewDefault = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewEncounters = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewTraps = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewChallenges = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewQuests = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewParcels = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewHighlighting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewLinks = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLinksCurved = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLinksAngled = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLinksStraight = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLevelling = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewTooltips = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewNavigation = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.FlowchartMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.FlowchartPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.FlowchartExport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator27 = new System.Windows.Forms.ToolStripSeparator();
            this.FlowchartAllXP = new System.Windows.Forms.ToolStripMenuItem();
            this.AdvancedBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.PlotAdvancedTreasure = new System.Windows.Forms.ToolStripMenuItem();
            this.PlotAdvancedIssues = new System.Windows.Forms.ToolStripMenuItem();
            this.PlotAdvancedDifficulty = new System.Windows.Forms.ToolStripMenuItem();
            this.PointMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextAddBetween = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextDisconnectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMoveTo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextState = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextStateNormal = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextStateCompleted = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextStateSkipped = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextExplore = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.FileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.FileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.FileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.FileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.FileAdvanced = new System.Windows.Forms.ToolStripMenuItem();
            this.AdvancedDelve = new System.Windows.Forms.ToolStripMenuItem();
            this.AdvancedSample = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator42 = new System.Windows.Forms.ToolStripSeparator();
            this.FileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectProject = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectOverview = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectCampaignSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator30 = new System.Windows.Forms.ToolStripSeparator();
            this.ProjectPassword = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.ProjectTacticalMaps = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectRegionalMaps = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ProjectPlayers = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectParcels = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectDecks = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectCustomCreatures = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectCalendars = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator37 = new System.Windows.Forms.ToolStripSeparator();
            this.ProjectEncounters = new System.Windows.Forms.ToolStripMenuItem();
            this.PlayerViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.PlayerViewShow = new System.Windows.Forms.ToolStripMenuItem();
            this.PlayerViewClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.PlayerViewOtherDisplay = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.PlayerViewTextSize = new System.Windows.Forms.ToolStripMenuItem();
            this.TextSizeSmall = new System.Windows.Forms.ToolStripMenuItem();
            this.TextSizeMedium = new System.Windows.Forms.ToolStripMenuItem();
            this.TextSizeLarge = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsImportProject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolsExportProject = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsExportHandout = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsExportLoot = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator34 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolsTileChecklist = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsMiniChecklist = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator49 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolsIssues = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsPowerStats = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolsLibraries = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolsAddIns = new System.Windows.Forms.ToolStripMenuItem();
            this.addinsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpManual = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.HelpFeedback = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.HelpTutorials = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator47 = new System.Windows.Forms.ToolStripSeparator();
            this.HelpWebsite = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpFacebook = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpTwitter = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.HelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.PreviewSplitter = new System.Windows.Forms.SplitContainer();
            this.NavigationSplitter = new System.Windows.Forms.SplitContainer();
            this.NavigationTree = new System.Windows.Forms.TreeView();
            this.PlotPanel = new System.Windows.Forms.Panel();
            this.PlotView = new Masterplan.Controls.PlotView();
            this.BreadcrumbBar = new System.Windows.Forms.StatusStrip();
            this.WorkspaceSearchBar = new System.Windows.Forms.ToolStrip();
            this.PlotSearchLbl = new System.Windows.Forms.ToolStripLabel();
            this.PlotSearchBox = new System.Windows.Forms.ToolStripTextBox();
            this.PlotClearBtn = new System.Windows.Forms.ToolStripLabel();
            this.PreviewInfoSplitter = new System.Windows.Forms.SplitContainer();
            this.PreviewPanel = new System.Windows.Forms.Panel();
            this.Preview = new System.Windows.Forms.WebBrowser();
            this.PreviewToolbar = new System.Windows.Forms.ToolStrip();
            this.EditBtn = new System.Windows.Forms.ToolStripButton();
            this.ExploreBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator41 = new System.Windows.Forms.ToolStripSeparator();
            this.PlotPointMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.PlotPointPlayerView = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator35 = new System.Windows.Forms.ToolStripSeparator();
            this.PlotPointExportHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.PlotPointExportFile = new System.Windows.Forms.ToolStripMenuItem();
            this.Pages = new System.Windows.Forms.TabControl();
            this.WorkspacePage = new System.Windows.Forms.TabPage();
            this.BackgroundPage = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.BackgroundList = new System.Windows.Forms.ListView();
            this.InfoHdr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BackgroundPanel = new System.Windows.Forms.Panel();
            this.BackgroundDetails = new System.Windows.Forms.WebBrowser();
            this.BackgroundToolbar = new System.Windows.Forms.ToolStrip();
            this.BackgroundAddBtn = new System.Windows.Forms.ToolStripButton();
            this.BackgroundRemoveBtn = new System.Windows.Forms.ToolStripButton();
            this.BackgroundEditBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.BackgroundUpBtn = new System.Windows.Forms.ToolStripButton();
            this.BackgroundDownBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.BackgroundPlayerView = new System.Windows.Forms.ToolStripDropDownButton();
            this.BackgroundPlayerViewSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.BackgroundPlayerViewAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator48 = new System.Windows.Forms.ToolStripSeparator();
            this.BackgroundShareBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.BackgroundShareExport = new System.Windows.Forms.ToolStripMenuItem();
            this.BackgroundShareImport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.BackgroundSharePublish = new System.Windows.Forms.ToolStripMenuItem();
            this.EncyclopediaPage = new System.Windows.Forms.TabPage();
            this.EncyclopediaSplitter = new System.Windows.Forms.SplitContainer();
            this.EntryList = new System.Windows.Forms.ListView();
            this.EntryHdr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EncyclopediaEntrySplitter = new System.Windows.Forms.SplitContainer();
            this.EntryPanel = new System.Windows.Forms.Panel();
            this.EntryDetails = new System.Windows.Forms.WebBrowser();
            this.EntryImageList = new System.Windows.Forms.ListView();
            this.EncyclopediaToolbar = new System.Windows.Forms.ToolStrip();
            this.EncAddBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.EncAddEntry = new System.Windows.Forms.ToolStripMenuItem();
            this.EncAddGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.EncRemoveBtn = new System.Windows.Forms.ToolStripButton();
            this.EncEditBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.EncCutBtn = new System.Windows.Forms.ToolStripButton();
            this.EncCopyBtn = new System.Windows.Forms.ToolStripButton();
            this.EncPasteBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.EncPlayerView = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator40 = new System.Windows.Forms.ToolStripSeparator();
            this.EncShareBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.EncShareExport = new System.Windows.Forms.ToolStripMenuItem();
            this.EncShareImport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.EncSharePublish = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            this.EncSearchLbl = new System.Windows.Forms.ToolStripLabel();
            this.EncSearchBox = new System.Windows.Forms.ToolStripTextBox();
            this.EncClearLbl = new System.Windows.Forms.ToolStripLabel();
            this.RulesPage = new System.Windows.Forms.TabPage();
            this.RulesSplitter = new System.Windows.Forms.SplitContainer();
            this.RulesList = new System.Windows.Forms.ListView();
            this.RulesHdr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RulesToolbar = new System.Windows.Forms.ToolStrip();
            this.RulesAddBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.AddRace = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator31 = new System.Windows.Forms.ToolStripSeparator();
            this.AddClass = new System.Windows.Forms.ToolStripMenuItem();
            this.AddTheme = new System.Windows.Forms.ToolStripMenuItem();
            this.AddParagonPath = new System.Windows.Forms.ToolStripMenuItem();
            this.AddEpicDestiny = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator32 = new System.Windows.Forms.ToolStripSeparator();
            this.AddBackground = new System.Windows.Forms.ToolStripMenuItem();
            this.AddFeat = new System.Windows.Forms.ToolStripMenuItem();
            this.AddWeapon = new System.Windows.Forms.ToolStripMenuItem();
            this.AddRitual = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator39 = new System.Windows.Forms.ToolStripSeparator();
            this.AddCreatureLore = new System.Windows.Forms.ToolStripMenuItem();
            this.AddDisease = new System.Windows.Forms.ToolStripMenuItem();
            this.AddPoison = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator33 = new System.Windows.Forms.ToolStripSeparator();
            this.RulesShareBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.RulesShareExport = new System.Windows.Forms.ToolStripMenuItem();
            this.RulesShareImport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.RulesSharePublish = new System.Windows.Forms.ToolStripMenuItem();
            this.RulesBrowserPanel = new System.Windows.Forms.Panel();
            this.RulesBrowser = new System.Windows.Forms.WebBrowser();
            this.EncEntryToolbar = new System.Windows.Forms.ToolStrip();
            this.RulesRemoveBtn = new System.Windows.Forms.ToolStripButton();
            this.RulesEditBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator43 = new System.Windows.Forms.ToolStripSeparator();
            this.RuleEncyclopediaBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator36 = new System.Windows.Forms.ToolStripSeparator();
            this.RulesPlayerViewBtn = new System.Windows.Forms.ToolStripButton();
            this.AttachmentsPage = new System.Windows.Forms.TabPage();
            this.AttachmentList = new System.Windows.Forms.ListView();
            this.AttachmentHdr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AttachmentSizeHdr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AttachmentToolbar = new System.Windows.Forms.ToolStrip();
            this.AttachmentImportBtn = new System.Windows.Forms.ToolStripButton();
            this.AttachmentRemoveBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.AttachmentExtract = new System.Windows.Forms.ToolStripDropDownButton();
            this.AttachmentExtractSimple = new System.Windows.Forms.ToolStripMenuItem();
            this.AttachmentExtractAndRun = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
            this.AttachmentPlayerView = new System.Windows.Forms.ToolStripButton();
            this.JotterPage = new System.Windows.Forms.TabPage();
            this.JotterSplitter = new System.Windows.Forms.SplitContainer();
            this.NoteList = new System.Windows.Forms.ListView();
            this.NoteHdr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NoteBox = new System.Windows.Forms.TextBox();
            this.JotterToolbar = new System.Windows.Forms.ToolStrip();
            this.NoteAddBtn = new System.Windows.Forms.ToolStripButton();
            this.NoteRemoveBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.NoteCategoryBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator38 = new System.Windows.Forms.ToolStripSeparator();
            this.NoteCutBtn = new System.Windows.Forms.ToolStripButton();
            this.NoteCopyBtn = new System.Windows.Forms.ToolStripButton();
            this.NotePasteBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.NoteSearchLbl = new System.Windows.Forms.ToolStripLabel();
            this.NoteSearchBox = new System.Windows.Forms.ToolStripTextBox();
            this.NoteClearLbl = new System.Windows.Forms.ToolStripLabel();
            this.ReferencePage = new System.Windows.Forms.TabPage();
            this.ReferenceSplitter = new System.Windows.Forms.SplitContainer();
            this.ReferencePages = new System.Windows.Forms.TabControl();
            this.PartyPage = new System.Windows.Forms.TabPage();
            this.PartyBrowser = new System.Windows.Forms.WebBrowser();
            this.ToolsPage = new System.Windows.Forms.TabPage();
            this.ToolBrowserPanel = new System.Windows.Forms.Panel();
            this.GeneratorBrowser = new System.Windows.Forms.WebBrowser();
            this.GeneratorToolbar = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator26 = new System.Windows.Forms.ToolStripSeparator();
            this.ElfNameBtn = new System.Windows.Forms.ToolStripButton();
            this.DwarfNameBtn = new System.Windows.Forms.ToolStripButton();
            this.HalflingNameBtn = new System.Windows.Forms.ToolStripButton();
            this.ExoticNameBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator44 = new System.Windows.Forms.ToolStripSeparator();
            this.TreasureBtn = new System.Windows.Forms.ToolStripButton();
            this.BookTitleBtn = new System.Windows.Forms.ToolStripButton();
            this.PotionBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator45 = new System.Windows.Forms.ToolStripSeparator();
            this.NPCBtn = new System.Windows.Forms.ToolStripButton();
            this.RoomBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator46 = new System.Windows.Forms.ToolStripSeparator();
            this.ElfTextBtn = new System.Windows.Forms.ToolStripButton();
            this.DwarfTextBtn = new System.Windows.Forms.ToolStripButton();
            this.PrimordialTextBtn = new System.Windows.Forms.ToolStripButton();
            this.CompendiumPage = new System.Windows.Forms.TabPage();
            this.CompendiumBrowser = new System.Windows.Forms.WebBrowser();
            this.InfoPanel = new Masterplan.Controls.InfoPanel();
            this.ReferenceToolbar = new System.Windows.Forms.ToolStrip();
            this.DieRollerBtn = new System.Windows.Forms.ToolStripButton();
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
            this.SuspendLayout();
            // 
            // WorkspaceToolbar
            // 
            this.WorkspaceToolbar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.WorkspaceToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddBtn,
            this.RemoveBtn,
            this.toolStripSeparator3,
            this.PlotCutBtn,
            this.PlotCopyBtn,
            this.PlotPasteBtn,
            this.toolStripSeparator5,
            this.SearchBtn,
            this.toolStripSeparator9,
            this.ViewMenu,
            this.FlowchartMenu,
            this.AdvancedBtn});
            this.WorkspaceToolbar.Location = new System.Drawing.Point(0, 0);
            this.WorkspaceToolbar.Name = "WorkspaceToolbar";
            this.WorkspaceToolbar.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.WorkspaceToolbar.Size = new System.Drawing.Size(938, 32);
            this.WorkspaceToolbar.TabIndex = 1;
            this.WorkspaceToolbar.Text = "toolStrip1";
            // 
            // AddBtn
            // 
            this.AddBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.AddBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddEncounter,
            this.AddChallenge,
            this.AddTrap,
            this.AddQuest});
            this.AddBtn.Image = ((System.Drawing.Image)(resources.GetObject("AddBtn.Image")));
            this.AddBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(67, 29);
            this.AddBtn.Text = "Add";
            this.AddBtn.ButtonClick += new System.EventHandler(this.AddBtn_Click);
            // 
            // AddEncounter
            // 
            this.AddEncounter.Name = "AddEncounter";
            this.AddEncounter.Size = new System.Drawing.Size(221, 30);
            this.AddEncounter.Text = "Encounter...";
            this.AddEncounter.Click += new System.EventHandler(this.AddEncounter_Click);
            // 
            // AddChallenge
            // 
            this.AddChallenge.Name = "AddChallenge";
            this.AddChallenge.Size = new System.Drawing.Size(221, 30);
            this.AddChallenge.Text = "Skill Challenge...";
            this.AddChallenge.Click += new System.EventHandler(this.AddChallenge_Click);
            // 
            // AddTrap
            // 
            this.AddTrap.Name = "AddTrap";
            this.AddTrap.Size = new System.Drawing.Size(221, 30);
            this.AddTrap.Text = "Trap / Hazard...";
            this.AddTrap.Click += new System.EventHandler(this.AddTrap_Click);
            // 
            // AddQuest
            // 
            this.AddQuest.Name = "AddQuest";
            this.AddQuest.Size = new System.Drawing.Size(221, 30);
            this.AddQuest.Text = "Quest...";
            this.AddQuest.Click += new System.EventHandler(this.AddQuest_Click);
            // 
            // RemoveBtn
            // 
            this.RemoveBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.RemoveBtn.Image = ((System.Drawing.Image)(resources.GetObject("RemoveBtn.Image")));
            this.RemoveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RemoveBtn.Name = "RemoveBtn";
            this.RemoveBtn.Size = new System.Drawing.Size(80, 29);
            this.RemoveBtn.Text = "Remove";
            this.RemoveBtn.Click += new System.EventHandler(this.RemoveBtn_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 32);
            // 
            // PlotCutBtn
            // 
            this.PlotCutBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.PlotCutBtn.Image = ((System.Drawing.Image)(resources.GetObject("PlotCutBtn.Image")));
            this.PlotCutBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PlotCutBtn.Name = "PlotCutBtn";
            this.PlotCutBtn.Size = new System.Drawing.Size(43, 29);
            this.PlotCutBtn.Text = "Cut";
            this.PlotCutBtn.Click += new System.EventHandler(this.CutBtn_Click);
            // 
            // PlotCopyBtn
            // 
            this.PlotCopyBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.PlotCopyBtn.Image = ((System.Drawing.Image)(resources.GetObject("PlotCopyBtn.Image")));
            this.PlotCopyBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PlotCopyBtn.Name = "PlotCopyBtn";
            this.PlotCopyBtn.Size = new System.Drawing.Size(58, 29);
            this.PlotCopyBtn.Text = "Copy";
            this.PlotCopyBtn.Click += new System.EventHandler(this.CopyBtn_Click);
            // 
            // PlotPasteBtn
            // 
            this.PlotPasteBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.PlotPasteBtn.Image = ((System.Drawing.Image)(resources.GetObject("PlotPasteBtn.Image")));
            this.PlotPasteBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PlotPasteBtn.Name = "PlotPasteBtn";
            this.PlotPasteBtn.Size = new System.Drawing.Size(57, 29);
            this.PlotPasteBtn.Text = "Paste";
            this.PlotPasteBtn.Click += new System.EventHandler(this.PasteBtn_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 32);
            // 
            // SearchBtn
            // 
            this.SearchBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SearchBtn.Image = ((System.Drawing.Image)(resources.GetObject("SearchBtn.Image")));
            this.SearchBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(68, 29);
            this.SearchBtn.Text = "Search";
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 32);
            // 
            // ViewMenu
            // 
            this.ViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewDefault,
            this.toolStripSeparator7,
            this.ViewEncounters,
            this.ViewTraps,
            this.ViewChallenges,
            this.ViewQuests,
            this.ViewParcels,
            this.toolStripSeparator8,
            this.ViewHighlighting,
            this.toolStripSeparator6,
            this.ViewLinks,
            this.ViewLevelling,
            this.ViewTooltips,
            this.toolStripSeparator11,
            this.ViewNavigation,
            this.ViewPreview});
            this.ViewMenu.Name = "ViewMenu";
            this.ViewMenu.Size = new System.Drawing.Size(67, 29);
            this.ViewMenu.Text = "View";
            this.ViewMenu.DropDownOpening += new System.EventHandler(this.ViewMenu_DropDownOpening);
            // 
            // ViewDefault
            // 
            this.ViewDefault.Name = "ViewDefault";
            this.ViewDefault.Size = new System.Drawing.Size(267, 30);
            this.ViewDefault.Text = "Default View";
            this.ViewDefault.Click += new System.EventHandler(this.ViewDefault_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(264, 6);
            // 
            // ViewEncounters
            // 
            this.ViewEncounters.Name = "ViewEncounters";
            this.ViewEncounters.Size = new System.Drawing.Size(267, 30);
            this.ViewEncounters.Text = "Show Encounters";
            this.ViewEncounters.Click += new System.EventHandler(this.ViewEncounters_Click);
            // 
            // ViewTraps
            // 
            this.ViewTraps.Name = "ViewTraps";
            this.ViewTraps.Size = new System.Drawing.Size(267, 30);
            this.ViewTraps.Text = "Show Traps / Hazards";
            this.ViewTraps.Click += new System.EventHandler(this.ViewTraps_Click);
            // 
            // ViewChallenges
            // 
            this.ViewChallenges.Name = "ViewChallenges";
            this.ViewChallenges.Size = new System.Drawing.Size(267, 30);
            this.ViewChallenges.Text = "Show Skill Challenges";
            this.ViewChallenges.Click += new System.EventHandler(this.ViewChallenges_Click);
            // 
            // ViewQuests
            // 
            this.ViewQuests.Name = "ViewQuests";
            this.ViewQuests.Size = new System.Drawing.Size(267, 30);
            this.ViewQuests.Text = "Show Quests";
            this.ViewQuests.Click += new System.EventHandler(this.ViewQuests_Click);
            // 
            // ViewParcels
            // 
            this.ViewParcels.Name = "ViewParcels";
            this.ViewParcels.Size = new System.Drawing.Size(267, 30);
            this.ViewParcels.Text = "Show Treasure Parcels";
            this.ViewParcels.Click += new System.EventHandler(this.ViewParcels_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(264, 6);
            // 
            // ViewHighlighting
            // 
            this.ViewHighlighting.Name = "ViewHighlighting";
            this.ViewHighlighting.Size = new System.Drawing.Size(267, 30);
            this.ViewHighlighting.Text = "Highlighting";
            this.ViewHighlighting.Click += new System.EventHandler(this.ViewHighlighting_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(264, 6);
            // 
            // ViewLinks
            // 
            this.ViewLinks.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewLinksCurved,
            this.ViewLinksAngled,
            this.ViewLinksStraight});
            this.ViewLinks.Name = "ViewLinks";
            this.ViewLinks.Size = new System.Drawing.Size(267, 30);
            this.ViewLinks.Text = "Show Links";
            this.ViewLinks.DropDownOpening += new System.EventHandler(this.ViewLinks_DropDownOpening);
            // 
            // ViewLinksCurved
            // 
            this.ViewLinksCurved.Name = "ViewLinksCurved";
            this.ViewLinksCurved.Size = new System.Drawing.Size(157, 30);
            this.ViewLinksCurved.Text = "Curved";
            this.ViewLinksCurved.Click += new System.EventHandler(this.ViewLinksCurved_Click);
            // 
            // ViewLinksAngled
            // 
            this.ViewLinksAngled.Name = "ViewLinksAngled";
            this.ViewLinksAngled.Size = new System.Drawing.Size(157, 30);
            this.ViewLinksAngled.Text = "Angled";
            this.ViewLinksAngled.Click += new System.EventHandler(this.ViewLinksAngled_Click);
            // 
            // ViewLinksStraight
            // 
            this.ViewLinksStraight.Name = "ViewLinksStraight";
            this.ViewLinksStraight.Size = new System.Drawing.Size(157, 30);
            this.ViewLinksStraight.Text = "Straight";
            this.ViewLinksStraight.Click += new System.EventHandler(this.ViewLinksStraight_Click);
            // 
            // ViewLevelling
            // 
            this.ViewLevelling.Name = "ViewLevelling";
            this.ViewLevelling.Size = new System.Drawing.Size(267, 30);
            this.ViewLevelling.Text = "Show Levelling";
            this.ViewLevelling.Click += new System.EventHandler(this.ViewLevelling_Click);
            // 
            // ViewTooltips
            // 
            this.ViewTooltips.Name = "ViewTooltips";
            this.ViewTooltips.Size = new System.Drawing.Size(267, 30);
            this.ViewTooltips.Text = "Show Tooltips";
            this.ViewTooltips.Click += new System.EventHandler(this.ViewTooltips_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(264, 6);
            // 
            // ViewNavigation
            // 
            this.ViewNavigation.Name = "ViewNavigation";
            this.ViewNavigation.Size = new System.Drawing.Size(267, 30);
            this.ViewNavigation.Text = "Show Navigation";
            this.ViewNavigation.Click += new System.EventHandler(this.ViewNavigation_Click);
            // 
            // ViewPreview
            // 
            this.ViewPreview.Name = "ViewPreview";
            this.ViewPreview.Size = new System.Drawing.Size(267, 30);
            this.ViewPreview.Text = "Show Preview";
            this.ViewPreview.Click += new System.EventHandler(this.ViewPreview_Click);
            // 
            // FlowchartMenu
            // 
            this.FlowchartMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FlowchartMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FlowchartPrint,
            this.FlowchartExport,
            this.toolStripSeparator27,
            this.FlowchartAllXP});
            this.FlowchartMenu.Image = ((System.Drawing.Image)(resources.GetObject("FlowchartMenu.Image")));
            this.FlowchartMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FlowchartMenu.Name = "FlowchartMenu";
            this.FlowchartMenu.Size = new System.Drawing.Size(106, 29);
            this.FlowchartMenu.Text = "Flowchart";
            // 
            // FlowchartPrint
            // 
            this.FlowchartPrint.Name = "FlowchartPrint";
            this.FlowchartPrint.Size = new System.Drawing.Size(277, 30);
            this.FlowchartPrint.Text = "Print...";
            this.FlowchartPrint.Click += new System.EventHandler(this.FlowchartPrint_Click);
            // 
            // FlowchartExport
            // 
            this.FlowchartExport.Name = "FlowchartExport";
            this.FlowchartExport.Size = new System.Drawing.Size(277, 30);
            this.FlowchartExport.Text = "Export...";
            this.FlowchartExport.Click += new System.EventHandler(this.FlowchartExport_Click);
            // 
            // toolStripSeparator27
            // 
            this.toolStripSeparator27.Name = "toolStripSeparator27";
            this.toolStripSeparator27.Size = new System.Drawing.Size(274, 6);
            // 
            // FlowchartAllXP
            // 
            this.FlowchartAllXP.Name = "FlowchartAllXP";
            this.FlowchartAllXP.Size = new System.Drawing.Size(277, 30);
            this.FlowchartAllXP.Text = "Maximum Available XP";
            this.FlowchartAllXP.Click += new System.EventHandler(this.FlowchartAllXP_Click);
            // 
            // AdvancedBtn
            // 
            this.AdvancedBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.AdvancedBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PlotAdvancedTreasure,
            this.PlotAdvancedIssues,
            this.PlotAdvancedDifficulty});
            this.AdvancedBtn.Image = ((System.Drawing.Image)(resources.GetObject("AdvancedBtn.Image")));
            this.AdvancedBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AdvancedBtn.Name = "AdvancedBtn";
            this.AdvancedBtn.Size = new System.Drawing.Size(109, 29);
            this.AdvancedBtn.Text = "Advanced";
            // 
            // PlotAdvancedTreasure
            // 
            this.PlotAdvancedTreasure.Name = "PlotAdvancedTreasure";
            this.PlotAdvancedTreasure.Size = new System.Drawing.Size(259, 30);
            this.PlotAdvancedTreasure.Text = "Export Treasure List...";
            this.PlotAdvancedTreasure.Click += new System.EventHandler(this.PlotAdvancedTreasure_Click);
            // 
            // PlotAdvancedIssues
            // 
            this.PlotAdvancedIssues.Name = "PlotAdvancedIssues";
            this.PlotAdvancedIssues.Size = new System.Drawing.Size(259, 30);
            this.PlotAdvancedIssues.Text = "Plot Design Issues";
            this.PlotAdvancedIssues.Click += new System.EventHandler(this.PlotAdvancedIssues_Click);
            // 
            // PlotAdvancedDifficulty
            // 
            this.PlotAdvancedDifficulty.Name = "PlotAdvancedDifficulty";
            this.PlotAdvancedDifficulty.Size = new System.Drawing.Size(259, 30);
            this.PlotAdvancedDifficulty.Text = "Adjust Difficulty...";
            this.PlotAdvancedDifficulty.Click += new System.EventHandler(this.PlotAdvancedDifficulty_Click);
            // 
            // PointMenu
            // 
            this.PointMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.PointMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextAdd,
            this.ContextAddBetween,
            this.toolStripSeparator28,
            this.ContextDisconnectAll,
            this.ContextDisconnect,
            this.toolStripSeparator1,
            this.ContextMoveTo,
            this.toolStripSeparator2,
            this.ContextState,
            this.toolStripSeparator20,
            this.ContextEdit,
            this.ContextRemove,
            this.toolStripSeparator29,
            this.ContextExplore});
            this.PointMenu.Name = "PointMenu";
            this.PointMenu.Size = new System.Drawing.Size(221, 304);
            this.PointMenu.Opening += new System.ComponentModel.CancelEventHandler(this.PointMenu_Opening);
            // 
            // ContextAdd
            // 
            this.ContextAdd.Name = "ContextAdd";
            this.ContextAdd.Size = new System.Drawing.Size(220, 30);
            this.ContextAdd.Text = "Add Point...";
            this.ContextAdd.Click += new System.EventHandler(this.ContextAdd_Click);
            // 
            // ContextAddBetween
            // 
            this.ContextAddBetween.Name = "ContextAddBetween";
            this.ContextAddBetween.Size = new System.Drawing.Size(220, 30);
            this.ContextAddBetween.Text = "Add Point";
            // 
            // toolStripSeparator28
            // 
            this.toolStripSeparator28.Name = "toolStripSeparator28";
            this.toolStripSeparator28.Size = new System.Drawing.Size(217, 6);
            // 
            // ContextDisconnectAll
            // 
            this.ContextDisconnectAll.Name = "ContextDisconnectAll";
            this.ContextDisconnectAll.Size = new System.Drawing.Size(220, 30);
            this.ContextDisconnectAll.Text = "Disconnect Point";
            this.ContextDisconnectAll.Click += new System.EventHandler(this.ContextDisconnectAll_Click);
            // 
            // ContextDisconnect
            // 
            this.ContextDisconnect.Name = "ContextDisconnect";
            this.ContextDisconnect.Size = new System.Drawing.Size(220, 30);
            this.ContextDisconnect.Text = "Disconnect From";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(217, 6);
            // 
            // ContextMoveTo
            // 
            this.ContextMoveTo.Name = "ContextMoveTo";
            this.ContextMoveTo.Size = new System.Drawing.Size(220, 30);
            this.ContextMoveTo.Text = "Move To Subplot";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(217, 6);
            // 
            // ContextState
            // 
            this.ContextState.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextStateNormal,
            this.ContextStateCompleted,
            this.ContextStateSkipped});
            this.ContextState.Name = "ContextState";
            this.ContextState.Size = new System.Drawing.Size(220, 30);
            this.ContextState.Text = "State";
            // 
            // ContextStateNormal
            // 
            this.ContextStateNormal.Name = "ContextStateNormal";
            this.ContextStateNormal.Size = new System.Drawing.Size(184, 30);
            this.ContextStateNormal.Text = "Normal";
            this.ContextStateNormal.Click += new System.EventHandler(this.ContextStateNormal_Click);
            // 
            // ContextStateCompleted
            // 
            this.ContextStateCompleted.Name = "ContextStateCompleted";
            this.ContextStateCompleted.Size = new System.Drawing.Size(184, 30);
            this.ContextStateCompleted.Text = "Completed";
            this.ContextStateCompleted.Click += new System.EventHandler(this.ContextStateCompleted_Click);
            // 
            // ContextStateSkipped
            // 
            this.ContextStateSkipped.Name = "ContextStateSkipped";
            this.ContextStateSkipped.Size = new System.Drawing.Size(184, 30);
            this.ContextStateSkipped.Text = "Skipped";
            this.ContextStateSkipped.Click += new System.EventHandler(this.ContextStateSkipped_Click);
            // 
            // toolStripSeparator20
            // 
            this.toolStripSeparator20.Name = "toolStripSeparator20";
            this.toolStripSeparator20.Size = new System.Drawing.Size(217, 6);
            // 
            // ContextEdit
            // 
            this.ContextEdit.Name = "ContextEdit";
            this.ContextEdit.Size = new System.Drawing.Size(220, 30);
            this.ContextEdit.Text = "Edit";
            this.ContextEdit.Click += new System.EventHandler(this.EditBtn_Click);
            // 
            // ContextRemove
            // 
            this.ContextRemove.Name = "ContextRemove";
            this.ContextRemove.Size = new System.Drawing.Size(220, 30);
            this.ContextRemove.Text = "Remove";
            this.ContextRemove.Click += new System.EventHandler(this.RemoveBtn_Click);
            // 
            // toolStripSeparator29
            // 
            this.toolStripSeparator29.Name = "toolStripSeparator29";
            this.toolStripSeparator29.Size = new System.Drawing.Size(217, 6);
            // 
            // ContextExplore
            // 
            this.ContextExplore.Name = "ContextExplore";
            this.ContextExplore.Size = new System.Drawing.Size(220, 30);
            this.ContextExplore.Text = "Explore Subplot";
            this.ContextExplore.Click += new System.EventHandler(this.ExploreBtn_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.ProjectMenu,
            this.PlayerViewMenu,
            this.ToolsMenu,
            this.HelpMenu});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.MainMenu.Size = new System.Drawing.Size(1296, 35);
            this.MainMenu.TabIndex = 4;
            this.MainMenu.Text = "menuStrip1";
            // 
            // FileMenu
            // 
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileNew,
            this.toolStripMenuItem1,
            this.FileOpen,
            this.toolStripMenuItem2,
            this.FileSave,
            this.FileSaveAs,
            this.toolStripMenuItem3,
            this.FileAdvanced,
            this.toolStripSeparator42,
            this.FileExit});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(50, 29);
            this.FileMenu.Text = "File";
            this.FileMenu.DropDownOpening += new System.EventHandler(this.FileMenu_DropDownOpening);
            // 
            // FileNew
            // 
            this.FileNew.Name = "FileNew";
            this.FileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.FileNew.Size = new System.Drawing.Size(276, 30);
            this.FileNew.Text = "New Project...";
            this.FileNew.Click += new System.EventHandler(this.FileNew_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(273, 6);
            // 
            // FileOpen
            // 
            this.FileOpen.Name = "FileOpen";
            this.FileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.FileOpen.Size = new System.Drawing.Size(276, 30);
            this.FileOpen.Text = "Open Project...";
            this.FileOpen.Click += new System.EventHandler(this.FileOpen_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(273, 6);
            // 
            // FileSave
            // 
            this.FileSave.Name = "FileSave";
            this.FileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.FileSave.Size = new System.Drawing.Size(276, 30);
            this.FileSave.Text = "Save Project";
            this.FileSave.Click += new System.EventHandler(this.FileSave_Click);
            // 
            // FileSaveAs
            // 
            this.FileSaveAs.Name = "FileSaveAs";
            this.FileSaveAs.Size = new System.Drawing.Size(276, 30);
            this.FileSaveAs.Text = "Save Project As...";
            this.FileSaveAs.Click += new System.EventHandler(this.FileSaveAs_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(273, 6);
            // 
            // FileAdvanced
            // 
            this.FileAdvanced.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AdvancedDelve,
            this.AdvancedSample});
            this.FileAdvanced.Name = "FileAdvanced";
            this.FileAdvanced.Size = new System.Drawing.Size(276, 30);
            this.FileAdvanced.Text = "Advanced";
            // 
            // AdvancedDelve
            // 
            this.AdvancedDelve.Name = "AdvancedDelve";
            this.AdvancedDelve.Size = new System.Drawing.Size(354, 30);
            this.AdvancedDelve.Text = "Create a Dungeon Delve...";
            this.AdvancedDelve.Click += new System.EventHandler(this.AdvancedDelve_Click);
            // 
            // AdvancedSample
            // 
            this.AdvancedSample.Name = "AdvancedSample";
            this.AdvancedSample.Size = new System.Drawing.Size(354, 30);
            this.AdvancedSample.Text = "Download a Premade Adventure";
            this.AdvancedSample.Click += new System.EventHandler(this.AdvancedSample_Click);
            // 
            // toolStripSeparator42
            // 
            this.toolStripSeparator42.Name = "toolStripSeparator42";
            this.toolStripSeparator42.Size = new System.Drawing.Size(273, 6);
            // 
            // FileExit
            // 
            this.FileExit.Name = "FileExit";
            this.FileExit.Size = new System.Drawing.Size(276, 30);
            this.FileExit.Text = "Exit";
            this.FileExit.Click += new System.EventHandler(this.FileExit_Click);
            // 
            // ProjectMenu
            // 
            this.ProjectMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProjectProject,
            this.ProjectOverview,
            this.ProjectCampaignSettings,
            this.toolStripSeparator30,
            this.ProjectPassword,
            this.toolStripSeparator10,
            this.ProjectTacticalMaps,
            this.ProjectRegionalMaps,
            this.toolStripSeparator4,
            this.ProjectPlayers,
            this.ProjectParcels,
            this.ProjectDecks,
            this.ProjectCustomCreatures,
            this.ProjectCalendars,
            this.toolStripSeparator37,
            this.ProjectEncounters});
            this.ProjectMenu.Name = "ProjectMenu";
            this.ProjectMenu.Size = new System.Drawing.Size(78, 29);
            this.ProjectMenu.Text = "Project";
            this.ProjectMenu.DropDownOpening += new System.EventHandler(this.ProjectMenu_DropDownOpening);
            // 
            // ProjectProject
            // 
            this.ProjectProject.Name = "ProjectProject";
            this.ProjectProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.ProjectProject.Size = new System.Drawing.Size(350, 30);
            this.ProjectProject.Text = "Project Properties";
            this.ProjectProject.Click += new System.EventHandler(this.ProjectProject_Click);
            // 
            // ProjectOverview
            // 
            this.ProjectOverview.Name = "ProjectOverview";
            this.ProjectOverview.Size = new System.Drawing.Size(350, 30);
            this.ProjectOverview.Text = "Project Overview";
            this.ProjectOverview.Click += new System.EventHandler(this.ProjectOverview_Click);
            // 
            // ProjectCampaignSettings
            // 
            this.ProjectCampaignSettings.Name = "ProjectCampaignSettings";
            this.ProjectCampaignSettings.Size = new System.Drawing.Size(350, 30);
            this.ProjectCampaignSettings.Text = "Campaign Settings";
            this.ProjectCampaignSettings.Click += new System.EventHandler(this.ProjectCampaignSettings_Click);
            // 
            // toolStripSeparator30
            // 
            this.toolStripSeparator30.Name = "toolStripSeparator30";
            this.toolStripSeparator30.Size = new System.Drawing.Size(347, 6);
            // 
            // ProjectPassword
            // 
            this.ProjectPassword.Name = "ProjectPassword";
            this.ProjectPassword.Size = new System.Drawing.Size(350, 30);
            this.ProjectPassword.Text = "Password Protection";
            this.ProjectPassword.Click += new System.EventHandler(this.ProjectPassword_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(347, 6);
            // 
            // ProjectTacticalMaps
            // 
            this.ProjectTacticalMaps.Name = "ProjectTacticalMaps";
            this.ProjectTacticalMaps.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.ProjectTacticalMaps.Size = new System.Drawing.Size(350, 30);
            this.ProjectTacticalMaps.Text = "Tactical Maps";
            this.ProjectTacticalMaps.Click += new System.EventHandler(this.ProjectTacticalMaps_Click);
            // 
            // ProjectRegionalMaps
            // 
            this.ProjectRegionalMaps.Name = "ProjectRegionalMaps";
            this.ProjectRegionalMaps.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.ProjectRegionalMaps.Size = new System.Drawing.Size(350, 30);
            this.ProjectRegionalMaps.Text = "Regional Maps";
            this.ProjectRegionalMaps.Click += new System.EventHandler(this.ProjectRegionalMaps_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(347, 6);
            // 
            // ProjectPlayers
            // 
            this.ProjectPlayers.Name = "ProjectPlayers";
            this.ProjectPlayers.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.ProjectPlayers.Size = new System.Drawing.Size(350, 30);
            this.ProjectPlayers.Text = "Player Characters";
            this.ProjectPlayers.Click += new System.EventHandler(this.ProjectPlayers_Click);
            // 
            // ProjectParcels
            // 
            this.ProjectParcels.Name = "ProjectParcels";
            this.ProjectParcels.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.ProjectParcels.Size = new System.Drawing.Size(350, 30);
            this.ProjectParcels.Text = "Treasure Parcels";
            this.ProjectParcels.Click += new System.EventHandler(this.ProjectParcels_Click);
            // 
            // ProjectDecks
            // 
            this.ProjectDecks.Name = "ProjectDecks";
            this.ProjectDecks.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.ProjectDecks.Size = new System.Drawing.Size(350, 30);
            this.ProjectDecks.Text = "Encounter Decks";
            this.ProjectDecks.Click += new System.EventHandler(this.ProjectDecks_Click);
            // 
            // ProjectCustomCreatures
            // 
            this.ProjectCustomCreatures.Name = "ProjectCustomCreatures";
            this.ProjectCustomCreatures.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.ProjectCustomCreatures.Size = new System.Drawing.Size(350, 30);
            this.ProjectCustomCreatures.Text = "Custom Creatures and NPCs";
            this.ProjectCustomCreatures.Click += new System.EventHandler(this.ProjectCustomCreatures_Click);
            // 
            // ProjectCalendars
            // 
            this.ProjectCalendars.Name = "ProjectCalendars";
            this.ProjectCalendars.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.ProjectCalendars.Size = new System.Drawing.Size(350, 30);
            this.ProjectCalendars.Text = "Calendars";
            this.ProjectCalendars.Click += new System.EventHandler(this.ProjectCalendars_Click);
            // 
            // toolStripSeparator37
            // 
            this.toolStripSeparator37.Name = "toolStripSeparator37";
            this.toolStripSeparator37.Size = new System.Drawing.Size(347, 6);
            // 
            // ProjectEncounters
            // 
            this.ProjectEncounters.Name = "ProjectEncounters";
            this.ProjectEncounters.Size = new System.Drawing.Size(350, 30);
            this.ProjectEncounters.Text = "Paused Encounters";
            this.ProjectEncounters.Click += new System.EventHandler(this.ProjectEncounters_Click);
            // 
            // PlayerViewMenu
            // 
            this.PlayerViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PlayerViewShow,
            this.PlayerViewClear,
            this.toolStripMenuItem7,
            this.PlayerViewOtherDisplay,
            this.toolStripSeparator14,
            this.PlayerViewTextSize});
            this.PlayerViewMenu.Name = "PlayerViewMenu";
            this.PlayerViewMenu.Size = new System.Drawing.Size(113, 29);
            this.PlayerViewMenu.Text = "Player View";
            this.PlayerViewMenu.DropDownOpening += new System.EventHandler(this.PlayerViewMenu_DropDownOpening);
            // 
            // PlayerViewShow
            // 
            this.PlayerViewShow.Name = "PlayerViewShow";
            this.PlayerViewShow.Size = new System.Drawing.Size(279, 30);
            this.PlayerViewShow.Text = "Show";
            this.PlayerViewShow.Click += new System.EventHandler(this.ToolsPlayerView_Click);
            // 
            // PlayerViewClear
            // 
            this.PlayerViewClear.Name = "PlayerViewClear";
            this.PlayerViewClear.Size = new System.Drawing.Size(279, 30);
            this.PlayerViewClear.Text = "Clear";
            this.PlayerViewClear.Click += new System.EventHandler(this.ToolsPlayerViewClear_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(276, 6);
            // 
            // PlayerViewOtherDisplay
            // 
            this.PlayerViewOtherDisplay.Name = "PlayerViewOtherDisplay";
            this.PlayerViewOtherDisplay.Size = new System.Drawing.Size(279, 30);
            this.PlayerViewOtherDisplay.Text = "Show on Other Display";
            this.PlayerViewOtherDisplay.Click += new System.EventHandler(this.ToolsPlayerViewSecondary_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(276, 6);
            // 
            // PlayerViewTextSize
            // 
            this.PlayerViewTextSize.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TextSizeSmall,
            this.TextSizeMedium,
            this.TextSizeLarge});
            this.PlayerViewTextSize.Name = "PlayerViewTextSize";
            this.PlayerViewTextSize.Size = new System.Drawing.Size(279, 30);
            this.PlayerViewTextSize.Text = "Text Size";
            // 
            // TextSizeSmall
            // 
            this.TextSizeSmall.Name = "TextSizeSmall";
            this.TextSizeSmall.Size = new System.Drawing.Size(162, 30);
            this.TextSizeSmall.Text = "Small";
            this.TextSizeSmall.Click += new System.EventHandler(this.TextSizeSmall_Click);
            // 
            // TextSizeMedium
            // 
            this.TextSizeMedium.Name = "TextSizeMedium";
            this.TextSizeMedium.Size = new System.Drawing.Size(162, 30);
            this.TextSizeMedium.Text = "Medium";
            this.TextSizeMedium.Click += new System.EventHandler(this.TextSizeMedium_Click);
            // 
            // TextSizeLarge
            // 
            this.TextSizeLarge.Name = "TextSizeLarge";
            this.TextSizeLarge.Size = new System.Drawing.Size(162, 30);
            this.TextSizeLarge.Text = "Large";
            this.TextSizeLarge.Click += new System.EventHandler(this.TextSizeLarge_Click);
            // 
            // ToolsMenu
            // 
            this.ToolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolsImportProject,
            this.toolStripSeparator25,
            this.ToolsExportProject,
            this.ToolsExportHandout,
            this.ToolsExportLoot,
            this.toolStripSeparator34,
            this.ToolsTileChecklist,
            this.ToolsMiniChecklist,
            this.toolStripSeparator49,
            this.ToolsIssues,
            this.ToolsPowerStats,
            this.toolStripMenuItem4,
            this.ToolsLibraries,
            this.toolStripMenuItem5,
            this.ToolsAddIns});
            this.ToolsMenu.Name = "ToolsMenu";
            this.ToolsMenu.Size = new System.Drawing.Size(65, 29);
            this.ToolsMenu.Text = "Tools";
            this.ToolsMenu.DropDownOpening += new System.EventHandler(this.ToolsMenu_DropDownOpening);
            // 
            // ToolsImportProject
            // 
            this.ToolsImportProject.Name = "ToolsImportProject";
            this.ToolsImportProject.Size = new System.Drawing.Size(288, 30);
            this.ToolsImportProject.Text = "Import Project...";
            this.ToolsImportProject.Click += new System.EventHandler(this.ToolsImportProject_Click);
            // 
            // toolStripSeparator25
            // 
            this.toolStripSeparator25.Name = "toolStripSeparator25";
            this.toolStripSeparator25.Size = new System.Drawing.Size(285, 6);
            // 
            // ToolsExportProject
            // 
            this.ToolsExportProject.Name = "ToolsExportProject";
            this.ToolsExportProject.Size = new System.Drawing.Size(288, 30);
            this.ToolsExportProject.Text = "Export Project...";
            this.ToolsExportProject.Click += new System.EventHandler(this.ToolsExportProject_Click);
            // 
            // ToolsExportHandout
            // 
            this.ToolsExportHandout.Name = "ToolsExportHandout";
            this.ToolsExportHandout.Size = new System.Drawing.Size(288, 30);
            this.ToolsExportHandout.Text = "Export Handout...";
            this.ToolsExportHandout.Click += new System.EventHandler(this.ToolsExportHandout_Click);
            // 
            // ToolsExportLoot
            // 
            this.ToolsExportLoot.Name = "ToolsExportLoot";
            this.ToolsExportLoot.Size = new System.Drawing.Size(288, 30);
            this.ToolsExportLoot.Text = "Export Treasure List...";
            this.ToolsExportLoot.Click += new System.EventHandler(this.ToolsExportLoot_Click);
            // 
            // toolStripSeparator34
            // 
            this.toolStripSeparator34.Name = "toolStripSeparator34";
            this.toolStripSeparator34.Size = new System.Drawing.Size(285, 6);
            // 
            // ToolsTileChecklist
            // 
            this.ToolsTileChecklist.Name = "ToolsTileChecklist";
            this.ToolsTileChecklist.Size = new System.Drawing.Size(288, 30);
            this.ToolsTileChecklist.Text = "Map Tile Checklist...";
            this.ToolsTileChecklist.Click += new System.EventHandler(this.ToolsTileChecklist_Click);
            // 
            // ToolsMiniChecklist
            // 
            this.ToolsMiniChecklist.Name = "ToolsMiniChecklist";
            this.ToolsMiniChecklist.Size = new System.Drawing.Size(288, 30);
            this.ToolsMiniChecklist.Text = "Miniature Checklist...";
            this.ToolsMiniChecklist.Click += new System.EventHandler(this.ToolsMiniChecklist_Click);
            // 
            // toolStripSeparator49
            // 
            this.toolStripSeparator49.Name = "toolStripSeparator49";
            this.toolStripSeparator49.Size = new System.Drawing.Size(285, 6);
            // 
            // ToolsIssues
            // 
            this.ToolsIssues.Name = "ToolsIssues";
            this.ToolsIssues.Size = new System.Drawing.Size(288, 30);
            this.ToolsIssues.Text = "Plot Design Issues";
            this.ToolsIssues.Click += new System.EventHandler(this.ToolsIssues_Click);
            // 
            // ToolsPowerStats
            // 
            this.ToolsPowerStats.Name = "ToolsPowerStats";
            this.ToolsPowerStats.Size = new System.Drawing.Size(288, 30);
            this.ToolsPowerStats.Text = "Creature Power Statistics";
            this.ToolsPowerStats.Click += new System.EventHandler(this.ToolsPowerStats_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(285, 6);
            // 
            // ToolsLibraries
            // 
            this.ToolsLibraries.Name = "ToolsLibraries";
            this.ToolsLibraries.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.ToolsLibraries.Size = new System.Drawing.Size(288, 30);
            this.ToolsLibraries.Text = "Libraries";
            this.ToolsLibraries.Click += new System.EventHandler(this.ToolsLibraries_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(285, 6);
            // 
            // ToolsAddIns
            // 
            this.ToolsAddIns.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addinsToolStripMenuItem});
            this.ToolsAddIns.Name = "ToolsAddIns";
            this.ToolsAddIns.Size = new System.Drawing.Size(288, 30);
            this.ToolsAddIns.Text = "Add-Ins";
            // 
            // addinsToolStripMenuItem
            // 
            this.addinsToolStripMenuItem.Name = "addinsToolStripMenuItem";
            this.addinsToolStripMenuItem.Size = new System.Drawing.Size(166, 30);
            this.addinsToolStripMenuItem.Text = "[add-ins]";
            // 
            // HelpMenu
            // 
            this.HelpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpManual,
            this.toolStripSeparator12,
            this.HelpFeedback,
            this.toolStripMenuItem8,
            this.HelpTutorials,
            this.toolStripSeparator47,
            this.HelpWebsite,
            this.HelpFacebook,
            this.HelpTwitter,
            this.toolStripSeparator13,
            this.HelpAbout});
            this.HelpMenu.Name = "HelpMenu";
            this.HelpMenu.Size = new System.Drawing.Size(61, 29);
            this.HelpMenu.Text = "Help";
            // 
            // HelpManual
            // 
            this.HelpManual.Name = "HelpManual";
            this.HelpManual.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.HelpManual.Size = new System.Drawing.Size(291, 30);
            this.HelpManual.Text = "Manual";
            this.HelpManual.Click += new System.EventHandler(this.HelpManual_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(288, 6);
            // 
            // HelpFeedback
            // 
            this.HelpFeedback.Name = "HelpFeedback";
            this.HelpFeedback.Size = new System.Drawing.Size(291, 30);
            this.HelpFeedback.Text = "Send Feedback";
            this.HelpFeedback.Click += new System.EventHandler(this.HelpFeedback_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(288, 6);
            // 
            // HelpTutorials
            // 
            this.HelpTutorials.Name = "HelpTutorials";
            this.HelpTutorials.Size = new System.Drawing.Size(291, 30);
            this.HelpTutorials.Text = "Tutorials";
            this.HelpTutorials.Click += new System.EventHandler(this.HelpTutorials_Click);
            // 
            // toolStripSeparator47
            // 
            this.toolStripSeparator47.Name = "toolStripSeparator47";
            this.toolStripSeparator47.Size = new System.Drawing.Size(288, 6);
            // 
            // HelpWebsite
            // 
            this.HelpWebsite.Name = "HelpWebsite";
            this.HelpWebsite.Size = new System.Drawing.Size(291, 30);
            this.HelpWebsite.Text = "Masterplan Website";
            this.HelpWebsite.Click += new System.EventHandler(this.HelpWebsite_Click);
            // 
            // HelpFacebook
            // 
            this.HelpFacebook.Name = "HelpFacebook";
            this.HelpFacebook.Size = new System.Drawing.Size(291, 30);
            this.HelpFacebook.Text = "Masterplan on Facebook";
            this.HelpFacebook.Click += new System.EventHandler(this.HelpFacebook_Click);
            // 
            // HelpTwitter
            // 
            this.HelpTwitter.Name = "HelpTwitter";
            this.HelpTwitter.Size = new System.Drawing.Size(291, 30);
            this.HelpTwitter.Text = "Masterplan on Twitter";
            this.HelpTwitter.Click += new System.EventHandler(this.HelpTwitter_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(288, 6);
            // 
            // HelpAbout
            // 
            this.HelpAbout.Name = "HelpAbout";
            this.HelpAbout.Size = new System.Drawing.Size(291, 30);
            this.HelpAbout.Text = "About";
            this.HelpAbout.Click += new System.EventHandler(this.HelpAbout_Click);
            // 
            // PreviewSplitter
            // 
            this.PreviewSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.PreviewSplitter.Location = new System.Drawing.Point(0, 0);
            this.PreviewSplitter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PreviewSplitter.Name = "PreviewSplitter";
            // 
            // PreviewSplitter.Panel1
            // 
            this.PreviewSplitter.Panel1.Controls.Add(this.NavigationSplitter);
            this.PreviewSplitter.Panel1.Controls.Add(this.WorkspaceToolbar);
            // 
            // PreviewSplitter.Panel2
            // 
            this.PreviewSplitter.Panel2.Controls.Add(this.PreviewInfoSplitter);
            this.PreviewSplitter.Size = new System.Drawing.Size(1288, 640);
            this.PreviewSplitter.SplitterDistance = 938;
            this.PreviewSplitter.SplitterWidth = 6;
            this.PreviewSplitter.TabIndex = 6;
            // 
            // NavigationSplitter
            // 
            this.NavigationSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NavigationSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.NavigationSplitter.Location = new System.Drawing.Point(0, 32);
            this.NavigationSplitter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NavigationSplitter.Name = "NavigationSplitter";
            // 
            // NavigationSplitter.Panel1
            // 
            this.NavigationSplitter.Panel1.Controls.Add(this.NavigationTree);
            // 
            // NavigationSplitter.Panel2
            // 
            this.NavigationSplitter.Panel2.Controls.Add(this.PlotPanel);
            this.NavigationSplitter.Panel2.Controls.Add(this.WorkspaceSearchBar);
            this.NavigationSplitter.Size = new System.Drawing.Size(938, 608);
            this.NavigationSplitter.SplitterDistance = 152;
            this.NavigationSplitter.SplitterWidth = 6;
            this.NavigationSplitter.TabIndex = 4;
            // 
            // NavigationTree
            // 
            this.NavigationTree.AllowDrop = true;
            this.NavigationTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NavigationTree.HideSelection = false;
            this.NavigationTree.Location = new System.Drawing.Point(0, 0);
            this.NavigationTree.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NavigationTree.Name = "NavigationTree";
            this.NavigationTree.ShowRootLines = false;
            this.NavigationTree.Size = new System.Drawing.Size(152, 608);
            this.NavigationTree.TabIndex = 0;
            this.NavigationTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.NavigationTree_AfterSelect);
            this.NavigationTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.NavigationTree_DragDrop);
            this.NavigationTree.DragOver += new System.Windows.Forms.DragEventHandler(this.NavigationTree_DragOver);
            // 
            // PlotPanel
            // 
            this.PlotPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PlotPanel.Controls.Add(this.PlotView);
            this.PlotPanel.Controls.Add(this.BreadcrumbBar);
            this.PlotPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlotPanel.Location = new System.Drawing.Point(0, 31);
            this.PlotPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PlotPanel.Name = "PlotPanel";
            this.PlotPanel.Size = new System.Drawing.Size(780, 577);
            this.PlotPanel.TabIndex = 5;
            // 
            // PlotView
            // 
            this.PlotView.AllowDrop = true;
            this.PlotView.ContextMenuStrip = this.PointMenu;
            this.PlotView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlotView.Filter = "";
            this.PlotView.LinkStyle = Masterplan.Controls.PlotViewLinkStyle.Curved;
            this.PlotView.Location = new System.Drawing.Point(0, 0);
            this.PlotView.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.PlotView.Mode = Masterplan.Controls.PlotViewMode.Normal;
            this.PlotView.Name = "PlotView";
            this.PlotView.Plot = null;
            this.PlotView.SelectedPoint = null;
            this.PlotView.ShowLevels = true;
            this.PlotView.ShowTooltips = true;
            this.PlotView.Size = new System.Drawing.Size(778, 553);
            this.PlotView.TabIndex = 2;
            this.PlotView.PlotChanged += new System.EventHandler(this.PlotView_PlotChanged);
            this.PlotView.PlotLayoutChanged += new System.EventHandler(this.PlotView_PlotLayoutChanged);
            this.PlotView.SelectionChanged += new System.EventHandler(this.PlotView_SelectionChanged);
            this.PlotView.DoubleClick += new System.EventHandler(this.PlotView_DoubleClick);
            // 
            // BreadcrumbBar
            // 
            this.BreadcrumbBar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.BreadcrumbBar.Location = new System.Drawing.Point(0, 553);
            this.BreadcrumbBar.Name = "BreadcrumbBar";
            this.BreadcrumbBar.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.BreadcrumbBar.Size = new System.Drawing.Size(778, 22);
            this.BreadcrumbBar.SizingGrip = false;
            this.BreadcrumbBar.TabIndex = 4;
            this.BreadcrumbBar.Text = "statusStrip1";
            // 
            // WorkspaceSearchBar
            // 
            this.WorkspaceSearchBar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.WorkspaceSearchBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PlotSearchLbl,
            this.PlotSearchBox,
            this.PlotClearBtn});
            this.WorkspaceSearchBar.Location = new System.Drawing.Point(0, 0);
            this.WorkspaceSearchBar.Name = "WorkspaceSearchBar";
            this.WorkspaceSearchBar.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.WorkspaceSearchBar.Size = new System.Drawing.Size(780, 31);
            this.WorkspaceSearchBar.TabIndex = 3;
            this.WorkspaceSearchBar.Text = "toolStrip1";
            // 
            // PlotSearchLbl
            // 
            this.PlotSearchLbl.Name = "PlotSearchLbl";
            this.PlotSearchLbl.Size = new System.Drawing.Size(96, 28);
            this.PlotSearchLbl.Text = "Search for:";
            // 
            // PlotSearchBox
            // 
            this.PlotSearchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PlotSearchBox.Name = "PlotSearchBox";
            this.PlotSearchBox.Size = new System.Drawing.Size(299, 31);
            this.PlotSearchBox.TextChanged += new System.EventHandler(this.SearchBox_TextChanged);
            // 
            // PlotClearBtn
            // 
            this.PlotClearBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.PlotClearBtn.Image = ((System.Drawing.Image)(resources.GetObject("PlotClearBtn.Image")));
            this.PlotClearBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PlotClearBtn.IsLink = true;
            this.PlotClearBtn.Name = "PlotClearBtn";
            this.PlotClearBtn.Size = new System.Drawing.Size(51, 28);
            this.PlotClearBtn.Text = "Clear";
            this.PlotClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // PreviewInfoSplitter
            // 
            this.PreviewInfoSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewInfoSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.PreviewInfoSplitter.Location = new System.Drawing.Point(0, 0);
            this.PreviewInfoSplitter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PreviewInfoSplitter.Name = "PreviewInfoSplitter";
            this.PreviewInfoSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // PreviewInfoSplitter.Panel1
            // 
            this.PreviewInfoSplitter.Panel1.Controls.Add(this.PreviewPanel);
            this.PreviewInfoSplitter.Panel1.Controls.Add(this.PreviewToolbar);
            this.PreviewInfoSplitter.Size = new System.Drawing.Size(344, 640);
            this.PreviewInfoSplitter.SplitterDistance = 455;
            this.PreviewInfoSplitter.SplitterWidth = 6;
            this.PreviewInfoSplitter.TabIndex = 1;
            // 
            // PreviewPanel
            // 
            this.PreviewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PreviewPanel.Controls.Add(this.Preview);
            this.PreviewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewPanel.Location = new System.Drawing.Point(0, 32);
            this.PreviewPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PreviewPanel.Name = "PreviewPanel";
            this.PreviewPanel.Size = new System.Drawing.Size(344, 423);
            this.PreviewPanel.TabIndex = 1;
            // 
            // Preview
            // 
            this.Preview.AllowWebBrowserDrop = false;
            this.Preview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Preview.IsWebBrowserContextMenuEnabled = false;
            this.Preview.Location = new System.Drawing.Point(0, 0);
            this.Preview.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Preview.MinimumSize = new System.Drawing.Size(30, 31);
            this.Preview.Name = "Preview";
            this.Preview.ScriptErrorsSuppressed = true;
            this.Preview.Size = new System.Drawing.Size(342, 421);
            this.Preview.TabIndex = 0;
            this.Preview.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.Preview_Navigating);
            // 
            // PreviewToolbar
            // 
            this.PreviewToolbar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.PreviewToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditBtn,
            this.ExploreBtn,
            this.toolStripSeparator41,
            this.PlotPointMenu});
            this.PreviewToolbar.Location = new System.Drawing.Point(0, 0);
            this.PreviewToolbar.Name = "PreviewToolbar";
            this.PreviewToolbar.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.PreviewToolbar.Size = new System.Drawing.Size(344, 32);
            this.PreviewToolbar.TabIndex = 1;
            this.PreviewToolbar.Text = "toolStrip1";
            // 
            // EditBtn
            // 
            this.EditBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.EditBtn.Image = ((System.Drawing.Image)(resources.GetObject("EditBtn.Image")));
            this.EditBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditBtn.Name = "EditBtn";
            this.EditBtn.Size = new System.Drawing.Size(127, 29);
            this.EditBtn.Text = "Edit Plot Point";
            this.EditBtn.Click += new System.EventHandler(this.EditBtn_Click);
            // 
            // ExploreBtn
            // 
            this.ExploreBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ExploreBtn.Image = ((System.Drawing.Image)(resources.GetObject("ExploreBtn.Image")));
            this.ExploreBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExploreBtn.Name = "ExploreBtn";
            this.ExploreBtn.Size = new System.Drawing.Size(142, 29);
            this.ExploreBtn.Text = "Explore Subplot";
            this.ExploreBtn.Click += new System.EventHandler(this.ExploreBtn_Click);
            // 
            // toolStripSeparator41
            // 
            this.toolStripSeparator41.Name = "toolStripSeparator41";
            this.toolStripSeparator41.Size = new System.Drawing.Size(6, 32);
            // 
            // PlotPointMenu
            // 
            this.PlotPointMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.PlotPointMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PlotPointPlayerView,
            this.toolStripSeparator35,
            this.PlotPointExportHTML,
            this.PlotPointExportFile});
            this.PlotPointMenu.Image = ((System.Drawing.Image)(resources.GetObject("PlotPointMenu.Image")));
            this.PlotPointMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PlotPointMenu.Name = "PlotPointMenu";
            this.PlotPointMenu.Size = new System.Drawing.Size(74, 29);
            this.PlotPointMenu.Text = "Share";
            // 
            // PlotPointPlayerView
            // 
            this.PlotPointPlayerView.Name = "PlotPointPlayerView";
            this.PlotPointPlayerView.Size = new System.Drawing.Size(252, 30);
            this.PlotPointPlayerView.Text = "Send to Player View";
            this.PlotPointPlayerView.Click += new System.EventHandler(this.PlotPointPlayerView_Click);
            // 
            // toolStripSeparator35
            // 
            this.toolStripSeparator35.Name = "toolStripSeparator35";
            this.toolStripSeparator35.Size = new System.Drawing.Size(249, 6);
            // 
            // PlotPointExportHTML
            // 
            this.PlotPointExportHTML.Name = "PlotPointExportHTML";
            this.PlotPointExportHTML.Size = new System.Drawing.Size(252, 30);
            this.PlotPointExportHTML.Text = "Export to HTML...";
            this.PlotPointExportHTML.Click += new System.EventHandler(this.PlotPointExportHTML_Click);
            // 
            // PlotPointExportFile
            // 
            this.PlotPointExportFile.Name = "PlotPointExportFile";
            this.PlotPointExportFile.Size = new System.Drawing.Size(252, 30);
            this.PlotPointExportFile.Text = "Export to File...";
            this.PlotPointExportFile.Click += new System.EventHandler(this.PlotPointExportFile_Click);
            // 
            // Pages
            // 
            this.Pages.Controls.Add(this.WorkspacePage);
            this.Pages.Controls.Add(this.BackgroundPage);
            this.Pages.Controls.Add(this.EncyclopediaPage);
            this.Pages.Controls.Add(this.RulesPage);
            this.Pages.Controls.Add(this.AttachmentsPage);
            this.Pages.Controls.Add(this.JotterPage);
            this.Pages.Controls.Add(this.ReferencePage);
            this.Pages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pages.Location = new System.Drawing.Point(0, 35);
            this.Pages.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Pages.Name = "Pages";
            this.Pages.SelectedIndex = 0;
            this.Pages.Size = new System.Drawing.Size(1296, 673);
            this.Pages.TabIndex = 5;
            // 
            // WorkspacePage
            // 
            this.WorkspacePage.Controls.Add(this.PreviewSplitter);
            this.WorkspacePage.Location = new System.Drawing.Point(4, 29);
            this.WorkspacePage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.WorkspacePage.Name = "WorkspacePage";
            this.WorkspacePage.Size = new System.Drawing.Size(1288, 640);
            this.WorkspacePage.TabIndex = 0;
            this.WorkspacePage.Text = "Plot Workspace";
            this.WorkspacePage.UseVisualStyleBackColor = true;
            // 
            // BackgroundPage
            // 
            this.BackgroundPage.Controls.Add(this.splitContainer1);
            this.BackgroundPage.Controls.Add(this.BackgroundToolbar);
            this.BackgroundPage.Location = new System.Drawing.Point(4, 29);
            this.BackgroundPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BackgroundPage.Name = "BackgroundPage";
            this.BackgroundPage.Size = new System.Drawing.Size(1288, 638);
            this.BackgroundPage.TabIndex = 4;
            this.BackgroundPage.Text = "Background";
            this.BackgroundPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 32);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.BackgroundList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.BackgroundPanel);
            this.splitContainer1.Size = new System.Drawing.Size(1288, 606);
            this.splitContainer1.SplitterDistance = 180;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 1;
            // 
            // BackgroundList
            // 
            this.BackgroundList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.InfoHdr});
            this.BackgroundList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackgroundList.FullRowSelect = true;
            this.BackgroundList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.BackgroundList.HideSelection = false;
            this.BackgroundList.Location = new System.Drawing.Point(0, 0);
            this.BackgroundList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BackgroundList.MultiSelect = false;
            this.BackgroundList.Name = "BackgroundList";
            this.BackgroundList.Size = new System.Drawing.Size(180, 606);
            this.BackgroundList.TabIndex = 0;
            this.BackgroundList.UseCompatibleStateImageBehavior = false;
            this.BackgroundList.View = System.Windows.Forms.View.Details;
            this.BackgroundList.SelectedIndexChanged += new System.EventHandler(this.BackgroundList_SelectedIndexChanged);
            this.BackgroundList.DoubleClick += new System.EventHandler(this.BackgroundEditBtn_Click);
            // 
            // InfoHdr
            // 
            this.InfoHdr.Text = "Information";
            this.InfoHdr.Width = 150;
            // 
            // BackgroundPanel
            // 
            this.BackgroundPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BackgroundPanel.Controls.Add(this.BackgroundDetails);
            this.BackgroundPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackgroundPanel.Location = new System.Drawing.Point(0, 0);
            this.BackgroundPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BackgroundPanel.Name = "BackgroundPanel";
            this.BackgroundPanel.Size = new System.Drawing.Size(1102, 606);
            this.BackgroundPanel.TabIndex = 0;
            // 
            // BackgroundDetails
            // 
            this.BackgroundDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackgroundDetails.IsWebBrowserContextMenuEnabled = false;
            this.BackgroundDetails.Location = new System.Drawing.Point(0, 0);
            this.BackgroundDetails.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BackgroundDetails.MinimumSize = new System.Drawing.Size(30, 31);
            this.BackgroundDetails.Name = "BackgroundDetails";
            this.BackgroundDetails.Size = new System.Drawing.Size(1100, 604);
            this.BackgroundDetails.TabIndex = 0;
            this.BackgroundDetails.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.BackgroundDetails_Navigating);
            // 
            // BackgroundToolbar
            // 
            this.BackgroundToolbar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.BackgroundToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BackgroundAddBtn,
            this.BackgroundRemoveBtn,
            this.BackgroundEditBtn,
            this.toolStripSeparator21,
            this.BackgroundUpBtn,
            this.BackgroundDownBtn,
            this.toolStripSeparator23,
            this.BackgroundPlayerView,
            this.toolStripSeparator48,
            this.BackgroundShareBtn});
            this.BackgroundToolbar.Location = new System.Drawing.Point(0, 0);
            this.BackgroundToolbar.Name = "BackgroundToolbar";
            this.BackgroundToolbar.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.BackgroundToolbar.Size = new System.Drawing.Size(1288, 32);
            this.BackgroundToolbar.TabIndex = 0;
            this.BackgroundToolbar.Text = "toolStrip1";
            // 
            // BackgroundAddBtn
            // 
            this.BackgroundAddBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BackgroundAddBtn.Image = ((System.Drawing.Image)(resources.GetObject("BackgroundAddBtn.Image")));
            this.BackgroundAddBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BackgroundAddBtn.Name = "BackgroundAddBtn";
            this.BackgroundAddBtn.Size = new System.Drawing.Size(50, 29);
            this.BackgroundAddBtn.Text = "Add";
            this.BackgroundAddBtn.Click += new System.EventHandler(this.BackgroundAddBtn_Click);
            // 
            // BackgroundRemoveBtn
            // 
            this.BackgroundRemoveBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BackgroundRemoveBtn.Image = ((System.Drawing.Image)(resources.GetObject("BackgroundRemoveBtn.Image")));
            this.BackgroundRemoveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BackgroundRemoveBtn.Name = "BackgroundRemoveBtn";
            this.BackgroundRemoveBtn.Size = new System.Drawing.Size(80, 29);
            this.BackgroundRemoveBtn.Text = "Remove";
            this.BackgroundRemoveBtn.Click += new System.EventHandler(this.BackgroundRemoveBtn_Click);
            // 
            // BackgroundEditBtn
            // 
            this.BackgroundEditBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BackgroundEditBtn.Image = ((System.Drawing.Image)(resources.GetObject("BackgroundEditBtn.Image")));
            this.BackgroundEditBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BackgroundEditBtn.Name = "BackgroundEditBtn";
            this.BackgroundEditBtn.Size = new System.Drawing.Size(46, 29);
            this.BackgroundEditBtn.Text = "Edit";
            this.BackgroundEditBtn.Click += new System.EventHandler(this.BackgroundEditBtn_Click);
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new System.Drawing.Size(6, 32);
            // 
            // BackgroundUpBtn
            // 
            this.BackgroundUpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BackgroundUpBtn.Image = ((System.Drawing.Image)(resources.GetObject("BackgroundUpBtn.Image")));
            this.BackgroundUpBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BackgroundUpBtn.Name = "BackgroundUpBtn";
            this.BackgroundUpBtn.Size = new System.Drawing.Size(89, 29);
            this.BackgroundUpBtn.Text = "Move Up";
            this.BackgroundUpBtn.Click += new System.EventHandler(this.BackgroundUpBtn_Click);
            // 
            // BackgroundDownBtn
            // 
            this.BackgroundDownBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BackgroundDownBtn.Image = ((System.Drawing.Image)(resources.GetObject("BackgroundDownBtn.Image")));
            this.BackgroundDownBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BackgroundDownBtn.Name = "BackgroundDownBtn";
            this.BackgroundDownBtn.Size = new System.Drawing.Size(113, 29);
            this.BackgroundDownBtn.Text = "Move Down";
            this.BackgroundDownBtn.Click += new System.EventHandler(this.BackgroundDownBtn_Click);
            // 
            // toolStripSeparator23
            // 
            this.toolStripSeparator23.Name = "toolStripSeparator23";
            this.toolStripSeparator23.Size = new System.Drawing.Size(6, 32);
            // 
            // BackgroundPlayerView
            // 
            this.BackgroundPlayerView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BackgroundPlayerView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BackgroundPlayerViewSelected,
            this.BackgroundPlayerViewAll});
            this.BackgroundPlayerView.Image = ((System.Drawing.Image)(resources.GetObject("BackgroundPlayerView.Image")));
            this.BackgroundPlayerView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BackgroundPlayerView.Name = "BackgroundPlayerView";
            this.BackgroundPlayerView.Size = new System.Drawing.Size(186, 29);
            this.BackgroundPlayerView.Text = "Send to Player View";
            // 
            // BackgroundPlayerViewSelected
            // 
            this.BackgroundPlayerViewSelected.Name = "BackgroundPlayerViewSelected";
            this.BackgroundPlayerViewSelected.Size = new System.Drawing.Size(203, 30);
            this.BackgroundPlayerViewSelected.Text = "Selected Item";
            this.BackgroundPlayerViewSelected.Click += new System.EventHandler(this.BackgroundPlayerViewSelected_Click);
            // 
            // BackgroundPlayerViewAll
            // 
            this.BackgroundPlayerViewAll.Name = "BackgroundPlayerViewAll";
            this.BackgroundPlayerViewAll.Size = new System.Drawing.Size(203, 30);
            this.BackgroundPlayerViewAll.Text = "All Items";
            this.BackgroundPlayerViewAll.Click += new System.EventHandler(this.BackgroundPlayerViewAll_Click);
            // 
            // toolStripSeparator48
            // 
            this.toolStripSeparator48.Name = "toolStripSeparator48";
            this.toolStripSeparator48.Size = new System.Drawing.Size(6, 32);
            // 
            // BackgroundShareBtn
            // 
            this.BackgroundShareBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BackgroundShareBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BackgroundShareExport,
            this.BackgroundShareImport,
            this.toolStripMenuItem10,
            this.BackgroundSharePublish});
            this.BackgroundShareBtn.Image = ((System.Drawing.Image)(resources.GetObject("BackgroundShareBtn.Image")));
            this.BackgroundShareBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BackgroundShareBtn.Name = "BackgroundShareBtn";
            this.BackgroundShareBtn.Size = new System.Drawing.Size(74, 29);
            this.BackgroundShareBtn.Text = "Share";
            // 
            // BackgroundShareExport
            // 
            this.BackgroundShareExport.Name = "BackgroundShareExport";
            this.BackgroundShareExport.Size = new System.Drawing.Size(165, 30);
            this.BackgroundShareExport.Text = "Export...";
            this.BackgroundShareExport.Click += new System.EventHandler(this.BackgroundShareExport_Click);
            // 
            // BackgroundShareImport
            // 
            this.BackgroundShareImport.Name = "BackgroundShareImport";
            this.BackgroundShareImport.Size = new System.Drawing.Size(165, 30);
            this.BackgroundShareImport.Text = "Import...";
            this.BackgroundShareImport.Click += new System.EventHandler(this.BackgroundShareImport_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(162, 6);
            // 
            // BackgroundSharePublish
            // 
            this.BackgroundSharePublish.Name = "BackgroundSharePublish";
            this.BackgroundSharePublish.Size = new System.Drawing.Size(165, 30);
            this.BackgroundSharePublish.Text = "Publish...";
            this.BackgroundSharePublish.Click += new System.EventHandler(this.BackgroundSharePublish_Click);
            // 
            // EncyclopediaPage
            // 
            this.EncyclopediaPage.Controls.Add(this.EncyclopediaSplitter);
            this.EncyclopediaPage.Controls.Add(this.EncyclopediaToolbar);
            this.EncyclopediaPage.Location = new System.Drawing.Point(4, 29);
            this.EncyclopediaPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.EncyclopediaPage.Name = "EncyclopediaPage";
            this.EncyclopediaPage.Size = new System.Drawing.Size(1288, 638);
            this.EncyclopediaPage.TabIndex = 1;
            this.EncyclopediaPage.Text = "Encyclopedia";
            this.EncyclopediaPage.UseVisualStyleBackColor = true;
            // 
            // EncyclopediaSplitter
            // 
            this.EncyclopediaSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EncyclopediaSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.EncyclopediaSplitter.Location = new System.Drawing.Point(0, 32);
            this.EncyclopediaSplitter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.EncyclopediaSplitter.Name = "EncyclopediaSplitter";
            // 
            // EncyclopediaSplitter.Panel1
            // 
            this.EncyclopediaSplitter.Panel1.Controls.Add(this.EntryList);
            // 
            // EncyclopediaSplitter.Panel2
            // 
            this.EncyclopediaSplitter.Panel2.Controls.Add(this.EncyclopediaEntrySplitter);
            this.EncyclopediaSplitter.Size = new System.Drawing.Size(1288, 606);
            this.EncyclopediaSplitter.SplitterDistance = 255;
            this.EncyclopediaSplitter.SplitterWidth = 6;
            this.EncyclopediaSplitter.TabIndex = 3;
            // 
            // EntryList
            // 
            this.EntryList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.EntryHdr});
            this.EntryList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EntryList.FullRowSelect = true;
            this.EntryList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.EntryList.HideSelection = false;
            this.EntryList.Location = new System.Drawing.Point(0, 0);
            this.EntryList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.EntryList.MultiSelect = false;
            this.EntryList.Name = "EntryList";
            this.EntryList.Size = new System.Drawing.Size(255, 606);
            this.EntryList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.EntryList.TabIndex = 0;
            this.EntryList.UseCompatibleStateImageBehavior = false;
            this.EntryList.View = System.Windows.Forms.View.Details;
            this.EntryList.SelectedIndexChanged += new System.EventHandler(this.EntryList_SelectedIndexChanged);
            this.EntryList.DoubleClick += new System.EventHandler(this.EncEditBtn_Click);
            // 
            // EntryHdr
            // 
            this.EntryHdr.Text = "Entries";
            this.EntryHdr.Width = 221;
            // 
            // EncyclopediaEntrySplitter
            // 
            this.EncyclopediaEntrySplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EncyclopediaEntrySplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.EncyclopediaEntrySplitter.Location = new System.Drawing.Point(0, 0);
            this.EncyclopediaEntrySplitter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.EncyclopediaEntrySplitter.Name = "EncyclopediaEntrySplitter";
            // 
            // EncyclopediaEntrySplitter.Panel1
            // 
            this.EncyclopediaEntrySplitter.Panel1.Controls.Add(this.EntryPanel);
            // 
            // EncyclopediaEntrySplitter.Panel2
            // 
            this.EncyclopediaEntrySplitter.Panel2.Controls.Add(this.EntryImageList);
            this.EncyclopediaEntrySplitter.Size = new System.Drawing.Size(1027, 606);
            this.EncyclopediaEntrySplitter.SplitterDistance = 893;
            this.EncyclopediaEntrySplitter.SplitterWidth = 6;
            this.EncyclopediaEntrySplitter.TabIndex = 5;
            // 
            // EntryPanel
            // 
            this.EntryPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EntryPanel.Controls.Add(this.EntryDetails);
            this.EntryPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EntryPanel.Location = new System.Drawing.Point(0, 0);
            this.EntryPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.EntryPanel.Name = "EntryPanel";
            this.EntryPanel.Size = new System.Drawing.Size(893, 606);
            this.EntryPanel.TabIndex = 0;
            // 
            // EntryDetails
            // 
            this.EntryDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EntryDetails.IsWebBrowserContextMenuEnabled = false;
            this.EntryDetails.Location = new System.Drawing.Point(0, 0);
            this.EntryDetails.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.EntryDetails.MinimumSize = new System.Drawing.Size(30, 31);
            this.EntryDetails.Name = "EntryDetails";
            this.EntryDetails.ScriptErrorsSuppressed = true;
            this.EntryDetails.Size = new System.Drawing.Size(891, 604);
            this.EntryDetails.TabIndex = 4;
            this.EntryDetails.WebBrowserShortcutsEnabled = false;
            this.EntryDetails.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.EntryDetails_Navigating);
            // 
            // EntryImageList
            // 
            this.EntryImageList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EntryImageList.Location = new System.Drawing.Point(0, 0);
            this.EntryImageList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.EntryImageList.Name = "EntryImageList";
            this.EntryImageList.Size = new System.Drawing.Size(128, 606);
            this.EntryImageList.TabIndex = 0;
            this.EntryImageList.UseCompatibleStateImageBehavior = false;
            this.EntryImageList.DoubleClick += new System.EventHandler(this.EntryImageList_DoubleClick);
            // 
            // EncyclopediaToolbar
            // 
            this.EncyclopediaToolbar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.EncyclopediaToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EncAddBtn,
            this.EncRemoveBtn,
            this.EncEditBtn,
            this.toolStripSeparator15,
            this.EncCutBtn,
            this.EncCopyBtn,
            this.EncPasteBtn,
            this.toolStripSeparator17,
            this.EncPlayerView,
            this.toolStripSeparator40,
            this.EncShareBtn,
            this.toolStripSeparator22,
            this.EncSearchLbl,
            this.EncSearchBox,
            this.EncClearLbl});
            this.EncyclopediaToolbar.Location = new System.Drawing.Point(0, 0);
            this.EncyclopediaToolbar.Name = "EncyclopediaToolbar";
            this.EncyclopediaToolbar.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.EncyclopediaToolbar.Size = new System.Drawing.Size(1288, 32);
            this.EncyclopediaToolbar.TabIndex = 2;
            this.EncyclopediaToolbar.Text = "toolStrip1";
            // 
            // EncAddBtn
            // 
            this.EncAddBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.EncAddBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EncAddEntry,
            this.EncAddGroup});
            this.EncAddBtn.Image = ((System.Drawing.Image)(resources.GetObject("EncAddBtn.Image")));
            this.EncAddBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EncAddBtn.Name = "EncAddBtn";
            this.EncAddBtn.Size = new System.Drawing.Size(64, 29);
            this.EncAddBtn.Text = "Add";
            // 
            // EncAddEntry
            // 
            this.EncAddEntry.Name = "EncAddEntry";
            this.EncAddEntry.Size = new System.Drawing.Size(199, 30);
            this.EncAddEntry.Text = "Add an Entry";
            this.EncAddEntry.Click += new System.EventHandler(this.EncAddEntry_Click);
            // 
            // EncAddGroup
            // 
            this.EncAddGroup.Name = "EncAddGroup";
            this.EncAddGroup.Size = new System.Drawing.Size(199, 30);
            this.EncAddGroup.Text = "Add a Group";
            this.EncAddGroup.Click += new System.EventHandler(this.EncAddGroup_Click);
            // 
            // EncRemoveBtn
            // 
            this.EncRemoveBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.EncRemoveBtn.Image = ((System.Drawing.Image)(resources.GetObject("EncRemoveBtn.Image")));
            this.EncRemoveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EncRemoveBtn.Name = "EncRemoveBtn";
            this.EncRemoveBtn.Size = new System.Drawing.Size(80, 29);
            this.EncRemoveBtn.Text = "Remove";
            this.EncRemoveBtn.Click += new System.EventHandler(this.EncRemoveBtn_Click);
            // 
            // EncEditBtn
            // 
            this.EncEditBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.EncEditBtn.Image = ((System.Drawing.Image)(resources.GetObject("EncEditBtn.Image")));
            this.EncEditBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EncEditBtn.Name = "EncEditBtn";
            this.EncEditBtn.Size = new System.Drawing.Size(46, 29);
            this.EncEditBtn.Text = "Edit";
            this.EncEditBtn.Click += new System.EventHandler(this.EncEditBtn_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 32);
            // 
            // EncCutBtn
            // 
            this.EncCutBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.EncCutBtn.Image = ((System.Drawing.Image)(resources.GetObject("EncCutBtn.Image")));
            this.EncCutBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EncCutBtn.Name = "EncCutBtn";
            this.EncCutBtn.Size = new System.Drawing.Size(43, 29);
            this.EncCutBtn.Text = "Cut";
            this.EncCutBtn.Click += new System.EventHandler(this.EncCutBtn_Click);
            // 
            // EncCopyBtn
            // 
            this.EncCopyBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.EncCopyBtn.Image = ((System.Drawing.Image)(resources.GetObject("EncCopyBtn.Image")));
            this.EncCopyBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EncCopyBtn.Name = "EncCopyBtn";
            this.EncCopyBtn.Size = new System.Drawing.Size(58, 29);
            this.EncCopyBtn.Text = "Copy";
            this.EncCopyBtn.Click += new System.EventHandler(this.EncCopyBtn_Click);
            // 
            // EncPasteBtn
            // 
            this.EncPasteBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.EncPasteBtn.Image = ((System.Drawing.Image)(resources.GetObject("EncPasteBtn.Image")));
            this.EncPasteBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EncPasteBtn.Name = "EncPasteBtn";
            this.EncPasteBtn.Size = new System.Drawing.Size(57, 29);
            this.EncPasteBtn.Text = "Paste";
            this.EncPasteBtn.Click += new System.EventHandler(this.EncPasteBtn_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(6, 32);
            // 
            // EncPlayerView
            // 
            this.EncPlayerView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.EncPlayerView.Image = ((System.Drawing.Image)(resources.GetObject("EncPlayerView.Image")));
            this.EncPlayerView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EncPlayerView.Name = "EncPlayerView";
            this.EncPlayerView.Size = new System.Drawing.Size(172, 29);
            this.EncPlayerView.Text = "Send to Player View";
            this.EncPlayerView.Click += new System.EventHandler(this.EncPlayerView_Click);
            // 
            // toolStripSeparator40
            // 
            this.toolStripSeparator40.Name = "toolStripSeparator40";
            this.toolStripSeparator40.Size = new System.Drawing.Size(6, 32);
            // 
            // EncShareBtn
            // 
            this.EncShareBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.EncShareBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EncShareExport,
            this.EncShareImport,
            this.toolStripMenuItem6,
            this.EncSharePublish});
            this.EncShareBtn.Image = ((System.Drawing.Image)(resources.GetObject("EncShareBtn.Image")));
            this.EncShareBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EncShareBtn.Name = "EncShareBtn";
            this.EncShareBtn.Size = new System.Drawing.Size(74, 29);
            this.EncShareBtn.Text = "Share";
            // 
            // EncShareExport
            // 
            this.EncShareExport.Name = "EncShareExport";
            this.EncShareExport.Size = new System.Drawing.Size(165, 30);
            this.EncShareExport.Text = "Export...";
            this.EncShareExport.Click += new System.EventHandler(this.EncShareExport_Click);
            // 
            // EncShareImport
            // 
            this.EncShareImport.Name = "EncShareImport";
            this.EncShareImport.Size = new System.Drawing.Size(165, 30);
            this.EncShareImport.Text = "Import...";
            this.EncShareImport.Click += new System.EventHandler(this.EncShareImport_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(162, 6);
            // 
            // EncSharePublish
            // 
            this.EncSharePublish.Name = "EncSharePublish";
            this.EncSharePublish.Size = new System.Drawing.Size(165, 30);
            this.EncSharePublish.Text = "Publish...";
            this.EncSharePublish.Click += new System.EventHandler(this.EncSharePublish_Click);
            // 
            // toolStripSeparator22
            // 
            this.toolStripSeparator22.Name = "toolStripSeparator22";
            this.toolStripSeparator22.Size = new System.Drawing.Size(6, 32);
            // 
            // EncSearchLbl
            // 
            this.EncSearchLbl.Name = "EncSearchLbl";
            this.EncSearchLbl.Size = new System.Drawing.Size(68, 29);
            this.EncSearchLbl.Text = "Search:";
            // 
            // EncSearchBox
            // 
            this.EncSearchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EncSearchBox.Name = "EncSearchBox";
            this.EncSearchBox.Size = new System.Drawing.Size(150, 32);
            this.EncSearchBox.TextChanged += new System.EventHandler(this.EncSearchBox_TextChanged);
            // 
            // EncClearLbl
            // 
            this.EncClearLbl.IsLink = true;
            this.EncClearLbl.Name = "EncClearLbl";
            this.EncClearLbl.Size = new System.Drawing.Size(51, 29);
            this.EncClearLbl.Text = "Clear";
            this.EncClearLbl.Click += new System.EventHandler(this.EncClearLbl_Click);
            // 
            // RulesPage
            // 
            this.RulesPage.Controls.Add(this.RulesSplitter);
            this.RulesPage.Location = new System.Drawing.Point(4, 29);
            this.RulesPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RulesPage.Name = "RulesPage";
            this.RulesPage.Size = new System.Drawing.Size(1288, 638);
            this.RulesPage.TabIndex = 5;
            this.RulesPage.Text = "Campaign Rules";
            this.RulesPage.UseVisualStyleBackColor = true;
            // 
            // RulesSplitter
            // 
            this.RulesSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RulesSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.RulesSplitter.Location = new System.Drawing.Point(0, 0);
            this.RulesSplitter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RulesSplitter.Name = "RulesSplitter";
            // 
            // RulesSplitter.Panel1
            // 
            this.RulesSplitter.Panel1.Controls.Add(this.RulesList);
            this.RulesSplitter.Panel1.Controls.Add(this.RulesToolbar);
            // 
            // RulesSplitter.Panel2
            // 
            this.RulesSplitter.Panel2.Controls.Add(this.RulesBrowserPanel);
            this.RulesSplitter.Panel2.Controls.Add(this.EncEntryToolbar);
            this.RulesSplitter.Size = new System.Drawing.Size(1288, 638);
            this.RulesSplitter.SplitterDistance = 231;
            this.RulesSplitter.SplitterWidth = 6;
            this.RulesSplitter.TabIndex = 1;
            // 
            // RulesList
            // 
            this.RulesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.RulesHdr});
            this.RulesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RulesList.FullRowSelect = true;
            listViewGroup1.Header = "Races";
            listViewGroup1.Name = "listViewGroup1";
            listViewGroup2.Header = "Classes";
            listViewGroup2.Name = "listViewGroup9";
            listViewGroup3.Header = "Themes";
            listViewGroup3.Name = "listViewGroup14";
            listViewGroup4.Header = "Paragon Paths";
            listViewGroup4.Name = "listViewGroup2";
            listViewGroup5.Header = "Epic Destinies";
            listViewGroup5.Name = "listViewGroup3";
            listViewGroup6.Header = "Backgrounds";
            listViewGroup6.Name = "listViewGroup4";
            listViewGroup7.Header = "Feats (heroic tier)";
            listViewGroup7.Name = "listViewGroup5";
            listViewGroup8.Header = "Feats (paragon tier)";
            listViewGroup8.Name = "listViewGroup6";
            listViewGroup9.Header = "Feats (epic tier)";
            listViewGroup9.Name = "listViewGroup7";
            listViewGroup10.Header = "Weapons";
            listViewGroup10.Name = "listViewGroup10";
            listViewGroup11.Header = "Rituals";
            listViewGroup11.Name = "listViewGroup8";
            listViewGroup12.Header = "Creature Lore";
            listViewGroup12.Name = "listViewGroup11";
            listViewGroup13.Header = "Diseases";
            listViewGroup13.Name = "listViewGroup12";
            listViewGroup14.Header = "Poisons";
            listViewGroup14.Name = "listViewGroup13";
            this.RulesList.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4,
            listViewGroup5,
            listViewGroup6,
            listViewGroup7,
            listViewGroup8,
            listViewGroup9,
            listViewGroup10,
            listViewGroup11,
            listViewGroup12,
            listViewGroup13,
            listViewGroup14});
            this.RulesList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.RulesList.HideSelection = false;
            this.RulesList.Location = new System.Drawing.Point(0, 32);
            this.RulesList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RulesList.MultiSelect = false;
            this.RulesList.Name = "RulesList";
            this.RulesList.Size = new System.Drawing.Size(231, 606);
            this.RulesList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.RulesList.TabIndex = 1;
            this.RulesList.UseCompatibleStateImageBehavior = false;
            this.RulesList.View = System.Windows.Forms.View.Details;
            this.RulesList.SelectedIndexChanged += new System.EventHandler(this.RulesList_SelectedIndexChanged);
            this.RulesList.DoubleClick += new System.EventHandler(this.RulesEditBtn_Click);
            // 
            // RulesHdr
            // 
            this.RulesHdr.Text = "Rules Elements";
            this.RulesHdr.Width = 193;
            // 
            // RulesToolbar
            // 
            this.RulesToolbar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.RulesToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RulesAddBtn,
            this.toolStripSeparator33,
            this.RulesShareBtn});
            this.RulesToolbar.Location = new System.Drawing.Point(0, 0);
            this.RulesToolbar.Name = "RulesToolbar";
            this.RulesToolbar.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.RulesToolbar.Size = new System.Drawing.Size(231, 32);
            this.RulesToolbar.TabIndex = 0;
            this.RulesToolbar.Text = "toolStrip1";
            // 
            // RulesAddBtn
            // 
            this.RulesAddBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.RulesAddBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddRace,
            this.toolStripSeparator31,
            this.AddClass,
            this.AddTheme,
            this.AddParagonPath,
            this.AddEpicDestiny,
            this.toolStripSeparator32,
            this.AddBackground,
            this.AddFeat,
            this.AddWeapon,
            this.AddRitual,
            this.toolStripSeparator39,
            this.AddCreatureLore,
            this.AddDisease,
            this.AddPoison});
            this.RulesAddBtn.Image = ((System.Drawing.Image)(resources.GetObject("RulesAddBtn.Image")));
            this.RulesAddBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RulesAddBtn.Name = "RulesAddBtn";
            this.RulesAddBtn.Size = new System.Drawing.Size(64, 29);
            this.RulesAddBtn.Text = "Add";
            // 
            // AddRace
            // 
            this.AddRace.Name = "AddRace";
            this.AddRace.Size = new System.Drawing.Size(201, 30);
            this.AddRace.Text = "Race";
            this.AddRace.Click += new System.EventHandler(this.AddRace_Click);
            // 
            // toolStripSeparator31
            // 
            this.toolStripSeparator31.Name = "toolStripSeparator31";
            this.toolStripSeparator31.Size = new System.Drawing.Size(198, 6);
            // 
            // AddClass
            // 
            this.AddClass.Name = "AddClass";
            this.AddClass.Size = new System.Drawing.Size(201, 30);
            this.AddClass.Text = "Class";
            this.AddClass.Click += new System.EventHandler(this.AddClass_Click);
            // 
            // AddTheme
            // 
            this.AddTheme.Name = "AddTheme";
            this.AddTheme.Size = new System.Drawing.Size(201, 30);
            this.AddTheme.Text = "Theme";
            this.AddTheme.Click += new System.EventHandler(this.AddTheme_Click);
            // 
            // AddParagonPath
            // 
            this.AddParagonPath.Name = "AddParagonPath";
            this.AddParagonPath.Size = new System.Drawing.Size(201, 30);
            this.AddParagonPath.Text = "Paragon Path";
            this.AddParagonPath.Click += new System.EventHandler(this.AddParagonPath_Click);
            // 
            // AddEpicDestiny
            // 
            this.AddEpicDestiny.Name = "AddEpicDestiny";
            this.AddEpicDestiny.Size = new System.Drawing.Size(201, 30);
            this.AddEpicDestiny.Text = "Epic Destiny";
            this.AddEpicDestiny.Click += new System.EventHandler(this.AddEpicDestiny_Click);
            // 
            // toolStripSeparator32
            // 
            this.toolStripSeparator32.Name = "toolStripSeparator32";
            this.toolStripSeparator32.Size = new System.Drawing.Size(198, 6);
            // 
            // AddBackground
            // 
            this.AddBackground.Name = "AddBackground";
            this.AddBackground.Size = new System.Drawing.Size(201, 30);
            this.AddBackground.Text = "Background";
            this.AddBackground.Click += new System.EventHandler(this.AddBackground_Click);
            // 
            // AddFeat
            // 
            this.AddFeat.Name = "AddFeat";
            this.AddFeat.Size = new System.Drawing.Size(201, 30);
            this.AddFeat.Text = "Feat";
            this.AddFeat.Click += new System.EventHandler(this.AddFeat_Click);
            // 
            // AddWeapon
            // 
            this.AddWeapon.Name = "AddWeapon";
            this.AddWeapon.Size = new System.Drawing.Size(201, 30);
            this.AddWeapon.Text = "Weapon";
            this.AddWeapon.Click += new System.EventHandler(this.AddWeapon_Click);
            // 
            // AddRitual
            // 
            this.AddRitual.Name = "AddRitual";
            this.AddRitual.Size = new System.Drawing.Size(201, 30);
            this.AddRitual.Text = "Ritual";
            this.AddRitual.Click += new System.EventHandler(this.AddRitual_Click);
            // 
            // toolStripSeparator39
            // 
            this.toolStripSeparator39.Name = "toolStripSeparator39";
            this.toolStripSeparator39.Size = new System.Drawing.Size(198, 6);
            // 
            // AddCreatureLore
            // 
            this.AddCreatureLore.Name = "AddCreatureLore";
            this.AddCreatureLore.Size = new System.Drawing.Size(201, 30);
            this.AddCreatureLore.Text = "Creature Lore";
            this.AddCreatureLore.Click += new System.EventHandler(this.AddCreatureLore_Click);
            // 
            // AddDisease
            // 
            this.AddDisease.Name = "AddDisease";
            this.AddDisease.Size = new System.Drawing.Size(201, 30);
            this.AddDisease.Text = "Disease";
            this.AddDisease.Click += new System.EventHandler(this.AddDisease_Click);
            // 
            // AddPoison
            // 
            this.AddPoison.Name = "AddPoison";
            this.AddPoison.Size = new System.Drawing.Size(201, 30);
            this.AddPoison.Text = "Poison";
            this.AddPoison.Click += new System.EventHandler(this.AddPoison_Click);
            // 
            // toolStripSeparator33
            // 
            this.toolStripSeparator33.Name = "toolStripSeparator33";
            this.toolStripSeparator33.Size = new System.Drawing.Size(6, 32);
            // 
            // RulesShareBtn
            // 
            this.RulesShareBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.RulesShareBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RulesShareExport,
            this.RulesShareImport,
            this.toolStripMenuItem9,
            this.RulesSharePublish});
            this.RulesShareBtn.Image = ((System.Drawing.Image)(resources.GetObject("RulesShareBtn.Image")));
            this.RulesShareBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RulesShareBtn.Name = "RulesShareBtn";
            this.RulesShareBtn.Size = new System.Drawing.Size(74, 29);
            this.RulesShareBtn.Text = "Share";
            // 
            // RulesShareExport
            // 
            this.RulesShareExport.Name = "RulesShareExport";
            this.RulesShareExport.Size = new System.Drawing.Size(165, 30);
            this.RulesShareExport.Text = "Export...";
            this.RulesShareExport.Click += new System.EventHandler(this.RulesShareExport_Click);
            // 
            // RulesShareImport
            // 
            this.RulesShareImport.Name = "RulesShareImport";
            this.RulesShareImport.Size = new System.Drawing.Size(165, 30);
            this.RulesShareImport.Text = "Import...";
            this.RulesShareImport.Click += new System.EventHandler(this.RulesShareImport_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(162, 6);
            // 
            // RulesSharePublish
            // 
            this.RulesSharePublish.Name = "RulesSharePublish";
            this.RulesSharePublish.Size = new System.Drawing.Size(165, 30);
            this.RulesSharePublish.Text = "Publish...";
            this.RulesSharePublish.Click += new System.EventHandler(this.RulesSharePublish_Click);
            // 
            // RulesBrowserPanel
            // 
            this.RulesBrowserPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RulesBrowserPanel.Controls.Add(this.RulesBrowser);
            this.RulesBrowserPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RulesBrowserPanel.Location = new System.Drawing.Point(0, 32);
            this.RulesBrowserPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RulesBrowserPanel.Name = "RulesBrowserPanel";
            this.RulesBrowserPanel.Size = new System.Drawing.Size(1051, 606);
            this.RulesBrowserPanel.TabIndex = 0;
            // 
            // RulesBrowser
            // 
            this.RulesBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RulesBrowser.IsWebBrowserContextMenuEnabled = false;
            this.RulesBrowser.Location = new System.Drawing.Point(0, 0);
            this.RulesBrowser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RulesBrowser.MinimumSize = new System.Drawing.Size(30, 31);
            this.RulesBrowser.Name = "RulesBrowser";
            this.RulesBrowser.ScriptErrorsSuppressed = true;
            this.RulesBrowser.Size = new System.Drawing.Size(1049, 604);
            this.RulesBrowser.TabIndex = 1;
            this.RulesBrowser.WebBrowserShortcutsEnabled = false;
            // 
            // EncEntryToolbar
            // 
            this.EncEntryToolbar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.EncEntryToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RulesRemoveBtn,
            this.RulesEditBtn,
            this.toolStripSeparator43,
            this.RuleEncyclopediaBtn,
            this.toolStripSeparator36,
            this.RulesPlayerViewBtn});
            this.EncEntryToolbar.Location = new System.Drawing.Point(0, 0);
            this.EncEntryToolbar.Name = "EncEntryToolbar";
            this.EncEntryToolbar.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.EncEntryToolbar.Size = new System.Drawing.Size(1051, 32);
            this.EncEntryToolbar.TabIndex = 2;
            this.EncEntryToolbar.Text = "toolStrip1";
            // 
            // RulesRemoveBtn
            // 
            this.RulesRemoveBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.RulesRemoveBtn.Image = ((System.Drawing.Image)(resources.GetObject("RulesRemoveBtn.Image")));
            this.RulesRemoveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RulesRemoveBtn.Name = "RulesRemoveBtn";
            this.RulesRemoveBtn.Size = new System.Drawing.Size(80, 29);
            this.RulesRemoveBtn.Text = "Remove";
            this.RulesRemoveBtn.Click += new System.EventHandler(this.RulesRemoveBtn_Click);
            // 
            // RulesEditBtn
            // 
            this.RulesEditBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.RulesEditBtn.Image = ((System.Drawing.Image)(resources.GetObject("RulesEditBtn.Image")));
            this.RulesEditBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RulesEditBtn.Name = "RulesEditBtn";
            this.RulesEditBtn.Size = new System.Drawing.Size(46, 29);
            this.RulesEditBtn.Text = "Edit";
            this.RulesEditBtn.Click += new System.EventHandler(this.RulesEditBtn_Click);
            // 
            // toolStripSeparator43
            // 
            this.toolStripSeparator43.Name = "toolStripSeparator43";
            this.toolStripSeparator43.Size = new System.Drawing.Size(6, 32);
            // 
            // RuleEncyclopediaBtn
            // 
            this.RuleEncyclopediaBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.RuleEncyclopediaBtn.Image = ((System.Drawing.Image)(resources.GetObject("RuleEncyclopediaBtn.Image")));
            this.RuleEncyclopediaBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RuleEncyclopediaBtn.Name = "RuleEncyclopediaBtn";
            this.RuleEncyclopediaBtn.Size = new System.Drawing.Size(164, 29);
            this.RuleEncyclopediaBtn.Text = "Encyclopedia Entry";
            this.RuleEncyclopediaBtn.Click += new System.EventHandler(this.RuleEncyclopediaBtn_Click);
            // 
            // toolStripSeparator36
            // 
            this.toolStripSeparator36.Name = "toolStripSeparator36";
            this.toolStripSeparator36.Size = new System.Drawing.Size(6, 32);
            // 
            // RulesPlayerViewBtn
            // 
            this.RulesPlayerViewBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.RulesPlayerViewBtn.Image = ((System.Drawing.Image)(resources.GetObject("RulesPlayerViewBtn.Image")));
            this.RulesPlayerViewBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RulesPlayerViewBtn.Name = "RulesPlayerViewBtn";
            this.RulesPlayerViewBtn.Size = new System.Drawing.Size(172, 29);
            this.RulesPlayerViewBtn.Text = "Send to Player View";
            this.RulesPlayerViewBtn.Click += new System.EventHandler(this.RulesPlayerViewBtn_Click);
            // 
            // AttachmentsPage
            // 
            this.AttachmentsPage.Controls.Add(this.AttachmentList);
            this.AttachmentsPage.Controls.Add(this.AttachmentToolbar);
            this.AttachmentsPage.Location = new System.Drawing.Point(4, 29);
            this.AttachmentsPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AttachmentsPage.Name = "AttachmentsPage";
            this.AttachmentsPage.Size = new System.Drawing.Size(1288, 638);
            this.AttachmentsPage.TabIndex = 3;
            this.AttachmentsPage.Text = "Attachments";
            this.AttachmentsPage.UseVisualStyleBackColor = true;
            // 
            // AttachmentList
            // 
            this.AttachmentList.AllowDrop = true;
            this.AttachmentList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.AttachmentHdr,
            this.AttachmentSizeHdr});
            this.AttachmentList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AttachmentList.FullRowSelect = true;
            this.AttachmentList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.AttachmentList.HideSelection = false;
            this.AttachmentList.Location = new System.Drawing.Point(0, 32);
            this.AttachmentList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AttachmentList.Name = "AttachmentList";
            this.AttachmentList.Size = new System.Drawing.Size(1288, 606);
            this.AttachmentList.TabIndex = 1;
            this.AttachmentList.UseCompatibleStateImageBehavior = false;
            this.AttachmentList.View = System.Windows.Forms.View.Details;
            this.AttachmentList.DragDrop += new System.Windows.Forms.DragEventHandler(this.AttachmentList_DragDrop);
            this.AttachmentList.DragOver += new System.Windows.Forms.DragEventHandler(this.AttachmentList_DragOver);
            this.AttachmentList.DoubleClick += new System.EventHandler(this.AttachmentExtractAndRun_Click);
            // 
            // AttachmentHdr
            // 
            this.AttachmentHdr.Text = "Attachment";
            this.AttachmentHdr.Width = 500;
            // 
            // AttachmentSizeHdr
            // 
            this.AttachmentSizeHdr.Text = "Size";
            this.AttachmentSizeHdr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AttachmentSizeHdr.Width = 100;
            // 
            // AttachmentToolbar
            // 
            this.AttachmentToolbar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.AttachmentToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AttachmentImportBtn,
            this.AttachmentRemoveBtn,
            this.toolStripSeparator19,
            this.AttachmentExtract,
            this.toolStripSeparator24,
            this.AttachmentPlayerView});
            this.AttachmentToolbar.Location = new System.Drawing.Point(0, 0);
            this.AttachmentToolbar.Name = "AttachmentToolbar";
            this.AttachmentToolbar.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.AttachmentToolbar.Size = new System.Drawing.Size(1288, 32);
            this.AttachmentToolbar.TabIndex = 0;
            this.AttachmentToolbar.Text = "toolStrip1";
            // 
            // AttachmentImportBtn
            // 
            this.AttachmentImportBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.AttachmentImportBtn.Image = ((System.Drawing.Image)(resources.GetObject("AttachmentImportBtn.Image")));
            this.AttachmentImportBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AttachmentImportBtn.Name = "AttachmentImportBtn";
            this.AttachmentImportBtn.Size = new System.Drawing.Size(71, 29);
            this.AttachmentImportBtn.Text = "Import";
            this.AttachmentImportBtn.Click += new System.EventHandler(this.AttachmentImportBtn_Click);
            // 
            // AttachmentRemoveBtn
            // 
            this.AttachmentRemoveBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.AttachmentRemoveBtn.Image = ((System.Drawing.Image)(resources.GetObject("AttachmentRemoveBtn.Image")));
            this.AttachmentRemoveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AttachmentRemoveBtn.Name = "AttachmentRemoveBtn";
            this.AttachmentRemoveBtn.Size = new System.Drawing.Size(80, 29);
            this.AttachmentRemoveBtn.Text = "Remove";
            this.AttachmentRemoveBtn.Click += new System.EventHandler(this.AttachmentRemoveBtn_Click);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(6, 32);
            // 
            // AttachmentExtract
            // 
            this.AttachmentExtract.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.AttachmentExtract.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AttachmentExtractSimple,
            this.AttachmentExtractAndRun});
            this.AttachmentExtract.Image = ((System.Drawing.Image)(resources.GetObject("AttachmentExtract.Image")));
            this.AttachmentExtract.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AttachmentExtract.Name = "AttachmentExtract";
            this.AttachmentExtract.Size = new System.Drawing.Size(82, 29);
            this.AttachmentExtract.Text = "Extract";
            // 
            // AttachmentExtractSimple
            // 
            this.AttachmentExtractSimple.Name = "AttachmentExtractSimple";
            this.AttachmentExtractSimple.Size = new System.Drawing.Size(326, 30);
            this.AttachmentExtractSimple.Text = "Extract to Desktop";
            this.AttachmentExtractSimple.Click += new System.EventHandler(this.AttachmentExtractSimple_Click);
            // 
            // AttachmentExtractAndRun
            // 
            this.AttachmentExtractAndRun.Name = "AttachmentExtractAndRun";
            this.AttachmentExtractAndRun.Size = new System.Drawing.Size(326, 30);
            this.AttachmentExtractAndRun.Text = "Extract to Desktop and Open";
            this.AttachmentExtractAndRun.Click += new System.EventHandler(this.AttachmentExtractAndRun_Click);
            // 
            // toolStripSeparator24
            // 
            this.toolStripSeparator24.Name = "toolStripSeparator24";
            this.toolStripSeparator24.Size = new System.Drawing.Size(6, 32);
            // 
            // AttachmentPlayerView
            // 
            this.AttachmentPlayerView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.AttachmentPlayerView.Image = ((System.Drawing.Image)(resources.GetObject("AttachmentPlayerView.Image")));
            this.AttachmentPlayerView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AttachmentPlayerView.Name = "AttachmentPlayerView";
            this.AttachmentPlayerView.Size = new System.Drawing.Size(172, 29);
            this.AttachmentPlayerView.Text = "Send to Player View";
            this.AttachmentPlayerView.Click += new System.EventHandler(this.AttachmentSendBtn_Click);
            // 
            // JotterPage
            // 
            this.JotterPage.Controls.Add(this.JotterSplitter);
            this.JotterPage.Controls.Add(this.JotterToolbar);
            this.JotterPage.Location = new System.Drawing.Point(4, 29);
            this.JotterPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.JotterPage.Name = "JotterPage";
            this.JotterPage.Size = new System.Drawing.Size(1288, 638);
            this.JotterPage.TabIndex = 2;
            this.JotterPage.Text = "Jotter";
            this.JotterPage.UseVisualStyleBackColor = true;
            // 
            // JotterSplitter
            // 
            this.JotterSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.JotterSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.JotterSplitter.Location = new System.Drawing.Point(0, 32);
            this.JotterSplitter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.JotterSplitter.Name = "JotterSplitter";
            // 
            // JotterSplitter.Panel1
            // 
            this.JotterSplitter.Panel1.Controls.Add(this.NoteList);
            // 
            // JotterSplitter.Panel2
            // 
            this.JotterSplitter.Panel2.Controls.Add(this.NoteBox);
            this.JotterSplitter.Size = new System.Drawing.Size(1288, 606);
            this.JotterSplitter.SplitterDistance = 180;
            this.JotterSplitter.SplitterWidth = 6;
            this.JotterSplitter.TabIndex = 1;
            // 
            // NoteList
            // 
            this.NoteList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NoteHdr});
            this.NoteList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NoteList.FullRowSelect = true;
            listViewGroup15.Header = "Issues";
            listViewGroup15.Name = "IssueGroup";
            listViewGroup16.Header = "Information";
            listViewGroup16.Name = "InfoGroup";
            listViewGroup17.Header = "Notes";
            listViewGroup17.Name = "NoteGroup";
            this.NoteList.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup15,
            listViewGroup16,
            listViewGroup17});
            this.NoteList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.NoteList.HideSelection = false;
            this.NoteList.Location = new System.Drawing.Point(0, 0);
            this.NoteList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NoteList.MultiSelect = false;
            this.NoteList.Name = "NoteList";
            this.NoteList.Size = new System.Drawing.Size(180, 606);
            this.NoteList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.NoteList.TabIndex = 0;
            this.NoteList.UseCompatibleStateImageBehavior = false;
            this.NoteList.View = System.Windows.Forms.View.Details;
            this.NoteList.SelectedIndexChanged += new System.EventHandler(this.NoteList_SelectedIndexChanged);
            // 
            // NoteHdr
            // 
            this.NoteHdr.Text = "Notes";
            this.NoteHdr.Width = 150;
            // 
            // NoteBox
            // 
            this.NoteBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NoteBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NoteBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoteBox.Location = new System.Drawing.Point(0, 0);
            this.NoteBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NoteBox.Multiline = true;
            this.NoteBox.Name = "NoteBox";
            this.NoteBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NoteBox.Size = new System.Drawing.Size(1102, 606);
            this.NoteBox.TabIndex = 0;
            this.NoteBox.TextChanged += new System.EventHandler(this.NoteBox_TextChanged);
            // 
            // JotterToolbar
            // 
            this.JotterToolbar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.JotterToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NoteAddBtn,
            this.NoteRemoveBtn,
            this.toolStripSeparator16,
            this.NoteCategoryBtn,
            this.toolStripSeparator38,
            this.NoteCutBtn,
            this.NoteCopyBtn,
            this.NotePasteBtn,
            this.toolStripSeparator18,
            this.NoteSearchLbl,
            this.NoteSearchBox,
            this.NoteClearLbl});
            this.JotterToolbar.Location = new System.Drawing.Point(0, 0);
            this.JotterToolbar.Name = "JotterToolbar";
            this.JotterToolbar.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.JotterToolbar.Size = new System.Drawing.Size(1288, 32);
            this.JotterToolbar.TabIndex = 0;
            this.JotterToolbar.Text = "toolStrip1";
            // 
            // NoteAddBtn
            // 
            this.NoteAddBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.NoteAddBtn.Image = ((System.Drawing.Image)(resources.GetObject("NoteAddBtn.Image")));
            this.NoteAddBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NoteAddBtn.Name = "NoteAddBtn";
            this.NoteAddBtn.Size = new System.Drawing.Size(94, 29);
            this.NoteAddBtn.Text = "Add Note";
            this.NoteAddBtn.Click += new System.EventHandler(this.NoteAddBtn_Click);
            // 
            // NoteRemoveBtn
            // 
            this.NoteRemoveBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.NoteRemoveBtn.Image = ((System.Drawing.Image)(resources.GetObject("NoteRemoveBtn.Image")));
            this.NoteRemoveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NoteRemoveBtn.Name = "NoteRemoveBtn";
            this.NoteRemoveBtn.Size = new System.Drawing.Size(124, 29);
            this.NoteRemoveBtn.Text = "Remove Note";
            this.NoteRemoveBtn.Click += new System.EventHandler(this.NoteRemoveBtn_Click);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(6, 32);
            // 
            // NoteCategoryBtn
            // 
            this.NoteCategoryBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.NoteCategoryBtn.Image = ((System.Drawing.Image)(resources.GetObject("NoteCategoryBtn.Image")));
            this.NoteCategoryBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NoteCategoryBtn.Name = "NoteCategoryBtn";
            this.NoteCategoryBtn.Size = new System.Drawing.Size(118, 29);
            this.NoteCategoryBtn.Text = "Set Category";
            this.NoteCategoryBtn.Click += new System.EventHandler(this.NoteCategoryBtn_Click);
            // 
            // toolStripSeparator38
            // 
            this.toolStripSeparator38.Name = "toolStripSeparator38";
            this.toolStripSeparator38.Size = new System.Drawing.Size(6, 32);
            // 
            // NoteCutBtn
            // 
            this.NoteCutBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.NoteCutBtn.Image = ((System.Drawing.Image)(resources.GetObject("NoteCutBtn.Image")));
            this.NoteCutBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NoteCutBtn.Name = "NoteCutBtn";
            this.NoteCutBtn.Size = new System.Drawing.Size(43, 29);
            this.NoteCutBtn.Text = "Cut";
            this.NoteCutBtn.Click += new System.EventHandler(this.NoteCutBtn_Click);
            // 
            // NoteCopyBtn
            // 
            this.NoteCopyBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.NoteCopyBtn.Image = ((System.Drawing.Image)(resources.GetObject("NoteCopyBtn.Image")));
            this.NoteCopyBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NoteCopyBtn.Name = "NoteCopyBtn";
            this.NoteCopyBtn.Size = new System.Drawing.Size(58, 29);
            this.NoteCopyBtn.Text = "Copy";
            this.NoteCopyBtn.Click += new System.EventHandler(this.NoteCopyBtn_Click);
            // 
            // NotePasteBtn
            // 
            this.NotePasteBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.NotePasteBtn.Image = ((System.Drawing.Image)(resources.GetObject("NotePasteBtn.Image")));
            this.NotePasteBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NotePasteBtn.Name = "NotePasteBtn";
            this.NotePasteBtn.Size = new System.Drawing.Size(57, 29);
            this.NotePasteBtn.Text = "Paste";
            this.NotePasteBtn.Click += new System.EventHandler(this.NotePasteBtn_Click);
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(6, 32);
            // 
            // NoteSearchLbl
            // 
            this.NoteSearchLbl.Name = "NoteSearchLbl";
            this.NoteSearchLbl.Size = new System.Drawing.Size(68, 29);
            this.NoteSearchLbl.Text = "Search:";
            // 
            // NoteSearchBox
            // 
            this.NoteSearchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NoteSearchBox.Name = "NoteSearchBox";
            this.NoteSearchBox.Size = new System.Drawing.Size(224, 32);
            this.NoteSearchBox.TextChanged += new System.EventHandler(this.NoteSearchBox_TextChanged);
            // 
            // NoteClearLbl
            // 
            this.NoteClearLbl.IsLink = true;
            this.NoteClearLbl.Name = "NoteClearLbl";
            this.NoteClearLbl.Size = new System.Drawing.Size(51, 29);
            this.NoteClearLbl.Text = "Clear";
            this.NoteClearLbl.Click += new System.EventHandler(this.NoteClearLbl_Click);
            // 
            // ReferencePage
            // 
            this.ReferencePage.Controls.Add(this.ReferenceSplitter);
            this.ReferencePage.Location = new System.Drawing.Point(4, 29);
            this.ReferencePage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ReferencePage.Name = "ReferencePage";
            this.ReferencePage.Size = new System.Drawing.Size(1288, 638);
            this.ReferencePage.TabIndex = 6;
            this.ReferencePage.Text = "In-Session Reference";
            this.ReferencePage.UseVisualStyleBackColor = true;
            // 
            // ReferenceSplitter
            // 
            this.ReferenceSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReferenceSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.ReferenceSplitter.Location = new System.Drawing.Point(0, 0);
            this.ReferenceSplitter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ReferenceSplitter.Name = "ReferenceSplitter";
            // 
            // ReferenceSplitter.Panel1
            // 
            this.ReferenceSplitter.Panel1.Controls.Add(this.ReferencePages);
            // 
            // ReferenceSplitter.Panel2
            // 
            this.ReferenceSplitter.Panel2.Controls.Add(this.InfoPanel);
            this.ReferenceSplitter.Panel2.Controls.Add(this.ReferenceToolbar);
            this.ReferenceSplitter.Size = new System.Drawing.Size(1288, 638);
            this.ReferenceSplitter.SplitterDistance = 1024;
            this.ReferenceSplitter.SplitterWidth = 6;
            this.ReferenceSplitter.TabIndex = 1;
            // 
            // ReferencePages
            // 
            this.ReferencePages.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.ReferencePages.Controls.Add(this.PartyPage);
            this.ReferencePages.Controls.Add(this.ToolsPage);
            this.ReferencePages.Controls.Add(this.CompendiumPage);
            this.ReferencePages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReferencePages.Location = new System.Drawing.Point(0, 0);
            this.ReferencePages.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ReferencePages.Multiline = true;
            this.ReferencePages.Name = "ReferencePages";
            this.ReferencePages.SelectedIndex = 0;
            this.ReferencePages.Size = new System.Drawing.Size(1024, 638);
            this.ReferencePages.TabIndex = 0;
            this.ReferencePages.SelectedIndexChanged += new System.EventHandler(this.ReferencePages_SelectedIndexChanged);
            // 
            // PartyPage
            // 
            this.PartyPage.Controls.Add(this.PartyBrowser);
            this.PartyPage.Location = new System.Drawing.Point(28, 4);
            this.PartyPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PartyPage.Name = "PartyPage";
            this.PartyPage.Size = new System.Drawing.Size(992, 630);
            this.PartyPage.TabIndex = 0;
            this.PartyPage.Text = "Party Breakdown";
            this.PartyPage.UseVisualStyleBackColor = true;
            // 
            // PartyBrowser
            // 
            this.PartyBrowser.AllowWebBrowserDrop = false;
            this.PartyBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PartyBrowser.IsWebBrowserContextMenuEnabled = false;
            this.PartyBrowser.Location = new System.Drawing.Point(0, 0);
            this.PartyBrowser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PartyBrowser.MinimumSize = new System.Drawing.Size(30, 31);
            this.PartyBrowser.Name = "PartyBrowser";
            this.PartyBrowser.ScriptErrorsSuppressed = true;
            this.PartyBrowser.Size = new System.Drawing.Size(992, 630);
            this.PartyBrowser.TabIndex = 0;
            this.PartyBrowser.WebBrowserShortcutsEnabled = false;
            this.PartyBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.PartyBrowser_Navigating);
            // 
            // ToolsPage
            // 
            this.ToolsPage.Controls.Add(this.ToolBrowserPanel);
            this.ToolsPage.Controls.Add(this.GeneratorToolbar);
            this.ToolsPage.Location = new System.Drawing.Point(28, 4);
            this.ToolsPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ToolsPage.Name = "ToolsPage";
            this.ToolsPage.Size = new System.Drawing.Size(859, 623);
            this.ToolsPage.TabIndex = 1;
            this.ToolsPage.Text = "Random Generators";
            this.ToolsPage.UseVisualStyleBackColor = true;
            // 
            // ToolBrowserPanel
            // 
            this.ToolBrowserPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ToolBrowserPanel.Controls.Add(this.GeneratorBrowser);
            this.ToolBrowserPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolBrowserPanel.Location = new System.Drawing.Point(161, 0);
            this.ToolBrowserPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ToolBrowserPanel.Name = "ToolBrowserPanel";
            this.ToolBrowserPanel.Size = new System.Drawing.Size(698, 623);
            this.ToolBrowserPanel.TabIndex = 3;
            // 
            // GeneratorBrowser
            // 
            this.GeneratorBrowser.AllowWebBrowserDrop = false;
            this.GeneratorBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GeneratorBrowser.IsWebBrowserContextMenuEnabled = false;
            this.GeneratorBrowser.Location = new System.Drawing.Point(0, 0);
            this.GeneratorBrowser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GeneratorBrowser.MinimumSize = new System.Drawing.Size(30, 31);
            this.GeneratorBrowser.Name = "GeneratorBrowser";
            this.GeneratorBrowser.ScriptErrorsSuppressed = true;
            this.GeneratorBrowser.Size = new System.Drawing.Size(696, 621);
            this.GeneratorBrowser.TabIndex = 1;
            this.GeneratorBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.GeneratorBrowser_Navigating);
            // 
            // GeneratorToolbar
            // 
            this.GeneratorToolbar.Dock = System.Windows.Forms.DockStyle.Left;
            this.GeneratorToolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.GeneratorToolbar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.GeneratorToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSeparator26,
            this.ElfNameBtn,
            this.DwarfNameBtn,
            this.HalflingNameBtn,
            this.ExoticNameBtn,
            this.toolStripSeparator44,
            this.TreasureBtn,
            this.BookTitleBtn,
            this.PotionBtn,
            this.toolStripSeparator45,
            this.NPCBtn,
            this.RoomBtn,
            this.toolStripSeparator46,
            this.ElfTextBtn,
            this.DwarfTextBtn,
            this.PrimordialTextBtn});
            this.GeneratorToolbar.Location = new System.Drawing.Point(0, 0);
            this.GeneratorToolbar.Name = "GeneratorToolbar";
            this.GeneratorToolbar.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.GeneratorToolbar.ShowItemToolTips = false;
            this.GeneratorToolbar.Size = new System.Drawing.Size(161, 623);
            this.GeneratorToolbar.TabIndex = 2;
            this.GeneratorToolbar.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(156, 25);
            this.toolStripLabel1.Text = "Generators";
            // 
            // toolStripSeparator26
            // 
            this.toolStripSeparator26.Name = "toolStripSeparator26";
            this.toolStripSeparator26.Size = new System.Drawing.Size(156, 6);
            // 
            // ElfNameBtn
            // 
            this.ElfNameBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ElfNameBtn.Image = ((System.Drawing.Image)(resources.GetObject("ElfNameBtn.Image")));
            this.ElfNameBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ElfNameBtn.Name = "ElfNameBtn";
            this.ElfNameBtn.Size = new System.Drawing.Size(156, 29);
            this.ElfNameBtn.Text = "Elvish Names";
            this.ElfNameBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ElfNameBtn.Click += new System.EventHandler(this.ElfNameBtn_Click);
            // 
            // DwarfNameBtn
            // 
            this.DwarfNameBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DwarfNameBtn.Image = ((System.Drawing.Image)(resources.GetObject("DwarfNameBtn.Image")));
            this.DwarfNameBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DwarfNameBtn.Name = "DwarfNameBtn";
            this.DwarfNameBtn.Size = new System.Drawing.Size(156, 29);
            this.DwarfNameBtn.Text = "Dwarvish Names";
            this.DwarfNameBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DwarfNameBtn.Click += new System.EventHandler(this.DwarfNameBtn_Click);
            // 
            // HalflingNameBtn
            // 
            this.HalflingNameBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.HalflingNameBtn.Image = ((System.Drawing.Image)(resources.GetObject("HalflingNameBtn.Image")));
            this.HalflingNameBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.HalflingNameBtn.Name = "HalflingNameBtn";
            this.HalflingNameBtn.Size = new System.Drawing.Size(156, 29);
            this.HalflingNameBtn.Text = "Halfling Names";
            this.HalflingNameBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.HalflingNameBtn.Click += new System.EventHandler(this.HalflingNameBtn_Click);
            // 
            // ExoticNameBtn
            // 
            this.ExoticNameBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ExoticNameBtn.Image = ((System.Drawing.Image)(resources.GetObject("ExoticNameBtn.Image")));
            this.ExoticNameBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExoticNameBtn.Name = "ExoticNameBtn";
            this.ExoticNameBtn.Size = new System.Drawing.Size(156, 29);
            this.ExoticNameBtn.Text = "Exotic Names";
            this.ExoticNameBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ExoticNameBtn.Click += new System.EventHandler(this.ExoticNameBtn_Click);
            // 
            // toolStripSeparator44
            // 
            this.toolStripSeparator44.Name = "toolStripSeparator44";
            this.toolStripSeparator44.Size = new System.Drawing.Size(156, 6);
            // 
            // TreasureBtn
            // 
            this.TreasureBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TreasureBtn.Image = ((System.Drawing.Image)(resources.GetObject("TreasureBtn.Image")));
            this.TreasureBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TreasureBtn.Name = "TreasureBtn";
            this.TreasureBtn.Size = new System.Drawing.Size(156, 29);
            this.TreasureBtn.Text = "Art Objects";
            this.TreasureBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TreasureBtn.Click += new System.EventHandler(this.TreasureBtn_Click);
            // 
            // BookTitleBtn
            // 
            this.BookTitleBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BookTitleBtn.Image = ((System.Drawing.Image)(resources.GetObject("BookTitleBtn.Image")));
            this.BookTitleBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BookTitleBtn.Name = "BookTitleBtn";
            this.BookTitleBtn.Size = new System.Drawing.Size(156, 29);
            this.BookTitleBtn.Text = "Book Titles";
            this.BookTitleBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BookTitleBtn.Click += new System.EventHandler(this.BookTitleBtn_Click);
            // 
            // PotionBtn
            // 
            this.PotionBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.PotionBtn.Image = ((System.Drawing.Image)(resources.GetObject("PotionBtn.Image")));
            this.PotionBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PotionBtn.Name = "PotionBtn";
            this.PotionBtn.Size = new System.Drawing.Size(156, 29);
            this.PotionBtn.Text = "Potions";
            this.PotionBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PotionBtn.Click += new System.EventHandler(this.PotionBtn_Click);
            // 
            // toolStripSeparator45
            // 
            this.toolStripSeparator45.Name = "toolStripSeparator45";
            this.toolStripSeparator45.Size = new System.Drawing.Size(156, 6);
            // 
            // NPCBtn
            // 
            this.NPCBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.NPCBtn.Image = ((System.Drawing.Image)(resources.GetObject("NPCBtn.Image")));
            this.NPCBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NPCBtn.Name = "NPCBtn";
            this.NPCBtn.Size = new System.Drawing.Size(156, 29);
            this.NPCBtn.Text = "NPC Description";
            this.NPCBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.NPCBtn.Click += new System.EventHandler(this.NPCBtn_Click);
            // 
            // RoomBtn
            // 
            this.RoomBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.RoomBtn.Image = ((System.Drawing.Image)(resources.GetObject("RoomBtn.Image")));
            this.RoomBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RoomBtn.Name = "RoomBtn";
            this.RoomBtn.Size = new System.Drawing.Size(156, 29);
            this.RoomBtn.Text = "Room Description";
            this.RoomBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RoomBtn.Click += new System.EventHandler(this.RoomBtn_Click);
            // 
            // toolStripSeparator46
            // 
            this.toolStripSeparator46.Name = "toolStripSeparator46";
            this.toolStripSeparator46.Size = new System.Drawing.Size(156, 6);
            // 
            // ElfTextBtn
            // 
            this.ElfTextBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ElfTextBtn.Image = ((System.Drawing.Image)(resources.GetObject("ElfTextBtn.Image")));
            this.ElfTextBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ElfTextBtn.Name = "ElfTextBtn";
            this.ElfTextBtn.Size = new System.Drawing.Size(156, 29);
            this.ElfTextBtn.Text = "Elvish Text";
            this.ElfTextBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ElfTextBtn.Click += new System.EventHandler(this.ElfTextBtn_Click);
            // 
            // DwarfTextBtn
            // 
            this.DwarfTextBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DwarfTextBtn.Image = ((System.Drawing.Image)(resources.GetObject("DwarfTextBtn.Image")));
            this.DwarfTextBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DwarfTextBtn.Name = "DwarfTextBtn";
            this.DwarfTextBtn.Size = new System.Drawing.Size(156, 29);
            this.DwarfTextBtn.Text = "Dwarvish Text";
            this.DwarfTextBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DwarfTextBtn.Click += new System.EventHandler(this.DwarfTextBtn_Click);
            // 
            // PrimordialTextBtn
            // 
            this.PrimordialTextBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.PrimordialTextBtn.Image = ((System.Drawing.Image)(resources.GetObject("PrimordialTextBtn.Image")));
            this.PrimordialTextBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrimordialTextBtn.Name = "PrimordialTextBtn";
            this.PrimordialTextBtn.Size = new System.Drawing.Size(156, 29);
            this.PrimordialTextBtn.Text = "Primordial Text";
            this.PrimordialTextBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PrimordialTextBtn.Click += new System.EventHandler(this.PrimordialTextBtn_Click);
            // 
            // CompendiumPage
            // 
            this.CompendiumPage.Controls.Add(this.CompendiumBrowser);
            this.CompendiumPage.Location = new System.Drawing.Point(28, 4);
            this.CompendiumPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CompendiumPage.Name = "CompendiumPage";
            this.CompendiumPage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CompendiumPage.Size = new System.Drawing.Size(859, 623);
            this.CompendiumPage.TabIndex = 2;
            this.CompendiumPage.Text = "Compendium";
            this.CompendiumPage.UseVisualStyleBackColor = true;
            // 
            // CompendiumBrowser
            // 
            this.CompendiumBrowser.AllowWebBrowserDrop = false;
            this.CompendiumBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CompendiumBrowser.Location = new System.Drawing.Point(4, 5);
            this.CompendiumBrowser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CompendiumBrowser.MinimumSize = new System.Drawing.Size(30, 31);
            this.CompendiumBrowser.Name = "CompendiumBrowser";
            this.CompendiumBrowser.ScriptErrorsSuppressed = true;
            this.CompendiumBrowser.Size = new System.Drawing.Size(851, 613);
            this.CompendiumBrowser.TabIndex = 0;
            // 
            // InfoPanel
            // 
            this.InfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InfoPanel.Level = 1;
            this.InfoPanel.Location = new System.Drawing.Point(0, 32);
            this.InfoPanel.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.InfoPanel.Name = "InfoPanel";
            this.InfoPanel.Size = new System.Drawing.Size(258, 606);
            this.InfoPanel.TabIndex = 0;
            // 
            // ReferenceToolbar
            // 
            this.ReferenceToolbar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ReferenceToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DieRollerBtn});
            this.ReferenceToolbar.Location = new System.Drawing.Point(0, 0);
            this.ReferenceToolbar.Name = "ReferenceToolbar";
            this.ReferenceToolbar.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.ReferenceToolbar.Size = new System.Drawing.Size(258, 32);
            this.ReferenceToolbar.TabIndex = 1;
            this.ReferenceToolbar.Text = "toolStrip1";
            // 
            // DieRollerBtn
            // 
            this.DieRollerBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DieRollerBtn.Image = ((System.Drawing.Image)(resources.GetObject("DieRollerBtn.Image")));
            this.DieRollerBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DieRollerBtn.Name = "DieRollerBtn";
            this.DieRollerBtn.Size = new System.Drawing.Size(91, 29);
            this.DieRollerBtn.Text = "Die Roller";
            this.DieRollerBtn.Click += new System.EventHandler(this.DieRollerBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1296, 708);
            this.Controls.Add(this.Pages);
            this.Controls.Add(this.MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Masterplan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.MainForm_Layout);
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
            this.ResumeLayout(false);
            this.PerformLayout();

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