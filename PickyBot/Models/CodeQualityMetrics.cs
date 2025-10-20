namespace PickyBot.Models;

/// <summary>
/// Represents the new 'code_quality' object, providing subjective scores.
/// </summary>
public record CodeQualityMetrics
{
    public string Readability { get; init; } = string.Empty;
    public string Maintainability { get; init; } = string.Empty;
    public string Robustness { get; init; } = string.Empty;
    public string SeparationOfConcerns { get; init; } = string.Empty;
    public string Performance { get; init; } = string.Empty;
}


