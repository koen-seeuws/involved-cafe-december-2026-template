using Microsoft.AspNetCore.Components.Server.Circuits;
using MudBlazor.Services;
using MyToDoList.Application.Abstractions;
using MyToDoList.Application.Business;
using MyToDoList.Infrastructure.EFC;
using MyToDoList.Presentation.BlazorServer.Components;
using MyToDoList.Presentation.BlazorServer.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddToDoApplicationServices();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSqlLitePersistenceServices(builder.Configuration);
}
else
{
    builder.Services.AddSqlServerPersistenceServices(builder.Configuration);
}

// Who is signed in, per circuit (browser tab).
builder.Services.AddScoped<CurrentUser>();

// Lets the use-case scopes created by the dispatcher reach the circuit's CurrentUser.
builder.Services.AddSingleton<CircuitServicesAccessor>();
builder.Services.AddScoped<CircuitHandler, CircuitServicesAccessorHandler>();
builder.Services.AddScoped<IUserContext, CircuitUserContext>();

builder.Services.AddMudServices();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // No migrations exist yet: create the schema on startup for local development.
    using var scope = app.Services.CreateScope();
    scope.ServiceProvider.GetRequiredService<ToDoDbContext>().Database.EnsureCreated();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
