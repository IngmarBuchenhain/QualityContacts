using System;
using QualityContacts.ServiceInterfaces.ErrorHandling;

namespace QualityContacts.Services
{
    public class ValidationResult : IValidationResult
    {

        public ValidationResult(bool isValid, bool hasWarnings, List<ValidationError> errors, List<ValidationWarning> warnings)
        {
            this.IsValid = isValid;
            this.hasWarnings = hasWarnings;
            this.validationErrors = errors;
            this.validationWarnings = warnings;
        }

        public bool IsValid { get; init; }

        public bool hasWarnings { get; init; }

        public List<ValidationError> validationErrors { get; init; }

        public List<ValidationWarning> validationWarnings { get; init; }
    }
}

