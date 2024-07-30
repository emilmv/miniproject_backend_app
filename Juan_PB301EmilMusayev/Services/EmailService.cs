using Juan_PB301EmilMusayev.Interfaces;
using Juan_PB301EmilMusayev.ViewModels;
using System.Net.Mail;
using System.Net;

namespace Juan_PB301EmilMusayev.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string email,string subject,string body)
        {
            MailMessage mailMessage = new();
            mailMessage.From = new("emilnm@code.edu.az", "Juan");
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;
            SmtpClient smtpClient = new();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("testmail@test.com", "your password key");
            smtpClient.Send(mailMessage);
        }
    }
}
