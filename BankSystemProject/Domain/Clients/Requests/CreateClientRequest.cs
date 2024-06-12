namespace BankSystem.Api.Domain.Clients.Requests
{
    public record CreateClientRequest(string FirstName, string LastName, string Email ,string? MiddleName = default);
}
