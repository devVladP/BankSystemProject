using BankSystem.Core.Common;
using BankSystem.Core.Domain.Cards.Models;
using BankSystem.Core.Domain.Clients.Data;
using BankSystem.Core.Domain.Clients.Validators;

namespace BankSystem.Core.Domain.Clients.Models;

public class Client : Entity
{
    private readonly List<ClientsCards> _clientsCards = [];

    public Guid Id { get; private set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? MiddleName { get; set; }

    public string Email { get; set; }

    public string Auth0Id { get; set; }

    public IReadOnlyCollection<ClientsCards> ClientsCards => _clientsCards;

    public static Client Create(CreateClientData data)
    {
        Validate(new CreateClientValidator(), data);

        return new Client
        {
            Id = Guid.NewGuid(),
            FirstName = data.FirstName,
            LastName = data.LastName,
            MiddleName = data.MiddleName,
            Email = data.Email,
            Auth0Id = data.Auth0Id,
        };
    }

    public void Update(UpdateClientData data)
    {
        Validate(new UpdateClientValidator(), data);

        FirstName = data.FirstName;
        LastName = data.LastName;
        MiddleName = data.MiddleName;   
        Email = data.Email;
    }
}
