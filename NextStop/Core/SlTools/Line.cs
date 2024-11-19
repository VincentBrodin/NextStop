using System.Text.Json.Serialization;

namespace NextStop.Core.SlTools;

public class Line {
    [JsonPropertyName("id")] public long Id { get; set; }
    [JsonPropertyName("designation")] public string Designation { get; set; } = "Missing";
    [JsonPropertyName("transport_mode")] public string TransportMode { get; set; } = "Missing";
    [JsonPropertyName("group_of_lines")] public string? GroupOfLines { get; set; }
}