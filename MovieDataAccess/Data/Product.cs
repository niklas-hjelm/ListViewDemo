using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MovieDataAccess.Data;

public record Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
}