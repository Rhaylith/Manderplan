using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Masterplan.Data;
using Utils;

namespace Masterplan.Commands.Combat
{
    public class MoveTokenCommand : ICommand
    {
        private IToken _token;
        private CombatData _data;
        private Point _destLocation;
        private Point _startLocation;

        public MoveTokenCommand(IToken token, Point startLocation, Point destLocation)
        {
            _startLocation = startLocation;
            _destLocation = destLocation;
            _token = token;

            int _distance = MMath.CalcDistance(destLocation, startLocation);
            if (_token is CreatureToken)
            {
                _data = (_token as CreatureToken).Data;
            }
            else if (_token is Hero)
            {
                _data = (_token as Hero).CombatData;
            }
            else if (_token is CustomToken)
            {
                _data = (_token as CustomToken).Data;
            }
        }
        public void Do()
        {
            _data.Location = _destLocation;
        }

        public void Undo()
        {
            _data.Location = _startLocation;
        }
    }
}
