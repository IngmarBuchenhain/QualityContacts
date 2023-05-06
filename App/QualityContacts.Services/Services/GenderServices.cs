using QualityContacts.ServiceInterfaces.Services;

namespace QualityContacts.Services
{
    /// <summary>
    /// Service for gender orientated methods.
    /// </summary>
    public static class GenderServices
    {
        // Access to registered genders.
        private static readonly IContactRepository _contactRepository = new ContactRepository();

        /// <summary>
        /// Validates whether the given string conforms to a registered gender.
        /// </summary>
        public static bool ConformsToRegisteredGenders(string genderCandidate)
        {
            foreach (string registeredGender in _contactRepository.GetRegisteredGenders())
            {
                if (registeredGender.Equals(genderCandidate))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
