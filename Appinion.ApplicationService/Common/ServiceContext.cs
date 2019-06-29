using Appinion.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Appinion.ApplicationService.Common
{
    public class ServiceContext : IServiceContext
    {
        private IList<KeyValuePair<string, string>> Params;
        public string UsuarioAtual { get; private set; }
        public int UsuarioAtualId
        {
            get { return Convert.ToInt32(UsuarioAtual); }
        }

        public ServiceContext()
        {
            Params = new List<KeyValuePair<string, string>>();
        }

        public void SetUsuarioAtual(string usuarioId)
        {
            UsuarioAtual = usuarioId;
        }

        public void AddParam(string attribute, string value)
        {
            Params.Add(new KeyValuePair<string, string>(attribute, value));
        }

        public string GetValue(string attribute)
        {
            if (Params.Any())
            {
                return Params.Where(x => x.Key == attribute)
                    .First()
                    .Value;
            }
            else
            {
                return string.Empty;
            }
        }

        public string ParamsToJson()
        {
            return FormatHelper.Serializer(Params);
        }

    }
}
