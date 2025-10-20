using PickyBot.Contracts;
using PickyBot.Enums;
using System.Text;

namespace PickyBot.Documents;

// <summary>
/// Generates documentation in a responsive HTML format, suitable for web viewing.
/// </summary>
public class HtmlGenerator : IDocumentGenerator
{
    public string Generate(string code, Language language)
    {
        var sb = new StringBuilder();

        // Use a modern, responsive single-file HTML structure with Tailwind CSS
        sb.AppendLine("<!DOCTYPE html>");
        sb.AppendLine("<html lang=\"en\">");
        sb.AppendLine("<head>");
        sb.AppendLine("    <meta charset=\"UTF-8\">");
        sb.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
        sb.AppendLine("    <title>Code Documentation Report</title>");
        sb.AppendLine("    <script src=\"https://cdn.tailwindcss.com\"></script>");
        sb.AppendLine("    <style>");
        sb.AppendLine("        /* Custom scrollbar and aesthetics */");
        sb.AppendLine("        code, pre { font-family: 'Fira Code', 'Consolas', monospace; }");
        sb.AppendLine("    </style>");
        sb.AppendLine("</head>");
        sb.AppendLine("<body class=\"bg-gray-50 p-6 sm:p-10 font-sans\">");

        sb.AppendLine("    <div class=\"max-w-4xl mx-auto bg-white shadow-xl rounded-xl p-6\">");
        sb.AppendLine("        <h1 class=\"text-4xl font-bold text-gray-800 mb-6 border-b-4 border-indigo-500 pb-2\">");
        sb.AppendLine("            Technical Documentation: Code Review");
        sb.AppendLine("        </h1>");

        sb.AppendLine("        <div class=\"mb-8\">");
        sb.AppendLine("            <h2 class=\"text-2xl font-semibold text-indigo-600 mb-3\">Source Code</h2>");
        sb.AppendLine("            <p class=\"text-sm text-gray-500 mb-2\">Language: " + language + "</p>");
        sb.AppendLine("            <pre class=\"p-4 bg-gray-900 text-green-300 rounded-lg overflow-x-auto text-sm shadow-inner\">");
        sb.AppendLine(HtmlEncode(code));
        sb.AppendLine("            </pre>");
        sb.AppendLine("        </div>");

        sb.AppendLine("        <div class=\"border-t pt-6\">");
        sb.AppendLine("            <h2 class=\"text-2xl font-semibold text-indigo-600 mb-3\">Architectural Notes</h2>");
        sb.AppendLine("            <p class=\"text-gray-600\">");
        sb.AppendLine("            This is a standardized documentation artifact generated from the reviewed source code. ");
        sb.AppendLine("            It serves as the immutable blueprint for the fixed component and is categorized as a L1 Technical Report.");
        sb.AppendLine("            </p>");
        sb.AppendLine("        </div>");

        sb.AppendLine("    </div>");
        sb.AppendLine("</body>");
        sb.AppendLine("</html>");

        return sb.ToString();
    }

    private static string HtmlEncode(string text) =>

         // Simple encoding for display inside <pre> tags
         text.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
}

