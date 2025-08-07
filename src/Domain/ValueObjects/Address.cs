using System.Text.RegularExpressions;

namespace KgvSystem.Domain.ValueObjects;

/// <summary>
/// Address Value Object für das KGV-System.
/// Represents a German address with postal code validation.
/// 
/// IMMUTABLE VALUE OBJECT - PROTECTED BY CLAUDE.MD
/// This implementation follows the exact domain specification and must not be modified
/// without explicit architectural approval.
/// </summary>
public sealed record Address
{
    private static readonly Regex GermanPostalCodePattern = 
        new(@"^\d{5}$", RegexOptions.Compiled);

    /// <summary>
    /// Street name (required)
    /// </summary>
    public string Street { get; }

    /// <summary>
    /// House number (required, can include letters like "12a")
    /// </summary>
    public string HouseNumber { get; }

    /// <summary>
    /// German postal code (required, 5 digits)
    /// </summary>
    public string PostalCode { get; }

    /// <summary>
    /// City name (required)
    /// </summary>
    public string City { get; }

    /// <summary>
    /// Country (defaults to "Deutschland")
    /// </summary>
    public string Country { get; }

    /// <summary>
    /// Computed property: Full address string for display purposes
    /// </summary>
    public string FullAddress => $"{Street} {HouseNumber}, {PostalCode} {City}, {Country}";

    /// <summary>
    /// Computed property: Street address without city and country
    /// </summary>
    public string StreetAddress => $"{Street} {HouseNumber}";

    /// <summary>
    /// Computed property: City with postal code
    /// </summary>
    public string CityWithPostalCode => $"{PostalCode} {City}";

    /// <summary>
    /// Creates a new Address instance with validation.
    /// This constructor ensures all business rules are enforced.
    /// </summary>
    /// <param name="street">Street name (required)</param>
    /// <param name="houseNumber">House number (required)</param>
    /// <param name="postalCode">German postal code (required, 5 digits)</param>
    /// <param name="city">City name (required)</param>
    /// <param name="country">Country (optional, defaults to "Deutschland")</param>
    /// <exception cref="ArgumentException">Thrown when validation fails</exception>
    public Address(string street, string houseNumber, string postalCode, string city, string country = "Deutschland")
    {
        // Validate street
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Street cannot be empty", nameof(street));

        // Validate house number
        if (string.IsNullOrWhiteSpace(houseNumber))
            throw new ArgumentException("House number cannot be empty", nameof(houseNumber));

        // Validate postal code (German format: 5 digits)
        if (string.IsNullOrWhiteSpace(postalCode))
            throw new ArgumentException("Postal code cannot be empty", nameof(postalCode));

        var trimmedPostalCode = postalCode.Trim();
        if (!GermanPostalCodePattern.IsMatch(trimmedPostalCode))
            throw new ArgumentException("Postal code must be exactly 5 digits", nameof(postalCode));

        // Validate city
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City cannot be empty", nameof(city));

        // Validate country
        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country cannot be empty", nameof(country));

        Street = street.Trim();
        HouseNumber = houseNumber.Trim();
        PostalCode = trimmedPostalCode;
        City = city.Trim();
        Country = country.Trim();
    }

    /// <summary>
    /// Creates an Address specifically for German addresses with validation
    /// </summary>
    /// <param name="street">Street name</param>
    /// <param name="houseNumber">House number</param>
    /// <param name="postalCode">5-digit postal code</param>
    /// <param name="city">City name</param>
    /// <returns>Address instance</returns>
    public static Address CreateGerman(string street, string houseNumber, string postalCode, string city)
    {
        return new Address(street, houseNumber, postalCode, city, "Deutschland");
    }

    /// <summary>
    /// Validates if a postal code is a valid German postal code
    /// </summary>
    /// <param name="postalCode">The postal code to validate</param>
    /// <returns>True if valid German postal code, false otherwise</returns>
    public static bool IsValidGermanPostalCode(string postalCode)
    {
        return !string.IsNullOrWhiteSpace(postalCode) && GermanPostalCodePattern.IsMatch(postalCode);
    }

    /// <summary>
    /// Determines the federal state (Bundesland) based on postal code.
    /// This is a simplified mapping for common postal code ranges.
    /// </summary>
    /// <returns>The federal state name or "Unbekannt" if not determinable</returns>
    public string GetFederalState()
    {
        if (string.IsNullOrEmpty(PostalCode) || PostalCode.Length != 5)
            return "Unbekannt";

        return PostalCode[0] switch
        {
            '0' => "Sachsen/Sachsen-Anhalt/Thüringen",
            '1' => "Brandenburg/Berlin",
            '2' => "Schleswig-Holstein/Hamburg/Bremen/Niedersachsen",
            '3' => "Niedersachsen/Nordrhein-Westfalen",
            '4' => "Nordrhein-Westfalen",
            '5' => "Nordrhein-Westfalen/Rheinland-Pfalz",
            '6' => "Hessen/Rheinland-Pfalz",
            '7' => "Baden-Württemberg",
            '8' => "Bayern",
            '9' => "Bayern",
            _ => "Unbekannt"
        };
    }

    /// <summary>
    /// Returns a string representation of the address
    /// </summary>
    public override string ToString() => FullAddress;
}