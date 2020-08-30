using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email.")]
        public string Email { get; set; }

        [DisplayName("Appointment Details")]
        [DataType(DataType.MultilineText)]
        public string Details { get; set; }

        [DisplayName("Appointment Day")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string AppointmentDay { get; set; }

        [DisplayName("Appointment Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
        public string AppointmentTime { get; set; }
    }
}