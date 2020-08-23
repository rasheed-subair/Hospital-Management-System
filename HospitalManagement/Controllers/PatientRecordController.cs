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
    public class PatientRecordController : Controller
    {
        private HospitalContext db = new HospitalContext();

        /***************************************/
        /*        Patient Record List          */
        /***************************************/
        public ActionResult Index()
        {
            var patientRecordTable = db.PatientRecordTable.Include(p => p.Doctor).Include(p => p.Patient);
            return View(patientRecordTable.ToList());
        }


        /***************************************/
        /*        View Patient Record          */
        /***************************************/
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientRecord patientRecord = db.PatientRecordTable.Find(id);
            if (patientRecord == null)
            {
                return HttpNotFound();
            }
            return View(patientRecord);
        }

        /***************************************/
        /*      Create New Patient Record      */
        /***************************************/
        public ActionResult Create()
        {
            ViewBag.DoctorId = new SelectList(db.DoctorTable, "DoctorId", "Name");
            ViewBag.PatientId = new SelectList(db.PatientTable, "PatientId", "FirstName");
            return View();
        }

        // POST: PatientRecord/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatientRecordId,Weight,Height,BloodPressure,Temperature,Complaint,TimIn,AdmissionCost,CommentsDoctor,Prescription,TestRequired,ToBeAdmitted,WardAndBed,IsAdmitted,IsDischarged,PriceMed,MedsGiven,TestResult,PriceTest,TotalCost,Paid,PatientId,DoctorId")] PatientRecord patientRecord)
        {
            if (ModelState.IsValid)
            {
                db.PatientRecordTable.Add(patientRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctorId = new SelectList(db.DoctorTable, "DoctorId", "Name", patientRecord.DoctorId);
            ViewBag.PatientId = new SelectList(db.PatientTable, "PatientId", "FirstName", patientRecord.PatientId);
            return View(patientRecord);
        }

        /***************************************/
        /*        Edit Patient Record          */
        /***************************************/
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientRecord patientRecord = db.PatientRecordTable.Find(id);
            if (patientRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorId = new SelectList(db.DoctorTable, "DoctorId", "Name", patientRecord.DoctorId);
            ViewBag.PatientId = new SelectList(db.PatientTable, "PatientId", "FirstName", patientRecord.PatientId);
            return View(patientRecord);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PatientRecordId,Weight,Height,BloodPressure,Temperature,Complaint,TimIn,AdmissionCost,CommentsDoctor,Prescription,TestRequired,ToBeAdmitted,WardAndBed,IsAdmitted,IsDischarged,PriceMed,MedsGiven,TestResult,PriceTest,TotalCost,Paid,PatientId,DoctorId")] PatientRecord patientRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patientRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorId = new SelectList(db.DoctorTable, "DoctorId", "Name", patientRecord.DoctorId);
            ViewBag.PatientId = new SelectList(db.PatientTable, "PatientId", "FirstName", patientRecord.PatientId);
            return View(patientRecord);
        }

        /***************************************/
        /*        Delete Patient Record        */
        /***************************************/
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientRecord patientRecord = db.PatientRecordTable.Find(id);
            if (patientRecord == null)
            {
                return HttpNotFound();
            }
            return View(patientRecord);
        }

        // POST: PatientRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PatientRecord patientRecord = db.PatientRecordTable.Find(id);
            db.PatientRecordTable.Remove(patientRecord);
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
