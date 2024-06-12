using BankSystem.Core.Domain.Cards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Core.Domain.Cards.Data;

public record SendMoneyData(Card CardSender, Card CardReceiver, decimal TotalMoney);