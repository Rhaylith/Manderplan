using Masterplan;
using Masterplan.Data;
using Masterplan.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;
using Utils.Wizards;

namespace Masterplan.Wizards
{
    partial class VariantBasePage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private VariantData fData;

        private Label InfoLbl;

        private ListView CreatureList;

        private ColumnHeader CreatureHdr;

        private ColumnHeader RoleHdr;

        private Panel panel1;

        private ToolStrip toolStrip1;

        private ToolStripLabel SearchLbl;

        private ToolStripTextBox SearchBox;

        private ToolStripLabel SearchClearBtn;

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
                return this.SelectedCreature != null;
            }
        }

        public Creature SelectedCreature
        {
            get
            {
                if (this.CreatureList.SelectedItems.Count == 0)
                {
                    return null;
                }
                return this.CreatureList.SelectedItems[0].Tag as Creature;
            }
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            this.SearchClearBtn.Enabled = this.SearchBox.Text != "";
        }

        private void CreatureList_DoubleClick(object sender, EventArgs e)
        {
            if (this.SelectedCreature != null)
            {
                EncounterCard encounterCard = new EncounterCard(this.SelectedCreature.ID);
                (new CreatureDetailsForm(encounterCard)).ShowDialog();
            }
        }

        private bool match(Creature c, string query)
        {
            string[] strArrays = query.Split(null);
            for (int i = 0; i < (int)strArrays.Length; i++)
            {
                if (!this.match_token(c, strArrays[i]))
                {
                    return false;
                }
            }
            return true;
        }

        private bool match_token(Creature c, string token)
        {
            if (c.Name.ToLower().Contains(token.ToLower()))
            {
                return true;
            }
            if (c.Category != null && c.Category.ToLower().Contains(token.ToLower()))
            {
                return true;
            }
            if (c.Info.ToLower().Contains(token.ToLower()))
            {
                return true;
            }
            if (c.Phenotype.ToLower().Contains(token.ToLower()))
            {
                return true;
            }
            return false;
        }

        public bool OnBack()
        {
            return false;
        }

        public bool OnFinish()
        {
            return false;
        }

        public bool OnNext()
        {
            this.fData.BaseCreature = this.SelectedCreature;
            if (this.fData.BaseCreature.Role is Minion)
            {
                this.fData.Templates.Clear();
            }
            return true;
        }

        public void OnShown(object data)
        {
            if (this.fData == null)
            {
                this.fData = data as VariantData;
                this.update_list();
            }
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            this.update_list();
        }

        private void SearchClearBtn_Click(object sender, EventArgs e)
        {
            this.SearchBox.Text = "";
        }

        private void update_list()
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
            sortedList.Add("Miscellaneous Creatures");
            this.CreatureList.BeginUpdate();
            this.CreatureList.Groups.Clear();
            foreach (string str in sortedList)
            {
                this.CreatureList.Groups.Add(str, str);
            }
            List<ListViewItem> listViewItems = new List<ListViewItem>();
            foreach (Creature creature1 in creatures)
            {
                if (!this.match(creature1, this.SearchBox.Text))
                {
                    continue;
                }
                ListViewItem listViewItem = new ListViewItem(creature1.Name);
                ListViewItem.ListViewSubItemCollection subItems = listViewItem.SubItems;
                object[] level = new object[] { "Level ", creature1.Level, " ", creature1.Role };
                subItems.Add(string.Concat(level));
                if (creature1.Category == null || !(creature1.Category != ""))
                {
                    listViewItem.Group = this.CreatureList.Groups["Miscellaneous Creatures"];
                }
                else
                {
                    listViewItem.Group = this.CreatureList.Groups[creature1.Category];
                }
                listViewItem.Tag = creature1;
                listViewItems.Add(listViewItem);
            }
            this.CreatureList.Items.Clear();
            this.CreatureList.Items.AddRange(listViewItems.ToArray());
            this.CreatureList.EndUpdate();
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.InfoLbl = new Label();
            this.CreatureList = new ListView();
            this.CreatureHdr = new ColumnHeader();
            this.RoleHdr = new ColumnHeader();
            this.panel1 = new Panel();
            this.toolStrip1 = new ToolStrip();
            this.SearchLbl = new ToolStripLabel();
            this.SearchBox = new ToolStripTextBox();
            this.SearchClearBtn = new ToolStripLabel();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.InfoLbl.Dock = DockStyle.Top;
            this.InfoLbl.Location = new Point(0, 0);
            this.InfoLbl.Name = "InfoLbl";
            this.InfoLbl.Size = new System.Drawing.Size(342, 40);
            this.InfoLbl.TabIndex = 0;
            this.InfoLbl.Text = "Select the creature you want to create a variant of.";
            ListView.ColumnHeaderCollection columns = this.CreatureList.Columns;
            ColumnHeader[] creatureHdr = new ColumnHeader[] { this.CreatureHdr, this.RoleHdr };
            columns.AddRange(creatureHdr);
            this.CreatureList.Dock = DockStyle.Fill;
            this.CreatureList.FullRowSelect = true;
            this.CreatureList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.CreatureList.HideSelection = false;
            this.CreatureList.Location = new Point(0, 25);
            this.CreatureList.MultiSelect = false;
            this.CreatureList.Name = "CreatureList";
            this.CreatureList.Size = new System.Drawing.Size(336, 179);
            this.CreatureList.Sorting = SortOrder.Ascending;
            this.CreatureList.TabIndex = 2;
            this.CreatureList.UseCompatibleStateImageBehavior = false;
            this.CreatureList.View = View.Details;
            this.CreatureList.DoubleClick += new EventHandler(this.CreatureList_DoubleClick);
            this.CreatureHdr.Text = "Creature";
            this.CreatureHdr.Width = 150;
            this.RoleHdr.Text = "Role";
            this.RoleHdr.Width = 150;
            this.panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.panel1.Controls.Add(this.CreatureList);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Location = new Point(3, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(336, 204);
            this.panel1.TabIndex = 3;
            ToolStripItemCollection items = this.toolStrip1.Items;
            ToolStripItem[] searchLbl = new ToolStripItem[] { this.SearchLbl, this.SearchBox, this.SearchClearBtn };
            items.AddRange(searchLbl);
            this.toolStrip1.Location = new Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(336, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            this.SearchLbl.Name = "SearchLbl";
            this.SearchLbl.Size = new System.Drawing.Size(45, 22);
            this.SearchLbl.Text = "Search:";
            this.SearchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(200, 25);
            this.SearchBox.TextChanged += new EventHandler(this.SearchBox_TextChanged);
            this.SearchClearBtn.IsLink = true;
            this.SearchClearBtn.Name = "SearchClearBtn";
            this.SearchClearBtn.Size = new System.Drawing.Size(34, 22);
            this.SearchClearBtn.Text = "Clear";
            this.SearchClearBtn.Click += new EventHandler(this.SearchClearBtn_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.InfoLbl);
            base.Name = "VariantBasePage";
            base.Size = new System.Drawing.Size(342, 250);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            base.ResumeLayout(false);
        }

        #endregion
    }
}
