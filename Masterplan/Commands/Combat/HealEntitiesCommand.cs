using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Masterplan.Commands;
using Masterplan.Data;
using Utils;

namespace Masterplan.Commands.Combat
{
    public class HealEntitiesCommand : ICommand
    {
        private List<Pair<CombatData, EncounterCard>> _tokens;
        private int _amount;
        private bool _isTemp;

        // For undo, either temp or totalHP depending on _isTemp.
        private int previousValue;


      
        public HealEntitiesCommand(List<Pair<CombatData, EncounterCard>> tokens, int amount, bool isTemp)
        {
            _tokens = tokens;
            _amount = amount;
            _isTemp = isTemp;
        }

        public void Do()
        {
            foreach (var data_card in _tokens)
            {
                previousValue = _isTemp ? data_card.First.TempHP : data_card.First.Damage;
                if (_isTemp)
                {
                    data_card.First.TempHP = Math.Max(_amount, data_card.First.TempHP);
                }
                else
                {
                    data_card.First.Damage -= _amount;
                }
            }
        }

        public void Undo()
        {
            foreach (var data_card in _tokens)
            {
                if (_isTemp)
                {
                    data_card.First.TempHP = previousValue;
                }
                else
                {
                    data_card.First.Damage = previousValue;
                }
            }
        }


        //private static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        //{
        //    if (val.CompareTo(min) < 0) return min;
        //    else if (val.CompareTo(max) > 0) return max;
        //    else return val;
        //}
    }
}
