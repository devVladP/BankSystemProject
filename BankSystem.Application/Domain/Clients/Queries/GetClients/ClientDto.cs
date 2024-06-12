using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.Application.Domain.Clients.Queries.GetClients;

public class ClientDto
{
    [Required]
    public Guid Id { get; init; }

    [Required]
    public string FirstName { get; init; }

    [Required]
    public string LastName { get; init; }

    [Required]
    public string Email { get; init; }

    public string? MiddleName { get; init; }
}
