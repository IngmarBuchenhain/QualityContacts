using System;
using QualityContacts.ServiceInterfaces.ErrorHandling;

namespace QualityContacts.Services
{
    public class ValidationResult : IValidationResult
    {

        public ValidationResult(bool isValid, bool hasWarnings, List<ValidationError> errors, List<ValidationWarning> warnings)
        {
            this.IsValid = isValid;
            this.HasWarnings = hasWarnings;
            this.ValidationErrors = errors;
            this.ValidationWarnings = warnings;
        }

        public bool IsValid { get; init; }

        public bool HasWarnings { get; init; }

        public IEnumerable<ValidationError> ValidationErrors { get; init; }

        public IEnumerable<ValidationWarning> ValidationWarnings { get; init; }
    }
}

