using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tutorial5.Models;

namespace Tutorial5.Model
{
    [Table("Purchase_History")]
    [PrimaryKey(nameof(AvalaibleProgramId), nameof(CustomerId))]
    public class PurchaseHistory
    {
        [ForeignKey(nameof(AvalaibleProgram))]
        public int AvalaibleProgramId { get; set; }
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int? Rating { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual AvalaibleProgram AvalaibleProgram { get; set; }
    }
}
