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
        private List<CombatData> _datas;
        private int _amount;
        private bool _isTemp;

        // For undo, either temp or totalHP depending on _isTemp.
        private int previousValue;


      
        public HealEntitiesCommand(List<CombatData> tokens, int amount, bool isTemp)
        {
            _datas = tokens;
            _amount = amount;
            _isTemp = isTemp;
        }

        public void Do()
        {
            foreach (var data_card in _datas)
            {
                previousValue = _isTemp ? data_card.TempHP : data_card.Damage;
                if (_isTemp)
                {
                    data_card.TempHP = Math.Max(_amount, data_card.TempHP);
                }
                else
                {
                    _amount = Math.Min(data_card.Damage, _amount);
                    data_card.Damage -= _amount;
                }
            }
        }

        public void Undo()
        {
            foreach (var data_card in _datas)
            {
                if (_isTemp)
                {
                    data_card.TempHP = previousValue;
                }
                else
                {
                    data_card.Damage = previousValue;
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
