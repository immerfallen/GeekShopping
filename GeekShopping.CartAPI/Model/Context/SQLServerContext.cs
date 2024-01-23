using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Model.Context
{
    public class SQLServerContext : DbContext
    {
        
        public SQLServerContext() { }           
        public SQLServerContext(DbContextOptions<SQLServerContext> options) : base(options) { }    
        
        public DbSet<Product> Products { get; set; }
        public DbSet<CartDetailVO> CartDetails { get; set; }
        public DbSet<CartHeaderVO> CartHeaders { get; set; }
        

    }
}
