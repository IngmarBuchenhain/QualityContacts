using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityContacts.ServiceInterfaces.Models
{
    public interface IContact
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Gender { get; set; }
        string Salutation { get; set; }
        string Titles { get; set; }
        string LetterSalutation { get; set; }
    }
}
