using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AdCreative.DataAccess
{
    public class WordContextFactory : IDesignTimeDbContextFactory<WordContext>
    {
        public WordContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WordContext>();

            // Design-time i�lemler i�in connection string'i manuel olarak sa�l�yoruz
            var connectionString = "Server=localhost;Database=AdCreativeDb;User Id='.';Password='';Integrated Security=True;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionString);

            return new WordContext(optionsBuilder.Options);
        }
    }
}
