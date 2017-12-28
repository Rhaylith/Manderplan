using Masterplan;
using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Events;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class PlayerViewForm : Form
	{
		public static bool UseOtherDisplay;

		public static Masterplan.Tools.DisplaySize DisplaySize;

		private PlayerViewMode fMode;

		private MapView fParentMap;

		private IContainer components;

		private ToolTip Tooltip;

		public PlayerViewMode Mode
		{
			get
			{
				return this.fMode;
			}
			set
			{
				this.fMode = value;
			}
		}

		public MapView ParentMap
		{
			get
			{
				return this.fParentMap;
			}
			set
			{
				this.fParentMap = value;
			}
		}

		static PlayerViewForm()
		{
			PlayerViewForm.UseOtherDisplay = true;
			PlayerViewForm.DisplaySize = Masterplan.Tools.DisplaySize.Small;
		}

		public PlayerViewForm(Form parent)
		{
			this.InitializeComponent();
			this.set_location(parent);
		}

		private void dicebtn_click(object sender, EventArgs e)
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

		private void hover_token_changed(object sender, EventArgs e)
		{
			SplitContainer item = base.Controls[0] as SplitContainer;
			MapView mapView = item.Panel1.Controls[0] as MapView;
			this.fParentMap.HoverToken = mapView.HoverToken;
			string category = "";
			string str = null;
			if (mapView.HoverToken is CreatureToken)
			{
				CreatureToken hoverToken = mapView.HoverToken as CreatureToken;
				EncounterSlot encounterSlot = mapView.Encounter.FindSlot(hoverToken.SlotID);
				ICreature creature = Session.FindCreature(encounterSlot.Card.CreatureID, SearchType.Global);
				int hP = encounterSlot.Card.HP;
				int damage = hP - hoverToken.Data.Damage;
				int num = hP / 2;
				if (!mapView.ShowCreatureLabels)
				{
					category = creature.Category;
					if (category == "")
					{
						category = "Creature";
					}
				}
				else
				{
					category = hoverToken.Data.DisplayName;
				}
				if (hoverToken.Data.Damage == 0)
				{
					str = "Not wounded";
				}
				if (damage < hP)
				{
					str = "Wounded";
				}
				if (damage < num)
				{
					str = "Bloodied";
				}
				if (damage <= 0)
				{
					str = "Dead";
				}
				if (hoverToken.Data.Conditions.Count != 0)
				{
					str = string.Concat(str, Environment.NewLine);
					foreach (OngoingCondition condition in hoverToken.Data.Conditions)
					{
						str = string.Concat(str, Environment.NewLine);
						str = string.Concat(str, condition.ToString(this.fParentMap.Encounter, false));
					}
				}
			}
			if (mapView.HoverToken is Hero)
			{
				Hero hero = mapView.HoverToken as Hero;
				category = hero.Name;
				str = string.Concat(hero.Race, " ", hero.Class);
				str = string.Concat(str, Environment.NewLine);
				str = string.Concat(str, "Player: ", hero.Player);
			}
			if (mapView.HoverToken is CustomToken)
			{
				CustomToken customToken = mapView.HoverToken as CustomToken;
				if (mapView.ShowCreatureLabels)
				{
					category = customToken.Name;
					str = "(custom token)";
				}
			}
			this.Tooltip.ToolTipTitle = category;
			this.Tooltip.ToolTipIcon = ToolTipIcon.Info;
			this.Tooltip.SetToolTip(mapView, str);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(PlayerViewForm));
			this.Tooltip = new ToolTip(this.components);
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = Color.Black;
			base.ClientSize = new System.Drawing.Size(534, 357);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "PlayerViewForm";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Player View";
			base.FormClosed += new FormClosedEventHandler(this.PlayerViewForm_FormClosed);
			base.ResumeLayout(false);
		}

		private void item_moved(object sender, MovementEventArgs e)
		{
			this.fParentMap.Invalidate();
		}

		private void mouse_move(object sender, MouseEventArgs e)
		{
			(base.Controls[0] as TitlePanel).Wake();
		}

		private void PlayerViewForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Session.PlayerView = null;
		}

		private void selected_tokens_changed(object sender, EventArgs e)
		{
			SplitContainer item = base.Controls[0] as SplitContainer;
			MapView mapView = item.Panel1.Controls[0] as MapView;
			this.fParentMap.SelectTokens(mapView.SelectedTokens, true);
		}

		private void set_html(string html)
		{
			WebBrowser webBrowser = new WebBrowser()
			{
				IsWebBrowserContextMenuEnabled = false,
				ScriptErrorsSuppressed = true,
				WebBrowserShortcutsEnabled = false,
				DocumentText = html
			};
			base.Controls.Clear();
			base.Controls.Add(webBrowser);
			webBrowser.Dock = DockStyle.Fill;
			this.fMode = PlayerViewMode.HTML;
		}

		private void set_location(Form parent)
		{
			if (!PlayerViewForm.UseOtherDisplay)
			{
				return;
			}
			if ((int)Screen.AllScreens.Length < 2)
			{
				return;
			}
			List<Screen> screens = new List<Screen>();
			Screen[] allScreens = Screen.AllScreens;
			for (int i = 0; i < (int)allScreens.Length; i++)
			{
				Screen screen = allScreens[i];
				if (!screen.Bounds.Contains(parent.ClientRectangle))
				{
					screens.Add(screen);
				}
			}
			if (screens.Count == 0)
			{
				return;
			}
			base.StartPosition = FormStartPosition.Manual;
			base.Location = screens[0].WorkingArea.Location;
			base.WindowState = FormWindowState.Maximized;
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		}

		public void ShowArtifact(Artifact artifact)
		{
			string str = HTML.Artifact(artifact, PlayerViewForm.DisplaySize, false, true);
			this.set_html(str);
			base.Show();
		}

		public void ShowBackground(Background background)
		{
			this.set_html(HTML.Background(background, PlayerViewForm.DisplaySize));
			base.Show();
		}

		public void ShowBackground(List<Background> backgrounds)
		{
			this.set_html(HTML.Background(backgrounds, PlayerViewForm.DisplaySize));
			base.Show();
		}

		public void ShowCalendar(Calendar calendar, int month_index, int year)
		{
			CalendarPanel calendarPanel = new CalendarPanel()
			{
				Calendar = calendar,
				MonthIndex = month_index,
				Year = year
			};
			base.Controls.Clear();
			base.Controls.Add(calendarPanel);
			calendarPanel.Dock = DockStyle.Fill;
			this.fMode = PlayerViewMode.Calendar;
			base.Show();
		}

		public void ShowCreatureTemplate(CreatureTemplate template)
		{
			this.set_html(HTML.CreatureTemplate(template, PlayerViewForm.DisplaySize, false));
			base.Show();
		}

		public void ShowDefault()
		{
			TitlePanel titlePanel = new TitlePanel()
			{
				Title = "Manderplan",
				Zooming = true,
				Mode = TitlePanel.TitlePanelMode.PlayerView,
				BackColor = Color.Black,
				ForeColor = Color.White
			};
			titlePanel.MouseMove += new MouseEventHandler(this.mouse_move);
			base.Controls.Clear();
			base.Controls.Add(titlePanel);
			titlePanel.Dock = DockStyle.Fill;
			this.fMode = PlayerViewMode.Blank;
			base.Show();
		}

		public void ShowEncounterCard(EncounterCard card)
		{
			string str = HTML.StatBlock(card, null, null, true, false, true, CardMode.View, PlayerViewForm.DisplaySize);
			this.set_html(str);
			base.Show();
		}

		public void ShowEncounterReportTable(ReportTable table)
		{
			this.set_html(HTML.EncounterReportTable(table, PlayerViewForm.DisplaySize));
			base.Show();
		}

		public void ShowEncyclopediaGroup(EncyclopediaGroup group)
		{
			string str = HTML.EncyclopediaGroup(group, Session.Project.Encyclopedia, PlayerViewForm.DisplaySize, false, false);
			this.set_html(str);
			base.Show();
		}

		public void ShowEncyclopediaItem(IEncyclopediaItem item)
		{
			if (item is EncyclopediaEntry)
			{
				string str = HTML.EncyclopediaEntry(item as EncyclopediaEntry, Session.Project.Encyclopedia, PlayerViewForm.DisplaySize, false, false, false, false);
				this.set_html(str);
			}
			if (item is EncyclopediaGroup)
			{
				string str1 = HTML.EncyclopediaGroup(item as EncyclopediaGroup, Session.Project.Encyclopedia, PlayerViewForm.DisplaySize, false, false);
				this.set_html(str1);
			}
			base.Show();
		}

		public void ShowHandout(List<object> items, bool include_dm_info)
		{
			this.set_html(HTML.Handout(items, PlayerViewForm.DisplaySize, include_dm_info));
			base.Show();
		}

		public void ShowHero(Hero h)
		{
			string str = HTML.StatBlock(h, null, true, false, false, PlayerViewForm.DisplaySize);
			this.set_html(str);
			base.Show();
		}

		public void ShowImage(Attachment att)
		{
			Image image = Image.FromStream(new MemoryStream(att.Contents));
			PictureBox pictureBox = new PictureBox()
			{
				Image = image,
				SizeMode = PictureBoxSizeMode.Zoom
			};
			base.Controls.Clear();
			base.Controls.Add(pictureBox);
			pictureBox.Dock = DockStyle.Fill;
			this.fMode = PlayerViewMode.Image;
			base.Show();
		}

		public void ShowImage(Image img)
		{
			PictureBox pictureBox = new PictureBox()
			{
				Image = img,
				SizeMode = PictureBoxSizeMode.Zoom
			};
			base.Controls.Clear();
			base.Controls.Add(pictureBox);
			pictureBox.Dock = DockStyle.Fill;
			this.fMode = PlayerViewMode.Image;
			base.Show();
		}

		public void ShowMagicItem(MagicItem item)
		{
			string str = HTML.MagicItem(item, PlayerViewForm.DisplaySize, false, true);
			this.set_html(str);
			base.Show();
		}

		public void ShowMessage(string message)
		{
			string str = HTML.Text(message, true, true, PlayerViewForm.DisplaySize);
			this.set_html(str);
			base.Show();
		}

		public void ShowPCs()
		{
			this.set_html(HTML.PartyBreakdown(PlayerViewForm.DisplaySize));
			base.Show();
		}

		public void ShowPlainText(Attachment att)
		{
			string str = (new ASCIIEncoding()).GetString(att.Contents);
			string str1 = HTML.Text(str, true, false, PlayerViewForm.DisplaySize);
			this.set_html(str1);
			base.Show();
		}

		public void ShowPlayerOption(IPlayerOption option)
		{
			this.set_html(HTML.PlayerOption(option, PlayerViewForm.DisplaySize));
			base.Show();
		}

		public void ShowPlotPoint(PlotPoint pp)
		{
			string str = HTML.Text(pp.ReadAloud, false, false, PlayerViewForm.DisplaySize);
			this.set_html(str);
			base.Show();
		}

		public void ShowRegionalMap(RegionalMapPanel panel)
		{
			RegionalMapPanel regionalMapPanel = new RegionalMapPanel()
			{
				Map = panel.Map,
				Mode = MapViewMode.PlayerView
			};
			if (panel.SelectedLocation != null)
			{
				regionalMapPanel.ShowLocations = true;
				regionalMapPanel.HighlightedLocation = panel.SelectedLocation;
			}
			else
			{
				regionalMapPanel.ShowLocations = false;
			}
			base.Controls.Clear();
			base.Controls.Add(regionalMapPanel);
			regionalMapPanel.Dock = DockStyle.Fill;
			this.fMode = PlayerViewMode.RegionalMap;
			base.Show();
		}

		public void ShowRichText(Attachment att)
		{
			string str = (new ASCIIEncoding()).GetString(att.Contents);
			RichTextBox richTextBox = new RichTextBox()
			{
				Rtf = str,
				ReadOnly = true,
				Multiline = true,
				ScrollBars = RichTextBoxScrollBars.Vertical
			};
			base.Controls.Clear();
			base.Controls.Add(richTextBox);
			richTextBox.Dock = DockStyle.Fill;
			this.fMode = PlayerViewMode.RichText;
			base.Show();
		}

		public void ShowSkillChallenge(SkillChallenge sc)
		{
			string str = HTML.SkillChallenge(sc, false, true, PlayerViewForm.DisplaySize);
			this.set_html(str);
			base.Show();
		}

		public void ShowTacticalMap(MapView mapview, string initiative)
		{
			this.fParentMap = mapview;
			MapView mapView = null;
			if (this.fParentMap != null)
			{
				mapView = new MapView()
				{
					Map = this.fParentMap.Map,
					Viewpoint = this.fParentMap.Viewpoint,
					BorderSize = this.fParentMap.BorderSize,
					ScalingFactor = this.fParentMap.ScalingFactor,
					Encounter = this.fParentMap.Encounter,
					Plot = this.fParentMap.Plot,
					TokenLinks = this.fParentMap.TokenLinks,
					AllowDrawing = this.fParentMap.AllowDrawing,
					Mode = MapViewMode.PlayerView,
					Tactical = true,
					HighlightAreas = false,
					FrameType = MapDisplayType.Opaque,
					ShowCreatures = Session.Preferences.PlayerViewFog,
					ShowHealthBars = Session.Preferences.PlayerViewHealthBars,
					ShowCreatureLabels = Session.Preferences.PlayerViewCreatureLabels,
					ShowGrid = (Session.Preferences.PlayerViewGrid ? MapGridMode.Overlay : MapGridMode.None),
					ShowGridLabels = Session.Preferences.PlayerViewGridLabels,
					ShowAuras = false
				};
				mapView.ShowGrid = MapGridMode.None;
				foreach (MapSketch sketch in mapview.Sketches)
				{
					mapView.Sketches.Add(sketch.Copy());
				}
				mapView.SelectedTokensChanged += new EventHandler(this.selected_tokens_changed);
				mapView.HoverTokenChanged += new EventHandler(this.hover_token_changed);
				mapView.ItemMoved += new MovementEventHandler(this.item_moved);
				mapView.TokenDragged += new DraggedTokenEventHandler(this.token_dragged);
				mapView.SketchCreated += new MapSketchEventHandler(this.sketch_created);
				mapView.Dock = DockStyle.Fill;
			}
			Button button = new Button()
			{
				Text = "Die Roller",
				BackColor = SystemColors.Control,
				Dock = DockStyle.Bottom
			};
			button.Click += new EventHandler(this.dicebtn_click);
			WebBrowser webBrowser = new WebBrowser()
			{
				IsWebBrowserContextMenuEnabled = false,
				ScriptErrorsSuppressed = true,
				WebBrowserShortcutsEnabled = false,
				Dock = DockStyle.Fill,
				DocumentText = initiative
			};
			SplitContainer splitContainer = new SplitContainer();
			splitContainer.Panel1.Controls.Add(mapView);
			splitContainer.Panel2.Controls.Add(webBrowser);
			splitContainer.Panel2.Controls.Add(button);
			base.Controls.Clear();
			base.Controls.Add(splitContainer);
			splitContainer.Dock = DockStyle.Fill;
			if (mapview == null)
			{
				splitContainer.Panel1Collapsed = true;
			}
			else if (initiative != null)
			{
				splitContainer.BackColor = Color.FromArgb(10, 10, 10);
				splitContainer.SplitterDistance = (int)((double)base.Width * 0.65);
				splitContainer.FixedPanel = FixedPanel.Panel2;
				splitContainer.Panel2Collapsed = !Session.Preferences.PlayerViewInitiative;
			}
			else
			{
				splitContainer.Panel2Collapsed = true;
			}
			this.fMode = PlayerViewMode.Combat;
			base.Show();
		}

		public void ShowTerrainPower(TerrainPower tp)
		{
			this.set_html(HTML.TerrainPower(tp, PlayerViewForm.DisplaySize));
			base.Show();
		}

		public void ShowTrap(Trap trap)
		{
			string str = HTML.Trap(trap, null, true, false, false, PlayerViewForm.DisplaySize);
			this.set_html(str);
			base.Show();
		}

		public void ShowWebPage(Attachment att)
		{
			WebBrowser webBrowser = new WebBrowser()
			{
				IsWebBrowserContextMenuEnabled = false,
				ScriptErrorsSuppressed = true,
				WebBrowserShortcutsEnabled = false
			};
			switch (att.Type)
			{
				case AttachmentType.URL:
				{
					string str = (new ASCIIEncoding()).GetString(att.Contents);
					string[] newLine = new string[] { Environment.NewLine };
					string[] strArrays = str.Split(newLine, StringSplitOptions.RemoveEmptyEntries);
					string str1 = "";
					string[] strArrays1 = strArrays;
					int num = 0;
					while (num < (int)strArrays1.Length)
					{
						string str2 = strArrays1[num];
						if (!str2.StartsWith("URL="))
						{
							num++;
						}
						else
						{
							str1 = str2.Substring(4);
							break;
						}
					}
					if (str1 == "")
					{
						break;
					}
					webBrowser.Navigate(str1);
					break;
				}
				case AttachmentType.HTML:
				{
					webBrowser.DocumentText = (new ASCIIEncoding()).GetString(att.Contents);
					break;
				}
			}
			base.Controls.Clear();
			base.Controls.Add(webBrowser);
			webBrowser.Dock = DockStyle.Fill;
			this.fMode = PlayerViewMode.HTML;
			base.Show();
		}

		private void sketch_created(object sender, MapSketchEventArgs e)
		{
			this.fParentMap.Sketches.Add(e.Sketch.Copy());
			this.fParentMap.Invalidate();
		}

		private void token_dragged(object sender, DraggedTokenEventArgs e)
		{
			this.fParentMap.SetDragInfo(e.OldLocation, e.NewLocation);
		}
	}
}