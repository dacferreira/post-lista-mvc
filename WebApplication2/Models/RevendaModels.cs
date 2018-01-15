using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class RevendaModels
    {
        public RevendaModels()
        {
            Enderecos = new List<EnderecoModels>();
        }

        public long Id { get; set; }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }

        public int IdEndereco { get; set; }
        public string Logradouro { get; set; }
        public string CEP { get; set; }
        public int Numero { get; set; }
        public List<EnderecoModels> Enderecos { get; set; }

    }

    public class EnderecoModels
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string CEP { get; set; }
        public int Numero { get; set; }
    }
}