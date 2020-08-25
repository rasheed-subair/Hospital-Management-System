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
    public class NurseController : Controller
    {
        private HospitalContext db = new HospitalContext();

        /***************************************/
        /*               Nurse List            */
        /***************************************/
        public ActionResult Index(string searchString)
        {
            if (Session["AdminId"] != null)
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    var nurses = from s in db.NurseTable
                                      select s;
                    nurses = nurses.Where(s => s.Name.Contains(searchString));

                    return View(nurses.ToList());
                }
                return View(db.NurseTable.ToList());
            }
            return RedirectToAction("Login", "Admin");
        }

        /***************************************/
        /*           Nurse Homepage            */
        /***************************************/
        public ActionResult Homepage()
        {
            if (Session["NurseId"] != null)
            {
                return View();
            }
            return RedirectToAction("Login");
        }

        /***************************************/
        /*          View Nurse Details         */
        /***************************************/
        public ActionResult Details(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Nurse nurse = db.NurseTable.Find(id);
                if (nurse == null)
                {
                    return HttpNotFound();
                }
                return View(nurse);
            }
            return RedirectToAction("Login", "Admin");
        }

        /***************************************/
        /*        Create New Nurse - Get       */
        /***************************************/
        public ActionResult Create()
        {
            if (Session["AdminId"] != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Admin");
        }

        // POST: Nurse/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NurseId,Name,Email,Username,Password,ConfirmPassword,Phone,Address")] Nurse nurse)
        {
            if (ModelState.IsValid)
            {
                db.NurseTable.Add(nurse);
                db.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = "New Account Successfully Created";
            }
            return View();
        }

        /***************************************/
        /*         Edit Nurse Detais           */
        /***************************************/
        public ActionResult Edit(int? id)
        {
            if (Session["NurseId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Nurse nurse = db.NurseTable.Find(id);
                if (nurse == null)
                {
                    return HttpNotFound();
                }
                return View(nurse);
            }
            return RedirectToAction("Login");
        }

        // POST: Nurse/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NurseId,Name,Email,Username,Password,ConfirmPassword,Phone,Address")] Nurse nurse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nurse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nurse);
        }

        /***************************************/
        /*            Delete Nurse             */
        /***************************************/
        public ActionResult Delete(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Nurse nurse = db.NurseTable.Find(id);
                if (nurse == null)
                {
                    return HttpNotFound();
                }
                return View(nurse);
            }
            return RedirectToAction("Login", "Admin");
        }

        // POST: Nurse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nurse nurse = db.NurseTable.Find(id);
            db.NurseTable.Remove(nurse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /***********************************************/
        /*              Nurse Login method             */
        /***********************************************/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Nurse account)
        {
            using (HospitalContext db = new HospitalContext())
            {
                try
                {
                    var mynurse = db.NurseTable.Single(a => a.Username == account.Username && a.Password == account.Password);
                    if (mynurse != null)
                    {
                        Session["NurseId"] = mynurse.NurseId.ToString();
                        Session["NurseName"] = mynurse.Name.ToString();

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
        /*             Nurse Logout method             */
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