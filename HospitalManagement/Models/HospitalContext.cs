using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace HospitalManagement.Models
{
    public class HospitalContext : DbContext
    {
        public HospitalContext() : base("HospitalContext")
        {
        }

        public DbSet<Admin> AdminTable { get; set; }
        public DbSet<Accountant> AccountantTable { get; set; }
        public DbSet<Appointment> AppointmentTable { get; set; }
        public DbSet<Doctor> DoctorTable { get; set; }
        public DbSet<LabTech> LabTechTable { get; set; }
        public DbSet<Medication> MedicationTable { get; set; }
        public DbSet<Newsletter> NewsletterTable { get; set; }
        public DbSet<Nurse> NurseTable { get; set; }
        public DbSet<Patient> PatientTable { get; set; }
        public DbSet<PatientRecord> PatientRecordTable { get; set; }
        public DbSet<Pharmacist> PharmacistTable { get; set; }

    }
}