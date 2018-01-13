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
    public partial class DicePanel : UserControl
    {
        public DicePanel()
        {
            InitializeComponent();
            Application.Idle += new EventHandler(this.Application_Idle);
            this.fCentred.Alignment = StringAlignment.Center;
            this.fCentred.LineAlignment = StringAlignment.Center;
        }
    }
}
