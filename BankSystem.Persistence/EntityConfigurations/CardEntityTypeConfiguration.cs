using BankSystem.Core.Domain.Cards.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Persistence.EntityConfigurations;

internal class CardEntityTypeConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.ClientsCards)
            .WithOne(x => x.Card)
            .HasForeignKey(x => x.CardId);

        builder.Property(x => x.Number)
            .IsRequired();
    }
}
