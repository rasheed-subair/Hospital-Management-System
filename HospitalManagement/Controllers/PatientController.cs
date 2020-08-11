using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagement.Models;
using System.Runtime.Caching;

namespace HospitalManagement.Controllers
{
    public class PatientController : Controller
    {
        /*---------------------------------------------*/
        /*-         Create and save cache             -*/
        /*---------------------------------------------*/
        ObjectCache cache = MemoryCache.Default;
        List<Patient> patients;

        public PatientController()
        {
            patients = cache["patients"] as List<Patient>;

            if (patients == null)
            {
                patients = new List<Patient>();
            }
        }

        public void SaveCache()
        {
            cache["patients"] = patients;
        }

        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }

        /*---------------------------------------------*/
        /*-        Add Information for Patient        -*/
        /*---------------------------------------------*/
        public ActionResult AddPatient()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPatient(Patient patient)
        {
            patient.Id = Guid.NewGuid().ToString();
            patients.Add(patient);
            SaveCache();

            return RedirectToAction("PatientList");
        }

        /*---------------------------------------------*/
        /*-              View Patient List            -*/
        /*---------------------------------------------*/
        public ActionResult PatientList()
        {
            return View(patients);
        }

        /*---------------------------------------------*/
        /*-             View Patient Detail           -*/
        /*---------------------------------------------*/



        /*---------------------------------------------*/
        /*-         Edit Patient Information          -*/
        /*---------------------------------------------*/
        public ActionResult EditPatient(string id)
        {
            // Search memory for Patient with unique id and assign to the variable then display "if found"
            Patient patient = patients.FirstOrDefault(s => s.Id == id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(patient);
            }
        }

        [HttpPost]
        public ActionResult EditPatient(Patient patient, string id)
        {
            Patient patientToEdit = patients.FirstOrDefault(s => s.Id == id);
            if (patientToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Update Patient record
                patientToEdit.Name = patient.Name;
                patientToEdit.Phone = patient.Phone;
                patientToEdit.Email = patient.Email;
                patientToEdit.Address = patient.Address;
                patientToEdit.PatientGender = patient.PatientGender;
                patientToEdit.DOB = patient.DOB;
                patientToEdit.Occupation = patient.Occupation;
                patientToEdit.Marital_Status = patient.Marital_Status;
                patientToEdit.ECName = patient.ECName;
                patientToEdit.ECRelationship = patient.ECRelationship;
                patientToEdit.ECPhone = patient.ECPhone;
                patientToEdit.Allergies = patient.Allergies;
                patientToEdit.Medication = patient.Medication;
                patientToEdit.Alcohol = patient.Alcohol;
                patientToEdit.Smoke = patient.Smoke;
                patientToEdit.Caffeine = patient.Caffeine;
                patientToEdit.Recreational_Drugs = patient.Recreational_Drugs;
                patientToEdit.DOB = patient.DOB;
                patientToEdit.DOB = patient.DOB;

                SaveCache();
                return RedirectToAction("PatientList");
            }
        }

        /*---------------------------------------------*/
        /*-         Delete Doctor Information         -*/
        /*---------------------------------------------*/
        public ActionResult DeletePatient(string id)
        {
            Patient patient = patients.FirstOrDefault(s => s.Id == id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(patient);
            }
        }

        [HttpPost]
        [ActionName("Delete Patient")]
        public ActionResult ConfirmDeletePatient(string id)
        {
            Patient patient = patients.FirstOrDefault(s => s.Id == id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            else
            {
                patients.Remove(patient);
                return RedirectToAction("PatientList");
            }
        }
    }
}