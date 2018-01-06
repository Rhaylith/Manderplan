using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Masterplan.Commands
{
    public class UndoQueue : Stack<ICommand>
    {
    }
}
