using AutoMapper;
using GeekShopping.CartAPI.Model;
using GeekShopping.CartAPI.Model.Context;
using GeekShopping.CartAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly SQLServerContext _context;
        private readonly IMapper _mapper;

        public CartRepository(SQLServerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ClearCart(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<CartVO> FindCartByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public  async Task<bool> RemoveCoupon(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(long cartDetailsId)
        {
            throw new NotImplementedException();
        }

        public async Task<CartVO> SaveOrUpdateCart(CartVO vo)
        {
            Cart cart = _mapper.Map<Cart>(vo);
            var product = await _context.Products.FirstOrDefaultAsync(
                p => p.Id == vo.CartDetailsVO.FirstOrDefault().ProductId);

            if(product == null) 
            {
                _context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _context.SaveChangesAsync();
            }

            var cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(
                c=> c.UserId == cart.CartHeader.UserId);

            if(cartHeader == null)
            {
                _context.CartHeaders.Add(cart.CartHeader);
                await _context.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null;
                _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
        }
    }
}
