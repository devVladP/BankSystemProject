using BankSystem.Core.Domain.Cards.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Core.Domain.Cards.Data;

public record CreateCardData(
    string Number,
    DateOnly IssueDate,
    DateOnly ExpirationDate,
    decimal Balance,
    string PaymentSystem);
