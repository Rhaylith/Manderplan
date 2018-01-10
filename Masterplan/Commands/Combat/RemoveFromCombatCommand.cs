using System.Collections.Generic;
using System.Drawing;
using Masterplan.Data;

namespace Masterplan.Commands.Combat
{
    public class RemoveFromCombatCommand : RemoveFromMapCommand
    {
        private Encounter _encounter;
        private IToken _token;

        public RemoveFromCombatCommand(IToken token, CombatData data, Encounter encounter) : base(data)
        {
            _encounter = encounter;
            _token = token;

        }

        public override void Do()
        {
            base.Do();

            if (_token is CreatureToken)
            {
                CreatureToken creature = _token as CreatureToken;
                EncounterSlot encounterSlot = _encounter.FindSlot(creature.SlotID);
                encounterSlot.CombatData.Remove(creature.Data);
            }
            else if (_token is Hero)
            {
                Hero hero = _token as Hero;
                //TODO:  Remove from initiative
                //hero.CombatData.Initiative = -2147483648;
                hero.CombatData.Location = CombatData.NoPoint;
            }
            else if (_token is CustomToken)
            {
                _encounter.CustomTokens.Remove(_token as CustomToken);
            }
        }

        public override void Undo()
        {
            base.Undo();

            if (_token is CreatureToken)
            {
                CreatureToken creature = _token as CreatureToken;
                EncounterSlot encounterSlot = _encounter.FindSlot(creature.SlotID);
                encounterSlot.CombatData.Add(creature.Data);
            }
            else if (_token is Hero)
            {
                Hero hero = _token as Hero;
                //TODO:  Remove from initiative
                //hero.CombatData.Initiative = -2147483648;
                //hero.CombatData.Location = CombatData.NoPoint;
            }
            else if (_token is CustomToken)
            {
                _encounter.CustomTokens.Add(_token as CustomToken);
            }
        }
    }
}
