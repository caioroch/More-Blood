using EP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace EP.Controllers
{
	public class NotificacaoController : ApiController
	{
		[HttpGet]
		public Notificacao Notif(int cod_doador)
		{
			SqlConnection conn = new SqlConnection("Server=tcp:wwzrd9x4jx.database.windows.net,1433;Database=mais_sangue;User ID=mais_sangue@wwzrd9x4jx;Password=M@is$angue;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");

			conn.Open();
			
			SqlCommand cmd = new SqlCommand(@"SELECT MAX(dt_doacao) ultimaDoacao,
											DATEADD(DAY,(CASE WHEN SEXO = 'F' THEN 90 ELSE 60 END), 
											MAX(dt_doacao)) ProximaDoacao
											FROM cad_doacao cd
											join cad_doador cd2 on cd.cod_doador = cd2.cod_doador
											WHERE cd.cod_doador = @a
											group by cd2.cod_doador, cd2.sexo", conn);

			cmd.Parameters.Add(new SqlParameter("@a", SqlDbType.Int));
			cmd.Parameters[0].Value = cod_doador;
						
            SqlDataReader reader = cmd.ExecuteReader();

			Notificacao n;

			if (reader.Read() == true)
			{
				n = new Notificacao();
				DateTime data = reader.GetDateTime(0);
				n.UltDoacao = data.ToString("yyyy-MM-dd");
				data = reader.GetDateTime(1);
				n.PrxDoacao = data.ToString("yyyy-MM-dd");
			}

			else
			{
				n = null; //Não existem registros
			}
			reader.Close();
			reader.Dispose();

			cmd.Dispose();
			conn.Close();
			conn.Dispose();

			return n;
		}

	}
}