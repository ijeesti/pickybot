using PickyBot.Contracts;
using PickyBot.Models;

namespace PickyBot.Bots;

public class SmartReviewBot : ISmartReviewBot
{
    public Task<CodeReviewReport> RunReviewAsync(CodeReviewRequest codeReviewRequest)
        => throw new NotImplementedException();

    public Task RunReviewAsync(string codeToReview, string responseLanguage = "Estonian")
    {
        throw new NotImplementedException();
    }

    public Task RunReviewStreamAsync(string codeToReview, string responseLanguage = "Estonian")
    {
        throw new NotImplementedException();
    }
}