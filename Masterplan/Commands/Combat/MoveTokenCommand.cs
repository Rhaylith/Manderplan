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
                CreatureToken creature = _token as CreatureToken;
                creature.Data.Location = destLocation;
            }
            if (_token is Hero)
            {
                Hero hero = _token as Hero;
                hero.CombatData.Location = destLocation;
            }
            if (_token is CustomToken)
            {
                CustomToken customToken = _token as CustomToken;
                customToken.Data.Location = startLocation;
            }
        }
        public void Do()
        {
            (_token as CreatureToken).Data.Location = _destLocation;
        }

        public void Undo()
        {
            (_token as CreatureToken).Data.Location = _startLocation;
        }
    }
}
