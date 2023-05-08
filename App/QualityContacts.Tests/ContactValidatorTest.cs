using QualityContacts.ServiceInterfaces.ErrorHandling;
using QualityContacts.Services;
using QualityContacts.Services.Models;

namespace QualityContacts.Tests
{
    public class ContactValidatorTest
    {

        [Fact]
        public void HasWarningsAtSemikolonInput()
        {
            ContactValidator contactValidator = new ContactValidator(new ContactRepository());

            IValidationResult result = contactValidator.Validate("Mustermann; Max");

            Assert.True(result.HasWarnings);
        }

        [Fact]
        public void HasWarningsAccordingToMissingGender()
        {
            Contact contact = new Contact();
            contact.FirstAndMiddleName = "Max";
            contact.LastName = "Mustermann";
            contact.Gender = "";
            contact.Salutation = "Herr";
            contact.Titles = "Dr.";

            ContactValidator contactValidator = new ContactValidator(new ContactRepository());

            IValidationResult result = contactValidator.Validate(contact);

            Assert.True(result.HasWarnings);
        }

        [Fact]
        public void HasWarningsAccordingToMissingSalutation()
        {
            Contact contact = new Contact();
            contact.FirstAndMiddleName = "Max";
            contact.LastName = "Mustermann";
            contact.Gender = "m�nnlich";
            contact.Titles = "Dr.";

            ContactValidator contactValidator = new ContactValidator(new ContactRepository());

            IValidationResult result = contactValidator.Validate(contact);

            Assert.True(result.HasWarnings);
        }

        [Fact]
        public void IsNotValidAccordingToMissingFirstName()
        {
            Contact contact = new Contact();
            contact.LastName = "Mustermann";
            contact.Gender = "m�nnlich";
            contact.Salutation = "Herr";
            contact.Titles = "Dr.";

            ContactValidator contactValidator = new ContactValidator(new ContactRepository());

            IValidationResult result = contactValidator.Validate(contact);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void IsNotValidAccordingToMissingLastName()
        {
            Contact contact = new Contact();
            contact.FirstAndMiddleName = "Max";
            contact.Gender = "m�nnlich";
            contact.Salutation = "Herr";
            contact.Titles = "Dr.";

            ContactValidator contactValidator = new ContactValidator(new ContactRepository());

            IValidationResult result = contactValidator.Validate(contact);

            Assert.False(result.IsValid);
        }
    }
}
