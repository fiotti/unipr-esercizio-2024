using System.Text.Json.Serialization;

namespace Esercizio.Data;

class BirthStats
{
    [JsonPropertyName("years")]
    public List<int> Years { get; set; } = [];

    [JsonPropertyName("0")]
    public List<GenderBirthStats> Male { get; set; } = [];

    [JsonPropertyName("1")]
    public List<GenderBirthStats> Female { get; set; } = [];
}
