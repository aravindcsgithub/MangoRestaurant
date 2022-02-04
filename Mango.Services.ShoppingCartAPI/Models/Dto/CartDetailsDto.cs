using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.ShoppingCartAPI.Models.Dto
{
    public class CartDetailsDto
    {
        public int CartDetailsID { get; set; }

        public int CartHeaderID { get; set; }
        public virtual CartHeaderDto CartHeader { get; set; }

        public int ProductID { get; set; }
        public virtual ProductDto Product { get; set; }

        public int Quantity { get; set; }
    }
}
