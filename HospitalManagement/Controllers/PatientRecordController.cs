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
        public ActionResult Index(string searchString)
        {
            if (Session["AdminId"] != null || Session["DoctorId"] != null || Session["NurseId"] != null || Session["PharmacistId"] != null || Session["LabTechId"] != null || Session["AccountantId"] != null)
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    var patientRecords = from s in db.PatientRecordTable
                                      select s;
                    patientRecords = patientRecords.Where(s => s.PatientId.Contains(searchString));

                    return View(patientRecords.ToList());
                }
                var patientRecordTable = db.PatientRecordTable.Include(p => p.Doctor).Include(p => p.Patient);
                return View(patientRecordTable.ToList());
            }
            return RedirectToAction("Login", "Admin");
            
        }


        /***************************************/
        /*        View Patient Record          */
        /***************************************/
        public ActionResult Details(int? id)
        {
            if (Session["AdminId"] != null || Session["DoctorId"] != null || Session["NurseId"] != null || Session["PharmacistId"] != null || Session["LabTechId"] != null || Session["AccountantId"] != null)
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
            return RedirectToAction("Login", "Admin");
        }

        /***************************************/
        /*      Create New Patient Record      */
        /***************************************/
        public ActionResult Create()
        {
            if (Session["AdminId"] != null)
            {
                ViewBag.DoctorId = new SelectList(db.DoctorTable, "DoctorId", "Name");
                ViewBag.PatientId = new SelectList(db.PatientTable, "PatientId", "PatientId");
                return View();
            }
            return RedirectToAction("Login", "Admin");
        }

        // POST: PatientRecord/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatientRecordId,Weight,Height,BloodPressure,Temperature,Complaint,TimIn,AdmissionCost,CommentsDoctor,Prescription,TestRequired,ToBeAdmitted,WardAndBed,IsAdmitted,IsDischarged,PriceMed,MedsGiven,TestResult,PriceTest,TotalCost,PaidTotal,PaidMed,PaidTest,PatientId,DoctorId")] PatientRecord patientRecord)
        {
            if (ModelState.IsValid)
            {
                db.PatientRecordTable.Add(patientRecord);
                db.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = "New Patient Record Successfully Created";
            }

            ViewBag.DoctorId = new SelectList(db.DoctorTable, "DoctorId", "Name", patientRecord.DoctorId);
            ViewBag.PatientId = new SelectList(db.PatientTable, "PatientId", "FirstName", patientRecord.PatientId);
            return View();
        }

        /***************************************/
        /*        Edit Patient Record          */
        /***************************************/
        public ActionResult Edit(int? id)
        {
            if (Session["AdminId"] != null || Session["DoctorId"] != null || Session["NurseId"] != null || Session["PharmacistId"] != null || Session["LabTechId"] != null || Session["AccountantId"] != null)
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
            return RedirectToAction("Login", "Admin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PatientRecordId,Weight,Height,BloodPressure,Temperature,Complaint,TimIn,AdmissionCost,CommentsDoctor,Prescription,TestRequired,ToBeAdmitted,WardAndBed,IsAdmitted,IsDischarged,PriceMed,MedsGiven,TestResult,PriceTest,TotalCost,PaidTotal,PaidMed,PaidTest,PatientId,DoctorId")] PatientRecord patientRecord)
        {
            if (ModelState.IsValid)
            {
                ViewBag.DoctorId = new SelectList(db.DoctorTable, "DoctorId", "Name", patientRecord.DoctorId);
                ViewBag.PatientId = new SelectList(db.PatientTable, "PatientId", "FirstName", patientRecord.PatientId);
                
                db.Entry(patientRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View();
        }

        /***************************************/
        /*        Delete Patient Record        */
        /***************************************/
        public ActionResult Delete(int? id)
        {
            if (Session["AdminId"] != null)
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
            return RedirectToAction("Login", "Admin");
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
