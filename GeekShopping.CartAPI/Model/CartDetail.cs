using GeekShopping.CartAPI.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CartAPI.Model
{
    [Table("cart_detail")]
    public class CartDetail : BaseEntity
    {
        [ForeignKey("CartHeaderId")]
        public virtual CartHeader CartHeader { get; set; }
        public long CartHeaderId { get; set; }       
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }       
        public long ProductId { get; set; }
        [Column("count")]
        public int Count { get; set; }
    }
}
