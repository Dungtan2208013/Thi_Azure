using System.ComponentModel.DataAnnotations;

namespace WebAppShopping.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [MaxLength(50)]
        public string? ItemCode { get; set; }

        public string? ItemName { get; set; }

        public int ItemQty { get; set; }

        public DateTime OrderDelivery { get; set; }

        public string? OrderAddress { get; set; }

        public string? PhoneNumber { get; set; }
    }

}
