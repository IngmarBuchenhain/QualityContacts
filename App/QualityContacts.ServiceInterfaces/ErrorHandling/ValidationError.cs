namespace QualityContacts.ServiceInterfaces.ErrorHandling
{
    /// <summary>
    /// Typed errors which can occur during validation.
    /// </summary>
    public enum ValidationError
    {
        // TODO: Which errors can occur?


        FirstNameMissing,
        LastNameMissing,
        GenderNotRegistered,
        SalutationNotRegistered
    }
}

