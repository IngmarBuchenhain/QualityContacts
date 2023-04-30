using QualityContacts.ServiceInterfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityContacts.Services
{
    public class Contact : IContact
    {
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Gender { get; set; } = String.Empty;
        public string Salutation { get; set; } = String.Empty;
        public string Titles { get; set; } = String.Empty;
        public string LetterSalutation { get => $"Hallo {Salutation} {Titles} {FirstName} {LastName}"; set { } }
    }
}
