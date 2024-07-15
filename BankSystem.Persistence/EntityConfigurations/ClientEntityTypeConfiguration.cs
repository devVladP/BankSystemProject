using BankSystem.Core.Domain.Clients.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Persistence.EntityConfigurations;

public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(a => a.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.MiddleName)
            .HasMaxLength(100);

        builder.Property(a => a.Auth0Id)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(x => x.ClientsCards)
            .WithOne(cc => cc.Client)
            .HasForeignKey(x => x.ClientId);
    }
}
