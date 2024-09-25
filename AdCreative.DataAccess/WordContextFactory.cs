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

            // Design-time configuration: appsettings.json'dan connection string'i alýyoruz
            // Ana proje dizinini kullanarak appsettings.json'a ulaþýr
            //var currentDirectory = Directory.GetCurrentDirectory();
            var currentDirectory = Path.Combine(AppContext.BaseDirectory, "../../..");


            var configuration = new ConfigurationBuilder()
                .SetBasePath(currentDirectory) // Bu, çalýþtýrýlan ana proje dizinini alýr
                .AddJsonFile("appsettings.json") // appsettings.json dosyasýndan connection string'i okur
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);

            return new WordContext(optionsBuilder.Options);
        }
    }
}
