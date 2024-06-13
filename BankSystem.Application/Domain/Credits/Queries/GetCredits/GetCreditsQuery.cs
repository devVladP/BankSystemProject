using MediatR;
using PagesResponses;

namespace BankSystem.Application.Domain.Credits.Queries.GetCredits;

public record GetCreditsQuery(int Page, int PageSize) : IRequest<PageResponse<CreditDto[]>>;