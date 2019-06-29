using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.ApplicationService.Commands
{
    /// <summary>
    /// Classe que representa um comando básico.
    /// </summary>
    public class CommandID
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public CommandID()
        {

        }

        public CommandID(int id)
        {
            Id = id;
        }
    }
}
