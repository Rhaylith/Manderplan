using System.Drawing;
using Masterplan.Data;
using Utils;

namespace Masterplan.Commands.Combat
{
    public class MoveTokenCommand : ICommand
    {
        private CombatData _data;
        private Point _destLocation;
        private Point _startLocation;

        public MoveTokenCommand(CombatData data, Point startLocation, Point destLocation)
        {
            _startLocation = startLocation;
            _destLocation = destLocation;
            _data = data;

            int _distance = Utils.MMath.CalcDistance(destLocation, startLocation);
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
