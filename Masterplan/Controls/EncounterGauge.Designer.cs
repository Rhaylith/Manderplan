using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Masterplan.Controls
{
    partial class EncounterGauge
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

        private const int CONTROL_HEIGHT = 20;

        private Masterplan.Data.Party fParty;

        private int fXP;

        public Masterplan.Data.Party Party
        {
            get
            {
                return this.fParty;
            }
            set
            {
                this.fParty = value;
                base.Invalidate();
            }
        }

        public int XP
        {
            get
            {
                return this.fXP;
            }
            set
            {
                this.fXP = value;
                base.Invalidate();
            }
        }

        private int get_max_level()
        {
            int creatureLevel = Experience.GetCreatureLevel(this.fXP / this.fParty.Size);
            return Math.Max(this.fParty.Level + 5, creatureLevel + 1);
        }

        private int get_min_level()
        {
            int creatureLevel = Experience.GetCreatureLevel(this.fXP / this.fParty.Size);
            int num = Math.Min(this.fParty.Level - 3, creatureLevel);
            return Math.Max(num, 0);
        }

        private int get_x(int xp)
        {
            int creatureXP = Experience.GetCreatureXP(this.get_min_level()) * this.fParty.Size;
            int num = Experience.GetCreatureXP(this.get_max_level()) * this.fParty.Size;
            int num1 = Math.Min(this.fXP, creatureXP);
            int num2 = Math.Max(this.fXP, num) - num1;
            return (xp - num1) * base.Width / num2;
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            base.Height = 20;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.fParty == null)
            {
                return;
            }
            System.Drawing.Font font = new System.Drawing.Font(this.Font.FontFamily, 7f);
            Rectangle rectangle = new Rectangle(0, 4, this.get_x(this.fXP), base.Height - 8);
            if (rectangle.Width > 0)
            {
                Brush linearGradientBrush = new LinearGradientBrush(rectangle, SystemColors.Control, SystemColors.ControlDark, LinearGradientMode.Horizontal);
                e.Graphics.FillRectangle(linearGradientBrush, rectangle);
            }
            int num = Math.Max(this.get_min_level(), 1);
            int maxLevel = this.get_max_level();
            for (int i = num; i != maxLevel; i++)
            {
                int creatureXP = Experience.GetCreatureXP(i) * this.fParty.Size;
                int _x = this.get_x(creatureXP);
                e.Graphics.DrawLine(Pens.Black, new Point(_x, 1), new Point(_x, base.Height - 3));
                e.Graphics.DrawString(i.ToString(), font, SystemBrushes.WindowText, new PointF((float)_x, 1f));
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            base.Height = 20;
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

            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Name = "EncounterGauge";
            base.ResumeLayout(false);
        }

        #endregion
    }
}
