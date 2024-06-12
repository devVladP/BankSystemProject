using System.ComponentModel.DataAnnotations;

namespace BankSystem.Application.Domain.Cards.Queries.GetCardDetails;

public record CardDetailsDto
{
    [Required]
    public Guid Id { get; init; }

    [Required]
    public string Number { get; init; }

    [Required]
    public string CVV2 { get; init; }

    [Required]
    public DateOnly IssueDate { get; init; }

    [Required]
    public DateOnly ExpirationDate { get; init; }

    public decimal Balance { get; init; }

    public decimal? Credit { get; init; }

    public ClientInformationDto[] Owners { get; init; }
}
