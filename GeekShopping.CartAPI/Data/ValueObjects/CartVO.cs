namespace GeekShopping.CartAPI.Model
{
    public class CartVO
    {
        public long Id { get; set; }
        public CartHeaderVO CartHeaderVO { get; set; }
        public IEnumerable<CartDetailVO> CartDetailsVO { get; set; }
    }
}
