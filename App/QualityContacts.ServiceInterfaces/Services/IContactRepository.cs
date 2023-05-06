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
    }
}
