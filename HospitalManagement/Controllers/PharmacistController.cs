using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagement.Models;
using System.Runtime.Caching;

namespace HospitalManagement.Controllers
{
    public class PharmacistController : Controller
    {
        // GET: Pharmacist
        public ActionResult Index()
        {
            return View();
        }

        /*---------------------------------------------*/
        /*-         Create and save cache             -*/
        /*---------------------------------------------*/
        ObjectCache cache = MemoryCache.Default;
        List<Pharmacist> pharmacists;

        public PharmacistController()
        {
            pharmacists = cache["pharmacists"] as List<Pharmacist>;

            if (pharmacists == null)
            {
                pharmacists = new List<Pharmacist>();
            }
        }

        public void SaveCache()
        {
            cache["pharmacists"] = pharmacists;
        }



        /*---------------------------------------------*/
        /*-      Add Information for Pharmacist       -*/
        /*---------------------------------------------*/
        public ActionResult AddPharmacist()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPharmacist(Pharmacist pharmacist)
        {
            pharmacist.Id = Guid.NewGuid().ToString();
            pharmacists.Add(pharmacist);
            SaveCache();

            return RedirectToAction("PharmacistList");
        }

        /*---------------------------------------------*/
        /*-            View Pharmacist List           -*/
        /*---------------------------------------------*/
        public ActionResult PharmacistList()
        {
            return View(pharmacists);
        }

        /*---------------------------------------------*/
        /*-       Edit Pharmacist Information         -*/
        /*---------------------------------------------*/
        public ActionResult EditPharmacist(string id)
        {
            // Search memory for Pharmacist with unique id and assign to the variable then display "if found"
            Pharmacist pharmacist = pharmacists.FirstOrDefault(s => s.Id == id);
            if (pharmacist == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(pharmacist);
            }
        }

        [HttpPost]
        public ActionResult EditPharmacist(Pharmacist pharmacist, string id)
        {
            Pharmacist pharmacistToEdit = pharmacists.FirstOrDefault(s => s.Id == id);
            if (pharmacistToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Update Pharmacist record
                pharmacistToEdit.Name = pharmacist.Name;
                pharmacistToEdit.Email = pharmacist.Email;
                pharmacistToEdit.Password = pharmacist.Password;
                pharmacistToEdit.Phone = pharmacist.Phone;
                pharmacistToEdit.Address = pharmacist.Address;


                SaveCache();
                return RedirectToAction("PharmacistList");
            }
        }

        /*---------------------------------------------*/
        /*-       Delete Pharmacist Information       -*/
        /*---------------------------------------------*/
        public ActionResult DeletePharmacist(string id)
        {
            Pharmacist pharmacist = pharmacists.FirstOrDefault(s => s.Id == id);
            if (pharmacist == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(pharmacist);
            }
        }

        [HttpPost]
        [ActionName("DeletePharmacist")]
        public ActionResult ConfirmDeletePharmacist(string id)
        {
            Pharmacist pharmacist = pharmacists.FirstOrDefault(s => s.Id == id);
            if (pharmacist == null)
            {
                return HttpNotFound();
            }
            else
            {
                pharmacists.Remove(pharmacist);
                return RedirectToAction("PharmacistList");
            }
        }
    }
}