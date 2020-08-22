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

        // GET: PatientRecord
        public ActionResult Index()
        {
            var patientRecordTable = db.PatientRecordTable.Include(p => p.Doctor).Include(p => p.Patient);
            return View(patientRecordTable.ToList());
        }

        // GET: PatientRecord/Details/5
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

        // GET: PatientRecord/Create
        public ActionResult Create()
        {
            ViewBag.DoctorId = new SelectList(db.DoctorTable, "DoctorId", "Name");
            ViewBag.PatientId = new SelectList(db.PatientTable, "PatientId", "FirstName");
            return View();
        }

        // POST: PatientRecord/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: PatientRecord/Edit/5
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

        // POST: PatientRecord/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: PatientRecord/Delete/5
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
