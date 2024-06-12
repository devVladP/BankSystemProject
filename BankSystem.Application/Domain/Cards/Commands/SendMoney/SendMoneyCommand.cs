using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Domain.Cards.Commands.SendMoney
{
    public record SendMoneyCommand(Guid CardSenderId, Guid CardReceiverId, decimal TotalMoneyToSend) : IRequest;
}
