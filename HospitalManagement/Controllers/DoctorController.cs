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
    public class DoctorController : Controller
    {
        private HospitalContext db = new HospitalContext();

        /***************************************/
        /*              Doctor List            */
        /***************************************/
        public ActionResult Index(string searchString)
        {
            if (Session["AdminId"] != null)
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    var doctors = from s in db.DoctorTable
                                  select s;
                    doctors = doctors.Where(s => s.Name.Contains(searchString));

                    return View(doctors.ToList());
                }
                return View(db.DoctorTable.ToList());
            }
            return RedirectToAction("Login", "Admin");
        }

        /***************************************/
        /*          Doctor Homepage            */
        /***************************************/
        public ActionResult Homepage()
        {
            if (Session["DoctorId"] != null)
            {
                return View();
            }
            return RedirectToAction("Login");
        }

        /***************************************/
        /*         View Doctor Details         */
        /***************************************/
        public ActionResult Details(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Doctor doctor = db.DoctorTable.Find(id);
                if (doctor == null)
                {
                    return HttpNotFound();
                }
                return View(doctor);
            }
            return RedirectToAction("Login", "Admin");
        }

        /***************************************/
        /*      Create New Doctor - Get        */
        /***************************************/
        public ActionResult Create()
        {
            if (Session["AdminId"] != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Admin");
        }

        // POST: Doctor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DoctorId,Name,Email,Username,Password,ConfirmPassword,Phone,Address,Department")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.DoctorTable.Add(doctor);
                db.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = "New Account Successfully Created";
            }
            return View();
        }

        /***************************************/
        /*        Edit Doctor Detais          */
        /***************************************/
        public ActionResult Edit(int? id)
        {
            if (Session["DoctorId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Doctor doctor = db.DoctorTable.Find(id);
                if (doctor == null)
                {
                    return HttpNotFound();
                }
                return View(doctor);
            }
            return RedirectToAction("Login");
        }

        // POST: Doctor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DoctorId,Name,Email,Username,Password,ConfirmPassword,Phone,Address,Department")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        /***************************************/
        /*            Delete Doctor           */
        /***************************************/
        public ActionResult Delete(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Doctor doctor = db.DoctorTable.Find(id);
                if (doctor == null)
                {
                    return HttpNotFound();
                }
                return View(doctor);
            }
            return RedirectToAction("Login", "Admin");
        }

        // POST: Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Doctor doctor = db.DoctorTable.Find(id);
            db.DoctorTable.Remove(doctor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /***********************************************/
        /*              Doctor Login method            */
        /***********************************************/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Doctor account)
        {
            using (HospitalContext db = new HospitalContext())
            {
                try
                {
                    var mydoctor = db.DoctorTable.Single(a => a.Username == account.Username && a.Password == account.Password);
                    if (mydoctor != null)
                    {
                        Session["DoctorId"] = mydoctor.DoctorId.ToString();
                        Session["DoctorName"] = mydoctor.Name.ToString();
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
        /*            Doctor Logout method             */
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