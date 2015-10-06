using EP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Data;

namespace EP.Controllers
{
    public class Cad_doacaoController : ApiController
    {
        //CRIAR NOVO CADASTRO DE DOAÇÃO NA TABELA Cad_doacao

        // POST api/Cadastro/Cad_doacao
        [HttpPost]
        public Cad_doacao Cadastrar([FromBody]Cad_doacao c)
        {
            SqlConnection conn = new SqlConnection("Server=tcp:wwzrd9x4jx.database.windows.net,1433;Database=mais_sangue;User ID=mais_sangue@wwzrd9x4jx;Password=M@is$angue;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
            conn.Open();

            SqlCommand cmd;

            //Se fosse necessário saber o id do registro criado:
            //**************
            cmd = new SqlCommand("INSERT INTO cad_doacao (dt_doacao, nome_hosp, cod_doador) OUTPUT INSERTED.reg_doacao VALUES (@b, @c, @d)", conn);
            //**************

            cmd.Parameters.Add(new SqlParameter("@b", SqlDbType.DateTime));
            cmd.Parameters.Add(new SqlParameter("@c", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@d", SqlDbType.Int));


            cmd.Parameters[0].Value = DateTime.Parse(c.Dtdoacao);
            cmd.Parameters[1].Value = c.Hosp;
            cmd.Parameters[2].Value = c.Coddoador;


            try
            {
                c.Registro = (int)cmd.ExecuteScalar();
            }

            catch (Exception ex)
            {
                c = null;
            }

            //**************
            cmd.Dispose();

            //Fecha a conexão ao final pois ela não é mais necessária
            conn.Close();
            conn.Dispose();

            return c;
        }
    }
}