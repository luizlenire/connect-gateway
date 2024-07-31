using Api.AppCore.Email.Models;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Api.AppCore.Email.Controllers
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class Email
    {
        #region --> Private propeties. <--

        private const string server = default;

        private const int port = default;

        private const string user = default;

        private const string password = default;

        #endregion --> Private propeties. <--

        #region --> Public static methods. <--

        public void Send(SendEmail sendEmail)
        {
            using SmtpClient smtpClient = new(server, port)
            {
                EnableSsl = false,
                Credentials = new NetworkCredential(user, password),
                UseDefaultCredentials = true
            };

            using MailMessage mailMessage = new()
            {
                From = new(user, "Connect"),
                Body = sendEmail.Body,
                Subject = sendEmail.Subject,
                Priority = MailPriority.Normal,
                IsBodyHtml = true
            };

            string[] emailRecipient = Regex.Split(sendEmail.Recipient, ";");

            if (emailRecipient != default)
            {
                for (int i = 0; i < emailRecipient.Length; ++i)
                {
                    if (emailRecipient[i] != default && emailRecipient[i] != "") mailMessage.To.Add(emailRecipient[i]);
                }
            }

            ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate,
                         X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };

            smtpClient.Send(mailMessage);
        }

        #endregion --> Public static methods. <--
    }
}
