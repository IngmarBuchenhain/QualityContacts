using System;
using System.Text.RegularExpressions;
using QualityContacts.ServiceInterfaces.Models;
using QualityContacts.ServiceInterfaces.ErrorHandling;
using QualityContacts.ServiceInterfaces.Services;
using QualityContacts.Services.ErrorHandling;

namespace QualityContacts.Services
{
    public class ContactValidator : IContactValidator
    {
        public ContactValidator()
        {
        }



        public IValidationResult Validate(string input)
        {
            Console.WriteLine("Not implemented");
            return null;
        }

        public IValidationResult Validate(IContact contact)
        {
            throw new NotImplementedException();
        }
    }
}

