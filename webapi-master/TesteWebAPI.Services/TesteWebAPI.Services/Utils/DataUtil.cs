using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TesteWebAPI.Services.Utils
{
    public class DataUtil
    {
        public static String DataHojeUs
        {
            get
            {
                var data = DateTime.Now;
                return data.Date.ToString("yyyy-MM-dd");
            }
        }
        public static String DataAmanhaUs
        {
            get
            {
                var data = DateTime.Now;
                data = data.AddDays(1);
                return data.Date.ToString("yyyy-MM-dd");
            }
        }
        public static IEnumerable<DateTime> DateRange(DateTime fromDate, DateTime toDate)
        {
            return Enumerable.Range(0, toDate.Subtract(fromDate).Days + 1)
                             .Select(d => fromDate.AddDays(d));
        }
        public static DateTime DiaUtil(DateTime dt)
        {
            while (true)
            {
                if (dt.DayOfWeek == DayOfWeek.Saturday)
                {
                    dt = dt.AddDays(2);
                    return DiaUtil(dt);
                }
                else if (dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    dt = dt.AddDays(1);
                    return DiaUtil(dt);
                }
                else if (Feriado(dt) == true)
                {
                    dt = dt.AddDays(1);
                    return DiaUtil(dt);
                }
                else return dt;
            }
        }
        public static DateTime QuintoDiaUtil(DateTime Data)
        {
            Int32 primeiroDiaUtil = RetornaPrimeiroDiaUtil(Data);
            Int32 auxDiasUteisLocalizados = 1;
            Int32 auxContadorDiaVerificados = 1;

            DateTime quintoDiaUtil = Convert.ToDateTime(Data);
            DateTime dataPrimeiroDiaUtil = new DateTime(Data.Year, Data.Month, primeiroDiaUtil);

            while (auxDiasUteisLocalizados < 5)
            {
                quintoDiaUtil = dataPrimeiroDiaUtil.AddDays(auxContadorDiaVerificados);

                if (VerificaDiaUtil(quintoDiaUtil))
                {
                    auxDiasUteisLocalizados++;
                }
                auxContadorDiaVerificados++;
            }

            return Convert.ToDateTime(quintoDiaUtil);
        }
        private static int RetornaPrimeiroDiaUtil(DateTime Data)
        {
            DateTime primeiroDiaMes = new DateTime(Data.Year, Data.Month, 1);

            while (!VerificaDiaUtil(primeiroDiaMes))
            {
                primeiroDiaMes = primeiroDiaMes.AddDays(1f);
            }

            return primeiroDiaMes.Day;
        }
        private static bool VerificaDiaUtil(DateTime DiaMes)
        {
            if (DiaMes.DayOfWeek == DayOfWeek.Saturday || DiaMes.DayOfWeek == DayOfWeek.Sunday ||
                Feriado(DiaMes))
            {
                return false;
            }

            return true;
        }
        public static bool Feriado(DateTime dt)
        {
            //verificar em banco de dados
            return false;

        }

    }
}