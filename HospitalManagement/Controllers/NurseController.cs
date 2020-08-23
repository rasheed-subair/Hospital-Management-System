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
        public ActionResult Index()
        {
            return View(db.NurseTable.ToList());
        }

        /***************************************/
        /*           Nurse Homepage            */
        /***************************************/
        public ActionResult Homepage()
        {
            return View();
        }

        /***************************************/
        /*          View Nurse Details         */
        /***************************************/
        public ActionResult Details(int? id)
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

        /***************************************/
        /*        Create New Nurse - Get       */
        /***************************************/
        public ActionResult Create()
        {
            return View();
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
                return RedirectToAction("Index");
            }

            return View(nurse);
        }

        /***************************************/
        /*         Edit Nurse Detais           */
        /***************************************/
        public ActionResult Edit(int? id)
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
