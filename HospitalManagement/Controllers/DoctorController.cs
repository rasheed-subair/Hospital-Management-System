using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using HospitalManagement.Models;
using System.Runtime.Caching;

namespace HospitalManagement.Controllers
{
    public class DoctorController : Controller
    {
        public ActionResult DoctorIndex()
        {
            ViewBag.Name = "Doctor";
            return View();
        }

        /*---------------------------------------------*/
        /*-         Create and save cache             -*/
        /*---------------------------------------------*/
        ObjectCache cache = MemoryCache.Default;
        List<Doctor> doctors;

        public DoctorController()
        {
            doctors = cache["doctors"] as List<Doctor>;

            if (doctors == null)
            {
                doctors = new List<Doctor>();
            }
        }

        public void SaveCache()
        {
            cache["doctors"] = doctors;
        }

        

        /*---------------------------------------------*/
        /*-         Add Information for Doctor        -*/
        /*---------------------------------------------*/
        public ActionResult AddDoctor()
        {
            ViewBag.Name = "Doctor";
            return View();
        }

        [HttpPost]
        public ActionResult AddDoctor(Doctor doctor)
        {
            ViewBag.Name = "Doctor";
            doctor.Id = Guid.NewGuid().ToString();
            doctors.Add(doctor);
            SaveCache();

            return RedirectToAction("DoctorList");
        }

        /*---------------------------------------------*/
        /*-              View Doctor List             -*/
        /*---------------------------------------------*/
        public ActionResult DoctorList()
        {
            return View(doctors);
        }

        /*---------------------------------------------*/
        /*-         Edit Doctor Information           -*/
        /*---------------------------------------------*/
        public ActionResult EditDoctor(string id)
        {
            // Search memory for Doctor with unique id and assign to the variable then display "if found"
            Doctor doctor = doctors.FirstOrDefault(s => s.Id == id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(doctor);
            }
        }

        [HttpPost]
        public ActionResult EditDoctor(Doctor doctor, string id)
        {
            Doctor doctorToEdit = doctors.FirstOrDefault(s => s.Id == id);
            if (doctorToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Update Doctor record
                doctorToEdit.Name = doctor.Name;
                doctorToEdit.Email = doctor.Email;
                doctorToEdit.Username = doctor.Username;
                doctorToEdit.Password = doctor.Password;
                doctorToEdit.Phone = doctor.Phone;
                doctorToEdit.Address = doctor.Address;
                doctorToEdit.D_Department = doctor.D_Department;

                SaveCache();
                return RedirectToAction("DoctorList");
            }
        }

        /*---------------------------------------------*/
        /*-         Delete Doctor Information         -*/
        /*---------------------------------------------*/
        public ActionResult DeleteDoctor(string id)
        {
            Doctor doctor = doctors.FirstOrDefault(s => s.Id == id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(doctor);
            }
        }

        [HttpPost]
        [ActionName("DeleteDoctor")]
        public ActionResult ConfirmDeleteDoctor(string id)
        {
            Doctor doctor = doctors.FirstOrDefault(s => s.Id == id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            else
            {
                doctors.Remove(doctor);
                return RedirectToAction("DoctorList");
            }
        }
    }
}