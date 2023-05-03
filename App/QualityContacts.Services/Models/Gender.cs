using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityContacts.Services.Models
{
    internal class Gender
    {
        internal Gender()
        {
            _registeredGenders = new ContactRepository().GetRegisteredGenders();
        }

        private IEnumerable<string> _registeredGenders;

        internal bool ConformsToRegisteredGenders(string genderCandidate)
        {
            foreach(string registeredGender in _registeredGenders)
            {
                if (registeredGender.Equals(genderCandidate))
                {

                    
                    return true;
                }
            }

            return false;
        }

        internal string FindFirst(string[] contactPartsToSearch)
        {
            foreach(string contactPart in contactPartsToSearch)
            {
                var lowerContactPart = contactPart.ToLower();
                foreach (string registeredGender in _registeredGenders)
                {
                    if (lowerContactPart.Contains(registeredGender))
                    {


                        return lowerContactPart;
                    }
                }
                
            }

            return String.Empty;
        }
    }
}
