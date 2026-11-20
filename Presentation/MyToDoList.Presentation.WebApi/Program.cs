using MyToDoList.Application.Abstractions;
using MyToDoList.Application.Business;
using MyToDoList.Infrastructure.EFC;
using MyToDoList.Presentation.WebApi;
using MyToDoList.Presentation.WebApi.Security;

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

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContext, HttpUserContext>();

builder.Services.AddControllers();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ExceptionHandler>();

builder.Services.AddOpenApi(options =>
    options.AddOperationTransformer<UserHeaderOperationTransformer>());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "MyToDoList API v1");
        options.RoutePrefix = "swagger";
    });

    // No migrations exist yet: create the schema on startup for local development.
    using var scope = app.Services.CreateScope();
    scope.ServiceProvider.GetRequiredService<ToDoDbContext>().Database.EnsureCreated();
}

app.UseExceptionHandler();
app.UseStatusCodePages();

app.MapControllers();

app.Run();
