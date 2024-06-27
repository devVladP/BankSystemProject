using BankSystem.Core.Common;
using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Core.Domain.Cards.Data;
using BankSystem.Core.Domain.Cards.Validators;
using BankSystem.Core.Domain.Clients.Models;


namespace BankSystem.Core.Domain.Cards.Models;

public class ClientsCards : Entity
{
    private ClientsCards() { }

    public ClientsCards(Guid clientId, Guid cardId)
    {
        ClientId = clientId;
        CardId = cardId;
    }

    public Guid ClientId { get; set; }

    public Client Client { get; set; }

    public Guid CardId { get; set; }

    public Card Card { get; set; }

    public async static Task<ClientsCards> Create(IClientMustExistChecker clientMustExistChecker,
        ICardMustExistChecker cardMustExistChecker,
        CreateClientCardData data,
        CancellationToken cancellationToken = default)
    {
        await ValidateAsync(new CreateClientCardValidator(clientMustExistChecker, cardMustExistChecker), data, cancellationToken);

        return new ClientsCards(data.clientId, data.cardId);
    }
}
