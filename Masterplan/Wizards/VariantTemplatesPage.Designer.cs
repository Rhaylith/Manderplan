using Masterplan;
using Masterplan.Data;
using Masterplan.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils.Wizards;

namespace Masterplan.Wizards
{
    partial class VariantTemplatesPage
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

        private ListView TemplateList;

        private ColumnHeader NameHdr;

        private ColumnHeader TypeHdr;

        private Label InfoLbl;

        private VariantData fData;

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
                return false;
            }
        }

        public bool AllowNext
        {
            get
            {
                return true;
            }
        }

        public bool OnBack()
        {
            return true;
        }

        public bool OnFinish()
        {
            return false;
        }

        public bool OnNext()
        {
            int count = 0;
            switch ((this.fData.BaseCreature.Role as ComplexRole).Flag)
            {
                case RoleFlag.Elite:
                    {
                        count = 1;
                        break;
                    }
                case RoleFlag.Solo:
                    {
                        count = 2;
                        break;
                    }
            }
            count += this.TemplateList.CheckedItems.Count;
            if (count > 2 && MessageBox.Show(string.Concat(string.Concat("You can not normally apply that many templates to this creature.", Environment.NewLine), "Are you sure you want to continue?"), "Creature Builder", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return false;
            }
            this.fData.Templates.Clear();
            foreach (ListViewItem checkedItem in this.TemplateList.CheckedItems)
            {
                this.fData.Templates.Add(checkedItem.Tag as CreatureTemplate);
            }
            return true;
        }

        public void OnShown(object data)
        {
            if (this.fData == null)
            {
                this.fData = data as VariantData;
                foreach (CreatureTemplate template in Session.Templates)
                {
                    ListViewItem listViewItem = this.TemplateList.Items.Add(template.Name);
                    listViewItem.SubItems.Add(template.Info);
                    listViewItem.Tag = template;
                }
            }
        }

        private void TemplateList_DoubleClick(object sender, EventArgs e)
        {
            if (this.TemplateList.SelectedItems.Count != 0)
            {
                CreatureTemplate tag = this.TemplateList.SelectedItems[0].Tag as CreatureTemplate;
                if (tag != null)
                {
                    (new CreatureTemplateDetailsForm(tag)).ShowDialog();
                }
            }
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TemplateList = new ListView();
            this.NameHdr = new ColumnHeader();
            this.TypeHdr = new ColumnHeader();
            this.InfoLbl = new Label();
            base.SuspendLayout();
            this.TemplateList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.TemplateList.CheckBoxes = true;
            ListView.ColumnHeaderCollection columns = this.TemplateList.Columns;
            ColumnHeader[] nameHdr = new ColumnHeader[] { this.NameHdr, this.TypeHdr };
            columns.AddRange(nameHdr);
            this.TemplateList.FullRowSelect = true;
            this.TemplateList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.TemplateList.HideSelection = false;
            this.TemplateList.Location = new Point(3, 43);
            this.TemplateList.MultiSelect = false;
            this.TemplateList.Name = "TemplateList";
            this.TemplateList.Size = new System.Drawing.Size(287, 188);
            this.TemplateList.TabIndex = 5;
            this.TemplateList.UseCompatibleStateImageBehavior = false;
            this.TemplateList.View = View.Details;
            this.TemplateList.DoubleClick += new EventHandler(this.TemplateList_DoubleClick);
            this.NameHdr.Text = "Template";
            this.NameHdr.Width = 150;
            this.TypeHdr.Text = "Role";
            this.TypeHdr.Width = 100;
            this.InfoLbl.Dock = DockStyle.Top;
            this.InfoLbl.Location = new Point(0, 0);
            this.InfoLbl.Name = "InfoLbl";
            this.InfoLbl.Size = new System.Drawing.Size(293, 40);
            this.InfoLbl.TabIndex = 3;
            this.InfoLbl.Text = "Select any templates you would like to apply to the new creature.";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.TemplateList);
            base.Controls.Add(this.InfoLbl);
            base.Name = "VariantTemplatesPage";
            base.Size = new System.Drawing.Size(293, 234);
            base.ResumeLayout(false);
        }

        #endregion
    }
}
