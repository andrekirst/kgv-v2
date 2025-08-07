using FluentAssertions;
using KgvSystem.Domain.ValueObjects;

namespace KgvSystem.Domain.Tests.ValueObjects;

/// <summary>
/// Comprehensive unit tests for the PersonData Value Object.
/// These tests validate all business rules and edge cases according to the domain specification.
/// Coverage Target: >95%
/// </summary>
public class PersonDataTests
{
    [Fact]
    public void Constructor_WithValidData_ShouldSucceed()
    {
        // Arrange
        var firstName = "Max";
        var lastName = "Mustermann";
        var dateOfBirth = new DateTime(1990, 1, 1);
        var email = "max@test.de";
        var phone = "123456789";

        // Act
        var personData = new PersonData(firstName, lastName, dateOfBirth, email, phone);

        // Assert
        personData.FirstName.Should().Be("Max");
        personData.LastName.Should().Be("Mustermann");
        personData.DateOfBirth.Should().Be(new DateTime(1990, 1, 1));
        personData.Email.Should().Be("max@test.de");
        personData.Phone.Should().Be("123456789");
        personData.FullName.Should().Be("Max Mustermann");
    }

    [Fact]
    public void Constructor_WithMinimalValidData_ShouldSucceed()
    {
        // Arrange
        var firstName = "A";
        var lastName = "B";
        var dateOfBirth = DateTime.Now.AddYears(-18).AddDays(-1); // Exactly 18 years and 1 day old

        // Act
        var personData = new PersonData(firstName, lastName, dateOfBirth);

        // Assert
        personData.FirstName.Should().Be("A");
        personData.LastName.Should().Be("B");
        personData.DateOfBirth.Should().Be(dateOfBirth.Date);
        personData.Email.Should().BeNull();
        personData.Phone.Should().BeNull();
        personData.FullName.Should().Be("A B");
        personData.Age.Should().BeGreaterThanOrEqualTo(18);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t\n")]
    public void Constructor_WithInvalidFirstName_ShouldThrow(string invalidFirstName)
    {
        // Arrange
        var lastName = "Mustermann";
        var dateOfBirth = new DateTime(1990, 1, 1);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            new PersonData(invalidFirstName, lastName, dateOfBirth));
        
        exception.ParamName.Should().Be("firstName");
        exception.Message.Should().Contain("First name cannot be empty");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t\n")]
    public void Constructor_WithInvalidLastName_ShouldThrow(string invalidLastName)
    {
        // Arrange
        var firstName = "Max";
        var dateOfBirth = new DateTime(1990, 1, 1);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            new PersonData(firstName, invalidLastName, dateOfBirth));
        
        exception.ParamName.Should().Be("lastName");
        exception.Message.Should().Contain("Last name cannot be empty");
    }

    [Theory]
    [InlineData(17)] // 17 years old
    [InlineData(10)] // 10 years old
    [InlineData(0)]  // Born today
    public void Constructor_WithUnderageApplicant_ShouldThrow(int ageInYears)
    {
        // Arrange
        var firstName = "Max";
        var lastName = "Mustermann";
        var dateOfBirth = DateTime.Now.AddYears(-ageInYears);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            new PersonData(firstName, lastName, dateOfBirth));
        
