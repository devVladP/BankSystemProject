using MediatR;
using PagesResponses;

namespace BankSystem.Application.Domain.Cards.Queries.GetCards;

public record GetCardsQuery(int page, int pageSize) : IRequest<PageResponse<CardDto[]>>;
