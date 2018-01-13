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
    public partial class TitlePanel : UserControl
    {
        public TitlePanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.fFormat.Alignment = StringAlignment.Center;
            this.fFormat.LineAlignment = StringAlignment.Center;
            this.fFormat.Trimming = StringTrimming.EllipsisWord;
            this.FadeTimer.Enabled = true;
        }
    }
}
