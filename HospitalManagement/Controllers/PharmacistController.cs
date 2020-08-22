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

        // GET: Pharmacist
        public ActionResult Index()
        {
            return View(db.PharmacistTable.ToList());
        }

        // GET: Pharmacist/Details/5
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

        // GET: Pharmacist/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pharmacist/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Pharmacist/Edit/5
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Pharmacist/Delete/5
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
