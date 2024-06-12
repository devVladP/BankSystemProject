using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Domain.Cards.Commands.CreateCard;

public record CreateCardCommand(string Number, DateOnly IssueDate, DateOnly ExpireDate, decimal Balance, string PaymentSystem) : IRequest<Guid>;

