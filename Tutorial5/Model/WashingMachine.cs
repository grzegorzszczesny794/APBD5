using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutorial5.Model
{
    [Table("Washing_Machine")]
    public class WashingMachine
    {
        [Key]
        public int WashingMachineId { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal MaxWeight { get; set; }
        [MaxLength(100)]
        public string SerialNumber { get; set; }
        public virtual ICollection<AvalaibleProgram> AvalaiblePrograms { get; set; }

    }
}
