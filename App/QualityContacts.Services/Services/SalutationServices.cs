using QualityContacts.ServiceInterfaces.Services;

namespace QualityContacts.Services
{
    /// <summary>
    /// Service for salutation orientated methods.
    /// </summary>
    public static class SalutationServices
    {
        // Access to registered genders.
        private static readonly IContactRepository _contactRepository = new ContactRepository();

        /// <summary>
        /// Validates whether the given string conforms to a registered salutation.
        /// </summary>
        public static bool ConformsToRegisteredSalutations(string salutationCandidate)
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
    }
}

