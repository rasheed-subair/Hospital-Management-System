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
    public class PharmacistController : Controller
    {
        private HospitalContext db = new HospitalContext();

        /***************************************/
        /*          Pharmacist List            */
        /***************************************/
        public ActionResult Index(string searchString)
        {
            if (Session["AdminId"] != null)
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    var pharmacists = from s in db.PharmacistTable
                                      select s;
                    pharmacists = pharmacists.Where(s => s.Name.Contains(searchString));

                    return View(pharmacists.ToList());
                }
                return View(db.PharmacistTable.ToList());
            }
            return RedirectToAction("Login", "Admin");
        }

        /***************************************/
        /*         Pharmacist Homepage         */
        /***************************************/
        public ActionResult Homepage()
        {
            if (Session["PharmacistId"] != null)
            {
                return View();
            }
            return RedirectToAction("Login");
        }

        /***************************************/
        /*      View Pharmacist Details        */
        /***************************************/
        public ActionResult Details(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Pharmacist pharmacist = db.PharmacistTable.Find(id);
                if (pharmacist == null)
                {
                    return HttpNotFound();
                }
                return View(pharmacist);
            }
            return RedirectToAction("Login", "Admin");
        }

        /***************************************/
        /*        Create New Pharmacist        */
        /***************************************/
        public ActionResult Create()
        {
            if (Session["AdminId"] != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Admin");
        }

        // POST: Pharmacist/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PharmacistId,Name,Email,Username,Password,ConfirmPassword,Phone,Address")] Pharmacist pharmacist)
        {
            if (ModelState.IsValid)
            {
                db.PharmacistTable.Add(pharmacist);
                db.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = "New Account Successfully Created";
            }

            return View();
        }

        /***************************************/
        /*      Edit Pharamcist Details        */
        /***************************************/
        public ActionResult Edit(int? id)
        {
            if (Session["PharamcistId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Pharmacist pharmacist = db.PharmacistTable.Find(id);
                if (pharmacist == null)
                {
                    return HttpNotFound();
                }
                return View(pharmacist);
            }
            return RedirectToAction("Login");
        }

        // POST: Pharmacist/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PharmacistId,Name,Email,Username,Password,ConfirmPassword,Phone,Address")] Pharmacist pharmacist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pharmacist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pharmacist);
        }

        /***************************************/
        /*      Delete Pharmacist Details      */
        /***************************************/
        public ActionResult Delete(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Pharmacist pharmacist = db.PharmacistTable.Find(id);
                if (pharmacist == null)
                {
                    return HttpNotFound();
                }
                return View(pharmacist);
            }
            return RedirectToAction("Login", "Admin");
        }

        // POST: Pharmacist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pharmacist pharmacist = db.PharmacistTable.Find(id);
            db.PharmacistTable.Remove(pharmacist);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /***********************************************/
        /*              Pharmacist Login method            */
        /***********************************************/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Pharmacist account)
        {
            using (HospitalContext db = new HospitalContext())
            {
                try
                {
                    var mypharmacist = db.PharmacistTable.Single(a => a.Username == account.Username && a.Password == account.Password);
                    if (mypharmacist != null)
                    {
                        Session["PharmacistId"] = mypharmacist.PharmacistId.ToString();
                        Session["PharmacistName"] = mypharmacist.Name.ToString();
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
        /*          Pharmacist Logout method           */
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
