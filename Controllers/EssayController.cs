using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Hosting;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Essays.Controllers
{
    public class EssayController : Controller
    {
        Random random = new Random();
        [HttpGet]
        public ActionResult WriteEssay()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WriteEssay(string title)
        {
            // Variable "al" for storing the text of the essay
            ArrayList al = new ArrayList(4);
            Dictionary<string, string> phrases = new Dictionary<string, string>();
            JToken templates;
            using (StreamReader sr = new StreamReader(new FileStream(HostingEnvironment.MapPath($"~/App_Data/DataEssays.json"), FileMode.Open)))
            {
                JObject JO = JObject.Parse(sr.ReadToEnd());
                templates = JO[title];
            }
            using (StreamReader sr = new StreamReader(new FileStream(HostingEnvironment.MapPath($"~/App_Data/Phrases.json"), FileMode.Open)))
            {
                JObject JO = JObject.Parse(sr.ReadToEnd());
                phrases["introduction"] = JO["вступление"][random.Next(JO["вступление"].Count())].ToString();
                phrases["life"] = JO["переходжизнь"][random.Next(JO["переходжизнь"].Count())].ToString();
                phrases["conclusion"] = JO["заключение"][random.Next(JO["заключение"].Count())].ToString();
            }
            string thesis = templates["тезис"][random.Next(templates["тезис"].Count())].ToString();
            string realLifeExample = templates["пример из жизни"][random.Next(templates["пример из жизни"].Count())].ToString();

            al.Add("\t" + phrases["introduction"] + " " + title + " это " + thesis + ".");
            al.Add("2 абзац.");
            al.Add(phrases["life"] + " " + realLifeExample + ".");
            al.Add(phrases["conclusion"] + " " + title + " это " + thesis + ".");
            return View(al);
        }


    }
}