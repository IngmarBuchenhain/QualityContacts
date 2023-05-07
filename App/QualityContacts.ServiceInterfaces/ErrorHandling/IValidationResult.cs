namespace QualityContacts.ServiceInterfaces.ErrorHandling
{
    /// <summary>
    /// Container for validation results, containing not only whether the validation was successful, but also which errors/warnings occured.
    /// </summary>
    public interface IValidationResult
    {
        /// <summary>
        /// The validation has no errors, but may have warnings.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// The validation has warnings, but may be successful.
        /// </summary>
        bool HasWarnings { get; }

        /// <summary>
        /// If unknown academic titles were found, they are stored here. Otherwise an empty string.
        /// </summary>
        string PossibleNewTitle { get; }

        /// <summary>
        /// All errors which occured during validation.
        /// </summary>
        IEnumerable<ValidationError> ValidationErrors { get; }

        /// <summary>
        /// All warnings which occured during validation.
        /// </summary>
        IEnumerable<ValidationWarning> ValidationWarnings { get; }

    }
}

