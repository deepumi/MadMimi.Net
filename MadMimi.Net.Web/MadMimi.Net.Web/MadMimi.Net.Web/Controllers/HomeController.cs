using System;
using System.Threading.Tasks;
using System.Web.Mvc;

using MadMimi.Net;
using System.Configuration;
using System.Net.Mail;

namespace MadMimi.Net.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public async Task<ActionResult> Index()
        {

            try
            {
                Mailer mailer = new Mailer(ConfigurationManager.AppSettings["MadMimiUserName"], ConfigurationManager.AppSettings["MadMimiApiKey"]);
                using (var message = new MailMessage())
                {
                    message.From = new MailAddress("from email", "display name");
                    message.To.Add("sender email");
                    message.Subject = "Test";
                    message.Body = "<h1>This is test</h1>";
                    await mailer.SendAsync("test", message);
                }
                var status = mailer.TrackStatus;
                var mailTransactionId = mailer.TransactionId;
            }
            catch (Exception)
            {
                 //log exception here
            }
            return View();
        }
    }
}