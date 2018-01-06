using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Masterplan.Commands;
using Masterplan.Data.Combat;

namespace Masterplan.Commands.Combat
{
    public class InitiativeAdvanceCommand : ICommand
    {
        private InitiativeList m_initList;
        public InitiativeAdvanceCommand(InitiativeList initList)
        {
            m_initList = initList;
        }

        public void Do()
        {
            this.m_initList.AdvanceNextTurn();
        }

        public void Undo()
        {
            this.m_initList.AdvanceToPrevTurn();
        }
    }
}
