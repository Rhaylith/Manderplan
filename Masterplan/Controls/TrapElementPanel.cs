using System;
using System.Windows.Forms;

namespace Masterplan.Controls
{
    public partial class TrapElementPanel : UserControl
    {
        public TrapElementPanel()
        {
            this.InitializeComponent();
            Application.Idle += new EventHandler(this.Application_Idle);
            this.update_view();
        }
    }
}
