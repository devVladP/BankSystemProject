using System.ComponentModel.DataAnnotations;

namespace BankSystem.Application.Domain.Clients.Queries.GetClientDetails;

public record ClientDetailsDto
{
    [Required]
    public Guid Id { get; init; }

    [Required]
    public string FirstName { get; init; }

    [Required]
    public string LastName { get; init; }

    [Required]
    public string Email { get; init; }

    public string? MiddleName { get; init; }

    public decimal? TotalCredit { get; init; } = 0;

    public decimal? TotalBalance { get; init; } = 0;

    public CardInformationDto[]? Cards { get; init; }
}
