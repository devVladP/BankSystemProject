namespace BankSystem.Application.Domain.Clients.Queries.GetClientDetails;

public record CardInformationDto
{
    public Guid Id { get; set; }

    public string Number { get; set; }

    public decimal Balance { get; set; }
};