using System.ComponentModel.DataAnnotations;
using Tutorial5.Model;

namespace Tutorial5.DTOs
{
    public class WachineMachineDto
    {
        public int WashingMachineId { get; set; }

        [Required]
        [StringLength(100)]
        public string SerialNumber { get; set; }
        [Required]
        public decimal MaxWeight { get; set; }

        public virtual ICollection<AvailableProgramDto> AvailablePrograms { get; set; } 
    }

    public class AvailableProgramDto
    {
      public string name { get; set; }
        public decimal price { get; set; }
    }
}
