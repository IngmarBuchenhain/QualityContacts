using System.ComponentModel;

namespace QualityContacts.ServiceInterfaces.Models
{
    /// <summary>
    /// Representation of a contact. Provides access to different parts, like first name or titles.
    /// </summary>
    public interface IContact : INotifyPropertyChanged
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Gender { get; set; }
        string Salutation { get; set; }
        string Titles { get; set; }
        string LetterSalutation { get; }
    }
}
