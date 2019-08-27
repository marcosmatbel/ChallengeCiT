using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteWebAPI.Services.Models
{
    public class Proponente
    {
        public int Id { get; set; }

        public string Nome {get;set;}

        public DateTime DtNascimento { get; set; }

        public virtual Endereco Endereco { get; set; }

        public virtual IList<ProponenteCoberturas> ProponenteCoberturas { get; set; }

    }
}

