using QualityContacts.ServiceInterfaces.Models;
using QualityContacts.Services;

namespace QualityContacts.Tests
{
    public class ContactParserTest
    {

        [Theory]
        [InlineData("Mustermann")]
        [InlineData("Mustermann, Max")]
        [InlineData("Max Mustermann")]
        [InlineData("Herr Mustermann")]
        [InlineData("Herr Max Mustermann")]
        [InlineData("Dr. Mustermann")]
        [InlineData("Herr Dr. Max Mustermann")]
        [InlineData("Frau Prof. Dr. rer. Nat. Maria Mustermann")]
        [InlineData("Herr Dr.-Ing. Dr. rer. Nat. Dr. h.c. mult. Max Mustermann")]
        public void GetCorrectSurename(string input)
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput(input);

            Assert.Equal("Mustermann", newContact.LastName);
        }

        [Fact]
        public void GetCorrectSurenameWithVon()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Dr. von Mustermann");

            Assert.Equal("von Mustermann", newContact.LastName);
        }

        [Fact]
        public void GetCorrectSurenameWithZu()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Herr zu Mustermann");

            Assert.Equal("zu Mustermann", newContact.LastName);
        }

        [Fact]
        public void GetCorrectSurenameWithVonUndZu()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Herr von und zu Mustermann");

            Assert.Equal("von und zu Mustermann", newContact.LastName);
        }

        [Fact]
        public void GetCorrectDoubleName()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Herr Prof. Nr. rer. Nat. Max Mustermann-Schnarrenberger");

            Assert.Equal("Mustermann-Schnarrenberger", newContact.LastName);
        }

        [Theory]
        [InlineData("Max Mustermann")]
        [InlineData("Dr. Max Mustermann")]
        [InlineData("Max Dr. Mustermann")]
        [InlineData("Herr Max Mustermann")]
        [InlineData("Mustermann, Max")]
        public void GetCorrectFirstname(string input)
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput(input);

            Assert.Equal("Max", newContact.FirstAndMiddleName);
        }

        [Fact]
        public void GetCorrectFirstAndMiddlenames()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Max Heinrich Mustermann");

            Assert.Equal("Max Heinrich", newContact.FirstAndMiddleName);
        }

        [Fact]
        public void GetNoFirstname()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Mustermann");

            Assert.Equal("", newContact.FirstName);
        }

        [Fact]
        public void GetMaleSalutationGerman()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Herr Max Mustermann");

            Assert.Equal("Herr", newContact.Salutation);
        }

        [Fact]
        public void GetFemaleSalutationGerman()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Frau Maria Musterfrau");

            Assert.Equal("Frau", newContact.Salutation);
        }

        [Fact]
        public void GetMaleSalutationEnglish()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Mr Max Mustermann");

            Assert.Equal("Mr", newContact.Salutation);
        }

        [Fact]
        public void GetFemaleSalutationEnglish()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Mrs Maria Musterfrau");

            Assert.Equal("Mrs", newContact.Salutation);
        }

        [Fact]
        public void GetMaleSalutationSpanish()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Signor Max Mustermann");

            Assert.Equal("Signor", newContact.Salutation);
        }

        [Fact]
        public void GetFemaleSalutationSpanish()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Signora Maria Musterfrau");

            Assert.Equal("Signora", newContact.Salutation);
        }

        [Fact]
        public void GetMaleSalutationFrench()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("M Max Mustermann");

            Assert.Equal("M", newContact.Salutation);
        }

        [Fact]
        public void GetFemaleSalutationFrench()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Mme Maria Musterfrau");

            Assert.Equal("Mme", newContact.Salutation);
        }

        // ### Gender Tests ###

        [Fact]
        public void GetCorrectDrTitle()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Dr. Musterfrau");

            Assert.Equal("Dr.", newContact.Titles);
        }

        [Fact]
        public void GetCorrectProfessorTitle()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Professor Mustermann");

            Assert.Equal("Professor", newContact.Titles);
        }

        [Fact]
        public void GetCorrectProfessorinTitle()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Professorin Musterfrau");

            Assert.Equal("Professorin", newContact.Titles);
        }

        [Fact]
        public void GetCorrectProfTitle()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Prof. Mustermann");

            Assert.Equal("Prof.", newContact.Titles);
        }

        [Fact]
        public void GetCorrectDrIngTitle()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Dr.-Ing. Musterfrau");

            Assert.Equal("Dr.-Ing.", newContact.Titles);
        }

        [Fact]
        public void GetCorrectHCTitle()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("h.c. Musterfrau");

            Assert.Equal("h.c.", newContact.Titles);
        }

        [Fact]
        public void GetCorrectMultTitle()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("mult. Musterfrau");

            Assert.Equal("mult.", newContact.Titles);
        }

        [Fact]
        public void GetCorrectRerTitle()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("rer. Musterfrau");

            Assert.Equal("rer.", newContact.Titles);
        }

        [Fact]
        public void GetCorrectNatTitle()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("nat. Musterfrau");

            Assert.Equal("nat.", newContact.Titles);
        }

        [Fact]
        public void GetCorrectDiplIngTitle()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Dipl.-Ing. Mustermann");

            Assert.Equal("Dipl.-Ing.", newContact.Titles);
        }

        [Fact]
        public void GetCorrectDiplTitle()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Dipl. Mustermann");

            Assert.Equal("Dipl.", newContact.Titles);
        }

        [Fact]
        public void GetCorrectIngTitle()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Ing. Musterfrau");

            Assert.Equal("Ing.", newContact.Titles);
        }

        [Fact]
        public void GetCorrectBSTitle()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("B.S. Mustermann");

            Assert.Equal("B.S.", newContact.Titles);
        }

        [Fact]
        public void GetCorrectBATitle()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("B.A. Mustermann");

            Assert.Equal("B.A.", newContact.Titles);
        }

        [Fact]
        public void GetCorrectMSTitle()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("M.S. Musterfrau");

            Assert.Equal("M.S.", newContact.Titles);
        }

        [Fact]
        public void GetCorrectMATitle()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("M.A. Musterfrau");

            Assert.Equal("M.A.", newContact.Titles);
        }

        // ### Gender Tests ###

        [Fact]
        public void GetCorrectMaleGender()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Herr Max Mustermann");

            Assert.Equal("mänlich", newContact.Gender);
        }

        [Fact]
        public void GetCorrectFemaleGender()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Frau Maria Musterfrau");

            Assert.Equal("weiblich", newContact.Gender);
        }

        [Fact]
        public void GetCorrectDiverseGender()
        {
            ContactParser contactParser = new ContactParser(new ContactRepository());

            IContact newContact = contactParser.ParseContactFreeInput("Max Mustermann");

            Assert.Equal("diverse", newContact.Gender);
        }
    }
}
