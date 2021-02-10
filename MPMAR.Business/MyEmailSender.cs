using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MPMAR.Business.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace MPMAR.Business
{
    public class MyEmailSender : IMyEmailSender
    {
        private readonly IConfiguration _configuration;
        private EmailCredentialsModel _emailSettings = new EmailCredentialsModel();
        public MyEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            _configuration.Bind("EmailCredentials", _emailSettings);
        }

        public Task<bool> SendEmailAsync(string toEmail, string toUserName, string subject, string message)
        {

            var executeValue = Execute(toEmail, toUserName, subject, message);
            executeValue.Wait();
            return executeValue;
        }

        private async Task<bool> Execute(string toEmail, string toUserName, string subject, string message)
        {
            try
            {
                var mail = CreateMail(toEmail, toUserName, subject, message);

                await SendMailAsync(mail);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private MimeMessage CreateMail(string toEmail, string toUserName, string subject, string message)
        {
            var mail = new MimeMessage();

            mail.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));

            mail.To.Add(new MailboxAddress(toUserName, toEmail));

            mail.Subject = subject;

            var bodyBuilder = new BodyBuilder();


            bodyBuilder.HtmlBody = message;

            mail.Body = bodyBuilder.ToMessageBody();

            return mail;
        }

        private async Task SendMailAsync(MimeMessage mail)
        {
            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                var ssl = _emailSettings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None;
                await smtp.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, ssl);

                await smtp.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.Password);

                await smtp.SendAsync(mail);

                await smtp.DisconnectAsync(true);

            };
        }
    }
}
