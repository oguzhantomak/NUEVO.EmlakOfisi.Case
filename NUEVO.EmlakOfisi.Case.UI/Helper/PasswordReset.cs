using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NUEVO.EmlakOfisi.Case.UI.Helper
{
    public static class PasswordReset
    {
        public static void PasswordResetSendEmail(string link, string mail)
        {
            try
            {
                MailMessage mail = new MailMessage();

                SmtpClient smtpClient = new SmtpClient("mail.oguz.kim");
                //SmtpClient smtpClient = new SmtpClient();
                mail.From = new MailAddress(mail);
                mail.To.Add("oguzhantomak@gmail.com");

                mail.Subject = $"Şifre Sıfırlama Talebiniz";
                mail.Body = "<h2>Şifre sıfırlama talebinize istinaden aşağıdaki linke tıklayınız.</h2><hr/>";
                mail.Body += $"<a href='{link}'>şifre yenileme linki</a>";
                mail.IsBodyHtml = true;
                smtpClient.Port = 587;

                smtpClient.Credentials = new NetworkCredential("oguz@oguz.kim", "alexander1");

                smtpClient.Send(mail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
