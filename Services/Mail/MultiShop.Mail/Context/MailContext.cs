using Microsoft.EntityFrameworkCore;
using MultiShop.Mail.Entities;

namespace MultiShop.Mail.Context
{
    public class MailContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public MailContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<SentMail> SentMails { get; set; }
    }
}
