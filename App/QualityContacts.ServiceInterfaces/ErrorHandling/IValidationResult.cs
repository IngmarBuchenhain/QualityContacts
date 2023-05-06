namespace QualityContacts.ServiceInterfaces.ErrorHandling
{
    /// <summary>
    /// Container for validation results, containing not only whether the validation was successful, but also which errors/warnings occured.
    /// </summary>
    public interface IValidationResult
    {
        /// <summary>
        /// The validation was successful if <see langword="true"/>, otherwise not.
        /// </summary>
        bool IsValid { get;  }

        /// <summary>
        /// The validation may be successful, but minor warnings occured.
        /// </summary>
        bool HasWarnings { get; }

        /// <summary>
        /// If unknown academic titles were found, they can be found here.
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

