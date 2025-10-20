namespace PickyBot.Models;


/// <summary>
/// Represents a single, specific issue identified during the code review.
/// (Kept for detailed, line-by-line feedback).
/// </summary>
public record Issue
{
    public int Id { get; set; }

    /// <summary>
    /// The Type of the issue (e.g., "Performance", "Security", "Style", "Bug").
    /// </summary>
    public string Type { get; init; } = string.Empty;

    /// <summary>
    /// The specific rule or guideline that was violated (e.g., "CA1822", "DoNotUseVar").
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// The approximate line number in the original code where the issue occurs.
    /// </summary>
    public string Location { get; init; } = string.Empty;

    /// <summary>
    /// A detailed explanation of why this issue is a problem.
    /// </summary>
    public string Explanation { get; init; } = string.Empty;
}