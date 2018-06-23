using System.Collections.Generic;
using System.Drawing;
using Masterplan.Data;

namespace Masterplan.Commands.Combat
{
    public class RemoveFromMapCommand : ICommand
    {
        public List<RemoveEffectCommand> EffectsToRemove = new List<RemoveEffectCommand>();
        public List<AddRemoveLinkCommand> LinksToRemove = new List<AddRemoveLinkCommand>();

        public bool RemoveFromInitiative = false;

        protected CombatData _data;
        protected Point prevoiusLocation;
        
        // HACK!
        public bool RefreshTerrainLayers = false;

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

            // HACK #1
            if (this.RemoveFromInitiative)
            {
                _data.SkipInitiative = true;
            }

            // HACK! #2
            if (this.RefreshTerrainLayers)
            {
                Masterplan.UI.CombatForm.TerrainLayersNeedRefresh = true;
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

            if (this.RemoveFromInitiative)
            {
                _data.SkipInitiative = false;
            }

            // HACK!
            if (this.RefreshTerrainLayers)
            {
                Masterplan.UI.CombatForm.TerrainLayersNeedRefresh = true;
            }
        }
    }
}
