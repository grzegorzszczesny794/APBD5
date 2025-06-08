using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutorial5.Models
{
    //[Table("PrescriptionMedicament")]
    public class PrescriptionMedicament
    {
        //[Key]
        [Column(Order = 1)]
        [ForeignKey("Medicament")]
        public int IdMedicament { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("Prescription")]
        public int IdPrescription { get; set; }
        public int Dose { get; set; }
        [MaxLength(100)]
        public string Details { get; set; }
        public virtual Medicament Medicament { get; set; }
        public virtual Prescription Prescription { get; set; }
    }
}
