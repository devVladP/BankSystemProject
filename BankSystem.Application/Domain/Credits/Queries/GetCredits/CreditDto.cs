using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Domain.Credits.Queries.GetCredits;

public record CreditDto
{
    [Required]
    public Guid Id { get; init; }

    [Required]
    public decimal InitialSum { get; init; }

    [Required]
    public Guid CardId { get; init; }
}
