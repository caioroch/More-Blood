using EP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using EP.Utils;

namespace EP.Controllers
{
    public class EnvioEmailController : ApiController
    {
        [HttpPost]
        public void enviar([FromBody]NotificarEmail c)
        {
            Notification.Send(c.name, c.email, c.subject, c.message);
        }
    }
}