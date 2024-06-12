﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using BankSystem.Core.Common;
using BankSystem.Infrastructure.Core.Common;
using BankSystem.Core.Domain.Clients.Common;
using BankSystem.Infrastructure.Core.Domain.Clients;
using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Infrastructure.Core.Domain.Cards;


namespace BankSystem.Infrastructure;

public static class InfrastructureRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // mediatr
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<ICardClientRepository, CardClientRepository>();


        //checker
        services.AddScoped<ICardMustExistChecker, CardMustExistChecker>();
        services.AddScoped<IClientMustExistChecker, ClientMustExistChecker>();
        services.AddScoped<ICardNumberMustBeUniqueChecker, CardNumberMustBeUniqueChecker>();
        // TODO exceptions
    }
}
