using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EP.Models
{
    public class Cadastro
    {
        public int Codigo { get; set; }
        public String Nome { get; set; }
        public String Sobrenome { get; set; }
        public String Sexo { get; set; }
        public int Peso { get; set; }
        public String TpSangue { get; set; }
        public String Nasc { get; set; }
        public String Email { get; set; }
        public String Fone { get; set; }
        public String Senha { get; set; }
    }
}