using EP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Data.SqlClient;
using System.Data;
using EP.Utils;

namespace EP.Controllers
{
    public class EsqSenhaController : ApiController
    {
        [HttpGet]
        public string RecuperarSenha(String email)
        {

            SqlConnection conn = new SqlConnection("Server=tcp:wwzrd9x4jx.database.windows.net,1433;Database=mais_sangue;User ID=mais_sangue@wwzrd9x4jx;Password=M@is$angue;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
            
            conn.Open();
            SqlCommand cmd;
            SqlDataReader reader;
 

            cmd = new SqlCommand("SELECT senha FROM cad_doador WHERE email=@a", conn);
            cmd.Parameters.Add(new SqlParameter("@a", SqlDbType.VarChar));
            cmd.Parameters[0].Value = email;
            reader = cmd.ExecuteReader();
            
            string senha, erro = null;

            if (reader.Read() == true)
            {
                senha = reader.GetString(0);
                //enviar email com a senha
                RecSenha.SendSenha("mais.sangue@hotmail.com", email, "Sua senha", "Grupo Mais Sangue\n\nSua senha é: " + senha);
            }
            else
            {
                //email não existe
                senha = null;
                erro = "email nao existe";
            }

            reader.Close();
            reader.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            return erro;
        }
    }
}