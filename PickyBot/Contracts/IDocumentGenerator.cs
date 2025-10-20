using PickyBot.Enums;

namespace PickyBot.Contracts;

/// <summary>
/// Interface for all documentation generators, ensuring flexibility.
/// </summary>
public interface IDocumentGenerator
{
    string Generate(string code, Language language);
}


public interface IDocumentationBot
{
    string GenerateDocument(string code, FileType fileType, Language language);
}
