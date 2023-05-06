using QualityContacts.ServiceInterfaces.ErrorHandling;
using QualityContacts.ServiceInterfaces.Models;
using QualityContacts.ServiceInterfaces.Services;
using QualityContacts.Services.ErrorHandling;
using System.Text.RegularExpressions;

namespace QualityContacts.Services
{
    /// <summary>
    /// <inheritdoc cref="IContactValidator"/>
    /// </summary>
    public partial class ContactValidator : IContactValidator
    {
        #region Members and Constructors

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

        // Regex to check for unusual characters.
        [GeneratedRegex("^[a-zäöüñáéíóúàèìòù., -]*$", RegexOptions.IgnoreCase)]
        private static partial Regex ExpectedCharacters();

        #endregion Members and Constructors

        #region Public Methods

        public IValidationResult Validate(string input)
        {
            HashSet<ValidationWarning> validationWarnings = new HashSet<ValidationWarning>();

            // Check whether unusual characters are present, to warn the user.
            if (!ExpectedCharacters().IsMatch(input))
            {
                validationWarnings.Add(ValidationWarning.UnusualCharacters);
            }

            // Check whether to less words in input to split unambiguously.
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

            // Convenience to the user. Prepare for validation:
            contact.Gender = contact.Gender.Trim().ToLower();
            contact.Salutation = contact.Salutation.Trim();

            // Gender
            if (String.IsNullOrEmpty(contact.Gender))
            {
                contact.Gender = _contactRepository.GetDefaultGender();
                validationWarnings.Add(ValidationWarning.GenderMissing);
            }
            if (!_genderServices.ConformsToRegisteredGenders(contact.Gender))
            {
                validationErrors.Add(ValidationError.GenderNotRegistered);
            }

            // Salutation
            if (string.IsNullOrEmpty(contact.Salutation))
            {
                validationWarnings.Add(ValidationWarning.SalutationMissing);
            }
            else
            {
                contact.Salutation = contact.Salutation.First().ToString().ToUpper() + (contact.Salutation.Length > 1 ? contact.Salutation.Substring(1).ToLower() : String.Empty);
            }

            if (!_salutationServices.ConformsToRegisteredSalutations(contact.Salutation))
            {
                validationErrors.Add(ValidationError.SalutationNotRegistered);
            }

            // Titles
            string validationNewTitles = _titleServices.ExtractPossibleNewAcademicTitles(contact.Titles);

            if (validationNewTitles.Length > 0)
            {
                validationWarnings.Add(ValidationWarning.TitleUnknown);
            }

            // First and middle names
            if (String.IsNullOrEmpty(contact.FirstAndMiddleName.Trim()))
            {
                validationErrors.Add(ValidationError.FirstNameMissing);
            }

            // Last name
            if (String.IsNullOrEmpty(contact.LastName.Trim()))
            {
                validationErrors.Add(ValidationError.LastNameMissing);
            }

            return new ValidationResult(validationErrors, validationWarnings, validationNewTitles);
        }

        #endregion Public Methods
    }
}

