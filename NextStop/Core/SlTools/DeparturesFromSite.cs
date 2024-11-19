using System.Text.Json.Serialization;

namespace NextStop.Core.SlTools;

public class DeparturesFromSite {
    [JsonPropertyName("departures")] public List<Departure> Departures { get; set; } = [];
}