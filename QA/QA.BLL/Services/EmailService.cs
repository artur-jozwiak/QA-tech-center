using FluentEmail.Core;
using FluentEmail.Smtp;
using FluentEmail.Razor;
using QA.BLL.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Configuration;
using QA.Domain.Models;
using QA.Domain.Models.Enums;

namespace QA.BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> SendOrderStatusNotificationEmail(Order order, string lastStatus, string link)
        {
            List<string> recipientEmails = new();
            if (order.Status == OrderStatus.Approved || order.Status == OrderStatus.NotApproved)
            {
                recipientEmails = _configuration.GetSection("EmailSettings:ControlersEmails").Get<List<string>>();
            }
            else
            {
                recipientEmails = _configuration.GetSection("EmailSettings:ManagersEmails").Get<List<string>>();
            }

            var senderEmail = _configuration["EmailSettings:SenderEmail"];
            var smtpHost = _configuration["EmailSettings:SmtpHost"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
            var senderPassword = _configuration["EmailSettings:SenderPassword"];

            using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
            {
                smtpClient.EnableSsl = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

                var sender = new SmtpSender(() => smtpClient);

                StringBuilder template = new();
                template.AppendLine("<html>");
                template.AppendLine("<body>");
                template.AppendLine("<p>Nastąpiła zmiana statusu zlecenia:</p>");
                template.AppendLine("<table border='1' cellpadding='5' cellspacing='0'>");
                template.AppendLine("<tr><th>Zlecenie</th><th>Produkt</th><th>Poprzedni Status</th><th>Obecny Status</th><th>Link</th></tr>");
                template.AppendLine("<tr>");
                template.AppendLine($"<td>{order.ShortenedKey}</td>");
                template.AppendLine($"<td>{order.Product.Symbol}</td>");
                template.AppendLine($"<td>{lastStatus}</td>");
                template.AppendLine($"<td>{order.Status.GetDisplayName()}</td>");
                template.AppendLine($"<td>{link}</td>");
                template.AppendLine("</tr>");
                template.AppendLine("</table>");
                template.AppendLine("</body>");
                template.AppendLine("</html>");

                Email.DefaultSender = sender;
                Email.DefaultRenderer = new RazorRenderer();

                try
                {
                    var email = Email
                        .From(senderEmail)
                        .Subject($"Zmiana statusu zlecenia: {order.ShortenedKey}")
                        .UsingTemplate(template.ToString(), new
                        {
                            OrderKey = order.ShortenedKey,
                            LastStatus = lastStatus,
                            CurrentStatus = order.Status.GetDisplayName()
                        })
                        .Body(template.ToString(), true);

                    foreach (var recipientEmail in recipientEmails)
                    {
                        email.To(recipientEmail);
                    }

                    await email.SendAsync();

                    return "Wiadomość została wysłana.";
                }
                catch (Exception ex)
                {
                    return $"Wystąpił błąd podczas wysyłania wiadomości: {ex.Message}";
                }
            }
        }

        public async Task<bool> SendMRBNotificationEmail(string recipientEmail, MRB mrb, string link)
        {
            var senderEmail = _configuration["EmailSettings:SenderEmail"];
            var smtpHost = _configuration["EmailSettings:SmtpHost"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
            var senderPassword = _configuration["EmailSettings:SenderPassword"];

            using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
            {
                smtpClient.EnableSsl = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

                var sender = new SmtpSender(() => smtpClient);

                StringBuilder template = new();

                template.AppendLine("<html>");
                template.AppendLine("<body>");
                template.AppendLine("<p>Please review and complete the MRB protocol by following the provided link.</p>");
                template.AppendLine("<p>Once completed, please click the 'Send' button in the MRB Members section to notify the next member.</p>");
                template.AppendLine("<table border='1' cellpadding='5' cellspacing='0'>");
                template.AppendLine("<tr><th>Order</th><th>Product</th><th>Description of Non-Conformance</th><th>Link</th></tr>");
                template.AppendLine("<tr>");
                template.AppendLine($"<td>{mrb.Order.OrderKey}</td>");
                template.AppendLine($"<td>{mrb.Order.Product.Symbol}</td>");
                template.AppendLine($"<td>{mrb.NonConformanceDescription}</td>");
                template.AppendLine($"<td>{link}</td>");
                template.AppendLine("</tr>");
                template.AppendLine("</table>");
                template.AppendLine("<p><i>If you are outside the company network, please ensure you are connected to the VPN first.</i></p>");
                template.AppendLine("<p><i>Do not reply to this message.</i></p>");
                template.AppendLine("</body>");
                template.AppendLine("</html>");

               

                Email.DefaultSender = sender;
                Email.DefaultRenderer = new RazorRenderer();

                try
                {
                    var email = Email
                        .From(senderEmail)
                        .Subject($"MRB Notification: {mrb.Order.OrderKey}")
                        .UsingTemplate(template.ToString(), new
                        {
                            OrderKey = mrb.Order.ShortenedKey,
                        })
                        .Body(template.ToString(), true);

                    email.To(recipientEmail);

                    await email.SendAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public async Task<bool> SendMrbCompleteNotivication(List<string> recipientsEmails, MRB mrb, string link)
        {
            var senderEmail = _configuration["EmailSettings:SenderEmail"];
            var smtpHost = _configuration["EmailSettings:SmtpHost"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
            var senderPassword = _configuration["EmailSettings:SenderPassword"];

            using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
            {
                smtpClient.EnableSsl = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

                var sender = new SmtpSender(() => smtpClient);

                StringBuilder template = new();

                template.AppendLine("<html>");
                template.AppendLine("<body>");
                template.AppendLine($"<p>MRB {mrb.Symbol} status has been changed to <strong>completed</strong>.</p>");
                template.AppendLine($"<p>Please follow the link for more details.</p>");
                template.AppendLine("<table border='1' cellpadding='5' cellspacing='0'>");
                template.AppendLine("<tr><th>Order</th><th>Product</th><th>Description of Non-Conformance</th><th>Link</th></tr>");
                template.AppendLine("<tr>");
                template.AppendLine($"<td>{mrb.Order.OrderKey}</td>");
                template.AppendLine($"<td>{mrb.Order.Product.Symbol}</td>");
                template.AppendLine($"<td>{mrb.NonConformanceDescription}</td>");
                template.AppendLine($"<td>{link}</td>");
                template.AppendLine("</tr>");
                template.AppendLine("</table>");
                template.AppendLine("<p><i>If you are outside the company network, please ensure you are connected to the VPN first.</i></p>");
                template.AppendLine("<p><i>Do not reply to this message.</i></p>");
                template.AppendLine("</body>");
                template.AppendLine("</html>");



                Email.DefaultSender = sender;
                Email.DefaultRenderer = new RazorRenderer();

                try
                {
                    var email = Email
                        .From(senderEmail)
                        .Subject($"MRB {mrb.Symbol} Completed")
                        .UsingTemplate(template.ToString(), new
                        {
                            OrderKey = mrb.Order.ShortenedKey,
                        })
                        .Body(template.ToString(), true);

                    //email.To(recipientEmail);
                    foreach (var recipient in recipientsEmails)
                    {
                        email.To(recipient);
                    }

                    await email.SendAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public List<string> GetDefaultMrbMembers()
        {
            var defaultMembers = _configuration.GetSection("EmailSettings:DefaultMrbMembers").Get<List<string>>();
            return defaultMembers;
        }

        public async Task<string> SendCoatingProcessnotification(string link, int processNo)
        {
            List<string> recipientEmails = new();
            recipientEmails = _configuration.GetSection("EmailSettings:ManagersEmails").Get<List<string>>();

            var senderEmail = _configuration["EmailSettings:SenderEmail"];
            var smtpHost = _configuration["EmailSettings:SmtpHost"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
            var senderPassword = _configuration["EmailSettings:SenderPassword"];

            using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
            {
                smtpClient.EnableSsl = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

                var sender = new SmtpSender(() => smtpClient);

                StringBuilder template = new();

                template.AppendLine("<html>");
                template.AppendLine("<body>");
                template.AppendLine($"<p>Raport procesu powlekania: {link}</p>");
                template.AppendLine("</body>");
                template.AppendLine("</html>");

                Email.DefaultSender = sender;
                Email.DefaultRenderer = new RazorRenderer();

                try
                {
                    var email = Email
                        .From(senderEmail)
                        .Subject($"Powiadomienie procesu powlekania {processNo}")
                        .Body(template.ToString(), true);

                    foreach (var recipientEmail in recipientEmails)
                    {
                        email.To(recipientEmail);
                    }

                    await email.SendAsync();

                    return "Wiadomość została wysłana.";
                }
                catch (Exception ex)
                {
                    return $"Wystąpił błąd podczas wysyłania wiadomości: {ex.Message}";
                }
            }
        }
    }
}
