using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankSystem.Persistence;

public static class BankPersistenceRegistration
{
    private const string _connectionStringName = "Bank";

    public static void AddBankPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(_connectionStringName) ??
            throw new AggregateException($"Connection string: {_connectionStringName} is not found");

        services.AddDbContext<BankSystemDbContext>(options =>
        {
            options.UseSqlServer(
                connectionString,
                sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsHistoryTable(
                        BankSystemDbContext.BankDbMigrationHistory,
                        BankSystemDbContext.BankDbSchema
                        );
                });
        });
    }
}
