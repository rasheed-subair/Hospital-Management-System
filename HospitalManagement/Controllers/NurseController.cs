using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagement.Models;
using System.Runtime.Caching;

namespace HospitalManagement.Controllers
{
    public class NurseController : Controller
    {
        // GET: Nurse
        public ActionResult Index()
        {
            return View();
        }

        /*---------------------------------------------*/
        /*-         Create and save cache             -*/
        /*---------------------------------------------*/
        ObjectCache cache = MemoryCache.Default;
        List<Nurse> nurses;

        public NurseController()
        {
            nurses = cache["nurses"] as List<Nurse>;

            if (nurses == null)
            {
                nurses = new List<Nurse>();
            }
        }

        public void SaveCache()
        {
            cache["nurses"] = nurses;
        }



        /*---------------------------------------------*/
        /*-         Add Information for Nurse        -*/
        /*---------------------------------------------*/
        public ActionResult AddNurse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNurse(Nurse nurse)
        {
            nurse.Id = Guid.NewGuid().ToString();
            nurses.Add(nurse);
            SaveCache();

            return RedirectToAction("NurseList");
        }

        /*---------------------------------------------*/
        /*-              View Nurse List             -*/
        /*---------------------------------------------*/
        public ActionResult NurseList()
        {
            return View(nurses);
        }

        /*---------------------------------------------*/
        /*-         Edit Nurse Information           -*/
        /*---------------------------------------------*/
        public ActionResult EditNurse(string id)
        {
            // Search memory for Nurse with unique id and assign to the variable then display "if found"
            Nurse nurse = nurses.FirstOrDefault(s => s.Id == id);
            if (nurse == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(nurse);
            }
        }

        [HttpPost]
        public ActionResult EditNurse(Nurse nurse, string id)
        {
            Nurse nurseToEdit = nurses.FirstOrDefault(s => s.Id == id);
            if (nurseToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Update Nurse record
                nurseToEdit.Name = nurse.Name;
                nurseToEdit.Email = nurse.Email;
                nurseToEdit.Password = nurse.Password;
                nurseToEdit.Phone = nurse.Phone;
                nurseToEdit.Address = nurse.Address;
               
                SaveCache();
                return RedirectToAction("NurseList");
            }
        }

        /*---------------------------------------------*/
        /*-         Delete Nurse Information         -*/
        /*---------------------------------------------*/
        public ActionResult DeleteNurse(string id)
        {
            Nurse nurse = nurses.FirstOrDefault(s => s.Id == id);
            if (nurse == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(nurse);
            }
        }

        [HttpPost]
        [ActionName("DeleteNurse")]
        public ActionResult ConfirmDeleteNurse(string id)
        {
            Nurse nurse = nurses.FirstOrDefault(s => s.Id == id);
            if (nurse == null)
            {
                return HttpNotFound();
            }
            else
            {
                nurses.Remove(nurse);
                return RedirectToAction("NurseList");
            }
        }
    }
}