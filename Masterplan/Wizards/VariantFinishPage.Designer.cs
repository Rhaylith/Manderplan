using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.Wizards
{
    partial class VariantFinishPage
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

        private Label InfoLbl;

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
                return true;
            }
        }

        public bool AllowNext
        {
            get
            {
                return false;
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
            return false;
        }

        public void OnShown(object data)
        {
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.InfoLbl = new Label();
            base.SuspendLayout();
            this.InfoLbl.Dock = DockStyle.Top;
            this.InfoLbl.Location = new Point(0, 0);
            this.InfoLbl.Name = "InfoLbl";
            this.InfoLbl.Size = new System.Drawing.Size(260, 40);
            this.InfoLbl.TabIndex = 0;
            this.InfoLbl.Text = "Press Finish to edit this creature, or Back to change your selections.";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.InfoLbl);
            base.Name = "VariantFinishPage";
            base.Size = new System.Drawing.Size(260, 150);
            base.ResumeLayout(false);
        }

        #endregion
    }
}
