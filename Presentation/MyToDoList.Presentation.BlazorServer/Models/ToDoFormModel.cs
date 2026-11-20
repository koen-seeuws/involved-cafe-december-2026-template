using System.ComponentModel.DataAnnotations;

namespace MyToDoList.Presentation.BlazorServer.Models;

/// <summary>Input for adding or editing a to-do (same rules as the Web API requests).</summary>
public sealed class ToDoFormModel
{
    [Required, StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(2000)]
    public string Description { get; set; } = string.Empty;
}
