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
        }



        public IValidationResult Validate(string input)
        {
            HashSet<ValidationWarning> validationWarnings = new HashSet<ValidationWarning>();

            if (!CheckForSpecialCharacters().IsMatch(input))
            {
                validationWarnings.Add(ValidationWarning.UnusualCharacters);
            }


            Console.WriteLine("Not implemented");
            return new ValidationResult(true, false, null, null);
        }

        public IValidationResult Validate(IContact contact)
        {
            bool isValid = true;
            var validationErrors = new HashSet<ValidationError>();

            // Check each property of the contact individually

            if (String.IsNullOrEmpty(contact.FirstName))
            {
                validationErrors.Add(ValidationError.FirstNameMissing);
                isValid = false;
            }

            if (String.IsNullOrEmpty(contact.LastName))
            {
                validationErrors.Add(ValidationError.LastNameMissing);
                isValid = false;
            }
            if (String.IsNullOrEmpty(contact.Gender))
            {
                validationErrors.Add(ValidationError.GenderMissing);
                isValid = false;
            }

            return new ValidationResult(isValid, false, validationErrors, null);
        }

        private ValidationResult ValidateFirstName(string firstName, ValidationResult? priorValidation = null)
        {
            if(priorValidation == null)
            {
                priorValidation = new ValidationResult();
            }
            if (String.IsNullOrEmpty(firstName))
            {
                priorValidation.ValidationErrors.Append(ValidationError.FirstNameMissing);
                priorValidation.IsValid = false;
                return priorValidation;
            }
            if (!CheckForSpecialCharacters().IsMatch(firstName))
            {
                priorValidation.ValidationWarnings.Append(ValidationWarning.UnusualCharacters);
                
            }
            return priorValidation;
        }

        private ValidationResult ValidateGender(string genderCandidate, ValidationResult? priorValidation = null)
        {
            if (priorValidation == null)
            {
                priorValidation = new ValidationResult();
            }
            if (String.IsNullOrEmpty(genderCandidate))
            {
                priorValidation.ValidationWarnings.Append(ValidationWarning.GenderMissing);
                priorValidation.HasWarnings = true;
                return priorValidation;
            }

            // Check for allowed genders
        }

        [GeneratedRegex("^[a-zA-Z. -]*$")]
        private static partial Regex CheckForSpecialCharacters();
    }
}

