using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Models
{
    public class Medication
    {
        [Key]
        public int MedicationId { get; set; }

        [DisplayName("Drug Name")]
        [Required(ErrorMessage = "Drug Name is Required")]
        public string DrugName { get; set; }

        [DisplayName("Brand Name")]
        [Required(ErrorMessage = "Brand Name is Required")]
        public string BrandName { get; set; }

        [DisplayName("Drug Description")]
        [DataType(DataType.MultilineText)]
        public string DrugDescription { get; set; }

        [DisplayName("Drug Price")]
        [Required(ErrorMessage = "Drug Price is Required")]
        public double DrugPrice { get; set; }

        public int Quantity { get; set; }

        [DisplayName("Drug Category")]
        [Required(ErrorMessage = "Drug Category is Required")]
        public DrugCategory Category { get; set; }

        
        [DisplayName("Expiry Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string ExpDate { get; set; }
    }

    public enum DrugCategory
    {
        Stimulants,
        Depressants,
        Hallucinogens,
        Inhalants,
        Opioids,
        Cannabinoids,
        Dissociative,
        Other
    }
}