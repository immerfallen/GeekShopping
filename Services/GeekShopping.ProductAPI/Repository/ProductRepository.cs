using AutoMapper;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Context;
using GeekShopping.ProductAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {

        private readonly IMapper _mapper;
        private readonly SQLServerContext _sqlServerContext;

        public ProductRepository(IMapper mapper, SQLServerContext sqlServerContext)
        {
            _mapper = mapper;
            _sqlServerContext = sqlServerContext;
        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            List<Product> products  = await _sqlServerContext.Products.ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> FindById(long id)
        {
            Product? product = await _sqlServerContext.Products
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            return _mapper.Map<ProductVO>(product);
        }

        public Task<ProductVO> Create(ProductVO product)
        {
            throw new NotImplementedException();
        }

        public Task<ProductVO> Update(ProductVO product)
        {
            throw new NotImplementedException();
        }

        

        public Task<bool> Delete(long id)
        {
            throw new NotImplementedException();
        }

       
    }
}
