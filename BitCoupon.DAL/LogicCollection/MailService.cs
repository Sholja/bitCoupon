using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BitCoupon.DAL.LogicCollection
{
   public static class MailService
    {

        public static bool SendMail(string subject, string body, MailAddress toAddres)
        {
            var fromAddress = new MailAddress("bitcouponnoreply@gmail.com", "BitCoupon Team");
            const string fromPassword = "bitcoupon123";


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddres)
            {
                Subject = subject,
                Body = body
            })
                try
                {
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

        }
    }
}
