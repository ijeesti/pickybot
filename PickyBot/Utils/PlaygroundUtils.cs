using Microsoft.SemanticKernel;
using PickyBot.Models;

namespace PickyBot.Utils;

internal class PlaygroundUtils
{
    internal static Kernel GetKernelInstance()
    {
        var builder = Kernel.CreateBuilder();

        builder.AddAzureOpenAIChatCompletion(
            deploymentName: "",     // your model deployment name
            endpoint: "https://xx.openai.azure.com",
            apiKey: "");
        return builder.Build();
    }

    internal static bool ProcessResult(string jsonString, CodeReviewReport? reviewResult)
    {
        // --- Initial Color Setup (Default Console Color) ---
        ConsoleColor defaultColor = Console.ForegroundColor;

        // --- Error Handling ---
        if (reviewResult == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n*******************************************************");
            Console.WriteLine(" FATAL ERROR: Deserialization failed.");
            Console.WriteLine(" The LLM did not return the expected analysis structure.");
            Console.WriteLine("*******************************************************");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\nRAW JSON OUTPUT RECEIVED:\n{jsonString}");
            Console.ForegroundColor = defaultColor;
            return false;
        }

        PrintStatus(reviewResult);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(" |");
        PrintIssues(reviewResult, defaultColor);
        PrintRecommendations(reviewResult, defaultColor);
        PrintCodeQuality(reviewResult, defaultColor);
        PrintSuggestedCode(reviewResult, defaultColor);
        Console.ForegroundColor = ConsoleColor.Yellow;
        PrintAnimatedHeader("| 💥 REVIEW SUMMARY|", ConsoleColor.DarkYellow, delay: 10);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(reviewResult.Summary ?? "N/A");

        return true;
    }

    private static void PrintStatus(CodeReviewReport reviewResult)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("| CODE REVIEW ANALYSIS REPORT | STATUS: ");
        var success = reviewResult.Issues.Count == 0;
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Review Status: ");
        Console.ForegroundColor = success ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine(success ? "[SUCCESS]" : "[FAILURE]");
        Console.ResetColor();
    }

    private static void PrintIssues(CodeReviewReport reviewResult, ConsoleColor defaultColor)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;
        PrintAnimatedHeader("| 💥 ISSUES FOUND |", ConsoleColor.Red, delay: 5);
        if (reviewResult.Issues?.Count > 0)
        {
            foreach (var issue in reviewResult.Issues)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"\n[ ISSUE #{issue.Id}: {issue.Type} ]");

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"  (Location: {issue.Location})");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"  -> Description: {issue.Description}");
            }

            return;
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("No critical issues reported.");

    }

    private static void PrintRecommendations(CodeReviewReport reviewResult, ConsoleColor defaultColor)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        PrintAnimatedHeader("| RECOMMENDATIONS & CODE IMPROVEMENTS |", ConsoleColor.Green, delay: 5);
        int recId = 1;
        foreach (var recommendation in reviewResult.Recommendations ?? [])
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[{recId++:D2}] {recommendation.RecommendationText}");
            Console.ResetColor();
        }
    }

    private static void PrintSuggestedCode(CodeReviewReport reviewResult, ConsoleColor defaultColor)
    {
        Console.WriteLine();
        PrintAnimatedText("███ DEPLOYING REWRITTEN CODE ███", ConsoleColor.DarkYellow, delay: 5);
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("┌─────────────────────────────────────────────────────────────────────────┐");
        Console.WriteLine($"{reviewResult?.RewrittenCode}");
        Console.WriteLine("└─────────────────────────────────────────────────────────────────────────┘");
        Console.ForegroundColor = defaultColor;
    }

    private static void PrintCodeQuality(CodeReviewReport reviewResult, ConsoleColor defaultColor)
    {
        if (reviewResult.CodeQuality is null)
        {
            return;
        }
        // Helper function for color-coding quality ratings
        static ConsoleColor GetQualityColor(string rating)
        {
            return rating?.ToLower(System.Globalization.CultureInfo.CurrentCulture).Contains("low", StringComparison.CurrentCultureIgnoreCase) == true ? ConsoleColor.Red :
                   rating?.ToLower().Contains("moderate", StringComparison.CurrentCultureIgnoreCase) == true ? ConsoleColor.Yellow :
                   ConsoleColor.Green;
        }
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        PrintAnimatedText(" 📊 CODE QUALITY METRICS", ConsoleColor.DarkYellow, delay: 5);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("  - Readability:            ");
        Console.ForegroundColor = GetQualityColor(reviewResult.CodeQuality.Readability);
        Console.WriteLine(reviewResult.CodeQuality.Readability);

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("  - Maintainability:        ");
        Console.ForegroundColor = GetQualityColor(reviewResult.CodeQuality.Maintainability);
        Console.WriteLine(reviewResult.CodeQuality.Maintainability);

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("  - Robustness:             ");
        Console.ForegroundColor = GetQualityColor(reviewResult.CodeQuality.Robustness);
        Console.WriteLine(reviewResult.CodeQuality.Robustness);

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("  - Separation of Concerns: ");
        Console.ForegroundColor = GetQualityColor(reviewResult.CodeQuality.SeparationOfConcerns);
        Console.WriteLine(reviewResult.CodeQuality.SeparationOfConcerns);

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("  - Performance:            ");
        Console.ForegroundColor = GetQualityColor(reviewResult.CodeQuality.Performance);
        Console.WriteLine(reviewResult.CodeQuality.Performance);

        Console.ForegroundColor = defaultColor;
    }

    public static void PrintAnimatedHeader(string text, ConsoleColor color, int delay = 5)
    {
        Console.WriteLine();
        PrintAnimatedText("┌" + new string('─', text.Length + 4) + "┐", ConsoleColor.DarkGray, delay: 0);
        Console.WriteLine();
        Console.Write("│  ");
        PrintAnimatedText(text, color, delay);
        Console.ResetColor();
        Console.Write("  │");
        Console.WriteLine();
        PrintAnimatedText("└" + new string('─', text.Length + 4) + "┘", ConsoleColor.DarkGray, delay: 0);
        Console.WriteLine();
    }

    public static void PrintAnimatedText(string text, ConsoleColor color, int delay = 1)
    {
        Console.ForegroundColor = color;
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delay);
        }
        Console.ResetColor();
    }
}