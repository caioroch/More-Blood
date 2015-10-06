﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace EP.Utils
{
    public class RecSenha
    {
        public static bool SendSenha(string from, string to, string subject, string mensagem)
        {
            System.Net.Mail.SmtpClient s = null;
            try
            {
                s = new System.Net.Mail.SmtpClient("smtp.live.com", 587);
                s.EnableSsl = true;
                s.UseDefaultCredentials = false;
                s.Credentials = new System.Net.NetworkCredential("mais.sangue@hotmail.com", "M@is$angue");
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(
                    new System.Net.Mail.MailAddress(from),
                    new System.Net.Mail.MailAddress(to));
                message.Body = mensagem;
                message.BodyEncoding = Encoding.UTF8;
                message.Subject = subject;
                message.SubjectEncoding = Encoding.UTF8;
                s.Send(message);
                s.Dispose();

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (s != null)
                    s.Dispose();
            }
        }
    }
}