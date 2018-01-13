using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.Controls
{
    partial class LibraryHelpPanel
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

        private WebBrowser Browser;

        private string get_html()
        {
            List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
            head.Add("<P>This is the Libraries screen.</P>");
            head.Add("<P><B>What is a library?</B> A library is a file containing a collection of reusable items such as creatures, traps and hazards, magic items and map tiles. On the left you can see the list of libraries that are currently installed. When you select one of these libraries you can see the items it contains.</P>");
            head.Add("<P><B>I have a library file, how do I install it?</B> First, find the folder containing Masterplan; there will be a sub-folder called Libraries. Move your library file into this folder, and restart Masterplan.</P>");
            head.Add("<P><B>How do I import a creature from Adventure Tools?</B> If you have exported a .monster file from Adventure Tools you can import them into a library. Choose the library you want to add the creature to, and select it on the left. On the Creatures tab, press the down-arrow beside the Add button, and select Import from Adventure Tools to bring up a file browser. Select the .monster file, and it will be imported into your library.</P>");
            head.Add("<P><B>How do I create map tiles from image files?</B> If you have a selection of image files that you want to use as map tiles, you can import them into a library. Choose the library you want to add the tiles to, and select it on the left. On the Map Tiles tab, press the Add button to bring up a file browser. Select the image files to import them into your library. Masterplan will try to work out the dimensions of each tile, but you can edit any that are incorrect by right-clicking on the tile and selecting Set Size. You can also set the category of each tile – this is particularly useful if you want to use the Map AutoBuild feature to build random dungeon maps automatically.</P>");
            return HTML.Concatenate(head);
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

            this.Browser = new WebBrowser();
            base.SuspendLayout();
            this.Browser.AllowWebBrowserDrop = false;
            this.Browser.Dock = DockStyle.Fill;
            this.Browser.IsWebBrowserContextMenuEnabled = false;
            this.Browser.Location = new Point(0, 0);
            this.Browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.Browser.Name = "Browser";
            this.Browser.ScriptErrorsSuppressed = true;
            this.Browser.Size = new System.Drawing.Size(330, 263);
            this.Browser.TabIndex = 0;
            this.Browser.WebBrowserShortcutsEnabled = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            base.Controls.Add(this.Browser);
            base.Name = "LibraryHelpPanel";
            base.Size = new System.Drawing.Size(330, 263);
            base.ResumeLayout(false);
        }

        #endregion
    }
}
