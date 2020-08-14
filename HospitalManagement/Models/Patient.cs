using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;


namespace HospitalManagement.Models
{
    public class Patient
    {
        /*      General Details       */
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        [DisplayName("Gender")]
        public Gender PatientGender { get; set; }
        public DateTime DOB { get; set; }
        public string Occupation { get; set; }
        [DisplayName("Marital Status")]
        public MaritalStatus Marital_Status { get; set; }

        /*      Emergency Contact       */
        [DisplayName("Full Name")]
        public string ECName { get; set; }
        [DisplayName("Relationship")]
        public string ECRelationship { get; set; }
        [DisplayName("Phone")]
        public string ECPhone { get; set; }

        /*      Medical History       */

        // Allergies to be listed in a textbox
        public string Allergies { get; set; }
        // Medication to be listed in a textbox
        public string Medication { get; set; }

        //Medical History
        public bool Arthritis { get; set; }
        public bool Asthma { get; set; }
        public bool Cancer { get; set; }
        public bool Depression { get; set; }
        public bool Diabetes { get; set; }
        public bool Emphysema { get; set; }
        public bool Epilepsy { get; set; }
        public bool Heart_Disease { get; set; }
        public bool HBP { get; set; }
        public bool High_Cholesterol { get; set; }
        public bool Hypothyroidism { get; set; }
        public bool Renal_Disease { get; set; }
        public bool Stroke { get; set; }
        public bool Thyroid { get; set; }

        public bool Alcohol { get; set; }
        public bool Smoke { get; set; }
        public bool Caffeine { get; set; }
        [DisplayName("Recreational Drugs")]
        public bool Recreational_Drugs { get; set; }
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
        Widowed
    }
}