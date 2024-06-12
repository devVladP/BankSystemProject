using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Domain.Cards.Commands.UpdateCard;

public record UpdateCardCommand(Guid Id, 
    string Number, 
    string CVV2, 
    DateOnly ExpirationDate, 
    DateOnly IssueDate, 
    decimal Balance,
    decimal Credit) : IRequest;
