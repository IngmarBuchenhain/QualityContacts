using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QualityContacts.ServiceInterfaces.Models;

namespace QualityContacts.ServiceInterfaces.Services
{
    public interface IContactParser
    {
        public IContact Parse(string input);
        IContact ParseContactInput(string input);
    }
}
