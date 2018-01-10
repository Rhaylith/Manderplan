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
            RunCommand(command);
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

        private void RunCommand(ICommand command)
        {
            command.Do();
            CallListenersForCommand(command);
            _UndoQueue.Push(command);
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
