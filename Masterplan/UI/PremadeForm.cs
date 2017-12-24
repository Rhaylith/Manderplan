using Masterplan;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using Utils;
using Utils.Forms;

namespace Masterplan.UI
{
	internal class PremadeForm : Form
	{
		private IContainer components;

		private ListView AdventureList;

		private Button OKBtn;

		private Button CancelBtn;

		private ColumnHeader NameHdr;

		private ColumnHeader SizeHdr;

		private ColumnHeader LevelHdr;

		private string fDownloadedFileName = "";

		private List<PremadeForm.Adventure> fAdventures;

		private ProgressScreen fProgressScreen;

		public string DownloadedFileName
		{
			get
			{
				return this.fDownloadedFileName;
			}
		}

		public PremadeForm.Adventure SelectedAdventure
		{
			get
			{
				if (this.AdventureList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.AdventureList.SelectedItems[0].Tag as PremadeForm.Adventure;
			}
		}

		public PremadeForm()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			WebClient webClient = new WebClient();
			webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(this.downloaded_html);
			webClient.DownloadStringAsync(new Uri("http://www.habitualindolence.net/masterplan/adventures.htm"));
			this.update_list();
		}

		private void AdventureList_DoubleClick(object sender, EventArgs e)
		{
			this.OKBtn_Click(sender, e);
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = this.SelectedAdventure != null;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void download_completed(object sender, AsyncCompletedEventArgs e)
		{
			this.fProgressScreen.Hide();
			this.fProgressScreen = null;
			base.DialogResult = System.Windows.Forms.DialogResult.OK;
			base.Close();
		}

		private void downloaded_html(object sender, DownloadStringCompletedEventArgs e)
		{
			try
			{
				this.fAdventures = new List<PremadeForm.Adventure>();
				string lower = e.Result.ToLower();
				int num = 0;
				while (true)
				{
					int num1 = lower.IndexOf("<tr>", num);
					if (num1 == -1)
					{
						break;
					}
					int length = lower.IndexOf("</tr>", num1);
					if (length == -1)
					{
						break;
					}
					length += "</tr>".Length;
					string str = e.Result.Substring(num1, length - num1);
					num = length;
					PremadeForm.Adventure _adventure = this.get_adventure(str);
					if (_adventure != null)
					{
						this.fAdventures.Add(_adventure);
					}
				}
			}
			catch
			{
			}
			this.update_list();
		}

		private PremadeForm.Adventure get_adventure(string html)
		{
			PremadeForm.Adventure adventure;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(html);
				XmlNode documentElement = xmlDocument.DocumentElement;
				if (documentElement != null)
				{
					PremadeForm.Adventure innerText = new PremadeForm.Adventure();
					XmlNode firstChild = documentElement.FirstChild;
					innerText.Name = firstChild.InnerText;
					innerText.URL = XMLHelper.GetAttribute(firstChild.FirstChild, "href");
					innerText.URL = string.Concat("http://www.habitualindolence.net/masterplan/", innerText.URL);
					XmlNode nextSibling = firstChild.NextSibling;
					string str = nextSibling.InnerText.Replace("Level", "");
					str = str.Replace("level", "");
					str = str.Replace(" ", "");
					innerText.PartyLevel = int.Parse(str);
					string str1 = nextSibling.NextSibling.InnerText.Replace("PCs", "");
					str1 = str1.Replace("pcs", "");
					str1 = str1.Replace("heroes", "");
					str1 = str1.Replace(" ", "");
					innerText.PartySize = int.Parse(str1);
					adventure = innerText;
				}
				else
				{
					adventure = null;
				}
			}
			catch
			{
				return null;
			}
			return adventure;
		}

		private void get_file_name(PremadeForm.Adventure adv)
		{
			string str = FileName.Name(adv.Name);
			str = string.Concat(FileName.TrimInvalidCharacters(str), ".masterplan");
			SaveFileDialog saveFileDialog = new SaveFileDialog()
			{
				Filter = Program.ProjectFilter,
				FileName = str
			};
			if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.start_download(adv, saveFileDialog.FileName);
			}
		}

