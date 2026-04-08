using FirstProject.Dtos;

namespace FirstProject.Endpoints;

public static class GamesEndpoints
{
    private static readonly List<GameDto> games = [
        new (1, "Dota1", 15.15m, "MOBA"),
        new (2, "Dota2", 16.16m, "MOBA"),
        new (3, "Dota3", 17.17m, "MOBA")
    ];

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        // GET /games 
        group.MapGet("/", () => games).WithName("GetAllGames");

        const string GetGameByid = "GetGameById";

        // GET /game/{id}
        group.MapGet("/{id}", (int id) =>
        {
            return games.Find(game => game.Id == id);
        }).WithName(GetGameByid);

        // POST /games
        group.MapPost("/", (CreateGameDto game) => {
            // if (string.IsNullOrWhiteSpace(game.Name))
            // {
            //     return Results.BadRequest("Name is required");
            // }

            GameDto NewGame = new (
                games.Count + 1, 
                game.Name,
                game.Cost,
                game.Genre
            );

            games.Add(NewGame);

            return Results.CreatedAtRoute(GetGameByid, new {id = NewGame.Id}, NewGame);
        });

        // PUT /games/{id}
        group.MapPut("/{id}", (int id, UpdateGameDto updateGameData) =>
        {
            games[id -1] = new GameDto(
                id,
                updateGameData.Name,
                updateGameData.Cost,
                updateGameData.Genre
            );

            return Results.NoContent();
        });

        // DELETE /games/{id}
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });
    }
}
