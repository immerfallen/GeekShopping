using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Repository.Interfaces;
using GeekShopping.ProductAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new
                ArgumentNullException(nameof(productRepository));
        }

        [HttpGet]        
        public async Task<ActionResult<List<ProductVO>>> FindAll()
        {
            var products = await _productRepository.FindAll();
            if (products == null) { return NotFound(); };
            return Ok(products);


        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductVO>> FindById(long id)
        {
            var product = await _productRepository.FindById(id);
            if(product.Id <= 0) { return NotFound(); };
            return Ok(product);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductVO>> Create([FromBody]ProductVO product)
        {
            if(product == null)
            {
                return BadRequest();
            }
            var res =  await _productRepository.Create(product);
           
            return Ok(res);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ProductVO>> Update([FromBody]ProductVO product)
        {
            if(product == null)
            {
                return BadRequest();
            }
            var res =  await _productRepository.Update(product);
           
            return Ok(res);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> Delete(long id)
        {
            var status = await _productRepository.Delete(id);
            if (!status) { return BadRequest(); };
            
            return Ok(status);
        }
    }
}
