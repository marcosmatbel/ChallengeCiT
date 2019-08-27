using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace TesteWebAPI.Services.Utils
{
    public class StringUtil
    {
        /// <summary>
        /// Capitaliza a frase
        /// </summary>
        /// <param name="frase"></param>
        /// <returns></returns>
        public static String FraseCapitalize(String frase)
        {
            if (frase != null)
            {
                frase = frase.Trim();
                if (frase.Length < 1) return frase;
                if (frase.LastIndexOf(' ') == -1) return frase;
                string[] partes = frase.Split(' ');
                frase = "";
                for (int i = 0; i < partes.Count(); i++)
                {
                    partes[i] = StringUtil.PrimeiraLetraUC(partes[i]);
                }
                return String.Join(" ", partes);
            }
            return "";
        }
        //Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

        public static String PrimeiraLetraUC(String p)
        {
            if (p != null)
            {
                p = p.Trim();
                if (p != null)
                {
                    if (p.Length > 0)
                    {
                        string pt = p.Substring(0, 1);
                        pt = pt.ToUpper();
                        p = StringUtil.SubstituirPrimeiraOccorrencia(p, p.Substring(0, 1), pt);
                    }
                }
                return p;
            }
            return "";
        }

        public static String AjustarLogin(String usuario)
        {
            if (usuario != null)
            {
                if (usuario.Contains("@"))
                {
                    usuario = usuario.Substring(0, usuario.IndexOf("@"));
                }
                return usuario.ToLower().Trim();
            }
            return "";
        }

        public static string SubstituirPrimeiraOccorrencia(string texto, string buscarPor, string substituirPor)
        {
            int pos = texto.IndexOf(buscarPor);
            if (pos < 0)
            {
                return texto;
            }
            return texto.Substring(0, pos) + substituirPor + texto.Substring(pos + buscarPor.Length);
        }

        public static string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            int place = Source.LastIndexOf(Find);

            if (place == -1)
                return string.Empty;

            string result = Source.Remove(place, Find.Length).Insert(place, Replace);
            return result;
        }
        /// <summary>
        /// Remove caracteres não numéricos
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveNaoNumericos(string text)
        {
            if (text != null)
            {
                System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[^0-9]");
                string ret = reg.Replace(text, string.Empty);
                return ret;
            }
            return "";
        }

        /// <summary>
        /// Valida se um cpf é válido
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static bool ValidaCPF(string cpf)
        {
            if (cpf == null) return false;
            //Remove formatação do número, ex: "123.456.789-01" vira: "12345678901"
            cpf = StringUtil.RemoveNaoNumericos(cpf);

            if (cpf.Length > 11)
                return false;

            while (cpf.Length != 11)
                cpf = '0' + cpf;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;

            if (igual || cpf == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(cpf[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }

        public static bool isCPFCNPJ(string cpfcnpj, bool vazio)
        {
            if (string.IsNullOrEmpty(cpfcnpj))
                return vazio;
            else
            {
                int[] d = new int[14];
                int[] v = new int[2];
                int j, i, soma;
                string Sequencia, SoNumero;

                SoNumero = Regex.Replace(cpfcnpj, "[^0-9]", string.Empty);

                //verificando se todos os numeros são iguais
                if (new string(SoNumero[0], SoNumero.Length) == SoNumero) return false;

                // se a quantidade de dígitos numérios for igual a 11
                // iremos verificar como CPF
                if (SoNumero.Length == 11)
                {
                    for (i = 0; i <= 10; i++) d[i] = Convert.ToInt32(SoNumero.Substring(i, 1));
                    for (i = 0; i <= 1; i++)
                    {
                        soma = 0;
                        for (j = 0; j <= 8 + i; j++) soma += d[j] * (10 + i - j);

                        v[i] = (soma * 10) % 11;
                        if (v[i] == 10) v[i] = 0;
                    }
                    return (v[0] == d[9] & v[1] == d[10]);
                }
                // se a quantidade de dígitos numérios for igual a 14
                // iremos verificar como CNPJ
                else if (SoNumero.Length == 14)
                {
                    Sequencia = "6543298765432";
                    for (i = 0; i <= 13; i++) d[i] = Convert.ToInt32(SoNumero.Substring(i, 1));
                    for (i = 0; i <= 1; i++)
                    {
                        soma = 0;
                        for (j = 0; j <= 11 + i; j++)
                            soma += d[j] * Convert.ToInt32(Sequencia.Substring(j + 1 - i, 1));

                        v[i] = (soma * 10) % 11;
                        if (v[i] == 10) v[i] = 0;
                    }
                    return (v[0] == d[12] & v[1] == d[13]);
                }
                // CPF ou CNPJ inválido se
                // a quantidade de dígitos numérios for diferente de 11 e 14
                else return false;
            }
        }
        public static String formataCPF(String cpf)
        {
            if (cpf != null)
            {
                if (cpf.Length == 11)
                {
                    return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
                }
                return cpf;
            }
            return "";
        }
        public static String formataCelular(String celular)
        {
            if (celular != null)
            {
                if (celular.Length == 11)
                {
                    return Convert.ToUInt64(celular).ToString(@"(00)00000-0000");
                }
                else if (celular.Length == 10)
                {
                    return Convert.ToUInt64(celular).ToString(@"(00)0000-0000");
                }

                return celular;
            }
            return "";
        }

        private static Random random = new Random((int)DateTime.Now.Ticks);

        public static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public static String NullParaEmpty(String valor)
        {
            if (valor == null) return "";
            if (valor == "NULL") return "";
            return valor.Trim();
        }

        public static bool hasEmpty(params string[] strings)
        {
            foreach (String s in strings)
            {
                String st = s ?? "";
                st = st.Trim();
                if (String.IsNullOrEmpty(st))
                    return true;
            }
            return false;
        }

        public static bool ValidaCEP(string cep)
        {
            Regex Rgx = new Regex(@"^\d{5}-\d{3}$");

            if (!Rgx.IsMatch(cep))
                return false;
            else
                return true;

        }
    }
}