using BankSystem.Core.Domain.Credits.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Persistence.EntityConfigurations;

internal class CreditEntityTypeConfiguration : IEntityTypeConfiguration<Credit>
{
    public void Configure(EntityTypeBuilder<Credit> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Card)
            .WithMany(x => x.Credits)
            .HasForeignKey(x => x.CardId);

        builder.Property(x => x.PercentPerMonth)
            .HasColumnType("tinyint");

        builder.Property(x => x.InitialSum)
            .HasColumnType("smallmoney");
    }
}
