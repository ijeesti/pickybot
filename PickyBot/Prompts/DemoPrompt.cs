namespace PickyBot.Prompts;

internal static class DemoPrompt
{
    internal static string CustomRules = @"Your rules";

   public const string SystemInstruction =
        """
        Define Role,Goal and Descriptions
        You MUST strictly enforce these rules:
        --- CUSTOM CODING STANDARDS ---
        {custom_rules}
        --- END OF STANDARDS ---
        Other Details...................      
        """;

    public static string GetUserPrompt(string codeToReview, string language) => "Your Prompt";
      
}

