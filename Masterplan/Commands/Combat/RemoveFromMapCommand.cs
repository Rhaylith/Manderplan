using System.Collections.Generic;
using System.Drawing;
using Masterplan.Data;

namespace Masterplan.Commands.Combat
{
    public class RemoveFromMapCommand : ICommand
    {
        public List<RemoveEffectCommand> EffectsToRemove = new List<RemoveEffectCommand>();
        public List<AddRemoveLinkCommand> LinksToRemove = new List<AddRemoveLinkCommand>();

        protected CombatData _data;
        protected Point prevoiusLocation;
        
        public RemoveFromMapCommand(CombatData data)
        {
            _data = data;
        }

        public virtual void Do()
        {
            this.prevoiusLocation = _data.Location;
            _data.Location = CombatData.NoPoint;

            foreach (var effect in this.EffectsToRemove)
            {
                // Call this manually but don't add it to the undo queue individually since it's part of this command
                effect.Do();
            }

            foreach (var link in this.LinksToRemove)
            {
                // Call this manually but don't add it to the undo queue individually since it's part of this command
                link.Do();
            }

        }

        public virtual void Undo()
        {
            _data.Location = this.prevoiusLocation;

            foreach (var effect in this.EffectsToRemove)
            {
                effect.Undo();
            }

            foreach (var link in this.LinksToRemove)
            {
                link.Undo();
            }

        }
    }
}
