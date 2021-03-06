﻿using Masterplan.Tools.Generators;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils.Wizards;

namespace Masterplan.Wizards
{
    partial class MapTypePage
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

        private MapBuilderData fData;

        private Label InfoLbl;

        private RadioButton DungeonBtn;

        private RadioButton AreaBtn;

        private RadioButton FreeformBtn;

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
                return true;
            }
        }

        public bool OnBack()
        {
            return true;
        }

        public bool OnFinish()
        {
            this.set_data();
            return true;
        }

        public bool OnNext()
        {
            this.set_data();
            return true;
        }

        public void OnShown(object data)
        {
            if (this.fData == null)
            {
                this.fData = data as MapBuilderData;
                switch (this.fData.Type)
                {
                    case MapAutoBuildType.Warren:
                        {
                            this.DungeonBtn.Checked = true;
                            return;
                        }
                    case MapAutoBuildType.FilledArea:
                        {
                            this.AreaBtn.Checked = true;
                            return;
                        }
                    case MapAutoBuildType.Freeform:
                        {
                            this.FreeformBtn.Checked = true;
                            break;
                        }
                    default:
                        {
                            return;
                        }
                }
            }
        }

        private void set_data()
        {
            if (this.DungeonBtn.Checked)
            {
                this.fData.Type = MapAutoBuildType.Warren;
            }
            if (this.AreaBtn.Checked)
            {
                this.fData.Type = MapAutoBuildType.FilledArea;
            }
            if (this.FreeformBtn.Checked)
            {
                this.fData.Type = MapAutoBuildType.Freeform;
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
            this.DungeonBtn = new RadioButton();
            this.AreaBtn = new RadioButton();
            this.FreeformBtn = new RadioButton();
            base.SuspendLayout();
            this.InfoLbl.Dock = DockStyle.Top;
            this.InfoLbl.Location = new Point(0, 0);
            this.InfoLbl.Name = "InfoLbl";
            this.InfoLbl.Size = new System.Drawing.Size(307, 40);
            this.InfoLbl.TabIndex = 0;
            this.InfoLbl.Text = "What kind of map do you want to create?";
            this.DungeonBtn.AutoSize = true;
            this.DungeonBtn.Location = new Point(3, 40);
            this.DungeonBtn.Name = "DungeonBtn";
            this.DungeonBtn.Size = new System.Drawing.Size(100, 17);
            this.DungeonBtn.TabIndex = 1;
            this.DungeonBtn.TabStop = true;
            this.DungeonBtn.Text = "A dungeon map";
            this.DungeonBtn.UseVisualStyleBackColor = true;
            this.AreaBtn.AutoSize = true;
            this.AreaBtn.Location = new Point(3, 63);
            this.AreaBtn.Name = "AreaBtn";
            this.AreaBtn.Size = new System.Drawing.Size(112, 17);
            this.AreaBtn.TabIndex = 2;
            this.AreaBtn.TabStop = true;
            this.AreaBtn.Text = "A rectangular area";
            this.AreaBtn.UseVisualStyleBackColor = true;
            this.FreeformBtn.AutoSize = true;
            this.FreeformBtn.Location = new Point(3, 86);
            this.FreeformBtn.Name = "FreeformBtn";
            this.FreeformBtn.Size = new System.Drawing.Size(97, 17);
            this.FreeformBtn.TabIndex = 3;
            this.FreeformBtn.TabStop = true;
            this.FreeformBtn.Text = "A freeform area";
            this.FreeformBtn.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.FreeformBtn);
            base.Controls.Add(this.AreaBtn);
            base.Controls.Add(this.DungeonBtn);
            base.Controls.Add(this.InfoLbl);
            base.Name = "MapTypePage";
            base.Size = new System.Drawing.Size(307, 129);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion
    }
}
