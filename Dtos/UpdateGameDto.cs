using System.ComponentModel.DataAnnotations;

namespace FirstProject.Dtos;

public record UpdateGameDto (
    [Required] [StringLength(15)] string Name,

    [Required] [Range(0, 10000)] decimal Cost,

    [Required] [StringLength(50)] string Genre
);
