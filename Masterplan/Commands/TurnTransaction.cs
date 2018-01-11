using System.Collections.Generic;

namespace Masterplan.Commands
{
    public class TurnTransaction
    {
        public UndoQueue UndoQueue = new UndoQueue();
        public UndoQueue RedoQueue = new UndoQueue();
    }
}
