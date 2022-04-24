using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EFData
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer("Server =(localdb)\\MSSQLLocalDB; Database=AuthFridgeDatabase; Trusted_Connection=True;");

            return new DataContext(optionsBuilder.Options);
        }
    }
}