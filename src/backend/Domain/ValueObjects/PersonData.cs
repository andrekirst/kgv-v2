using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace KgvSystem.Domain.ValueObjects;

/// <summary>
/// PersonData Value Object für das KGV-System.
/// Kapselt alle persönlichen Daten eines Antragstellers mit Validierung.
/// 
/// IMMUTABLE VALUE OBJECT - PROTECTED BY CLAUDE.MD
/// This implementation follows the exact domain specification and must not be modified
/// without explicit architectural approval.
/// </summary>
public sealed record PersonData
{
    private static readonly Regex EmailPattern = 
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    /// <summary>
    /// First name of the person (required, minimum 1 character)
    /// </summary>
    public string FirstName { get; }

    /// <summary>
    /// Last name of the person (required, minimum 1 character)
    /// </summary>
    public string LastName { get; }

    /// <summary>
    /// Date of birth (required, person must be at least 18 years old)
    /// </summary>
    public DateTime DateOfBirth { get; }

    /// <summary>
    /// Email address (optional, must be valid RFC format if provided)
    /// </summary>
    public string? Email { get; }

    /// <summary>
    /// Phone number (optional)
    /// </summary>
    public string? Phone { get; }

    /// <summary>
    /// Computed property: Full name combining first and last name
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// Computed property: Current age in years
    /// </summary>
    public int Age => DateTime.Now.Year - DateOfBirth.Year - 
                     (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);

    /// <summary>
    /// Creates a new PersonData instance with validation.
    /// This constructor ensures all business rules are enforced.
    /// </summary>
    /// <param name="firstName">First name (required, min 1 char)</param>
    /// <param name="lastName">Last name (required, min 1 char)</param>
    /// <param name="dateOfBirth">Date of birth (required, min 18 years old)</param>
    /// <param name="email">Email address (optional, RFC format)</param>
    /// <param name="phone">Phone number (optional)</param>
    /// <exception cref="ArgumentException">Thrown when validation fails</exception>
    public PersonData(string firstName, string lastName, DateTime dateOfBirth, string? email = null, string? phone = null)
    {
        // Validate first name
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty", nameof(firstName));

        if (firstName.Length < 1)
            throw new ArgumentException("First name must be at least 1 character", nameof(firstName));

        // Validate last name
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty", nameof(lastName));

        if (lastName.Length < 1)
            throw new ArgumentException("Last name must be at least 1 character", nameof(lastName));

        // Validate date of birth is not in the future (check first)
        if (dateOfBirth > DateTime.Now)
            throw new ArgumentException("Date of birth cannot be in the future", nameof(dateOfBirth));

        // Validate age (minimum 18 years)
        var age = DateTime.Now.Year - dateOfBirth.Year - 
                 (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear ? 1 : 0);

        if (age < 18)
            throw new ArgumentException("Person must be at least 18 years old", nameof(dateOfBirth));

        // Process email before validation
        var processedEmail = string.IsNullOrWhiteSpace(email) ? null : email.Trim();
        
        // Validate email format if provided
        if (!string.IsNullOrWhiteSpace(processedEmail) && !EmailPattern.IsMatch(processedEmail))
            throw new ArgumentException("Invalid email format", nameof(email));

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        DateOfBirth = dateOfBirth.Date; // Remove time component
        Email = processedEmail?.ToLowerInvariant();
        Phone = string.IsNullOrWhiteSpace(phone) ? null : phone.Trim();
    }


    /// <summary>
    /// Returns a string representation of the person
    /// </summary>
    public override string ToString() => $"{FullName} (born {DateOfBirth:yyyy-MM-dd})";
}