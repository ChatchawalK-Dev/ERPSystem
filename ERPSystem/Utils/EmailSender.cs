using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ERPSystem.Utils
{
    public class EmailSender
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;
        private readonly string _fromAddress;

        public EmailSender(string smtpServer, int smtpPort, string smtpUser, string smtpPass, string fromAddress)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUser = smtpUser;
            _smtpPass = smtpPass;
            _fromAddress = fromAddress;
        }

        public async Task SendEmailAsync(string toAddress, string subject, string body, bool isHtml = false)
        {
            if (string.IsNullOrEmpty(toAddress))
            {
                throw new ArgumentException("Recipient address is required.", nameof(toAddress));
            }

            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_fromAddress),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = isHtml
                };

                mailMessage.To.Add(toAddress);

                using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
                    smtpClient.EnableSsl = true;

                    await Task.Run(() => smtpClient.SendMailAsync(mailMessage));
                    Console.WriteLine($"Email sent to {toAddress}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error sending email: {ex.Message}");
                throw;
            }
        }
    }
}
