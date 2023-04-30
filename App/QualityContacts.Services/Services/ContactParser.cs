using QualityContacts.ServiceInterfaces.Models;
using QualityContacts.ServiceInterfaces.Services;
using QualityContacts.Services.Models;

namespace QualityContacts.Services
{
    public class ContactParser : IContactParser
    {


        public IContact ParseContactInput(string input)
        {
            return new Contact();
        }
    }
}
