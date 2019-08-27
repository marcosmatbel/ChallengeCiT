using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TesteWebAPI.Services.Models;
using TesteWebAPI.Services.Utils;

namespace TesteWebAPI.Services.Controllers
{
    public class priceController : ApiController
    {
        private RootObject cidades;
        private int idade;
        private decimal fatorAcrescimo;
        private decimal fatorDesconto;
        [HttpPost]
        [AcceptVerbs("POST")]
        public HttpResponseMessage Post(Cotacao Cotacao)
        {
            if(Cotacao==null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "FAVOR PREENCHER OS CAMPOS CORRETAMENTE");


            fatorAcrescimo = 0;
            fatorDesconto = 0;
            SolicitacaoCotacaoProponente solicitacaoCotacao = new SolicitacaoCotacaoProponente();

            try
            {
                decimal ValorCoberturasSelecionadas = Dados.ObterCoberturas().Where(w => Cotacao.coberturas.Contains(w.Id)).Sum(s => s.Premio);
                decimal CoberturasTotal = Dados.ObterCoberturas().Where(w => Cotacao.coberturas.Contains(w.Id)).Sum(s => s.Valor);

                #region CARREGANDO AS CIDADES VÁLIDAS PARA UM OBJETO
                //CARREGANDO AS CIDADES VÁLIDAS PARA UM OBJETO
                string localJsonCities = HttpContext.Current.Server.MapPath("~/App_Data/cities.json");
                string JsonStr = string.Empty;
                using (StreamReader sr = new StreamReader(localJsonCities))
                {
                    JsonStr = sr.ReadToEnd();
                    cidades = JsonConvert.DeserializeObject<RootObject>(JsonStr);
                }
                #endregion

                #region VALIDAÇÕES
                DateTime DataNascimento;
                if(!DateTime.TryParse(Cotacao.nascimento,out DataNascimento))
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "DATA NASCIMENTO INVÁLIDA");

                //VERIFICA A IDADE, NAO VOU ME PREOCUPAR COM PARSE POIS SE DER ERRO DE PARSE DATETIME VAI PARA O CATCH
                idade = (DateTime.Now.Year - Convert.ToDateTime(Cotacao.nascimento).Year);
                if(idade < 18)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "COTAÇÃO PERMITIDA SOMENTE PARA MAIORES DE 18 ANOS");
                }
               
                

                //CHECA SE A CIDADE ESTA NA LISTA DE CIDADES
                var verificaCidade = cidades.cities.Where(x => x.name.ToLower() == Cotacao.endereco.cidade.ToLower()).FirstOrDefault();
                if (verificaCidade == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "COTAÇÃO NÃO PERMITIDA PARA SUA CIDADE");

                // VALIDAR CEP
                if(!StringUtil.ValidaCEP(Cotacao.endereco.cep))
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CEP INVÁLIDO");

                //VALIDAR COBERTURAS
                if(Cotacao.coberturas.Count > 4)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "É PERMITIDO ATÉ 4 COBERTURAS PARA SOLICITAÇÃO DE COTAÇÃO");

                #endregion

                #region Calcular Fator Desconto/Acrescimmo
                if (idade >= 18 && idade <= 30)
                {
                    fatorAcrescimo = (8 * (30 - idade));

                }
                if (idade >= 31 && idade <= 45)
                {
                    fatorDesconto = (2 * (idade - 30));
                }
                #endregion

                #region CALCULAR PREMIO
                var AcrescimoPremio = ((ValorCoberturasSelecionadas / 100) * fatorAcrescimo);

                var DescontoPremio = ((ValorCoberturasSelecionadas / 100) * fatorDesconto);

                ValorCoberturasSelecionadas = (ValorCoberturasSelecionadas + AcrescimoPremio);
                ValorCoberturasSelecionadas = (ValorCoberturasSelecionadas - DescontoPremio);

                solicitacaoCotacao.Premio = ValorCoberturasSelecionadas;
                #endregion

                #region CALCULAR PARCELAS
                if (ValorCoberturasSelecionadas <= 500)
                {
                    solicitacaoCotacao.Parcelas = 1;
                }
                else if (ValorCoberturasSelecionadas >= 501 &&
                    ValorCoberturasSelecionadas <= 1000)
                {
                    solicitacaoCotacao.Parcelas = 2;
                }
                else if (ValorCoberturasSelecionadas >= 1001 &&
                    ValorCoberturasSelecionadas <= 2000)
                {
                    solicitacaoCotacao.Parcelas = 3;
                }
                else if (ValorCoberturasSelecionadas >= 2000)
                {
                    solicitacaoCotacao.Parcelas = 4;
                }
                solicitacaoCotacao.ValorParcela = (solicitacaoCotacao.Premio / solicitacaoCotacao.Parcelas);
                #endregion

                #region CALCULAR DATA PROX VENCIMENTO
                solicitacaoCotacao.PrimeiroVencimento = DataUtil.QuintoDiaUtil(DateTime.Now);
                #endregion

                solicitacaoCotacao.CoberturaTotal = CoberturasTotal;

                var message = Request.CreateResponse(HttpStatusCode.Created, solicitacaoCotacao);
                 message.Headers.Location = new Uri(Request.RequestUri + solicitacaoCotacao.Id.ToString());

                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
