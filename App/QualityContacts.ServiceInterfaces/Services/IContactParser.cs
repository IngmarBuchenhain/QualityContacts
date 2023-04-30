using QualityContacts.ServiceInterfaces.Models;

namespace QualityContacts.ServiceInterfaces.Services
{
    /// <summary>
    /// Provides methods for parsing a free input string with contact information to an <see cref="IContact"/>.
    /// </summary>
    public interface IContactParser
    {
        /// <summary>
        /// Parses the given input string for contact information.
        /// </summary>
        /// <param name="input">The contact informations.</param>
        /// <returns><see cref="IContact"/> containing the split input string.</returns>
        IContact ParseContactInput(string input);
    }
}
