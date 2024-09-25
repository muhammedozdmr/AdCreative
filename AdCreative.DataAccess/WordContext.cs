using AdCreative.DataAccess.EntityConfigurations;
using AdCreative.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AdCreative.DataAccess
{
    public class WordContext : DbContext
    {
        private readonly IConfiguration _configuration;

        // Run-time işlemler için kullanılan constructor
        public WordContext(DbContextOptions<WordContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        // Design-time işlemler için sadece DbContextOptions kullanan constructor
        public WordContext(DbContextOptions<WordContext> options)
            : base(options)
        {
        }

        public DbSet<WordAdd> Words { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RandomWordsConfigurations());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured && _configuration != null)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
