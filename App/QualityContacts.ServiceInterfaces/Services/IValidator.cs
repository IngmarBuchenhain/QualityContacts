using QualityContacts.ServiceInterfaces.ErrorHandling;

namespace QualityContacts.ServiceInterfaces.Services
{
    public interface IValidator
    {
        public IValidationResult Validate(string input);
    }
}