using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Full Name is Required")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is Required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password  is Required")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must exceed 8 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Please confirm your Password.")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Phone number is Required")]
        public string Phone { get; set; }
        public string Address { get; set; }
        public Department Department { get; set; }

        //Since each patient record is usually related to a doctor, the list is required here as well
        public virtual ICollection<PatientRecord> PatientRecord { get; set; }
    }

    public enum Department
    {
        Aneasthetics,
        Cardiology,
        Dentistry,
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