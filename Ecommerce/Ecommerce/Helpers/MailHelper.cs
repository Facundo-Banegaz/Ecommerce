using Ecommerce.Common;
using MailKit.Net.Smtp;
using MimeKit;


namespace Ecommerce.Helpers
{
    public class MailHelper : IMailHelper
    {
        public IConfiguration _configuration { get; }
        public MailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public Response SendMail(string toName, string toEmail, string subject, string body)
        {
            try
            {
                string from = _configuration["Email:From"];
                string name = _configuration["Email:Name"];
                string smtp = _configuration["Email:Smtp"];
                string port = _configuration["Email:Port"];
                string password = _configuration["Email:Password"];

                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress(name, from));
                message.To.Add(new MailboxAddress(toName, toEmail));
                message.Subject = subject;
                BodyBuilder bodyBuilder = new BodyBuilder
                {
                    HtmlBody = body
                };
                message.Body = bodyBuilder.ToMessageBody();

                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect(smtp, int.Parse(port), false);
                    client.Authenticate(from, password);
                    client.Send(message);
                    client.Disconnect(true);
                }

                return new Response { IsSuccess = true };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = ex
                };
            }

        }
    }
}
