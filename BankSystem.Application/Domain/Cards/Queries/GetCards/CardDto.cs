using System.ComponentModel.DataAnnotations;

namespace BankSystem.Application.Domain.Cards.Queries.GetCards;

public record CardDto
{
    [Required]
    public Guid Id { get; init; }

    [Required]
    public string Number { get; init; }

    public decimal Balance { get; init; }
}
