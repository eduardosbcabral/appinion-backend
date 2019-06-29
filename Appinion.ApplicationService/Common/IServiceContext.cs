using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.ApplicationService.Common
{
    public interface IServiceContext
    {
        string UsuarioAtual { get; }
        int UsuarioAtualId { get; }
        void SetUsuarioAtual(string usuarioId);
        void AddParam(string attribute, string value);
        string GetValue(string attribute);
        string ParamsToJson();
    }
}
