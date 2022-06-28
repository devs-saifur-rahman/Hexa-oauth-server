using Hexa.Data.DB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqlConnection = new SqlConnectionStringBuilder();
sqlConnection.ConnectionString = builder.Configuration.GetConnectionString("Hexa");
sqlConnection.UserID = builder.Configuration["sqlServerUser"];
sqlConnection.Password = builder.Configuration["sqlServerPassword"];

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(sqlConnection.ConnectionString);
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/oauth/v2/authorize", () =>
{
    var resp = "not implemented";

    return resp;
});

app.MapPost("/oauth/v2/access_token", () =>
{
    var resp = "not implemented";
    return resp;
});


app.Run();
