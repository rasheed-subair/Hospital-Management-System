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
    public class MedicationController : Controller
    {
        private HospitalContext db = new HospitalContext();

        /***************************************/
        /*          Medication  List           */
        /***************************************/
        public ActionResult Index(string searchString)
        {
            if (Session["PharmacistId"] != null)
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    var meds = from s in db.MedicationTable
                               select s;
                    meds = meds.Where(s => s.DrugName.Contains(searchString)
                    || s.BrandName.Contains(searchString));

                    return View(meds.ToList());
                }
                return View(db.MedicationTable.ToList());
            }
            return RedirectToAction("Login", "Pharmacist");
        }

        /***************************************/
        /*      View Medication Record         */
        /***************************************/
        public ActionResult Details(int? id)
        {
            if (Session["PharmacistId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Medication medication = db.MedicationTable.Find(id);
                if (medication == null)
                {
                    return HttpNotFound();
                }
                return View(medication);
            }
            return RedirectToAction("Login", "Pharmacist");
        }

        /***************************************/
        /*        Create New Medication        */
        /***************************************/
        public ActionResult Create()
        {
            if (Session["PharmacistId"] != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Pharmacist");
        }

        // POST: Medication/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MedicationId,DrugName,BrandName,DrugDescription,DrugPrice,Quantity,Category,ExpDate")] Medication medication)
        {
            if (ModelState.IsValid)
            {
                db.MedicationTable.Add(medication);
                db.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = "Medication Successfully Created";
            }

            return View(medication);
        }

        /***************************************/
        /*          Edit Medication            */
        /***************************************/
        public ActionResult Edit(int? id)
        {
            if (Session["PharmacistId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Medication medication = db.MedicationTable.Find(id);
                if (medication == null)
                {
                    return HttpNotFound();
                }
                return View(medication);
            }
            return RedirectToAction("Login", "Pharmacist");
            
        }

        // POST: Medication/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MedicationId,DrugName,BrandName,DrugDescription,DrugPrice,Quantity,Category,ExpDate")] Medication medication)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medication).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medication);
        }

        /***************************************/
        /*          Delete Medication          */
        /***************************************/
        public ActionResult Delete(int? id)
        {
            if (Session["PharmacistId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Medication medication = db.MedicationTable.Find(id);
                if (medication == null)
                {
                    return HttpNotFound();
                }
                return View(medication);
            }
            return RedirectToAction("Login", "Pharmacist");
        }

        // POST: Medication/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medication medication = db.MedicationTable.Find(id);
            db.MedicationTable.Remove(medication);
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
