using System.Text.Json.Serialization;

namespace Esercizio.Data;

class GenderBirthStats
{
    [JsonPropertyName("year")]
    public int Year { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; } = "m";

    [JsonPropertyName("percent")]
    public double Percentage { get; set; }
}