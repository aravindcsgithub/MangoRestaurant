using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.ShoppingCartAPI.Models
{
    public class CartDetails
    {
        [Key]
        public int CartDetailsID { get; set; }

        public int CartHeaderID { get; set; }
        [ForeignKey("CartHeaderID")]
        public virtual CartHeader CartHeader { get; set; }

        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
