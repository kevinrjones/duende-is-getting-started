using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
}).AddTestUsers(new List<TestUser>())
.AddInMemoryClients(new List<Client>())
.AddInMemoryApiResources(new List<ApiResource>())
.AddInMemoryApiScopes(new List<ApiScope>())
.AddInMemoryIdentityResources(new List<IdentityResource>());

var app = builder.Build();
app.UseIdentityServer();

app.MapGet("/", () => "Hello World!");

app.Run();
