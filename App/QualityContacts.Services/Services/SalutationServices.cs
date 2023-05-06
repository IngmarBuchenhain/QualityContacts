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

            // TODO: IMPROVE
            switch (gender)
            {
                case Gender.Male:
                    switch (language)
                    {
                        case Language.German:
                            return "Sehr geehrter Herr";
                        case Language.English:
                            return "Dear Mr";
                        case Language.Italian:
                            return "Egregio Signor";
                        case Language.French:
                            return "Monsieur";
                        case Language.Spanish:
                            return "Estimado Señor";
                        default:
                            return "Sehr geehrter Herr";
                    }
                case Gender.Female:
                    switch (language)
                    {
                        case Language.German:
                            return "Sehr geehrte Frau";
                        case Language.English:
                            return "Dear Ms";
                        case Language.Italian:
                            return "Gentile Signora";
                        case Language.French:
                            return "Madame";
                        case Language.Spanish:
                            return "Estimada Señora";
                        default:
                            return "Sehr geehrter Frau";
                    }
                case Gender.Diverse:
                    switch (language)
                    {
                        case Language.German:
                            return "Hallo";
                        case Language.English:
                            return "Dear";
                        case Language.Italian:
                            return "Ciao";
                        case Language.French:
                            return "Bonjour";
                        case Language.Spanish:
                            return "Hola";
                        default:
                            return "Hallo";
                    }
                case Gender.None:
                    switch (language)
                    {
                        case Language.German:
                            return "Sehr geehrte Damen und Herren";
                        case Language.English:
                            return "Dear Sirs";
                        case Language.Italian:
                            return "Egregi Signori";
                        case Language.French:
                            return "Messieursdames";
                        case Language.Spanish:
                            return "Estimados Señores y Señoras";
                        default:
                            return "Sehr geehrte Damen und Herren";
                    }
                default:
                    return "Hallo";
            }
        }
    }
}

