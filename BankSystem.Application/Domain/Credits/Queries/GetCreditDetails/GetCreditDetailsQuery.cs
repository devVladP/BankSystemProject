using MediatR;

namespace BankSystem.Application.Domain.Credits.Queries.GetCreditDetails;

public record GetCreditDetailsQuery(Guid Id) : IRequest<CreditDetailsDto>;
