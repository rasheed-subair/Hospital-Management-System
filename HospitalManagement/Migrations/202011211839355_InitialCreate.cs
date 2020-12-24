namespace HospitalManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accountants",
                c => new
                    {
                        AccountantId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 50),
                        ConfirmPassword = c.String(),
                        Phone = c.String(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.AccountantId);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 50),
                        ConfirmPassword = c.String(),
                        Phone = c.String(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AppointmentId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(),
                        Details = c.String(),
                        AppointmentDay = c.String(),
                        AppointmentTime = c.String(),
                    })
                .PrimaryKey(t => t.AppointmentId);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 50),
                        ConfirmPassword = c.String(),
                        Phone = c.String(nullable: false),
                        Address = c.String(),
                        Department = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DoctorId);
            
            CreateTable(
                "dbo.PatientRecords",
                c => new
                    {
                        PatientRecordId = c.Int(nullable: false, identity: true),
                        Weight = c.Double(nullable: false),
                        Height = c.Double(nullable: false),
                        BloodPressure = c.Double(nullable: false),
                        Temperature = c.Double(nullable: false),
                        Complaint = c.String(),
                        TimIn = c.String(),
                        AdmissionCost = c.Double(nullable: false),
                        CommentsDoctor = c.String(),
                        Prescription = c.String(),
                        TestRequired = c.String(),
                        ToBeAdmitted = c.Boolean(nullable: false),
                        WardAndBed = c.String(),
                        IsAdmitted = c.Boolean(nullable: false),
                        IsDischarged = c.Boolean(nullable: false),
                        PriceMed = c.Double(nullable: false),
                        MedsGiven = c.Boolean(nullable: false),
                        TestResult = c.String(),
                        PriceTest = c.Double(nullable: false),
                        TotalCost = c.Double(nullable: false),
                        PaidTotal = c.Boolean(nullable: false),
                        PaidTest = c.Boolean(nullable: false),
                        PaidMed = c.Boolean(nullable: false),
                        PatientId = c.String(maxLength: 128),
                        DoctorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PatientRecordId)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.PatientId)
                .Index(t => t.PatientId)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Phone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        PatientGender = c.Int(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        Occupation = c.String(),
                        Marital_Status = c.Int(nullable: false),
                        Photograph = c.String(),
                        ECName = c.String(),
                        ECRelationship = c.String(),
                        ECPhone = c.String(),
                        Allergies = c.String(),
                        Medication = c.String(),
                        Arthritis = c.Boolean(nullable: false),
                        Asthma = c.Boolean(nullable: false),
                        Cancer = c.Boolean(nullable: false),
                        Depression = c.Boolean(nullable: false),
                        Diabetes = c.Boolean(nullable: false),
                        Epilepsy = c.Boolean(nullable: false),
                        Heart_Disease = c.Boolean(nullable: false),
                        HBP = c.Boolean(nullable: false),
                        High_Cholesterol = c.Boolean(nullable: false),
                        Renal_Disease = c.Boolean(nullable: false),
                        Stroke = c.Boolean(nullable: false),
                        Thyroid = c.Boolean(nullable: false),
                        Alcohol = c.Boolean(nullable: false),
                        Smoke = c.Boolean(nullable: false),
                        Caffeine = c.Boolean(nullable: false),
                        Recreational_Drugs = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PatientId);
            
            CreateTable(
                "dbo.LabTeches",
                c => new
                    {
                        LabTechId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 50),
                        ConfirmPassword = c.String(),
                        Phone = c.String(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.LabTechId);
            
            CreateTable(
                "dbo.Medications",
                c => new
                    {
                        MedicationId = c.Int(nullable: false, identity: true),
                        DrugName = c.String(nullable: false),
                        BrandName = c.String(nullable: false),
                        DrugDescription = c.String(),
                        DrugPrice = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Category = c.Int(nullable: false),
                        ExpDate = c.String(),
                    })
                .PrimaryKey(t => t.MedicationId);
            
            CreateTable(
                "dbo.Newsletters",
                c => new
                    {
                        NewsletterId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.NewsletterId);
            
            CreateTable(
                "dbo.Nurses",
                c => new
                    {
                        NurseId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 50),
                        ConfirmPassword = c.String(),
                        Phone = c.String(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.NurseId);
            
            CreateTable(
                "dbo.Pharmacists",
                c => new
                    {
                        PharmacistId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 50),
                        ConfirmPassword = c.String(),
                        Phone = c.String(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.PharmacistId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientRecords", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.PatientRecords", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.PatientRecords", new[] { "DoctorId" });
            DropIndex("dbo.PatientRecords", new[] { "PatientId" });
            DropTable("dbo.Pharmacists");
            DropTable("dbo.Nurses");
            DropTable("dbo.Newsletters");
            DropTable("dbo.Medications");
            DropTable("dbo.LabTeches");
            DropTable("dbo.Patients");
            DropTable("dbo.PatientRecords");
            DropTable("dbo.Doctors");
            DropTable("dbo.Appointments");
            DropTable("dbo.Admins");
            DropTable("dbo.Accountants");
        }
    }
}
