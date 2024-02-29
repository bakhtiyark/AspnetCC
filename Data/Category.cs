using System.ComponentModel.DataAnnotations;

namespace CarvedRock.Data;

public class Category
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = null!;
}