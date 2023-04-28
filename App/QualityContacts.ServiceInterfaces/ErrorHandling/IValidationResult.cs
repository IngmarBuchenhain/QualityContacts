using System;
namespace QualityContacts.ServiceInterfaces.ErrorHandling
{
	public interface IValidationResult
	{
        bool IsValid { get; init; }
        bool hasWarnings { get; init; }
        List<ValidationError> validationErrors { get; init; }
        List<ValidationWarning> validationWarnings { get; init; }
    }
}

