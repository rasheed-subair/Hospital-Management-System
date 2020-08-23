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
    public class LabTechController : Controller
    {
        private HospitalContext db = new HospitalContext();

        /***************************************/
        /*            LabTech List             */
        /***************************************/
        public ActionResult Index()
        {
            if (Session["AdminId"] != null)
            {
                return View(db.LabTechTable.ToList());
            }
            return RedirectToAction("Login", "Admin");
            
        }

        /***************************************/
        /*           LabTech Homepage          */
        /***************************************/
        public ActionResult Homepage()
        {
            if (Session["LabTechId"] != null)
            {
                return View();
            }
            return RedirectToAction("Login");
        }

        /***************************************/
        /*        View LabTech Details         */
        /***************************************/
        public ActionResult Details(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LabTech labTech = db.LabTechTable.Find(id);
                if (labTech == null)
                {
                    return HttpNotFound();
                }
                return View(labTech);
            }
            return RedirectToAction("Login", "Admin");
            
        }

        /***************************************/
        /*      Create New LabTech - Get       */
        /***************************************/
        public ActionResult Create()
        {
            if (Session["AdminId"] != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Admin");
        }

        // POST: LabTech/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LabTechId,Name,Email,Username,Password,ConfirmPassword,Phone,Address")] LabTech labTech)
        {
            if (ModelState.IsValid)
            {
                db.LabTechTable.Add(labTech);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(labTech);
        }

        /***************************************/
        /*        Edit LabTech Detais          */
        /***************************************/
        public ActionResult Edit(int? id)
        {
            if (Session["LabTechId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LabTech labTech = db.LabTechTable.Find(id);
                if (labTech == null)
                {
                    return HttpNotFound();
                }
                return View(labTech);
            }
            return RedirectToAction("Login");
        }

        // POST: LabTech/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LabTechId,Name,Email,Username,Password,ConfirmPassword,Phone,Address")] LabTech labTech)
        {
            if (ModelState.IsValid)
            {
                db.Entry(labTech).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(labTech);
        }

        /***************************************/
        /*            Delete LabTech           */
        /***************************************/
        public ActionResult Delete(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LabTech labTech = db.LabTechTable.Find(id);
                if (labTech == null)
                {
                    return HttpNotFound();
                }
                return View(labTech);
            }
            return RedirectToAction("Login");
        }

        // POST: LabTech/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LabTech labTech = db.LabTechTable.Find(id);
            db.LabTechTable.Remove(labTech);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        /***********************************************/
        /*              LabTech Login method           */
        /***********************************************/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LabTech account)
        {
            using (HospitalContext db = new HospitalContext())
            {
                try
                {
                    var mylabtech = db.LabTechTable.Single(a => a.Username == account.Username && a.Password == account.Password);
                    if (mylabtech != null)
                    {
                        Session["LabTechId"] = mylabtech.LabTechId.ToString();
                        Session["LabTechName"] = mylabtech.Name.ToString();

                        return RedirectToAction("Homepage");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "");
                }
            }
            return View();
        }

        /***********************************************/
        /*            LabTech Logout method            */
        /***********************************************/
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
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
