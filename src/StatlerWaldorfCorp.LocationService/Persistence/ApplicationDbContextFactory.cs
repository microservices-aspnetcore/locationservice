using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace StatlerWaldorfCorp.LocationService.Persistence 
{
    /*
    public class ApplicationDbContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext Create(DbContextFactoryOptions options)
        {
            string connStr = "User ID=locationuser;Password=locationpassword;Host=localhost;Port=5432;Database=locationdb;Pooling=true;";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(connStr);
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
    */
}