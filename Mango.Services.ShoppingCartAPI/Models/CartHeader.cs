using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ShoppingCartAPI.Models
{
    public class CartHeader
    {
        [Key]
        public int CartHeaderID { get; set; }
        public string UserID { get; set; }
        public string CouponCode { get; set; }
    }
}
