using System.ComponentModel.DataAnnotations;

namespace CommanderGQL.Models;

public class Platform
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = null!;

    public string? LicenseKey { get; set; }

    public ICollection<Command> Commands { get; set; } = new List<Command>();
}