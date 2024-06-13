using BankSystem.Core.Domain.Cards.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Persistence.EntityConfigurations;

internal class ClientCardEntityTypeConfiguration : IEntityTypeConfiguration<ClientsCards>
{
    public void Configure(EntityTypeBuilder<ClientsCards> builder)
    {
        builder.HasKey(cc => new {cc.ClientId, cc.CardId});

        builder.HasOne(cc => cc.Client)
            .WithMany(cl => cl.ClientsCards)
            .HasForeignKey(cc => cc.ClientId);

        builder.HasOne(cc => cc.Card)
            .WithMany(ca => ca.ClientsCards)
            .HasForeignKey(cc => cc.CardId);
    }
}
