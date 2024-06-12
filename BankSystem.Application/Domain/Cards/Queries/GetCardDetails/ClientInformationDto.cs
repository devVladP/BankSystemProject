namespace BankSystem.Application.Domain.Cards.Queries.GetCardDetails;

public record ClientInformationDto
{
    public Guid Id { get; init; }

    public string Name { get; init; }
}
