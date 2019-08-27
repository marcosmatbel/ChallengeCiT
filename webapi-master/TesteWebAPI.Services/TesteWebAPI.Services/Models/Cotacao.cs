using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TesteWebAPI.Services.Models
{
    public class Cotacao
    {
        public int Id { get; set; }

        [DisplayName("Nome")]
        public string nome { get; set; }

        [DisplayName("Dt Nascimento")]
        public string nascimento { get; set; }

        [DisplayName("Coberturas")]
        public List<int> coberturas { get; set; }

        public Endereco endereco { get; set; }
    }
}