using GeekShopping.OrderAPI.Model;
using GeekShopping.OrderAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using GeekShopping.OrderAPI.Model.Context;

namespace GeekShopping.OrderAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextOptions<SQLServerContext> _context;
        

        public OrderRepository(DbContextOptions<SQLServerContext> context)
        {
            _context = context;            
        }

        public async Task<bool> AddOrder(OrderHeader header)
        {
            if(header == null) { return false; }
            await using var _db = new SQLServerContext(_context);
            _db.Headers.Add(header);
            await _db.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public async Task UpdateOrderPaymentStatus(long orderHeaderId, bool status)
        {
            await using var _db = new SQLServerContext(_context);
            var header = await _db.Headers.FirstOrDefaultAsync(h => h.Id == orderHeaderId);
            if(header!= null)
            {
                header.PaymentStatus = status;
                await _db.SaveChangesAsync();
            }
            throw new NotImplementedException();
        }

        public async Task<CartVO> SaveOrUpdateCart(CartVO vo)
        {
            Cart cart = _mapper.Map<Cart>(vo);
            //Checks if the product is already saved in the database if it does not exist then save
            var product = await _context.Products.FirstOrDefaultAsync(
                p => p.Id == vo.CartDetails.FirstOrDefault().ProductId);

            if (product == null)
            {
                _context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _context.SaveChangesAsync();
            }

            //Check if CartHeader is null

            var cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(
                c => c.UserId == cart.CartHeader.UserId);

            if (cartHeader == null)
            {
                //Create CartHeader and CartDetails
                if(cart.CartHeader.CouponCode == null)
                {
                    cart.CartHeader.CouponCode = "";
                }

                _context.CartHeaders.Add(cart.CartHeader);
                await _context.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null;
                _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else
            {
                //If CartHeader is not null
                //Check if CartDetails has same product
                var cartDetail = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                    p => p.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                    p.CartHeaderId == cartHeader.Id);

                if (cartDetail == null)
                {
                    //Create CartDetails
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeader.Id;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
                else
                {
                    //Update product count and CartDetails
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetail.Count;
                    cart.CartDetails.FirstOrDefault().Id = cartDetail.Id;
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
                    _context.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
            }
            return _mapper.Map<CartVO>(cart);
        }

       
    }
}
