using PickyBot.Models;

namespace PickyBot.Contracts;

public interface ISmartReviewBot : ICodeReviewBase
{
    Task<CodeReviewReport> RunReviewAsync(CodeReviewRequest codeReviewRequest);
    Task RunReviewStreamAsync(string codeToReview, string responseLanguage = "Estonian");
}
