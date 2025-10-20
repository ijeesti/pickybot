using PickyBot.Utils;
using System.Text.Json.Serialization;
using static PickyBot.Utils.JsonUtils;

namespace PickyBot.Models;

/// <summary>
/// The top-level record containing the core analysis data of the code review.
/// This structure now directly matches the flat JSON output from the LLM.
/// </summary>
/// 

public record CodeReviewReport(
    /// <summary>
    /// Indicates whether the code passed the review or if issues were found.
    /// </summary>
    bool Success,
    /// <summary>
    /// A high-level, human-readable summary of the review.
    /// </summary>
    string Summary,
    /// <summary>
    /// A list of all specific code issues found during the review.
    /// </summary>
    List<Issue> Issues,

    ///// <summary>
    ///// A general list of actionable recommendations for the developer.
    ///// </summary>
    [property: JsonPropertyName("recommendations")]
    [property: JsonConverter(typeof(StringOrObjectListConverter<Recommendation>))]
    List<Recommendation> Recommendations,
    /// <summary>
    /// The overall subjective quality assessment.
    /// </summary>
    CodeQualityMetrics CodeQuality,
    /// <summary>
    /// The corrected and improved version of the entire code, provided only if the RewriteCodeFlag was set in the request.
    /// </summary>
    /// 
    [property: JsonPropertyName("rewritten_code")]
    string RewrittenCode
);