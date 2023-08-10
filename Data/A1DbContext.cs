using Microsoft.EntityFrameworkCore;


public class A1DbContext : DbContext
{

    
    public A1DbContext(DbContextOptions<A1DbContext> options) : base(options)
    {
        
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Comment> Comments { get; set; }
    


}