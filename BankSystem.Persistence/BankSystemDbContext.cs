using BankSystem.Core.Domain.Cards.Models;
using BankSystem.Core.Domain.Clients.Models;
using BankSystem.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Persistence;

public class BankSystemDbContext(DbContextOptions<BankSystemDbContext> options) : DbContext(options)
{
    internal const string BankDbSchema = "bankdb";
    internal const string BankDbMigrationHistory = "_BankDbMigrationHistory";

    public DbSet<Client> Clients { get; set; }

    public DbSet<Card> Cards { get; set; }

    public DbSet<ClientsCards> ClientsCards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // todo configurations
        modelBuilder.HasDefaultSchema(BankDbSchema);
        modelBuilder.ApplyConfiguration(new ClientEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ClientCardEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CardEntityTypeConfiguration());
    }
}
