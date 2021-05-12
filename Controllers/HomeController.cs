using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Essays.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveUserData()
        {
            // MailAddress from = new MailAddress("qwesssss2@mail.ru", "Essays");
            // SmtpClient smtp = new SmtpClient("smtp.mail.ru", 2525);
            // smtp.EnableSsl = true;
            // smtp.Credentials = new NetworkCredential("qwesssss2@mail.ru", "99ee3MEkHEj7Zgd");
            // string t = $"<h1>Пишет: {HttpContext.Request.Form["name"]} | {HttpContext.Request.Form["email"]}</h1>" +
            //     $"<h4>{HttpContext.Request.Form["message"]}</h4>";
            // MailAddress to = new MailAddress("dnevnik1923@mail.ru");
            // MailMessage m = new MailMessage(from, to);
            // m.Subject = "Essays | Недовольные посетители";
            // m.Body = t;
            // m.IsBodyHtml = true;
            // smtp.Send(m);
            using (StreamWriter sw = new StreamWriter(new FileStream($"~/Content/offers/{Guid.NewGuid()}.txt", FileMode.OpenOrCreate, FileAccess.Write)))
            {
                sw.WriteLine($"From: {HttpContext.Request.Form["name"]}");
                sw.WriteLine($"Email: {HttpContext.Request.Form["email"]}");
                sw.WriteLine($"Message: {HttpContext.Request.Form["message"]}");
            }
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}