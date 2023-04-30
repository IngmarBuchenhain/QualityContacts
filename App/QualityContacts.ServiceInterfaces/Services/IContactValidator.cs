using QualityContacts.ServiceInterfaces.ErrorHandling;
using QualityContacts.ServiceInterfaces.Models;

namespace QualityContacts.ServiceInterfaces.Services
{
    /// <summary>
    /// Provides methods for validating contacts in free or structured form.
    /// </summary>
    public interface IContactValidator
    {
        /// <summary>
        /// Validate whether a string of data can be splitted into an <see cref="IContact"/>.
        /// </summary>
        /// <param name="possibleContact">Contact information.</param>
        public IValidationResult Validate(string possibleContact);

        /// <summary>
        /// Validate whether an <see cref="IContact"/> is valid.
        /// </summary>
        /// <param name="contact">Contact to validate.</param>
        public IValidationResult Validate(IContact contact);
    }
}