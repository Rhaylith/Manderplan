using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Masterplan.Commands
{
    public class CommandManager
    {
        public delegate void MessageHandler();

        private Dictionary<Type, MessageHandler> ListenerMap = new Dictionary<Type, MessageHandler>();
        private UndoQueue _UndoQueue = new UndoQueue();
        private UndoQueue _RedoQueue = new UndoQueue();
        private static CommandManager _instance = null;
        private CommandManager() { }

        private CompoundCommand currentCompoundCommand = null;

        public static CommandManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CommandManager();
            }
            return _instance;
        }

        public void ExecuteCommand(ICommand command)
        {
            if (this.currentCompoundCommand != null)
            {
                this.currentCompoundCommand.AddCommand(command);
            }
            else
            {
                RunCommand(command);
            }
            _RedoQueue.Clear();
        }

        public void UndoLastCommand()
        {
            if (_UndoQueue.Count > 0)
            {
                ICommand command = _UndoQueue.Pop();
                command.Undo();
                _RedoQueue.Push(command);

                CallListenersForCommand(command);
            }
        }

        public void RedoNextCommand()
        {
            if (_RedoQueue.Count > 0)
            {
                ICommand command = _RedoQueue.Pop();
                RunCommand(command);
            }
        }

        public void RegisterListener(Type command, MessageHandler callback)
        {
            if (ListenerMap.ContainsKey(command))
            {
                ListenerMap[command] += callback;
            }
            else
            {
                ListenerMap.Add(command, callback);
            }
        }

        public void BeginCompoundCommand<T>() where T : CompoundCommand, new()
        {
            this.currentCompoundCommand = new T();
        }

        public void EndAndExecuteCompoundCommand()
        {
            if (this.currentCompoundCommand != null)
            {
                if (this.currentCompoundCommand.SubCommandCount > 0)
                {
                    this.RunCommand(this.currentCompoundCommand);
                }
                this.currentCompoundCommand = null;
            }
        }

        private void RunCommand(ICommand command)
        {
            command.Do();
            _UndoQueue.Push(command);
            CallListenersForCommand(command);
        }

        private void CallListenersForCommand(ICommand command)
        {
            // Find any listeners and invoke them
            Type commandType = command.GetType();
            if (ListenerMap.ContainsKey(commandType))
            {
                ListenerMap[commandType].Invoke();
            }
        }
    }
}
