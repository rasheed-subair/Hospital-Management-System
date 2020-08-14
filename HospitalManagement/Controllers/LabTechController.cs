using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagement.Models;
using System.Runtime.Caching;

namespace HospitalManagement.Controllers
{
    public class LabTechController : Controller
    {
        /*---------------------------------------------*/
        /*-         Create and save cache             -*/
        /*---------------------------------------------*/
        ObjectCache cache = MemoryCache.Default;
        List<LabTech> labtechs;

        public LabTechController()
        {
            labtechs = cache["labtechs"] as List<LabTech>;

            if (labtechs == null)
            {
                labtechs = new List<LabTech>();
            }
        }

        public void SaveCache()
        {
            cache["labtechs"] = labtechs;
        }

        public ActionResult LabTechIndex()
        {
            ViewBag.Name = "Laboratorist";
            return View();
        }

        /*---------------------------------------------*/
        /*-         Add Information for LabTech        -*/
        /*---------------------------------------------*/
        public ActionResult AddLabTech()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddLabTech(LabTech labtech)
        {
            labtech.Id = Guid.NewGuid().ToString();
            labtechs.Add(labtech);
            SaveCache();

            return RedirectToAction("LabTechList");
        }

        /*---------------------------------------------*/
        /*-              View LabTech List             -*/
        /*---------------------------------------------*/
        public ActionResult LabTechList()
        {
            return View(labtechs);
        }

        /*---------------------------------------------*/
        /*-         Edit LabTech Information           -*/
        /*---------------------------------------------*/
        public ActionResult EditLabTech(string id)
        {
            // Search memory for LabTech with unique id and assign to the variable then display "if found"
            LabTech labtech = labtechs.FirstOrDefault(s => s.Id == id);
            if (labtech == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(labtech);
            }
        }

        [HttpPost]
        public ActionResult EditLabTech(LabTech labtech, string id)
        {
            LabTech labtechToEdit = labtechs.FirstOrDefault(s => s.Id == id);
            if (labtechToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Update LabTech record
                labtechToEdit.Name = labtech.Name;
                labtechToEdit.Email = labtech.Email;
                labtechToEdit.Password = labtech.Password;
                labtechToEdit.Phone = labtech.Phone;
                labtechToEdit.Address = labtech.Address;

                SaveCache();
                return RedirectToAction("LabTechList");
            }
        }

        /*---------------------------------------------*/
        /*-         Delete LabTech Information         -*/
        /*---------------------------------------------*/
        public ActionResult DeleteLabTech(string id)
        {
            LabTech labtech = labtechs.FirstOrDefault(s => s.Id == id);
            if (labtech == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(labtech);
            }
        }

        [HttpPost]
        [ActionName("DeleteLabTech")]
        public ActionResult ConfirmDeleteLabTech(string id)
        {
            LabTech labtech = labtechs.FirstOrDefault(s => s.Id == id);
            if (labtech == null)
            {
                return HttpNotFound();
            }
            else
            {
                labtechs.Remove(labtech);
                return RedirectToAction("LabTechList");
            }
        }
    }
}