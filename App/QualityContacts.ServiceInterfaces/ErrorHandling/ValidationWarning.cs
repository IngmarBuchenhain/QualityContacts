namespace QualityContacts.ServiceInterfaces.ErrorHandling
{
    /// <summary>
    /// Typed warnings which can occur during validation. 
    /// </summary>
	public enum ValidationWarning
    {
        /// <summary>The input can be interpreted in different ways but is valid.</summary>
        Ambiguous,

        /// <summary>The input contains characters which are unusual in a contact but is valid.</summary>
        UnusualCharacters

        // TODO: Add more possible warnings.
    }
}

