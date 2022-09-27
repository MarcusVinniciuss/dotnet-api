using EuPagoAPI.Models;
using EuPagoAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace EuPagoAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DataContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>().HasQueryFilter(p => p.StatusCadastro != StatusCadastro.Desativado);
            modelBuilder.Entity<Cartao>().HasQueryFilter(c => c.StatusCadastro != StatusCadastro.Desativado);
            modelBuilder.Entity<Estabelecimento>().HasQueryFilter(e => e.StatusCadastro != StatusCadastro.Desativado);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("EuPagoDB");
                optionsBuilder.UseOracle(connectionString);
            }
        }

        //public DbSet<Compra> Compras { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
       // public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<Cartao> Cartoes { get; set; }
        public DbSet<Estabelecimento> Estabelecimentos { get; set; }
    }
}
