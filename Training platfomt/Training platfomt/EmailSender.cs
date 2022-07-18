using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Mail;
using System.Linq;
using System.Collections.Generic;

namespace Training_platfomt
{
    class EmailSender
    {
        private static readonly string emailLogin = "sliderertrainingplatfom@mail.ru";
        private static readonly string emailPassword = "Ss7D4f5m1Lpstoqeuzdq";
        private static readonly string nickname = "Slider platrom - platform with free courses";

        public static bool SendWelcomEmail(string userEmail)
        {
            return SendEmail(userEmail);
        }
        
        public static void SendNotificationAboutNewCourse(DatabaseController database)
        {
            List<string> emails = database.GetAllEmails().ToList();
            foreach(string email in emails)
            {
                SendEmailAsync(email);
            }
        }

        private static bool SendEmail(string userEmail)
        {
            MailAddress from = new MailAddress(emailLogin, nickname);
            MailAddress to;

            try
            {
                to = new MailAddress(userEmail);

            }
            catch
            {
                return false;
            }

            MailMessage message = new MailMessage(from, to);
            message.Subject = "Добро пожаловать!";
            message.Body = "Добро пожаловать на нашу платформу!";
            message.IsBodyHtml = true;
            using (SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587))
            {
                smtp.Credentials = new NetworkCredential(emailLogin, emailPassword);
                smtp.EnableSsl = true;
                try
                {
                    smtp.Send(message);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        private async static void SendEmailAsync(string userEmail)
        {
            MailAddress from = new MailAddress(emailLogin, nickname);
            MailAddress to = new MailAddress(userEmail);

            MailMessage message = new MailMessage(from, to);
            message.Subject = "Новый бесплатный курс!";
            message.Body = "На нашей платформе появился новый курс!";
            message.IsBodyHtml = true;
            using (SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587))
            {
                smtp.Credentials = new NetworkCredential(emailLogin, emailPassword);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
        }
    }
}
