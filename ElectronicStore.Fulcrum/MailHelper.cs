using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace ElectronicStore.Fulcrum
{
    public class MailHelper
    {
        public static void SendMail(string toEmailAddress, string subject, string content)
        {
            var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var fromDisplayName = ConfigurationManager.AppSettings["FromDisplayName"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = int.Parse(ConfigurationManager.AppSettings["SMTPPort"].ToString());
            bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

            var message = new MailMessage
            {
                Body = content,
                Subject = subject,
                From = new MailAddress(fromEmailAddress, fromDisplayName)
            };

            message.To.Add(new MailAddress(toEmailAddress));
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;

            try
            {
                using (var smtpClient = new System.Net.Mail.SmtpClient())
                {
                    smtpClient.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
                    smtpClient.Host = smtpHost;
                    smtpClient.EnableSsl = enabledSsl;
                    smtpClient.Port = smtpPort;
                    smtpClient.Send(message);
                }
            }
            catch (SmtpException ex)
            {
                throw ex;
            }            
        }
    }
}