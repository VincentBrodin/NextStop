using System.Text.Json.Serialization;

namespace NextStop.Core.SlTools;

public class Site {
    [JsonPropertyName("id")] public long Id { get; set; }
    [JsonPropertyName("gid")] public long Gid { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; } = "Name";
    [JsonPropertyName("note")] public string? Note { get; set; }
    [JsonPropertyName("lat")] public double? Lat { get; set; }
    [JsonPropertyName("lon")] public double? Lon { get; set; }
    // [JsonPropertyName("stop_areas")] public List<int>? StopAreas { get; set; }
    [JsonPropertyName("abbreviation")] public string? Abbreviation { get; set; }
    [JsonPropertyName("alias")] public List<string> Alias { get; set; } = [];
}