using Masterplan.Data;
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
    public partial class FilterPanel : UserControl
    {
        public FilterPanel()
        {
            this.InitializeComponent();
            foreach (RoleType value in Enum.GetValues(typeof(RoleType)))
            {
                this.FilterRoleBox.Items.Add(value);
            }
            this.FilterRoleBox.SelectedIndex = 0;
            this.FilterModBox.Items.Add("Standard");
            this.FilterModBox.Items.Add("Elite");
            this.FilterModBox.Items.Add("Solo");
            this.FilterModBox.Items.Add("Minion");
            this.FilterModBox.SelectedIndex = 0;
            foreach (CreatureOrigin creatureOrigin in Enum.GetValues(typeof(CreatureOrigin)))
            {
                this.FilterOriginBox.Items.Add(creatureOrigin);
            }
            if (this.FilterOriginBox.Items.Count != 0)
            {
                this.FilterOriginBox.SelectedIndex = 0;
            }
            foreach (CreatureType creatureType in Enum.GetValues(typeof(CreatureType)))
            {
                this.FilterTypeBox.Items.Add(creatureType);
            }
            if (this.FilterTypeBox.Items.Count != 0)
            {
                this.FilterTypeBox.SelectedIndex = 0;
            }
            foreach (Library library in Session.Libraries)
            {
                this.FilterSourceBox.Items.Add(library);
            }
            if (this.FilterSourceBox.Items.Count != 0)
            {
                this.FilterSourceBox.SelectedIndex = 0;
            }
            this.update_allowed_filters();
            this.update_option_state();
            this.open_close_editor(false);
        }
    }
}
