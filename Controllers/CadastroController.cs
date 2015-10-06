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
	public class CadastroController : ApiController
	{

		//CONSULTAR CADASTROS


		// GET api/Cadastro/ObterTodos
		[HttpGet]
		public IEnumerable<Cadastro> ObterTodos()
		{
			SqlConnection conn = new SqlConnection("Server=tcp:wwzrd9x4jx.database.windows.net,1433;Database=mais_sangue;User ID=mais_sangue@wwzrd9x4jx;Password=M@is$angue;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
			conn.Open();

			SqlCommand cmd;
			SqlDataReader reader;
			List<Cadastro> lista = new List<Cadastro>();
			Cadastro c;

			cmd = new SqlCommand("SELECT codigo, nome, sobrenome, sexo, peso, tp_sangue, dt_nasc, email, tel FROM Cad_Usuario", conn);
			reader = cmd.ExecuteReader();

			//Obtém os registros, um por vez
			while (reader.Read() == true)
			{
				c = new Cadastro();
				c.Codigo = reader.GetInt32(0);
				c.Nome = reader.GetString(1);
				c.Sobrenome = reader.GetString(2);
				c.Sexo = reader.GetString(3);
				c.Peso = reader.GetInt32(4);
				c.TpSangue = reader.GetString(5);
				//c.Nasc = reader.GetDateTime(6);
				c.Email = reader.GetString(7);
				c.Fone = reader.GetString(8);

				lista.Add(c);
			}

			reader.Close();
			reader.Dispose();
			cmd.Dispose();

			//Fecha a conexão ao final pois ela não é mais necessária
			conn.Close();
			conn.Dispose();

			return lista;
		}




		//CONSULTA ESPECÍFICA


		// GET api/Cadastro/Obter?ra=XXXX
		[HttpGet]
		public Cadastro Obter(String Nome)
		{
			SqlConnection conn = new SqlConnection("Server=tcp:wwzrd9x4jx.database.windows.net,1433;Database=mais_sangue;User ID=mais_sangue@wwzrd9x4jx;Password=M@is$angue;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
			conn.Open();

			SqlCommand cmd;
			SqlDataReader reader;
			Cadastro c;
			 

			cmd = new SqlCommand("SELECT Nome FROM Cad_usuario WHERE Nome = @a", conn);
			cmd.Parameters.Add(new SqlParameter("@a", SqlDbType.VarChar));
			cmd.Parameters[0].Value = Nome;
			reader = cmd.ExecuteReader();

			//Obtém os registros, um por vez
			if (reader.Read() == true)
			{
				c = new Cadastro();
				c.Nome = Nome;
				c.Nome = reader.GetString(0);
			}

			else
			{
				c = null;
			}

			reader.Close();
			reader.Dispose();
			cmd.Dispose();

			//Fecha a conexão ao final pois ela não é mais necessária
			conn.Close();
			conn.Dispose();

			return c;
		}



        //LOGIN NO WEBSITE
        // GET api/Cadastro/Login
        [HttpPost]
        public int Login([FromBody]Cadastro d)
        {
            SqlConnection conn = new SqlConnection("Server=tcp:wwzrd9x4jx.database.windows.net,1433;Database=mais_sangue;User ID=mais_sangue@wwzrd9x4jx;Password=M@is$angue;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
            conn.Open();

            SqlCommand cmd;
            SqlDataReader reader;

            cmd = new SqlCommand("SELECT cod_doador FROM cad_doador WHERE email = @a AND senha = @b", conn);
            cmd.Parameters.Add(new SqlParameter("@a", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@b", SqlDbType.VarChar));


            cmd.Parameters[0].Value = d.Email;
            cmd.Parameters[1].Value = d.Senha;

            int cod;
            reader = cmd.ExecuteReader();
            if (reader.Read() == true)
            {
                cod = reader.GetInt32(0);
            }
            else
            {
                cod = -1;
            }

            cmd.Dispose();

            conn.Close();
            conn.Dispose();

            return cod;
        }

		//CRIAR NOVO CADASTRO NA TABELA TB_USUARIO

		// POST api/Cadastro/Criar
		[HttpPost]
		public Cadastro Criar([FromBody]Cadastro c)
		{
			SqlConnection conn = new SqlConnection("Server=tcp:wwzrd9x4jx.database.windows.net,1433;Database=mais_sangue;User ID=mais_sangue@wwzrd9x4jx;Password=M@is$angue;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
			conn.Open();

			SqlCommand cmd;

			//Se fosse necessário saber o id do registro criado:
			//**************
            cmd = new SqlCommand("INSERT INTO cad_doador (nome, sobrenome, sexo, peso, tp_sangue, dt_nasc, email, tel, senha) OUTPUT INSERTED.cod_doador VALUES (@b, @c, @d, @e, @f, @g, @h, @i, @j)", conn);
			//**************

            cmd.Parameters.Add(new SqlParameter("@b", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@c", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@d", SqlDbType.Char));
            cmd.Parameters.Add(new SqlParameter("@e", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@f", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@g", SqlDbType.DateTime));
            cmd.Parameters.Add(new SqlParameter("@h", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@i", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@j", SqlDbType.VarChar));

            cmd.Parameters[0].Value = c.Nome;
            cmd.Parameters[1].Value = c.Sobrenome;
            cmd.Parameters[2].Value = c.Sexo;
            cmd.Parameters[3].Value = c.Peso;
            cmd.Parameters[4].Value = c.TpSangue;
            cmd.Parameters[5].Value = DateTime.Parse(c.Nasc);
            cmd.Parameters[6].Value = c.Email;
            cmd.Parameters[7].Value = c.Fone;
            cmd.Parameters[8].Value = c.Senha;
			

			try
			{
				c.Codigo = (int)cmd.ExecuteScalar();
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