﻿namespace QualityContacts.ServiceInterfaces.ErrorHandling
{
    /// <summary>
    /// Typed warnings which can occur during validation.
    /// </summary>
	public enum ValidationWarning
    {
        /// <summary>The input can be interpreted in different ways but is valid.</summary>
        Ambiguous

        // TODO: Add more possible warnings.
    }
}

