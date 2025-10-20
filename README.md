##  🤖 Architectural Code Review & Documentation System

This repository serves as a technical demonstration of a resilient, scalable backend service for automated code analysis and documentation generation using any programming language like C#, Python, and others.

### 💡 Core Architectural Concepts

We designed this system to be robust and highly maintainable, focusing on three key architectural principles:

#### 1. Uniform Language Handling for Rules Enforcement

Concept: Applying a consistent analysis and documentation process across diverse programming languages.

Implementation: We employ the LLM as a unified rules engine. The C# host detects the source language (via the Language enum), and the LLM, given the code and the desired ruleset, performs the analysis. The subsequent Documentation Service then uses the detected language to apply the correct syntax highlighting (e.g., in the LaTeX output), ensuring a consistent, high-quality documentation standard regardless of the source code.

#### 2. Resilience via Custom Deserialization

Concept: Fault Tolerance and Robustness against external APIs.

Implementation: We utilize a Custom JSON Converter `(StringOrObjectListConverter)` to handle inconsistent output from the Large Language Model (LLM). This ensures that whether the LLM returns an array of structured JSON objects or a simple array of strings for recommendations, the application remains resilient and can correctly parse the data.

#### 3. Scalability via the Strategy Pattern

Concept: Open/Closed Principle (OCP) software entities should be open for extension but closed for modification.

Implementation: The `DocumentationBot` implements the `Strategy Pattern` via the IDocumentGenerator interface. Adding support for a new output format (e.g., Markdown) requires only creating a new class (extension) without modifying the core generation logic (closed for modification).

Supported Strategies: HTML (web viewing) and LaTeX (PDF/archival reports).

## Demo
[Live Demo](sample/pickybot.MP4)

