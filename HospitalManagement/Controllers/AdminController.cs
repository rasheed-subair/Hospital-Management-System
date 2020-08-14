using HospitalManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagement.Models;
using System.Runtime.Caching;

namespace HospitalManagement.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult admin_index()
        {
            ViewBag.Name = "Admin";
            return View();
        }
        /*---------------------------------------------*/
        /*-         Create and save cache             -*/
        /*---------------------------------------------*/
        ObjectCache cache = MemoryCache.Default;
        List<Admin> admins;

        public AdminController()
        {
            admins = cache["admins"] as List<Admin>;

            if (admins == null)
            {
                admins = new List<Admin>();
            }
        }

        public void SaveCache()
        {
            cache["admins"] = admins;
        }

        /*---------------------------------------------*/
        /*-         Add Information for Admin        -*/
        /*---------------------------------------------*/
        public ActionResult AddAdmin()
        {
            ViewBag.Name = "Admin";
            return View();
        }

        [HttpPost]
        public ActionResult AddAdmin(Admin admin)
        {
            ViewBag.Name = "Admin";
            admin.Id = Guid.NewGuid().ToString();
            admins.Add(admin);
            SaveCache();

            return RedirectToAction("AdminList");
        }

        /*---------------------------------------------*/
        /*-              View Admin List             -*/
        /*---------------------------------------------*/
        public ActionResult AdminList()
        {
            return View(admins);
        }

        /*---------------------------------------------*/
        /*-         Edit Admin Information           -*/
        /*---------------------------------------------*/
        public ActionResult EditAdmin(string id)
        {
            // Search memory for Admin with unique id and assign to the variable then display "if found"
            Admin admin = admins.FirstOrDefault(s => s.Id == id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(admin);
            }
        }

        [HttpPost]
        public ActionResult EditAdmin(Admin admin, string id)
        {
            Admin adminToEdit = admins.FirstOrDefault(s => s.Id == id);
            if (adminToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Update Admin record
                adminToEdit.Name = admin.Name;
                adminToEdit.Email = admin.Email;
                adminToEdit.Password = admin.Password;
                adminToEdit.Phone = admin.Phone;
                adminToEdit.Address = admin.Address;

                SaveCache();
                return RedirectToAction("AdminList");
            }
        }

        /*---------------------------------------------*/
        /*-         Delete Admin Information         -*/
        /*---------------------------------------------*/
        public ActionResult DeleteAdmin(string id)
        {
            Admin admin = admins.FirstOrDefault(s => s.Id == id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(admin);
            }
        }

        [HttpPost]
        [ActionName("DeleteAdmin")]
        public ActionResult ConfirmDeleteAdmin(string id)
        {
            Admin admin = admins.FirstOrDefault(s => s.Id == id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            else
            {
                admins.Remove(admin);
                return RedirectToAction("AdminList");
            }
        }
    }
}