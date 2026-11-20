using System.ComponentModel.DataAnnotations;

namespace MyToDoList.Presentation.WebApi.Models;

public record UpdateToDoRequest(
    [Required, StringLength(200)] string Title,
    [Required, StringLength(2000)] string Description);
