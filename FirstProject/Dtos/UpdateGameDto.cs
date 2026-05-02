using System.ComponentModel.DataAnnotations;

namespace FirstProject.Dtos;

public record UpdateGameDto (
    [StringLength(15)] string Name,

    [Range(0, 10000)] decimal Cost,

    int GenreId
);
