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

            bool lastNameAlreadyFound = false;

            for (int wordIndex = 0; wordIndex < numberOfWords; wordIndex++)
            {
                // Get current word
                string currentWord = contactParts[wordIndex];

                // RULE: It it is the last word, it is the last name
                if (wordIndex == numberOfWords - 1 && !lastNameAlreadyFound)
                {

                    // Append to last name, add space if needed
                    bool alreadyLastNameExisting = newContact.LastName.Length > 0;
                    if (alreadyLastNameExisting)
                    {
                        newContact.LastName += " " + currentWord;
                    }
                    else
                    {
                        newContact.LastName += currentWord;
                    }

                }
                else if (wordIndex == 0)
                {
                    // RULE: If word ends with ',' assume this is the last name.
                    if (currentWord.EndsWith(','))
                    {
                        lastNameAlreadyFound = true;
                        newContact.LastName = currentWord.Substring(0, currentWord.Length - 1);
                        continue;
                    }

                    // RULE: The first word may be a salutation.
                    // Check this first



                    foreach (string salutation in _contactRepository.GetRegisteredSalutations())
                    {
                        if (currentWord.ToLower().Equals(salutation.ToLower()))
                        {
                            newContact.Salutation = currentWord;

                            break;
                        }
                    }

                    // If a salutation could be extracted, determine the gender.
                    if (!String.IsNullOrEmpty(newContact.Salutation))
                    {
                        newContact.Gender = new ContactRepository().GetRegisteredGenderForSalutation(newContact.Salutation);
                        continue;
                    }

                    // RULE: If no salutation check for academic title
                    // Check this second

                    foreach (string academicTitle in _contactRepository.GetTitles())
                    {
                        if (currentWord.ToLower().Equals(academicTitle.ToLower()) || (currentWord.ToLower() + ".").Equals(academicTitle.ToLower()))
                        {
                            newContact.Titles = academicTitle;

                            break;
                        }
                    }

                    if (!String.IsNullOrEmpty(newContact.Titles)) continue;

                    // RULE: If no academic title check for noble title
                    // Check this third

                    foreach (string nobleTitle in _contactRepository.GetRegisteredNobleTitles())
                    {
                        if (currentWord.ToLower().Equals(nobleTitle.ToLower()))
                        {
                            newContact.LastName = nobleTitle;
                            break;
                        }
                    }



                    if (!String.IsNullOrEmpty(newContact.LastName)) continue;

                    foreach (string noblePreSuffix in _contactRepository.GetRegisteredNoblePreSuffixes())
                    {
                        if (currentWord.ToLower().Equals(noblePreSuffix.ToLower()))
                        {
                            newContact.LastName = noblePreSuffix;
                            for (int wordIndexFromPreSuffix = wordIndex + 1; wordIndexFromPreSuffix < numberOfWords; wordIndexFromPreSuffix++)
                            {
                                newContact.LastName += " " + contactParts[wordIndexFromPreSuffix];
                            }

                            return newContact;
                        }
                    }



                    // RULE: If no applies, it should be a first name.
                    newContact.FirstAndMiddleName = currentWord;
                }
                else
                {
                    // RULE: Middle words may be titles or first/middle names
                    // RULE: If word ends with ',' assume this is the last name.
                    if (currentWord.EndsWith(','))
                    {
                        lastNameAlreadyFound = true;
                        newContact.LastName = currentWord.Substring(0, currentWord.Length - 1);
                        continue;
                    }

                    // RULE: The first word may be a salutation.
                    // Check this first


                    if (String.IsNullOrEmpty(newContact.Salutation))
                    {
                        foreach (string salutation in _contactRepository.GetRegisteredSalutations())
                        {
                            if (currentWord.ToLower().Equals(salutation.ToLower()))
                            {
                                newContact.Salutation = currentWord;

                                break;
                            }
                        }

                        // If a salutation could be extracted, determine the gender.
                        if (!String.IsNullOrEmpty(newContact.Salutation))
                        {
                            newContact.Gender = new ContactRepository().GetRegisteredGenderForSalutation(newContact.Salutation);
                            continue;
                        }
                    }

                    // Check for academic titles first
                    bool foundAcademicTitle = false;
                    foreach (string academicTitle in _contactRepository.GetTitles())
                    {
                        if (currentWord.ToLower().Equals(academicTitle.ToLower()) || (currentWord.ToLower() + ".").Equals(academicTitle.ToLower()))
                        {
                            foundAcademicTitle = true;
                            if (!String.IsNullOrEmpty(newContact.Titles))
                            {
                                newContact.Titles += " " + academicTitle;
                            }
                            else
                            {
                                newContact.Titles = academicTitle;
                            }


                            break;
                        }
                    }
                    if (foundAcademicTitle) continue;

                    // If no academic title, check for noble titles
                    bool foundNobleTitle = false;
                    foreach (string nobleTitle in _contactRepository.GetRegisteredNobleTitles())
                    {
                        if (currentWord.ToLower().Equals(nobleTitle.ToLower()))
                        {
                            foundNobleTitle = true;
                            if (!String.IsNullOrEmpty(newContact.LastName))
                            {
                                newContact.LastName += " " + nobleTitle;
                            }
                            else
                            {
                                newContact.LastName = nobleTitle;
                            }

                            break;
                        }
                    }



                    if (foundNobleTitle) continue;

                    foreach (string noblePreSuffix in _contactRepository.GetRegisteredNoblePreSuffixes())
                    {
                        if (currentWord.ToLower().Equals(noblePreSuffix.ToLower()))
                        {
                            if (!String.IsNullOrEmpty(newContact.LastName))
                            {
                                newContact.LastName += " " + noblePreSuffix;
                            }
                            else
                            {
                                newContact.LastName = noblePreSuffix;
                            }

                            for (int wordIndexFromPreSuffix = wordIndex + 1; wordIndexFromPreSuffix < numberOfWords; wordIndexFromPreSuffix++)
                            {
                                newContact.LastName += " " + contactParts[wordIndexFromPreSuffix];
                            }

                            return newContact;
                        }
                    }

                    // If no applies, it should be a first or middle name.
                    if (!String.IsNullOrEmpty(newContact.FirstAndMiddleName))
                    {
                        newContact.FirstAndMiddleName += " " + currentWord;
                    }
                    else
                    {
                        newContact.FirstAndMiddleName = currentWord;
                    }
                }



            }

            return newContact;


        }

        #endregion Public Methods

        #region Private Methods


        #endregion Private Methods       
    }
}
