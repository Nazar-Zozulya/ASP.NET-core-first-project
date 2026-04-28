using FirstProject.Data;
using FirstProject.Dtos;
using FirstProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace FirstProject.Endpoints;

public static class GamesEndpoints
{

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        // GET /games 
        group.MapGet("/", async (GameStoreContext DBContext) =>
        {
            return await DBContext.Games
                .Include(game => game.Genre)
                .Select(game => new GameDto(
                    game.Id,
                    game.Name,
                    game.Cost,
                    game.Genre!.Name
                ))
                    .AsNoTracking()
                    .ToListAsync(); 
        }).WithName("GetAllGames");

        const string GetGameByid = "GetGameById";

        // GET /games/{id}
        group.MapGet("/{id}", async (int id, GameStoreContext DBContext) =>
        {
            var game = await DBContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(new GameSerializeDto(
                game.Id,
                game.Name,
                game.Cost,
                game.GenreId
                // DBContext.Genres.Find(game.GenreId)!.Name
            ));
        }).WithName(GetGameByid);

        // POST /games
        group.MapPost("/", async (CreateGameDto game, GameStoreContext DBContext) => {
            
            Game NewGame = new ()
            {
                Name = game.Name,
                Cost = game.Cost,
                GenreId = game.GenreId  
            };

            DBContext.Games.Add(NewGame);
            await DBContext.SaveChangesAsync();

            GameSerializeDto NewGameDto = new (
                    NewGame.Id,
                    NewGame.Name,
                    NewGame.Cost,
                    NewGame.GenreId
            );


            return Results.Created();
            // return Results.CreatedAtRoute(GetGameByid, new {id = NewGameDto.Id}, NewGameDto);
        });

        // PUT /games/{id}
        group.MapPut("/{id}", async (int id, UpdateGameDto updateGameData, GameStoreContext DBContext) =>
        {
            var game = await DBContext.Games.FindAsync(id);

            if (game is null) return Results.NotFound();

            game.Name = updateGameData.Name;
            game.Cost = updateGameData.Cost;
            game.GenreId = 2;
            await DBContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE /games/{id}
        group.MapDelete("/{id}", async (int id, GameStoreContext DBContext) =>
        {
            var game = await DBContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();
            
            return Results.NoContent();
        });
    }
}
