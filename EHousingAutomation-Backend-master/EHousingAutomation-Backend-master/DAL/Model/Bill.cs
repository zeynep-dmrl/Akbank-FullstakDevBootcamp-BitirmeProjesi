using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Model
{
    public class Bill
    {
        [Key]
        public int billId { get; set; }

        [ForeignKey("houseId")]
        public int houseId { get; set; }
        public DateTime monthly { get; set; }
        public int price { get; set; }
        public string billDesc { get; set; }
        public bool isPaid { get; set; }
    }
}
