using Masterplan.Data;
using System;
using System.Windows.Forms;

namespace Masterplan.Controls
{
    public partial class QuestPanel : UserControl
    {
        public QuestPanel()
        {
            this.InitializeComponent();
            foreach (QuestType value in Enum.GetValues(typeof(QuestType)))
            {
                this.TypeBox.Items.Add(value);
            }
        }
    }
}
