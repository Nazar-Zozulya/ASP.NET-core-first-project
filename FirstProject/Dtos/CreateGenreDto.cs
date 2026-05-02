using System.ComponentModel.DataAnnotations;

namespace FirstProject.Dtos;

public record CreateGenreDto (
    [Required] string Name
);