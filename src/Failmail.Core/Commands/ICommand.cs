using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;

namespace Failmail.Core.Commands
{
    public interface ICommand
    {
        void Execute(IDocumentSession session);
    }
}
