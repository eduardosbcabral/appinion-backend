using Appinion.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.ApplicationService.Commands
{
    public interface ICommand
    {
        TransacaoOpt Operacao { get; }
    }
}
