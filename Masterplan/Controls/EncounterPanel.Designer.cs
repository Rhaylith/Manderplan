using Masterplan;
using Masterplan.Data;
using Masterplan.Tools;
using Masterplan.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.Controls
{
    partial class EncounterPanel
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

        private ToolStrip Toolbar;

        private ToolStripButton EditBtn;

        private ToolStripSeparator toolStripSeparator2;

        private ToolStripLabel XPLbl;

        private ToolStripLabel DiffLbl;

        private ListView ItemList;

        private ColumnHeader CreatureHdr;

        private ColumnHeader CountHdr;

        private ColumnHeader RoleHdr;

        private ColumnHeader XPHdr;

        private ToolStripButton RunBtn;

        private Masterplan.Data.Encounter fEncounter;

        private int fPartyLevel = Session.Project.Party.Level;

        public Masterplan.Data.Encounter Encounter
        {
            get
            {
                return this.fEncounter;
            }
            set
            {
                this.fEncounter = value;
                this.update_view();
            }
        }

        public int PartyLevel
        {
            get
            {
                return this.fPartyLevel;
            }
            set
            {
                this.fPartyLevel = value;
                this.update_view();
            }
        }

        public SkillChallenge SelectedChallenge
        {
            get
            {
                if (this.ItemList.SelectedItems.Count == 0)
                {
                    return null;
                }
                return this.ItemList.SelectedItems[0].Tag as SkillChallenge;
            }
        }

        public EncounterSlot SelectedSlot
        {
            get
            {
                if (this.ItemList.SelectedItems.Count == 0)
                {
                    return null;
                }
                return this.ItemList.SelectedItems[0].Tag as EncounterSlot;
            }
        }

        public Trap SelectedTrap
        {
            get
            {
                if (this.ItemList.SelectedItems.Count == 0)
                {
                    return null;
                }
                return this.ItemList.SelectedItems[0].Tag as Trap;
            }
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            this.RunBtn.Enabled = (this.fEncounter.Count != 0 || this.fEncounter.Traps.Count != 0 ? true : this.fEncounter.SkillChallenges.Count != 0);
        }

        private void CreatureList_DoubleClick(object sender, EventArgs e)
        {
            if (this.SelectedSlot != null)
            {
                (new CreatureDetailsForm(this.SelectedSlot.Card)).ShowDialog();
            }
            if (this.SelectedTrap != null)
            {
                (new TrapDetailsForm(this.SelectedTrap)).ShowDialog();
            }
            if (this.SelectedChallenge != null)
            {
                (new SkillChallengeDetailsForm(this.SelectedChallenge)).ShowDialog();
            }
        }

        public void Edit()
        {
            this.EditBtn_Click(null, null);
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            EncounterBuilderForm encounterBuilderForm = new EncounterBuilderForm(this.fEncounter, this.fPartyLevel, false);
            if (encounterBuilderForm.ShowDialog() == DialogResult.OK)
            {
                this.fEncounter.Slots = encounterBuilderForm.Encounter.Slots;
                this.fEncounter.Traps = encounterBuilderForm.Encounter.Traps;
                this.fEncounter.SkillChallenges = encounterBuilderForm.Encounter.SkillChallenges;
                this.fEncounter.CustomTokens = encounterBuilderForm.Encounter.CustomTokens;
                this.fEncounter.MapID = encounterBuilderForm.Encounter.MapID;
                this.fEncounter.MapAreaID = encounterBuilderForm.Encounter.MapAreaID;
                this.fEncounter.Notes = encounterBuilderForm.Encounter.Notes;
                this.fEncounter.Waves = encounterBuilderForm.Encounter.Waves;
                this.update_view();
            }
        }

        private void RunBtn_Click(object sender, EventArgs e)
        {
            CombatState combatState = new CombatState()
            {
                Encounter = this.fEncounter,
                PartyLevel = this.fPartyLevel
            };
            (new CombatForm(combatState)).Show();
        }

        private void update_view()
        {
            this.ItemList.Items.Clear();
            foreach (EncounterSlot slot in this.fEncounter.Slots)
            {
                ListViewItem green = this.ItemList.Items.Add(slot.Card.Title);
                green.SubItems.Add(slot.Card.Info);
                ListViewItem.ListViewSubItemCollection subItems = green.SubItems;
                int count = slot.CombatData.Count;
                subItems.Add(count.ToString());
                green.SubItems.Add(slot.XP.ToString());
                green.Tag = slot;
                ICreature creature = Session.FindCreature(slot.Card.CreatureID, SearchType.Global);
                Difficulty threatDifficulty = AI.GetThreatDifficulty(creature.Level + slot.Card.LevelAdjustment, this.fPartyLevel);
                if (threatDifficulty == Difficulty.Trivial)
                {
                    green.ForeColor = Color.Green;
                }
                if (threatDifficulty != Difficulty.Extreme)
                {
                    continue;
                }
                green.ForeColor = Color.Red;
            }
            foreach (Trap trap in this.fEncounter.Traps)
            {
                ListViewItem listViewItem = this.ItemList.Items.Add(trap.Name);
                listViewItem.SubItems.Add(trap.Info);
                listViewItem.SubItems.Add("1");
                ListViewItem.ListViewSubItemCollection listViewSubItemCollections = listViewItem.SubItems;
                int creatureXP = Experience.GetCreatureXP(trap.Level);
                listViewSubItemCollections.Add(creatureXP.ToString());
                listViewItem.Tag = trap;
            }
            foreach (SkillChallenge skillChallenge in this.fEncounter.SkillChallenges)
            {
                ListViewItem listViewItem1 = this.ItemList.Items.Add(skillChallenge.Name);
                listViewItem1.SubItems.Add(skillChallenge.Info);
                listViewItem1.SubItems.Add("1");
                ListViewItem.ListViewSubItemCollection subItems1 = listViewItem1.SubItems;
                int xP = skillChallenge.GetXP();
                subItems1.Add(xP.ToString());
                listViewItem1.Tag = skillChallenge;
            }
            if (this.ItemList.Items.Count == 0)
            {
                ListViewItem grayText = this.ItemList.Items.Add("(none)");
                grayText.ForeColor = SystemColors.GrayText;
            }
            this.ItemList.Sort();
            this.XPLbl.Text = string.Concat(this.fEncounter.GetXP(), " XP");
            this.DiffLbl.Text = string.Concat("Difficulty: ", this.fEncounter.GetDifficulty(this.fPartyLevel, Session.Project.Party.Size));
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            //components = new System.ComponentModel.Container();
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.Toolbar = new ToolStrip();
            this.EditBtn = new ToolStripButton();
            this.RunBtn = new ToolStripButton();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.XPLbl = new ToolStripLabel();
            this.DiffLbl = new ToolStripLabel();
            this.ItemList = new ListView();
            this.CreatureHdr = new ColumnHeader();
            this.CountHdr = new ColumnHeader();
            this.RoleHdr = new ColumnHeader();
            this.XPHdr = new ColumnHeader();
            this.Toolbar.SuspendLayout();
            base.SuspendLayout();
            ToolStripItemCollection items = this.Toolbar.Items;
            ToolStripItem[] editBtn = new ToolStripItem[] { this.EditBtn, this.RunBtn, this.toolStripSeparator2, this.XPLbl, this.DiffLbl };
            items.AddRange(editBtn);
            this.Toolbar.Location = new Point(0, 0);
            this.Toolbar.Name = "Toolbar";
            this.Toolbar.Size = new System.Drawing.Size(435, 25);
            this.Toolbar.TabIndex = 0;
            this.Toolbar.Text = "toolStrip1";
            this.EditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.EditBtn.ImageTransparentColor = Color.Magenta;
            this.EditBtn.Name = "EditBtn";
            this.EditBtn.Size = new System.Drawing.Size(105, 22);
            this.EditBtn.Text = "Encounter Builder";
            this.EditBtn.Click += new EventHandler(this.EditBtn_Click);
            this.RunBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.RunBtn.ImageTransparentColor = Color.Magenta;
            this.RunBtn.Name = "RunBtn";
            this.RunBtn.Size = new System.Drawing.Size(89, 22);
            this.RunBtn.Text = "Run Encounter";
            this.RunBtn.Click += new EventHandler(this.RunBtn_Click);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            this.XPLbl.Name = "XPLbl";
            this.XPLbl.Size = new System.Drawing.Size(78, 22);
            this.XPLbl.Text = "[N XP (CL N)]";
            this.DiffLbl.Name = "DiffLbl";
            this.DiffLbl.Size = new System.Drawing.Size(62, 22);
            this.DiffLbl.Text = "[difficulty]";
            ListView.ColumnHeaderCollection columns = this.ItemList.Columns;
            ColumnHeader[] creatureHdr = new ColumnHeader[] { this.CreatureHdr, this.RoleHdr, this.CountHdr, this.XPHdr };
            columns.AddRange(creatureHdr);
            this.ItemList.Dock = DockStyle.Fill;
            this.ItemList.FullRowSelect = true;
            this.ItemList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.ItemList.HideSelection = false;
            this.ItemList.Location = new Point(0, 25);
            this.ItemList.MultiSelect = false;
            this.ItemList.Name = "ItemList";
            this.ItemList.Size = new System.Drawing.Size(435, 181);
            this.ItemList.TabIndex = 1;
            this.ItemList.UseCompatibleStateImageBehavior = false;
            this.ItemList.View = View.Details;
            this.ItemList.DoubleClick += new EventHandler(this.CreatureList_DoubleClick);
            this.CreatureHdr.Text = "Creature";
            this.CreatureHdr.Width = 150;
            this.CountHdr.Text = "Count";
            this.CountHdr.TextAlign = HorizontalAlignment.Right;
            this.RoleHdr.Text = "Role";
            this.RoleHdr.Width = 150;
            this.XPHdr.Text = "XP";
            this.XPHdr.TextAlign = HorizontalAlignment.Right;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.ItemList);
            base.Controls.Add(this.Toolbar);
            base.Name = "EncounterPanel";
            base.Size = new System.Drawing.Size(435, 206);
            this.Toolbar.ResumeLayout(false);
            this.Toolbar.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();

        }

        #endregion
    }
}
