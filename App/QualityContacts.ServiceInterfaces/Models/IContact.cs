using System.ComponentModel;

namespace QualityContacts.ServiceInterfaces.Models
{
    /// <summary>
    /// Representation of a contact. Provides access to different parts, like first name or titles.
    /// </summary>
    public interface IContact : INotifyPropertyChanged
    {
        /// <summary>
        /// One or multiple first/middle names.
        /// </summary>
        string FirstAndMiddleName { get; set; }

        /// <summary>
        /// One Lastname or a lastname with noble titles.
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// The gender of the contact.
        /// </summary>
        string Gender { get; set; }

        /// <summary>
        /// The salutation of the contact.
        /// </summary>
        string Salutation { get; set; }

        /// <summary>
        /// The optional titles of the contact.
        /// </summary>
        string Titles { get; set; }

        /// <summary>
        /// The letter salutation of the contact.
        /// </summary>
        string LetterSalutation { get; set; }
    }
}
