using System;
namespace QualityContacts.Services.Models
{
	internal class Salutation
	{
        internal Salutation()
        {
            _registeredSalutations = new ContactRepository().GetRegisteredGenders();
        }

        private IEnumerable<string> _registeredSalutations;

        internal bool ConformsToRegisteredSalutations(string salutationCandidate)
        {
            foreach (string registeredSalutation in _registeredSalutations)
            {
                if (registeredSalutation.Equals(salutationCandidate))
                {


                    return true;
                }
            }

            return false;
        }

        internal string FindFirst(string[] contactPartsToSearch)
        {
            foreach (string contactPart in contactPartsToSearch)
            {
                var lowerContactPart = contactPart.ToLower();
                foreach (string registeredSalutation in _registeredSalutations)
                {
                    if (lowerContactPart.Contains(registeredSalutation.ToLower()))
                    {


                        return contactPart;
                    }
                }

            }

            return String.Empty;
        }

        
    }
}

