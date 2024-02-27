using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEWAN.Models
{
    public class item
    {
        [Key]
        public int itemId { get; set; }
        public string itemName { get; set; }
        public string image { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public int balance { get; set; }
        public int? recieptId { get; set; }
       
    }
}
