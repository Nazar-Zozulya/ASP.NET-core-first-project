using System;
using FirstProject.Data;
using FirstProject.Dtos;
using FirstProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Endpoints;

public static class GenresEndpoints
{
    public static void MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/genres");

        // GET /genres
        const string GetAllGenres = "GetAllGenres";
        group.MapGet("/", async (GameStoreContext DBContext) =>
        {
            return await DBContext.Genres
                .Select(genre => new GenreDto(
                    genre.Id,
                    genre.Name
                ))
                    .AsNoTracking()
                    .ToListAsync(); 
        }).WithName(GetAllGenres);
        

        // GET /genres/{id}
        const string GetGenreById = "GetGenreById";
        group.MapGet("/{id}", async (int id, GameStoreContext DBContext) =>
        {
            var genre = await DBContext.Genres.FindAsync(id);

            return genre is null ? Results.NotFound() : Results.Ok(new GenreSerializeDto(
                genre.Id,
                genre.Name
            ));
        }).WithName(GetGenreById);


        // POST /genres
        const string CreateGenre = "CreateGenre";
        group.MapPost("/", async (CreateGenreDto genre, GameStoreContext DBContext) => {
            
            Genre NewGenre = new ()
            {
                Name = genre.Name
            };

            DBContext.Genres.Add(NewGenre);
            await DBContext.SaveChangesAsync();

            GenreSerializeDto NewGenreDto = new (
                    NewGenre.Id,
                    NewGenre.Name
            );


            return Results.Created();
        }).WithName(CreateGenre);

        // PUT /genres/{id}
        const string UpdateGenres = "UpdateGenres";
        group.MapPut("/{id}", async (int id, UpdateGenreDto updateGameData, GameStoreContext DBContext) =>
        {
            var genre = await DBContext.Genres.FindAsync(id);

            if (genre is null) return Results.NotFound();

            genre.Name = updateGameData.Name;
            await DBContext.SaveChangesAsync();

            return Results.NoContent();
        }).WithName(UpdateGenres);

        // DELETE /genres/{id}
        const string DeleteGenre = "DeleteGenre";
        group.MapDelete("/{id}", async (int id, GameStoreContext DBContext) =>
        {
            var genre = await DBContext.Genres.Where(genre => genre.Id == id).ExecuteDeleteAsync();
            
            return Results.NoContent();
        }).WithName(DeleteGenre);
    }
}
