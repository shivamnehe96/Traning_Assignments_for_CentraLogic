using System.Net.Mail;
using System.Net.Security;
using System.Net;

namespace SecurityClearanceSystem.Services
{
    public class EmailService
    {
        public static bool SendEmail(string to, string from, string subject, string text)
        {
            bool flag = false;

            try
            {
               
                var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential("shivamnehe720@gmail.com", "agnoiviomvpbswlm"); 

                var message = new MailMessage();
                message.From = new MailAddress("shivamnehe720@gmail.com");
                message.To.Add(new MailAddress("shivamnehe720@gmail.com"));
                message.Subject = "Pass Created Succesfully";
                message.Body = "your request for visiting is approved";
              

                smtpClient.Send(message);
                flag = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message); 
            }

            return flag;
        }

    }
}
