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
    public partial class PlotView : UserControl
    {
        public PlotView()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.fCentred.Alignment = StringAlignment.Center;
            this.fCentred.LineAlignment = StringAlignment.Center;
            this.fCentred.Trimming = StringTrimming.EllipsisWord;
        }
    }
}
