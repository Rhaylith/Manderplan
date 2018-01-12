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
    public partial class MapView : UserControl
    {
        public MapView()
        {
            InitializeComponent();
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.fCentred.Alignment = StringAlignment.Center;
            this.fCentred.LineAlignment = StringAlignment.Center;
            this.fCentred.Trimming = StringTrimming.EllipsisWord;
            this.fTop.Alignment = StringAlignment.Center;
            this.fTop.LineAlignment = StringAlignment.Near;
            this.fTop.Trimming = StringTrimming.EllipsisCharacter;
            this.fBottom.Alignment = StringAlignment.Center;
            this.fBottom.LineAlignment = StringAlignment.Far;
            this.fBottom.Trimming = StringTrimming.EllipsisCharacter;
            this.fLeft.Alignment = StringAlignment.Near;
            this.fLeft.LineAlignment = StringAlignment.Center;
            this.fLeft.Trimming = StringTrimming.EllipsisCharacter;
            this.fRight.Alignment = StringAlignment.Far;
            this.fRight.LineAlignment = StringAlignment.Center;
            this.fRight.Trimming = StringTrimming.EllipsisCharacter;
        }
    }
}
