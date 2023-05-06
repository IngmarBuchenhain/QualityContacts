using System;
using System.Text.RegularExpressions;
using QualityContacts.ServiceInterfaces.Models;
using QualityContacts.ServiceInterfaces.ErrorHandling;
using QualityContacts.ServiceInterfaces.Services;
using QualityContacts.Services.ErrorHandling;
using QualityContacts.Services.Models;

namespace QualityContacts.Services
{
    public partial class ContactValidator : IContactValidator
    {
        public ContactValidator()
        {
            _contactRepository = new ContactRepository();
        }

        private ContactRepository _contactRepository;

        public IValidationResult Validate(string input)
        {
            HashSet<ValidationWarning> validationWarnings = new HashSet<ValidationWarning>();

            if (!CheckForSpecialCharacters().IsMatch(input))
            {
                validationWarnings.Add(ValidationWarning.UnusualCharacters);
            }

            if (input.Split(' ').Where(word => !string.IsNullOrEmpty(word)).ToArray().Length < 3)
            {
                validationWarnings.Add(ValidationWarning.Incomplete);
            }

            
            return new ValidationResult(new List<ValidationError>(), validationWarnings);
        }

        public IValidationResult Validate(IContact contact)
        {
            bool isValid = true;
            var validationErrors = new HashSet<ValidationError>();
            var validationWarnings = new HashSet<ValidationWarning>();
            contact.Gender = contact.Gender.Trim();
            contact.Salutation = contact.Salutation.Trim();
            if (String.IsNullOrEmpty(contact.Gender))
            {
                contact.Gender = "ohne";
                validationWarnings.Add(ValidationWarning.GenderMissing);
            }
            if(!GenderServices.ConformsToRegisteredGenders(contact.Gender)){
                isValid = false;
                validationErrors.Add(ValidationError.GenderNotRegistered);
            }

            if(!SalutationServices.ConformsToRegisteredSalutations(contact.Salutation) && !String.IsNullOrEmpty(contact.Salutation))
            {
                isValid = false;
                validationErrors.Add(ValidationError.SalutationNotRegistered);
            }
            if (string.IsNullOrEmpty(contact.Salutation))
            {
                validationWarnings.Add(ValidationWarning.SalutationMissing);
            }

            // Check each property of the contact individually
            string validationNewTitles = TitleServices.ExtractPossibleNewAcademicTitles(contact.Titles);
            if (validationNewTitles.Length > 0)
            {
                validationWarnings.Add(ValidationWarning.TitleUnknown);
            }

            if (String.IsNullOrEmpty(contact.FirstAndMiddleName.Trim()))
            {
                validationErrors.Add(ValidationError.FirstNameMissing);
                isValid = false;
            }

            if (String.IsNullOrEmpty(contact.LastName.Trim()))
            {
                validationErrors.Add(ValidationError.LastNameMissing);
                isValid = false;
            }


            return new ValidationResult(validationErrors, validationWarnings, validationNewTitles);
        }




        [GeneratedRegex("^[a-zäöüñáéíóúàèìòù., -]*$", RegexOptions.IgnoreCase)]
        private static partial Regex CheckForSpecialCharacters();

    }
}

