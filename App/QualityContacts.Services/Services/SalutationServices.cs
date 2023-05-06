using QualityContacts.ServiceInterfaces.Models;
using QualityContacts.ServiceInterfaces.Services;

namespace QualityContacts.Services
{
    /// <summary>
    /// Service for salutation orientated methods.
    /// </summary>
    public class SalutationServices
    {
        public SalutationServices(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        // Access to registered genders.
        private readonly IContactRepository _contactRepository;

        /// <summary>
        /// Validates whether the given string conforms to a registered salutation.
        /// </summary>
        public bool ConformsToRegisteredSalutations(string salutationCandidate)
        {
            foreach (string registeredSalutation in _contactRepository.GetRegisteredSalutations())
            {
                if (registeredSalutation.Equals(salutationCandidate))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Generates the letter salutation for a given <see cref="IContact"/>.
        /// </summary>
        /// <param name="contact">The contact for which the salutation should be generated.</param>
        /// <returns>The letter salutation.</returns>
        public string GenerateLetterSalutation(IContact contact)
        {
            // Type language and gender for the contact.
            Language language = _contactRepository.GetLanguageForSalutation(contact.Salutation);
            Gender gender = _contactRepository.GetTypedGenderForGender(contact.Gender);

            return gender switch
            {
                Gender.Male => _contactRepository.GetMaleLetterSalutation(language) + GenerateMainPartOfLetterSalutation(gender, contact.Titles, contact.FirstAndMiddleName, contact.LastName),
                Gender.Female => _contactRepository.GetFemaleLetterSalutation(language) + GenerateMainPartOfLetterSalutation(gender, contact.Titles, contact.FirstAndMiddleName, contact.LastName),
                Gender.Diverse => _contactRepository.GetDiverseLetterSalutation(language) + GenerateMainPartOfLetterSalutation(gender, contact.Titles, contact.FirstAndMiddleName, contact.LastName),
                Gender.None => _contactRepository.GetDefaultLetterSalutation(language) + GenerateMainPartOfLetterSalutation(gender, contact.Titles, contact.FirstAndMiddleName, contact.LastName),
                _ => throw new InvalidOperationException("No letter salutation can be generated for an invalid contact!"),
            };
        }

        private string GenerateMainPartOfLetterSalutation(Gender gender, string titles, string firstAndMiddleNames, string lastName)
        {
            return gender switch
            {
                Gender.Male => GenerateTitlePartOfLetterSalutation(titles) + lastName,
                Gender.Female => GenerateTitlePartOfLetterSalutation(titles) + lastName,
                Gender.Diverse => GenerateTitlePartOfLetterSalutation(titles) + GenerateFirstNamePartOfLetterSalutation(firstAndMiddleNames) + lastName,
                Gender.None => String.Empty,
                _ => throw new InvalidOperationException("No letter salutation can be generated for an invalid contact!"),
            };
        }

        private string GenerateTitlePartOfLetterSalutation(string titles)
        {
            var singleTitles = titles.Split(' ').Where(word => word.Length > 0);

            if (singleTitles.Any())
            {
                return " " + singleTitles.First();
            }

            return String.Empty;
        }

        private string GenerateFirstNamePartOfLetterSalutation(string firstAndMiddleNames)
        {
            if (String.IsNullOrEmpty(firstAndMiddleNames.Trim())) throw new InvalidOperationException("No letter salutation can be generated for an invalid contact!");

            return " " + firstAndMiddleNames.Split(' ').Where(word => word.Length > 0).First();
        }
    }
}

