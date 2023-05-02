using System;
using QualityContacts.ServiceInterfaces.ErrorHandling;

namespace QualityContacts.Services.ErrorHandling
{
    public class ValidationResult : IValidationResult
    {

        public ValidationResult(bool isValid, bool hasWarnings, IEnumerable<ValidationError> errors, IEnumerable<ValidationWarning> warnings)
        {
            IsValid = isValid;
            HasWarnings = hasWarnings;
            ValidationErrors = errors;
            ValidationWarnings = warnings;
        }

        public bool IsValid { get; init; }

        public bool HasWarnings { get; init; }

        public IEnumerable<ValidationError> ValidationErrors { get; init; }

        public IEnumerable<ValidationWarning> ValidationWarnings { get; init; }
    }
}

