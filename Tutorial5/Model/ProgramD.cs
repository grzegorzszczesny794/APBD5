using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutorial5.Model
{
    [Table("Program")]
    public class ProgramD
    {
        public int ProgramId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }    
        public int DurationMinutes { get; set; }
        public int TemperatureCelsius { get; set; }
        public virtual ICollection<AvalaibleProgram> AvalaiblePrograms { get; set; }

    }
}
