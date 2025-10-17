using Asp.Versioning.ApiExplorer;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using NorthWind.API.Endpoints;
using NorthWind.API.Extensions;
using NorthWind.API.Middlewares;
using NorthWind.API.Migration;
using NorthWind.API.OpenApi;
using NorthWind.API.Security;
using NorthWind.API.Version;
using NorthWind.Application;
using NorthWind.Application.Orders.Validators;
using NorthWind.Infrastructure;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddEndpoints();
builder.Services.AddVersions();
builder.Services.AddValidatorsFromAssemblyContaining<PaginatedQueryRequestValidator>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddApiCors();

//security
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = "http://localhost:8080/realms/northwind"; // Keycloak realm
    options.Audience = "northwind-api"; // must match clientId of API client
    options.RequireHttpsMetadata = !builder.Environment.IsDevelopment(); // dev only

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        //RoleClaimType = "realm_access.roles" // Keycloak puts roles inside "realm_access.roles"
    };

    options.Events = new JwtBearerEvents
    {
        //map the realm roles
        OnTokenValidated = ctx =>
        {
            List<AuthenticationToken> tokens = ctx.Properties!.GetTokens().ToList();
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)ctx.Principal!.Identity!;

            var realm_access = claimsIdentity.FindFirst((claim) => claim.Type == "realm_access")?.Value;

            JObject obj = JObject.Parse(realm_access);
            var roleAccess = obj.GetValue("roles");
            foreach (JToken role in roleAccess!)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.ToString()));
            }

            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(AuthorizationPolicyNames.ProductView, policy =>
        policy.RequireRole(RoleNames.ProductManager, RoleNames.SalesRepresentative, RoleNames.WarehouseClerk));

    options.AddPolicy(AuthorizationPolicyNames.ProductCreate, policy =>
        policy.RequireRole(RoleNames.ProductManager));

    options.AddPolicy(AuthorizationPolicyNames.CustomerView, policy =>
        policy.RequireRole(RoleNames.ProductManager, RoleNames.SalesRepresentative, RoleNames.WarehouseClerk));

    options.AddPolicy(AuthorizationPolicyNames.OrderView, policy =>
        policy.RequireRole(RoleNames.ProductManager, RoleNames.SalesRepresentative, RoleNames.WarehouseClerk));

    options.AddPolicy(AuthorizationPolicyNames.OrderCreate, policy =>
        policy.RequireRole(RoleNames.SalesRepresentative));

    options.AddPolicy(AuthorizationPolicyNames.OrderUpdateStatus, policy =>
        policy.RequireRole(RoleNames.WarehouseClerk));

    options.AddPolicy(AuthorizationPolicyNames.OrderDelete, policy =>
        policy.RequireRole(RoleNames.ProductManager));

    options.AddPolicy(AuthorizationPolicyNames.OrderUpdate, policy =>
        policy.RequireRole(RoleNames.ProductManager, RoleNames.SalesRepresentative));

    options.AddPolicy(AuthorizationPolicyNames.EmployeeView, policy =>
        policy.RequireRole(RoleNames.SalesRepresentative));
});




var app = builder.Build();
app.MapDefaultEndpoints();
app.MapEndpoints(app.CreateRouteGorupBuilder());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        IReadOnlyList<ApiVersionDescription> descriptions = app.DescribeApiVersions();
        // Create a Swagger doc per version
        foreach (var description in descriptions)
        {
            string url = $"/swagger/{description.GroupName}/swagger.json";
            string name = description.GroupName.ToUpperInvariant();

            options.SwaggerEndpoint(url, name);
        }
    });

    app.ApplyMigrations();
}


app.UseExceptionHandler();
app.UseApiCors();


var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
