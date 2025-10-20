using PickyBot.Contracts;
using PickyBot.Documents;
using PickyBot.Enums;

namespace PickyBot.Bots;

/// <summary>
/// Provides a unified entry point to generate documentation based on required format.
/// </summary>
public class DocumentationBot : IDocumentationBot
{
    // Simple property injection for demonstration. In a real application, 
    // this would be handled via a Dependency Injection container.
    private readonly IDictionary<FileType, IDocumentGenerator> _generators;

    public DocumentationBot()
    {
        _generators = new Dictionary<FileType, IDocumentGenerator>
        {
            { FileType.Html, new HtmlGenerator() },
            { FileType.Pdf, new LatexGenerator() }
        };
    }

    /// <summary>
    /// Generates documentation content based on the provided code and file type.
    /// </summary>
    /// <param name="code">The final, fixed source code.</param>
    /// <param name="fileType">The desired output format (HTML or PDF/LaTeX).</param>
    /// <returns>The generated document content as a string.</returns>
    public string GenerateDocument(string code, FileType fileType, Language language)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException("Source code cannot be null or empty.", nameof(code));
        }

        if (!_generators.TryGetValue(fileType, out var generator))
        {
            throw new NotSupportedException($"Documentation generation for {fileType} is not supported.");
        }

        // Delegate the work to the specific generator strategy
        return generator.Generate(code, language);
    }
}

