using QualityContacts.ServiceInterfaces.Services;

namespace QualityContacts.Services
{
    /// <summary>
    /// Service for title orientated methods.
    /// </summary>
    public static class TitleServices
    {
        // Access to registered genders.
        private static readonly IContactRepository _contactRepository = new ContactRepository();

        /// <summary>
        /// Splits the given string by ' ' and returns all single words which does not conform to a registered academic title.
        /// </summary>
        /// <returns>A single string containing all unknown titles separated by ' '.</returns>
        public static string ExtractPossibleNewAcademicTitles(string titleCandidates)
        {
            var singleTitles = titleCandidates.Split(' ').Where(word => !string.IsNullOrEmpty(word)).ToArray();

            string possibleNewTitles = string.Empty;

            foreach (var title in singleTitles)
            {
                if (!ConformsToRegisteredAcademicTitles(title))
                {
                    if (possibleNewTitles.Length > 0)
                    {
                        possibleNewTitles += " " + title;
                    }
                    else
                    {
                        possibleNewTitles += title;
                    }
                }
            }
            return possibleNewTitles;
        }

        /// <summary>
        /// Validates whether the given string conforms to a registered academic title.
        /// </summary>
        public static bool ConformsToRegisteredAcademicTitles(string titleCandidate)
        {
            foreach (var title in _contactRepository.GetTitles())
            {
                if (title.Equals(titleCandidate))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
