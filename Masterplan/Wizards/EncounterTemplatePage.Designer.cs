using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;
using Utils.Wizards;

namespace Masterplan.Wizards
{
	internal class EncounterTemplatePage : UserControl, IWizardPage
	{
		private Label InfoLbl;

		private ListView TemplatesList;

		private ColumnHeader TemplateHdr;

		private AdviceData fData;

		public bool AllowBack
		{
			get
			{
				return false;
			}
		}

		public bool AllowFinish
		{
			get
			{
				return false;
			}
		}

		public bool AllowNext
		{
			get
			{
				return this.SelectedTemplate != null;
			}
		}

		public EncounterTemplate SelectedTemplate
		{
			get
			{
				if (this.TemplatesList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.TemplatesList.SelectedItems[0].Tag as EncounterTemplate;
			}
		}

		public EncounterTemplatePage()
		{
			this.InitializeComponent();
		}

		private void InitializeComponent()
		{
			this.InfoLbl = new Label();
			this.TemplatesList = new ListView();
			this.TemplateHdr = new ColumnHeader();
			base.SuspendLayout();
			this.InfoLbl.Dock = DockStyle.Top;
			this.InfoLbl.Location = new Point(0, 0);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new System.Drawing.Size(372, 40);
			this.InfoLbl.TabIndex = 1;
			this.InfoLbl.Text = "The following templates fit the creatures you have added to the encounter so far. Select one to continue.";
			this.TemplatesList.Columns.AddRange(new ColumnHeader[] { this.TemplateHdr });
			this.TemplatesList.Dock = DockStyle.Fill;
			this.TemplatesList.FullRowSelect = true;
			this.TemplatesList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.TemplatesList.HideSelection = false;
			this.TemplatesList.Location = new Point(0, 40);
			this.TemplatesList.Name = "TemplatesList";
			this.TemplatesList.Size = new System.Drawing.Size(372, 206);
			this.TemplatesList.TabIndex = 2;
			this.TemplatesList.UseCompatibleStateImageBehavior = false;
			this.TemplatesList.View = View.Details;
			this.TemplateHdr.Text = "Encounter Template";
			this.TemplateHdr.Width = 300;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.TemplatesList);
			base.Controls.Add(this.InfoLbl);
			base.Name = "EncounterTemplatePage";
			base.Size = new System.Drawing.Size(372, 246);
			base.ResumeLayout(false);
		}

		public bool OnBack()
		{
			return true;
		}

		public bool OnFinish()
		{
			return true;
		}

		public bool OnNext()
		{
			if (this.fData.SelectedTemplate != this.SelectedTemplate)
			{
				this.fData.SelectedTemplate = this.SelectedTemplate;
				this.fData.FilledSlots.Clear();
			}
			return true;
		}

		public void OnShown(object data)
		{
			if (this.fData == null)
			{
				this.fData = data as AdviceData;
				if (!this.fData.TabulaRasa)
				{
					this.InfoLbl.Text = "The following encounter templates fit the creatures you have added to the encounter so far. Select one to continue.";
				}
				else
				{
					this.InfoLbl.Text = "The following encounter templates are available. Select one to continue.";
				}
				BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
				foreach (Pair<EncounterTemplateGroup, EncounterTemplate> template in this.fData.Templates)
				{
					binarySearchTree.Add(template.First.Category);
				}
				foreach (string sortedList in binarySearchTree.SortedList)
				{
					this.TemplatesList.Groups.Add(sortedList, sortedList);
				}
				this.TemplatesList.Items.Clear();
				foreach (Pair<EncounterTemplateGroup, EncounterTemplate> pair in this.fData.Templates)
				{
					ListViewItem second = this.TemplatesList.Items.Add(string.Concat(pair.First.Name, " (", pair.Second.Difficulty.ToString().ToLower(), ")"));
					second.Tag = pair.Second;
					second.Group = this.TemplatesList.Groups[pair.First.Category];
				}
				if (this.TemplatesList.Items.Count == 0)
				{
					this.TemplatesList.ShowGroups = false;
					ListViewItem grayText = this.TemplatesList.Items.Add("(no templates)");
					grayText.ForeColor = SystemColors.GrayText;
				}
			}
		}
	}
}