using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ShoppingCartAPI.Models.Dto
{
    public class CartHeaderDto
    {
        public int CartHeaderID { get; set; }
        public string UserID { get; set; }
        public string CouponCode { get; set; }
    }
}
