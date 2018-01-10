using System;
using System.Collections.Generic;
using Masterplan.Data;

namespace Masterplan.Commands.Combat
{
    public class DamageEntityCommand : ICommand
    {
        private struct DamageEntry
        {
            public CombatData Entity;
            public int damageAmount;
            public int previousTempHP;
        }

        private List<DamageEntry> _damageTargets = new List<DamageEntry>();

        //private Encounter _encounter;
        public DamageEntityCommand()
        {
            //_encounter = enc;
        }

        public void AddTarget(CombatData entity, int dmgAmount)
        {
            if (dmgAmount > 0)
            {
                _damageTargets.Add(new DamageEntry { Entity = entity, damageAmount = dmgAmount });
            }
        }

        public void Do()
        {
            for (int i=0;i<_damageTargets.Count;++i)
            {
                DamageEntry entry = _damageTargets[i];
                CombatData data = entry.Entity;
                int dmgAmount = entry.damageAmount;
                if (data.TempHP > 0)
                {
                    entry.previousTempHP = data.TempHP;
                    int num = Math.Min(data.TempHP, entry.damageAmount);
                    data.TempHP = data.TempHP - num;
                    dmgAmount -= num;
                }
                data.Damage += dmgAmount;

                //this.fLog.AddDamageEntry(pair2.First.ID, damage, damageForm.Types);

                //TODO:  If the creature state changes the log it
                //CreatureState creatureState = _encounter.GetState(data);
                //this.fLog.AddStateEntry(pair2.First.ID, creatureState);
            }
        }

        public void Undo()
        {
            for (int i = 0; i < _damageTargets.Count; ++i)
            {
                DamageEntry entry = _damageTargets[i];
                CombatData data = entry.Entity;
                int dmgAmount = entry.damageAmount;
                if (entry.previousTempHP > 0)
                {
                    int num = Math.Min(entry.previousTempHP, entry.damageAmount);
                    data.TempHP += num;
                    dmgAmount -= num;
                }
                if (dmgAmount > 0)
                {
                    data.Damage -= dmgAmount;
                }

                //this.fLog.AddDamageEntry(pair2.First.ID, damage, damageForm.Types);

                //TODO:  If the creature state changes the log it
                //CreatureState creatureState = _encounter.GetState(data);
                //this.fLog.AddStateEntry(pair2.First.ID, creatureState);
            }
        }
    }
}
