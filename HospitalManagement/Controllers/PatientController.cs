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
        public ActionResult Index(string searchString)
        {
            if (Session["AdminId"] != null || Session["DoctorId"] != null)
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    var patients = from s in db.PatientTable
                                      select s;
                    patients = patients.Where(s => s.LastName.Contains(searchString)
                               || s.FirstName.Contains(searchString));

                    return View(patients.ToList());
                }
                return View(db.PatientTable.ToList());
            }
            return RedirectToAction("Login", "Admin");
        }

        /***************************************/
        /*        View Patient Details         */
        /***************************************/
        public ActionResult Details(string id)
        {
            if (Session["AdminId"] != null || Session["DoctorId"] != null)
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
            return RedirectToAction("Login", "Admin");
        }

        /***************************************/
        /*      Create New Patient - Get       */
        /***************************************/
        public ActionResult Create()
        {
            if (Session["AdminId"] != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Admin");
        }

        // POST: Patient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatientId,FirstName,LastName,Phone,Email,Address,PatientGender,DOB,Occupation,Marital_Status,Photograph,ECName,ECRelationship,ECPhone,Allergies,Medication,Arthritis,Asthma,Cancer,Depression,Diabetes,Epilepsy,Heart_Disease,HBP,High_Cholesterol,Renal_Disease,Stroke,Thyroid,Alcohol,Smoke,Caffeine,Recreational_Drugs")] Patient patient, HttpPostedFileBase UploadImage)
        {
            if (ModelState.IsValid)
            {
                ///This allows you to upload images and save them to the database
                if (UploadImage != null)
                {
                    if (UploadImage.ContentType == "Image/jpg" || UploadImage.ContentType == "image/jpeg" || UploadImage.ContentType == "image/png")
                    {
                        UploadImage.SaveAs(Server.MapPath("/") + "/Content/images/" + UploadImage.FileName);
                        patient.Photograph = UploadImage.FileName;
                    }
                    else
                    {
                        ViewBag.Feedback = "Image Format not supported";
                        return View();
                    }

                }
                else
                {
                    return View();
                }
                //Upload image Ends

                string sample = Guid.NewGuid().ToString();
                patient.PatientId = sample.Substring(0, 5);
                db.PatientTable.Add(patient);
                db.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = "New Account Successfully Created";
            }

            return View();
        }

        /***************************************/
        /*        Edit Patient Detais          */
        /***************************************/
        public ActionResult Edit(string id)
        {
            if (Session["AdminId"] != null)
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
            return RedirectToAction("Login", "Admin");
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
            if (Session["AdminId"] != null)
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
            return RedirectToAction("Login", "Admin");
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
