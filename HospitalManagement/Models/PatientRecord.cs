using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Models
{
    public class PatientRecord
    {
        /********************************/
        /*       Edited By Admin        */
        /********************************/
        [Key]
        public int PatientRecordId { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }

        [DisplayName("Blood Pressure")]
        public int BloodPressure { get; set; }
        public int Temperature { get; set; }

        [DisplayName("Patient Complaint")]
        [DataType(DataType.MultilineText)]
        public string Complaint { get; set; }

        public string TimIn { get; set; }

        [DisplayName("Total cost - Admitted")]
        public int AdmissionCost { get; set; }

        /********************************/
        /*       Edited By Doctor       */
        /********************************/

        [DisplayName("Doctor Comments")]
        [DataType(DataType.MultilineText)]
        public string CommentsDoctor { get; set; }

        [DataType(DataType.MultilineText)]
        public string Prescription { get; set; }

        [DisplayName("Test(s) Required")]
        [DataType(DataType.MultilineText)]
        public string TestRequired { get; set; }

        [DisplayName("To Be Admitted")]
        public bool ToBeAdmitted { get; set; }

        /********************************/
        /*       Edited By Nurse        */
        /********************************/
        [DisplayName("Ward and Bed")]
        [DataType(DataType.MultilineText)]
        public string WardAndBed { get; set; }
        [DisplayName("Is Admitted")]
        public bool IsAdmitted { get; set; }

        [DisplayName("Is Discharged")]
        public bool IsDischarged { get; set; }

        /********************************/
        /*   Edited By Pharamacist      */
        /********************************/

        [DisplayName("Price- Medication")]
        public int PriceMed { get; set; }

        [DisplayName("Medication Delivered")]
        public bool MedsGiven { get; set; }

        /********************************/
        /*      Edited By Labtech       */
        /********************************/
        [DataType(DataType.MultilineText)]
        public string TestResult { get; set; }


        [DisplayName("Price- Lab Test")]
        public int PriceTest { get; set; }

        /********************************/
        /*    Edited By Accountant      */
        /********************************/

        [DisplayName("Total Cost")]
        public int TotalCost { get; set; }

        [DisplayName("Paid Total")]
        public bool PaidTotal { get; set; }

        [DisplayName("Paid Test")]
        public bool PaidTest { get; set; }

        [DisplayName("Paid Med")]
        public bool PaidMed { get; set; }

        /********************************/
        /*      Navigation Content      */
        /********************************/
        public string PatientId { get; set; }
        public int DoctorId { get; set; }


        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}