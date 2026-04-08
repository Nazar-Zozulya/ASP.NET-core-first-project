using FirstProject.Data;
using FirstProject.Dtos;
using FirstProject.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var conString = builder.Configuration.GetConnectionString("GameStore");

builder.Services.AddSqlite<GameStoreContext>(conString);

var app = builder.Build();




app.MapGamesEndpoints();

app.MigrateDb();

app.Run();

