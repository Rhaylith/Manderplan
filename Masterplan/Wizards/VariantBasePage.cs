using System;
using System.Windows.Forms;

namespace Masterplan.Wizards
{
    public partial class VariantBasePage : UserControl, Utils.Wizards.IWizardPage
    {
        public VariantBasePage()
        {
            InitializeComponent();
            Application.Idle += new EventHandler(this.Application_Idle);

        }
    }
}
