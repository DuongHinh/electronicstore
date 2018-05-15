using ElectronicStore.Data.Entities;
using ElectronicStore.Fulcrum;
using System;
using System.Net;
using System.Net.Mail;

namespace ElectronicStore.Service
{
    public interface IMailService
    {
        void SendMail(string toEmailAddress, string subject, string content);
    }

    public class MailService : IMailService
    {
        private ILogErrorService logErrorService;
        private AppSettings appSettings;

        public MailService(ILogErrorService logErrorService, AppSettings appSettings)
        {
            this.logErrorService = logErrorService;
            this.appSettings = appSettings;
        }

        public void SendMail(string toEmailAddress, string subject, string content)
        {
            var fromEmailAddress = this.appSettings.FromEmailAddress;
            var fromDisplayName = this.appSettings.FromDisplayName;
            var fromEmailPassword = this.appSettings.FromEmailPassword;
            var smtpHost = this.appSettings.SMTPHost;
            var smtpPort = int.Parse(this.appSettings.SMTPPort);
            bool enabledSsl = bool.Parse(this.appSettings.EnabledSSL);

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
                LogError error = new LogError();
                error.Date = DateTime.Now;
                error.Message = ex.Message;
                error.StackTrace = ex.StackTrace;
                this.logErrorService.Create(error);
                this.logErrorService.Save();
            }
        }
    }
}