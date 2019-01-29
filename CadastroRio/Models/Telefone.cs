using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CadastroRio.Models
{
    public class Telefone
    {
        public int idTelefone { get; set; }
        public int idCliente { get; set; }
        public String ddd{ get; set; }
        public String numero { get; set; }
        public int qtdTelefone { get; set; }
    }
}