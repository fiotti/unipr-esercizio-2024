using System.Text.Json.Serialization;

namespace Esercizio.Data;

[JsonSerializable(typeof(BirthStats))]
class BirthStats
{
    [JsonPropertyName("years")]
    public List<int> Years { get; set; } = new();

    [JsonPropertyName("0")]
    public List<GenderBirthStats> Male { get; set; } = new();

    [JsonPropertyName("1")]
    public List<GenderBirthStats> Female { get; set; } = new();
}
