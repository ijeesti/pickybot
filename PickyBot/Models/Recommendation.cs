
using System.Text.Json.Serialization;

namespace PickyBot.Models;

// Models used for deserializing the JSON analysis report.
// Converted from 'record' to 'class' for explicit constructor control.
/// <summary>
/// Represents a suggested action to resolve a specific issue.
/// </summary>
public class Recommendation
{
    // Properties must have public setters for System.Text.Json deserialization
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("recommendation")]
    public string RecommendationText { get; set; } = string.Empty;

    // Parameterless constructor for standard JSON deserialization
    public Recommendation() { }

    // Primary constructor for custom converter (StringOrObjectListConverter)
    public Recommendation(int id, string recommendationText)
    {
        Id = id;
        RecommendationText = recommendationText;
    }
}