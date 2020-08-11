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
        [DisplayName("Personal Medical History")]
        public Disease_Condition Medical_History { get; set; }
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
    public enum Disease_Condition
    {
        Arthritis,
        Asthma,
        Cancer,
        Depression,
        Diabetes,
        Emphysema,
        Epilepsy,
        Heart_Disease,
        HBP,
        High_Cholesterol,
        Hypothyroidism,
        Renal_Disease,
        Stroke,
        Thyroid
    }

    public class PatientRecord
    {
        public string Id { get; set; }
        public int weight { get; set; }
        public int height { get; set; }
        public int BloodPressure { get; set; }
        public int Temperature { get; set; }
        public string Complaint { get; set; }
        public string Comments { get; set; }
        public string Prescription { get; set; }
        public string TestResult { get; set; }
        [DisplayName("Price of Medication")]
        public int Price_Med { get; set; }
        [DisplayName("Price of Laboratory Test")]
        public int Price_Lab { get; set; }
    }

}