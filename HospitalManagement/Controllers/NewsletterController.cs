using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospitalManagement.Models;

namespace HospitalManagement.Controllers
{
    public class NewsletterController : Controller
    {
        private HospitalContext db = new HospitalContext();

        /***************************************/
        /*          Newsletter List            */
        /***************************************/
        public ActionResult Index()
        {
            if (Session["AdminId"] != null) 
            {
                return View(db.NewsletterTable.ToList());
            }
            return RedirectToAction("Login", "Admin");
        }


        /***************************************/
        /*         Create Newsletter           */
        /***************************************/
        public ActionResult Create()
        {
            return View();
        }

        // POST: Newsletter/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsletterId,Email")] Newsletter newsletter)
        {
            if (ModelState.IsValid)
            {
                db.NewsletterTable.Add(newsletter);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(newsletter);
        }

        /***************************************/
        /*         Delete Newsletter           */
        /***************************************/
        public ActionResult Delete(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Newsletter newsletter = db.NewsletterTable.Find(id);
                if (newsletter == null)
                {
                    return HttpNotFound();
                }
                return View(newsletter);
            }
            return RedirectToAction("Login", "Admin");
            
        }

        // POST: Newsletter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Newsletter newsletter = db.NewsletterTable.Find(id);
            db.NewsletterTable.Remove(newsletter);
            db.SaveChanges();
            return RedirectToAction("Index");
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
