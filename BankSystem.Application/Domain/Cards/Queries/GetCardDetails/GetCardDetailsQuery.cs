using MediatR;

namespace BankSystem.Application.Domain.Cards.Queries.GetCardDetails;

public record GetCardDetailsQuery(Guid Id) : IRequest<CardDetailsDto>;

