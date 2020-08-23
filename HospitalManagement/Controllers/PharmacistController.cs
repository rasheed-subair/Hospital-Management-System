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
        public ActionResult Index()
        {
            return View(db.PharmacistTable.ToList());
        }

        /***************************************/
        /*         Pharmacist Homepage         */
        /***************************************/
        public ActionResult Homepage()
        {
            return View();
        }


        /***************************************/
        /*      View Pharmacist Details        */
        /***************************************/
        public ActionResult Details(int? id)
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

        /***************************************/
        /*        Create New Pharmacist        */
        /***************************************/
        public ActionResult Create()
        {
            return View();
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
                return RedirectToAction("Index");
            }

            return View(pharmacist);
        }

        /***************************************/
        /*      Edit Pharamcist Details        */
        /***************************************/
        public ActionResult Edit(int? id)
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
