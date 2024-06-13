using Auth0.AspNetCore.Authentication;
using BankSystem.Api.Auth0;
using BankSystem.Application;
using BankSystem.Infrastructure;
using BankSystem.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BankSystemProject;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("BankEmployeePolicy", policy =>
            {
                policy.Requirements.Add(new HasRoleRequirement("BankEmployee"));
            });

            options.AddPolicy("BankClientPolicy", policy =>
            {
                policy.Requirements.Add(new HasRoleRequirement("BankClient"));
            });

            options.InvokeHandlersAfterFailure = true;
        });

        builder.Services.AddSingleton<IAuthorizationHandler, HasRoleHandler>();
        builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddScoped<IClaimsTransformation, ClaimsTransformation>();

        builder.Services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.ToString().Replace("+", "_"));
            options.CustomOperationIds(apiDescription =>
                apiDescription.TryGetMethodInfo(out var methodInfo)
                    ? methodInfo.Name
                    : null);
            options.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
            var key = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
            var securityRequirement = new OpenApiSecurityRequirement
            {
                {key, Array.Empty<string>()}
            };
            options.AddSecurityRequirement(securityRequirement);
            options.UseAllOfToExtendReferenceSchemas();
        });

        builder.Services.AddAuth0WebAppAuthentication(options =>
        {
            options.Domain = builder.Configuration["Auth0:Domain"];
            options.ClientId = builder.Configuration["Auth0:ClientId"];
            options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
            options.Scope = "openid profile email offline_access audience permissions issuer roles";
        })
        .WithAccessToken(options =>
        {
            options.Audience = builder.Configuration["Auth0:Audience"];
        });


        builder.Services.AddApplicationServices(builder.Configuration);
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddBankPersistence(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(x => x.DisplayOperationId());
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();
        app.Run();
    }
}
