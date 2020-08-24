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
    public class AppointmentController : Controller
    {
        private HospitalContext db = new HospitalContext();

        /***************************************/
        /*          Appointment List           */
        /***************************************/
        public ActionResult Index(string searchString)
        {
            if (Session["AdminId"] != null || Session["DoctorId"] != null)
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    var appointments = from s in db.AppointmentTable
                                      select s;
                    appointments = appointments.Where(s => s.LastName.Contains(searchString)
                               || s.FirstName.Contains(searchString));

                    return View(appointments.ToList());
                }
                return View(db.AppointmentTable.ToList());
            }
            return RedirectToAction("Login", "Admin");
        }

        /***************************************/
        /*      View Appointment Details       */
        /***************************************/
        public ActionResult Details(int? id)
        {
            if (Session["AdminId"] != null || Session["DoctorId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Appointment appointment = db.AppointmentTable.Find(id);
                if (appointment == null)
                {
                    return HttpNotFound();
                }
                return View(appointment);
            }
            return RedirectToAction("Login", "Admin");
        }

        /***************************************/
        /*        Create New Appointment       */
        /***************************************/
        public ActionResult Create()
        {
            return View();
        }

        // POST: Appointment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppointmentId,FirstName,LastName,Email,Details,AppointmentDay,AppointmentTime")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.AppointmentTable.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appointment);
        }

        /***************************************/
        /*     Delete Appointment Details      */
        /***************************************/
        public ActionResult Delete(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Appointment appointment = db.AppointmentTable.Find(id);
                if (appointment == null)
                {
                    return HttpNotFound();
                }
                return View(appointment);
            }
            return RedirectToAction("Login", "Admin");
            
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.AppointmentTable.Find(id);
            db.AppointmentTable.Remove(appointment);
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
