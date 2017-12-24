using Masterplan;
using Masterplan.Data;
using Masterplan.Tools;
using Masterplan.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Xml;
using Utils;

namespace Masterplan.Controls
{
	internal class WelcomePanel : UserControl
	{
		private const int MAX_HEADLINES = 10;

		private const int MAX_LENGTH = 45;

		private IContainer components;

		private Masterplan.Controls.TitlePanel TitlePanel;

		private WebBrowser MenuBrowser;

		private List<WelcomePanel.Headline> fHeadlines;

		private bool fShowHeadlines;

		public bool ShowHeadlines
		{
			get
			{
				return this.fShowHeadlines;
			}
			set
			{
				this.fShowHeadlines = value;
			}
		}

		public WelcomePanel(bool show_headlines)
		{
			this.InitializeComponent();
			this.fShowHeadlines = show_headlines;
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.MenuBrowser.DocumentText = "";
			this.set_options();
			if (this.fShowHeadlines)
			{
				this.DownloadHeadlines("http://www.habitualindolence.net/masterplanblog/feed/");
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

		private void downloaded_headlines(object sender, DownloadStringCompletedEventArgs e)
		{
			try
			{
				this.fHeadlines = new List<WelcomePanel.Headline>();
				if (e.Error == null)
				{
					string result = e.Result;
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(result);
					XmlNode documentElement = xmlDocument.DocumentElement;
					if (documentElement != null)
					{
						XmlNode firstChild = documentElement.FirstChild;
						if (firstChild != null)
						{
							foreach (XmlNode childNode in firstChild.ChildNodes)
							{
								if (childNode.Name.ToLower() != "item")
								{
									continue;
								}
								WelcomePanel.Headline headline = new WelcomePanel.Headline();
								XmlNode xmlNodes = XMLHelper.FindChild(childNode, "title");
								if (xmlNodes == null)
								{
									continue;
								}
								headline.Title = xmlNodes.InnerText;
								XmlNode xmlNodes1 = XMLHelper.FindChild(childNode, "link");
								if (xmlNodes1 == null)
								{
									continue;
								}
								headline.URL = xmlNodes1.InnerText;
								XmlNode xmlNodes2 = XMLHelper.FindChild(childNode, "pubDate");
								if (xmlNodes1 == null)
								{
									continue;
								}
								headline.Date = DateTime.Parse(xmlNodes2.InnerText);
								if (headline.Title.Length > 45)
								{
									headline.Title = string.Concat(headline.Title.Substring(0, 45), "...");
								}
								this.fHeadlines.Add(headline);
							}
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
				this.set_options();
			}
			catch
			{
			}
		}

		public void DownloadHeadlines(string address)
		{
			try
			{
				WebClient webClient = new WebClient();
				webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(this.downloaded_headlines);
				webClient.DownloadStringAsync(new Uri(address));
			}
			catch (WebException webException)
			{
				LogSystem.Trace(webException);
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private string get_manual_filename()
		{
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			return string.Concat(FileName.Directory(entryAssembly.FullName), "Manual.pdf");
		}

		private void InitializeComponent()
		{
			this.MenuBrowser = new WebBrowser();
			this.TitlePanel = new Masterplan.Controls.TitlePanel();
			base.SuspendLayout();
			this.MenuBrowser.Dock = DockStyle.Right;
			this.MenuBrowser.IsWebBrowserContextMenuEnabled = false;
			this.MenuBrowser.Location = new Point(364, 0);
			this.MenuBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.MenuBrowser.Name = "MenuBrowser";
			this.MenuBrowser.ScriptErrorsSuppressed = true;
			this.MenuBrowser.Size = new System.Drawing.Size(345, 429);
			this.MenuBrowser.TabIndex = 5;
			this.MenuBrowser.WebBrowserShortcutsEnabled = false;
			this.MenuBrowser.Navigating += new WebBrowserNavigatingEventHandler(this.MenuBrowser_Navigating);
			this.TitlePanel.Dock = DockStyle.Fill;
			this.TitlePanel.Font = new System.Drawing.Font("Calibri", 11f);
			this.TitlePanel.ForeColor = Color.MidnightBlue;
			this.TitlePanel.Location = new Point(0, 0);
			this.TitlePanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.TitlePanel.Mode = Masterplan.Controls.TitlePanel.TitlePanelMode.WelcomeScreen;
			this.TitlePanel.Name = "TitlePanel";
			this.TitlePanel.Size = new System.Drawing.Size(364, 429);
			this.TitlePanel.TabIndex = 4;
			this.TitlePanel.Title = "Masterplan";
			this.TitlePanel.Zooming = false;
			this.TitlePanel.FadeFinished += new EventHandler(this.TitlePanel_FadeFinished);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = Color.White;
			base.Controls.Add(this.TitlePanel);
			base.Controls.Add(this.MenuBrowser);
			base.Name = "WelcomePanel";
			base.Size = new System.Drawing.Size(709, 429);
			base.ResumeLayout(false);
		}

		private void MenuBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.Scheme == "masterplan")
			{
				e.Cancel = true;
				if (e.Url.LocalPath == "new")
				{
					this.OnNewProjectClicked();
				}
				if (e.Url.LocalPath == "open")
				{
					this.OnOpenProjectClicked();
				}
				if (e.Url.LocalPath == "last")
				{
					this.OnOpenLastProjectClicked();
				}
				if (e.Url.LocalPath == "delve")
				{
					this.OnDelveClicked();
				}
				if (e.Url.LocalPath == "premade")
				{
					this.OnPremadeClicked();
				}
				if (e.Url.LocalPath == "genesis")
				{
					Creature creature = new Creature()
					{
						Name = "New Creature"
					};
					(new CreatureBuilderForm(creature)).ShowDialog();
				}
				if (e.Url.LocalPath == "exodus")
				{
					CreatureTemplate creatureTemplate = new CreatureTemplate()
					{
						Name = "New Template"
					};
					(new CreatureTemplateBuilderForm(creatureTemplate)).ShowDialog();
				}
				if (e.Url.LocalPath == "minos")
				{
					Trap trap = new Trap()
					{
						Name = "New Trap"
					};
					trap.Attacks.Add(new TrapAttack());
					(new TrapBuilderForm(trap)).ShowDialog();
				}
				if (e.Url.LocalPath == "excalibur")
				{
					MagicItem magicItem = new MagicItem()
					{
						Name = "New Magic Item"
					};
					(new MagicItemBuilderForm(magicItem)).ShowDialog();
				}
				if (e.Url.LocalPath == "indiana")
				{
					Artifact artifact = new Artifact()
					{
						Name = "New Artifact"
					};
					(new ArtifactBuilderForm(artifact)).ShowDialog();
				}
				if (e.Url.LocalPath == "manual")
				{
					this.open_manual();
				}
			}
		}

		protected void OnDelveClicked()
		{
			if (this.DelveClicked != null)
			{
				this.DelveClicked(this, new EventArgs());
			}
		}

		protected void OnNewProjectClicked()
		{
			if (this.NewProjectClicked != null)
			{
				this.NewProjectClicked(this, new EventArgs());
			}
		}

		protected void OnOpenLastProjectClicked()
		{
			if (this.OpenLastProjectClicked != null)
			{
				this.OpenLastProjectClicked(this, new EventArgs());
			}
		}

		protected void OnOpenProjectClicked()
		{
			if (this.OpenProjectClicked != null)
			{
				this.OpenProjectClicked(this, new EventArgs());
			}
		}

		protected void OnPremadeClicked()
		{
			if (this.PremadeClicked != null)
			{
				this.PremadeClicked(this, new EventArgs());
			}
		}

		private void open_manual()
		{
			string manualFilename = this.get_manual_filename();
			if (!File.Exists(manualFilename))
			{
				return;
			}
			Process.Start(manualFilename);
		}

		private void set_options()
		{
			List<string> strs = new List<string>()
			{
				"<HTML>"
			};
			strs.AddRange(HTML.GetHead("Masterplan", "Main Menu", DisplaySize.Small));
			strs.Add("<BODY>");
			strs.Add("<P class=table>");
			strs.Add("<TABLE>");
			strs.Add("<TR class=heading>");
			strs.Add("<TD>");
			strs.Add("<B>Getting Started</B>");
			strs.Add("</TD>");
			strs.Add("</TR>");
			if (this.show_last_file_option())
			{
				string str = FileName.Name(Session.Preferences.LastFile);
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<A href=\"masterplan:last\">Reopen <I>", str, "</I></A>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			strs.Add("<TR>");
			strs.Add("<TD>");
			strs.Add("<A href=\"masterplan:new\">Create a new adventure project</A>");
			strs.Add("</TD>");
			strs.Add("</TR>");
			strs.Add("<TR>");
			strs.Add("<TD>");
			strs.Add("<A href=\"masterplan:open\">Open an existing project</A>");
			strs.Add("</TD>");
			strs.Add("</TR>");
			if (this.show_delve_option())
			{
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add("<A href=\"masterplan:delve\">Generate a random dungeon delve</A>");
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			strs.Add("<TR>");
			strs.Add("<TD>");
			strs.Add("<A href=\"masterplan:premade\">Download a premade adventure</A>");
			strs.Add("</TD>");
			strs.Add("</TR>");
			strs.Add("</TABLE>");
			strs.Add("</P>");
			if (Program.IsBeta)
			{
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				strs.Add("<TR class=heading>");
				strs.Add("<TD>");
				strs.Add("<B>Development Links</B>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add("<A href=\"masterplan:genesis\">Project Genesis</A>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add("<A href=\"masterplan:exodus\">Project Exodus</A>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add("<A href=\"masterplan:minos\">Project Minos</A>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add("<A href=\"masterplan:excalibur\">Project Excalibur</A>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add("<A href=\"masterplan:indiana\">Project Indiana</A>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("</TABLE>");
				strs.Add("</P>");
			}
			strs.Add("<P class=table>");
			strs.Add("<TABLE>");
			strs.Add("<TR class=heading>");
			strs.Add("<TD>");
			strs.Add("<B>More Information</B>");
			strs.Add("</TD>");
			strs.Add("</TR>");
			if (this.show_manual_option())
			{
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add("<A href=\"masterplan:manual\">Read the Masterplan user manual</A>");
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			strs.Add("<TR>");
			strs.Add("<TD>");
			strs.Add("<A href=\"http://www.habitualindolence.net/masterplan/tutorials.htm\" target=_new>Watch a tutorial video</A>");
			strs.Add("</TD>");
			strs.Add("</TR>");
			strs.Add("<TR>");
			strs.Add("<TD>");
			strs.Add("<A href=\"http://www.habitualindolence.net/masterplan/\" target=_new>Visit the Masterplan website</A>");
			strs.Add("</TD>");
			strs.Add("</TR>");
			strs.Add("</TABLE>");
			strs.Add("</P>");
			strs.Add("<P class=table>");
			strs.Add("<TABLE>");
			strs.Add("<TR class=heading>");
			strs.Add("<TD>");
			strs.Add("<B>Latest News</B>");
			strs.Add("</TD>");
			strs.Add("</TR>");
			if (!this.fShowHeadlines)
			{
				strs.Add("<TR>");
				strs.Add("<TD class=dimmed>");
				strs.Add("Headlines are disabled");
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			else if (this.fHeadlines == null)
			{
				strs.Add("<TR>");
				strs.Add("<TD class=dimmed>");
				strs.Add("Retrieving headlines...");
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			else if (this.fHeadlines.Count != 0)
			{
				this.fHeadlines.Sort();
				int num = 0;
				List<WelcomePanel.Headline>.Enumerator enumerator = this.fHeadlines.GetEnumerator();
				try
				{
					do
					{
						if (!enumerator.MoveNext())
						{
							break;
						}
						WelcomePanel.Headline current = enumerator.Current;
						strs.Add("<TR>");
						strs.Add("<TD>");
						strs.Add(string.Concat(current.Date.ToString("dd MMM"), ":"));
						string[] uRL = new string[] { "<A href=\"", current.URL, "\" target=_new>", current.Title, "</A>" };
						strs.Add(string.Concat(uRL));
						strs.Add("</TD>");
						strs.Add("</TR>");
						num++;
					}
					while (num != 10);
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
			}
			else
			{
				strs.Add("<TR>");
				strs.Add("<TD class=dimmed>");
				strs.Add("Could not download headlines");
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			strs.Add("</TABLE>");
			strs.Add("</P>");
			strs.Add("</BODY>");
			strs.Add("</HTML>");
			this.MenuBrowser.Document.OpenNew(true);
			this.MenuBrowser.Document.Write(HTML.Concatenate(strs));
		}

		private bool show_delve_option()
		{
			bool flag;
			List<Library>.Enumerator enumerator = Session.Libraries.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.ShowInAutoBuild)
					{
						continue;
					}
					flag = true;
					return flag;
				}
				return false;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return flag;
		}

		private bool show_last_file_option()
		{
			if (Session.Preferences.LastFile == null || Session.Preferences.LastFile == "")
			{
				return false;
			}
			return File.Exists(Session.Preferences.LastFile);
		}

		private bool show_manual_option()
		{
			return File.Exists(this.get_manual_filename());
		}

		private void TitlePanel_FadeFinished(object sender, EventArgs e)
		{
		}

		[Category("Actions")]
		public event EventHandler DelveClicked;

		[Category("Actions")]
		public event EventHandler NewProjectClicked;

		[Category("Actions")]
		public event EventHandler OpenLastProjectClicked;

		[Category("Actions")]
		public event EventHandler OpenProjectClicked;

		[Category("Actions")]
		public event EventHandler PremadeClicked;

		private class Headline : IComparable<WelcomePanel.Headline>
		{
			public string Title;

			public string URL;

			public DateTime Date;

			public Headline()
			{
			}

			public int CompareTo(WelcomePanel.Headline rhs)
			{
				return this.Date.CompareTo(rhs.Date) * -1;
			}
		}
	}
}