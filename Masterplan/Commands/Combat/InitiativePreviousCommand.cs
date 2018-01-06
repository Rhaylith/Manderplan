using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Masterplan.Data.Combat;

namespace Masterplan.Commands.Combat
{
    public class InitiativePreviousCommand : ICommand
    {
        private InitiativeList m_initList;
        public InitiativePreviousCommand(InitiativeList initList)
        {
            m_initList = initList;
        }

        public void Do()
        {
            this.m_initList.AdvanceToPrevTurn();
        }

        public void Undo()
        {
            this.m_initList.AdvanceNextTurn();
        }
    }
}
