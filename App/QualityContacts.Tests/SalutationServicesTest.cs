using QualityContacts.Services;
using QualityContacts.Services.Models;

namespace QualityContacts.Tests
{
    public class SalutationServicesTest
    {

        [Fact]
        public void GenerateSalutationWithoutGender()
        {
            Contact contact = new Contact();
            contact.FirstAndMiddleName = "Max";
            contact.LastName = "Mustermann";
            contact.Gender = "";
            contact.Salutation = "Herr";
            contact.Titles = "Dr.";

            SalutationServices salutationServices = new SalutationServices(new ContactRepository());

            string result = salutationServices.GenerateLetterSalutation(contact);

            Assert.Equal("Sehr geehrte Damen und Herren", result);
        }

        [Fact]
        public void GenerateSalutationForDiverse()
        {
            Contact contact = new Contact();
            contact.FirstAndMiddleName = "Max";
            contact.LastName = "Mustermann";
            contact.Gender = "divers";
            contact.Salutation = "";
            contact.Titles = "Dr.";

            SalutationServices salutationServices = new SalutationServices(new ContactRepository());

            string result = salutationServices.GenerateLetterSalutation(contact);

            Assert.Equal("Hallo Dr. Max Mustermann", result);
        }
    }
}
