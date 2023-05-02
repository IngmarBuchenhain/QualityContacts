using System;
using System.Text.RegularExpressions;
using QualityContacts.ServiceInterfaces.Models;
using QualityContacts.ServiceInterfaces.ErrorHandling;
using QualityContacts.ServiceInterfaces.Services;
using QualityContacts.Services.ErrorHandling;

namespace QualityContacts.Services
{
    public partial class ContactValidator : IContactValidator
    {
        public ContactValidator()
        {
        }



        public IValidationResult Validate(string input)
        {
            HashSet<ValidationWarning> validationWarnings = new HashSet<ValidationWarning>();

            if (!CheckForSpecialCharacters().IsMatch(input))
            {
                validationWarnings.Add(ValidationWarning.UnusualCharacters);
            }


            Console.WriteLine("Not implemented");
            return null;
        }

        public IValidationResult Validate(IContact contact)
        {
            var validationErrors = new HashSet<ValidationError>();

            // Check each property of the contact individually

            if (String.IsNullOrEmpty(contact.FirstName))
            {
                validationErrors.Add(ValidationError.FirstNameMissing);
            }

            if (String.IsNullOrEmpty(contact.LastName))
            {
                validationErrors.Add(ValidationError.LastNameMissing);
            }
            if (String.IsNullOrEmpty(contact.Gender))
            {
                validationErrors.Add(ValidationError.GenderMissing);
            }

            throw new NotImplementedException();
        }

        [GeneratedRegex("^[a-zA-Z. -]*$")]
        private static partial Regex CheckForSpecialCharacters();
    }
}

