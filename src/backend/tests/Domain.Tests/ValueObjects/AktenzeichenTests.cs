using FluentAssertions;
using KgvSystem.Domain.Shared;
using KgvSystem.Domain.ValueObjects;

namespace KgvSystem.Domain.Tests.ValueObjects;

/// <summary>
/// Comprehensive unit tests for the Aktenzeichen Value Object.
/// These tests validate all business rules and edge cases according to the domain specification.
/// Coverage Target: >95%
/// </summary>
public class AktenzeichenTests
{
    [Fact]
    public void Create_WithValidNr32Parameters_ShouldSucceed()
    {
        // Arrange
        var listType = WaitingListType.Nr32;
        var number = 123;
        var year = 2024;

        // Act
        var result = Aktenzeichen.Create(listType, number, year);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be("32.2 123 2024");
        result.Value.Prefix.Should().Be("32.2");
        result.Value.Number.Should().Be(123);
        result.Value.Year.Should().Be(2024);
        result.Value.ListType.Should().Be(WaitingListType.Nr32);
    }

    [Fact]
    public void Create_WithValidNr33Parameters_ShouldSucceed()
    {
        // Arrange
        var listType = WaitingListType.Nr33;
        var number = 456;
        var year = 2024;

        // Act
        var result = Aktenzeichen.Create(listType, number, year);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be("33.2 456 2024");
        result.Value.Prefix.Should().Be("33.2");
        result.Value.Number.Should().Be(456);
        result.Value.Year.Should().Be(2024);
        result.Value.ListType.Should().Be(WaitingListType.Nr33);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Create_WithInvalidNumber_ShouldFail(int invalidNumber)
    {
        // Arrange
        var listType = WaitingListType.Nr32;
        var year = 2024;

        // Act
        var result = Aktenzeichen.Create(listType, invalidNumber, year);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Number must be positive");
    }

    [Theory]
    [InlineData(1999)]
    [InlineData(1900)]
    [InlineData(2030)] // Assuming current year is 2024 or earlier
    public void Create_WithInvalidYear_ShouldFail(int invalidYear)
    {
        // Arrange
        var listType = WaitingListType.Nr32;
        var number = 123;

        // Act
        var result = Aktenzeichen.Create(listType, number, invalidYear);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Invalid year");
    }

    [Theory]
    [InlineData("32.2 123 2024", "32.2", 123, 2024, WaitingListType.Nr32)]
    [InlineData("33.2 456 2024", "33.2", 456, 2024, WaitingListType.Nr33)]
    [InlineData("32.2 1 2020", "32.2", 1, 2020, WaitingListType.Nr32)]
    [InlineData("33.2 9999 2023", "33.2", 9999, 2023, WaitingListType.Nr33)]
    public void Parse_WithValidFormats_ShouldSucceed(string input, string expectedPrefix, 
        int expectedNumber, int expectedYear, WaitingListType expectedListType)
    {
        // Act
        var result = Aktenzeichen.Parse(input);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(input);
        result.Value.Prefix.Should().Be(expectedPrefix);
        result.Value.Number.Should().Be(expectedNumber);
        result.Value.Year.Should().Be(expectedYear);
        result.Value.ListType.Should().Be(expectedListType);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t\n")]
    public void Parse_WithEmptyOrWhitespaceInput_ShouldFail(string input)
    {
        // Act
        var result = Aktenzeichen.Parse(input);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Aktenzeichen cannot be empty");
    }

    [Theory]
    [InlineData("32.2123 2024")] // Missing space
    [InlineData("32.2 123")] // Missing year
    [InlineData("32.2 2024")] // Missing number
    [InlineData("34.2 123 2024")] // Invalid prefix
    [InlineData("32.2 abc 2024")] // Non-numeric number
    [InlineData("32.2 123 abcd")] // Non-numeric year
    [InlineData("32.2   123   2024")] // Multiple spaces (3 spaces)
    [InlineData("32,2 123 2024")] // Wrong separator
    [InlineData("32.2 123 24")] // Two-digit year
    public void Parse_WithInvalidFormats_ShouldFail(string invalidInput)
    {
        // Act
        var result = Aktenzeichen.Parse(invalidInput);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().StartWith("Invalid format:");
    }

    [Fact]
    public void Parse_WithWhitespaceAroundValidInput_ShouldSucceed()
    {
        // Arrange
        var input = "  32.2 123 2024  ";

        // Act
        var result = Aktenzeichen.Parse(input);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be("32.2 123 2024");
    }

    [Fact]
    public void ToString_ShouldReturnValue()
    {
        // Arrange
        var aktenzeichen = Aktenzeichen.Create(WaitingListType.Nr32, 123, 2024).Value;

        // Act
        var stringValue = aktenzeichen.ToString();

        // Assert
        stringValue.Should().Be("32.2 123 2024");
    }

    [Fact]
    public void ImplicitConversionToString_ShouldWork()
    {
        // Arrange
        var aktenzeichen = Aktenzeichen.Create(WaitingListType.Nr33, 456, 2024).Value;

        // Act
        string stringValue = aktenzeichen;

        // Assert
        stringValue.Should().Be("33.2 456 2024");
    }

    [Fact]
    public void Equality_SameValues_ShouldBeEqual()
    {
        // Arrange
        var aktenzeichen1 = Aktenzeichen.Create(WaitingListType.Nr32, 123, 2024).Value;
        var aktenzeichen2 = Aktenzeichen.Create(WaitingListType.Nr32, 123, 2024).Value;

        // Act & Assert
        aktenzeichen1.Should().Be(aktenzeichen2);
        aktenzeichen1.GetHashCode().Should().Be(aktenzeichen2.GetHashCode());
    }

    [Fact]
    public void Equality_DifferentValues_ShouldNotBeEqual()
    {
        // Arrange
        var aktenzeichen1 = Aktenzeichen.Create(WaitingListType.Nr32, 123, 2024).Value;
        var aktenzeichen2 = Aktenzeichen.Create(WaitingListType.Nr32, 124, 2024).Value;

        // Act & Assert
        aktenzeichen1.Should().NotBe(aktenzeichen2);
    }

    [Fact]
    public void Equality_DifferentListTypes_ShouldNotBeEqual()
    {
        // Arrange
        var aktenzeichen1 = Aktenzeichen.Create(WaitingListType.Nr32, 123, 2024).Value;
        var aktenzeichen2 = Aktenzeichen.Create(WaitingListType.Nr33, 123, 2024).Value;

        // Act & Assert
        aktenzeichen1.Should().NotBe(aktenzeichen2);
    }

    [Fact]
    public void CreateAndParse_Roundtrip_ShouldBeIdentical()
    {
        // Arrange
        var original = Aktenzeichen.Create(WaitingListType.Nr32, 789, 2023).Value;

        // Act
        var parsed = Aktenzeichen.Parse(original.Value).Value;

        // Assert
        parsed.Should().Be(original);
        parsed.Value.Should().Be(original.Value);
        parsed.Prefix.Should().Be(original.Prefix);
        parsed.Number.Should().Be(original.Number);
        parsed.Year.Should().Be(original.Year);
        parsed.ListType.Should().Be(original.ListType);
    }

    [Theory]
    [InlineData(WaitingListType.Nr32, "32.2")]
    [InlineData(WaitingListType.Nr33, "33.2")]
    public void Create_PrefixMapping_ShouldBeCorrect(WaitingListType listType, string expectedPrefix)
    {
        // Arrange
        var number = 123;
        var year = 2024;

        // Act
        var result = Aktenzeichen.Create(listType, number, year);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Prefix.Should().Be(expectedPrefix);
    }

    [Fact]
    public void Create_CurrentYear_ShouldBeValid()
    {
        // Arrange
        var currentYear = DateTime.Now.Year;
        var listType = WaitingListType.Nr32;
        var number = 123;

        // Act
        var result = Aktenzeichen.Create(listType, number, currentYear);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Year.Should().Be(currentYear);
    }

    [Fact]
    public void Create_NextYear_ShouldBeValid()
    {
        // Arrange
        var nextYear = DateTime.Now.Year + 1;
        var listType = WaitingListType.Nr32;
        var number = 123;

        // Act
        var result = Aktenzeichen.Create(listType, number, nextYear);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Year.Should().Be(nextYear);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(99)]
    [InlineData(999)]
    [InlineData(9999)]
    [InlineData(int.MaxValue)]
    public void Create_VariousValidNumbers_ShouldSucceed(int number)
    {
        // Arrange
        var listType = WaitingListType.Nr32;
        var year = 2024;

        // Act
        var result = Aktenzeichen.Create(listType, number, year);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Number.Should().Be(number);
    }
}