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
    public class AccountantController : Controller
    {
        private HospitalContext db = new HospitalContext();

        /***************************************/
        /*          Accountant List            */
        /***************************************/
        public ActionResult Index(string searchString)
        {
            if (Session["AdminId"] != null)
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    var accountants = from s in db.AccountantTable
                                   select s;
                    accountants = accountants.Where(s => s.Name.Contains(searchString));

                    return View(accountants.ToList());
                }
                return View(db.AccountantTable.ToList());
            }
            return RedirectToAction("Login", "Admin");
        }

        /***************************************/
        /*         Accountant Homepage         */
        /***************************************/
        public ActionResult Homepage()
        {
            if (Session["AccountantId"] != null)
            {
                return View();
            }
            return RedirectToAction("Login");
            
        }

        /***************************************/
        /*      View Accountant Details        */
        /***************************************/
        public ActionResult Details(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Accountant accountant = db.AccountantTable.Find(id);
                if (accountant == null)
                {
                    return HttpNotFound();
                }
                return View(accountant);
            }
            return RedirectToAction("Login","Admin");
        }

        /***************************************/
        /*        Create New Accountant        */
        /***************************************/
        public ActionResult Create()
        {
            if (Session["AdminId"] != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Admin");
            
        }

        // POST: Accountant/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountantId,Name,Email,Username,Password,ConfirmPassword,Phone,Address")] Accountant accountant)
        {
            if (ModelState.IsValid)
            {
                db.AccountantTable.Add(accountant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accountant);
        }

        /***************************************/
        /*      Edit Accountant Details        */
        /***************************************/
        public ActionResult Edit(int? id)
        {
            if (Session["AccountantId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Accountant accountant = db.AccountantTable.Find(id);
                if (accountant == null)
                {
                    return HttpNotFound();
                }
                return View(accountant);
            }
            return RedirectToAction("Login");
        }

        // POST: Accountant/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountantId,Name,Email,Username,Password,ConfirmPassword,Phone,Address")] Accountant accountant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountant);
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
                Accountant accountant = db.AccountantTable.Find(id);
                if (accountant == null)
                {
                    return HttpNotFound();
                }
                return View(accountant);
            }
            return RedirectToAction("Login", "Admin");
        }

        // POST: Accountant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accountant accountant = db.AccountantTable.Find(id);
            db.AccountantTable.Remove(accountant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        /***********************************************/
        /*          Accountant Login method            */
        /***********************************************/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Accountant account)
        {
            using (HospitalContext db = new HospitalContext())
            {
                try
                {
                    var myaccountant = db.AccountantTable.Single(a => a.Username == account.Username && a.Password == account.Password);
                    if (myaccountant != null)
                    {
                        Session["AccountantId"] = myaccountant.AccountantId.ToString();
                        Session["AccountantName"] = myaccountant.Name.ToString();

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
        /*         Accountant Logout method            */
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
