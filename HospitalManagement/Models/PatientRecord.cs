using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HospitalManagement.Models
{
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