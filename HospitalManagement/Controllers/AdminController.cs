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
    public class AdminController : Controller
    {
        private HospitalContext db = new HospitalContext();

        /***************************************/
        /*             Admin List              */
        /***************************************/
        public ActionResult Index()
        {
            if (Session["AdminId"] != null)
            {
                return View(db.AdminTable.ToList());
            }
            return RedirectToAction("Login");
        }

        /***************************************/
        /*             Admin Homeapge          */
        /***************************************/
        public ActionResult Homepage()
        {
            if (Session["AdminId"] != null)
            {
                return View();
            }
            return RedirectToAction("Login");
        }


        /***************************************/
        /*          View Admin Details         */
        /***************************************/
        public ActionResult Details(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Admin admin = db.AdminTable.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                return View(admin);
            }
            return RedirectToAction("Login");
        }

        /***************************************/
        /*        Create New Admin - Get       */
        /***************************************/
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdminId,Name,Email,Username,Password,ConfirmPassword,Phone,Address")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.AdminTable.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        /***************************************/
        /*        Edit Admin Detais            */
        /***************************************/
        public ActionResult Edit(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Admin admin = db.AdminTable.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                return View(admin);
            }
            return RedirectToAction("Login");
            
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdminId,Name,Email,Username,Password,ConfirmPassword,Phone,Address")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        /***************************************/
        /*            Delete Admin             */
        /***************************************/
        public ActionResult Delete(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Admin admin = db.AdminTable.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                return View(admin);
            }
            return RedirectToAction("Login");

        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.AdminTable.Find(id);
            db.AdminTable.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /***********************************************/
        /*             Admin Login method              */
        /***********************************************/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin account)
        {
            using (HospitalContext db = new HospitalContext())
            {
                try
                {
                    var myadmin = db.AdminTable.Single(a => a.Username == account.Username && a.Password == account.Password);
                    if (myadmin != null)
                    {
                        Session["AdminId"] = myadmin.AdminId.ToString();
                        Session["AdminName"] = myadmin.Name.ToString();

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
        /*            Admin Logout method              */
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
