using QualityContacts.ServiceInterfaces.ErrorHandling;
using QualityContacts.ServiceInterfaces.Models;

namespace QualityContacts.ServiceInterfaces.Services
{
    public interface IContactValidator
    {
        public IValidationResult Validate(string possibleContact);

        public IValidationResult Validate(IContact contact);
    }
}