using PickyBot.Contracts;
using PickyBot.Enums;
using System.Text;

namespace PickyBot.Documents;

/// <summary>
/// Generates a LaTeX (.tex) document structure, which is the prerequisite for PDF generation.
/// </summary>
public class LatexGenerator : IDocumentGenerator
{
    public string Generate(string code, Language language)
    {
        var sb = new StringBuilder();

        // Use a simple, robust article class with the listings package for code formatting.
        sb.AppendLine("\\documentclass[11pt, a4paper]{article}");
        sb.AppendLine("\\usepackage[a4paper, margin=1in]{geometry}");
        sb.AppendLine("\\usepackage{listings}");
        sb.AppendLine("\\usepackage{xcolor}");
        sb.AppendLine("\\usepackage[utf8]{inputenc}");
        sb.AppendLine("\\usepackage{hyperref}");
        sb.AppendLine("\\hypersetup{colorlinks=true, linkcolor=blue, urlcolor=blue}");
        sb.AppendLine("");

        sb.AppendLine("\\definecolor{codegray}{rgb}{0.5,0.5,0.5}");
        sb.AppendLine("\\definecolor{codegreen}{rgb}{0,0.6,0}");
        sb.AppendLine("\\lstset{");
        sb.AppendLine("    basicstyle=\\footnotesize\\ttfamily,");
        sb.AppendLine("    commentstyle=\\color{codegreen},");
        sb.AppendLine("    keywordstyle=\\color{blue},");
        sb.AppendLine("    stringstyle=\\color{red},");
        sb.AppendLine("    numberstyle=\\tiny\\color{codegray},");
        sb.AppendLine("    breaklines=true,");
        sb.AppendLine("    frame=single,");
        sb.AppendLine("    numbers=left,");
        sb.AppendLine("    stepnumber=1,");
        sb.AppendLine("    numbersep=5pt,");
        sb.AppendLine("    tabsize=4");
        sb.AppendLine("}");

        sb.AppendLine("\\title{Architectural Code Documentation}");
        sb.AppendLine("\\author{Automated Code Review System}");
        sb.AppendLine("\\date{\\today}");
        sb.AppendLine("\\begin{document}");
        sb.AppendLine("\\maketitle");

        sb.AppendLine("\\section{Source Code Artifact}");
        sb.AppendLine("The following artifact represents the fixed and reviewed source code for the " + language + " component.");
        sb.AppendLine("");

        // Determine listings language based on input (simple check)
        string lstLanguage = language == Language.CSharp ? "C#" : "Python";

        sb.AppendLine("\\begin{lstlisting}[language=" + lstLanguage + ", caption=Fixed " + language + " Code]");
        sb.AppendLine(code);
        sb.AppendLine("\\end{lstlisting}");

        sb.AppendLine("\\section{Documentation Metadata}");
        sb.AppendLine("\\begin{itemize}");
        sb.AppendLine("\\item \\textbf{Format Strategy}: Strategy Pattern (IDocumentGenerator)");
        sb.AppendLine("\\item \\textbf{Purpose}: Archival and Immutable Blueprint Generation");
        sb.AppendLine("\\end{itemize}");

        sb.AppendLine("\\end{document}");

        return sb.ToString();
    }
}
