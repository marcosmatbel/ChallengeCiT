using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TesteWebAPI.Services.Models
{
    public class City
    {
        public string longitude { get; set; }
        public string name_uri { get; set; }
        public string name { get; set; }
        public string uf { get; set; }
        public string pais { get; set; }
        public string summary { get; set; }
        public string latitude { get; set; }
        public int id { get; set; }


    }
    public class RootObject
    {
        public List<City> cities { get; set; }
    }
}