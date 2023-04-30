using QualityContacts.ServiceInterfaces.Models;
using QualityContacts.ServiceInterfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityContacts.Services
{
    public class Repository : IRepository
    {
        public IContact GetNewContact()
        {
            return new Contact();
        }
    }
}
