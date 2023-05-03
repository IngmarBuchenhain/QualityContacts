using QualityContacts.ServiceInterfaces.Models;

namespace QualityContacts.ServiceInterfaces.Services
{
    /// <summary>
    /// Provides methods for parsing a free input string with contact information to an <see cref="IContact"/>.
    /// </summary>
    public interface IContactParser
    {
        /// <summary>
        /// Parses the given input string for contact information.<br/>
        /// The returned <see cref="IContact"/> may not be valid for saving and is only the best splitting the parser can perform.
        /// </summary>
        /// <param name="contactCandidate">The free input containing contact information.</param>
        /// <returns><see cref="IContact"/> containing the information from the split free input <paramref name="contactCandidate"/>.</returns>
        IContact ParseContactFreeInput(string contactCandidate);
    }
}
