using System.Text.RegularExpressions;
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
            string[] contactParts = SplitIntoWords(contactCandidate, ' ');

            // Based on number of words in input, choose further parsing method.

            // Case the input string only contained spaces.
            if (contactParts.Length == 0) return newContact;

            // Special case with only one given word: We assume the given word is the last name!
            if (contactParts.Length == 1)
            {
                newContact.LastName = contactParts[0];
                return newContact;
            } else
            {
                // In case of more then one word, check whether one word ends with ',', in this case this is the last name, otherwise assume the last word is the last name.
                string possibleRevertedLastName;
                if(TryFindRevertedLastName(out possibleRevertedLastName, contactParts))
                {
                    newContact.LastName = possibleRevertedLastName;
                } else
                {
                    newContact.LastName = contactParts[contactParts.Length - 1];
                }

                // Get all titles
                

                return newContact;
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Loops through a given range of words and checks whether exactly one word ends with ',', if so it is returned as possible last name.
        /// </summary>
        /// <param name="extractedLastName">The possible last name.</param>
        /// <param name="contactWords">Collection of words to check.</param>
        /// <returns><see langword="true"/> if exactly one possible last name was found, otherwise <see langword="false"/>.</returns>
        private static bool TryFindRevertedLastName(out string extractedLastName, string[] contactWords)
        {
            bool foundUnambiguouslyLastName = false;
            extractedLastName = String.Empty;

            foreach (var word in contactWords)
            {
                if (word.Last().Equals(','))
                {
                    // If a last name already was found until now, reset and return ambiguousity.
                    if (foundUnambiguouslyLastName)
                    {
                        extractedLastName = String.Empty;
                        return false;
                    } else
                    {
                        extractedLastName = word.Substring(0, word.Length - 1);
                        foundUnambiguouslyLastName = true;
                    }                   
                }
            }

            return foundUnambiguouslyLastName;
        }

        /// <summary>
        /// Split the given string into 'words' by the given separator.<br/>
        /// </summary>
        /// <param name="freeInput">Some string which should be separated into single words.</param>
        /// <param name="separator"><see cref="Char"/> which separates words.</param>
        /// <returns><see cref="Array"/> containing the single words, empty words are removed.</returns>
        private static string[] SplitIntoWords(string freeInput, char separator)
        {
            var words = freeInput.Split(separator).Where(word => !string.IsNullOrEmpty(word)).ToArray();

            return words;
        }

        #endregion Private Methods
       
    }
}
