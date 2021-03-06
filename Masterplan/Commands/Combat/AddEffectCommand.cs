﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Masterplan.Data;

namespace Masterplan.Commands.Combat
{
    public class AddEffectCommand : ICommand
    {
        private CombatData _data;
        private OngoingCondition _effect;

        public AddEffectCommand(CombatData data, OngoingCondition effect)
        {
            _data = data;
            _effect = effect;
        }

        public void Do()
        {
            _data.Conditions.Add(_effect);
            //TODO:  Log this effect
            //this.fLog.AddEffectEntry(data.ID, tag.ToString(this.fEncounter, false), false);
        }

        public void Undo()
        {
            _data.Conditions.Remove(_effect);
        }
    }
}
