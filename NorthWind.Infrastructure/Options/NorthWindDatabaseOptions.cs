using System.ComponentModel.DataAnnotations;

namespace NorthWind.Infrastructure.Options;

public class NorthWindDatabaseOptions
{
    public const string ConnectionStringName = "NorthWind";

    [Required]
    [MinLength(1)]
    public string ConnectionString { get; set; } = string.Empty;
}
