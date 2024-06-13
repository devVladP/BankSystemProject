using BankSystem.Application.Domain.Clients.Queries.GetClientDetails;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Domain.Credits.Queries.GetCreditDetails;

public record CreditDetailsDto
{
    [Required]
    public Guid Id { get; init; }

    [Required]
    public decimal InitialSum { get; init; }

    [Required]
    public DateOnly CreditIssueDate { get; init; }

    [Required]
    public byte PercentPerMonth { get; init; }

    [Required]
    public CardInformationDto Card { get; init; }

    public decimal FinalSum { get; init; }
}
