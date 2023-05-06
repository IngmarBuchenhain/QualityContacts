namespace QualityContacts.ServiceInterfaces.ErrorHandling
{
    /// <summary>
    /// Typed warnings which can occur during validation of a contact or free contact input string. 
    /// </summary>
	public enum ValidationWarning
    {
        /// <summary>The contact can be interpreted in different ways but is valid.</summary>
        Ambiguous,

        /// <summary>The contact contains characters which are unusual in a contact but is valid.</summary>
        UnusualCharacters,

        /// <summary>The contact is missing a specific gender but is valid.</summary>
        GenderMissing,

        /// <summary>The contact input string has not enough words to be parsed into a valid contact.</summary>
        Incomplete,

        /// <summary>The contact has no salutation but is valid.</summary>
        SalutationMissing,

        /// <summary>The contact contains unknown titles but is valid.</summary>
        TitleUnknown
    }
}

