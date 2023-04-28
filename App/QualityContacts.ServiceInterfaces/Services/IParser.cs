using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QualityContacts.ServiceInterfaces.Models;

namespace QualityContacts.ServiceInterfaces.Services
{
    public interface IParser
    {
        public IContact Parse(string input);
    }
}
