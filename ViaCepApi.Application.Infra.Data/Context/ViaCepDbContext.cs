using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using CepApi.Application.Abstraction.Domain.Models;

namespace CepApi.Application.Infra.Data.Context
{
    public class CepApiDbContext : DbContext
    {
        public CepApiDbContext(DbContextOptions<CepApiDbContext> options) : base(options) {}

        public DbSet<Address> Address { get; set; }
        public DbSet<Login> Login { get; set; }

        public class IntegrationContextFactory : IDesignTimeDbContextFactory<CepApiDbContext>
        {
            public CepApiDbContext CreateDbContext(string[] args)
            {
                var optionBuilder = new DbContextOptionsBuilder<CepApiDbContext>();
                var cnn = "Data Source=localhost;Database=ViaCepDb;uid=root;password=your_password!";
                optionBuilder.UseMySql(cnn,ServerVersion.AutoDetect(cnn));

                return new CepApiDbContext(optionBuilder.Options);
            }
        }
    }
}
