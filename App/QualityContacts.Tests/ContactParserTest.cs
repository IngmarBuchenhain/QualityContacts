using QualityContacts.ServiceInterfaces.Models;
using QualityContacts.Services;

namespace QualityContacts.Tests
{
    public class ContactParserTest
    {

        // VF10
        [Theory]
        [InlineData("Mustermann")]
        [InlineData("Max Mustermann")]
        [InlineData("Herr Mustermann")]
        [InlineData("Herr Max Mustermann")]
        [InlineData("Dr. Mustermann")]
        [InlineData("Herr Dr. Max Mustermann")]
        [InlineData("Dr. von Mustermann")]
        [InlineData("Herr zu Mustermann")]
        [InlineData("Herr von und zu Mustermann")]
        [InlineData("Frau Prof. Dr. rer. Nat. Maria Mustermann")]
        [InlineData("Herr Dr.-Ing. Dr. rer. Nat. Dr. h.c. mult. Max Mustermann")]
        public void GetCorrectSurename(string input)
        {
            ContactParser contactParser = new ContactParser();

            IContact newContact = contactParser.ParseContactFreeInput(input);

            Assert.Equal("Mustermann", newContact.LastName);
        }

        // VF10.10
        [Fact]
        public void GetCorrectDoubleName()
        {
            ContactParser contactParser = new ContactParser();

            IContact newContact = contactParser.ParseContactFreeInput("Herr Prof. Nr. rer. Nat. Max Mustermann-Schnarrenberger");

            Assert.Equal("Mustermann-Schnarrenberger", newContact.LastName);
        }

        // VF20
        // TODO

        // VF30.2, VF30.3, VF30.5, VF30.6
        [Theory]
        [InlineData("Max Mustermann")]
        [InlineData("Dr. Max Mustermann")]
        [InlineData("Max Dr. Mustermann")]
        [InlineData("Herr Max Mustermann")]
        public void GetCorrectFirstname(string input)
        {
            ContactParser contactParser = new ContactParser();

            IContact newContact = contactParser.ParseContactFreeInput(input);

            Assert.Equal("Max", newContact.FirstName);
        }

        // VF30.4
        [Fact]
        public void GetCorrectFirstnames()
        {
            ContactParser contactParser = new ContactParser();

            IContact newContact = contactParser.ParseContactFreeInput("Max Heinrich Mustermann");

            Assert.Equal("Max", newContact.FirstName);
        }

        // VF30.1
        [Fact]
        public void GetNoFirstname()
        {
            ContactParser contactParser = new ContactParser();

            IContact newContact = contactParser.ParseContactFreeInput("Mustermann");

            Assert.Equal("", newContact.FirstName);
        }

        // VF40.1
        [Fact]
        public void GetMaleSalutation()
        {
            ContactParser contactParser = new ContactParser();

            IContact newContact = contactParser.ParseContactFreeInput("Herr Max Mustermann");

            Assert.Equal("Herr", newContact.Salutation);
        }

        // VF40.2
        [Fact]
        public void GetFemaleSalutation()
        {
            ContactParser contactParser = new ContactParser();

            IContact newContact = contactParser.ParseContactFreeInput("Frau Maria Musterfrau");

            Assert.Equal("Frau", newContact.Salutation);
        }

        // VF50.1
        [Fact]
        public void GetCorrectDrTitle()
        {
            ContactParser contactParser = new ContactParser();

            IContact newContact = contactParser.ParseContactFreeInput("Dr. Musterfrau");

            Assert.Equal("Dr.", newContact.Titles);
        }

        // VF50.2
        [Fact]
        public void GetCorrectProfessorTitle()
        {
            ContactParser contactParser = new ContactParser();

            IContact newContact = contactParser.ParseContactFreeInput("Professor Mustermann");

            Assert.Equal("Professor", newContact.Titles);
        }

