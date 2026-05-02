using System.ComponentModel.DataAnnotations;

namespace FirstProject.Dtos;

public record UpdateGenreDto (
    [Required] string Name
);