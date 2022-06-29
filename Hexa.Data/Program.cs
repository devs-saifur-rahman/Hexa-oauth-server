using Hexa.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAuthorizationRepo, AuthorizationRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.Run();
