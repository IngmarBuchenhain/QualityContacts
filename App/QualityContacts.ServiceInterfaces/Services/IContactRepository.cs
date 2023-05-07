using QualityContacts.ServiceInterfaces.Models;
using System.Collections.ObjectModel;

namespace QualityContacts.ServiceInterfaces.Services
{
    /// <summary>
    /// Service for retrieving and storing data to the persistence store.
    /// </summary>
    /// <remarks>
    /// NOTE: During prototyping phase no saving to persistance store will be performed.
    /// </remarks>
    public interface IContactRepository
    {
        /// <summary>
        /// Creates an instance of <see cref="IContact"/>.
        /// </summary>
        /// <returns>An empty <see cref="IContact"/>.</returns>
        IContact GetNewContact();

        /// <summary>
        /// Save an <see cref="IContact"/> to the persistence store.
        /// </summary>
        /// <remarks>
        /// NOTE: During prototyping phase no saving to persistance store will be performed.
        /// </remarks>
        /// <param name="contact">The contact to save.</param>
        void SaveNewContact(IContact contact);

        /// <summary>
        /// Load all stored contacts.
        /// </summary>
        /// <remarks>
        /// NOTE: During prototyping phase no saving to persistance store will be performed.
        /// </remarks>
        ObservableCollection<IContact> GetContacts();

        /// <summary>
        /// Save a new title to the persistence store if not already present.
        /// </summary>
        /// <remarks>
        /// NOTE: During prototyping phase no saving to persistance store will be performed.
        /// </remarks>
        /// <param name="title">The new title to save.</param>
        void SaveNewTitle(string title);

        /// <summary>
        /// Load all stored titles.
        /// </summary>
        /// <remarks>
        /// NOTE: During prototyping phase no saving to persistance store will be performed.
        /// </remarks>
        HashSet<string> GetTitles();

        /// <summary>
        /// Load all registered salutations.
        /// </summary>
        public IEnumerable<string> GetRegisteredSalutations();

        /// <summary>
        /// Load all registered genders.
        /// </summary>
        public IEnumerable<string> GetRegisteredGenders();

        /// <summary>
        /// Returns the default gender.
        /// </summary>
        public string GetDefaultGender();

        /// <summary>
        /// Load all registered noble titles.
        /// </summary>
        public IEnumerable<string> GetRegisteredNobleTitles();

        /// <summary>
        /// Load all registered prefixes and suffixes.
        /// </summary>
        public IEnumerable<string> GetRegisteredNoblePreSuffixes();

        /// <summary>
        /// Returns the registered gender for the given salutation.
        /// </summary>
        /// <param name="salutation">A salutation for which the matching gender is needed.</param>
        public string GetRegisteredGenderForSalutation(string salutation);

        /// <summary>
        /// Returns the typed language for the given salutation.
        /// </summary>
        /// <param name="salutation">A salutation for which the matching language is needed.</param>
        public Language GetLanguageForSalutation(string salutation);

        /// <summary>
        /// Returns the typed gender for the given gender.
        /// </summary>
        /// <param name="gender">A gender for which the matching typed gender is needed.</param>
        public Gender GetTypedGenderForGender(string  gender);

        /// <summary>
        /// Returns the language specific letter salutation beginning for a male contact.
        /// </summary>
        public string GetMaleLetterSalutation(Language language);

        /// <summary>
        /// Returns the language specific letter salutation beginning for a female contact.
        /// </summary>
        public string GetFemaleLetterSalutation(Language language);

        /// <summary>
        /// Returns the language specific letter salutation beginning for a diverse contact.
        /// </summary>
        public string GetDiverseLetterSalutation(Language language);

        /// <summary>
        /// Returns the language specific letter salutation beginning for a contact without gender.
        /// </summary>
        public string GetDefaultLetterSalutation(Language language);
    }
}
