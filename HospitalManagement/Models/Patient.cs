using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace HospitalManagement.Models
{
    public class Patient
    {
        /********************************/
        /*       General Details        */
        /********************************/
        [Key]
        public string PatientId { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }

        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email.")]
        public string Email { get; set; }
        public string Address { get; set; }

        [DisplayName("Gender")]
        public Gender PatientGender { get; set; }
        public DateTime DOB { get; set; }
        public string Occupation { get; set; }

        [DisplayName("Marital Status")]
        public MaritalStatus Marital_Status { get; set; }

        public string Photograph { get; set; }

        /********************************/
        /*      Emergency Contact       */
        /********************************/
        [DisplayName("Full Name")]
        public string ECName { get; set; }

        [DisplayName("Relationship")]
        public string ECRelationship { get; set; }

        [DisplayName("Phone")]
        public string ECPhone { get; set; }

        /********************************/
        /*        Medical History       */
        /********************************/

        // Allergies to be listed in a textarea
        public string Allergies { get; set; }
        // Medication to be listed in a textarea
        public string Medication { get; set; }

        //Medical History
        public bool Arthritis { get; set; }
        public bool Asthma { get; set; }
        public bool Cancer { get; set; }
        public bool Depression { get; set; }
        public bool Diabetes { get; set; }
        public bool Epilepsy { get; set; }
        public bool Heart_Disease { get; set; }

        [DisplayName("High Blood Pressure")]
        public bool HBP { get; set; }

        [DisplayName("High Cholesterol")]
        public bool High_Cholesterol { get; set; }
        public bool Renal_Disease { get; set; }
        public bool Stroke { get; set; }
        public bool Thyroid { get; set; }

        public bool Alcohol { get; set; }
        public bool Smoke { get; set; }
        public bool Caffeine { get; set; }
        [DisplayName("Recreational Drugs")]
        public bool Recreational_Drugs { get; set; }

        //This collection holds the list for the one-to-many relationship between Patients and their "many" Records
        public virtual ICollection<PatientRecord> PatientRecord { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }
    public enum MaritalStatus
    {
        Married,
        Single,
        Divorced,
        Widowed,
        Other
    }
}