        exception.ParamName.Should().Be("dateOfBirth");
        exception.Message.Should().Contain("Person must be at least 18 years old");
    }

    [Fact]
    public void Constructor_WithFutureDateOfBirth_ShouldThrow()
    {
        // Arrange
        var firstName = "Max";
        var lastName = "Mustermann";
        var dateOfBirth = DateTime.Now.AddDays(1); // Tomorrow

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            new PersonData(firstName, lastName, dateOfBirth));
        
        exception.ParamName.Should().Be("dateOfBirth");
        exception.Message.Should().Contain("Date of birth cannot be in the future");
    }

    [Theory]
    [InlineData("invalid-email")]
    [InlineData("@test.de")]
    [InlineData("test@")]
    [InlineData("test@.de")]
    [InlineData("test@test")]
    [InlineData("test test@test.de")]
    [InlineData("")]
    public void Constructor_WithInvalidEmail_ShouldThrow(string invalidEmail)
    {
        // Arrange
        var firstName = "Max";
        var lastName = "Mustermann";
        var dateOfBirth = new DateTime(1990, 1, 1);

        // Act & Assert
        if (string.IsNullOrEmpty(invalidEmail))
        {
            // Empty email should be allowed (converted to null)
            var personData = new PersonData(firstName, lastName, dateOfBirth, invalidEmail);
            personData.Email.Should().BeNull();
        }
        else
        {
            var exception = Assert.Throws<ArgumentException>(() => 
                new PersonData(firstName, lastName, dateOfBirth, invalidEmail));
            
            exception.ParamName.Should().Be("email");
            exception.Message.Should().Contain("Invalid email format");
        }
    }

    [Theory]
    [InlineData("test@example.com")]
    [InlineData("user@test.de")]
    [InlineData("complex.email+tag@domain.co.uk")]
    [InlineData("Test@Example.COM")] // Should be converted to lowercase
    public void Constructor_WithValidEmail_ShouldSucceed(string validEmail)
    {
        // Arrange
        var firstName = "Max";
        var lastName = "Mustermann";
        var dateOfBirth = new DateTime(1990, 1, 1);

        // Act
        var personData = new PersonData(firstName, lastName, dateOfBirth, validEmail);

        // Assert
        personData.Email.Should().Be(validEmail.ToLowerInvariant());
    }

    [Fact]
    public void Constructor_ShouldTrimWhitespace()
    {
        // Arrange
        var firstName = "  Max  ";
        var lastName = "  Mustermann  ";
        var email = "  test@example.com  ";
        var phone = "  123456789  ";
        var dateOfBirth = new DateTime(1990, 1, 1);

        // Act
        var personData = new PersonData(firstName, lastName, dateOfBirth, email, phone);

        // Assert
        personData.FirstName.Should().Be("Max");
        personData.LastName.Should().Be("Mustermann");
        personData.Email.Should().Be("test@example.com");
        personData.Phone.Should().Be("123456789");
    }

    [Fact]
    public void DateOfBirth_ShouldRemoveTimeComponent()
    {
        // Arrange
        var firstName = "Max";
        var lastName = "Mustermann";
        var dateOfBirth = new DateTime(1990, 1, 1, 14, 30, 45); // With time component

        // Act
        var personData = new PersonData(firstName, lastName, dateOfBirth);

        // Assert
        personData.DateOfBirth.Should().Be(new DateTime(1990, 1, 1)); // Time removed
        personData.DateOfBirth.TimeOfDay.Should().Be(TimeSpan.Zero);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithNullOrEmptyOptionalFields_ShouldConvertToNull(string optionalValue)
    {
        // Arrange
        var firstName = "Max";
        var lastName = "Mustermann";
        var dateOfBirth = new DateTime(1990, 1, 1);

        // Act
        var personData = new PersonData(firstName, lastName, dateOfBirth, optionalValue, optionalValue);

        // Assert
        personData.Email.Should().BeNull();
        personData.Phone.Should().BeNull();
    }

    [Fact]
    public void Age_ShouldCalculateCorrectly()
    {
        // Arrange
        var birthYear = DateTime.Now.Year - 25; // 25 years ago
        var dateOfBirth = new DateTime(birthYear, 1, 1);
        var personData = new PersonData("Max", "Mustermann", dateOfBirth);

        // Act
        var age = personData.Age;

        // Assert
        age.Should().BeInRange(24, 25); // Depending on current date
    }

    [Fact]
    public void Age_OnBirthday_ShouldBeCorrect()
    {
        // Arrange
        var today = DateTime.Now.Date;
        var dateOfBirth = today.AddYears(-30); // Exactly 30 years ago
        var personData = new PersonData("Max", "Mustermann", dateOfBirth);

        // Act
        var age = personData.Age;

        // Assert
        age.Should().Be(30);
    }

    [Fact]
    public void Age_BeforeBirthday_ShouldBeOneYearLess()
    {
        // Arrange
        var today = DateTime.Now.Date;
        var dateOfBirth = today.AddYears(-30).AddDays(1); // Birthday tomorrow, but 30 years ago
        var personData = new PersonData("Max", "Mustermann", dateOfBirth);

        // Act
        var age = personData.Age;

        // Assert
        age.Should().Be(29); // Not yet 30
    }

    [Fact]
    public void FullName_ShouldCombineFirstAndLastName()
    {
        // Arrange
        var personData = new PersonData("Johann Sebastian", "Bach", new DateTime(1685, 3, 21));

        // Act
        var fullName = personData.FullName;

        // Assert
        fullName.Should().Be("Johann Sebastian Bach");
    }

    [Fact]
    public void ToString_ShouldReturnFormattedString()
    {
        // Arrange
        var dateOfBirth = new DateTime(1990, 5, 15);
        var personData = new PersonData("Max", "Mustermann", dateOfBirth);

        // Act
        var stringValue = personData.ToString();

        // Assert
        stringValue.Should().Be("Max Mustermann (born 1990-05-15)");
    }


    [Fact]
    public void Equality_SameValues_ShouldBeEqual()
    {
        // Arrange
        var dateOfBirth = new DateTime(1990, 1, 1);
        var personData1 = new PersonData("Max", "Mustermann", dateOfBirth, "test@example.com");
        var personData2 = new PersonData("Max", "Mustermann", dateOfBirth, "test@example.com");

        // Act & Assert
        personData1.Should().Be(personData2);
        personData1.GetHashCode().Should().Be(personData2.GetHashCode());
    }

    [Fact]
    public void Equality_DifferentValues_ShouldNotBeEqual()
    {
        // Arrange
        var dateOfBirth = new DateTime(1990, 1, 1);
        var personData1 = new PersonData("Max", "Mustermann", dateOfBirth);
        var personData2 = new PersonData("John", "Doe", dateOfBirth);

        // Act & Assert
        personData1.Should().NotBe(personData2);
    }

    [Theory]
    [InlineData(18)] // Exactly 18
    [InlineData(21)] // Adult
    [InlineData(65)] // Senior
    [InlineData(100)] // Very old
    public void Constructor_WithValidAges_ShouldSucceed(int ageInYears)
    {
        // Arrange
        var firstName = "Test";
        var lastName = "Person";
        var dateOfBirth = DateTime.Now.AddYears(-ageInYears).AddDays(-1); // Ensure they're actually this age

        // Act
        var personData = new PersonData(firstName, lastName, dateOfBirth);

        // Assert
        personData.Age.Should().BeGreaterThanOrEqualTo(18);
        personData.DateOfBirth.Should().Be(dateOfBirth.Date);
    }

    [Fact]
    public void Constructor_EmailCaseInsensitive_ShouldNormalizeToLowercase()
    {
        // Arrange
        var firstName = "Max";
        var lastName = "Mustermann";
        var dateOfBirth = new DateTime(1990, 1, 1);
        var email = "Test.User@EXAMPLE.COM";

        // Act
        var personData = new PersonData(firstName, lastName, dateOfBirth, email);

        // Assert
        personData.Email.Should().Be("test.user@example.com");
    }

    [Theory]
    [InlineData("123-456-7890")]
    [InlineData("+49 123 456789")]
    [InlineData("0123 456789")]
    [InlineData("123456789")]
    [InlineData("(123) 456-7890")]
    public void Constructor_WithVariousPhoneFormats_ShouldAccept(string phoneNumber)
    {
        // Arrange
        var firstName = "Max";
        var lastName = "Mustermann";
        var dateOfBirth = new DateTime(1990, 1, 1);

        // Act
        var personData = new PersonData(firstName, lastName, dateOfBirth, phone: phoneNumber);

        // Assert
        personData.Phone.Should().Be(phoneNumber);
    }
}