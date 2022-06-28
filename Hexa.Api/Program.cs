var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.MapGet("/oauth/v2/access_token", () =>
{
    var resp = "not implemented";
    return resp;
});


app.Run();
