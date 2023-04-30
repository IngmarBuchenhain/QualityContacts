using QualityContacts.ServiceInterfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityContacts.Services.Models
{
    public class Contact : IContact
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Salutation { get; set; } = string.Empty;
        public string Titles { get; set; } = string.Empty;
        public string LetterSalutation { get => $"Hallo {Salutation} {Titles} {FirstName} {LastName}"; set { } }
    }
}
