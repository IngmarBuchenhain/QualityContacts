using System;
using QualityContacts.ServiceInterfaces.ErrorHandling;

namespace QualityContacts.Services.ErrorHandling
{
    public class ValidationResult : IValidationResult
    {

        public ValidationResult(bool isValid, bool hasWarnings, IEnumerable<ValidationError> errors, IEnumerable<ValidationWarning> warnings, string possibleNewTitles = "")
        {
            IsValid = isValid;
            HasWarnings = hasWarnings;
            ValidationErrors = errors;
            ValidationWarnings = warnings;
            PossibleNewTitle = possibleNewTitles;
        }

        public ValidationResult()
        {
            IsValid = true;
            HasWarnings = false;
            ValidationErrors = new List<ValidationError>();
            ValidationWarnings = new List<ValidationWarning>();
        }

        public bool IsValid { get; set; }

        public bool HasWarnings { get; set; }

        public IEnumerable<ValidationError> ValidationErrors { get; set; }

        public IEnumerable<ValidationWarning> ValidationWarnings { get; set; }

        public string PossibleNewTitle {get; set; }
    }
}