        // VF50.3
        [Fact]
        public void GetCorrectProfessorinTitle()
        {
            ContactParser contactParser = new ContactParser();

            IContact newContact = contactParser.ParseContactFreeInput("Professorin Musterfrau");

            Assert.Equal("Professorin", newContact.Titles);
        }

        // VF50.4
        [Fact]
        public void GetCorrectProfTitle()
        {
            ContactParser contactParser = new ContactParser();

            IContact newContact = contactParser.ParseContactFreeInput("Prof. Mustermann");

            Assert.Equal("Prof.", newContact.Titles);
        }

        // VF50.5
        [Fact]
        public void GetCorrectDrIngTitle()
        {
            ContactParser contactParser = new ContactParser();

            IContact newContact = contactParser.ParseContactFreeInput("Dr.-Ing. Musterfrau");

            Assert.Equal("Dr.-Ing.", newContact.Titles);
        }

        // VF50.6
        [Fact]
        public void GetCorrectHCTitle()
        {
            ContactParser contactParser = new ContactParser();

            IContact newContact = contactParser.ParseContactFreeInput("h.c. Musterfrau");

            Assert.Equal("h.c.", newContact.Titles);
        }

        // VF50.7
        [Fact]
        public void GetCorrectMultTitle()
        {
            ContactParser contactParser = new ContactParser();

            IContact newContact = contactParser.ParseContactFreeInput("mult. Musterfrau");

            Assert.Equal("mult.", newContact.Titles);
        }

        // VF50.8
        [Fact]
        public void GetCorrectRerTitle()
        {
            ContactParser contactParser = new ContactParser();

            IContact newContact = contactParser.ParseContactFreeInput("rer. Musterfrau");

            Assert.Equal("rer.", newContact.Titles);
        }

        // VF50.9
        [Fact]
        public void GetCorrectNatTitle()
        {
            ContactParser contactParser = new ContactParser();

            IContact newContact = contactParser.ParseContactFreeInput("nat. Musterfrau");

            Assert.Equal("nat.", newContact.Titles);
        }

        // VF50.10
        [Fact]
        public void GetCorrectDiplTitle()
        {
            ContactParser contactParser = new ContactParser();

            IContact newContact = contactParser.ParseContactFreeInput("Dipl. Mustermann");

            Assert.Equal("Dipl.", newContact.Titles);
        }

        // VF50.11
        [Fact]
        public void GetCorrectIngTitle()
        {
            ContactParser contactParser = new ContactParser();

            IContact newContact = contactParser.ParseContactFreeInput("Ing. Musterfrau");

            Assert.Equal("Ing.", newContact.Titles);
        }

        // VF50.12
        [Fact]
        public void GetCorrectBSTitle()
        {
            ContactParser contactParser = new ContactParser();

            IContact newContact = contactParser.ParseContactFreeInput("B.S. Mustermann");

            Assert.Equal("B.S.", newContact.Titles);
        }

        // VF50.13
        [Fact]
        public void GetCorrectMSTitle()
        {
            ContactParser contactParser = new ContactParser();

            IContact newContact = contactParser.ParseContactFreeInput("M.S. Musterfrau");

            Assert.Equal("M.S.", newContact.Titles);
        }

        // VF60.1
        [Fact]
        public void GetCorrectMaleGender()
        {
            ContactParser contactParser = new ContactParser();

            IContact newContact = contactParser.ParseContactFreeInput("Herr Max Mustermann");

            Assert.Equal("mänlich", newContact.Gender);
        }

        // VF60.2
        [Fact]
        public void GetCorrectFemaleGender()
        {
            ContactParser contactParser = new ContactParser();

            IContact newContact = contactParser.ParseContactFreeInput("Frau Maria Musterfrau");

            Assert.Equal("weiblich", newContact.Gender);
        }

        // VF60.3
        [Fact]
        public void GetCorrectDiverseGender()
        {
            ContactParser contactParser = new ContactParser();

            IContact newContact = contactParser.ParseContactFreeInput("Max Mustermann");

            Assert.Equal("diverse", newContact.Gender);
        }
    }
}
