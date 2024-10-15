

using WebApiReportes.models;

using Microsoft.EntityFrameworkCore;


namespace WebApiReportes.context
{
    public class AppDbContext: DbContext
    {
        //Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }
        //dbset
        public DbSet<Cargo> Cargo { get; set; }
       
    }
    
}
