using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteWebAPI.Services.Models
{
    public class ProponenteCoberturas
    {
        public int Id { get; set; }

        public virtual Proponente Proponente { get; set; }
        public int ProponenteId { get; set; }

        public virtual Coberturas Coberturas { get; set; }
        public int CoberturasId { get; set; }


    }
}