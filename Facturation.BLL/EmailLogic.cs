using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Facturation.BLL
{
    public class EmailLogic
    {
        public EmailLogic()
        {
            
        }
        public void SendEmail(string to, string subject, string body)
        {
            MailMessage o = new MailMessage();
            o.From = new MailAddress("systeemfacturen@gmail.com");
            o.To.Add(to);
            o.Subject = subject;
            o.Body = body;

            NetworkCredential netCred = new NetworkCredential("//add email", "//add credentials");
            SmtpClient smtpobj = new SmtpClient("smtp.gmail.com", 587);
            smtpobj.EnableSsl = true;
            smtpobj.Credentials = netCred;
            smtpobj.Send(o);
        }
    }
}
