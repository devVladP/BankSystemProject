namespace BankSystem.Api.Domain.Clients.Requests
{
    public record UpdateClientRequest(string FirstName, string LastName, string Email, string? MiddleName = "");
}
