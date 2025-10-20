namespace PickyBot.Models;

// <summary>
/// Defines the full set of parameters required to execute a complex code review.
/// </summary>
public record CodeReviewRequest
{
    /// <summary>
    /// A list of code snippets or file contents that need to be reviewed.
    /// </summary>
    public required List<string> CodeToReview { get; init; }

    /// <summary>
    /// The programming language of the code being reviewed (e.g., "C#", "Python", "TypeScript").
    /// </summary>
    public required string ProgrammingLanguage { get; init; }

    /// <summary>
    /// The human language for the AI's review and summary (e.g., "English", "French", "Japanese").
    /// </summary>
    public required string ResponseLanguage { get; init; }

    /// <summary>
    /// The persona or style the AI should adopt for the review (e.g., "sarcastic", "helpful mentor", "strict").
    /// </summary>
    public required string Flavor { get; init; }

    /// <summary>
    /// The desired output format for any unstructured text (like the summary) within the response 
    /// (e.g., "json", "markdown", though JSON is typically enforced for the structured part).
    /// </summary>
    public string ResponseFormat { get; init; } = "json";

    /// <summary>
    /// A flag indicating if the model should attempt to provide a fully rewritten/corrected version of the code
    /// in addition to the issues list.
    /// </summary>
    public bool RewriteCodeFlag { get; init; } = false;
}