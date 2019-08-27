using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TesteWebAPI.Services.Models
{
    public class SolicitacaoCotacaoProponente
    {
        public int Id { get; set; }


        [DisplayName("Prêmio")]
        public decimal Premio { get; set; }

        [DisplayName("Parcelas")]
        public int Parcelas { get; set; }

        [DisplayName("Valor Parcela")]
        public decimal ValorParcela { get; set; }

        [DisplayName("Primeiro Vencimento")]
        public DateTime? PrimeiroVencimento { get; set; }

        [DisplayName("Cobertura Total")]
        public decimal CoberturaTotal { get; set; }
    }
}