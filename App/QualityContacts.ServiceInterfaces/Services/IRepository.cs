using QualityContacts.ServiceInterfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityContacts.ServiceInterfaces.Services
{
    public interface IRepository
    {
        IContact GetNewContact();
    }
}
