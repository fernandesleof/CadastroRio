using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CadastroRio.Models
{
    public class Cliente
    {
        public int idCliente { get; set; }
        public String nomeCliente { get; set; }
        public String cpfCliente { get; set; }
        public DateTime dataNascimentoCliente { get; set; }
        public String generoCliente { get; set; }

        public virtual List<Telefone> Telefones { get; set; }

    }
}