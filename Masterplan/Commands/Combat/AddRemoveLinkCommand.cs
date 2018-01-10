using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Masterplan.Data;

namespace Masterplan.Commands.Combat
{
    public class AddRemoveLinkCommand : ICommand
    {
        public enum AddRemoveOption
        {
            Add = 0,
            Remove = 1,
        }

        private AddRemoveOption _option;
        private List<TokenLink> _tokenLinks;
        private TokenLink _newLink;

        public AddRemoveLinkCommand(AddRemoveOption option, List<TokenLink> tokenLinks, TokenLink newLink)
        {
            _option = option;
            _tokenLinks = tokenLinks;
            _newLink = newLink;
        }

        public void Do()
        {
            if (_option == AddRemoveOption.Add)
            {
                _tokenLinks.Add(_newLink);
            }
            else
            {
                _tokenLinks.Remove(_newLink);
            }
        }

        public void Undo()
        {
            if (_option == AddRemoveOption.Add)
            {
                _tokenLinks.Remove(_newLink);
            }
            else
            {
                _tokenLinks.Add(_newLink);
            }
        }
    }
}
