using QualityContacts.ServiceInterfaces.ErrorHandling;

namespace QualityContacts.Services.ErrorHandling
{
    /// <summary>
    /// <inheritdoc cref="IValidationResult"/>
    /// </summary>
    public class ValidationResult : IValidationResult
    {
        /// <summary>
        /// Creates a new <see cref="ValidationResult"/> with information about the validation.
        /// </summary>
        /// <param name="errors">Errors, which occured during validation.</param>
        /// <param name="warnings">Warnings, which occured during validation.</param>
        /// <param name="possibleNewTitles">Titles, which are not in the database which occured during validation.</param>
        public ValidationResult(IEnumerable<ValidationError> errors, IEnumerable<ValidationWarning> warnings, string possibleNewTitles = "")
        {
            ValidationErrors = errors;
            ValidationWarnings = warnings;
            PossibleNewTitle = possibleNewTitles;
        }

        public bool IsValid { get => !ValidationErrors.Any(); }

        public bool HasWarnings { get => ValidationWarnings.Any(); }

        public IEnumerable<ValidationError> ValidationErrors { get; set; }

        public IEnumerable<ValidationWarning> ValidationWarnings { get; set; }

        public string PossibleNewTitle { get; set; }
    }
}

