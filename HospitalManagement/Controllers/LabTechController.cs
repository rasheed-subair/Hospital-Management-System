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
    public class LabTechController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: LabTech
        public ActionResult Index()
        {
            return View(db.LabTechTable.ToList());
        }

        public ActionResult Homepage()
        {
            return View();
        }

        // GET: LabTech/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LabTech labTech = db.LabTechTable.Find(id);
            if (labTech == null)
            {
                return HttpNotFound();
            }
            return View(labTech);
        }

        // GET: LabTech/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LabTech/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LabTechId,Name,Email,Username,Password,ConfirmPassword,Phone,Address")] LabTech labTech)
        {
            if (ModelState.IsValid)
            {
                db.LabTechTable.Add(labTech);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(labTech);
        }

        // GET: LabTech/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LabTech labTech = db.LabTechTable.Find(id);
            if (labTech == null)
            {
                return HttpNotFound();
            }
            return View(labTech);
        }

        // POST: LabTech/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LabTechId,Name,Email,Username,Password,ConfirmPassword,Phone,Address")] LabTech labTech)
        {
            if (ModelState.IsValid)
            {
                db.Entry(labTech).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(labTech);
        }

        // GET: LabTech/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LabTech labTech = db.LabTechTable.Find(id);
            if (labTech == null)
            {
                return HttpNotFound();
            }
            return View(labTech);
        }

        // POST: LabTech/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LabTech labTech = db.LabTechTable.Find(id);
            db.LabTechTable.Remove(labTech);
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
