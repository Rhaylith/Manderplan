﻿using Masterplan;
using Masterplan.Data;
using Masterplan.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.Controls
{
    partial class MapElementPanel
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

        private Masterplan.Data.MapElement fMapElement;

        private ToolStrip Toolbar;

        private Masterplan.Controls.MapView MapView;

        private ToolStripButton toolStripButton1;

        public Masterplan.Data.MapElement MapElement
        {
            get
            {
                return this.fMapElement;
            }
            set
            {
                this.fMapElement = value;
                this.update_view();
            }
        }

        private void MapSelectBtn_Click(object sender, EventArgs e)
        {
            MapAreaSelectForm mapAreaSelectForm = new MapAreaSelectForm(this.fMapElement.MapID, this.fMapElement.MapAreaID);
            if (mapAreaSelectForm.ShowDialog() == DialogResult.OK)
            {
                this.fMapElement.MapID = (mapAreaSelectForm.Map != null ? mapAreaSelectForm.Map.ID : Guid.Empty);
                this.fMapElement.MapAreaID = (mapAreaSelectForm.MapArea != null ? mapAreaSelectForm.MapArea.ID : Guid.Empty);
                this.update_view();
            }
        }

        private void update_view()
        {
            Map map = Session.Project.FindTacticalMap(this.fMapElement.MapID);
            if (map == null)
            {
                this.MapView.Map = null;
                this.MapView.Viewpoint = Rectangle.Empty;
                return;
            }
            this.MapView.Map = map;
            MapArea mapArea = map.FindArea(this.fMapElement.MapAreaID);
            if (mapArea == null)
            {
                this.MapView.Viewpoint = Rectangle.Empty;
                return;
            }
            this.MapView.Viewpoint = mapArea.Region;
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Toolbar = new ToolStrip();
            this.toolStripButton1 = new ToolStripButton();
            this.MapView = new Masterplan.Controls.MapView();
            this.Toolbar.SuspendLayout();
            base.SuspendLayout();
            this.Toolbar.Items.AddRange(new ToolStripItem[] { this.toolStripButton1 });
            this.Toolbar.Location = new Point(0, 0);
            this.Toolbar.Name = "Toolbar";
            this.Toolbar.Size = new System.Drawing.Size(410, 25);
            this.Toolbar.TabIndex = 0;
            this.Toolbar.Text = "toolStrip1";
            this.toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.ImageTransparentColor = Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(69, 22);
            this.toolStripButton1.Text = "Select Map";
            this.toolStripButton1.Click += new EventHandler(this.MapSelectBtn_Click);
            this.MapView.AllowDrop = true;
            this.MapView.AllowLinkCreation = false;
            this.MapView.AllowScrolling = false;
            this.MapView.BackgroundMap = null;
            this.MapView.BorderSize = 0;
            this.MapView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MapView.Dock = DockStyle.Fill;
            this.MapView.Encounter = null;
            this.MapView.FrameType = MapDisplayType.Dimmed;
            this.MapView.HighlightAreas = false;
            this.MapView.LineOfSight = false;
            this.MapView.Location = new Point(0, 25);
            this.MapView.Map = null;
            this.MapView.Mode = MapViewMode.Thumbnail;
            this.MapView.Name = "MapView";
            this.MapView.ScalingFactor = 1;
            this.MapView.SelectedTiles = null;
            this.MapView.Selection = new Rectangle(0, 0, 0, 0);
            this.MapView.ShowCreatureLabels = false;
            this.MapView.ShowCreatures = CreatureViewMode.None;
            this.MapView.ShowGrid = MapGridMode.None;
            this.MapView.ShowHealthBars = false;
            this.MapView.Size = new System.Drawing.Size(410, 216);
            this.MapView.TabIndex = 1;
            this.MapView.Tactical = false;
            this.MapView.TokenLinks = null;
            this.MapView.Viewpoint = new Rectangle(0, 0, 0, 0);
            this.MapView.DoubleClick += new EventHandler(this.MapSelectBtn_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.MapView);
            base.Controls.Add(this.Toolbar);
            base.Name = "MapElementPanel";
            base.Size = new System.Drawing.Size(410, 241);
            this.Toolbar.ResumeLayout(false);
            this.Toolbar.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion
    }
}
