using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Masterplan.Controls
{
    public partial class LibraryHelpPanel : UserControl
    {
        public LibraryHelpPanel()
        {
            this.InitializeComponent();
            this.Browser.DocumentText = this.get_html();
        }
    }
}
