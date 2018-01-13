using Masterplan.Data;
using Masterplan.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils.Wizards;

namespace Masterplan.Wizards
{
    partial class EncounterSelectionPage
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

        private AdviceData fData;

        private Label InfoLbl;

        private ListView SlotList;

        private ColumnHeader SlotHdr;

        private ColumnHeader CardHdr;

        public bool AllowBack
        {
            get
            {
                return true;
            }
        }

        public bool AllowFinish
        {
            get
            {
                bool flag;
                List<EncounterTemplateSlot>.Enumerator enumerator = this.fData.SelectedTemplate.Slots.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        EncounterTemplateSlot current = enumerator.Current;
                        if (this.fData.FilledSlots.ContainsKey(current))
                        {
                            continue;
                        }
                        flag = false;
                        return flag;
                    }
                    return true;
                }
                finally
                {
                    ((IDisposable)enumerator).Dispose();
                }
            }
        }

        public bool AllowNext
        {
            get
            {
                return false;
            }
        }

        public EncounterTemplateSlot SelectedSlot
        {
            get
            {
                if (this.SlotList.SelectedItems.Count == 0)
                {
                    return null;
                }
                return this.SlotList.SelectedItems[0].Tag as EncounterTemplateSlot;
            }
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
            return true;
        }

        public void OnShown(object data)
        {
            if (this.fData == null)
            {
                this.fData = data as AdviceData;
            }
            this.update_list();
        }

        private string slot_info(EncounterTemplateSlot slot)
        {
            int partyLevel = this.fData.PartyLevel + slot.LevelAdjustment;
            string str = (slot.Flag != RoleFlag.Standard ? string.Concat(" ", slot.Flag) : "");
            string str1 = "";
            foreach (RoleType role in slot.Roles)
            {
                if (str1 != "")
                {
                    str1 = string.Concat(str1, " / ");
                }
                str1 = string.Concat(str1, role.ToString().ToLower());
            }
            if (str1 == "")
            {
                str1 = "any role";
            }
            if (slot.Minions)
            {
                str1 = string.Concat(str1, ", minion");
            }
            string str2 = "";
            if (slot.Count != 1)
            {
                str2 = string.Concat(" (x", slot.Count, ")");
            }
            object[] objArray = new object[] { "Level ", partyLevel, str, " ", str1, str2 };
            return string.Concat(objArray);
        }

        private void SlotList_DoubleClick(object sender, EventArgs e)
        {
            if (this.SelectedSlot != null)
            {
                CreatureSelectForm creatureSelectForm = new CreatureSelectForm(this.SelectedSlot, this.fData.PartyLevel);
                if (creatureSelectForm.ShowDialog() == DialogResult.OK)
                {
                    this.fData.FilledSlots[this.SelectedSlot] = creatureSelectForm.Creature;
                    this.update_list();
                }
            }
        }

        private void update_list()
        {
            this.SlotList.Items.Clear();
            foreach (EncounterTemplateSlot slot in this.fData.SelectedTemplate.Slots)
            {
                ListViewItem listViewItem = this.SlotList.Items.Add(this.slot_info(slot));
                if (!this.fData.FilledSlots.ContainsKey(slot))
                {
                    listViewItem.SubItems.Add("(not filled)");
                }
                else
                {
                    listViewItem.SubItems.Add(this.fData.FilledSlots[slot].Title);
                }
                listViewItem.Tag = slot;
            }
            if (this.SlotList.Items.Count == 0)
            {
                ListViewItem grayText = this.SlotList.Items.Add("(no unused slots)");
                grayText.ForeColor = SystemColors.GrayText;
            }
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.InfoLbl = new Label();
            this.SlotList = new ListView();
            this.SlotHdr = new ColumnHeader();
            this.CardHdr = new ColumnHeader();
            base.SuspendLayout();
            this.InfoLbl.Dock = DockStyle.Top;
            this.InfoLbl.Location = new Point(0, 0);
            this.InfoLbl.Name = "InfoLbl";
            this.InfoLbl.Size = new System.Drawing.Size(372, 40);
            this.InfoLbl.TabIndex = 1;
            this.InfoLbl.Text = "Double-click on each of the empty slots in the list below to select creatures to fill them.";
            ListView.ColumnHeaderCollection columns = this.SlotList.Columns;
            ColumnHeader[] slotHdr = new ColumnHeader[] { this.SlotHdr, this.CardHdr };
            columns.AddRange(slotHdr);
            this.SlotList.Dock = DockStyle.Fill;
            this.SlotList.FullRowSelect = true;
            this.SlotList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.SlotList.HideSelection = false;
            this.SlotList.Location = new Point(0, 40);
            this.SlotList.Name = "SlotList";
            this.SlotList.Size = new System.Drawing.Size(372, 206);
            this.SlotList.TabIndex = 2;
            this.SlotList.UseCompatibleStateImageBehavior = false;
            this.SlotList.View = View.Details;
            this.SlotList.DoubleClick += new EventHandler(this.SlotList_DoubleClick);
            this.SlotHdr.Text = "Slot";
            this.SlotHdr.Width = 160;
            this.CardHdr.Text = "Selected Creature";
            this.CardHdr.Width = 160;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.SlotList);
            base.Controls.Add(this.InfoLbl);
            base.Name = "EncounterSelectionPage";
            base.Size = new System.Drawing.Size(372, 246);
            base.ResumeLayout(false);
        }

        #endregion
    }
}
