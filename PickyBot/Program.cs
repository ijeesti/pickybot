using PickyBot.Bots;
using PickyBot.Utils;

var bot = new SimpleReviewBot();

//Stream
//await bot.RunReviewStreamAsync(codeToReview: CodeSamples.CSharpCode, responseLanguage: "English");

await bot.RunReviewAsync(codeToReview: CodeSamples.PythonCode, responseLanguage: "French");
Console.ReadLine();
