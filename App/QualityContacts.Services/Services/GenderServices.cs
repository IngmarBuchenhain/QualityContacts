using QualityContacts.ServiceInterfaces.Services;

namespace QualityContacts.Services
{
    /// <summary>
    /// Service for gender orientated methods.
    /// </summary>
    public class GenderServices
    {
        public GenderServices(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        // Access to registered genders.
        private readonly IContactRepository _contactRepository;

        /// <summary>
        /// Validates whether the given string conforms to a registered gender.
        /// </summary>
        public bool ConformsToRegisteredGenders(string genderCandidate)
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
