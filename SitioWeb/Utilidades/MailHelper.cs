using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;

namespace SitioWeb.Utilidades
{
    public class MailHelper : IMailHelper
    {
        private readonly IConfiguration _configuration;

        public MailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendMail(string to, string cc, string subject, string body)
        {
            try
            {
                string from = _configuration["Mail:From"];
                string smtp = _configuration["Mail:Smtp"];
                string port = _configuration["Mail:Port"];
                string password = _configuration["Mail:Password"];

                MimeMessage message = new();
                message.From.Add(new MailboxAddress("Sol y Luna", from));

                if (!string.IsNullOrEmpty(cc))
                {
                    message.Cc.Add(new MailboxAddress("Cc", cc));
                }

                message.To.Add(new MailboxAddress("Usuario", to));
                message.Subject = subject;

                BodyBuilder bodyBuilder = new()
                {
                    HtmlBody = body
                };

                message.Body = bodyBuilder.ToMessageBody();

                using (SmtpClient client = new())
                {
                    client.Connect(smtp, int.Parse(port), false);
                    client.Authenticate(from, password);
                    client.Send(message);
                    client.Disconnect(true);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}


