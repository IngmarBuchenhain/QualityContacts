using QualityContacts.ServiceInterfaces.Models;
using QualityContacts.ServiceInterfaces.Services;
using QualityContacts.Services.Models;

namespace QualityContacts.Services
{
    /// <summary>
    /// <inheritdoc cref="IContactParser"/><br/>
    /// Implementation of <see cref="IContactParser"/>.
    /// </summary>
    public class ContactParser : IContactParser
    {
        #region Members and Constructors

        /// <summary>
        /// Creates a new <see cref="ContactParser"/> instance with a given repository.
        /// </summary>
        /// <param name="contactRepository">The repository to use for persistance.</param>
        public ContactParser(IContactRepository contactRepository)
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


        #endregion Members and Constructors

        #region Public Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="contactCandidate"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public IContact ParseContactFreeInput(string contactCandidate)
        {
            // Initialize result
            var newContact = new Contact();

            // If no input is given, an empty contact is returned, which the user has to complete.
            if (String.IsNullOrEmpty(contactCandidate)) return newContact;

            // Extract words from free input string. Assume words are separated by spaces.
            string[] contactParts = contactCandidate.Split(' ').Where(word => !string.IsNullOrEmpty(word)).ToArray();

            // Case the input string only contained spaces.
            if (contactParts.Length == 0) return newContact;

            // Iterate through all words, beginning with first and apply priorised rules
            int numberOfWords = contactParts.Length;

            // Indicator whether we found the last name before other names (ending with ',')
            bool lastNameAlreadyFound = false;

            // Academic titles collection, which can be sorted at the end.
            List<string> titles = new List<string>();

            for (int wordIndex = 0; wordIndex < numberOfWords; wordIndex++)
            {
                // Get current word
                string currentWord = contactParts[wordIndex];

                // RULE: If it is the last word, it is the last name, except we already found an inverted last name.
                if (wordIndex == numberOfWords - 1 && !lastNameAlreadyFound)
                {
                    // Append to last name, add space if needed
                    if (String.IsNullOrEmpty(newContact.LastName))
                    {
                        newContact.LastName += currentWord;
                    }
                    else
                    {
                        newContact.LastName += " " + currentWord;
                    }

                }
                // RULE: If it is NOT the last word or the last name is already found, check for all other contact parts.
                else
                {
                    // RULE: If word ends with ',' assume this is the last name, if we did
                    if (!lastNameAlreadyFound && currentWord.EndsWith(','))
                    {
                        lastNameAlreadyFound = true;
                        newContact.LastName = currentWord[..^1];
                        continue;
                    }

                    // RULE: If no salutation found yet, check for it
                    if (String.IsNullOrEmpty(newContact.Salutation))
                    {
                        foreach (string salutation in _contactRepository.GetRegisteredSalutations())
                        {
                            var trimmedCurrentWord = currentWord.Substring(0, currentWord.Length - 1) + currentWord.Substring(currentWord.Length - 1).Replace(".", "");
                            if (trimmedCurrentWord.ToLower().Equals(salutation.ToLower()) || (trimmedCurrentWord.ToLower() + ".").Equals(salutation.ToLower()))
                            {
                                newContact.Salutation = salutation;
                                break;
                            }
                        }

                        // If a salutation could be extracted, determine the gender.
                        if (!String.IsNullOrEmpty(newContact.Salutation))
                        {
                            newContact.Gender = _contactRepository.GetRegisteredGenderForSalutation(newContact.Salutation);
                            continue;
                        }
                    }

                    // RULE: Check for academic titles if no last name and salutation were found.
                    bool foundAcademicTitle = false;

                    foreach (string academicTitle in _contactRepository.GetTitles())
                    {
                        if (currentWord.ToLower().Equals(academicTitle.ToLower()) || (currentWord.ToLower() + ".").Equals(academicTitle.ToLower()))
                        {
                            foundAcademicTitle = true;

                            titles.Add(academicTitle);

                            break;
                        }
                    }

                    if (foundAcademicTitle) continue;

                    // RULE: If no academic title, check for noble titles
                    bool foundNobleTitle = false;

                    foreach (string nobleTitle in _contactRepository.GetRegisteredNobleTitles())
                    {
                        if (currentWord.ToLower().Equals(nobleTitle.ToLower()))
                        {
                            foundNobleTitle = true;
                            if (String.IsNullOrEmpty(newContact.LastName))
                            {
                                newContact.LastName = nobleTitle;
                            }
                            else
                            {
                                newContact.LastName += " " + nobleTitle;
                            }
                            break;
                        }
                    }

                    if (foundNobleTitle) continue;

                    bool foundNoblePreSuffix = false;

                    foreach (string noblePreSuffix in _contactRepository.GetRegisteredNoblePreSuffixes())
                    {
                        if (currentWord.ToLower().Equals(noblePreSuffix.ToLower()))
                        {
                            foundNoblePreSuffix = true;
                            if (String.IsNullOrEmpty(newContact.LastName))
                            {
                                newContact.LastName = noblePreSuffix;
                            }
                            else
                            {
                                newContact.LastName += " " + noblePreSuffix;
                            }

                            for (int wordIndexFromPreSuffix = wordIndex + 1; wordIndexFromPreSuffix < numberOfWords; wordIndexFromPreSuffix++)
                            {
                                newContact.LastName += " " + contactParts[wordIndexFromPreSuffix];
                            }

                            break;
                        }
                    }

                    if (foundNoblePreSuffix) break;

                    // RULE: If no other rule applies, assume it to be a first or middle name.
                    if (String.IsNullOrEmpty(newContact.FirstAndMiddleName))
                    {
                        newContact.FirstAndMiddleName = currentWord;
                    }
                    else
                    {
                        newContact.FirstAndMiddleName += " " + currentWord;
                    }
                }
            }

            // Sort academic titles
            newContact.Titles = GenerateSortedAcademicTitles(titles);

            // Generate letter salutation
            try
            {
                newContact.LetterSalutation = _salutationServices.GenerateLetterSalutation(newContact);
            }
            catch (Exception)
            {
                newContact.LetterSalutation = "Es konnte keine Briefanrede generiert werden!";
            }

            return newContact;
        }

        #endregion Public Methods

        #region Private Methods

        private string GenerateSortedAcademicTitles(List<string> academicTitles)
        {
            if (academicTitles == null || academicTitles.Count == 0) return string.Empty;

            string sortedTitles = String.Empty;

            foreach (string title in _contactRepository.GetTitles())
            {
                var mostImportantTitles = academicTitles.FindAll(word => word.Equals(title));
                if (mostImportantTitles.Any())
                {
                    academicTitles.RemoveAll(word => word.Equals(title));

                    foreach (var foundTitle in mostImportantTitles)
                    {
                        sortedTitles += foundTitle + " ";
                    }
                }
            }

            return sortedTitles.Trim();
        }

        #endregion Private Methods       
    }
}
