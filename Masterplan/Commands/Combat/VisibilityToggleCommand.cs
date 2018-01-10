using System;
using System.Collections.Generic;
using Masterplan.Data;

namespace Masterplan.Commands.Combat
{
    public class VisibilityToggleCommand : ICommand
    {
        private List<CombatData> _entities;
        public VisibilityToggleCommand(List<CombatData> entities)
        {
            _entities = entities;
        }

        public void Do()
        {
            foreach(var entity in _entities)
            {
                entity.Visible = !entity.Visible;
            }
        }

        public void Undo()
        {
            foreach(var entity in _entities)
            {
                entity.Visible = !entity.Visible;
            }
        }
    }
}
