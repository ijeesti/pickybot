using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using PickyBot.Contracts;
using PickyBot.Models;
using PickyBot.Prompts;
using PickyBot.Utils;
using System.Text;
using System.Text.Json;

namespace PickyBot.Bots;

public class SimpleReviewBot : ICodeReviewBot
{
    public async Task RunReviewAsync(string codeToReview, string responseLanguage = "Estonian")
    {
        var kernel = PlaygroundUtils.GetKernelInstance();

        var openAISettings = new OpenAIPromptExecutionSettings
        {
            Temperature = 0.1,
            ResponseFormat = "json_object"
        };
        var arguments = new KernelArguments(openAISettings)
        {
            { "system_instruction", DemoPrompt.SystemInstruction }
            //add more ...
        };

        // The user message contains the C# code itself
        var userPrompt = DemoPrompt.GetUserPrompt(codeToReview, responseLanguage);

        Console.OutputEncoding = Encoding.UTF8;
        PlaygroundUtils.PrintAnimatedText("--- AWAITING AI CODE REVIEW ---", ConsoleColor.DarkCyan, 1);
       
        try
        {
            var result = await kernel.InvokePromptAsync(userPrompt, arguments);
            var rawResultString = result.GetValue<string>()!;
            var jsonString = JsonUtils.ExtractJsonFromMarkdown(rawResultString);
            var wrappedReview = JsonSerializer.Deserialize<CodeReviewReport>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            PlaygroundUtils.ProcessResult(jsonString, wrappedReview);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during the review: {ex.Message}");
            Console.WriteLine("Check your API key, endpoint, model deployment, and ensure the LLM returned a valid JSON structure.");
        }
    }

    public async Task RunReviewStreamAsync(string codeToReview, string responseLanguage = "Estonian")
    {
        var kernel = PlaygroundUtils.GetKernelInstance();

        var openAISettings = new OpenAIPromptExecutionSettings
        {
            Temperature = 0.1,
            ResponseFormat = "json_object"
        };
        var arguments = new KernelArguments(openAISettings)
        {
            { "system_instruction", DemoPrompt.SystemInstruction }
        };

        // The user message contains the C# code itself
        var userPrompt = DemoPrompt.GetUserPrompt(codeToReview, responseLanguage);

        Console.WriteLine("--- STARTING AI CODE REVIEW (Streaming) ---");
        Console.Write("AI Reviewer: ");

        var rawResultBuilder = new StringBuilder();
        try
        {
            // Invoke the LLM asynchronously with streaming.
            var stream = kernel.InvokePromptStreamingAsync(userPrompt, arguments);
            await foreach (var chunk in stream)
            {
                var text = chunk.ToString();
                Console.Write(text);
                rawResultBuilder.Append(text);
            }

            Console.WriteLine(); // Add a final newline after the stream is complete

            var rawResultString = rawResultBuilder.ToString();
            var jsonString = JsonUtils.ExtractJsonFromMarkdown(rawResultString);
            var wrappedReview = JsonSerializer.Deserialize<CodeReviewReport>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            PlaygroundUtils.ProcessResult(jsonString, wrappedReview);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nAn error occurred during the review: {ex.Message}");
            Console.WriteLine("Check your API key, endpoint, model deployment, and ensure the LLM returned a valid JSON structure.");
        }
    }
}