using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Tutorial5.Models;

namespace Tutorial5.Model
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        [MaxLength(100)]
        [AllowNull]
        public string? PhoneNumber { get; set; }
        public virtual ICollection<PurchaseHistory> PurchaseHistories { get; set; }
    }
}
