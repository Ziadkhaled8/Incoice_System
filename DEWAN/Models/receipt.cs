using DEWAN.Models;
using Microsoft.AspNetCore.Routing.Constraints;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
namespace DEWAN.Models
{
    public class receipt
    {
        [Key]
        public int receiptId { get; set; }
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Customer name must be between 2 and 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Customer name must contain only letters.")]
        [Display(Name = "Customer Name")]
        public string customerName { get; set; }
        public decimal totalAmount { get; set; } = 0;
        public decimal paidAmount { get; set; }= 0;
        public decimal remainingAmount { get { return totalAmount - paidAmount; } }

    }
}
