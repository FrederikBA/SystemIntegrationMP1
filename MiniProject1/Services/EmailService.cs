using System.Net;
using System.Net.Mail;
using System.Text;
using MiniProject1.Models;

namespace MiniProject1.Services
{
    public class EmailService
    {
        private readonly GenderService _genderService;

        public EmailService(GenderService genderService)
        {
            _genderService = genderService;
        }

        public async Task SendEmailAsync()
        {
            var recipients = new List<Recipient>
            {
                new Recipient("Donald", "receiver email", "87.52.111.60"),
                new Recipient("Sarah", "receiver email", "46.32.143.255"),
                new Recipient("John", "receiver email", "87.52.111.60")
            };

            foreach (var recipient in recipients)
            {
                var gender = await _genderService.FindGenderByNameAndCountryAsync(recipient);
                var salutation = GetSalutation(gender);

                var subject = $"Hello {salutation} {recipient.Name}";
                var body = $"Dear {salutation} {recipient.Name},\n\n" +
                           "We are happy to announce that we are launching a new product!\n\n" +
                           "Best regards,\n" +
                           "The MiniProject1 Team";
                
                // Create an attachment in-memory
                var attachmentData = Encoding.UTF8.GetBytes("This is the content of the attached text file.");
                var attachmentStream = new MemoryStream(attachmentData);
                var attachment = new Attachment(attachmentStream, "yearlyreport.txt", "text/plain");
                
                await SendAsync(recipient.Email, subject, body, attachment);

                
                attachmentStream.Dispose();
            }
        }

        private string GetSalutation(String gender)
        {
            return gender == "male" ? "Mr." : "Ms.";
        }

        private async Task SendAsync(string to, string subject, string body, Attachment attachment)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("senderemail", "senderpassword"),
                EnableSsl = true,
            };

            var from = new MailAddress("senderemail");
            var toAddress = new MailAddress(to);
            var mailMessage = new MailMessage(from, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            // Add the attachment
            mailMessage.Attachments.Add(attachment);

            smtpClient.Send(mailMessage);
        }
    }
}