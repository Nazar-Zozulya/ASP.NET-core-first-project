using FirstProject.Data;
using FirstProject.Dtos;
using FirstProject.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

builder.SeedGenresDB();

var app = builder.Build();




app.MapGamesEndpoints();

app.MapGenresEndpoints();

app.MigrateDb();

app.Run();

