using System.Text.Json.Serialization;

namespace NextStop.Core.SlTools;

public class Departure {
    [JsonPropertyName("direction")] public string? Direction { get; set; }
    [JsonPropertyName("direction_code")] public int DirectionCode { get; set; }
    [JsonPropertyName("via")] public string? Via { get; set; }
    [JsonPropertyName("destination")] public string? Destination { get; set; }
    [JsonPropertyName("State")] public string? State { get; set; }
    [JsonPropertyName("scheduled")] public DateTime? Scheduled { get; set; }
    [JsonPropertyName("expected")] public DateTime? Expected { get; set; }
    [JsonPropertyName("display")] public string? Display { get; set; }

    [JsonPropertyName("line")] public Line Line { get; set; } = new();

}