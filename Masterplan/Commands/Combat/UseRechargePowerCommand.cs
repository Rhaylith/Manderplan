using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Masterplan.Data;

namespace Masterplan.Commands.Combat
{
    public class UseRechargePowerCommand : ICommand
    {
        public enum RechargePowerAction
        {
            UsePower = 0,
            RechargePower = 1,
        }

        private RechargePowerAction _action;
        private CombatData _data;
        private Guid _powerID;

        public UseRechargePowerCommand(CombatData data, Guid powerID, RechargePowerAction action)
        {
            _data = data;
            _powerID = powerID;
            _action = action;
        }

        public void Do()
        {
            if (_action == RechargePowerAction.UsePower)
            {
                _data.UsedPowers.Add(_powerID);
            }
            else
            {
                _data.UsedPowers.Remove(_powerID);
            }
        }

        public void Undo()
        {
            if (_action == RechargePowerAction.UsePower)
            {
                _data.UsedPowers.Remove(_powerID);
            }
            else
            {
                _data.UsedPowers.Add(_powerID);
            }

        }
    }
}
