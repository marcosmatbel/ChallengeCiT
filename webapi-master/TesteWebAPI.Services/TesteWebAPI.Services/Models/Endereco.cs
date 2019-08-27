using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteWebAPI.Services.Models
{
    public class Endereco
    {
        public int Id { get; set; }

        public string logradouro { get; set; }

        public string bairro { get; set; }

        public string cep { get; set; }

        public string cidade { get; set; }

        //public int? Proponented { get; set; }
    }
}