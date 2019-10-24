using System.Net;
using System.Net.Mail;
using System.Text;

namespace SmartTools.Common.Helper
{
    public static class EmailHelper
    {
        public static void Send(string addressFrom, string sendUser, string sendPwd, string addressTo, string subject = "", string body = "")
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(addressFrom, sendUser);
            mail.To.Add(addressTo);
            mail.SubjectEncoding = Encoding.UTF8;
            mail.Subject = subject;
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = false;
            mail.Body = body;
            mail.Priority = MailPriority.High;

            SmtpClient smtpService = new SmtpClient();
            smtpService.Host = "smtp.163.com"; //
            smtpService.Port = 25;             //
            smtpService.UseDefaultCredentials = false;
            smtpService.Credentials = new NetworkCredential(addressFrom, sendPwd);
            smtpService.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpService.EnableSsl = true;
            // smtpService.Timeout = 10000;
            smtpService.Send(mail);
        }
    }
}
