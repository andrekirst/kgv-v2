using System.Text.RegularExpressions;
using KgvSystem.Domain.Shared;

namespace KgvSystem.Domain.ValueObjects;

/// <summary>
/// Aktenzeichen Value Object für das KGV-System.
/// Format: [Präfix] [Laufende Nummer] [Jahr]
/// Beispiele: "32.2 128 2024", "33.2 456 2024"
/// 
/// IMMUTABLE VALUE OBJECT - PROTECTED BY CLAUDE.MD
/// This implementation follows the exact domain specification and must not be modified
/// without explicit architectural approval.
/// </summary>
public sealed record Aktenzeichen
{
    private static readonly Regex Pattern = 
        new(@"^(32\.2|33\.2)\s(\d+)\s(\d{4})$", RegexOptions.Compiled);

    /// <summary>
    /// The full Aktenzeichen string value (e.g., "32.2 128 2024")
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// The prefix part determining the waiting list type ("32.2" or "33.2")
    /// </summary>
    public string Prefix { get; }

    /// <summary>
    /// The sequential number part
    /// </summary>
    public int Number { get; }

    /// <summary>
    /// The year part
    /// </summary>
    public int Year { get; }

    /// <summary>
    /// The waiting list type derived from the prefix
    /// </summary>
    public WaitingListType ListType { get; }

    private Aktenzeichen(string value, string prefix, int number, int year, WaitingListType listType)
    {
        Value = value;
        Prefix = prefix;
        Number = number;
        Year = year;
        ListType = listType;
    }

    /// <summary>
    /// Creates a new Aktenzeichen with the specified parameters.
    /// This is the primary factory method for creating new Aktenzeichen instances.
    /// </summary>
    /// <param name="listType">The waiting list type (Nr32 or Nr33)</param>
    /// <param name="number">The sequential number (must be positive)</param>
    /// <param name="year">The application year (must be valid range)</param>
    /// <returns>Result containing the created Aktenzeichen or error message</returns>
    public static Result<Aktenzeichen> Create(WaitingListType listType, int number, int year)
    {
        if (number <= 0)
            return Result<Aktenzeichen>.Failure("Number must be positive");

        if (year < 2000 || year > DateTime.Now.Year + 1)
            return Result<Aktenzeichen>.Failure("Invalid year");

        var prefix = listType == WaitingListType.Nr32 ? "32.2" : "33.2";
        var value = $"{prefix} {number} {year}";

        return Result<Aktenzeichen>.Success(new Aktenzeichen(value, prefix, number, year, listType));
    }

    /// <summary>
    /// Parses an Aktenzeichen from its string representation.
    /// This method is used when reading Aktenzeichen values from external sources.
    /// </summary>
    /// <param name="value">The string value to parse</param>
    /// <returns>Result containing the parsed Aktenzeichen or error message</returns>
    public static Result<Aktenzeichen> Parse(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<Aktenzeichen>.Failure("Aktenzeichen cannot be empty");

        var match = Pattern.Match(value.Trim());
        if (!match.Success)
            return Result<Aktenzeichen>.Failure($"Invalid format: {value}");

        var prefix = match.Groups[1].Value;
        var number = int.Parse(match.Groups[2].Value);
        var year = int.Parse(match.Groups[3].Value);
        var listType = prefix == "32.2" ? WaitingListType.Nr32 : WaitingListType.Nr33;

        return Result<Aktenzeichen>.Success(new Aktenzeichen(value.Trim(), prefix, number, year, listType));
    }

    /// <summary>
    /// Returns the string representation of the Aktenzeichen
    /// </summary>
    public override string ToString() => Value;

    /// <summary>
    /// Implicit conversion to string for convenience
    /// </summary>
    public static implicit operator string(Aktenzeichen aktenzeichen) => aktenzeichen.Value;
}