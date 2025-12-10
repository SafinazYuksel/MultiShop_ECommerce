using Microsoft.EntityFrameworkCore;
using MultiShop.Payment.Entities;

namespace MultiShop.Payment.Concrete
{
    public class PaymentContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public PaymentContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<PaymentDetail> PaymentDetails { get; set; }
    }
}
