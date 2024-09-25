using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AdCreative.DataAccess
{
    public class WordContextFactory : IDesignTimeDbContextFactory<WordContext>
    {
        public WordContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WordContext>();

            // Design-time iþlemler için connection string'i manuel olarak saðlýyoruz
            var connectionString = "Server=localhost;Database=AdCreativeDb;User Id='.';Password='';Integrated Security=True;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionString);

            return new WordContext(optionsBuilder.Options);
        }
    }
}
