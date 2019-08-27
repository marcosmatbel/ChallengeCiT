using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteWebAPI.Services.Models
{
    public class Dados
    {
        public static List<Coberturas> ObterCoberturas()
        {
            List<Coberturas> dadosCobertura = new List<Coberturas>();
            dadosCobertura.Add(new Coberturas()
            {
                Id=1,
                Nome= "Morte Acidental",
                Premio = 100,
                Valor = 50000,
                Obrigatorio= "S"
            });
            dadosCobertura.Add(new Coberturas()
            {
                Id = 2,
                Nome = "Quebra de Ossos",
                Premio = 30,
                Valor = 5000,
                Obrigatorio = "N"
            });
            dadosCobertura.Add(new Coberturas()
            {
                Id = 3,
                Nome = "Internacao Hospitalars",
                Premio = 50,
                Valor = 10000,
                Obrigatorio = "N"
            });
            dadosCobertura.Add(new Coberturas()
            {
                Id = 4,
                Nome = "Assistencia Funeraria",
                Premio = 10,
                Valor = 2500,
                Obrigatorio = "N"
            });
            dadosCobertura.Add(new Coberturas()
            {
                Id = 5,
                Nome = "Invalidez Permanente",
                Premio = 130,
                Valor = 90000,
                Obrigatorio = "S"
            });
            dadosCobertura.Add(new Coberturas()
            {
                Id = 6,
                Nome = "Assistencia Odontologia Emergencial",
                Premio = 10,
                Valor = 2500,
                Obrigatorio = "N"
            });
            dadosCobertura.Add(new Coberturas()
            {
                Id = 7,
                Nome = "Diária Incapacidade Temporária",
                Premio = 30,
                Valor = 5000,
                Obrigatorio = "N"
            });
            dadosCobertura.Add(new Coberturas()
            {
                Id = 8,
                Nome = "Invalidez Funcional",
                Premio = 80,
                Valor = 40000,
                Obrigatorio = "S"
            });
            dadosCobertura.Add(new Coberturas()
            {
                Id = 9,
                Nome = "Doenças Graves",
                Premio = 100,
                Valor = 50000,
                Obrigatorio = "N"
            });
            dadosCobertura.Add(new Coberturas()
            {
                Id = 10,
                Nome = "Diagnostico de Cancer",
                Premio = 50,
                Valor = 10000,
                Obrigatorio = "N"
            });
            return dadosCobertura;
        }

        public static List<Proponente> ObterProponente()
        {
            List<Proponente> dados = new List<Proponente>();
            dados.Add(new Proponente()
            {
                Id= 1,
                Nome = "José da Silva",
                DtNascimento = Convert.ToDateTime("1986-08-26"),
                Endereco = new Endereco
                {
                    logradouro = "Rua das Flores, 15",
                    bairro = "Jardim Floresta",
                    cep = "14500-000",
                    cidade = "São Paulo",
                },
                ProponenteCoberturas = new List<ProponenteCoberturas> {
                    new ProponenteCoberturas() { CoberturasId = 1, ProponenteId = 1 },
                    new ProponenteCoberturas() { CoberturasId = 3, ProponenteId = 1 },
                    new ProponenteCoberturas() { CoberturasId = 4, ProponenteId = 1 },
                },

            });

            return dados;
        }



    }
}

