using Masterplan.Data;
using Masterplan.Data.Combat;
using Masterplan.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Masterplan.Commands.Combat
{
    public class DelayAction : ICommand
    {
        CombatData _data;
        InitiativeList _initList;
        private bool wasCurrentActor;
        private int oldInitiative;
        private CombatData oldNextTurn;
        public DelayAction(CombatData data, CombatState combatState)
        {
            _data = data;
            _initList = combatState.InitiativeList;
        }

        public void Do()
        {
            _data.Delaying = !_data.Delaying;
            if (!_data.Delaying)
            {
                //Undelay
                oldInitiative = _data.Initiative;
                oldNextTurn = _initList.Remove(_data);

                _data.Initiative = _initList.CurrentActor.Initiative;
                _initList.AddAfter(_initList.CurrentActor, _data);  // Move to current
            }
            else
            {
                wasCurrentActor = _initList.CurrentActor == _data;
            }
        }

        public void Undo()
        {
            _data.Delaying = !_data.Delaying;
            if (_data.Delaying)
            {
                // We're undoing back to a delayed state
                _data.Initiative = oldInitiative;
                _initList.Remove(_data);
                _initList.AddBefore(oldNextTurn, _data);
            }
        }
    }
}
