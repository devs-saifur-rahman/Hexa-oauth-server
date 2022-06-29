using AutoMapper;
using Hexa.Api.DTOs;
using Hexa.Api.Repositories;
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


builder.Services.AddScoped<IAuthorizationRepo, AuthorizationRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/oauth/v2/authorize", async (IAuthorizationRepo repo, IMapper mapper, AuthRequest req) =>
{

    var resp = await repo.GetAuthorizationCode(req);

    return Results.Ok(mapper.Map<ApiResponse>(resp));

    //ApiResponse resp = new ApiResponse{
    //    success = false,
    //    message="Not Implemented"
    //};




    //    AuthorizationRepository repo = new AuthorizationRepository();

  //  return resp;
});

app.MapPost("/oauth/v2/access_token", (TokenRequest req) =>
{
    ApiResponse resp = new ApiResponse
    {
        success = false,
        message = "Not Implemented"
    };
    return resp;
});


app.Run();
