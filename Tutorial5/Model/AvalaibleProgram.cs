using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tutorial5.Models;

namespace Tutorial5.Model
{
    [Table("Available_Program")]
    public class AvalaibleProgram
    {
        [Key]
        public int AvailableProgramId { get; set; }
        [ForeignKey(nameof(WaschingMachineId))]
        public int WaschingMachineId { get; set; }
        [ForeignKey(nameof(ProgramId))]
        public int ProgramId { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public virtual WashingMachine WachineMachine { get; set; }
        public virtual ProgramD Program { get; set; }
        public virtual ICollection<PurchaseHistory> PurchaseHistories { get; set; }
    }
}
