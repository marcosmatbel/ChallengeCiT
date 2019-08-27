using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TesteWebAPI.Services.Models
{
    public class Coberturas
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        [DisplayName("Prêmio")]
        public decimal Premio { get; set; }

        public decimal Valor { get; set; }

        [DisplayName("Obrigatório")]
        public string Obrigatorio { get; set; }

       
    }
}