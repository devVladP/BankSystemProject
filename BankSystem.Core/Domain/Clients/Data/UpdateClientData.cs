using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Core.Domain.Clients.Data;

public record UpdateClientData(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string? MiddleName);
