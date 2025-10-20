
using PickyBot.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PickyBot.Utils;

public static class JsonUtils
{
    /// <summary>
    /// Removes markdown code fences (```json, ```) from the LLM response to ensure pure JSON parsing.
    /// This is necessary because some models, even in JSON mode, wrap the output.
    /// </summary>
    /// <param name="markdown">The raw string output from the LLM.</param>
    /// <returns>A string containing only the JSON content.</returns>
    public static string ExtractJsonFromMarkdown(string markdown)
    {
        if (string.IsNullOrWhiteSpace(markdown))
        {
            return string.Empty;
        }
        int startIndex = markdown.IndexOf('{');
        int endIndex = markdown.LastIndexOf('}');

        if (startIndex == -1 || endIndex == -1 || endIndex <= startIndex)
        {
            return markdown.Trim();
        }
        return markdown.Substring(startIndex, endIndex - startIndex + 1);
    }
}


public class StringOrObjectListConverter<T> : JsonConverter<List<T>> where T : new()
{
    public override List<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException($"Expected StartArray but found {reader.TokenType}");
        }

        var list = new List<T>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                return list;
            }

            // Check if the token is a string (simple format)
            if (reader.TokenType == JsonTokenType.String && typeof(T) == typeof(Recommendation))
            {
                string? recommendationText = reader.GetString();
                if (!string.IsNullOrEmpty(recommendationText))
                {
                    // If it's a string, create a new Recommendation object and assign a sequential ID
                    list.Add((T)(object)new Recommendation { Id = list.Count + 1, RecommendationText = recommendationText });
                }
            }
            // Check if the token is the start of an object (rich format)
            else if (reader.TokenType == JsonTokenType.StartObject)
            {
                // Deserialize the object normally
                var item = JsonSerializer.Deserialize<T>(ref reader, options);
                if (item != null)
                {
                    list.Add(item);
                }
            }
            // Skip unrecognized tokens to prevent crashing
            else if (reader.TokenType != JsonTokenType.Comment)
            {
                // Skip the current token and its children
                reader.Skip();
            }
        }
        return list;
    }

    public override void Write(Utf8JsonWriter writer, List<T> value, JsonSerializerOptions options)
    {
        // For simplicity, we won't implement serialization back to JSON in this example
        writer.WriteStartArray();
        foreach (var item in value)
        {
            JsonSerializer.Serialize(writer, item, options);
        }
        writer.WriteEndArray();
    }
}