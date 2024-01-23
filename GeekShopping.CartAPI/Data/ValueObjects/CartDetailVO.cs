using GeekShopping.CartAPI.Data.ValueObjects;

namespace GeekShopping.CartAPI.Model
{

    public class CartDetailVO 
    {
        public long Id  { get; set; }
        public CartHeaderVO CartHeaderVO { get; set; }
        public long CartHeaderId { get; set; }          
        public ProductVO ProductVO { get; set; }       
        public long ProductId { get; set; }        
        public int Count { get; set; }
    }
}