		private void InitializeComponent()
		{
			this.AdventureList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.LevelHdr = new ColumnHeader();
			this.SizeHdr = new ColumnHeader();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			base.SuspendLayout();
			this.AdventureList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			ListView.ColumnHeaderCollection columns = this.AdventureList.Columns;
			ColumnHeader[] nameHdr = new ColumnHeader[] { this.NameHdr, this.LevelHdr, this.SizeHdr };
			columns.AddRange(nameHdr);
			this.AdventureList.FullRowSelect = true;
			this.AdventureList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.AdventureList.HideSelection = false;
			this.AdventureList.Location = new Point(12, 12);
			this.AdventureList.MultiSelect = false;
			this.AdventureList.Name = "AdventureList";
			this.AdventureList.Size = new System.Drawing.Size(452, 188);
			this.AdventureList.TabIndex = 0;
			this.AdventureList.UseCompatibleStateImageBehavior = false;
			this.AdventureList.View = View.Details;
			this.AdventureList.DoubleClick += new EventHandler(this.AdventureList_DoubleClick);
			this.NameHdr.Text = "Adventure Name";
			this.NameHdr.Width = 219;
			this.LevelHdr.Text = "Party Level";
			this.LevelHdr.TextAlign = HorizontalAlignment.Right;
			this.LevelHdr.Width = 100;
			this.SizeHdr.Text = "Party Size";
			this.SizeHdr.TextAlign = HorizontalAlignment.Right;
			this.SizeHdr.Width = 100;
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(308, 206);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 1;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(389, 206);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 2;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(476, 241);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.AdventureList);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PremadeForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Premade Adventures";
			base.FormClosing += new FormClosingEventHandler(this.PremadeForm_FormClosing);
			base.ResumeLayout(false);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedAdventure != null)
			{
				this.get_file_name(this.SelectedAdventure);
			}
		}

		private void PremadeForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.fProgressScreen != null)
			{
				e.Cancel = true;
			}
		}

		private void progress_changed(object sender, DownloadProgressChangedEventArgs e)
		{
			this.fProgressScreen.Progress = e.ProgressPercentage;
		}

		private void start_download(PremadeForm.Adventure adv, string filename)
		{
			this.fProgressScreen = new ProgressScreen("Downloading Adventure...", 100)
			{
				CurrentSubAction = adv.Name
			};
			this.fProgressScreen.Show();
			WebClient webClient = new WebClient();
			webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.progress_changed);
			webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.download_completed);
			webClient.DownloadFileAsync(new Uri(adv.URL), filename);
			this.fDownloadedFileName = filename;
		}

		private void update_list()
		{
			this.AdventureList.Items.Clear();
			this.AdventureList.Enabled = false;
			if (this.fAdventures == null)
			{
				ListViewItem grayText = this.AdventureList.Items.Add("Downloading adventure list...");
				grayText.ForeColor = SystemColors.GrayText;
				return;
			}
			if (this.fAdventures.Count == 0)
			{
				ListViewItem listViewItem = this.AdventureList.Items.Add("(could not download adventures)");
				listViewItem.ForeColor = SystemColors.GrayText;
			}
			else
			{
				this.AdventureList.Enabled = true;
				foreach (PremadeForm.Adventure fAdventure in this.fAdventures)
				{
					ListViewItem listViewItem1 = this.AdventureList.Items.Add(fAdventure.Name);
					listViewItem1.SubItems.Add(string.Concat("Level ", fAdventure.PartyLevel));
					listViewItem1.SubItems.Add(string.Concat(fAdventure.PartySize, " PCs"));
					listViewItem1.Tag = fAdventure;
				}
			}
		}

		public class Adventure
		{
			public string Name;

			public int PartyLevel;

			public int PartySize;

			public string URL;

			public Adventure()
			{
			}
		}
	}
}