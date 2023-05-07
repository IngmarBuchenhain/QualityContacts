using QualityContacts.ServiceInterfaces;
using QualityContacts.ServiceInterfaces.ErrorHandling;
using QualityContacts.Services;

namespace QualityContacts.Tests
{
    public class Validator_ValidateShould
    {
        [Theory]
        [InlineData("")]
        [InlineData("01234")]
        [InlineData("0123456789101112131412")]
        [InlineData(null)]
        [InlineData("1asdf")]
        public void Validate_InputIsNullOrEmpty_ReturnFalse(string value)
        {
            var validator = new ContactValidator(new ContactRepository());

            IValidationResult result = validator.Validate(value);

            Assert.False(result.IsValid, $"{value} is no valid phone number input");
        }
    }
}


// Äquivalenzklassen:
// Eingabe ist: Null, Länge 0-5, Länge 15 - unendlich
// Eingabe ist Enthält Buchstaben