using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private Stack<TurnTransaction> previousTurns = new Stack<TurnTransaction>();
        private Stack<TurnTransaction> futureTurns = new Stack<TurnTransaction>();
        private TurnTransaction currentTurn = new TurnTransaction();

        public bool HasFutureTurns
        {
            get { return this.futureTurns.Count > 0; }
        }

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
            if (_UndoQueue.Count == 0 && this.previousTurns.Count > 0)
            {
                this.StartTurn(this.previousTurns.Pop());
            }

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
            if (this._RedoQueue.Count == 0 && this.futureTurns.Count > 0)
            {
                this.previousTurns.Push(this.EndCurrentTurn());
                this.StartTurn(this.futureTurns.Pop());
            }

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
                var oldCompoundCommand = this.currentCompoundCommand;
                this.currentCompoundCommand = null;
                if (oldCompoundCommand.SubCommandCount > 0)
                {
                    this.RunCommand(oldCompoundCommand);
                }
            }
        }

        public void NewTurn()
        {
            TurnTransaction turn = this.EndCurrentTurn();
            this.previousTurns.Push(turn);
            this.StartTurn(new TurnTransaction());
        }

        public void BackupOneTurn()
        {
            if (this.previousTurns.Count > 0)
            {
                while (this._UndoQueue.Count > 0)
                {
                    ICommand command = _UndoQueue.Pop();
                    command.Undo();
                    this._RedoQueue.Push(command);
                }

                TurnTransaction turn = this.EndCurrentTurn();
                this.futureTurns.Push(turn);
                this.StartTurn(this.previousTurns.Pop());
            }
        }

        public void ForwardOneTurn()
        {
            Debug.Assert(this.HasFutureTurns);
            while(this._RedoQueue.Count > 0)
            {
                ICommand command = _RedoQueue.Pop();
                command.Do();
                this._UndoQueue.Push(command);
            }

            this.EndCurrentTurn();
            TurnTransaction turn = this.futureTurns.Pop();
            this.StartTurn(turn);
            while(this._RedoQueue.Count > 0)
            {
                ICommand command = this._RedoQueue.Pop();
                command.Do();
                this._UndoQueue.Push(command);
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

        private TurnTransaction EndCurrentTurn()
        {
            this.currentTurn.UndoQueue = this._UndoQueue;
            this.currentTurn.RedoQueue = this._RedoQueue;
            TurnTransaction turn = this.currentTurn;
            this.currentTurn = null;
            return turn;
        }

        private void StartTurn(TurnTransaction turn)
        {
            this.currentTurn = turn;
            this._UndoQueue = turn.UndoQueue;

            // Only reset Redoqueue if we're restoring a turn that already has one, otherwise we're just undoing over turn boundaries
            if (turn.RedoQueue.Count > 0)
            {
                this._RedoQueue = turn.RedoQueue;
            }
        }
    }
}
