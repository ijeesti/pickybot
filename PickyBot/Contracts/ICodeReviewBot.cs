
namespace PickyBot.Contracts;

public interface ICodeReviewBase 
{
    Task RunReviewAsync(string codeToReview, string responseLanguage = "Estonian");
}

public interface ICodeReviewBot : ICodeReviewBase
{
    Task RunReviewStreamAsync(string codeToReview, string responseLanguage = "Estonian");
}


