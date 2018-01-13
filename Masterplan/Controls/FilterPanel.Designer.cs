using Masterplan;
using Masterplan.Data;
using Masterplan.Tools;
using Masterplan.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Masterplan.Controls
{
    partial class FilterPanel
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

        private ListMode fMode;

        private int fPartyLevel;

        private string[] fNameTokens;

        private string[] fCatTokens;

        private string[] fKeyTokens;

        private bool fSuppressEvents;

        private CheckBox FilterLevelToggle;

        private CheckBox FilterModToggle;

        private ComboBox FilterModBox;

        private CheckBox FilterNameToggle;

        private CheckBox FilterCatToggle;

        private TextBox FilterKeywordBox;

        private CheckBox FilterKeywordToggle;

        private CheckBox FilterTypeToggle;

        private ComboBox FilterTypeBox;

        private CheckBox FilterOriginToggle;

        private ComboBox FilterOriginBox;

        private TextBox FilterCatBox;

        private CheckBox FilterRoleToggle;

        private ComboBox FilterRoleBox;

        private TextBox FilterNameBox;

        private Panel FilterPnl;

        private NumericUpDown LevelToBox;

        private NumericUpDown LevelFromBox;

        private Label ToLbl;

        private Label FromLbl;

        private CheckBox FilterLevelAppropriateToggle;

        private ToolStripStatusLabel InfoLbl;

        private ToolStripStatusLabel EditLbl;

        private StatusStrip Statusbar;

        private ComboBox FilterSourceBox;

        private CheckBox FilterSourceToggle;

        public ListMode Mode
        {
            get
            {
                return this.fMode;
            }
            set
            {
                this.fMode = value;
                this.update_allowed_filters();
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
            }
        }

        public bool AllowItem(object obj, out Difficulty diff)
        {
            bool flag;
            string[] strArrays;
            int num;
            diff = Difficulty.Moderate;
            if (obj is ICreature)
            {
                ICreature creature = obj as ICreature;
                bool flag1 = false;
                diff = AI.GetThreatDifficulty(creature.Level, this.fPartyLevel);
                if ((int)diff == 1 || (int)diff == 5)
                {
                    flag1 = true;
                }
                if (flag1 && this.FilterLevelAppropriateToggle.Checked)
                {
                    return false;
                }
                if (this.FilterNameToggle.Checked && this.fNameTokens != null)
                {
                    string lower = creature.Name.ToLower();
                    strArrays = this.fNameTokens;
                    num = 0;
                    while (num < (int)strArrays.Length)
                    {
                        if (lower.Contains(strArrays[num]))
                        {
                            num++;
                        }
                        else
                        {
                            flag = false;
                            return flag;
                        }
                    }
                }
                if (this.FilterCatToggle.Checked && this.fCatTokens != null)
                {
                    string str = creature.Category.ToLower();
                    string[] strArrays1 = this.fCatTokens;
                    int num1 = 0;
                    while (num1 < (int)strArrays1.Length)
                    {
                        if (str.Contains(strArrays1[num1]))
                        {
                            num1++;
                        }
                        else
                        {
                            flag = false;
                            return flag;
                        }
                    }
                }
                if (this.FilterRoleToggle.Checked)
                {
                    RoleType selectedItem = (RoleType)this.FilterRoleBox.SelectedItem;
                    if (creature.Role is ComplexRole && (creature.Role as ComplexRole).Type != selectedItem)
                    {
                        return false;
                    }
                    if (creature.Role is Minion)
                    {
                        Minion role = creature.Role as Minion;
                        if (!role.HasRole || role.Type != selectedItem)
                        {
                            return false;
                        }
                    }
                }
                if (this.FilterModToggle.Checked)
                {
                    RoleFlag roleFlag = RoleFlag.Standard;
                    bool flag2 = false;
                    if (this.FilterModBox.Text == "Elite")
                    {
                        roleFlag = RoleFlag.Elite;
                    }
                    if (this.FilterModBox.Text == "Solo")
                    {
                        roleFlag = RoleFlag.Solo;
                    }
                    if (this.FilterModBox.Text == "Minion")
                    {
                        flag2 = true;
                    }
                    ComplexRole complexRole = creature.Role as ComplexRole;
                    if (complexRole != null)
                    {
                        if (flag2)
                        {
                            return false;
                        }
                        if (roleFlag != complexRole.Flag)
                        {
                            return false;
                        }
                    }
                    if (creature.Role is Minion && !flag2)
                    {
                        return false;
                    }
                }
                if (this.FilterOriginToggle.Checked)
                {
                    CreatureOrigin creatureOrigin = (CreatureOrigin)this.FilterOriginBox.SelectedItem;
                    if (creature.Origin != creatureOrigin)
                    {
                        return false;
                    }
                }
                if (this.FilterTypeToggle.Checked)
                {
                    CreatureType creatureType = (CreatureType)this.FilterTypeBox.SelectedItem;
                    if (creature.Type != creatureType)
                    {
                        return false;
                    }
                }
                if (this.FilterKeywordToggle.Checked && this.fKeyTokens != null)
                {
                    string str1 = (creature.Keywords != null ? creature.Keywords.ToLower() : "");
                    string[] strArrays2 = this.fKeyTokens;
                    int num2 = 0;
                    while (num2 < (int)strArrays2.Length)
                    {
                        if (str1.Contains(strArrays2[num2]))
                        {
                            num2++;
                        }
                        else
                        {
                            flag = false;
                            return flag;
                        }
                    }
                }
                if (this.FilterLevelToggle.Checked && (creature.Level < this.LevelFromBox.Value || creature.Level > this.LevelToBox.Value))
                {
                    return false;
                }
                if (this.FilterSourceToggle.Checked)
                {
                    Creature creature1 = creature as Creature;
                    if (creature1 == null)
                    {
                        return false;
                    }
                    if (!(this.FilterSourceBox.SelectedItem as Library).Creatures.Contains(creature1))
                    {
                        return false;
                    }
                }
                return true;
            }
            else if (obj is CreatureTemplate)
            {
                CreatureTemplate creatureTemplate = obj as CreatureTemplate;
                if (this.FilterNameToggle.Checked && this.fNameTokens != null)
                {
                    string lower1 = creatureTemplate.Name.ToLower();
                    string[] strArrays3 = this.fNameTokens;
                    num = 0;
                    while (num < (int)strArrays3.Length)
                    {
                        if (lower1.Contains(strArrays3[num]))
                        {
                            num++;
                        }
                        else
                        {
                            flag = false;
                            return flag;
                        }
                    }
                }
                if (this.FilterCatToggle.Checked)
                {
                    string[] strArrays4 = this.fCatTokens;
                }
                if (this.FilterRoleToggle.Checked)
                {
                    RoleType roleType = (RoleType)this.FilterRoleBox.SelectedItem;
                    if (creatureTemplate.Role != roleType)
                    {
                        return false;
                    }
                }
                bool @checked = this.FilterOriginToggle.Checked;
                bool checked1 = this.FilterTypeToggle.Checked;
                if (this.FilterKeywordToggle.Checked)
                {
                    string[] strArrays5 = this.fKeyTokens;
                }
                if (this.FilterSourceToggle.Checked && !(this.FilterSourceBox.SelectedItem as Library).Templates.Contains(creatureTemplate))
                {
                    return false;
                }
                return true;
            }
            else if (obj is NPC)
            {
                NPC nPC = obj as NPC;
                bool flag3 = false;
                diff = AI.GetThreatDifficulty(nPC.Level, this.fPartyLevel);
                if ((int)diff == 1 || (int)diff == 5)
                {
                    flag3 = true;
                }
                if (flag3 && this.FilterLevelAppropriateToggle.Checked)
                {
                    return false;
                }
                if (this.FilterNameToggle.Checked && this.fNameTokens != null)
                {
                    string lower2 = nPC.Name.ToLower();
                    strArrays = this.fNameTokens;
                    num = 0;
                    while (num < (int)strArrays.Length)
                    {
                        if (lower2.Contains(strArrays[num]))
                        {
                            num++;
                        }
                        else
                        {
                            flag = false;
                            return flag;
                        }
                    }
                }
                if (this.FilterCatToggle.Checked && this.fCatTokens != null)
                {
                    string str2 = nPC.Category.ToLower();
                    strArrays = this.fCatTokens;
                    num = 0;
                    while (num < (int)strArrays.Length)
                    {
                        if (str2.Contains(strArrays[num]))
                        {
                            num++;
                        }
                        else
                        {
                            flag = false;
                            return flag;
                        }
                    }
                }
                if (this.FilterRoleToggle.Checked)
                {
                    RoleType selectedItem1 = (RoleType)this.FilterRoleBox.SelectedItem;
                    if (nPC.Role is ComplexRole && (nPC.Role as ComplexRole).Type != selectedItem1)
                    {
                        return false;
                    }
                    if (nPC.Role is Minion)
                    {
                        Minion minion = nPC.Role as Minion;
                        if (!minion.HasRole || minion.Type != selectedItem1)
                        {
                            return false;
                        }
                    }
                }
                if (this.FilterOriginToggle.Checked)
                {
                    CreatureOrigin creatureOrigin1 = (CreatureOrigin)this.FilterOriginBox.SelectedItem;
                    if (nPC.Origin != creatureOrigin1)
                    {
                        return false;
                    }
                }
                if (this.FilterTypeToggle.Checked)
                {
                    CreatureType creatureType1 = (CreatureType)this.FilterTypeBox.SelectedItem;
                    if (nPC.Type != creatureType1)
                    {
                        return false;
                    }
                }
                if (this.FilterKeywordToggle.Checked && this.fKeyTokens != null)
                {
                    string str3 = (nPC.Keywords != null ? nPC.Keywords.ToLower() : "");
                    strArrays = this.fKeyTokens;
                    num = 0;
                    while (num < (int)strArrays.Length)
                    {
                        if (str3.Contains(strArrays[num]))
                        {
                            num++;
                        }
                        else
                        {
                            flag = false;
                            return flag;
                        }
                    }
                }
                if (this.FilterLevelToggle.Checked && (nPC.Level < this.LevelFromBox.Value || nPC.Level > this.LevelToBox.Value))
                {
                    return false;
                }
                return true;
            }
            else if (!(obj is Trap))
            {
                if (!(obj is SkillChallenge))
                {
                    return false;
                }
                SkillChallenge skillChallenge = obj as SkillChallenge;
                if (this.FilterNameToggle.Checked && this.fNameTokens != null)
                {
                    string lower3 = skillChallenge.Name.ToLower();
                    strArrays = this.fNameTokens;
                    num = 0;
                    while (num < (int)strArrays.Length)
                    {
                        if (lower3.Contains(strArrays[num]))
                        {
                            num++;
                        }
                        else
                        {
                            flag = false;
                            return flag;
                        }
                    }
                }
                if (this.FilterCatToggle.Checked)
                {
                    string[] strArrays6 = this.fCatTokens;
                }
                bool checked2 = this.FilterRoleToggle.Checked;
                bool checked3 = this.FilterOriginToggle.Checked;
                bool checked4 = this.FilterTypeToggle.Checked;
                if (this.FilterKeywordToggle.Checked)
                {
                    string[] strArrays7 = this.fKeyTokens;
                }
                if (this.FilterSourceToggle.Checked && !(this.FilterSourceBox.SelectedItem as Library).SkillChallenges.Contains(skillChallenge))
                {
                    return false;
                }
                return true;
            }
            else
            {
                Trap trap = obj as Trap;
                bool flag4 = false;
                diff = AI.GetThreatDifficulty(trap.Level, this.fPartyLevel);
                if ((int)diff == 1 || (int)diff == 5)
                {
                    flag4 = true;
                }
                if (flag4 && this.FilterLevelAppropriateToggle.Checked)
                {
                    return false;
                }
                if (this.FilterNameToggle.Checked && this.fNameTokens != null)
                {
                    string lower4 = trap.Name.ToLower();
                    strArrays = this.fNameTokens;
                    num = 0;
                    while (num < (int)strArrays.Length)
                    {
                        if (lower4.Contains(strArrays[num]))
                        {
                            num++;
                        }
                        else
                        {
                            flag = false;
                            return flag;
                        }
                    }
                }
                if (this.FilterCatToggle.Checked)
                {
                    string[] strArrays8 = this.fCatTokens;
                }
                if (this.FilterRoleToggle.Checked)
                {
                    RoleType roleType1 = (RoleType)this.FilterRoleBox.SelectedItem;
                    if (trap.Role is ComplexRole && (trap.Role as ComplexRole).Type != roleType1)
                    {
                        return false;
                    }
                    if (trap.Role is Minion)
                    {
                        Minion role1 = trap.Role as Minion;
                        if (!role1.HasRole || role1.Type != roleType1)
                        {
                            return false;
                        }
                    }
                }
                if (this.FilterModToggle.Checked)
                {
                    RoleFlag roleFlag1 = RoleFlag.Standard;
                    bool flag5 = false;
                    if (this.FilterModBox.Text == "Elite")
                    {
                        roleFlag1 = RoleFlag.Elite;
                    }
                    if (this.FilterModBox.Text == "Solo")
                    {
                        roleFlag1 = RoleFlag.Solo;
                    }
                    if (this.FilterModBox.Text == "Minion")
                    {
                        flag5 = true;
                    }
                    ComplexRole complexRole1 = trap.Role as ComplexRole;
                    if (complexRole1 != null)
                    {
                        if (flag5)
                        {
                            return false;
                        }
                        if (roleFlag1 != complexRole1.Flag)
                        {
                            return false;
                        }
                    }
                    if (trap.Role is Minion && !flag5)
                    {
                        return false;
                    }
                }
                bool checked5 = this.FilterOriginToggle.Checked;
                bool checked6 = this.FilterTypeToggle.Checked;
                if (this.FilterKeywordToggle.Checked)
                {
                    string[] strArrays9 = this.fKeyTokens;
                }
                if (this.FilterLevelToggle.Checked && (trap.Level < this.LevelFromBox.Value || trap.Level > this.LevelToBox.Value))
                {
                    return false;
                }
                if (this.FilterSourceToggle.Checked && !(this.FilterSourceBox.SelectedItem as Library).Traps.Contains(trap))
                {
                    return false;
                }
                return true;
            }
        }

        public void Collapse()
        {
            this.open_close_editor(false);
        }

        private void DropdownOptionChanged(object sender, EventArgs e)
        {
            this.OnFilterChanged();
        }

        private void EditLbl_Click(object sender, EventArgs e)
        {
            this.open_close_editor(!this.FilterPnl.Visible);
        }

        public void Expand()
        {
            this.open_close_editor(true);
        }

        public void FilterByPartyLevel()
        {
            this.FilterLevelAppropriateToggle.Checked = true;
            this.OnFilterChanged();
            if (!this.FilterPnl.Visible)
            {
                this.InfoLbl.Text = this.get_filter_string();
            }
        }

        private string get_filter_string()
        {
            string str = "";
            if (this.FilterNameToggle.Checked && this.FilterNameToggle.Enabled && this.FilterNameBox.Text != "")
            {
                if (str != "")
                {
                    str = string.Concat(str, "; ");
                }
                str = string.Concat(str, "Name: ", this.FilterNameBox.Text);
            }
            if (this.FilterCatToggle.Checked && this.FilterCatToggle.Enabled && this.FilterCatBox.Text != "")
            {
                if (str != "")
                {
                    str = string.Concat(str, "; ");
                }
                str = string.Concat(str, "Category: ", this.FilterCatBox.Text);
            }
            string str1 = "";
            if (this.FilterModToggle.Checked && this.FilterModToggle.Enabled)
            {
                str1 = string.Concat(str1, this.FilterModBox.SelectedItem);
            }
            if (this.FilterRoleToggle.Checked && this.FilterRoleToggle.Enabled)
            {
                if (str1 != "")
                {
                    str1 = string.Concat(str1, " ");
                }
                str1 = string.Concat(str1, this.FilterRoleBox.SelectedItem);
            }
            if (str1 != "")
            {
                if (str != "")
                {
                    str = string.Concat(str, "; ");
                }
                str = string.Concat(str, "Role: ", str1);
            }
            if (this.FilterTypeToggle.Checked && this.FilterTypeToggle.Enabled)
            {
                if (str != "")
                {
                    str = string.Concat(str, "; ");
                }
                str = string.Concat(str, "Type: ", this.FilterTypeBox.SelectedItem);
            }
            if (this.FilterOriginToggle.Checked && this.FilterOriginToggle.Enabled)
            {
                if (str != "")
                {
                    str = string.Concat(str, "; ");
                }
                str = string.Concat(str, "Origin: ", this.FilterOriginBox.SelectedItem);
            }
            if (this.FilterKeywordToggle.Checked && this.FilterKeywordToggle.Enabled && this.FilterKeywordBox.Text != "")
            {
                if (str != "")
                {
                    str = string.Concat(str, "; ");
                }
                str = string.Concat(str, "Keywords: ", this.FilterKeywordBox.Text);
            }
            if (this.FilterLevelToggle.Checked && this.FilterLevelToggle.Enabled)
            {
                if (str != "")
                {
                    str = string.Concat(str, "; ");
                }
                int value = (int)this.LevelFromBox.Value;
                int num = (int)this.LevelToBox.Value;
                if (value != num)
                {
                    object obj = str;
                    object[] objArray = new object[] { obj, "Level: ", value, "-", num };
                    str = string.Concat(objArray);
                }
                else
                {
                    str = string.Concat(str, "Level: ", value);
                }
            }
            if (this.FilterLevelAppropriateToggle.Checked && this.FilterLevelAppropriateToggle.Enabled)
            {
                if (str != "")
                {
                    str = string.Concat(str, "; ");
                }
                str = string.Concat(str, "Level-appropriate only");
            }
            if (this.FilterSourceToggle.Checked && this.FilterSourceToggle.Enabled)
            {
                if (str != "")
                {
                    str = string.Concat(str, "; ");
                }
                str = string.Concat(str, "Source: ", this.FilterSourceBox.SelectedItem);
            }
            return str;
        }

        private void NumericOptionChanged(object sender, EventArgs e)
        {
            if (sender == this.LevelFromBox)
            {
                this.LevelToBox.Minimum = this.LevelFromBox.Value;
            }
            if (sender == this.LevelToBox)
            {
                this.LevelFromBox.Maximum = this.LevelToBox.Value;
            }
            this.OnFilterChanged();
        }

        protected void OnFilterChanged()
        {
            if (this.fSuppressEvents)
            {
                return;
            }
            if (this.FilterChanged != null)
            {
                this.FilterChanged(this, new EventArgs());
            }
        }

        private void open_close_editor(bool open)
        {
            if (open)
            {
                this.FilterPnl.Visible = true;
                this.InfoLbl.Text = "";
                this.EditLbl.Text = "Collapse";
                return;
            }
            this.FilterPnl.Visible = false;
            this.InfoLbl.Text = this.get_filter_string();
            this.EditLbl.Text = "Expand";
        }

        public bool SetFilter(int level, IRole role)
        {
            this.fSuppressEvents = true;
            bool flag = false;
            if (level != 0)
            {
                this.FilterLevelToggle.Checked = true;
                this.LevelFromBox.Value = level;
                this.LevelToBox.Value = level;
                flag = true;
            }
            if (role != null)
            {
                if (role is ComplexRole)
                {
                    ComplexRole complexRole = role as ComplexRole;
                    this.FilterRoleToggle.Checked = true;
                    this.FilterRoleBox.SelectedItem = complexRole.Type;
                    this.FilterModToggle.Checked = true;
                    this.FilterModBox.SelectedItem = complexRole.Flag.ToString();
                    flag = true;
                }
                if (role is Minion)
                {
                    Minion minion = role as Minion;
                    if (minion.HasRole)
                    {
                        this.FilterRoleToggle.Checked = true;
                        this.FilterRoleBox.SelectedItem = minion.Type;
                    }
                    this.FilterModToggle.Checked = true;
                    this.FilterModBox.SelectedItem = "Minion";
                    flag = true;
                }
            }
            this.fSuppressEvents = false;
            if (flag)
            {
                this.update_option_state();
                this.OnFilterChanged();
                if (!this.FilterPnl.Visible)
                {
                    this.InfoLbl.Text = this.get_filter_string();
                }
            }
            return flag;
        }

        private void TextOptionChanged(object sender, EventArgs e)
        {
            this.fNameTokens = this.FilterNameBox.Text.ToLower().Split(null);
            if ((int)this.fNameTokens.Length == 0)
            {
                this.fNameTokens = null;
            }
            this.fCatTokens = this.FilterCatBox.Text.ToLower().Split(null);
            if ((int)this.fCatTokens.Length == 0)
            {
                this.fCatTokens = null;
            }
            this.fKeyTokens = this.FilterKeywordBox.Text.ToLower().Split(null);
            if ((int)this.fKeyTokens.Length == 0)
            {
                this.fKeyTokens = null;
            }
            this.OnFilterChanged();
        }

        private void ToggleChanged(object sender, EventArgs e)
        {
            this.update_option_state();
            if (sender == this.FilterNameToggle && this.FilterNameBox.Text == "")
            {
                return;
            }
            if (sender == this.FilterCatToggle && this.FilterCatBox.Text == "")
            {
                return;
            }
            if (sender == this.FilterKeywordToggle && this.FilterKeywordBox.Text == "")
            {
                return;
            }
            if (sender == this.FilterLevelToggle)
            {
                this.FilterLevelAppropriateToggle.Checked = false;
            }
            if (sender == this.FilterLevelAppropriateToggle)
            {
                this.FilterLevelToggle.Checked = false;
            }
            this.OnFilterChanged();
        }

        private void update_allowed_filters()
        {
            bool flag;
            bool flag1;
            this.FilterCatToggle.Enabled = (this.fMode == ListMode.Creatures ? true : this.fMode == ListMode.NPCs);
            this.FilterRoleToggle.Enabled = this.fMode != ListMode.SkillChallenges;
            this.FilterModToggle.Enabled = (this.fMode == ListMode.Creatures ? true : this.fMode == ListMode.Traps);
            this.FilterOriginToggle.Enabled = (this.fMode == ListMode.Creatures ? true : this.fMode == ListMode.NPCs);
            this.FilterTypeToggle.Enabled = (this.fMode == ListMode.Creatures ? true : this.fMode == ListMode.NPCs);
            this.FilterKeywordToggle.Enabled = (this.fMode == ListMode.Creatures ? true : this.fMode == ListMode.NPCs);
            this.FilterLevelToggle.Enabled = (this.fMode == ListMode.Creatures || this.fMode == ListMode.NPCs ? true : this.fMode == ListMode.Traps);
            CheckBox filterLevelAppropriateToggle = this.FilterLevelAppropriateToggle;
            if (this.fPartyLevel == 0)
            {
                flag = false;
            }
            else
            {
                flag = (this.fMode == ListMode.Creatures || this.fMode == ListMode.NPCs ? true : this.fMode == ListMode.Traps);
            }
            filterLevelAppropriateToggle.Enabled = flag;
            CheckBox filterSourceToggle = this.FilterSourceToggle;
            if (Session.Libraries.Count == 0)
            {
                flag1 = false;
            }
            else
            {
                flag1 = (this.fMode == ListMode.Creatures || this.fMode == ListMode.Templates || this.fMode == ListMode.Traps ? true : this.fMode == ListMode.SkillChallenges);
            }
            filterSourceToggle.Enabled = flag1;
        }

        private void update_option_state()
        {
            this.FilterNameBox.Enabled = this.FilterNameToggle.Checked;
            this.FilterCatBox.Enabled = (!this.FilterCatToggle.Enabled ? false : this.FilterCatToggle.Checked);
            this.FilterRoleBox.Enabled = (!this.FilterRoleToggle.Enabled ? false : this.FilterRoleToggle.Checked);
            this.FilterModBox.Enabled = (!this.FilterModToggle.Enabled ? false : this.FilterModToggle.Checked);
            this.FilterOriginBox.Enabled = (!this.FilterOriginToggle.Enabled ? false : this.FilterOriginToggle.Checked);
            this.FilterTypeBox.Enabled = (!this.FilterTypeToggle.Enabled ? false : this.FilterTypeToggle.Checked);
            this.FilterKeywordBox.Enabled = (!this.FilterKeywordToggle.Enabled ? false : this.FilterKeywordToggle.Checked);
            this.FilterSourceBox.Enabled = (!this.FilterSourceToggle.Enabled ? false : this.FilterSourceToggle.Checked);
            this.FromLbl.Enabled = (!this.FilterLevelToggle.Enabled ? false : this.FilterLevelToggle.Checked);
            this.ToLbl.Enabled = (!this.FilterLevelToggle.Enabled ? false : this.FilterLevelToggle.Checked);
            this.LevelFromBox.Enabled = (!this.FilterLevelToggle.Enabled ? false : this.FilterLevelToggle.Checked);
            this.LevelToBox.Enabled = (!this.FilterLevelToggle.Enabled ? false : this.FilterLevelToggle.Checked);
        }

        public event EventHandler FilterChanged;


        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            //components = new System.ComponentModel.Container();
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.FilterLevelToggle = new CheckBox();
            this.FilterModToggle = new CheckBox();
            this.FilterModBox = new ComboBox();
            this.FilterNameToggle = new CheckBox();
            this.FilterCatToggle = new CheckBox();
            this.FilterKeywordBox = new TextBox();
            this.FilterKeywordToggle = new CheckBox();
            this.FilterTypeToggle = new CheckBox();
            this.FilterTypeBox = new ComboBox();
            this.FilterOriginToggle = new CheckBox();
            this.FilterOriginBox = new ComboBox();
            this.FilterCatBox = new TextBox();
            this.FilterRoleToggle = new CheckBox();
            this.FilterRoleBox = new ComboBox();
            this.FilterNameBox = new TextBox();
            this.FilterPnl = new Panel();
            this.FilterSourceBox = new ComboBox();
            this.FilterSourceToggle = new CheckBox();
            this.FilterLevelAppropriateToggle = new CheckBox();
            this.LevelToBox = new NumericUpDown();
            this.LevelFromBox = new NumericUpDown();
            this.ToLbl = new Label();
            this.FromLbl = new Label();
            this.InfoLbl = new ToolStripStatusLabel();
            this.EditLbl = new ToolStripStatusLabel();
            this.Statusbar = new StatusStrip();
            this.FilterPnl.SuspendLayout();
            ((ISupportInitialize)this.LevelToBox).BeginInit();
            ((ISupportInitialize)this.LevelFromBox).BeginInit();
            this.Statusbar.SuspendLayout();
            base.SuspendLayout();
            this.FilterLevelToggle.AutoSize = true;
            this.FilterLevelToggle.Location = new Point(3, 190);
            this.FilterLevelToggle.Name = "FilterLevelToggle";
            this.FilterLevelToggle.Size = new System.Drawing.Size(55, 17);
            this.FilterLevelToggle.TabIndex = 14;
            this.FilterLevelToggle.Text = "Level:";
            this.FilterLevelToggle.UseVisualStyleBackColor = true;
            this.FilterLevelToggle.Click += new EventHandler(this.ToggleChanged);
            this.FilterModToggle.AutoSize = true;
            this.FilterModToggle.Location = new Point(3, 84);
            this.FilterModToggle.Name = "FilterModToggle";
            this.FilterModToggle.Size = new System.Drawing.Size(66, 17);
            this.FilterModToggle.TabIndex = 6;
            this.FilterModToggle.Text = "Modifier:";
            this.FilterModToggle.UseVisualStyleBackColor = true;
            this.FilterModToggle.Click += new EventHandler(this.ToggleChanged);
            this.FilterModBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.FilterModBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.FilterModBox.FormattingEnabled = true;
            this.FilterModBox.Location = new Point(84, 82);
            this.FilterModBox.Name = "FilterModBox";
            this.FilterModBox.Size = new System.Drawing.Size(165, 21);
            this.FilterModBox.TabIndex = 7;
            this.FilterModBox.SelectedIndexChanged += new EventHandler(this.DropdownOptionChanged);
            this.FilterNameToggle.AutoSize = true;
            this.FilterNameToggle.Location = new Point(3, 5);
            this.FilterNameToggle.Name = "FilterNameToggle";
            this.FilterNameToggle.Size = new System.Drawing.Size(57, 17);
            this.FilterNameToggle.TabIndex = 0;
            this.FilterNameToggle.Text = "Name:";
            this.FilterNameToggle.UseVisualStyleBackColor = true;
            this.FilterNameToggle.Click += new EventHandler(this.ToggleChanged);
            this.FilterCatToggle.AutoSize = true;
            this.FilterCatToggle.Location = new Point(3, 31);
            this.FilterCatToggle.Name = "FilterCatToggle";
            this.FilterCatToggle.Size = new System.Drawing.Size(71, 17);
            this.FilterCatToggle.TabIndex = 2;
            this.FilterCatToggle.Text = "Category:";
            this.FilterCatToggle.UseVisualStyleBackColor = true;
            this.FilterCatToggle.Click += new EventHandler(this.ToggleChanged);
            this.FilterKeywordBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.FilterKeywordBox.Location = new Point(84, 163);
            this.FilterKeywordBox.Name = "FilterKeywordBox";
            this.FilterKeywordBox.Size = new System.Drawing.Size(165, 20);
            this.FilterKeywordBox.TabIndex = 13;
            this.FilterKeywordBox.TextChanged += new EventHandler(this.TextOptionChanged);
            this.FilterKeywordToggle.AutoSize = true;
            this.FilterKeywordToggle.Location = new Point(3, 165);
            this.FilterKeywordToggle.Name = "FilterKeywordToggle";
            this.FilterKeywordToggle.Size = new System.Drawing.Size(75, 17);
            this.FilterKeywordToggle.TabIndex = 12;
            this.FilterKeywordToggle.Text = "Keywords:";
            this.FilterKeywordToggle.UseVisualStyleBackColor = true;
            this.FilterKeywordToggle.Click += new EventHandler(this.ToggleChanged);
            this.FilterTypeToggle.AutoSize = true;
            this.FilterTypeToggle.Location = new Point(3, 138);
            this.FilterTypeToggle.Name = "FilterTypeToggle";
            this.FilterTypeToggle.Size = new System.Drawing.Size(53, 17);
            this.FilterTypeToggle.TabIndex = 10;
            this.FilterTypeToggle.Text = "Type:";
            this.FilterTypeToggle.UseVisualStyleBackColor = true;
            this.FilterTypeToggle.Click += new EventHandler(this.ToggleChanged);
            this.FilterTypeBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.FilterTypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.FilterTypeBox.FormattingEnabled = true;
            this.FilterTypeBox.Location = new Point(84, 136);
            this.FilterTypeBox.Name = "FilterTypeBox";
            this.FilterTypeBox.Size = new System.Drawing.Size(165, 21);
            this.FilterTypeBox.TabIndex = 11;
            this.FilterTypeBox.SelectedIndexChanged += new EventHandler(this.DropdownOptionChanged);
            this.FilterOriginToggle.AutoSize = true;
            this.FilterOriginToggle.Location = new Point(3, 111);
            this.FilterOriginToggle.Name = "FilterOriginToggle";
            this.FilterOriginToggle.Size = new System.Drawing.Size(56, 17);
            this.FilterOriginToggle.TabIndex = 8;
            this.FilterOriginToggle.Text = "Origin:";
            this.FilterOriginToggle.UseVisualStyleBackColor = true;
            this.FilterOriginToggle.Click += new EventHandler(this.ToggleChanged);
            this.FilterOriginBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.FilterOriginBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.FilterOriginBox.FormattingEnabled = true;
            this.FilterOriginBox.Location = new Point(84, 109);
            this.FilterOriginBox.Name = "FilterOriginBox";
            this.FilterOriginBox.Size = new System.Drawing.Size(165, 21);
            this.FilterOriginBox.TabIndex = 9;
            this.FilterOriginBox.SelectedIndexChanged += new EventHandler(this.DropdownOptionChanged);
            this.FilterCatBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.FilterCatBox.Location = new Point(84, 29);
            this.FilterCatBox.Name = "FilterCatBox";
            this.FilterCatBox.Size = new System.Drawing.Size(165, 20);
            this.FilterCatBox.TabIndex = 3;
            this.FilterCatBox.TextChanged += new EventHandler(this.TextOptionChanged);
            this.FilterRoleToggle.AutoSize = true;
            this.FilterRoleToggle.Location = new Point(3, 57);
            this.FilterRoleToggle.Name = "FilterRoleToggle";
            this.FilterRoleToggle.Size = new System.Drawing.Size(51, 17);
            this.FilterRoleToggle.TabIndex = 4;
            this.FilterRoleToggle.Text = "Role:";
            this.FilterRoleToggle.UseVisualStyleBackColor = true;
            this.FilterRoleToggle.Click += new EventHandler(this.ToggleChanged);
            this.FilterRoleBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.FilterRoleBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.FilterRoleBox.FormattingEnabled = true;
            this.FilterRoleBox.Location = new Point(84, 55);
            this.FilterRoleBox.Name = "FilterRoleBox";
            this.FilterRoleBox.Size = new System.Drawing.Size(165, 21);
            this.FilterRoleBox.TabIndex = 5;
            this.FilterRoleBox.SelectedIndexChanged += new EventHandler(this.DropdownOptionChanged);
            this.FilterNameBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.FilterNameBox.Location = new Point(84, 3);
            this.FilterNameBox.Name = "FilterNameBox";
            this.FilterNameBox.Size = new System.Drawing.Size(165, 20);
            this.FilterNameBox.TabIndex = 1;
            this.FilterNameBox.TextChanged += new EventHandler(this.TextOptionChanged);
            this.FilterPnl.Controls.Add(this.FilterSourceBox);
            this.FilterPnl.Controls.Add(this.FilterSourceToggle);
            this.FilterPnl.Controls.Add(this.FilterLevelAppropriateToggle);
            this.FilterPnl.Controls.Add(this.LevelToBox);
            this.FilterPnl.Controls.Add(this.LevelFromBox);
            this.FilterPnl.Controls.Add(this.ToLbl);
            this.FilterPnl.Controls.Add(this.FromLbl);
            this.FilterPnl.Controls.Add(this.FilterNameBox);
            this.FilterPnl.Controls.Add(this.FilterLevelToggle);
            this.FilterPnl.Controls.Add(this.FilterRoleBox);
            this.FilterPnl.Controls.Add(this.FilterModToggle);
            this.FilterPnl.Controls.Add(this.FilterRoleToggle);
            this.FilterPnl.Controls.Add(this.FilterModBox);
            this.FilterPnl.Controls.Add(this.FilterCatBox);
            this.FilterPnl.Controls.Add(this.FilterNameToggle);
            this.FilterPnl.Controls.Add(this.FilterOriginBox);
            this.FilterPnl.Controls.Add(this.FilterCatToggle);
            this.FilterPnl.Controls.Add(this.FilterOriginToggle);
            this.FilterPnl.Controls.Add(this.FilterKeywordBox);
            this.FilterPnl.Controls.Add(this.FilterTypeBox);
            this.FilterPnl.Controls.Add(this.FilterKeywordToggle);
            this.FilterPnl.Controls.Add(this.FilterTypeToggle);
            this.FilterPnl.Dock = DockStyle.Top;
            this.FilterPnl.Location = new Point(0, 0);
            this.FilterPnl.Name = "FilterPnl";
            this.FilterPnl.Size = new System.Drawing.Size(252, 294);
            this.FilterPnl.TabIndex = 0;
            this.FilterSourceBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.FilterSourceBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.FilterSourceBox.FormattingEnabled = true;
            this.FilterSourceBox.Location = new Point(84, 264);
            this.FilterSourceBox.Name = "FilterSourceBox";
            this.FilterSourceBox.Size = new System.Drawing.Size(165, 21);
            this.FilterSourceBox.TabIndex = 21;
            this.FilterSourceBox.SelectedIndexChanged += new EventHandler(this.DropdownOptionChanged);
            this.FilterSourceToggle.AutoSize = true;
            this.FilterSourceToggle.Location = new Point(3, 266);
            this.FilterSourceToggle.Name = "FilterSourceToggle";
            this.FilterSourceToggle.Size = new System.Drawing.Size(63, 17);
            this.FilterSourceToggle.TabIndex = 20;
            this.FilterSourceToggle.Text = "Source:";
            this.FilterSourceToggle.UseVisualStyleBackColor = true;
            this.FilterSourceToggle.CheckedChanged += new EventHandler(this.ToggleChanged);
            this.FilterLevelAppropriateToggle.AutoSize = true;
            this.FilterLevelAppropriateToggle.Location = new Point(3, 241);
            this.FilterLevelAppropriateToggle.Name = "FilterLevelAppropriateToggle";
            this.FilterLevelAppropriateToggle.Size = new System.Drawing.Size(183, 17);
            this.FilterLevelAppropriateToggle.TabIndex = 19;
            this.FilterLevelAppropriateToggle.Text = "Show level-appropriate items only";
            this.FilterLevelAppropriateToggle.UseVisualStyleBackColor = true;
            this.FilterLevelAppropriateToggle.Click += new EventHandler(this.ToggleChanged);
            this.LevelToBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.LevelToBox.Location = new Point(130, 215);
            NumericUpDown levelToBox = this.LevelToBox;
            int[] numArray = new int[] { 40, 0, 0, 0 };
            levelToBox.Maximum = new decimal(numArray);
            NumericUpDown num = this.LevelToBox;
            int[] numArray1 = new int[] { 1, 0, 0, 0 };
            num.Minimum = new decimal(numArray1);
            this.LevelToBox.Name = "LevelToBox";
            this.LevelToBox.Size = new System.Drawing.Size(119, 20);
            this.LevelToBox.TabIndex = 18;
            NumericUpDown numericUpDown = this.LevelToBox;
            int[] numArray2 = new int[] { 10, 0, 0, 0 };
            numericUpDown.Value = new decimal(numArray2);
            this.LevelToBox.ValueChanged += new EventHandler(this.NumericOptionChanged);
            this.LevelFromBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.LevelFromBox.Location = new Point(130, 189);
            NumericUpDown levelFromBox = this.LevelFromBox;
            int[] numArray3 = new int[] { 40, 0, 0, 0 };
            levelFromBox.Maximum = new decimal(numArray3);
            NumericUpDown levelFromBox1 = this.LevelFromBox;
            int[] numArray4 = new int[] { 1, 0, 0, 0 };
            levelFromBox1.Minimum = new decimal(numArray4);
            this.LevelFromBox.Name = "LevelFromBox";
            this.LevelFromBox.Size = new System.Drawing.Size(119, 20);
            this.LevelFromBox.TabIndex = 16;
            NumericUpDown num1 = this.LevelFromBox;
            int[] numArray5 = new int[] { 1, 0, 0, 0 };
            num1.Value = new decimal(numArray5);
            this.LevelFromBox.ValueChanged += new EventHandler(this.NumericOptionChanged);
            this.ToLbl.AutoSize = true;
            this.ToLbl.Location = new Point(81, 217);
            this.ToLbl.Name = "ToLbl";
            this.ToLbl.Size = new System.Drawing.Size(23, 13);
            this.ToLbl.TabIndex = 17;
            this.ToLbl.Text = "To:";
            this.FromLbl.AutoSize = true;
            this.FromLbl.Location = new Point(81, 191);
            this.FromLbl.Name = "FromLbl";
            this.FromLbl.Size = new System.Drawing.Size(33, 13);
            this.FromLbl.TabIndex = 15;
            this.FromLbl.Text = "From:";
            this.InfoLbl.AutoToolTip = true;
            this.InfoLbl.Name = "InfoLbl";
            this.InfoLbl.Size = new System.Drawing.Size(202, 17);
            this.InfoLbl.Spring = true;
            this.InfoLbl.Text = "[info]";
            this.InfoLbl.TextAlign = ContentAlignment.MiddleLeft;
            this.EditLbl.IsLink = true;
            this.EditLbl.Name = "EditLbl";
            this.EditLbl.Size = new System.Drawing.Size(35, 17);
            this.EditLbl.Text = "(edit)";
            this.EditLbl.Click += new EventHandler(this.EditLbl_Click);
            this.Statusbar.Dock = DockStyle.Top;
            ToolStripItemCollection items = this.Statusbar.Items;
            ToolStripItem[] infoLbl = new ToolStripItem[] { this.InfoLbl, this.EditLbl };
            items.AddRange(infoLbl);
            this.Statusbar.Location = new Point(0, 294);
            this.Statusbar.Name = "Statusbar";
            this.Statusbar.Size = new System.Drawing.Size(252, 22);
            this.Statusbar.SizingGrip = false;
            this.Statusbar.TabIndex = 1;
            this.Statusbar.Text = "statusStrip1";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            base.Controls.Add(this.Statusbar);
            base.Controls.Add(this.FilterPnl);
            base.Name = "FilterPanel";
            base.Size = new System.Drawing.Size(252, 331);
            this.FilterPnl.ResumeLayout(false);
            this.FilterPnl.PerformLayout();
            ((ISupportInitialize)this.LevelToBox).EndInit();
            ((ISupportInitialize)this.LevelFromBox).EndInit();
            this.Statusbar.ResumeLayout(false);
            this.Statusbar.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion
    }
}
