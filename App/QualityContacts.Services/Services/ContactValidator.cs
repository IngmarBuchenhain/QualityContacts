using System;
using System.Text.RegularExpressions;
using QualityContacts.ServiceInterfaces.Models;
using QualityContacts.ServiceInterfaces.ErrorHandling;
using QualityContacts.ServiceInterfaces.Services;
using QualityContacts.Services.ErrorHandling;
using QualityContacts.Services.Models;

namespace QualityContacts.Services
{
    /// <summary>
    /// <inheritdoc cref="IContactValidator"/>
    /// </summary>
    public partial class ContactValidator : IContactValidator
    {
        /// <summary>
        /// Creates a new <see cref="ContactValidator"/> instance with a given repository.
        /// </summary>
        /// <param name="contactRepository">The repository to use for persistance.</param>
        public ContactValidator(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;

            _genderServices = new GenderServices(contactRepository);
            _salutationServices = new SalutationServices(contactRepository);
            _titleServices = new TitleServices(contactRepository);
        }

        private readonly IContactRepository _contactRepository;

        private readonly GenderServices _genderServices;

        private readonly SalutationServices _salutationServices;

        private readonly TitleServices _titleServices;

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
   
            var validationErrors = new HashSet<ValidationError>();
            var validationWarnings = new HashSet<ValidationWarning>();
            contact.Gender = contact.Gender.Trim();
            contact.Salutation = contact.Salutation.Trim();
            if (String.IsNullOrEmpty(contact.Gender))
            {
                contact.Gender = "ohne";
                validationWarnings.Add(ValidationWarning.GenderMissing);
            }
            if(!_genderServices.ConformsToRegisteredGenders(contact.Gender)){
      
                validationErrors.Add(ValidationError.GenderNotRegistered);
            }

            if(!_salutationServices.ConformsToRegisteredSalutations(contact.Salutation) && !String.IsNullOrEmpty(contact.Salutation))
            {
         
                validationErrors.Add(ValidationError.SalutationNotRegistered);
            }
            if (string.IsNullOrEmpty(contact.Salutation))
            {
                validationWarnings.Add(ValidationWarning.SalutationMissing);
            }

            // Check each property of the contact individually
            string validationNewTitles = _titleServices.ExtractPossibleNewAcademicTitles(contact.Titles);
            if (validationNewTitles.Length > 0)
            {
                validationWarnings.Add(ValidationWarning.TitleUnknown);
            }

            if (String.IsNullOrEmpty(contact.FirstAndMiddleName.Trim()))
            {
                validationErrors.Add(ValidationError.FirstNameMissing);
            
            }

            if (String.IsNullOrEmpty(contact.LastName.Trim()))
            {
                validationErrors.Add(ValidationError.LastNameMissing);
        
            }


            return new ValidationResult(validationErrors, validationWarnings, validationNewTitles);
        }




        [GeneratedRegex("^[a-zäöüñáéíóúàèìòù., -]*$", RegexOptions.IgnoreCase)]
        private static partial Regex CheckForSpecialCharacters();

    }
}

