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
    public class PatientController : Controller
    {
        private HospitalContext db = new HospitalContext();

        /***************************************/
        /*             Patient List            */
        /***************************************/
        public ActionResult Index()
        {
            return View(db.PatientTable.ToList());
        }


        /***************************************/
        /*        View Patient Details         */
        /***************************************/
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.PatientTable.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        /***************************************/
        /*      Create New Patient - Get       */
        /***************************************/
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatientId,FirstName,LastName,Phone,Email,Address,PatientGender,DOB,Occupation,Marital_Status,Photograph,ECName,ECRelationship,ECPhone,Allergies,Medication,Arthritis,Asthma,Cancer,Depression,Diabetes,Epilepsy,Heart_Disease,HBP,High_Cholesterol,Renal_Disease,Stroke,Thyroid,Alcohol,Smoke,Caffeine,Recreational_Drugs")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.PatientTable.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        /***************************************/
        /*        Edit Patient Detais          */
        /***************************************/
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.PatientTable.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patient/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PatientId,FirstName,LastName,Phone,Email,Address,PatientGender,DOB,Occupation,Marital_Status,Photograph,ECName,ECRelationship,ECPhone,Allergies,Medication,Arthritis,Asthma,Cancer,Depression,Diabetes,Epilepsy,Heart_Disease,HBP,High_Cholesterol,Renal_Disease,Stroke,Thyroid,Alcohol,Smoke,Caffeine,Recreational_Drugs")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        /***************************************/
        /*            Delete Patient           */
        /***************************************/
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.PatientTable.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Patient patient = db.PatientTable.Find(id);
            db.PatientTable.Remove(patient);
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
