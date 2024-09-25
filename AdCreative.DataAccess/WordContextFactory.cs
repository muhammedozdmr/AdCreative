using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AdCreative.DataAccess
{
    public class WordContextFactory : IDesignTimeDbContextFactory<WordContext>
    {
        public WordContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WordContext>();

            // Design-time configuration: appsettings.json'dan connection string'i al�yoruz
            // Ana proje dizinini kullanarak appsettings.json'a ula��r
            //var currentDirectory = Directory.GetCurrentDirectory();
            var currentDirectory = Path.Combine(AppContext.BaseDirectory, "../../..");


            var configuration = new ConfigurationBuilder()
                .SetBasePath(currentDirectory) // Bu, �al��t�r�lan ana proje dizinini al�r
                .AddJsonFile("appsettings.json") // appsettings.json dosyas�ndan connection string'i okur
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);

            return new WordContext(optionsBuilder.Options);
        }
    }
}
