﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Masterplan.Commands
{
    public class CompoundCommand : ICommand
    {
        List<ICommand> commands = new List<ICommand>();

        public int SubCommandCount
        {
            get { return commands.Count; }
        }

        public void AddCommand(ICommand command)
        {
            commands.Add(command);
        }

        public void Do()
        {
            foreach(var command in commands)
            {
                command.Do();
            }
        }

        public void Undo()
        {
            for(int i=commands.Count-1;i>=0;--i)
            {
                commands[i].Undo();
            }
        }
    }
}
