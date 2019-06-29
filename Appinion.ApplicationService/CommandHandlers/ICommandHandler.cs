using Appinion.ApplicationService.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.ApplicationService.CommandHandlers
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        IResultCommand ResultCommand { get; }
        void Execute(TCommand command);
    }
}
