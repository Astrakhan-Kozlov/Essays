using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplicationAuthorization.Models;

namespace WebApplicationAuthorization.Controllers
{
    public class UsersController : Controller
    {
        private UserContext db = new UserContext();

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string login, string password)
        {
            User usr = db.Users.Where(u => u.Login == login && u.Password == password).FirstOrDefault();
            if (usr != null)
            {
                Session["Login"] = usr.Login;
                Session["Role"] = usr.Role;
            }
            return Redirect("/Home/Index");
        }

        public ActionResult LogOut()
        {
            Session["Login"] = null;
            Session["Password"] = null;
            Session["Role"] = null;
            return Redirect("/Home/Index");
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View(new User { Login = "", Password = "", Role = "" });
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Login,Password,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return Redirect("/Home/Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Role"] == null)
            {
                return Content("Только для авторизованных");
            }
            else if (!Session["Role"].Equals("Admin"))
            {
                return Content("Только для администраторов");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Login,Password,Role")] User user)
        {
            if (Session["Role"] == null)
            {
                return Content("Только для авторизованных");
            }
            else if (!Session["Role"].Equals("Admin"))
            {
                return Content("Только для администраторов");
            }
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Home/Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Role"] == null)
            {
                return Content("Только для авторизованных");
            }
            else if (!Session["Role"].Equals("Admin"))
            {
                return Content("Только для администраторов");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Role"] == null)
            {
                return Content("Только для авторизованных");
            }
            else if (!Session["Role"].Equals("Admin"))
            {
                return Content("Только для администраторов");
            }
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return Redirect("/Home/Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
