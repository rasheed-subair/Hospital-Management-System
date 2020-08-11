using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagement.Models
{
    public class Doctor
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string D_Department { get; set; }
    }

    public enum Department
    {
        Aneasthetics,
        Cardiology,
        ENT,
        Geriatrics,
        Gastroenterology,
        General_Surgery,
        Gynaecology,
        Haematology,
        Maternity,
        Neurology,
        Oncology,
        Opthalmology,
        Orthopedics,
        Urology,
        Psychiatry,
        CSU,
        Physiotherapy,
    }
}