namespace QualityContacts.ServiceInterfaces.ErrorHandling
{
    /// <summary>
    /// Typed errors which can occur during validation of a contact.
    /// </summary>
    public enum ValidationError
    {
        FirstNameMissing,

        LastNameMissing,

        GenderNotRegistered,

        SalutationNotRegistered
    }
}

